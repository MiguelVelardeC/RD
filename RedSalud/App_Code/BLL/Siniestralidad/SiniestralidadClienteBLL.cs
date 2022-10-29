using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Artexacta.App.User.BLL;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Siniestralidad;
/// <summary>
/// Summary description for SiniestralidadBLL
/// </summary>
namespace Artexacta.App.Siniestralidad.BLL
{
    public class SiniestralidadClienteBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        //
        // TODO: Add constructor logic here
        //
        private static SiniestralidadCliente FillRecord(SiniestralidadClienteDS.SiniestralidadClienteRow row)
        {
            SiniestralidadCliente objSiniestralidad = new SiniestralidadCliente(
                row.ClienteId
                ,row.CodigoCliente
                ,row.NombreJuridico
                ,row.Nit.ToString()
                ,row.Direccion
                ,row.RowNumber
                );
            return objSiniestralidad;
        }
        private static SiniestralidadClienteDetail FillRecord(SiniestralidadClienteDS.SiniestralidadClienteDetailRow row)
        {
            SiniestralidadClienteDetail objSiniestralidadDetail = new SiniestralidadClienteDetail(
            row.Prestacion
            ,row.MontoTope
            ,row.MontoAcumulado
            ,row.MontoCoPago
            ,row.ConsultasAno
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
        
        public static int GetReporteSiniestralidadALL(List<SiniestralidadCliente> _cache, string codigo, string nombre, int pageSize, int firstRow)
        {

            int? intTotalRows = 0;
            try
            {

                SiniestralidadClienteDSTableAdapters.SiniestralidadClienteTableAdapter theAdapter = new SiniestralidadClienteDSTableAdapters.SiniestralidadClienteTableAdapter();

                SiniestralidadClienteDS.SiniestralidadClienteDataTable theTable = theAdapter.GetAllSiniestralidad(codigo, nombre, pageSize, firstRow, ref intTotalRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestralidadClienteDS.SiniestralidadClienteRow row in theTable.Rows)
                    {
                        SiniestralidadCliente ListaSiniestralidad = FillRecord(row);
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

        public static int GetReporteSiniestralidadDetail(List<SiniestralidadClienteDetail> _cache, int IdCliente , string fechaIni, string fechaFin)
        {
            int? intTotalRows = 0;
            try
            {

                SiniestralidadClienteDSTableAdapters.SiniestralidadClienteDetailTableAdapter theAdapter = new SiniestralidadClienteDSTableAdapters.SiniestralidadClienteDetailTableAdapter();

                SiniestralidadClienteDS.SiniestralidadClienteDetailDataTable theTable = theAdapter.GETSiniestralidadDetail(IdCliente, fechaIni, fechaFin);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestralidadClienteDS.SiniestralidadClienteDetailRow row in theTable.Rows)
                    {
                        SiniestralidadClienteDetail ListaSiniestralidadDetail = FillRecord(row);
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
    }
}