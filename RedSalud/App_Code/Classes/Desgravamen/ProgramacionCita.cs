using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ProgramacionCita
    /// </summary>
    public class ProgramacionCita
    {

        public int CitaDesgravamen { get; set; }
        public int ProveedorMedicoId { get; set; }
        public string NombreProveedor { get; set; }
        public int MedicoId { get; set; }
        public string NombreMedico { get; set; }
        public string Estado { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int Pasada { get; set; }
        public bool Aprobado { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public int FinancieraId { get; set; }
        public string FinancieraNombre { get; set; }
        public int EjecutivoId { get; set; }
        public string EjecutivoNombre { get; set; }
        public bool Atendida
        {
            get
            {
                return Estado == "ATENDIDA";
            }
        }

        public string FechaHoraCitaForDisplay
        {
            get
            {
                return String.Format("{0:dd/MMM/yyyy HH:mm}", FechaHoraCita);
            }
        }

        public ProgramacionCita()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}