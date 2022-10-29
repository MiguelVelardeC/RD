using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for Estudio
    /// </summary>
    public class Estudio
    {
        public int EstudioId { get; set; }
        public string NombreEstudio { get; set; }
        public string EstudioGrupoIds { get; set; }
        public bool Realizado { get; set; }
        public int ProveedorMedicoId { get; set; }
        public string NombreProveedor { get; set; }
        public DateTime FechaCita { get; set; }
        public bool NecesitaCita { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public int ParentEstudioId { get; set; }

        public string EstudioGrupoIdsForDisplay
        {
            get { return string.IsNullOrEmpty(EstudioGrupoIds) ? "" : "e" + EstudioGrupoIds.Replace(",", " e"); }
        }

        public string RealizadoForDisplay
        {
            get { return Realizado ? "Si" : "No"; }
        }

        public string FechaCitaForDisplay
        {
            get {
                if (FechaCita == DateTime.MinValue && !NecesitaCita && FechaHoraCita != DateTime.MinValue)
                    return string.Format("{0:dd/MMM/yyyy}", FechaHoraCita);
                if (FechaCita != DateTime.MinValue && NecesitaCita)
                    return string.Format("{0:dd/MMM/yyyy HH:mm}", FechaCita);
                if (FechaCita != DateTime.MinValue && !NecesitaCita)
                    return string.Format("{0:dd/MMM/yyyy}", FechaCita);
                return "-";
            }
        }

        public Estudio()
        {

        }
    }
}