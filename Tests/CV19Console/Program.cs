using System;
using System.Net;
using System.Linq;
using System.Globalization;

namespace CV19Console
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        /*Формируем поток байт*/
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient(); //создаем клиента
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseContentRead); //получаем ответ от удаленного сервера(читаем только заголовки, остальное на сет.карте)
            return await response.Content.ReadAsStreamAsync(); //возращаем поток, который обеспечит чтение данных из сет.карты
        }

        /*Разбиваем поток на последовательность строк*/
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream); //создаем объект для чтения строковых данных

            while (!data_reader.EndOfStream)//читаем данные пока не встретится конец потока
            {
                var line = data_reader.ReadLine(); //читаем строку
                if (string.IsNullOrWhiteSpace(line))
                    continue; //если строка не пуста -> продолжаем
                yield return line
                    .Replace("Korea,","Korea -")        //"Korea, South" 
                    .Replace("Bonaire,", "Bonaire -")   //"Bonaire, Sint Eustatius and Saba"
                    .Replace("Helena,", "Helena -")     //"Saint Helena, Ascension and Tristan da Cunha"
                    ;     //TO DO убрать костыль и сделать парсинг по-человечески                        
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()    //первая строка
            .Split(',') //разделитель ","
            .Skip(4)    //пропускаем первые 4 колонки
            .Select(item => DateTime.Parse(item, CultureInfo.InvariantCulture)) //преобразуем строку в датаТайм
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));
                
            foreach(var row in lines)
            {
                //var province = row[0].Trim();
                //var country = row[1].Trim(' ','"');
                //var counts = row.Skip(4).Select(int.Parse).ToArray();

                //var province = row[1].Trim();
                //var country = row[2].Trim(' ','"');
                //var counts = row.Skip(5).Select(int.Parse).ToArray();
                //yield return (country, province,  counts);

                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country_name, province, counts);
            }

        }
        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var response = client.GetAsync(data_url).Result;

            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            //var dates = GetDates();
            //Console.WriteLine(string.Join("\r\n",dates) );


          
            var russia_data = GetData()
                .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date:dd/MM} - {count}")));
        }
    }
}