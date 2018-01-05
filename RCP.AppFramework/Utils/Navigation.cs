using Browser.Core.Framework;
using OpenQA.Selenium;
using System;

namespace RCP.AppFramework
{
	public static class Navigation
	{
        // Responsible for basic page navigation and specific-page initialization
        public static LoginPage GoToLoginPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new LoginPage(p), driver, waitForInitialize);
            return new LoginPage(driver);
        }

        public static MyDashboardPage GoToMyDashboardPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new MyDashboardPage(p), driver, waitForInitialize);
            return new MyDashboardPage(driver);
        }

        public static CBDLearnerPage GoToLearnerPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new CBDLearnerPage(p), driver, waitForInitialize);
            return new CBDLearnerPage(driver);
        }

        public static MyCPDActivitiesListPage GoToMyCPDActivitiesListPage(this IWebDriver driver, bool waitForInitialize = true)
        {
            var page = Navigate(p => new MyCPDActivitiesListPage(p), driver, waitForInitialize);
            return new MyCPDActivitiesListPage(driver);
        }

        private static T Navigate<T>(Func<IWebDriver, T> createPage, IWebDriver driver, bool waitForInitialize) where T : Page
        {
            var page = createPage(driver);
            page.GoToPage(waitForInitialize);
            return page;
        }


        

    }
}
