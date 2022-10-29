using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User.BLL;
using log4net;
using Artexacta.App.LoginSecurity;
using System.Data.SqlTypes;

namespace Artexacta.App.Reportes.BLL
{
    /// <summary>
    /// Summary description for CasoBLL
    /// </summary>
    public class EnfermeriaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EnfermeriaBLL () { }

        private static Enfermeria FillRecord ( EnfermeriaDS.EnfermeriaRow row )
        {
            Enfermeria objCaso = new Enfermeria(
                  row.IsNombreClienteNull() ? "" : row.NombreCliente
                , row.IsCodigoCasoNull() ? "" : row.CodigoCaso
                , row.IsMedicoNull() ? "" : row.Medico
                , row.IsTipoConsultaNull() ? "" : row.TipoConsulta
                , row.IsMotivoConsultaNull() ? "" : row.MotivoConsulta
                , row.IsObservacionesNull() ? "" : row.Observaciones
                , row.IsTratamientoNull() ? "" : row.Tratamiento
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.IsEnfermedad2Null() ? "" : row.Enfermedad2
                , row.IsEnfermedad3Null() ? "" : row.Enfermedad3
                , row.IsDiagnosticoPresuntivoNull() ? "" : row.DiagnosticoPresuntivo
                , row.IsFechaCasoNull() ? SqlDateTime.MinValue.Value : row.FechaCaso
                , row.IsCiudadNull() ? "" : row.Ciudad
                , row.IsNombreNull() ? "" : row.Nombre
                , row.IsFechaNacimientoNull() ? SqlDateTime.MinValue.Value : row.FechaNacimiento
                , row.IsGeneroNull() ? "" : row.Genero
                , row.IsCasoCriticoNull() ? "" : row.CasoCritico
                , row.IsNumeroPolizaNull() ? "" : row.NumeroPoliza
                , row.IsNombrePlanNull() ? "" : row.NombrePlan
                , row.IsMedicamentoNull() ? "" : row.Medicamento
                , row.Presentacion
                , row.IsIndicacionesNull() ? "" : row.Indicaciones
                , row.IsTipoDocumentoNull() ? "" : row.TipoDocumento
                , row.NroFacturaRecibo.ToString()
                , row.IsFechaCreacionNull() ? SqlDateTime.MinValue.Value : row.FechaCreacion
                , row.IsFechaGastoNull() ? SqlDateTime.MinValue.Value : row.FechaGasto
                , row.IsMontoNull() ? 0 : row.Monto
                , row.RowNumber
                );
            return objCaso;
        }

        public static List<Enfermeria> SearchEnfermeria ( string where, int ClienteId, int AseguradoId, DateTime FechaIni, DateTime FechaFin, int UserId )
        {
            List<Enfermeria> _cache = new List<Enfermeria>();
            try
            {
                EnfermeriaDSTableAdapters.EnfermeriaTableAdapter theAdapter = new EnfermeriaDSTableAdapters.EnfermeriaTableAdapter();
                EnfermeriaDS.EnfermeriaDataTable theTable = theAdapter.GetEnfermeria(ClienteId, AseguradoId, UserId, FechaIni, FechaFin, where);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EnfermeriaDS.EnfermeriaRow row in theTable.Rows)
                    {
                        Enfermeria theCaso = FillRecord(row);
                        _cache.Add(theCaso);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Caso", ex);
                throw;
            }
            return _cache;
        }
    }
}