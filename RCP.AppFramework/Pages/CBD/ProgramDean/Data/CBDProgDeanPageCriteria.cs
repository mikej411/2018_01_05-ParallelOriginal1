using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login to the
    /// Learner role page, we need to wait for the circular load icon to disappear and for the table below the tabs to appear.
    /// </summary>
    public class CBDProgDeanPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met for the main prog admin page, when the learners table is present. <see cref="PageReady"/>
        /// at the bottom of this class. That is where these specific criteria are combined
        /// </summary>
        public readonly ICriteria<CBDProgDeanPage> LoadElementClassAttributeSetToHide = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon class attribute value set to \"ng-hide\"");        

        public readonly ICriteria<CBDProgDeanPage> LoadElementDisappeared = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.RCPPage.SplashPage, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<CBDProgDeanPage> LearnersTblBodyRowEnabled = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.CBDProgDeanPage.LearnersTblBodyRow);
        }, "Learners table body row 1 enabled");

        public readonly ICriteria<CBDProgDeanPage> LearnersTblBodyRowVisibleAndEnabled = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.CBDProgDeanPage.LearnersTblBodyRow, ElementCriteria.IsEnabled);
        }, "Learners table body row 1 visible and enabled");

        // This waits until the Learners tab text is equal to something other than zero. Whenever the page appears, it is first set to zero.
        // After it loads fully, it then gets set to the amount of learners in the table
        public readonly ICriteria<CBDProgDeanPage> LearnersTabTextNotEqualToZero = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.CBDProgDeanPage.LearnersTab, ElementCriteria.TextNotEquals("Learners (0)"));
        }, "Learners tab text not equal to \"Learners (0)\"");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<CBDProgDeanPage> ApprovalsTblBodyRowVisibleAndEnabled = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.CBDProgDeanPage.ApprovalsTblBodyRow, ElementCriteria.IsEnabled);
        }, "Approvals table body row 1 visible and enabled");

        public readonly ICriteria<CBDProgDeanPage> ProgramSelElemHasItems = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.CBDProgDeanPage.ProgramSelElem, ElementCriteria.SelectElementHasItems);
        }, "Program select element has items");

        public readonly ICriteria<CBDProgDeanPage> CBDTabVisibleAndEnabled = new Criteria<CBDProgDeanPage>(p =>
        {
            return p.Exists(Bys.RCPPage.CBDTab, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "CBD tab visible and enabled");


        /// <summary>
        /// 
        /// </summary>
        public readonly ICriteria<CBDProgDeanPage> PageReady;
        public readonly ICriteria<CBDProgDeanPage> LoadElementDoneLoading;
        public CBDProgDeanPageCriteria()
        {
            LoadElementDoneLoading = LoadElementClassAttributeSetToHide.AND(LoadElementDisappeared);
            PageReady = LoadElementDoneLoading.AND(ProgramSelElemHasItems);
        }
    }
}
