namespace RCP.AppFramework
{
    #region Program Admin and Program Director form objects

    /// <summary>
    /// Represents what is chosen when a prog admin or dorector fills the fields on the Add Observation window and completes an observation
    /// </summary>
    public class AssignedObservationInfo
    {
        public string LrName { get; set; }
        public string ObsType { get; set; }
        public string Stage { get; set; }
        public string Epa { get; set; }
        public string ObsTool { get; set; }
        public string[] ObsNames { get; set; }

        public AssignedObservationInfo(string lrName, string obsType, string stage, string epa, string obsTool, params string[] obsNames)
        {
            LrName = lrName;
            ObsType = obsType;
            Stage = stage;
            Epa = epa;
            ObsTool = obsTool;
            ObsNames = obsNames;

        }
    }

    #endregion Program Admin and Program Director form objects

    #region Learner page form objects

    /// <summary>
    /// Represents what is chosen on the Add Reflection window from the learner page
    /// </summary>
    /// TODO: Add properties for the Upload Evidence tab
    public class LearnerRelectionObject
    {
        public string ReflectionTitle { get; set; }
        public bool SharingOption { get; set; }
        string Stimulus { get; set; }
        public string RelflectionDesc { get; set; }

        public LearnerRelectionObject(string reflectionTitle, bool sharingOption, string stimulus, string relflectionDesc)
        {
            ReflectionTitle = reflectionTitle;
            SharingOption = sharingOption;
            Stimulus = stimulus;
            RelflectionDesc = relflectionDesc;

        }
    }

    /// <summary>
    /// Represents what is chosen on the Request Observation window from the learner page
    /// </summary>
    /// TODO: Add properties for the Upload Evidence tab
    public class LearnerRequestObsObject
    {
        public string Observer { get; set; }
        public string Template { get; set; }

        public LearnerRequestObsObject(string observer, string template)
        {
            Observer = observer;
            Template = template;
        }
    }

    #endregion Learner page form objects

    #region Observer page form objects

    /// <summary>
    /// Represents what is chosen on the Accept/Decline Assignment window from the Observer page
    /// </summary>
    public class AcceptDeclineAssignmentFormInfo
    {
        public string Comments { get; set; }
        public AcceptDeclineAssignmentFormInfo(string comments)
        {
            Comments = comments;
        }
    }

    /// <summary>
    /// Represents what is chosen on the Complete Assessment window from the Observer page. TODO: I am just using required fields
    /// for now. I will have to add properties for all fields as we continue more testing
    /// </summary>
    public class CompletedAssessmentInfo
    {
        public string DateLearnerWasObserved { get; set; }
        public string RatingText { get; set; }
        public int RatingValue { get; set; }
        public string Feedback { get; set; }
        public string DateObservationWasCompletedInSystem { get; set; }

        public CompletedAssessmentInfo(string dateLearnerWasObserved, string ratingText, int ratingValue, string feedback, string dateObservationWasCompletedInSystem)
        {
            DateLearnerWasObserved = dateLearnerWasObserved;
            RatingText = ratingText;
            RatingValue = ratingValue;
            Feedback = feedback;
            DateObservationWasCompletedInSystem = dateObservationWasCompletedInSystem;
        }
    }

    /// <summary>
    /// Represents what is chosen when an observer fills the fields on the Add Observation window and completes an observation
    /// </summary>
    public class AddedObservationInfo
    {
        public string LearnerName { get; set; }
        public string EPAStage { get; set; }
        public string ObservationTool { get; set; }
        public CompletedAssessmentInfo CAO { get; set; }

        public AddedObservationInfo(string learnerName, string ePAStage, string observationTool, CompletedAssessmentInfo cao)
        {
            LearnerName = learnerName;
            EPAStage = ePAStage;
            ObservationTool = observationTool;
            CAO = cao;
        }
    }

    #endregion Observer page form objects
}