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
    public class DiplomaCredentialStaffPage : RCPPage, IDisposable
    {
        #region constructors
        public DiplomaCredentialStaffPage(IWebDriver driver) : base(driver)
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

        public IWebElement BackGroundBackDrop { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.BackGroundBackDrop); } }       
        public IWebElement NoPortfoliosLbl { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.NoPortfoliosLbl); } }
        public IWebElement RecordPaymentFormDateTxt { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.RecordPaymentFormDateTxt); } }
        public IWebElement RecordPaymentFormCommentsTxt { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.RecordPaymentFormCommentsTxt); } }
        public IWebElement RecordPaymentFormSubmitBtn { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.RecordPaymentFormSubmitBtn); } }
        public IWebElement MarkPortfolioAsAchievedFormSubmitBtn { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.MarkPortfolioAsAchievedFormSubmitBtn); } }
        public IWebElement ResubmittedMilestonesTbl { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.ResubmittedMilestonesTbl); } }
        public IWebElement ResubmittedMilestonesTblFirstRow { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.ResubmittedMilestonesTblFirstRow); } }
        public IWebElement ResubmitedMilestonesTab { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.ResubmitedMilestonesTab); } }
        public IWebElement MyProgramSnapshotTblFirstRowPrgLnk { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnk); } }
        public SelectElement AssignAssessor3rdAssFormThirdAssSelElem { get { return new SelectElement(this.FindElement(Bys.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElem)); } }
        public IWebElement AssignAssessor3rdAssFormSubmitBtn { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtn); } }
        public IWebElement FinalReviewFormAchievedRdo { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.FinalReviewFormAchievedRdo); } }
        public IWebElement FinalReviewFormNotAchievedRdo { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.FinalReviewFormNotAchievedRdo); } }
        public IWebElement FinalReviewFormRequestCommentsTxt { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.FinalReviewFormRequestCommentsTxt); } }
        public IWebElement AssignAssessor2AssFormTitleLbl { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormTitleLbl); } }
        public IWebElement AssignAssessor2AssFormSubmitBtn { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormSubmitBtn); } }
        public IWebElement PortfoliosUnderReviewTab { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTab); } }
        public IWebElement AssessorTab { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.AssessorTab); } }
        public IWebElement PortfoliosUnderReviewTbl { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTbl); } }
        public IWebElement PortfoliosUnderReviewTblFirstRow { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRow); } }
        public SelectElement AssignAssessor2AssFormFirstAssSelElem { get { return new SelectElement(this.FindElement(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElem)); } }
        public SelectElement AssignAssessor2AssFormSecondAssSelElem { get { return new SelectElement(this.FindElement(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormSecondAssSelElem)); } }
        public IWebElement AssessorTbl { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.AssessorTbl); } }
        public IWebElement AssessorTblFirstRow { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.AssessorTblFirstRow); } }
        public IWebElement FinalReviewFormSubmitBtn { get { return this.FindElement(Bys.DiplomaCredentialStaffPage.FinalReviewFormSubmitBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaCredentialStaffPage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
                this.WaitUntilAny(Criteria.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRowVisible, Criteria.DiplomaCredentialStaffPage.NoPortfoliosLblVisible);
            }
            catch
            {
                RefreshPage();
            }
            // If you look at the page when it loads, the tabs shift downward a little after the page loads to make room for the My Program Snapshot table, so
            // we have to add a little sleep here because Selenium might try to click on an element that moved after all wait criteria is satisfied
            Thread.Sleep(0200);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DiplomaCredentialStaffPage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.DiplomaCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
            this.WaitUntilAny(Criteria.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRowVisible, Criteria.DiplomaCredentialStaffPage.NoPortfoliosLblVisible);
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
            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTab))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PortfoliosUnderReviewTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRowVisible);
                    Thread.Sleep(0400);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.RecordPaymentFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RecordPaymentFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.DiplomaCredentialStaffPage.RecordPaymentFormSubmitBtnSubmitBtnNotVisible, 
                        Criteria.DiplomaCredentialStaffPage.BackGroundBackDropNotExists);
                    // After this backdrop element disappears, I think we need a very small sleep, because I once encountered a StaleElementException
                    // when trying to click something after this above wait, when not having a sleep. Monitor going forward
                    Thread.Sleep(0200);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.MarkPortfolioAsAchievedFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkPortfolioAsAchievedFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtnNotVisible,
                        Criteria.DiplomaCredentialStaffPage.BackGroundBackDropNotExists);
                    // After this backdrop element disappears, I think we need a very small sleep, because I once encountered a StaleElementException
                    // when trying to click something after this above wait, when not having a sleep. Monitor going forward
                    Thread.Sleep(0200);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtn, ElementCriteria.IsVisible))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignAssessor3rdAssFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtnNotVisible,
                        Criteria.DiplomaCredentialStaffPage.BackGroundBackDropNotExists);
                    // After this backdrop element disappears, I think we need a very small sleep, because I once encountered a StaleElementException
                    // when trying to click something after this above wait, when not having a sleep. Monitor going forward
                    Thread.Sleep(0200);
                }

                return;
            }

            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.AssessorTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssessorTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaCredentialStaffPage.AssessorTblFirstRowVisible);
                    Thread.Sleep(0400);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AssignAssessor2AssFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.DiplomaCredentialStaffPage.AssignAssessor2AssFormSubmitBtnNotVisible,
                        Criteria.DiplomaCredentialStaffPage.BackGroundBackDropNotExists);
                    // After this backdrop element disappears, I think we need a very small sleep, because I once encountered a StaleElementException
                    // when trying to click something after this above wait, when not having a sleep. Monitor going forward
                    Thread.Sleep(0200);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaCredentialStaffPage.FinalReviewFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == FinalReviewFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAll(Criteria.DiplomaCredentialStaffPage.FinalReviewFormAchievedRdoNotVisible,
                        Criteria.DiplomaCredentialStaffPage.BackGroundBackDropNotExists);
                    // After this backdrop element disappears, I think we need a very small sleep, because I once encountered a StaleElementException
                    // when trying to click something after this above wait, when not having a sleep. Monitor going forward
                    Thread.Sleep(0200);
                    return;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }
        }

        /// <summary>
        /// Clicks on the Portfolios Under Review tab, clicks on the Assign Reviewer button for a user-specified trainee, assigns 2 assessors,
        /// clicks the Submit button on the confirmation popup window
        /// </summary>
        /// <param name="traineeFullName"></param>
        public void AssignReviewer(string traineeFullName, string assessor1FullName, string assessor2FullName)
        {
            ClickAndWait(PortfoliosUnderReviewTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, PortfoliosUnderReviewTbl, Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRow,
                traineeFullName, null, "Assign Reviewer");

            this.WaitUntil(Criteria.DiplomaCredentialStaffPage.AssignAssessor2AssFormSubmitBtnVisible);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DiplomaCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElemVisible);
            Thread.Sleep(0200);

            ElemSet.ScrollToSelectElement(Browser, AssignAssessor2AssFormFirstAssSelElem);
            AssignAssessor2AssFormFirstAssSelElem.SelectByText(assessor1FullName);
            AssignAssessor2AssFormSecondAssSelElem.SelectByText(assessor2FullName);

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

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AssessorTbl, Bys.DiplomaCredentialStaffPage.AssessorTblFirstRow,
                traineeFullName, null, "Assign Third Assessor");

            this.WaitUntil(Criteria.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtnVisible);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElemVisible);
            Thread.Sleep(0200);

            ElemSet.ScrollToSelectElement(Browser, AssignAssessor3rdAssFormThirdAssSelElem);
            AssignAssessor3rdAssFormThirdAssSelElem.SelectByText(assessor3FullName);

            ClickAndWait(AssignAssessor3rdAssFormSubmitBtn);
        }

        /// <summary>
        /// Clicks on the Portfolios Under Review tab, clicks on the Record Payment button for a user-specified trainee, fills in the date and
        /// comments fields clicks the Submit button on the confirmation popup window
        /// </summary>
        /// <param name="traineeFullName"></param>
        public void RecordPayment(string traineeFullName)
        {
            ClickAndWait(PortfoliosUnderReviewTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, PortfoliosUnderReviewTbl, Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRow,
                traineeFullName, null, "Record Payment");

            this.WaitUntil(Criteria.DiplomaCredentialStaffPage.RecordPaymentFormSubmitBtnVisible);

            RecordPaymentFormDateTxt.SendKeys("09/09/2017");
            RecordPaymentFormDateTxt.SendKeys(Keys.Tab);
            Thread.Sleep(0200);

            RecordPaymentFormCommentsTxt.SendKeys("these are my comments :)");

            ClickAndWait(RecordPaymentFormSubmitBtn);
        }

        /// <summary>
        /// Clicks on the Portfolios Under Review tab, clicks on the Mark As Achieved button for a user-specified trainee,
        /// clicks the Submit button on the confirmation popup window
        /// </summary>
        /// <param name="traineeFullName"></param>
        /// <param name="assessor1FullName"></param>
        public void MarkTraineeAsAchieved(string traineeFullName)
        {
            ClickAndWait(PortfoliosUnderReviewTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, PortfoliosUnderReviewTbl, Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRow,
                traineeFullName, null, "Mark as Achieved");

            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.DiplomaCredentialStaffPage.MarkPortfolioAsAchievedFormSubmitBtnVisible);

            ClickAndWait(MarkPortfolioAsAchievedFormSubmitBtn);
        }

        /// <summary>
        /// Clicks on the Assessors tab if we are not already there, clicks the Make Final Review button for a user-specified trainee,
        /// clicks on either the Achieved or Not Achieved radio button, enters text into Request Comments field, then clicks the Submit button
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameThe exact text of the trainee from the Trainee column inside the Trainee table on the Assessor tab</param>
        /// <param name="achieved">true or false depending on if you want to achieve the trainee or not</param>
        public void MakeFinalReview(string traineeFullName, bool achieved)
        {
            ClickAndWait(AssessorTab);

            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, AssessorTbl, Bys.DiplomaCredentialStaffPage.AssessorTblFirstRow,
                traineeFullName, null, "Make Final Review");

            this.WaitUntil(TimeSpan.FromSeconds(10), Criteria.DiplomaCredentialStaffPage.FinalReviewFormAchievedRdoVisible);

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



        #endregion methods: page specific



    }


}