using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Browser.Core.Framework
{
    public static class DataUtils
    {
        #region Properties

        private static Random random = new Random();

        #endregion Properties

        #region Dates

        /// <summary>
        /// Returns 3 key-value pairs to use with our Date controls. 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDateForCalendarElem(DateTime date)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("year", date.ToString("yy"));
            dictionary.Add("month", date.ToString("MMMM"));
            dictionary.Add("day", date.Day.ToString("00"));

            return dictionary;
        }  

        /// <summary>
        /// Returns a random date between 2 dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DateTime GetRandomDateBetween(DateTime startDate, DateTime endDate)
        {

            Random gen = new Random();
            int range = (endDate - startDate).Days;
            DateTime randonmDate = startDate.AddDays(gen.Next(range));
            return randonmDate;
        }

        #endregion Dates 

        /// <summary>
        /// Compares a group of integers and returns true if they are equal
        /// </summary>
        /// <param name="ints"> integers</param>
        /// <returns> if all ints are equals will return true otherwise false</returns>
        public static bool intsEqual(params int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                if (ints[i] != ints[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a random integer from the range of values below the user-specified maximum value. 
        /// If 0 is passed for minValue and 3 for maxValue, then this will return either 0, 1, or 2
        /// </summary>
        /// <param name="minValue">The minimum value of the range</param>
        /// <param name="maxValue">The maximum value of the range</param>
        public static int GetRandomIntegerWithinRange(int minValue, int maxValue)
        {
            Random r = new Random();
            int rInt = r.Next(minValue, maxValue);
            return rInt;
        }

        public static List<string> RemoveConsecutiveSpacesFromList(SelectElement elem)
        {
            List<string> items = new List<string>();
            foreach (var item in elem.Options)
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                items.Add(regex.Replace(item.Text, " "));
            }
            return items;
        }

        public static DataTable ConvertVariousTypeDataTableToStringDataTable(DataTable dt)
        {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }

        public static DataTable RemoveConsecutiveSpacesFromDatatable(DataTable table)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);

            // Loop through each row of the table
            table.AsEnumerable().ToList().ForEach(row =>
            {
                var cellList = row.ItemArray.ToList();
                // Loop through each cell of the table
                foreach (var cell in cellList)
                {
                    // Remove consecutive spaces for each cell
                    var blah = cell.ToString();
                    regex.Replace(blah, " ");
                }
            });
            return table;
        }

        /// <summary>
        /// Returns a random integer from the range of values below the user-specified maximum value
        /// </summary>
        /// <param name="maxValue">The maximum value of integers that you want to return. i.e. if you pass 2, the method will return either 1 or 2</param>
        public static int GetRandomInteger(int maxValue)
        {
            Random random = new Random();
            int optionIndex = random.Next(maxValue);
            var randomInt = optionIndex++;
            return randomInt;
        }

        public static string GetRandomSentence(int sizeOfString)
        {
            var sb = new StringBuilder();

            // Have to add 1 to sizeOfString here because at the end of this method we trim off any white space,
            // which is most likely only going to be one space
            while (sb.Length < sizeOfString + 1)
            {
                int wordLength = random.Next(8) + 1;
                sb.Append(GetRandomString(wordLength)).Append(" ");
            }

            sb.Length = sizeOfString;
            // We are trimming at the end just in case we have any white space character at the end of the 
            // randomized string. I then had add 1 to the sizeOfString variable everywhere else.
            return sb.ToString().TrimEnd();
        }

        public static string GetRandomString(int sizeOfString)
        {
            const string chars = "AbCDeFgHI12345678";

            var randomChars =
                InitInfinite(() => chars[random.Next(chars.Length)])
                    .SkipWhile(c => c == ' ')
                    .Take(sizeOfString);

            return new string(randomChars.ToArray());
        }

        public static Guid GetRandomUuid()
        {
            return new Guid();
        }

        public static IEnumerable<T> InitInfinite<T>(Func<T> selector)
        {
            while (true)
            {
                yield return selector();
            }
        }



        public static List<string> DatatableToListString(DataTable dataTable)
        {
            List<string> myList = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                myList.Add((string)row[0]);
            }
            return myList;
        }

        public static string GetStringBetweenCharacters(string originalString, string firstCharacter, string secondCharacter)
        {
            int start = originalString.IndexOf(firstCharacter) + 1;
            int end = originalString.IndexOf(secondCharacter, start);
            string result = originalString.Substring(start, end - start);
            return result;
        }

        public static string GetStringAfterCharacter(string originalString, string characterBeforeStartOfNewString, int indexOfStartOfNewStringAfterCutoffCharacter)
        {
            string newString = originalString.Substring(originalString.IndexOf(characterBeforeStartOfNewString) + 2);
            return newString;
        }

        /// <summary>
        /// Returns the cell text in a datatable
        /// </summary>
        /// <param name="datatable">Your datatable</param>
        /// <param name="rowNumber">The row number </param>
        /// <param name="columnName">The column name</param>
        public static string GetDatatableCellByRowNumAndColName(DataTable datatable, int rowNumber, string columnName)
        {
            var columnNameWithoutSpaces = Regex.Replace(columnName, @"\s+", "");
            return datatable.Rows[rowNumber][columnNameWithoutSpaces].ToString();
        }

        /// <summary>
        /// Returns the cell text in a datatable
        /// </summary>
        /// <param name="datatable">Your datatable</param>
        /// <param name="colNameBelongingToRecord">The column name of the first column from the DataTable</param>
        /// <param name="recordName">The row name, or in other words, the text inside the cell of the row you are targeting</param>
        /// <param name="colNameOfReturningCell">The column name of the column that you are targeting to get the cell text from</param>
        public static string GetDataTableCellByRecNameAndColName(DataTable datatable, string colNameBelongingToRecord, string recordName, string colNameOfReturningCell)
        {
            string cellText = datatable.AsEnumerable()
                                        .Where(f => f.Field<string>(colNameBelongingToRecord) == recordName)
                                        .Select(f => f.Field<string>(colNameOfReturningCell)).FirstOrDefault();
            return cellText;
        }

        // http://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
        public static List<string> CustomSortListWithDashes(List<string> list)
        {
            var sortedIOrderedEnumerableResult = list.OrderBy(x => PadNumbersForListWithDashes(x));
            return sortedIOrderedEnumerableResult.ToList();
        }

        public static string PadNumbersForListWithDashes(string input)
        {
            input = input.Replace('-', '#');
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }

        // http://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
        public static List<string> CustomSortForListWithUnderscores(List<string> list)
        {
            var sortedIOrderedEnumerableResult = list.OrderBy(x => PadNumbersForListWithUnderscores(x));
            return sortedIOrderedEnumerableResult.ToList();
        }

        public static string PadNumbersForListWithUnderscores(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }


        /// <summary>
        /// Utility used for minor string conversions to match data found in pivot grids.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertNumberToStringWithCommas(long x)
        {
            var numberConvertedToInt = (int)x;
            return numberConvertedToInt.ToString("N0");
        }

        public static bool GetRandomBool()
        {
            Random r = new Random();
            bool tf = false;

            for (int i = 1; i <= 15; i++)
            {
                tf = r.Next(2) == 0 ? false : true;
            }

            return tf;
        }
    }

    #region Classes that represent sets of data


    #endregion Classes that represent sets of data
}