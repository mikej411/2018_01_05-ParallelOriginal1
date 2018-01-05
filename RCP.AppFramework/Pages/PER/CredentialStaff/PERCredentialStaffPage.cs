using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class PERCredentialStaffPage : RCPPage, IDisposable
    {
        #region constructors
        public PERCredentialStaffPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements    
   
        public IWebElement BackGroundBackDrop { get { return this.FindElement(Bys.PERCredentialStaffPage.BackGroundBackDrop); } }
        public SelectElement AssignAssessor3rdAssFormThirdAssSelElem { get { return new SelectElement(this.FindElement(Bys.PERCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElem)); } }
        public IWebElement AssignAssessor3rdAssFormSubmitBtn { get { return this.FindElement(Bys.PERCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtn); } }
        public IWebElement FinalReviewFormAchievedRdo { get { return this.FindElement(Bys.PERCredentialStaffPage.FinalReviewFormAchievedRdo); } }
        public IWebElement FinalReviewFormNotAchievedRdo { get { return this.FindElement(Bys.PERCredentialStaffPage.FinalReviewFormNotAchievedRdo); } }
        public IWebElement FinalReviewFormRequestCommentsTxt { get { return this.FindElement(Bys.PERCredentialStaffPage.FinalReviewFormRequestCommentsTxt); } }
        public IWebElement AssignAssessor2AssFormTitleLbl { get { return this.FindElement(Bys.PERCredentialStaffPage.AssignAssessor2AssFormTitleLbl); } }
        public IWebElement AssignReferee2PERRefsFormTitleLbl { get { return this.FindElement(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormTitleLbl); } }
        public IWebElement AssignAssessor2AssFormSubmitBtn { get { return this.FindElement(Bys.PERCredentialStaffPage.AssignAssessor2AssFormSubmitBtn); } }
        public IWebElement RefereesTab { get { return this.FindElement(Bys.PERCredentialStaffPage.RefereesTab); } }
        public IWebElement AssessorTab { get { return this.FindElement(Bys.PERCredentialStaffPage.AssessorTab); } }
        public IWebElement AssignReferee2PERRefsFormSubmitBtn { get { return this.FindElement(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormSubmitBtn); } }
        public IWebElement RefereesTabTraineeTbl { get { return this.FindElement(Bys.PERCredentialStaffPage.RefereesTabTraineeTbl); } }
        public IWebElement RefereesTabTraineeTblFirstRow { get { return this.FindElement(Bys.PERCredentialStaffPage.RefereesTabTraineeTblFirstRow); } }
        public SelectElement AssignReferee2PERRefsFormFirstRefSelElem { get { return new SelectElement(this.FindElement(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormFirstRefSelElem)); } }
        public SelectElement AssignReferee2PERRefsFormSecondRefSelElem { get { return new SelectElement(this.FindElement(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormSecondRefSelElem)); } }
        public SelectElement AssignAssessor2AssFormFirstAssSelElem { get { return new SelectElement(this.FindElement(Bys.PERCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElem)); } }
        public SelectElement AssignAssessor2AssFormSecondAssSelElem { get { return new SelectElement(this.FindElement(Bys.PERCredentialStaffPage.AssignAssessor2AssFormSecondAssSelElem)); } }
        public IWebElement AssessorTabTraineeTbl { get { return this.FindElement(Bys.PERCredentialStaffPage.AssessorTabTraineeTbl); } }
        public IWebElement AssessorTabTraineeTblFirstRow { get { return this.FindElement(Bys.PERCredentialStaffPage.AssessorTabTraineeTblFirstRow); } }
        public IWebElement FinalReviewFormSubmitBtn { get { return this.FindElement(Bys.PERCredentialStaffPage.FinalReviewFormSubmitBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.PERCredentialStaffPage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.PERCredentialStaffPage.RefereesTabTraineeTblFirstRowVisible, Criteria.PERCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
            }
            catch
            {
                RefreshPage();
            }
            // If you look at the page when it loads, the tabs shift downward a little after the page loads to make room for the My Program Snapshot table, so
            // we have to add a little sleep here because Selenium might try to click on an element that moved after all wait criteria is satisfied
            Thread.Sleep(0400);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.PERCredentialStaffPage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.PERCredentialStaffPage.RefereesTabTraineeTblFirstRowVisible, Criteria.PERCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }


        #endregion methods: repeated per page

        #region methods: page specific


        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.PERCredentialStaffPage.RefereesTab))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RefereesTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.PERCredentialStaffPage.RefereesTabTraineeTblFirstRowVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtn, ElementCriteria.IsVisible))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignAssessor3rdAssFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAny(Criteria.PERCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElemNotVisible,
                        Criteria.PERCredentialStaffPage.BackGroundBackDropNotExists);
                }

                return;
            }            

            if (Browser.Exists(Bys.PERCredentialStaffPage.AssessorTab))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssessorTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.PERCredentialStaffPage.AssessorTabTraineeTblFirstRowVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormSubmitBtn, ElementCriteria.IsVisible))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignReferee2PERRefsFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.PERCredentialStaffPage.AssignReferee2PERRefsFormFirstRefSelElemNotVisible,
                        Criteria.PERCredentialStaffPage.BackGroundBackDropNotExists);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERCredentialStaffPage.AssignAssessor2AssFormSubmitBtn, ElementCriteria.IsVisible))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignAssessor2AssFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.PERCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElemNotVisible,
                        Criteria.PERCredentialStaffPage.BackGroundBackDropNotExists);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERCredentialStaffPage.FinalReviewFormSubmitBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FinalReviewFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();

                    if (Browser.Exists(Bys.PERCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElem))
                    {
                        this.WaitUntilAll(Criteria.PERCredentialStaffPage.FinalReviewFormAchievedRdoNotVisible,
                        Criteria.PERCredentialStaffPage.BackGroundBackDropNotExists);
                        return;
                    }
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }
        }

        /// <summary>
        /// Clicks on the Referees tab if we are not already there, clicks the Assign Referees button for a user-specified trainee, chooses
        /// a user-specified first and second referee, then clicks the Submit button
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="referee1FullName">The first and last name of the referee</param>
        /// <param name="referee2FullName">The first and last name of the referee</param>
        public void AssignReferees(string traineeFullName, string referee1FullName, string referee2FullName)
        {
            ClickAndWait(RefereesTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, RefereesTabTraineeTbl, Bys.PERCredentialStaffPage.RefereesTabTraineeTblFirstRow,
                traineeFullName, null, "Assign Referee ");

            this.WaitUntil(TimeSpan.FromMinutes(1), Criteria.PERCredentialStaffPage.AssignReferee2PERRefsFormFirstRefSelElemHasItemsIsVisible);
            Thread.Sleep(0200);

            AssignReferee2PERRefsFormFirstRefSelElem.SelectByText(referee1FullName);
            AssignReferee2PERRefsFormSecondRefSelElem.SelectByText(referee2FullName);

            ClickAndWait(AssignReferee2PERRefsFormSubmitBtn);
        }

        /// <summary>
        /// Clicks on the Assessors tab if we are not already there, clicks the Make Final Review button for a user-specified trainee,
        /// Clicks on either the Achieved or Not Achieved radio button, enters text into Request Comments field, then clicks the Submit button
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Assessor tab</param>
        /// <param name="achieved">true or false depending on if you want to achieve the trainee or not</param>
        public void MakeFinalReview(string traineeFullName, bool achieved)
        {
            ClickAndWait(AssessorTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AssessorTabTraineeTbl, Bys.PERCredentialStaffPage.AssessorTabTraineeTblFirstRow,
                traineeFullName, null, "Make Final Review  ");

            this.WaitUntil(TimeSpan.FromSeconds(10), Criteria.PERCredentialStaffPage.FinalReviewFormAchievedRdoVisible);

            if (achieved)
            {
                FinalReviewFormAchievedRdo.Click();
            }
            else
            {
                FinalReviewFormNotAchievedRdo.Click();
            }

            FinalReviewFormRequestCommentsTxt.SendKeys(DataUtils.GetRandomSentence(12));

            ClickAndWait(FinalReviewFormSubmitBtn);
        }

        /// <summary>
        /// Clicks on the Assessors tab if we are not already there, clicks the Assign Assessors button for a user-specified trainee, chooses
        /// a user-specified first and second assessor, then clicks the Submit button
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="assessor1FullName">The first and last name of the assessor</param>
        /// <param name="assessor2FullName">The first and last name of the assessor</param>
        public void AssignFirst2Assessors(string traineeFullName, string Assessor1FullName, string Assessor2FullName)
        {
            ClickAndWait(AssessorTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AssessorTabTraineeTbl, Bys.PERCredentialStaffPage.AssessorTabTraineeTblFirstRow,
                traineeFullName, null, "Assign Assessors ");

            this.WaitUntil(TimeSpan.FromMinutes(1), Criteria.PERCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElemHasItemsIsVisible);
            Thread.Sleep(0200);

            AssignAssessor2AssFormFirstAssSelElem.SelectByText(Assessor1FullName);
            AssignAssessor2AssFormSecondAssSelElem.SelectByText(Assessor2FullName);

            ClickAndWait(AssignAssessor2AssFormSubmitBtn);
        }

        /// <summary>
        /// Clicks on the Assessors tab if we are not already there, clicks the Assign Third Assessor button for a user-specified trainee, chooses
        /// a user-specified assessor, then clicks the Submit button
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Referee tab</param>
        /// <param name="assessor3FullName">The first and last name of the assessor</param>
        public void Assign3rdAssessor(string traineeFullName, string assessor3FullName)
        {
            ClickAndWait(AssessorTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AssessorTabTraineeTbl, Bys.PERCredentialStaffPage.AssessorTabTraineeTblFirstRow,
                traineeFullName, null, "Assign Third Assessor ");

            this.WaitUntil(TimeSpan.FromMinutes(1), Criteria.PERCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElemHasItemsIsVisible);
            Thread.Sleep(0200);

            AssignAssessor3rdAssFormThirdAssSelElem.SelectByText(assessor3FullName);

            ClickAndWait(AssignAssessor3rdAssFormSubmitBtn);
        }

        #endregion methods: page specific



    }


}