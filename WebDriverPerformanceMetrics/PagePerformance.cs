using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverPerformanceMetrics;

namespace Web_driverPerformanceMetrics
{
    [TestFixture]
    public class PagePerformance
    {
        public ChromeOptions _options = new ChromeOptions();
        public IWebDriver? _driver;

        [SetUp]
        public void Setup()
        {
            PerformanceResultsBuilder.ClearLoadSpeeds();
            _options.AddArguments("--headless");

            _driver = new ChromeDriver(_options);
        }

        [TearDown]
        public void Teardown()
        {
            PerformanceResultsBuilder.PageLoadTimeLogToTxt();
            _driver.Quit();
        }

        [Test]
        public void LoginAndHomePage()
        {
            // Navigate to the website you want to measure
            _driver.Navigate().GoToUrl("https://autoa.qa.energyone.com/enTrader/1120/");

            _driver.PageLoad(_driver).FindElement(By.Id("ctl00_ContentBody_Login1_UserName")).SendKeys("Automation");
            _driver.FindElement(By.Id("ctl00_ContentBody_Login1_Password")).SendKeys("Password1");
            _driver.FindElement(By.Id("ctl00_ContentBody_Login1_LoginButton")).Click();

            Assert.IsTrue(_driver.PageLoad(_driver).FindElement(By.ClassName("main-menu-item")).Displayed);

        }
    }
}