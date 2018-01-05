using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class DiplomaAssessorPageBys
    {
        // Buttons
        public readonly By MarkAsAchievedBtn = By.XPath("//td[contains(., 'Mark as Achieved')]");
        public readonly By MarkAsNotAchievedBtn = By.XPath("//td[contains(., 'Mark as not Achieved')]");
        public readonly By BackToDashboardBtn = By.XPath("//a[contains(., 'Back to Dashboard')]");
        public readonly By RequestAdditionalInfoBtn = By.XPath("(//span[text()='Request Additional Information'])[1]");
        public readonly By RequestAdditionalInfoFormSubmitBtn = By.XPath("//div[@id='mdlSubmitAIR']/descendant::button[contains(., 'Submit')]");
        public readonly By MarkAsAchievedFormSubmitBtn = By.XPath("//div[@id='mdlMarkKPOAsAchieved']/descendant::span[text()='Submit']");
        public readonly By MarkAsNotAchievedFormSubmitBtn = By.XPath("//div[@id='mdlMarkKPOAsNotAchieved']/descendant::span[text()='Submit']");


        // Charts

        // Check boxes

        // Date control

        // Frames



        // Labels
        public readonly By ReviewStageValueLbl = By.XPath("//span[text()='Review Stage']/ancestor::td/following-sibling::td");

        // Links
        public readonly By MilestonesInMilestonesTblLnks = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::tr[contains(@class, 'item-row')]/descendant::a[@class='external-link kpo-link']");
        
        // Radio Buttons


        // Select Elements


        // Scripts

        // Tables   
        public readonly By PortfolioAssignmentsTbl = By.XPath("(//table[@class='table table-rc table-striped grid'])[1]");
        public readonly By PortfolioAssignmentsTblFirstRow = By.XPath("(//table[@class='table table-rc table-striped grid'])[1]/descendant::tr[@class='item-row']");
        public readonly By ResubmittedMilestonesTbl = By.XPath("//table[@class='table table-rc table-striped grid']");
        public readonly By ResubmittedMilestonesTblFirstRow = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::tr[@class='item-row']");
        public readonly By MilestonesTbl = By.XPath("//table[@class='table table-rc table-striped grid']");
        public readonly By MilestonesTblFirstRow = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::tr[contains(@class, 'item-row')]");


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