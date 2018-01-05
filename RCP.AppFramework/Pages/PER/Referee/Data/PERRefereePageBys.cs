using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class PERRefereePageBys
    {
        // Buttons
        public readonly By TraineeSurveyFormSaveAndFinBtn = By.Id("ctl00_ContentPlaceHolder1_btnSaveAndFinish");
        public readonly By TraineeSurveyFormCloseBtn = By.Id("ctl00_ContentPlaceHolder1_btnSaveAndFinish");
        public readonly By TraineeSurveyFormSaveAndFinLatBtn = By.Id("ctl00_ContentPlaceHolder1_btnSaveAndFinishLater");



        // Charts

        // Check boxes
        public readonly By TraineeSurveyFormIAttestChk = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl14_3225195|1");

        // Date control

        // Frames
        
        public readonly By TraineeSurveyFormFrame = By.XPath("//div[@id='mdlViewSurvey']/descendant::iframe[@id='surveyForm']"); // The frame that PER is in

        
        // Labels

        // Links

        // Radio Buttons
        public readonly By TraineeSurveyFormAreYouFamYesRdo = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl08_3160940|1");
        public readonly By TraineeSurveyFormAreYouFamNoRdo = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl08_3160940|2");
        public readonly By TraineeSurveyFormTheApplNamedYesRdo = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl12_3225185|1");
        public readonly By TraineeSurveyFormTheApplNamedNoRdo = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl12_3225185|2");

        // Random
        public readonly By TraineeSurveyFormLoadingIcon = By.XPath("//img[@id='ctl00_ContentPlaceHolder1_fb1_progressFormImage']"); // This is the loading icon that appears after we click on the TraineeSurveyFormAreYouFamYesRdo radio button. We will use this to wait after we click it
       // public readonly By TraineeSurveyFormLoadingIcon = By.XPath("//img[@id='ctl00_ContentPlaceHolder1_fb1_progressFormImage']/ancestor::td[1]"); // This is the loading icon that appears after we click on the TraineeSurveyFormAreYouFamYesRdo radio button. We will use this to wait after we click it


        // Select Elements


        // Scripts

        // Tables   
        public readonly By PendingSurveysTbl = By.XPath("//table[@zebra-model='PagedPortfolios']");
        public readonly By PendingSurveysTblFirstRow = By.XPath("//table[@zebra-model='PagedPortfolios']/descendant::tr[@ng-repeat='portfolio in PagedPortfolios']");


        // Tabs

        // Text boxes
        public readonly By TraineeSurveyFormProfessTxt = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl03_t3225177");
        public readonly By TraineeSurveyFormPracticeRelTxt = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl04_t3225178");
        public readonly By TraineeSurveyFormSpecialCertTxt = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl05_t3225179");
        public readonly By TraineeSurveyFormYearTxt = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl06_t3160936");
        public readonly By TraineeSurveyFormPleaseAddTxt = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl13_t3225186");
        public readonly By TraineeSurveyFormHowLongTxt = By.Id("ctl00_ContentPlaceHolder1_fb1_ctl06_ctl07_t3160938");













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

    }
}