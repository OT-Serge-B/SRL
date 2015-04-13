using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class CheckBox
    {
        public static void setCheckBox(By by, bool state)
        {
            //WebDriverUtils.WaitForPage();
            if (Test.driver.FindElement(by).Selected != state)
                Test.driver.FindElement(by).Click();
        }
    }
}
