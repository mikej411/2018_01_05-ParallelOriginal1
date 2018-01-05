using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class MyHoldingAreaPage : RCPPage, IDisposable
    {
        #region constructors
        public MyHoldingAreaPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default2.aspx"; } }

        #endregion properties

        #region elements
        public IWebElement DeleteActivityYesBtn { get { return this.FindElement(Bys.MyHoldingAreaPage.DeleteActivityYesBtn); } }
        public IWebElement EnterACPDActivityBtn { get { return this.FindElement(Bys.MyHoldingAreaPage.EnterACPDActivityBtn); } }
        public IWebElement IncompleteActivitiesTblBodyRow { get { return this.FindElement(Bys.MyHoldingAreaPage.IncompleteActivitiesTblBodyRow); } }
        public IWebElement IncompleteActivitiesTblBody { get { return this.FindElement(Bys.MyHoldingAreaPage.IncompleteActivitiesTblBody); } }
        public IWebElement IncompleteActivitiesTblTHead { get { return this.FindElement(Bys.MyHoldingAreaPage.IncompleteActivitiesTblTHead); } }
        public IWebElement IncompleteActivitiesTbl { get { return this.FindElement(Bys.MyHoldingAreaPage.IncompleteActivitiesTbl); } }
        public IWebElement AwaitingCredValidationTbl { get { return this.FindElement(Bys.MyHoldingAreaPage.AwaitingCredValidationTbl); } }
        public IWebElement AwaitingCredValidationTblTHead { get { return this.FindElement(Bys.MyHoldingAreaPage.AwaitingCredValidationTblTHead); } }
        public IWebElement AwaitingCredValidationTblBody { get { return this.FindElement(Bys.MyHoldingAreaPage.AwaitingCredValidationTblBody); } }
        public IWebElement AwaitingCredValidationTblBodyRow { get { return this.FindElement(Bys.MyHoldingAreaPage.AwaitingCredValidationTblBodyRow); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(480), Criteria.MyHoldingAreaPage.PageReady);
            }
            catch (Exception)
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.MyHoldingAreaPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose DashboardPge", activeRequests.Count, ex); }
        }



        #endregion methods: repeated per page

        #region methods: wrappers

        /// <summary>
        /// Clicks on the Complete Activity button for a user-specified activity in the user-specified table, then waits for the Enter a CPD Activity page 
        /// to appear, then returns that page object
        /// </summary>
        /// <param name="tbl">The table that your activity is in</param>
        /// <param name="tblBodyRow">Any row within the table, in By form. This is just used to wait until any row loads</param>
        /// <param name="activityName">The exact text from the first column of the table for your activity, or your activity name</param>
        /// <returns></returns>
        public EnterCPDActivityPage ClickCompleteActivityBtn(IWebElement tbl, By tblBodyRow, string activityName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, tbl, tblBodyRow, activityName, null, "Complete Activity");

            EnterCPDActivityPage page = new EnterCPDActivityPage(Browser);
            page.WaitForInitialize();
            Browser.SwitchTo().Frame(page.EnterACPDFrame);
            return page;
        }

        /// <summary>
        /// Clicks the X button to delete the activity for a user-specified activity in the user-specified table, clicks Yes on the Delete Activity popup, 
        /// then waits for the popup to disappear
        /// </summary>
        /// <param name="activityName">The exact text from the first column of the table for your activity, or your activity name</param>
        public void DeleteActivity(string activityName)
        {
            IWebElement row = ElemGet.Grid_GetRowByRowName(IncompleteActivitiesTbl, Bys.MyHoldingAreaPage.IncompleteActivitiesTblBodyRow,
                activityName);
            ElemSet.Grid_ClickElementWithoutTextInsideRow(row, "input");
            ClickAndWait(DeleteActivityYesBtn);
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.MyHoldingAreaPage.DeleteActivityYesBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DeleteActivityYesBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    // Add dynamic wait here, however I might not be able to, it looks like the same load icon as on the My CPD Activities
                    // List tab, where I was unable to find a valid wait criteria
                    Thread.Sleep(5000);
                    return null;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }
        #endregion wrappers


    }
}