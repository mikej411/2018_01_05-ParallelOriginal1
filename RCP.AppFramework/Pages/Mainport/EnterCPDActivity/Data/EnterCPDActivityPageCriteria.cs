using Browser.Core.Framework;

namespace RCP.AppFramework
{
    public class EnterCPDActivityPageCriteria
    {
        public readonly ICriteria<EnterCPDActivityPage> iFrameVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.EnterACPDFrame, ElementCriteria.IsVisible);

        }, "iFrame is visible");

        public readonly ICriteria<EnterCPDActivityPage> IsIsActivityAccreditedYesVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.IsActivityAccrYesRdo, ElementCriteria.IsVisible);

        }, "IsIsActivityAccreditedYes radio button visible");

        public readonly ICriteria<EnterCPDActivityPage> LoadingImgNotVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.LoadingImg, ElementCriteria.IsNotVisible);

        }, "Loading image not visible");

        public readonly ICriteria<EnterCPDActivityPage> LoadingImgVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.LoadingImg, ElementCriteria.IsVisible);

        }, "Loading image not visible");

        public readonly ICriteria<EnterCPDActivityPage> SAPNameSelElemVisibleAndHasItems = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.SAPNameSelElem, ElementCriteria.IsVisible, ElementCriteria.SelectElementHasItems);

        }, "SAP Name select element visible and has items");

        public readonly ICriteria<EnterCPDActivityPage> CloseBtnVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.CloseBtn, ElementCriteria.IsVisible);

        }, "Close button visible");

        public readonly ICriteria<EnterCPDActivityPage> CloseSecondInstanceBtnVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.CloseSecondInstanceBtn, ElementCriteria.IsVisible);

        }, "Close button visible");

        public readonly ICriteria<EnterCPDActivityPage> SupportingDocumentsTabLblVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.SupportingDocumentsTabLbl, ElementCriteria.IsVisible);

        }, "Supporting Documents tab Label visible");

        public readonly ICriteria<EnterCPDActivityPage> SupportingDocumentsTabSubmitBtnVisibleAndEnabled = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.SupportingDocumentsTabSubmitBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Supporting Documents tab, Submit button visible and enabled");

        public readonly ICriteria<EnterCPDActivityPage> ContinueBtnVisible = new Criteria<EnterCPDActivityPage>(p =>
        {
            return p.Exists(Bys.EnterCPDActivityPage.ContinueBtn, ElementCriteria.IsVisible);

        }, "Continue button visible");

        public readonly ICriteria<EnterCPDActivityPage> PageReady;
        public EnterCPDActivityPageCriteria()
        {
            
            PageReady = iFrameVisible;
        }
    }
}
