﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace OnTargetAutomation.PO
{
    public class LifePortraits
    {
        //http://stoneriverdev.lifeportraits.com/LifePortraits.aspx
        By labelUserFirstName = By.Id("userFirstName");
        By labelUserLastName = By.Id("userLastName");
        By linkChannel = By.Id("agentDChannelHyperlink");

        private IWebDriver driver;
        public LifePortraits(IWebDriver drv)
        {
            this.driver = drv;

            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(_driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(ExpectedConditions.ElementIsVisible(labelUserFirstName));
            wait.Until(ExpectedConditions.ElementIsVisible(labelUserLastName));
            wait.Until(ExpectedConditions.ElementIsVisible(linkChannel));
        }

        public bool ValidatePageURL(TestSettings settings) {
            
            return driver.Url.Contains(settings.Environment + "LifePortraits.aspx");
        }

        public bool ValidateUserFirstName(string firstName)
        {
            return Link.getLinkText(driver, this.labelUserFirstName).Equals(firstName);
        }

        public bool ValidateUserLastName(string lastName)
        {
            return Link.getLinkText(driver, this.labelUserLastName).Equals(lastName);
        }

        public bool ValidateUserChannel(string channelName)
        {
            return Link.getLinkText(driver, this.linkChannel).Equals(channelName);
        }
    }
}
