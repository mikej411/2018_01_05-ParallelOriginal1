using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;

namespace RCP.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class RCP_General_Tests : TestBase
    {
        #region Constructors
        public RCP_General_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_General_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        // Example of how to override the teardown at the test class level
        //public override void TearDown() 
        //{
        //    Browser.Manage().Window.Size = new System.Drawing.Size(1040, 784);
        //    CleanupBrowser();
        //}

        #region Tests

        [Test]
        [Description("Verifying everything within the login page. Any validations that are in place, etc. It also tests that a user is able to login successfully")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TestLoginPage()
        {
            /// 1. Navigate to the login page
            LoginPage LP = Navigation.GoToLoginPage(browser);

            /// 2. Click the login button without entering in the username or password, and then verify the warning messages
            LP.LoginBtn.Click();
            browser.WaitForElement(Bys.LoginPage.UserNameWarningLbl, ElementCriteria.IsVisible);
            Assert.AreEqual("Please enter your username.", LP.UserNameWarningLbl.Text);
            // Firefox produces different RGB values, so just commenting out for now. Will revisit and add code for FF later
            if (BrowserName == BrowserNames.InternetExplorer || BrowserName == BrowserNames.Chrome)
            {
                Assert.True(AssertUtils.VerifyLabel(browser, LP.UserNameWarningLbl, "Please enter your username.", "rgba(255, 0, 0, 1)"),
                    "The label's text, display property, or CSS color value is not correct");
            }

            /// 3. Enter text in the required fields and verify the warning messages disappear
            LP.UserNameTxt.SendKeys("Not a valid user");
            LP.PasswordTxt.SendKeys("blah");
            LP.PasswordTxt.SendKeys(Keys.Tab);
            
            // NOTE that the following is a limitation of IE and selenium. Tabbing after entering text doesnt register
            // an event, so the warning message doesnt go away. Commenting this out in IE. 
            // Also Firefoxes current Geckodriver is having inconsistency issues with this assert as well
            if (BrowserName == BrowserNames.Chrome)
            {
                Thread.Sleep(0400);
                // Two ways to check that the warning message disappeared
                // Through selenium:
                Assert.False(LP.UserNameWarningLbl.Displayed);
                // Or through my custom extension method
                Assert.False(Browser.Exists(Bys.LoginPage.UserNameWarningLbl, ElementCriteria.IsVisible));

                /// 4. The user above does not exist, so click the Login and verify the system warns the user
                LP.PasswordTxt.SendKeys(Keys.Enter);
                browser.WaitForElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl, ElementCriteria.IsVisible);
                // Firefox produces different RGB values, so just commenting out for now. Will revisit and add code for FF later
                if (BrowserName == BrowserNames.InternetExplorer || BrowserName == BrowserNames.Chrome)
                {
                    Assert.True(AssertUtils.VerifyLabel(browser, LP.LoginUnsuccessfullWarningLbl,
                    "Your login attempt was not successful. Please try again.", "rgba(255, 0, 0, 1)"));
                }
            }

            /// 5. Login with a valid user
            LP.UserNameTxt.Clear();
            LP.PasswordTxt.Clear();
            LP.UserNameTxt.SendKeys(UserUtils.Learner1FullName);
            LP.PasswordTxt.SendKeys("test");
            LP.PasswordTxt.SendKeys(Keys.Tab);
            LP.ClickAndWait(LP.LoginBtn);
        }
  
        #endregion Tests
    }
}

