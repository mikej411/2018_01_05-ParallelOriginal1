//using Browser.Core.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Generic;
//using LOG4NET = log4net.ILog;

//namespace RCP.AppFramework
//{
//    public class MyCPDPlanningPage : RCPPage, IDisposable
//    {
//        #region constructors
//        public MyCPDPlanningPage(IWebDriver driver) : base(driver)
//        {
//        }

//        #endregion constructors

//        #region properties

//        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//        // Keep track of the requests that I start so I can clean them up at the end.
//        private List<string> activeRequests = new List<string>();

//        public override string PageUrl { get { return "Default2.aspx"; } }

//        #endregion properties

//        #region elements

//        #endregion elements

//        #region methods: repeated per page

//        public override void WaitForInitialize()
//        {
//            try
//            {
//                this.WaitUntil(TimeSpan.FromSeconds(480), Criteria.MyCPDPlanningPage.PageReady);
//            }
//            catch (Exception)
//            {
//                RefreshPage();
//            }

//        }

//        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
//        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
//        /// randomly refresh the page
//        /// </summary>
//        public void RefreshPage()
//        {
//            Browser.Navigate().Refresh();
//            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.MyCPDPlanningPage.PageReady);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//        }

//        protected virtual void Dispose(bool isDisposing)
//        {
//            try { activeRequests.Clear(); }
//            catch (Exception ex) { _log.ErrorFormat("Failed to dispose DashboardPge", activeRequests.Count, ex); }
//        }

//        #endregion methods: repeated per page

//        #region methods: wrappers

    
//        #endregion wrappers


//    }
//}