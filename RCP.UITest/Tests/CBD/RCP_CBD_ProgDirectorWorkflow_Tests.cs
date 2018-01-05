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
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace RCP.UITest
{
    [BrowserMode(BrowserMode.New)]
    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    [RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    // If you dont want a class to be tested in parallel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class RCP_CBD_ProgDirectorWorkflow_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_ProgDirectorWorkflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_ProgDirectorWorkflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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

        /// <summary>
        /// These properties represent the usernames for static users in the database that we use in certain test methods when we want to run tests in 
        /// parallel across browsers where we otherwise couldnt do that because using a single user would cross paths per browser
        /// </summary>
        public string userLoginPerBrowser { get; set; }
        public string userFullNamePerBrowser { get; set; }
        #endregion properties

        #region testfixtures
        /// Using this to store usernames into variables per browser
        /// </summary>
        [OneTimeSetUp]
        public void setup()
        {
            // First assign the learner username per browser. This is so that we can run these tests in parallel. Note that this test still may fail
            // when running in parallel, see the comment above the NonParallelizable attribute above. If this happens.
            if (BrowserName == BrowserNames.Chrome) { userLoginPerBrowser = UserUtils.LearnerCH1Login; userFullNamePerBrowser = "_TA_AStatic User_LR_CH_001"; }
            if (BrowserName == BrowserNames.InternetExplorer) { userLoginPerBrowser = "UserLogin_TA_AStaticUser_IE_IE_001"; userFullNamePerBrowser = UserUtils.LearnerIE1FullName; }
            if (BrowserName == BrowserNames.Firefox) { userLoginPerBrowser = UserUtils.LearnerFF1Login; userFullNamePerBrowser = UserUtils.LearnerFF1FullName; }
        }

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
        [Description("Workflow test make sure nothing go worng when we add supporting documents from the learner tab")]
        [Property("Status", "In Progress")]
        [Author("Lakshmi")]
        public void ProgDirectorCanAddSupportingDocuments()
        {
            /// 1. Login as a program director, choose a learner, click the Actions menu item, then the Add Supporting Documentation
            /// button, fill in the form and click Submit
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, "test");
            PA.AddSupportDocumentation(UserUtils.Learner1FullName, "C:\\Myfolder");
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we add notes to a learner from the learner tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgDirectorCanAddNotes()
        {
            /// 1. Login as a Program Director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, "test");

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Add Notes, add some notes
            /// and click Submit
            PA.AddNotes(UserUtils.Learner1FullName, "C:\\Myfolder");
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we schedule a progress meeting with a learner from the learner tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgDirectorCanSchedProgMeet()
        {
            /// 1. Login as a Program Director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, "test");

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Schedule Progress Meeting, fill in 
            /// all of the fields and click Submit
            PA.ScheduleProgressMeeting(UserUtils.Learner1FullName);
        }

        [Test]
        [Description("Workflow test make sure nothing go wrong when we add and also remove a flag from the learner table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ProgDirectorCanAddAndRemoveFlag()
        {
            /// 1. Login as a Program Director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PA = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, "test");

            /// 2. Choose a learner in the learners table, click on the Actions button, click on Add/Remove Flag, 
            /// fill in all of the fields and click Save Flag
            PA.AddFlag(UserUtils.Learner1FullName);

            /// 3. Choose a learner in the learners table, click on the Actions button, click on Add/Remove Flag,
            /// fill in all of the fields and click Remove Flag
            PA.RemoveFlag(UserUtils.Learner1FullName);
        }

        [Test]
        [Description("Workflow test making sure nothing goes wrong when a prog director creates, edits a learner and finalizes an agenda")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        [Category("test1")]
        // This cant be run in parallel across browsers because agendas in the Meeting Agenda dropdown may get mixed up between parallel runs
        [NonParallelizable]
        public void ProgDirectorCanCreateEditAndFinalizeAgenda()
        {
            /// 1. Login as a program director
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgDirectorPage PP = LP.LoginAsExistingUser(UserUtils.UserRole.PD, UserUtils.ProgDirector1Login, "test");

            /// 2. Create an upcoming agenda, then choose this agenda in the Meeting Agenda dropdown
            /// 4. Click on the Set Status button and then set the status of the learner to stage 23
            /// 5. Finalize the Agenda
            PP.CreateChangeAndFinalizeAgendaForLearner(userFullNamePerBrowser, "Reviewed", "High", "_TA_AStatic User_CC_001", "Progressing as Expected",
                "Promote Learner - to Stage 3");

            // Cleanup data by setting the learner back to stage 2
            PP.CreateChangeAndFinalizeAgendaForLearner(userFullNamePerBrowser, null, null, null, "Progressing as Expected",
                "Promote Learner - to Stage 2");
        }

        #endregion Tests
    }
}






