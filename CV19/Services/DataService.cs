using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19.Models;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Globalization;
using System.Windows;
using System.Threading;
//using CV19.Services.Interfaces;

namespace CV19.Services
{
    internal class DataService
    {
        private const string __DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
       
        /*Формируем поток байт*/
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient(); //создаем клиента
            var response = await client.GetAsync(
                __DataSourceAddress, 
                HttpCompletionOption.ResponseContentRead); //получаем ответ от удаленного сервера(читаем только заголовки, остальное на сет.карте)
            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false); //возращаем поток, который обеспечит чтение данных из сет.карты
        }


        /*Разбиваем поток на последовательность строк*/
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
            using var data_reader = new StreamReader(data_stream); //создаем объект для чтения строковых данных

            while (!data_reader.EndOfStream)//читаем данные пока не встретится конец потока
            {
                var line = data_reader.ReadLine(); //читаем строку
                if (string.IsNullOrWhiteSpace(line))
                    continue; //если строка не пуста -> продолжаем
                yield return line
                    .Replace("Korea,", "Korea -")        //"Korea, South" 
                    .Replace("Bonaire,", "Bonaire -")   //"Bonaire, Sint Eustatius and Saba"
                    .Replace("Helena,", "Helena -")     //"Saint Helena, Ascension and Tristan da Cunha"
                    ;     //TO DO убрать костыль и сделать парсинг по-человечески                        
            }
        }

        /*получение дат, для которых указаны данные*/
        private static DateTime[] GetDates() => GetDataLines()
          .First()    //первая строка
          .Split(',') //разделитель ","
          .Skip(4)    //пропускаем первые 4 колонки
          .Select(item => DateTime.Parse(item, CultureInfo.InvariantCulture)) //преобразуем строку в датаТайм
          .ToArray();

        /*получение информации о стране (название*/
        private static IEnumerable<(string Country, string Province,(double Lat, double Lon) Place, int[] Counts)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2], CultureInfo.InvariantCulture);
                var longitude = double.Parse(row[3], CultureInfo.InvariantCulture);
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country_name, province,(latitude, longitude), counts);
            }

        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();

            var data = GetCountriesData().GroupBy(c => c.Country);
            
            foreach (var country_info in data)
            {
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Counts, (date, counts)=> new ConfirmedCount
                        {
                            Date = date,
                            Count = counts
                        })
                    })
                };
                yield return country;
            }
        }
    }
}
