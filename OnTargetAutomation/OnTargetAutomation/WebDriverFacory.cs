using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace OnTargetAutomation.Driver
{
    public class WebDriverFacory
    {
        //factory settings
        private static string defaultHub = null;
        private static int restartFrequency = int.MaxValue;

        public static void setDefaultHub(string newDefaultHub) {
            defaultHub = newDefaultHub;
        }
        public static void setRestartFrequency(int newRestartFrequency)
        {
            restartFrequency = newRestartFrequency;
        }

        //factory
        private static string key = null;
        private static int count = 0;
        private static IWebDriver driver;

        public static IWebDriver getDriver(string hub, DesiredCapabilities capabilities) {
            count++;
            //create new instance
            if (driver == null)
                return newWebDriver(hub, capabilities);

            //new capabilities needed
            string newKey = capabilities.ToString() + ":" + hub;
            if (!newKey.Equals(key))
            {
                dismissDriver();
                key = newKey;
                return newWebDriver(hub, capabilities);
            }

            //dead browser
            try
            {
                string url = driver.Url;
            }
            catch (Exception ex) { 
                //TODO - add ex to log
                return newWebDriver(hub, capabilities);
            }

            //time to restart
            if (count >= restartFrequency) {
                dismissDriver();
                return newWebDriver(hub, capabilities);
            }

            //else - use already existing driver instance
            return driver;
        }

        public static IWebDriver getDriver(DesiredCapabilities capabilities)
        {
            return getDriver(defaultHub, capabilities);
        }
        private static IWebDriver newWebDriver(string hub, DesiredCapabilities capabilities)
        {
            driver = (hub == null)
                ? createLocalDriver(capabilities)
                : createRemoteDriver(hub, capabilities);
            key = capabilities.ToString() + ":" + hub;
            count = 0;
            return driver;
        }

        private static IWebDriver createRemoteDriver(string hub, DesiredCapabilities capabilities)
        {
            try
            {
                return new RemoteWebDriver(new Uri(hub), capabilities);
            }
            catch (Exception ex)
            { 
                //TODO - use ex
                throw new Exception("Unable to connect to new WebDriver hub");
            }
        }

        private static IWebDriver createLocalDriver(DesiredCapabilities capabilities)
        {
            string browser = capabilities.BrowserName;
            if (browser.Equals("firefox"))
                return new FirefoxDriver(capabilities);
            //else if (browser.Equals("internet explorer"))
            //    return new InternetExplorerDriver(new DesiredCapabilities().)
            //if (browser.Equals("chrome"))
            //    return new ChromeDriver(capabilities);
            else
                throw new Exception("Unrecognized browser: " + browser);

        }

        private static void dismissDriver()
        {
            //try to quit
            if (driver != null) {
                try
                {
                    driver.Quit();
                    driver = null;
                    key = null;
                }
                catch (Exception ex) { 
                //dead or unreachable driver
                }
            }
        }


    }
}
