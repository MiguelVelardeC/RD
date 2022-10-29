using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Preliquidacion.BLL
{
    public class PreliquidacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public PreliquidacionBLL () { }


        //private static Preliquidacion FillRecord(PreliquidacionDS.PreliquidacionRow row)
        //{
        //    Preliquidacion objPreliquidacion = new
        //        Preliquidacion(
        //             row.PreliquidacionId
        //            ,row.SiniestroId
        //            ,row.AccidentadoId
        //            ,row.Fecha
        //            ,row.MontoGestion
        //            ,row.Reservas
        //            ,row.Hospitalarios
        //            ,row.Cirugia
        //            ,row.Ambulancias
        //            ,row.Laboratorios
        //            ,row.Farmacia
        //            ,row.Honorarios
        //            ,row.Ambulatorio
        //            ,row.Osteosintesis
        //        );

        //    return objPreliquidacion;
        //}
        private static PreliquidacionDetalle FillRecordDetalle ( PreliquidacionDS.PreliquidacionDetalleRow row )
        {
            PreliquidacionDetalle objPreliquidacion = new
                PreliquidacionDetalle(
                     row.PreliquidacionDetalleId
                    , row.PreliquidacionId
                    , row.Fecha
                    , row.Proveedor
                    , row.FechaReciboFactura
                    , row.NumeroReciboFactura
                    , row.IsFactura
                    , row.Tipo
                    , row.Monto
                    , row.Estado
                );

            return objPreliquidacion;
        }

        //public static Preliquidacion GetPreliquidacionBySiniestroID ( int SiniestroID, int AccidentadoId )
        //{
        //    if (SiniestroID < 0)
        //        throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");
        //    if (AccidentadoId < 0)
        //        throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

        //    Preliquidacion ThePreliquidacion = null;
        //    try
        //    {
        //        PreliquidacionDSTableAdapters.PreliquidacionTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionTableAdapter();
        //        PreliquidacionDS.PreliquidacionDataTable theTable = theAdapter.GetPreliquidacionById(SiniestroID, AccidentadoId);
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        {
        //            ThePreliquidacion = FillRecord(theTable[0]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting Preliquidacion data", ex);
        //        throw;
        //    }
        //    return ThePreliquidacion;
        //}

        public static PreliquidacionDetalle GetPreliquidacionDetalleByID ( int PreliquidacionDetalleID )
        {
            if (PreliquidacionDetalleID < 0)
                throw new ArgumentException("PreliquidacionDetalleID cannot be less than or equal to zero.");

            PreliquidacionDetalle ThePreliquidacionDetalle = null;
            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                PreliquidacionDS.PreliquidacionDetalleDataTable theTable = theAdapter.GetPreliquidacionDetalleById(PreliquidacionDetalleID);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ThePreliquidacionDetalle = FillRecordDetalle(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PreliquidacionDetalle data", ex);
                throw;
            }
            return ThePreliquidacionDetalle;
        }

        public static List<PreliquidacionDetalle> GetPreliquidacionBySiniestroIdAndAccidentadoId ( int SiniestroID, int AccidentadoId )
        {
            if (SiniestroID < 0)
                throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            List<PreliquidacionDetalle> TheList = new List<PreliquidacionDetalle>();
            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                PreliquidacionDS.PreliquidacionDetalleDataTable theTable = theAdapter.GetPreliquidacionBySiniestroIdAndAccidentadoId(SiniestroID, AccidentadoId);
                decimal total = 0;
                foreach (PreliquidacionDS.PreliquidacionDetalleRow row in theTable.Rows)
                {
                    TheList.Add(new PreliquidacionDetalle(row.PreliquidacionDetalleId, row.PreliquidacionId, row.Tipo, row.Monto));
                    
                    if(row.Tipo != "RESERVAS")
                        total += row.Monto;
                }
                TheList.Add(new PreliquidacionDetalle(0, 0, "TOTALES", total));
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PreliquidacionDetalle list data", ex);
                throw;
            }
            return TheList;
        }

        public static List<PreliquidacionDetalle> GetPreliquidacionDetalleForCombo ( int SiniestroID, int AccidentadoId )
        {
            if (SiniestroID < 0)
                throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            List<PreliquidacionDetalle> TheList = new List<PreliquidacionDetalle>();
            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                PreliquidacionDS.PreliquidacionDetalleDataTable theTable = theAdapter.GetPreliquidacionDetalleForCombo(SiniestroID, AccidentadoId);
                foreach (PreliquidacionDS.PreliquidacionDetalleRow row in theTable.Rows)
                {
                    PreliquidacionDetalle pre = FillRecordDetalle(row);
                    TheList.Add(pre);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PreliquidacionDetalle list data", ex);
                throw;
            }
            return TheList;
        }


        public static List<PreliquidacionDetalle> GetAllPreliquidacionDetalle ( int SiniestroID, int AccidentadoId )
        {
            return GetPreliquidacionDetalleByTipo(SiniestroID, AccidentadoId, "");
        }
        public static List<PreliquidacionDetalle> GetPreliquidacionDetalleByTipo ( int SiniestroID, int AccidentadoId, string tipo )
        {
            if (SiniestroID < 0)
                throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            List<PreliquidacionDetalle> TheList = new List<PreliquidacionDetalle>();
            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                PreliquidacionDS.PreliquidacionDetalleDataTable theTable = theAdapter.GetPreliquidacionDetalleBySiniestroIdAndAccidentadoId(SiniestroID, AccidentadoId, tipo);
                foreach (PreliquidacionDS.PreliquidacionDetalleRow row in theTable.Rows)
                {
                    TheList.Add(FillRecordDetalle(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PreliquidacionDetalle list data", ex);
                throw;
            }
            return TheList;
        }

        //public static void InsertPreliquidacion ( ref Preliquidacion Preliquidacion )
        //{
        //    Preliquidacion.PreliquidacionId = InsertPreliquidacion(Preliquidacion.SiniestroId, Preliquidacion.AccidentadoId
        //        , Preliquidacion.MontoGestion, Preliquidacion.Reservas, Preliquidacion.Hospitalarios, Preliquidacion.Cirujia
        //        , Preliquidacion.Ambulancias, Preliquidacion.Laboratorios, Preliquidacion.Farmacia, Preliquidacion.Honorarios
        //        , Preliquidacion.Ambulatorios, Preliquidacion.Osteosintesis);
        //}
        //public static int InsertPreliquidacion ( int SiniestroId, int AccidentadoId, decimal montoGestion, decimal reservas, decimal hospitalarios, 
        //    decimal cirujia, decimal ambulancias, decimal laboratorios, decimal farmacia, decimal honorarios, decimal ambulatorios, decimal osteosintesis )
        //{
        //    if (SiniestroId <= 0)
        //        throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
        //    if (AccidentadoId <= 0)
        //        throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

        //    try
        //    {
        //        PreliquidacionDSTableAdapters.PreliquidacionTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionTableAdapter();
        //        int? PreliquidacionId = 0;
        //        theAdapter.Insert(ref PreliquidacionId, SiniestroId, AccidentadoId, montoGestion, reservas, hospitalarios, cirujia,
        //            ambulancias, laboratorios, farmacia, honorarios, ambulatorios, osteosintesis);
        //        return (int)PreliquidacionId;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while inserting Preliquidacion", ex);
        //        throw;
        //    }
        //}

        public static int InsertPreliquidacionDetalle ( PreliquidacionDetalle pre )
        {
            return InsertPreliquidacionDetalle(pre.SiniestroId, pre.AccidentadoId, pre.Fecha, pre.Proveedor, pre.FechaReciboFactura, 
                pre.NumeroReciboFactura, pre.IsFactura, pre.Tipo, pre.Cobertura, pre.Monto, pre.Estado);
        }
        public static int InsertPreliquidacionDetalle ( int SiniestroId, int AccidentadoId, DateTime fecha,
            string proveedor, DateTime fechaReciboFactura, string numeroReciboFactura, bool isFactura, string tipo,
            decimal cobertura, decimal monto, bool estado)
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                int? PreliquidacionDetalleId = 0;
                theAdapter.Insert(SiniestroId, AccidentadoId, fecha, proveedor, fechaReciboFactura, numeroReciboFactura,
                    isFactura, tipo, cobertura, monto, estado, ref PreliquidacionDetalleId);
                return (int)PreliquidacionDetalleId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Preliquidacion", ex);
                throw;
            }
        }

        public static void UpdatePreliquidacionDetalle ( PreliquidacionDetalle pre )
        {
            UpdatePreliquidacionDetalle(pre.PreliquidacionDetalleId, pre.Fecha, pre.Proveedor, pre.FechaReciboFactura,
                pre.NumeroReciboFactura, pre.IsFactura, pre.Tipo, pre.Cobertura, pre.Monto, pre.Estado);
        }
        public static void UpdatePreliquidacionDetalle (int PreliquidacionId, DateTime fecha,
            string proveedor, DateTime fechaReciboFactura, string numeroReciboFactura, bool isFactura, string tipo,
            decimal cobertura, decimal monto, bool estado )
        {
            if (PreliquidacionId <= 0)
                throw new ArgumentException("PreliquidacionId cannot be less than or equal to zero.");

            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                theAdapter.Update(PreliquidacionId, fecha, proveedor, fechaReciboFactura, numeroReciboFactura,
                    isFactura, tipo, cobertura, monto, estado);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Preliquidacion", ex);
                throw;
            }
        }

        //public static void UpdatePreliquidacion ( Preliquidacion Preliquidacion )
        //{
        //    UpdatePreliquidacion(Preliquidacion.PreliquidacionId, Preliquidacion.SiniestroId
        //        , Preliquidacion.AccidentadoId, Preliquidacion.Reservas, Preliquidacion.Hospitalarios, Preliquidacion.Cirujia
        //        , Preliquidacion.Ambulancias, Preliquidacion.Laboratorios, Preliquidacion.Farmacia, Preliquidacion.Honorarios
        //        , Preliquidacion.Ambulatorios, Preliquidacion.Osteosintesis);
        //}
        //public static void UpdatePreliquidacion ( int PreliquidacionId, int SiniestroId, int AccidentadoId, decimal reservas, decimal hospitalarios, decimal cirujia, decimal ambulancias,
        //    decimal laboratorios, decimal farmacia, decimal honorarios, decimal ambulatorios, decimal osteosintesis )
        //{
        //    if (PreliquidacionId <= 0)
        //        throw new ArgumentException("PreliquidacionId cannot be less than or equal to zero.");
        //    if (SiniestroId <= 0)
        //        throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
        //    if (AccidentadoId <= 0)
        //        throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

        //    try
        //    {
        //        PreliquidacionDSTableAdapters.PreliquidacionTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionTableAdapter();
        //        theAdapter.Update(PreliquidacionId, SiniestroId, AccidentadoId, reservas, hospitalarios, cirujia,
        //            ambulancias, laboratorios, farmacia, honorarios, ambulatorios, osteosintesis);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while inserting Preliquidacion", ex);
        //        throw;
        //    }
        //}
        public static void DeletePreliquidacionDetalle ( int PreliquidacionId )
        {
            if (PreliquidacionId <= 0)
                throw new ArgumentException("PreliquidacionId cannot be less than or equal to zero.");

            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                theAdapter.Delete(PreliquidacionId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Preliquidacion", ex);
                throw;
            }
        }

        public static List<string> GetTags ( string search )
        {
            List<string> list = new List<string>();
            try
            {
                PreliquidacionDSTableAdapters.ProveedorMedicoTableAdapter theAdapter = new PreliquidacionDSTableAdapters.ProveedorMedicoTableAdapter();
                PreliquidacionDS.ProveedorMedicoDataTable theTable = theAdapter.SearchProveedores(search);
                foreach (PreliquidacionDS.ProveedorMedicoRow row in theTable.Rows)
                {
                    list.Add(row.Proveedor);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GestionMedica Tags data", ex);
                throw;
            }
            return list;
        }

        public static int SearchPreliquidacion ( ref List<PreliquidacionSearch> preliquidaciones, 
            int PageSize, int firstRow, int ClienteId, string where, string orderBy )
        {
            int? TotalRows = 0;

            try
            {
                PreliquidacionDSTableAdapters.SearchPreliquidacionesTableAdapter theAdapter = new PreliquidacionDSTableAdapters.SearchPreliquidacionesTableAdapter();
                PreliquidacionDS.SearchPreliquidacionesDataTable theTable =
                    theAdapter.SearchPreliquidaciones(PageSize, firstRow, ref TotalRows, ClienteId, where, orderBy);
                foreach (PreliquidacionDS.SearchPreliquidacionesRow row in theTable.Rows)
                {
                    PreliquidacionSearch preliquidacionObj = new PreliquidacionSearch();

                    preliquidacionObj.PreliquidacionDetalleId = row.PreliquidacionDetalleId;
                    preliquidacionObj.NumeroRoseta = row.NumeroRoseta;
                    preliquidacionObj.TipoGasto = row.TipoGasto;
                    preliquidacionObj.Paciente = row.Paciente;
                    preliquidacionObj.Proveedor = row.Proveedor;
                    preliquidacionObj.FechaAprobacion = row.FechaAprobacion;
                    preliquidacionObj.FechaEmision = row.FechaEmision;
                    preliquidacionObj.IsFactura = row.IsFactura;
                    preliquidacionObj.NumeroReciboFactura = row.NumeroReciboFactura;
                    preliquidacionObj.Monto = row.Monto;
                    preliquidacionObj.Estado = row.Estado;
                    preliquidacionObj.TieneFactura = row.TieneFactura;
                    preliquidacionObj.NumeroFactura = row.NumeroFactura;
                    preliquidacionObj.MontoFactura = row.MontoFactura;
                    preliquidacionObj.RowNumber = row.RowNumber;
                    preliquidacionObj.GastosEjecutadosId = row.GastosEjecutadosId;
                    preliquidacionObj.GastosEjecutadosDetalleId = row.GastosEjecutadosDetalleId;
                    preliquidacionObj.PagoGastosId = row.PagoGastosId;
                    preliquidacionObj.FechaPago = (!row.IsFechaPagoNull()) ? row.FechaPago : DateTime.MinValue;
                    preliquidacionObj.CantidadDias = (!row.IsCantidadDiasNull()) ? row.CantidadDias : 0;

                    preliquidaciones.Add(preliquidacionObj);
                    //preliquidaciones.Add(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PreliquidacionDetalle list data", ex);
                throw;
            }
            return TotalRows.Value;
        }

        public static int GetPreliquidacionDetalleIdByGastoesEjecutadosDetalleId(int GastosEjecutadosDetalleId)
        {
            int? PreliquidacionDetalleId = 0;
            try
            {
                PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter theAdapter = new PreliquidacionDSTableAdapters.PreliquidacionDetalleTableAdapter();
                theAdapter.GetPreliquidacionDetalleIdByGastoesEjecutadosDetalleId(GastosEjecutadosDetalleId, ref PreliquidacionDetalleId);

                return PreliquidacionDetalleId.Value;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PreliquidacionDetalle list data", ex);
                throw;
            }
        }
    }
}