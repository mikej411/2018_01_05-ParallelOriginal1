using OpenQA.Selenium;

namespace RCP.AppFramework
{
    public class MyMOCPageBys
    {
        // Buttons
        public readonly By EnterACPDActivityBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        public readonly By ViewActivitiesFormCloseBtn = By.Id("ctl00_ContentPlaceHolder1_ViewExternalActivitiesByRequirement1_btnClose"); // This is on the form that pops up whenever you click on the View link inside an activity group table
        public readonly By ViewActivitiesFormXBtn = By.XPath("(//a[@class='rwCloseButton'])[2]");
        public readonly By DeleteActivityWarningFormYesBtn = By.Id("ctl00_ContentPlaceHolder1_ViewExternalActivitiesByRequirement1_btnYes");

        
        // Charts

        // Check boxes

        // Date control

        // Frames
        public readonly By ViewActivitiesFormFrame = By.XPath("//iframe[@name='wndViewActivityList']");

        

        // Labels     
        public readonly By GroupLearnTblCreditsAppliedValueLbl = By.Id("ctl00_ContentPlaceHolder1_Part1_lblCreditsEarnedToDate");
        public readonly By GroupLearnTblYouHaveMetMinCredsLbl = By.Id("ctl00_ContentPlaceHolder1_LblGroupLearnMinReqMet");
        public readonly By GroupLearnTblYouHaveNotMetMinCredsLbl = By.Id("ctl00_ContentPlaceHolder1_LblGroupLearnMinReqNotMet");
        public readonly By GroupLearnTblAccrActRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part1_Repeater_ctl00_lblCreditsEarned");
        public readonly By GroupLearnTblAccrActRowLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part1_Repeater_ctl00_lblLastUpdateDate");
        public readonly By GroupLearnTblUnaccrActRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part1_Repeater_ctl01_lblCreditsEarned");
        public readonly By GroupLearnTblUnaccrActRowLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part1_Repeater_ctl01_lblLastUpdateDate");
        // If the IDs ever change for the above labels, which means they would be dynamic, use the below type of Xpath instead
        //public readonly By GroupLearnTblAccrActRowCredsRptLbl = By.XPath("//span[text()='Accredited Activities']/ancestor::tr[1]/descendant::span[3]");

        public readonly By SelfLearningTblCreditsAppliedValueLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_lblCreditsEarnedToDate");
        public readonly By SelfLearningTblYouHaveMetMinCredsLbl = By.Id("ctl00_ContentPlaceHolder1_LblSelfLearnMinReqMet");
        public readonly By SelfLearningTblYouHaveNotMetMinCredsLbl = By.Id("ctl00_ContentPlaceHolder1_LblSelfLearnMinReqNotMet");
        public readonly By SelfLearningTblPlanLearnActRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_Repeater_ctl00_lblCreditsEarned");
        public readonly By SelfLearningTblPlanLearnActRowLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_Repeater_ctl00_lblLastUpdateDate");
        public readonly By SelfLearningTblScanActRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_Repeater_ctl01_lblCreditsEarned");
        public readonly By SelfLearningTblScanActRowLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_Repeater_ctl01_lblLastUpdateDate");
        public readonly By SelfLearningTblSysLearnActRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_Repeater_ctl02_lblCreditsEarned");
        public readonly By SelfLearningTblSysLearnActRowLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part2_Repeater_ctl02_lblLastUpdateDate");
        // If the IDs ever change for the above labels, which means they would be dynamic, use the below type of Xpath instead
        //public readonly By SelfLearningTblPlanLearnActRowCredsRptLbl = By.XPath("//span[text()='Planned Learning Activities']/ancestor::tr[1]/descendant::span[2]");

        public readonly By AssessmentTblCreditsAppliedValueLbl = By.Id("ctl00_ContentPlaceHolder1_Part3_lblCreditsEarnedToDate");
        public readonly By AssessmentTblYouHaveMetMinCredsLbl = By.Id("ctl00_ContentPlaceHolder1_LblAssessmentMinReqMet");
        public readonly By AssessmentTblYouHaveNotMetMinCredsLbl = By.Id("ctl00_ContentPlaceHolder1_LblAssessmentMinReqNotMet");
        public readonly By AssessmentTblKnowledgeAssRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part3_Repeater_ctl00_lblCreditsEarned");
        public readonly By AssessmentTblKnowledgeAssRowLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part3_Repeater_ctl00_lblLastUpdateDate");
        public readonly By AssessmentTblPerformanceAssRowCredsRptLbl = By.Id("ctl00_ContentPlaceHolder1_Part3_Repeater_ctl01_lblCreditsEarned");
        public readonly By AssessmentTblPerformanceAssLastUpLbl = By.Id("ctl00_ContentPlaceHolder1_Part3_Repeater_ctl01_lblLastUpdateDate");
        // If the IDs ever change for the above labels, which means they would be dynamic, use the below type of Xpath instead
        //public readonly By AssessmentTblKnowledgeAssRowCredsRptLbl = By.XPath("//span[text()='Knowledge Assessment']/ancestor::tr[1]/descendant::span[3]");

        public readonly By OverallCreditsAppliedLbl = By.Id("ctl00_ContentPlaceHolder1_lblOverallReqValue");


        // Links
        public readonly By GroupLearnTblAccrActRowViewLnk = By.XPath("//span[text()='Accredited Activities']/ancestor::tr[1]/descendant::a");
        public readonly By GroupLearnTblUnaccrActRowViewLnk = By.XPath("//span[text()='Unaccredited Activities']/ancestor::tr[1]/descendant::a");
        public readonly By SelfLearningTblPlanLearnActRowViewLnk = By.XPath("//span[text()='Planned Learning Activities']/ancestor::tr[1]/descendant::a");
        public readonly By SelfLearningTblScanActRowViewLnk = By.XPath("//span[text()='Scanning Activities']/ancestor::tr[1]/descendant::a");
        public readonly By SelfLearningTblSysLearnActRowViewLnk = By.XPath("//span[text()='Systems Learning Activities']/ancestor::tr[1]/descendant::a");
        public readonly By AssessmentTblKnowledgeAssRowViewLnk = By.XPath("//span[text()='Knowledge Assessment']/ancestor::tr[1]/descendant::a");
        public readonly By AssessmentTblPerformanceAssRowViewLnk = By.XPath("//span[text()='Performance Assessment']/ancestor::tr[1]/descendant::a");

        // Radio Buttons


        // Select Elements


        // Scripts

        // Tables   
        public readonly By ViewActivitiesFormActivitiesTbl = By.Id("ctl00_ContentPlaceHolder1_ViewExternalActivitiesByRequirement1_ExternalActivitiesGrid_ctl00"); // This is on the form that pops up whenever you click on the View link inside an activity group table
        public readonly By ViewActivitiesFormActivitiesTblThead = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_ViewExternalActivitiesByRequirement1_ExternalActivitiesGrid_ctl00']/thead"); 
        public readonly By ViewActivitiesFormActivitiesTblBody = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_ViewExternalActivitiesByRequirement1_ExternalActivitiesGrid_ctl00']/tbody");
        public readonly By ViewActivitiesFormActivitiesTblBodyRow = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_ViewExternalActivitiesByRequirement1_ExternalActivitiesGrid_ctl00']/tbody/tr"); // If one row exists, this will represent that row

        

        // Tabs

        // Text boxes
    }
}