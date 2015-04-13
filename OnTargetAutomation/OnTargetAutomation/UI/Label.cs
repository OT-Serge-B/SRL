using System;
using OpenQA.Selenium;

namespace OnTargetAutomation
{
    public class Label
    {
        public static bool IsLabelPresented(string labelText)
        {
            try
            {
                return Test.driver.FindElement(By.XPath("//label[text()='" + labelText + "']")).Displayed;
            }
            catch (NoSuchElementException e)
            {
                //Console.WriteLine("Unable to locate " + labelText);
                return false;
            }
        }
    }
}
