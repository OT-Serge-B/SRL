using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class Link
    {
        public static void clickLink(By by)
        {
            //WebDriverUtils.WaitForPage();
            Test.driver.FindElement(by).Click();
        }

        public static void clickLinkByText(string linkText)
        {
            //WebDriverUtils.WaitForPage();
            By by = By.LinkText(linkText);
            clickLink(by);
        }

        public static string getLinkText(By by) {
            //WebDriverUtils.WaitForPage();
            return Test.driver.FindElement(by).Text;
        }
    }
}
