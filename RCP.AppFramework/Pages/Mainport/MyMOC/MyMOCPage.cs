using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class MyMOCPage : RCPPage, IDisposable
    {
        #region constructors
        public MyMOCPage(IWebDriver driver) : base(driver)
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
        
        public IWebElement DeleteActivityWarningFormYesBtn { get { return this.FindElement(Bys.MyMOCPage.DeleteActivityWarningFormYesBtn); } }
        public IWebElement ViewActivitiesFormXBtn { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormXBtn); } }
        public IWebElement AssessmentTblPerformanceAssRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblPerformanceAssRowViewLnk); } }
        public IWebElement AssessmentTblKnowledgeAssRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblKnowledgeAssRowViewLnk); } }
        public IWebElement SelfLearningTblSysLearnActRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblSysLearnActRowViewLnk); } }
        public IWebElement SelfLearningTblScanActRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblScanActRowViewLnk); } }
        public IWebElement SelfLearningTblPlanLearnActRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblPlanLearnActRowViewLnk); } }
        public IWebElement GroupLearnTblUnaccrActRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblUnaccrActRowViewLnk); } }
        public IWebElement GroupLearnTblAccrActRowViewLnk { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblAccrActRowViewLnk); } }
        public IWebElement OverallCreditsAppliedLbl { get { return this.FindElement(Bys.MyMOCPage.OverallCreditsAppliedLbl); } }
        public IWebElement AssessmentTblPerformanceAssLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblPerformanceAssLastUpLbl); } }
        public IWebElement AssessmentTblPerformanceAssRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblPerformanceAssRowCredsRptLbl); } }
        public IWebElement AssessmentTblKnowledgeAssRowLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblKnowledgeAssRowLastUpLbl); } }
        public IWebElement AssessmentTblKnowledgeAssRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblKnowledgeAssRowCredsRptLbl); } }
        public IWebElement AssessmentTblYouHaveNotMetMinCredsLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblYouHaveNotMetMinCredsLbl); } }
        public IWebElement SelfLearningTblSysLearnActRowLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblSysLearnActRowLastUpLbl); } }
        public IWebElement SelfLearningTblSysLearnActRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblSysLearnActRowCredsRptLbl); } }
        public IWebElement SelfLearningTblScanActRowLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblScanActRowLastUpLbl); } }
        public IWebElement SelfLearningTblScanActRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblScanActRowCredsRptLbl); } }
        public IWebElement SelfLearningTblPlanLearnActRowLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblPlanLearnActRowLastUpLbl); } }
        public IWebElement SelfLearningTblPlanLearnActRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblPlanLearnActRowCredsRptLbl); } }
        public IWebElement SelfLearningTblYouHaveNotMetMinCredsLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblYouHaveNotMetMinCredsLbl); } }
        public IWebElement SelfLearningTblYouHaveMetMinCredsLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblYouHaveMetMinCredsLbl); } }
        public IWebElement SelfLearningTblCreditsAppliedValueLbl { get { return this.FindElement(Bys.MyMOCPage.SelfLearningTblCreditsAppliedValueLbl); } }
        public IWebElement GroupLearnTblUnaccrActRowLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblUnaccrActRowLastUpLbl); } }
        public IWebElement GroupLearnTblUnaccrActRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblUnaccrActRowCredsRptLbl); } }
        public IWebElement GroupLearnTblAccrActRowLastUpLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblAccrActRowLastUpLbl); } }
        public IWebElement GroupLearnTblAccrActRowCredsRptLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblAccrActRowCredsRptLbl); } }
        public IWebElement GroupLearnTblYouHaveNotMetMinCredsLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblYouHaveNotMetMinCredsLbl); } }
        public IWebElement GroupLearnTblYouHaveMetMinCredsLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblYouHaveMetMinCredsLbl); } }
        public IWebElement GroupLearnTblCreditsAppliedValueLbl { get { return this.FindElement(Bys.MyMOCPage.GroupLearnTblCreditsAppliedValueLbl); } }
        public IWebElement AssessmentTblCreditsAppliedValueLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblCreditsAppliedValueLbl); } }
        public IWebElement ViewActivitiesFormCloseBtn { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormCloseBtn); } }
        public IWebElement ViewActivitiesFormActivitiesTbl { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormActivitiesTbl); } }
        public IWebElement ViewActivitiesFormActivitiesTblThead { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormActivitiesTblThead); } }
        public IWebElement ViewActivitiesFormActivitiesTblBody { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBody); } }
        public IWebElement ViewActivitiesFormActivitiesTblBodyRow { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRow); } }
        public IWebElement ViewActivitiesFormFrame { get { return this.FindElement(Bys.MyMOCPage.ViewActivitiesFormFrame); } }
        public IWebElement EnterACPDActivityBtn { get { return this.FindElement(Bys.MyMOCPage.EnterACPDActivityBtn); } }
        public IWebElement AssessmentTblYouHaveMetMinCredsLbl { get { return this.FindElement(Bys.MyMOCPage.AssessmentTblYouHaveMetMinCredsLbl); } }

        
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(480), Criteria.MyMOCPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.MyMOCPage.PageReady);
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
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.MyMOCPage.GroupLearnTblUnaccrActRowViewLnk))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GroupLearnTblUnaccrActRowViewLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    SwitchToViewActivitiesFormFrame();
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyMOCPage.GroupLearnTblAccrActRowViewLnk))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == GroupLearnTblAccrActRowViewLnk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    SwitchToViewActivitiesFormFrame();
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyMOCPage.ViewActivitiesFormCloseBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ViewActivitiesFormCloseBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.SwitchTo().DefaultContent();
                    this.WaitUntil(Criteria.MyMOCPage.EnterACPDActivityBtnEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyMOCPage.ViewActivitiesFormXBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ViewActivitiesFormXBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.SwitchTo().DefaultContent();
                    this.WaitUntil(Criteria.MyMOCPage.EnterACPDActivityBtnEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyMOCPage.DeleteActivityWarningFormYesBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DeleteActivityWarningFormYesBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRowVisible);
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
        /// Switches to the View Activities popup iframe
        /// </summary>
        public void SwitchToViewActivitiesFormFrame()
        {
            Browser.SwitchTo().DefaultContent();
            this.WaitUntil(Criteria.MyMOCPage.ViewActivitiesFormFrameExists);
            Browser.SwitchTo().Frame(ViewActivitiesFormFrame);
            this.WaitUntil(Criteria.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRowVisible);
        }

        /// <summary>
        /// Switches to the Enter a CPD Activity page iframe
        /// </summary>
        /// <returns></returns>
        public EnterCPDActivityPage SwitchToEnterACPDActivitiesFormFrame()
        {
            Browser.SwitchTo().DefaultContent();
            Browser.WaitForElement(Bys.EnterCPDActivityPage.EnterACPDFrame, ElementCriteria.IsVisible);
            EnterCPDActivityPage page = new EnterCPDActivityPage(Browser);
            page.WaitForInitialize();
            Browser.SwitchTo().Frame(page.EnterACPDFrame);

            return page;
        }

        /// <summary>
        /// Clicks the View link for a user-specified activity type
        /// </summary>
        /// <param name="activityTypeViewLnk">The View link for a given activity type. <see cref="GroupLearnTblAccrActRowViewLnk"/> for an example</param>
        public void OpenViewActivitiesForm(IWebElement activityTypeViewLnk)
        {
            ClickAndWait(activityTypeViewLnk);
        }

        /// <summary>
        /// Clicks the View link for a user-specified activity type, waits for the View Activities form to load, clicks on the pencil icon for a 
        /// user-specified activity, then waits for the Enter an Activity form to load
        /// </summary>
        /// <param name="activityTypeViewLnk">The View link for a given activity type. <see cref="GroupLearnTblAccrActRowViewLnk"/> for an example</param>
        /// <param name="activityName">The exact text from the first column of the table for your activity, or your activity name</param>
        public EnterCPDActivityPage OpenEditActivityForm(IWebElement activityTypeViewLnk, string activityName)
        {
            OpenViewActivitiesForm(activityTypeViewLnk);

            IWebElement row = ElemGet.Grid_GetRowByRowName(ViewActivitiesFormActivitiesTbl, Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRow,
                activityName);

            ElemSet.Grid_ClickElementWithoutTextInsideRow(row, "img");

            return SwitchToEnterACPDActivitiesFormFrame();
        }

        #endregion wrappers


    }
}