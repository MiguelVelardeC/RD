using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Reportes
{
    /// <summary>
    /// Summary description for Odontologia
    /// </summary>
    public class Odontologia
    {
        public Odontologia()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string NombreCliente {get;set;}
        public string CodigoCaso {get;set;}
        public string Medico {get;set;}
        public DateTime FechaCreacion {get;set;}
        public string Ciudad {get;set;}
        public string Nombre {get;set;}
        public string Genero {get;set;}
        public DateTime FechaNacimiento {get;set;}
        public string NumeroPoliza {get;set;}
        public string NombrePlan {get;set;}
        public string Enfermedad {get;set;}
        public string Prestacion {get;set;}
        public string Pieza {get;set;}
        public string Observacion {get;set;}
        public int EdadCalculadaForDisplay
        {
            get
            {
                int Edad = 0;
                DateTime birthday = FechaNacimiento;

                if (birthday == null || birthday == DateTime.MinValue || birthday.Year < 1900)
                    return 0;

                Edad = DateTime.UtcNow.Year - birthday.Year;

                if (Edad > 0)
                {
                    Edad -= (DateTime.UtcNow.Date < birthday.Date.AddYears(Edad)) ? 1 : 0;
                }
                else
                {
                    Edad = 0;
                }

                return Edad;
            }
        }
    }
}