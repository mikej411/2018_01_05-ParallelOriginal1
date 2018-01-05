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
    public class DiplomaClinicalSupervisorPage : RCPPage, IDisposable
    {
        #region constructors
        public DiplomaClinicalSupervisorPage(IWebDriver driver) : base(driver)
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
        
        public IWebElement UnderReviewTblBodyRowChk { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRowChk); } }
        public IWebElement MarkSelMilestonesAchFormSubmitBtn { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.MarkSelMilestonesAchFormSubmitBtn); } }
        public IWebElement MarkSelectedMilestonesAsAchievedBtn { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.MarkSelectedMilestonesAsAchievedBtn); } }
        public IWebElement UnderReviewTbl { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.UnderReviewTbl); } }
        public IWebElement UnderReviewTblBodyRow { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRow); } }
        public IWebElement RequestAdUnderReviewTabditionalInfoBtn { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.UnderReviewTab); } }
        public IWebElement MilestonesByTraineeTab { get { return this.FindElement(Bys.DiplomaClinicalSupervisorPage.MilestonesByTraineeTab); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(178), Criteria.DiplomaClinicalSupervisorPage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntilAny(TimeSpan.FromSeconds(179), Criteria.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRowCheckBoxVisible);
            }
            catch
            {
                RefreshPage();
            }
            // If you look at the page when it loads, the rows of the table dont all appear at once, they appear one by one really quickly
            // so we have to add a little sleep here because Selenium might try to click on a row that moved after all wait criteria is satisfied
            Thread.Sleep(0400);
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DiplomaClinicalSupervisorPage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntilAny(TimeSpan.FromSeconds(181), Criteria.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRowCheckBoxVisible);
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
            if (Browser.Exists(Bys.DiplomaClinicalSupervisorPage.MarkSelectedMilestonesAsAchievedBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkSelectedMilestonesAsAchievedBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DiplomaClinicalSupervisorPage.MarkSelMilestonesAchFormSubmitBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaClinicalSupervisorPage.MarkSelMilestonesAchFormSubmitBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkSelMilestonesAchFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.DiplomaClinicalSupervisorPage.MarkSelMilestonesAchFormSubmitBtnNotVisible);
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
        /// Clicks on all milestones for a user-specified trainee, clicks the Mark As Achieved button, then clicks Submit on the popup
        /// </summary>
        /// <param name="traineeFullName">First and last name of the trainee</param>
        public void MarkAllMilestonesAchieved(string traineeFullName)
        {
            IList<IWebElement> rows = ElemGet.Grid_GetRowsByRowName(UnderReviewTbl, Bys.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRow, traineeFullName);

            foreach(IWebElement row in rows)
            {
                ElemSet.Grid_ClickElementWithoutTextInsideRow(row, "input");
            }

            ClickAndWait(MarkSelectedMilestonesAsAchievedBtn);

            ClickAndWait(MarkSelMilestonesAchFormSubmitBtn);
        }

        #endregion methods: page specific



    }


}