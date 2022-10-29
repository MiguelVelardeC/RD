using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Seguimiento.BLL
{
    public class SeguimientoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public SeguimientoBLL () { }


        private static Seguimiento FillRecord(SeguimientoDS.SeguimientoRow row)
        {
            DateTime FechaMax = DateTime.MaxValue;
            Seguimiento objSeguimiento = new
                Seguimiento(
                    row.SiniestroId
                    ,row.Estado
                    ,row.Acuerdo
                    ,row.Rechazado
                    ,row.Observaciones
                );

            return objSeguimiento;
        }

        public static Seguimiento GetSeguimientoByID ( int SiniestroId )
        {
            if (SiniestroId < 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            Seguimiento TheSeguimiento = null;
            try
            {
                SeguimientoDSTableAdapters.SeguimientoTableAdapter theAdapter = new SeguimientoDSTableAdapters.SeguimientoTableAdapter();
                SeguimientoDS.SeguimientoDataTable theTable = theAdapter.GetSeguimientoById(SiniestroId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheSeguimiento = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Seguimiento data", ex);
                throw;
            }
            return TheSeguimiento;
        }

        public static bool IsAprobado ( int SiniestroId, int AccidentadoId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            try
            {
                SeguimientoDSTableAdapters.SeguimientoTableAdapter theAdapter = new SeguimientoDSTableAdapters.SeguimientoTableAdapter();
                return (bool)theAdapter.IsAprobado(SiniestroId, AccidentadoId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while validating if Is Aprobado", ex);
                throw;
            }
        }

        public static string GetEstado ( int SiniestroId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            try
            {
                SeguimientoDSTableAdapters.SeguimientoTableAdapter theAdapter = new SeguimientoDSTableAdapters.SeguimientoTableAdapter();
                object estado = theAdapter.GetEstado(SiniestroId);
                return estado == null ? "EN CURSO" : estado.ToString();
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while validating if Is Aprobado", ex);
                return "EN CURSO";
            }
        }
        public static bool IsTerminado ( int SiniestroId, int AccidentadoId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            try
            {
                SeguimientoDSTableAdapters.SeguimientoTableAdapter theAdapter = new SeguimientoDSTableAdapters.SeguimientoTableAdapter();
                return (bool)theAdapter.IsTerminado(SiniestroId, AccidentadoId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while validating if Is Terminado", ex);
                throw;
            }
        }
        public static void UpdateEstado ( int SiniestroId, string Estado )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (string.IsNullOrWhiteSpace(Estado))
                throw new ArgumentException("Estado cannot be empty.");

            try
            {
                SeguimientoDSTableAdapters.SeguimientoTableAdapter theAdapter = new SeguimientoDSTableAdapters.SeguimientoTableAdapter();
                theAdapter.UpdateSiniestroEstado(SiniestroId, Estado);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating Estado", ex);
                throw;
            }
        }
    }
}