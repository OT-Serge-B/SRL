using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class TextBox
    {
        public static void SetTextInTextBox(By by, string Text)
        {
            //WebDriverUtils.WaitForPage();
            Test.driver.FindElement(by).Clear();
            Test.driver.FindElement(by).SendKeys(Text);
        }

        public static bool ValidateTextBoxText(By by, string Text)
        {
            //WebDriverUtils.WaitForPage();
            return Test.driver.FindElement(by).GetAttribute("value").Equals(Text);
        }

        public static bool ValidateTextBoxAmount(By by, string Text)
        {
            //WebDriverUtils.WaitForPage();
            if (Text == "") return Test.driver.FindElement(by).GetAttribute("value").Equals(Text);
            else return Test.driver.FindElement(by).GetAttribute("value").Equals("$" + Text + ".00");
        }

        public static bool ValidateTextBoxIsDisplayed(By by)
        {
            //WebDriverUtils.WaitForPage();
            try
            {
                return Test.driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public static bool ValidateTextBoxIsEnabled(By by)
        {
            //WebDriverUtils.WaitForPage();
            try
            {
                return Test.driver.FindElement(by).Enabled;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public static bool ValidateTextBoxIsMandatory(By by)
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
    }
}
