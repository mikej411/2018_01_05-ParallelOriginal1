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
    // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document explaining why we can not do that
    // in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    // If you dont want a class to be tested in parallel, then include this attribute. You can also place this above a test method
    //[NonParallelizable]
    public class RCP_PER_Workflow_Tests : TestBase
    {
        #region Constructors
        public RCP_PER_Workflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_PER_Workflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests
        [Test]
        [Description("Workflow test making sure nothing goes wrong when a trainee submits all required milestones, doesnt submit an optional " +
            "milestone, then advances through each stage and gets marked as Achieved")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PERTraineeAdvancesAllStagesWRequiredMilestones()
        {
            // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document
            // explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
            if (BrowserName == BrowserNames.Chrome || BrowserName == BrowserNames.Firefox)
            {

                /// 1. Create a trainee user, login, complete all required milestones, submit the portfolio
                UserInfo newTrainee = new UserInfo();
                newTrainee = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER);
                PERHelperMethods.AsTrainee_CompleteRequiredMilestones_ThenSubmitPortfolio(browser, newTrainee.Username);

                /// 2. Log in as a Credential Staff member and assign 2 referees to the trainee
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndRefs(browser, newTrainee.FullName, UserUtils.Referee1PERFullName, UserUtils.Referee2PERFullName);

                /// 3. Login as the first and then second referee, fill the questionnaire to advance the trainee, click Save and Finish
                PERHelperMethods.AsReferee_ApproveTrainee(browser, UserUtils.Referee1PERLogin, newTrainee.FullName);
                PERHelperMethods.AsReferee_ApproveTrainee(browser, UserUtils.Referee2PERLogin, newTrainee.FullName);

                /// 4. Login as the trainee and verify that the Review Stage says Credentials Unit, and verify that the Referee labels are correct
                LoginPage LP = Navigation.GoToLoginPage(browser);
                PERTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineePER, newTrainee.Username, "test");
                Assert.AreEqual(TP.ReviewStageValueLbl.Text, "Credentials Unit");
                Assert.AreEqual(TP.Referee1PERValueLbl.Text.Trim(), UserUtils.Referee1PERFullName);
                Assert.AreEqual(TP.Referee2PERValueLbl.Text.Trim(), UserUtils.Referee2PERFullName);
                LP.Logout();

                /// 5. Log in as a Credential Staff member, click on Assign assessors, then assign 2 assessors
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndAssessors(browser, newTrainee.FullName, UserUtils.Assessor1PERFullName, UserUtils.Assessor2PERFullName);

                /// 6. Login as both assessors and mark all milestones as achieved.
                PERHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor1PERLogin, newTrainee.FullName);
                PERHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor2PERLogin, newTrainee.FullName);

                /// 7. Login as the CU and mark the trainee as Not Achieved for a final review
                PERHelperMethods.AsCredentialStaff_MarkTraineeAsAchievedOrNotAchieved(browser, newTrainee.FullName, true);

                /// 8. Login as the trainee and verify that the Review Stage says Achieved
                LoginPage LP2 = Navigation.GoToLoginPage(browser);
                PERTraineePage TP2 = LP2.LoginAsExistingUser(UserUtils.UserRole.TraineePER, newTrainee.Username, "test");
                Assert.AreEqual(TP2.ReviewStageValueLbl.Text, "Achieved");
            }
        }

        [Test]
        [Description("Given a Trainee gets assigned a 3rd assessor due to 2nd assessor rejection, When a 3rd assessor approves and a" +
            " CU member still marks as Not Achieved, Then the trainee gets marked as Not Achieved")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PERTraineeGets3rdAssessorAndGetsMarksNotAchieved()
        {
            // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document
            // explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
            if (BrowserName == BrowserNames.Chrome)
            {

                /// 1. Create a trainee user, login, complete all milestones, submit the portfolio
                UserInfo newTrainee = new UserInfo();
                newTrainee = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER);
                PERHelperMethods.AsTrainee_CompleteRequiredMilestones_ThenSubmitPortfolio(browser, newTrainee.Username);

                /// 2. Log in as a Credential Staff member and assign 2 referees to the trainee
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndRefs(browser, newTrainee.FullName, UserUtils.Referee1PERFullName, UserUtils.Referee2PERFullName);

                /// 3. Login as the first and then second referee, fill the questionnaire to advance the trainee, click Save and Finish
                PERHelperMethods.AsReferee_ApproveTrainee(browser, UserUtils.Referee1PERLogin, newTrainee.FullName);
                PERHelperMethods.AsReferee_ApproveTrainee(browser, UserUtils.Referee2PERLogin, newTrainee.FullName);

                /// 4. Log in as a Credential Staff member, click on Assign assessors, then assign 2 assessors
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndAssessors(browser, newTrainee.FullName, UserUtils.Assessor1PERFullName, UserUtils.Assessor2PERFullName);

                /// 5. Login as both assessors. For the first assessor, mark all milestones as achieved. For the other, mark 1 of the milestones as
                /// Not Achieved 
                PERHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor1PERLogin, newTrainee.FullName);
                PERHelperMethods.AsAssessor_RejectTrainee(browser, UserUtils.Assessor2PERLogin, newTrainee.FullName);

                /// 6. Login as a Credential Staff member, and assign a third referee
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo3rdAssessor(browser, newTrainee.FullName, UserUtils.Assessor3PERFullName);

                /// 7. Login as the 3rd assessor and approve the trainee
                PERHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor3PERLogin, newTrainee.FullName);

                /// 8. Login as the CU and mark the trainee as Not Achieved for a final review
                PERHelperMethods.AsCredentialStaff_MarkTraineeAsAchievedOrNotAchieved(browser, newTrainee.FullName, false);

                /// 9. Login as the trainee and verify that the Review Stage says Not Achieved
                LoginPage LP = Navigation.GoToLoginPage(browser);
                PERTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineePER, newTrainee.Username, "test");
                Assert.AreEqual(TP.ReviewStageValueLbl.Text, "Not Achieved");
            }
        }

        [Test]
        [Description("Verifying an assessor can request additional information from trainee, and the trainee can submit the additional info")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PERTraineeAndAssessorAdditionalInfoFeature()
        {
            // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document
            // explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Create a trainee user, login, complete all milestones, and submit the portfolio
                UserInfo newTrainee = new UserInfo();
                newTrainee = UserUtils.CreateAndRegisterUser(UserUtils.Application.PER, UserUtils.UserRole.TraineePER);
                List<string> milestoneNames = PERHelperMethods.AsTrainee_CompleteAllMilestones_ThenSubmitPortfolio(browser, newTrainee.Username);

                /// 2. Log in as a Credential Staff member and assign 2 referees to the trainee
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndRefs(browser, newTrainee.FullName, UserUtils.Referee1PERFullName, UserUtils.Referee2PERFullName);

                /// 3. Login as the first and then second referee, fill the questionnaire to advance the trainee, click Save and Finish
                PERHelperMethods.AsReferee_ApproveTrainee(browser, UserUtils.Referee1PERLogin, newTrainee.FullName);
                PERHelperMethods.AsReferee_ApproveTrainee(browser, UserUtils.Referee2PERLogin, newTrainee.FullName);

                /// 4. Log in as a Credential Staff member, click on Assign assessors, then assign 2 assessors
                PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndAssessors(browser, newTrainee.FullName, UserUtils.Assessor1PERFullName, UserUtils.Assessor2PERFullName);

                /// 5. Login as both assessors. For the first assessor, mark all milestones as achieved. For the second assessor, request additional information
                /// from the trainee. 
                PERHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor1PERLogin, newTrainee.FullName);
                PERHelperMethods.AsAssessor_RequestAdditionalInfo(browser, UserUtils.Assessor2PERLogin, newTrainee.FullName, milestoneNames[0]);

                /// 6. Login as the trainee and then send the additional information to the assessor
                string additionalInfo = "here is the additional info";
                PERHelperMethods.AsTrainee_SendAdditionalInfo(browser, newTrainee.Username, milestoneNames[0], additionalInfo);

                /// 7. Login as the assessor and verify that the assessor got the additional information
                Assert.True(PERHelperMethods.AsAssessor_VerifyAdditionalInfo(browser, UserUtils.Assessor2PERLogin, newTrainee.FullName, milestoneNames[0], additionalInfo));

                /// 8. Advance the trainee to Achieved
                PERHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor2PERLogin, newTrainee.FullName);
                PERHelperMethods.AsCredentialStaff_MarkTraineeAsAchievedOrNotAchieved(browser, newTrainee.FullName, true);
            }
        }




        //[Test]
        public void sandboxtest()
        {
            PERHelperMethods.AsCredentialStaff_AssignTraineeTo1stAnd2ndRefs(browser, "TTA_Trainee _6I654D2FDe5H", UserUtils.Referee1PERFullName, UserUtils.Referee2PERFullName);

        }
        #endregion Tests
    }
}






