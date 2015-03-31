using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace OnTargetAutomation
{
    public class Hover
    {
        public static void hoverOverElement(IWebDriver driver, By by)
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(by)).Perform();
        }
    }
}
