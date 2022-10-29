using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for CitaDesgravamen
    /// </summary>
    public class CitaDesgravamen
    {
        public int CitaDesgravamenId { get; set; }
        public int PropuestoAseguradoId { get; set; }
        public int FinancieraId { get; set; }
        public string CiudadId { get; set; }
        public bool NecesitaExamen { get; set; }
        public bool NecesitaLaboratorio { get; set; }
        public bool CobroFinanciera { get; set; }
        public string Referencia { get; set; }
        public string TipoProducto { get; set; }
        public int EjecutivoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string CobroFinancieraForDisplay
        {
            get { return CobroFinanciera ? "Si" : "No"; }
        }
        public string CobroAseguradoForDisplay
        {
            get { return CobroFinanciera ? "No" : "Si"; }
        }

        public string TipoProductoForDisplay
        {
            get
            {
                if (TipoProducto == "DESGRAVAMEN")
                    return "Desgravamen";
                if (TipoProducto == "INDIVIDUAL")
                    return "Vida Individual";
                return "Error";
            }
        }

        public CitaDesgravamen()
        {
            
        }
    }
}