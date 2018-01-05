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
    public static class PERHelperMethods
    {
        #region properties


        #endregion properties

        #region methods

        #region trainee

        /// <summary>
        /// Logs in as a trainee, clicks on each required milestone, uploads a document, saves and completes the milestone, submits the portfolio, then logs out. 
        /// This does not complete the milestones labeled as "optional", then logs out
        /// Note that for this to work, you must seed this data into your environment, meaning you have to create a program on 
        /// Lifetime Support. And you have to label your optional milestones as "optional" when creating your
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="traineeLogin">The trainee's username</param>
        public static List<string> AsTrainee_CompleteRequiredMilestones_ThenSubmitPortfolio(IWebDriver browser, string traineeLogin)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERTraineePage TP = LP.LoginAsNewUser(UserUtils.UserRole.TraineePER, traineeLogin, "test");
            List<string> milestoneNames = TP.CompleteAllRequiredMilestones();
            TP.SubmitPortfolio();
            TP.Logout();
            return milestoneNames;
        }

        /// <summary>
        /// Logs in as a trainee, clicks on each milestone, uploads a document, saves and completes the milestone,
        /// submits the portfolio to the Credential Staff user, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="traineeLogin">The trainee's username</param>
        /// <returns>A list of all milestones</returns>
        public static List<string> AsTrainee_CompleteAllMilestones_ThenSubmitPortfolio(IWebDriver browser, string traineeLogin)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERTraineePage TP = LP.LoginAsNewUser(UserUtils.UserRole.TraineePER, traineeLogin, "test");
            List<string> milestoneNames = TP.CompleteAllMilestones();
            TP.SubmitPortfolio();
            TP.Logout();
            return milestoneNames;
        }

        /// <summary>
        /// Logs in as a trainee, selects "Needs Additional Information" in the Status drop down, clicks on the user-specified milestone, enters the user-specified
        /// text into the "Your Reply" text box, clicks Save, goes back to the Dashboard, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="milestoneName">The milestone name as it appears in the text of the HTML elementThe exact text of the milestone inside the milestone table</param>
        /// <param name="additionalInfoText">The text you want to enter for the requested or submitted additional infoThe text of your choice</param>
        public static void AsTrainee_SendAdditionalInfo(IWebDriver browser, string traineeLogin, string milestoneName, string additionalInfoText)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERTraineePage TP = LP.LoginAsExistingUser(UserUtils.UserRole.TraineePER, traineeLogin, "test");
            TP.SubmitAdditionalInfo(milestoneName, additionalInfoText);
            TP.Logout();
        }

        #endregion trainee

        #region referee

        /// <summary>
        /// Logs in as a referee, opens the questionnaire survey form for a user-specified trainee, fills out the form, approves
        /// the trainee depending on your passed parameter, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="refereeLogin">The referee username</param>
        /// <param name="traineeFullName">The first and last name of the trainee</param>
        /// <param name="approveTrainee">true or false depending on if you want to approve or reject the trainee</param>
        public static void AsReferee_ApproveTrainee(IWebDriver browser, string refereeLogin, string traineeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERRefereePage RP = LP.LoginAsExistingUser(UserUtils.UserRole.REF, refereeLogin, "test");
            RP.CompleteQuestionnaire(traineeFullName, true);
            RP.Logout();
        }

        #endregion referee

        #region assessor

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
            PERAssessorPage AP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, assessorLogin, "test");
            bool verified = AP.VerifyAdditionalInfo(traineeFullName, milestone, additionalInfo);
            AP.Logout();

            return verified;
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
            PERAssessorPage AP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, assessorLogin, "test");
            AP.MarkAllMilestones(traineeFullName, false);
            AP.Logout();
        }

        /// <summary>
        /// Logs in as an assessor, clicks the Review button for a user-specified trainee, clicks on the milestone links in the Milestone table, 
        /// clicks on the Request Additional Information button, enters text into the Comments field, clicks the Submit button, logs out
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        /// <param name="milestoneName">The milestone name as it appears in the text of the HTML element</param>
        /// <param name="additionalInfoText">The text you want to enter for the requested or submitted additional info</param>
        public static void AsAssessor_RequestAdditionalInfo(IWebDriver browser, string assessorLogin, string traineeFullName, string requestedMilestone)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERAssessorPage AP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, assessorLogin, "test");
            AP.RequestAdditionalInfo(traineeFullName, requestedMilestone, "This is the requested additional info text");
            AP.Logout();
        }

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
            PERAssessorPage AP = LP.LoginAsExistingUser(UserUtils.UserRole.ASRPER, assessorLogin, "test");
            AP.MarkAllMilestones(traineeFullName, true);
            AP.Logout();
        }

        #endregion assessor  

        #region cu

        /// <summary>
        /// Logs in as as the Credential Staff user, clicks on the Referees tab if we are not already there, clicks the Assign Referees button for a 
        /// user-specified trainee, chooses a user-specified first and second referee, clicks the Submit button, then logs out
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last name. The exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="referee1FullName">The first and last name of the referee</param>
        /// <param name="referee2FullName">The first and last name of the referee</param>
        public static void AsCredentialStaff_AssignTraineeTo1stAnd2ndRefs(IWebDriver browser, string traineeFullName, string firstRefereeFullName, string secondRefereeFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERCredentialStaffPage CP = LP.LoginAsExistingUser(UserUtils.UserRole.CUPER, UserUtils.CredentialStaffPERLogin, "password");
            CP.AssignReferees(traineeFullName, firstRefereeFullName, secondRefereeFullName);
            CP.Logout();
        }

        /// <summary>
        /// Logs in as the Credential Staff user, cicks on the Assessors tab if we are not already there, clicks the Assign Assessors button for a 
        /// user-specified trainee, chooses a user-specified first and second assessor, clicks the Submit button, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="traineeFullName">The trainee's first and last name. The exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="assessor1FullName">The first and last name of the assessor</param>
        /// <param name="assessor2FullName">The first and last name of the assessor</param>
        public static void AsCredentialStaff_AssignTraineeTo1stAnd2ndAssessors(IWebDriver browser, string traineeFullName, string firstAssessorFullName, string secondAssessorFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERCredentialStaffPage CP = LP.LoginAsExistingUser(UserUtils.UserRole.CUPER, UserUtils.CredentialStaffPERLogin, "password");
            CP.AssignFirst2Assessors(traineeFullName, firstAssessorFullName, secondAssessorFullName);
            CP.Logout();
        }

        /// <summary>
        /// Logs in as the Credential Staff user, clicks on the Assessors tab if we are not already there, clicks the Assign Third Assessor button for a
        /// user-specified trainee, chooses a user-specified third assessor, clicks the Submit button, then logs out
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="assessor3FullName">The first and last name of the assessor</param>
        public static void AsCredentialStaff_AssignTraineeTo3rdAssessor(IWebDriver browser, string traineeFullName, string thirdAssessorFullName)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERCredentialStaffPage CP = LP.LoginAsExistingUser(UserUtils.UserRole.CUPER, UserUtils.CredentialStaffPERLogin, "password");
            CP.Assign3rdAssessor(traineeFullName, thirdAssessorFullName);
            CP.Logout();
        }

        /// <summary>
        /// Logs in as the Credential Staff user, clicks on the Assessors tab if we are not already there, clicks the Make Final Review button for a 
        /// user-specified trainee, clicks on either the Achieved or Not Achieved radio button, enters text into Request Comments field, clicks 
        /// the Submit button, then logs out
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Assessor tab</param>
        /// <param name="achieved">true or false depending on if you want to achieve the trainee or not</param>
        public static void AsCredentialStaff_MarkTraineeAsAchievedOrNotAchieved(IWebDriver browser, string traineeFullName, bool achieved)
        {
            LoginPage LP = Navigation.GoToLoginPage(browser);
            PERCredentialStaffPage CP = LP.LoginAsExistingUser(UserUtils.UserRole.CUPER, UserUtils.CredentialStaffPERLogin, "password");
            CP.MakeFinalReview(traineeFullName, achieved);
            CP.Logout();
        }

        #endregion cu

  



   

  





        #endregion methods
    }
}
