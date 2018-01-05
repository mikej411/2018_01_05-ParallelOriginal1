using OpenQA.Selenium;

namespace RCP.AppFramework
{
    public class MyCPDActivitiesListPageBys
    {
        // Buttons
        public readonly By EnterACPDActivityBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        public readonly By DeleteActivityFormOkBtn = By.XPath("//span[contains(.,'OK')]");
        public readonly By DeleteActivityFormCancelBtn = By.XPath("//span[contains(.,'Cancel')]");

        // Charts

        // Check boxes

        // Date control

        // Form
        public readonly By DeleteActivityForm = By.XPath("//div[@id='RadWindowWrapper_confirm1501100671184']");


        // Frames

        // Labels


        // Links


        // Radio Buttons


        // Select Elements


        // Scripts

        // Tables   
        public readonly By ActivityTblBody = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_ctrlCPDActivities_grdCPDActiity_ctl00']/tbody");
        public readonly By ActivityTblFirstRow = By.XPath("//table[@class='rgMasterTable']/tbody/tr");

        
        // Tabs

        // Text boxes




    }
}