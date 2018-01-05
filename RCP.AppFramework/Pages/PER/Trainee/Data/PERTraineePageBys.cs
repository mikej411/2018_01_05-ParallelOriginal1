using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class PERTraineePageBys
    {
        // Buttons
        public readonly By EvidForAchieveFormBrowseHiddenBtn = By.Id("markerDocUploader"); // The hidden browse button that appears when calling FileUtils.UploadFileUsingSendKeys
        public readonly By BackToDashboardBtn = By.XPath("//a[contains(., 'Back to Dashboard')]");
        public readonly By DescriptionSaveChangesBtn = By.XPath("//button[text()='Save Changes']");
        public readonly By MarkCompleteBtn = By.XPath("//span[contains(., 'Mark Complete')]");
        public readonly By SubmitPortfolioBtn = By.XPath("//button[contains(., 'Submit Portfolio')]");
        public readonly By SubmitPortfolioFormSubmitBtn = By.XPath("//div[@id='mdlMarkAsSubmitted']/descendant::button[@id='btnSubmit']");
        public readonly By YourReplySaveChangesBtn = By.XPath("(//button[text()='Save Changes'])[2]");
        public readonly By ResubmitBtn = By.XPath("//span[contains(., 'Resubmit')]");

        // Charts

        // Check boxes

        // Date control

        // Frames
        

        // Labels
        public readonly By ReviewStageValueLbl = By.XPath("//td[text()='Review Stage:']/following-sibling::td");
        public readonly By Referee1PERValueLbl = By.XPath("//td[text()='Referee 1:']/following-sibling::td");
        public readonly By Referee2PERValueLbl = By.XPath("//td[text()='Referee 2:']/following-sibling::td");
        public readonly By Referee3ValueLbl = By.XPath("//td[text()='Referee 3:']/following-sibling::td");

        // Links
        public readonly By MilestonesInMilestonesTblLnks = By.XPath("//table[@class='table table-rc table-tbody-striped-reverse grid']/descendant::a[contains(@class, 'external-link kpo-dashboard-link')]"); // The milestone name links that appear on each row of the main page of PER
        public readonly By EvidenceTableUpdateLnks = By.XPath("//tbody[@ng-repeat='evidenceItem in milestoneDetails.EvidenceInstances']/descendant::a[text()='Update']"); // The "Update" links that appear on the last column of every row on the Evidence table
        public readonly By EvidForAchieveFormCloseLnk = By.XPath("//button[text()='Close']");
        public readonly By UploadedFileLnk = By.XPath("//td[@class='td-doc']/descendant::a[1]"); // This represents any file that gets uploaded on the milestone page. Note this is not setup for multiple files being uploaded

        // Select Elements
        public readonly By StatusSelElem = By.Id("ddlStatusFilter");


        // Scripts
        public readonly By SrcScriptElem = By.XPath("//script[contains(@src, 'newrelic')]");

        // Tables       
        public readonly By MilestonesTbl = By.XPath("//table[@class='table table-rc table-tbody-striped-reverse grid']");
        public readonly By MilestonesTblFirstRow = By.XPath("//table[@class='table table-rc table-tbody-striped-reverse grid']/descendant::tr[contains(@class, 'item-row')]"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By EvidForAchieveFormFileRow = By.XPath("//tbody[@ng-repeat='doc in evidenceItem.Files']"); // The row that appears under the Add Files button from the Evidence for Achievement of Milestone popup
        public readonly By EvidenceTblRows = By.XPath("//tbody[@ng-repeat='evidenceItem in milestoneDetails.EvidenceInstances']"); // The rows in the Evidence for Achievement of Milestone table


        // Tabs
        public readonly By blah = By.XPath("/descendant::a/img"); //
        public readonly By blah2 = By.XPath("//td/table/tbody/tr/td/a/img"); // 
        
        
        
        // Text boxes
        public readonly By DescriptionTxt = By.Id("newComments");
        public readonly By YourReplyTxt = By.XPath("//textarea[@ng-model='Comment']");

        












        // Locator examples
        //public readonly By Menu_About = By.XPath("//li[@id='menu-item-1155']/a");

        //public readonly By Menu_FunctionalTesting = By.XPath("//li[@id='menu-item-1150']/a");
        //public readonly By Menu_FunctionalTesting_BDDSpecFlow = By.XPath("//li[@id='menu-item-1154']/a");

        // This XPath line selects the first TD element with the exact text
        //string xPathVariable = "//td[./text()='yourtext']";
        //string xPathVariable = "//td[contains(text(),'yourtext')]";
        //string xPathVariable = string.Format("//div[contains(.,'{0}')]", textOfCell);
        //IWebElement TDCell = gridElem.FindElement(By.XPath(xPathVariable));

        // Mulitple elements or multiple attributes
        //string xpathString = string.Format("//span[text()='{0}' and @class=\"ui-iggrid-headertext\"]", textOfHeaderCell);

        // Attribute does not equal
        //IWebElement lists = Browser.FindElement(By.CssSelector("li:not([class=hidden])"));

    }
}