using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.GastosEjecutados.BLL
{
    public class GastosEjecutadosBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public GastosEjecutadosBLL () { }

        private static GastosEjecutadosDetalle FillRecordDetalle ( GastosEjecutadosDS.GastosEjecutadosDetalleRow row )
        {
            GastosEjecutadosDetalle objGastosEjecutados = new
                GastosEjecutadosDetalle(
                     row.GastosEjecutadosDetalleId
                    , row.GastosEjecutadosId
                    , row.PreliquidacionDetalleId
                    , row.Fecha
                    , row.Proveedor
                    , row.FechaReciboFactura
                    , row.NumeroReciboFactura
                    , row.Tipo
                    , row.Monto
                );

            if (!row.IsIsFacturaNull())
            {
                objGastosEjecutados.IsFactura = row.IsFactura;
            }

            return objGastosEjecutados;
        }

        public static GastosEjecutadosDetalle GetGastosEjecutadosDetalleByID ( int GastosEjecutadosDetalleID )
        {
            if (GastosEjecutadosDetalleID < 0)
                throw new ArgumentException("GastosEjecutadosDetalleID cannot be less than or equal to zero.");

            GastosEjecutadosDetalle TheGastosEjecutadosDetalle = null;
            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                GastosEjecutadosDS.GastosEjecutadosDetalleDataTable theTable = theAdapter.GetGastosEjecutadosDetalleById(GastosEjecutadosDetalleID);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheGastosEjecutadosDetalle = FillRecordDetalle(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GastosEjecutadosDetalle data", ex);
                throw;
            }
            return TheGastosEjecutadosDetalle;
        }

        public static List<GastosEjecutadosDetalle> GetGastosEjecutadosBySiniestroIdAndAccidentadoId ( int SiniestroID, int AccidentadoId )
        {
            if (SiniestroID < 0)
                throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            List<GastosEjecutadosDetalle> TheList = new List<GastosEjecutadosDetalle>();
            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                GastosEjecutadosDS.GastosEjecutadosDetalleDataTable theTable = theAdapter.GetGastosEjecutadosBySiniestroIdAndAccidentadoId(SiniestroID, AccidentadoId);
                decimal total = 0;
                decimal totalPreliquidado = 0;
                decimal reservas = 0;
                int last = 0;
                foreach (GastosEjecutadosDS.GastosEjecutadosDetalleRow row in theTable.Rows)
                {
                    switch (row.Tipo)
                    {
                        case "RESERVAS":
                            reservas = row.Monto;
                            TheList.Add(new GastosEjecutadosDetalle(row.GastosEjecutadosDetalleId, row.GastosEjecutadosId, row.Tipo, row.Monto));
                            break;
                        case "TOTALPRELIQUIDADO":
                            totalPreliquidado = row.Monto;
                            break;
                        default:
                            total += row.Monto;
                            TheList.Add(new GastosEjecutadosDetalle(row.GastosEjecutadosDetalleId, row.GastosEjecutadosId, row.Tipo, row.Monto));
                            break;
                    }
                    last = row.GastosEjecutadosDetalleId;
                }
                TheList.Add(new GastosEjecutadosDetalle(0, 0, "TOTALES", total));
                TheList.Add(new GastosEjecutadosDetalle(0, 0, "SALDO", last - total));
                TheList.Add(new GastosEjecutadosDetalle(0, 0, "AHORRO", totalPreliquidado - total));
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GastosEjecutadosDetalle list data", ex);
                throw;
            }
            return TheList;
        }


        public static List<GastosEjecutadosDetalle> GetAllGastosEjecutadosDetalle ( int SiniestroID, int AccidentadoId )
        {
            return GetGastosEjecutadosDetalleByTipo(SiniestroID, AccidentadoId, "");
        }
        public static List<GastosEjecutadosDetalle> GetGastosEjecutadosDetalleByTipo ( int SiniestroID, int AccidentadoId, string tipo )
        {
            if (SiniestroID < 0)
                throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            List<GastosEjecutadosDetalle> TheList = new List<GastosEjecutadosDetalle>();
            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                GastosEjecutadosDS.GastosEjecutadosDetalleDataTable theTable = theAdapter.GetGastosEjecutadosDetalleBySiniestroIdAndAccidentadoId(SiniestroID, AccidentadoId, tipo);
                foreach (GastosEjecutadosDS.GastosEjecutadosDetalleRow row in theTable.Rows)
                {
                    TheList.Add(FillRecordDetalle(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GastosEjecutadosDetalle list data", ex);
                throw;
            }
            return TheList;
        }

        public static void InsertGastosEjecutadosDetalle ( GastosEjecutadosDetalle gastos )
        {
            InsertGastosEjecutadosDetalle(gastos.SiniestroId, gastos.AccidentadoId, gastos.PreliquidacionDetalleId, 
                gastos.Tipo, gastos.Fecha, gastos.Proveedor, gastos.FechaReciboFactura, gastos.NumeroReciboFactura, 
                gastos.Cobertura, gastos.Monto);
        }
        public static int InsertGastosEjecutadosDetalle ( int SiniestroId, int AccidentadoId, int PreliquidacionDetalleId, string tipo, DateTime fecha,
            string proveedor, DateTime fechaReciboFactura, string numeroReciboFactura, decimal cobertura, decimal monto )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                int? GastosEjecutadosId = 0;
                theAdapter.Insert(SiniestroId, AccidentadoId, PreliquidacionDetalleId, proveedor, fechaReciboFactura, numeroReciboFactura, 
                    tipo, fecha, cobertura, monto);
                return (int)GastosEjecutadosId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting GastosEjecutados", ex);
                throw;
            }
        }

        public static void UpdateGastosEjecutadosDetalle ( GastosEjecutadosDetalle gastos )
        {
            UpdateGastosEjecutadosDetalle(gastos.GastosEjecutadosDetalleId, gastos.PreliquidacionDetalleId, gastos.Proveedor,
                gastos.FechaReciboFactura, gastos.NumeroReciboFactura, gastos.Tipo, gastos.Fecha, gastos.Monto);
        }
        public static void UpdateGastosEjecutadosDetalle ( int GastosEjecutadosId, int PreliquidacionDetalleId, string proveedor, 
            DateTime fechaReciboFactura, string numeroReciboFactura, string tipo, DateTime fecha, decimal monto )
        {
            if (GastosEjecutadosId <= 0)
                throw new ArgumentException("GastosEjecutadosId cannot be less than or equal to zero.");

            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                theAdapter.Update(GastosEjecutadosId,PreliquidacionDetalleId, proveedor, fechaReciboFactura, numeroReciboFactura, tipo, fecha, monto);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting GastosEjecutados", ex);
                throw;
            }
        }
        public static void UpdateReserva ( int SiniestroId, int AccidentadoId, decimal monto )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                decimal montoGestion = Configuration.Configuration.GetMontoGestion();
                theAdapter.UpdateReserva(SiniestroId, AccidentadoId, montoGestion, monto);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating reserva", ex);
                throw;
            }
        }
        public static void DeleteGastosEjecutadosDetalle ( int GastosEjecutadosId )
        {
            if (GastosEjecutadosId <= 0)
                throw new ArgumentException("GastosEjecutadosId cannot be less than or equal to zero.");

            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                theAdapter.Delete(GastosEjecutadosId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting GastosEjecutados", ex);
                throw;
            }
        }

        public static decimal GetReservaBySiniestroIdAndAccidentadoId(int SiniestroId, int AccidentadoId)
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter theAdapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();
                decimal? decMonto = 0;
                theAdapter.GetReservaBySiniestroIdAndAccidentadoId(SiniestroId, AccidentadoId, ref decMonto);
                return (decMonto.Value != null) ? decMonto.Value : 0;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating reserva", ex);
                throw;
            }
        }

        public static bool UpdateGastosEjecutados(int SiniestroId, int AccidentadoId, decimal montoGestion)
        {
            try
            {
                GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter adapter = new GastosEjecutadosDSTableAdapters.GastosEjecutadosDetalleTableAdapter();

                adapter.UpdateGastosEjecutados(SiniestroId, AccidentadoId, montoGestion);

                return true;
            }
            catch (Exception eq)
            {
                log.Error("No se pudo actualizar UpdateGastosEjecutados", eq);
                throw eq;
            }
        }
    }
}