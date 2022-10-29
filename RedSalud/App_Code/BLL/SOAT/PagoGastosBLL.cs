using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.PagoGastos.BLL
{
    public class PagoGastosBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public PagoGastosBLL () { }


        private static PagoGastos FillRecord(PagoGastosDS.PagoGastosRow row)
        {
            DateTime FechaMax = DateTime.MaxValue;
            PagoGastos objPagoGastos = new
                PagoGastos( row.PagoGastosId
                          , row.GastosEjecutadosDetalleId
                          , row.UserId
                          , row.FechaPago
                          , row.Efectivo
                          , row.NroCheque
                          , row.bancoEmisor);

            return objPagoGastos;
        }

        public static PagoGastos GetPagoGastosByID ( int PagoGastosId )
        {
            if (PagoGastosId < 0)
                throw new ArgumentException("PagoGastosId cannot be less than or equal to zero.");

            PagoGastos ThePagoGastos = null;
            try
            {
                PagoGastosDSTableAdapters.PagoGastosTableAdapter theAdapter = new PagoGastosDSTableAdapters.PagoGastosTableAdapter();
                PagoGastosDS.PagoGastosDataTable theTable = theAdapter.GetPagoGastosByID(PagoGastosId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ThePagoGastos = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PagoGastos data", ex);
                throw;
            }
            return ThePagoGastos;
        }

        public static int SearchPagoGastos(ref List<PagoGastosDS.SearchPagoGastosRow> pagos, int PageSize, int firstRow, int ClienteId, string nroFacturas, string orderBy, string estado)
        {
            int? TotalRows = 0;
            try
            {
                PagoGastosDSTableAdapters.SearchPagoGastosTableAdapter theAdapter = new PagoGastosDSTableAdapters.SearchPagoGastosTableAdapter();
                PagoGastosDS.SearchPagoGastosDataTable theTable = theAdapter.SearchPagoGastos(PageSize, firstRow, ref TotalRows, ClienteId, nroFacturas, orderBy, estado);
                foreach (PagoGastosDS.SearchPagoGastosRow ThePagoGastos in theTable.Rows)
                {
                    pagos.Add(ThePagoGastos);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while searching PagoGastos data", ex);
                throw;
            }
            return TotalRows.Value;
        }

        public static void InsertPagoGastos ( ref PagoGastos PagoGastos )
        {
            PagoGastos.PagoGastosId = InsertPagoGastos(PagoGastos.GastosEjecutadosDetalleId, PagoGastos.UserId, PagoGastos.Efectivo, 
                PagoGastos.NroCheque, PagoGastos.BancoEmisor);
        }
        public static int InsertPagoGastos ( int GastosEjecutadosDetalleId, int UserId, bool Efectivo, string NroCheque, string BancoEmisor )
        {
            try
            {
                int? PagoGastosId = 0;
                PagoGastosDSTableAdapters.PagoGastosTableAdapter theAdapter = new PagoGastosDSTableAdapters.PagoGastosTableAdapter();
                theAdapter.Insert(ref PagoGastosId, GastosEjecutadosDetalleId, UserId, Efectivo, NroCheque, BancoEmisor);
                return PagoGastosId.Value;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert PagoGastos", ex);
                throw;
            }
        }
        public static void DeletePagoGastos ( int PagoGastosId )
        {
            if (PagoGastosId <= 0)
                throw new ArgumentException("PagoGastosId cannot be less than or equal to zero.");
            try
            {
                PagoGastosDSTableAdapters.PagoGastosTableAdapter theAdapter = new PagoGastosDSTableAdapters.PagoGastosTableAdapter();
                theAdapter.Delete(PagoGastosId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Delete PagoGastos", ex);
                throw;
            }
        }
    }
}