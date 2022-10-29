using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ReporteEstadoEstudios
    /// </summary>
    public class ReporteEstadoEstudios
    {
        public ReporteEstadoEstudios()
        {
        }

        public int CitaDesgravamenId { get; set; }
        public string CiudadId { get; set; }
        public string CiudadNombre { get; set; }
        public int ProveedorMedicoId { get; set; }
        public string ProveedorMedicoNombre { get; set; }
        public string NombreCompleto { get; set; }
        public string CarnetIdentidad { get; set; }
        public int FinancieraId { get; set; }
        public string FinancieraNombre { get; set; }
        public string TipoId { get; set; }
        public string TipoProductoDescripcion { get; set; }
        public int EstudioId { get; set; }
        public string EstudioNombre { get; set; }
        public string FechaCita { get; set; }
        public string FechaAtencion { get; set; }
        public string FechaRealizado { get; set; }
        public bool Aprobado { get; set; }
        public string EstadoAprobado { get; set; }
        public bool Realizado { get; set; }
        public string EstadoRealizado { get; set; }
    }
}