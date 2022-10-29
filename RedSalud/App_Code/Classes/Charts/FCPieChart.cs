using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using log4net;
using Artexacta.App.Chart;
using Artexacta.App.Utilities.Chart;

namespace Artexacta.App.Chart
{

    /// <summary>
    /// Summary description for FCPieChart
    /// </summary>
    [Serializable]
    public class FCPieChart : FusionChart
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public FCPieChart()
        {
            this.TheChart = null;
            this._xmlFormat = ChartType.TypeOfChart.PIECHART;
            this._xmlSchemaFile = "~/FusionCharts/Schema/pie2D.xsd";
        }

        protected override void PrepareConfigurationDataForChart()
        {
            // Setting filename for exporting
            ExportFileName = "PieChart";
        }

        /// <summary>
        /// This builds the chart data from the db
        /// </summary>
        /// <param name="dts"></param>
        protected override void BuildChartData(DataSet dts)
        {
            string xVariableId = this.TheChart.TheChartData.XVariableId;
            string yVariableId = this.TheChart.TheChartData.YVariableId;

            BuildDataSeries(dts);
        }

        private void BuildDataSeries(DataSet dts)
        {
            //string language = Artexacta.IMP.Utilities.LanguageUtilities.GetLanguageFromContext();
            //DataVariablesBLL variablesBll = new DataVariablesBLL();

            //// Building XML
            //if (setOfData == null || setOfData.TheDataPoints.Count <= 0)
            //{
            //    log.Info("There are no points in this serie ... leaving");
            //    return;
            //}

            //// Obtains the listitems of the X variable
            //ListDataVariable xVariable =
            //    (ListDataVariable)variablesBll.GetDataVariableById(false, Chart.ChartData.XVariableId, language);

            //decimal totalForPie = 0;
            //int indexDataPoint = 0;

            //foreach (IMPDataPoint point in setOfData.TheDataPoints)
            //{
            //    if (point == null || !(point is IMP2DDataPoint))
            //    {
            //        log.Warn("Point must not be null and should be IMP2DDataPoint, fetching next point.");
            //        continue;
            //    }
            //    IMP2DDataPoint point2d = (IMP2DDataPoint)point;
            //    totalForPie += point2d.YDataPoint;
            //}

            //foreach (ListDataItem li in xVariable.ListItems)
            //{
            //    IMPDataPoint point = null;
            //    if (indexDataPoint < setOfData.TheDataPoints.Count)
            //    {
            //        point = setOfData.TheDataPoints[indexDataPoint];
            //        if (point == null || !(point is IMP2DDataPoint))
            //        {
            //            log.Warn("Point must not be null and should be IMP2DDataPoint, fetching next point.");
            //            continue;
            //        }
            //    }
            //    IMP2DDataPoint point2d = (IMP2DDataPoint)point;

            //    if (point2d == null || point2d.XDataPoint > li.Value)
            //    {
            //        log.Info("Empty value for X = " + (point2d == null ? "NULL" : point2d.XDataPoint.ToString()));
            //        continue;
            //    }

            //    DataRow setRow = dts.Tables["set"].NewRow();
            //    setRow["chart_Id"] = 1;
            //    setRow["label"] = ChartUtilities.SafeStringForXml(li.Description);

            //    decimal percentage = 100 * (point2d.YDataPoint / totalForPie);

            //    // Do NOT show values when count is less than 4
            //    if (point2d.ObservationCount >= setOfData.DontShowValueIfCountLessThan)
            //    {
            //        setRow["value"] = point2d.YDataPoint;
            //        setRow["toolText"] = ChartUtilities.SafeStringForXml(xVariable.GetDescriptionForListItem(point2d.XDataPoint)) +
            //                "{br}" + Resources.Chart.ValueLabel + ": " + ChartUtilities.FormatNumber(point2d.YDataPoint) + " (" + ChartUtilities.FormatNumber(percentage) + "%)" + 
            //                "{br}" + Resources.Chart.CountLabel + ": " + point2d.ObservationCount;
            //    }

            //    dts.Tables["set"].Rows.Add(setRow);

            //    indexDataPoint++;
            //}

            //// Setting the total as a subcaption in the chart
            //DataRow dtrChart = dts.Tables["chart"].Rows[0];
            //dtrChart["subCaption"] = Resources.Chart.TotalLabel + ": " + ChartUtilities.FormatNumber(totalForPie);
        }

        protected override void BuildExtraSettings(System.Data.DataSet dts)
        {
            return;
        }

        protected override string BuildCsvHeaderInformation()
        {
            throw new NotImplementedException();
        }

        protected override string BuildCsvChartData()
        {
            throw new NotImplementedException();
        }
    }
}