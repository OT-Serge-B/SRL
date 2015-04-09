using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class RadioButton
    {
        public static void clickRadioButton(By by)
        {
            Test.driver.FindElement(by).Click();
        }
    }
}
