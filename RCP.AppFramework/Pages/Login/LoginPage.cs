using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class LoginPage : RCPPage, IDisposable
    {
        #region constructors
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }
        public IWebElement UserNameWarningLbl { get { return this.FindElement(Bys.LoginPage.UserNameWarningLbl); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement PasswordWarningLbl { get { return this.FindElement(Bys.LoginPage.PasswordWarningLbl); } }
        public IWebElement RememberMeChk { get { return this.FindElement(Bys.LoginPage.RememberMeChk); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }
        public IWebElement ForgotPasswordLnk { get { return this.FindElement(Bys.LoginPage.ForgotPasswordLnk); } }
        public IWebElement LoginUnsuccessfullWarningLbl { get { return this.FindElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl); } }
        public IWebElement iAcceptBtn { get { return this.FindElement(Bys.LoginPage.iAcceptBtn); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.LoginPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.LoginPage.PageReady);
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

        #region methods: wrappers

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (buttonOrLinkElem.GetAttribute("id") == LoginBtn.GetAttribute("id"))
                {
                    LoginBtn.Click();
                    //new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("Default2"));
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
        /// Logs out of the current user only if a user is currently logged in, enters text in the username and password field, 
        /// clicks the login button, then waits for the URL of the Dashboard page to load
        /// </summary>
        /// <param name="role">Either "learner", "observer", "program admin", etc.</param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="newUser">true or false, depending on if it is a new user or not</param>
        public dynamic LoginAsNewUser(UserUtils.UserRole role, string userName, string password)
        {
            IWebElement acceptBtn = null;

            // Login with a valid user
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName);
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            ClickAndWait(LoginBtn);

            // For some reason, the Selenium click fails to make the RCP page login, it instead stays at the login page. 
            // This happens maybe 5% of the time. So we will add a try catch block here to click the login button
            // again if it fails
            try
            {
                acceptBtn = Browser.WaitForElement(Bys.LoginPage.iAcceptBtn, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible);
            }
            catch
            {
                ClickAndWait(LoginBtn);
                acceptBtn = Browser.WaitForElement(Bys.LoginPage.iAcceptBtn, ElementCriteria.IsVisible);
            }

            Thread.Sleep(0300);
            acceptBtn.Click();

            // Depending on the role of the user that we use, we need to wait differently and return different pages
            switch (role)
            {
                case UserUtils.UserRole.LR:
                    CBDLearnerPage LP = new CBDLearnerPage(Browser);
                    LP.WaitForInitialize();

                    // Adding this if statements here because sometimes the graph doesnt appear after we login.
                    // It only happens about 5 percent of the time, so DEV is not looking into it. So if the 
                    // graph does not appear, then we will navigate to the already loaded page again

                    if (Browser.FindElements(By.Id("EPAChart")).Count == 0)
                    {
                        Navigation.GoToLearnerPage(Browser);
                        LP.WaitUntil(TimeSpan.FromSeconds(30), Criteria.CBDLearnerPage.EPAChartVisibleAndEnabledAndHasText);
                    }

                    return LP;
                case UserUtils.UserRole.OB:
                    CBDObserverPage OP = new CBDObserverPage(Browser);
                    OP.WaitForInitialize();
                    return OP;
                case UserUtils.UserRole.PA:
                    CBDProgAdminPage PA = new CBDProgAdminPage(Browser);
                    PA.WaitForInitialize();
                    return PA;
                case UserUtils.UserRole.TraineePER:
                    PERTraineePage PTP = new PERTraineePage(Browser);
                    PTP.WaitForInitialize();
                    return PTP;
                case UserUtils.UserRole.TraineeDiploma:
                    DiplomaTraineePage DTP = new DiplomaTraineePage(Browser);
                    DTP.WaitForInitialize();
                    return DTP;
                case UserUtils.UserRole.MP:
                    MyDashboardPage DP = new MyDashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
            }

            return null;
        }


        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Dashboard page to load
        /// </summary>
        /// <param name="role">Either "learner", "observer", "program admin", etc.</param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public dynamic LoginAsExistingUser(UserUtils.UserRole role, string userName, string password)
        {
            // Login with a valid user
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName);
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            ClickAndWait(LoginBtn);

            switch (role)
            {
                case UserUtils.UserRole.LR:
                    CBDLearnerPage LP = new CBDLearnerPage(Browser);
                    LP.WaitForInitialize();

                    // Adding this if statements here because sometimes the graph doesnt appear after we login. It only happens about 5 percent of the time,
                    // so DEV is not looking into it. So if the graph does not appear, then we will navigate to the already loaded page again
                    if (Browser.FindElements(By.Id("EPAChart")).Count == 0)
                    {
                        Navigation.GoToLearnerPage(Browser);
                        LP.WaitUntil(TimeSpan.FromSeconds(30), Criteria.CBDLearnerPage.EPAChartVisibleAndEnabledAndHasText);
                    }
                    return LP;

                case UserUtils.UserRole.OB:
                    CBDObserverPage OP = new CBDObserverPage(Browser);
                    OP.WaitForInitialize();
                    return OP;

                case UserUtils.UserRole.PA:
                    CBDProgAdminPage PA = new CBDProgAdminPage(Browser);
                    PA.WaitForInitialize();
                    return PA;

                case UserUtils.UserRole.PD:
                    CBDProgDirectorPage PD = new CBDProgDirectorPage(Browser);
                    PD.WaitForInitialize();
                    return PD;

                case UserUtils.UserRole.PGD:
                    CBDProgDeanPage page = new CBDProgDeanPage(Browser);
                    page.WaitForInitialize();
                    return page;

                case UserUtils.UserRole.CUPER:
                    MyDashboardPage PERCUMyDashboardPage = new MyDashboardPage(Browser);
                    PERCUMyDashboardPage.WaitForInitialize();
                    PERCredentialStaffPage CP = PERCUMyDashboardPage.ClickAndWaitBasePage(PERCUMyDashboardPage.PERAFCTab);
                    return CP;

                case UserUtils.UserRole.CSDiploma:
                    DiplomaClinicalSupervisorPage CSP = new DiplomaClinicalSupervisorPage(Browser);
                    CSP.WaitForInitialize();
                    return CSP;

                case UserUtils.UserRole.CUDiploma:
                    MyDashboardPage DiplomaCUMyDashboardPage = new MyDashboardPage(Browser);
                    DiplomaCUMyDashboardPage.WaitForInitialize();
                    DiplomaCredentialStaffPage DCUP = DiplomaCUMyDashboardPage.ClickAndWaitBasePage(DiplomaCUMyDashboardPage.MyDiplomaTab);
                    return DCUP;

                case UserUtils.UserRole.DDDiploma:
                    DiplomaDirectorPage DD = new DiplomaDirectorPage(Browser);
                    DD.WaitForInitialize();
                    return DD;

                case UserUtils.UserRole.FOMDiploma:
                    DiplomaFacOfMedicinePage FOMP = new DiplomaFacOfMedicinePage(Browser);
                    FOMP.WaitForInitialize();
                    return FOMP;

                case UserUtils.UserRole.MP:
                    MyDashboardPage DP = new MyDashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;

                case UserUtils.UserRole.TraineePER:
                    PERTraineePage PTP = new PERTraineePage(Browser);
                    PTP.WaitForInitialize();
                    return PTP;

                case UserUtils.UserRole.TraineeDiploma:
                    DiplomaTraineePage DTP = new DiplomaTraineePage(Browser);
                    DTP.WaitForInitialize();
                    return DTP;

                case UserUtils.UserRole.REF:
                    PERRefereePage PERRP = new PERRefereePage(Browser);
                    PERRP.WaitForInitialize();
                    return PERRP;

                case UserUtils.UserRole.ASRPER:
                    PERAssessorPage AP = new PERAssessorPage(Browser);
                    AP.WaitForInitialize();
                    return AP;

                case UserUtils.UserRole.ASRDiploma:
                    DiplomaAssessorPage DAP = new DiplomaAssessorPage(Browser);
                    DAP.WaitForInitialize();
                    return DAP;
            }

            return null;
        }

        #endregion wrappers



    }
}