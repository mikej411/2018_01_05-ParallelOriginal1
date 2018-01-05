using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class DiplomaFacOfMedicinePageBys
    {
        // Buttons
        public readonly By MarkSelectedPortfoliosAsAchievedBtn = By.XPath("//span[text()='Mark Selected Portfolios as Achieved']/ancestor::button/following-sibling::a");
        public readonly By MarkSelPortAchFormSubmitBtn = By.XPath("//div[@id='mdlMarkSelectedPortfoliosAsAchieved']/descendant::span[text()='Submit']");

        // Charts

        // Check boxes
        public readonly By MarkSelPortAchFormIAttestChk = By.XPath("//input[@id='chkAttestation']");
        public readonly By PortfoliosUnderReviewTblBodyRowChk = By.XPath("//div[@id='portfolios']/descendant::table[@class='table table-rc table-striped grid']/descendant::tr[@class='item-row']/td/input"); // If one row exists in this table, then this will be the checkbox inside that row

        // Date control

        // Frames



        // Labels

        // Links


        // Radio Buttons


        // Select Elements


        // Scripts

        // Tables   
        public readonly By PortfoliosUnderReviewTbl = By.XPath("//div[@id='portfolios']/descendant::table[@class='table table-rc table-striped grid']");
        public readonly By PortfoliosUnderReviewTblBodyRow = By.XPath("//div[@id='portfolios']/descendant::table[@class='table table-rc table-striped grid']/descendant::tr[@class='item-row']"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load
        public readonly By MyProgramSnapshotTblFirstRowPrgLnk = By.XPath("(//table[@class='table table-rc table-striped grid'])[1]/tbody[2]/tr/td/a"); // If a row exists in the My Program Snapshot table, this will be the program name link in that row


        // Tabs
        public readonly By PortfoliosUnderReviewTab = By.XPath("//span[text()='Portfolios Under Review']");
        public readonly By ResubmittedMilestonesTab = By.XPath("//span[text()='Resubmitted Milestones']");

        // Text boxes









    }
}