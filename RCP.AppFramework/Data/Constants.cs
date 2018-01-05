using System.Configuration;

namespace RCP.AppFramework
{
    public static class Constants
    {

        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Constants));

        /// <summary>
        /// The connection string to the database of the Web Application
        /// </summary>
        public static readonly string SQLconnString = ConfigurationManager.AppSettings["SQLConnectionString"];

        /// <summary>
        /// The folder that contains the Selenium binaries (remote web browser drivers)
        /// </summary>
        //public  static readonly string SELENIUM_BIN_PATH = @"c:\Selenium\bin";

        public static string NarrativeObservation = "Narrative";

        public static string EPAObservation = "EPA/IM Observation";

        public static string EPAStage1_1 = "1.1 Performing preoperative assessments for ASA 1 or 2 patients who will be undergoing a minor scheduled surgical procedure";

        public static string EPAStage1_2 = "1.2 Preparing the operating room (OR) for minor scheduled surgical procedures for ASA 1 or 2 patients (elective)";

        public static string EPAStage1_3 = "1.3 Monitoring ASA 1 or 2 adult patients undergoing minor scheduled surgical procedures, under general or regional anesthesia";

        public static string EPAStage1_4 = "1.4 Performing the postoperative transfer of care of ASA 1 or 2 adult patients following minor surgical procedures, including postoperative orders";

        public static string ObsToolPartForm1 = "Part A: Direct Observation - Form 1";

        public static string UserLoginLearner1Login = "Learner1Login";

        public static string UserLoginObserver1Login = "Observer1Login";

        public static string UserLoginObserver2Login = "Observer2Login";

        public static string UserLoginProgAdmin1Login = "ProgAdmin1Login";

        public static string UserLoginProgDirector1Login = "ProgDirector1Login";

        public static string UserLoginLearnerIE1Login = "LearnerIE1Login";

        public static string UserLoginLearnerFF1Login = "LearnerFF1Login";

        public static string UserLoginLearnerCH1Login = "LearnerCH1Login";

        public static string UserFullNameLearner1FullName = "_TA_AStatic User_LR_001";

        public static string UserFullNameObserver1FullName = "_TA_AStatic User_OB_001";

        public static string UserFullNameObserver2FullName = "_TA_AStatic User_OB_002";

        public static string UserFullNameProgAdmin1FullName = "_TA_AStatic User_PA_001";

        public static string UserFullNameLearnerIE1FullName = "_TA_AStatic User_LR_IE_001";

        public static string UserFullNameLearnerFF1FullName = "_TA_AStatic User_LR_FF_001";

        public static string UserFullNameLearnerCH1FullName = "_TA_AStatic User_LR_CH_001";

        /// <summary>
        /// Represents the activities within the Mainport application. There are various different rules that govern the way credits are given per
        /// activity, and rules determining if the activity needs validation from a third party. I have named the activities as per the activity
        /// section, activity name, credit rules, and validation rules:
        /// 1. The section number precedes the activity name. 
        /// 2. The text "_FixedCredits(number)" is appended if the activity provides a fixed amount of credits, as opposed to entering the amount of credits
        /// yourself. 
        /// 3. The text "_CreditsDoubles/Tripled" is appended if the credits given for the activity are doubled for the amount of hours/articles.
        /// 4. The text "_IfNotAccreditedCutCreditsCreditsInHalf" is appended if the credits given for the activity are cut in half when the No radio button is 
        /// selected in the "Is Activity Accredited" section
        /// 5. ValidationRequired is appended if the activity requires validation regardless of the submitted date, cycle, etc.. Note that voluntary 
        /// users will not require this validation
        /// </summary>
        public enum MainportActivityTypes
        {
            Sec1_Conference_IfNotAccreditedCutCreditsInHalf,
            Sec1_JournalClub_IfNotAccreditedCutCreditsInHalf,
            Sec1_Rounds_IfNotAccreditedCutCreditsInHalf,
            Sec1_SmallGroupSession_IfNotAccreditedCutCreditsInHalf,
            Sec2_Fellowship_FixedCredits25,
            Sec2_FormalCourse_FixedCredits25,
            Sec2_PLPPersonalLearningProject_CreditsDoubled,  
            Sec2_Traineeship_CreditsDoubled,
            Sec2_Reading_FixedCredits1,   // Note the amount depends on what is chosen in the Please Select The Type Of Reading select element
            Sec2_BulkJournalReadingwithTranscript_ValidationRequired,
            Sec2_BulkOnlineReadingScanningwithTranscript_ValidationRequired,
            Sec2_PodcastAudio_FixedCredits0_5,
            Sec2_Video_FixedCredits0_5,
            Sec2_InternetSearching_FixedCredits0_5,
            Sec2_POEMS_FixedCredits0_25,
            Sec2_PosterViewing_FixedCredits0_5,
            Sec2_ClinicalPracticeGuidelineDevelopment_FixedCredits20,
            Sec2_QualityCarePatientSafetyCommittee_FixedCredits15,
            Sec2_CurriculumDevelopment_FixedCredits15,
            Sec2_ExaminationDevelopment_FixedCredits15,
            Sec2_PeerReview_FixedCredits15,
            Sec2_OtherSystemsLearningActivity_FixedCredits15,
            Sec3_PracticeAssessment_CreditsTripled, 
            Sec3_AccreditedSelfAssessmentPrograms_CreditsTripled, 
            Sec3_AccreditedSimulationActivities_CreditsTripled,
            Sec3_MultisourceFeedback_CreditsTripled,
            Sec3_DirectObservation_CreditsTripled,
            Sec3_ChartAuditandFeedback_CreditsTripled, 
            Sec3_FeedbackonTeaching_CreditsTripled,
            Sec3_AnnualPerformanceReview_CreditsTripled
        }

    }
}