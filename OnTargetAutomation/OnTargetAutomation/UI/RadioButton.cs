using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class RadioButton
    {
        public static void clickRadioButton(IWebDriver driver, By by)
        {
            driver.FindElement(by).Click();
        }
    }
}
