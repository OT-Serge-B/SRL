﻿using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;


namespace OnTargetAutomation.PO
{
    public class CA
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
        //application first screen
        By textFirstName = By.Id("FirstName");
        By textLastName = By.Id("LastName");
        //leftNav for application
        By leftNavPaymentInformation = By.Id("paymentInformation_text");
        By leftNavCompleteApplication = By.Id("completeApplication_text");
        //CA
        By linkPaymentInformation = By.Id("Payment Information");
        By linkBeneficiary = By.Id("Beneficiary");

        public CA()
        {
        }

        public void CreateNewAppNewUser(TestData data)
        {
            WebDriverUtils.WaitForPage();
            //service method, not to test anything, just to prepare fresh new application for testing
            Link.clickLink(this.leftNavCreateApp);
            WebDriverUtils.WaitForPage();

            RadioButton.clickRadioButton(this.rbNewClient);
            WebDriverUtils.WaitForPage();

            TextBox.SetTextInTextBox(this.textNewClientFirstName, data.ClientFirstName);
            TextBox.SetTextInTextBox(this.textNewClientLastName, data.ClientLastName);
            TextBox.SetTextInTextBox(this.textNewClientDateOfBirth, data.ClientDateBirth);
            ComboBox.Select(this.cbNewClientGender, data.ClientSex);
            Button.ClickButton(btnDialogNext);
            WebDriverUtils.WaitForPage();
            Button.ClickButton(btnDialogCreate);
        }
        public void OpenCA()
        {
            WebDriverUtils.WaitForPage();
            IWait<IWebDriver> wait = new WebDriverWait(Test.driver, TimeSpan.FromSeconds(30.00));
            wait.Until(ExpectedConditions.ElementIsVisible(this.leftNavCompleteApplication));
            Link.clickLink(this.leftNavCompleteApplication);
            wait.Until(ExpectedConditions.ElementIsVisible(this.linkPaymentInformation));
        }


        public void ValidateCompleteApplication(string[] expectedList)
        {
            //too long - about 4 minutes. Ca 1 minute to go through the whole DOM and find label there
            //string[] actualList = this.getAllSectionItemsInCA(this.linkPaymentInformation, this.linkBeneficiary);
            //Assert.AreEqual(expectedList, actualList);

            //faster way - via ValidateGeneralMandatoryFieldsListedOnCA
        }
        private string[] getAllSectionItemsInCA(By sectionStartLink, By sectionStopLink)
        {
            List<string> list = new List<string>();
            IList<IWebElement> elements = Test.driver.FindElements(By.CssSelector("*"));

            int start, finish;
            start = -1;
            finish = -1;
                for (int a = 0; a < elements.Count; a++)
                {
                    if (elements[a].GetAttribute("id") == Test.driver.FindElement(sectionStartLink).GetAttribute("id"))
                        start = a;
                    else if (elements[a].GetAttribute("id") == Test.driver.FindElement(sectionStopLink).GetAttribute("id"))
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
        public void ValidateGeneralMandatoryFieldsListedOnCA(bool PlannedMP, bool Cash, bool PaymentMethod, bool PaymentMode, bool Amount)
        {
            if (PlannedMP)
                Assert.True(Label.IsLabelPresented("Planned Modal Premium"));
            else
                Assert.False(Label.IsLabelPresented("Planned Modal Premium"));

            if (Cash)
                Assert.True(Label.IsLabelPresented("Cash with Application"));
            else
                Assert.False(Label.IsLabelPresented("Cash with Application"));

            if (PaymentMethod)
                Assert.True(Label.IsLabelPresented("Payment Method"));
            else
                Assert.False(Label.IsLabelPresented("Payment Method"));

            if (PaymentMode)
                Assert.True(Label.IsLabelPresented("Payment Mode"));
            else
                Assert.False(Label.IsLabelPresented("Payment Mode"));

            if (Amount)
                Assert.True(Label.IsLabelPresented("Amount"));
            else
                Assert.False(Label.IsLabelPresented("Amount"));
        }
        public void ValidateCardMandatoryFieldsListedOnCA(bool AccNo, bool Expired, bool Name) 
        {
            if (AccNo)
                Assert.True(Label.IsLabelPresented("Account Number"));
            else
                Assert.False(Label.IsLabelPresented("Account Number"));

            if (Expired)
                Assert.True(Label.IsLabelPresented("Expiration Date"));
            else
                Assert.False(Label.IsLabelPresented("Expiration Date"));

            if (Name)
                Assert.True(Label.IsLabelPresented("Name on Card"));
            else
                Assert.False(Label.IsLabelPresented("Name on Card"));
        }
    }
}
