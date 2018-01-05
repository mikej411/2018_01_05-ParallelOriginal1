using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace RCP.AppFramework
{
    public abstract class RCPPage : Page
    {
        #region Constructors

        public RCPPage(IWebDriver driver) : base(driver) { }

        #endregion

        #region Elements


        public IWebElement MyELearningTab { get { return this.FindElement(Bys.RCPPage.MyELearningTab); } }
        public IWebElement MyCPDActivitiesTab { get { return this.FindElement(Bys.RCPPage.MyCPDActivitiesTab); } }
        public IWebElement MyCPDPlanningTab { get { return this.FindElement(Bys.RCPPage.MyCPDPlanningTab); } }
        public IWebElement MyReportsTab { get { return this.FindElement(Bys.RCPPage.MyReportsTab); } }
        public IWebElement MyHoldingAreaTab { get { return this.FindElement(Bys.RCPPage.MyHoldingAreaTab); } }
        public IWebElement MyMOCTab { get { return this.FindElement(Bys.RCPPage.MyMOCTab); } }
        public IWebElement MyDashboardTab { get { return this.FindElement(Bys.RCPPage.MyDashboardTab); } }
        public IWebElement MainFrame { get { return this.FindElement(Bys.RCPPage.MainFrame); } }
        public IWebElement PERAFCTab { get { return this.FindElement(Bys.RCPPage.PERAFCTab); } }
        public IWebElement MyDiplomaTab { get { return this.FindElement(Bys.RCPPage.MyDiplomaTab); } }
        public IWebElement LoadIconForPERAndDiploma { get { return this.FindElement(Bys.RCPPage.LoadIconForPERAndDiploma); } }
        public IWebElement Menu_About { get { return this.FindElement(Bys.RCPPage.Menu_MyDashboard); } }
        public IWebElement Menu_MyCPDActivitiesList { get { return this.FindElement(Bys.RCPPage.Menu_MyCPDActivitiesList); } }
        public IWebElement LoadIcon { get { return this.FindElement(Bys.RCPPage.LoadIcon); } }
        public IWebElement LogoutLnk { get { return this.FindElement(Bys.RCPPage.LogoutLnk); } }
        public IWebElement TOSAcceptBtn { get { return this.FindElement(Bys.RCPPage.TOSAcceptBtn); } }

        #endregion Elements

        #region Methods: wrappers

        /// <summary>
        /// Clicks the user-specified element that exists on every page of RCP, and then waits for a window to close or open,
        /// or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The button element</param>
        public dynamic ClickAndWaitBasePage(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.RCPPage.PERAFCTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PERAFCTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    PERCredentialStaffPage page = new PERCredentialStaffPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.RCPPage.MyDiplomaTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MyDiplomaTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    DiplomaCredentialStaffPage page = new DiplomaCredentialStaffPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.RCPPage.MyDashboardTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MyDashboardTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    MyDashboardPage page = new MyDashboardPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.RCPPage.MyCPDActivitiesTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MyCPDActivitiesTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    MyCPDActivitiesListPage page = new MyCPDActivitiesListPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.RCPPage.MyMOCTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MyMOCTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    MyMOCPage page = new MyMOCPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.RCPPage.MyHoldingAreaTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == MyHoldingAreaTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    MyHoldingAreaPage page = new MyHoldingAreaPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            else
            {
                throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                    "or if the button is already added, then the page you were on did not contain the button.");
            }

            return null;
        }

        /// <summary>
        /// Clicks the logout link, waits for the Royal College client site to appear, navigates to the backdoor login,
        /// then returns the backdoor login page
        /// </summary>
        /// <returns></returns>
        public LoginPage Logout()
        {
            Browser.SwitchTo().DefaultContent();
            ElemSet.ScrollToElement(Browser, LogoutLnk);
            LogoutLnk.Click();
            new WebDriverWait(Browser, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.UrlContains("login"));
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            return LP;
        }

        public void SwitchToFrame(By frameBy, IWebElement frameElem, By elemOnFrame)
        {
            Browser.WaitForElement(frameBy, ElementCriteria.IsVisible);
            Browser.SwitchTo().Frame(frameElem);
            Browser.WaitForElement(elemOnFrame, ElementCriteria.IsVisible);
        }


       
        
        
        
        
        
        
        
        
        
        
        
        ///// <summary>
        ///// Navigates to a given page through a single or multi-layered menu system
        ///// </summary>
        ///// <param name="browser">The driver instance</param>
        ///// <param name="menuItems">If it is a single-layered menu, then only 1 By type will be needed. If it is multi-layered, the tester should
        ///// pass the By types in the order they would click them manually</param>
        ///// <returns>The page object, which contains all elements of the page and any page related methods</returns>
        //public static dynamic NavigateThroughMenuItems(IWebDriver browser, params By[] menuItems) //By menu1, By menu2 = null, By menu3 = null,
        //{
        //    if(menuItems.Length == 1)
        //    {
        //        IWebElement elemToClick = browser.FindElement(menuItems[0]);
        //        elemToClick.Click();
        //    }

        //    else
        //    {
        //        for(int i = 0; i < menuItems.Length - 1; i++)
        //        {
        //            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(1));
        //            IWebElement elemToHover = wait.Until(ExpectedConditions.ElementIsVisible(menuItems[0]));
        //            Actions action = new Actions(browser);
        //            action.MoveToElement(elemToHover).Perform();
        //        }

        //        IWebElement elemtToClick = browser.FindElement(menuItems[menuItems.Length - 1]);
        //        elemtToClick.Click();
        //    }

        //    if (browser.Url.Contains("CPDActivities.aspx"))
        //    {
        //        var APPage = new MyCPDActivitiesListPage(browser);
        //        APPage.WaitForInitialize();
        //        return APPage;
        //    }


        //    //if (browser.Url.Contains("About Me"))
        //    //{
        //    //    var aboutPage = new AboutPage(browser);
        //    //    aboutPage.WaitForInitialize();
        //    //    return aboutPage;
        //    //}
        //    return null;
        //}


        #endregion Methods: Wrappers
    }
}