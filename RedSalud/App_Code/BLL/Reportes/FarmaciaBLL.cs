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
    public class FarmaciaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public FarmaciaBLL () { }

        private static Farmacia FillRecord ( FarmaciaDS.FarmaciaRow row )
        {
            Farmacia objCaso = new Farmacia(
                  row.IsNombreClienteNull() ? "" : row.NombreCliente
                , row.IsCodigoCasoNull() ? "" : row.CodigoCaso
                , row.IsMedicoNull() ? "" : row.Medico
                , row.IsTipoConsultaNull() ? "" : row.TipoConsulta
                , row.IsFechaCasoNull() ? SqlDateTime.MinValue.Value : row.FechaCaso
                , row.IsCiudadNull() ? "" : row.Ciudad
                , row.IsNombreNull() ? "" : row.Nombre
                , row.IsFechaNacimientoNull() ? SqlDateTime.MinValue.Value : row.FechaNacimiento
                , row.IsGeneroNull() ? "" : row.Genero
                , row.IsCasoCriticoNull() ? "" : row.CasoCritico
                , row.IsNumeroPolizaNull() ? "" : row.NumeroPoliza
                , row.IsNombrePlanNull() ? "" : row.NombrePlan
                , row.IsMedicamentoNull() ? "" : row.Medicamento
                , row.IsIsLinameNull() ? false : row.IsLiname
                , row.Presentacion
                , row.Concentracion
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

        public static List<Farmacia> SearchFarmacia ( string where, int ClienteId, int AseguradoId, int UserId, DateTime fechaIni, DateTime Fechafin )
        {
            List<Farmacia> _cache = new List<Farmacia>();
            try
            {
                FarmaciaDSTableAdapters.FarmaciaTableAdapter theAdapter = new FarmaciaDSTableAdapters.FarmaciaTableAdapter();
                FarmaciaDS.FarmaciaDataTable theTable = theAdapter.GetFarmacia(ClienteId, AseguradoId, UserId, fechaIni, Fechafin, where);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (FarmaciaDS.FarmaciaRow row in theTable.Rows)
                    {
                        Farmacia theCaso = FillRecord(row);
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