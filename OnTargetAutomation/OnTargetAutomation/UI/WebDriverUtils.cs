using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;

using NUnit.Framework;

namespace OnTargetAutomation
{
    public class WebDriverUtils
    {

        public static void WaitForPage() {
            WaitForPageIsNotBusy();
            WaitForPageAutomatedReady();
        }
        
        static public void WaitForPageIsNotBusy()
        {
            By busyIndicator = By.XPath("//img[contains(src,'loading.gif')]");
            try
            {
                //wait start
                Test.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
                IWebElement element = Test.driver.FindElement(busyIndicator);
                if (element.Displayed)
                {
                    do
                    {
                        Thread.Sleep(250);
                    }
                    while (!element.Displayed);
                    Thread.Sleep(250);
                    if (element.Displayed) 
                        WaitForPageIsNotBusy();
                }
            }
            catch (Exception ex) { }
            finally
            {
                Test.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            }
        }
        static public void WaitForPageAutomatedReady()
        {
            By statusIndicator = By.Id("automation_status");
            try
            {
                //wait start
                Test.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
                IWebElement element = Test.driver.FindElement(statusIndicator);
                if (element.GetAttribute("value") != "Ready")
                {
                    do
                    {
                        Thread.Sleep(500);
                    }
                    while (element.GetAttribute("value") != "Ready");
                }
            }
            catch (Exception ex) { }
            finally
            {
                Test.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            }
        }
    }
}
