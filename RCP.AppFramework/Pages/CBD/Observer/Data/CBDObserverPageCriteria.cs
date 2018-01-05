using Browser.Core.Framework;

namespace RCP.AppFramework
{
    /// <summary>
    /// A set of criteria that we define that needs to be met for the test to proceed. For example on this page, when we login
    /// we need to wait for the circular load icon to disappear and for the table below the tabs to appear.
    /// </summary>
    public class CBDObserverPageCriteria
    {
        /// <summary>
        /// This is the criteria that needs to be met for elements within the main page of the observer role section 
        /// <see cref="PageReady"/> at the bottom of this class. That is where these specific criteria are combined.
        /// </summary>
        public readonly ICriteria<CBDObserverPage> LoadElementClassAttributeSetToHide = new Criteria<CBDObserverPage>(p =>
        {
            return p.Exists(Bys.RCPPage.LoadIcon, ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-hide")
                .OR(ElementCriteria.AttributeValue("class", "page-splash dissolve-animation ng-animate ng-hide")));
        }, "Load icon disappeared");

        public readonly ICriteria<CBDObserverPage> LoadElementDisappeared = new Criteria<CBDObserverPage>(p =>
        {
            return p.Exists(Bys.RCPPage.SplashPage, ElementCriteria.IsNotVisible);
        }, "Load icon disappeared");

        public readonly ICriteria<CBDObserverPage> AcceptedTblVisibleAndEnabled = new Criteria<CBDObserverPage>(p =>
        {
            return p.Exists(Bys.CBDObserverPage.AcceptedTbl, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Accepted table visible and enabled");

        public readonly ICriteria<CBDObserverPage> PendingAcceptanceTblEnabled = new Criteria<CBDObserverPage>(p =>
        {
            return p.Exists(Bys.CBDObserverPage.PendingAcceptanceTbl, ElementCriteria.IsEnabled);
        }, "Pending Acceptance table visible and enabled");

        public readonly ICriteria<CBDObserverPage> ExpiredDeclinedTblVisibleAndEnabled = new Criteria<CBDObserverPage>(p =>
        {
            return p.Exists(Bys.CBDObserverPage.ExpiredDeclinedTbl, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Expired Declined table visible and enabled");

        public readonly ICriteria<CBDObserverPage> CBDTabVisibleAndEnabled = new Criteria<CBDObserverPage>(p =>
        {
            return p.Exists(Bys.RCPPage.CBDTab, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "CBD tab visible and enabled");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<CBDObserverPage> ArchivedObservationsTblVisibleAndEnabled = new Criteria<CBDObserverPage>(p =>
        {            
            return p.Exists(Bys.CBDObserverPage.ArchivedObservationsTbl, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
        }, "Archived Observations table visible and enabled");

        /// <summary>
        /// The criteria that should be used for this constructor are only elements that are contained within the main page
        /// of the observer role section. We use this PageReady property inside <see cref="CBDObserverPage.WaitForInitialize()"/>
        /// </summary>
        public readonly ICriteria<CBDObserverPage> PageReady;
        public readonly ICriteria<CBDObserverPage> LoadElementDoneLoading;
        public CBDObserverPageCriteria()
        {
            LoadElementDoneLoading = LoadElementClassAttributeSetToHide.AND(LoadElementDisappeared);
            PageReady = LoadElementClassAttributeSetToHide.AND(LoadElementDisappeared).AND(AcceptedTblVisibleAndEnabled).AND(PendingAcceptanceTblEnabled);
        }
    }
}
