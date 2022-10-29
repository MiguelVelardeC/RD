using Artexacta.App.Desgravamen.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for DesgravamenManager
    /// </summary>
    public class DesgravamenManager
    {
        public DesgravamenManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string CheckOrdenDeServicioParaImprimir(int citaDesgravamenId, bool necesitaExamen)
        {
            StringBuilder resultado = new StringBuilder();
            resultado.Append("<ul>");

            bool panelVisible = false;

            ProgramacionCita objCita = PropuestoAseguradoBLL.GetProgramacionCita(citaDesgravamenId);
            if (necesitaExamen && (objCita == null || objCita.FechaHoraCita == DateTime.MinValue))
            {
                resultado.Append("<li>Debe marcar cita para la revisión médica con el enlace de Programar Cita</li>");
                panelVisible = true;
            }

            List<ProgramacionCitaLabo> labos = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(citaDesgravamenId);
            foreach (ProgramacionCitaLabo objLabo in labos)
            {
                if (objLabo.NecesitaCita && objLabo.FechaCita == DateTime.MinValue)
                {
                    resultado.Append("<li>Debe marcar cita para " + objLabo.EstudioNombre + " en "+ objLabo.ProveedorNombre + "</li>");
                    panelVisible = true;
                }
                if (!objLabo.NecesitaCita && objLabo.FechaCita == DateTime.MinValue)
                {
                    resultado.Append("<li>Debe marcar una cita, solamente fecha, para " + objLabo.EstudioNombre + " en " + objLabo.ProveedorNombre + "</li>");
                    panelVisible = true;
                }
            }
            resultado.Append("</ul>");
            if (panelVisible)
                return resultado.ToString();
            else
                return "";
        }
    }
}