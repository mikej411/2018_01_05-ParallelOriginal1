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
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    // If you dont want a class to be tested in parallel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class RCP_CBD_LearnerWorkflow_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_LearnerWorkflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_LearnerWorkflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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
        [Description("Workflow test making sure nothing goes wrong when a learner adds a reflection")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void LearnerCanAddReflection()
        {            
            /// 1. Login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, UserUtils.Learner1Login, "test");

            /// 2. Add a reflection
            LearnerRelectionObject LR = CLP.AddReflection();
        }

        [Test]
        [Description("Complete: Workflow test making sure nothing goes wrong when a learner submits multiple requests for observation")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void LearnerCanRequestObservation()
        {
            /// 1. Login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, UserUtils.Learner1Login, "test");

            /// 2. Request an observation for a specific EPA
            CLP.RequestObservationForEPA("Transition to Discipline",
                "Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
                UserUtils.Observer1FullName, "Part A: Direct observation - Form 1");

            /// 3. Request an observation again for that EPA
            CLP.RequestObservation(UserUtils.Observer1FullName, "Part A: Direct observation - Form 1");
        }

        #endregion Tests
    }
}






