using OpenQA.Selenium;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the learner role page
    /// </summary>
    public class CBDLearnerPageBys
    {
        // Buttons
        public readonly By BackBtn = By.XPath("//Span[contains(., 'Back')]");
        public readonly By AddReflectBtn = By.XPath("//span[text()='Add Reflection']");
        public readonly By UploadEvidenceBtn = By.XPath("//span[text()='Upload Evidence']");
        public readonly By RequestObservationBtn = By.XPath("//span[text()='Request Observation']");
        public readonly By AddReflectFormContinueBtn = By.XPath("//span[text()='Continue']");
        public readonly By AddReflectFormSubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By AddReflectFormBrowseBtn = By.Id("btnBrowse");
        public readonly By AddReflectFormHiddenBrowseBtn = By.Id("markerDocUploader");
        public readonly By AddReflectFormBackBtn = By.XPath("//span[text()='Back']");
        public readonly By TableFirstBtn = By.XPath("//a[@aria-label='First']"); // This is a generic element that can be used for any table
        public readonly By TableNextBtn = By.XPath("//a[@aria-label='Next']"); // This is a generic element that can be used for any table
        public readonly By RequestObsFormSearchBtn = By.XPath("//span[text()='Search']");
        public readonly By RequestObsFormOkBtn = By.XPath("//span[text()='Ok']/ancestor::button[@class='blue-button1 ng-scope']");
        public readonly By RequestObsFormCancelBtn = By.XPath("//button[@class='blue-button1 ng-scope'][2]/span[2]");
        public readonly By RequestObsFormRequestBtn = By.XPath("//span[text()='Request']");
        public readonly By ReportsFormShowBtn = By.Id("btnShow");
        public readonly By ReportsFormCloseBtn = By.Id("btnClose");

        // Charts
        public readonly By EPAObservCntChrt = By.Id("EPAChart");
        public readonly By ReportsFormEPAObservCntChrt = By.Id("EPAChartTitle");
        public readonly By ReportsFormEPAAttainmentChrt = By.Id("EPAAttainmentGraph");
        public readonly By ReportsFormEPAProgressionOverTimeChrt = By.Id("EPAProgressionOverTimeGraph");

        // Check boxes
        public readonly By HideAchievedChk = By.XPath("//input[@name='hide']");

        // Labels
        public readonly By UserNameLbl = By.ClassName("userName");
        public readonly By ShowingLbl = By.XPath("//span[text()='Showing']/..");
        public readonly By LearnerStatusLbl = By.XPath("//span[text()='Learner Status']");
        public readonly By CurrentStageValueLbl = By.XPath("//span[text()='Current Stage']/../following-sibling::div[1]"); // First locating the static label Current Stage, then the parent of that, then the sibling with a div tag, first indexed div tag.

        // Links
        public readonly By RoleLnk = By.XPath("//div[@class='dropdown userName']/a");
        public readonly By ViewMoreRptsLnk = By.LinkText("View More Reports");

        // Radio buttons
        public readonly By RequestObsFormPartARdo = By.XPath("//label[text()='Part A: Direct observation - Form 1']");
        public readonly By RequestObsFormPartBRdo = By.XPath("//label[text()='Part B: Chart review - Form 1']");
        public readonly By RequestObsFormPartCRdo = By.XPath("//label[text()='Part C: Logbook - Form 1']");
        public readonly By RequestObsFormFirstRdo = By.XPath("//tbody[@aria-label='Select Observer']/descendant::input"); // This represents the first radio button in the list of radio buttons from the Observer Name column after the user clicks search on the Request Observation form

        // Select Elements
        public readonly By ReportsFormReportSelElem = By.Id("report");

        // Tables       
        public readonly By EPAIMTbl = By.Id("PortfolioEPAs");       
        public readonly By GenericTable = By.TagName("table");
        // QA is waiting on IDs from DEV for the following tables. Until then, we will just use the tag of the HTML
        public readonly By ReflectionsTbl = By.TagName("table");
        public readonly By ReflectionsTblBdy = By.XPath("//table/tbody");
        public readonly By AssessmentDetailsTbl = By.TagName("table");
        public readonly By RequestObsFormRdoBtnTbody = By.XPath("//tbody[@aria-label='Select Observer']"); // the table that contains the radio buttons on this form
        public readonly By RequestObsFormObsTblFacultyColHdr = By.XPath("//span[text()='Faculty Affiliation']");

        // Tabs
        public readonly By PrgLrnPlanTab = By.XPath("//li[@id='learnerEPAsByStage']/a");
        public readonly By ReflectionsTab = By.XPath("//li[@id='learnerReflections']/a");
        public readonly By NarrativesTab = By.XPath("//li[@id='learnerNarratives']/a");
        public readonly By SupportingDocsTab = By.XPath("//li[@id='learnerSupportingDocuments']/a");
        public readonly By AssessDetailsTab = By.XPath("//li[@id='epaAssessmentDetails']/a");
        public readonly By ObservationsTab = By.XPath("//li[@id='epaObservations']/a");
        public readonly By EvidenceTab = By.XPath("//li[@id='epaEvidences']/a");

        // Text boxes
        public readonly By AddReflectFormReflectionTitleTxt = By.Id("ReflectionTitle");
        public readonly By AddReflectFormStimulusTxt = By.Id("Stimulus");
        public readonly By AddReflectFormReflectionDescriptionTxt = By.XPath("//textarea[@id='Reflection']");
        public readonly By RequestObsFormObsNameTxt = By.Id("ObserverName");
        public readonly By AddReflectFormBrowseToAddTxt = By.XPath("//input[@value='Browse to add Attachment']");












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