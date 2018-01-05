using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login,
    /// one of the criteria that we wait for is the circular load icon to disappear
    /// </summary>
    public class DiplomaTraineePageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met for after we login as a trainee.
        /// </summary>
        public readonly ICriteria<DiplomaTraineePage> MainFrameVisibleAndEnabled = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.RCPPage.MainFrame, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Main frame visible and enabled");

        public readonly ICriteria<DiplomaTraineePage> LoadIconDisappeared = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<DiplomaTraineePage> MilestonesTblEnabled = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.MilestonesTbl, ElementCriteria.IsEnabled);
        }, "Milestones table enabled");

        public readonly ICriteria<DiplomaTraineePage> MilestonesTblVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.MilestonesTbl, ElementCriteria.IsVisible);
        }, "Milestones table visible");

        public readonly ICriteria<DiplomaTraineePage> MilestonesTblMilestoneNameLinksEnabled = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.MilestonesInMilestonesTblLnks, ElementCriteria.IsEnabled);
        }, "Milestones table row name links enabled");
       
        public readonly ICriteria<DiplomaTraineePage> EvidenceTblUpdateLinksEnabledAndVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.EvidenceTableUpdateLnks, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
        }, "Evidence table row Update links enabled and visible");

        public readonly ICriteria<DiplomaTraineePage> EvidForAchieveFormDoneBtnEnabled = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.EvidForAchieveFormDoneBtn, ElementCriteria.IsEnabled);
        }, "Evidence For Achievement window Close link enabled");

        public readonly ICriteria<DiplomaTraineePage> EvidForAchieveFormDoneBtnNotVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.EvidForAchieveFormDoneBtn, ElementCriteria.IsNotVisible);
        }, "Evidence For Achievement window Close link not visible");

        /// <summary>
        /// This row holds the file after it is uploaded
        /// </summary>
        public readonly ICriteria<DiplomaTraineePage> EvidForAchieveFormFileRowEnabledAndVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.EvidForAchieveFormFileRow, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
        }, "Evidence For Achievement window file row enabled and visible");

        public readonly ICriteria<DiplomaTraineePage> DescriptionSaveChangesButtonVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.DescriptionSaveChangesBtn, ElementCriteria.IsVisible);
        }, "Description Save Changes button visible");

        public readonly ICriteria<DiplomaTraineePage> DescriptionSaveChangesButtonNotExists = new Criteria<DiplomaTraineePage>(p =>
        {
            return !p.Exists(Bys.DiplomaTraineePage.DescriptionSaveChangesBtn);
        }, "Description Save Changes not exists");

        public readonly ICriteria<DiplomaTraineePage> YourReplySaveChangesButtonVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.YourReplySaveChangesBtn, ElementCriteria.IsVisible);
        }, "Your Reply Save Changes button visible");

        public readonly ICriteria<DiplomaTraineePage> YourReplySaveChangesBtnNotVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.YourReplySaveChangesBtn, ElementCriteria.IsNotVisible);
        }, "Your Reply Save Changes button not visible");

        public readonly ICriteria<DiplomaTraineePage> MarkCompleteButtonNotVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.DescriptionSaveChangesBtn, ElementCriteria.IsNotVisible);
        }, "Mark Complete button not visible");

        public readonly ICriteria<DiplomaTraineePage> ResubmitButtonNotVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.ResubmitBtn, ElementCriteria.IsNotVisible);
        }, "Resubmit button not visible");
        

        public readonly ICriteria<DiplomaTraineePage> EvidenceTblRowsVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.EvidenceTblRows, ElementCriteria.IsVisible);
        }, "Evidence table rows visible");

        public readonly ICriteria<DiplomaTraineePage> SubmitPortfolioBtnVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.SubmitPortfolioBtn, ElementCriteria.IsVisible);
        }, "Submit Portfolio button visible");

        public readonly ICriteria<DiplomaTraineePage> SubmitPortfolioBtnNotVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.SubmitPortfolioBtn, ElementCriteria.IsNotVisible);
        }, "Submit Portfolio button not visible");

        public readonly ICriteria<DiplomaTraineePage> BackToDashboardBtnVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.BackToDashboardBtn, ElementCriteria.IsVisible);
        }, "Back To Dashboard button visible");

        public readonly ICriteria<DiplomaTraineePage> BackToDashboardBtnEnabled = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.BackToDashboardBtn, ElementCriteria.IsVisible);
        }, "Back To Dashboard button enabled");

        public readonly ICriteria<DiplomaTraineePage> SubmitPortfolioFormSubmitBtnNotVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.SubmitPortfolioFormSubmitBtn, ElementCriteria.IsNotVisible);
        }, "Submit Portfolio form Submit button not visible");

        public readonly ICriteria<DiplomaTraineePage> SubmitPortfolioFormSubmitBtnVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.SubmitPortfolioFormSubmitBtn, ElementCriteria.IsVisible);
        }, "Submit Portfolio form Submit button visible");

        public readonly ICriteria<DiplomaTraineePage> ReviewStageValueLblHasTextIsVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.ReviewStageValueLbl, ElementCriteria.HasText, ElementCriteria.IsVisible);
        }, "Review Stage value label has text and visible");

        public readonly ICriteria<DiplomaTraineePage> SubmitSelectedMilestonesBtnVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.SubmitSelectedMilestonesBtn, ElementCriteria.IsVisible);
        }, "Submit Selected Milestones button visible");

        public readonly ICriteria<DiplomaTraineePage> LoadIconVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIconForPERAndDiploma, ElementCriteria.IsVisible);
        }, "Load icon visible");

        public readonly ICriteria<DiplomaTraineePage> UploadedFileLnkVisible = new Criteria<DiplomaTraineePage>(p =>
        {
            return p.Exists(Bys.DiplomaTraineePage.UploadedFileLnk, ElementCriteria.IsVisible);
        }, "File uploaded on milestone page visible");

        public readonly ICriteria<DiplomaTraineePage> SubmitMilestoneFormSelectReviewerSelElemIsVisibleHasItems = new Criteria<DiplomaTraineePage>(p =>
        {
        return p.Exists(Bys.DiplomaTraineePage.SubmitMilestoneFormSelectReviewerSelElem, ElementCriteria.IsVisible, ElementCriteria.SelectElementHasItems);
        }, "Submit Milestone form Select Reviewer select element visible and has items");

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="DiplomaTraineePage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<DiplomaTraineePage> PageReady;
        public DiplomaTraineePageCriteria()
        {
            PageReady = LoadIconDisappeared.AND(MainFrameVisibleAndEnabled); 
        }
    }
}
