using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace OnTargetAutomation
{
    [TestFixture]
        public class Test
        {
            public static IWebDriver driver;
            public TestSettings settingsObj;
            public TestData dataObj;
            [SetUp]
            public void Setup()
            {
                DesiredCapabilities capabilities = new DesiredCapabilities();
                settingsObj = new TestSettings();
                capabilities.SetCapability(CapabilityType.BrowserName, settingsObj.BrowserName);
                
                driver = WebDriverFacory.getDriver(capabilities);
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            }

            [TearDown]
            public void TearDown()
            {
                //quit webdriver
                WebDriverFacory.dismissDriver();
            }

            [Test]
            public void T0001_TestUserLogin()
            {
                //step 1 - open login page
                dataObj = new TestData("TestUserLogin");
                driver.Navigate().GoToUrl(settingsObj.Environment);
                PO.LoginPage loginPage = new PO.LoginPage();//driver);
                Assert.True(loginPage.ValidatePageURL(settingsObj));
                //step 2 - login and validate
                loginPage.Login(dataObj.UserName, dataObj.Password);

                PO.HomePage homePage = new PO.HomePage(driver);
                Assert.True(homePage.ValidatePageURL(settingsObj));
                Assert.True(homePage.ValidateUserFirstName(dataObj.UserFirstName));
                Assert.True(homePage.ValidateUserLastName(dataObj.UserLastName));
                Assert.True(homePage.ValidateUserChannel(dataObj.Channel));
            }

            [Test]
            public void T0002_CompleteApplicationForEmprtyPaymentInformation()
            {
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to Complete Aplication
                PO.CA appPage = new PO.CA();

                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                appPage.OpenCA();
                //step 3 - validate PI section on CA screen
                //"Planned Modal Premium", 
                //"Cash with Application", 
                //"Payment Method", 
                //"Payment Mode"
                Assert.True(Label.IsLabelPresented("Planned Modal Premium"));
                Assert.True(Label.IsLabelPresented("Cash with Application"));
                Assert.True(Label.IsLabelPresented("Payment Method"));
                Assert.True(Label.IsLabelPresented("Payment Mode"));
            }

            [Test]
            public void T0003_CompleteApplicationForCashWithApplication()
            {
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to PI and validate
                PO.CA appPage = new PO.CA();
                PO.PI piPage = new PO.PI();
                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                piPage.OpenPI();
                piPage.ValidateCashAmount(string.Empty, string.Empty);
                //step 3 - set cash = yes and validate
                piPage.SetCash("Yes");
                piPage.ValidateCashAmount("Yes", string.Empty);
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - cash disappears 
                Assert.False(Label.IsLabelPresented("Cash with Application"));
                Assert.True(Label.IsLabelPresented("Amount"));
                //step 6 - go to PI, set cash = no and validate
                piPage.OpenPI();
                piPage.SetCash("No");
                piPage.ValidateCashAmount("No", string.Empty);
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - amount disappears  
                Assert.False(Label.IsLabelPresented("Cash with Application"));
                Assert.False(Label.IsLabelPresented("Amount"));
                //step 9 -  go to PI, set cash = yes, amount = 100 and validate 
                piPage.OpenPI();
                piPage.SetCash("Yes");
                piPage.SetAmount("100");
                piPage.ValidateCashAmount("Yes", "100");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - neither amount nor cash appears  
                Assert.False(Label.IsLabelPresented("Cash with Application"));
                Assert.False(Label.IsLabelPresented("Amount"));
                //step 12 -  go to PI, set cash = yes, amount = 100 and validate 
                piPage.OpenPI();
                piPage.SetCash("Yes");
                piPage.SetAmount("0");
                piPage.ValidateCashAmount("Yes", "0");
                //step 13 -  go to CA 
                appPage.OpenCA();
                //step 14 -  validate CA - neither amount nor cash appears  
                Assert.False(Label.IsLabelPresented("Cash with Application"));
                Assert.False(Label.IsLabelPresented("Amount"));
                //step 15 -  go to PI, set cash = yes, amount = 100 and validate 
                piPage.OpenPI();
                piPage.SetCash("Yes");
                piPage.SetAmount("");
                piPage.ValidateCashAmount("Yes", "");
                //step 16 -  go to CA 
                appPage.OpenCA();
                //step 17 -  validate CA - amount appears  
                Assert.False(Label.IsLabelPresented("Cash with Application"));
                Assert.True(Label.IsLabelPresented("Amount"));
            }

            [Test]
            public void T0004_CompleteApplicationForPlannedModalPremium()
            {
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to PI and validate
                PO.CA appPage = new PO.CA();
                PO.PI piPage = new PO.PI();
                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                piPage.OpenPI();
                piPage.ValidatePlannedModalPremium(string.Empty);
                //step 3 - enter 100 in PlannedModalPremium and validate
                piPage.SetPlannedMP("100");
                piPage.ValidatePlannedModalPremium("100");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - PlannedModalPremium disappears 
                Assert.False(Label.IsLabelPresented("Planned Modal Premium"));
                //step 6 - go to PI, set 0 in PlannedModalPremium and validate
                piPage.OpenPI();
                piPage.SetPlannedMP("0");
                piPage.ValidatePlannedModalPremium("0");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - PlannedModalPremium still disappears   
                Assert.False(Label.IsLabelPresented("Planned Modal Premium"));
                //step 9 -  go to PI, leave  PlannedModalPremium empty
                piPage.OpenPI();
                piPage.SetPlannedMP("");
                piPage.ValidatePlannedModalPremium("");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - PlannedModalPremium appears  
                Assert.True(Label.IsLabelPresented("Planned Modal Premium"));
                
            }

            [Test]
            public void T0005_CompleteApplicationForCardpaymentMethod()
            {
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to PI and validate
                PO.CA appPage = new PO.CA();
                PO.PI piPage = new PO.PI();
                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                piPage.OpenPI();
                //empty checkboxes = " "; empty textboxes = ""
                piPage.ValidateCardPaymentInformation("", "", "", "", "");
                //step 3 - set credit card
                piPage.SetPaymentMethod("Credit Card");
                piPage.ValidateCardPaymentInformation("Credit Card", "MasterCard", "", "", "");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - PaymentMethod disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(true, true, true);
                //step 6 - go to PI, set Card no
                piPage.OpenPI();
                piPage.SetCardNumber("1111222233334444");
                piPage.ValidateCardPaymentInformation("Credit Card", "MasterCard", "1111222233334444", "", "");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - account Number disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, true);
                //step 9 -  go to PI, set cardholdername 
                piPage.OpenPI();
                piPage.SetCardholder("Cardholder's Name");
                piPage.ValidateCardPaymentInformation("Credit Card", "MasterCard", "1111222233334444", "", "Cardholder's Name");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - Cardholder disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, false);
                //step 12 -  go to PI set invalid expiration date 
                piPage.OpenPI();
                piPage.SetCardNumber("11112222333344445");
                piPage.ValidateCardPaymentInformation("Credit Card", "MasterCard", "1111222233334444", "", "Cardholder's Name");
                //step 13 - set 12/16 in expired and validate  
                piPage.OpenPI();
                piPage.SetExpirationDate("12/16");
                piPage.ValidateCardPaymentInformation("Credit Card", "MasterCard", "1111222233334444", "12/2016", "Cardholder's Name");
                //step 14 - go to CA
                appPage.OpenCA();
                //step 15 -  validate CA - Expiration date disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 16 - set in PI expiration date in past and validate
                piPage.OpenPI();
                piPage.SetExpirationDate("12/91");
                piPage.ValidateCardPaymentInformation("Credit Card", "MasterCard", "1111222233334444", "12/1991", "Cardholder's Name");
                //step 17 - go to CA
                appPage.OpenCA();
                //step 18 -  validate CA - no fields appear  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 19 - empty PI fields, set credit card type to visa and validate
                piPage.OpenPI();
                piPage.SetCardNumber("");
                piPage.SetCardholder("");
                piPage.SetExpirationDate("");
                piPage.SetCardType("Visa");
                piPage.ValidateCardPaymentInformation("Credit Card", "Visa", "", "", "");
                //step 20 - go to CA
                appPage.OpenCA();
                //step 21 - validate CA
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(true, true, true);
                //step 22 - go to PI, set Card no
                piPage.OpenPI();
                piPage.SetCardNumber("1111222233334444");
                piPage.ValidateCardPaymentInformation("Credit Card", "Visa", "1111222233334444", "", "");
                //step 23 -  go to CA 
                appPage.OpenCA();
                //step 24 -  validate CA - account Number disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, true);
                //step 25 -  go to PI, set cardholdername 
                piPage.OpenPI();
                piPage.SetCardholder("Cardholder's Name");
                piPage.ValidateCardPaymentInformation("Credit Card", "Visa", "1111222233334444", "", "Cardholder's Name");
                //step 26 -  go to CA 
                appPage.OpenCA();
                //step 27 -  validate CA - Cardholder disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, false);
                //step 28 -  go to PI set invalid expiration date 
                piPage.OpenPI();
                piPage.SetCardNumber("11112222333344445");
                piPage.ValidateCardPaymentInformation("Credit Card", "Visa", "1111222233334444", "", "Cardholder's Name");
                //step 29 - set 12/16 in expired and validate  
                piPage.OpenPI();
                piPage.SetExpirationDate("12/16");
                piPage.ValidateCardPaymentInformation("Credit Card", "Visa", "1111222233334444", "12/2016", "Cardholder's Name");
                //step 30 - go to CA
                appPage.OpenCA();
                //step 31 -  validate CA - Expiration date disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 32 - set in PI expiration date in past and validate
                piPage.OpenPI();
                piPage.SetExpirationDate("12/91");
                piPage.ValidateCardPaymentInformation("Credit Card", "Visa", "1111222233334444", "12/1991", "Cardholder's Name");
                //step 33 - go to CA
                appPage.OpenCA();
                //step 34 -  validate CA - no fields appear  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 35 - empty PI fields, set credit card type to visa and validate
                piPage.OpenPI();
                piPage.SetCardNumber("");
                piPage.SetCardholder("");
                piPage.SetExpirationDate("");
                piPage.SetCardType("American Express");
                piPage.ValidateCardPaymentInformation("Credit Card", "American Express", "", "", "");
                //step 36 - go to CA
                appPage.OpenCA();
                //step 37 - validate CA
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(true, true, true);
                //step 38 - go to PI, set Card no
                piPage.OpenPI();
                piPage.SetCardNumber("1111222233334444");
                piPage.ValidateCardPaymentInformation("Credit Card", "American Express", "1111222233334444", "", "");
                //step 39 -  go to CA 
                appPage.OpenCA();
                //step 40 -  validate CA - account Number disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, true);
                //step 41 -  go to PI, set cardholdername 
                piPage.OpenPI();
                piPage.SetCardholder("Cardholder's Name");
                piPage.ValidateCardPaymentInformation("Credit Card", "American Express", "1111222233334444", "", "Cardholder's Name");
                //step 42 -  go to CA 
                appPage.OpenCA();
                //step 43 -  validate CA - Cardholder disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, false);
                //step 44 -  go to PI set invalid expiration date 
                piPage.OpenPI();
                piPage.SetCardNumber("11112222333344445");
                piPage.ValidateCardPaymentInformation("Credit Card", "American Express", "1111222233334444", "", "Cardholder's Name");
                //step 45 - set 12/16 in expired and validate  
                piPage.OpenPI();
                piPage.SetExpirationDate("12/16");
                piPage.ValidateCardPaymentInformation("Credit Card", "American Express", "1111222233334444", "12/2016", "Cardholder's Name");
                //step 46 - go to CA
                appPage.OpenCA();
                //step 47 -  validate CA - Expiration date disappears  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 48 - set in PI expiration date in past and validate
                piPage.OpenPI();
                piPage.SetExpirationDate("12/91");
                piPage.ValidateCardPaymentInformation("Credit Card", "American Express", "1111222233334444", "12/1991", "Cardholder's Name");
                //step 49 - go to CA
                appPage.OpenCA();
                //step 50 -  validate CA - no fields appear  
                Assert.False(Label.IsLabelPresented("Payment Method"));
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false); 
            }
            [Test]
            public void T0006_CompleteApplicationForBillPaymentMethod()
            {
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to PI and validate
                PO.CA appPage = new PO.CA();
                PO.PI piPage = new PO.PI();
                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                piPage.OpenPI();
                piPage.ValidatePaymentMethod("");
                //step 3 - Select DirectBilling
                piPage.SetPaymentMethod("Direct Billing");
                piPage.ValidatePaymentMethod("Direct Billing");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA  
                Assert.False(Label.IsLabelPresented("Payment Method"));
            }

            [Test]
            public void T0007_CompleteApplicationForBillPaymentMethod()
            {
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to PI and validate
                PO.CA appPage = new PO.CA();
                PO.PI piPage = new PO.PI();
                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                piPage.OpenPI();
                //empty checkboxes = " "; empty textboxes = ""
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - Select DirectBilling
                piPage.SetPaymentMethod("Bank Draft");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Bank Draft");
                piPage.ValidateBankDraftPaymentInformation("", "", "", "", " ", "Checking");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                //step 6 - set Financial Institution Name and validate
                piPage.OpenPI();
                piPage.SetFinancialInstitutionName("FinancialName");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Bank Draft");
                piPage.ValidateBankDraftPaymentInformation("FinancialName", "", "", "", " ", "Checking");
                //step 7 - go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - 
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                //step 9 - set Financial Institution Name and validate
                piPage.OpenPI();
                piPage.SetFinancialInstitutionName("FinancialName");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Bank Draft");
                piPage.ValidateBankDraftPaymentInformation("FinancialName", "", "", "", " ", "Checking");

            }

            [Test]
            public void T0009_ValidatePaymentInformationDropDowns()
            { 
                //step 1 - login
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.T0001_TestUserLogin();
                //step 2 - go to PI and validate
                PO.CA appPage = new PO.CA();
                PO.PI piPage = new PO.PI();
                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                piPage.OpenPI();
                


                driver.FindElement(By.Id("paymentMode")).Click();
                List<string> l = null;
                ComboBox.ValidateComboBoxContent(By.Id("paymentMode_dropdown"), l);

            }
        }
    
}
