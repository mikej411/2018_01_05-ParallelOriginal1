using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class DiplomaFacOfMedicinePageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met after logging in.
        /// </summary>
        public readonly ICriteria<DiplomaFacOfMedicinePage> MainFrameVisibleAndEnabled = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<DiplomaFacOfMedicinePage> LoadIconDisappeared = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<DiplomaFacOfMedicinePage> PortfoliosUnderReviewTblBodyRowVisible = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.DiplomaFacOfMedicinePage.PortfoliosUnderReviewTblBodyRow, ElementCriteria.IsVisible);
        }, "Portfolios Under Review table, first row visible");

        public readonly ICriteria<DiplomaFacOfMedicinePage> MarkSelPortAchFormSubmitBtnVisible = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.DiplomaFacOfMedicinePage.MarkSelPortAchFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Mark Selected Portfolios As Achieved form, Submit button visible");

        public readonly ICriteria<DiplomaFacOfMedicinePage> MarkSelPortAchFormSubmitBtnNotVisible = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.DiplomaFacOfMedicinePage.MarkSelPortAchFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Mark Selected Portfolios As Achieved form, Submit button not visible");

        public readonly ICriteria<DiplomaFacOfMedicinePage> UnderReviewTblBodyRowCheckBoxVisible = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.DiplomaFacOfMedicinePage.PortfoliosUnderReviewTblBodyRowChk, ElementCriteria.IsVisible);
        }, "Portfolios Under Review table, first row check box visible");

        public readonly ICriteria<DiplomaFacOfMedicinePage> MyProgramSnapshotTblFirstRowPrgLnkVisible = new Criteria<DiplomaFacOfMedicinePage>(p =>
        {
            return p.Exists(Bys.DiplomaFacOfMedicinePage.MyProgramSnapshotTblFirstRowPrgLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled, ElementCriteria.HasText);
        }, "My Program Snapshot table first row, program name link visible");

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="DiplomaFacOfMedicinePage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<DiplomaFacOfMedicinePage> PageReady;
        public DiplomaFacOfMedicinePageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled);
        }
    }
}
