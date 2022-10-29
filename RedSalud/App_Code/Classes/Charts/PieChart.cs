using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Chart;
using log4net;

namespace Artexacta.App.Chart
{
    /// <summary>
    /// Summary description for IMPPieChart
    /// </summary>
    [Serializable]
    public class PieChart : ChartObject
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public override List<ChartAxis> AxisForChart
        {
            get
            {
                List<ChartAxis> result = new List<ChartAxis>();
                result.Add(ChartAxis.GROUPING);
                result.Add(ChartAxis.NUMERIC);
                result.Add(ChartAxis.FILTER);

                return result;
            }
        }

        public PieChart() : base("", "",
                DateTime.Now, ChartType.TypeOfChart.PIECHART)
        {
            this.TheChartData = new Artexacta.App.Chart.ChartData("FUSION", "",
                null, Artexacta.App.Chart.ChartData.ComputationType.AVG.ToString(),
                null, Artexacta.App.Chart.ChartData.ComputationType.AVG.ToString(),
                null, null, // No Z variable
                null,       // no filter
                null,       // no series
                null,       // no continent
                false,      // weights
                null,      // no pivot
                null,           // No Main group name
                null, null,       // no compare1 data
                null, null,       // no compare2 data
                null, null);      // no compare3 data
        }

        public PieChart(string title, string description, DateTime creationDate)
            : base(title, description, creationDate, 
                ChartType.TypeOfChart.PIECHART)
        {
            this.TheChartData = new Artexacta.App.Chart.ChartData("FUSION", "",
                    null, Artexacta.App.Chart.ChartData.ComputationType.AVG.ToString(),
                    null, Artexacta.App.Chart.ChartData.ComputationType.AVG.ToString(),
                    null, null, // No Z variable
                    null,       // no filter
                    null,       // no series
                    null,       // no continent
                    false,      // weights
                    null,      // no pivot
                    null,           // No Main group name
                    null, null,       // no compare1 data
                    null, null,       // no compare2 data
                    null, null);      // no compare3 data
        }

        public override string GetChartDetails()
        {
            return "";
        }
    }
}