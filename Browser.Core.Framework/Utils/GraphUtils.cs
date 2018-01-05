using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.Core.Framework.Utils
{
    public static class GraphUtils
    {
        #region methods
        /// <summary>
        /// Gets a <see cref="DataTable"/> equivalent of the data behind the specified highchart
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="chartElem">The chart element</param>
        /// <returns></returns>
        public static DataTable GetHighChartDataTable(IWebDriver browser, IWebElement chartElem)
        {
            string json = GetHighChartJSON(browser, chartElem);

            // I dont believe I need to use this method anymore, as I found out a way to retreive the data
            // pre-organized in JSON format. See GetHighChartJSON method for additional clarification.
            // string fixedJson = TransformJSON(json, chartElem);

            DataTable dt = SerializationUtils.DeserializeToDataTable(json);
            return DataUtils.ConvertVariousTypeDataTableToStringDataTable(dt);
        }

        /// <summary>
        /// Gets the data of a specified chart in JSON format
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="chartElem">The chart element</param>
        /// <returns></returns>
        private static string GetHighChartJSON(IWebDriver browser, IWebElement chartElem)
        {
            string jsText = "";

            if (chartElem.GetAttribute("id") == "EPAChart" || chartElem.GetAttribute("id") == "EPAChartTitle")
            {
                jsText = "var epadatasource = []; for(var i=0;i<angular.element($('#EPAChart')).scope().$parent.vm['epaGraphData'].Categories.length;i++){epadatasource.push({category: angular.element($('#EPAChart')).scope().$parent.vm['epaGraphData'].Categories[i], data: angular.element($('#EPAChart')).scope().$parent.vm['epaGraphData'].Data[i]}); }; return JSON.stringify(epadatasource);";
                // I used the below function first, but it didnt return the JSON in an organized way. It returned all EPAs first, and then all EPA values second. 
                // This did not translate to a Datatable
                // jsText = string.Format("return JSON.stringify(angular.element($('#EPAChart')).scope().$parent.vm['epaGraphData']);");
                // jsText = string.Format("return JSON.stringify(angular.element($(arguments[0])).scope().$parent.vm['epaGraphData']);");
            }

            if (chartElem.GetAttribute("id") == "EPAAttainmentGraph")
            {
                jsText = "var epadatasource = []; for (var i = 0;i < angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Categories.length; i++){epadatasource.push({category: angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Categories[i], average: angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Average[i], range0: angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Range[i] == null ? null : angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Range[i][0], range1: angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Range[i] == null ? null : angular.element($('#EPAAttainmentGraph')).scope().$parent.$parent.$parent.vm['epaGraphData'].Range[i][1]}); }; return JSON.stringify(epadatasource);";
                //"[{/category/:/EPA 1.1(11)/,/average/:2.5454545454545454,/range0/:1,/range1/:5},{/category/:/EPA 1.2(3)/,/average/:2.3333333333333335,/range0/:1,/range1/:5},{/category/:/EPA 1.3(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 1.4(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.1(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.2(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.3(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.4(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.5(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.6(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.7(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.8(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.9(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.10(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.11(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.12(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.13(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.14(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.15(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.16(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.17(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.18(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.19(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.20(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.21(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.22(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.23(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.24(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.25(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.26(0)/,/average/:null,/range0/:null,/range1/:null},{/category/:/EPA 2.27(0)/,/average/:null,/range0/:null,/range1/:null}]"
            }

            if (chartElem.GetAttribute("id") == "EPAProgressionOverTimeGraph")
            {
                jsText = "var epadatasource = []; for (var i = 0; i < angular.element($('#EPAProgressionOverTimeGraph')).scope().$parent.$parent.$parent.vm['EPAProgressionOverTimeGraphConfig'].series.length; i++) { for (var j = 0; j < angular.element($('#EPAProgressionOverTimeGraph')).scope().$parent.$parent.$parent.vm['EPAProgressionOverTimeGraphConfig'].series[i].data.length; j++) { epadatasource.push({ name: angular.element($('#EPAProgressionOverTimeGraph')).scope().$parent.$parent.$parent.vm['EPAProgressionOverTimeGraphConfig'].series[i].name, date: new Date(angular.element($('#EPAProgressionOverTimeGraph')).scope().$parent.$parent.$parent.vm['EPAProgressionOverTimeGraphConfig'].series[i].data[j][0]), rating: angular.element($('#EPAProgressionOverTimeGraph')).scope().$parent.$parent.$parent.vm['EPAProgressionOverTimeGraphConfig'].series[i].data[j][1] }); } }; return JSON.stringify(epadatasource);";                
            }
            
            return browser.ExecuteScript(jsText, chartElem) as string;
        }

        /// <summary>
        /// I have to create a JQuery function to return me a pre-organized JSON equivalent of the EPA Observation Count chart. 
        /// Until I do, we get the JSON unorganized, and then organize it ourselves with this method
        /// </summary>
        /// <param name="json">The JSON representing one of the high charts</param>
        /// <param name="chartElem">The chart element</param>
        /// <returns></returns>
        public static string TransformJSON(string json, IWebElement chartElem)
        {
            string fixedJson = "";

            if (chartElem.GetAttribute("id") == "EPAChart")
            {
                dynamic obj = JsonConvert.DeserializeObject<EPAObservationCountOriginal>(json);
                List<EPAObservationCountFixed> fixedObject = new List<EPAObservationCountFixed>();
                for (int i = 0; i < obj.Categories.Length; i++)
                {
                    fixedObject.Add(new EPAObservationCountFixed() { category = obj.Categories[i], data = obj.Data[i] });
                }

                fixedJson = JsonConvert.SerializeObject(fixedObject);
            }

            return fixedJson;
        }

        #endregion methods

        #region Class objects representing graphs

        public class EPAObservationCountFixed
        {
            public string category { get; set; }
            public string data { get; set; }
        }

        public class EPAObservationCountOriginal
        {
            public string[] Categories { get; set; }
            public string[] Data { get; set; }
        }

        #endregion Class objects representing graphs
    }
}
