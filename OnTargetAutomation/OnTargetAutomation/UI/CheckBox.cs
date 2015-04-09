using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class CheckBox
    {
        public static void setCheckBox(By by, bool state)
        {
            if (Test.driver.FindElement(by).Selected != state)
                Test.driver.FindElement(by).Click();
        }
    }
}
