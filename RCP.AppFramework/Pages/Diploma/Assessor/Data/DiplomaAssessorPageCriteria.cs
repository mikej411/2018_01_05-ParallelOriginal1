using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class DiplomaAssessorPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met after logging in.
        /// </summary>
        public readonly ICriteria<DiplomaAssessorPage> MainFrameVisibleAndEnabled = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<DiplomaAssessorPage> LoadIconDisappeared = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<DiplomaAssessorPage> PortfolioAssignmentsTblVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.PortfolioAssignmentsTbl, ElementCriteria.IsVisible);
        }, "Portfolio Assignments table visible");

        public readonly ICriteria<DiplomaAssessorPage> PortfolioAssignmentsTblFirstRowVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.PortfolioAssignmentsTblFirstRow, ElementCriteria.IsVisible);
        }, "Portfolio Assignments table first row visible");

        public readonly ICriteria<DiplomaAssessorPage> MilestonesTblEnabled = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MilestonesTbl, ElementCriteria.IsVisible);
        }, "Milestones table visible");

        public readonly ICriteria<DiplomaAssessorPage> RequestAdditionalInfoFormSubmitBtnVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.RequestAdditionalInfoFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Request Addition Information form Submit button visible");


        public readonly ICriteria<DiplomaAssessorPage> ReviewStageValueLblVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.ReviewStageValueLbl, ElementCriteria.IsVisible);
        }, "Review Stage value label visible");
        
        public readonly ICriteria<DiplomaAssessorPage> MilestonesTblFirstRowEnabled = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MilestonesTblFirstRow, ElementCriteria.IsVisible);
        }, "Milestones table first row visible");

        public readonly ICriteria<DiplomaAssessorPage> MarkAsAchievedBtnVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MarkAsAchievedBtn, ElementCriteria.IsVisible);
        }, "Mark as Achieved button visible");

        public readonly ICriteria<DiplomaAssessorPage> BackToDashboardBtnVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.BackToDashboardBtn, ElementCriteria.IsVisible);
        }, "Back To Portfolio button visible");

        public readonly ICriteria<DiplomaAssessorPage> MarkAsAchievedFormSubmitBtnVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MarkAsAchievedFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark as Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaAssessorPage> MarkAsAchievedFormSubmitBtnNotVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MarkAsAchievedFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark as Achieved form, Submit button not visible");

        public readonly ICriteria<DiplomaAssessorPage> MarkAsNotAchievedFormSubmitBtnVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MarkAsNotAchievedFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark as not Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaAssessorPage> MarkAsNotAchievedFormSubmitBtnNotVisible = new Criteria<DiplomaAssessorPage>(p =>
        {
            return p.Exists(Bys.DiplomaAssessorPage.MarkAsNotAchievedFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark as not Achieved form, Submit button not visible");


        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="DiplomaAssessorPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<DiplomaAssessorPage> PageReady;
        public DiplomaAssessorPageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
