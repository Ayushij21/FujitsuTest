using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace UIAutomationProject_Test
{
    [TestClass]
    public class TestBase
    {
        public IWebDriver driver;
        public TestContext TestContext { get; set; }
        public static ExtentReports extent = null;
        public static ExtentTest test = null;
        public void LaunchApplication(string appUrl)
        {
            ILog logger = LogManager.GetLogger(typeof(TestBase));
            logger.Info("sddsd");
            LaunchBrowser();
            driver.Navigate().GoToUrl(appUrl);
        }

        public void LaunchBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        public void QuitBrowser()
        {
            driver.Quit();
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            extent = new ExtentReports();
            string testCaseName = context.TestName;
            var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\debug", "");
            Directory.CreateDirectory(dir + "\\Test_Reports\\");
            string path = dir + "\\Test_Reports\\" + testCaseName;
            var htmlReporter = new ExtentHtmlReporter(path + ".html");
            extent.AttachReporter(htmlReporter);
        }


        [TestInitialize]
        public virtual void TestInitialize()
        {
           
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            extent.Flush();
        }

        public static string captureScreenShot(IWebDriver driver, string name)
        {

            ITakesScreenshot a = (ITakesScreenshot)driver;
            Screenshot ss = a.GetScreenshot();
            var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\debug", "");
            Directory.CreateDirectory(dir + "\\ErrorShot\\");
            string path = dir + "\\Test_Reports\\" + name + ".png";
            ss.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;
        }
    }
}
