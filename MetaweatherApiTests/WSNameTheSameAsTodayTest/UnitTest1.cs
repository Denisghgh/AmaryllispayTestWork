using NUnit.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
/// <summary>
/// Ќа сайте https://www.metaweather.com/api дл€ минска сохранены данные по погоде только начина€ с 2017 года.
/// т.е. https://www.metaweather.com/api/location/834463/2016/4/10/ - не работает.
/// ѕоэтому дл€ выполнени€ этого задани€ € использовал 2017/4/10/ - вот эту дату.
/// </summary>
namespace WSNameTheSameAsTodayTest
{
    public class TeWSNameTheSameAsTodayTeststs
    {
        private const string _city = "minsk";
        private const string _Woeid = "834463";
        private const string _5yearsAgoDate = "/2017/4/10/";
        private const string _todaysDate = "/2021/4/10/";

        private bool isTestPassed { get; set; } = false;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task WeatherForecastTest()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/" + _Woeid + _todaysDate);
            List<CitiesInfo> cityInfo = JsonConvert.DeserializeObject<List<CitiesInfo>>(response);
            string _todaysWearher = cityInfo[0].weather_state_name;
            Console.WriteLine("_todaysWearher = " + _todaysWearher);

            response = await client.GetStringAsync("https://www.metaweather.com/api/location/" + _Woeid + _5yearsAgoDate);
            cityInfo = JsonConvert.DeserializeObject<List<CitiesInfo>>(response);
            foreach (var item in cityInfo)
            {
                if (item.weather_state_name == _todaysWearher)
                    isTestPassed = true;
            }
            AssertTest();
        }

        class CitiesInfo
        {
            public string weather_state_name { get; set; }
        }
      
        public void AssertTest()
        {
            Assert.True(isTestPassed, "the same weather conditions as today didnt exist 5 years ago");
        }
    }
}
