using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Globalization;

namespace RCP.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    public class RCP_CBD_ObserverUIAsserts_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_ObserverUIAsserts_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_ObserverUIAsserts_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties
        /// <summary>
        /// Use these properties inside your test methods when calling <see cref="UserUtils.CreateAndRegisterUser(string, UserUtils.User, UserUtils.UserRole)"/>
        /// </summary>
        public UserInfo LRUser;
        public UserInfo OBUser;
        public UserInfo PAUser;
        
        #endregion properties

        #region testfixtures
        /// <summary>
        /// This method will run for every test in this class. If any of the below users were created, it wil delete them.
        /// TODO: Refactor this so that it gets called at the entire UITest project level. So ill have to move it to a new
        /// class maybe, or something else.
        /// </summary>
        //[TestFixtureTearDown]
        public void DeleteUserIfCreated()
        {
            if (LRUser != null)
            {
                UserUtils.DeleteUser(LRUser.Username);
                LRUser = null;               
            }
            if (OBUser != null)
            {
                UserUtils.DeleteUser(OBUser.Username);
                OBUser = null;
            }
            if (PAUser != null)
            {
                UserUtils.DeleteUser(PAUser.Username);
                PAUser = null;
            }
        }
        #endregion testfixtures

        #region Tests
        [Test]
        [Description("The Pending Acceptance table on the Observer page gets updated with a new request after a learner submits it")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ObserverGetsObservationRequestFromLearner()
        {
            // TEMPORARY: When the Delete User API is developed, we can remove the below code and uncomment the code below it, which creates new users
            // on the fly. Decide this when the delete API is complete
            /// 1. Login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, UserUtils.Learner1Login, "test");

            /// 2. Request an observation from the observer
            CLP.RequestObservationForEPA("Transition to Discipline",
                "Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
                UserUtils.Observer1FullName, "Part A: Direct observation - Form 1");

            /// 3. Log out and then log in as the observer that the learner requested
            CLP.ClickAndWait(CLP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            CBDObserverPage OP = LP.LoginAsExistingUser(UserUtils.UserRole.OB, UserUtils.Observer1Login, "test");

            /// 4. Check that the observation that was just created is now in the Pending Acceptance table
            Assert.True(ElemGet.Grid_ContainsRecord(browser,  
                OP.PendingAcceptanceTbl, Bys.CBDObserverPage.PendingAcceptanceTblRowBody, UserUtils.Learner1FullName, "td"), 
                "The observation that was requested from the learner is not showing in the Observer's Pending Acceptance table");



            ///// 1. Create learner and observer users and login as the learner
            //LoginPage LP = Navigation.GoToLoginPage(browser);
            //LRUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //OBUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.OB);
            //CBDLearnerPage CLP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            ///// 2. Request an observation from the observer
            //CLP.RequestObservationForEPA(CLP.EPAIMTbl, "Transition to Discipline",
            //    "Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
            //    OBUser.FullName, "Part A: Direct observation - Form 1");

            ///// 3. Log out and then log in as the observer that the learner requested
            //CLP.ClickAndWait(CLP.LogoutLnk);
            //Navigation.GoToLoginPage(browser);
            //CBDObserverPage OP = LP.LoginAsNewUser(UserUtils.UserRole.OB, OBUser.Username, OBUser.Password);

            ///// 4. Check that the observation that was just created is now in the Pending Acceptance table
            //Assert.True(ElemGet.Grid_ContainsRecord(browser, OP.PendingAcceptanceTbl, LRUser.FullName, null));
        }

        [Test]
        [Description("When an observer declines an assessment, then the Expired/Declined table populates with this assessment. Then when" +
            " an observer clicks Remove in the Declined/Expired table, the assessment is removed from that table and appears in the Archived table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void DeclinedExpiredTableUpdatesAfterObserverMakesChanges()
        {
            /// 1. Create learner and observer users and login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            // We are using a try catch here for the following reason. This test sometimes is the very first test that gets run during the build. For some reason, the
            // RegisterUser API fails to complete and throws a 500 error whenever the first test gets set off on the grid. So if the error happens, then we will 
            // just call the API again in the Catch block and proceed
            UserInfo LRUser = null;
            try
            {
                LRUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);
            }
            catch
            {
                LRUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);
            }
            OBUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.OB);
            CBDLearnerPage CLP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 2. Request an observation from the observer
            CLP.RequestObservationForEPA("Transition to Discipline",
                "Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
                OBUser.FullName, "Part A: Direct observation - Form 1");

            /// 3. Log out and then log in as the observer that the learner requested
            CLP.ClickAndWait(CLP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            CBDObserverPage OP = LP.LoginAsNewUser(UserUtils.UserRole.OB, OBUser.Username, OBUser.Password);

            /// 4. Decline the Pending observation request
            OP.AcceptOrDeclineAssignment(LRUser.FullName, "Part A: Direct observation - Form 1", "Decline");
            OP.ExpandTable(OP.ExpiredDeclinedTblHdr, "", "expand");
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, OP.ExpiredDeclinedTbl, Bys.CBDObserverPage.ExpiredDeclinedTblRowBody, LRUser.FullName, "td"));

            /// 5. Remove the declined request and verify that the table disappears. The table should disappear at this point 
            /// because there was only 1 declined request to begin with
            OP.RemoveExpiredDeclinedRequest(LRUser.FullName, "Part A: Direct observation - Form 1");
            Assert.True(browser.FindElements(Bys.CBDObserverPage.ExpiredDeclinedTblRowBody).Count == 0);

            /// 6. Switch to the Archived Observations tab and verify that the request shows there
            OP.SwitchToTab(OP.ArchivedObsTab);
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, OP.ArchivedObservationsTbl, Bys.CBDObserverPage.ArchivedObservationsTblRowBody, LRUser.FullName, "td"));
        }

        /// <summary> 
        /// Verifies that after multiple observations are made, the EPA Observation Over Time graph is returning the correct data </summary>
        /// Author: Mike Johnston
        /// Status: In Progress.  Will need the New User utility for this to be effective
        ////[Test]
        public void ObvOvrTimeGraphContainsCorrectData()
        {
        }


        //[Test]
        //public void sandboxtest()
        //{
        //    browser.Navigate().GoToUrl("https://www.google.com");
        //    Thread.Sleep(2000);
        //}


        #endregion Tests
    }
}

