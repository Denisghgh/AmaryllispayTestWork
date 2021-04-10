using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test
{
    public class Tests
    {
        private const string _lattLongMinsk = "53.90255,27.563101";//information from Google
        private const string _city = "minsk";
        public static int Woeid { get; set; }

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task Latt_LongTest()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/search/?query=" + _city);
            List<cityInformations> cityInformations = JsonConvert.DeserializeObject<List<cityInformations>>(response);
            
            writeWoeid write = new writeWoeid(cityInformations[0].woeid);
            
            Assert.AreEqual(_lattLongMinsk, cityInformations[0].latt_long, "latt_long is not equal");
        }
        public class cityInformations
        {
            public int woeid { get; set; }
            public string latt_long { get; set; }
        }

        public class writeWoeid
        {
            public writeWoeid(int Woeid)
            {
                string writePath = @"..\..\..\..\_Woeid.txt";

                string text = Woeid.ToString();
                try
                {
                    using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(text);
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