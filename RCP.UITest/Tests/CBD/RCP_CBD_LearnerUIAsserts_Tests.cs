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
    // If you dont want a class to be tested in parrelel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class RCP_CBD_LearnerUIAsserts_Tests : TestBase
    {
        #region Constructors
        public RCP_CBD_LearnerUIAsserts_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_CBD_LearnerUIAsserts_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
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

        /// <summary>
        /// These properties represent the usernames for static users in the database that we use in certain test methods when we want to run tests in 
        /// parallel across browsers where we otherwise couldnt do that because using a single user would cross paths per browser
        /// </summary>
        public string learnerLoginPerBrowser { get; set; }
        public string learnerFullNamePerBrowser { get; set; }
        #endregion properties

        #region testfixtures
        /// <summary>
        /// Using this to store usernames into variables per browser
        /// </summary>
        [OneTimeSetUp]
        public void setup()
        {
            // First assign the learner username per browser. This is so that we can run these tests in parallel. Note that this test still may fail
            // when running in parallel, see the comment above the NonParallelizable attribute above. If this happens.
            if (BrowserName == BrowserNames.Chrome)
            { learnerLoginPerBrowser = UserUtils.LearnerCH1Login; learnerFullNamePerBrowser = UserUtils.LearnerCH1FullName; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { learnerLoginPerBrowser = UserUtils.LearnerIE1Login; learnerFullNamePerBrowser = UserUtils.LearnerIE1FullName; }
            if (BrowserName == BrowserNames.Firefox)
            { learnerLoginPerBrowser = UserUtils.LearnerFF1Login; learnerFullNamePerBrowser = UserUtils.LearnerFF1FullName; }
        }

        /// <summary>
        /// This method will run for every test in this class. If any of the below users were created, it wil delete them.
        /// TODO: Refactor this so that it gets called at the entire UITest project level. So ill have to move it to a new
        /// class maybe, or something else. NOTE that it may not work when running in parallel. Will have to monitor when 
        /// implemented
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
        [Description("After a learner adds a reflection, it is populated into the Reflections table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ReflectionShowsInTableAfterAdded()
        {
            // TEMPORARY: When the Delete User API is developed, we can remove the below code and uncomment the code below it, which creates new users
            // on the fly. Decide this when the delete API is complete
            /// 1. Login as a learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, UserUtils.Learner1Login, "test");

            /// 2. Add a reflection
            LearnerRelectionObject LR = CLP.AddReflection();

            /// 3. Switch to the Reflection tab and verify the reflection is shown in the table
            CLP.SwitchToTab(CLP.ReflectionsTab, Bys.CBDLearnerPage.ReflectionsTab);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, CLP.ReflectionsTbl, Bys.CBDLearnerPage.ReflectionsTblBdy, LR.ReflectionTitle, "td", Bys.CBDLearnerPage.TableFirstBtn, Bys.CBDLearnerPage.TableNextBtn));

            ///// 1. Login as learner
            //LoginPage LP = Navigation.GoToLoginPage(browser);
            //UserInfo LRUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //CBDLearnerPage CLP = LP.Login(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password, false);

            ///// 2. Add a reflection
            //LearnerRelectionObject LR = CLP.AddReflection();

            ///// 3. Switch to the Reflection tab and verify the reflection is shown in the table
            //CLP.SwitchToTab(CLP.ReflectionsTab, Bys.CBDLearnerPage.ReflectionsTab);
            //Assert.True(ElemGet.Grid_ContainsRecord(browser, CLP.ReflectionsTbl, LR.ReflectionTitle, Bys.CBDLearnerPage.TableFirstBtn, Bys.CBDLearnerPage.TableNextBtn));
        }

        [Test]
        [Description("After a learner add a reflections, the reflection shows in labels and in the table")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UIUpdatesAfterLearnerAddsReflection()
        {
            /// 1. Create learner user and login
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LRUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);
            CBDLearnerPage CLP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 2. Store the number from the label text of the Reflections tab, and also "Showing" label, for the purpose of a future Assert within this 
            /// test. If there are no Reflections yet, this showing label will not appear, so skip it if so
            CLP.SwitchToTab(CLP.ReflectionsTab, Bys.CBDLearnerPage.ReflectionsTab);
            string origNumbOfReflectionsOnReflectionsTab = DataUtils.GetStringBetweenCharacters(CLP.ReflectionsTab.Text, "(", ")");
            if (browser.Exists(Bys.CBDLearnerPage.ShowingLbl, ElementCriteria.IsVisible))
            {
                string origNumbOfReflectionsOnShowingLbl = DataUtils.GetStringAfterCharacter(CLP.ShowingLbl.Text, "f", 2);
            }

            /// 3. Add a reflection
            LearnerRelectionObject LR = CLP.AddReflection();

            /// 4. Assert that the Reflections tab label increased by 1
            Assert.AreEqual(Int32.Parse(origNumbOfReflectionsOnReflectionsTab) + 1, Int32.Parse(DataUtils.GetStringBetweenCharacters(CLP.ReflectionsTab.Text, "(", ")")));

            /// 5. Assert that the label within the Reflections tab increased by 1
            // Bug RCPSC-264: "Learner->Add Reflection: "Showing" label does not update after learner adds reflection" 
            // Uncomment and run the test when fixed
            if (browser.Exists(Bys.CBDLearnerPage.ShowingLbl, ElementCriteria.IsVisible))
            {
                //Assert.AreEqual(Int32.Parse(origNumbOfReflectionsOnShowingLbl) + 1, Int32.Parse(DataUtils.GetStringAfterCharacter(CLP.ShowingLbl.Text, "f", 2)));
            }

            /// 5. Assert that the table contains the reflection
            Assert.True(ElemGet.Grid_ContainsRecord(browser, CLP.ReflectionsTbl, Bys.CBDLearnerPage.ReflectionsTblBdy, LR.ReflectionTitle, "td", Bys.CBDLearnerPage.TableFirstBtn, Bys.CBDLearnerPage.TableNextBtn));
        }

        [Test]
        [Description("The EPA Observation Count graph (On the main page and also the View More Reports form) updates after " +
            "an observer completes an assessment for an observation request from a learner.")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("mike2")]
        public void ObvCntGraphUpdatesAfterObserverCompletesAssessment()
        {
            /// 1. Create learner and observer users and login as the learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LRUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.LR);
            OBUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.OB);
            CBDLearnerPage CLP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 2. Store the EPA observation count of EPA1.1 from the graph now, so that when the observer completes an observation, we can Assert 
            /// the EPA observation count increased from this stored count
            CLP.StoreObvsCntFromEPAObvCntGraph(CLP.EPAObservCntChrt, "EPA 1.1");

            /// 3. Request an observation
            CLP.RequestObservationForEPA("Transition to Discipline",
                "Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
                OBUser.FullName, "Part A: Direct observation - Form 1");

            /// 4. Log out and then log in as the observer that the learner requested
            CLP.ClickAndWait(CLP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            CBDObserverPage OP = LP.LoginAsNewUser(UserUtils.UserRole.OB, OBUser.Username, OBUser.Password);

            /// 5. Accept the Pending observation request and complete the Assessment form
            OP.AcceptAndCompleteObservation(LRUser.FullName, "Part A: Direct observation - Form 1");

            /// 6. Log out and then log in as the learner
            CLP.ClickAndWait(CLP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            LP.LoginAsExistingUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 7. Verify that the EPA count has increased on the main graph
            Assert.AreEqual(CLP.PreviousEPAObvsCntFromEPAObvGraph + 1, CLP.GetEPAObsvCntFromEPAObsGraph(CLP.EPAObservCntChrt, "EPA 1.1"),
                "The EPA1.1 count on the graph has not increased after the observer completed the observation");

            /// 8. Open the Reports form and verify the EPA count on there
            CLP.ClickAndWait(CLP.ViewMoreRptsLnk);
            CLP.ReportsFormReportSelElem.SelectByText("EPA Observation Count");
            CLP.ClickAndWait(CLP.ReportsFormShowBtn);
            Assert.AreEqual(CLP.PreviousEPAObvsCntFromEPAObvGraph + 1, CLP.GetEPAObsvCntFromEPAObsGraph(CLP.ReportsFormEPAObservCntChrt, "EPA 1.1"),
                "The EPA1.1 count on the graph has not increased after the observer completed the observation");
        }

        [Test]
        [Description("Verifies the EPA Attainment Graph shows the correct data (min, max, mean) after an observer completes 4 observations")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void EPAAttnGraphUpdatesAfterObserverCompletesAssessment()
        {
            /// 1. Create learner and observer users and login as the learner
            LoginPage LP = Navigation.GoToLoginPage(browser);
            // We are using a try catch here for the following reason. This test is the very first test that gets run during the build. For some reason, the
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
            UserInfo OBUser = UserUtils.CreateAndRegisterUser(UserUtils.Application.CBD, UserUtils.UserRole.OB);
            CBDLearnerPage CLP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 2. Store the number of observations we will use throughout the test into a variable
             int noOfObs = 4;

            /// 3. Request 4 observations for EPA 1.1, with randomized ratings for each observation
            List<LearnerRequestObsObject> LRO = CLP.RequestObservationsForEPA(CLP.EPAIMTbl, Bys.CBDLearnerPage.EPAIMTbl, "Transition to Discipline",
                "Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure",
                OBUser.FullName, "Part A: Direct observation - Form 1", noOfObs);

            /// 4. Log out and then log in as the observer that the learner requested
            CLP.ClickAndWait(CLP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            CBDObserverPage OP = LP.LoginAsNewUser(UserUtils.UserRole.OB, OBUser.Username, OBUser.Password);

            /// 5. Accept the pending observation requests and complete the Assessment forms for all 4 of the observations
            List<CompletedAssessmentInfo> completedAssessments = OP.AcceptAndCompleteObservations(LRUser.FullName, 
                "Part A: Direct observation - Form 1", 
                noOfObs);

            /// 6. Calculate the min, max and mean values for the completed observations above
            decimal minExpected = OP.GetMinMaxOrMeanFromMultipleCompletedAssessments(completedAssessments, "min");
            decimal maxExpected = OP.GetMinMaxOrMeanFromMultipleCompletedAssessments(completedAssessments, "max");
            decimal meanExpected = OP.GetMinMaxOrMeanFromMultipleCompletedAssessments(completedAssessments, "mean");

            /// 8. Log out and then log in as the learner
            CLP.ClickAndWait(CLP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            LP.LoginAsExistingUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);

            /// 9. Open the Reports popup, obtain the actual values from the graph through JQuery, then assert them against the expected calculated values obtained above
            CLP.ClickAndWait(CLP.ViewMoreRptsLnk);
            decimal minActual = CLP.GetMinMeanOrMaxFromEPAAttainGraph(CLP.ReportsFormEPAAttainmentChrt, string.Format("EPA 1.1 ({0})", noOfObs), "min");
            Assert.AreEqual(minExpected, minActual);
            decimal maxActual = CLP.GetMinMeanOrMaxFromEPAAttainGraph(CLP.ReportsFormEPAAttainmentChrt, string.Format("EPA 1.1 ({0})", noOfObs), "max");
            Assert.AreEqual(maxExpected, maxActual);
            decimal meanActual = CLP.GetMinMeanOrMaxFromEPAAttainGraph(CLP.ReportsFormEPAAttainmentChrt, string.Format("EPA 1.1 ({0})", noOfObs), "mean");
            Assert.AreEqual(meanExpected, meanActual);
        }

        [Test]
        [Description("Given a learner is in stage 2, when a program admin promotes a learner to stage 3, then the learner page reflects this change")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        // This cant be run in parallel across browsers because agendas in the Meeting Agenda dropdown may get mixed up between parallel runs. See RCPSC-547
        // for the status of a redesign that would fix that
        //[NonParallelizable]
        public void LearnerStageLabelReflectsPromotion()
        {
            /// 1. Login as a program admin
            LoginPage LP = Navigation.GoToLoginPage(browser);
            CBDProgAdminPage PP = LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");

            /// 2. Create an upcoming agenda, then choose this agenda in the Meeting Agenda dropdown
            /// 4. Click on the Set Status button and then set the status of the learner to stage 2 
            /// 5. Finalize the Agenda
            PP.CreateChangeAndFinalizeAgendaForLearner(learnerFullNamePerBrowser, "Reviewed", "High", "_TA_AStatic User_CC_001", "Progressing as Expected",
                "Promote Learner - to Stage 3");

            /// 6. Log out then log back in as the learner
            PP.ClickAndWait(PP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            CBDLearnerPage CLP = LP.LoginAsExistingUser(UserUtils.UserRole.LR, learnerLoginPerBrowser, "test");

            /// 7. Assert that the text to the right of the Program label reflects the change to stage 2
            Assert.True(CLP.CurrentStageValueLbl.Text.Contains("Core of Discipline"));

            // Cleanup data by setting the learner back to stage 2
            PP.ClickAndWait(PP.LogoutLnk);
            Navigation.GoToLoginPage(browser);
            LP.LoginAsExistingUser(UserUtils.UserRole.PA, UserUtils.ProgAdmin1Login, "test");
            PP.CreateChangeAndFinalizeAgendaForLearner(learnerFullNamePerBrowser, null, null, null, "Progressing as Expected",
                "Promote Learner - to Stage 2");
        }




        //[Test]
        //public void sandboxtest()
        //{
        //UserInfo LRUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.LR);
        //UserInfo OBUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.OB);
        //UserInfo PAUser = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.CBD, UserUtils.UserRole.OB);

        //LoginPage LP = Navigation.GoToLoginPage(browser);
        //CBDLearnerPage OP = LP.LoginAsNewUser(UserUtils.UserRole.LR, LRUser.Username, LRUser.Password);
        //OP.ClickAndWait(OP.LogoutLnk);
        //Navigation.GoToLoginPage(browser);
        //CBDObserverPage OBP = LP.LoginAsNewUser(UserUtils.UserRole.OB, OBUser.Username, OBUser.Password);
        //OBP.ClickAndWait(OBP.LogoutLnk);
        //Navigation.GoToLoginPage(browser);
        //LP.LoginAsNewUser(UserUtils.UserRole.PA, PAUser.Username, PAUser.Password);
        //}


        #endregion Tests
    }
}







