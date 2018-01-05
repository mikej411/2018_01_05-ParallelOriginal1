using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class MyDashboardPage : RCPPage, IDisposable
    {
        #region constructors
        public MyDashboardPage(IWebDriver driver) : base(driver)
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
        public IWebElement MOCSectionReqsGraphAssessmentSquare { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphAssessmentSquare); } }
        public IWebElement MOCSectionReqsGraphSelfLearningSquare { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphSelfLearningSquare); } }
        public IWebElement MOCSectionReqsGraphGroupLearningSquare { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphGroupLearningSquare); } }
        public IWebElement MOCSectionReqsGraphAssessmentTickBox { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphAssessmentTickBox); } }
        public IWebElement MOCSectionReqsGraphSelfLearningTickBox { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphSelfLearningTickBox); } }
        public IWebElement MOCSectionReqsGraphGroupLearningTickBox { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphGroupLearningTickBox); } }
        public IWebElement MOCSectionReqsGraphAssessmentCreditLbl { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphAssessmentCreditLbl); } }
        public IWebElement MOCSectionReqsGraphSelfLearningCreditLbl { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphSelfLearningCreditLbl); } }
        public IWebElement MOCSectionReqsGraphGroupLearningCreditLbl { get { return this.FindElement(Bys.MyDashboardPage.MOCSectionReqsGraphGroupLearningCreditLbl); } }
        public IWebElement ViewMoreBtn { get { return this.FindElement(Bys.MyDashboardPage.ViewMoreBtn); } }
        public IWebElement MyHoldingAreaTbl { get { return this.FindElement(Bys.MyDashboardPage.MyHoldingAreaTbl); } }
        public IWebElement MyHoldingAreaTblBody { get { return this.FindElement(Bys.MyDashboardPage.MyHoldingAreaTblBody); } }
        public IWebElement CreateAGoalFrame { get { return this.FindElement(Bys.MyDashboardPage.CreateAGoalFrame); } }
        public IWebElement GoalsTbl { get { return this.FindElement(Bys.MyDashboardPage.GoalsTbl); } }
        public IWebElement AddAGoalBtn { get { return this.FindElement(Bys.MyDashboardPage.AddAGoalBtn); } }
        public IWebElement CreateAGoalFormDateTxt { get { return this.FindElement(Bys.MyDashboardPage.CreateAGoalFormDateTxt); } }
        public IWebElement CreateAGoalFormHowWillYouTxt { get { return this.FindElement(Bys.MyDashboardPage.CreateAGoalFormHowWillYouTxt); } }
        public IWebElement CreateAGoalFormWhatIsThisGoalTxt { get { return this.FindElement(Bys.MyDashboardPage.CreateAGoalFormWhatIsThisGoalTxt); } }
        public IWebElement CreateAGoalFormCloseBtn { get { return this.FindElement(Bys.MyDashboardPage.CreateAGoalFormCloseBtn); } }
        public IWebElement CreateAGoalFormNextBtnTxt { get { return this.FindElement(Bys.MyDashboardPage.CreateAGoalFormNextBtnTxt); } }
        public IWebElement EnterACPDActivityBtn { get { return this.FindElement(Bys.MyDashboardPage.EnterACPDActivityBtn); } }
        public IWebElement TotalCreditsSubmittedValueLbl { get { return this.FindElement(Bys.MyDashboardPage.TotalCreditsSubmittedValueLbl); } }
        public IWebElement TotalCreditsAppliedValueLbl { get { return this.FindElement(Bys.MyDashboardPage.TotalCreditsAppliedValueLbl); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(480), Criteria.MyDashboardPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.MyDashboardPage.PageReady);
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
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.MyDashboardPage.EnterACPDActivityBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EnterACPDActivityBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.EnterCPDActivityPage.EnterACPDFrame, ElementCriteria.IsVisible);
                    EnterCPDActivityPage EAP = new EnterCPDActivityPage(Browser);
                    EAP.WaitForInitialize();
                    Browser.SwitchTo().Frame(EAP.EnterACPDFrame);
                    return EAP;
                }
            }

            if (Browser.Exists(Bys.MyDashboardPage.AddAGoalBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddAGoalBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    SwitchToFrame(Bys.MyDashboardPage.CreateAGoalFrame, CreateAGoalFrame, Bys.MyDashboardPage.CreateAGoalFormNextBtnTxt);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyDashboardPage.CreateAGoalFormNextBtnTxt))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreateAGoalFormNextBtnTxt.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.MyDashboardPage.CreateAGoalFormCloseBtnVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyDashboardPage.CreateAGoalFormCloseBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CreateAGoalFormCloseBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.MyDashboardPage.TotalCreditsAppliedValueLblVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.MyDashboardPage.ViewMoreBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ViewMoreBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    MyHoldingAreaPage page = new MyHoldingAreaPage(Browser);
                    page.WaitForInitialize();

                    return page;
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
        /// Clicks the Add a Goal button, fills in the information, clicks Next, then clicks Close and waits for the window to close
        /// </summary>
        /// <returns></returns>
        public string AddAGoal()
        {
            ClickAndWait(AddAGoalBtn);

            CreateAGoalFormWhatIsThisGoalTxt.SendKeys(DataUtils.GetRandomString(5));
            CreateAGoalFormHowWillYouTxt.SendKeys(DataUtils.GetRandomString(5));
            CreateAGoalFormDateTxt.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
            CreateAGoalFormDateTxt.SendKeys(Keys.Tab);

            ClickAndWait(CreateAGoalFormNextBtnTxt);

            string goalName = CreateAGoalFormWhatIsThisGoalTxt.GetAttribute("innerText");

            ClickAndWait(CreateAGoalFormCloseBtn);

            return goalName;

        }

 

        #endregion wrappers


    }
}