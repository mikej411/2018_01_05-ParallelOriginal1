namespace RCP.AppFramework
{
    /// <summary>
    /// Provides access to all the known "Criteria" for the the application.
    /// Criteria are typically used when waiting for elements.  I often wait until some
    /// "Criteria" is met before continuing with a test.
    /// </summary>
    public static class Criteria
    {
        public static readonly LoginPageCriteria LoginPage = new LoginPageCriteria();

        public static readonly MyCPDActivitiesListPageCriteria MyCPDActivitiesListPage = new MyCPDActivitiesListPageCriteria();

        public static readonly MyDashboardPageCriteria MyDashboardPage = new MyDashboardPageCriteria();

        public static readonly MyMOCPageCriteria MyMOCPagePage = new MyMOCPageCriteria();

        public static readonly MyHoldingAreaPageCriteria MyHoldingAreaPage = new MyHoldingAreaPageCriteria();

        public static readonly MyMOCPageCriteria MyMOCPage = new MyMOCPageCriteria();

        public static readonly EnterCPDActivityPageCriteria EnterCPDActivityPage = new EnterCPDActivityPageCriteria();

        public static readonly CBDLearnerPageCriteria CBDLearnerPage = new CBDLearnerPageCriteria();

        public static readonly CBDObserverPageCriteria CBDObserverPage = new CBDObserverPageCriteria();

        public static readonly CBDProgAdminPageCriteria CBDProgAdminPage = new CBDProgAdminPageCriteria();

        public static readonly CBDProgDirectorPageCriteria CBDProgDirectorPage = new CBDProgDirectorPageCriteria();

        public static readonly CBDProgDeanPageCriteria CBDProgDeanPage = new CBDProgDeanPageCriteria();

        public static readonly PERTraineePageCriteria PERTraineePage = new PERTraineePageCriteria();

        public static readonly PERCredentialStaffPageCriteria PERCredentialStaffPage = new PERCredentialStaffPageCriteria();

        public static readonly PERRefereePageCriteria PERRefereePage = new PERRefereePageCriteria();

        public static readonly PERAssessorPageCriteria PERAssessorPage = new PERAssessorPageCriteria();

        public static readonly DiplomaAssessorPageCriteria DiplomaAssessorPage = new DiplomaAssessorPageCriteria();

        public static readonly DiplomaTraineePageCriteria DiplomaTraineePage = new DiplomaTraineePageCriteria();

        public static readonly DiplomaCredentialStaffPageCriteria DiplomaCredentialStaffPage = new DiplomaCredentialStaffPageCriteria();

        public static readonly DiplomaClinicalSupervisorPageCriteria DiplomaClinicalSupervisorPage = new DiplomaClinicalSupervisorPageCriteria();

        public static readonly DiplomaDirectorPageCriteria DiplomaDirectorPage = new DiplomaDirectorPageCriteria();

        public static readonly DiplomaFacOfMedicinePageCriteria DiplomaFacOfMedicinePage = new DiplomaFacOfMedicinePageCriteria();



    }
}