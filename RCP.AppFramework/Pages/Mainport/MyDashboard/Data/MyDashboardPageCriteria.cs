using Browser.Core.Framework;

namespace RCP.AppFramework
{
    public class MyDashboardPageCriteria
    {
        public readonly ICriteria<MyDashboardPage> EnterACPDActivityBtnEnabled = new Criteria<MyDashboardPage>(p =>
        {
            return p.Exists(Bys.MyDashboardPage.EnterACPDActivityBtn, ElementCriteria.IsEnabled);

        }, "Enter A CPD Activity Button enabled");

        public readonly ICriteria<MyDashboardPage> TotalCreditsAppliedValueLblVisible = new Criteria<MyDashboardPage>(p =>
        {
            return p.Exists(Bys.MyDashboardPage.TotalCreditsAppliedValueLbl, ElementCriteria.IsVisible);

        }, "Total Credits Applied value labell visible");

        public readonly ICriteria<MyDashboardPage> CreateAGoalFormNextBtnTxtVisible = new Criteria<MyDashboardPage>(p =>
        {
            return p.Exists(Bys.MyDashboardPage.CreateAGoalFormNextBtnTxt, ElementCriteria.IsVisible);

        }, "Create A Goal form, Next button visible");

        public readonly ICriteria<MyDashboardPage> CreateAGoalFormCloseBtnVisible = new Criteria<MyDashboardPage>(p =>
        {
            return p.Exists(Bys.MyDashboardPage.CreateAGoalFormCloseBtn, ElementCriteria.IsVisible);

        }, "Create A Goal form, Close button visible");

        public readonly ICriteria<MyDashboardPage> PageReady;

        public MyDashboardPageCriteria()
        {
            PageReady = EnterACPDActivityBtnEnabled.AND(TotalCreditsAppliedValueLblVisible);
        }
    }
}
