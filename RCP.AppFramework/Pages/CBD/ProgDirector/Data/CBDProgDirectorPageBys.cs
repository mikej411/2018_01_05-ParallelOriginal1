using OpenQA.Selenium;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the prog admin role page
    /// </summary>
    public class CBDProgDirectorPageBys
    {
        // Buttons
        public readonly By AssignObsFormNextBtn = By.Id("btnNext");
        public readonly By AssignObsFormSearchBtn = By.Id("btnSearch");
        public readonly By AssignObsFormAddBtn = By.Id("btnAdd");
        public readonly By AssignObsFormAssignBtn = By.Id("btnAssign");
        public readonly By AssignObsFormBackBtn = By.Id("btnBack");
        public readonly By ActionsBtn = By.XPath("//span[text()='Actions']");
        public readonly By FinalizeAgendaBtn = By.XPath("//span[text()='Finalize Agenda']");
        public readonly By CreateNewAgendaFormCreateBtn = By.XPath("//span[text()='Create']");
        public readonly By FinalizeAgendaFormFinalizeBtn = By.XPath("//span[text()='Finalize']");
        public readonly By SetStatusFormConfirmBtn = By.XPath("//span[text()='Confirm Status']");
        public readonly By AddSupportingDocumentationFormSubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By AddNotesFormSubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By SchedProgMeetFormScheduleBtn = By.XPath("//span[text()='Schedule']");
        public readonly By AddRemoveFlagFormRemoveFlagBtn = By.XPath("//span[text()='Remove Flag']");
        public readonly By AddRemoveFlagFormSaveFlagBtn = By.XPath("//span[text()='Save Flag']");
        public readonly By AddSupportingDocumentationFormBrowseBtn = By.XPath("//span[text()='Browse']");


        // Charts


        // Check boxes
        public readonly By SchedProgMeetFormRecurChk = By.Name("IsRecurring");

        
        // Labels
        public readonly By CreateNewAgendaFormMaxNumOfAgendasLbl = By.XPath("//span[text()='The maximum number of Competence Committee Agendas has been reached.']");


        // Links
        public readonly By ExportAgendaLnk = By.XPath("//span[text()='Export Agenda']");
        public readonly By CreateNewAgendaLnk = By.XPath("//span[text()='Create New Agenda']");
        


        // Radio buttons


        // Select Elements
        public readonly By AssignObsFormStageSelElem = By.Id("Stage");
        public readonly By CreateNewAgendaFormProgramSelElem = By.Id("CAInstitution");
        public readonly By CreateNewAgendaFormFaculMedSelElem = By.Id("CAprogram");
        public readonly By ProgramSelElem = By.Id("program");
        public readonly By FacultyOfMedSelElem = By.Id("FacultyOfMedicine");
        public readonly By MeetingAgendaSelElem = By.Id("MeetingAgenda");
        public readonly By SetStatusFormLearnerStatusSelElem = By.Id("LearnerStatus");
        public readonly By SetStatusFormActionsSelElem = By.Id("actions");
        public readonly By SchedProgMeetFormRecurringSelElem = By.Name("Recurring");


        // Tables       
        public readonly By LearnersTbl = By.Id("ProgramSnapshotLearners");
        public readonly By LearnersTblRowBody = By.XPath("//table[@id='ProgramSnapshotLearners']/descendant::tbody[@class='ng-scope']"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By AgendaTbl = By.XPath("//table"); // Need DEV to ID this table. Right now we will use this xpath, which is not good
        public readonly By AgendaTblRowBody = By.XPath("//table/tbody[2]"); // Need DEV to ID this table. Right now we will use this xpath, which is not good



        // Tabs
        public readonly By CBDHomeTab = By.XPath("//span[text()='CBD Home']");
        public readonly By CompCommiteeTab = By.XPath("//span[text()='Competence Committee']");
        public readonly By LearnersTab = By.XPath("//li[@id='Learners']/a");
        public readonly By ProgramOffiliatedObsTab = By.XPath("//li[@id='Observers']/a");
        public readonly By MSFAssignmentsTab = By.XPath("//li[@id='MSFAssignments']/a");
        public readonly By AgendaTab = By.XPath("//li[@id='Agenda']/a");
        public readonly By PresentersTab = By.XPath("//li[@id='Presenters']/a");


        // Text boxes
        public readonly By AssignObsFormObserverNameTxt = By.Id("ObserverName");
        public readonly By AddSupportingDocumentationFormFilelocationTxt = By.Id("FileLocation");
        public readonly By AddNotesFormSubjectTxt = By.Id("NoteSubject");
        public readonly By AddNotesFormNotesTxt = By.Id("txtNotes");
        public readonly By SchedProgMeetFormSubjectTxt = By.Id("Subject");
        public readonly By AddRemoveFlagFormReasonTxt = By.Id("addRemoveFlagComments");


        


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