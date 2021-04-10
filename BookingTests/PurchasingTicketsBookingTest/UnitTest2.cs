using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;


namespace PurchasingTicketsBookingTest
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _airTicketsButton = By.XPath("//a[@class = 'bui-tab__link']");
        private readonly By _destinationAirportButton = By.XPath("//div[contains(@id, 'destination-airport-display')]");
        private readonly By _destinationAirportInputButton= By.XPath("//input[contains(@id, 'destination-airport') and @type = 'text']");
        private readonly By _destinationAriportContry = By.XPath("//div[@class='item-info']");
        private readonly By _chooseNextDayFlightButton = By.XPath("//button[contains(@id, 'dateRangeInput-start-inc')]");
        private readonly By _searchButton = By.XPath("//button[contains(@id, 'submit')]");
        private readonly By _actualIsFilter = By.XPath("//h3[contains(@id, 'myfilters-title-text')]");
        
        private const string _expectedIsFilter = "Рекомендуемые фильтры"; //only used for acert, idk how to acert better
        private const string _destinationAirportName = "Berlin, Germany";

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com");
        }

        [Test]
        public void Test1()
        {
            var airTickets = driver.FindElement(_airTicketsButton);
            airTickets.Click();

            Thread.Sleep(800);
            var destinationAirport = driver.FindElement(_destinationAirportButton);
            destinationAirport.Click();

            Thread.Sleep(800);
            var destinationAirportInput = driver.FindElement(_destinationAirportInputButton);
            destinationAirportInput.SendKeys(_destinationAirportName);

            Thread.Sleep(800);
            var destinationAriportContry = driver.FindElement(_destinationAriportContry);
            destinationAriportContry.Click();

            var chooseDayOfFlight = driver.FindElement(_chooseNextDayFlightButton);
            chooseDayOfFlight.Click();

            Thread.Sleep(1000);
            var search = driver.FindElement(_searchButton);
            search.Click();

            Thread.Sleep(3000);
            var actualIsFilter = driver.FindElement(_actualIsFilter).Text;
            
            Assert.AreEqual(_expectedIsFilter, actualIsFilter, "purchase went wrong");
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}