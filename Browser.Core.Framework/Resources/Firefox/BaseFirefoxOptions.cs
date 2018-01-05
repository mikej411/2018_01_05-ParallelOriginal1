using OpenQA.Selenium.Firefox;

namespace Browser.Core.Framework.Resources
{
    /// <summary>
    /// Base FirefoxOptions for a new FirefoxDriver.
    /// </summary>
    public class BaseFirefoxOptions : FirefoxOptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseFirefoxOptions()
        {
            // The below line is obsolete as of now and will fail, but we are not using Firefox right now, so I am commenting out for now
            //IsMarionette = false;
            //LogLevel = FirefoxDriverLogLevel.Trace;
        }
    }
}
