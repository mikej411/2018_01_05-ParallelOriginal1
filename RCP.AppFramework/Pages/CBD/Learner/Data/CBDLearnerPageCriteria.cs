using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login to the
    /// Learner role page, we need to wait for the circular load icon to disappear and for the table below the tabs to appear.
    /// </summary>
    public class CBDLearnerPageCriteria
    {
        /// <summary>
        /// This is the criteria that should be used for the landing page (Page after you login). This means that the load icons have completed, 
        /// and then any buttons or tables on this page, such as the table underneath the Program Learning Plan table. This does not include popups. 
        /// <see cref="PageReadyWithProgramLearningTabReady"/> at the bottom of this class. That is where these specific criteria are combined
        /// </summary>
        public readonly ICriteria<CBDLearnerPage> LoadElementClassAttributeSetToHide = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon class attribute value set to \"ng-hide\"");


        public readonly ICriteria<CBDLearnerPage> LoadElementDisappeared = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.RCPPage.SplashPage, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        public readonly ICriteria<CBDLearnerPage> EPAIMTableVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.EPAIMTbl, ElementCriteria.IsVisible);

        }, "EPA/IM Table visible");

        // There is currently a bug where this chart doesnt appear sometimes, so we will leave this out of the PageReadyWithProgramLearningTabReady property 
        // at the bottom of this class
        public readonly ICriteria<CBDLearnerPage> EPAChartVisibleAndEnabledAndHasText = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.EPAObservCntChrt, ElementCriteria.IsEnabled, ElementCriteria.IsVisible, ElementCriteria.HasText);

        }, "EPA Chart enabled");

        /// <summary>
        /// The following criteria does not need to be met on the page after login. This below criteria can be used for other elements or instances
        /// </summary>
        public readonly ICriteria<CBDLearnerPage> EPAIMTableEnabled = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.EPAIMTbl, ElementCriteria.IsEnabled);

        }, "EPA/IM Table enabled");

        public readonly ICriteria<CBDLearnerPage> BackButtonIsEnabledAndVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.BackBtn, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Back button visible and enabled");

        public readonly ICriteria<CBDLearnerPage> ProgramLearningPlanTabEnabledAndVisible = new Criteria<CBDLearnerPage>(p =>
        {
        return p.Exists(Bys.CBDLearnerPage.PrgLrnPlanTab, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Program Learning Plan tab enabled and visible");

        public readonly ICriteria<CBDLearnerPage> ReflectionsTabEnabledAndVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.ReflectionsTab, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Reflections tab enabled and visible");

        public readonly ICriteria<CBDLearnerPage> NarrativesTabEnabledAndVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.NarrativesTab, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Narratives tab enabled and visible");

        public readonly ICriteria<CBDLearnerPage> SuppDocsTabEnabledAndVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.SupportingDocsTab, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Supporting Documents tab enabled and visible");

        public readonly ICriteria<CBDLearnerPage> AddReflectionsButtonEnabledAndVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.AddReflectBtn, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Add Reflections button enabled and visible");

        public readonly ICriteria<CBDLearnerPage> ReflectionsTableEnabled = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.AddReflectBtn, ElementCriteria.IsEnabled);

        }, "Reflections table enabled");

        public readonly ICriteria<CBDLearnerPage> AssessmentDetailsTableEnabled = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.AssessmentDetailsTbl, ElementCriteria.IsEnabled);

        }, "Assessment Details table enabled");

        // This can be used to wait on any table that has the "table" tag in the HTML
        public readonly ICriteria<CBDLearnerPage> GenericTableEnabled = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.GenericTable, ElementCriteria.IsEnabled);

        }, "Generic table enabled");

        public readonly ICriteria<CBDLearnerPage> RequestObsFormRdoBtnTbodyVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.RequestObsFormRdoBtnTbody, ElementCriteria.IsVisible);

        }, "Request Observation form, radio button table visible");

        public readonly ICriteria<CBDLearnerPage> RequestObsFormFirstRdoVisible = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.CBDLearnerPage.RequestObsFormFirstRdo, ElementCriteria.IsVisible);

        }, "Request Observation form, first radio button visible");

        public readonly ICriteria<CBDLearnerPage> CBDTabVisibleAndEnabled = new Criteria<CBDLearnerPage>(p =>
        {
            return p.Exists(Bys.RCPPage.CBDTab, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "CBD tab visible and enabled");

        /// <summary>
        /// You can combine criteria here. Note that the criteria that should be used for the PageReadyWithProgramLearningTabReady property are only
        /// elements that are contained within the landing page (Page after you login). This means that the load icons have completed, and then
        /// any buttons or tables on this page, such as the table underneath the Program Learning Plan table. This does not include popups. 
        /// We use this PageReadyWithProgramLearningTabReady property inside <see cref="CBDLearnerPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<CBDLearnerPage> PageReadyWithProgramLearningTabReady;
        public readonly ICriteria<CBDLearnerPage> PageReadyWithReflectionsTabReady;
        public readonly ICriteria<CBDLearnerPage> LoadElementDoneLoading;
        public readonly ICriteria<CBDLearnerPage> PageReadyWithAssessmentDetailsTabReady;    
        public CBDLearnerPageCriteria()
        {
            LoadElementDoneLoading = LoadElementClassAttributeSetToHide.AND(LoadElementDisappeared);
            PageReadyWithProgramLearningTabReady = LoadElementDoneLoading.AND(EPAIMTableEnabled).AND(EPAIMTableVisible);//.AND(EPAChartVisibleAndEnabledAndHasText);            
            PageReadyWithReflectionsTabReady = LoadElementDoneLoading.AND(AddReflectionsButtonEnabledAndVisible).AND(ReflectionsTableEnabled);
            PageReadyWithAssessmentDetailsTabReady = LoadElementDoneLoading.AND(AssessmentDetailsTableEnabled);
        }
    }
}
