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

        public LoginPage()
        {
            IWait<IWebDriver> wait = new WebDriverWait(Test.driver, TimeSpan.FromSeconds(30.00));
            wait.Until(_driver => ((IJavaScriptExecutor)Test.driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(ExpectedConditions.ElementIsVisible(txtLogin));
            wait.Until(ExpectedConditions.ElementIsVisible(txtPassword));
            wait.Until(ExpectedConditions.ElementIsVisible(btnLogin));
        }

        public void Login(string username, string password)
        {
            TextBox.SetTextInTextBox(txtLogin, username);
            TextBox.SetTextInTextBox(txtPassword, password);
            Button.ClickButton(btnLogin);
        }

        public bool ValidatePageURL(TestSettings settings)
        {
            return Test.driver.Url.Contains(settings.Environment.Substring(0, settings.Environment.Length - 2));
        }
    }
}
