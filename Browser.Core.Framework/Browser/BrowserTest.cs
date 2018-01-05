using Browser.Core.Framework.Resources;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using LOG4NET = log4net.ILog;
using NUnit.Framework.Interfaces;

namespace Browser.Core.Framework
{
    /// <summary>
    /// Base class for all Selenium tests.  Handles setup and configuration to run tests against multiple
    /// web browsers (Chrome, Firefox, IE).
    /// </summary>
	public abstract class BrowserTest
    {
        #region Private Fields

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _browserName;

        // Support for Selenium Grid Testing
        private bool _isRemote = false;
        private string _version = null;
        private string _platform = null;
        private string _hubUri = null;
        private string _extrasUri = null;

        private Stopwatch _testStopwatch;
        private string initialUrl = null;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the context associated with the test fixture.  There is a SEPARATE context associated with each TEST.
        /// You can access the test context by calling TestContext.Current within any given test.
        /// </summary>
        protected TestContext FixtureContext { get; private set; }

        /// <summary>
        /// Gets the initial URL that the browser shows upon startup in selenium.
        /// Most browser drivers define this, but if not we'll default to 'about:blank'.
        /// about:blank is supported in Chrome, Firefox, and IE
        /// </summary>        
        protected string InitialUrl
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(initialUrl))
                    return initialUrl;

                return "about:blank";
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the browser for the current test.  You can specify whether to re-use an existing browser instance or force a new
        /// browser instance by applying the BrowserModeAttribute to the test method or test class.
        /// </summary>        
        public IWebDriver Browser { get; private set; }

        /// <summary>
        /// The name of the browser to use for this test.
        /// </summary>
        public string BrowserName
        {
            get { return _browserName; }
            private set { _browserName = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserTest"/> class.
        /// </summary>
        /// <param name="browserName">The name of the browser to use.</param>
        public BrowserTest(string browserName)
        {
            if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(browserName.ToString());
            // I had to comment this out and change it to browerName.TosTring() because we are using TFS 2013 for build. The "nameof" is C#6.0 syntax,
            // and TFS 2013 does not build with C#6.0 syntax
            // if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(nameof(browserName));

            _isRemote = false;
            BrowserName = browserName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserTest"/> class,
        /// for remote testing against a Selenium Grid.
        /// </summary>
        /// <param name="browserName">The browser name to test.</param>
        /// <param name="version">The version of the browser to test.</param>
        /// <param name="platform">The platform to run the browser on.</param>
        /// <param name="hubUri">The uri of the Selenium Hub.</param>
        /// <param name="extrasUri">The uri for Selenium Extras.</param>
        public BrowserTest(string browserName, string version, string platform, string hubUri, string extrasUri)
        {
            // Note: version, platform, hubUri, and extrasUri are not "optional" so that this constructor
            // can work with NUnit. 

            if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(browserName.ToString());
            // I had to comment this out and change it to browerName.TosTring() because we are using TFS 2013 for build. The "nameof" is C#6.0 syntax,
            // and TFS 2013 does not build with C#6.0 syntax
            // if (string.IsNullOrEmpty(browserName)) throw new ArgumentException(nameof(browserName));

            _isRemote = true;

            BrowserName = browserName;

            // Note: Selenium Grid does not like when any of these parameters are null.
            _version = version ?? string.Empty;
            _platform = platform ?? string.Empty;

            _hubUri = string.IsNullOrEmpty(hubUri) ? SeleniumCoreSettings.HubUri : hubUri;
            _extrasUri = string.IsNullOrEmpty(extrasUri) ? SeleniumCoreSettings.ExtrasUri : extrasUri;
        }

        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Override this method to perform setup logic that should occur before EVERY TEST
        /// </summary>
        [SetUp]
        public virtual void TestSetup()
        {
            // We need to set the security protocol for APIs that we use here, so APIs can be called through our automation code
            // Note this may change from time to time, so well need to update this code
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            var context = TestContext.CurrentContext;
            var mode = GetBrowserMode();
            _log.Info(TestSetupLogMessage(context));
            // Determine whether to create a new browser session or reuse the existing one
            // Only create a new session if it was explicitly requested, or if this is the first test.
            if (Browser == null || mode == BrowserMode.New)
            {
                var sw = Stopwatch.StartNew();

                // Note: Each test can determine if it uses "New". Therefore, I can't do
                // the cleanup until the next test starts. Also note "Reuse" does not work 
                // across test fixtures.
                CleanupBrowser();

                CreateBrowser();
                sw.Stop();

                // Log requested and actual browser, version and platform
                string requestedVersion = _version ?? "Unknown";
                string requestedPlatform = _platform ?? "Unknown";
                var cap = Browser.GetCapabilities();

                _log.InfoFormat(
                string.Format("Browser Setup: Requested Browser={0}, Requested Version={1}, Requested Platform{2}, Actual Browser={3}, Actual Version={4}, Actual Platform{5}, Time={6}", BrowserName, requestedVersion, requestedPlatform, cap.BrowserName, cap.Version, cap.Platform, sw.Elapsed));
            }
            else
            {
                Browser.Navigate().GoToUrl(InitialUrl);
            }

            _testStopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Override this method to perform cleanup that should be performed after EVERY TEST
        /// </summary>
        [TearDown]
        public virtual void TestTearDown()
        {
            var context = TestContext.CurrentContext;
            _testStopwatch.Stop();
            _log.Info(TestTeardownLogMessage(context, _testStopwatch.Elapsed));

            LogAlertIfPresentAndDismiss();


            if (_isRemote)
            {
                // Note: I write to the console so that TFS will show the video
                // link when viewing test results.

                var sessionId = GetSessionId();
                var videoLink = string.Format("{0}download_video/{1}.mp4", _extrasUri, sessionId);

                var msg = string.Format("Closing remote session {0}, video={1}, screenshot={2}", sessionId, videoLink, " ");
                //var msg = string.Format("Closing remote session {0}, video={1}, screenshot{2}", sessionId, videoLink, screenshotLocation);
                Console.WriteLine(msg);
                _log.Info(msg);
            }

            string screenshotLocation = "";
            if (context.Result.Outcome.Status == NUnit.Framework.Interfaces.ResultState.Failure.Status)
            {
                screenshotLocation = TakeScreenshot(context.Test);
            }

        }

        /// <summary>
        /// Override this method to perform setup logic that should be performed once per CLASS
        /// </summary>
        [OneTimeSetUpAttribute]
        public virtual void Setup()
        {
            // Track the test FIXTURE context.  This is different than the TEST context.
            FixtureContext = TestContext.CurrentContext;

            // Not sure I can explain this, but in this project, log4Net doesn't
            // seem to get configured automatically (via the Assembly XmlConfiguratorAttribute)
            // The "fix" is to call configure manually
            if (!log4net.LogManager.GetRepository().Configured)
            {
                log4net.GlobalContext.Properties["LogFileName"] = @"C:\\file1";
                log4net.Config.XmlConfigurator.Configure();
                _log.Info("Manually configured log4net");
            }
        }

        /// <summary>
        /// Override this method to perform TearDown logic that should be performed once per CLASS
        /// </summary>
        [OneTimeTearDownAttribute]
        public virtual void TearDown()
        {
            CleanupBrowser();
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Provides a way to customize the InternetExplorer configuration
        /// </summary>
        protected virtual BaseInternetExplorerOptions InternetExplorerOptions
        {
            get { return new BaseInternetExplorerOptions(); }
        }

        /// <summary>
        /// Provides a way to customize the Chrome configuration
        /// </summary>
        protected virtual BaseChromeOptions ChromeOptions
        {
            get { return new BaseChromeOptions(); }
        }

        /// <summary>
        /// Provides a way to customize the Firefox configuration.
        /// </summary>
        protected virtual BaseFirefoxProfile FirefoxProfile
        {
            get { return new BaseFirefoxProfile(); }
        }

        /// <summary>
        /// Provides a way to customize the Firefox options.
        /// </summary>
        protected virtual BaseFirefoxOptions FirefoxOptions
        {
            get { return new BaseFirefoxOptions(); }
        }

        /// <summary>
        /// Gets the message that is logged when the test begins.  
        /// The default is "BeginTest &gt;&gt; (TestName) - IEDRiverServer: (processcount), chromedriver: (processcount)"
        /// </summary>
        /// <returns></returns>
        protected string TestSetupLogMessage(TestContext context)
        {
            return string.Format("BeginTest >> {0} - {1}", context.Test.FullName, GetDriverCountMessage());
        }

        /// <summary>
        /// Gets the message that is logged when the test begins.  
        /// The default is "EndTest &lt;&lt; (TestName) - (testStatus) - (elapsedTime) - IEDRiverServer: (processcount), chromedriver: (processcount)"
        /// </summary>
        /// <returns></returns>
        protected string TestTeardownLogMessage(TestContext context, TimeSpan elapsedTime)
        {
            return string.Format("EndTest << {0} - {1} - {2}", context.Result.Outcome, elapsedTime, GetDriverCountMessage());
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the CURRENT BrowserMode based on attributes applied to either the Test or the TestFixture.
        /// If no attributes are found, it defaults to TestProperties.BrowserModeDefault.
        /// </summary>
        /// <returns></returns>
        protected BrowserMode GetBrowserMode()
        {
            // The below block of code broke when upgrading from Nunit 2.6.4 to Nunit 3. So for now, we will just return Browsermode.New
            //BrowserMode? mode = GetContextProperty<BrowserMode?>(SeleniumCoreSettings.BrowserModeKey, p => (BrowserMode)Enum.Parse(typeof(BrowserMode), (string)p));
            //if (mode.HasValue)
            //    return mode.Value;

            //return SeleniumCoreSettings.BrowserModeDefault;

            return BrowserMode.New;
        }

        /// <summary>
        /// Gets a property value as applied to EITHER the Test OR the TestFixture.
        /// This allows the developer to apply attributes in EITHER location.  Attributes
        /// applied to a Test will take precedence over attributes applied to a TestFixture.
        /// <![CDATA[http://www.nunit.org/index.php?p=property&r=2.4.8]]>
        /// </summary>
        /// <typeparam name="T">The type of property value.  This is typically string, int, or double, or an enum.</typeparam>
        /// <param name="propertyName">Name of the property to find.</param>
        /// <param name="converter">(Optional): The converter that can convert from the primitive type (since NUnit only recognized string, int, double) into the specified type.  If this property is omitted, the result will simply be casted to type T.</param>
        /// <returns>The property (if specified) or default(T) if not.</returns>
        protected T GetContextProperty<T>(string propertyName, Func<object, T> converter = null)
        {
            // If no converter is specified, attempt to simply cast the property to the specified type
            var convert = converter ?? new Func<object, T>(p => (T)p);

            // First check the CURRENT context to see if the BrowserMode was specified
            if (TestContext.CurrentContext.Test.Properties.ContainsKey(propertyName))
                return convert(TestContext.CurrentContext.Test.Properties[propertyName]);

            // If the property was not found on the current context, next check the FIXTURE CONTEXT.
            if (FixtureContext.Test.Properties.ContainsKey(propertyName))
                return convert(FixtureContext.Test.Properties[propertyName]);

            // If the property was not found on the fixture context, next check the app.config's AppSettings
            if (ConfigurationManager.AppSettings.AllKeys.Contains(propertyName))
                return convert(ConfigurationManager.AppSettings[propertyName]);

            // If all else fails, just return the default
            return default(T);
        }

        private void CreateLocalBrowser(TimeSpan commandTimeout)
        {
            string driverPath = SeleniumCoreSettings.DriverLocation;

            if (BrowserName == BrowserNames.Chrome)
            {
                Browser = new ChromeDriver(driverPath, ChromeOptions, commandTimeout);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                var service = InternetExplorerDriverService.CreateDefaultService(driverPath);
                service.LogFile = GetFileName("IEDriverServer.log", SeleniumCoreSettings.DriverLogsLocation);
                service.LoggingLevel = InternetExplorerDriverLogLevel.Trace;

                Browser = new InternetExplorerDriver(service, InternetExplorerOptions, commandTimeout);
                // Note: This may not be imporant anymore if we're using javascript events (enablenativeevents=false)
                // but we'll leave it for now.
                ForceIEZoomLevel(Browser);
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                // NOTE: Only some of the FirefoxDriver constructors use the Marionette driver. 
                // See this page for more information:
                // https://seleniumhq.github.io/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Firefox_FirefoxDriver.htm

                // NOTE: I want the ability to pass a Profile into the constructor of the FirefoxDriver. 
                // Ideally I'd like to use the new Marionette Driver, but it currently (July 2016) does 
                // not support accepting a profile. I believe this is a known issue in the .NET FirefoxDriver:
                //      https://github.com/seleniumhq/selenium/issues/2296
                // Therefore, in the meantime, I plan to use the older Firefox driver that supports
                // accepting a Profile. Note further that Firefox version 47 notoriously stopped working 
                // with this older driver, but the issue has since been resolved in Firefox 47.0.1. So,
                // it is necssary that these automated tests work with Firefox 47.0.1 or higher.

                // OPTION - Use the original Firefox driver
                //Browser = new FirefoxDriver(new FirefoxBinary(), FirefoxProfile, commandTimeout);


                // OPTION - Use the original Firefox driver, but with capabilities instead. This approach
                //          also allows me to pass in the Profile, but not a commandTimeout 
                //var caps = FirefoxOptions.ToCapabilities() as DesiredCapabilities;
                //if (FirefoxProfile != null)
                //{
                //    caps.SetCapability(FirefoxDriver.ProfileCapabilityName, FirefoxProfile);
                //}
                //Browser = new FirefoxDriver(caps);


                // OPTION - Use the new Marionette driver with CreateDefaultService - but can't pass in profile?
                // UPDATE: See the options.Profile line of code below. I think that is how we will pass the profile
                // https://github.com/seleniumhq/selenium/issues/2296
                var service = FirefoxDriverService.CreateDefaultService(@"C:\seleniumdrivers", "geckodriver.exe");
                service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

                // DesiredCapabilities dc = new FirefoxOptions().(firefoxProfile).addTo(DesiredCapabilities.firefox());
                //    driver = new FirefoxDriver(dc);



                var options = new FirefoxOptions();
                // https://github.com/SeleniumHQ/selenium/issues/2645
                options.Profile = FirefoxProfile;
                Browser = new FirefoxDriver(service, options, commandTimeout);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Browser name of {0} not recognized.", BrowserName));
            }

            _log.Info(string.Format("Starting local session {0}", GetSessionId()));
        }

        private void CreateRemoteBrowser(TimeSpan commandTimeout)
        {
            DesiredCapabilities caps = null;
            if (BrowserName == BrowserNames.Chrome)
            {
                caps = ChromeOptions.ToCapabilities() as DesiredCapabilities;

                // MJ 10/23/17 - These next 4 lines were placed outside of the If statements, but when I upgraded to Selenium 3.6, they threw exceptions for IE and Chrome
                // 04/04/2015 - These go unused.... for now...
                var buildNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                caps.SetCapability("build", buildNumber);
                caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            }
            else if (BrowserName == BrowserNames.InternetExplorer)
            {
                caps = InternetExplorerOptions.ToCapabilities() as DesiredCapabilities;
            }
            else if (BrowserName == BrowserNames.Firefox)
            {
                // 04/04/2015 - By default, I use the Marionette Driver, which is capable of accepting a profile in this way when using the RemoteWebDriver.
                caps = FirefoxOptions.ToCapabilities() as DesiredCapabilities;


                // MJ 10/23/17 - This next line of code plus this If statement stopped working when I upgraded to Selenium 3.6,. They threw exceptions.
                // This might be the cause: https://github.com/SeleniumHQ/selenium/issues/4704 So maybe these capabilities dont exist anymore with new versions. For example,
                // e.g. "The error message is telling you exactly what the issue is. The IE driver does not allow you to accept self-signed certificates. In truth, it never has been able to do so. What has changed is that the driver now implements the W3C WebDriver specification that dictates that an error be thrown if capabilities cannot be matched. You specifically requested a browser with the capability that could accept self-signed certificates, but there is no matching browser for that set of capabilities (because the IE driver does not)."
                //if (FirefoxProfile != null)
                //caps.SetCapability("acceptInsecureCerts", true);    
                //{
                //    caps.SetCapability(FirefoxDriver.ProfileCapabilityName, FirefoxProfile.ToBase64String());
                //}
            }
            else
            {
                throw new InvalidOperationException(string.Format("Uknonwn browser name {0}", BrowserName));
            }

            caps.SetCapability(CapabilityType.Version, _version);
            caps.SetCapability(CapabilityType.Platform, _platform);

            Console.WriteLine(_hubUri);
              
            Browser = new RemoteWebDriver(new Uri(_hubUri), caps, commandTimeout);
            ((RemoteWebDriver)Browser).FileDetector = new LocalFileDetector();
            _log.Info(string.Format("Starting remote session {0}", GetSessionId()));
        }

        /// <summary>
        /// Creates and initializes the browser based on the _driverType.
        /// </summary>
        protected void CreateBrowser()
        {
            var commandTimeout = SeleniumCoreSettings.CommandTimeout;

            if (!_isRemote)
            {
                CreateLocalBrowser(commandTimeout);
            }
            else
            {
                CreateRemoteBrowser(commandTimeout);
            }

            // Firefox (in particular) will sometimes throw an ElementNotVisibleException if the desired element is off screen (scrolled out of view).
            // For consistency, I want all tests to run in a maximized window.  If a test needs other behavior, it can override this locally
            Browser.Manage().Window.Maximize();
            // Store the initial url so I can reset the browser back to this url after each test
            // this makes it "safer" to Reuse the browser session by ensuring a consisten state for each test.
            initialUrl = Browser.Url;
        }

        /// <summary>
        /// Disposes the Browser if it still exists.
        /// </summary>
        protected void CleanupBrowser()
        {
            if (Browser == null)
                return;

            try
            {   // Need to add this If statement workaround to close firefox with geckodriver, as geckodriver has a known issue when closing without the 
                // below workaround. See https://github.com/mozilla/geckodriver/issues/225 and https://github.com/mozilla/geckodriver/issues/285
                if (BrowserName == BrowserNames.Firefox)
                {
                    Browser.Navigate().GoToUrl("about:config");
                    Browser.Navigate().GoToUrl("about:blank");
                }
                Browser.Dispose();
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Failed to close the driver: {0}", ex);
            }
        }

        /// <summary>
        /// Gets the count of running processes that match the given name.  Do NOT include the file extension (i.e. specify "explorer" instead of "explorer.exe").
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns></returns>
        protected int GetProcessCount(string processName)
        {
            try
            {
                var procs = System.Diagnostics.Process.GetProcessesByName(processName);
                return procs.Count();
            }
            catch (Exception ex)
            {
                _log.DebugFormat("Unable to log process information for {0}: {1}", processName, ex);
                return -1;
            }
        }

        #endregion

        #region Private Methods  

        private string GetSessionId()
        {
            return ((IHasSessionId)Browser).SessionId.ToString();
        }

        private void LogAlertIfPresentAndDismiss()
        {
            try
            {
                var alert = Browser.SwitchTo().Alert();
                if (alert != null)
                {
                    _log.ErrorFormat("Unexpected alert: {0}", alert.Text);
                    alert.Dismiss(); // This maintains the old behavior of dismissing alerts.  This may be necessary since I re-use browser sessions by default.
                }
            }
            catch (NoAlertPresentException)
            {
                // Nothing to do here.  I only wanted to log the message if one exists
            }
        }

        /// <summary>
        /// If I lost communication with the RemoteWebDriver, I may not be able to get the url.
        /// I don't want to interfere with properly closing the driver, so I wrap it in a try/catch
        /// </summary>
        /// <returns></returns>
        private string GetUrlOrDefault()
        {
            try
            {
                return Browser.Url;
            }
            catch (Exception ex)
            {
                return string.Format("Unable to get url: {0}", ex);
            }
        }

        private string GetFileName(string fileName, string directory)
        {
            var fullPath = String.Concat(directory, fileName);
            int x = 2;
            // Handle naming collisions
            while (File.Exists(fullPath))
            {
                fullPath = String.Concat(directory, fileName, " (", x.ToString(), ")");
                x++;
            }
            return fullPath;
        }

        private void CreateDirectoryIfNotExists(string fileName)
        {
            var info = new DirectoryInfo(Path.GetDirectoryName(fileName));

            if (!info.Exists)
            {
                info.Create();
            }
        }

        /// <summary>
        /// Forces the zoom level to be 100%.  This is important so that clicks and locations are reported
        /// accurately.
        /// </summary>
        /// <param name="Driver"></param>
        private void ForceIEZoomLevel(IWebDriver Driver)
        {
            var body = Driver.WaitForElement(By.TagName("html"));
            // Ctrl+0 is the IE shortcut to reset the zoom level to 100%
            body.SendKeys(Keys.Control + "0");
        }

        private string GetDriverCountMessage()
        {
            var browsers = new[] { "IEDriverServer", "chromedriver" };
            List<string> values = new List<string>();
            foreach (var i in browsers)
            {
                values.Add(string.Format("{0}: {1}", i, GetProcessCount(i)));
            }

            return string.Join(", ", values);
        }

        #region Print Screen

        //Takes the screenshot and will save file in bin/debug
        private string TakeScreenshot(TestContext.TestAdapter test)
        {
            var testName = GetScreenshotFileName(test);
            var fileName = GetFileName(testName, SeleniumCoreSettings.ScreenshotLocation);
            var tsc = Browser as ITakesScreenshot;

            if (tsc != null)
            {
                try
                {
                    CreateDirectoryIfNotExists(fileName);
                    var ss = tsc.GetScreenshot();
                    ss.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                    _log.InfoFormat("Browser saved the Screenshot: {0}", fileName);
                }
                catch (Exception ex)
                {
                    _log.ErrorFormat(string.Format("Failed to save screenshot: {0}", ex));
                }
            }
            else
            {
                _log.Error("Browser (as ITakesScreenshot) could not take the Screenshot ()");
            }

            return fileName;
        }

        private string GetScreenshotFileName(TestContext.TestAdapter test)
        {
            var filename = SeleniumCoreSettings.ScreenShotNameFormat;

            filename = filename.Replace("{driverName}", Browser.GetType().Name);
            filename = filename.Replace("{fullDriverName}", Browser.GetType().FullName);
            filename = filename.Replace("{testName}", test.Name);
            // I don't currently have a way to get the full test name without the driver name (NUnit includes it by default)
            filename = filename.Replace("{fullTestNameWithDriver}", test.FullName);
            filename = filename.Replace("{className}", this.GetType().Name);
            filename = filename.Replace("{fullClassName}", this.GetType().FullName);
            filename = filename.Replace("{sessionId}", GetSessionId());
            filename = filename.Replace("{browserName}", BrowserName);

            filename = string.Concat(filename, "_", DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture), ".png");

            return filename;
        }

        #endregion Print Screen

        #endregion
    }
}

