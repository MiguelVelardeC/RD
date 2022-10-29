using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Chart
{

    /// <summary>
    /// Summary description for IMPChart
    /// </summary>
    [Serializable]
    public abstract class ChartObject
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public enum ChartMode
        {
            VIEW, EDIT, READONLY
        };

        public enum ChartAxis
        {
            X, Y, SIZE, SERIES, COLOR, PIVOT, FILTER, NUMERIC, COUNT, GROUPING, EXPORT, CLASSIFICATION
        };

        public abstract List<ChartAxis> AxisForChart
        {
            get;
        }

        public static string GetChartAxisName(ChartAxis axis)
        {
            switch (axis)
            {
                case ChartAxis.X :
                    return Resources.ChartVariables.XAxisName;
                case ChartAxis.Y:
                    return Resources.ChartVariables.YAxisName;
                case ChartAxis.SIZE:
                    return Resources.ChartVariables.SizeAxisName;
                case ChartAxis.SERIES:
                    return Resources.ChartVariables.SeriesAxisName;
                case ChartAxis.COLOR:
                    return Resources.ChartVariables.ColorAxisName;
                case ChartAxis.PIVOT:
                    return Resources.ChartVariables.PivotAxisName;
                case ChartAxis.FILTER:
                    return Resources.ChartVariables.FilterAxisName;
                case ChartAxis.NUMERIC:
                    return Resources.ChartVariables.NumericAxisName;
                case ChartAxis.COUNT:
                    return Resources.ChartVariables.CountAxisName;
                case ChartAxis.GROUPING:
                    return Resources.ChartVariables.GroupingAxisName;
                case ChartAxis.CLASSIFICATION:
                    return Resources.ChartVariables.Classification;
                case ChartAxis.EXPORT:
                    return Resources.ChartVariables.Export;
            }
            return "";
        }

        public static ChartAxis GetChartAxisFromText(string axisAsString)
        {
            try
            {
                return (ChartAxis)Enum.Parse(typeof(ChartAxis), axisAsString);
            }
            catch
            {
                throw new ArgumentException("Not a known type: " + axisAsString);
            }
        }

        public static ChartMode GetChartModeFromText(string modeAsString)
        {
            if (string.IsNullOrEmpty(modeAsString))
                throw new ArgumentException("empty string cannot be chart mode");

            if (modeAsString.Equals("VIEW"))
                return ChartMode.VIEW;

            if (modeAsString.Equals("EDIT"))
                return ChartMode.EDIT;

            if (modeAsString.Equals("READONLY"))
                return ChartMode.READONLY;

            throw new ArgumentException("Neither view nor edit");
        }

        #region Attributes

        private ChartType.TypeOfChart _chartTypeId;


        private int _width;
        private int _height;
        
        //Chart border and background
        private string _bgColor;
        private string _bgAlpha;
        private bool _showBorder;
        private string _showBorderColor;
        private int _borderThickness;
        private int _borderAlpha;

        //Canvas
        private string _canvasBgColor;
        private string _canvasBgAlpha;
        private int _canvasBorderThickness;
        private string _canvasBgRatio;

        //Title and Axis Name
        private string _caption;
        private string _subCaption;
        private string _xAxisName;
        private string _yAxisName;
        private bool _rotateYAxisName;
        private int _yAxisNameWidth;

        //Data plot
        private string _numberPrefix;

        //Data Labels
        private bool _showValues;

        private bool _showTooltip;

        #endregion

        #region Properties

        public string LanguageID { get; set; }

        public ChartType.TypeOfChart ChartTypeId
        {
            set { _chartTypeId = value; }
            get { return _chartTypeId; }
        }

        public ChartData TheChartData
        {
            get ;
            set ; 
        }

        public int Width
        {
            set { _width = value; }
            get { return _width; }
        }

        public int Height
        {
            set { _height = value; }
            get { return _height; }
        }

        public bool ShowToolTip
        {
            set { _showTooltip = value; }
            get
            { return _showTooltip; }
        }

        public string BgColor
        {
            set { _bgColor = value; }
            get { return _bgColor; }
        }

        public string BgAlpha
        {
            set { _bgAlpha = value; }
            get { return _bgAlpha; }
        }

        public bool ShowBorder
        {
            set { _showBorder = value; }
            get { return _showBorder; }
        }

        public string ShowBorderColor
        {
            set { _showBorderColor = value; }
            get { return _showBorderColor; }
        }

        public int BorderThickness
        {
            set { _borderThickness = value; }
            get { return _borderThickness; }
        }

        public int BorderAlpha
        {
            set { _borderAlpha = value; }
            get { return _borderAlpha; }
        }

        public string CanvasBgColor
        {
            set { _canvasBgColor = value; }
            get { return _canvasBgColor; }
        }

        public string CanvasBgAlpha
        {
            set { _canvasBgAlpha = value; }
            get { return _canvasBgAlpha; }
        }

        public int CanvasBorderThickness
        {
            set { _canvasBorderThickness = value; }
            get { return _canvasBorderThickness; }
        }

        public string CanvasBgRatio
        {
            set { _canvasBgRatio = value; }
            get { return _canvasBgRatio; }
        }

        public string Caption
        {
            set { _caption = value; }
            get { return _caption; }
        }

        public string SubCaption
        {
            set { _subCaption = value; }
            get { return _subCaption; }
        }

        public string XAxisName
        {
            set { _xAxisName = value; }
            get { return _xAxisName; }
        }

        public string YAxisName
        {
            set { _yAxisName = value; }
            get { return _yAxisName; }
        }

        public bool RotateYAxisName
        {
            set { _rotateYAxisName = value; }
            get { return _rotateYAxisName; }
        }

        public int YAxisNameWidth
        {
            set { _yAxisNameWidth = value; }
            get { return _yAxisNameWidth; }
        }
        
        public string NumberPrefix
        {
            set { _numberPrefix = value; }
            get { return _numberPrefix; }
        }

        public bool ShowValues
        {
            set { _showValues = value; }
            get { return _showValues; }
        }

        #endregion

        public ChartObject(string title, string description, DateTime creationDate,
             ChartType.TypeOfChart chartType)
        {
            SetDefaultUIValues();

            this._caption = title;
            this._subCaption = description;
            this._chartTypeId = chartType;
        }

        public ChartObject()
        {
            this.LanguageID = "ES";
            SetDefaultUIValues();            
        }

        public void SetDefaultUIValues() {
            _width = 600;
            _height = 400;
            
            _numberPrefix = "";
            _rotateYAxisName = true;
            _showBorder = true;
            _showBorderColor = "";
            _showValues = true;
            _showTooltip = true;

            _subCaption = "";
            _caption = "";

            _xAxisName = "";
            _yAxisName = "";
            _yAxisNameWidth = 0;

            _canvasBgAlpha = "";
            _canvasBgColor = "";
            _canvasBgRatio = "";
            _canvasBorderThickness = 1;
        }
        
        public static ChartObject NewChart(string title, string description, DateTime creationDate,
            ChartType.TypeOfChart chartType)
        {
            ChartObject result = null;
            if (chartType == ChartType.TypeOfChart.BARCHART2AXIS)
            {
                BarChart objNew = new BarChart(title, description, creationDate);
                return objNew;
            }
            else if (chartType == ChartType.TypeOfChart.PIECHART)
            {
                result = new PieChart(title, description, creationDate);
                return result;
            }
            else if (chartType == ChartType.TypeOfChart.BUBBLECHART)
            {
                result = null;
                return result;
            }
            else if (chartType == ChartType.TypeOfChart.HISTOCHART)
            {
                result = null;
                return result;
            }
            else if (chartType == ChartType.TypeOfChart.NUMHEATMAP)
            {
                result = null;
                return result;
            }
            else if (chartType == ChartType.TypeOfChart.COLUMNCHART)
            {
                BarChart objNew = new BarChart(title, description, creationDate);
                return objNew;
            }
            else if (chartType == ChartType.TypeOfChart.ERRORCHART2D)
            {
                result = new ErrorChart(title, description, creationDate);
                return result;
            }
            else
            {
                log.Error("chartType cannot be recognized:" + chartType.ToString());
                return null;
            }
        }

        /// <summary>
        /// This should select by default the first variables possible in the sert of variables.
        /// </summary>
        /// <param name="surveyGroupId"></param>
        /// <param name="theChartType"></param>
        /// <returns></returns>
        public static ChartObject NewChart(ChartType.TypeOfChart theChartType)
        {
            ChartObject result = null;
            if (theChartType == ChartType.TypeOfChart.BARCHART2AXIS)
            {
                BarChart obj = new BarChart();
                result = obj;
                
                return result;
            }
            else if (theChartType == ChartType.TypeOfChart.PIECHART)
            {
                return null;
            }
            else if (theChartType == ChartType.TypeOfChart.BUBBLECHART)
            {
                result = null;
                return result;
            }
            else if (theChartType == ChartType.TypeOfChart.HISTOCHART)
            {
                return null;
            }
            else if (theChartType == ChartType.TypeOfChart.NUMHEATMAP)
            {
                return null;
            }
            else if (theChartType == ChartType.TypeOfChart.DATAEXPORT)
            {
                return null;
            }
            else if (theChartType == ChartType.TypeOfChart.COLUMNCHART)
            {
                BarChart obj = new BarChart();
                result = obj;

                return result;
            }
            if (theChartType == ChartType.TypeOfChart.ERRORCHART2D)
            {
                result = new ErrorChart();
                return result;
            }
            else
            {
                log.Error("chartType cannot be recognized:" + theChartType.ToString());
                return null;
            }
        }
        public abstract string GetChartDetails();
    }
}