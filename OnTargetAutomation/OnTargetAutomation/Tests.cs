using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace OnTargetAutomation
{
    class Tests
    {
        [TestFixture]
        public class TestCases
        {
            public static IWebDriver driver;
            public TestSettings settingsObj;
            public TestData dataObj;
            [SetUp]
            public void Setup()
            {
                driver = new FirefoxDriver();
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(55));
                settingsObj = new TestSettings();
            }

            [TearDown]
            public void TearDown()
            {
                driver.Quit();
            }

            [Test]
            public void TestUserLogin()
            {
                dataObj = new TestData("TestUserLogin");
                driver.Navigate().GoToUrl(settingsObj.Environment);
                PO.LoginPage loginPage = new PO.LoginPage(driver);
                Assert.True(loginPage.ValidatePageURL(settingsObj));
                loginPage.Login(dataObj.UserName, dataObj.Password);

                PO.HomePage homePage = new PO.HomePage(driver);
                Assert.True(homePage.ValidatePageURL(settingsObj));
                Assert.True(homePage.ValidateUserFirstName(dataObj.UserFirstName));
                Assert.True(homePage.ValidateUserLastName(dataObj.UserLastName));
                Assert.True(homePage.ValidateUserChannel(dataObj.Channel));
            }

            [Test]
            public void CompleteApplicationForEmprtyPaymentInformation()
            {
                
                driver.Navigate().GoToUrl(settingsObj.Environment);
                this.TestUserLogin();

                PO.CreateApplication appPage = new PO.CreateApplication(driver);

                dataObj = new TestData("CompleteApplicationForEmprtyPaymentInformation");
                appPage.CreateNewAppNewUser(dataObj);
                string[] list = { "Planned Modal Premium",                                     "Cash with Application",                                     "Payment Method", 
                                    "Payment Mode" };
                appPage.ValidateCompleteApplication(dataObj, list);

            }
        }
    }
}
