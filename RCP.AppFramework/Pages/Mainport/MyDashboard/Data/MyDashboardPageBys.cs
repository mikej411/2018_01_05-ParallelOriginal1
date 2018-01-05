using OpenQA.Selenium;

namespace RCP.AppFramework
{
    public class MyDashboardPageBys
    {
        // Buttons
        public readonly By EnterACPDActivityBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        public readonly By AddAGoalBtn = By.XPath("//a[@id='ctl00_ContentPlaceHolder1_PerformanceGoals1_lnkAddAGoal']/span");
        public readonly By CreateAGoalFormNextBtnTxt = By.Id("ctl00_ContentPlaceHolder1_ctrlGoalsWizard_btnNext");
        public readonly By CreateAGoalFormCloseBtn = By.Id("ctl00_ContentPlaceHolder1_ctrlGoalsWizard_btnCancel");
        public readonly By ViewMoreBtn = By.Id("ctl00_ContentPlaceHolder1_SelfReportedActivities_InComplete1_lnkViewAll");


        // Charts

        // Check boxes

        // Date control

        // Frames

        public readonly By CreateAGoalFrame = By.XPath("//iframe[@name='rwAddGoalsData']");

        // Labels
        public readonly By TotalCreditsSubmittedValueLbl = By.XPath("//span[text()='Total Credits Submitted: ']/ancestor::div[1]/span[2]");
        public readonly By TotalCreditsAppliedValueLbl = By.XPath("(//span[text()='Total Credits Applied: '])[2]/ancestor::div[1]/span[2]");
        public readonly By MOCSectionReqsGraphGroupLearningCreditLbl = By.Id("ctl00_ContentPlaceHolder1_lblGroupLearningValues");
        public readonly By MOCSectionReqsGraphSelfLearningCreditLbl = By.Id("ctl00_ContentPlaceHolder1_lblSelfLearningValues");
        public readonly By MOCSectionReqsGraphAssessmentCreditLbl = By.Id("ctl00_ContentPlaceHolder1_lblAssessmentValues");
        public readonly By MOCSectionReqsGraphGroupLearningTickBox = By.Id("glTickBox"); //Whenever a user completes 25 out of 25 credits, this element will appear, which is a check box inside the square graph looking elements on this page
        public readonly By MOCSectionReqsGraphSelfLearningTickBox = By.Id("slTickBox"); //Whenever a user completes 25 out of 25 credits, this element will appear, which is a check box inside the square graph looking elements on this page
        public readonly By MOCSectionReqsGraphAssessmentTickBox = By.Id("assessmentTickBox"); //Whenever a user completes 25 out of 25 credits, this element will appear, which is a check box inside the square graph looking elements on this page


        // Links


        // Radio Buttons


        // Random
        public readonly By MOCSectionReqsGraphGroupLearningSquare = By.Id("groupLearningBox"); // This is 1 of the three square graph looking elements on the page representing the MOC Section Requirement amount of credits
        public readonly By MOCSectionReqsGraphSelfLearningSquare = By.Id("selfLearningBox"); // This is 1 of the three square graph looking elements on the page representing the MOC Section Requirement amount of credits
        public readonly By MOCSectionReqsGraphAssessmentSquare = By.Id("assessmentBox"); // This is 1 of the three square graph looking elements on the page representing the MOC Section Requirement amount of credits


        // Select Elements


        // Scripts

        // Tables   
        public readonly By GoalsTbl = By.Id("ctl00_ContentPlaceHolder1_PerformanceGoals1_grdGoals_ctl00");
        public readonly By GoalsTblBody = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_PerformanceGoals1_grdGoals_ctl00']/tbody");
        public readonly By MyHoldingAreaTbl = By.Id("ctl00_ContentPlaceHolder1_SelfReportedActivities_InComplete1_grdActivityListing_ctl00");
        public readonly By MyHoldingAreaTblBody = By.XPath("//table[@id='ctl00_ContentPlaceHolder1_SelfReportedActivities_InComplete1_grdActivityListing_ctl00']/tbody");


        // Tabs

        // Text boxes
        public readonly By CreateAGoalFormWhatIsThisGoalTxt = By.Id("ctl00_ContentPlaceHolder1_ctrlGoalsWizard_ctrlCreateGoal_txtName");
        public readonly By CreateAGoalFormHowWillYouTxt = By.Id("ctl00_ContentPlaceHolder1_ctrlGoalsWizard_ctrlCreateGoal_txtAccomplish");
        public readonly By CreateAGoalFormDateTxt = By.Id("ctl00_ContentPlaceHolder1_ctrlGoalsWizard_ctrlCreateGoal_radDatePickerTargetDate_dateInput");




    }
}