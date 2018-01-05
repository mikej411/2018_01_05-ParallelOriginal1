using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;
using System.Threading;
using System.Globalization;
using NUnit.Framework;

namespace RCP.AppFramework
{
    public class EnterCPDActivityPage : RCPPage, IDisposable
    {
        #region constructors
        public EnterCPDActivityPage(IWebDriver driver) : base(driver)
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


        public IWebElement CloseFourthInstanceBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.CloseFourthInstanceBtn); } }
        public IWebElement SendToHoldingAreaBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.SendToHoldingAreaBtn); } }
        public IWebElement ScrollBar { get { return this.FindElement(Bys.EnterCPDActivityPage.ScrollBar); } }
        public IWebElement TypeOfPLPTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.TypeOfPLPTxt); } }
        public SelectElement SelectTheRelevantDomainSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.SelectTheRelevantDomainSelElem)); } }
        public SelectElement SAPNameSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.SAPNameSelElem)); } }
        public IWebElement IWillBesendingDocumentsChk { get { return this.FindElement(Bys.EnterCPDActivityPage.IWillBesendingDocumentsChk); } }
        public IWebElement TotalNumberOfArticlesTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.TotalNumberOfArticlesTxt); } }
        public SelectElement PleaseSelectTheTypeOfProjectItWasSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.PleaseSelectTheTypeOfProjectItWasSelElem)); } }
        public IWebElement CreditsForActivityValueLbl { get { return this.FindElement(Bys.EnterCPDActivityPage.CreditsForActivityValueLbl); } }
        public IWebElement DescribeTheQuestionTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.DescribeTheQuestionTxt); } }
        public SelectElement PleaseSelectTheTypeOfReadingActivitySelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.PleaseSelectTheTypeOfReadingActivitySelElem)); } }
        public SelectElement PleaseSelectTheTypeOfReadingSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.PleaseSelectTheTypeOfReadingSelElem)); } }
        public SelectElement Sec2SelfLearnActSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.Sec2SelfLearnActSelElem)); } }
        public SelectElement Sec3AssessActSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.Sec3AssessActSelElem)); } }
        public IWebElement EnterACPDFrame { get { return this.FindElement(Bys.EnterCPDActivityPage.EnterACPDFrame); } }
        public SelectElement Sec1GroupLearnActSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterCPDActivityPage.Sec1GroupLearnActSelElem)); } }
        public IWebElement IsActivityAccrYesRdo { get { return this.FindElement(Bys.EnterCPDActivityPage.IsActivityAccrYesRdo); } }
        public IWebElement IsActivityAccrNoRdo { get { return this.FindElement(Bys.EnterCPDActivityPage.IsActivityAccrNoRdo); } }
        public IWebElement HowManyHoursTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.HowManyHoursTxt); } }
        public IWebElement NameTheGroupActivityTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.NameTheGroupActivityTxt); } }
        public IWebElement WhatDateTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.WhatDateTxt); } }
        public IWebElement WhatDidYouLearnTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.WhatDidYouLearnTxt); } }
        public IWebElement WhatAdditLearningTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.WhatAdditLearningTxt); } }
        public IWebElement WhatChangesTxt { get { return this.FindElement(Bys.EnterCPDActivityPage.WhatChangesTxt); } }
        public IWebElement AddFilesBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.AddFilesBtn); } }
        public IWebElement CancelBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.CancelBtn); } }
        public IWebElement SendToHoldingBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.SendToHoldingBtn); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.ContinueBtn); } }
        public IWebElement OptionalTabSubmitBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.OptionalTabSubmitBtn); } }
        public IWebElement SupportingDocumentsTabSubmitBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.SupportingDocumentsTabSubmitBtn); } }
        public IWebElement CloseBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.CloseBtn); } }
        public IWebElement CloseSecondInstanceBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.CloseSecondInstanceBtn); } }
        public IWebElement CloseThirdInstanceBtn { get { return this.FindElement(Bys.EnterCPDActivityPage.CloseThirdInstanceBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(480), Criteria.EnterCPDActivityPage.PageReady);
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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.EnterCPDActivityPage.PageReady);
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

        #endregion methods: repeated per page

        #region methods: wrappers

        /// <summary>
        /// Selects a user-specified item from any of the Section 1/2/3 select elements on the Enter a CPD Activity form, fills in the fields on the
        /// activity form, Clicks the Continue button, clicks the Submit button, If the I Attest checkbox appears it will check this box and then click
        /// Submit again, then clicks Close and waits for the window to close
        /// </summary>
        /// <param name="activityType"><see cref="Constants.MainportActivityTypes"/></param>
        /// <param name="amountOfHoursOrArticlesForCredits">(Optional) The amount to enter in the How Many Hours/Articles text box. The hours/articles will then be your amount of credits. If the activity type you choose has a static amount of credits, which means they wont have these text boxes, then dont send this parameter.</param>
        /// <param name="accredited">(Optional) "true" to select the Yes radio button for certain activities, "false" to select the No radio button. Dont send this parameter if your activity does not have this radio button</param>
        /// <param name="dateCompleted">(Optional) The date the activity was completed, in the format of 12/1/2017. Dont send this parameter if you dont care about the date, it will then enter the current date for you</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity AddActivityThenSendToHoldingArea(Constants.MainportActivityTypes activityType, string amountOfHoursOrArticlesForCredits = "", bool accredited = true, string dateCompleted = "")
        {
            Activity NewActivity = FillActivityForm(activityType, amountOfHoursOrArticlesForCredits, accredited, dateCompleted);

            // Proceed to the next tab
            ClickAndWait(SendToHoldingAreaBtn);

            NewActivity.RequiresValidation = CloseActivityForm();

            NewActivity = AdjustCreditsPerRule(NewActivity, activityType, amountOfHoursOrArticlesForCredits, accredited);

            return NewActivity;
        }

        /// <summary>
        /// Used in conjunction with <see cref="AddActivityThenSubmit(Constants.MainportActivityTypes, string, bool, string)"/> This adjusts the amount of credits
        /// if the activity in question has a credit adjustment rule. For example, for some activities, the total number of credits will double per the number
        /// of hours the participant spent on the activity.
        /// </summary>
        /// <param name="Activity">The activity</param>
        /// <param name="activityType"><see cref="Constants.MainportActivityTypes"/></param>
        /// <param name="amountOfHoursOrArticlesForCredits">The amount to enter in the How Many Hours/Articles text box. The hours/articles will then be your amount of credits. </param>
        /// <param name="accredited">"true" to select the Yes radio button for certain activities, "false" to select the No radio button. </param>
        /// <returns></returns>
        private Activity AdjustCreditsPerRule(Activity Activity, Constants.MainportActivityTypes activityType, string amountOfHoursOrArticlesForCredits, bool accredited = true)
        {
            // For some activities, the total number of credits might double, triple or get cut in half. If an activity has that rule,
            // we will handle that here see Constants.MainportActivityTypes for more info on these rules
            switch (activityType)
            {
                case Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf:
                case Constants.MainportActivityTypes.Sec1_JournalClub_IfNotAccreditedCutCreditsInHalf:
                case Constants.MainportActivityTypes.Sec1_Rounds_IfNotAccreditedCutCreditsInHalf:
                case Constants.MainportActivityTypes.Sec1_SmallGroupSession_IfNotAccreditedCutCreditsInHalf:
                    if (!accredited)
                    {
                        Activity.Credits = (Int32.Parse(amountOfHoursOrArticlesForCredits) / 2).ToString();
                    }
                    break;
                case Constants.MainportActivityTypes.Sec2_PLPPersonalLearningProject_CreditsDoubled:
                case Constants.MainportActivityTypes.Sec2_Traineeship_CreditsDoubled:
                    Activity.Credits = (Int32.Parse(amountOfHoursOrArticlesForCredits) * 2).ToString();
                    break;
                case Constants.MainportActivityTypes.Sec3_AccreditedSelfAssessmentPrograms_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_AccreditedSimulationActivities_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_AnnualPerformanceReview_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_ChartAuditandFeedback_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_DirectObservation_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_FeedbackonTeaching_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_MultisourceFeedback_CreditsTripled:
                case Constants.MainportActivityTypes.Sec3_PracticeAssessment_CreditsTripled:
                    Activity.Credits = (Int32.Parse(amountOfHoursOrArticlesForCredits) * 3).ToString();
                    break;
            }

            return Activity;
        }

        /// <summary>
        /// Selects a user-specified item from any of the Section 1/2/3 select elements on the Enter a CPD Activity form, fills in the fields on the
        /// activity form, Clicks the Continue button, clicks the Submit button. If the I Attest checkbox appears it will check this box and then click
        /// Submit again, then clicks Close and waits for the window to close
        /// </summary>
        /// <param name="activityType"><see cref="Constants.MainportActivityTypes"/></param>
        /// <param name="amountOfHoursOrArticlesForCredits">(Optional) The amount to enter in the How Many Hours/Articles text box. The hours/articles will then be your amount of credits. If the activity type you choose has a static amount of credits, which means they wont have these text boxes, then dont send this parameter.</param>
        /// <param name="accredited">(Optional) "true" to select the Yes radio button for certain activities, "false" to select the No radio button. Dont send this parameter if your activity does not have this radio button</param>
        /// <param name="dateCompleted">(Optional) The date the activity was completed, in the format of 12/1/2017. Dont send this parameter if you dont care about the date, it will then enter the current date for you</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity AddActivityThenSubmit(Constants.MainportActivityTypes activityType, string amountOfHoursOrArticlesForCredits = "", bool accredited = true, string dateCompleted = "")
        {
            Activity NewActivity = FillActivityForm(activityType, amountOfHoursOrArticlesForCredits, accredited, dateCompleted);

            // Proceed to the next tab
            ClickAndWait(ContinueBtn);
            ClickAndWait(OptionalTabSubmitBtn);

            NewActivity.RequiresValidation = CloseActivityForm();

            NewActivity = AdjustCreditsPerRule(NewActivity, activityType, amountOfHoursOrArticlesForCredits, accredited);

            return NewActivity;
        }

        /// <summary>
        /// For certain scenarios, after we click the Submit or Send To Holding Area button, it will tell us we need to check a check box before proceeding. See 
        /// "Mainport user/activity/cycle/date Rules" in this class file for an explanation of why we are conditioning the code to wait for two different 
        /// scenarios. Note that this then triggers credit validation, meaning to receive credits, we have to login to Lifetime Support and click the Validate 
        /// button for this activity for this user 
        /// </summary>
        /// <returns></returns>
        private bool CloseActivityForm()
        {
            bool requiresValidation = true;

            if (Browser.FindElements(Bys.EnterCPDActivityPage.SupportingDocumentsTabLbl).Count > 1)
            {
                ClickAndWait(IWillBesendingDocumentsChk);
                ClickAndWait(SupportingDocumentsTabSubmitBtn);
                ClickAndWait(CloseSecondInstanceBtn);

                requiresValidation = true;
            }
            else
            {
                // Depending on if we have Submitted the activity or Send It To The Holding Area, a different instance of the close button will appear. Because
                // of this, we have to add the condition below
                if (Browser.Exists(Bys.EnterCPDActivityPage.CloseBtn))
                {
                    ClickAndWait(CloseBtn);
                }
                else if (Browser.Exists(Bys.EnterCPDActivityPage.CloseThirdInstanceBtn))
                {
                    ClickAndWait(CloseThirdInstanceBtn);
                }

                requiresValidation = false;
            }

            return requiresValidation;
        }

        /// <summary>
        /// Selects a user-specified item from any of the Section 1/2/3 select elements on the Enter a CPD Activity form, fills in the fields on the
        /// activity form.
        /// </summary>
        /// <param name="activityType"><see cref="Constants.MainportActivityTypes"/></param>
        /// <param name="amountOfHoursOrArticlesForCredits">(Optional) The amount to enter in the How Many Hours/Articles text box. The hours/articles will then be your amount of credits. If the activity type you choose has a static amount of credits, which means they wont have these text boxes, then dont send this parameter.</param>
        /// <param name="accredited">(Optional) "true" to select the Yes radio button for certain activities, "false" to select the No radio button. Dont send this parameter if your activity does not have this radio button</param>
        /// <param name="dateCompleted">(Optional) The date the activity was completed, in the format of 12/1/2017. Dont send this parameter if you dont care about the date, it will then enter the current date for you</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillActivityForm(Constants.MainportActivityTypes activityType, string amountOfHoursOrArticlesForCredits = "", bool accredited = true, string dateCompleted = "")
        {
            Activity NewActivity = new Activity("", "", true, "", "", true);

            switch (activityType)
            {
                case Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf:
                    SelectAndWait(Sec1GroupLearnActSelElem, "Conference");
                    NewActivity = FillForm_EnterACPDActivity_Template1("Conference", accredited, amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec1_JournalClub_IfNotAccreditedCutCreditsInHalf:
                    SelectAndWait(Sec1GroupLearnActSelElem, "Journal Club");
                    NewActivity = FillForm_EnterACPDActivity_Template1("Journal Club", accredited, amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec1_Rounds_IfNotAccreditedCutCreditsInHalf:
                    SelectAndWait(Sec1GroupLearnActSelElem, "Rounds");
                    NewActivity = FillForm_EnterACPDActivity_Template1("Rounds", accredited, amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec1_SmallGroupSession_IfNotAccreditedCutCreditsInHalf:
                    SelectAndWait(Sec1GroupLearnActSelElem, "Small Group Session");
                    NewActivity = FillForm_EnterACPDActivity_Template1("Small Group Session", accredited, amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec2_Fellowship_FixedCredits25:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Fellowship");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Fellowship");
                    break;

                case Constants.MainportActivityTypes.Sec2_FormalCourse_FixedCredits25:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Formal Course");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Formal Course");
                    break;

                case Constants.MainportActivityTypes.Sec2_PLPPersonalLearningProject_CreditsDoubled:
                    SelectAndWait(Sec2SelfLearnActSelElem, "PLP (Personal Learning Project)");
                    NewActivity = FillForm_EnterACPDActivity_Template3("PLP (Personal Learning Project)", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec2_Traineeship_CreditsDoubled:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Traineeship");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Traineeship", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec2_Reading_FixedCredits1:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Reading");
                    NewActivity = FillForm_EnterACPDActivity_Template5("Reading");
                    break;

                case Constants.MainportActivityTypes.Sec2_BulkJournalReadingwithTranscript_ValidationRequired:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Bulk Journal Reading with Transcript");
                    NewActivity = FillForm_EnterACPDActivity_Template6("Bulk Journal Reading with Transcript", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec2_BulkOnlineReadingScanningwithTranscript_ValidationRequired:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Bulk Online Reading / Scanning with Transcript");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Bulk Online Reading / Scanning with Transcript", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec2_PodcastAudio_FixedCredits0_5:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Podcast, Audio");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Podcast, Audio");
                    break;

                case Constants.MainportActivityTypes.Sec2_Video_FixedCredits0_5:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Video");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Video");
                    break;

                case Constants.MainportActivityTypes.Sec2_InternetSearching_FixedCredits0_5:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Internet Searching");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Internet Searching");
                    break;

                case Constants.MainportActivityTypes.Sec2_POEMS_FixedCredits0_25:
                    SelectAndWait(Sec2SelfLearnActSelElem, "POEMS");
                    NewActivity = FillForm_EnterACPDActivity_Template2("POEMS");
                    break;

                case Constants.MainportActivityTypes.Sec2_PosterViewing_FixedCredits0_5:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Poster Viewing");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Poster Viewing");
                    break;

                case Constants.MainportActivityTypes.Sec2_ClinicalPracticeGuidelineDevelopment_FixedCredits20:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Clinical Practice Guideline Development");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Clinical Practice Guideline Development");
                    break;

                case Constants.MainportActivityTypes.Sec2_CurriculumDevelopment_FixedCredits15:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Curriculum Development");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Curriculum Development");
                    break;

                case Constants.MainportActivityTypes.Sec2_ExaminationDevelopment_FixedCredits15:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Examination Development");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Examination Development");
                    break;

                case Constants.MainportActivityTypes.Sec2_OtherSystemsLearningActivity_FixedCredits15:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Other Systems Learning Activity");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Other Systems Learning Activity");
                    break;

                case Constants.MainportActivityTypes.Sec2_PeerReview_FixedCredits15:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Peer Review");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Peer Review");
                    break;

                case Constants.MainportActivityTypes.Sec2_QualityCarePatientSafetyCommittee_FixedCredits15:
                    SelectAndWait(Sec2SelfLearnActSelElem, "Quality Care / Patient Safety Committee");
                    NewActivity = FillForm_EnterACPDActivity_Template2("Quality Care / Patient Safety Committee");
                    break;

                case Constants.MainportActivityTypes.Sec3_PracticeAssessment_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Practice Assessment");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Practice Assessment", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_AccreditedSelfAssessmentPrograms_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Accredited Self-Assessment Programs");
                    NewActivity = FillForm_EnterACPDActivity_Template7("Accredited Self-Assessment Programs", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_AccreditedSimulationActivities_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Accredited Simulation Activities");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Accredited Simulation Activities", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_MultisourceFeedback_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Multi-source Feedback");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Multi-source Feedback", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_DirectObservation_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Direct Observation");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Direct Observation", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_ChartAuditandFeedback_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Chart Audit and Feedback");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Chart Audit and Feedback", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_FeedbackonTeaching_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Feedback on Teaching");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Feedback on Teaching", amountOfHoursOrArticlesForCredits);
                    break;

                case Constants.MainportActivityTypes.Sec3_AnnualPerformanceReview_CreditsTripled:
                    SelectAndWait(Sec3AssessActSelElem, "Annual Performance Review");
                    NewActivity = FillForm_EnterACPDActivity_Template4("Annual Performance Review", amountOfHoursOrArticlesForCredits);
                    break;
            }

            // The following elements are common across all templates
            string dateToEnter = null;
            if (string.IsNullOrEmpty(dateCompleted))
            {
                dateToEnter = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                dateToEnter = dateCompleted;
            }
            NewActivity.DateCompleted = dateToEnter;

            WhatDateTxt.SendKeys(dateToEnter);

            WhatDidYouLearnTxt.SendKeys(DataUtils.GetRandomSentence(5));

            return NewActivity;
        }

        /// <summary>
        /// Note this is not setup yet to change the activity type, and not setup for any other activity type other than Conference. Will have to implement that 
        /// at a later date if deemed necessary for more test coverage, but I dont think editing activities is a priority, or even a need at all to test, versus
        /// how much time it will take to automate. Note that this is a bare bones implementation right now, and you may find it doesnt work to return you the
        /// correct Activity details in your returned object. We can revisit this method at a later date if deemed necessary to add all of this
        /// </summary>
        /// <param name="activityType"><see cref="Constants.MainportActivityTypes"/></param>
        /// <param name="amountOfHoursOrArticlesForCredits">(Optional) The amount to enter in the How Many Hours/Articles text box. The hours/articles will then be your amount of credits. If the activity type you choose has a static amount of credits, which means they wont have these text boxes, then dont send this parameter.</param>
        /// <param name="accredited">(Optional) "true" to select the Yes radio button for certain activities, "false" to select the No radio button. Dont send this parameter if your activity does not have this radio button</param>
        /// <param name="dateCompleted">(Optional) The date the activity was completed, in the format of 12/1/2017. Dont send this parameter if you dont care about the date, it will then enter the current date for you</param>
        /// <returns></returns>
        public Activity EditActivity(Constants.MainportActivityTypes activityType, string amountOfHoursOrArticlesForCredits = "", bool accredited = true, string dateCompleted = "")
        {
            string activityName = "";

            Activity EditedActivity = new Activity("", "", true, "", "", true);

            switch (activityType)
            {
                case Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf:
                    // Is activity accredited radio buttons
                    if (accredited)
                    {
                        IsActivityAccrYesRdo.Click();
                    }
                    else
                    {
                        IsActivityAccrNoRdo.Click();
                    }
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

                    // How many hours text box
                    HowManyHoursTxt.Clear();
                    HowManyHoursTxt.SendKeys(amountOfHoursOrArticlesForCredits);

                    // Name The Group Activity text box
                    activityName = string.Format("Dt{0}_Tm{1}_{2}",
                        DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture),
                        DateTime.Now.ToString("HHmmss", CultureInfo.InvariantCulture),
                        DataUtils.GetRandomString(3));
                    NameTheGroupActivityTxt.Clear();
                    NameTheGroupActivityTxt.SendKeys(activityName);
                    break;
            }

            // The following elements are common across all templates
            string dateToEnter = null;
            if (string.IsNullOrEmpty(dateCompleted))
            {
                dateToEnter = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                dateToEnter = dateCompleted;
            }
            EditedActivity.DateCompleted = dateToEnter;

            WhatDateTxt.Clear();
            WhatDateTxt.SendKeys(dateToEnter);

            WhatDidYouLearnTxt.SendKeys(DataUtils.GetRandomSentence(5));

            // Proceed to the next tab
            ClickAndWait(ContinueBtn);
            ClickAndWait(OptionalTabSubmitBtn);

            // Whe we edit activities, after we click the Close button, the View Activities MOC window is underneath, so for now, we are just leaving this
            // ambiguity wait criteria right here and not putting into ClickAndWait, which would add confusion to the Close button for ClickAndWait thats
            // already there when we Add an activity. Maybe we can revisit this in the future
            CloseBtn.Click();
            Thread.Sleep(5000);

            // For some activities, the total number of credits might double, triple or get cut in half. If an activity has that rule,
            // we will handle that here see Constants.MainportActivityTypes for more info on these rules
            switch (activityType)
            {
                case Constants.MainportActivityTypes.Sec1_Conference_IfNotAccreditedCutCreditsInHalf:
                case Constants.MainportActivityTypes.Sec1_JournalClub_IfNotAccreditedCutCreditsInHalf:
                case Constants.MainportActivityTypes.Sec1_Rounds_IfNotAccreditedCutCreditsInHalf:
                case Constants.MainportActivityTypes.Sec1_SmallGroupSession_IfNotAccreditedCutCreditsInHalf:
                    if (!accredited)
                    {
                        EditedActivity.Credits = (Int32.Parse(amountOfHoursOrArticlesForCredits) / 2).ToString();
                    }
                    break;
            }

            EditedActivity.ActivityType = "Conference";
            EditedActivity.ActivityName = activityName;
            EditedActivity.IsIsActivityAccredited = accredited;

            return EditedActivity;
        }

        #region activity form templates

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: Conference, Journal Club, Rounds, Small Group Session
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <param name="accredited">True to click the Yes radio button, false to click the No radio button for "Is activity accrediated" section</param>
        /// <param name="amountOfHoursOrArticlesForCredits">The amount of hours you spent, or the number of articles you read for the activity. This will then represent the amount of credits the activity will receive</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillForm_EnterACPDActivity_Template1(string activityType, bool accredited, string amountOfHoursOrArticlesForCredits)
        {
            // Is activity accredited radio buttons
            if (accredited)
            {
                IsActivityAccrYesRdo.Click();
            }
            else
            {
                IsActivityAccrNoRdo.Click();
            }
            this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

            // How many hours text box
            HowManyHoursTxt.SendKeys(amountOfHoursOrArticlesForCredits);

            // Name The Group Activity text box
            string activityName = string.Format("Dt{0}_Tm{1}_{2}",
                DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture),
                DateTime.Now.ToString("HHmmss", CultureInfo.InvariantCulture),
                DataUtils.GetRandomString(3));
            NameTheGroupActivityTxt.SendKeys(activityName);

            return new Activity(activityType, activityName, accredited, amountOfHoursOrArticlesForCredits);
        }

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: Fellowship, Formal Course, Podcast Audio, Video, Internet Searching, POEMS, Poster Viewing, Clinical Practice 
        /// Guideline Development, Curriculum Development, Examination Development, Peer Review, Other Systems Learning Activity, 
        /// Quality Care/Patient Safety Committee
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillForm_EnterACPDActivity_Template2(string activityType)
        {
            // Describe The Question text box
            string activityName = string.Format("Dt{0}_Tm{1}_{2}",
                DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture),
                DateTime.Now.ToString("HHmmss", CultureInfo.InvariantCulture),
                DataUtils.GetRandomString(3));
            DescribeTheQuestionTxt.SendKeys(activityName);

            return new Activity(activityType, activityName, true, CreditsForActivityValueLbl.Text);
        }

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: PLP
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <param name="amountOfHoursOrArticlesForCredits">The amount of hours you spent, or the number of articles you read for the activity. This will then represent the amount of credits the activity will receive</param>
        /// <returns><see cref="Activity"/></returns>
        /// <returns></returns>
        public Activity FillForm_EnterACPDActivity_Template3(string activityType, string amountOfHoursOrArticlesForCredits)
        {
            // Please Select The Type Of Project It Was dropdown
            PleaseSelectTheTypeOfProjectItWasSelElem.SelectByText("Development of research activities");
            this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

            // How many hours text box
            HowManyHoursTxt.SendKeys(amountOfHoursOrArticlesForCredits);

            // Describe The Question text box
            string activityName = string.Format("Dt{0}_Tm{1}_{2}",
                DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture),
                DateTime.Now.ToString("HHmmss", CultureInfo.InvariantCulture),
                DataUtils.GetRandomString(3));
            DescribeTheQuestionTxt.SendKeys(activityName);

            return new Activity(activityType, activityName, true, amountOfHoursOrArticlesForCredits);
        }

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: Traineeship, Bulk Online Reading/Scanning With Transcript, Practice Assessment, Accredited Simulation 
        /// Activities, Multi - source Feedback, Direct Observation, Chart Audit and Feedback, Feedback on Teaching, Annual Performance Review
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <param name="amountOfHoursOrArticlesForCredits">The amount of hours you spent, or the number of articles you read for the activity. This will then represent the amount of credits the activity will receive</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillForm_EnterACPDActivity_Template4(string activityType, string amountOfHoursOrArticlesForCredits)
        {
            // How many hours text box
            HowManyHoursTxt.SendKeys(amountOfHoursOrArticlesForCredits);

            // Describe The Question text box
            string activityName = string.Format("Dt{0}_Tm{1}_{2}",
                DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture),
                DateTime.Now.ToString("HHmmss", CultureInfo.InvariantCulture),
                DataUtils.GetRandomString(3));
            DescribeTheQuestionTxt.SendKeys(activityName);

            return new Activity(activityType, activityName, true, amountOfHoursOrArticlesForCredits);
        }

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: Reading
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillForm_EnterACPDActivity_Template5(string activityType)
        {
            // Please Select The Type Of Reading dropdown
            PleaseSelectTheTypeOfReadingSelElem.SelectByText("Reading a journal article");
            this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

            // Describe The Question text box
            string activityName = string.Format("Dt{0}_Tm{1}_{2}",
                DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture),
                DateTime.Now.ToString("HHmmss", CultureInfo.InvariantCulture),
                DataUtils.GetRandomString(3));
            DescribeTheQuestionTxt.SendKeys(activityName);

            return new Activity(activityType, activityName, true, CreditsForActivityValueLbl.Text);
        }

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: Bulk Journal Reading With Transcript
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <param name="amountOfHoursOrArticlesForCredits">The amount of hours you spent, or the number of articles you read for the activity. This will then represent the amount of credits the activity will receive</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillForm_EnterACPDActivity_Template6(string activityType, string amountOfHoursOrArticlesForCredits)
        {
            // Please Select The Type Of Project It Was dropdown
            PleaseSelectTheTypeOfReadingActivitySelElem.SelectByText("Bulk reading with attached third party report");
            this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

            // Total Number Of Articles text box
            TotalNumberOfArticlesTxt.SendKeys(amountOfHoursOrArticlesForCredits);

            return new Activity(activityType, PleaseSelectTheTypeOfReadingActivitySelElem.SelectedOption.Text, true, amountOfHoursOrArticlesForCredits);
        }

        /// <summary>
        /// When the Enter a CPD Activity form is open, this will fill the form with randomized and/or user-specified data. This specific method can be used
        /// for the following activity types: Accredited Self-Assessment Programs
        /// </summary>
        /// <param name="activityType">The name of the activity type as it exists in the "Section 1/2/3 dropdowns</param>
        /// <param name="amountOfHoursOrArticlesForCredits">The amount of hours you spent, or the number of articles you read for the activity. This will then represent the amount of credits the activity will receive</param>
        /// <returns><see cref="Activity"/></returns>
        public Activity FillForm_EnterACPDActivity_Template7(string activityType, string amountOfHoursOrArticlesForCredits)
        {
            // Select The Relevant Domain dropdown
            SelectTheRelevantDomainSelElem.SelectByText("Dermatology");
            this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

            // SAP Name dropdown
            SAPNameSelElem.SelectByText("MOCmd - Volume 2: dec.2008 - dec. 2011");
            this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);

            // How many hours text box
            HowManyHoursTxt.SendKeys(amountOfHoursOrArticlesForCredits);

            return new Activity(activityType, SAPNameSelElem.SelectedOption.Text, true, amountOfHoursOrArticlesForCredits);
        }

        #endregion activity form templates

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="activityGroupName">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string activityGroupName)
        {
            if (Browser.Exists(Bys.EnterCPDActivityPage.Sec1GroupLearnActSelElem))
            {
                if (selectElement.AllSelectedOptions[0].GetAttribute("outerHTML") == Sec1GroupLearnActSelElem.AllSelectedOptions[0].GetAttribute("outerHTML"))
                {
                    selectElement.SelectByText(activityGroupName);
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgVisible);
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterCPDActivityPage.Sec2SelfLearnActSelElem))
            {
                if (selectElement.AllSelectedOptions[0].GetAttribute("outerHTML") == Sec2SelfLearnActSelElem.AllSelectedOptions[0].GetAttribute("outerHTML"))
                {

                    selectElement.SelectByText(activityGroupName);
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgVisible);
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);
                    Thread.Sleep(0300);
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterCPDActivityPage.Sec3AssessActSelElem))
            {
                if (selectElement.AllSelectedOptions[0].GetAttribute("outerHTML") == Sec3AssessActSelElem.AllSelectedOptions[0].GetAttribute("outerHTML"))
                {

                    selectElement.SelectByText(activityGroupName);
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgVisible);
                    this.WaitUntil(Criteria.EnterCPDActivityPage.LoadingImgNotVisible);
                    Thread.Sleep(0300);
                    return null;
                }
            }

            else
            {
                throw new Exception("No select element was found with your passed parameter");
            }

            return null;
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or an element to be visible or enabled
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.EnterCPDActivityPage.ContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem, ScrollBar);
                    // Note, sometimes in IE, clicking the Continue button does not do anything, including when I click it manually
                    // while in debug mode. This might be a legit application bug. Monitor going forward
                    Browser.WaitForElement(Bys.EnterCPDActivityPage.OptionalTabSubmitBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterCPDActivityPage.SendToHoldingAreaBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SendToHoldingAreaBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, buttonOrLinkElem, ScrollBar);
                    // Note, sometimes in IE, clicking this button does not do anything, including when I click it manually
                    // while in debug mode. This might be a legit application bug. Monitor going forward
                    Browser.WaitForElement(Bys.EnterCPDActivityPage.CloseThirdInstanceBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.OptionalTabSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == OptionalTabSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    // Mainport user/activity/cycle/date Rules:
                    // Depending on the Cycle type of the user, activity type (bulk journal), activity completed date (if date of activity submitted was
                    // a year prior, etc., they need proof of documents) and the user being tested (voluntary vs mandatory), different elements will appear after 
                    // Submit is clicked. We will wait until any of these elements appear. For example, if we choose the Conference activity, then after we click this
                    // button, it will go to the "Your activity has been saved and processed" window, and then we can wait for the close button. But if the activity 
                    // is "Bulk Journal Reading with Transcript", it could go to another tab (supporting documents tab) which tells the user to upload documents or 
                    // check a check box before proceeding. Note that voluntary users dont ever need to upload documents, so they wont see this window. More rules 
                    // are in place for this, but those are just a few. Note that this then triggers credit validation, meaning to receive credits, we have to 
                    // login to Lifetime Support and click the Validate button for this activity for this user
                    this.WaitUntilAny(Criteria.EnterCPDActivityPage.CloseBtnVisible, Criteria.EnterCPDActivityPage.SupportingDocumentsTabLblVisible);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.SupportingDocumentsTabSubmitBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SupportingDocumentsTabSubmitBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.EnterCPDActivityPage.CloseSecondInstanceBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.CloseBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CloseBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.MyDashboardPage.EnterACPDActivityBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.IWillBesendingDocumentsChk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == IWillBesendingDocumentsChk.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    this.WaitUntilAny(Criteria.EnterCPDActivityPage.SupportingDocumentsTabSubmitBtnVisibleAndEnabled);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.CloseSecondInstanceBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CloseSecondInstanceBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.MyDashboardPage.EnterACPDActivityBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.CloseThirdInstanceBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CloseThirdInstanceBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.MyDashboardPage.EnterACPDActivityBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            else if (Browser.Exists(Bys.EnterCPDActivityPage.CloseFourthInstanceBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CloseFourthInstanceBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.MyDashboardPage.EnterACPDActivityBtn, ElementCriteria.IsVisible);
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



        #endregion wrappers


    }

    #region additional classes

    public class Activity
    {
        public string ActivityType { get; set; }
        public string ActivityName { get; set; }
        public bool IsIsActivityAccredited { get; set; }
        public string Credits { get; set; }
        public string DateCompleted { get; set; }
        public bool RequiresValidation { get; set; }


        public Activity(string activityType, string activityName, bool isIsActivityAccredited, string credits, string dateCompleted = "", bool requiresValidation = true)
        {
            ActivityType = activityType;
            ActivityName = activityName;
            IsIsActivityAccredited = isIsActivityAccredited;
            Credits = credits;
            RequiresValidation = requiresValidation;
            DateCompleted = dateCompleted;
        }



        #endregion additional classes

    }
}