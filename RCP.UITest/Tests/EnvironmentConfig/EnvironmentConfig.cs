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
    public class EnvironmentConfig : TestBase
    {
        #region Constructors
        public EnvironmentConfig(string browserName) : base(browserName) { }

        // Remote Selenium Grid Test
        public EnvironmentConfig(string browserName, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Description("This method should be run on a new environment to create all the necessary users that are needed for all of the tests. A couple of manual " +
            "steps need to be taken before this method is run. 1. There must be 2 institutions, 1 named 'Premier' and the other named 'RCP'. 'Premier' is for CBD users," +
            " 'RCP' is for PER users. 2. For PER users, you must manually create a PER program titled TA_Program1 (or whatever the ProgramCode is called inside the" +
            " UserUtils class). You should do this on the Lifetime Support site, and finalize the program. 3. For Diploma users, you must manually create a PER program " +
            "titled TA_DiplomaProgram1 (or whatever the ProgramCode is called inside the UserUtils class). You should do this on the Lifetime Support site, and " +
            "finalize the program. Once 1 through 3 is complete, then run the below method. After the below method is complete and the users are created, you will have " +
            "to first manually login and accept the User License Agreement. " +
            "IMPORTANT: Only run the below method once per new environment. After it is run, comment the below test attribute so it is not run again")]
        [Test]
        public void CreateAllStaticUsers()
        {

            //// CBD
            //UserUtils.CreateAndRegisterUser(UserUtils.Learner1Login, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //UserUtils.CreateAndRegisterUser(UserUtils.LearnerCH1Login, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //UserUtils.CreateAndRegisterUser(UserUtils.LearnerFF1Login, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //UserUtils.CreateAndRegisterUser(UserUtils.LearnerIE1Login, UserUtils.Application.CBD, UserUtils.UserRole.LR);
            //UserUtils.CreateAndRegisterUser(UserUtils.Observer1Login, UserUtils.Application.CBD, UserUtils.UserRole.OB);
            //UserUtils.CreateAndRegisterUser(UserUtils.Observer2Login, UserUtils.Application.CBD, UserUtils.UserRole.OB);
            //UserUtils.CreateAndRegisterUser(UserUtils.ProgAdmin1Login, UserUtils.Application.CBD, UserUtils.UserRole.PA);
            //UserUtils.CreateAndRegisterUser(UserUtils.ProgDean1Login, UserUtils.Application.CBD, UserUtils.UserRole.PGD);
            //UserUtils.CreateAndRegisterUser(UserUtils.ProgDirector1Login, UserUtils.Application.CBD, UserUtils.UserRole.PD);
            //UserUtils.CreateAndRegisterUser(UserUtils.CC1Login, UserUtils.Application.CBD, UserUtils.UserRole.CC);

            //// PER
            //UserUtils.CreateAndRegisterUser(UserUtils.Assessor1PERLogin, UserUtils.Application.PER, UserUtils.UserRole.ASRPER);
            //UserUtils.CreateAndRegisterUser(UserUtils.Assessor2PERLogin, UserUtils.Application.PER, UserUtils.UserRole.ASRPER);
            //UserUtils.CreateAndRegisterUser(UserUtils.Assessor3PERLogin, UserUtils.Application.PER, UserUtils.UserRole.ASRPER);
            //UserUtils.CreateAndRegisterUser(UserUtils.Referee1PERLogin, UserUtils.Application.PER, UserUtils.UserRole.REF);
            //UserUtils.CreateAndRegisterUser(UserUtils.Referee2PERLogin, UserUtils.Application.PER, UserUtils.UserRole.REF);

            //// Diploma
            //UserUtils.CreateAndRegisterUser(UserUtils.ClinicalSupervisor1DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.CSDiploma);
            //UserUtils.CreateAndRegisterUser(UserUtils.DiplDirector1DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.DDDiploma);
            //UserUtils.CreateAndRegisterUser(UserUtils.FacultyOfMed1DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.FOMDiploma);
            //UserUtils.CreateAndRegisterUser(UserUtils.Assessor1DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma);
            //UserUtils.CreateAndRegisterUser(UserUtils.Assessor2DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma);
            //UserUtils.CreateAndRegisterUser(UserUtils.Assessor3DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.ASRDiploma);
            //UserUtils.CreateAndRegisterUser(UserUtils.Trainee1DiplomaLogin, UserUtils.Application.Diploma, UserUtils.UserRole.TraineeDiploma);

            //// Mainport
            //UserUtils.CreateAndRegisterUser(UserUtils.MainportUser1Login, UserUtils.Application.Mainport, UserUtils.UserRole.MP);
        }

        #endregion Tests
    }
}

