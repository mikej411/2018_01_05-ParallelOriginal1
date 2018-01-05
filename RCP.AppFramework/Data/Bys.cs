namespace RCP.AppFramework
{
    /// <summary>
    /// Provides access to all known selenium "By"s
    /// "By"s provide a way to find a particular element on a page.
    /// </summary>
    public static class Bys
    {
        /// <summary>
        /// Locators to find elements on the skeleton of the RCP page. i.e. The menu items, header items and footer items.
        /// </summary>
        public static readonly RCPPageBys RCPPage = new RCPPageBys();

        /// <summary>
        /// Locators to find elements on the login page
        /// </summary>
        public static readonly LoginPageBys LoginPage = new LoginPageBys();

        public static readonly MyCPDActivitiesListPageBys MyCPDActivitiesListPage = new MyCPDActivitiesListPageBys();

        public static readonly MyDashboardPageBys MyDashboardPage = new MyDashboardPageBys();

        public static readonly MyHoldingAreaPageBys MyHoldingAreaPage = new MyHoldingAreaPageBys();

        public static readonly MyMOCPageBys MyMOCPage = new MyMOCPageBys();

        public static readonly EnterCPDActivityPageBys EnterCPDActivityPage = new EnterCPDActivityPageBys();

        public static readonly CBDLearnerPageBys CBDLearnerPage = new CBDLearnerPageBys();

        public static readonly CBDObserverPageBys CBDObserverPage = new CBDObserverPageBys();

        public static readonly CBDProgAdminPageBys CBDProgAdminPage = new CBDProgAdminPageBys();

        public static readonly CBDProgDirectorPageBys CBDProgDirectorPage = new CBDProgDirectorPageBys();

        public static readonly CBDProgDeanPageBys CBDProgDeanPage = new CBDProgDeanPageBys();

        public static readonly PERTraineePageBys PERTraineePage = new PERTraineePageBys();

        public static readonly PERCredentialStaffPageBys PERCredentialStaffPage = new PERCredentialStaffPageBys();

        public static readonly PERRefereePageBys PERRefereePage = new PERRefereePageBys();

        public static readonly PERAssessorPageBys PERAssessorPage = new PERAssessorPageBys();

        public static readonly DiplomaAssessorPageBys DiplomaAssessorPage = new DiplomaAssessorPageBys();

        public static readonly DiplomaTraineePageBys DiplomaTraineePage = new DiplomaTraineePageBys();

        public static readonly DiplomaCredentialStaffPageBys DiplomaCredentialStaffPage = new DiplomaCredentialStaffPageBys();

        public static readonly DiplomaClinicalSupervisorPageBys DiplomaClinicalSupervisorPage = new DiplomaClinicalSupervisorPageBys();

        public static readonly DiplomaDirectorPageBys DiplomaDirectorPage = new DiplomaDirectorPageBys();

        public static readonly DiplomaFacOfMedicinePageBys DiplomaFacOfMedicinePage = new DiplomaFacOfMedicinePageBys();

        
    }
}