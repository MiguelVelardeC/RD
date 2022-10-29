using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Chart
{

    /// <summary>
    /// Summary description for IMP2DDataPoint
    /// </summary>
    [Serializable]
    public class TwoDDataPoint : DataPoint
    {
        public string XDataPointString { get; set; }
        private int _xDataPoint;
        private decimal _yDataPoint;
        private int _observationCount;
        private decimal _errorValue;

        public decimal ErrorValue
        {
            get { return _errorValue; }
            set { _errorValue = value; }
        }
        
        /// <summary>
        /// The number of observations that were used to calculate the value
        /// </summary>
        public int ObservationCount
        {
            get { return _observationCount; }
            set { _observationCount = value; }
        }

        public int XDataPoint
        {
            get { return _xDataPoint; }
            set { _xDataPoint = value; }
        }

        public decimal YDataPoint
        {
            get { return _yDataPoint; }
            set { _yDataPoint = value; }
        }

        public TwoDDataPoint()
        {
        }

        public TwoDDataPoint(string xdataPointString, int xDataPoint, decimal yDataPoint, int observationCount)
        {
            XDataPointString = xdataPointString;
            _xDataPoint = xDataPoint;
            _yDataPoint = yDataPoint;
            _observationCount = observationCount;
        }
    }
}