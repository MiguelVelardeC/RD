using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Reportes
{
    /// <summary>
    /// Summary description for CasoMedicoAtomicoSearch
    /// </summary>
    public class CasoMedicoAtomicoSearch
    {
        public CasoMedicoAtomicoSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string NombreCliente { get; set; }
        public string CodigoCaso { get; set; }
        public string Ciudad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string MotivoConsultaId { get; set; }
        public string MotivoConsultaDesc
        {
            get
            {
                string result = "OTRO";
                if (MotivoConsultaId == "ACCID")
                {
                    if (CasoIdDerivacion)
                    {
                        result = "ESPECIALISTA";
                    }
                    else
                    {
                        result = "MEDICO GENERAL";
                    }
                }

                if (MotivoConsultaId == "RECASO")
                    result = "RECONSULTA";

                if (MotivoConsultaId == "ENFER")
                    result = "ENFERMERIA";

                if (MotivoConsultaId == "EMERG")
                    result = "EMERGENCIA";

                if (MotivoConsultaId == "ODONTO")
                    result = "ODONTOLOGIA";

                return result;
            }
        }
        public string Medico { get; set; }
        public string NombrePaciente { get; set; }
        public string CodigoAsegurado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string NumeroPoliza { get; set; }
        public string NombrePlan { get; set; }
        public string MotivoConsulta { get; set; }
        public string EnfermedadActual { get; set; }
        public string PresionArterial { get; set; }
        public string Pulso { get; set; }
        public string Temperatura { get; set; }
        public string FrecuenciaCardiaca { get; set; }

        public string PulsoForDisplay
        {
            get
            {
                string result = Pulso;

                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                    result = "-";
                return result;
            }
        }

        public string TemperaturaForDisplay
        {
            get
            {
                string result = Temperatura;

                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                    result = "-";
                return result;
            }
        }

        public string FrecuenciaCardiacaForDisplay
        {
            get
            {
                string result = FrecuenciaCardiaca;

                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                    result = "-";
                return result;
            }
        }

        public string PresionArterialForDisplay
        {
            get
            {
                string result = PresionArterial;

                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                    result = "-";
                return result;
            }
        }

        public string Talla { get; set; }        
        public int EstaturaCm { get; set; }

        public string EstaturaCmForDisplay
        {
            get
            {
                double estaturaDouble = 0;
                double EstaturaMetro = 0;
                string result  = "";
                if (EstaturaCm != null && EstaturaCm > 0)
                {
                    estaturaDouble = ((double)EstaturaCm / 100);
                    EstaturaMetro = Math.Round(estaturaDouble, 2);
                    result = EstaturaMetro.ToString();
                }
                else
                {
                    result = "-";
                }   
                
                return result;
            }
        }

        public double Peso { get; set; }
        public string Enfermedad { get; set; }
        public string EnfermedadForDisplay
        {
            get
            {                
                return (!string.IsNullOrEmpty(Enfermedad))? Enfermedad: "-";
            }
        }
        public string Enfermedad2 { get; set; }
        public string Enfermedad2ForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(Enfermedad2)) ? Enfermedad2 : "-";
            }
        }
        public string Enfermedad3 { get; set; }
        public string Enfermedad3ForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(Enfermedad3)) ? Enfermedad3 : "-";
            }
        }
        public string DiagnosticoPresuntivo { get; set; }
        public string DiagnosticoPresuntivoForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(DiagnosticoPresuntivo)) ? DiagnosticoPresuntivo : "-";
            }
        }
        public string Observaciones { get; set; }
        public double IMC
        {
            get
            {
                if (Edad < 18)
                    return 0.00;

                if (Peso > 0 && EstaturaCm > 30)
                    return Math.Round((Peso / ((double) (EstaturaCm / 100) * (double) (EstaturaCm / 100))),2);

                return 0.00;
            }
        }
        public string IMCDescription
        {
            get
            {
                string result = "-";

                if (Edad < 18 || IMC <= 0)
                    return result;

                double IMC_Kg = IMC;

                if (IMC_Kg <= 0)
                    result = "-";

                if(IMC_Kg < 16.00)
                    result = "Infrapeso: Delgadez Severa";

                if(IMC_Kg >= 16.00 && IMC_Kg <= 16.99)
                    result = "Infrapeso: Delgadez moderada";

                if(IMC_Kg >= 17.00 && IMC_Kg <= 18.49)
                    result = "Infrapeso: Delgadez aceptable";

                if(IMC_Kg >= 18.50 && IMC_Kg <= 24.99)
                    result = "Peso Normal";

                if(IMC_Kg >= 25.00 && IMC_Kg <= 29.99)
                    result = "Sobrepeso";

                if(IMC_Kg >= 30.00 && IMC_Kg <= 34.99)
                    result = "Obeso: Tipo I";

                if(IMC_Kg >= 35.00 && IMC_Kg <= 40.00)
                    result = "Obeso: Tipo II";

                if(IMC_Kg > 40.00)
                    result = "Obeso: Tipo III";

                return result;
            }
        }

        public bool CasoIdDerivacion { get; set; }

        public string CasoIdDerivacionForDisplay
        {
            get
            {
                return (CasoIdDerivacion)? "SI" : "NO";
            }
        }

        public string TipoEstudio { get; set; }

        public string TipoEstudioForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(TipoEstudio))? TipoEstudio: "-";
            }
        }

        public string ProveedorEstudio { get; set; }

        public string ProveedorEstudioForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(ProveedorEstudio)) ? ProveedorEstudio : "-";
            }
        }
        public string ParentNombre { get; set; }

        public string ParentNombreForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(ParentNombre)) ? ParentNombre : "-";
            }
        }
        public string ObservacionLab { get; set; }

        public string ObservacionLabForDisplay
        {
            get
            {
                return (!string.IsNullOrEmpty(ObservacionLab)) ? ObservacionLab : "-";
            }
        }
    }
}