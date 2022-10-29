using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.Especialidad
{
    /// <summary>
    /// Summary description for Especialidad
    /// </summary>
    public class Especialidad
    {
        public int EspecialidadId { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int TiempoAtencion { get; set; }
        public Especialidad(int especialidadId, string nombre, bool estado, int tiempoAten)
        {
            this.EspecialidadId = especialidadId;
            this.Nombre = nombre;
            this.Estado = estado;
            this.TiempoAtencion = tiempoAten;
        }
        public string NombreForDisplay { get { return TextUtilities.MakeForDisplay(EspecialidadId, Nombre); } }
        public string EstadoForDisplay
        {
            get
            {

                if (Estado)
                {
                    return "Activo";
                }
                else
                {
                    return "Inactivo";
                }
            }
        }
    }
}