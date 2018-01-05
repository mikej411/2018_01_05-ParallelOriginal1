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
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    // If you dont want a class to be tested in parallel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class RCP_CBD_ProgAdminWorkflow_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_ProgAdminWorkflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_ProgAdminWorkflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties
        /// <summary>
        /// Use these properties inside your test methods when calling <see cref="UserUtils.CreateAndRegisterUser(null, UserUtils.User, UserUtils.UserRole)"/>
        /// </summary>
        public List<UserInfo> LRUsers;
        public List<UserInfo> OBUsers;
        public List<UserInfo> PAUsers;
        public UserInfo LRUser;
        public UserInfo OBUser;
        public UserInfo PAUser;
        #endregion properties

        #region testfixtures
        /// <summary>
        /// This method will run for every test in this class. If any of the below users were created, it wil delete them.
        /// TODO: Refactor this so that it gets called at the entire UITest project level. So ill have to move it to a new
        /// class maybe, or something else. Note that parallel testing may cause this to not work properly as tests may 
        /// share the same thread with parallel testing
        /// </summary>
        //[TestFixtureTearDown]
        public void DeleteUserIfCreated()
        {
            foreach (var learner in LRUsers)
            {
                UserUtils.DeleteUser(learner.Username);
            }
            foreach (var learner in OBUsers)
            {
                UserUtils.DeleteUser(learner.Username);
            }
            foreach (var learner in PAUsers)
            {
                UserUtils.DeleteUser(learner.Username);
            }
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
        [Description("Workflow test making sure nothing goes wrong when a Program Admin assigns an MSF Observation to 2 observers")]
        [Property("Status", "Complete")]
        [Property("ToDo", "For now we are using static users. When get the Delete User API, we should use the New User API because if not, the " +
            "Multiple Source Feedback Asignment table will get huge. Also when we get that API, we should delete these static users and hopefully" +
            "their MSFs will get deleted")]
        [Author("Mike Johnston")]
        public void ProgAdminCanAssignMSFObservation()
        {
            /// 1. Login as a program admin
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgAdminPage PAP = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");

            /// 2. Choose a learner, open the Assign Observation form, fill it out, choose 2 observers, then click Assign
            PAP.AssignObservation(UserUtils.Learner1FullName,
                "Multiple Source Feedback",
                "2 - Foundations of Discipline", 
                "2.20 Managing common complications of labour analgesia",
                "Part B: Multisource feedback - Form 3",
                UserUtils.Observer1FullName,
                UserUtils.Observer2FullName);
        }

        [Test]
        [Description("Workflow test make sure nothing go worng when we add supporting documents from the learner tab")]
        [Property("Status", "In Progress")]
        [Author("Lakshmi")]
        public void ProgAdminCanAddSupportingDocuments()
        {
            /// 1. Login
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgAdminPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");
            PA.AddSupportDocumentation(UserUtils.Learner1FullName, "C:\\Myfolder");
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we add notes to a learner from the learner tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgAdminCanAddNotes()
        {
            /// 1. Login as a program admin
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgAdminPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Add Notes, add some notes
            /// and click Submit
            PA.AddNotes(UserUtils.Learner1FullName, "C:\\Myfolder");
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we schedule a progress meeting with a learner from the learner tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgAdminCanSchedProgMeet()
        {
            /// 1. Login as a program admin
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgAdminPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Schedule Progress Meeting, fill in 
            /// all of the fields and click Submit
            PA.ScheduleProgressMeeting(UserUtils.Learner1FullName);
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we add and also remove a flag from the learner table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgAdminCanAddAndRemoveFlag()
        {
            /// 1. Login as a program admin
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgAdminPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Add/Remove Flag, 
            /// fill in all of the fields and click Save Flag
            PA.AddFlag(UserUtils.Learner1FullName);

            /// 3. Choose a learner in the learners table, click on the Actions button, click on Add/Remove Flag,
            /// fill in all of the fields and click Remove Flag
            PA.RemoveFlag(UserUtils.Learner1FullName);
        }

        //[Test]
        [Description("Workflow test making sure nothing goes wrong when a Program Admin removes an Observation from the Program Affiliated Observers tab")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void ProgAdminCanRemoveObservation()
        {
        //    /// 1. Login as a program admin
        //    LoginPage LP = Navigation.GoToLoginPage(browser);
        //    CBDProgAdminPage PAP = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");

        //    /// 2. Choose a learner, open the Assign Observation form, fill it out, choose an observer, then click Assign
        //    PAP.AssignObservation(UserUtils.Learner1Login,
        //        "Multiple Source Feedback",
        //        "2 - Foundations of Discipline", "2.20 Managing common complications of labour analgesia",
        //        "Part B: Multisource feedback - Form 3"
        //        , UserUtils.Observer2FullName);

        ///// 3. Click on the Program Affiliated Observers tab, click the number link under the Pending Observations tab for the observer,
        ///// click the Actions button on the popup, then select Remove
        //  PAP.RemindOrRemoveObservation(UserUtils.Observer2FullName, UserUtils.Learner1FullName, "EPA 2.20", "Remove");
    }



        #endregion Tests
    }
}






