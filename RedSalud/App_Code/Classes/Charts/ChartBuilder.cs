using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Chart;
using Artexacta.App.Utilities;
using log4net;


namespace Artexacta.App.Chart
{
    /// <summary>
    /// Summary description for ChartBuilder
    /// </summary>
    public class ChartBuilder
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public static List<DataSeries> GetSOATTotales(int siniestros, int accidentes, int fallecidos)
        {
            List<DataSeries> result = new List<DataSeries>();
            DataSeries points = new DataSeries();
            TwoDDataPoint a2dPoint = new TwoDDataPoint("SINIESTROS", 1, siniestros, 1);
            
            points.TheDataPoints.Add(a2dPoint);
            a2dPoint = new TwoDDataPoint("ACCIDENTADOS", 2, accidentes, 1);
            points.TheDataPoints.Add(a2dPoint);
            a2dPoint = new TwoDDataPoint("FALLECIDOS", 3, fallecidos, 1);
            points.TheDataPoints.Add(a2dPoint);

            result.Add(points);
            return result;
        }

        public static List<DataSeries> GetSOATXSector(int gestion, int ClienteId)
        {
            List<DataSeries> result = new List<DataSeries>();
            DataSeries points = new DataSeries();

            if (gestion < 0)
                throw new ArgumentException("Gestion cannot be less than or equal to zero.");
            if (ClienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            try
            {
                EstadisticasDSTableAdapters.SOATEstadisticasXSectorTableAdapter adapter =
                    new EstadisticasDSTableAdapters.SOATEstadisticasXSectorTableAdapter();
                EstadisticasDS.SOATEstadisticasXSectorDataTable theTable = adapter.GetSOATEstadisticasXSector(gestion, ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (EstadisticasDS.SOATEstadisticasXSectorRow row in theTable.Rows)
                    {
                        i++;
                        TwoDDataPoint a2dPoint = new TwoDDataPoint(row.sector, i, row.Column1, 1);
                        points.TheDataPoints.Add(a2dPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while GetSOATEstadisticasTotales", ex);
                throw;
            }
            result.Add(points);
            return result;
        }

        public static List<DataSeries> GetSOATXVehiculo(int gestion, int ClienteId)
        {
            List<DataSeries> result = new List<DataSeries>();
            DataSeries points = new DataSeries();

            if (gestion < 0)
                throw new ArgumentException("Gestion cannot be less than or equal to zero.");
            if (ClienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            try
            {
                EstadisticasDSTableAdapters.SOATEstadisticasXVehiculoTableAdapter adapter =
                    new EstadisticasDSTableAdapters.SOATEstadisticasXVehiculoTableAdapter();
                EstadisticasDS.SOATEstadisticasXVehiculoDataTable theTable = adapter.GetEstadisticasXVehiculo(gestion, ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (EstadisticasDS.SOATEstadisticasXVehiculoRow row in theTable.Rows)
                    {
                        i++;
                        TwoDDataPoint a2dPoint = new TwoDDataPoint(row.Tipo, i, row.Column1, 1);
                        points.TheDataPoints.Add(a2dPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while GetSOATEstadisticasTotales", ex);
                throw;
            }
            result.Add(points);
            return result;
        }

        public static List<DataSeries> GetSOATXGastos(int gestion, int ClienteId)
        {
            List<DataSeries> result = new List<DataSeries>();
            DataSeries points = new DataSeries();

            if (gestion < 0)
                throw new ArgumentException("Gestion cannot be less than or equal to zero.");
            if (ClienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            try
            {
                EstadisticasDSTableAdapters.SOATEstadisticasGastosTableAdapter adapter =
                    new EstadisticasDSTableAdapters.SOATEstadisticasGastosTableAdapter();
                EstadisticasDS.SOATEstadisticasGastosDataTable theTable = adapter.GetSOATEstadisticasXGastos(gestion, ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (EstadisticasDS.SOATEstadisticasGastosRow row in theTable.Rows)
                    {
                        i++;
                        TwoDDataPoint a2dPoint = new TwoDDataPoint(row.Tipo, i, row.Column1, 1);
                        points.TheDataPoints.Add(a2dPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while SOATEstadisticasGastos", ex);
                throw;
            }
            result.Add(points);
            return result;
        }

        public static List<DataSeries> GetSOATXLugar(int gestion, int ClienteId)
        {
            List<DataSeries> result = new List<DataSeries>();
            DataSeries points = new DataSeries();

            if (gestion < 0)
                throw new ArgumentException("Gestion cannot be less than or equal to zero.");
            if (ClienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            try
            {
                EstadisticasDSTableAdapters.SOATEstadisticasXLugarTableAdapter adapter =
                    new EstadisticasDSTableAdapters.SOATEstadisticasXLugarTableAdapter();
                EstadisticasDS.SOATEstadisticasXLugarDataTable theTable = adapter.GetSOATEstadisticasXLugar(gestion, ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (EstadisticasDS.SOATEstadisticasXLugarRow row in theTable.Rows)
                    {
                        i++;
                        TwoDDataPoint a2dPoint = new TwoDDataPoint(row.LugarDpto, i, row.Column1, 1);
                        points.TheDataPoints.Add(a2dPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while SOATEstadisticasXLugarDataTable", ex);
                throw;
            }
            result.Add(points);
            return result;
        }

        public static List<DataSeries> GetSOATXMes(int gestion, int ClienteId)
        {
            List<DataSeries> result = new List<DataSeries>();
            DataSeries points = new DataSeries();

            if (gestion < 0)
                throw new ArgumentException("Gestion cannot be less than or equal to zero.");
            if (ClienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            try
            {
                EstadisticasDSTableAdapters.SOATEstadisticasXMesTableAdapter adapter =
                    new EstadisticasDSTableAdapters.SOATEstadisticasXMesTableAdapter();
                EstadisticasDS.SOATEstadisticasXMesDataTable theTable = adapter.GetSOATEstadisticasXMes(gestion, ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (EstadisticasDS.SOATEstadisticasXMesRow row in theTable.Rows)
                    {
                        i++;
                        TwoDDataPoint a2dPoint = new TwoDDataPoint(row.nombreMes, i, row.siniestralidad, 1);
                        points.TheDataPoints.Add(a2dPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while SOATEstadisticasXLugarDataTable", ex);
                throw;
            }
            result.Add(points);
            return result;
        }
    }
}