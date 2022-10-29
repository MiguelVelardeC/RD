using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.CitasVideoLLamada
{
    /// <summary>
    /// Summary description for Caso
    /// </summary>
    public class CitaVideoLLamada
    {
        public int citId { get; set; }
        public string Medico { get; set; }
        public string NroPoliza { get; set; }
        public string Asegurado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Cliente { get; set; }
        public string Ciudad { get; set; }

        public CitaVideoLLamada()
        {
        }
	}
}