using Browser.Core.Framework;
using Browser.Core.Framework.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace RCP.AppFramework
{
    public class CBDLearnerPage : RCPPage, IDisposable
    {
        #region constructors
        public CBDLearnerPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that we start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "cbd#/learner"; } }

        /// <summary>
        /// Property generated from <see cref="StoreObvsCntFromEPAObvCntGraph"/>
        /// </summary>
        private static int lastEPAObvsCntFromEPAObvGraph;
        public int PreviousEPAObvsCntFromEPAObvGraph { get { return lastEPAObvsCntFromEPAObvGraph; } }


        #endregion properties

        #region elements     
        public IWebElement CBDTab { get { return this.FindElement(Bys.RCPPage.CBDTab); } }
        public IWebElement RequestObsFormFirstRdo { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormFirstRdo); } }
        public IWebElement AddReflectFormHiddenBrowseBtn { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormHiddenBrowseBtn); } }
        public IWebElement AddReflectFormBrowseToAddTxt { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormBrowseToAddTxt); } }
        public IWebElement RequestObsFormRdoBtnTbody { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormRdoBtnTbody); } }
        public IWebElement ReflectionsTblBdy { get { return this.FindElement(Bys.CBDLearnerPage.ReflectionsTblBdy); } }
        public IWebElement CurrentStageValueLbl { get { return this.FindElement(Bys.CBDLearnerPage.CurrentStageValueLbl); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.CBDLearnerPage.BackBtn); } }
        public IWebElement UploadEvidenceBtn { get { return this.FindElement(Bys.CBDLearnerPage.UploadEvidenceBtn); } }
        public IWebElement RequestObservationBtn { get { return this.FindElement(Bys.CBDLearnerPage.RequestObservationBtn); } }
        public IWebElement EPAIMTbl { get { return this.FindElement(Bys.CBDLearnerPage.EPAIMTbl); } }
        public IWebElement EPAObservCntChrt { get { return this.FindElement(Bys.CBDLearnerPage.EPAObservCntChrt); } }
        public IWebElement HideAchievedChk { get { return this.FindElement(Bys.CBDLearnerPage.HideAchievedChk); } }
        public IWebElement UserNameLbl { get { return this.FindElement(Bys.CBDLearnerPage.UserNameLbl); } }
        public IWebElement RoleLnk { get { return this.FindElement(Bys.CBDLearnerPage.RoleLnk); } }
        public IWebElement ViewMoreRptsLnk { get { return this.FindElement(Bys.CBDLearnerPage.ViewMoreRptsLnk); } }
        public IWebElement PrgLrnPlanTab { get { return this.FindElement(Bys.CBDLearnerPage.PrgLrnPlanTab); } }
        public IWebElement ReflectionsTab { get { return this.FindElement(Bys.CBDLearnerPage.ReflectionsTab); } }
        public IWebElement NarrativesTab { get { return this.FindElement(Bys.CBDLearnerPage.NarrativesTab); } }
        public IWebElement SupportingDocsTab { get { return this.FindElement(Bys.CBDLearnerPage.SupportingDocsTab); } }
        public IWebElement AssessDetailsTab { get { return this.FindElement(Bys.CBDLearnerPage.AssessDetailsTab); } }
        public IWebElement ObservationsTab { get { return this.FindElement(Bys.CBDLearnerPage.ObservationsTab); } }
        public IWebElement EvidenceTab { get { return this.FindElement(Bys.CBDLearnerPage.EvidenceTab); } }
        public IWebElement AddReflectBtn { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectBtn); } }

        public IWebElement AddReflectFormReflectionTitleTxt { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormReflectionTitleTxt); } }
        public IWebElement AddReflectFormContinueBtn { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormContinueBtn); } }
        public IWebElement AddReflectFormSubmitBtn { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormSubmitBtn); } }
        public IWebElement AddReflectFormStimulusTxt { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormStimulusTxt); } }
        public IWebElement AddReflectFormReflectionDescriptionTxt { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormReflectionDescriptionTxt); } }
        public IWebElement AddReflectFormBrowseBtn { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormBrowseBtn); } }
        public IWebElement ShowingLbl { get { return this.FindElement(Bys.CBDLearnerPage.ShowingLbl); } }
        public IWebElement TableFirstBtn { get { return this.FindElement(Bys.CBDLearnerPage.TableFirstBtn); } }
        public IWebElement TableNextBtn { get { return this.FindElement(Bys.CBDLearnerPage.TableNextBtn); } }
        public IWebElement RequestObsFormObsNameTxt { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormObsNameTxt); } }
        public IWebElement RequestObsFormSearchBtn { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormSearchBtn); } }
        public IWebElement RequestObsFormPartARdo { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormPartARdo); } }
        public IWebElement RequestObsFormPartBRdo { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormPartBRdo); } }
        public IWebElement RequestObsFormPartCRdo { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormPartCRdo); } }
        public IWebElement RequestObsFormOkBtn { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormOkBtn); } }
        public IWebElement RequestObsFormCancelBtn { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormCancelBtn); } }
        public IWebElement RequestObsFormRequestBtn { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormRequestBtn); } }
        public IWebElement RequestObsFormObsTblFacultyColHdr { get { return this.FindElement(Bys.CBDLearnerPage.RequestObsFormObsTblFacultyColHdr); } }
        public IWebElement ReportsFormEPAAttainmentChrt { get { return this.FindElement(Bys.CBDLearnerPage.ReportsFormEPAAttainmentChrt); } }
        public IWebElement ReportsFormEPAProgressionOverTimeChrt { get { return this.FindElement(Bys.CBDLearnerPage.ReportsFormEPAProgressionOverTimeChrt); } }
        public IWebElement AddReflectFormBackBtn { get { return this.FindElement(Bys.CBDLearnerPage.AddReflectFormBackBtn); } }
        public SelectElement ReportsFormReportSelElem { get { return new SelectElement(this.FindElement(Bys.CBDLearnerPage.ReportsFormReportSelElem)); } }
        public IWebElement ReportsFormShowBtn { get { return this.FindElement(Bys.CBDLearnerPage.ReportsFormShowBtn); } }
        public IWebElement ReportsFormCloseBtn { get { return this.FindElement(Bys.CBDLearnerPage.ReportsFormCloseBtn); } }
        public IWebElement ReportsFormEPAObservCntChrt { get { return this.FindElement(Bys.CBDLearnerPage.ReportsFormEPAObservCntChrt); } }
        public IWebElement LearnerStatusLbl { get { return this.FindElement(Bys.CBDLearnerPage.LearnerStatusLbl); } }
        public IWebElement ReflectionsTbl { get { return this.FindElement(Bys.CBDLearnerPage.ReflectionsTbl); } }
        public IWebElement AssessmentDetailsTbl { get { return this.FindElement(Bys.CBDLearnerPage.AssessmentDetailsTbl); } }



        #endregion elements

        #region methods: repeated per page

        /// <summary>
        /// This method waits for the landing page (page after we login, which consists of the Program Learning Plan table) to load
        /// (if the tester set his criteria up like he is supposed to, inside the PageCritera class). It can be called in other
        /// scenarios also
        /// </summary>
        public override void WaitForInitialize()
        {
            // Sometimes the full page takes forever to load (and in some cases I dont think it will ever load), so we will put a try/catch here.
            // Try for waiting for the full page load. If the full page doesnt load in time, then refresh the page and try again
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.PageReadyWithProgramLearningTabReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDLearnerPage.PageReadyWithProgramLearningTabReady);
        }

        /// <summary>
        /// I added this at one point, but I forgot what this accomplishes
        /// </summary>
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
        /// Clicks the expansion icon of any row within a table underneath the lower tabs (Reflections, Narratives, etc.) of this page. It will
        /// then click on the link underneath that expansion. 
        /// </summary>
        /// <param name="tblElemBy">The table element that contains the expansion/collapse icon. Send the By type of the table. <see cref="CBDLearnerPageBys"/></param>
        /// <param name="groupItemToExpand">The exact text of the row name with the the expansion icon</param>
        /// <param name="linkTextToClick">The exact text of the link you want to click on</param>
        /// ToDo: I still need to add logic to wait for a given criteria after the link is clicked.
        /// I may want to transfer this to <see cref="ElemSet"/> once I get familar with all of the roles, as it may be able to be used for all of them
        public void ExpandTableAndClickLink(By tblElemBy, string groupItemToExpand, string linkTextToClick)
        {
            // Expand the grouping row
            Grid_ExpandOrCollapseButton(tblElemBy, groupItemToExpand, "expand");

            // Get the link elem
            IWebElement table = Browser.FindElement(tblElemBy);
            IWebElement linkElem = table.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]", linkTextToClick)));

            ElemSet.ScrollToElement(Browser, linkElem);
            linkElem.Click();
            // ToDo: Add more wait criteria for different conditions. Right now, this is only satisfying waiting after a user 
            // clicks on an EPA in the EPA/IM table of Program Learning Plan table
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDLearnerPage.LoadElementDoneLoading);
        }

        /// <summary>
        /// Expans or collapses the given row within a table underneath the lower tabs (Reflections, Narratives, etc.)
        /// </summary>
        /// <param name="tblElemBy">The table element that contains the expansion/collapse icon. Send the By type of the table. <see cref="CBDLearnerPageBys"/></param>
        /// <param name="groupdItemToExpandOrCollapse">The exact text of the row name with the the expansion/collapse icon</param>
        /// <param name="expandOrCollapse">Either "expand" or "collapse"</param>
        public void Grid_ExpandOrCollapseButton(By tblElemBy, string groupdItemToExpandOrCollapse, string expandOrCollapse)
        {
            // I had trouble locating this element. At first I used //td[contains(text(),'{0}')] But that did not work. After posting to stackoverflow, I was told 
            // that whenever an element has 2 text nodes (this one does), I need to use this the Xpath like this instead //td[contains(., '{0}')] as that one
            // searches all text nodes of an element. If you object inspect the "Transition to discipline" row in the Program Learning Plan tab of the Learner page,
            // you will see that the element has comment text (green text) before and after a span element within it. That means it has 2 text nodes
            // https://stackoverflow.com/questions/45446631/using-xpath-contains-function-to-find-element-that-contains-text?noredirect=1#comment77872147_45446631
            string xpath = string.Format("//td[contains(., '{0}')]", groupdItemToExpandOrCollapse);
            // string xpath = string.Format("//tr[td//text()[contains(., '{0}')]]", groupdItemToExpandOrCollapse);

            // Get the group item to expand or collapse  
            IWebElement table = Browser.FindElement(tblElemBy);
            IWebElement elemToExpandOrCollapse = table.FindElement(By.XPath(xpath));

            // Get the expandable button and collapsable button
            IWebElement expandableElem = elemToExpandOrCollapse.FindElements(By.TagName("img"))[0];
            IWebElement collapsableElem = elemToExpandOrCollapse.FindElements(By.TagName("img"))[1];

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
            Thread.Sleep(0500);
        }

        /// <summary>
        /// Clicks on any tab within the main page (this does not include popups) of the Learner page
        /// </summary>
        /// <param name="tabElem">The tab element</param>
        /// <param name="by">The element as it exists in the By type. For examples, <see cref="Bys.CBDLearnerPage"/></param>
        public void SwitchToTab(IWebElement tabElem, By by)
        {
            Browser.WaitForElement(Bys.RCPPage.LoadIcon, TimeSpan.FromSeconds(3), ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
            tabElem.Click();
            this.WaitForElement(by, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDLearnerPage.GenericTableEnabled);
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public void ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.CBDLearnerPage.BackBtn))
            {
                // This is a workaround to be able to use an IF statement on an IWebElement type.
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackBtn.GetAttribute("outerHTML"))
                {
                    BackBtn.Click();
                    this.WaitForInitialize();
                    return;
                }
            }

            if (Browser.Exists(Bys.RCPPage.CBDTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CBDTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.CBDLearnerPage.PageReadyWithProgramLearningTabReady);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.AddReflectBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddReflectBtn.GetAttribute("outerHTML"))
                {
                    // NOTE: This sometimes does not work in Firefox whenever the tooltip for one of the tabs appears. Firefox
                    // fails to click when a tooltip is covering the button. However, this started working today. If it stops
                    // working again, we will have to implement a workaround in the SwitchTabs method
                    AddReflectBtn.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDLearnerPage.AddReflectFormContinueBtn);
                    //Browser.WaitForElement(Bys.CBDLearnerPage.AddReflectFormContinueBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    this.WaitUntil(TimeSpan.FromSeconds(360), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.AddReflectFormContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddReflectFormContinueBtn.GetAttribute("outerHTML"))
                {
                    AddReflectFormContinueBtn.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDLearnerPage.AddReflectFormBrowseBtn);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                    //Browser.WaitForElement(Bys.CBDLearnerPage.AddReflectFormBrowseBtn, ElementCriteria.IsVisible);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.AddReflectFormSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddReflectFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    AddReflectFormSubmitBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.PageReadyWithReflectionsTabReady);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.RequestObservationBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestObservationBtn.GetAttribute("outerHTML"))
                {
                    RequestObservationBtn.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDLearnerPage.RequestObsFormObsNameTxt);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                    //Browser.WaitForElement(Bys.CBDLearnerPage.RequestObsFormObsNameTxt, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    //Browser.WaitForElement(Bys.CBDLearnerPage.RequestObsFormSearchBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    // Need to revisit the above 2 lines. For some reason, this needs a tiny static wait to work sometimes
                    //Thread.Sleep(0600);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.RequestObsFormSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestObsFormSearchBtn.GetAttribute("outerHTML"))
                {
                    // 11/30/17: It failed today. Said "Failed to find any elements By.XPath: //tbody[@aria-label='Select Observer']"
                    // I looked on the screenshot and the radio button never got returned, even though the label beneath it said 
                    // "Showing 1-1 of 1". This tells me that this is an application bug. Hopefully this fixes itself when DEV works
                    // on performance improvements. If not, and I see this again, I might have to add some more logic below to keep 
                    // clicking or something else
                    // 11/05/17: For some reason, selenium sometimes fails to click this button. So I am adding a Catch and using javascript based click event if this fails
                    try
                    {
                        RequestObsFormSearchBtn.Click();
                        this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.RequestObsFormRdoBtnTbodyVisible,
                            Criteria.CBDLearnerPage.RequestObsFormFirstRdoVisible);
                        this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                        // Adding a Sleep here for now until I have time to come back and add some dynamic wait criteria
                        Thread.Sleep(0300);
                    }
                    catch
                    {
                        ((IJavaScriptExecutor)Browser).ExecuteScript("arguments[0].click()", RequestObsFormSearchBtn);
                        this.WaitForElement(Bys.CBDLearnerPage.RequestObsFormRdoBtnTbody, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
                        this.WaitUntilAll(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                        // Adding a Sleep here for now until I have time to come back and add some dynamic wait criteria
                        Thread.Sleep(0300);
                    }
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.RequestObsFormRequestBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestObsFormRequestBtn.GetAttribute("outerHTML"))
                {
                    RequestObsFormRequestBtn.Click();
                    this.WaitForElement(Bys.CBDLearnerPage.RequestObsFormOkBtn, ElementCriteria.IsVisible);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.RequestObsFormOkBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == RequestObsFormOkBtn.GetAttribute("outerHTML"))
                {
                    RequestObsFormOkBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
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

            if (Browser.Exists(Bys.CBDLearnerPage.AddReflectFormContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddReflectFormContinueBtn.GetAttribute("outerHTML"))
                {
                    AddReflectFormContinueBtn.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDLearnerPage.AddReflectFormBrowseBtn);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.ViewMoreRptsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ViewMoreRptsLnk.GetAttribute("outerHTML"))
                {
                    ViewMoreRptsLnk.Click();
                    WaitUtils.WaitForPopup(Browser, TimeSpan.FromSeconds(30), Bys.CBDLearnerPage.ReportsFormShowBtn);
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.LoadElementDoneLoading);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.ReportsFormShowBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ReportsFormShowBtn.GetAttribute("outerHTML"))
                {
                    ReportsFormShowBtn.Click();
                    Browser.WaitForElement(Bys.CBDLearnerPage.ReportsFormEPAObservCntChrt, ElementCriteria.IsVisible, ElementCriteria.IsEnabled, ElementCriteria.HasText);
                    return;
                }
            }

            if (Browser.Exists(Bys.CBDLearnerPage.ReportsFormCloseBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ReportsFormCloseBtn.GetAttribute("outerHTML"))
                {
                    ReportsFormCloseBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CBDLearnerPage.PageReadyWithProgramLearningTabReady);
                    return;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, or " +
                    "if the button is already added, then the page you were on did not contain the button.");
            }
        }

        /// <summary>
        /// Stores the observation count from any EPA inside the EPA Observation Count graph into <see cref="PreviousEPAObvsCntFromEPAObvGraph"/>
        /// </summary>
        /// <param name="chrt">The chart element</param>
        /// <param name="epaName">The EPA name that you want the count of observations from. ie. "EPA 1.1"</param>
        public void StoreObvsCntFromEPAObvCntGraph(IWebElement chrt, string epaName)
        {
            lastEPAObvsCntFromEPAObvGraph = GetEPAObsvCntFromEPAObsGraph(chrt, epaName);
        }

        public void StoreObvsCntsFromEPAObvCntGraph(string epaName)
        {
            //Browser.FindElement(By.Id("This Should Fail"));
            Assert.AreEqual(13, 12, "The count on the Observation Count Graph for EPA 3.14 did not increase to 13 observations after an observation was submitted");
        }

        /// <summary>
        /// Gets the current observation count from the EPA Observation Count graph
        /// </summary>
        /// <param name="chrt">The chart element</param>
        /// <param name="epaName"></param>
        /// <returns></returns>
        public int GetEPAObsvCntFromEPAObsGraph(IWebElement chrt, string epaName)
        {
            DataTable EPAObvCountGraphDT = GraphUtils.GetHighChartDataTable(Browser, chrt);
            string EPAObservationCount = DataUtils.GetDataTableCellByRecNameAndColName(EPAObvCountGraphDT, "category", epaName, "data");
            return Int32.Parse(EPAObservationCount);
        }

        /// <summary>
        /// Gets the Min, Mean, or Max value from a specific EPA on the EPA Attaintment graph popup after the
        /// View More Reports link is clicked
        /// </summary>
        /// <param name="chrt">The chart element</param>
        /// <param name="epaName">The exact text from the EPA name on the graph. For example, if you have 6 observations completed for EPA 1.1, then the exact text would be "EPA 1.1 (6)"</param>
        /// <param name="meanMinOrMax">Either "min", "max" or "mean"</param>
        /// <returns></returns>
        public decimal GetMinMeanOrMaxFromEPAAttainGraph(IWebElement chrt, string epaName, string meanMinOrMax)
        {
            string columnToReturn = "";

            DataTable EPAAttainGraphDT = GraphUtils.GetHighChartDataTable(Browser, chrt);

            if (meanMinOrMax == "mean") { columnToReturn = "average"; }
            if (meanMinOrMax == "min") { columnToReturn = "range0"; }
            if (meanMinOrMax == "max") { columnToReturn = "range1"; }

            string EPAObservationCount = DataUtils.GetDataTableCellByRecNameAndColName(EPAAttainGraphDT, "category", epaName, columnToReturn);

            // We need to round to the nearest whole number for min and max because DEV intentionally subtracts .05 for either value
            // because if not, the blue bar wouldnt show
            if (meanMinOrMax == "min" || meanMinOrMax == "max")
            {
                return Math.Round(System.Convert.ToDecimal(EPAObservationCount));
            }
            else
            {
                return System.Convert.ToDecimal(EPAObservationCount);
            }
        }

        /// <summary>
        /// Opens the Add Reflection form from the Learner page if it is not already open, fills the form with random data. 
        /// </summary>
        /// <param name="isActivityAccredited">"true" is you want credits, "false" is not</param>
        /// <returns><see cref="LearnerRelectionObject"/></returns>
        public LearnerRelectionObject AddReflection()
        {
            if (!Browser.Exists(Bys.CBDLearnerPage.AddReflectFormStimulusTxt, ElementCriteria.IsVisible))
            {
                ClickAndWait(AddReflectBtn);
            }

            string reflectionTitle = DataUtils.GetRandomString(10);
            AddReflectFormReflectionTitleTxt.SendKeys(reflectionTitle);

            bool tf = DataUtils.GetRandomBool();

            string stimulus = DataUtils.GetRandomString(10);
            AddReflectFormStimulusTxt.SendKeys(stimulus);

            string reflectionDesc = DataUtils.GetRandomSentence(40);
            AddReflectFormReflectionDescriptionTxt.SendKeys(reflectionDesc);

            ClickAndWait(AddReflectFormContinueBtn);

            //FileUtils.UploadFileUsingSendKeys(Browser, AddReflectFormHiddenBrowseBtn, Bys.CBDLearnerPage.AddReflectFormHiddenBrowseBtn, "C:\\SeleniumAutoIt\\test.txt");

            ClickAndWait(AddReflectFormSubmitBtn);

            return new LearnerRelectionObject(reflectionTitle, tf, stimulus, reflectionDesc);
        }

        /// <summary>
        /// Navigates to the main page of the learner application, if that page is not already navigated to. Chooses a
        /// user-defined EPA, then opens the Request Observation window , then chooses a user-specified observer and template, then
        /// clicks the Request button and then the Ok button
        /// </summary>
        /// <param name="tblElem">Any table element underneath the Program Learning Plan tab of the Learner role main page</param>
        /// <param name="groupItemToExpand">The exact text of the row to expand in the above table</param>
        /// <param name="linkTextToClick">The exact text of the EPA link to click on</param>
        /// <param name="observerName">The observer name as it appears on the Request Observation window</param>
        /// <param name="templateName">The learner name as it appears on the Request Observation window</param>
        /// <returns></returns>
        public LearnerRequestObsObject RequestObservationForEPA(string groupItemToExpand, string linkTextToClick, string observerName, string templateName)
        {
            LearnerRequestObsObject LRO = new LearnerRequestObsObject("", "");

            // If we are not on the main page, then click Back to go there
            if (!Browser.Exists(Bys.CBDLearnerPage.AddReflectBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(BackBtn);
            }

            // Choose the EPA
            ExpandTableAndClickLink(Bys.CBDLearnerPage.EPAIMTbl, groupItemToExpand, linkTextToClick);

            // If the above ExpandTableAndClickLink method failed to click the link, then lets click the link again
            if (!Browser.Exists(Bys.CBDLearnerPage.RequestObservationBtn, ElementCriteria.IsVisible))
            {
                IWebElement table = Browser.FindElement(Bys.CBDLearnerPage.EPAIMTbl);
                IWebElement linkElem = table.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]", linkTextToClick)));
                linkElem.Click();
                // ToDo: Add more wait criteria for different conditions. Right now, this is only satisfying waiting after a user 
                // clicks on an EPA in the EPA/IM table of Program Learning Plan table
                this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CBDLearnerPage.LoadElementDoneLoading);
            }

            // Open the Request Observation form
            ClickAndWait(RequestObservationBtn);

            // Fill out the form and return the values that were chosen
            LRO = SearchAndSelectObserverAndTemplateOnRequestObservationForm(observerName, templateName);

            ClickAndWait(RequestObsFormRequestBtn);
            ClickAndWait(RequestObsFormOkBtn);

            return LRO;
        }

        /// <summary>
        /// Requests 1 or more observations. First it navigates to the main page of the learner application, if that page is not already 
        /// navigated to. Chooses a user-defined EPA, then opens the Request Observation window , then chooses a user-specified observer 
        /// and template, then clicks the Request button and then the Ok button. 
        /// </summary>
        /// <param name="tblElem">Any table element underneath the Program Learning Plan tab of the Learner role main page</param>
        /// <param name="groupItemToExpand">The exact text of the row to expand in the above table</param>
        /// <param name="linkTextToClick">The exact text of the EPA link to click on</param>
        /// <param name="observerName">The observer name as it appears on the Request Observation window</param>
        /// <param name="templateName">The learner name as it appears on the Request Observation window</param>
        /// <param name="noOfObsvs">The number of observations you want to request for the EPA</param>
        /// <returns></returns>
        public List<LearnerRequestObsObject> RequestObservationsForEPA(IWebElement tblElem, By by, string groupItemToExpand, string linkTextToClick, string observerName, string templateName, int noOfObsvs)
        {

            List<LearnerRequestObsObject> LRO = new List<LearnerRequestObsObject>();

            LRO.Add(RequestObservationForEPA(groupItemToExpand, linkTextToClick, observerName, templateName));

            for (var i = 0; i < noOfObsvs - 1; i++)
            {
                LRO.Add(RequestObservation(observerName, "Part A: Direct observation - Form 1"));
            }
            return LRO;
        }

        /// <summary>
        /// Opens the Request Observation window if it is not already open, then chooses a user-specified observer and template, then
        /// clicks the Request button and then the Ok button
        /// </summary>
        /// <param name="observerName"></param>
        /// <param name="templateName"></param>
        public LearnerRequestObsObject RequestObservation(string observerName, string templateName)
        {
            LearnerRequestObsObject LRO = new LearnerRequestObsObject("", "");

            // If the form is not open, then open it. 
            if (!Browser.Exists(Bys.CBDLearnerPage.RequestObsFormRequestBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(RequestObservationBtn);
            }

            // Fill out the form and return the values that were chosen
            LRO = SearchAndSelectObserverAndTemplateOnRequestObservationForm(observerName, templateName);

            ClickAndWait(RequestObsFormRequestBtn);
            ClickAndWait(RequestObsFormOkBtn);

            return LRO;
        }

        public LearnerRequestObsObject SearchAndSelectObserverAndTemplateOnRequestObservationForm(string observerName, string templateName)
        {
            RequestObsFormObsNameTxt.SendKeys(observerName);
            ClickAndWait(RequestObsFormSearchBtn);

            ElemSet.RdoBtn_ClickByText(Browser, observerName);
            ElemSet.RdoBtn_ClickByText(Browser, templateName);

            return new LearnerRequestObsObject(observerName, templateName);
        }

        #endregion wrappers

    }


}