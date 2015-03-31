using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class TextBox
    {
        public static void SetTextInTextBox(IWebDriver driver, By by, string Text)
        {
            driver.FindElement(by).SendKeys(Text);
        }
    }
}
