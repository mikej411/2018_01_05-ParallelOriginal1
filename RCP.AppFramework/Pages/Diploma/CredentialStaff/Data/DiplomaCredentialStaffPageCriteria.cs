using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class DiplomaCredentialStaffPageCriteria
{
        /// <summary>
        /// This is the criteria that needs to be met for after we click on the Diploma-AFC tab as a CredentialUnit user.
        /// </summary>
        public readonly ICriteria<DiplomaCredentialStaffPage> MainFrameVisibleAndEnabled = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<DiplomaCredentialStaffPage> LoadIconDisappeared = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<DiplomaCredentialStaffPage> PortfoliosUnderReviewTblFirstRowVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.PortfoliosUnderReviewTblFirstRow, ElementCriteria.IsVisible);
        }, "Portfolios Under Review table, first row visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> MarkPortfolioAsAchievedFormSubmitBtnVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.MarkPortfolioAsAchievedFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark Portfolio As Achieved form Submit Button visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> MarkPortfolioAsAchievedFormSubmitBtnNotVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.MarkPortfolioAsAchievedFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark Portfolio As Achieved form Submit Button not visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> RecordPaymentFormSubmitBtnVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.RecordPaymentFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Record Payment form Submit Button visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> RecordPaymentFormSubmitBtnSubmitBtnNotVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.RecordPaymentFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Record Payment form Submit Button not visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssignAssessor2AssFormSubmitBtnVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Assign 2 Assessors form Submit Button visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssignAssessor2AssFormFirstAssSelElemVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElem, ElementCriteria.IsVisible);
        }, "Assign 2 Assessors form First Assessor select element visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssignAssessor3rdAssFormThirdAssSelElemVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElem, ElementCriteria.IsVisible);
        }, "Assign 2 Assessors form Third Assessor select element visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssignAssessor2AssFormSubmitBtnNotVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor2AssFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Assign 2 Assessors form Submit Button not visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssignAssessor3rdAssFormSubmitBtnVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Assign 3rd Assessor form Submit Button visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssignAssessor3rdAssFormSubmitBtnNotVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssignAssessor3rdAssFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Assign 3rd Assessor form Submit Button not visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssessorTblVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssessorTbl, ElementCriteria.IsVisible);
        }, "Assessors tab, Trainee table visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> AssessorTblFirstRowVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.AssessorTblFirstRow, ElementCriteria.IsVisible);
        }, "Assessors tab, Trainee table, first row visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> BackGroundBackDropNotExists = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return !p.Exists(Bys.DiplomaCredentialStaffPage.BackGroundBackDrop);
        }, "Background back drop behind popup not exists");

        public readonly ICriteria<DiplomaCredentialStaffPage> FinalReviewFormAchievedRdoVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.FinalReviewFormAchievedRdo, ElementCriteria.IsVisible);
        }, "Final Review form Achieved radio button visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> FinalReviewFormAchievedRdoNotVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.FinalReviewFormAchievedRdo, ElementCriteria.IsNotVisible);
        }, "Final Review form Achieved radio button not visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> MyProgramSnapshotTblFirstRowPrgLnkVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled, ElementCriteria.HasText);
        }, "My Program Snapshot table first row, program name link visible");

        public readonly ICriteria<DiplomaCredentialStaffPage> NoPortfoliosLblVisible = new Criteria<DiplomaCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.DiplomaCredentialStaffPage.NoPortfoliosLbl, ElementCriteria.IsVisible);
        }, "No Portfolios To Display label visible");
        
        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="DiplomaCredentialStaffPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<DiplomaCredentialStaffPage> PageReady;
        public DiplomaCredentialStaffPageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
