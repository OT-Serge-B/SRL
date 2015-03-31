using System;
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
                settingsObj = new TestSettings();
            }

            [TearDown]
            public void TearDown()
            {
                driver.Quit();
            }

            [Test]
            public void TestUserLogin(/*IWebDriver driver*/)
            {
                //driver.Navigate().GoToUrl("http://stoneriverdev.lifeportraits.com/lifeportraits.aspx");
                dataObj = new TestData("TestUserLogin");
                driver.Navigate().GoToUrl(settingsObj.Environment);
                PO.LoginPage loginPage = new PO.LoginPage(driver);
                Assert.True(loginPage.ValidatePageURL(settingsObj));
                loginPage.Login(dataObj.UserName, dataObj.Password);

                PO.LifePortraits lifePortraits = new PO.LifePortraits(driver);
                Assert.True(lifePortraits.ValidatePageURL(settingsObj));
                Assert.True(lifePortraits.ValidateUserFirstName(dataObj.UserFirstName));
                Assert.True(lifePortraits.ValidateUserLastName(dataObj.UserLastName));
                Assert.True(lifePortraits.ValidateUserChannel(dataObj.Channel));
            }
        }
    }
}
