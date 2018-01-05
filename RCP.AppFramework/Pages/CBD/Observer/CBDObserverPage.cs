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
    public class CBDObserverPage : RCPPage, IDisposable
    {
        #region constructors
        public CBDObserverPage(IWebDriver driver) : base(driver)
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
     
        public IWebElement CBDTab { get { return this.FindElement(Bys.RCPPage.CBDTab); } }
        public IWebElement AddObsFormObsTblFacultyColHdr { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormObsTblFacultyColHdr); } }
        public IWebElement UserNameLbl { get { return this.FindElement(Bys.CBDObserverPage.UserNameLbl); } }
        public IWebElement AddObservationBtn { get { return this.FindElement(Bys.CBDObserverPage.AddObservationBtn); } }
        public IWebElement UploadEvidenceBtn { get { return this.FindElement(Bys.CBDObserverPage.UploadEvidenceBtn); } }
        public IWebElement RoleLnk { get { return this.FindElement(Bys.CBDObserverPage.RoleLnk); } }
        public IWebElement PendingObsTab { get { return this.FindElement(Bys.CBDObserverPage.PendingObsTab); } }
        public IWebElement ArchivedObsTab { get { return this.FindElement(Bys.CBDObserverPage.ArchivedObsTab); } }
        public IWebElement AccDecAssgnMntFormCancelBtn { get { return this.FindElement(Bys.CBDObserverPage.AccDecAssgnMntFormCancelBtn); } }
        public IWebElement AccDecAssgnMntFormDeclineBtn { get { return this.FindElement(Bys.CBDObserverPage.AccDecAssgnMntFormDeclineBtn); } }
        public IWebElement AccDecAssgnMntFormAcceptBtn { get { return this.FindElement(Bys.CBDObserverPage.AccDecAssgnMntFormAcceptBtn); } }
        public IWebElement AccDecAssgnMntFormCommentsTxt { get { return this.FindElement(Bys.CBDObserverPage.AccDecAssgnMntFormCommentsTxt); } }
        public IWebElement CompleteAssessFormFeedbackTxt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormFeedbackTxt); } }
        public IWebElement CompleteAssessFormSubmitBtn { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormSubmitBtn); } }
        public IWebElement ConfirmFormCompleteAssessYesBtn { get { return this.FindElement(Bys.CBDObserverPage.ConfirmFormCompleteAssessYesBtn); } }
        public IWebElement ConfirmFormDeclineAssessYesBtn { get { return this.FindElement(Bys.CBDObserverPage.ConfirmFormDeclineAssessYesBtn); } }
        public IWebElement CompleteAssessFormDateControlTopMiddleBtn { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormDateControlTopMiddleBtn); } }
        public IWebElement CompleteAssessFormDateControlExpandBtn { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormDateControlExpandBtn); } }
        public IWebElement CompleteAssessFormDateTxt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormDateTxt); } }
        public IWebElement AddObsFormSearchBtn { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormSearchBtn); } }
        public IWebElement AddObsFormCancelBtn { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormCancelBtn); } }
        public IWebElement AddObsFormBackBtn { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormBackBtn); } }
        public IWebElement AddObsFormNextBtn { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormNextBtn); } }
        public IWebElement AddObsFormLearnerNameTxt { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormLearnerNameTxt); } }
        public SelectElement AddObsFormLearnerFacSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.AddObsFormLearnerFacSelElem)); } }
        public SelectElement AddObsFormLearnProgSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.AddObsFormLearnProgSelElem)); } }
        public SelectElement AddObsFormStageSelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.AddObsFormStageSelElem)); } }
        public IWebElement AddObsFormSubmitBtn { get { return this.FindElement(Bys.CBDObserverPage.AddObsFormSubmitBtn); } }
        public IWebElement ConfirmFormAddObsYesBtn { get { return this.FindElement(Bys.CBDObserverPage.ConfirmFormAddObsYesBtn); } }
        public SelectElement CompleteAssessFormGeneric1SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric1SelElem)); } }
        public SelectElement CompleteAssessFormGeneric2SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric2SelElem)); } }
        public SelectElement CompleteAssessFormGeneric3SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric3SelElem)); } }
        public SelectElement CompleteAssessFormGeneric4SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric4SelElem)); } }
        public SelectElement CompleteAssessFormGeneric5SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric5SelElem)); } }
        public SelectElement CompleteAssessFormGeneric6SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric6SelElem)); } }
        public SelectElement CompleteAssessFormGeneric7SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric7SelElem)); } }
        public SelectElement CompleteAssessFormGeneric8SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric8SelElem)); } }
        public SelectElement CompleteAssessFormGeneric9SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric9SelElem)); } }
        public SelectElement CompleteAssessFormGeneric10SelElem { get { return new SelectElement(this.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormGeneric10SelElem)); } }
        public IWebElement CompleteAssessFormGeneric1Txt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormGeneric1Txt); } }
        public IWebElement CompleteAssessFormGeneric2Txt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormGeneric2Txt); } }
        public IWebElement CompleteAssessFormGeneric3Txt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormGeneric3Txt); } }
        public IWebElement CompleteAssessFormGeneric4Txt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormGeneric4Txt); } }
        public IWebElement CompleteAssessFormGeneric5Txt { get { return this.FindElement(Bys.CBDObserverPage.CompleteAssessFormGeneric5Txt); } }
        public IWebElement PendingAcceptanceTblHdr { get { return this.FindElement(Bys.CBDObserverPage.PendingAcceptanceTblHdr); } }
        public IWebElement ConfirmFormRemoveAssessOkBtn { get { return this.FindElement(Bys.CBDObserverPage.ConfirmFormRemoveAssessOkBtn); } }
        public IWebElement PendingAcceptanceTbl { get { return this.FindElement(Bys.CBDObserverPage.PendingAcceptanceTbl); } }
        public IWebElement PendingAcceptanceTblRowBody { get { return this.FindElement(Bys.CBDObserverPage.PendingAcceptanceTblRowBody); } }
        public IWebElement AcceptedTblHdr { get { return this.FindElement(Bys.CBDObserverPage.AcceptedTblHdr); } }
        public IWebElement AcceptedTbl { get { return this.FindElement(Bys.CBDObserverPage.AcceptedTbl); } }
        public IWebElement AcceptedTblRowBody { get { return this.FindElement(Bys.CBDObserverPage.AcceptedTblRowBody); } }
        public IWebElement ExpiredDeclinedTblHdr { get { return this.FindElement(Bys.CBDObserverPage.ExpiredDeclinedTblHdr); } }
        public IWebElement ExpiredDeclinedTbl { get { return this.FindElement(Bys.CBDObserverPage.ExpiredDeclinedTbl); } }
        public IWebElement ExpiredDeclinedTblRowBody { get { return this.FindElement(Bys.CBDObserverPage.ExpiredDeclinedTblRowBody); } }
        public IWebElement ArchivedObservationsTbl { get { return this.FindElement(Bys.CBDObserverPage.ArchivedObservationsTbl); } }
        public IWebElement ArchivedObservationsTblRowBody { get { return this.FindElement(Bys.CBDObserverPage.ArchivedObservationsTblRowBody); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {          
            // Sometimes the full page takes forever to load (and in some cases I dont think it will ever load), so we will put a try/catch here.
            // Try for waiting for the full page load. If the full page doesnt load in time, then refresh the page and try again
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDObserverPage.PageReady);
            }
            catch
            {
                RefreshPage();
            }
        }

        /// <summary>
        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDObserverPage.PageReady);
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
        /// Switches to any tab within the main page (this does not include popups)
        /// </summary>
        /// <param name="tabElem">The tab element</param>
        public void SwitchToTab(IWebElement tabElem)
        {
            if (Browser.Exists(Bys.CBDObserverPage.PendingObsTab))
            {
                if (tabElem.GetAttribute("outerHTML") == PendingObsTab.GetAttribute("outerHTML"))
                {
                    PendingObsTab.Click();
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.ArchivedObsTab))
            {
                if (tabElem.GetAttribute("outerHTML") == ArchivedObsTab.GetAttribute("outerHTML"))
                {
                    ArchivedObsTab.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDObserverPage.LoadElementClassAttributeSetToHide);
                    this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDObserverPage.LoadElementDisappeared);
                    this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDObserverPage.ArchivedObservationsTblVisibleAndEnabled);
                    return;
                }
            }

            else
            {
                throw new Exception("No tab was found with your passed parameter");
            }


        }

        /// <summary>
        /// Expands or collapses an entire table, or row within a table. 
        /// </summary>
        /// <param name="tblElem">The table element that contains the expansion/collapse icon</param>
        /// <param name="groupdItemToExpandOrCollapse">The exact text of the row name with the the expansion/collapse icon. If the element you want to expand is not a row, but is a table, you can leave this blank</param>
        /// <param name="expandOrCollapse">Either "expand" or "collapse"</param>
        public void ExpandTable(IWebElement tblElem, string groupdItemToExpandOrCollapse, string expandOrCollapse)
        {
            // Get the group item to expand or collapse:        
            IWebElement expandableElem = tblElem.FindElements(By.TagName("img"))[0];
            IWebElement collapsableElem = tblElem.FindElements(By.TagName("img"))[1];

            if (expandOrCollapse == "expand")
            {
                // If the expandable button is displayed, then we should click it to expand. If not, it is already expanded
                if (expandableElem.Displayed)
                {
                    expandableElem.Click();
                }
            }
            else
            {
                // If the collapsable button is displayed, then we should click it to collapse. If not, it is already collapsed
                if (collapsableElem.Displayed)
                {
                    collapsableElem.Click();
                }
            }
            Thread.Sleep(0200);
            Browser.WaitForElement(Bys.CBDObserverPage.ExpiredDeclinedTbl, ElementCriteria.IsEnabled);
            WaitForInitialize();
        }

        /// <summary>
        /// Clicks the Actions button inside of a user-specified row, clicks the Accept/Decline button, fills the form and either
        /// clicks the Accept or Decline button
        /// </summary>
        /// <param name="learnerName">The first and last name of the learner with the pending observation request</param>
        /// <param name="acceptOrDecline">Either "Accept" "Decline"</param>
        /// <returns></returns>
        public AcceptDeclineAssignmentFormInfo AcceptOrDeclineAssignment(string learnerName, string formName, string acceptOrDecline)
        {
            ElemSet.Grid_ClickMenuItemInsideButton(Browser, PendingAcceptanceTbl, Bys.CBDObserverPage.PendingAcceptanceTblRowBody,  learnerName, formName, "Actions", "Accept/Decline Assignment");

            // Wait until the Accept/Decline assesment window opens
            Browser.WaitForElement(Bys.CBDObserverPage.AccDecAssgnMntFormCommentsTxt, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            Browser.WaitForElement(Bys.RCPPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
            .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));

            AcceptDeclineAssignmentFormInfo ADFO = FillAcceptDeclineAssignmentForm();

            if (acceptOrDecline == "Accept")
            {
                ClickAndWait(AccDecAssgnMntFormAcceptBtn);
            }
            else
            {   // TOD: Add code for declining assessment
                ClickAndWait(AccDecAssgnMntFormDeclineBtn);
                ClickAndWait(ConfirmFormDeclineAssessYesBtn);
            }
            return ADFO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="learnerName">The first and last name of the learner with the pending observation request</param>
        /// <param name="acceptOrDecline">Either "Accept" "Decline"</param>
        /// <returns></returns>
        public void RemoveExpiredDeclinedRequest(string learnerName, string formName)
        {
            ElemSet.Grid_ClickMenuItemInsideButton(Browser, ExpiredDeclinedTbl, Bys.CBDObserverPage.ExpiredDeclinedTblRowBody, learnerName, formName, "Actions", "Remove");
            ClickAndWait(ConfirmFormRemoveAssessOkBtn);
        }

        public CompletedAssessmentInfo CompleteAssessment(string learnerName, string formName)
        {
            OpenCompleteAssessFormmentWindow(learnerName, formName);
            CompletedAssessmentInfo CAF = FillObservationFieldsOnFormToCompleteAssessment();

            ClickAndWait(CompleteAssessFormSubmitBtn);
            ClickAndWait(ConfirmFormCompleteAssessYesBtn);
            return CAF;
        }

        public void OpenCompleteAssessFormmentWindow(string learnerName, string formName)
        {
            ElemSet.Grid_ClickMenuItemInsideButton(Browser, AcceptedTbl, Bys.CBDObserverPage.AcceptedTblRowBody, learnerName, formName, "Actions", "Complete Assessment");
            Browser.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormFeedbackTxt, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormDateControlExpandBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            this.WaitForInitialize();
        }

        /// <summary>
        /// Fills the Accept/Decline form with random data
        /// </summary>
        /// <returns></returns>
        public AcceptDeclineAssignmentFormInfo FillAcceptDeclineAssignmentForm()
        {
            string comments = DataUtils.GetRandomSentence(20);
            AccDecAssgnMntFormCommentsTxt.SendKeys(comments);

            return new AcceptDeclineAssignmentFormInfo(comments);
        }

        public CompletedAssessmentInfo AcceptAndCompleteObservation(string learnerName, string formName)
        {
            AcceptOrDeclineAssignment(learnerName, formName, "Accept");
            return CompleteAssessment(learnerName, formName);
        }

        /// <summary>
        /// <see cref="AcceptOrDeclineAssignment"/> <see cref="CompleteAssessFormment"/>
        /// </summary>
        /// <param name="learnerName">The first and last name of the learner with the pending observation request</param>
        /// <param name="noOfObservations">However many observations you want to complete</param>
        /// <returns></returns>
        public List<CompletedAssessmentInfo> AcceptAndCompleteObservations(string learnerName, string formName, int noOfObservations)
        {
            List<CompletedAssessmentInfo> CA = new List<CompletedAssessmentInfo>();

            for (var i = 0; i < noOfObservations; i++)
            {
                AcceptOrDeclineAssignment(learnerName, formName, "Accept");
                CA.Add(CompleteAssessment(learnerName, formName));
            }

            return CA;
        }

        /// <summary>
        /// Fills the Complete Assessment form with random data. TODO: Need to add code to fill in the non-required fields
        /// </summary>
        /// <returns></returns>
        public CompletedAssessmentInfo FillObservationFieldsOnFormToCompleteAssessment()
        {
            Dictionary<string, string> date = DataUtils.GetDateForCalendarElem(DateTime.Today.AddMonths(-1));
            string dateChosen = ElemSet.DatePicker_ChooseDate(Browser, date["year"], date["month"], date["day"]);

            // TODO: See below
            // T
            FillGenericFieldsOnObservationForm();

            string rating = ElemSet.RdoBtn_ClickRandom(Browser, "I needed to be there just in case");
            // TODO: Add dynamic wait logic instead of sleep, only if it is needed though, as I dont think the page loads at this point
            Thread.Sleep(0500);

            int ratingValue = GetAssessmentRating(rating);

            string feedback = DataUtils.GetRandomSentence(40);
            CompleteAssessFormFeedbackTxt.SendKeys(feedback);

            return new CompletedAssessmentInfo(dateChosen, rating, ratingValue, feedback, DateTime.Now.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// The Observation form has required fields that are dynamically different between each form. Because of this, we are going to have to 
        /// condition our code to make a selection/TextBoxEnterText whether the controls exist on a form or not. Speaking with Nirav, he said there can
        /// be a maximum of 5 controls on a form. So for example, there can be 4 single select dropdowns and 1 text box. Or 5 multi select 
        /// dropdowns. Or 1 text box, 2 multi selects, and 3 single selects. These controls will have IDs as follows: text boxes will have 
        /// IDs starting with "Context1freetext" through "Context5freetext". Single select dropdowns will be "Context1" through "Context5", 
        /// and  then multi select will be "Context1multiselect" through "Context5multiselect"
        /// </summary>
        private void FillGenericFieldsOnObservationForm()
        {
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric1Txt))
            {
                CompleteAssessFormGeneric1Txt.SendKeys("33");
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric2Txt))
            {
                CompleteAssessFormGeneric2Txt.SendKeys("33");
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric3Txt))
            {
                CompleteAssessFormGeneric3Txt.SendKeys("33");
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric4Txt))
            {
                CompleteAssessFormGeneric4Txt.SendKeys("33");
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric5Txt))
            {
                CompleteAssessFormGeneric5Txt.SendKeys("33");
            }

            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric1SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric1SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric2SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric2SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric3SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric3SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric4SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric4SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric5SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric5SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric6SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric6SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric7SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric7SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric8SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric8SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric9SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric9SelElem);
            }
            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormGeneric10SelElem))
            {
                ElemSet.SelElem_Single_SelectRandomItem(CompleteAssessFormGeneric10SelElem);
            }
        }

        public decimal GetMinMaxOrMeanFromMultipleCompletedAssessments(List<CompletedAssessmentInfo> assessments, string minMaxOrMean)
        {
            List<decimal> ratingValues = new List<decimal>();

            foreach (var assessment in assessments)
            {
                ratingValues.Add(assessment.RatingValue);
            }

            if (minMaxOrMean == "min") { return ratingValues.Min(); }
            else if (minMaxOrMean == "max") { return ratingValues.Max(); }
            else { return ratingValues.Average(); }

        }

        private int GetAssessmentRating(string rating)
        {
            int ratingValue = 0;

            if (rating == "I had to do") { return 1; }
            if (rating == "I had to talk them through") { return 2; }
            if (rating == "I needed to prompt") { return 3; }
            if (rating == "I needed to be there just in case") { return 4; }
            if (rating == "I didn't need to be there") { return 5; }

            return ratingValue;
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.CBDObserverPage.AccDecAssgnMntFormAcceptBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AccDecAssgnMntFormAcceptBtn.GetAttribute("outerHTML"))
                {
                    AccDecAssgnMntFormAcceptBtn.Click();
                    this.WaitForInitialize();
                    // 9/18/17 Commented the following line. Im pretty sure this is not needed anymore, because I implemented this wait in the PageCriteria class
                    Browser.WaitForElement(Bys.RCPPage.LoadIcon, TimeSpan.FromSeconds(60), ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                    .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
                    return;
                }
            }

            if (Browser.Exists(Bys.RCPPage.CBDTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CBDTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(Criteria.CBDObserverPage.PageReady);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.AccDecAssgnMntFormDeclineBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AccDecAssgnMntFormDeclineBtn.GetAttribute("outerHTML"))
                {
                    AccDecAssgnMntFormDeclineBtn.Click();
                    Browser.WaitForElement(Bys.CBDObserverPage.ConfirmFormCompleteAssessYesBtn, ElementCriteria.IsEnabled);
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.ConfirmFormRemoveAssessOkBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ConfirmFormRemoveAssessOkBtn.GetAttribute("outerHTML"))
                {
                    ConfirmFormRemoveAssessOkBtn.Click();
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.ConfirmFormDeclineAssessYesBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ConfirmFormDeclineAssessYesBtn.GetAttribute("outerHTML"))
                {
                    ConfirmFormDeclineAssessYesBtn.Click();
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.CompleteAssessFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CompleteAssessFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    CompleteAssessFormSubmitBtn.Click();
                    this.WaitForInitialize();
                    Browser.WaitForElement(Bys.CBDObserverPage.ConfirmFormCompleteAssessYesBtn, ElementCriteria.IsEnabled);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.ConfirmFormCompleteAssessYesBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ConfirmFormCompleteAssessYesBtn.GetAttribute("outerHTML"))
                {
                    ConfirmFormCompleteAssessYesBtn.Click();
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.RCPPage.LogoutLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LogoutLnk.GetAttribute("outerHTML"))
                {
                    LogoutLnk.Click();
                    new WebDriverWait(Browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.UrlContains("login"));
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.AddObservationBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddObservationBtn.GetAttribute("outerHTML"))
                {
                    AddObservationBtn.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDObserverPage.AddObsFormSearchBtn);
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.AddObsFormSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddObsFormSearchBtn.GetAttribute("outerHTML"))
                {
                    AddObsFormSearchBtn.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDObserverPage.AddObsFormObsTblFacultyColHdr);
                    this.WaitForInitialize();
                    Thread.Sleep(1000);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDObserverPage.AddObsFormNextBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddObsFormNextBtn.GetAttribute("outerHTML"))
                {
                    AddObsFormNextBtn.Click();
                    Browser.WaitForElement(Bys.CBDObserverPage.AddObsFormBackBtn, ElementCriteria.IsEnabled);
                    Browser.WaitForElement(Bys.CBDObserverPage.AddObsFormBackBtn, ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.CBDObserverPage.AddObsFormBackBtn, ElementCriteria.HasText);
                    this.WaitForInitialize();
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
        /// Opens the Add Observation window if it is not already open, then chooses a user-specified learner, clicks the Next button,
        /// chooses the EPA radio button, chooses a user-specified stage, epa and observation tool, then completes the observation
        /// </summary>
        /// <param name="learnerFullName">The first and last name of the learner</param>
        /// <param name="observationType">Not implemented yet, just leave this null for now</param>
        /// <param name="stage">The exact text of any stage within the Stage select element on the Add Observation form</param>
        /// <param name="epa">The exact text of any EPA underneath the Stage select element</param>
        /// <param name="observationTool">The exact text of any observation tool underneath the EPAs on this form</param>
        /// <returns>An object that represents the learner, the stage, epa, observation tool, and also the completed observation details</returns>
        // TODO: Implement Narrative observation type. Make another method where the selection of stage, epa and observation tool are random selections (AddRandomObservation).
        public AddedObservationInfo AddObservation(string learnerFullName, string observationType, string stage, string epa, string observationTool)
        {
            //AddObservationObject AOO = new AddObservationObject("", "", "", );

            // If the form is not open, then open it. 
            if (!Browser.Exists(Bys.CBDObserverPage.AddObsFormLearnerNameTxt, ElementCriteria.IsVisible))
            {
                ClickAndWait(AddObservationBtn);
            }

            SearchAndSelectLearnerOnAddObservationForm(learnerFullName);
            ClickAndWait(AddObsFormNextBtn);

            ElemSet.RdoBtn_ClickByText(Browser, "EPA/IM Observation");
            ClickAndWait(AddObsFormNextBtn);

            AddObsFormStageSelElem.SelectByText(stage);
            Browser.WaitForElement(Bys.CBDObserverPage.AddObsFormBackBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled, ElementCriteria.HasText);
            this.WaitForInitialize();

            ElemSet.RdoBtn_ClickByText(Browser, epa);
            Browser.WaitForElement(Bys.CBDObserverPage.AddObsFormBackBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled, ElementCriteria.HasText);
            this.WaitForInitialize();

            ElemSet.RdoBtn_ClickByText(Browser, observationTool);
            AddObsFormNextBtn.Click();
            Browser.WaitForElement(Bys.CBDObserverPage.CompleteAssessFormFeedbackTxt, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
            this.WaitForInitialize();

            // Fill out the observation form and return the values that were chosen
            CompletedAssessmentInfo assessment = FillObservationFieldsOnFormToCompleteAssessment();

            ClickAndWait(CompleteAssessFormSubmitBtn);

            return new AddedObservationInfo(learnerFullName, stage, observationTool, assessment);
        }

        /// <summary>
        /// Enters text into the Learner Name field on the Add Observation form, clicks the Search button, then selects
        /// a learner from the list of radio buttons
        /// </summary>
        /// <param name="learnerFullName">The first and last name of the learner</param>
        private void SearchAndSelectLearnerOnAddObservationForm(string learnerFullName)
        {
            AddObsFormLearnerNameTxt.SendKeys(learnerFullName);
            ClickAndWait(AddObsFormSearchBtn);
            ElemSet.RdoBtn_ClickByText(Browser, learnerFullName);
        }



        #endregion wrappers



    }

 
}