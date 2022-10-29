using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Cita
{
    /// <summary>
    /// Summary description for Caso
    /// </summary>
    public class Cita
    {
        public int CitaId { get; set; }
        public int MedicoId { get; set; }
        public int ProveedorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int PacienteId { get; set; }

        public Cita()
        {
        }
	}
}