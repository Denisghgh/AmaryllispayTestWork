using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace AuthorizationTest
{
    /// <summary>
    /// please use Chrome version 85.0
    /// </summary>
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _signInButton = By.XPath("//span[@class = 'bui-button__text' and contains(text(),'Войти в аккаунт')]");
        
        private readonly By _emailInputButton = By.XPath("//input[@type = 'email' and @id = 'username']");
        private readonly By _continueWithEmailButton = By.XPath("//button[@class = 'bui-button bui-button--large bui-button--wide' and @type = 'submit']");
        private readonly By _passwordInputButton = By.XPath("//input[@type = 'password' and @id = 'password']");
        private readonly By _continueWithPasswordButton = By.XPath("//button[@class = 'bui-button bui-button--large bui-button--wide']");
        private readonly By _actualUserLogin = By.XPath("//span[@id='profile-menu-trigger--title']");
        
        private const string email = "denismarhenko123@gmail.com";
        private const string password = "aDq9PkaNmdvi6ZV";
        private const string _expectedIsLogIn = "Ваш аккаунт";

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com");
        }

        [Test]
        public void Test1()
        {
            var sighIn = driver.FindElement(_signInButton);
            sighIn.Click();

            Thread.Sleep(800);
            var emailInput = driver.FindElement(_emailInputButton);
            emailInput.SendKeys(email);

            var continueButton = driver.FindElement(_continueWithEmailButton);
            continueButton.Click();

            Thread.Sleep(1000);
            var passwordInput = driver.FindElement(_passwordInputButton);
            passwordInput.SendKeys(password);

            Thread.Sleep(1000);
            var continueButton2 = driver.FindElement(_continueWithPasswordButton);
            continueButton2.Click();


            Thread.Sleep(2000);
            var actualIsLogIn = driver.FindElement(_actualUserLogin).Text;


            Assert.AreEqual(_expectedIsLogIn, actualIsLogIn, "Failed to log in");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}