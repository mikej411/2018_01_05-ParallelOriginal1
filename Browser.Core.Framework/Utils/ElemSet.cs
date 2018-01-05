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
    /// A utility class for manipulating elements
    /// </summary>
    public static class ElemSet
    {
        #region select elements

        /// <summary>
        /// Selects a random item in a single select dropdown. It will not select the already selected item.
        /// </summary>
        /// <param name="elem">The SelectElement that contains the items</param>
        public static string SelElem_Single_SelectRandomItem(SelectElement selectElem)
        {
            for (var i = 0; i < 100; i++)
            {
                Random random = new Random();
                int randomInt = DataUtils.GetRandomIntegerWithinRange(0, selectElem.Options.Count);
                if (selectElem.SelectedOption.Text != selectElem.Options[randomInt].Text)
                {
                    selectElem.SelectByIndex(randomInt);
                    break;
                }
            }
            return selectElem.SelectedOption.Text;
        }

        /// <summary>
        /// Selects a user-specified number of random items in a multi-select SelectElement. If you need to select a lot of items in a dropdown that
        /// contains a large list, then this method might take a while. In that case, you can Use DropdownCustomClickRandomItems with IWebElement instead.
        /// </summary>
        /// <param name="elem">The IWebElement that contains the items</param>
        /// <param name="numberOfItemsToSelect"></param>
        public static string SelElem_Multi_SelectRandomItems(SelectElement selectElem, int numberOfItemsToSelect)
        {
            string selectedOptions = null;
            List<string> listOfSelectedOptions = new List<string>();
            Random random = new Random();
            int randomInt = 0;
            List<int> listOfIntsUsed = new List<int>();
            int countOfItemsInList = selectElem.Options.Count;

            for (var i = 0; i < numberOfItemsToSelect; i++)
            {
                // If all the items in the list are already selected, exit above for loop
                if (selectElem.AllSelectedOptions.Count == countOfItemsInList)
                {
                    break;
                }

                for (var j = 0; j < 100; j++)
                {
                    if (listOfIntsUsed.Contains(randomInt))
                    {
                        randomInt = DataUtils.GetRandomIntegerWithinRange(0, selectElem.Options.Count);
                    }
                    if (!listOfIntsUsed.Contains(randomInt))
                    {
                        break;
                    }
                }

                selectElem.SelectByIndex(randomInt);
                listOfIntsUsed.Add(randomInt);
            }

            // Store all the elements selected above into a comma separated string
            IList<IWebElement> selectedElements = selectElem.AllSelectedOptions;

            foreach (var elem in selectedElements)
            {
                listOfSelectedOptions.Add(elem.Text);
            }

            selectedOptions = string.Join(", ", listOfSelectedOptions);
            return selectedOptions;
        }

        /// <summary>
        /// Selects the first indexed item in the dropdown that contains the user-specified text
        /// </summary>
        /// <param name="elem">The element</param>
        /// <param name="text">The text to search for</param>
        /// <returns></returns>
        public static string SelElem_SelectItemContainingText(SelectElement elem, string text)
        {
            List<string> itemStrings = ElemGet.SelElem_ListTextToListString(elem);
            string selectedString = "";

            // For each item's string in the dropdown
            foreach (string itemString in itemStrings)
            {
                // If the string contains the user-specified text, then select it
                if (itemString.Contains(text))
                {
                    elem.SelectByText(itemString);
                    selectedString = itemString;
                    break;
                }
            }

            return selectedString;
        }

        #endregion select elements

        #region dropdown custom

        /// <summary>
        /// Chrome browser is failing in tests when simply using multiselectdropdown.click(). For some reason in this browser, whenever a dropdown
        /// gets expanded, a new element is created in the HTML with a classname of dropdown-backdrop. This new element blocks Selenium from all of the
        /// controls within the form, so if the user tries to do a multiselectdropdown.click(), selenium says it failed to click on that dropdown, and
        /// instead says the dropdown-backdrop would get the click. So the below method takes that into account and works to open and close the control
        /// </summary>
        /// <param name="elem">The button element of the multi-select dropdown</param>
        public static void DropdownCustom_Multi_SelectClick(IWebDriver browser, IWebElement elem)
        {
            if (browser.FindElements(By.ClassName("dropdown-backdrop")).Count > 0)
            {
                var form = browser.FindElement(By.ClassName("dropdown-backdrop"));
                form.Click();
                Thread.Sleep(0500);
            }
            else
            {
                elem.Click();
            }
        }

        /// <summary>
        /// Selects an item by name in a dropdown that is already expanded. This is an alternative to Selenium's built-in Select class Select methods.
        /// Use it instead when you want to select an item while the control is expanded. It will be left expanded upon completion if the list
        /// is multi-select, allowing the user to test specific test steps with the control expanded.
        /// </summary>
        /// <param name="elem">The element from which you are selecting from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="itemName">The item to choose</param>
        public static void DropdownCustom_ClickItemByName(IWebElement elem, string itemName)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element
            var liElems = DivElem.FindElements(By.CssSelector("li:not([class=hidden])")).ToList();
            foreach (var liElem in liElems)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (liElem.Text == itemName)
                {
                    // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
                    IWebElement aTagOfLIElem = liElem.FindElement(By.TagName("a"));
                    aTagOfLIElem.Click();
                    Thread.Sleep(0200);
                    break;
                }
            }
        }

        /// <summary>
        /// Clicks an item by index in a dropdown that is already expanded. This is an alternative to Selenium's built-in Select class Select methods.
        /// Use it instead when you want to click an item while the control is expanded. It will be left expanded upon completion if the list
        /// is multi-select, allowing the user to test specific test steps with the control expanded.
        /// </summary>
        /// <param name="elem">The IWebElement you are clicking the item from</param>
        /// <param name="index">The index of the item you want to choose</param>
        public static void DropdownCustom_ClickItemByIndex(IWebElement elem, int index)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var liElems = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
            IWebElement aTagOfLIElem = liElems[index].FindElement(By.TagName("a"));
            aTagOfLIElem.Click();
            Thread.Sleep(0200);
        }

        /// <summary>
        /// Selects random items from single or multi-select IWebElement dropdowns list that was expanded from a button. The dropdown must be expanded
        /// </summary>
        /// <param name="elem">The button you are selecting the item from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="index">The index of the item you want to choose</param>
        /// <param name="lowRange">Starting integer</param>
        /// <param name="highRange">Highest integer minus 1</param>
        public static void DropdownCustom_ClickRandomItems(IWebElement dropDownBtn, int lowRange, int highRange)
        {
            int randomInt = DataUtils.GetRandomIntegerWithinRange(lowRange, highRange);
            DropdownCustomSelectListItemByIndex(dropDownBtn, randomInt);
        }

        /// <summary>
        /// Alternative to DropdownCustomClickItemByIndex. This will select the item regardless of it was selected in the first place or not. DropdownCustomClickItemByIndex 
        /// can instead deselect items.
        /// </summary>
        /// <param name="elem">The IWebElement button you are selecting the item from</param>
        /// <param name="index">The index of the item you want to choose</param>
        public static void DropdownCustomSelectListItemByIndex(IWebElement elem, int index)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var listItems = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            // If the item is already selected, click it twice so it is still selected
            if (listItems[index].GetAttribute("class") != "selected")
            {
                //list[index].Click();
                // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
                var aTagOfLIItem = listItems[index].FindElement(By.TagName("a"));
                aTagOfLIItem.Click();
            }
        }

        /// <summary>
        /// Clicks on the dropdown button to expand it, selects an item by it's index, then closes the dropdown. This is an extension to DropdownCustomSelectListItemByIndex.
        /// NOTE: If the item is already selected, it will stay selected because the method conditions it so the item will be clicked twice in this case.
        /// </summary>
        /// <param name="elem">The IWebElement button you are selecting the item from</param>
        /// <param name="index">The index of the item you want to choose</param>
        public static void DropdownCustom_ChooseItemByIndex(IWebDriver browser, IWebElement elem, int index)
        {
            ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);

            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var listItems = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            // If the item is not already selected, then click it
            if (listItems[index].GetAttribute("class") != "selected")
            {
                //list[index].Click();
                // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
                var aTagOfLIItem = listItems[index].FindElement(By.TagName("a"));
                aTagOfLIItem.Click();
            }

            if (elem.GetAttribute("aria-expanded") == "true")
            {
                ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);
            }
        }

        /// <summary>
        /// Clicks on the dropdown button to expand it, selects an item by name, then closes the dropdown. This is an extension to DropdownCustomSelectListItemByIndex. 
        /// NOTE: If the item is already selected, it will stay selected because the method conditions it so the item will be clicked twice in this case.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="elem">The button you are selecting the item from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="name">The name of the item you want to click on</param>
        public static void DropdownCustom_ChooseItemByName(IWebDriver browser, IWebElement elem, string itemName)
        {
            ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);

            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var listItemElems = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])")).ToList();

            foreach (var item in listItemElems)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (item.Text == itemName)
                {
                    // If the item is not selected already, then select it
                    if (item.GetAttribute("class") != "selected")
                    {
                        // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
                        var aTagOfLIItem = item.FindElement(By.TagName("a"));
                        aTagOfLIItem.Click();
                        break;
                    }
                }
            }

            if (elem.GetAttribute("aria-expanded") == "true")
            {
                ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);
            }
        }

        /// <summary>
        /// Clicks on the dropdown button to expand it, clicks on the selected item, then closes the dropdown.
        /// </summary>
        /// <param name="elem">The IWebElement button you are selecting the item from</param>
        /// <param name="index">The index of the item you want to choose</param>
        public static void DropdownCustom_DeselectItemByIndex(IWebDriver browser, IWebElement elem, int index)
        {
            ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);

            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var listItems = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            // If the item is already selected, click it to deselect it
            if (listItems[index].GetAttribute("class") == "selected")
            {
                //list[index].Click();
                // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
                var aTagOfLIItem = listItems[index].FindElement(By.TagName("a"));
                aTagOfLIItem.Click();
            }

            if (elem.GetAttribute("aria-expanded") == "true")
            {
                ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);
            }
        }

        /// <summary>
        /// Clicks on the dropdown button to expand it, clicks on the selected item to deselect it (if it is not already deselected), then closes the dropdown.
        /// </summary>
        /// <param name="elem">The IWebElement button you are selecting the item from</param>
        /// <param name="itemName">The name of the item you want to deselect</param>
        public static void DropdownCustom_DeselectItemByName(IWebDriver browser, IWebElement elem, string itemName)
        {
            ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);

            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var listItemElems = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            foreach (var item in listItemElems)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (item.Text == itemName)
                {
                    // If the item is already selected, click to deselect it
                    //item.Click();
                    // Finding the A tag because IE has trouble clicking on LI items. So we are going to click on the A tag inside of the LI item for all browsers
                    var aTagOfLIItem = item.FindElement(By.TagName("a"));
                    aTagOfLIItem.Click();
                    break;
                }
            }

            if (elem.GetAttribute("aria-expanded") == "true")
            {
                ElemSet.DropdownCustom_Multi_SelectClick(browser, elem);
            }
        }

        #endregion dropdown custom

        #region TextBox

        /// <summary>
        /// This should be used to enter text into a text box for all text inside bootstrap forms. The javascript is needed to run so that the entered values
        /// do not get lost after the text is written into the text boxes. This issue occurs in IE all the time, and in Firefox less frequently. For background
        /// on the subject, see (http://stackoverflow.com/questions/9505588/selenium-webdriver-is-clearing-out-fields-after-sendkeys-had-previously-populate)
        /// If you want to see this issue, use SendKeys (without this method) inside a bootstrap form, then click Save. Notice
        /// that you receive an AJAX error, and the Web App log (if DEV provides one for your Web App) will show that there were no values in the text boxes when
        /// Save was clicked.
        /// </summary>
        /// <param browser="The driver instance"></param>
        /// <param elem="The element to enter text into"></param>
        /// <param clearText="Whether you want to clear the text before you enter text or not"></param>
        /// <param text="The exact string you want to enter"></param>
        public static void TextBox_EnterText(IWebDriver browser, IWebElement elem, bool clearText, string text)
        {
            elem.Click();

            if (clearText == true)
            {
                elem.Clear();
            }
            elem.SendKeys(text);

            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)browser;
            jsExecutor.ExecuteScript("$(arguments[0]).change();", elem);
        }

        #endregion TextBox

        #region Grids

        /// <summary>
        /// Expands or collapses an entire table, or row within a table. Note that this has been crteated for RCP CBD Learner and Prog Admin
        /// You may or may not have to add code to make it work with your individual application
        /// </summary>
        /// <param name="tblElem">The table element that contains the expansion/collapse icon</param>
        /// <param name="groupdItemToExpandOrCollapse">The exact text of the row name with the the expansion/collapse icon</param>
        /// <param name="expandOrCollapse">Either "expand" or "collapse"</param>
        public static void Grid_ExpandOrCollapseButton(IWebElement tblElem, string groupdItemToExpandOrCollapse, string expandOrCollapse)
        {
            IWebElement rowToExpandOrCollapse = null;
            IWebElement expandableElem = null;
            IWebElement collapsableElem = null;

            // I had trouble locating this element. At first I used //td[contains(text(),'{0}')] But that did not work. After posting to stackoverflow, I was told 
            // that whenever an element has 2 text nodes (this one does), I need to use this the Xpath like this instead //td[contains(., '{0}')] as that one
            // searches all text nodes of an element. If you object inspect the "Transition to discipline" row in the Program Learning Plan tab of the Learner page,
            // you will see that the element has comment text (green text) before and after a span element within it. That means it has 2 text nodes
            // https://stackoverflow.com/questions/45446631/using-xpath-contains-function-to-find-element-that-contains-text?noredirect=1#comment77872147_45446631
            string xpath = string.Format("//td[contains(., '{0}')]", groupdItemToExpandOrCollapse);
            // string xpath = string.Format("//tr[td//text()[contains(., '{0}')]]", groupdItemToExpandOrCollapse);

            // Get the group item to expand or collapse:        
            // If there are no elements found when using the xpath with the td tag, then that means we are trying to expand an entire table, so
            // we will then just locate the expand/collapse icon within the table
            if (tblElem.FindElements(By.XPath(xpath)).Count == 0)
            {
                expandableElem = tblElem.FindElements(By.TagName("img"))[0];
                collapsableElem = tblElem.FindElements(By.TagName("img"))[1];
            }
            // else we need to locate the expand/collpase icon within the td tag
            else
            {
                rowToExpandOrCollapse = tblElem.FindElement(By.XPath(xpath));
                expandableElem = rowToExpandOrCollapse.FindElements(By.TagName("img"))[0];
                collapsableElem = rowToExpandOrCollapse.FindElements(By.TagName("img"))[1];
            }

            if (expandOrCollapse == "expand")
            {
                // If the expandable button is displayed, then we should click it to expand. If not, it is already expanded
                if (expandableElem.Displayed)
                {
                    expandableElem.Click();
                }
            }
            else
            {
                // If the collapsable button is displayed, then we should click it to collapse. If not, it is already collapsed
                if (collapsableElem.Displayed)
                {
                    collapsableElem.Click();
                }
            }
            Thread.Sleep(0500);
        }

        /// <summary>
        /// Clicks any cell inside of a grid by the cell text of that cell
        /// </summary>
        /// <param gridElem="The driver instance"></param>
        /// <param cellText="The exact cell text inside the cell"></param>
        public static void Grid_ClickCellByCellText(IWebElement gridElem, string cellText)
        {
            //Get table rows 
            IList<IWebElement> allRows = gridElem.FindElements(By.TagName("tr"));

            foreach (IWebElement row in allRows)
            {
                //Find all cells in the row
                IList<IWebElement> cells = row.FindElements(By.XPath(".//*[local-name(.)='th' or local-name(.)='td']"));
                List<string> cellContent = new List<string>();

                foreach (IWebElement cell in cells)
                {
                    if (cell.GetAttribute("textContent") == cellText)
                    {
                        cell.Click();
                    }
                }
            }
        }

        /// <summary>
        /// Hovers over any cell inside of a grid by the cell text of that cell
        /// </summary>
        /// <param gridElem="The driver instance"></param>
        /// <param cellText="The exact cell text inside the cell"></param>
        public static void Grid_HoverOverCellByCellText(IWebDriver browser, IWebElement gridElem, string cellText)
        {
            //Get table rows 
            IList<IWebElement> allRows = gridElem.FindElements(By.TagName("tr"));

            foreach (IWebElement row in allRows)
            {
                //Find all cells in the row
                IList<IWebElement> cells = row.FindElements(By.XPath(".//*[local-name(.)='th' or local-name(.)='td']"));
                List<string> cellContent = new List<string>();

                foreach (IWebElement cell in cells)
                {
                    if (cell.GetAttribute("textContent") == cellText)
                    {
                        Actions action = new Actions(browser);
                        action.MoveToElement(cell).Perform();
                    }
                }
            }
        }

        public static void Grid_ClickRowByRowNumber(IWebDriver browser, IWebElement gridElem, int rowNumber)
        {
            IWebElement rowToClick = gridElem.FindElements(By.TagName("tr"))[rowNumber - 1];
            rowToClick.Click();
        }

        public static void Grid_HoverOverRowByRowNumber(IWebDriver browser, IWebElement gridElem, int rowNumber)
        {
            IWebElement rowToClick = gridElem.FindElements(By.TagName("tr"))[rowNumber - 1];
            Actions action = new Actions(browser);
            action.MoveToElement(rowToClick).Perform();
        }

        /// <summary>
        /// This will click on any button within a row on a table and then click the menu item that appears from that button
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellText">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="btnText">The exact text from the button to click, which when clicked on, results in a menu appearing</param>
        /// <param name="menuItemText">The exact text from the menu item after the button is clicked</param>
        public static void Grid_ClickMenuItemInsideButton(IWebDriver browser, IWebElement tblElem, By rowElemBy, string firstColumnCellText, string additionalCellText, string btnText, string menuItemText)
        {
            IWebElement btn = Grid_ClickButtonOrLinkWithinRow(browser, tblElem, rowElemBy, firstColumnCellText, additionalCellText, btnText);
            Thread.Sleep(0300);

            IWebElement btnParent = XpathUtils.GetNthParentElem(btn, 3);

            IWebElement menuItemElem = ElemGet.Grid_GetMenuItemOnRowButton(browser, btnParent, menuItemText);

            menuItemElem.Click();
        }

        /// <summary>
        /// This will click on any button within a row on a table
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Any row on your table, as it exists in your By type. We need to use this to wait for a row before we proceed with the test. i.e. Bys.CBDObserverPage,PendingAcceptanceTblBodyRow"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellText">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="btnText">The exact text from the button you want to click</param>
        public static IWebElement Grid_ClickButtonOrLinkWithinRow(IWebDriver browser, IWebElement tblElem, By rowElemBy, string firstColumnCellText, string additionalCellText, string btnText)
        {
            IWebElement btn = ElemGet.Grid_GetButtonOrLinkInsideRowByText(tblElem, rowElemBy, firstColumnCellText, additionalCellText, btnText);
            ElemSet.ScrollToElement(browser, btn);
            Thread.Sleep(0200);
            btn.Click();
            return btn;
        }

        /// <summary>
        /// This will select an item inside of a select element within a row of a grid
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="by">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellName">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="idOfSelElem">The exact text of the ID of the Select Element, however, if your select element is dynamically numbered per row, then only send the text before the number. For example, is the select tag has an ID of "Priority_0", only send "Priority"</param>
        /// <param name="itemToChoose">The exact text of the item you want to choose</param>
        public static void Grid_SelectItemWithinSelElem(IWebElement tblElem, By by, string firstColumnCellText, string additionalCellText, string idOfSelElem, string itemToChoose)
        {
            SelectElement selElem = ElemGet.Grid_GetSelElemInsideRowByID(tblElem, by, firstColumnCellText, additionalCellText, idOfSelElem);
            selElem.SelectByText(itemToChoose);
        }

        /// <summary>
        /// Clicks on an element within a user-specified row, such as a check box, or an X icon, or a + icon, or a Pencil Icon
        /// </summary>
        /// <param name="row">The row that contains the element you want to click on. To get the row, <see cref="ElemGet.Grid_GetRowByRowName(IWebElement, By, string)"/></param>
        /// <param name="tagTypeOfElemToclick"></param>
        public static void Grid_ClickElementWithoutTextInsideRow(IWebElement row, string tagTypeOfElemToclick)
        {
            string xpathString = string.Format(string.Format("./descendant::{0}", tagTypeOfElemToclick));

            var checkbox = row.FindElement(By.XPath(xpathString));

            checkbox.Click();
        }

        #endregion Grids

        #region Buttons


        #endregion Buttons

        #region Radio buttons

        /// <summary>
        /// Clicks a radio button of your choice
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text as it appears in the HTML of the radio button to click</param>
        /// <returns></returns>
        public static string RdoBtn_ClickByText(IWebDriver browser, string textOfRadioBtn)
        {
            // Right now I have to implement the below IF statement for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathString = string.Format("//label/span[text()='{0}']", textOfRadioBtn);

            //Thread.Sleep(3000);

            if (browser.FindElements(By.XPath(xpathString)).Count > 0)
            {
                IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtn(browser, textOfRadioBtn);
                IWebElement rdoBtnParent = XpathUtils.GetNthParentElem(rdoBtn, 1);
                rdoBtnParent.Click();
                return textOfRadioBtn;
            }
            else
            {
                IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtn(browser, textOfRadioBtn);
                rdoBtn.Click();
                return textOfRadioBtn;
            }
        }


        /// <summary>
        /// Selects a radio button of your choice
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text as it appears in the HTML of the radio button to click</param>
        /// <returns></returns>
      

        /// <summary>
        /// Selects multiple radio buttons of your choice
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtns">The exact text as it appears in the HTML of the radio buttons to click</param>
        /// <returns></returns>
        public static void RdoBtn_ClickMultipleByText(IWebDriver browser, params string[] textOfRadioBtns)
        {
            foreach (string textOfRadioBtn in textOfRadioBtns)
            {
                RdoBtn_ClickByText(browser, textOfRadioBtn);
            }
        }

        /// <summary>
        /// Clicks on a random radio button within a "table" tag
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text of one of the radio buttons inside</param>
        public static string RdoBtn_ClickRandom(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtn(browser, textOfRadioBtn);
            IList<IWebElement> rdoBtns = ElemGet.RdoBtn_GetRdoBtns(rdoBtn);

            Random r = new Random();
            int randomIndex = r.Next(rdoBtns.Count); //Getting a random value that is between 0 and (list's size)-1
            rdoBtns[randomIndex].Click();
            return rdoBtns[randomIndex].Text;
        }

        #endregion Radio bnuttons

        #region Check boxes

        /// <summary>
        /// Clicks on a random check box that is contained within a "div" tag with a class attribute value of "form-group"
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfChkBx">The exact text of one of the check box that you want to click</param>
        public static string ChkBx_ChooseRandom(IWebDriver browser, string textOfChkBx)
        {
            IWebElement chkBx = ElemGet.ChkBx_GetChkBx(browser, textOfChkBx);
            IList<IWebElement> chkBxs = ElemGet.ChkBx_GetListOfChkBxsWithinForm(chkBx);

            Random r = new Random();
            int randomIndex = r.Next(chkBxs.Count); //Getting a random value that is between 0 and count of items
            chkBxs[randomIndex].Click();
            return chkBxs[randomIndex].Text;
        }

        #endregion Check boxes

        #region General

        /// <summary>
        /// Scrolls to your element, then clicks it. Use this when your click fails on small laptops or screens. The
        /// exception thrown in most of these cases is "Element is not clickable at point..."
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="elem">The element to scroll to then click on</param>
        /// <param name="divElem">If your element is inside a popup window, or inside a frame (not inside the main window), then you must send the scroll bar element of the frame/popup window. This is usually the a Div tag. If the element is on the main window, then just dont pass this parameter</param>
        public static void ClickAfterScroll(IWebDriver browser, IWebElement elem, IWebElement divElem = null)
        {
            if(divElem == null)
            {
                ScrollToElement(browser, elem);
            }
            else
            {
                ScrollToElementWithinFrame(browser, divElem, elem, "Vertical");
            }

            elem.Click();
        }

        public static void DragAndDropToElement(IWebDriver browser, IWebElement sourceElem, IWebElement destinationElem, int x, int y)
        {
            int Width = destinationElem.Size.Width;
            int Height = destinationElem.Size.Height;
            Console.WriteLine(Width);
            Console.WriteLine(Height);
            int MyX = (Width * x) / 100;//spot to drag to is at x of the width
            int MyY = (Height * y) / 100; ;//spot to drag to is at y of the height

            if (x == -1)
            {
                Actions builder = new Actions(browser);
                IAction dragAndDrop = builder.ClickAndHold(sourceElem)
                .MoveToElement(destinationElem)
                    .Release()
                    .Build();
                dragAndDrop.Perform();
            }
            else
            {
                Actions builder = new Actions(browser);
                IAction dragAndDrop = builder.ClickAndHold(sourceElem)
                .MoveToElement(destinationElem, MyX, MyY) // TODO: See the begining of this method for details on MyX and MyY
                   .Release(destinationElem)
                   .Build();
                dragAndDrop.Perform();
            }
        }

        /// <summary>
        /// Scrolls horizontally or vertically to a specified element within a frame that contains a scroll bar
        /// <param name="browser">The driver</param>
        /// <param name="divElem">The div element that contains the scroll bar must be passed here</param>
        /// <param name="elemToScrollTo">The element the tester wants to scroll to</param>
        /// <param name="HorizontalOrVertical">'Horizontal' or 'Vertical'</param>
        /// </summary>
        public static void ScrollToElementWithinFrame(IWebDriver browser, IWebElement divElem, IWebElement elemToScrollTo, string HorizontalOrVertical)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;

            if (HorizontalOrVertical == "Vertical")
            {
                js.ExecuteScript("arguments[0].scrollTop = arguments[1];", divElem, elemToScrollTo.Location.Y);
            }

            if (HorizontalOrVertical == "Horizontal")
            {
                // Scroll inside the popup frame element vertically. See the following...
                // http://stackoverflow.com/questions/22709200/selenium-webdriver-scrolling-inside-a-div-popup
                js.ExecuteScript("arguments[0].scrollLeft = arguments[1];", divElem, elemToScrollTo.Location.X);
            }
        }

        /// <summary>
        /// Scrolls horizontally or vertically to a specified element within a frame that contains a scroll bar
        /// <param name="browser">The driver</param>
        /// <param name="divElem">The div element that contains the scroll bar must be passed here</param>
        /// <param name="xOrYCoordinate">The X or the Y coordinate</param>
        /// <param name="HorizontalOrVertical">'Horizontal' or 'Vertical'</param>
        /// </summary>
        public static void ScrollToWithinFrame(IWebDriver browser, IWebElement divElem, int xOrYCoordinate, string HorizontalOrVertical)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;

            if (HorizontalOrVertical == "Vertical")
            {
                js.ExecuteScript("arguments[0].scrollTop = arguments[1];", divElem, xOrYCoordinate);
            }

            if (HorizontalOrVertical == "Horizontal")
            {
                // Scroll inside the popup frame element vertically. See the following...
                // http://stackoverflow.com/questions/22709200/selenium-webdriver-scrolling-inside-a-div-popup
                js.ExecuteScript("arguments[0].scrollLeft = arguments[1];", divElem, xOrYCoordinate);
            }
        }

        /// <summary>
        /// Scrolls vertically to a specified element within the active window. This only scrolls on the window scroll bar, not any scroll bars embedded
        /// scroll bars
        /// <param name="browser">The driver</param>
        /// <param name="divElem">The element to scroll to</param>
        /// </summary>
        public static void ScrollToElement(IWebDriver browser, IWebElement elem)
        {
            ((IJavaScriptExecutor)browser).ExecuteScript("window.scrollTo(0," + elem.Location.Y + ")");
        }

        /// <summary>
        /// Scrolls vertically to a specified Select element within the active window. This only scrolls on the window scroll bar, not any scroll bars embedded
        /// scroll bars
        /// <param name="browser">The driver</param>
        /// <param name="divElem">The select element to scroll to</param>
        /// </summary>
        public static void ScrollToSelectElement(IWebDriver browser, SelectElement elem)
        {
            ((IJavaScriptExecutor)browser).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        #endregion General

        #region Date Picker

        /// <summary>
        /// Expands the date picker, clicks the upper middle button until the Year frame appears, chooses the year, then month, 
        /// then day of month, then closes the control
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="yr">The 2 digit year. ie. "17"</param>
        /// <param name="monthName">The full month name. ie. "January"</param>
        /// <param name="dayOfMonth">The day of the month. ie. "24"</param>
        /// <returns></returns>
        public static string DatePicker_ChooseDate(IWebDriver browser, string yr, string monthName, string dayOfMonth)
        {
            IWebElement dateControlTxt = browser.FindElement(By.XPath("//input[@type='text' and @name='Date']"));

            //Thread.Sleep(0800);
            IWebElement expandBtn = dateControlTxt.FindElement(By.XPath("..//span[@class='input-group-btn']//button[@class='btn btn-default']//span"));
            expandBtn.Click();
            Thread.Sleep(0300);

            IWebElement topMiddleBtn = dateControlTxt.FindElement(By.XPath(".././/strong/.."));
            topMiddleBtn.Click();

            IWebElement topMiddleBtn2 = dateControlTxt.FindElement(By.XPath(".././/strong/.."));
            topMiddleBtn2.Click();

            IWebElement yearBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", yr)));
            yearBtn.Click();

            IWebElement monthBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", monthName)));
            monthBtn.Click();

            IWebElement dayOfMonthBtn = dateControlTxt.FindElement(By.XPath(string.Format(".././/span[text()='{0}' and contains(@class ,'binding')]", dayOfMonth)));
            dayOfMonthBtn.Click();

            return dateControlTxt.GetAttribute("value");
        }

        #endregion Date Picker








    }
}

