using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.SOATEstadisticas.BLL
{
    public class SOATEstadisticasBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public SOATEstadisticasBLL() {}

        public static void GetSOATEstadisticasTotales ( int clienteId, int gestion, ref int siniestros, ref int accidentados, ref int fallecidos )
        {
            if (gestion < 0)
                throw new ArgumentException("Gestion cannot be less than or equal to zero.");

            try
            {
                EstadisticasDSTableAdapters.SOATEstadisticasTotalesTableAdapter adapter =
                    new EstadisticasDSTableAdapters.SOATEstadisticasTotalesTableAdapter();
                EstadisticasDS.SOATEstadisticasTotalesDataTable theTable = adapter.GetSOATEstadisticasTotales(gestion, clienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    siniestros = theTable[0].Siniestros;
                    accidentados = theTable[0].Accidentados;
                    fallecidos = theTable[0].Fallecidos;
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while GetSOATEstadisticasTotales", ex);
                throw;
            }
        }
    }
}