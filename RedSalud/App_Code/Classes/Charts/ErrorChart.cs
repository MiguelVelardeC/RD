﻿using Artexacta.App.Chart;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Chart
{
    /// <summary>
    /// Summary description for IMPBarChart
    /// </summary>
    [Serializable]
    public class ErrorChart : ChartObject
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public override List<ChartAxis> AxisForChart
        {
            get
            {
                List<ChartAxis> result = new List<ChartAxis>();
                result.Add(ChartAxis.Y);
                result.Add(ChartAxis.X);
                result.Add(ChartAxis.SERIES);
                result.Add(ChartAxis.FILTER);

                return result;
            }
        }

        public ErrorChart() : base("", "",
                DateTime.Now, ChartType.TypeOfChart.ERRORCHART2D)
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

        public ErrorChart(string title, string description, DateTime creationDate)
            : base(title, description, creationDate,
                ChartType.TypeOfChart.ERRORCHART2D)
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