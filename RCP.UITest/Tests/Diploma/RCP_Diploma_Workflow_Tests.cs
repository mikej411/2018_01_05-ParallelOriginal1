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
    public class RCP_Diploma_Workflow_Tests : TestBase
    {
        #region Constructors
        public RCP_Diploma_Workflow_Tests(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public RCP_Diploma_Workflow_Tests(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests
        [Test]
        [Description("Workflow test making sure nothing goes wrong when a trainee submits all required milestones, doesnt submit an optional " +
            "milestone, then advances through each stage and gets marked as Achieved")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void DiplomaTraineeAdvancesAllStagesWRequiredMilestones()
        {
            // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document
            // explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
            if (BrowserName == BrowserNames.Chrome)
            {

                ///// 1. Create a trainee user, login, complete and assign all required milestones to a clinical supervisor
                UserInfo newTrainee = new UserInfo();
                newTrainee = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma);
                DiplomaHelperMethods.AsTrainee_SubmitAllRequiredMilestones(browser, newTrainee.Username, UserUtils.ClinicalSupervisor1DiplomaFullName);

                /// 2. As the CS, mark the trainee's portfolio is achieved
                DiplomaHelperMethods.AsCS_MarkAllMilestonesAchieved(browser, UserUtils.ClinicalSupervisor1DiplomaLogin, newTrainee.FullName);

                /// 3. As the trainee, submit the portfolio
                DiplomaHelperMethods.AsTrainee_SubmitPortfolio(browser, newTrainee.Username);

                /// 4. As the diploma director, mark the trainee's portfolio as achieved
                DiplomaHelperMethods.AsDD_MarkPortfolioAsAchieved(browser, UserUtils.DiplDirector1DiplomaLogin, newTrainee.FullName);

                /// 5. As the faculty of medicine, mark the trainee's portfolio as achieved
                DiplomaHelperMethods.AsFacOfMed_MarkMarkPortfolioAAsAchieved(browser, UserUtils.FacultyOfMed1DiplomaLogin, newTrainee.FullName);

                /// 6. As the credential staff unit, mark the trainee as achieved, record a payment, and assign 2 assessors
                DiplomaHelperMethods.AsCU_MarkAsAchievedRecordPaymentAndAssign2Assessors(browser, newTrainee.FullName, UserUtils.Assessor1DiplomaFullName, UserUtils.Assessor2DiplomaFullName);

                /// 7. As both assessors, mark all of the trainee's milestones as achieved
                DiplomaHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor1DiplomaLogin, newTrainee.FullName);
                DiplomaHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor2DiplomaLogin, newTrainee.FullName);

                /// 8. As the credential staff unit, make a final review and mark the trainee as achieved
                DiplomaHelperMethods.AsCU_MakeFinalReview_MarkAchieved(browser, newTrainee.FullName);

                /// 9. Login as the trainee, and verify that the Review Stage label has been updated to Achieved
                LoginPage LP = Navigation.GoToLoginPage(browser);
                DiplomaTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineeDiploma, newTrainee.Username, "test");
                Assert.AreEqual(TP.ReviewStageValueLbl.Text, "Achieved");
            }
        }

        [Test]
        [Description("Given a Trainee gets assigned a 3rd assessor due to 2nd assessor rejection, When a 3rd assessor approves and a" +
            " CU member still marks as Not Achieved, Then the trainee gets marked as Not Achieved")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void DiplomaTraineeGets3rdAssessorAndGetsMarksNotAchieved()
        {
            // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document
            // explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
            if (BrowserName == BrowserNames.Chrome)
            {

                /// 1. Create a trainee user, login, complete and assign all milestones to a clinical supervisor
                UserInfo newTrainee = new UserInfo();
                newTrainee = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma);
                DiplomaHelperMethods.AsTrainee_SubmitAllMilestones(browser, newTrainee.Username, UserUtils.ClinicalSupervisor1DiplomaFullName);

                /// 2. As the CS, mark the trainee's portfolio is achieved
                DiplomaHelperMethods.AsCS_MarkAllMilestonesAchieved(browser, UserUtils.ClinicalSupervisor1DiplomaLogin, newTrainee.FullName);

                /// 3. As the trainee, submit the portfolio
                DiplomaHelperMethods.AsTrainee_SubmitPortfolio(browser, newTrainee.Username);

                /// 4. As the diploma director, mark the trainee's portfolio as achieved
                DiplomaHelperMethods.AsDD_MarkPortfolioAsAchieved(browser, UserUtils.DiplDirector1DiplomaLogin, newTrainee.FullName);

                /// 5. As the faculty of medicine, mark the trainee's portfolio as achieved
                DiplomaHelperMethods.AsFacOfMed_MarkMarkPortfolioAAsAchieved(browser, UserUtils.FacultyOfMed1DiplomaLogin, newTrainee.FullName);

                /// 6. As the credential staff unit, mark the trainee as achieved, record a payment, and assign 2 assessors
                DiplomaHelperMethods.AsCU_MarkAsAchievedRecordPaymentAndAssign2Assessors(browser, newTrainee.FullName, UserUtils.Assessor1DiplomaFullName, UserUtils.Assessor2DiplomaFullName);

                /// 7. Login as both assessors. For the first assessor, mark all milestones as achieved. For the other, mark 1 of the milestones as
                /// Not Achieved 
                DiplomaHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor1DiplomaLogin, newTrainee.FullName);
                DiplomaHelperMethods.AsAssessor_RejectTrainee(browser, UserUtils.Assessor2DiplomaLogin, newTrainee.FullName);

                /// 8. Login as a Credential Staff member, and assign a third referee
                DiplomaHelperMethods.AsCredentialStaff_AssignTraineeTo3rdAssessor(browser, newTrainee.FullName, UserUtils.Assessor3DiplomaFullName);

                /// 9. Login as the 3rd assesor and approve the trainee
                DiplomaHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor3DiplomaLogin, newTrainee.FullName);

                /// 10. As the credential staff unit, make a final review and mark the trainee as not achieved
                DiplomaHelperMethods.AsCU_MakeFinalReview_MarkNotAchieved(browser, newTrainee.FullName);

                /// 11. Login as the trainee, and verify that the Review Stage label has been updated to Not Achieved
                LoginPage LP = Navigation.GoToLoginPage(browser);
                DiplomaTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineeDiploma, newTrainee.Username, "test");
                Assert.AreEqual(TP.ReviewStageValueLbl.Text, "Not Achieved");
            }
        }

        [Test]
        [Description("Verifying an assessor can request additional information from trainee, and the trainee can submit the additional info")]
        [Property("Status", "In Progress")]
        [Author("Mike Johnston")]
        public void DiplomaTraineeAndAssessorAdditionalInfoFeature()
        {
            // Ignoring this test in IE and Firefox because this test needs to upload files. See the following document
            // explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General
            if (BrowserName == BrowserNames.Chrome)
            {


                /// 1. Create a trainee user, login, complete and assign all required milestones to a clinical supervisor
                UserInfo newTrainee = new UserInfo();
                newTrainee = UserUtils.CreateAndRegisterUser(UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma);
                List<string> milestoneNames = DiplomaHelperMethods.AsTrainee_SubmitAllMilestones(browser, newTrainee.Username, UserUtils.ClinicalSupervisor1DiplomaFullName);

                /// 2. As the CS, mark the trainee's portfolio is achieved
                DiplomaHelperMethods.AsCS_MarkAllMilestonesAchieved(browser, UserUtils.ClinicalSupervisor1DiplomaLogin, newTrainee.FullName);

                /// 3. As the trainee, submit the portfolio
                DiplomaHelperMethods.AsTrainee_SubmitPortfolio(browser, newTrainee.Username);

                /// 4. As the diploma director, mark the trainee's portfolio as achieved
                DiplomaHelperMethods.AsDD_MarkPortfolioAsAchieved(browser, UserUtils.DiplDirector1DiplomaLogin, newTrainee.FullName);

                /// 5. As the faculty of medicine, mark the trainee's portfolio as achieved
                DiplomaHelperMethods.AsFacOfMed_MarkMarkPortfolioAAsAchieved(browser, UserUtils.FacultyOfMed1DiplomaLogin, newTrainee.FullName);

                /// 6. As the credential staff unit, mark the trainee as achieved, record a payment, and assign 2 assessors
                DiplomaHelperMethods.AsCU_MarkAsAchievedRecordPaymentAndAssign2Assessors(browser, newTrainee.FullName, UserUtils.Assessor1DiplomaFullName, UserUtils.Assessor2DiplomaFullName);

                /// 7. Login as both assessors. For the first assessor, mark all milestones as achieved. For the second assessor, request additional information
                /// from the trainee. 
                DiplomaHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor1DiplomaLogin, newTrainee.FullName);
                DiplomaHelperMethods.AsAssessor_RequestAdditionalInfo(browser, UserUtils.Assessor2DiplomaLogin, newTrainee.FullName, milestoneNames[0]);

                /// 8. Login as the trainee and then send the additional information to the assessor
                string additionalInfo = "here is the additional info";
                DiplomaHelperMethods.AsTrainee_SendAdditionalInfo(browser, newTrainee.Username, milestoneNames[0], additionalInfo);

                /// 8. Login as the assessor and verify that the assessor got the additional information
                Assert.True(DiplomaHelperMethods.AsAssessor_VerifyAdditionalInfo(browser, UserUtils.Assessor2DiplomaLogin, newTrainee.FullName, milestoneNames[0], additionalInfo));

                /// 9. Advance the trainee to Achieved
                DiplomaHelperMethods.AsAssessor_ApproveTrainee(browser, UserUtils.Assessor2DiplomaLogin, newTrainee.FullName);
                DiplomaHelperMethods.AsCU_MakeFinalReview_MarkAchieved(browser, newTrainee.FullName);
            }
        }




        //// Want to create a test where DD requests additional info then trainee submits it and it is in the resubmitted milestones table
        //[Test]
        //public void Temp()
        //{
        //    if (BrowserName == BrowserNames.InternetExplorer || BrowserName == BrowserNames.Firefox)
        //    {
        //        Assert.Ignore(string.Format("Ignoring this test in IE and Firefox because this test needs to upload files. See the following" +
        //          "document explaining why we can not do that in these browsers: https://code.premierinc.com/docs/display/PGHLMSDOCS/General"));
        //    }

        //    /// 1. Create a trainee user, login, complete and assign all required milestones to a clinical supervisor
        //    UserInfo newTrainee = new UserInfo();
        //    newTrainee = UserUtils.CreateAndRegisterUser(null, UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma);
        //    List<string> milestones = DiplomaHelperMethods.AsTrainee_SubmitAllRequiredMilestones(browser, newTrainee.Username, UserUtils.ClinicalSupervisor1DiplomaFullName);

        //    /// 2. As the CS, mark the trainee's portfolio is achieved
        //    DiplomaHelperMethods.AsCS_MarkAllMilestonesAchieved(browser, UserUtils.ClinicalSupervisor1DiplomaLogin, newTrainee.FullName);

        //    /// 3. As the trainee, submit the portfolio
        //    DiplomaHelperMethods.AsTrainee_SubmitPortfolio(browser, newTrainee.Username);

        //    /// 4. As the diploma director, mark the trainee's portfolio as achieved
        //    string milestoneToRequest = milestones[0];
        //    DiplomaHelperMethods.AsDD_RequestAdditionalInfo(browser, UserUtils.DiplDirector1DiplomaLogin, newTrainee.FullName, milestoneToRequest);

        //    /// 5. Login as the trainee and then send the additional information to the director
        //    string additionalInfo = "here is the additional info";
        //    DiplomaHelperMethods.AsTrainee_SendAdditionalInfo(browser, newTrainee.Username, milestoneToRequest, additionalInfo);
        //}
        #endregion Tests
    }
}






