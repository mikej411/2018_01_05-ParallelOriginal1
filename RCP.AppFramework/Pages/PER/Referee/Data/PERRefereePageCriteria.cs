using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class PERRefereePageCriteria
{
        /// <summary>
        /// This is the criteria that needs to be met for after we click on the PER-AFC tab as a Referee user.
        /// </summary>
        public readonly ICriteria<PERRefereePage> MainFrameVisibleAndEnabled = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<PERRefereePage> LoadIconDisappeared = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<PERRefereePage> PendingSurveysTblVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.PendingSurveysTbl, ElementCriteria.IsVisible);
        }, "Referee tab, Trainee table visible");

        public readonly ICriteria<PERRefereePage> PendingSurveysTblFirstRowVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.PendingSurveysTblFirstRow, ElementCriteria.IsVisible);
        }, "Referee tab, Trainee table, first row visible");

        public readonly ICriteria<PERRefereePage> TraineeSurveyFormProfessTxtVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.TraineeSurveyFormProfessTxt, ElementCriteria.IsVisible);
        }, "Trainee Survey form, Profession text visible");

        public readonly ICriteria<PERRefereePage> TraineeSurveyFormProfessTxtNotVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.TraineeSurveyFormProfessTxt, ElementCriteria.IsNotVisible);
        }, "Trainee Survey form, Profession text not visible");

        public readonly ICriteria<PERRefereePage> TraineeSurveyFormFrameVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.TraineeSurveyFormFrame, ElementCriteria.IsVisible);
        }, "Trainee Survey form visible");

        public readonly ICriteria<PERRefereePage> TraineeSurveyFormLoadingIconNotVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.TraineeSurveyFormLoadingIcon, ElementCriteria.IsNotVisible);
        }, "Trainee Survey form Loading icon not visible");

        public readonly ICriteria<PERRefereePage> TraineeSurveyFormLoadingIconVisible = new Criteria<PERRefereePage>(p =>
        {
            return p.Exists(Bys.PERRefereePage.TraineeSurveyFormLoadingIcon, ElementCriteria.IsVisible);
        }, "Trainee Survey form Loading icon not visible");

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="PERRefereePage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<PERRefereePage> PageReady;
        public PERRefereePageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
