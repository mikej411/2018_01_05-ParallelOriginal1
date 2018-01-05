using Browser.Core.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RCP.AppFramework
{
    public static class UserUtils
    {
        #region properties

        #region static users


        // CBD Users
        public static string Learner1Login = "_TA_AStaticUser_LR_001";
        public static string Learner1FullName = "_TA_AStatic User_LR_001";

        public static string LearnerIE1Login = "_TA_AStaticUser_LR_IE_001";
        public static string LearnerIE1FullName = "_TA_AStatic User_LR_IE_001";

        public static string LearnerFF1Login = "_TA_AStaticUser_LR_FF_001";
        public static string LearnerFF1FullName = "_TA_AStatic User_LR_FF_001";

        public static string LearnerCH1Login = "_TA_AStaticUser_LR_CH_001";
        public static string LearnerCH1FullName = "_TA_AStatic User_LR_CH_001";

        public static string Observer1Login = "_TA_AStaticUser_OB_001";
        public static string Observer1FullName = "_TA_AStatic User_OB_001";

        public static string Observer2Login = "_TA_AStaticUser_OB_002";
        public static string Observer2FullName = "_TA_AStatic User_OB_002";

        public static string ProgAdmin1Login = "_TA_AStaticUser_PA_001";
        public static string ProgAdmin1FullName = "_TA_AStatic User_PA_001";

        public static string ProgDirector1Login = "_TA_AStaticUser_PD_001";

        public static string ProgDean1Login = "_TA_AStaticUser_PGD_001";

        public static string CC1Login = "_TA_AStaticUser_CC_001";

        // PER Users
        public static string Trainee1PERLogin = "_TA_AStaticUser_TraineePER_001";

        public static string Assessor1PERLogin = "_TA_AStaticUser_AssessorPER_001";
        public static string Assessor1PERFullName = "_TA_AStatic User_AssessorPER_001";

        public static string Assessor2PERLogin = "_TA_AStaticUser_AssessorPER_002";
        public static string Assessor2PERFullName = "_TA_AStatic User_AssessorPER_002";

        public static string Assessor3PERLogin = "_TA_AStaticUser_AssessorPER_003";
        public static string Assessor3PERFullName = "_TA_AStatic User_AssessorPER_003";

        public static string Referee1PERLogin = "_TA_AStaticUser_Referee_001";
        public static string Referee1PERFullName = "_TA_AStatic User_Referee_001";

        public static string Referee2PERLogin = "_TA_AStaticUser_Referee_002";
        public static string Referee2PERFullName = "_TA_AStatic User_Referee_002";

        public static string CredentialStaffPERLogin = System.Configuration.ConfigurationManager.AppSettings["CredentialStaffUserNamePER"].ToString();

        // Diploma Users
        public static string Trainee1DiplomaLogin = "_TA_AStaticUser_TraineeDiploma_001";
        public static string Trainee1DiplomaFullName = "_TA_AStatic User_TraineeDiploma_001";

        public static string ClinicalSupervisor1DiplomaLogin = "_TA_AStaticUser_ClinicalSupervisorDiploma_001";
        public static string ClinicalSupervisor1DiplomaFullName = "_TA_AStatic User_ClinicalSupervisorDiploma_001";

        public static string FacultyOfMed1DiplomaLogin = "_TA_AStaticUser_FacultyOfMedDiploma_001";
        public static string FacultyOfMed1DiplomaFullName = "_TA_AStatic User_FacultyOfMedDiploma_001";

        public static string DiplDirector1DiplomaLogin = "_TA_AStaticUser_DiplDirectorDiploma_001";
        public static string DiplDirector1DiplomaFullName = "_TA_AStatic User_DiplDirectorDiploma_001";

        public static string Assessor1DiplomaLogin = "_TA_AStaticUser_AssessorDiploma_001";
        public static string Assessor1DiplomaFullName = "_TA_AStatic User_AssessorDiploma_001";

        public static string Assessor2DiplomaLogin = "_TA_AStaticUser_AssessorDiploma_002";
        public static string Assessor2DiplomaFullName = "_TA_AStatic User_AssessorDiploma_002";

        public static string Assessor3DiplomaLogin = "_TA_AStaticUser_AssessorDiploma_003";
        public static string Assessor3DiplomaFullName = "_TA_AStatic User_AssessorDiploma_003";

        public static string CredentialStaffDiplomaLogin = System.Configuration.ConfigurationManager.AppSettings["CredentialStaffUserNameDiploma"].ToString();

        // Mainport Users
        public static string MainportUser1Login = "_TA_AStaticUser_Mainport_001";
        public static string MainportUser1FullName = "_TA_AStatic User_Mainport_001";

        #endregion static users


        static string baseAPIUrl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"].ToString();

        // I changed this to be a static token, less room for test failure if we dont have to call generatetoken every single time
        // static string token = GetToken("RCPSC");
        // If the static token doesnt work, get a new static token from DEV or do it manually
        static string token = System.Configuration.ConfigurationManager.AppSettings["APIToken"].ToString();

        /// <summary>
        /// Specifies which application the user will be registered to/>
        /// </summary>
        /// <see cref="UserUtils.CreateUser(null, string, string, string)"/>
        public enum Application
        {
            Mainport,
            CBD,
            PER,
            Diploma
        }

        /// <summary>
        /// Specifies type of role to assign to the user"/>
        /// </summary>
        /// <see cref="UserUtils.CreateUser(null, string, string, string)"/>
        public enum UserRole
        {
            LR, // Learner
            OB, // Observer
            PA, // Program Admin
            PD, // Program Director
            PGD, // Program Deam
            CC, // CC member
            MP, // Mainport
            TraineePER,
            TraineeDiploma,
            ASRPER, // Assessor
            ASRDiploma,
            REF, // Referee
            CUPER, // Credential Staff Unit for PER application
            CUDiploma, // Credential Staff Unit for Diploma application
            CSDiploma, // Clinical Supervisor for Diploma
            FOMDiploma, // Faculty of medicine
            DDDiploma //Diploma Director


        }

        #endregion properties

        #region methods

        /// <summary>
        /// Creates a new user and assigns a role to that user, depending on what user and role the tester chooses
        /// </summary>
        /// <param name="application"><see cref="UserUtils.Application"/></param>
        /// <param name="role"><see cref="UserRole.UserRole"/></param>
        /// <param name="customUserName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customEmailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customMainportCycleStartDate">(Optional) If you want a custom start date for your cycle. The date should be in the format of "yyyy-MM-dd"</param>
        /// <returns>An object that contains all the user's information, such as username, full name (first and last name), email, etc.</returns>
        public static UserInfo CreateAndRegisterUser(Application application, UserRole role, string customUserName = "", string customEmailAddress = "", string customMainpotCycleStartDate = "")
        {
            UserInfo newUserModel = CreateUser(role, customUserName, customEmailAddress);

            RegisterUser(newUserModel.Guid, newUserModel.Username, application, role, customMainpotCycleStartDate);

            return newUserModel;
        }

        /// <summary>
        /// Creates a new user and assigns a role to that user, depending on what user and role the tester chooses
        /// </summary>
        /// <param name="role"><see cref="UserRole.UserRole"/></param>
        /// <param name="newUserModel">Set this to null if you are calling this method from one of the test classes. Otherwise, this parameter is populated from <see cref="CreateAndRegisterUser( Application, UserRole,string)"/></param>
        /// <param name="customUserName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customEmailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <returns>An object that contains all the user's information, such as username, full name (first and last name), email, etc.</returns>
        public static UserInfo CreateUser(UserRole role, string customUserName = "", string customEmailAddress = "")
        {
            string resp = string.Empty;

            UserInfo newUserModel = BuildUserModel(role, customUserName, customEmailAddress);

            using (var wc = new WebClient())
            {

                String fullAPIUrl = String.Format("{0}api/user", baseAPIUrl);
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Token", token);
                string body = JsonConvert.SerializeObject(newUserModel);
                resp = wc.UploadString(fullAPIUrl, body);
            }

            // Add the guid to the userinfo object
            dynamic data = JObject.Parse(resp);
            newUserModel.Guid = data.Id.ToString();

            return newUserModel;
        }

        /// <summary>
        /// Builds the user information that then gets plugged into the API request's body
        /// </summary>
        /// <param name="role"><see cref="UserRole.UserRole"/></param>
        /// <param name="customUserName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customEmailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <returns></returns>
        private static UserInfo BuildUserModel(UserRole role, string customUserName = "", string customEmailAddress = "")
        {
            string userName = "";
            string emailAddress = "";
            string currentDate = string.Format("{0}_", DateTime.Now.ToString("MdyyyyHHmmss", CultureInfo.InvariantCulture));

            // Set the email address and user name
            if (string.IsNullOrEmpty(customEmailAddress))
            {
                emailAddress = "";
            }
            else
            {
                emailAddress = customEmailAddress;
            }

            if (string.IsNullOrEmpty(customUserName))
            {
                switch (role)
                {
                    case UserRole.LR:
                        userName = "TA_LR_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.OB:
                        userName = "TA_OB_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.PA:
                        userName = "TA_PA_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.PD:
                        userName = "TA_PD_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.PGD:
                        userName = "TA_PGD_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.CC:
                        userName = "TA_CC_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.TraineePER:
                        userName = "TA_TraineePER_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.ASRPER:
                        userName = "TA_AssessorPER_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.REF:
                        userName = "TA_Referee_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.TraineeDiploma:
                        userName = "TA_TraineeDiploma_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.ASRDiploma:
                        userName = "TA_AssessorDiploma_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.CSDiploma:
                        userName = "TA_ClinicalSuperDiploma_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.DDDiploma:
                        userName = "TA_DiplDirectorDiploma_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.FOMDiploma:
                        userName = "TA_FacultyOfMedDiploma_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                    case UserRole.MP:
                        userName = "TA_MainportUser_" + currentDate + DataUtils.GetRandomString(3);
                        break;
                }
            }
            else
            {
                userName = customUserName;
            }


            UserInfo newUserModel = new UserInfo();
            newUserModel.Username = userName;
            newUserModel.Password = "test";
            newUserModel.FirstName = userName.Substring(0, 11);
            newUserModel.LastName = userName.Substring(11);
            newUserModel.FullName = string.Format("{0} {1}", newUserModel.FirstName, newUserModel.LastName);
            newUserModel.Degree = "test";
            newUserModel.EmailAddress = emailAddress;
            newUserModel.Address = "Address01";
            newUserModel.Address2 = "Address02";
            newUserModel.City = "CityInfo";
            newUserModel.State = "TEST STATE";
            newUserModel.PostalCode = "TEST POSTALCODE";
            newUserModel.CountryCode = "TEST COUNTRY INFO";
            newUserModel.OccupationInfo = null;
            newUserModel.Fields = new Field[] { new Field() { Name = "profession", Value = "PHY" },
                new Field() { Name = "MP_Reg_Title", Value = "Dr" },
                new Field() { Name = "MP_Professional_Designation", Value = "AES" },
                new Field() { Name = "GENDER", Value = "male" },
                new Field() { Name = "language", Value = "en-US" },
                new Field() { Name = "PARTICIPANT_ID", Value = userName }};

            return newUserModel;
        }

        /// <summary>
        /// Assigns the tester-specfified role to a user
        /// </summary>
        /// <param name="userGUID">The GUID of the user you want to assign the role to</param>
        /// <param name="userName"></param>
        /// <param name="application"><see cref="Application"/></param>
        /// <param name="user"><see cref="User.User"/></param>
        /// <param name="role"><see cref="UserRole.UserRole"/></param>
        /// <param name="customMainportCycleStartDate">(Optional) If you want a custom start date for your cycle. The date should be in the format of "yyyy-MM-dd"</param>
        public static void RegisterUser(string userGUID, string userName, Application application, UserRole role, string customMainportCycleStartDate = "")
        {
            string resp = string.Empty;
            string roleName = string.Empty;

            switch (role)
            {
                case UserRole.LR:
                    roleName = "lr";
                    break;
                case UserRole.OB:
                    roleName = "ob";
                    break;
                case UserRole.PA:
                    roleName = "pa";
                    break;
                case UserRole.PD:
                    roleName = "pd";
                    break;
                case UserRole.PGD:
                    roleName = "pgd";
                    break;
                case UserRole.CC:
                    roleName = "cc";
                    break;
                case UserRole.TraineePER:
                case UserRole.TraineeDiploma:
                    roleName = "trainee";
                    break;
                case UserRole.ASRPER:
                case UserRole.ASRDiploma:
                    roleName = "asr";
                    break;
                case UserRole.REF:
                    roleName = "ref";
                    break;
                case UserRole.CSDiploma:
                    roleName = "cs";
                    break;
                case UserRole.DDDiploma:
                    roleName = "dd";
                    break;
                case UserRole.FOMDiploma:
                    roleName = "fom";
                    break;
                case UserRole.MP:
                    roleName = "User";
                    break;
            }

            // Use a different role and different serialize objects depending on the type of user the tester passes
            switch (application)
            {
                case Application.CBD:
                    using (var wc = new WebClient())
                    {

                        String fullAPIUrl = String.Format("{0}/api/apps/cbd/portfolio/{1}/register", baseAPIUrl, userName);

                        wc.Headers.Add("Content-Type", "application/json");
                        wc.Headers.Add("Token", token);

                        resp = wc.UploadString(fullAPIUrl, JsonConvert.SerializeObject(new
                        {
                            Role = roleName,

                            //InstitutionName = "RCP",
                            // Premier institution is one that Arbab created so Prod Working doesnt get flooded with automation users for
                            // any of their institutions
                            InstitutionName = "Premier",

                            // anestheloiology program
                            ProgramCode = "64BE12F9-608B-4C9F-A882-BA494AC43456"
                            // Octonoloriology program
                            //ProgramCode = "513B0F18-7241-409D-86E2-37C0893EE354",


                        }));
                    }
                    break;

                case Application.Mainport:
                    using (var wc = new WebClient())
                    {
                        // Set the mainport cycle date
                        string mainportCycleStartDate = "";
                        if (string.IsNullOrEmpty(customMainportCycleStartDate))
                        {
                            mainportCycleStartDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            mainportCycleStartDate = customMainportCycleStartDate;
                        }

                        String fullAPIUrl = String.Format("{0}/api/Activity/AF78C6CF-8EA7-4B12-B399-B91D99649F5F/user", baseAPIUrl);

                        wc.Headers.Add("Content-Type", "application/json");
                        wc.Headers.Add("Token", token);

                        resp = wc.UploadString(fullAPIUrl, JsonConvert.SerializeObject(new
                        {
                            UserIdentifier = userGUID,
                            MembershipType = "Individual",
                            MembershipRole = roleName,
                            StartDate = mainportCycleStartDate
                        }));
                    }
                    break;

                case Application.PER:
                    string perProgramCode = System.Configuration.ConfigurationManager.AppSettings["PERProgramCode"].ToString();
                    using (var wc = new WebClient())
                    {

                        String fullAPIUrl = String.Format("{0}/api/apps/per/portfolio/{1}/register", baseAPIUrl, userGUID);

                        wc.Headers.Add("Content-Type", "application/json");
                        wc.Headers.Add("Token", token);

                        resp = wc.UploadString(fullAPIUrl, JsonConvert.SerializeObject(new
                        {
                            Role = roleName,

                            InstitutionCode = "RCP",

                            // This is a custom program I created that has 2 required milestones and 2 optional milestones. If you ever need
                            // to create a new one,
                            // you go to lifetime support, then Diploma Management Tools, then make sure you select PER as the program type.
                            // Once created, you have to click the Actions link then click Publish to finalize the program. The ProgramCode
                            // is usually the Short label when you click Actions->Edit. Note that you should not create the same program
                            // name across RC Final and Working for PER. This is because whenever you finalize a program, an activity gets
                            // created, and then you cannot use this API because it will throw and error saying...
                            // "There was not a matching activity record for the provided ActivityID (Value: 00000000-0000-0000-0000-000000000000)."

                            ProgramCode = perProgramCode,
                        }));
                    }
                    break;

                case Application.Diploma:
                    string diplomaProgramCode = System.Configuration.ConfigurationManager.AppSettings["DiplomaProgramCode"].ToString();
                    using (var wc = new WebClient())
                    {

                        String fullAPIUrl = String.Format("{0}/api/apps/rcp/portfolio/{1}/register", baseAPIUrl, userGUID);

                        wc.Headers.Add("Content-Type", "application/json");
                        wc.Headers.Add("Token", token);

                        resp = wc.UploadString(fullAPIUrl, JsonConvert.SerializeObject(new
                        {
                            Role = roleName,

                            InstitutionCode = "RCP",

                            // This is a custom program I created that has 2 required milestones and 2 optional milestones. If you ever need
                            // to create a new one,
                            // you go to lifetime support, then Diploma Management Tools, then make sure you select PER as the program type.
                            // Once created, you have to click the Actions link then click Publish to finalize the program. The ProgramCode
                            // is usually the Short label when you click Actions->Edit. Note that you should not create the same program
                            // name across RC Final and Working for PER. This is because whenever you finalize a program, an activity gets
                            // created, and then you cannot use this API because it will throw and error saying...
                            // "There was not a matching activity record for the provided ActivityID (Value: 00000000-0000-0000-0000-000000000000)."

                            ProgramCode = diplomaProgramCode,
                        }));
                    }
                    break;
            }
        }

        public static void DeleteUser(string userName)
        {
            string resp = string.Empty;
            DeleteUser user = new DeleteUser();
            user.UserName = userName;
            user.ActivityGUID = "AF78C6CF-8EA7-4B12-B399-B91D99649F5F";

            using (var wc = new WebClient())
            {
                String url =
                    String.Format("{0}/api/apps/cbd/integration/delete", baseAPIUrl, userName);
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Token", token);
                resp = wc.UploadString(url, JsonConvert.SerializeObject(user));
            }
        }


        private static String GetToken(String siteCode = null, String accountKey = null, String password = null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["APIUrl"] == null) throw new Exception("Appsetting 'APIUrl' is missing.");
            accountKey = accountKey ?? ConfigurationManager.AppSettings["APIAccountKey"];
            password = password ?? ConfigurationManager.AppSettings["APIAccountPassword"];
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"].ToString();
            var postData = new { AccountKey = accountKey, Password = password, SiteCode = siteCode };

            var postString = JsonConvert.SerializeObject(postData);
            var tokenResponse = new { Token = "", Expiration = "" };
            using (var wc = new WebClient())
            {
                String url =
                    String.Format("{0}/api/AccessToken", baseUrl);
                wc.Headers.Add("content-type", "application/json");
                wc.Headers.Add("accept", "application/json");
                var tokenModel = wc.UploadString(url, postString);
                var tokenAnon = JsonConvert.DeserializeAnonymousType(tokenModel, tokenResponse);
                return tokenAnon.Token;
            }
        }

        #endregion methods
    }
}
