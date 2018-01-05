using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class DiplomaTraineePageBys
    {
        // Buttons
        public readonly By EvidForAchieveFormBrowseHiddenBtn = By.Id("markerDocUploader"); // The hidden browse button that appears when calling FileUtils.UploadFileUsingSendKeys
        public readonly By BackToDashboardBtn = By.XPath("//a[contains(., 'Back to Dashboard')]");
        public readonly By DescriptionSaveChangesBtn = By.XPath("//span[text()='Save Changes']");
        public readonly By SubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By SubmitPortfolioBtn = By.XPath("//button[@id='btnSubmitPortfolio']/span");
        public readonly By SubmitPortfolioFormSubmitBtn = By.XPath("//div[@id='mdlSubmitPortfolio']/descendant::span[text()='Submit']");
        public readonly By YourReplySaveChangesBtn = By.XPath("//span[text()='Save Changes']");
        public readonly By ResubmitBtn = By.XPath("(//span[contains(., 'Resubmit')])[2]");
        public readonly By SubmitMilestoneFormSubmitBtn = By.XPath("//div[@id='mdlSubmitKPO']/descendant::span[text()='Submit']");
        public readonly By SubmitSelectedMilestonesBtn = By.XPath("//span[text()='Submit Selected Milestones']");

        
        // Charts

        // Check boxes

        // Date control

        // Frames
        

        // Labels
        public readonly By ReviewStageValueLbl = By.XPath("//span[text()='Review Stage']/ancestor::td/following-sibling::td");
        public readonly By ReviewerValueLbl = By.XPath("//span[text()='Reviewer']/ancestor::td/following-sibling::td");

        // Links
        public readonly By MilestonesInMilestonesTblLnks = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::a[contains(@class, 'external-link kpo-dashboard-link')]"); // The milestone name links that appear on each row of the main page of Diploma
        public readonly By EvidenceTableUpdateLnks = By.XPath("//table[@class='table table-rc table-striped']/descendant::span[text()='Update']"); // The "Update" links that appear on the last column of every row on the Evidence table
        public readonly By EvidForAchieveFormDoneBtn = By.XPath("//span[text()='Done']");
        public readonly By UploadedFileLnk = By.XPath("//li[@class='documnt']/descendant::a"); // This represents any file that gets uploaded on the milestone page. Note this is not setup for multiple files being uploaded

        // Select Elements
        public readonly By StatusSelElem = By.Id("ddlStatusFilter");
        public readonly By SubmitMilestoneFormSelectReviewerSelElem = By.XPath("//div[@id='mdlSubmitKPO']/descendant::select");


        // Tables       
        public readonly By MilestonesTbl = By.XPath("//table[@class='table table-rc table-striped grid']");
        public readonly By MilestonesTblFirstRow = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::tr[contains(@class, 'item-row')]");
        public readonly By EvidForAchieveFormFileRow = By.XPath("//div[@id='mdlUploadMarkerDocuments']/descendant::table[@class='table table-rc table-striped']/descendant::tbody"); // The row that appears under the Add Files button from the Evidence for Achievement of Milestone popup
        public readonly By EvidenceTblRows = By.XPath("//span[text()='Update']/ancestor::tr"); // The rows in the Evidence for Achievement of Milestone table


        // Tabs
        
        
        
        // Text boxes
        public readonly By DescriptionTxt = By.XPath("//span[text()='Description of how this Milestone was achieved']/parent::div/parent::div/descendant::textarea");
        public readonly By YourReplyTxt = By.XPath("//span[text()='Your Reply']/ancestor::div[2]/descendant::textarea");

        












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