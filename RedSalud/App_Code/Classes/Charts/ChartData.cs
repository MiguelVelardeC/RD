using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Artexacta.App.Utilities.Chart;

namespace Artexacta.App.Chart
{

    /// <summary>
    /// Descripción breve de ChartData
    /// </summary>
    [Serializable]
    public class ChartData
    {
        #region enums
        public enum FilterType
        {
            MainFilter, Comparison1Filter, Comparison2Filter, Comparison3Filter
        };

        public enum ComputationType { AVG, MEDIAN, SUM };
        #endregion

        #region Attributes

        private string _chartLibraryId;
        private string _sourceCode;
        private string _xVariableId;
        private string _xOperationId;
        private string _yVariableId;
        private string _yOperationId;
        private string _zVariableId;
        private string _zOperationId;
        private string _filterVariables;
        private string _seriesVariableId;
        private string _continentId;
        private bool _usedWeights;
        private string _pivotVariableId;
        private string _mainGroupName;
        private string _compare1GroupName;
        private string _compare1FilterVariables;
        private string _compare2GroupName;
        private string _compare2FilterVariables;
        private string _compare3GroupName;
        private string _compare3FilterVariables;

        #endregion

        #region Properties

        public string ChartLibraryId
        {
            set { _chartLibraryId = value; }
            get { return _chartLibraryId; }
        }

        public string SourceCode
        {
            set { _sourceCode = value; }
            get { return _sourceCode; }
        }

        public string XVariableId
        {
            set { _xVariableId = value; }
            get { return _xVariableId; }
        }

        public string XOperationId
        {
            set { _xOperationId = value; }
            get { return _xOperationId; }
        }

        public string YVariableId
        {
            set { _yVariableId = value; }
            get { return _yVariableId; }
        }

        public string YOperationId
        {
            set { _yOperationId = value; }
            get { return _yOperationId; }
        }

        public string ZVariableId
        {
            set { _zVariableId = value; }
            get { return _zVariableId; }
        }

        public string ZOperationId
        {
            set { _zOperationId = value; }
            get { return _zOperationId; }
        }

        public string FilterVariables
        {
            set { _filterVariables = value; }
            get { return _filterVariables; }
        }

        public string MainGroupName
        {
            set { _mainGroupName = value; }
            get { return _mainGroupName; }
        }

        public string Compare1GroupName
        {
            set { _compare1GroupName = value; }
            get { return _compare1GroupName; }
        }

        public string Compare2GroupName
        {
            set { _compare2GroupName = value; }
            get { return _compare2GroupName; }
        }

        public string Compare3GroupName
        {
            set { _compare3GroupName = value; }
            get { return _compare3GroupName; }
        }

        public string Compare1FilterVariables
        {
            set { _compare1FilterVariables = value; }
            get { return _compare1FilterVariables; }
        }

        public string Compare2FilterVariables
        {
            set { _compare2FilterVariables = value; }
            get { return _compare2FilterVariables; }
        }

        public string Compare3FilterVariables
        {
            set { _compare3FilterVariables = value; }
            get { return _compare3FilterVariables; }
        }

        public string SeriesVariableId
        {
            set { _seriesVariableId = value; }
            get { return _seriesVariableId; }
        }

        public string ContinentId
        {
            set { _continentId = value; }
            get { return _continentId; }
        }

        public bool UsedWeights
        {
            set { _usedWeights = value; }
            get { return _usedWeights; }
        }

        public string PivotVariableId
        {
            set { _pivotVariableId = value; }
            get { return _pivotVariableId; }
        }

        public List<DataSeries> SetOfData { get; set; }
        #endregion

        public ChartData(string chartLibraryId,
            string sourceCode,
            string xVariableId,
            string xOperationId,
            string yVariableId,
            string yOperationId,
            string zVariableId,
            string zOperationId,
            string filterVariables,
            string seriesVariableId,
            string continentId,
            bool usedWeights,
            string pivotVariableId,
            string mainGroupName,
            string compare1GroupName,
            string compare1FilterVariables,
            string compare2GroupName,
            string compare2FilterVariables,
            string compare3GroupName,
            string compare3FilterVariables)
        {
            _chartLibraryId = chartLibraryId;
            _sourceCode = sourceCode;
            _xVariableId = xVariableId;
            _xOperationId = xOperationId;
            _yVariableId = yVariableId;
            _yOperationId = yOperationId;
            _zVariableId = zVariableId;
            _zOperationId = zOperationId;
            _filterVariables = filterVariables;
            _seriesVariableId = seriesVariableId;
            _continentId = continentId;
            _usedWeights = usedWeights;
            _pivotVariableId = pivotVariableId;
            _mainGroupName = mainGroupName;
            _compare1GroupName = compare1GroupName;
            _compare1FilterVariables = compare1FilterVariables;
            _compare2GroupName = compare2GroupName;
            _compare2FilterVariables = compare2FilterVariables;
            _compare3GroupName = compare3GroupName;
            _compare3FilterVariables = compare3FilterVariables;
        }
    }
}