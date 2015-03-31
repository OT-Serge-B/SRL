using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class CheckBox
    {
        public static void setCheckBox(IWebDriver driver, By by, bool state)
        {
            if (driver.FindElement(by).Selected != state)
                driver.FindElement(by).Click();
        }
    }
}
