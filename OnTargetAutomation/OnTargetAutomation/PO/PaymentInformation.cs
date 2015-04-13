using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;


namespace OnTargetAutomation.PO
{
    public class PI
    {

        //leftNav for application
        By leftNavPaymentInformation = By.Id("paymentInformation_text");
        //PI
        By cbCashWithApplication = By.Id("cashApplication");
        By textCashApplication = By.Id("amount");
        By textPlannedModalPremium = By.Id("plannedModalPremium");
        By cbPaymantMode = By.Id("paymentMode");
        By cbPaymantMethod = By.Id("paymentMethod");
        By textCreditCardNo = By.Id("creditCardNumber");
        By textCreditCardExpired = By.Id("creditCardExpDate");
        By textCardholderName = By.Id("nameOnCard");
        By cbCreditCardType = By.Id("creditCardType");

        public void OpenPI() 
        {
            WebDriverUtils.WaitForPage();
            Link.clickLink(this.leftNavPaymentInformation);
        }

        public void SetCash(string value)
        {
            ComboBox.Select(this.cbCashWithApplication, value);
            Assert.True(ComboBox.ValidateComboBoxText(this.cbCashWithApplication, value));
        }
        public void SetAmount(string value)
        {
            TextBox.SetTextInTextBox(this.textCashApplication, value);
            //while control is focused, it does not contain any differences from entered value 
            Assert.True(TextBox.ValidateTextBoxText(this.textCashApplication, value));
        }
        public void SetPlannedMP(string value)
        {
            TextBox.SetTextInTextBox(this.textPlannedModalPremium, value);
            //while control is focused, it does not contain any differences from entered value 
            Assert.True(TextBox.ValidateTextBoxText(this.textPlannedModalPremium, value));
        }
        public void SetPaymentMethod(string value)
        {
            ComboBox.Select(this.cbPaymantMethod, value);
            Assert.True(ComboBox.ValidateComboBoxText(this.cbPaymantMethod, value));
        }
        public void SetCardNumber(string value)
        {
            TextBox.SetTextInTextBox(this.textCreditCardNo, value);
            Assert.True(TextBox.ValidateTextBoxText(this.textCreditCardNo, value));
        }
        public void SetCardholder(string value)
        {
            TextBox.SetTextInTextBox(this.textCardholderName, value);
            Assert.True(TextBox.ValidateTextBoxText(this.textCardholderName, value));
        }
        public void SetExpirationDate(string value)
        {
            TextBox.SetTextInTextBox(this.textCreditCardExpired, value);
        }
        public void SetCardType(string value)
        {
            ComboBox.Select(this.cbCreditCardType, value);
            Assert.True(ComboBox.ValidateComboBoxText(this.cbCreditCardType, value));
        }
        

        public void ValidateGeneralPaymentInformation(string Cash, string Amount, string PaymentMode, string PlannedMP, string PaymentMethod)
        {
            //click PI to get possible focus off any text control
            this.OpenPI();
            //Cash
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbCashWithApplication));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbCashWithApplication));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbCashWithApplication));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbCashWithApplication, Cash));
            //PaymentMode
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbPaymantMode));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbPaymantMode));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbPaymantMode));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbPaymantMode, PaymentMode));

            //PaymentMethod
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbPaymantMethod));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbPaymantMethod));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbPaymantMethod));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbPaymantMethod, PaymentMethod));

            //PlannedModalPremium
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textPlannedModalPremium));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textPlannedModalPremium));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textPlannedModalPremium));
            Assert.True(TextBox.ValidateTextBoxAmount(this.textPlannedModalPremium, PlannedMP));

            //Amount
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textCashApplication));
            Assert.True(TextBox.ValidateTextBoxAmount(this.textCashApplication, Amount));
            if (Cash != " " && Cash != "No")
            {
                Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textCashApplication));
                Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textCashApplication));
            }
            else
            {
                Assert.False(TextBox.ValidateTextBoxIsEnabled(this.textCashApplication));
                Assert.False(TextBox.ValidateTextBoxIsMandatory(this.textCashApplication));
            }
        }
        public void ValidateCardPaymentInformation(string cardType, string CardNo, string Expired, string CardholderName)
        {
            //cardtype
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbCreditCardType));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbCreditCardType));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbCreditCardType));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbCreditCardType, cardType));

            //cardNo
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textCreditCardNo));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textCreditCardNo));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textCreditCardNo));
            Assert.True(TextBox.ValidateTextBoxText(this.textCreditCardNo, CardNo));

            //expired
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textCreditCardExpired));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textCreditCardExpired));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textCreditCardExpired));
            Assert.True(TextBox.ValidateTextBoxText(this.textCreditCardExpired, Expired));

            //cardholderName
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textCardholderName));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textCardholderName));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textCardholderName));
            Assert.True(TextBox.ValidateTextBoxText(this.textCardholderName, CardholderName));

        }
        public bool ValidateBankDraftPaymentInformation()
        {
            return true;
        }
    }
}
