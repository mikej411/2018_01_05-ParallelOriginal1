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
using System.Diagnostics;
using OpenQA.Selenium.Firefox;

namespace RCP.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
   // [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    // If you dont want a class to be tested in parallel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class RCP_CBD_ObserverWorkflow_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_ObserverWorkflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_ObserverWorkflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties
        /// <summary>
        /// Use these properties inside your test methods when calling <see cref="UserUtils.CreateAndRegisterUser(null, UserUtils.User, UserUtils.UserRole)"/>
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
            }
            if (OBUser != null)
            {
                UserUtils.DeleteUser(OBUser.Username);
            }
            if (PAUser != null)
            {
                UserUtils.DeleteUser(PAUser.Username);
            }
        }
        #endregion testfixtures

        #region Tests
        [Test]
        [Description("Workflow test making sure nothing goes wrong when an Observer completes an observation assessment")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ObserverCanCompleteObservation()
        {
            
            /// 1. Create learner and observer users and login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LRUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);
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

            /// 4. Accept the Pending observation request
            OP.AcceptOrDeclineAssignment(LRUser.FullName, "Part A: Direct observation - Form 1", "Accept");

            /// 5. Open the Complete Assessment form for the observation request and complete the observation
            OP.CompleteAssessment(LRUser.FullName, "Part A: Direct observation - Form 1");
        }

        [Test]
        [Description("Workflow for an Observer adding his own observation from the Add Observation window without a request from the learner")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ObserverCanAddObs()
        {
            // TEMPORARY: When the Delete User API is developed, we can remove the below code and uncomment the code below it, which creates new users
            // on the fly. Decide this when the delete API is complete
            /// 1. Create learner and observer user and login as the observer
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDObserverPage OP = LP.LoginAsExistingUser(UserUtils.UserRole.OB, UserUtils.Observer1Login, "test");

            /// 2. Add and complete an observation from the Add Observation form
            AddedObservationInfo addedObservation = OP.AddObservation(UserUtils.Learner1FullName, "EPA/IM Observation", "1 - Transition to Discipline",
            "1.1 Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
            "Part A: Direct observation - Form 1");


            ///// 1. Create learner and observer user and login as the observer
            //LoginPage LP = Navigation.GoToLoginPage(browser);
            //LRUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //OBUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.OB);
            //CBDObserverPage OP = LP.LoginAsNewUser(UserUtils.UserRole.OB, OBUser.Username, OBUser.Password);

            ///// 2. Add and complete an observation from the Add Observation form
            //AddedObservationInfo addedObservation = OP.AddObservation(LRUser.FullName, "EPA/IM Observation", "1 - Transition to Discipline",
            //"1.1 Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure", "Part A: Direct observation - Form 1");
        }



        #endregion Tests
    }
}






