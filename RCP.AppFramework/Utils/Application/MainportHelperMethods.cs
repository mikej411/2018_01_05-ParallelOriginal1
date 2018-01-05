using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;

namespace RCP.AppFramework
{
    public static class MainportHelperMethods
    {
        #region properties


        #endregion properties

        #region methods

        /// <summary>
        /// If you add an activity which gives you credits, this method can be called to wait for a user-specified label on your application
        /// to get updated with those credits after that activity was added. Once an activity is submitted, a record gets put into a windows service
        /// queue, and then waits for that service to push the activity through, because of this, we need to wait in our code. Note that there
        /// is not database flag to check instead of just randomly refreshing every couple of seconds. Right now, we will refresh every 4 seconds,
        /// 30 times. If your application ever takes longer than that, then increase the For loop below
        /// Note: This method is a workaround to be able to refresh this page. It can be removed when/if defect 
        /// https://code.premierinc.com/issues/browse/RCPSC-793 is fixed. If that gets fixed, then
        /// we will remove this method, and just use the regular <see cref="ApplicationUtils.WaitForCreditsToBeApplied(Page, By, string)"/>
        /// </summary>
        /// <param name="Page">The page to refresh</param>
        /// <param name="creditLabelBy">the label which stores the amount of credits that you are waiting to be refreshed</param>
        /// <param name="amountOfCredits">The amount of credits that will show when the windows service is complete</param>
        public static void WaitForCreditsToBeApplied(IWebDriver Browser, Page page, By creditLabelBy, string amountOfCredits)
        {
            MyMOCPage MP = new MyMOCPage(Browser);
            MyDashboardPage DP = new MyDashboardPage(Browser);

            DP.ClickAndWaitBasePage(DP.MyMOCTab);
            DP.ClickAndWaitBasePage(DP.MyDashboardTab);

            ApplicationUtils.WaitForCreditsToBeApplied(page, creditLabelBy, amountOfCredits);
        }


        #endregion methods
    }
}
