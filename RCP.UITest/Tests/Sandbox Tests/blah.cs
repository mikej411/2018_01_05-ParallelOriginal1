using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using RCP.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;

namespace RCP.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    ////[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class blah : TestBase
    {
        #region Constructors
        public blah(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public blah(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

  

        //[Test]
        [Parallelizable(ParallelScope.All)]
        public void sandboxtest()
        {
            browser.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(30000);
        }

        //[Test]
        [Parallelizable(ParallelScope.All)]
        public void sandboxtest2()
        {
            browser.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(30000);
        }

        #endregion Tests
    }
}

