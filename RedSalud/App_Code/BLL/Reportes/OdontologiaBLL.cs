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
    public class OdontologiaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public OdontologiaBLL () { }

        public static List<Odontologia> SearchOdontologia(string where, int ClienteId, int AseguradoId, int userId, DateTime FechaIni, DateTime FechaFin)
        {
            List<Odontologia> _cache = new List<Odontologia>();
            try
            {
                OdontologiaRepDSTableAdapters.OdontologiaRepTableAdapter theAdapter = new OdontologiaRepDSTableAdapters.OdontologiaRepTableAdapter();
                OdontologiaRepDS.OdontologiaRepDataTable theTable = theAdapter.GetOdontologiaRep(ClienteId, AseguradoId, userId, FechaIni, FechaFin, where);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (OdontologiaRepDS.OdontologiaRepRow row in theTable.Rows)
                    {
                        Odontologia odontologia = new Odontologia();
                        odontologia.NombreCliente = row.NombreCliente;
                        odontologia.CodigoCaso = row.CodigoCaso;
                        odontologia.Medico = row.Medico;
                        odontologia.FechaCreacion = row.FechaCreacion;
                        odontologia.Ciudad = row.Ciudad;
                        odontologia.Nombre = row.Nombre;
                        odontologia.Genero = row.Genero;
                        odontologia.FechaNacimiento = row.FechaNacimiento;
                        odontologia.NumeroPoliza = row.NumeroPoliza;
                        odontologia.NombrePlan = row.NombrePlan;
                        odontologia.Enfermedad = row.Enfermedad;
                        odontologia.Prestacion = row.Prestacion;
                        odontologia.Pieza = row.Pieza;
                        odontologia.Observacion = row.Observacion;

                        _cache.Add(odontologia);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Odontologia", ex);
                throw;
            }
            return _cache;
        }
    }
}