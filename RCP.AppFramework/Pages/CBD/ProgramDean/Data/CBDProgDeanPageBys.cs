using OpenQA.Selenium;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the prog admin role page
    /// </summary>
    public class CBDProgDeanPageBys
    {
        // Buttons
        public readonly By AddSupportingDocumentationFormSubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By AddNotesFormSubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By AddRemoveFlagFormRemoveFlagBtn = By.XPath("//span[text()='Remove Flag']");
        public readonly By AddRemoveFlagFormSaveFlagBtn = By.XPath("//span[text()='Save Flag']");
        public readonly By AddSupportingDocumentationFormBrowseBtn = By.XPath("//span[text()='Browse']");


        // Charts


        // Check boxes

        
        // Labels


        // Links
  


        // Radio buttons


        // Select Elements
        public readonly By ProgramSelElem = By.Id("program");


        // Tables       
        public readonly By LearnersTbl = By.Id("tblLearnersMain");
        public readonly By LearnersTblBody = By.Id("tblLearnersContent"); 
        public readonly By LearnersTblBodyRow = By.XPath("//table[@id='tblLearnersContent']/tbody"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load. Change to id when DEV adds it
        public readonly By ApprovalsTbl = By.Id("tblPendingApprovalsContent");
        public readonly By ApprovalsTblBody = By.Id("tblPendingApprovalsContentDetail"); 
        public readonly By ApprovalsTblBodyRow = By.XPath("//table[@id='tblPendingApprovalsContentDetail']/tbody"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load. Change to id when DEV adds it
        public readonly By AwarenessTbl = By.Id("tblPendingAwarenessContent"); // 
        public readonly By AwarenessTblBody = By.Id("tblPendingAwarenessContentDetail"); // 
        public readonly By AwarenessTblBodyRow = By.XPath("//table[@id='tblPendingAwarenessContentDetail']/tbody"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load. Change to id when DEV adds it


        // Tabs
        public readonly By CBDHomeTab = By.XPath("//span[text()='CBD Home']");
        public readonly By LearnersTab = By.XPath("//li[@id='Learners']/a");
        public readonly By PendingAwareAndAppTab = By.XPath("//li[@id='PendingStatusApprovals']/a");
        public readonly By ArchivedAwareAndAppTab = By.XPath("//li[@id='ArchievedStatusApprovals']/a");



        // Text boxes
        public readonly By AddSupportingDocumentationFormFilelocationTxt = By.Id("FileLocation");
        public readonly By AddNotesFormSubjectTxt = By.Id("NoteSubject");
        public readonly By AddNotesFormNotesTxt = By.Id("txtNotes");
        public readonly By AddRemoveFlagFormReasonTxt = By.Id("addRemoveFlagComments");


    }
}