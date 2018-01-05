using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class DiplomaDirectorPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met after logging in.
        /// </summary>
        public readonly ICriteria<DiplomaDirectorPage> MainFrameVisibleAndEnabled = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<DiplomaDirectorPage> LoadIconDisappeared = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<DiplomaDirectorPage> PortfoliosUnderReviewTblBodyRowVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRow, ElementCriteria.IsVisible);
        }, "Portfolios Under Review table, first row visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkSelPortAchFormSubmitBtnVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkSelPortAchFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark Selected Portfolios As Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkSelPortAchFormSubmitBtnNotVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkSelPortAchFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark Selected Portfolios As Achieved form, Submit button not visible");

        public readonly ICriteria<DiplomaDirectorPage> PortfoliosUnderReviewTblBodyRowCheckBoxVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.PortfoliosUnderReviewTblBodyRowChk, ElementCriteria.IsVisible);
        }, "Portfolios Under Review table, first row check box visible");

        public readonly ICriteria<DiplomaDirectorPage> MyProgramSnapshotTblFirstRowPrgLnkVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MyProgramSnapshotTblFirstRowPrgLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled, ElementCriteria.HasText);
        }, "My Program Snapshot table first row, program name link visible");

        public readonly ICriteria<DiplomaDirectorPage> MilestonesTblEnabled = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MilestonesTbl, ElementCriteria.IsVisible);
        }, "Milestones table visible");

        public readonly ICriteria<DiplomaDirectorPage> RequestAdditionalInfoFormSubmitBtnVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.RequestAdditionalInfoFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Request Addition Information form Submit button visible");


        public readonly ICriteria<DiplomaDirectorPage> ReviewStageValueLblVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.ReviewStageValueLbl, ElementCriteria.IsVisible);
        }, "Review Stage value label visible");

        public readonly ICriteria<DiplomaDirectorPage> MilestonesTblFirstRowEnabled = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MilestonesTblFirstRow, ElementCriteria.IsVisible);
        }, "Milestones table first row visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkAsAchievedBtnVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkAsAchievedBtn, ElementCriteria.IsVisible);
        }, "Mark as Achieved button visible");

        public readonly ICriteria<DiplomaDirectorPage> BackToDashboardBtnVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.BackToDashboardBtn, ElementCriteria.IsVisible);
        }, "Back To Portfolio button visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkAsAchievedFormSubmitBtnVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkAsAchievedFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark as Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkAsAchievedFormSubmitBtnNotVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkAsAchievedFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark as Achieved form, Submit button not visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkAsNotAchievedFormSubmitBtnVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkAsNotAchievedFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark as not Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaDirectorPage> MarkAsNotAchievedFormSubmitBtnNotVisible = new Criteria<DiplomaDirectorPage>(p =>
        {
            return p.Exists(Bys.DiplomaDirectorPage.MarkAsNotAchievedFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark as not Achieved form, Submit button not visible");

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="DiplomaDirectorPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<DiplomaDirectorPage> PageReady;
        public DiplomaDirectorPageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
