using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class PERCredentialStaffPageBys
    {
        // Buttons
        public readonly By AssignReferee2PERRefsFormSubmitBtn = By.XPath("//div[@id='mdlReferee']/descendant::button[contains(.,'Submit')]");
        public readonly By AssignAssessor2AssFormSubmitBtn = By.XPath("//div[@id='mdlAssignAssessors']/descendant::button[contains(.,'Submit')]");
        public readonly By FinalReviewFormSubmitBtn = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::button[@id='btnSubmit']");
        public readonly By AssignAssessor3rdAssFormSubmitBtn = By.XPath("//div[@id='mdlAssignThirdAssessor']/descendant::button[@id='btnSubmit']");

        // Charts

        // Check boxes

        // Date control

        // Forms
        public readonly By AssignReferee2PERRefsForm = By.XPath("//button[text()='Submit ']/ancestor::div[@class='modal-content']");
        public readonly By AssignAssessor2AssForm = By.XPath("(//button[text()='Submit '])[2]/ancestor::div[@class='modal-content']");

        // Frames
        

        // Labels
        public readonly By AssignAssessor2AssFormTitleLbl = By.XPath("//b[text()='Assessor Name ']"); 
        public readonly By AssignReferee2PERRefsFormTitleLbl = By.XPath("//strong[text()='Royal College Assessors Stage Date:']");



        // Links

        // Radio buttons
        public readonly By FinalReviewFormAchievedRdo = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::div[contains(.,'Achieved')]/descendant::input[1]");
        public readonly By FinalReviewFormNotAchievedRdo = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::div[contains(.,'Achieved')]/descendant::input[2]");

        // Random
        public readonly By BackGroundBackDrop = By.XPath("//div[contains(@class, 'modal-backdrop fade')]"); // This element appears after a popup has been opened. It is the gray background behind the pop. It will persist for about a half second after the popup closes. So we use this element in a wait criteria when popups close


        // Select Elements
        public readonly By AssignReferee2PERRefsFormFirstRefSelElem = By.XPath("//div[@id='mdlReferee']/descendant::select[@ng-model='modalassignReferee.first.Id' and @ng-options='referee.Id as referee.Name for referee in modalassignReferee.referees ']");
        public readonly By AssignReferee2PERRefsFormSecondRefSelElem = By.XPath("//div[@id='mdlReferee']/descendant::select[@ng-model='modalassignReferee.second.Id' and @ng-options='referee.Id as referee.Name for referee in modalassignReferee.referees ']");
        public readonly By AssignAssessor2AssFormFirstAssSelElem = By.XPath("//div[@id='mdlAssignAssessors']/descendant::select[@ng-model='modalassignAssessors.first.Id']");
        public readonly By AssignAssessor2AssFormSecondAssSelElem = By.XPath("//div[@id='mdlAssignAssessors']/descendant::select[@ng-model='modalassignAssessors.second.Id']");
        public readonly By AssignAssessor3rdAssFormThirdAssSelElem = By.XPath("//div[@id='mdlAssignThirdAssessor']/descendant::select[@ng-model='modalthirdAssessor.third.Id']");



        // Scripts

        // Tables   
        public readonly By RefereesTabTraineeTbl = By.XPath("//table[@zebra-model='PagedPortfolios']");
        public readonly By RefereesTabTraineeTblFirstRow = By.XPath("//table[@zebra-model='PagedPortfolios']/descendant::tr[@ng-repeat='portfolio in PagedPortfolios ']");
        public readonly By AssessorTabTraineeTbl = By.XPath("//table[@zebra-model='PagedAssessorPortfolios']");
        public readonly By AssessorTabTraineeTblFirstRow = By.XPath("//table[@zebra-model='PagedAssessorPortfolios']/descendant::tr[@ng-repeat='assessor in PagedAssessorPortfolios']");
        public readonly By MyProgramSnapshotTbl = By.XPath("(//table[@zebra-model='programSummaryList'])[1]");
        public readonly By MyProgramSnapshotTblFirstRowPrgLnk = By.XPath("(//table[@zebra-model='programSummaryList'])[1]/tbody[2]/tr/td/a"); // If a row exists in the My Program Snapshot table, this will be the program name link in that row
    

        // Tabs
        public readonly By RefereesTab = By.XPath("//a[@data-toggle='tab' and contains(text(),'Referees')]/span");
        public readonly By AssessorTab = By.XPath("//a[@data-toggle='tab' and contains(text(),'Assessor')]/span");



        // Text boxes
        public readonly By FinalReviewFormRequestCommentsTxt = By.XPath("//div[@id='mdlAssessorMarkAsFinalReview']/descendant::textarea[@id='portfoliofinalComments']");













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