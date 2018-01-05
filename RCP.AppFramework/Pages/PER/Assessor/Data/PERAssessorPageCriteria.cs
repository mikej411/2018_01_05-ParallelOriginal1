using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class PERAssessorPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met after logging in.
        /// </summary>
        public readonly ICriteria<PERAssessorPage> MainFrameVisibleAndEnabled = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<PERAssessorPage> LoadIconDisappeared = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<PERAssessorPage> PortfolioAssignmentsTblVisible = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.PortfolioAssignmentsTbl, ElementCriteria.IsVisible);
        }, "Portfolio Assignments table visible");

        public readonly ICriteria<PERAssessorPage> PortfolioAssignmentsTblFirstRowVisible = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.PortfolioAssignmentsTblFirstRow, ElementCriteria.IsVisible);
        }, "Portfolio Assignments table first row visible");

        public readonly ICriteria<PERAssessorPage> MilestonesTblEnabled = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.MilestonesTbl, ElementCriteria.IsVisible);
        }, "Milestones table visible");

        public readonly ICriteria<PERAssessorPage> RequestAdditionalInfoFormSubmitBtnVisible = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.RequestAdditionalInfoFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Request Addition Information form Submit button visible");


        public readonly ICriteria<PERAssessorPage> ReviewStageValueLblVisible = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.ReviewStageValueLbl, ElementCriteria.IsVisible);
        }, "Review Stage value label visible");
        
        public readonly ICriteria<PERAssessorPage> MilestonesTblFirstRowEnabled = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.MilestonesTblFirstRow, ElementCriteria.IsVisible);
        }, "Milestones table first row visible");

        public readonly ICriteria<PERAssessorPage> MarkAsAchievedBtnVisible = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.MarkAsAchievedBtn, ElementCriteria.IsVisible);
        }, "Mark as Achieved button visible");

        public readonly ICriteria<PERAssessorPage> BackToPortfolioBtnVisible = new Criteria<PERAssessorPage>(p =>
        {
            return p.Exists(Bys.PERAssessorPage.BackToPortfolioBtn, ElementCriteria.IsVisible);
        }, "Back To Portfolio button visible");
        
        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="PERAssessorPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<PERAssessorPage> PageReady;
        public PERAssessorPageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
