using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class Link
    {
        public static void clickLink(IWebDriver driver, By by)
        {
            driver.FindElement(by).Click();
        }

        public static void clickLinkByText(IWebDriver driver, string linkText)
        {
            By by = By.LinkText(linkText);
            clickLink(driver, by);
        }

        public static string getLinkText(IWebDriver driver, By by) {
            return driver.FindElement(by).Text;
        }
    }
}
