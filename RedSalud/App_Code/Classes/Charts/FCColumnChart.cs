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
    public class FCColumnChart : FusionChart
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public FCColumnChart()
        {
            this.TheChart = null;
            this._xmlFormat = ChartType.TypeOfChart.BARCHART2AXIS;
            this._xmlSchemaFile = "~/FusionCharts/Schema/MultiSeriesColumn2DSchema.xsd";
        }

        protected override void PrepareConfigurationDataForChart()
        {
            // Setting filename for exporting
            ExportFileName = "ExampleRadar";
        }

        /// <summary>
        /// This builds the chart data from the db
        /// </summary>
        /// <param name="dts"></param>
        protected override void BuildChartData(DataSet dts)
        {
            string xVariableId = this.TheChart.TheChartData.XVariableId;
            string yVariableId = this.TheChart.TheChartData.YVariableId;
            
            // Suppose first we dont use weights
            this.TheChart.TheChartData.UsedWeights = false;

            List<string> categories = BuildCategories(dts);
            BuildDataSeries(categories, dts);
        }

        private void BuildDataSeries(List<string> categories, DataSet dts)
        {
            int idForDataSets = 1;

            if (TheChart.TheChartData.SetOfData.Count == 1)
            {
                BuildDataSeriesOneSerie(dts);
                return;
            }

            foreach (DataSeries serie in TheChart.TheChartData.SetOfData)
            {
                DataRow datasetRow = dts.Tables["dataset"].NewRow();
                datasetRow["dataset_Id"] = idForDataSets;
                datasetRow["chart_Id"] = 1;

                try
                {
                    datasetRow["seriesName"] = ChartUtilities.SafeStringForXml(serie.SeriesTitle);
                }
                catch (Exception q)
                {
                    log.Error("Series error ", q);
                    continue;
                }

                // Adds data series
                dts.Tables["dataset"].Rows.Add(datasetRow);

                // Gets data 
                List<DataPoint> values = serie.TheDataPoints;
                int catIndex = 0;

                foreach (DataPoint aPoint in values)
                {
                    TwoDDataPoint a2dPoint  = (TwoDDataPoint)aPoint;

                    DataRow setRow = dts.Tables["set"].NewRow();
                    setRow["dataset_Id"] = idForDataSets;
                    setRow["value"] = a2dPoint.YDataPoint;
                    setRow["toolText"] = ChartUtilities.SafeStringForXml(a2dPoint.XDataPointString) + ": " + a2dPoint.YDataPoint.ToString() +
                        "{br}Series: " + ChartUtilities.SafeStringForXml(serie.SeriesTitle);

                    dts.Tables["set"].Rows.Add(setRow);

                    catIndex++;
                }

                idForDataSets++;
            }
        }

        private List<string> BuildCategories(DataSet dts)
        {
            List<string> categories = new List<string>();
            if (TheChart.TheChartData.SetOfData.Count > 1)
            {
                categories = BuildCategoriesOneSerie();
            }
            else
            {
                return categories;
            }

            // TODO: for multiple series
            DataRow categoriesRow = dts.Tables["categories"].NewRow();

            categoriesRow["chart_id"] = 1;
            categoriesRow["categories_id"] = 1;

            dts.Tables["categories"].Rows.Add(categoriesRow);

            foreach (string categoryName in categories)
            {
                DataRow categoryRow = dts.Tables["category"].NewRow();

                categoryRow["categories_id"] = 1;
                categoryRow["label"] = ChartUtilities.SafeStringForXml(categoryName);

                dts.Tables["category"].Rows.Add(categoryRow);
            }

            return categories;
        }

        private List<string> BuildCategoriesOneSerie()
        {
            List<string> categoriesList = new List<string>();
            foreach(DataPoint aPoint in TheChart.TheChartData.SetOfData[0].TheDataPoints) {
                TwoDDataPoint a2dPoint = (TwoDDataPoint)aPoint;
                if (!string.IsNullOrEmpty(a2dPoint.XDataPointString))
                    categoriesList.Add(a2dPoint.XDataPointString);
            }

            return categoriesList;
        }

        private void BuildDataSeriesOneSerie(DataSet dts)
        {
            // Gets data 
            List<DataPoint> values = TheChart.TheChartData.SetOfData[0].TheDataPoints;

            foreach (DataPoint aPoint in values)
            {
                TwoDDataPoint a2dPoint = (TwoDDataPoint)aPoint;

                DataRow setRow = dts.Tables["set"].NewRow();
                setRow["value"] = a2dPoint.YDataPoint;
                setRow["label"] = a2dPoint.XDataPointString;
                setRow["chart_id"] = 1;

                dts.Tables["set"].Rows.Add(setRow);
            }
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