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

namespace RCP.AppFramework
{
    public static class DiplomaHelperMethods
    {
        #region properties


        #endregion properties

        #region methods

        #region trainee

        /// <summary>
        /// Logs in as a trainee, clicks on each required milestone (doesnt click on optional milestones), uploads a document, sends the milestone to the CS,
        /// then logs out. Note that for this to work, you must seed this data into your environment, meaning you have to create a program on 
        /// Lifetime Support. And you have to label your optional milestones as "optional" when creating your
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="traineeLogin">The trainee's username</param>
        public static List<string> AsTrainee_SubmitAllRequiredMilestones(IWebDriver browser, string traineeLogin, string clinicalSupervisorFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaTraineePage TP = LP.LoginAsNewUser(UserUtils.UserRole.TraineeDiploma, traineeLogin, "test");
            List<string> milestoneNames = TP.CompleteAndSubmitAllRequiredMilestones(clinicalSupervisorFullName);
            TP.Logout();
            return milestoneNames;
        }

        /// <summary>
        /// Logs in as a trainee, clicks on each milestone, uploads a document, sends the milestone to the CS, then logs out. Note that for this to work, you 
        /// must seed this data into your environment, meaning you have to create a program on Lifetime Support
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeLogin"></param>
        /// <param name="clinicalSupervisorFullName"></param>
        /// <returns></returns>
        public static List<string> AsTrainee_SubmitAllMilestones(IWebDriver browser, string traineeLogin, string clinicalSupervisorFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaTraineePage TP = LP.LoginAsNewUser(UserUtils.UserRole.TraineeDiploma, traineeLogin, "test");
            List<string> milestoneNames = TP.CompleteAndSubmitAllMilestones(clinicalSupervisorFullName);
            TP.Logout();
            return milestoneNames;
        }

        /// <summary>
        /// Logs in as a trainee, clicks the Submit Portfolio button, then clicks the Submit button on the resulting popup, then logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeLogin"></param>
        public static void AsTrainee_SubmitPortfolio(IWebDriver browser, string traineeLogin)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineeDiploma, traineeLogin, "test");
            TP.SubmitPortfolio();
            TP.Logout();
        }

        /// <summary>
        /// Logs in as a trainee, selects "Needs Additional Information" in the Status drop down, clicks on the user-specified milestone, enters the user-specified
        /// text into the "Your Reply" text box, clicks Save, goes back to the Dashboard, clicks Save then logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeLogin"></param>
        /// <param name="milestoneName">The milestone to click in the milestone table. The milestone you want to send additional info for</param>
        /// <param name="additionalInfoText"></param>
        public static void AsTrainee_SendAdditionalInfo(IWebDriver browser, string traineeLogin, string milestoneName, string additionalInfoText)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineeDiploma, traineeLogin, "test");
            TP.SubmitAdditionalInfo(milestoneName, additionalInfoText);
            TP.Logout();
        }

        #endregion trainee

        #region clinical supervisor

        /// <summary>
        /// Logs in as a CS, clicks on all milestones for a user-specified trainee, clicks the Mark As Achieved button, clicks Submit on the popup, then
        /// logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="clinicalSupervisorLogin"></param>
        /// <param name="traineeFullName">First and last name of the trainee</param>
        public static void AsCS_MarkAllMilestonesAchieved(IWebDriver browser, string clinicalSupervisorLogin, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaClinicalSupervisorPage CSP = LP.LoginAsExistingUser(UserUtils.UserRole.CSDiploma, clinicalSupervisorLogin, "test");
            CSP.MarkAllMilestonesAchieved(traineeFullName);
            CSP.Logout();
        }

        #endregion clinical supervisor

        #region diploma director

        /// <summary>
        /// Logs in as the diploma director, checks the check box in the Portfolios Under Review tab for a user-specified trainee, clicks the Mark Selected
        /// Portfolio as Achieved button, checks the I Attest check box on the Mark Selected Portfolios as Achieved form, clicks Submbit, then logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="diplomaDirectorLogin"></param>
        /// <param name="traineeFullName">trainee first and last name</param>
        public static void AsDD_MarkPortfolioAsAchieved(IWebDriver browser, string diplomaDirectorLogin, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaDirectorPage DD = LP.LoginAsExistingUser(UserUtils.UserRole.DDDiploma, diplomaDirectorLogin, "test");
            DD.MarkPortfolioAchieved(traineeFullName);
            DD.Logout();
        }

        /// <summary>
        /// Logs in as a director, clicks the user-specified trainee in the Portfolios Under Review table, clicks on the milestone links in the Milestone table, 
        /// clicks on the Request Additional Information button, enters text into the Comments field, clicks the Submit button, logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        /// <param name="milestoneName">The milestone name as it appears in the text of the HTML element</param>
        /// <param name="additionalInfoText">The text you want to enter for the requested or submitted additional info</param>
        public static void AsDD_RequestAdditionalInfo(IWebDriver browser, string diplomaDirectorLogin, string traineeFullName, string requestedMilestone)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaDirectorPage DD = LP.LoginAsExistingUser(UserUtils.UserRole.DDDiploma, diplomaDirectorLogin, "test");
            DD.RequestAdditionalInfo(traineeFullName, requestedMilestone, "This is the requested additional info text");
            DD.Logout();
        }

        #endregion diploma director

        #region faculty of medicine

        public static void AsFacOfMed_MarkMarkPortfolioAAsAchieved(IWebDriver browser, string facOfMedicineLogin, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaFacOfMedicinePage FOMP = LP.LoginAsExistingUser(UserUtils.UserRole.FOMDiploma, facOfMedicineLogin, "test");
            FOMP.MarkPortfolioAchieved(traineeFullName);
            FOMP.Logout();
        }

        #endregion faculty of medicine

        #region assessor

        /// <summary>
        /// Logs in as an assessor, clicks the Review button for a user-specified Trainee, clicks on the milestone links in the Milestone table, 
        /// marks the milestone as Achieved, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="assessorLogin">The assessor's login</param>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        public static void AsAssessor_ApproveTrainee(IWebDriver browser, string assessorLogin, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaAssessorPage DP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, assessorLogin, "test");
            DP.MarkAllMilestones(traineeFullName, true);
            DP.Logout();
        }

        /// <summary>
        /// Logs in as an assessor, clicks the Review button for a user-specified Trainee, clicks on the milestone links in the Milestone table, 
        /// marks the milestone as Not Achieved, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="assessorLogin">The assessor's login</param>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        public static void AsAssessor_RejectTrainee(IWebDriver browser, string assessorLogin, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaAssessorPage DP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, assessorLogin, "test");
            DP.MarkAllMilestones(traineeFullName, false);
            DP.Logout();
        }

        /// <summary>
        /// Logs in as an assessor, clicks the Review button for a user-specified trainee, clicks on the milestone links in the Milestone table, 
        /// clicks on the Request Additional Information button, enters text into the Comments field, clicks the Submit button, logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        /// <param name="milestoneName">The milestone name as it appears in the text of the HTML element</param>
        /// <param name="additionalInfoText">The text you want to enter for the requested or submitted additional info</param>
        public static void AsAssessor_RequestAdditionalInfo(IWebDriver browser, string assessorLogin, string traineeFullName, string requestedMilestone)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaAssessorPage DP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, assessorLogin, "test");
            DP.RequestAdditionalInfo(traineeFullName, requestedMilestone, "This is the requested additional info text");
            DP.Logout();
        }


        /// <summary>
        /// Logs in as an assessor, clicks on the Review button for a trainee to go to the trainee dashboard, clicks on a user-specified milestone, returns 
        /// either true or false depeneding on if it finds the user-specified additional information 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="assessorLogin"></param>
        /// <param name="traineeFullName">Trainee's first and last name</param>
        /// <param name="milestoneName">The milestone you want to verify</param>
        /// <param name="additionalInfo">The additional info text that you want to verify is present for the milestone</param>
        /// <returns></returns>
        public static bool AsAssessor_VerifyAdditionalInfo(IWebDriver browser, string assessorLogin, string traineeFullName, string milestone, string additionalInfo)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaAssessorPage DP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRDiploma, assessorLogin, "test");
            bool verified = DP.VerifyAdditionalInfo(traineeFullName, milestone, additionalInfo);
            DP.Logout();

            return verified;
        }

        #endregion assessor

        #region credential staff unit

        /// <summary>
        /// Logs in as the Credential Staff user, clicks on the Portfolios Under Review tab, clicks on the Mark As Achieved button for a user-specified trainee,
        /// clicks the Submit button on the confirmation popup window, clicks on the Portfolios Under Review tab, clicks on the Record Payment button for the
        /// trainee, fills in the date and comments fields, clicks the Submit button on the confirmation popup window, clicks on the Portfolios Under Review tab,
        /// clicks on the Assign Reviewer button for a user-specified trainee, assigns 2 assessors, clicks the Submit button on the confirmation popup window,
        /// then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="traineeFullName">The trainee's first and last name. The exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="assessor1FullName">The first and last name of the assessor</param>
        /// <param name="assessor2FullName">The first and last name of the assessor</param>
        public static void AsCU_MarkAsAchievedRecordPaymentAndAssign2Assessors(IWebDriver browser, string traineeFullName, string assessor1FullName, string assessor2FullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaCredentialStaffPage Page = LP.LoginAsExistingUser(UserUtils.UserRole.CUDiploma, UserUtils.CredentialStaffDiplomaLogin, "password");
            Page.MarkTraineeAsAchieved(traineeFullName);
            Page.RecordPayment(traineeFullName);
            Page.AssignReviewer(traineeFullName, assessor1FullName, assessor2FullName);
            Page.Logout();
        }

        /// <summary>
        /// Logs in as the CU, clicks on the Assessors tab if we are not already there, clicks the Make Final Review button for a user-specified trainee,
        /// clicks on the Achieved radio button, enters text into Request Comments field, clicks the Submit button, then logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeFullName"></param>
        public static void AsCU_MakeFinalReview_MarkAchieved(IWebDriver browser, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaCredentialStaffPage Page = LP.LoginAsExistingUser(UserUtils.UserRole.CUDiploma, UserUtils.CredentialStaffDiplomaLogin, "password");
            Page.MakeFinalReview(traineeFullName, true);
            Page.Logout();
        }

        /// <summary>
        /// Logs in as the CU, clicks on the Assessors tab if we are not already there, clicks the Make Final Review button for a user-specified trainee,
        /// clicks on the Not Achieved radio button, enters text into Request Comments field, clicks the Submit button, then logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeFullName"></param>
        public static void AsCU_MakeFinalReview_MarkNotAchieved(IWebDriver browser, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaCredentialStaffPage Page = LP.LoginAsExistingUser(UserUtils.UserRole.CUDiploma, UserUtils.CredentialStaffDiplomaLogin, "password");
            Page.MakeFinalReview(traineeFullName, false);
            Page.Logout();
        }

        /// <summary>
        /// Logs in as the Credential Staff user, clicks on the Assessors tab if we are not already there, clicks the Assign Third Assessor button for a
        /// user-specified trainee, chooses a user-specified third assessor, clicks the Submit button, then logs out
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="traineeFullName"></param>
        /// <param name="thirdAssessorFullName"></param>
        public static void AsCredentialStaff_AssignTraineeTo3rdAssessor(IWebDriver browser, string traineeFullName, string thirdAssessorFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DiplomaCredentialStaffPage Page = LP.LoginAsExistingUser(UserUtils.UserRole.CUDiploma, UserUtils.CredentialStaffDiplomaLogin, "password");
            Page.Assign3rdAssessor(traineeFullName, thirdAssessorFullName);
            Page.Logout();
        }

        #endregion credential staff unit

        #endregion methods
    }
}
