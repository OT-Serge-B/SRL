using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace OnTargetAutomation.PO
{
    public class HomePage
    {
        //http://stoneriverdev.lifeportraits.com/LifePortraits.aspx
        By labelUserFirstName = By.Id("userFirstName");
        By labelUserLastName = By.Id("userLastName");
        By linkChannel = By.Id("agentDChannelHyperlink");

        private IWebDriver driver;
        public HomePage(IWebDriver drv)
        {
            this.driver = drv;

            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(_driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(ExpectedConditions.ElementIsVisible(labelUserFirstName));
            wait.Until(ExpectedConditions.ElementIsVisible(labelUserLastName));
            wait.Until(ExpectedConditions.ElementIsVisible(linkChannel));
        }
        #region validations
        public bool ValidatePageURL(TestSettings settings) {
            
            return driver.Url.Contains(settings.Environment + "LifePortraits.aspx");
        }

        public bool ValidateUserFirstName(string firstName)
        {
            return Link.getLinkText(this.labelUserFirstName).Equals(firstName);
        }

        public bool ValidateUserLastName(string lastName)
        {
            return Link.getLinkText(this.labelUserLastName).Equals(lastName);
        }

        public bool ValidateUserChannel(string channelName)
        {
            return Link.getLinkText(this.linkChannel).Equals(channelName);
        }
        #endregion
    }
}
