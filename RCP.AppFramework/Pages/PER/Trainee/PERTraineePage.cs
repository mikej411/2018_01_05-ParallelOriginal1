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
    public class PERTraineePage : RCPPage, IDisposable
    {
        #region constructors
        public PERTraineePage(IWebDriver driver) : base(driver)
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

        public IWebElement MilestonesTbl { get { return this.FindElement(Bys.PERTraineePage.MilestonesTbl); } }
        public IWebElement MilestonesTblFirstRow { get { return this.FindElement(Bys.PERTraineePage.MilestonesTblFirstRow); } }
        public IWebElement YourReplySaveChangesBtn { get { return this.FindElement(Bys.PERTraineePage.YourReplySaveChangesBtn); } }
        public IWebElement ResubmitBtn { get { return this.FindElement(Bys.PERTraineePage.ResubmitBtn); } }
        public IWebElement YourReplyTxt { get { return this.FindElement(Bys.PERTraineePage.YourReplyTxt); } }
        public SelectElement StatusSelElem { get { return new SelectElement(this.FindElement(Bys.PERTraineePage.StatusSelElem)); } }
        public IWebElement Referee1PERValueLbl { get { return this.FindElement(Bys.PERTraineePage.Referee1PERValueLbl); } }
        public IWebElement Referee2PERValueLbl { get { return this.FindElement(Bys.PERTraineePage.Referee2PERValueLbl); } }
        public IWebElement Referee3ValueLbl { get { return this.FindElement(Bys.PERTraineePage.Referee3ValueLbl); } }
        public IWebElement ReviewStageValueLbl { get { return this.FindElement(Bys.PERTraineePage.ReviewStageValueLbl); } }
        public IWebElement SubmitPortfolioFormSubmitBtn { get { return this.FindElement(Bys.PERTraineePage.SubmitPortfolioFormSubmitBtn); } }
        public IWebElement EvidForAchieveFormBrowseHiddenBtn { get { return this.FindElement(Bys.PERTraineePage.EvidForAchieveFormBrowseHiddenBtn); } }
        public IList<IWebElement> MilestonesInMilestonesTblLnks { get { return this.FindElements(Bys.PERTraineePage.MilestonesInMilestonesTblLnks); } }
        // Locating ONLY required milestones here. Doing this by using LINQ, and filtering out milestones that have the word "optional" in them
        public IList<IWebElement> RequiredMilestonesInMilestonesTblLnks { get { return this.FindElements(Bys.PERTraineePage.MilestonesInMilestonesTblLnks).Where(m => !m.Text.ToLower().Contains("optional")).ToList(); } }
        public IList<IWebElement> EvidenceTableUpdateLnks { get { return this.FindElements(Bys.PERTraineePage.EvidenceTableUpdateLnks); } }
        public IWebElement SrcScriptElem { get { return this.FindElement(Bys.PERTraineePage.SrcScriptElem); } }
        public IWebElement EvidForAchieveFormCloseLnk { get { return this.FindElement(Bys.PERTraineePage.EvidForAchieveFormCloseLnk); } }
        public IList<IWebElement> EvidForAchieveFormFileRow { get { return this.FindElements(Bys.PERTraineePage.EvidForAchieveFormFileRow); } }
        public IWebElement BackToDashboardBtn { get { return this.FindElement(Bys.PERTraineePage.BackToDashboardBtn); } }
        public IWebElement DescriptionTxt { get { return this.FindElement(Bys.PERTraineePage.DescriptionTxt); } }
        public IWebElement DescriptionSaveChangesBtn { get { return this.FindElement(Bys.PERTraineePage.DescriptionSaveChangesBtn); } }
        public IWebElement MarkCompleteBtn { get { return this.FindElement(Bys.PERTraineePage.MarkCompleteBtn); } }
        public IList<IWebElement> EvidenceTblRows { get { return this.FindElements(Bys.PERTraineePage.EvidenceTblRows); } }
        public IWebElement SubmitPortfolioBtn { get { return this.FindElement(Bys.PERTraineePage.SubmitPortfolioBtn); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                // First we have to wait for the iFrame to be enabled. This is handled int he PageReady property
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.PERTraineePage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);

                // After switching to the frame, we have to wait for the Review Stage value label to appear first
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.PERTraineePage.ReviewStageValueLblHasText);
                this.WaitUntilAny(TimeSpan.FromSeconds(20), Criteria.PERTraineePage.MilestonesTblEnabled,
                Criteria.PERTraineePage.SubmitPortfolioBtnVisible);
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
            // First we have to wait for the iFrame to be enabled. This is handled in the PageReady property
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.PERTraineePage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);

            // After switching to the frame, we have to wait for the Review Stage value label to appear first
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.PERTraineePage.ReviewStageValueLblHasText);
            this.WaitUntilAny(TimeSpan.FromSeconds(20), Criteria.PERTraineePage.MilestonesTblEnabled,
            Criteria.PERTraineePage.SubmitPortfolioBtnVisible);
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
        /// Selects "Needs Additional Information" in the Status drop down, clicks on the user-specified milestone, 
        /// enters user-specified text into the "Your Reply" text box, clicks Save, then goes back to the Dashboard
        /// </summary>
        /// <param name="milestoneName">The milestone name as it appears in the text of the HTML elementThe exact text of the milestone inside the milestone table</param>
        /// <param name="additionalInfoText">The text you want to enter for the requested or submitted additional infoThe text of your choice</param>
        public void SubmitAdditionalInfo(string milestoneName, string additionalInfoText)
        {
            this.WaitUntilAny(Criteria.PERTraineePage.MilestonesTblMilestoneNameLinksEnabled);

            ElemSet.ScrollToElement(Browser, MilestonesInMilestonesTblLnks[0]);
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, MilestonesTbl, Bys.PERTraineePage.MilestonesTblFirstRow, milestoneName, null, milestoneName);
            this.WaitUntilAll(Criteria.PERTraineePage.EvidenceTblUpdateLinksEnabledAndVisible);

            YourReplyTxt.SendKeys(additionalInfoText);
            this.WaitUntilAll(Criteria.PERTraineePage.YourReplySaveChangesButtonVisible);

            ClickAndWait(YourReplySaveChangesBtn);

            ClickAndWait(ResubmitBtn);
        }

        /// <summary>
        /// Clicks on each milestone, uploads a document, saves and completes the milestone, then submits the portfolio to the Credential Staff user
        /// </summary>
        public List<string> CompleteAllMilestones()
        {
            string currentMileStoneText = "";

            // Have to wait for the milestones to appear in the table first. Note that we cant use this Wait Criteria
            // our WaitForInitialize method because sometimes we login expecting there not to be any milestones
            this.WaitUntilAny(Criteria.PERTraineePage.MilestonesTblMilestoneNameLinksEnabled);

            StatusSelElem.SelectByText("Not Started");

            // Get all of the the milestone names first so we can then return them at the end of the method. Note that we have 
            // to get the textContent property, not Selenium's Text property. This is because the text inside the HTML has 2 
            // spaces between the milestone number and the title. Selenium's text property only has 1 space
            List<string> milestoneNames = MilestonesInMilestonesTblLnks.Select(t => t.GetAttribute("textContent")).ToList();

            // Get the count of all rows on the milestone table and then loop through them. 
            int countOfRowsInMilestoneTable = MilestonesInMilestonesTblLnks.Count;

            for (int i = 0; i <= countOfRowsInMilestoneTable; i++)
            {
                if (MilestonesInMilestonesTblLnks.Count > 0)
                {
                    // Store the text of the milestone that we are manipulating, so that we can use this text in the method CleanStaleMilestone
                    // See description of that method for explanation of this issue
                    currentMileStoneText = MilestonesInMilestonesTblLnks[0].Text;

                    ClickAndWait(MilestonesInMilestonesTblLnks[0]);

                    UploadFileForAllEvidenceRows();

                    AddDescriptionAndMarkMilestoneComplete();

                    ClickAndWait(BackToDashboardBtn);
                }
                else
                {
                    break;
                }

                StatusSelElem.SelectByText("Not Started");

                CleanStaleMilestone(currentMileStoneText);
            }

            return milestoneNames;
        }

        /// <summary>
        /// For all required milestones, this method clicks on each milestone, uploads a document, saves and completes the milestone. 
        /// This does not complete the milestones labeled as "optional". Note that for this method to work, you
        /// have to label your optional milestones as "optional" when creating your program in Lifetime Support
        /// </summary>
        public List<string> CompleteAllRequiredMilestones()
        {
            string currentMileStoneText = "";

            // Have to wait for the milestones to appear in the table first. Note that we cant use this Wait Criteria
            // our WaitForInitialize method because sometimes we login expecting there not to be any milestones
            this.WaitUntilAny(Criteria.PERTraineePage.MilestonesTblMilestoneNameLinksEnabled);

            StatusSelElem.SelectByText("Not Started");

            // Get all of the the milestone names first so we can then return them at the end of the method. Note that we have 
            // to get the textContent property, not Selenium's Text property. This is because the text inside the HTML has 2 
            // spaces between the milestone number and the title. Selenium's text property only has 1 space
            List<string> milestoneNames = MilestonesInMilestonesTblLnks.Select(t => t.GetAttribute("textContent")).ToList();

            // Get the count of all rows (except for the last one) on the milestone table and then loop through them. The reason
            // why we are leaving the last one out is because this is the optional milestone. Note that for this method to work, a
            // PER Program has to be created that includes an optional milestone that comes last in the sort order
            int countOfRowsInMilestoneTable = MilestonesInMilestonesTblLnks.Count;

            for (int i = 0; i <= countOfRowsInMilestoneTable; i++)
            {
                if (RequiredMilestonesInMilestonesTblLnks.Count > 0)
                {
                    // Store the text of the milestone that we are manipulating, so that we can use this text in the method CleanStaleMilestone
                    // See description of that method for explanation of this issue
                    currentMileStoneText = RequiredMilestonesInMilestonesTblLnks[0].Text;

                    RequiredMilestonesInMilestonesTblLnks[0].Click();
                    this.WaitUntilAll(Criteria.PERTraineePage.EvidenceTblUpdateLinksEnabledAndVisible,
                        Criteria.PERTraineePage.BackToDashboardBtnVisible);
                    UploadFileForAllEvidenceRows();

                    AddDescriptionAndMarkMilestoneComplete();

                    ClickAndWait(BackToDashboardBtn);
                }
                else
                {
                    break;
                }

                StatusSelElem.SelectByText("Not Started");

                CleanStaleMilestone(currentMileStoneText);
            }

            return milestoneNames;
        }

        /// <summary>
        /// Stale Milestone Issue: Sometimes after we complete a milestone and then click the Back To Dashboard button, the milestone doesnt 
        /// properly disappear, So we are adding the following logic to click on this milestone again, then click back again, to refresh the
        /// table so the milestone disappears. I was going to create a bug for this, but I cant reproduce it manually
        /// </summary>
        /// <param name="milestoneText">The milestone that may be left in a stale state</param>
        private void CleanStaleMilestone(string milestoneText)
        {
            IList<IWebElement> staleMilestoneElement = Browser.FindElements(By.XPath(string.Format("//a[text()='{0}']", milestoneText)));

            if (staleMilestoneElement.Count > 0)
            {
                staleMilestoneElement[0].Click();
                this.WaitUntilAll(Criteria.PERTraineePage.EvidenceTblUpdateLinksEnabledAndVisible,
                    Criteria.PERTraineePage.BackToDashboardBtnVisible);
                ClickAndWait(BackToDashboardBtn);
            }
        }

        /// <summary>
        /// Clicks the Submit Portfolio button, then clicks the Submit button on the resulting popup
        /// </summary>
        public void SubmitPortfolio()
        {
            ClickAndWait(SubmitPortfolioBtn);

            ClickAndWait(SubmitPortfolioFormSubmitBtn);
        }

        /// <summary>
        /// Adds a description to the Description textbox, clicks Save Changes, then clicks the Mark Complete button
        /// </summary>
        private void AddDescriptionAndMarkMilestoneComplete()
        {
            DescriptionTxt.SendKeys("wsefwefwefwewe");
            this.WaitUntilAll(Criteria.PERTraineePage.DescriptionSaveChangesButtonVisible);

            ClickAndWait(DescriptionSaveChangesBtn);
            Thread.Sleep(0200);

            ClickAndWait(MarkCompleteBtn);
        }
        /// <summary>
        /// Uploads a file for every row that exists within the Evidence for Achievement of Milestone table
        /// </summary>
        private void UploadFileForAllEvidenceRows()
        {
            // Get the count of rows in the evidence table, then loop through them to add evidence to each one
            int countOfEvidenceRows = EvidenceTblRows.Count;
            for (int j = 0; j < countOfEvidenceRows; j++)
            {
                // If no file has been added yet for a row. We are checking this by seeing if findelements returns zero for an X link element
                // If a file is uploaded, this X link appears on the row
                if (EvidenceTblRows[j].FindElements(By.XPath("./descendant::a[@ng-show='canMarkComplete']")).Count == 0)
                {
                    // We have to find the Update link here, because if we find it outside of the for loop, we get a stale element exception
                    // for some reason
                    IWebElement evidenceUpdateLnk = EvidenceTblRows[j].FindElement(By.XPath("./descendant::a[text()='Update']"));
                    ElemSet.ScrollToElement(Browser, evidenceUpdateLnk);
                    evidenceUpdateLnk.Click();
                    this.WaitUntilAll(Criteria.PERTraineePage.EvidForAchieveFormDoneBtnEnabled);
                    Thread.Sleep(0400); // Need a little sleep here before upload

                    FileUtils.UploadFileUsingSendKeys(Browser, EvidForAchieveFormBrowseHiddenBtn, Bys.PERTraineePage.EvidForAchieveFormBrowseHiddenBtn, "C:\\SeleniumAutoIt\\test.txt");
                    this.WaitUntilAll(Criteria.PERTraineePage.EvidForAchieveFormFileRowEnabledAndVisible);

                    ElemSet.ScrollToElement(Browser, EvidForAchieveFormCloseLnk);
                    ClickAndWait(EvidForAchieveFormCloseLnk);
                }
            }
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.PERTraineePage.BackToDashboardBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackToDashboardBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAny(TimeSpan.FromSeconds(180), Criteria.PERTraineePage.MilestonesTblMilestoneNameLinksEnabled,
                        Criteria.PERTraineePage.SubmitPortfolioBtnVisible, Criteria.PERTraineePage.ReviewStageValueLblHasText);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.MilestonesInMilestonesTblLnks))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MilestonesInMilestonesTblLnks[0].GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAll(Criteria.PERTraineePage.EvidenceTblUpdateLinksEnabledAndVisible,
                        Criteria.PERTraineePage.BackToDashboardBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.ResubmitBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ResubmitBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAll(Criteria.PERTraineePage.ReviewStageValueLblHasText);
                    return;
                }
            }
            
            if (Browser.Exists(Bys.PERTraineePage.EvidForAchieveFormCloseLnk))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EvidForAchieveFormCloseLnk.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntil(Criteria.PERTraineePage.UploadedFileLnkVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.YourReplySaveChangesBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == YourReplySaveChangesBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    // Add a better wait here. Note that waiting for the save changes button to not be visible doesnt work for some reason
                    Thread.Sleep(2000);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.DescriptionSaveChangesBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DescriptionSaveChangesBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAll(Criteria.PERTraineePage.DescriptionSaveChangesButtonNotVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.MarkCompleteBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkCompleteBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAll(Criteria.PERTraineePage.MarkCompleteButtonNotVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.SubmitPortfolioFormSubmitBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SubmitPortfolioFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAll(Criteria.PERTraineePage.SubmitPortfolioFormSubmitBtnNotVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERTraineePage.SubmitPortfolioBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SubmitPortfolioBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem);
                    this.WaitUntilAll(Criteria.PERTraineePage.SubmitPortfolioFormSubmitBtnVisible);
                    return;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }
        }



        #endregion methods: page specific



    }


}