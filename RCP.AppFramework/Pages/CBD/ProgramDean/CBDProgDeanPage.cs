using Browser.Core.Framework;
using Browser.Core.Framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;
namespace RCP.AppFramework
{
    public class CBDProgDeanPage : RCPPage, IDisposable
    {
        #region constructors
        public CBDProgDeanPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that we start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "/cbd#/programadministrator"; } }

        #endregion properties

        #region elements   
        public IWebElement CBDTab { get { return this.FindElement(Bys.RCPPage.CBDTab); } }
        public IWebElement LearnersTbl { get { return this.FindElement(Bys.CBDProgDeanPage.LearnersTbl); } }
        public IWebElement LearnersTblBody { get { return this.FindElement(Bys.CBDProgDeanPage.LearnersTblBody); } }
        public IWebElement LearnersTblBodyRow { get { return this.FindElement(Bys.CBDProgDeanPage.LearnersTblBodyRow); } }
        public IWebElement ApprovalsTbl { get { return this.FindElement(Bys.CBDProgDeanPage.ApprovalsTbl); } }
        public IWebElement ApprovalsTblBody { get { return this.FindElement(Bys.CBDProgDeanPage.ApprovalsTblBody); } }
        public IWebElement ApprovalsTblBodyRow { get { return this.FindElement(Bys.CBDProgDeanPage.ApprovalsTblBodyRow); } }
        public IWebElement AwarenessTbl { get { return this.FindElement(Bys.CBDProgDeanPage.AwarenessTbl); } }
        public IWebElement AwarenessTblBody { get { return this.FindElement(Bys.CBDProgDeanPage.AwarenessTblBody); } }
        public IWebElement AwarenessTblBodyRow { get { return this.FindElement(Bys.CBDProgDeanPage.AwarenessTblBodyRow); } }
        public IWebElement AddSupportingDocumentationFormBrowseBtn { get { return this.FindElement(Bys.CBDProgDeanPage.AddSupportingDocumentationFormBrowseBtn); } }
        public IWebElement AddRemoveFlagFormRemoveFlagBtn { get { return this.FindElement(Bys.CBDProgDeanPage.AddRemoveFlagFormRemoveFlagBtn); } }
        public IWebElement AddRemoveFlagFormSaveFlagBtn { get { return this.FindElement(Bys.CBDProgDeanPage.AddRemoveFlagFormSaveFlagBtn); } }
        public IWebElement AddRemoveFlagFormReasonTxt { get { return this.FindElement(Bys.CBDProgDeanPage.AddRemoveFlagFormReasonTxt); } }
        public IWebElement AddNotesFormSubjectTxt { get { return this.FindElement(Bys.CBDProgDeanPage.AddNotesFormSubjectTxt); } }
        public IWebElement AddNotesFormNotesTxt { get { return this.FindElement(Bys.CBDProgDeanPage.AddNotesFormNotesTxt); } }
        public IWebElement AddNotesFormSubmitBtn { get { return this.FindElement(Bys.CBDProgDeanPage.AddNotesFormSubmitBtn); } }
        public SelectElement ProgramSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDProgDeanPage.ProgramSelElem)); } }
        public IWebElement CBDHomeTab { get { return this.FindElement(Bys.CBDProgDeanPage.CBDHomeTab); } }
        public IWebElement LearnersTab { get { return this.FindElement(Bys.CBDProgDeanPage.LearnersTab); } }
        public IWebElement AddSupportingDocumentationFormFilelocationTxt { get { return this.FindElement(Bys.CBDProgDeanPage.AddSupportingDocumentationFormFilelocationTxt); } }
        public IWebElement AddSupportingDocumentationFormSubmitBtn { get { return this.FindElement(Bys.CBDProgDeanPage.AddSupportingDocumentationFormSubmitBtn); } }
        public IWebElement PendingAwareAndAppTab { get { return this.FindElement(Bys.CBDProgDeanPage.PendingAwareAndAppTab); } }
        public IWebElement ArchivedAwareAndAppTab { get { return this.FindElement(Bys.CBDProgDeanPage.ArchivedAwareAndAppTab); } }

        #endregion elements

        #region methods: repeated per page
        public override void WaitForInitialize()
        {
            // Sometimes the full page takes forever to load (and in some cases I dont think it will ever load), so we will put a try/catch here.
            // Try for waiting for the full page load. If the full page doesnt load in time, then refresh the page and try again
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDProgDeanPage.PageReady);
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
        /// Chooses a program from the Program dropdown, clicks on the Learner tab, chooses a user specified Learner, 
        /// clicks the Action button, selects Add Supporting Documentation, fills in the File Name text box, 
        /// then clicks Submit 
        /// </summary>
        /// <param name="program">The text from one of the items in the Program dropdown</param>
        /// <param name="learnerFullName">First and last name of the learner</param>
        /// <param name="fileLocation">File location</param>
        public void AddSupportDocumentation(string program, string learnerFullName, string fileLocation)
        {
            SelectProgram(program);

            ClickAndWait(LearnersTab);

            ElemSet.Grid_ClickMenuItemInsideButton(Browser, LearnersTbl, Bys.CBDProgDeanPage.LearnersTblBodyRow, learnerFullName, null, "Actions", "Add Supporting Documentation");
            Browser.WaitForElement(Bys.CBDProgDeanPage.AddSupportingDocumentationFormFilelocationTxt, ElementCriteria.IsVisible);
            this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);

            AddSupportingDocumentationFormFilelocationTxt.SendKeys(fileLocation);

            // FileUtils.Upload(AddSupportingDocumentationFormBrowseBtn, @"C:\SeleniumAutoIt\FileUpload.exe", browserName, "C:\\SeleniumAutoIt\\test.txt");

            ClickAndWait(AddSupportingDocumentationFormSubmitBtn);
        }

        /// <summary>
        /// Chooses a program from the Program dropdown, clicks on the Learner tab, chooses a user specified Learner, 
        /// clicks the Action button, selects Add Notes, fills in the Subject and Notes text box with
        /// a random string, chooses the user-specified radio button  then clicks Submit 
        /// </summary>
        /// <param name="program">The text from one of the items in the Program dropdown</param>
        /// <param name="learnerFullName">First and last name of the learner</param>
        /// <param name="sharingOption">The exact text of either radio button on the Add Notes form</param>
        public void AddNotes(string program, string learnerFullName)
        {
            SelectProgram(program);

            ClickAndWait(LearnersTab);

            ElemSet.Grid_ClickMenuItemInsideButton(Browser, LearnersTbl, Bys.CBDProgDeanPage.LearnersTblBodyRow, learnerFullName, null, "Actions", "Add Notes");
            Browser.WaitForElement(Bys.CBDProgDeanPage.AddNotesFormSubjectTxt, ElementCriteria.IsVisible);
            this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);

            AddNotesFormNotesTxt.SendKeys(DataUtils.GetRandomSentence(12));
            ElemSet.RdoBtn_ClickRandom(Browser, "Share This Note");
            AddNotesFormSubjectTxt.SendKeys(DataUtils.GetRandomSentence(12));

            ClickAndWait(AddNotesFormSubmitBtn);
        }

        /// <summary>
        /// Chooses a program from the Program dropdown, clicks on the Learner tab, chooses a user specified Learner, 
        /// clicks on the Actions button, clicks on Add/Remove Flag, fill in 
        /// all of the fields with random data and clicks Save Flag
        /// </summary>
        /// <param name="program">The text from one of the items in the Program dropdown</param>
        /// <param name="learnerFullName">First and last name of the learner</param>
        public void AddFlag(string program, string learnerFullName)
        {
            SelectProgram(program);

            ClickAndWait(LearnersTab);

            ElemSet.Grid_ClickMenuItemInsideButton(Browser, LearnersTbl, Bys.CBDProgDeanPage.LearnersTblBodyRow, learnerFullName, null, "Actions", "Add/Remove Flag");
            Browser.WaitForElement(Bys.CBDProgDeanPage.AddRemoveFlagFormReasonTxt, ElementCriteria.IsVisible);
            this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);

            AddRemoveFlagFormReasonTxt.SendKeys(DataUtils.GetRandomSentence(10));
            ClickAndWait(AddRemoveFlagFormSaveFlagBtn);
        }

        /// <summary>
        /// Chooses a program from the Program dropdown, clicks on the Learner tab, chooses a user specified Learner, 
        /// clicks on the Actions button, clicks on Add/Remove Flag, fill in 
        /// all of the fields with random data and clicks Remove Flag if a flag is present
        /// </summary>
        /// <param name="program">The text from one of the items in the Program dropdown</param>
        /// <param name="learnerFullName">First and last name of the learner</param>
        public void RemoveFlag(string program, string learnerFullName)
        {
            SelectProgram(program);

            ClickAndWait(LearnersTab);

            ElemSet.Grid_ClickMenuItemInsideButton(Browser, LearnersTbl, Bys.CBDProgDeanPage.LearnersTblBodyRow, learnerFullName, null, "Actions", "Add/Remove Flag");
            Browser.WaitForElement(Bys.CBDProgDeanPage.AddRemoveFlagFormReasonTxt, ElementCriteria.IsVisible);
            this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);

            // If the flag is there, remove it
            if (AddRemoveFlagFormRemoveFlagBtn.Enabled)
            {
                ClickAndWait(AddRemoveFlagFormRemoveFlagBtn);
            }
        }

        /// <summary>
        /// Selects a program in the program dropdown, then waits until the page loads
        /// </summary>
        /// <param name="programToSelect">Exact text from the Option element insdie the Program Select Element</param>
        public void SelectProgram(string programToSelect)
        {
            ProgramSelElem.SelectByText(programToSelect);
            this.WaitUntil(TimeSpan.FromSeconds(5), Criteria.CBDProgDeanPage.LearnersTabTextNotEqualToZero);
            this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);
            // Sometimes the load icon shows twice (disappears then reappears after selecting an item in the Program dropdown. 
            // Due to this, we have to add a sleep. I couldnt think of a way to dynamically wait
            Thread.Sleep(0500);
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.CBDProgDeanPage.LearnersTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LearnersTab.GetAttribute("outerHTML"))
                {
                    LearnersTab.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LearnersTblBodyRowVisibleAndEnabled);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LoadElementDoneLoading);
                    return;
                }
            }

            if (Browser.Exists(Bys.RCPPage.CBDTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == Browser.FindElement(Bys.RCPPage.CBDTab).GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.CBDProgDeanPage.PageReady);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDeanPage.AddRemoveFlagFormRemoveFlagBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddRemoveFlagFormRemoveFlagBtn.GetAttribute("outerHTML"))
                {
                    AddRemoveFlagFormRemoveFlagBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LearnersTblBodyRowVisibleAndEnabled);
                    this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDeanPage.AddRemoveFlagFormSaveFlagBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddRemoveFlagFormSaveFlagBtn.GetAttribute("outerHTML"))
                {
                    AddRemoveFlagFormSaveFlagBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LearnersTblBodyRowVisibleAndEnabled);
                    this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);
                    return;
                }
            }   

            if (Browser.Exists(Bys.CBDProgDeanPage.AddNotesFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddNotesFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    AddNotesFormSubmitBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LearnersTblBodyRowVisibleAndEnabled);
                    this.WaitUntil(Criteria.CBDProgDeanPage.LoadElementDoneLoading);
                    return;
                }
            } 

            if (Browser.Exists(Bys.RCPPage.LogoutLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LogoutLnk.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, LogoutLnk);
                    LogoutLnk.Click();
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("login"));
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDProgDeanPage.AddSupportingDocumentationFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddSupportingDocumentationFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    AddSupportingDocumentationFormSubmitBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LearnersTblBodyRowVisibleAndEnabled);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDProgDeanPage.LoadElementDoneLoading);
                    return;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }
        }

        #endregion methods: wrappers
    }

}
