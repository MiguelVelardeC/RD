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


namespace Artexacta.App.Chart
{
    /// <summary>
    /// Descripción breve de ChartType
    /// </summary>
    [Serializable]
    public class ChartType
    {
        #region Enum

        public enum TypeOfChart
        {
            PIECHART, BARCHART2AXIS, BUBBLECHART, HISTOCHART, NUMHEATMAP, DATAEXPORT, RADARCHART, ERRORCHART2D, COLUMNCHART, NONE
        };


        public static TypeOfChart GetChartTypeFromText(string typeAsString)
        {
            try
            {
                return (TypeOfChart)Enum.Parse(typeof(TypeOfChart), typeAsString);
            }
            catch
            {
                throw new ArgumentException("Not a known type: " + typeAsString);
            }
        }
        #endregion


        #region Attributes

        private string _chartTypeId;
        private string _name;

        #endregion

        #region Properties

        public string ChartTypeId
        {
            set { _chartTypeId = value; }
            get { return _chartTypeId; }
        }

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        #endregion

        #region Constructor

        public ChartType(string chartTypeId, string name)
        {
            _chartTypeId = chartTypeId;
            _name = name;
        }

        #endregion
    }
}