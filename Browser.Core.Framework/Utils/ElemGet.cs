using OpenQA.Selenium;
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
    /// A utility class retreiving data and attributes from elements
    /// </summary>
    public static class ElemGet
    {
        #region Checkbox

        public static IList<IWebElement> ChkBx_GetListOfChkBxsWithinForm(IWebElement chkBx)
        {
            // Get the first ancestor of the radio button with the tag name of div that has an attribute value of form-group. 
            // Check boxes in CBD are within these div tags. For other applications, we can add conditions
            //string xpath = "ancestor::tbody[1]";
            string xpath = "ancestor::div[@class='form-group']";
            IList<IWebElement> allChkBxs = null;

            try
            {
                IWebElement parentTableOfRdoBtns = chkBx.FindElement(By.XPath(xpath));
                allChkBxs = parentTableOfRdoBtns.FindElements(By.TagName("input"));

                //// Some radio buttons in RCP are located within spans, some are labels, so we need to condition this
                //if (allChkBxs.Count == 0)
                //{
                //    allChkBxs = parentTableOfRdoBtns.FindElements(By.TagName("label"));
                //}

                return allChkBxs;
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// Gets the check box element based on it's text
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfChkBx">The exact text of one of the check boxes</param>
        public static IWebElement ChkBx_GetChkBx(IWebDriver browser, string textOfChkBx)
        {
            IWebElement chkBx = null;

            string xpathString = string.Format("//div[contains(., '{0}')]/input", textOfChkBx);

            chkBx = browser.FindElement(By.XPath(xpathString));
            return chkBx;
        }

        /// <summary>
        /// Determines whether a list item within a dropdown has been checked or not
        /// </summary>
        /// <param name="elem">The dropdown element</param>
        /// <param name="itemName">The text attribute value of the list item element</param>
        /// <returns>True if checkbox is checked, false if not</returns>
        public static bool ChkBx_IsDisplayedOnListItem(IWebElement elem, string itemName)
        {
            bool result = false;

            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));
            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));
            // Store all li elements within the Div element
            var lists = DivElem.FindElements(By.CssSelector("li:not([class=hidden])"));
            foreach (var list in lists)
            {
                // if the current list item's text value in the for loop equals the users passed parameter itemName
                if (list.Text == itemName)
                {
                    // Find the check mark element. find span class=glyphicon
                    var checkMarkElem = list.FindElement(By.ClassName("glyphicon"));
                    if (checkMarkElem.Displayed)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        #endregion Checkbox

        #region dropdown custom

        /// <summary>
        /// Returns a Datatable representing multi-select select element. The control must be expanded for this method to work, as the Text property of the
        /// LI items do not get populated until the drop down is expanded
        /// </summary>
        /// <param name="elem">The element to grab the list of items from. It must be of type IWebElement, not SelectElement</param>
        public static DataTable DropdownCustom_MultiSelect_LITextToDataTable(IWebElement elem)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));
            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));
            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var lists = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));
            return lists.Select(p => p.Text.Trim()).ToDataTable<string>("Text");
        }

        /// <summary>
        /// Returns the text of an LI element from a dropdown control
        /// </summary>
        /// <param name="elem">The element from which you are selecting from. It must be of type IWebElement, not SelectElement</param>
        /// <param name="index">The index (zero based) of the LI (list item) that you want to grab the text from</param>
        public static string DropdownCustom_ListItemTextByIndex(IWebElement elem, int index)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var lists = DivElem.FindElements(By.CssSelector("li:not([class=hidden]):not([class='selected hidden'])"));

            return lists[index].Text;
        }

        /// <summary>
        /// Returns a List<string> of the contents of a multi-select IWebElement. The control must be expanded for this method to
        /// work, as the Text property of the LI items do not get populated until the drop down is expanded
        /// </summary>
        /// <param name="elem">The element to grab the list of items from. It must be of type IWebElement, not SelectElement</param>
        public static List<string> DropdownCustom_MultiSelect_LITextToListString(IWebElement elem)
        {
            // Find the parent element of the drop down. So we can then find a child DIV element
            var ParentElement = elem.FindElement(By.XPath(".."));

            // This DIV element contains the list items, whether filtered or not. If values are filtered, there is code in the line after
            // this to handle that. i.e. class<>hidden
            var DivElem = ParentElement.FindElement(By.CssSelector("ul[role=listbox]"));

            // Store all li elements within the Div element that do not have a class that is either equal to "hidden" or "selected hidden"
            var lists = DivElem.FindElements(By.CssSelector("li:not([class='hidden']):not([class='selected hidden'])"));

            return lists.Select(p => p.Text).ToList();
        }


        #endregion dropdown custom

        #region Select Elements

        /// <summary>
        /// Returns Datatable representing single-select SelectElement
        /// </summary>
        /// <param name="elem">The element to grab the list of items from. It must be of type SelectElement</param>
        public static DataTable SelElem_ListTextToDataTable(SelectElement elem)
        {
            return elem.Options.Select(p => p.Text.Trim()).ToDataTable<string>("Text");
        }

        /// <summary>
        /// Returns a Datatable from your Select Element. Note that this method trims consecutive spaces from the list of strings. So if you are comparing this
        /// list to anything else, make sure you trim the consecutive spaces off of that comparison object as well
        /// </summary>
        /// <param name="elem">The element to grab the list from. Must be an element with a Select tag in the HTML</param>
        public static List<string> SelElem_ListTextToListString(SelectElement elem)
        {
            return elem.Options.Select(p => p.GetAttribute("textContent")).ToList();
        }

        /// <summary>
        /// If any of the items in the select element contain the user specified text, return true, else return false
        /// </summary>
        /// <param name="elem">The select element</param>
        /// <param name="textToSearchFor">The text you want to verify exists in the select element</param>
        /// <returns></returns>
        public static bool SelElem_ContainsText(SelectElement elem, string textToSearchFor)
        {
            List<string> itemTextForAllItems = ElemGet.SelElem_ListTextToListString(elem);

            // If any of the items of the list<string> contain the text , return true
            foreach (string itemText in itemTextForAllItems)
            {
                if (itemText.Contains(textToSearchFor))

                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the count of items in the select element containing the user specified text
        /// </summary>
        /// <param name="elem">The select element</param>
        /// <param name="textToSearchFor">The text you want to check for</param>
        /// <returns></returns>
        public static int SelElem_GetCountOfItemsContainingText(SelectElement elem, string textToSearchFor)
        {
            List<string> itemTextForAllItems = ElemGet.SelElem_ListTextToListString(elem);
            int countOfItems = 0;

            // If any of the items of the list<string> contain the text, add it to the list =
            foreach (string itemText in itemTextForAllItems)
            {
                if (itemText.Contains(textToSearchFor))
                {
                    countOfItems++;
                }

            }

            return countOfItems;
        }

        /// <summary>
        /// Gets the first element in a select element containing the user specified text
        /// </summary>
        /// <param name="elem">The select element</param>
        /// <param name="textToSearchFor">The text you want to check for</param>
        /// <returns></returns>
        public static IWebElement SelElem_GetFirstItemContainingText(SelectElement elem, string textToSearchFor)
        {
            List<string> itemTextForAllItems = ElemGet.SelElem_ListTextToListString(elem);
            IWebElement elemContainingText = null;

            // If any of the items of the list<string> contain the text, add it to the list =
            foreach (string itemText in itemTextForAllItems)
            {
                if (itemText.Contains(textToSearchFor))
                {
                    return elemContainingText;
                }

            }

            return elemContainingText;
        }

        /// <summary>
        /// Returns a Datatable from your Select Element, with a user-specified parameter to trim any text in the string
        /// </summary>
        /// <param name="elem">The element to grab the list from. Must be an element with a Select tag in the HTML</param>
        /// <param name="textToRemove">The text you want to remove from the string</param>
        public static DataTable SelElem_ListTextTrimmedToDataTable(SelectElement elem, string textToRemove)
        {
            return elem.Options.Select(p => p.Text.Replace(textToRemove, string.Empty)).ToDataTable<string>("Text");
        }

        /// <summary>
        /// Returns a list of strings from your Select Element. Until this code is refactored to become faster,  Only use this
        /// on a small list, preferably under 30 items. Otherwise, it will take a long time to complete. An alternative is to use
        /// the SelectElementListItemTextToDataTable method
        /// </summary>
        /// <param name="elem">The element to grab the list from. Must be anelement with a Select tag in the HTML</param>
        public static List<string> SelElem_ListTextToArray(SelectElement elem)
        {
            // IList<string> orgTypesActual = new List<string>();
            // orgTypesActual = OrgPage.GetDropDownItemsViaIList(OrgPage.CreateUpdateOrgFormOrgTypeSelectItem1);
            List<string> DropDownItems = new List<string>();
            for (var i = 0; i < elem.Options.Count; i++)
            {
                DropDownItems.Add(elem.Options[i].Text);
            }
            return DropDownItems;
        }

        #endregion Select Elements

        #region Grids

        /// <summary>
        /// Returns the cell text from a user-specified row name and column name
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="tblElem">You table header (thead) element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="by">Send any row in it's By type (Most likely just send the iwebelement that is a generic row 'tableId/tr' in the table. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody. We need any row so that we can wait for it to appear before proceeding with the test"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string Grid_GetCellTextByRowNameAndColumnName(IWebElement tblElem, IWebElement theadElem, By by, string firstColumnCellText, string colName)
        {
            // Get all column headers
            IList<IWebElement> columnHeaders = theadElem.FindElements(By.TagName("th"));

            // Loop through column headers until the user-specified colName is reached. Once reached, break the loop and store the index into colNum
            int colNum = 0;

            for (int i = 0; i < columnHeaders.Count; i++)
            {
                if (columnHeaders[i].Text == colName)
                {
                    colNum = i;
                    break;
                }
            }

            // Get the row that the user wants to grab the text from
            IWebElement row = Grid_GetRowByRowName(tblElem, by, firstColumnCellText);

            // Within the row, find the cell text from the index of the column name that we found above
            string cellText = row.FindElements(By.TagName("td"))[colNum].Text;

            return cellText;
        }

        /// <summary>
        /// Returns the row count of any grid
        /// </summary>
        /// <param gridElem="The driver instance"></param>
        public static int Grid_GetRowCount(IWebElement gridElem)
        {
            if (gridElem.FindElements(By.TagName("tr")).Count == 0)
            {
                return 0;
            }
            int rowCount = gridElem.FindElements(By.TagName("tr")).Count;
            return rowCount;
        }

        /// <summary>
        /// Gets the user-specified Select Element within a table by the ID of the Select Element
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="by">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellText">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="idOfSelElem">The exact text of the ID of the Select Element, however, if your select element is dynamically numbered per row, then only send the text before the number. For example, is the select tag has an ID of "Priority_0", only send "Priority"</param>
        public static SelectElement Grid_GetSelElemInsideRowByID(IWebElement tblElem, By by, string firstColumnCellText, string additionalCellText, string idOfSelElem)
        {
            IWebElement row = null;

            // Get the row element
            if (additionalCellText == null)
            {
                row = ElemGet.Grid_GetRowByRowName(tblElem, by, firstColumnCellText);
            }
            else
            {
                row = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(tblElem, by, firstColumnCellText, additionalCellText);
            }

            SelectElement selElem = new SelectElement(row.FindElement(By.XPath(string.Format(".//select[contains(@id, '{0}')]", idOfSelElem))));
            // //a[text()='_TA_AStatic User_LR_CH_001']/ancestor::tr[1]//select[contains(@id, 'ReviewStatus')]

            return selElem;
        }

        /// <summary>
        /// Gets the user-specified button element within a table by the text value of the button
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemBy">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellText">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <param name="buttonText">The exact text from the button you want/param>
        public static IWebElement Grid_GetButtonOrLinkInsideRowByText(IWebElement tblElem, By rowElemBy, string firstColumnCellText, string additionalCellText, string btnText)
        {
            IWebElement row = null;
            IWebElement btnLink = null;

            // Get the row element
            if (additionalCellText == null)
            {
                row = ElemGet.Grid_GetRowByRowName(tblElem, rowElemBy, firstColumnCellText);
            }
            else
            {
                row = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(tblElem, rowElemBy, firstColumnCellText, additionalCellText);
            }

            // Get the button element with the user-specified text and click on it
            string textOfButtonWithoutSpaces = btnText.Replace(" ", "");


            // Sometimes the text of the cell is contained within the span tag, and sometimes it is contained
            // within the button or A tag. Sometimes additional attributes are needed to find the button. We will
            // use IF statements below for these conditions. 
            string xpathStringForFirstTypeOfButton = string.Format(".//span[text()='{0}' and @data-i18n='_{1}_']", btnText, textOfButtonWithoutSpaces);
            string xpathStringForSecondTypeOfButton = string.Format(".//span[text()='{0}' and @role='button']", btnText);
            string xpathStringForThirdTypeOfButton = string.Format(".//button[text()='{0}']", btnText);
            string xpathStringFor4thTypeOfButton = string.Format(".//a[text()='{0}']", btnText);
            string xpathStringFor5thTypeOfButton = string.Format(".//span[text()='{0}']", btnText);

            if (row.FindElements(By.XPath(xpathStringForFirstTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForFirstTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringForSecondTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForSecondTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringForThirdTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringForThirdTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringFor4thTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringFor4thTypeOfButton));
            }
            else if (row.FindElements(By.XPath(xpathStringFor5thTypeOfButton)).Count > 0)
            {
                btnLink = row.FindElement(By.XPath(xpathStringFor5thTypeOfButton));
            }
            else
            {
                throw new Exception("The button/link could not be found in the table you have specified with the celltext you specified");
            }

            return btnLink;
        }

        /// <summary>
        /// Returns a datatable from a grid. Alternative to <see cref="BrowserExtensions.GetDataFromIgGrid(IWebDriver, IWebElement, DataColumnDefinition[])"/>
        /// </summary>
        /// <param name="gridColumnsClass">A class must be created that contains the columns names of the grid of type string. For an example, see the 
        /// SamplePage.NameAgeTableColumns. Once this class is crated, then pass the text: "typeof(NameAgeTableColumns)"</param>
        /// <param name="gridBodyElem">The grid body element</param>
        public static DataTable Grid_ToDatatable(Type gridColumnsClass, IWebElement gridBodyElem)
        {
            //Make Generic - pass in property specs, more simple for QA to define than json objects
            var gridProperties = Activator.CreateInstance(gridColumnsClass);

            //Build DataTable with columns relative to passed class
            DataTable gridData = new DataTable();
            var fields = ((TypeInfo)gridColumnsClass).DeclaredFields;
            foreach (FieldInfo item in fields)
            {
                gridData.Columns.Add(item.Name);
            }

            //Get table rows 
            IList<IWebElement> allRows = gridBodyElem.FindElements(By.TagName("tr"));

            foreach (IWebElement row in allRows)
            {
                //Find all cells in the row
                //IList<IWebElement> cells = row.FindElements(By.XPath(".//*[local-name(.)='th' or local-name(.)='td']"));
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                List<string> cellContent = new List<string>();

                foreach (IWebElement cell in cells)
                {
                    cellContent.Add(cell.Text);
                }

                //Add row content to data table
                gridData.Rows.Add(cellContent.ToArray());
                cellContent.Clear();
            }

            return gridData;
        }

        /// <summary>
        /// Returns the row that contains the user-specified cell
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="firstRowby">Send any row (Most likely just send the iwebelement that is a generic row 'tableId/tr' in the table, as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody). We need any row so that we can wait for it to appear before proceeding with the test"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellName">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowName(IWebElement tblElem, By firstRowby, string firstColumnCellText)
        {
            IWebElement firstColumnCell = null;

            // First wait for the table
            tblElem.WaitForElement(firstRowby, ElementCriteria.HasText, ElementCriteria.IsEnabled);
            Thread.Sleep(0400);

            // Sometimes the text of the cell is contained within the A tag, and sometimes it is contained
            // within the td tag, and even a div tag. We will use IF statements below for these conditions
            // We will also use IF statements to condition when cells have leading and trailing white space
            string xpathStringWithTDTag = string.Format(".//td[text()='{0}']", firstColumnCellText);
            string xpathStringWithTDTagAndExtraSpaces = string.Format(".//td[contains(., '{0}')]", firstColumnCellText);
            string xpathStringWithATag = string.Format(".//a[text()='{0}']", firstColumnCellText);
            string xpathStringWithATagAndExtraSpaces = string.Format(".//a[contains(., '{0}')]", firstColumnCellText);
            string xpathStringWithDivTag = string.Format(".//div[text()='{0}']", firstColumnCellText);
            string xpathStringWithDivTagAndExtraSpaces = string.Format(".//div[contains(., '{0}')]", firstColumnCellText);

            // The first IF statement will use this xpath. This is for the Learners table in the Program Dean page of CBD. This is a weird
            // table and I had to come up with a special way to locate the cell. 
            // NOTE: THE ORDER OF THE IF STATEMENTS IS CRUCIAL. DONT CHANGE THE ORDER. First RCP specific is needed. Then we need the text= before
            // the textContains because 
            string xpathStringForSpecificRCPTable = string.Format("//td/a/div[@class='row']/div[contains(., '{0}')]", firstColumnCellText);
            // If we find elements by using the TD tag text equals xpathstring for specific RCP table
            if (tblElem.FindElements(By.XPath(xpathStringForSpecificRCPTable)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringForSpecificRCPTable))[0];
            }
            // If we find elements by using the TD tag text equals xpathstring
            else if (tblElem.FindElements(By.XPath(xpathStringWithTDTag)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringWithTDTag))[0];
            }
            // else If we find elements by using the A tag text equals xpathstring
            else if (tblElem.FindElements(By.XPath(xpathStringWithATag)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringWithATag))[0];
            }
            // else If we find elements by using the DIV tag text equals xpathstring
            else if (tblElem.FindElements(By.XPath(xpathStringWithDivTag)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringWithDivTag))[0];
            }
            // else If we find elements by using the TD tag text contains xpathstring
            else if (tblElem.FindElements(By.XPath(xpathStringWithTDTagAndExtraSpaces)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringWithTDTagAndExtraSpaces))[0];
            }
            // else If we find elements by using the A tag text contains xpathstring
            else if (tblElem.FindElements(By.XPath(xpathStringWithATagAndExtraSpaces)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringWithATagAndExtraSpaces))[0];
            }

            else if (tblElem.FindElements(By.XPath(xpathStringWithDivTagAndExtraSpaces)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathStringWithDivTagAndExtraSpaces))[0];
            }

            else
            {
                throw new Exception("The cell text for either column could not be found in the table you have specified. Either the row" +
                    " does not exist in your table, or you made have to add an extra IF statement above in this method for your specific table");
            }

            // Then get the 1st parent row element
            IWebElement row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(firstColumnCell, "parent", "tr[1]");

            return row;
        }

        /// <summary>
        /// Returns the row that contains the user-specified cell
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="rowElemby">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellName">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <returns></returns>
        public static IList<IWebElement> Grid_GetRowsByRowName(IWebElement tblElem, By rowElemby, string firstColumnCellText)
        {
            // We need to instantiate this rows object, because if we dont, then when we use the Add method for this object, it will say Object
            // Reference Not Set To An Instance Of An Object. But to instantiate it, we have to use List, not iList. This is because using new()
            // means creating Objects and since IList is an interface, we can not create objects of it. 
            IList<IWebElement> rows = new List<IWebElement>();

            // First wait for the table
            tblElem.WaitForElement(rowElemby, ElementCriteria.HasText, ElementCriteria.IsEnabled, ElementCriteria.IsEnabled);
            Thread.Sleep(0300);

            // Sometimes the text of the cell is contained within the A tag, and sometimes it is contained
            // within the td tag, and even a div tag. We will use IF statements below for these conditions
            // We will also use IF statements to condition when cells have leading and trailing white space
            string xpathStringWithTDTag = string.Format(".//td[text()='{0}']", firstColumnCellText);
            string xpathStringWithATag = string.Format(".//a[text()='{0}']", firstColumnCellText);

            // If we find elements by using the TD tag text equals xpathstring
            if (tblElem.FindElements(By.XPath(xpathStringWithTDTag)).Count > 0)
            {
                foreach (IWebElement cell in tblElem.FindElements(By.XPath(xpathStringWithTDTag)))
                {
                    IWebElement nextRow = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(cell, "parent", "tr[1]");
                    rows.Add(nextRow);
                }
            }

            // else If we find elements by using the A tag text equals xpathstring
            else if (tblElem.FindElements(By.XPath(xpathStringWithATag)).Count > 0)
            {
                foreach (IWebElement cell in tblElem.FindElements(By.XPath(xpathStringWithATag)))
                {
                    rows.Add(XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(cell, "parent", "tr[1]"));
                }
            }

            else
            {
                throw new Exception("The cell text for either column could not be found in the table you have specified. Either the row" +
                    " does not exist in your table, or you made have to add an extra IF statement above for your specific table");
            }

            return rows;
        }

        /// <summary>
        /// Returns the row that contains the user-specified cell text from 2 cells. This is useful for tables that can contain non-unique rows (First column cell text can bethe same)
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="by">Your row element as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="additionalCellName">Send "null" to this parameter if your table does not allow duplicate rows for the first column. If the first column in your row does not have to be unique compared to other rows in your table, and you would want to specify an additional column value to find your row, you can do that here. Send the exact text of any other column.</param>
        /// <returns></returns>
        internal static IWebElement Grid_GetRowByRowNameAndAdditionalCellName(IWebElement tblElem, By by, string firstColumnCellText, string additionalColumnText)
        {
            IList<IWebElement> firstColumnCells = null;
            IList<IWebElement> additionalColumnCells = null;
            IWebElement row = null;

            // Sometimes the text of the cell is contained within the A tag, and sometimes it is contained
            // within the td tag. We will use IF statement below for this
            string xpathStringWithTDTag = string.Format(".//td[text()='{0}']", firstColumnCellText);
            string xpathStringWithATag = string.Format(".//a[text()='{0}']", firstColumnCellText);

            // First wait for the table
            tblElem.WaitForElement(by, ElementCriteria.HasText, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
            Thread.Sleep(0400);

            // Now find all the cells that contain the firstColumnCellText:
            // If we find the cell by using the TD tag
            if (tblElem.FindElements(By.XPath(xpathStringWithTDTag)).Count > 0)
            {
                firstColumnCells = tblElem.FindElements(By.XPath(xpathStringWithTDTag));
            }
            // Else use the A tag
            else
            {
                firstColumnCells = tblElem.FindElements(By.XPath(xpathStringWithATag));
            }

            // Loop through each cell
            foreach (IWebElement cell in firstColumnCells)
            {
                // See above comment about td tag versus a tag
                string xpathStringWithTDTagAdditionalCell = string.Format(".//td[text()='{0}']", additionalColumnText);
                string xpathStringWithATagAdditionalCell = string.Format(".//a[text()='{0}']", additionalColumnText);
                string xpathStringWithSpanTagAdditionalCell = string.Format(".//span[text()='{0}']", additionalColumnText);

                // Get the row for the current cell in the loop
                row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(cell, "parent", "tr[1]");

                // If we find the cell by using the TD tag
                if (row.FindElements(By.XPath(xpathStringWithTDTagAdditionalCell)).Count > 0 ||
                    row.FindElements(By.XPath(xpathStringWithATagAdditionalCell)).Count > 0 ||
                    row.FindElements(By.XPath(xpathStringWithSpanTagAdditionalCell)).Count > 0)
                {
                    additionalColumnCells = row.FindElements(By.XPath(xpathStringWithTDTagAdditionalCell));
                    // Return the row of this cell
                    return row;
                }
            }

            // Mostly unreachable code, blah
            return row;
        }

        /// <summary>
        /// Returns true if the text is found under the first column of the table
        /// </summary>
        /// <param name="tableBodyElem">The table element</param>
        /// <param name="tableElemBodyBy">The tbody element within the table as it exists in it's By type</param>
        /// <param name="expectedText">The text you expect to be under the first column of your table</param>
        /// <param name="tagNameThatTextExistsWithin">Inspect your table's cell to determine what type of element the text exists within. Then send the tag name to this parameter</param>
        /// <param name="FirstBtn">Enter "null" if your table does NOT contain Previous and Next button. If it does contain these buttons, then pass the First button element first, and then the Next button element. For example, pass "Bys.CBDLearnerPage.TableFirstBtn"</param>
        /// <param name="NextBtn">Enter "null" if your table does NOT contain Previous and Next button. If it does contain these buttons, then pass the First button element first, and then the Next button element. For example, pass "Bys.CBDLearnerPage.TableFirstBtn"</param>
        /// <returns></returns>
        public static bool Grid_ContainsRecord(IWebDriver browser, IWebElement tableElem, By tableElemBodyBy, string expectedText, string tagNameThatTextExistsWithin, By FirstBtn = null, By NextBtn = null)
        {
            browser.WaitForElement(tableElemBodyBy, TimeSpan.FromSeconds(180), ElementCriteria.HasText, ElementCriteria.IsEnabled, ElementCriteria.IsEnabled);

            // If the table does not contain first, next, previous and last buttons. Or if the table does contains them, 
            // but there are not enough records to have multiple pages, then we only need to check one page of results
            if (FirstBtn == null || !browser.Exists(NextBtn, ElementCriteria.IsVisible))
            {
                if (Grid_CellTextFound(tableElem, tagNameThatTextExistsWithin, expectedText))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // If the table has First, Previous, Next and Last buttons. And if those buttons are visible and enabled (The class attribute would equal "first cdisabled" if
            // it was disabled), then click the First button to go to the first listing of records
            if (browser.Exists(FirstBtn, ElementCriteria.IsVisible, ElementCriteria.AttributeValue("class", "first")))
            {
                browser.FindElement(FirstBtn).Click();
                Thread.Sleep(2000);      // TODO: Implement logic for dynamic wait instead of sleep
                                         // Check to see if the cell is found on the first page of results
                if (Grid_CellTextFound(tableElem, tagNameThatTextExistsWithin, expectedText))
                {
                    return true;
                }
            }

            if (Grid_CellTextFound(tableElem, tagNameThatTextExistsWithin, expectedText))
            {
                return true;
            }

            // If code reaches here, the cell text was not found yet, and so we should click the next button if there is one and 
            // try to find the cell text on the next page, and continue until there are no pages left
            if (browser.Exists(NextBtn, ElementCriteria.IsVisible))
            {
                while (browser.Exists(NextBtn, ElementCriteria.AttributeValue("class", "next"))) // While the next button is not disabled
                {
                    browser.FindElement(NextBtn).Click(); // Click the next button
                    Thread.Sleep(2000);  // TODO: Implement logic for dynamic wait instead of sleep

                    if (Grid_CellTextFound(tableElem, tagNameThatTextExistsWithin, expectedText))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// If any rows in the grid contain the user-specified text, return true
        /// </summary>
        /// <param name="tableBodyElem"></param>
        /// <param name="tagNameThatTextExistsWithin">Inspect your table's cell to determine what type of element the text exists within. Then send the tag name to this parameter</param>
        /// <param name="expectedText"></param>
        /// <returns></returns>
        public static bool Grid_CellTextFound(IWebElement tableBodyElem, string tagNameThatTextExistsWithin, string expectedText)
        {
            IList<IWebElement> allRows = tableBodyElem.FindElements(By.TagName("tr")); // Store all TR (rows) from the table into a variable
            foreach (var row in allRows)  // Loop through each row
            {
                if (row.FindElements(By.TagName("td")).Count > 0) // If the given row contains any cells
                {
                    IWebElement cell = row.FindElements(By.TagName("td"))[0]; // Get the cell under the first column

                    // So far in different LMS applications, I have seen the cell text represented in not only the td tag, but also the a tag within
                    // td, and also the span tag within an a tag within a td tag. So we will handle those conditions here:
                    if(tagNameThatTextExistsWithin == "td")
                    {
                        if (cell.Text == expectedText)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // Sometimes a table can have rows with certain tags, while this same table on the next row wont contain this tag. Because of this,
                        // we have to make sure we find this tag before we proceed
                        if(cell.FindElements(By.TagName(tagNameThatTextExistsWithin)).Count > 0)
                        {
                            IWebElement elemTextExistsWithin = cell.FindElement(By.TagName(tagNameThatTextExistsWithin));
                            if (elemTextExistsWithin.Text == expectedText)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static IWebElement Grid_GetMenuItemOnRowButton(IWebDriver browser, IWebElement btnParent, string menuItemText)
        {
            IWebElement dropdownMenuItem = null;

            // Sometimes the menu items are inside span tags, and sometimes a tags. We will check for both
            string xpathString = string.Format(".//span[text()='{0}']", menuItemText);
            string xpathString2 = string.Format(".//a[text()='{0}']", menuItemText);

            if (btnParent.FindElements(By.XPath(xpathString)).Count > 0)
            {
                dropdownMenuItem = btnParent.FindElement(By.XPath(xpathString), ElementCriteria.IsVisible);
            }
            else if (btnParent.FindElements(By.XPath(xpathString2)).Count > 0)
            {
                dropdownMenuItem = btnParent.FindElement(By.XPath(xpathString2), ElementCriteria.IsVisible);
            }

            else
            {
                throw new Exception("The menu item could not be found in the table you have specified with the celltext you specified");
            }

            return dropdownMenuItem;
        }

        #endregion Grids

        #region Radio Buttons

        /// <summary>
        /// Gets the radio button element based on it's label text
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="textOfRadioBtn">The exact text of one of the radio buttons inside</param>
        public static IWebElement RdoBtn_GetRdoBtn(IWebDriver browser, string textOfRadioBtn)
        {
            IWebElement rdoBtn = null;

            // Right now I have to implement the below IF statement for radio buttons, as their tags are different
            // between learners and observers. Nirav is going to fix this. Once he does, I can implement the simpler solution
            string xpathString = string.Format("//label/span[text()='{0}']", textOfRadioBtn);

            if (browser.FindElements(By.XPath(xpathString)).Count > 0)
            {
                rdoBtn = browser.FindElement(By.XPath(xpathString));
                return rdoBtn;
            }
            else
            {
                xpathString = string.Format("//label[text()='{0}']", textOfRadioBtn);
                rdoBtn = browser.FindElement(By.XPath(xpathString));
                return rdoBtn;
            }
        }

        public static IList<IWebElement> RdoBtn_GetRdoBtns(IWebElement rdoBtn)
        {
            // Get the first ancestor of the radio button with the tag name of tbody. Radio buttons
            // in CBD are within tables
            //string xpath = "ancestor::tbody[1]";
            string xpath = "ancestor::div[@role='radiogroup']";
            IList<IWebElement> allRdoBtns = null;

            try
            {
                IWebElement parentTableOfRdoBtns = rdoBtn.FindElement(By.XPath(xpath));
                allRdoBtns = parentTableOfRdoBtns.FindElements(By.TagName("span"));

                // Some radio buttons in RCP are located within spans, some are labels, so we need to condition this
                if (allRdoBtns.Count == 0)
                {
                    allRdoBtns = parentTableOfRdoBtns.FindElements(By.TagName("label"));
                }

                return allRdoBtns;
            }
            catch
            {

            }

            return null;
        }

        #endregion Radio Buttons


        #region General

        /// <summary>
        /// This method checks whether an element is currently visible to the eye on the screen. Selenium's Display property did not accomplish this,
        /// so I created this method.
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="elem">The element to verify</param>
        public static bool GeneralElemVisibleOnScreen(IWebDriver browser, IWebElement elem)
        {
            // See: http://darrellgrainger.blogspot.com/2013/05/is-element-on-visible-screen.html
            int x = browser.Manage().Window.Size.Width;
            int y = browser.Manage().Window.Size.Height;
            int x2 = elem.Size.Width + elem.Location.X;
            int y2 = elem.Size.Height + elem.Location.Y;

            return x2 <= x && y2 <= y;
        }


        #endregion General
    }


}