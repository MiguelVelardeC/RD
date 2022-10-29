using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Artexacta.App.User.BLL;
using Artexacta.App.LoginSecurity;
/// <summary>
/// Summary description for SiniestralidadBLL
/// </summary>
namespace Artexacta.App.Siniestralidad.BLL
{
    public class SiniestralidadBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        //
        // TODO: Add constructor logic here
        //
        private static Siniestralidad FillRecord(SiniestralidadDS.SiniestralidadRow row)
        {
            Siniestralidad objSiniestralidad = new Siniestralidad(

                row.PolizaId
                ,row.NombrePaciente
                ,row.ClienteId
                ,row.NombreCliente
                ,row.NumeroPoliza
                ,row.CarnetIdentidad
                ,row.Relacion
                ,row.FechaInicio
                ,row.FechaFin
                ,row.RowNumber
                );
            return objSiniestralidad;
        }
        private static SiniestralidadDetail FillRecord(SiniestralidadDS.SiniestralidadDetailRow row)
        {
            SiniestralidadDetail objSiniestralidadDetail = new SiniestralidadDetail(
            row.NombrePrestacion
            ,row.MontoTope
           // ,row.ValorCoPago
            ,row.ConsultasPorAnos
            ,row.MontoAcumulado
            ,row.CoPagoAcumulado
            ,row.ConsultasAcumuladas    
            );
            return objSiniestralidadDetail;
        }
        private static SiniestralidadSearchPolizaDetail FillRecord(SiniestralidadDS.SiniestralidadSearchPolizaDetailRow row)
        {
            SiniestralidadSearchPolizaDetail objSiniestralidadPolizaxClienteDetail = new SiniestralidadSearchPolizaDetail(
            row.Nombre
            ,row.CedulaIdentidad
            ,row.NumeroPoliza
            ,row.NombrePlan
            );
            return objSiniestralidadPolizaxClienteDetail;
        }
        
        public static int GetReporteSiniestralidadALL(List<Siniestralidad> _cache, int ClienteId, string FechaIni, string FechaFin, string TipoBusqueda, string CiudadId, string nombre, string carnet, int pageSize, int firstRow)
        {

            int? intTotalRows = 0;
            try
            {

                SiniestralidadDSTableAdapters.SiniestralidadTableAdapter theAdapter = new SiniestralidadDSTableAdapters.SiniestralidadTableAdapter();

                SiniestralidadDS.SiniestralidadDataTable theTable = theAdapter.GetAllSiniestralidad(ClienteId, CiudadId, FechaIni, FechaFin, nombre, carnet, TipoBusqueda, pageSize, firstRow, ref intTotalRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestralidadDS.SiniestralidadRow row in theTable.Rows)
                    {
                        Siniestralidad ListaSiniestralidad = FillRecord(row);
                        _cache.Add(ListaSiniestralidad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Report Siniestralidad", ex);
                throw;
            }
            return (int)intTotalRows;
        }
        //public static List<Siniestralidad> SearchSiniestralidadXmanagement(int ClienteId, string ciudadid, int Gestion, string nombre, int pageSize, int firstRow)
        //{
        //    List<Siniestralidad> _cache = new List<Siniestralidad>();
        //    int? intTotalRows = 0;
        //    try
        //    {
        //        SiniestralidadDSTableAdapters.SiniestralidadTableAdapter theAdapter = new SiniestralidadDSTableAdapters.SiniestralidadTableAdapter();

        //        SiniestralidadDS.SiniestralidadDataTable theTable = theAdapter.GetAllSiniestralidad(ClienteId, ciudadid, Gestion, nombre,pageSize,firstRow,ref intTotalRows);
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        {
        //            foreach (SiniestralidadDS.SiniestralidadRow row in theTable.Rows)
        //            {
        //                Siniestralidad ListaSiniestralidad = FillRecord(row);
        //                _cache.Add(ListaSiniestralidad);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting Report Siniestralidad", ex);
        //        throw;
        //    }
        //    return _cache;
        //}

        public static SiniestralidadSearchPolizaDetail SearchSiniestralidadPolizaXCliente(int PolizaId, int IdCliente)
        {
            if (PolizaId <= 0)
                throw new ArgumentException("PolizaId no puede ser menor o igual a cero.");

            SiniestralidadSearchPolizaDetail objSiniestralidadxPolizaxCliente = null;
         
            try
            {
                SiniestralidadDSTableAdapters.SiniestralidadSearchPolizaDetailTableAdapter theAdapter = new SiniestralidadDSTableAdapters.SiniestralidadSearchPolizaDetailTableAdapter();

                SiniestralidadDS.SiniestralidadSearchPolizaDetailDataTable theTable = theAdapter.GetSiniestralidadSearchPolizaxClienteDetail(PolizaId, IdCliente);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestralidadDS.SiniestralidadSearchPolizaDetailRow row in theTable.Rows)
                    {
                        objSiniestralidadxPolizaxCliente = FillRecord(theTable[0]);
                      
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Report Siniestralidad", ex);
                throw;
            }
        return objSiniestralidadxPolizaxCliente;

        }
        public static int GetReporteSiniestralidadDetail(List<SiniestralidadDetail> _cache, int IdCliente ,int polizaId,string fechaIni, string fechaFin)
        {
            int? intTotalRows = 0;
            try
            {

                SiniestralidadDSTableAdapters.SiniestralidadDetailTableAdapter theAdapter = new SiniestralidadDSTableAdapters.SiniestralidadDetailTableAdapter();

                SiniestralidadDS.SiniestralidadDetailDataTable theTable = theAdapter.GETSiniestralidadDetail(IdCliente,polizaId,fechaIni,fechaFin,0);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestralidadDS.SiniestralidadDetailRow row in theTable.Rows)
                    {
                        SiniestralidadDetail ListaSiniestralidadDetail = FillRecord(row);
                        _cache.Add(ListaSiniestralidadDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Report Siniestralidad", ex);
                throw;
            }
            return (int)intTotalRows;
        }

        private static SiniestralidadDetail FillRecord(SiniestralidadDS.SiniestralidadDetailGetDateRow row)
        {
            SiniestralidadDetail objSiniestralidadDetail = new SiniestralidadDetail(
            row.NombrePrestacion
            , row.MontoTope
            , row.ConsultasPorAnos
            , row.MontoAcumulado
            , row.CoPagoAcumulado
            , row.ConsultasAcumuladas
            );
            return objSiniestralidadDetail;
        }

        public static int GetReporteSiniestralidadDetailGetDate(List<SiniestralidadDetail> _cache,int clienteid, int polizaid)
        {
            int? intTotalRows = 0;
            try
            {

                SiniestralidadDSTableAdapters.SiniestralidadDetailGetDateTableAdapter theAdapter = new SiniestralidadDSTableAdapters.SiniestralidadDetailGetDateTableAdapter();

                SiniestralidadDS.SiniestralidadDetailGetDateDataTable theTable = theAdapter.SiniestralidadDetailGetDate(clienteid,polizaid,null);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestralidadDS.SiniestralidadDetailGetDateRow row in theTable.Rows)
                    {
                        SiniestralidadDetail ListaSiniestralidadDetail = FillRecord(row);
                        _cache.Add(ListaSiniestralidadDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Report Siniestralidad", ex);
                throw;
            }
            return _cache.Count;
        }
    }
}