using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class Button
    {
        public static void ClickButton(IWebDriver driver, By by)
        {
            driver.FindElement(by).Click();
        }
    }
}
