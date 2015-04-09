using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class Link
    {
        public static void clickLink(By by)
        {
            Test.driver.FindElement(by).Click();
        }

        public static void clickLinkByText(string linkText)
        {
            By by = By.LinkText(linkText);
            clickLink(by);
        }

        public static string getLinkText(By by) {
            return Test.driver.FindElement(by).Text;
        }
    }
}
