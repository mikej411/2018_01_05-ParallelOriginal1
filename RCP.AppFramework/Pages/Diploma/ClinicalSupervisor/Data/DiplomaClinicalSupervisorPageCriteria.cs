using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class DiplomaClinicalSupervisorPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met after logging in.
        /// </summary>
        public readonly ICriteria<DiplomaClinicalSupervisorPage> MainFrameVisibleAndEnabled = new Criteria<DiplomaClinicalSupervisorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<DiplomaClinicalSupervisorPage> LoadIconDisappeared = new Criteria<DiplomaClinicalSupervisorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<DiplomaClinicalSupervisorPage> UnderReviewTblBodyRowVisible = new Criteria<DiplomaClinicalSupervisorPage>(p =>
        {
            return p.Exists(Bys.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRow, ElementCriteria.IsVisible);
        }, "Under Review table, first row visible");

        public readonly ICriteria<DiplomaClinicalSupervisorPage> MarkSelMilestonesAchFormSubmitBtnVisible = new Criteria<DiplomaClinicalSupervisorPage>(p =>
        {
            return p.Exists(Bys.DiplomaClinicalSupervisorPage.MarkSelMilestonesAchFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark Selected Milestones As Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaClinicalSupervisorPage> MarkSelMilestonesAchFormSubmitBtnNotVisible = new Criteria<DiplomaClinicalSupervisorPage>(p =>
        {
            return p.Exists(Bys.DiplomaClinicalSupervisorPage.MarkSelMilestonesAchFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark Selected Milestones As Achieved form, Submit button not visible");

        public readonly ICriteria<DiplomaClinicalSupervisorPage> UnderReviewTblBodyRowCheckBoxVisible = new Criteria<DiplomaClinicalSupervisorPage>(p =>
        {
            return p.Exists(Bys.DiplomaClinicalSupervisorPage.UnderReviewTblBodyRowChk, ElementCriteria.IsVisible);
        }, "Under Review table, first row check box visible");

        

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="DiplomaClinicalSupervisorPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<DiplomaClinicalSupervisorPage> PageReady;
        public DiplomaClinicalSupervisorPageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
