using OpenQA.Selenium;
using System;
using System.Globalization;

namespace RCP.AppFramework
{
    /// <summary>
    /// Elements on the observer role page
    /// </summary>
    public class DiplomaClinicalSupervisorPageBys
    {
        // Buttons
        public readonly By MarkSelectedMilestonesAsAchievedBtn = By.XPath("//span[text()='Mark Selected Milestone as Achieved']/ancestor::button/following-sibling::a");
        public readonly By MarkSelMilestonesAchFormSubmitBtn = By.XPath("//span[text()='Submit']");

        // Charts

        // Check boxes
        public readonly By UnderReviewTblBodyRowChk = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::tr[@class='item-row']/td/input"); // If one row exists in this table, then this will be the checkbox inside that row

        

        // Date control

        // Frames



        // Labels

        // Links


        // Radio Buttons


        // Select Elements


        // Scripts

        // Tables   
        public readonly By UnderReviewTbl = By.XPath("//table[@class='table table-rc table-striped grid']");
        public readonly By UnderReviewTblBodyRow = By.XPath("//table[@class='table table-rc table-striped grid']/descendant::tr[@class='item-row']"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load


        // Tabs
        public readonly By UnderReviewTab = By.XPath("//span[text()='Under Review']");
        public readonly By MilestonesByTraineeTab = By.XPath("//span[text()='Milestones by Trainee']");

        // Text boxes









    }
}