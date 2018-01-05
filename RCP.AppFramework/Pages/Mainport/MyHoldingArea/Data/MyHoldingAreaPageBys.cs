using OpenQA.Selenium;

namespace RCP.AppFramework
{
    public class MyHoldingAreaPageBys
    {
        // Buttons
        public readonly By EnterACPDActivityBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        public readonly By DeleteActivityYesBtn = By.XPath("//span[text()='Yes']");

        
        // Charts

        // Check boxes

        // Date control

        // Frames


        
        // Labels


        // Links


        // Radio Buttons


        // Select Elements


        // Scripts

        // Tables   
        public readonly By IncompleteActivitiesTbl = By.Id("ctl00_ContentPlaceHolder1_grdSummaryIncompleteActivities_ctl00"); 
        public readonly By IncompleteActivitiesTblTHead = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_grdSummaryIncompleteActivities_ctl00']/thead");
        public readonly By IncompleteActivitiesTblBody = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_grdSummaryIncompleteActivities_ctl00']/tbody");
        public readonly By IncompleteActivitiesTblBodyRow = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_grdSummaryIncompleteActivities_ctl00']/tbody/tr"); // If one row exists, this will represent that row
        public readonly By AwaitingCredValidationTbl = By.Id("ctl00_ContentPlaceHolder1_grdSummaryCreditValidationActivities_ctl00");
        public readonly By AwaitingCredValidationTblTHead = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_grdSummaryCreditValidationActivities_ctl00']/thead");
        public readonly By AwaitingCredValidationTblBody = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_grdSummaryCreditValidationActivities_ctl00']/tbody");
        public readonly By AwaitingCredValidationTblBodyRow = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_grdSummaryCreditValidationActivities_ctl00']/tbody/tr"); // If one row exists, this will represent that row


        // Tabs

        // Text boxes
    }
}