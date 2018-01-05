using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class PERCredentialStaffPageCriteria
{
        /// <summary>
        /// This is the criteria that needs to be met for after we click on the PER-AFC tab as a CredentialUnit user.
        /// </summary>
        public readonly ICriteria<PERCredentialStaffPage> MainFrameVisibleAndEnabled = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<PERCredentialStaffPage> LoadIconDisappeared = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<PERCredentialStaffPage> RefereesTabTraineeTblVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.RefereesTabTraineeTbl, ElementCriteria.IsVisible);
        }, "Referee tab, Trainee table visible");

        public readonly ICriteria<PERCredentialStaffPage> RefereesTabTraineeTblFirstRowVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.RefereesTabTraineeTblFirstRow, ElementCriteria.IsVisible);
        }, "Referee tab, Trainee table, first row visible");

        public readonly ICriteria<PERCredentialStaffPage> AssessorTabTraineeTblVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssessorTabTraineeTbl, ElementCriteria.IsVisible);
        }, "Assessors tab, Trainee table visible");

        public readonly ICriteria<PERCredentialStaffPage> AssessorTabTraineeTblFirstRowVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssessorTabTraineeTblFirstRow, ElementCriteria.IsVisible);
        }, "Assessors tab, Trainee table, first row visible");

        public readonly ICriteria<PERCredentialStaffPage> AssignReferee2PERRefsFormFirstRefSelElemNotVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormFirstRefSelElem, ElementCriteria.IsNotVisible);
        }, "Assign Referee form Referee 1 Select Element not visible");

        public readonly ICriteria<PERCredentialStaffPage> AssignAssessor2AssFormFirstAssSelElemNotVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElem, ElementCriteria.IsNotVisible);
        }, "Assign Asssessor form Asssessor 1 Select Element not visible");

        public readonly ICriteria<PERCredentialStaffPage> AssignAssessor3rdAssFormThirdAssSelElemNotVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElem, ElementCriteria.IsNotVisible);
        }, "Assign Asssessor form Asssessor 3 Select Element not visible");

        public readonly ICriteria<PERCredentialStaffPage> BackGroundBackDropNotExists = new Criteria<PERCredentialStaffPage>(p =>
        {
            return !p.Exists(Bys.PERCredentialStaffPage.BackGroundBackDrop);
        }, "Background back drop behind popup not exists");

        public readonly ICriteria<PERCredentialStaffPage> AssignReferee2PERRefsFormSubmitBtnVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Assign Referee First Referee form Submit button visible");

        public readonly ICriteria<PERCredentialStaffPage> AssignReferee2PERRefsFormFirstRefSelElemHasItemsIsVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignReferee2PERRefsFormFirstRefSelElem, ElementCriteria.SelectElementHasItems, ElementCriteria.IsVisible);
        }, "Assign Referee First Referee Select Element has items and visible");

        public readonly ICriteria<PERCredentialStaffPage> AssignAssessor2AssFormFirstAssSelElemHasItemsIsVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignAssessor2AssFormFirstAssSelElem, ElementCriteria.SelectElementHasItems, ElementCriteria.IsVisible);
        }, "Assign Assessor First Assessor Select Element has items and visible");

        public readonly ICriteria<PERCredentialStaffPage> AssignAssessor3rdAssFormThirdAssSelElemHasItemsIsVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.AssignAssessor3rdAssFormThirdAssSelElem, ElementCriteria.SelectElementHasItems, ElementCriteria.IsVisible);
        }, "Assign Assessor Third Assessor Select Element has items and visible");

        public readonly ICriteria<PERCredentialStaffPage> FinalReviewFormAchievedRdoVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.FinalReviewFormAchievedRdo, ElementCriteria.IsVisible);
        }, "Final Review form Achieved radio button visible");

        public readonly ICriteria<PERCredentialStaffPage> FinalReviewFormAchievedRdoNotVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.FinalReviewFormAchievedRdo, ElementCriteria.IsNotVisible);
        }, "Final Review form Achieved radio button not visible");

        public readonly ICriteria<PERCredentialStaffPage> MyProgramSnapshotTblFirstRowPrgLnkVisible = new Criteria<PERCredentialStaffPage>(p =>
        {
            return p.Exists(Bys.PERCredentialStaffPage.MyProgramSnapshotTblFirstRowPrgLnk, ElementCriteria.IsVisible);
        }, "My Program Snapshot table first row, program name link is visible");
        


        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="PERCredentialStaffPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<PERCredentialStaffPage> PageReady;
        public PERCredentialStaffPageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
