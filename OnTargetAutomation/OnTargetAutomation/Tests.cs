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
                //driver.Quit();
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
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, true, true, false);

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
                //empty checkboxes = " "; empty textboxes = ""
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - set cash = yes and validate
                piPage.SetCash("Yes");
                piPage.ValidateGeneralPaymentInformation("Yes", "", " ", "", " ");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - cash disappears 
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, true);
                //step 6 - go to PI, set cash = no and validate
                piPage.OpenPI();
                piPage.SetCash("No");
                piPage.ValidateGeneralPaymentInformation("No", "", " ", "", " ");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - amount disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, false);
                //step 9 -  go to PI, set cash = yes, amount = 100 and validate 
                piPage.OpenPI();
                piPage.SetCash("Yes");
                piPage.SetAmount("100");
                piPage.ValidateGeneralPaymentInformation("Yes", "100", " ", "", " ");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - neither amount nor cash appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, false);
                //step 12 -  go to PI, set cash = yes, amount = 100 and validate 
                piPage.OpenPI();
                piPage.SetCash("Yes");
                piPage.SetAmount("0");
                piPage.ValidateGeneralPaymentInformation("Yes", "0", " ", "", " ");
                //step 13 -  go to CA 
                appPage.OpenCA();
                //step 14 -  validate CA - neither amount nor cash appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, false);
                //step 15 -  go to PI, set cash = yes, amount = 100 and validate 
                piPage.OpenPI();
                piPage.SetCash("Yes");
                piPage.SetAmount("");
                piPage.ValidateGeneralPaymentInformation("Yes", "", " ", "", " ");
                //step 16 -  go to CA 
                appPage.OpenCA();
                //step 17 -  validate CA - amount appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, true); 
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
                //empty checkboxes = " "; empty textboxes = ""
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - enter 100 in PlannedModalPremium and validate
                piPage.SetPlannedMP("100");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "100", " ");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - PlannedModalPremium disappears 
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(false, true, true, true, false);
                //step 6 - go to PI, set 0 in PlannedModalPremium and validate
                piPage.OpenPI();
                piPage.SetPlannedMP("0");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "0", " ");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - PlannedModalPremium still disappears   
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(false, true, true, true, false);
                //step 9 -  go to PI, leave  PlannedModalPremium empty
                piPage.OpenPI();
                piPage.SetPlannedMP("");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - PlannedModalPremium appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, true, true, false);
                
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
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - set credit card
                piPage.SetPaymentMethod("Credit Card");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("MasterCard", "", "", "");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - PaymentMethod disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(true, true, true);
                //step 6 - go to PI, set Card no
                piPage.OpenPI();
                piPage.SetCardNumber("1111222233334444");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("MasterCard", "1111222233334444", "", "");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - account Number disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, true);
                //step 9 -  go to PI, set cardholdername 
                piPage.OpenPI();
                piPage.SetCardholder("Cardholder's Name");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("MasterCard", "1111222233334444", "", "Cardholder's Name");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - Cardholder disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, false);
                //step 12 -  go to PI set invalid expiration date 
                piPage.OpenPI();
                piPage.SetExpirationDate("0");
                //step 13 
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("MasterCard", "1111222233334444", "", "Cardholder's Name");
                //step 14 -  validate CA - neither amount nor cash appears  
                piPage.OpenPI();
                piPage.SetExpirationDate("12/16");
                //step 15 - go to CA
                appPage.OpenCA();
                //step 16 -  validate CA - Cardholder disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 17 -  validate PI
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("MasterCard", "1111222233334444", "12/2016", "Cardholder's Name");
                //step 18 - set in PI expiration date in past
                piPage.OpenPI();
                piPage.SetExpirationDate("12/91");
                //step 19 - go to CA
                appPage.OpenCA();
                //step 20 -  validate CA - no fields appear  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 21 - validate PI
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("MasterCard", "1111222233334444", "12/1991", "Cardholder's Name");
                //step 22 - empty PI fields, set creditcard type to visa
                piPage.OpenPI();
                piPage.SetCardNumber("");
                piPage.SetCardholder("");
                piPage.SetExpirationDate("");
                piPage.SetCardType("Visa");
                //step 23 - go to CA
                appPage.OpenCA();
                //step 24 - validate CA
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(true, true, true);
                //step 25 - go to PI, set Card no
                piPage.OpenPI();
                piPage.SetCardNumber("1111222233334444");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("Visa", "1111222233334444", "", "");
                //step 26 -  go to CA 
                appPage.OpenCA();
                //step 27 -  validate CA - account Number disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, true);
                //step 28 -  go to PI, set cardholdername 
                piPage.OpenPI();
                piPage.SetCardholder("Cardholder's Name");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("Visa", "1111222233334444", "", "Cardholder's Name");
                //step 29 -  go to CA 
                appPage.OpenCA();
                //step 30 -  validate CA - Cardholder disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, false);
                //step 31 -  go to PI set invalid expiration date 
                piPage.OpenPI();
                piPage.SetExpirationDate("0");
                //step 32 
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("Visa", "1111222233334444", "", "Cardholder's Name");
                //step 33 -  validate CA - neither amount nor cash appears  
                piPage.OpenPI();
                piPage.SetExpirationDate("12/16");
                //step 34 - go to CA
                appPage.OpenCA();
                //step 35 -  validate CA - Cardholder disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 36 -  validate PI
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("Visa", "1111222233334444", "12/2016", "Cardholder's Name");
                //step 37 - set in PI expiration date in past
                piPage.OpenPI();
                piPage.SetExpirationDate("12/91");
                //step 38 - go to CA
                appPage.OpenCA();
                //step 39 -  validate CA - no fields appear  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 40 - validate PI
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("Visa", "1111222233334444", "12/1991", "Cardholder's Name");
                //step 41 - empty PI fields, set creditcard type to visa
                piPage.OpenPI();
                piPage.SetCardNumber("");
                piPage.SetExpirationDate("");
                piPage.SetCardholder("");
                piPage.SetCardType("American Express");
                //step 42 - go to CA
                appPage.OpenCA();
                //step 43 - validate CA
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(true, true, true);
                //step 44 - go to PI, set Card no
                piPage.OpenPI();
                piPage.SetCardNumber("1111222233334444");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("American Express", "1111222233334444", "", "");
                //step 45 -  go to CA 
                appPage.OpenCA();
                //step 46 -  validate CA - account Number disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, true);
                //step 47 -  go to PI, set cardholdername 
                piPage.OpenPI();
                piPage.SetCardholder("Cardholder's Name");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("American Express", "1111222233334444", "", "Cardholder's Name");
                //step 48 -  go to CA 
                appPage.OpenCA();
                //step 49 -  validate CA - Cardholder disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, true, false);
                //step 50 -  go to PI set invalid expiration date 
                piPage.OpenPI();
                piPage.SetExpirationDate("0");
                //step 51 
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("American Express", "1111222233334444", "", "Cardholder's Name");
                //step 52 -  validate CA - neither amount nor cash appears  
                piPage.OpenPI();
                piPage.SetExpirationDate("12/16");
                //step 53 - go to CA
                appPage.OpenCA();
                //step 54 -  validate CA - Cardholder disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 55 -  validate PI
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("American Express", "1111222233334444", "12/2016", "Cardholder's Name");
                //step 56 - set in PI expiration date in past
                piPage.OpenPI();
                piPage.SetExpirationDate("12/91");
                //step 57 - go to CA
                appPage.OpenCA();
                //step 58 -  validate CA - no fields appear  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
                appPage.ValidateCardMandatoryFieldsListedOnCA(false, false, false);
                //step 59 - validate PI
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Credit Card");
                piPage.ValidateCardPaymentInformation("American Express", "1111222233334444", "12/1991", "Cardholder's Name");


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
                //empty checkboxes = " "; empty textboxes = ""
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - Select DirectBilling
                piPage.SetPaymentMethod("Direct Billing");
                piPage.ValidateGeneralPaymentInformation(" ", "", " ", "", "Direct Billing");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - cash disappears 
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, false, true, false);
            }
        }
    
}
