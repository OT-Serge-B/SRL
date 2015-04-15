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
        By cbPaymantModeExpanded = By.Id("paymentMode_dropdown");
        By cbPaymantMethod = By.Id("paymentMethod");
        By cbPaymantMethodExpanded = By.Id("paymentMethod_dropdown");

        By textCreditCardNo = By.Id("creditCardNumber");
        By textCreditCardExpired = By.Id("creditCardExpDate");
        By textCardholderName = By.Id("nameOnCard");
        By cbCreditCardType = By.Id("creditCardType");
        By cbCreditCardTypeExpanded = By.Id("creditCardType_dropdown");

        By textFinancialInstitutionName = By.Id("institutionName");
        By textRoutingNumber = By.Id("routingNumber");
        By textAccountNumber = By.Id("accountNumber");
        By textBranch = By.Id("branch");
        By cbAccountType = By.Id("accountType");
        By cbAccountTypeExpanded = By.Id("accountType_dropdown");
        By cbDraftDay = By.Id("paymentDay");
        By cbDraftDayExpanded = By.Id("paymentDay_dropdown");

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

        public void SetFinancialInstitutionName(string value)
        {
            TextBox.SetTextInTextBox(this.textFinancialInstitutionName, value);
            Assert.True(TextBox.ValidateTextBoxText(this.textFinancialInstitutionName, value));
        }
        public void SetRoutingNumber(string value)
        {
            TextBox.SetTextInTextBox(this.textRoutingNumber, value);
            Assert.True(TextBox.ValidateTextBoxText(this.textRoutingNumber, value));
        }
        public void SetAccountNumber(string value)
        {
            TextBox.SetTextInTextBox(this.textAccountNumber, value);
            Assert.True(TextBox.ValidateTextBoxText(this.textAccountNumber, value));
        }
        public void SetBranch(string value)
        {
            TextBox.SetTextInTextBox(this.textBranch, value);
            Assert.True(TextBox.ValidateTextBoxText(this.textBranch, value));
        }
        public void SetAccountType(string value)
        {
            ComboBox.Select(this.cbAccountType, value);
            Assert.True(ComboBox.ValidateComboBoxText(this.cbAccountType, value));
        }
        public void SetDraftDay(string value)
        {
            ComboBox.Select(this.cbDraftDay, value);
            Assert.True(ComboBox.ValidateComboBoxText(this.cbDraftDay, value));
        }


        public void ValidateCashAmount(string Cash, string Amount)
        {
            //click PI to get possible focus off any text control
            this.OpenPI();
            //Cash
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbCashWithApplication));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbCashWithApplication));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbCashWithApplication));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbCashWithApplication, Cash));
            

            //Amount
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textCashApplication));
            Assert.True(TextBox.ValidateTextBoxAmount(this.textCashApplication, Amount));
            if (Cash != string.Empty && Cash != "No")
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
        public void ValidatePlannedModalPremium(string PlannedMP)
        {
            //click PI to get possible focus off any text control
            this.OpenPI(); 
            //then - validate
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textPlannedModalPremium));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textPlannedModalPremium));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textPlannedModalPremium));
            Assert.True(TextBox.ValidateTextBoxAmount(this.textPlannedModalPremium, PlannedMP));
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
        public void ValidateCardPaymentInformation(string PaymentMethod, string cardType, string CardNo, string Expired, string CardholderName)
        {
            //click PI to get possible focus off any text control
            this.OpenPI();
            //the - validate PaymentMethod
            ValidatePaymentMethod(PaymentMethod);

            if (PaymentMethod == "Credit Card")
            {
                Assert.True(ComboBox.ValidateComboBoxText(this.cbPaymantMethod, PaymentMethod));
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
        }
        public void ValidatePaymentMethod(string PaymentMethod) 
        {
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbPaymantMethod));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbPaymantMethod));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbPaymantMethod));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbPaymantMethod, PaymentMethod));
        }
        public void ValidateBankDraftPaymentInformation(string FinancialInstitutionName, string RoutingNumber, string Branch, string AccountNumber, string DraftDay, string AccountType)
        {
            //FinancialInstitutionName
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textFinancialInstitutionName));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textFinancialInstitutionName));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textFinancialInstitutionName));
            Assert.True(TextBox.ValidateTextBoxText(this.textFinancialInstitutionName, FinancialInstitutionName));
            //RoutingNumber
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textRoutingNumber));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textRoutingNumber));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textRoutingNumber));
            Assert.True(TextBox.ValidateTextBoxText(this.textRoutingNumber, RoutingNumber));
            //Branch
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textBranch));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textBranch));
            Assert.False(TextBox.ValidateTextBoxIsMandatory(this.textBranch));
            Assert.True(TextBox.ValidateTextBoxText(this.textBranch, Branch));
            //AccountNumber
            Assert.True(TextBox.ValidateTextBoxIsDisplayed(this.textAccountNumber));
            Assert.True(TextBox.ValidateTextBoxIsEnabled(this.textAccountNumber));
            Assert.True(TextBox.ValidateTextBoxIsMandatory(this.textAccountNumber));
            Assert.True(TextBox.ValidateTextBoxText(this.textAccountNumber, AccountNumber));
            //DraftDay
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbDraftDay));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbDraftDay));
            Assert.False(ComboBox.ValidateComboBoxIsMandatory(this.cbDraftDay));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbDraftDay, DraftDay));
            //AccountType
            Assert.True(ComboBox.ValidateComboBoxIsDisplayed(this.cbAccountType));
            Assert.True(ComboBox.ValidateComboBoxIsEnabled(this.cbAccountType));
            Assert.True(ComboBox.ValidateComboBoxIsMandatory(this.cbAccountType));
            Assert.True(ComboBox.ValidateComboBoxText(this.cbAccountType, AccountType));
        }
    }
}
