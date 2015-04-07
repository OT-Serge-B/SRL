using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class ComboBox
    {
        public static void Select(IWebDriver driver, By by, string itemTextToBeSelected)
        {
            driver.FindElement(by).Click();
            driver.FindElement(By.XPath("//td[contains(.,'" + itemTextToBeSelected + "')]")).Click();
        }
    }
}
