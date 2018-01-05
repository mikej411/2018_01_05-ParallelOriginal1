using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class for various operations on our LMS platform applications
    /// </summary>
    public static class ApplicationUtils
    {
        /// <summary>
        /// If you add an activity which gives you credits, this method can be called to wait for a user-specified label on your application
        /// to get updated with those credits after that activity was added. Once an activity is submitted, a record gets put into a windows service
        /// queue, and then waits for that service to push the activity through, because of this, we need to wait in our code. Note that there
        /// is not database flag to check instead of just randomly refreshing every couple of seconds. Right now, we will refresh every 4 seconds,
        /// 30 times. If your application ever takes longer than that, then increase the For loop below
        /// </summary>
        /// <param name="Page">The page to refresh</param>
        /// <param name="creditLabelBy">the label which stores the amount of credits that you are waiting to be refreshed</param>
        /// <param name="amountOfCredits">The amount of credits that will show when the windows service is complete</param>
        public static void WaitForCreditsToBeApplied(Page Page, By creditLabelBy, string amountOfCredits)
        {
            for (int i = 1; i < 400; i++)
            {
                Thread.Sleep(3000);
                Page.RefreshPage(true);
                try
                {
                    Page.WaitForElement(creditLabelBy, TimeSpan.FromMilliseconds(0100), ElementCriteria.AttributeValue("innerText", amountOfCredits));
                    break;
                }
                catch
                {
                    continue;
                }
            }
        }


    }
}

