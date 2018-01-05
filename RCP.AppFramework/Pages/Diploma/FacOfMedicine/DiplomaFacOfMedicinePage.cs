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
    public class DiplomaFacOfMedicinePage : RCPPage, IDisposable
    {
        #region constructors
        public DiplomaFacOfMedicinePage(IWebDriver driver) : base(driver)
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

        public IWebElement MarkSelectedPortfoliosAsAchievedBtn { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.MarkSelectedPortfoliosAsAchievedBtn); } }
        public IWebElement MarkSelPortAchFormSubmitBtn { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.MarkSelPortAchFormSubmitBtn); } }
        public IWebElement MarkSelPortAchFormIAttestChk { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.MarkSelPortAchFormIAttestChk); } }
        public IWebElement PortfoliosUnderReviewTbl { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.PortfoliosUnderReviewTbl); } }
        public IWebElement PortfoliosUnderReviewTblBodyRow { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.PortfoliosUnderReviewTblBodyRow); } }
        public IWebElement PortfoliosUnderReviewTab { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.PortfoliosUnderReviewTab); } }
        public IWebElement ResubmittedMilestonesTab { get { return this.FindElement(Bys.DiplomaFacOfMedicinePage.ResubmittedMilestonesTab); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaFacOfMedicinePage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaFacOfMedicinePage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
                this.WaitUntilAny(TimeSpan.FromSeconds(60), Criteria.DiplomaFacOfMedicinePage.UnderReviewTblBodyRowCheckBoxVisible);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DiplomaFacOfMedicinePage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DiplomaFacOfMedicinePage.MyProgramSnapshotTblFirstRowPrgLnkVisible);
            this.WaitUntilAny(TimeSpan.FromSeconds(60), Criteria.DiplomaFacOfMedicinePage.UnderReviewTblBodyRowCheckBoxVisible);
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
            if (Browser.Exists(Bys.DiplomaFacOfMedicinePage.MarkSelectedPortfoliosAsAchievedBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkSelectedPortfoliosAsAchievedBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaFacOfMedicinePage.MarkSelPortAchFormSubmitBtnVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.DiplomaFacOfMedicinePage.MarkSelPortAchFormSubmitBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MarkSelPortAchFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.DiplomaFacOfMedicinePage.MarkSelPortAchFormSubmitBtnNotVisible);
                    return;
                }
            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                "or if the button is already added, then the page you were on did not contain the button.");
        }

        public void MarkPortfolioAchieved(string traineeFullName)
        {
            IWebElement row = ElemGet.Grid_GetRowByRowName(PortfoliosUnderReviewTbl, Bys.DiplomaFacOfMedicinePage.PortfoliosUnderReviewTblBodyRow, traineeFullName);

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