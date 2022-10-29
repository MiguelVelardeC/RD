using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Artexacta.App.Utilities.Text;
using EvoPdf;
using log4net;
using System.Globalization;
using Artexacta.App.Paciente.BLL;

namespace Artexacta.App.Caso
{
    /// <summary>
    /// Summary description for Caso
    /// </summary>
    public class Caso
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        private int _CasoId;
        private int _CitaId = 0;
        private string _CodigoCaso;
        private int _Correlativo;
        private string _CiudadId;
        private DateTime _FechaCreacion;
        private DateTime _FechaReconsulta;
        private DateTime _FechaEstadoReceta;
        private DateTime _FechaEstadoExamenes;
        private DateTime _FechaEstadoEspecialista;
        private int _UserId;
        private int _PolizaId;
        private int _ReconsultaId;
        private string _MotivoConsultaId;
        private string _Estado;// estado del caso:EnAtencion/Cerrado
        private bool _Dirty;
        private bool _IsGastoBlocked;

        //de Paciente
        private int _PacienteId;
        private string _Antecedentes;
        private string _AntecedentesAlergicos;//alergias medicamentos
        private string _AntecedentesGinecoobstetricos;//alergias alimentos

        //Histotia //Tratamiento no se si al final queda eliminado?
        private int _HistoriaId;
        private string _MotivoConulta;
        private string _EnfermedadActual;
        private int _ProtocoloId;
        private string _PresionArterial;
        private string _Pulso;
        private string _Temperatura;
        private string _FrecuenciaCardiaca;
		private string _Talla;
        private int _EstaturaCm;
        private decimal _Peso;
        private string _ExFisicoRegionalyDeSistema;
        private string _EnfermedadId;
        private string _Enfermedad;
        private string _Enfermedad2Id;
        private string _Enfermedad2;
        private string _Enfermedad3Id;
        private string _Enfermedad3;
        private string _DiagnosticoPresuntivo;
        private string _BiometriaHematica;
        private string _Observaciones;
        private bool _CasoCritico;

        private string _NombrePaciente;
        private int _clienteId;
        private string _NombreAseguradora;
        private string _CodigoAsegurado;

        //poliza
        private string _NumeroPoliza;
        private DateTime _FechaInicio;
        private DateTime _FechaFin;
        private decimal _MontoTotal;
        private string _NombrePlan;
        private string _EstadoPoliza; //estado de la poliza: Activo/Inactivo(inacivo cuando no cancelo su cuota)

        private decimal _GastoTotal;

        //para el historial
        private string _UserName;
        private string _MotivoConsultaTipo;

        //proveedor
        private int _proveedorId;
        private string _NombreProveedorJuridico;

        //Medico
        private string _medicoName;

        //Search
        private int _HistoriaCount;
        private int _rowNumber;
        private int _cantGastos;

        #region TblCaso

        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public int CitaId
        {
            get { return this._CitaId; }
            set { this._CitaId = value; }
        }
        public string CodigoCaso
        {
            get { return this._CodigoCaso; }
            set { this._CodigoCaso = value; }
        }
        public int Correlativo
        {
            get { return this._Correlativo; }
            set { this._Correlativo = value; }
        }
        public string CiudadId
        {
            get { return this._CiudadId; }
            set { this._CiudadId = value; }
        }
        public DateTime FechaCreacion
        {
            get
            {
                return this._FechaCreacion;
            }
            set
            {
                this._FechaCreacion = value;
            }
        }
        public DateTime HoraCreacion
        {
            get { return this._FechaCreacion; }
            set
            {
                DateTime hora = value;
                TimeSpan time = new TimeSpan(0, hora.Hour, hora.Minute, hora.Second, hora.Millisecond);
                this._FechaCreacion = new DateTime(this._FechaCreacion.Year, this._FechaCreacion.Month,
                    this._FechaCreacion.Day, time.Hours, time.Minutes, time.Seconds);
            }
        }
        public DateTime FechaReconsulta
        {
            get { return this._FechaReconsulta; }
            set { this._FechaReconsulta = value; }
        }
        public DateTime FechaEstadoReceta
        {
            get { return this._FechaEstadoReceta; }
            set { this._FechaEstadoReceta = value; }
        }
        public DateTime FechaEstadoEspecialista
        {
            get { return this._FechaEstadoEspecialista; }
            set { this._FechaEstadoEspecialista = value; }
        }
        public DateTime FechaEstadoExamenes
        {
            get { return this._FechaEstadoExamenes; }
            set { this._FechaEstadoExamenes = value; }
        }
        public int UserId
        {
            get { return this._UserId; }
            set { this._UserId = value; }
        }
        public int PolizaId
        {
            get { return this._PolizaId; }
            set { this._PolizaId = value; }
        }
        public int ReconsultaId
        {
            get { return this._ReconsultaId; }
            set { this._ReconsultaId = value; }
        }
        public string MotivoConsultaId
        {
            get
            {
                return this._MotivoConsultaId;
            }
            set { this._MotivoConsultaId = value; }
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
                    case "ODONTO":
                        return "ODONTOLOGÍA";
                    default:
                        return "CASO MÉDICO";
                }
            }
        }
        public string Estado
        {
            get { return this._Estado; }
            set { this._Estado = value; }
        }
        public bool Dirty
        {
            get { return this._Dirty; }
            set { this._Dirty = value; }
        }
        #endregion

        public string FechaCreacionString
        {
            get
            {
                return this._FechaCreacion.ToString("dd/MM/yyyy hh:mm tt");
            }
        }
        public string FechaReconsultaString
        {
            get { return this.FechaReconsulta.ToString("dd/MM/yyyy hh:mm tt"); }
        }

        public bool ExistsFechaReconsulta
        {
            get
            {
                if (_FechaCreacion == _FechaReconsulta)
                    return false;
                else
                    return true;
            }
        }
        //para ocultar algunas cosas en historial
        public bool IsEmergencia
        {
            get
            {
                if (this._MotivoConsultaId != null)
                {
                    if (this._MotivoConsultaId.Equals("EMERG"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (this._MotivoConsultaTipo.Equals("Emergencia"))
                        return true;
                    else
                        return false;
                }
            }
        }
        public bool IsReconsulta
        {
            get
            {
                return this._MotivoConsultaId == "RECASO";
            }
        }

        #region TblPaciente
        public int PacienteId
        {
            get { return this._PacienteId; }
            set { this._PacienteId = value; }
        }
        public string Antecedentes
        {
            get
            {//,\"type\": \"textarea\"
                return compareJSon("[{\"name\": \"Antecedentes familiares\",\"value\": \"\",\"type\": \"textarea\"},{\"name\": \"Antecedentes personales alérgicos\",\"value\": \"\",\"type\": \"textarea\"},{\"name\": \"Ancedentes personales patológicos\",\"value\": \"\",\"type\": \"textarea\"},{\"name\": \"Antecedentes quirurgicos\",\"value\": \"\",\"type\": \"textarea\"}]"
                                       , this._Antecedentes);
            }
            set { this._Antecedentes = value; }
        }
        public string AntecedentesAutoComplete
        {
            get
            {
                if (string.IsNullOrEmpty(_Antecedentes))
                {
                    Paciente.Paciente objPaciente = new Paciente.Paciente();
                    return objPaciente.StaticEmptyAntecedentes;
                }
                else
                    return this._Antecedentes;
            }
            set { this._Antecedentes = value; }
        }
        public string AntecedentesAlergicos
        {
            get { return this._AntecedentesAlergicos; }
            set { this._AntecedentesAlergicos = value; }
        }
        public string AntecedentesAlergicosAutoComplete
        {
            get
            {
                if (string.IsNullOrEmpty(_AntecedentesAlergicos))
                {
                    Paciente.Paciente objPaciente = new Paciente.Paciente();
                    return objPaciente.StaticEmptyAntecedentesAlergicos;
                }
                else
                    return this._AntecedentesAlergicos;
            }
            set { this._AntecedentesAlergicos = value; }
        }
        public string AntecedentesGinecoobstetricos
        {
            get
            {
                return compareJSon("[{\"name\": \"Menarquia\",\"value\": \"\"},{\"name\": \"FUM\",\"type\": \"date\",\"value\": \"\"},{\"name\": \"G\",\"value\": \"\"},{\"name\": \"P\",\"value\": \"\"},{\"name\": \"A\",\"value\": \"\"},{\"name\": \"C\",\"value\": \"\"}," +
                    "{\"name\": \"PAP\",\"value\": \"\"},{\"name\": \"Anticonceptivos\",\"value\": \"\"}]", this._AntecedentesGinecoobstetricos);
            }
            set { this._AntecedentesGinecoobstetricos = value; }
        }
        #endregion

        #region TblHistoria
        public int HistoriaId
        {
            get { return this._HistoriaId; }
            set { this._HistoriaId = value; }
        }
        public string MotivoConsulta
        {
            get { return this._MotivoConulta; }
            set { this._MotivoConulta = value; }
        }
        public string EnfermedadActual
        {
            get { return this._EnfermedadActual; }
            set { this._EnfermedadActual = value; }
        }
        public int ProtocoloId
        {
            get { return this._ProtocoloId; }
            set { this._ProtocoloId = value; }
        }
        public string PresionArterial
        {
            get { return this._PresionArterial; }
            set { this._PresionArterial = value; }
        }
        public string Pulso
        {
            get { return this._Pulso; }
            set { this._Pulso = value; }
        }
        public string Temperatura
        {
            get { return this._Temperatura; }
            set { this._Temperatura = value; }
        }
        public string FrecuenciaCardiaca
        {
            get { return this._FrecuenciaCardiaca; }
            set { this._FrecuenciaCardiaca = value; }
        }
        public string Talla
        {
            get { return this._Talla; }
            set { this._Talla = value; }
        }
        public double IMC
        {
            get
            {
                if (PacienteId != 0)
                {
                    Paciente.Paciente p = PacienteBLL.GetPacienteByPacienteId(PacienteId);
                    if (p != null)
                    {
                        string EdadRAW = p.Edad;
                        string Edad = EdadRAW.Substring(0, EdadRAW.Length - 4);                        
                        int intEdad = 0;
                        if (int.TryParse(Edad, out intEdad))
                        {
                            if (intEdad >= 18)
                            {
                                double estaturaDouble = EstaturaM * EstaturaM;
                                return Math.Round((PesoDouble / estaturaDouble),2);
                            }
                        }
                    }
                }
                return 0.00;
            }
        }
        public string IMC_DESC
        {
            get
            {
                double d_imc = IMC;
                if (d_imc < 16.00 && d_imc != 0)
                {
                    return "Infrapeso: Delgadez Severa";
                }
                else if (d_imc >= 16.00 && d_imc <= 16.99)
                {
                    return "Infrapeso: Delgadez moderada";
                }
                else if (d_imc >= 17.00 && d_imc <= 18.49)
                {
                    return "Infrapeso: Delgadez aceptable";
                }

                else if (d_imc >= 18.50 && d_imc <= 24.99)
                {
                    return "Peso Normal";
                }
                else if (d_imc >= 25.00 && d_imc <= 29.99)
                {
                    return "Sobrepeso";
                }
                else if (d_imc >= 30.00 && d_imc <= 34.99)
                {
                    return "Obeso: Tipo I";
                }
                else if (d_imc >= 35.00 && d_imc <= 40.00)
                {
                    return "Obeso: Tipo II";
                }
                else if (d_imc > 40.00)
                {
                    return "Obeso: Tipo III";
                }
                return "";
            }
        }

        public int EstaturaCm
        {
            get { return this._EstaturaCm; }
            set { this._EstaturaCm = value; }
        }

        public double EstaturaM
        {
            get
            {
                double v = Convert.ToDouble(this._EstaturaCm / 100.0);
                if (v < 0.3)
                    return 0.0;
                if (v > 3.0)
                    return 3.0;
                return v;
            }
            set { this._EstaturaCm = Convert.ToInt32(value * 100.0); }
        }

        public decimal Peso
        {
            get { return this._Peso; }
            set { this._Peso = value; }
        }
        public double PesoDouble
        {
            get { return Convert.ToDouble(this._Peso); }
            set
            {
                this._Peso = Convert.ToDecimal(value);
            }
        }
        public string PesoDisplay
        {
            get { return this._Peso > 0 ? this._Peso.ToString("0.##", CultureInfo.InvariantCulture) + " Kg" : ""; }
        }
        public string ExFisicoRegionalyDeSistema
        {
            get
            {
                if (MotivoConsultaId == "ENFER" || MotivoConsultaTipo == "ENFERMERIA")
                {
                    return this._ExFisicoRegionalyDeSistema.StartsWith("[{") ? "" : this._ExFisicoRegionalyDeSistema;
                }
                else if (MotivoConsultaId == "EMERG" || MotivoConsultaTipo == "EMERGENCIA")
                {
                    return compareJSon("[{\"name\": \"Examen físico General\",\"value\": \"\",\"type\": \"textarea\", \"rows\": \"3\"}]"
                                       , this._ExFisicoRegionalyDeSistema);
                }
                else
                {
                    return compareJSon("[{\"name\":\"Cabeza\",\"value\":\"\"},{\"name\":\"Cuello\",\"value\":\"\"},{\"name\":\"Tórax\",\"type\":\"group\",\"group\":[{\"name\":\"Aparato Respiratorio\",\"value\":\"\"}]},{\"name\":\"Aparato Cardiocirculatorio\",\"value\":\"\"},{\"name\":\"Abdomen\",\"type\":\"group\",\"group\":[{ \"name\": \"Aparato Digestivo\",\"value\": \"\" }, { \"name\": \"Aparato Genitourinario\", \"value\": \"\" }]},{\"name\": \"Extremidades\",\"value\": \"\"},{\"name\": \"Evaluacion neurologica y circulacion\",\"value\": \"\",\"type\": \"textarea\"}]"
                                       , this._ExFisicoRegionalyDeSistema);
                }
            }
            set { this._ExFisicoRegionalyDeSistema = value; }
        }

        public string EnfermedadId
        {
            get { return this._EnfermedadId; }
            set { this._EnfermedadId = value; }
        }

        public string Enfermedad
        {
            get { return this._Enfermedad; }
            set { this._Enfermedad = value; }
        }

        public string Enfermedad2Id
        {
            get { return this._Enfermedad2Id; }
            set { this._Enfermedad2Id = value; }
        }

        public string Enfermedad2
        {
            get { return this._Enfermedad2; }
            set { this._Enfermedad2 = value; }
        }

        public string Enfermedad3Id
        {
            get { return this._Enfermedad3Id; }
            set { this._Enfermedad3Id = value; }
        }

        public string Enfermedad3
        {
            get { return this._Enfermedad3; }
            set { this._Enfermedad3 = value; }
        }

        public string DiagnosticoPresuntivoForDisplay
        {
            get { 
                return string.IsNullOrWhiteSpace(this._Enfermedad) ? this._DiagnosticoPresuntivo : this._Enfermedad; 
            }
        }

        public string DiagnosticoPresuntivoExtra
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this._DiagnosticoPresuntivo) &&
                    (!string.IsNullOrEmpty(this._Enfermedad) || !string.IsNullOrEmpty(this._Enfermedad2Id) ||
                    !string.IsNullOrEmpty(this._Enfermedad3Id))? this._DiagnosticoPresuntivo : "";
            }
        }

        public string DiagnosticoPresuntivo
        {
            get { return this._DiagnosticoPresuntivo; }
            set { this._DiagnosticoPresuntivo = value; }
        }
        public string BiometriaHematica
        {
            get { return this._BiometriaHematica; }
            set { this._BiometriaHematica = value; }
        }
        public string Observaciones
        {
            get { return this._Observaciones; }
            set { this._Observaciones = value; }
        }
        public bool CasoCritico
        {
            get { return this._CasoCritico; }
            set { this._CasoCritico = value; }
        }

        #endregion

        public string NombrePaciente
        {
            get { return this._NombrePaciente; }
        }
        public string NombrePacienteForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_PacienteId, this._NombrePaciente); }
        }
        public string NombreAseguradora
        {
            get { return this._NombreAseguradora; }
        }
        public string NombreAseguradoraForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_clienteId, this._NombreAseguradora); }
        }
        public string CodigoAsegurado
        {
            get { return this._CodigoAsegurado; }
        }

        #region Poliza
        public string NumeroPoliza
        {
            get
            {
                if (!string.IsNullOrEmpty(this._NumeroPoliza))
                    return this._NumeroPoliza;
                else
                {
                    if (this._PolizaId <= 0)
                        return "";

                    Poliza.Poliza objPoliza = Poliza.BLL.PolizaBLL.GetPolizaByPolizaId(this._PolizaId);
                    if (objPoliza != null)
                        return objPoliza.NumeroPoliza;
                    else
                        return "";
                }
            }
        }
        public DateTime FechaInicio
        {
            get { return this._FechaInicio; }
        }
        public DateTime FechaFin
        {
            get { return this._FechaFin; }
        }
        public decimal MontoTotal
        {
            get { return this._MontoTotal; }
        }
        public string NombrePlan
        {
            get { return this._NombrePlan; }
        }
        public string EstadoPoliza
        {
            get { return this._EstadoPoliza; }
        }
        #endregion

        public decimal GastoTotal
        {
            get { return this._GastoTotal; }
        }
        public int CantGastos
        {
            get
            {
                return this._cantGastos;
            }
        }
        public int HistoriaCount
        {
            get { return _HistoriaCount; }
            set { _HistoriaCount = value; }
        }
        public int RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; }
        }

        public string Siniestralidad
        {
            get
            {
                decimal Porcentaje = _GastoTotal * 100 / _MontoTotal;
                return _GastoTotal.ToString() + "Bs/" + _MontoTotal.ToString() + "Bs (" + Porcentaje.ToString("#,##0.00") + "%)";
            }
        }

        public string MedicoName
        {
            get
            {
                if (_medicoName != null)
                    return _medicoName;
                if (_UserId > 0)
                {
                    string strUser = User.BLL.UserBLL.GetUserById(this._UserId).FullName;
                    return strUser;
                }
                else return "--";
            }
        }

        public string UserName
        {
            get
            {
                if (this._UserName != null)
                    return this._UserName;
                else if (this._UserId > 0)
                {
                    string strUser = User.BLL.UserBLL.GetUserById(this._UserId).Username;
                    return strUser;
                }
                else return "--";
            }
        }
        public string MotivoConsultaTipo
        {
            get { return this._MotivoConsultaTipo; }
        }

        public string GroupHistorial
        {
            get { return this._MotivoConsultaTipo + "<span class='noPrint'> - " + this._MotivoConulta + " - " + this._FechaCreacion.ToShortDateString() + "</span>"; }
        }
        public int ProveedorId
        {
            get { return _proveedorId; }
            set { _proveedorId = value; }
        }
        public string NombreProveedorJuridico
        {
            get { return this._NombreProveedorJuridico; }
        }
        public bool IsGastoBlocked
        {
            get { return _IsGastoBlocked; }
        }

        public string RangoDeFechas
        {
            get { return _FechaInicio.ToShortDateString() + " - " + _FechaFin.ToShortDateString(); }
        }

        public int DerivacionId { get; set; }

        public Caso()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //Caso Medico
        public Caso(int CasoId, string CodigoCaso, int Correlativo
            , string CiudadId, DateTime FechaCreacion, DateTime FechaReconsulta
            , DateTime FechaEstadoReceta, DateTime FechaEstadoEspecialista
            , DateTime FechaEstadoExamenes, int UserId, int PolizaId
            , string MotivoConsultaId, string Estado, bool Dirty
            , int PacienteId
            , string Antecedentes, string AntecedentesAlergicos, string AntecedentesGinecoobstetricos
            , int HistoriaId
            , string PresionArterial, string Pulso, string Temperatura
            , string FrecuenciaCardiaca, string Talla, decimal Peso, string ExFisicoRegionalyDeSistema
            , string enfermedadId, string enfermedad, string enfermedad2Id, string enfermedad2, string enfermedad3Id, string enfermedad3
            , string DiagnosticoPresuntivo, string BiometriaHematica, string MotivoConsulta, string EnfermedadActual, string Observaciones
            , bool CasoCritico, int ProveedorId, string Proveedor)
        {
            this._CasoId = CasoId;
            this._CodigoCaso = CodigoCaso;
            this._Correlativo = Correlativo;
            this._CiudadId = CiudadId;
            this._FechaCreacion = FechaCreacion;
            this._FechaReconsulta = FechaReconsulta;
            this._FechaEstadoReceta = FechaEstadoReceta;
            this._FechaEstadoEspecialista = FechaEstadoEspecialista;
            this._FechaEstadoExamenes = FechaEstadoExamenes;
            this._UserId = UserId;
            this._PolizaId = PolizaId;
            this._MotivoConsultaId = MotivoConsultaId;
            this._Estado = Estado;
            this._Dirty = Dirty;

            this._PacienteId = PacienteId;
            this._Antecedentes = Antecedentes;
            this._AntecedentesAlergicos = AntecedentesAlergicos;
            this._AntecedentesGinecoobstetricos = AntecedentesGinecoobstetricos;

            this._HistoriaId = HistoriaId;
            this._PresionArterial = PresionArterial;
            this._Pulso = Pulso;
            this._Temperatura = Temperatura;
            this._FrecuenciaCardiaca = FrecuenciaCardiaca;
            this._Talla = Talla;
            this._Peso = Peso;
            this._ExFisicoRegionalyDeSistema = ExFisicoRegionalyDeSistema;
            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._Enfermedad2Id = enfermedad2Id;
            this._Enfermedad2 = enfermedad2;
            this._Enfermedad3Id = enfermedad3Id;
            this._Enfermedad3 = enfermedad3;
            this._DiagnosticoPresuntivo = DiagnosticoPresuntivo;
            this._BiometriaHematica = BiometriaHematica;
            this._Observaciones = Observaciones;

            this._MotivoConulta = MotivoConsulta;
            this._EnfermedadActual = EnfermedadActual;

            this._CasoCritico = CasoCritico;

            //SOLO EMERGENCIAS
            this._proveedorId = ProveedorId;
            this._NombreProveedorJuridico = Proveedor;
        }

        //Caso Medico Basic (Dirty) AND Search
        public Caso(int CasoId, string CodigoCaso, int Correlativo
            , string CiudadId, DateTime FechaCreacion, int UserId, int PolizaId
            , string MotivoConsultaId, string Estado, bool Dirty, bool IsGastoBlocked
            , int PacienteId
            , string NombrePaciente
            , int HistoriaId, string enfermedadId, string enfermedad, string enfermedad2Id
            , string enfermedad2, string enfermedad3Id, string enfermedad3
            , string DiagnosticoPresuntivo, string MotivoConsulta
            , int clienteId
            , string NombreAseguradora, string CodigoAsegurado, bool CasoCritico, string numeroPoliza, string medicoName, int cantGastos)
        {
            this._CasoId = CasoId;
            this._CodigoCaso = CodigoCaso;
            this._Correlativo = Correlativo;
            this._CiudadId = CiudadId;
            this._FechaCreacion = FechaCreacion;
            this._UserId = UserId;
            this._PolizaId = PolizaId;
            this._MotivoConsultaId = MotivoConsultaId;
            this._Estado = Estado;
            this._Dirty = Dirty;
            this._IsGastoBlocked = IsGastoBlocked;

            this._PacienteId = PacienteId;
            this._NombrePaciente = NombrePaciente;

            this._HistoriaId = HistoriaId;

            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._Enfermedad2Id = enfermedad2Id;
            this._Enfermedad2 = enfermedad2;
            this._Enfermedad3Id = enfermedad3Id;
            this._Enfermedad3 = enfermedad3;
            this._DiagnosticoPresuntivo = DiagnosticoPresuntivo;

            this._MotivoConulta = MotivoConsulta;

            this._clienteId = clienteId;
            this._NombreAseguradora = NombreAseguradora;
            this._CodigoAsegurado = CodigoAsegurado;
            this._CasoCritico = CasoCritico;
            this._NumeroPoliza = numeroPoliza;
            this._medicoName = medicoName;
            this._cantGastos = cantGastos;
        }

        //Search Caso For Aprobation 
        public Caso(int CasoId, string CodigoCaso, int Correlativo
            , string CiudadId, DateTime FechaCreacion, int UserId, int PolizaId
            , string MotivoConsultaId, string Estado, bool Dirty
            , int PacienteId
            , string NombrePaciente
            , int HistoriaId, string enfermedadId, string enfermedad, string enfermedad2Id
            , string enfermedad2, string enfermedad3Id, string enfermedad3
            , string DiagnosticoPresuntivo, string MotivoConsulta
            , int clienteId, string NombreAseguradora, string CodigoAsegurado

            , string NumeroPoliza, DateTime FechaInicio, DateTime FechaFin, decimal MontoTotal
            , string NombrePlan, string EstadoPoliza, decimal GastoTotal)
        {
            this._CasoId = CasoId;
            this._CodigoCaso = CodigoCaso;
            this._Correlativo = Correlativo;
            this._CiudadId = CiudadId;
            this._FechaCreacion = FechaCreacion;
            this._UserId = UserId;
            this._PolizaId = PolizaId;
            this._MotivoConsultaId = MotivoConsultaId;
            this._Estado = Estado;
            this._Dirty = Dirty;

            this._PacienteId = PacienteId;
            this._NombrePaciente = NombrePaciente;

            this._HistoriaId = HistoriaId;

            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._Enfermedad2Id = enfermedad2Id;
            this._Enfermedad2 = enfermedad2;
            this._Enfermedad3Id = enfermedad3Id;
            this._Enfermedad3 = enfermedad3;
            this._DiagnosticoPresuntivo = DiagnosticoPresuntivo;

            this._MotivoConulta = MotivoConsulta;

            this._clienteId = clienteId;
            this._NombreAseguradora = NombreAseguradora;
            this._CodigoAsegurado = CodigoAsegurado;

            this._NumeroPoliza = NumeroPoliza;
            this._FechaInicio = FechaInicio;
            this._FechaFin = FechaFin;
            this._MontoTotal = MontoTotal;
            this._NombrePlan = NombrePlan;
            this._EstadoPoliza = EstadoPoliza;
            this._GastoTotal = GastoTotal;
        }

        //Historial
        public Caso(int CasoId, string CodigoCaso, int Correlativo
            , DateTime FechaCreacion
            , string UserName, string NumeroPoliza, string MotivoConsultaTipo
            , string Estado
            , int HistoriaId, string MotivoConsulta, string EnfermedadActual
            , string PresionArterial, string Pulso, string Temperatura
            , string FrecuenciaCardiaca, string Talla, decimal Peso, string ExFisicoRegionalyDeSistema
            , string enfermedadId, string enfermedad, string enfermedad2Id
            , string enfermedad2, string enfermedad3Id, string enfermedad3
            , string DiagnosticoPresuntivo, string BiometriaHematica, string Observaciones, int ProveedorId, string Proveedor)
        {
            this._CasoId = CasoId;
            this._CodigoCaso = CodigoCaso;
            this._Correlativo = Correlativo;
            this._FechaCreacion = FechaCreacion;
            this._UserName = UserName;
            this._NumeroPoliza = NumeroPoliza;
            this._MotivoConsultaTipo = MotivoConsultaTipo;
            this._Estado = Estado;

            this._HistoriaId = HistoriaId;
            this._MotivoConulta = MotivoConsulta;
            this._EnfermedadActual = EnfermedadActual;
            this._PresionArterial = PresionArterial;
            this._Pulso = Pulso;
            this._Temperatura = Temperatura;
            this._FrecuenciaCardiaca = FrecuenciaCardiaca;
            this._Talla = Talla;
            this._Peso = Peso;
            this._ExFisicoRegionalyDeSistema = ExFisicoRegionalyDeSistema;
            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._Enfermedad2Id = enfermedad2Id;
            this._Enfermedad2 = enfermedad2;
            this._Enfermedad3Id = enfermedad3Id;
            this._Enfermedad3 = enfermedad3;
            this._DiagnosticoPresuntivo = DiagnosticoPresuntivo;
            this._BiometriaHematica = BiometriaHematica;
            this._Observaciones = Observaciones;

            //SOLO EMERGENCIAS
            this._proveedorId = ProveedorId;
            this._NombreProveedorJuridico = Proveedor;
        }

        /* sin uso
        public Caso(int CasoId, string CodigoCaso, int Correlativo
            //, string CiudadId, DateTime FechaCreacion, int UserId, int PolizaId
            , string MotivoConsultaId, string Estado, bool Dirty
            , string Antecedentes, string AntecedentesAlergicos, string AntecedentesGinecoobstetricos
            , string MotivoConsulta
            , string PresionArterial, string Pulso, string Temperatura, string FrecuenciaCardiaca
            , string ExFisicoRegionalyDeSistema, string DiagnosticoPresuntivo, string enfermedadId
            , string enfermedad, string enfermedad2Id, string enfermedad2, string enfermedad3Id, string enfermedad3
            )
        {
            this._CasoId = CasoId;
            this._CodigoCaso = CodigoCaso;
            this._Correlativo = Correlativo;
            //this._CiudadId = CiudadId;
            //this._FechaCreacion = FechaCreacion);
            //this._UserId = UserId;
            //this._PolizaId = PolizaId;
            this._MotivoConsultaId = MotivoConsultaId;
            this._Estado = Estado;
            this._Dirty = Dirty;

            this._Antecedentes = Antecedentes;
            this._AntecedentesAlergicos = AntecedentesAlergicos;
            this._AntecedentesGinecoobstetricos = AntecedentesGinecoobstetricos;

            this._MotivoConulta = MotivoConsulta;
            this._PresionArterial = PresionArterial;
            this._Pulso = Pulso;
            this._Temperatura = Temperatura;
            this._FrecuenciaCardiaca = FrecuenciaCardiaca;
            this._ExFisicoRegionalyDeSistema = ExFisicoRegionalyDeSistema;
            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._Enfermedad2Id = enfermedad2Id;
            this._Enfermedad2 = enfermedad2;
            this._Enfermedad3Id = enfermedad3Id;
            this._Enfermedad3 = enfermedad3;
            this._DiagnosticoPresuntivo = DiagnosticoPresuntivo;
        }

        //search Emergencia
        public Caso(int CasoId, string CodigoCaso, int Correlativo
            , string CiudadId, DateTime FechaCreacion, int UserId
            , string UserName, int PolizaId
            , string MotivoConsultaId, string Estado
            , int PacienteId, string NombrePaciente
            , string CodigoAsegurado, string NombreProveedorJuridico
            , string enfermedadId, string enfermedad, string enfermedad2Id
            , string enfermedad2, string enfermedad3Id, string enfermedad3
            , string MotivoConsulta)
        {
            this._CasoId = CasoId;
            this._CodigoCaso = CodigoCaso;
            this._Correlativo = Correlativo;
            this._CiudadId = CiudadId;
            this._FechaCreacion = FechaCreacion;
            this._UserId = UserId;
            this._UserName = UserName;
            this._PolizaId = PolizaId;
            this._MotivoConsultaId = MotivoConsultaId;
            this._Estado = Estado;

            this._PacienteId = PacienteId;
            this._NombrePaciente = NombrePaciente;

            this._CodigoAsegurado = CodigoAsegurado;
            this._NombreProveedorJuridico = NombreProveedorJuridico;
            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._Enfermedad2Id = enfermedad2Id;
            this._Enfermedad2 = enfermedad2;
            this._Enfermedad3Id = enfermedad3Id;
            this._Enfermedad3 = enfermedad3;
            this._MotivoConulta = MotivoConsulta;
        }*/

        private string compareJSon(string JsonDef, string JsonData)
        {
            if (string.IsNullOrWhiteSpace(JsonData))
            {
                return JsonDef;
            }
            string JsonDataTemp = Regex.Replace(JsonData, ",\"value\":\"[^\"]+\"", ",\"value\":\"\"").Replace(", ", ",").Replace(": ", ":").Replace("{ ", "{").Replace(" }", "}");
            JsonDef = JsonDef.Replace(", ", ",").Replace(": ", ":").Replace("{ ", "{").Replace(" }", "}");
            if (!JsonDef.Equals(JsonDataTemp))
            {
                JsonData = JsonDef;
            }
            return JsonData;
        }

        public static byte[] GetHistorialEnPdf_EVO(EnlaceDesgravamenSISA objEnlace, string urlServer)
        {
            byte[] pdfBytes = null;
            string queryString = "PacienteId=" + objEnlace.PacienteId.ToString();
            //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
            //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/CasoMedico/HistorialPrint.aspx?" + queryString);
            urlToConvert = urlToConvert.Replace(" ", "%20");

            try
            {
                // Create the PDF converter. Optionally the HTML viewer width can be specified as parameter
                // The default HTML viewer width is 1024 pixels.
                PdfConverter pdfConverter = new PdfConverter();

                // set the license key - required
                pdfConverter.LicenseKey = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

                // set the converter options - optional
                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.Letter;
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;


                // set if header and footer are shown in the PDF - optional - default is false 
                pdfConverter.PdfDocumentOptions.ShowHeader = false;
                pdfConverter.PdfDocumentOptions.ShowFooter = false;
                // set if the HTML content is resized if necessary to fit the PDF page width - default is true
                pdfConverter.PdfDocumentOptions.FitWidth = true;

                // set the margins of the page
                pdfConverter.PdfDocumentOptions.TopMargin = 30;
                pdfConverter.PdfDocumentOptions.BottomMargin = 25;

                // set the embedded fonts option - optional - default is false
                pdfConverter.PdfDocumentOptions.EmbedFonts = false;
                // set the live HTTP links option - optional - default is true
                pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;

                pdfConverter.MediaType = "print";

                // set if the JavaScript is enabled during conversion to a PDF - default is true
                pdfConverter.JavaScriptEnabled = true;

                // set if the images in PDF are compressed with JPEG to reduce the PDF document size - default is true
                pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = true;

                // enable auto-generated bookmarks for a specified list of HTML selectors (e.g. H1 and H2)

                pdfConverter.PdfBookmarkOptions.HtmlElementSelectors = new string[] { "H1", "H2" };

                pdfConverter.NavigationTimeout = 3600;

                // Performs the conversion and get the pdf document bytes that can 
                // be saved to a file or sent as a browser response
                pdfBytes = pdfConverter.ConvertUrl(urlToConvert);
            }
            catch (Exception q)
            {
                log.Error("An error ocurred trying to generate the PDF report.", q);
                return null;
            }

            return pdfBytes;
        }
        public static byte[] GetHistorialEnPdf_HIQ(string urlToConvert)
        {
            byte[] pdfBytes = null;

            try
            {
                // create the HTML to PDF converter
                HiQPdf.HtmlToPdf htmlToPdfConverter = new HiQPdf.HtmlToPdf();

                // set a demo serial number
                htmlToPdfConverter.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
                htmlToPdfConverter.BrowserWidth = 1024;
                // set PDF page size and orientation
                htmlToPdfConverter.Document.PageSize = HiQPdf.PdfPageSize.Letter;
                htmlToPdfConverter.Document.PageOrientation = HiQPdf.PdfPageOrientation.Portrait;
                htmlToPdfConverter.Document.PdfStandard = HiQPdf.PdfStandard.Pdf;
                htmlToPdfConverter.Document.Margins = new HiQPdf.PdfMargins(25, 25, 10, 10);

                // set triggering mode; for WaitTime mode set the wait time before convert
                htmlToPdfConverter.TriggerMode = HiQPdf.ConversionTriggerMode.Auto;
                htmlToPdfConverter.MediaType = "hiqpdf";

                // convert HTML to PDF
                pdfBytes = htmlToPdfConverter.ConvertUrlToMemory(urlToConvert);
            }
            catch (Exception q)
            {
                log.Error("An error ocurred trying to generate the PDF report.", q);
                return null;
            }

            return pdfBytes;
        }

        public static byte[] GetHistorialEnPdf_HIQ(EnlaceDesgravamenSISA objEnlace, string urlServer)
        {

            string queryString = (objEnlace.ClienteDesgravamenId <= 0) ? "PacienteId=" + objEnlace.PacienteId.ToString() : 
                "PacienteId=" + objEnlace.PacienteId.ToString()+"&ClienteId="+objEnlace.ClienteDesgravamenId.ToString();
            //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
            //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
            string urlDesg = @"~/CasoMedico/HistorialPrintDESG.aspx?";
            string urlStandard = @"~/CasoMedico/HistorialPrint.aspx?";

            string urlABS = (objEnlace.ClienteDesgravamenId <= 0) ? urlStandard : urlDesg;
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute(urlABS + queryString);
            urlToConvert = urlToConvert.Replace(" ", "%20");

            return GetHistorialEnPdf_HIQ(urlToConvert);
        }

        public static byte[] GetHistorialEnPdf(EnlaceDesgravamenSISA objEnlace, string urlServer)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
                return GetHistorialEnPdf_EVO(objEnlace, urlServer);
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetHistorialEnPdf_HIQ(objEnlace, urlServer);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }

        public static byte[] GetHistorialEnPdf(string urlToConvert)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetHistorialEnPdf_HIQ(urlToConvert);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }
    }
}