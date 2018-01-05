using Browser.Core.Framework;

namespace RCP.AppFramework
{
    public class MyHoldingAreaPageCriteria
    {
        public readonly ICriteria<MyHoldingAreaPage> IncompleteActivitiesTblBodyVisible = new Criteria<MyHoldingAreaPage>(p =>
        {
            return p.Exists(Bys.MyHoldingAreaPage.IncompleteActivitiesTblBody, ElementCriteria.IsVisible);

        }, "Incomplete Activities table body visible");

        public readonly ICriteria<MyHoldingAreaPage> PageReady;

        public MyHoldingAreaPageCriteria()
        {
            PageReady = IncompleteActivitiesTblBodyVisible;
        }
    }
}
