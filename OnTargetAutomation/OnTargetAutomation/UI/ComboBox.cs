using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class ComboBox
    {
        public static void Select(By by, string itemTextToBeSelected)
        {
            //WebDriverUtils.WaitForPage();
            Test.driver.FindElement(by).Click();
            //WebDriverUtils.WaitForPage();
            Test.driver.FindElement(By.XPath("//td[contains(.,'" + itemTextToBeSelected + "')]")).Click();
        }

        public static bool ValidateComboBoxIsDisplayed(By by)
        {
            WebDriverUtils.WaitForPage();
            try
            {
                return Test.driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public static bool ValidateComboBoxIsEnabled(By by)
        {
            WebDriverUtils.WaitForPage();
            try
            {
                return Test.driver.FindElement(by).Enabled;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public static bool ValidateComboBoxIsMandatory(By by)
        {
            //WebDriverUtils.WaitForPage();
            string state = Test.driver.FindElement(by).GetAttribute("aria-required");
            if (state == "false")
                return false;
            else if (state == "true")
                return true;
            else
                throw new Exception("State of " + by + " is not supperted: " + state);
        }

        public static bool ValidateComboBoxText(By by, string Text)
        {
            //WebDriverUtils.WaitForPage();
            return Test.driver.FindElement(by).Text.Equals(Text);
        }
    }
}
