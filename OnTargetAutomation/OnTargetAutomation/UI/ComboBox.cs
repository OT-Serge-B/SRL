using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;

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
            if (state == "false" || state == null)
                return false;
            else if (state == "true")
                return true;
            else
                throw new Exception("State of " + by + " is not supperted: " + state);
        }

        public static bool ValidateComboBoxText(By by, string Text)
        {
            if (Text != string.Empty)
                return Test.driver.FindElement(by).Text.Equals(Text);
            else
                return Test.driver.FindElement(by).Text.Equals(" ");
        }

        public static bool ValidateComboBoxContent(By expandedBy, List<string> expectedItems)
        {
            var dropdown = Test.driver.FindElement(expandedBy);
            List<string> actualList = new List<string>();
            IReadOnlyCollection<IWebElement> list =  dropdown.FindElements(By.ClassName("dijitMenuItemLabel"));
            foreach (var l in list)
            {
                Console.WriteLine(l.Text);
            }
            return true;
        }
    }
}
