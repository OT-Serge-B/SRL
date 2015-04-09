using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class Button
    {
        public static void ClickButton(By by)
        {
            Test.driver.FindElement(by).Click();
        }
    }
}
