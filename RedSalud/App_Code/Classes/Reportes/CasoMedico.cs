using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;
using System.Data.SqlTypes;

namespace Artexacta.App.Reportes
{
    /// <summary>
    /// Summary description for RedMedica
    /// </summary>
    public class CasoMedico
    {
        private string _decimalFormat = "#,##0.00";

        private string _NombreCliente;
        private string _CodigoCaso;
        private int _Correlativo;
        private string _Ciudad;
        private DateTime _FechaCreacion;
        private string _EnfermedadCronica;
        private string _MotivoConsultaId;
		private string _Estado;
		private string _NombreMedico;
			
		private string _CodigoAsegurado;
		private string _NombrePaciente;
        private DateTime _FechaNacimiento;
        private string _Genero;
        private string _CasoCritico;
		private string _Antecedentes;
		private string _AntecedentesAlergicos;
		private string _AntecedentesGinecoobstetricos;
			
		private string _NumeroPoliza; 
		private string _NombrePlan;

        private string _MotivoConsulta;
        private string _EnfermedadActual;
		private string _PresionArterial;
		private string _Pulso;
		private string _Temperatura;
		private string _FrecuenciaCardiaca;
		private string _Talla;
		private decimal _Peso;
		private string _ExFisicoRegionalyDeSistema;
		private string _Enfermedad;
		private string _Enfermedad2; 
		private string _Enfermedad3; 
		private string _DiagnosticoPresuntivo;
        private string _BiometriaHematica;
        private string _Observaciones;

        private decimal _CostoConsultaInternista;
        private string _Medicamento;
        private string _Presentacion;
        private string _Concentracion;
        private decimal _TotalRecetas;
        private string _ProveedorEstudio;
        private string _TipoEstudio;
        private string _EstudioObservacion;
        private decimal _TotalLaboratorios;
        private string _ProveedorImagen;
        private string _TipoImagen;
        private string _ImagenObservacion;
        private decimal _TotalImagenes;
        private string _DerivacionMedico;
        private string _DerivacionEspecialidad;
        private string _DerivacionObservacion;
        private decimal _TotalDerivacion;
        private string _Clinica;
        private string _Procedimiento;
        private bool _IsCirugia;
        private string _InternacionObservacion;
        private decimal _TotalInternacion;
        private decimal _TotalEmergencia;
        private decimal _TotalOdontologia;
        private decimal _Total;



        public CasoMedico ( string NombreCliente, string CodigoCaso, int Correlativo, string Ciudad, DateTime FechaCreacion,
            string EnfermedadCronica, string MotivoConsultaId,
            string Estado, string Medico,

            string CodigoPaciente, string NombrePaciente, DateTime FechaNacimiento, string Genero, string CasoCritico, 
            string Antecedentes, string AntecedentesAlergicos, string AntecedentesGinecoobstetricos,

            string NumeroPoliza, string NombrePlan,

            string MotivoConsulta, string EnfermedadActual, string PresionArterial, string Pulso, string Temperatura, string FrecuenciaCardiaca, string Talla,
            decimal Peso, string ExFisicoRegionalyDeSistema, string Enfermedad, string Enfermedad2, string Enfermedad3,
            string DiagnosticoPresuntivo, string BiometriaHematica, string Observaciones, decimal CostoConsultaInternista, 
            string Medicamento, string Presentacion, string Concentracion, decimal TotalRecetas,
            string ProveedorEstudio, string TipoEstudio, string EstudioObservacion, decimal TotalLaboratorios,
            string ProveedorImagen, string TipoImagen, string ImagenObservacion, decimal TotalImagenes, 
            string DerivacionMedico, string DerivacionEspecialidad, string DerivacionObservacion, decimal TotalDerivacion,
            string Clinica, string Procedimiento, bool IsCirugia, string InternacionObservacion, decimal TotalInternacion, decimal TotalEmergencia, decimal TotalOdontologia)
        {
            this.NombreCliente = NombreCliente;
            this.CodigoCaso = CodigoCaso;
            this.Correlativo = Correlativo;
            this.Ciudad = Ciudad;
            this.FechaCreacion = Configuration.Configuration.ConvertToClientTimeZone(FechaCreacion);
            this._EnfermedadCronica = EnfermedadCronica;
            this.MotivoConsultaId = MotivoConsultaId;
            this.Estado = Estado;
            this.NombreMedico = Medico;
            this.CodigoAsegurado = CodigoPaciente;
            this.NombrePaciente = NombrePaciente;
            this.FechaNacimiento = Configuration.Configuration.ConvertToClientTimeZone(FechaNacimiento);
            this.Genero = Genero;
            this.CasoCritico = CasoCritico;
            this.Antecedentes = Antecedentes;
            this.AntecedentesAlergicos = AntecedentesAlergicos;
            this.AntecedentesGinecoobstetricos = AntecedentesGinecoobstetricos;
            this.NumeroPoliza = NumeroPoliza;
            this.NombrePlan = NombrePlan;
            this.MotivoConsulta = MotivoConsulta;
            this.EnfermedadActual = EnfermedadActual;
            this.PresionArterial = PresionArterial;
            this.Pulso = Pulso;
            this.Temperatura = Temperatura;
            this.FrecuenciaCardiaca = FrecuenciaCardiaca;
            this.Talla = Talla;
            this.Peso = Peso;
            this.ExFisicoRegionalyDeSistema = ExFisicoRegionalyDeSistema;
            this.Enfermedad = Enfermedad;
            this.Enfermedad2 = Enfermedad2;
            this.Enfermedad3 = Enfermedad3;
            this.DiagnosticoPresuntivo = DiagnosticoPresuntivo;
            this.BiometriaHematica = BiometriaHematica;
            this.Observaciones = Observaciones;

            this.CostoConsultaInternista = CostoConsultaInternista;

            this.Medicamento = Medicamento;
            this.Presentacion = Presentacion;
            this.Concentracion = Concentracion;
            this.TotalRecetas = TotalRecetas;
            this.ProveedorEstudio = ProveedorEstudio;
            this.TipoEstudio = TipoEstudio;
            this.EstudioObservacion = EstudioObservacion;
            this.TotalLaboratorios = TotalLaboratorios;
            this.ProveedorImagen = ProveedorImagen;
            this.TipoImagen = TipoImagen;
            this.ImagenObservacion = ImagenObservacion;
            this.TotalImagenes = TotalImagenes;
            this.DerivacionMedico = DerivacionMedico;
            this.DerivacionEspecialidad = DerivacionEspecialidad;
            this.DerivacionObservacion = DerivacionObservacion;
            this.TotalDerivacion = TotalDerivacion;
            this.Clinica = Clinica;
            this.Procedimiento = Procedimiento;
            this.IsCirugia = IsCirugia;
            this.InternacionObservacion = InternacionObservacion;
            this.TotalInternacion = TotalInternacion;
            this.TotalEmergencia = TotalEmergencia;
            this.TotalOdontologia = TotalOdontologia;
        }

        public string NombreCliente
        {
            get { return this._NombreCliente; }
            set { this._NombreCliente = value.ToUpper(); }
        }
        public string CodigoCaso
        {
            get { return this._CodigoCaso; }
            set { this._CodigoCaso = value.ToUpper(); }
        }
        public int Correlativo
        {
            get { return this._Correlativo; }
            set { this._Correlativo = value; }
        }
        public string Ciudad
        {
            get { return this._Ciudad; }
            set { this._Ciudad = value.ToUpper(); }
        }
        public DateTime FechaCreacion
        {
            get { return this._FechaCreacion; }
            set { this._FechaCreacion = value; }
        }
        public string EnfermedadCronica
        {
            get { return this._EnfermedadCronica; }
            set { this._EnfermedadCronica = value.ToUpper(); }
        }
        public string MotivoConsultaId
        {
            get { return this._MotivoConsultaId; }
            set { this._MotivoConsultaId = value.ToUpper(); }
        }
        public string Estado
        {
            get { return this._Estado; }
            set { this._Estado = value.ToUpper(); }
        }
        public string NombreMedico
        {
            get { return this._NombreMedico; }
            set { this._NombreMedico = value.ToUpper(); }
        }
        public string CodigoAsegurado
        {
            get { return this._CodigoAsegurado; }
            set { this._CodigoAsegurado = value.ToUpper(); }
        }
        public string NombrePaciente
        {
            get { return this._NombrePaciente; }
            set { this._NombrePaciente = value.ToUpper(); }
        }
        public DateTime FechaNacimiento
        {
            get { return this._FechaNacimiento; }
            set { this._FechaNacimiento = value; }
        }
        public string Edad
        {
            get {
                if (this._FechaNacimiento <= SqlDateTime.MinValue.Value ||
                    this._FechaNacimiento <= new DateTime(1900, 1, 1))
                {
                    return "";
                }
                else
                {
                    int age = DateTime.Now.Year - this._FechaNacimiento.Year;
                    if (this._FechaNacimiento > DateTime.Now.AddYears(-age)) age--;
                    return this._FechaNacimiento.ToShortDateString();
                    //return age + " AÑOS";
                }
            }
        }

        public int EdadCalculada
        {
            get
            {
                int Edad = 0;
                DateTime birthday = FechaNacimiento;

                if (birthday == null || birthday == DateTime.MinValue || birthday.Year <= 1900)
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

        public int EstaturaCm { get; set; }

        public double EstaturaMetros
        {
            get
            {
                if (EstaturaCm <= 30)
                    return 0.00;

                double estaturaMetros = ((double) EstaturaCm / 100.0);
                return Math.Round(estaturaMetros, 2);
            }
        }
        public double IMC
        {
            get
            {
                if (EdadCalculada < 18 || Peso <= 0)
                    return 0.00;
                double pesoDouble = 0;
                decimal pesoDec = Peso;
                double.TryParse(pesoDec.ToString(), out pesoDouble);

                double estaturaMetros = ((double)EstaturaCm / 100.0);
                double estaturaFinal = (pesoDouble / (estaturaMetros * estaturaMetros));
                if (pesoDouble > 0 && EstaturaCm > 30)
                    return estaturaFinal;

                return 0.00;
            }
        }
        public string IMCDescription
        {
            get
            {
                string result = "";
                
                double IMC_Kg = IMC;

                if (EdadCalculada < 18 || IMC_Kg <= 0)
                    return result;


                if (IMC_Kg <= 0)
                    result = "";

                if (IMC_Kg < 16.00)
                    result = "Infrapeso: Delgadez Severa";

                if (IMC_Kg >= 16.00 && IMC_Kg <= 16.99)
                    result = "Infrapeso: Delgadez moderada";

                if (IMC_Kg >= 17.00 && IMC_Kg <= 18.49)
                    result = "Infrapeso: Delgadez aceptable";

                if (IMC_Kg >= 18.50 && IMC_Kg <= 24.99)
                    result = "Peso Normal";

                if (IMC_Kg >= 25.00 && IMC_Kg <= 29.99)
                    result = "Sobrepeso";

                if (IMC_Kg >= 30.00 && IMC_Kg <= 34.99)
                    result = "Obeso: Tipo I";

                if (IMC_Kg >= 35.00 && IMC_Kg <= 40.00)
                    result = "Obeso: Tipo II";

                if (IMC_Kg > 40.00)
                    result = "Obeso: Tipo III";

                return result;
            }
        }

        public string Genero
        {
            get { return this._Genero; }
            set { this._Genero = value.ToUpper(); }
        }
        public string CasoCritico
        {
            get { return this._CasoCritico; }
            set { this._CasoCritico = value.ToUpper(); }
        }
        public string Antecedentes
        {
            get { return this._Antecedentes; }
            set { this._Antecedentes = value.ToUpper(); }
        }
        public string AntecedentesAlergicos
        {
            get { return this._AntecedentesAlergicos; }
            set { this._AntecedentesAlergicos = value.ToUpper(); }
        }
        public string AntecedentesGinecoobstetricos
        {
            get { return this._AntecedentesGinecoobstetricos; }
            set { this._AntecedentesGinecoobstetricos = value.ToUpper(); }
        }
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value.ToUpper(); }
        }
        public string NombrePlan
        {
            get { return this._NombrePlan; }
            set { this._NombrePlan = value.ToUpper(); }
        }
        public string EnfermedadActual
        {
            get { return this._EnfermedadActual; }
            set { this._EnfermedadActual = value.ToUpper(); }
        }
        public string MotivoConsulta
        {
            get { return this._MotivoConsulta; }
            set { this._MotivoConsulta = value.ToUpper(); }
        }
        public string PresionArterial
        {
            get { return this._PresionArterial; }
            set { this._PresionArterial = value.ToUpper(); }
        }
        public string Pulso
        {
            get { return this._Pulso; }
            set { this._Pulso = value.ToUpper(); }
        }
        public string Temperatura
        {
            get { return this._Temperatura; }
            set { this._Temperatura = value.ToUpper(); }
        }
        public string FrecuenciaCardiaca
        {
            get { return this._FrecuenciaCardiaca; }
            set { this._FrecuenciaCardiaca = value.ToUpper(); }
        }
        public string Talla
        {
            get { return this._Talla; }
            set { this._Talla = value.ToUpper(); }
        }
        public decimal Peso
        {
            get { return this._Peso; }
            set { this._Peso = value; }
        }
        public string ExFisicoRegionalyDeSistema
        {
            get { return this._ExFisicoRegionalyDeSistema; }
            set { this._ExFisicoRegionalyDeSistema = value.ToUpper(); }
        }
        public string Enfermedad
        {
            get { return this._Enfermedad; }
            set { this._Enfermedad = value.ToUpper(); }
        }
        public string Enfermedad2
        {
            get { return this._Enfermedad2; }
            set { this._Enfermedad2 = value.ToUpper(); }
        }
        public string Enfermedad3
        {
            get { return this._Enfermedad3; }
            set { this._Enfermedad3 = value.ToUpper(); }
        }
        public string DiagnosticoPresuntivo
        {
            get { return this._DiagnosticoPresuntivo; }
            set { this._DiagnosticoPresuntivo = value.ToUpper(); }
        }
        public string DiagnosticoPresuntivoToDisplay
        {
            get {
                if (!string.IsNullOrEmpty(Enfermedad))
                    return Enfermedad + "<br />" +
                           Enfermedad2 + "<br />" +
                           Enfermedad3;
                else
                    return this._DiagnosticoPresuntivo; 
            }
        }
        public string BiometriaHematica
        {
            get { return this._BiometriaHematica; }
            set { this._BiometriaHematica = value.ToUpper(); }
        }
        public string Observaciones
        {
            get { return this._Observaciones; }
            set { this._Observaciones = value.ToUpper(); }
        }

        public decimal CostoConsultaInternista
        {
            get { return this._CostoConsultaInternista; }
            set { this._CostoConsultaInternista = value; }
        }
        public string Medicamento
        {
            get { return _Medicamento; }
            set { _Medicamento = value.ToUpper(); }
        }
        public string Presentacion
        {
            get { return _Presentacion; }
            set { _Presentacion = value.ToUpper(); }
        }
        public string Concentracion
        {
            get { return _Concentracion; }
            set { _Concentracion = value.ToUpper(); }
        }
        public decimal TotalRecetas
        {
            get { return this._TotalRecetas; }
            set { this._TotalRecetas = value; }
        }
        public string ProveedorEstudio
        {
            get { return _ProveedorEstudio; }
            set { _ProveedorEstudio = value.ToUpper(); }
        }
        public string TipoEstudio
        {
            get { return _TipoEstudio; }
            set { _TipoEstudio = value.ToUpper(); }
        }
        public string EstudioObservacion
        {
            get { return _EstudioObservacion; }
            set { _EstudioObservacion = value.ToUpper(); }
        }
        public decimal TotalLaboratorios
        {
            get { return this._TotalLaboratorios; }
            set { this._TotalLaboratorios = value; }
        }
        public string ProveedorImagen
        {
            get { return _ProveedorImagen; }
            set { _ProveedorImagen = value.ToUpper(); }
        }
        public string TipoImagen
        {
            get { return _TipoImagen; }
            set { _TipoImagen = value.ToUpper(); }
        }
        public string ImagenObservacion
        {
            get { return _ImagenObservacion; }
            set { _ImagenObservacion = value.ToUpper(); }
        }
        public decimal TotalImagenes
        {
            get { return this._TotalImagenes; }
            set { this._TotalImagenes = value; }
        }
        public string DerivacionMedico
        {
            get { return _DerivacionMedico; }
            set { _DerivacionMedico = value.ToUpper(); }
        }
        public string DerivacionEspecialidad
        {
            get { return _DerivacionEspecialidad; }
            set { _DerivacionEspecialidad = value.ToUpper(); }
        }
        public string DerivacionObservacion
        {
            get { return _DerivacionObservacion; }
            set { _DerivacionObservacion = value.ToUpper(); }
        }
        public decimal TotalDerivacion
        {
            get { return this._TotalDerivacion; }
            set { this._TotalDerivacion = value; }
        }
        public string Clinica
        {
            get { return _Clinica; }
            set { _Clinica = value.ToUpper(); }
        }
        public string Procedimiento
        {
            get { return _Procedimiento; }
            set { _Procedimiento = value.ToUpper(); }
        }
        public bool IsCirugia
        {
            get { return _IsCirugia; }
            set { _IsCirugia = value; }
        }
        public string IsCirugiaForDisplay
        {
            get { return _IsCirugia ? "SI" : "NO"; }
        }
        public string InternacionObservacion
        {
            get { return _InternacionObservacion; }
            set { _InternacionObservacion = value.ToUpper(); }
        }

        public decimal TotalEmergencia
        {
            get { return this._TotalEmergencia; }
            set { this._TotalEmergencia = value; }
        }

        public decimal TotalOdontologia
        {
            get { return this._TotalOdontologia; }
            set { this._TotalOdontologia = value; }
        }

        public decimal TotalInternacion
        {
            get { return this._TotalInternacion; }
            set { this._TotalInternacion = value; }
        }
        public decimal Total
        {
            get { return CostoConsultaInternista + TotalRecetas + TotalLaboratorios + TotalDerivacion + TotalInternacion; }
        }
        public string MotivoConsultaIdforDisplay
        {
            get
            {
                switch (this._MotivoConsultaId)
                {
                    case "ENFER":
                        return "ENFERMERIA";
                    case "EMERG":
                        return "EMERGENCIA";
                    case "RECASO":
                        return "RECONSULTA";
                    default:
                        return "CASO MEDICO";
                }
            }
        }
        public string CostoConsultaInternistaToDisplay
        {
            get { return this.CostoConsultaInternista.ToString(_decimalFormat); }
        }
        public string TotalRecetasToDisplay
        {
            get { 
                return this.TotalRecetas.ToString(_decimalFormat); 
            }
        }
        public string TotalLaboratoriosToDisplay
        {
            get { return this.TotalLaboratorios.ToString(_decimalFormat); }
        }
        public string TotalDerivacionToDisplay
        {
            get { return this.TotalDerivacion.ToString(_decimalFormat); }
        }
        public string TotalInternacionToDisplay
        {
            get { return this.TotalInternacion.ToString(_decimalFormat); }
        }
        public string TotalToDisplay
        {
            get { return this.Total.ToString(_decimalFormat); }
        }
    }
}