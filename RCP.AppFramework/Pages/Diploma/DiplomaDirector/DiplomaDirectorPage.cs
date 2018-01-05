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
    public class DiplomaDirectorPage : RCPPage, IDisposable
    {
        #region constructors

        public DiplomaDirectorPage(IWebDriver driver) : base(driver)
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

        public IWebElement MarkAsAchievedFormSubmitBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkAsAchievedFormSubmitBtn); } }
        public IWebElement MarkAsNotAchievedFormSubmitBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkAsNotAchievedFormSubmitBtn); } }
        public IWebElement ResubmittedMilestonesTbl { get { return this.FindElement(Bys.DiplomaDirectorPage.ResubmittedMilestonesTbl); } }
        public IWebElement ResubmittedMilestonesTblFirstRow { get { return this.FindElement(Bys.DiplomaDirectorPage.ResubmittedMilestonesTblFirstRow); } }
        public IWebElement RequestAdditionalInfoBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.RequestAdditionalInfoBtn); } }
        public IWebElement RequestAdditionalInfoFormSubmitBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.RequestAdditionalInfoFormSubmitBtn); } }
        public IWebElement RequestAdditionalInfoFormCommentsTxt { get { return this.FindElement(Bys.DiplomaDirectorPage.RequestAdditionalInfoFormCommentsTxt); } }
        public IWebElement BackToDashboardBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.BackToDashboardBtn); } }
        public IList<IWebElement> MilestonesInMilestonesTblLnks { get { return this.FindElements(Bys.DiplomaDirectorPage.MilestonesInMilestonesTblLnks); } }
        public IWebElement MarkAsAchievedBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkAsAchievedBtn); } }
        public IWebElement MilestonesTbl { get { return this.FindElement(Bys.DiplomaDirectorPage.MilestonesTbl); } }
        public IWebElement MarkAsNotAchievedBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkAsNotAchievedBtn); } }
        public IWebElement MarkSelectedPortfoliosAsAchievedBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkSelectedPortfoliosAsAchievedBtn); } }
        public IWebElement MarkSelPortAchFormSubmitBtn { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkSelPortAchFormSubmitBtn); } }
        public IWebElement MarkSelPortAchFormIAttestChk { get { return this.FindElement(Bys.DiplomaDirectorPage.MarkSelPortAchFormIAttestChk); } }
        public IWebElement PortfoliosUnderReviewTbl { get { return this.FindElement(Bys.DiplomaDirectorPage.PortfoliosUnderReviewTbl); } }
        public IWebElement PortfoliosUnderReviewTblBodyRow { get { return this.FindElement(Bys.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRow); } }
        public IWebElement PortfoliosUnderReviewTab { get { return this.FindElement(Bys.DiplomaDirectorPage.PortfoliosUnderReviewTab); } }
        public IWebElement ResubmittedMilestonesTab { get { return this.FindElement(Bys.DiplomaDirectorPage.ResubmittedMilestonesTab); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaDirectorPage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.DiplomaDirectorPage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
                this.WaitUntilAny(TimeSpan.FromSeconds(120), Criteria.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRowCheckBoxVisible);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DiplomaDirectorPage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.DiplomaDirectorPage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
            this.WaitUntilAny(TimeSpan.FromSeconds(60), Criteria.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRowCheckBoxVisible);
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
        /// Clicks the user-specified trainee in the Portfolios table, clicks on the milestone links in the Milestone table, clicks on the Request
        /// Additional Information button, enters text into the Comments field, clicks the Submit button
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        /// <param name="milestoneName">The milestone name as it appears in the text of the HTML element</param>
        /// <param name="additionalInfoText">The text you want to enter for the requested or submitted additional info</param>
        public void RequestAdditionalInfo(string traineeFullName, string milestoneName, string additionalInfoText)
        {
            GoToTraineeDashboard(traineeFullName);

            GoToSpecificMilestone(milestoneName);

            ClickAndWait(RequestAdditionalInfoBtn);

            RequestAdditionalInfoFormCommentsTxt.SendKeys(additionalInfoText);

            ClickAndWait(RequestAdditionalInfoFormSubmitBtn);
        }

        /// <summary>
        /// Clicks the user-specified trainee in the Portfolios table and waits for the Dashboard for that trainee to appear
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        public void GoToTraineeDashboard(string traineeFullName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, PortfoliosUnderReviewTbl, Bys.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRow,
                traineeFullName, null, traineeFullName);
            Browser.WaitForElement(Bys.DiplomaDirectorPage.MilestonesTblFirstRow);
            Thread.Sleep(0300);
        }

        /// <summary>
        /// Clicks on the user-specified milestone link in the table from the trainee dashboard page and waits for that page to appear
        /// </summary>
        /// <param name="milestoneName">The trainee's first and last name</param>
        public void GoToSpecificMilestone(string milestoneName)
        {
            ElemSet.ScrollToElement(Browser, Browser.FindElements(Bys.DiplomaTraineePage.MilestonesInMilestonesTblLnks)[0]);
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, Browser.FindElement(Bys.DiplomaTraineePage.MilestonesTbl), Bys.DiplomaTraineePage.MilestonesTblFirstRow, milestoneName, null, milestoneName);
            this.WaitUntil(Criteria.DiplomaDirectorPage.MainFrameVisibleAndEnabled);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntil(Criteria.DiplomaDirectorPage.MarkAsAchievedBtnVisible);
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.DiplomaDirectorPage.MarkSelectedPortfoliosAsAchievedBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkSelectedPortfoliosAsAchievedBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaDirectorPage.MarkSelPortAchFormSubmitBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaDirectorPage.MarkSelPortAchFormSubmitBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkSelPortAchFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaDirectorPage.MarkSelPortAchFormSubmitBtnNotVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaDirectorPage.RequestAdditionalInfoBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestAdditionalInfoBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaDirectorPage.RequestAdditionalInfoFormSubmitBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaDirectorPage.RequestAdditionalInfoFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestAdditionalInfoFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaDirectorPage.ReviewStageValueLblVisible);
                    Thread.Sleep(1000);
                    return;
                }
            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                "or if the button is already added, then the page you were on did not contain the button.");
        }

        /// <summary>
        /// Checks the check box in the Portfolios Under Review tab for a user-specified trainee, clicks the Mark Selected Portfolio as Achieved button, checks the
        /// I Attest check box on the Mark Selected Portfolios as Achieved form, then clicks Submbit
        /// </summary>
        /// <param name="traineeFullName"></param>
        public void MarkPortfolioAchieved(string traineeFullName)
        {
            IWebElement row = ElemGet.Grid_GetRowByRowName(PortfoliosUnderReviewTbl, Bys.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRow, traineeFullName);

            ElemSet.Grid_ClickElementWithoutTextInsideRow(row, "input");
            ClickAndWait(MarkSelectedPortfoliosAsAchievedBtn);

            MarkSelPortAchFormIAttestChk.Click();
            // This failed once or twice, maybe because after I click on the I Atttest chekc box above, the Submit button sometimes
            // takes a long time to enable. So adding a sleep and then a try catch
            Thread.Sleep(0400);
            try
            {
                ClickAndWait(MarkSelPortAchFormSubmitBtn);
            }
            catch
            {
                ClickAndWait(MarkSelPortAchFormSubmitBtn);
            }
        }

        #endregion methods: page specific



    }


}