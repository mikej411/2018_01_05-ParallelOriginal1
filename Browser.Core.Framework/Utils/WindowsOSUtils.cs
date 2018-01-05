using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class retreiving elements through various methods
    /// </summary>
    public static class WindowsOSUtils
    {



        /// <summary>
        /// Runs any AutoIt script that you create. For an example, see: https://code.premierinc.com/docs/display/PGHLMSDOCS/Uploading+Files
        /// </summary>
        /// <param name="scriptFileLocation">The AutoIt script file. For example, our upload script is called FileUpload.au3 and should be located here: C:\SeleniumAutoIt\FileUpload.au3</param>
        /// <param name="arguments">The list of strings you want to pass as arguments to the AutoIt script. You can test this out in the CMD line of windows. i.e. Inside the command prompt, navigate to the script location, then type the script name with it's parameters: fileupload.exe "internetexplorer" "c:\seleniumautoit.exe"</param>
        public static void RunAutoItScript(string scriptFile, params string[] arguments)
        {
            string args = "";

            foreach (string arg in arguments)
            {
                //if (arg.Split(' ').Length > 1)
                //{
                //    string subarg = "\"";
                //    foreach (string str in arg.Split(' '))
                //    {
                //        subarg += str + " ";
                //    }
                //    subarg.Trim();
                //    subarg += "\"";
                //    args += subarg + " ";
                //    continue;
                //}

                args += arg + " ";
            }


            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = args;
            // Enter the executable to run, including the complete path
            start.FileName = scriptFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Normal;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }


            //{
            //    new Thread(new ThreadStart(delegate ()
            //    {
            //        string args = "";


            //        foreach (string arg in arguments)
            //        {
            //            if (arg.Split(' ').Length > 1)
            //            {
            //                string subarg = "\"";
            //                foreach (string str in arg.Split(' '))
            //                {
            //                    subarg += str + " ";
            //                }
            //                subarg.Trim();
            //                subarg += "\"";
            //                args += subarg + " ";
            //                continue;
            //            }

            //            args += arg + " ";
            //        }

            //        var info = new ProcessStartInfo()
            //        {
            //            FileName = scriptFile,
            //            Arguments = args

            //        };

            //        Process.Start(info);

            //    })).Start();
            //}

        }
    }




}