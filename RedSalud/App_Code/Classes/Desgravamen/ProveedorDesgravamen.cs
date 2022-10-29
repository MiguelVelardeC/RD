using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ProveedorDesgravamenSearchResult
    /// </summary>
    public class ProveedorDesgravamen
    {
        public int ProveedorMedicoId { get; set; }
        public string ProveedorNombre { get; set; }
        public string CiudadId { get; set; }
        public string CiudadNombre { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int DuracionCita { get; set; }
        public bool Principal { get; set; }
        public int EstudiosHabilitados { get; set; }
        public string EstudiosHabilitadosDisplay
        {
            get
            {
                return (EstudiosHabilitados > 0) ? "SI" : "NO";
            }
        }

        public ProveedorDesgravamen()
        {
            //
            // TODO: Add constructor logic here
            //
        }


    }
}