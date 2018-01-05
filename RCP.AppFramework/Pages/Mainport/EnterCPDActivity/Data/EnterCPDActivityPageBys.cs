using OpenQA.Selenium;

namespace RCP.AppFramework
{
    public class EnterCPDActivityPageBys
    {
        // Buttons
        public readonly By AddFilesBtn = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_DocUpload1_RadUploadFilesfile0");
        public readonly By CancelBtn = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_btnCancel");
        public readonly By SendToHoldingBtn = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_btnSaveAndFinishLater");
        public readonly By ContinueBtn = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_btnSubmit");
        public readonly By OptionalTabSubmitBtn = By.Id("ctl00_ContentPlaceHolder1_btnSubmitOptional");
        public readonly By SupportingDocumentsTabSubmitBtn = By.Id("ctl00_ContentPlaceHolder1_btnSubmitForValidation");
        public readonly By CloseBtn = By.Id("ctl00_ContentPlaceHolder1_btnClose3");
        public readonly By CloseSecondInstanceBtn = By.Id("ctl00_ContentPlaceHolder1_btnClose4"); // There are multiple close button element instances depending on the activity you choose in the select element
        public readonly By CloseThirdInstanceBtn = By.Id("ctl00_ContentPlaceHolder1_btnClose2"); // There are multiple close button element instances depending on the activity you choose in the select element
        public readonly By CloseFourthInstanceBtn = By.Id("ctl00_ContentPlaceHolder1_btnClose1"); // There are multiple close button element instances depending on the activity you choose in the select element


        public readonly By SendToHoldingAreaBtn = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_btnSaveAndFinishLater");

        
        // Charts

        // Check boxes
        public readonly By IWillBesendingDocumentsChk = By.Id("ctl00_ContentPlaceHolder1_ctl00_chkDocumentsInMail"); 


        // frames
        public readonly By EnterACPDFrame = By.XPath("//iframe[@name='wndAddActivity']");

        // images
        public readonly By LoadingImg = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_fb1_progressFormImage"); // This is the little loading image that appears in the bottom let hand corner whenever we select an item in the first select element of this page, or when we click on the radio button button that select element 

        // Labels                                              
        public readonly By CreditsForActivityValueLbl = By.XPath("//td[contains(text(),'Credits for this activity')]/following-sibling::td/descendant::span");
        public readonly By SupportingDocumentsTabLbl = By.XPath("//span[text()='Supporting Documents']");



        // Links


        // Menu Items    

        // Radio buttons
        public readonly By IsActivityAccrYesRdo = By.XPath("//label[text()='Yes']/preceding-sibling::input");
        public readonly By IsActivityAccrNoRdo = By.XPath("//label[text()='No']/preceding-sibling::input");


        // scroll bars
        
        public readonly By ScrollBar = By.Id("TextToSelect");

        // select elements
        public readonly By Sec1GroupLearnActSelElem = By.XPath("//span[contains(text(),'Section 1 - Group Learning Activities')]/ancestor::tr[1]/descendant::select");
        public readonly By Sec2SelfLearnActSelElem = By.XPath("//span[contains(text(),'Section 2 - Self-Learning Activities')]/ancestor::tr[1]/descendant::select");
        public readonly By Sec3AssessActSelElem = By.XPath("//span[contains(text(),'Section 3 - Assessment Activities')]/ancestor::tr[1]/descendant::select");
        public readonly By PleaseSelectTheTypeOfProjectItWasSelElem = By.XPath("//span[contains(text(),'Please select the type of project it was')]/ancestor::tr[1]/descendant::select");
        public readonly By PleaseSelectTheTypeOfReadingSelElem = By.XPath("//span[contains(text(),'Please select the type of reading:')]/ancestor::tr[1]/descendant::select"); // This is for the Reading activity type
        public readonly By PleaseSelectTheTypeOfReadingActivitySelElem = By.XPath("//span[contains(text(),'Please select the type of reading activity:')]/ancestor::tr[1]/descendant::select"); // This is for the Bulk Journal Reading with Transcript activity type
        public readonly By SelectTheRelevantDomainSelElem = By.XPath("//span[contains(text(),'Select the relevant domain for this accredited SAP')]/ancestor::tr[1]/descendant::select");
        public readonly By SAPNameSelElem = By.XPath("//span[contains(text(),'SAP Name')]/ancestor::tr[1]/descendant::select");


        // Tables       

        // Tabs

        // Text boxes                              
        public readonly By HowManyHoursTxt = By.XPath("//span[contains(text(),'How many hours did you spend participating in this activity?')]/ancestor::tr[1]/td[2]/input");
        public readonly By NameTheGroupActivityTxt = By.XPath("//span[contains(text(),'Name the group activity you completed')]/ancestor::tr[1]/td[2]/textarea");
        public readonly By WhatDateTxt = By.XPath("//span[contains(@id, 'dateInput')]/input[1]");
        public readonly By WhatDidYouLearnTxt = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_txtLearn");
        public readonly By WhatAdditLearningTxt = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_txtAffirm");
        public readonly By WhatChangesTxt = By.Id("ctl00_ContentPlaceHolder1_AddEditExternalFormActivity1_txtPractice");
        public readonly By DescribeTheQuestionTxt = By.XPath("//span[contains(text(),'Describe the question, focus or title for this activity')]/ancestor::tr[1]/td[2]/textarea");
        public readonly By TotalNumberOfArticlesTxt = By.XPath("//span[contains(text(),'Total number of articles read')]/ancestor::tr[1]/td[2]/input");
        public readonly By TypeOfPLPTxt = By.XPath("//span[contains(text(),'Type of PLP:')]/ancestor::tr[1]/td[2]/textarea"); // This appears after "Other - Please describe what type of PLP" is selcted in the Please Select The Type Of Project select element for the PLP activity

        

    }
}