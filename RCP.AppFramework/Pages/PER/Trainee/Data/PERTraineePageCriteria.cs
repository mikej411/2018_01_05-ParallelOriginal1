using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class PERTraineePageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met for after we login as a trainee.
        /// </summary>
        public readonly ICriteria<PERTraineePage> MainFrameVisibleAndEnabled = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<PERTraineePage> LoadIconDisappeared = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<PERTraineePage> MilestonesTblEnabled = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.MilestonesTbl, ElementCriteria.IsEnabled);
        }, "Milestones table enabled");
        

        public readonly ICriteria<PERTraineePage> MilestonesTblMilestoneNameLinksEnabled = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.MilestonesInMilestonesTblLnks, ElementCriteria.IsEnabled);
        }, "Milestones table row name links enabled");

        public readonly ICriteria<PERTraineePage> EvidenceTblUpdateLinksEnabledAndVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.EvidenceTableUpdateLnks, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
        }, "Evidence table row Update links enabled and visible");

        public readonly ICriteria<PERTraineePage> EvidForAchieveFormDoneBtnEnabled = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.EvidForAchieveFormCloseLnk, ElementCriteria.IsEnabled);
        }, "Evidence For Achievement window Close link enabled");

        public readonly ICriteria<PERTraineePage> EvidForAchieveFormDoneBtnNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.EvidForAchieveFormCloseLnk, ElementCriteria.IsNotVisible);
        }, "Evidence For Achievement window Close link not visible");

        /// <summary>
        /// This row holds the file after it is uploaded
        /// </summary>
        public readonly ICriteria<PERTraineePage> EvidForAchieveFormFileRowEnabledAndVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.EvidForAchieveFormFileRow, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
        }, "Evidence For Achievement window file row enabled and visible");

        public readonly ICriteria<PERTraineePage> DescriptionSaveChangesButtonVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.DescriptionSaveChangesBtn, ElementCriteria.IsVisible);
        }, "Description Save Changes button visible");

        public readonly ICriteria<PERTraineePage> DescriptionSaveChangesButtonNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.DescriptionSaveChangesBtn, ElementCriteria.IsNotVisible);
        }, "Description Save Changes button not visible");

        public readonly ICriteria<PERTraineePage> YourReplySaveChangesButtonVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.YourReplySaveChangesBtn, ElementCriteria.IsVisible);
        }, "Your Reply Save Changes button visible");

        public readonly ICriteria<PERTraineePage> YourReplySaveChangesBtnNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.YourReplySaveChangesBtn, ElementCriteria.IsNotVisible);
        }, "Your Reply Save Changes button not visible");

        public readonly ICriteria<PERTraineePage> MarkCompleteButtonNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.DescriptionSaveChangesBtn, ElementCriteria.IsNotVisible);
        }, "Mark Complete button not visible");

        public readonly ICriteria<PERTraineePage> ResubmitButtonNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.ResubmitBtn, ElementCriteria.IsNotVisible);
        }, "Resubmit button not visible");
        

        public readonly ICriteria<PERTraineePage> EvidenceTblRowsVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.EvidenceTblRows, ElementCriteria.IsVisible);
        }, "Evidence table rows visible");

        public readonly ICriteria<PERTraineePage> SubmitPortfolioBtnVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.SubmitPortfolioBtn, ElementCriteria.IsVisible);
        }, "Submit Portfolio button visible");

        public readonly ICriteria<PERTraineePage> SubmitPortfolioBtnNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.SubmitPortfolioBtn, ElementCriteria.IsNotVisible);
        }, "Submit Portfolio button not visible");

        public readonly ICriteria<PERTraineePage> BackToDashboardBtnVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.BackToDashboardBtn, ElementCriteria.IsVisible);
        }, "Back To Dashboard button visible");

        public readonly ICriteria<PERTraineePage> SubmitPortfolioFormSubmitBtnNotVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.SubmitPortfolioFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Submit Portfolio form Submit button not visible");

        public readonly ICriteria<PERTraineePage> SubmitPortfolioFormSubmitBtnVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.SubmitPortfolioFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Submit Portfolio form Submit button visible");

        public readonly ICriteria<PERTraineePage> UploadedFileLnkVisible = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.UploadedFileLnk, ElementCriteria.IsVisible);
        }, "File uploaded on milestone page visible");

        public readonly ICriteria<PERTraineePage> ReviewStageValueLblHasText = new Criteria<PERTraineePage>(p =>
        {
            return p.Exists(Bys.PERTraineePage.ReviewStageValueLbl, ElementCriteria.HasText);
        }, "Review Stage value label has text");
        

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="PERTraineePage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<PERTraineePage> PageReady;
        public PERTraineePageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled); 
        }
    }
}
