using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for PropuestoAsegurado
    /// </summary>
    public class PropuestoAsegurado
    {
        public int PropuestoAseguradoId { get; set; }
        public string NombreCompleto { get; set; }
        public string CarnetIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoCelular { get; set; }
        public int FotoId { get; set; }
        public string Genero { get; set; }
        public int ClienteId { get; set; }
        public string FotoUrl_Ajax { get; set; }
        public string FechaNacimientoLong { get; set; }
        public string NombreCompletoForDisplay
        {
            get
            {
                return "(" + PropuestoAseguradoId + ") " + NombreCompleto + " / " + String.Format("{0:dd/MMM/yyyy}",FechaNacimiento);
            }
        }

        public string FechaNacimientoForDisplay
        {
            get
            {
                return FechaNacimiento.ToLongDateString();
            }
        }
        public string FotoUrl
        {
            get
            {
                if (FotoId == 0)
                {
                    return "~/Images/Neutral/paciente.jpg";
                }
                return "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";
            }
        }

        public string FotoUrlSmall
        {
            get
            {
                if (FotoId == 0)
                {
                    return "~/Images/Neutral/paciente.jpg";
                }
                return "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=32&H=32";
            }
        }

        public string GeneroForDisplay
        {
            get
            {
                if (Genero == "M") return "Masculino";
                if (Genero == "F") return "Femenino";
                return "Dato Faltante";
            }
        }

        public PropuestoAsegurado()
        {
            
        }
    }
}