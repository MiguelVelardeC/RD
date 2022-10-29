using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ProveedorEstudios
    /// </summary>
    public class ProveedorEstudios
    {
        public int ClienteId { get; set; }
        public int ProveedorMedicoId { get; set; }
        public int EstudioId { get; set; }
        public string EstudioNombre { get; set; }
        public bool NecesitaCita { get; set; }
        public string NecesitaCitaEstado { get; set; }
        public bool Deshabilitado { get; set; }
        public string DeshabilitadoEstado { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string HoraInicioDisplay { get; set; }
        public string HoraFinDisplay { get; set; }

        public ProveedorEstudios()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}