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
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(10));
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
                PO.CreateApplication appPage = new PO.CreateApplication();

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
                PO.CreateApplication appPage = new PO.CreateApplication();

                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                appPage.OpenPI();
                //empty checkboxes = " "; empty textboxes = ""
                appPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - set cash = yes and validate
                appPage.SetCash("Yes");
                appPage.ValidateGeneralPaymentInformation("Yes", "", " ", "", " ");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - cash disappears 
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, true);
                //step 6 - go to PI, set cash = no and validate
                appPage.OpenPI();
                appPage.SetCash("No");
                appPage.ValidateGeneralPaymentInformation("No", "", " ", "", " ");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - amount disappears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, false);
                //step 9 -  go to PI, set cash = yes, amount = 100 and validate 
                appPage.OpenPI();
                appPage.SetCash("Yes");
                appPage.SetAmount("100");
                appPage.ValidateGeneralPaymentInformation("Yes", "100", " ", "", " ");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - neither amount nor cash appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, false);
                //step 12 -  go to PI, set cash = yes, amount = 100 and validate 
                appPage.OpenPI();
                appPage.SetCash("Yes");
                appPage.SetAmount("0");
                appPage.ValidateGeneralPaymentInformation("Yes", "0", " ", "", " ");
                //step 13 -  go to CA 
                appPage.OpenCA();
                //step 14 -  validate CA - neither amount nor cash appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, false, true, true, false);
                //step 15 -  go to PI, set cash = yes, amount = 100 and validate 
                appPage.OpenPI();
                appPage.SetCash("Yes");
                appPage.SetAmount("");
                appPage.ValidateGeneralPaymentInformation("Yes", "", " ", "", " ");
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
                PO.CreateApplication appPage = new PO.CreateApplication();

                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                appPage.OpenPI();
                //empty checkboxes = " "; empty textboxes = ""
                appPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 3 - enter 100 in PlannedModalPremium and validate
                appPage.SetPlannedMP("100");
                appPage.ValidateGeneralPaymentInformation(" ", "", " ", "100", " ");
                //step 4 - go to CA 
                appPage.OpenCA();
                //step 5 -  validate CA - PlannedModalPremium disappears 
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(false, true, true, true, false);
                //step 6 - go to PI, set 0 in PlannedModalPremium and validate
                appPage.OpenPI();
                appPage.SetPlannedMP("0");
                appPage.ValidateGeneralPaymentInformation(" ", "", " ", "0", " ");
                //step 7 -  go to CA 
                appPage.OpenCA();
                //step 8 -  validate CA - PlannedModalPremium still disappears   
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(false, true, true, true, false);
                //step 9 -  go to PI, leave  PlannedModalPremium empty
                appPage.OpenPI();
                appPage.SetPlannedMP("");
                appPage.ValidateGeneralPaymentInformation(" ", "", " ", "", " ");
                //step 10 -  go to CA 
                appPage.OpenCA();
                //step 11 -  validate CA - PlannedModalPremium appears  
                appPage.ValidateGeneralMandatoryFieldsListedOnCA(true, true, true, true, false);
                
            }

            
        }
    
}
