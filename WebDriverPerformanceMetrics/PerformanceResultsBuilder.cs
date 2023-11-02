using OpenQA.Selenium;
using Serilog;
using Serilog.Formatting.Compact;

namespace WebDriverPerformanceMetrics
{
    public static class PerformanceResultsBuilder
    {
        public static Dictionary<string, int> loadSpeeds = new Dictionary<string, int>();

        public static void ClearLoadSpeeds()
        {
            loadSpeeds.Clear();
        }

        public static IWebDriver PageLoad(this IWebDriver wd, IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            loadSpeeds.Add(driver.Title, Convert.ToInt32(js.ExecuteScript("return window.performance.timing.domContentLoadedEventEnd-window.performance.timing.navigationStart;")));
            return driver;
        }

        public static void PageLoadTimeLogToTxt()
        {
            //var log = new LoggerConfiguration()
            //            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            //            .CreateLogger();

            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var log = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            var log = new LoggerConfiguration()
            .WriteTo.File(new CompactJsonFormatter(), "log.json")
            .CreateLogger();

            foreach (var item in loadSpeeds)
            {
                log.Information(item.Key.ToString() + " - " + item.Value.ToString() + "ms");
                if (item.Value > 7500)
                {
                    //Ping Automation team on Teams??
                }
            }
        }
    }
}
