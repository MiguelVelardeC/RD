using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Caso
{
    /// <summary>
    /// Summary description for Derivacion
    /// </summary>
    public class PrintInfo
    {
        //Paciente
        public string NombrePaciente {get;set;}
        public string NumeroPoliza { get; set; }
        public string NombrePoliza { get; set; }
        public string Telefono { get; set; }
        public string CarnetIdentidad { get; set; }

        //Proveedor
        public string NombreProveedor { get; set; }

        //Medico
        public string MedicoNombre { get; set; }
        public string Especialidad { get; set; }

        public string otros { get; set; }

        public PrintInfo ( string NombrePaciente, string NumeroPoliza, string NombrePoliza, string Telefono, string CarnetIdentidad,
            string nombreProveedor, string MedicoNombre, string Especialidad )
        {
            this.NombrePaciente = NombrePaciente;
            this.NumeroPoliza = NumeroPoliza;
            this.NombrePoliza = NombrePoliza;
            this.Telefono = Telefono;
            this.CarnetIdentidad = CarnetIdentidad;

            this.NombreProveedor = nombreProveedor;

            this.MedicoNombre = MedicoNombre;
            this.Especialidad = Especialidad;
        }
    }
}