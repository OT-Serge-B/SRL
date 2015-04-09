using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace OnTargetAutomation
{
    public class Hover
    {
        public static void hoverOverElement(By by)
        {
            Actions builder = new Actions(Test.driver);
            builder.MoveToElement(Test.driver.FindElement(by)).Perform();
        }
    }
}
