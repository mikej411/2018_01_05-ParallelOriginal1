using Browser.Core.Framework;

namespace RCP.AppFramework
{
    public class MyCPDActivitiesListPageCriteria
    {
        public readonly ICriteria<MyCPDActivitiesListPage> EnterACPDActivityBtnEnabled = new Criteria<MyCPDActivitiesListPage>(p =>
        {
            return p.Exists(Bys.MyCPDActivitiesListPage.EnterACPDActivityBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");

        public readonly ICriteria<MyCPDActivitiesListPage> ActivityTblBodyVisibleAndEnabled = new Criteria<MyCPDActivitiesListPage>(p =>
        {
            return p.Exists(Bys.MyCPDActivitiesListPage.ActivityTblBody, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);

        }, "Activity table body enabled and visible");

        public readonly ICriteria<MyCPDActivitiesListPage> PageReady;

        public MyCPDActivitiesListPageCriteria()
        {
            PageReady = EnterACPDActivityBtnEnabled.AND(ActivityTblBodyVisibleAndEnabled);
        }
    }
}
