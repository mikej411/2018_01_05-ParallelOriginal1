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
    public class DiplomaAssessorPage : RCPPage, IDisposable
    {
        #region constructors
        public DiplomaAssessorPage(IWebDriver driver) : base(driver)
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

        public IWebElement MarkAsAchievedFormSubmitBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.MarkAsAchievedFormSubmitBtn); } }
        public IWebElement MarkAsNotAchievedFormSubmitBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.MarkAsNotAchievedFormSubmitBtn); } }
        public IWebElement ResubmittedMilestonesTbl { get { return this.FindElement(Bys.DiplomaAssessorPage.ResubmittedMilestonesTbl); } }
        public IWebElement ResubmittedMilestonesTblFirstRow { get { return this.FindElement(Bys.DiplomaAssessorPage.ResubmittedMilestonesTblFirstRow); } }
        public IWebElement RequestAdditionalInfoBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.RequestAdditionalInfoBtn); } }
        public IWebElement RequestAdditionalInfoFormSubmitBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.RequestAdditionalInfoFormSubmitBtn); } }
        public IWebElement RequestAdditionalInfoFormCommentsTxt { get { return this.FindElement(Bys.DiplomaAssessorPage.RequestAdditionalInfoFormCommentsTxt); } }
        public IWebElement BackToDashboardBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.BackToDashboardBtn); } }
        public IList<IWebElement> MilestonesInMilestonesTblLnks { get { return this.FindElements(Bys.DiplomaAssessorPage.MilestonesInMilestonesTblLnks); } }

        public IWebElement MarkAsAchievedBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.MarkAsAchievedBtn); } }
        public IWebElement MilestonesTbl { get { return this.FindElement(Bys.DiplomaAssessorPage.MilestonesTbl); } }
        public IWebElement MarkAsNotAchievedBtn { get { return this.FindElement(Bys.DiplomaAssessorPage.MarkAsNotAchievedBtn); } }
        public IWebElement PortfolioAssignmentsTbl { get { return this.FindElement(Bys.DiplomaAssessorPage.PortfolioAssignmentsTbl); } }
        public IWebElement PortfolioAssignmentsTblFirstRow { get { return this.FindElement(Bys.DiplomaAssessorPage.PortfolioAssignmentsTblFirstRow); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaAssessorPage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntilAny(TimeSpan.FromSeconds(120), Criteria.DiplomaAssessorPage.PortfolioAssignmentsTblFirstRowVisible);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DiplomaAssessorPage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntilAny(TimeSpan.FromSeconds(60), Criteria.DiplomaAssessorPage.PortfolioAssignmentsTblFirstRowVisible);
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
            if (Browser.Exists(Bys.DiplomaAssessorPage.BackToDashboardBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackToDashboardBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaAssessorPage.MilestonesTblEnabled);
                    Thread.Sleep(0200);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaAssessorPage.RequestAdditionalInfoBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestAdditionalInfoBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaAssessorPage.RequestAdditionalInfoFormSubmitBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaAssessorPage.RequestAdditionalInfoFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestAdditionalInfoFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaAssessorPage.ReviewStageValueLblVisible);
                    Thread.Sleep(1000);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaAssessorPage.MarkAsAchievedFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkAsAchievedFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaAssessorPage.MilestonesTblEnabled);
                    Thread.Sleep(0300);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaAssessorPage.MarkAsNotAchievedFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkAsNotAchievedFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaAssessorPage.MilestonesTblEnabled);
                    Thread.Sleep(0300);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaAssessorPage.MarkAsAchievedBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkAsAchievedBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaAssessorPage.MarkAsAchievedFormSubmitBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaAssessorPage.MarkAsNotAchievedBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkAsNotAchievedBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAny(Criteria.DiplomaAssessorPage.MarkAsNotAchievedFormSubmitBtnVisible);
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
        /// Clicks on the Review button for a trainee to go to the trainee dashboard, clicks on a user-specified milestone, returns either true or false
        /// depeneding on if it finds the additional information that should have been sent by the trainee
        /// then verifies
        /// </summary>
        /// <param name="traineeFullName">Trainee's first and last name</param>
        /// <param name="milestoneName">The milestone you want to verify</param>
        /// <param name="additionalInfo">The additional info text that you want to verify is present for the milestone</param>
        public bool VerifyAdditionalInfo(string traineeFullName, string milestoneName, string additionalInfo)
        {
            GoToTraineeDashboard(traineeFullName);

            GoToSpecificMilestone(milestoneName);

            if (Browser.FindElements(By.XPath(string.Format("//span[text()='{0}']", additionalInfo))).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Clicks on the Review button for a user-specified trainee and waits for the Dashboard for that trainee to appear
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last name</param>
        public void GoToTraineeDashboard(string traineeFullName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, PortfolioAssignmentsTbl, Bys.DiplomaAssessorPage.PortfolioAssignmentsTblFirstRow,
                traineeFullName, null, "Review");
            this.WaitUntil(TimeSpan.FromMinutes(1), Criteria.DiplomaAssessorPage.MilestonesTblFirstRowEnabled);
            Thread.Sleep(0300);
        }

        /// <summary>
        /// Clicks on the user-specified milestone link in the table from the trainee dashboard page and waits for that page to appear
        /// </summary>
        /// <param name="milestoneName">The trainee's first and last name</param>
        public void GoToSpecificMilestone(string milestoneName)
        {
            ElemSet.ScrollToElement(Browser, MilestonesInMilestonesTblLnks[0]);
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, MilestonesTbl, Bys.DiplomaAssessorPage.MilestonesTblFirstRow, milestoneName, null, milestoneName);
            this.WaitUntil(Criteria.DiplomaAssessorPage.MainFrameVisibleAndEnabled);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntil(Criteria.DiplomaAssessorPage.MarkAsAchievedBtnVisible);
        }

        /// <summary>
        /// Clicks the Review button for a user-specified trainee, clicks on the milestone links in the Milestone table, clicks on the Request
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
        /// Clicks the Review button for a user-specified Trainee, clicks on the milestone links in the Milestone table, then marks the milestone
        /// as Achieved or Not Achieved, depending on the AllAchieved parameter
        /// </summary>
        /// <param name="traineeFullName">The trainee's first and last nameFirst and last name of the trainee</param>
        /// <param name="AllAchieved">If true, then mark all milestones as achieved, else mark all but 1 milestone as achieved</param>
        public void MarkAllMilestones(string traineeFullName, bool AllAchieved)
        {
            GoToTraineeDashboard(traineeFullName);

            int countOfRowsInMilestoneTable = MilestonesInMilestonesTblLnks.Count;
            string currentMileStoneText = "";

            // loop through every row on the milestone table
            for (int i = 1; i <= countOfRowsInMilestoneTable; i++)
            {
                currentMileStoneText = MilestonesInMilestonesTblLnks[0].Text;
                ElemSet.ScrollToElement(Browser, MilestonesInMilestonesTblLnks[0]);
                MilestonesInMilestonesTblLnks[0].Click();
                this.WaitUntil(Criteria.DiplomaAssessorPage.MainFrameVisibleAndEnabled);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntil(Criteria.DiplomaAssessorPage.MarkAsAchievedBtnVisible);

                if (AllAchieved == true) // Mark all milestones as achieved if set to true
                {
                    ClickAndWait(MarkAsAchievedBtn);
                    ClickAndWait(MarkAsAchievedFormSubmitBtn);
                }

                else // Mark all milestones except for one as achieved. Mark the last one as not achieved
                {

                    if (i < countOfRowsInMilestoneTable) // If we are not on the last row, mark as achieved. Else mark as not achieved
                    {
                        ClickAndWait(MarkAsAchievedBtn);
                        ClickAndWait(MarkAsAchievedFormSubmitBtn);
                    }
                    else
                    {
                        ClickAndWait(MarkAsNotAchievedBtn);
                        ClickAndWait(MarkAsNotAchievedFormSubmitBtn);
                    }
                }
            }
        }

        #endregion methods: page specific



    }


}