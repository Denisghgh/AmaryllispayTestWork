using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace Currency_and_LanguageBookingTests
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _changeLanguageButton = By.XPath("//button[@data-modal-id='language-selection']");
        private readonly By _chooseLanguageButton = By.XPath("//a[@class='bui-list-item bui-list-item--size-small ']");
        private readonly By _changeCurrencyButton = By.XPath("//button[@class='bui-button bui-button--light bui-button--large']");
        private readonly By _chooseCurrencyButton = By.XPath("//a[@data-modal-header-async-url-param='changed_currency=1;selected_currency=EUR;top_currency=1']");
        private readonly By _actualCurrency = By.XPath("//span[@class='bui-u-sr-only']");
            
        private const string _expectedCurrencyText = "Choose your currency. Your current currency is Euro";
      
        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com");
        }

        [Test]
        public void Test1()
        {
            var changeLanguage = driver.FindElement(_changeLanguageButton);
            changeLanguage.Click();

            var chooselanguage = driver.FindElement(_chooseLanguageButton);
            chooselanguage.Click();

            Thread.Sleep(1000);
            var changeCurrency = driver.FindElement(_changeCurrencyButton);
            changeCurrency.Click();

            Thread.Sleep(800);
            var chooseCurrency = driver.FindElement(_chooseCurrencyButton);
            chooseCurrency.Click();

            Thread.Sleep(800);
            var actualCurrency = driver.FindElement(_actualCurrency).Text;
           
            Assert.AreEqual(_expectedCurrencyText, actualCurrency, "language is wrong");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}