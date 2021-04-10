using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;


namespace PurchasingTicketsBookingTest
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _selectCityButton = By.XPath("//input[@type = 'search' and @name = 'ss']");
        private readonly By _calenderActivateButton = By.XPath("//span[@class = 'sb-date-field__icon sb-date-field__icon-btn bk-svg-wrapper calendar-restructure-sb']");
        private readonly By _depatureDateButton = By.XPath("//td[ @data-date= '2021-04-17']");
        private readonly By _arrivalDateButton = By.XPath("//td[ @data-date= '2021-04-19']");
        private readonly By _chooseNumberOfPeopleButton = By.XPath("//span[@class='xp__guests__count']");
        private readonly By _increaseNumberOfChildrenButton = By.XPath("//button[@class='bui-button bui-button--secondary bui-stepper__add-button ' and @aria-describedby = 'group_children_desc']");
        private readonly By _submitButton = By.XPath("//button[@data-sb-id='main' and @type= 'submit']");
        private readonly By _actualIsCity = By.XPath(" //div[@class='sr_header ' and @role = 'heading']");

        private const string _selectedCity = "Berlin";
        private const string _expectedIsCity = "Берлин: найдено";

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com");
        }

        [Test]
        public void Test1()
        {
            var selectedCity = driver.FindElement(_selectCityButton);
            selectedCity.SendKeys(_selectedCity);

            var activateCalender=driver.FindElement(_calenderActivateButton);
            activateCalender.Click();

            Thread.Sleep(800);
            var depatureDate = driver.FindElement(_depatureDateButton);
            depatureDate.Click();

            Thread.Sleep(800);
            var arrivalDate = driver.FindElement(_arrivalDateButton);
            arrivalDate.Click();

            var numberOfPeole = driver.FindElement(_chooseNumberOfPeopleButton);
            numberOfPeole.Click();

            Thread.Sleep(800);
            var increaseChildrenAmount = driver.FindElement(_increaseNumberOfChildrenButton);
            increaseChildrenAmount.Click();

            Thread.Sleep(800);
            var submitClick = driver.FindElement(_submitButton);
            submitClick.Click();

            Thread.Sleep(2000);
            var actualIsCity = driver.FindElement(_actualIsCity).Text;
            
            Assert.IsTrue(actualIsCity.Contains(_expectedIsCity), "filter is not working");
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}