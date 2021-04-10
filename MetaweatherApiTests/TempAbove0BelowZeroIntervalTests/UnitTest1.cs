using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
/// <summary>
/// Я не понял, что значит "проверить длявсех дней в полученном прогнозе", 
/// поэтому проверил для середины лета, зимы, весны и осени
/// </summary>
namespace TempAbove0BelowZeroIntervalTests
{
    public class Tests
    {
        public static int _Woeid { get; set; }
        private const string _city = "minsk";
        private const string _summerDate = "/2020/7/14/";
        private const string _winterDate = "/2020/12/2/";
        private const string _autumnDate = "/2020/10/14/";
        private const string _springDate = "/2020/5/10/";
        private bool isTestPassed { get; set; } = true;


        [SetUp]
        public async Task Setup()
        {
            ReadWoeid ReadWoeid = new ReadWoeid();
            await WeatherForecastTest(_summerDate);
            await WeatherForecastTest(_winterDate);
            await WeatherForecastTest(_autumnDate);
            await WeatherForecastTest(_springDate);
        }

        public async Task WeatherForecastTest(string _todaysDate)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/" + _Woeid + _todaysDate);
            List<CitiesInfo> cityInfo = JsonConvert.DeserializeObject<List<CitiesInfo>>(response);
            switch (_todaysDate)
            {
                case "/2020/7/14/":                    
                    foreach (var item in cityInfo)
                    {
                        if (float.Parse(item.min_temp.Replace(".", ",")) < 0)
                            isTestPassed = false;
                    } break;
                case "/2020/12/2/":                
                    foreach (var item in cityInfo)
                    {           
                        if (float.Parse(item.min_temp.Replace(".", ",")) > 0)
                            isTestPassed = false;
                    } break;
                default:
                    foreach (var item in cityInfo)
                    {                        
                        if (((float.Parse(item.min_temp.Replace(".", ","))) < -10) && ((float.Parse(item.min_temp.Replace(".", ","))) > 20))
                        {
                            isTestPassed = false;
                        }
                    } break;
            }
        }

        class CitiesInfo
        {
            public string min_temp { get; set; }
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
        [Test]
        public void AssertTest()
        {
              Assert.True(isTestPassed, "temperatures are out of range");
        }
    }
}
