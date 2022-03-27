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
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()    //первая строка
            .Split(',') //разделитель ","
            .Skip(4)    //пропускаем первые 4 колонки
            .Select(item => DateTime.Parse(item, CultureInfo.InvariantCulture)) //преобразуем строку в датаТайм
            .ToArray();

        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var response = client.GetAsync(data_url).Result;

            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDates();
            Console.WriteLine(string.Join("\r\n",dates) );
        }
    }
}