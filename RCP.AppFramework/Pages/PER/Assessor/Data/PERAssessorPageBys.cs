using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class PERAssessorPageBys
    {
        // Buttons
        public readonly By MarkAsAchievedBtn = By.XPath("//td[contains(., 'Mark as Achieved')]");
        public readonly By MarkAsNotAchievedBtn = By.XPath("//td[contains(., 'Mark as not Achieved')]");
        public readonly By BackToPortfolioBtn = By.XPath("//a[contains(., 'Back to Portfolio')]");
        public readonly By RequestAdditionalInfoBtn = By.XPath("//span[text()='Request Additional Information']");
        public readonly By RequestAdditionalInfoFormSubmitBtn = By.XPath("//div[@id='mdlSubmitAIR']/descendant::button[contains(., 'Submit')]");

        
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
        public readonly By MilestonesInMilestonesTblLnks = By.XPath("//table[@class='table table-rc table-tbody-striped-reverse grid']/descendant::tr[contains(@class, 'item-row')]/descendant::a[@class='ng-binding']");
        // Radio Buttons


        // Select Elements
        public readonly By StatusSelElem = By.XPath("//select[@ng-model='milestoneStatusFilter']");

        
        // Scripts

        // Tables   
        public readonly By PortfolioAssignmentsTbl = By.XPath("//table[@zebra-model='portfoliosToDisplay']");
        public readonly By PortfolioAssignmentsTblFirstRow = By.XPath("//table[@zebra-model='portfoliosToDisplay']/descendant::tr[@ng-repeat='portfolio in portfoliosToDisplay']"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By MilestonesTbl = By.XPath("//table[@class='table table-rc table-tbody-striped-reverse grid']");
        public readonly By MilestonesTblFirstRow = By.XPath("//table[@class='table table-rc table-tbody-striped-reverse grid']/descendant::tr[contains(@class, 'item-row')]"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load


        // Tabs

        // Text boxes
        public readonly By RequestAdditionalInfoFormCommentsTxt = By.XPath("//div[@id='mdlSubmitAIR']/descendant::textarea");

        








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