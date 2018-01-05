using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class CBDObserverPageBys
    {     
        // Buttons
        public readonly By AddObservationBtn = By.XPath("//span[text()='Add Observation']");
        public readonly By UploadEvidenceBtn = By.XPath("//span[text()='Upload Evidence']");
        public readonly By AccDecAssgnMntFormCancelBtn = By.XPath("//span[text()='Cancel']");
        public readonly By AccDecAssgnMntFormDeclineBtn = By.XPath("//span[text()='Decline']");
        public readonly By AccDecAssgnMntFormAcceptBtn = By.XPath("//span[text()='Accept']");
        public readonly By CompleteAssessFormSubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By ConfirmFormCompleteAssessYesBtn = By.XPath("//span[text()='Yes' and @class='b-center']");
        public readonly By ConfirmFormDeclineAssessYesBtn = By.XPath("//span[text()='Yes' and @class='b-center']");
        public readonly By ConfirmFormRemoveAssessOkBtn = By.XPath("//span[text()='Ok']/ancestor::button[@class='blue-button1 ng-scope']");
        public readonly By ConfirmFormAddObsYesBtn = By.XPath("//span[text()='Yes' and @class='b-center']");
        public readonly By AddObsFormSearchBtn = By.Id("btnSearch");
        public readonly By AddObsFormCancelBtn = By.Id("btnCancelMain");
        public readonly By AddObsFormBackBtn = By.Id("btnBackMain");
        public readonly By AddObsFormNextBtn = By.Id("btnNextMain");
        public readonly By AddObsFormSubmitBtn = By.XPath("//span[text()='Submit']");

        // Charts

        // Check boxes

        // Date control
        public readonly By CompleteAssessFormDateControlExpandBtn = By.XPath("//span[@class='glyphicon glyphicon-calendar']/../..");
        public readonly By CompleteAssessFormDateControlTopMiddleBtn = By.XPath(string.Format("//strong[text()='{0}']", string.Format(DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture) + " " + DateTime.Now.Year.ToString())));

        // Labels
        public readonly By UserNameLbl = By.ClassName("userName");

        // Links
        public readonly By RoleLnk = By.XPath("//div[@class='dropdown userName']/a");

        // Select Elements
        public readonly By AddObsFormLearnerFacSelElem = By.Id("AddObLearnerFaculty");
        public readonly By AddObsFormLearnProgSelElem = By.XPath("AddObLearnerProgram");
        public readonly By AddObsFormStageSelElem = By.Id("addObStage");
        // These select elements represent the 10 different variations (5 single selects and 5 multli selects) of select elements that can show on this form above the 
        // "Based on this ovservation, overall" label
        public readonly By CompleteAssessFormGeneric1SelElem = By.Id("Context1");
        public readonly By CompleteAssessFormGeneric2SelElem = By.Id("Context2");
        public readonly By CompleteAssessFormGeneric3SelElem = By.Id("Context3");
        public readonly By CompleteAssessFormGeneric4SelElem = By.Id("Context4");
        public readonly By CompleteAssessFormGeneric5SelElem = By.Id("Context5");
        public readonly By CompleteAssessFormGeneric6SelElem = By.Id("AskNiravWhatTheMultiSelectIDsAre");
        public readonly By CompleteAssessFormGeneric7SelElem = By.Id("AskNiravWhatTheMultiSelectIDsAre");
        public readonly By CompleteAssessFormGeneric8SelElem = By.Id("AskNiravWhatTheMultiSelectIDsAre");
        public readonly By CompleteAssessFormGeneric9SelElem = By.Id("AskNiravWhatTheMultiSelectIDsAre");
        public readonly By CompleteAssessFormGeneric10SelElem = By.Id("AskNiravWhatTheMultiSelectIDsAre");

        // Tables       
        public readonly By PendingAcceptanceTblHdr = By.Id("PendingAcceptance"); 
        public readonly By PendingAcceptanceTbl = By.Id("PendingAcceptanceData");
        public readonly By PendingAcceptanceTblRowBody = By.XPath("//table[@id='PendingAcceptanceData']/.//tbody[@data-ng-repeat='observation in vm.pendingObservations.PendingAcceptanceRequests']"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load. This is used to wait for the table to load
        public readonly By AcceptedTblHdr = By.Id("Accepted");
        public readonly By AcceptedTbl = By.Id("AcceptedData");
        public readonly By AcceptedTblRowBody = By.XPath("//table[@id='AcceptedData']/.//tbody"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By ExpiredDeclinedTblHdr = By.Id("ExpiredRejected");
        public readonly By ExpiredDeclinedTbl = By.Id("ExpiredRejectedData");
        public readonly By ExpiredDeclinedTblRowBody = By.XPath("//table[@id='ExpiredRejectedData']/.//tbody[@data-ng-repeat='observation in vm.pendingObservations.ExpiredORRejected']"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By ArchivedObservationsTbl = By.XPath("//table[@id='ArchivedObservations']");
        public readonly By ArchivedObservationsTblRowBody = By.XPath("//table[@id='ArchivedObservations']/.//tbody"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By AddObsFormObsTblFacultyColHdr = By.XPath("//span[text()='Learner Name']");

        // Tabs
        public readonly By PendingObsTab = By.XPath("//li[@id='PendingObservations']/a");
        public readonly By ArchivedObsTab = By.XPath("//li[@id='ArchivedObservationsTab']/a");



        // Text boxes
        public readonly By CBDTab = By.XPath("//div[@id='ctl00_MainTabs']/descendant::span[text()='CBD']");
        public readonly By CompleteAssessFormDateTxt = By.XPath("//input[@type='text' and @name='Date']");
        public readonly By CompleteAssessFormFeedbackTxt = By.Id("Feedback");
        public readonly By AccDecAssgnMntFormCommentsTxt = By.Id("comments");
        public readonly By AddObsFormLearnerNameTxt = By.Id("AddObLearnerName");
        // These text boxes represent the 5 different variations of text boxes that can show on this form above the "Based on this ovservation, overall" label
        public readonly By CompleteAssessFormGeneric1Txt = By.Id("Context1freetext");
        public readonly By CompleteAssessFormGeneric2Txt = By.Id("Context2freetext");
        public readonly By CompleteAssessFormGeneric3Txt = By.Id("Context3freetext");
        public readonly By CompleteAssessFormGeneric4Txt = By.Id("Context4freetext");
        public readonly By CompleteAssessFormGeneric5Txt = By.Id("Context5freetext");

















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