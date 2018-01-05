using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;
using System.Threading;

namespace RCP.AppFramework
{
    public class MyCPDActivitiesListPage : RCPPage, IDisposable
    {
        #region constructors
        public MyCPDActivitiesListPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "0/CPDActivities.aspx"; } }

        #endregion properties

        #region elements

        // Main page
        public IWebElement EnterACPDActivityBtn { get { return this.FindElement(Bys.MyCPDActivitiesListPage.EnterACPDActivityBtn); } }
        public IWebElement ActivityTblBody { get { return this.FindElement(Bys.MyCPDActivitiesListPage.ActivityTblBody); } }
        // Delete Activity Form
        public IWebElement DeleteActivityForm { get { return this.FindElement(Bys.MyCPDActivitiesListPage.DeleteActivityForm); } }
        public IWebElement DeleteActivityFormOkBtn { get { return this.FindElement(Bys.MyCPDActivitiesListPage.DeleteActivityFormOkBtn); } }
        public IWebElement DeleteActivityFormCancelBtn { get { return this.FindElement(Bys.MyCPDActivitiesListPage.DeleteActivityFormCancelBtn); } }




        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.MyCPDActivitiesListPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.MyCPDActivitiesListPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose HomePage", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: wrappers

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.MyCPDActivitiesListPage.EnterACPDActivityBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EnterACPDActivityBtn.GetAttribute("outerHTML"))
                {
                    EnterACPDActivityBtn.Click();
                    Browser.WaitForElement(Bys.EnterCPDActivityPage.EnterACPDFrame, ElementCriteria.IsVisible);
                    EnterCPDActivityPage EAP = new EnterCPDActivityPage(Browser);
                    EAP.WaitForInitialize();
                    Browser.SwitchTo().Frame(EAP.EnterACPDFrame);
                    return EAP;
                }
            }

            if (Browser.Exists(Bys.MyCPDActivitiesListPage.DeleteActivityFormOkBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DeleteActivityFormOkBtn.GetAttribute("outerHTML"))
                {
                    DeleteActivityFormOkBtn.Click();
                    // Mike J 12/21/2017: I tried everything to implement a dynamic wait here, but couldnt find a way to do it. I tried to wait until
                    // various different loading elements disappear, but none of them worked. I even element inspected the exact circle load icon that
                    // I visually see when we click the delete button, but that didnt work, as its not within the body of the HTML. We might have to use
                    // a static wait here. Dont waste time trying to implement a dynamic wait later on, as I dont think its possible, or if it is, it
                    // is very hidden
                    Thread.Sleep(10000);
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

        /// <summary>
        /// Click the X button on the activity inside the grid, then clicks Ok on the popup to delete the activity
        /// </summary>
        /// <param name="activityName">The activity name. Specifically the exact text from the first column of the grid for the activity</param>
        public void DeleteActivityFromGrid(string activityName)
        {
            IWebElement row = ElemGet.Grid_GetRowByRowName(ActivityTblBody, Bys.MyCPDActivitiesListPage.ActivityTblBody, activityName);

            ElemSet.Grid_ClickElementWithoutTextInsideRow(row, "input");
            Browser.WaitForElement(Bys.MyCPDActivitiesListPage.DeleteActivityFormOkBtn, ElementCriteria.IsVisible);

            ClickAndWait(DeleteActivityFormOkBtn);
        }

        #endregion wrappers


    }
}