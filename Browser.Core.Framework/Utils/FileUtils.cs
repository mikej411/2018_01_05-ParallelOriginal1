using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Browser.Core.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Checks for the file once every second until the file exists and is not locked or the timeout is reached.
        /// </summary>
        /// <param name="filePath">The relative or absolute path to a file.</param>
        /// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 10000 (10 seconds).</param>
        /// <exception cref="System.TimeoutException">Thrown if the file does not exist within the timeout specified.</exception>
        public static void WaitForFile(string filePath, double fileWaitTimeout = 10000)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var fi = new FileInfo(filePath);
            while (!File.Exists(filePath) || IsFileLocked(fi))
            {
                if (sw.ElapsedMilliseconds > fileWaitTimeout)
                {
                    var msg = "The file \"{0}\" was not found within the timeout period of {1} milliseconds.";
                    if (!File.Exists(filePath))
                        msg += "  The file does not exist.";
                    else if (IsFileLocked(fi))
                        msg += "  The file is locked by another user or process.";
                    throw new TimeoutException(string.Format(msg, filePath, fileWaitTimeout));
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Tries to lock the given file.  If locking fails returns true; otherwise, returns false.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }

            //file is not locked
            return false;
        }

        public static string CreateFile(string folderLocation, string fileNameAndExtension)
        {
            // var folderLocation = "c:\\TestCases\\PerformanceResults";

            // Create the above directory folder if it doesnt exist
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }

            string filePath = folderLocation + string.Format("\\\\{0}", fileNameAndExtension); //"\\\\Results.csv";

            // Create the above file if it doesnt exist
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            WaitForFile(filePath);

            return filePath;
        }

        public static void DeleteFile(string filePath)
        {
            // Create the above file if it doesnt exist
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Uploads any file through the windows upload file window using Autoit. For an explanation, 
        /// see: https://code.premierinc.com/docs/display/PGHLMSDOCS/Uploading+Files
        /// Note that this only works in Chrome locally. For full compatibility of uploading files,
        /// <see cref="UploadFileUsingsendKeys(IWebDriver, IWebElement, string)"/>
        /// </summary>
        /// <param name="browseBtnElem">The Browse button element</param>
        /// <param name="scriptFileLocation">The AutoIt script file. Our upload script is called FileUpload.au3 and should be located here: C:\SeleniumAutoIt</param>
        /// <param name="browserName">Either "chrome", "firefox" or "internetexplorer"</param>
        /// <param name="fileToUploadLocation">The file location and file name. i.e. C:\SeleniumAutoIt\test.txt</param>
        public static void UploadFileWithAutoIt(IWebElement browseBtnElem, string scriptFileLocation, string browserName, string fileToUploadLocation)
        {
            browseBtnElem.Click();
            WindowsOSUtils.RunAutoItScript(scriptFileLocation, browserName, fileToUploadLocation);
            Thread.Sleep(0300);
        }

        /// <summary>
        /// Uploads a file by directly sending the file path to the JQuery File Upload browse button. To 
        /// determine if you application is compatible (it uses the JQuery file upload system), there 
        /// should be an input tag in your HTML representing the browse/fileupload/etc button (Note
        /// that this button may be hidden under a browse button that doesnt meet these requirements) 
        /// This Input element should have an attribute titled "type", and it's attribute value should be
        /// "file". If you find it still doesnt work, then tell your developer to change this element from multiple
        /// to single. Further reading: https://stackoverflow.com/questions/27331884/selenium-chromedriver-sendkeys-breaks-jquery-file-upload-plugin</param>
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="browseBtnElem">The browse button of IWebElement type, which should be the Input tag with an attribute of "file". See summary above for further explanation </param>
        /// <param name="browseBtnBy">The browse button of By type, which should be the Input tag with an attribute of "file". See summary above for further explanation </param>
        /// <param name="filePath">The full path, including the filename</param>
        public static void UploadFileUsingSendKeys(IWebDriver browser, IWebElement browseBtnElem, By browseBtnBy, string filePath)
        {
            // If this button is not visible, then we need to remove the style attribute, as the style attribute most likely has a value
            // of "display:none", which controls whether it is displayed or not
            browser.ExecuteScript("arguments[0].removeAttribute(\"style\");", browseBtnElem);
            // We also have to remove the multiple attribute, if it has one
            browser.ExecuteScript("arguments[0].removeAttribute(\"multiple\");", browseBtnElem);


            // Once the button is displayed, then send keys
            browser.WaitForElement(browseBtnBy, TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
            browseBtnElem.SendKeys(filePath);
            Thread.Sleep(0300);
        }
    }
}