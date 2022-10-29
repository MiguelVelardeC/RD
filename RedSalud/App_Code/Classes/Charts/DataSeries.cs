using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Chart
{

    /// <summary>
    /// Summary description for IMPDataSeries
    /// </summary>
    [Serializable]
    public class DataSeries
    {
        private string _xDataVariable = "";
        private string _yDataVariable = "";
        private string _zDataVariable = "";
        private string _pivotDataVariable = "";
        private string _colorDataVariable = "";

        private int _seriesTitleID = 0;

        private bool _usesWeights = false;
        private int _dontShowValueIfCountLessThan = 4;
        private List<DataPoint> _theDataPoints = new List<DataPoint>();

        public string SeriesTitle { get; set; }

        public List<DataPoint> TheDataPoints
        {
            get { return _theDataPoints; }
            set { _theDataPoints = value; }
        }

        /// <summary>
        /// True if weights were used in the calculation of the data series
        /// </summary>
        public bool UsedWeights
        {
            get { return _usesWeights; }
            set { _usesWeights = value; }
        }

        /// <summary>
        /// The Id of the variable used for the X axis, if any
        /// </summary>
        public string XDataVariableID
        {
            get { return _xDataVariable; }
            set { _xDataVariable = value; }
        }

        /// <summary>
        /// The Id of the variable used for the Y axis, if any
        /// </summary>
        public string YDataVariableID
        {
            get { return _yDataVariable; }
            set { _yDataVariable = value; }
        }

        /// <summary>
        /// The Id of the variable used for the Z axis, if any
        /// </summary>
        public string ZDataVariableID
        {
            get { return _yDataVariable; }
            set { _yDataVariable = value; }
        }

        /// <summary>
        /// The Id of the variable used for the grouping/pivot, if any
        /// </summary>
        public string PivotDataVariableID
        {
            get { return _pivotDataVariable; }
            set { _pivotDataVariable = value; }
        }

        /// <summary>
        /// The Id of the variable used for the color, if any
        /// </summary>
        public string ColorDataVariableID
        {
            get { return _colorDataVariable; }
            set { _colorDataVariable = value; }
        }

        /// <summary>
        /// The title of the series, if any
        /// </summary>
        public int SeriesTitleID
        {
            get { return _seriesTitleID; }
            set { _seriesTitleID = value; }
        }

        /// <summary>
        /// The title of the series, if any
        /// </summary>
        public int DontShowValueIfCountLessThan
        {
            get { return _dontShowValueIfCountLessThan; }
            set { _dontShowValueIfCountLessThan = value; }
        }

        public DataSeries()
        {
        }

        public TwoDDataPoint Get2DDataPointForX(int xValue)
        {
            foreach (DataPoint point in _theDataPoints)
            {
                if (!(point is TwoDDataPoint))
                    continue;

                TwoDDataPoint result = (TwoDDataPoint)point;

                if (result == null)
                    continue;

                if (result.XDataPoint == xValue)
                    return result;
            }
            return null;
        }

        public TwoDDataPoint Get2DDataPointForX(string xValue)
        {
            foreach (DataPoint point in _theDataPoints)
            {
                if (!(point is TwoDDataPoint))
                    continue;

                TwoDDataPoint result = (TwoDDataPoint)point;

                if (result == null)
                    continue;

                if (result.XDataPointString == xValue)
                    return result;
            }
            return null;
        }

        public void sortData()
        {
            List<TwoDDataPoint> sortedData = new List<TwoDDataPoint>();
            foreach(DataPoint p in _theDataPoints) {
                TwoDDataPoint dp = (TwoDDataPoint)p;
                sortedData.Add(dp);
            }
            sortedData = sortedData.OrderBy(o => o.YDataPoint).ToList();

            _theDataPoints = new List<DataPoint>();
            foreach (TwoDDataPoint p in sortedData)
            {
                _theDataPoints.Add(p);
            }
        }
    }
}