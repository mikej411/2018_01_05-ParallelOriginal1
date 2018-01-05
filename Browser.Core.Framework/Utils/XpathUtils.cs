using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class retreiving elements through various methods
    /// </summary>
    public static class XpathUtils
    {
        public static IWebElement GetNthParentElem(IWebElement childElem, int nThParent)
        {
            StringBuilder xpathString = new StringBuilder("..");

            for (int i = 1; i < nThParent; i++)
            {
                xpathString.Append("/..");
                
            }

            var parentElem = childElem.FindElement(By.XPath(xpathString.ToString()));

            return parentElem;
        }

        /// <summary>
        /// Gets a parent or child element of your  element. You specify what criteria the parent element must meet to be found
        /// see https://stackoverflow.com/questions/3005370/xpath-to-find-nearest-ancestor-element-that-contains-an-element-that-has-an-attr
        /// </summary>
        /// <param name="elem">The element</param>
        /// <param name="criteriaForParentElem">The specific criteria to find the parent element. For example, send "tr[1]" to find
        /// <param name="parentOrChild"/>Either "parent" or "child"
        /// the first parent element with the tr tag. Or send "div[@role='radiogroup']" to find a parent elem with a div tag, that
        /// contains a role attribute that equals radiogroup</param>
        /// <returns></returns>
        public static IWebElement GetParentOrChildElemWithSpecifiedCriteria(IWebElement elem, string parentOrChild, string criteriaForParentElem)
        {

            string newXpath = string.Format("ancestor::{0}", criteriaForParentElem);
            IWebElement parentElem = elem.FindElement(By.XPath(newXpath));

            return parentElem;
        }

    }

    // Locator examples
    //public readonly By Menu_About = By.XPath("//li[@id='menu-item-1155']/a");

    //public readonly By Menu_FunctionalTesting = By.XPath("//li[@id='menu-item-1150']/a");
    //public readonly By Menu_FunctionalTesting_BDDSpecFlow = By.XPath("//li[@id='menu-item-1154']/a");

    // This XPath line selects the first TD element with the exact text
    //string xPathVariable = "//td[./text()='yourtext']";
    //string xPathVariable = "//td[contains(text(),'yourtext')]";
    //string xPathVariable = string.Format("//div[contains(.,'{0}')]", textOfCell);
    //IWebElement TDCell = gridElem.FindElement(By.XPath(xPathVariable));

    // Mulitple elements or multiple attributes
    //string xpathString = string.Format("//span[text()='{0}' and @class=\"ui-iggrid-headertext\"]", textOfHeaderCell);

    // Attribute does not equal
    //IWebElement lists = Browser.FindElement(By.CssSelector("li:not([class=hidden])"));

    // Second instance of
    //    (//iframe)[2]

    // Sibling
    //td/following-sibling::span

    // Class contains
    // div[contains(@class, 'header')]

    // text contains
    //     //td[contains(text(),'yourtext')]

    // All text nodes contains:
    // Whenever an element has multiple text nodes, We need to use this Xpath //td[contains(., '{0}')] as this
    // searches all text nodes of an element. If you object inspect the "Transition to discipline" row in the 
    // Program Learning Plan tab of the Learner page in the RCP application, you will see that the element has
    // comment text (green text) before and after a span element within it. This is an example of multiple 
    // text nodes
    // https://stackoverflow.com/questions/45446631/using-xpath-contains-function-to-find-element-that-contains-text?noredirect=1#comment77872147_45446631
    //div[contains(., 'YourText')]

}