using Browser.Core.Framework;

namespace RCP.AppFramework
{
    public class MyMOCPageCriteria
    {
        public readonly ICriteria<MyMOCPage> EnterACPDActivityBtnEnabled = new Criteria<MyMOCPage>(p =>
        {
            return p.Exists(Bys.MyMOCPage.EnterACPDActivityBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");

        public readonly ICriteria<MyMOCPage> ViewActivitiesFormActivitiesTblBodyRowVisible = new Criteria<MyMOCPage>(p =>
        {
            return p.Exists(Bys.MyMOCPage.ViewActivitiesFormActivitiesTblBodyRow, ElementCriteria.IsVisible);

        }, "View Activities form, Activities table body first row visible");

        public readonly ICriteria<MyMOCPage> ViewActivitiesFormFrameExists = new Criteria<MyMOCPage>(p =>
        {
            return p.Exists(Bys.MyMOCPage.ViewActivitiesFormFrame);

        }, "View Activities form frame exists");

        public readonly ICriteria<MyMOCPage> GroupLearnTblAccrActRowCredsRptLblVisible = new Criteria<MyMOCPage>(p =>
        {
            return p.Exists(Bys.MyMOCPage.GroupLearnTblAccrActRowCredsRptLbl, ElementCriteria.IsVisible);

        }, "Group Learning table, Accredited Activities row, Credits Reports label Visible");

        public readonly ICriteria<MyMOCPage> PageReady;

        public MyMOCPageCriteria()
        {
            PageReady = EnterACPDActivityBtnEnabled.AND(GroupLearnTblAccrActRowCredsRptLblVisible);
        }
    }
}
