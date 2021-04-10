using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace todaysWeatherForecastTest
{
    public class todaysWeatherForecastTest
    {
        public static int _Woeid { get; set; }
        private const string _city = "minsk";
        private const string _todaysDate = "/2021/4/10/";

        [SetUp]
        public async Task Setup()
        {
            ReadWoeid ReadWoeid = new ReadWoeid();
        }

        [Test]
        public async Task WeatherForecastTest()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/"+_Woeid+ _todaysDate);

            List<CitiesInfo> cityInfo = JsonConvert.DeserializeObject<List<CitiesInfo>>(response);

            foreach(var item in cityInfo)
            {
                Console.WriteLine("On the date: "+ item.created + " the weather was: " + item.weather_state_name
                    +"; mintemp: " + item.min_temp + "; maxtemp: " + item.max_temp + "; windSpeed: "+ item.wind_speed);
            }
        }

        class CitiesInfo
        {
            public string created { get; set; }
            public string weather_state_name { get; set; }
            public string min_temp { get; set; }
            public string max_temp { get; set; }
            public string wind_speed { get; set; }
          
        }

        public class ReadWoeid
        {
            public ReadWoeid()
            {
                string path = @"..\..\..\..\_Woeid.txt";
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string s = sr.ReadToEnd();
                        _Woeid = int.Parse(s);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
     