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
    public class PERRefereePage : RCPPage, IDisposable
    {
        #region constructors
        public PERRefereePage(IWebDriver driver) : base(driver)
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

        public IWebElement TraineeSurveyFormSaveAndFinBtn { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormSaveAndFinBtn); } }
        public IWebElement TraineeSurveyFormCloseBtn { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormCloseBtn); } }
        public IWebElement TraineeSurveyFormSaveAndFinLatBtn { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormSaveAndFinLatBtn); } }
        public IWebElement TraineeSurveyFormIAttestChk { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormIAttestChk); } }
        public IWebElement TraineeSurveyFormAreYouFamYesRdo { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormAreYouFamYesRdo); } }
        public IWebElement TraineeSurveyFormAreYouFamNoRdo { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormAreYouFamNoRdo); } }
        public IWebElement TraineeSurveyFormTheApplNamedYesRdo { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormTheApplNamedYesRdo); } }
        public IWebElement TraineeSurveyFormTheApplNamedNoRdo { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormTheApplNamedNoRdo); } }
        public IWebElement PendingSurveysTbl { get { return this.FindElement(Bys.PERRefereePage.PendingSurveysTbl); } }
        public IWebElement PendingSurveysTblFirstRow { get { return this.FindElement(Bys.PERRefereePage.PendingSurveysTblFirstRow); } }
        public IWebElement TraineeSurveyFormProfessTxt { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormProfessTxt); } }
        public IWebElement TraineeSurveyFormPracticeRelTxt { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormPracticeRelTxt); } }
        public IWebElement TraineeSurveyFormSpecialCertTxt { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormSpecialCertTxt); } }
        public IWebElement TraineeSurveyFormYearTxt { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormYearTxt); } }
        public IWebElement TraineeSurveyFormPleaseAddTxt { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormPleaseAddTxt); } }
        public IWebElement TraineeSurveyFormHowLongTxt { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormHowLongTxt); } }
        public IWebElement TraineeSurveyFormFrame { get { return this.FindElement(Bys.PERRefereePage.TraineeSurveyFormFrame); } }

        
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.PERRefereePage.PageReady);
                Browser.SwitchTo().Frame(MainFrame);
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.PERRefereePage.PendingSurveysTblFirstRowVisible);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.PERRefereePage.PageReady);
            Browser.SwitchTo().Frame(MainFrame);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.PERRefereePage.PendingSurveysTblFirstRowVisible);
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
        /// Opens the questionnaire survey form for a user-specified trainee, fills out the form, and then either approves
        /// or rejects the trainee depending on your passed parameter
        /// </summary>
        /// <param name="traineeFullName">The first and last name of the trainee</param>
        /// <param name="approveTrainee">true or false depending on if you want to approve or reject the trainee</param>
        public void CompleteQuestionnaire(string traineeFullName, bool approveTrainee)
        {
            OpenTraineeSurveyForm(traineeFullName);

            TraineeSurveyFormProfessTxt.SendKeys(DataUtils.GetRandomString(12));
            TraineeSurveyFormPracticeRelTxt.SendKeys(DataUtils.GetRandomString(12));
            TraineeSurveyFormSpecialCertTxt.SendKeys(DataUtils.GetRandomString(12));
            TraineeSurveyFormYearTxt.SendKeys("2020");
            TraineeSurveyFormHowLongTxt.SendKeys("2");
            ClickAndWait(TraineeSurveyFormAreYouFamYesRdo);

            if (approveTrainee)
            {
                TraineeSurveyFormTheApplNamedYesRdo.Click();
            }
            else
            {
                TraineeSurveyFormTheApplNamedNoRdo.Click();
            }

            TraineeSurveyFormPleaseAddTxt.SendKeys(DataUtils.GetRandomString(12));
            TraineeSurveyFormIAttestChk.Click();

            ClickAndWait(TraineeSurveyFormSaveAndFinBtn);
        }

        /// <summary>
        /// Clicks the Access Questionnaire button for a user-specified trainee in the Pending Surveys table, then waits for the
        /// questionnaire to open
        /// </summary>
        /// <param name="traineeFullName"></param>
        private void OpenTraineeSurveyForm(string traineeFullName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, PendingSurveysTbl, Bys.PERRefereePage.PendingSurveysTblFirstRow, traineeFullName, null,
                "Access Questionnaire  ");

            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.PERRefereePage.TraineeSurveyFormFrameVisible);
            Browser.SwitchTo().Frame(TraineeSurveyFormFrame);

            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.PERRefereePage.TraineeSurveyFormProfessTxtVisible);
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.PERRefereePage.TraineeSurveyFormSaveAndFinBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TraineeSurveyFormSaveAndFinBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.SwitchTo().DefaultContent();
                    Browser.SwitchTo().Frame(MainFrame);
                    this.WaitUntilAny(Criteria.PERRefereePage.PendingSurveysTblVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.PERRefereePage.TraineeSurveyFormAreYouFamYesRdo))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TraineeSurveyFormAreYouFamYesRdo.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Thread.Sleep(0300);
                    this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.PERRefereePage.TraineeSurveyFormLoadingIconNotVisible);
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