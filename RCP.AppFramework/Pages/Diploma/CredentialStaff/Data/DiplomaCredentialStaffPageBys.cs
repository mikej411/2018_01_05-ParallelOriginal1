using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class DiplomaCredentialStaffPageBys
    {
        // Buttons
        public readonly By AssignAssessor2AssFormSubmitBtn = By.XPath("//div[@id='mdlAssignNewAssessors']/descendant::span[text()='Submit']");
        public readonly By AssignAssessor3rdAssFormSubmitBtn = By.XPath("//div[@id='mdlAssignThirdAssessor']/descendant::button[@id='btnSubmit']");
        public readonly By FinalReviewFormSubmitBtn = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::button[@id='btnSubmit']");
        public readonly By MarkPortfolioAsAchievedFormSubmitBtn = By.XPath("//div[@id='mdlCUMarkPortfolioAsAchieved']/descendant::span[text()='Submit']");
        public readonly By RecordPaymentFormSubmitBtn = By.XPath("//div[@id='mdlRecordPayment']/descendant::span[text()='Submit']");

        // Charts

        // Check boxes

        // Date control

        // Forms
        public readonly By AssignAssessor2AssForm = By.XPath("(//button[text()='Submit '])[2]/ancestor::div[@class='modal-content']");

        // Frames


        // Labels
        public readonly By AssignAssessor2AssFormTitleLbl = By.XPath("//b[text()='Assessor Name ']");
        public readonly By NoPortfoliosLbl = By.XPath("//span[text()='No portfolios to display. Try changing filter criteria.']");

        

        // Links

        // Radio buttons
        public readonly By FinalReviewFormAchievedRdo = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::div[contains(.,'Achieved')]/descendant::input[1]");
        public readonly By FinalReviewFormNotAchievedRdo = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::div[contains(.,'Achieved')]/descendant::input[2]");

        // Random
        public readonly By BackGroundBackDrop = By.XPath("//div[contains(@class, 'modal-backdrop fade')]"); // This element appears after a popup has been opened. It is the gray background behind the pop. It will persist for about a half second after the popup closes. So we use this element in a wait criteria when popups close

        // Select Elements
        public readonly By AssignAssessor2AssFormFirstAssSelElem = By.XPath("(//div[@id='mdlAssignNewAssessors']/descendant::select)[1]");
        public readonly By AssignAssessor2AssFormSecondAssSelElem = By.XPath("(//div[@id='mdlAssignNewAssessors']/descendant::select)[2]");
        public readonly By AssignAssessor3rdAssFormThirdAssSelElem = By.XPath("//div[@id='mdlAssignThirdAssessor']/descendant::select");



        // Scripts

        // Tables   
        public readonly By PortfoliosUnderReviewTbl = By.XPath("//div[@id='portfolios']/descendant::table[@class='table table-rc table-striped grid']");
        public readonly By PortfoliosUnderReviewTblFirstRow = By.XPath("//div[@id='portfolios']/descendant::table[@class='table table-rc table-striped grid']/descendant::tr[@class='item-row']");
        public readonly By AssessorTbl = By.XPath("//div[@id='assessor']/descendant::table[@class='table table-rc table-striped grid']");
        public readonly By AssessorTblFirstRow = By.XPath("//div[@id='assessor']/descendant::table[@class='table table-rc table-striped grid']");
        public readonly By ResubmittedMilestonesTbl = By.XPath("//div[@id='outcomes']/descendant::table[@class='table table-rc table-striped grid']");
        public readonly By ResubmittedMilestonesTblFirstRow = By.XPath("//div[@id='outcomes']/descendant::table[@class='table table-rc table-striped grid']");
        public readonly By MyProgramSnapshotTbl = By.XPath("(//table[@class='table table-rc table-striped grid'])[1]");
        public readonly By MyProgramSnapshotTblFirstRowPrgLnk = By.XPath("(//table[@class='table table-rc table-striped grid'])[1]/tbody[2]/tr/td/a"); // If a row exists in the My Program Snapshot table, this will be the program name link in that row


        // Tabs
        public readonly By PortfoliosUnderReviewTab = By.XPath("//ul[@id='diplomaDirectorTabs']/descendant::span[text()='Portfolios Under Review']");
        public readonly By ResubmitedMilestonesTab = By.XPath("//ul[@id='diplomaDirectorTabs']/descendant::span[text()='Resubmitted Milestones']");
        public readonly By AssessorTab = By.XPath("//ul[@id='diplomaDirectorTabs']/descendant::span[text()='Assessor']");



        // Text boxes
        public readonly By FinalReviewFormRequestCommentsTxt = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::textarea[@id='portfoliofinalComments']");
        public readonly By RecordPaymentFormDateTxt = By.Id("recordPaymentDate");
        public readonly By RecordPaymentFormCommentsTxt = By.Id("recordPaymentComment");

        











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