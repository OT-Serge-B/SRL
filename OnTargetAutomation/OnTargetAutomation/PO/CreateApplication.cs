using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;


namespace OnTargetAutomation.PO
{
    public class CreateApplication
    {
        //http://stoneriverdev.lifeportraits.com/LifePortraits.aspx#
        By leftNavCreateApp = By.Id("CreateApplication_text");
        //create new client/application dialog
        By rbNewClient = By.Id("radioNewClient");
        By textNewClientFirstName = By.Id("NewClientFirstName");
        By textNewClientLastName = By.Id("NewClientLastName");
        By textNewClientDateOfBirth = By.Id("NewClientDateOfBirth");
        By textNewClientAge = By.Id("NewClientAge");
        By cbNewClientGender = By.Id("NewClientGender");
        By btnDialogNext = By.Id("srcore_widget_ModalDialog_0_button_ok_label");
        By btnDialogCreate = By.Id("srcore_widget_ModalDialog_1_button_ok_label");
        //application screen
        By textFirstName = By.Id("FirstName");
        By textLastName = By.Id("LastName");
        //leftNav for application
        By leftNavPaymentInformation = By.Id("paymentInformation_text");
        By leftNavCompleteApplication = By.Id("completeApplication_text");
        //PI
        By cbCashWithApplication = By.Id("cashApplication");
        By textCashApplication = By.Id("amount");
        By textPlannedModalPremium = By.Id("plannedModalPremium");
        By cbPaymantMode = By.Id("paymentMode");
        By cbPaymantMethod = By.Id("paymentMethod");
        //CA
        By linkPaymentInformation = By.Id("Payment Information");
        By linkBeneficiary = By.Id("Beneficiary");


        private IWebDriver driver;
        public CreateApplication(IWebDriver drv)
        {
            this.driver = drv;
        }

        public void CreateNewAppNewUser(TestData data)
        {
            //service method, not to test anything, just to prepare fresh new application for testing
            Link.clickLink(this.driver, this.leftNavCreateApp);
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(ExpectedConditions.ElementIsVisible(this.rbNewClient));

            RadioButton.clickRadioButton(this.driver, this.rbNewClient);
            wait.Until(ExpectedConditions.ElementIsVisible(this.textNewClientFirstName));

            TextBox.SetTextInTextBox(this.driver, this.textNewClientFirstName, data.ClientFirstName);
            TextBox.SetTextInTextBox(this.driver, this.textNewClientLastName, data.ClientLastName);
            TextBox.SetTextInTextBox(this.driver, this.textNewClientDateOfBirth, data.ClientDateBirth);
            ComboBox.Select(this.driver, this.cbNewClientGender, data.ClientSex);
            Button.ClickButton(this.driver, btnDialogNext);

            wait.Until(ExpectedConditions.ElementIsVisible(this.btnDialogCreate));
            Button.ClickButton(this.driver, btnDialogCreate);
        }

        public void SetPI(TestData data) 
        {
            Link.clickLink(this.driver, this.leftNavPaymentInformation);
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(ExpectedConditions.ElementIsVisible(this.cbCashWithApplication));
        }

        public void ValidateCompleteApplication(TestData data, string[] expectedList)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(ExpectedConditions.ElementIsVisible(this.textFirstName));
            Link.clickLink(this.driver, this.leftNavCompleteApplication);

            wait.Until(ExpectedConditions.ElementIsVisible(this.linkPaymentInformation));

            string[] actualList = this.getAllSectionItemsInCA(this.linkPaymentInformation, this.linkBeneficiary);
            Assert.AreEqual(expectedList, actualList);
        }

        private string[] getAllSectionItemsInCA(By sectionStartLink, By sectionStopLink)
        {
            List<string> list = new List<string>();
            IList<IWebElement> elements = driver.FindElements(By.CssSelector("*"));

            int start, finish;
            start = -1;
            finish = -1;
                for (int a = 0; a < elements.Count; a++)
                {
                    if (elements[a].GetAttribute("id") == driver.FindElement(sectionStartLink).GetAttribute("id"))
                        start = a;
                    else if (elements[a].GetAttribute("id") == driver.FindElement(sectionStopLink).GetAttribute("id"))
                    {
                        finish = a;
                        break;
                    }
                }
                for (int b = start + 1; b < finish; b++)
                {
                    if (elements[b].TagName == "label")
                        list.Add(elements[b].Text);
                }   
             return list.ToArray();
        }

#region validations
        //public bool ValidatePageURL(TestSettings settings) {
            
        //    return driver.Url.Contains(settings.Environment + "LifePortraits.aspx");
        //}

        //public bool ValidateUserFirstName(string firstName)
        //{
        //    return Link.getLinkText(driver, this.labelUserFirstName).Equals(firstName);
        //}

        //public bool ValidateUserLastName(string lastName)
        //{
        //    return Link.getLinkText(driver, this.labelUserLastName).Equals(lastName);
        //}

        //public bool ValidateUserChannel(string channelName)
        //{
        //    return Link.getLinkText(driver, this.linkChannel).Equals(channelName);
        //}
#endregion
    }
}
