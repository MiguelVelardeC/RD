using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace Artexacta.App.Chart
{
    /// <summary>
    /// Summary description for FusionChart
    /// </summary>
    [Serializable]
    public abstract class FusionChart
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        #region Attributes

        #region Export

        private bool _exportEnabled;
        private string _exportHandler;
        private string _exportAction;
        private string _exportFileName;
        private string _exportCallback;
        private bool _exportShowMenuItem;
        private bool _onlySimpleExport;

        #endregion

        #region Logo

        private string _logoPosition;
        private string _logoUrl;
        private int _logoAlpha;

        #endregion

        #region Data plot

        private bool _useRoundEdges;

        protected string _xmlSchemaFile;

        protected string _xmlSchemaRoot = "chart";

        protected bool _forceDoNotUseWeights = false;

        protected ChartType.TypeOfChart _xmlFormat = ChartType.TypeOfChart.NONE;

        #endregion

        #endregion

        #region Properties

        public Artexacta.App.Chart.ChartObject TheChart
        {
            set;
            get;
        }

        #region Export

        public bool ExportEnabled
        {
            set { _exportEnabled = value; }
            get { return _exportEnabled; }
        }

        public string ExportHandler
        {
            set { _exportHandler = value; }
            get { return _exportHandler; }
        }

        public string ExportAction
        {
            set { _exportAction = value; }
            get { return _exportAction; }
        }

        public string ExportFileName
        {
            set { _exportFileName = value; }
            get { return this._exportFileName; }
        }

        public string ExportCallback
        {
            set { this._exportCallback = value; }
            get { return _exportCallback; }
        }

        public bool ExportShowMenuItem
        {
            set { _exportShowMenuItem = value; }
            get { return _exportShowMenuItem; }
        }

        public bool OnlySimpleExport
        {
            set { _onlySimpleExport = value; }
            get { return _onlySimpleExport; }
        }

        #endregion

        #region Logo

        public string LogoPosition
        {
            set { _logoPosition = value; }
            get { return _logoPosition; }
        }

        public string LogoUrl
        {
            set { _logoUrl = value; }
            get { return _logoUrl; }
        }

        public int LogoAlpha
        {
            set { _logoAlpha = value; }
            get { return _logoAlpha; }
        }

        #endregion

        #region Data plot

        public bool UseRoundEdges
        {
            set { _useRoundEdges = value; }
            get { return _useRoundEdges; }
        }

        public bool ForceDoNotUseWeights
        {
            set { _forceDoNotUseWeights = value; }
            get { return _forceDoNotUseWeights; }
        }

        public ChartType.TypeOfChart XMLFormat
        {
            get { return _xmlFormat; }
            set { _xmlFormat = value; }
        }

        #endregion

        #endregion

        #region Constructor

        public FusionChart()
        {
            _exportEnabled = false;
            _exportAction = "none";
            _exportHandler = ""; // VirtualPathUtility.ToAbsolute("~/UserControls/Charts/FCExporter.aspx");
            _logoPosition = "";
            _exportShowMenuItem = false;
        }

        #endregion

        #region Methods

        protected virtual void BuildConfiguration(DataSet dts)
        {
            DataRow chartRow = dts.Tables["chart"].NewRow();

            chartRow["chart_id"] = 1;

            // Export options
            chartRow["exportEnabled"] = (ExportEnabled ? 1 : 0);
            chartRow["exportAtClient"] = false;
            if (OnlySimpleExport)
            {
                chartRow["exportHandler"] = VirtualPathUtility.ToAbsolute("~/Chart/AnalysisFCExporter.aspx"); ;
            }
            else
            {
                if (!string.IsNullOrEmpty(ExportCallback))
                {
                    chartRow["exportCallback"] = ExportCallback;
                }
                chartRow["exportHandler"] = ExportHandler;
            }
            chartRow["exportAction"] = ExportAction;
            chartRow["exportFileName"] = ExportFileName;
            chartRow["exportShowMenuItem"] = (ExportShowMenuItem ? 1 : 0);

            chartRow["showExportDataMenuItem"] = 1;
            chartRow["exportDataSeparator"] = "{tab}";

            // Logo options
            if (!string.IsNullOrEmpty(LogoUrl))
            {
                chartRow["logoUrl"] = LogoUrl;
                if (!string.IsNullOrEmpty(LogoPosition))
                {
                    chartRow["logoPosition"] = LogoPosition;
                }
                chartRow["logoAlpha"] = LogoAlpha;
            }

            if (TheChart == null)
                throw new Exception("Cannot get xml from fusionchart with chart null");

            chartRow["showTooltip"] = TheChart.ShowToolTip;
            chartRow["numberPrefix"] = TheChart.NumberPrefix; ;
            chartRow["showBorder"] = TheChart.ShowBorder;

            if (!string.IsNullOrEmpty(TheChart.ShowBorderColor))
                chartRow["showBorderColor"] = TheChart.ShowBorderColor;
            if (!string.IsNullOrEmpty(TheChart.BgColor))
                chartRow["bgColor"] = TheChart.BgColor;

            if (!string.IsNullOrEmpty(TheChart.BgColor))
                chartRow["bgAlpha"] = TheChart.BgAlpha;

            if (TheChart is Artexacta.App.Chart.BarChart)
            {
                // data plot
                chartRow["useRoundEdges"] = UseRoundEdges;
            }

            if (TheChart is Artexacta.App.Chart.ErrorChart)
            {
                // data plot
                chartRow["halfErrorBar"] = false;
            }

            if (TheChart is Artexacta.App.Chart.BarChart || TheChart is Artexacta.App.Chart.ErrorChart)
            {
                //Canvas
                if (!string.IsNullOrEmpty(TheChart.CanvasBgColor))
                    chartRow["canvasBgColor"] = TheChart.CanvasBgColor;

                if (!string.IsNullOrEmpty(TheChart.CanvasBgAlpha))
                    chartRow["canvasBgAlpha"] = TheChart.CanvasBgAlpha;

                if (!string.IsNullOrEmpty(TheChart.CanvasBgRatio))
                    chartRow["canvasBgRatio"] = TheChart.CanvasBgRatio;

                //chartRow["canvasBorderThickness"] = Chart.BgAlpha;

                //Axis
                if (!string.IsNullOrEmpty(TheChart.XAxisName))
                    chartRow["xAxisName"] = TheChart.XAxisName;

                if (!string.IsNullOrEmpty(TheChart.YAxisName))
                    chartRow["yAxisName"] = TheChart.YAxisName;

                if (TheChart.YAxisNameWidth > 0)
                    chartRow["yAxisNameWidth"] = TheChart.YAxisNameWidth;

                //chartRow["rotateYAxisName"] = Chart.RotateYAxisName;
            }

            //if (TheChart is Artexacta.IMP.IMPObject.IMPPieChart)
            //{
            //    chartRow["showPercentValues"] = 1;
            //    chartRow["manageLabelOverflow"] = 1;
            //}

            //if (Chart is Artexacta.IMP.IMPObject.IMPBubbleChart)
            //{
            //    // data plot
            //    chartRow["clipBubbles"] = 0;
            //}

            //Data labels
            chartRow["decimals"] = 2;
            chartRow["showValues"] = TheChart.ShowValues;
            chartRow["formatNumberScale"] = 1;
            chartRow["formatNumber"] = true;

            //Title
            if (!string.IsNullOrEmpty(TheChart.Caption))
                chartRow["caption"] = TheChart.Caption;

            if (!string.IsNullOrEmpty(TheChart.SubCaption))
                chartRow["subcaption"] = TheChart.SubCaption;

            dts.Tables["chart"].Rows.Add(chartRow);
        }

        protected abstract string BuildCsvHeaderInformation();

        protected abstract string BuildCsvChartData();

        protected abstract void BuildChartData(DataSet dts);

        protected DataSet GetDataSetForBuildingXml()
        {
            if (this.TheChart.TheChartData.SetOfData.Count == 1)
            {
                if (this.TheChart.ChartTypeId == ChartType.TypeOfChart.BARCHART2AXIS)
                    this._xmlSchemaFile = "~/FusionCharts/Schema/Bar2D.xsd";
                if (this.TheChart.ChartTypeId == ChartType.TypeOfChart.COLUMNCHART)
                    this._xmlSchemaFile = "~/FusionCharts/Schema/Column2D.xsd";
            }

            DataSet dts = null;
            FileStream schemaStream = null;
            try
            {
                schemaStream = new FileStream(HttpContext.Current.Server.MapPath(_xmlSchemaFile), FileMode.Open);
                dts = new DataSet(_xmlSchemaRoot);
                dts.ReadXmlSchema(schemaStream);
                schemaStream.Close();
            }
            catch (Exception e)
            {
                if (schemaStream != null)
                    schemaStream.Close();
                log.Error("Could not load schema, returns null", e);
                dts = null;
            }
            return dts;
        }

        protected abstract void BuildExtraSettings(DataSet dts);

        protected abstract void PrepareConfigurationDataForChart();

        public string GetCSVData()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.Append(BuildCsvHeaderInformation());
            csvData.Append(BuildCsvChartData());
            csvData.Append(BuildCsvFilterInformation());

            return csvData.ToString();
        }

        /// <summary>
        /// This procedure only produces the html for the filter information of a chart.
        /// Normally a chart has only ONE filter. This should work for pie, histo, bar and heatmap charts.
        /// </summary>
        /// <returns></returns>
        protected virtual string BuildCsvFilterInformation()
        {
            if (string.IsNullOrEmpty(this.TheChart.TheChartData.FilterVariables))
            {
                // no filter information
                return "";
            }

            StringBuilder filterInfo = new StringBuilder();
                        
            filterInfo.Append("<table border=\"1\"><tr><td>&nbsp;</td><td>&nbsp;</td></tr>");

            this.AddFilterInformation(filterInfo, Resources.Chart.FilterVariablesLabel, TheChart.TheChartData.FilterVariables);
            
            filterInfo.Append("</table>");

            return filterInfo.ToString();
        }

        public void AddFilterInformation(StringBuilder target, string filterTitle, string filters)
        {
            
        }

        public string GetXMLData()
        {
            DataSet dts = GetDataSetForBuildingXml();

            PrepareConfigurationDataForChart();

            // Builds everything needed
            BuildConfiguration(dts);
            BuildChartData(dts);
            BuildExtraSettings(dts);

            // Prepares output XML string
            StringBuilder outputString = new StringBuilder();
            XmlWriterSettings objSettings = new XmlWriterSettings();
            objSettings.Encoding = Encoding.UTF8;
            objSettings.Indent = false;
            objSettings.NewLineChars = String.Empty;

            XmlWriter xmlWriter = XmlWriter.Create(outputString, objSettings);
            
            dts.WriteXml(xmlWriter);
            xmlWriter.Close();

            //<?xml version=\"1.0\" encoding=\"utf-8\" ?>
            outputString.Replace("<NewDataSet>", "");
            outputString.Replace("</NewDataSet>", "");
            outputString.Replace("\"", "'");
            outputString.Replace("<?xml version='1.0' encoding='utf-16'?>", "<?xml version='1.0' encoding='utf-8'?>");
            // hideous limitation of fusion charts Flash interpreter (does not recognize true or false)
            outputString.Replace("='true'", "='1'");
            outputString.Replace("='false'", "='0'");
            return outputString.ToString();
        }

        #endregion
    }
}