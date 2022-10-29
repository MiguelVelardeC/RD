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
    public class EmergenciaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EmergenciaBLL () { }

        public static List<EmergenciaRepDS.EmergenciaRepRow> SearchEmergencia(string where, int ClienteId, int AseguradoId, int userId, DateTime FechaIni, DateTime FechaFin)
        {
            List<EmergenciaRepDS.EmergenciaRepRow> _cache = new List<EmergenciaRepDS.EmergenciaRepRow>();
            try
            {
                EmergenciaRepDSTableAdapters.EmergenciaRepTableAdapter theAdapter = new EmergenciaRepDSTableAdapters.EmergenciaRepTableAdapter();
                EmergenciaRepDS.EmergenciaRepDataTable theTable = theAdapter.GetEmergenciaRep(ClienteId, AseguradoId, userId, FechaIni, FechaFin, where);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EmergenciaRepDS.EmergenciaRepRow row in theTable.Rows)
                    {
                        _cache.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Emergencias", ex);
                throw;
            }
            return _cache;
        }
    }
}