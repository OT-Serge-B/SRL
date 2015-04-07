using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace OnTargetAutomation.PO
{
    public class LoginPage
    {
        //http://stoneriverdev.lifeportraits.com/
        By txtLogin = By.Id("userName");
        By txtPassword = By.Id("pwd");
        By btnLogin = By.Id("btnLogin_label");
        private IWebDriver driver;
        public LoginPage(IWebDriver drv)
        {
            this.driver = drv;
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(_driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(ExpectedConditions.ElementIsVisible(txtLogin));
            wait.Until(ExpectedConditions.ElementIsVisible(txtPassword));
            wait.Until(ExpectedConditions.ElementIsVisible(btnLogin));
        }

        public void Login(string username, string password)
        {
            TextBox.SetTextInTextBox(driver, txtLogin, username);
            TextBox.SetTextInTextBox(driver, txtPassword, password);
            Button.ClickButton(driver, btnLogin);
        }

        public bool ValidatePageURL(TestSettings settings)
        {
            return driver.Url.Contains(settings.Environment.Substring(0, settings.Environment.Length-2));
        }
    }
}
