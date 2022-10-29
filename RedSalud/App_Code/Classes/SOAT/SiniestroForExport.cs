using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Siniestro
{
    public class SiniestroForExport
    {
        private string dateFormat = "dd/MMM/yyyy";
        private string decimalFormat = "0,00";
        private int numberDecimal = 2;
        //Accidentado
        private string _nombre;
        private bool _genero;
        private DateTime _fechaNacimiento;
        private string _carnetIdentidad;
        private string _estadoCivil;
        private bool _esConductor;
        private bool _licenciaConducir;
        private char _tipoAccidentado;
        private bool _accidentado;
        //Poliza
        private string _operacionesId;
        private string _numeroRoseta;
        private string _numeroPoliza;
        private string _nombreTitular;
        private string _CITitular;
        //Siniestro
        private DateTime _FechaSiniestro;
        private DateTime _FechaDenuncia;
        private int _TotalOcupantes;
        private int _CantidadHeridos;
        private int _CantidadFallecidos;
        private string _LugarDpto;
        private string _LugarProvincia;
        private string _zona;
        private string _sindicato;
        private string _creador;
        //Vehiculo
        private string _tipo;
        private string _cilindrada;
        private string _sector;
        private string _placa;
        //Gestion Medica
        private string _nombreES;
        private DateTime _fechaVisita;
        private string _grado;
        private string _diagnosticoPreliminar;
        //Preliquidacion
        private decimal _reservas;
        private decimal _hospitalarios;
        private decimal _cirugia;
        private decimal _ambulancias;
        private decimal _laboratorios;
        private decimal _farmacia;
        private decimal _honorarios;
        private decimal _ambulatorios;
        private decimal _osteosintesis;
        private decimal _reembolso;
        private decimal _montoGestion;
        //Gastos
        private decimal _gastosHospitalarios;
        private decimal _gastosCirugia;
        private decimal _gastosAmbulancias;
        private decimal _gastosLaboratorios;
        private decimal _gastosFarmacia;
        private decimal _gastosHonorarios;
        private decimal _gastosAmbulatorios;
        private decimal _gastosOsteosintesis;
        private decimal _gastosReembolso;
        //Seguimiento
        private string _estado;
        private string _nombreAuditor;
        private DateTime _fechaAprobacion;
        private DateTime _fechaPago;
        private bool _acuerdo;
        private bool _rechazado;
        private string _observaciones;

        private string _TieneAdjuntos;
        public string TieneAdjuntos
        {
            get { return _TieneAdjuntos; }
            set { _TieneAdjuntos = value; }
        }
        public int _rowNumber;

        public int RowNumber
        {
            get { return _rowNumber; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Genero
        {
            get { return _genero ? "MASCULINO" : "FEMENINO"; }
        }

        public string Edad
        {
            get
            {
                if (_fechaNacimiento <= SqlDateTime.MinValue)
                {
                    return "-";
                }
                DateTime now = DateTime.Now;
                int age = now.Year - _fechaNacimiento.Year;
                if (now.Month < _fechaNacimiento.Month || (now.Month == _fechaNacimiento.Month && now.Day < _fechaNacimiento.Day))
                    age--;
                return age + " AÑOS"; 
            }
        }

        public string FechaNacimiento
        {
            get {
                if (_fechaNacimiento <= SqlDateTime.MinValue)
                {
                    return "-";
                }
                return _fechaNacimiento.ToString(dateFormat); 
            }
        }

        public string CarnetIdentidad
        {
            get { return _carnetIdentidad; }
            set { _carnetIdentidad = value; }
        }

        public string EstadoCivil
        {
            get { return _estadoCivil; }
            set { _estadoCivil = value; }
        }

        public string LicenciaConducir
        {
            get { return _licenciaConducir ? "SI" : "NO"; }
            set { _licenciaConducir = (value.ToUpper() == "SI"); }
        }

        public string EsConductor
        {
            get { return _esConductor ? "SI" : "NO"; }
            set { _esConductor = (value.ToUpper() == "SI"); }
        }

        public string TipoAccidentado
        {
            get { return _tipoAccidentado == 'A' ? "PASAJERO" : "PEATÓN"; }
            set { _tipoAccidentado = value.ToCharArray()[1]; }
        }

        public string EstadoAccidentado
        {
            get { return _accidentado ? "ACCIDENTADO" : "FALLECIDO"; }
        }

        public string IdentificadorOperaciones
        {
            set { _operacionesId = value; }
            get { return _operacionesId; }
        }

        public string NumeroRoseta
        {
            get { return _numeroRoseta; }
            set { _numeroRoseta = value; }
        }

        public string NumeroPoliza
        {
            get { return _numeroPoliza; }
            set { _numeroPoliza = value; }
        }

        public string NombreTitular
        {
            get { return _nombreTitular; }
            set { _nombreTitular = value; }
        }

        public string CITitular
        {
            get { return _CITitular; }
            set { _CITitular = value; }
        }

        public string FechaSiniestro
        {
            get { 
                return this._FechaSiniestro.ToString(dateFormat); 
            }
        }
        public string Dia
        {
            get { return this._FechaSiniestro.ToString("dddd").ToUpper(); }
        }
        public string Mes
        {
            get { return this._FechaSiniestro.ToString("MMMM").ToUpper(); }
        }
        public string Semana
        {
            get {
                System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
                return cul.Calendar.GetWeekOfYear( DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday).ToString(); 
            }
        }
        public string Gestion
        {
            get { return this._FechaSiniestro.ToString("yyyy"); }
        }
        public string FechaDenuncia
        {
            get
            {
                if (_FechaDenuncia <= SqlDateTime.MinValue)
                {
                    return "-";
                } 
                return this._FechaDenuncia.ToString(dateFormat);
            }
        }
        public int TotalOcupantes
        {
            get { return this._TotalOcupantes; }
            set { this._TotalOcupantes = value; }
        }
        public int CantidadHeridos
        {
            get { return this._CantidadHeridos; }
            set { this._CantidadHeridos = value; }
        }
        public int CantidadFallecidos
        {
            get { return this._CantidadFallecidos; }
            set { this._CantidadFallecidos = value; }
        }
        public string LugarDpto
        {
            get { return this._LugarDpto; }
            set { this._LugarDpto = value; }
        }
        public string LugarProvincia
        {
            get { return this._LugarProvincia; }
            set { this._LugarProvincia = value; }
        }

        public string Zona
        {
            get { return this._zona; }
            set { this._zona = value; }
        }
        public string Sindicato
        {
            get { return this._sindicato; }
            set { this._sindicato = value; }
        }
        public string Creador
        {
            get { return this._creador; }
            set { this._creador = value; }
        }

        public string TipoVehiculo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string Cilindrada
        {
            get { return _cilindrada; }
            set { _cilindrada = value; }
        }

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public string NombreES
        {
            get { return _nombreES; }
            set { _nombreES = value; }
        }

        public string FechaVisita
        {
            get
            {
                if (_fechaVisita <= SqlDateTime.MinValue)
                {
                    return "-";
                } 
                return _fechaVisita.ToString(dateFormat);
            }
        }

        public string Grado
        {
            get { return _grado; }
            set { _grado = value; }
        }

        public string DiagnosticoPreliminar
        {
            get { return _diagnosticoPreliminar; }
            set { _diagnosticoPreliminar = value; }
        }

        public decimal Reservas
        {
            get { return Math.Round(_reservas, numberDecimal); }
        }

        public decimal Hospitalarios
        {
            get { return Math.Round(_hospitalarios, numberDecimal); }
        }

        public decimal Cirugia
        {
            get { return Math.Round(_cirugia, numberDecimal); }
        }

        public decimal Ambulancias
        {
            get { return Math.Round(_ambulancias, numberDecimal); }
        }

        public decimal Laboratorios
        {
            get { return Math.Round(_laboratorios, numberDecimal); }
        }

        public decimal Farmacia
        {
            get { return Math.Round(_farmacia, numberDecimal); }
        }

        public decimal Honorarios
        {
            get { return Math.Round(_honorarios, numberDecimal); }
        }

        public decimal Ambulatorios
        {
            get { return Math.Round(_ambulatorios, numberDecimal); }
        }

        public decimal Osteosintesis
        {
            get { return Math.Round(_osteosintesis, numberDecimal); }
        }

        public decimal Reembolso
        {
            get { return Math.Round(_reembolso, numberDecimal); }
        }

        public decimal SiniestrosPreliquidacion
        {
            get
            {
                return Math.Round(this._hospitalarios +
                this._cirugia +
                this._ambulancias +
                this._laboratorios +
                this._farmacia +
                this._honorarios +
                this._ambulatorios +
                this._osteosintesis +
                this._reembolso, numberDecimal);
            }
        }

        public decimal GastosHospitalarios
        {
            get { return Math.Round(_gastosHospitalarios, numberDecimal); }
        }

        public decimal GastosCirugia
        {
            get { return Math.Round(_gastosCirugia, numberDecimal); }
        }

        public decimal GastosAmbulancias
        {
            get { return Math.Round(_gastosAmbulancias, numberDecimal); }
        }

        public decimal GastosLaboratorios
        {
            get { return Math.Round(_gastosLaboratorios, numberDecimal); }
        }

        public decimal GastosFarmacia
        {
            get { return Math.Round(_gastosFarmacia, numberDecimal); }
        }

        public decimal GastosHonorarios
        {
            get { return Math.Round(_gastosHonorarios, numberDecimal); }
        }

        public decimal GastosAmbulatorios
        {
            get { return Math.Round(_gastosAmbulatorios, numberDecimal); }
        }

        public decimal GastosOsteosintesis
        {
            get { return Math.Round(_gastosOsteosintesis, numberDecimal); }
        }

        public decimal GastosReembolso
        {
            get { return Math.Round(_gastosReembolso, numberDecimal); }
        }

        public decimal SiniestrosPagados
        {
            get
            {
                return Math.Round(this._gastosHospitalarios +
                this._gastosCirugia +
                this._gastosAmbulancias +
                this._gastosLaboratorios +
                this._gastosFarmacia +
                this._gastosHonorarios +
                this._gastosAmbulatorios +
                this._gastosOsteosintesis +
                this._gastosReembolso, numberDecimal);
            }
        }

        public decimal SaldoAFavor
        {
            get
            {
                decimal totalPagado = (this._gastosHospitalarios +
                this._gastosCirugia +
                this._gastosAmbulancias +
                this._gastosLaboratorios +
                this._gastosFarmacia +
                this._gastosHonorarios +
                this._gastosAmbulatorios +
                this._gastosOsteosintesis +
                this._gastosReembolso);
                return Math.Round(_montoGestion - totalPagado, numberDecimal);
            }
        }

        public decimal Ahorro
        {
            get {
                decimal totalPagado = (this._gastosHospitalarios +
                this._gastosCirugia +
                this._gastosAmbulancias +
                this._gastosLaboratorios +
                this._gastosFarmacia +
                this._gastosHonorarios +
                this._gastosAmbulatorios +
                this._gastosOsteosintesis +
                this._gastosReembolso);
                decimal totalPreliquidacion = (this._hospitalarios +
                this._cirugia +
                this._ambulancias +
                this._laboratorios +
                this._farmacia +
                this._honorarios +
                this._ambulatorios +
                this._osteosintesis+
                this._reembolso);
                return Math.Round(totalPagado - totalPreliquidacion, numberDecimal); 
            }
        }

        public string ControlSaldos
        {
            get
            {
                decimal totalPagado = (this._gastosHospitalarios +
                this._gastosCirugia +
                this._gastosAmbulancias +
                this._gastosLaboratorios +
                this._gastosFarmacia +
                this._gastosHonorarios +
                this._gastosAmbulatorios +
                this._gastosOsteosintesis +
                this._gastosReembolso);
                decimal saldo = _montoGestion - totalPagado;
                if (saldo < 0)
                    return "EXCEDE  LIMITE SOAT";
                else if (saldo > 0)
                    return "AUN TIENE SALDO";
                else
                    return "-";
            }
        }
        public string EstadoSeguimiento
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string NombreAuditor
        {
            get { return _nombreAuditor; }
            set { _nombreAuditor = value; }
        }

        public string FechaAprobacion
        {
            get
            {
                if (_fechaAprobacion <= SqlDateTime.MinValue)
                {
                    return "-";
                } 
                return _fechaAprobacion.ToString(dateFormat);
            }
        }

        public string DiasDesdeAprobacion
        {
            get
            {
                if (_fechaAprobacion <= SqlDateTime.MinValue)
                {
                    return "-";
                } 
                TimeSpan dayDiff = _FechaSiniestro - _fechaAprobacion;
                return dayDiff.Days + " DÍAS";
            }
        }

        public string FechaPago
        {
            get
            {
                if (_fechaPago <= SqlDateTime.MinValue)
                {
                    return "-";
                } 
                return _fechaPago.ToString(dateFormat);
            }
        }

        public string DiasDesdePago
        {
            get
            {
                if (_fechaPago <= SqlDateTime.MinValue)
                {
                    return "-";
                } 
                TimeSpan dayDiff = _FechaSiniestro - _fechaPago;
                return dayDiff.Days + " DÍAS";
            }
        }

        public string Acuerdo
        {
            get { return _acuerdo ? "SI" : "NO"; }
        }
        public string Rechazado
        {
            get { return _rechazado ? "SI" : "NO"; }
        }

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public string NroChasis { get; set; }
        public string NroMotor { get; set; }

        public SiniestroForExport ( int rowNumber, string nombre, bool genero, DateTime fechaNacimiento, string estadoCivil, string carnetIdentidad,
            bool esConductor, bool licenciaConducir, string tipoAccidentado, bool estadoAccidentado, 
            string operacionesId,string medicoCreador, string numeroRoseta, string numeroPoliza, string nombreTitular, string CITitular, 
            DateTime fechaSiniestro, DateTime fechaDenuncia, int totalOcupantes, int cantidadHeridos, int cantidadFallecidos,
            string lugarDpto, string lugarProvincia, string zona, string sindicato,
            string tipo, string cilindrada, string nroChasis, string nroMotor, string sector, string placa, 
            string nombreES, DateTime fechaVisita, string grado, string diagnosticoPreliminar,
            decimal montoGestion, decimal reservas, decimal hospitalarios, decimal cirujia, decimal ambulancias, decimal laboratorios,
            decimal farmacia, decimal honorarios, decimal ambulatorios, decimal osteosintesis, decimal reembolso,
            decimal gastosHospitalarios, decimal gastosCirujia, decimal gastosAmbulancias, decimal gastosLaboratorios,
            decimal gastosFarmacia, decimal gastosHonorarios, decimal gastosAmbulatorios, decimal gastosOsteosintesis, decimal gastosReembolso,
            string estado, bool acuerdo, bool rechazado, string observaciones, string tieneAdjuntos)
        {
            this._rowNumber = rowNumber;

            this._nombre = nombre;
            this._carnetIdentidad = carnetIdentidad;
            this._genero = genero;
            this._fechaNacimiento = fechaNacimiento;
            this._estadoCivil = estadoCivil;
            this._esConductor = esConductor;
            this._licenciaConducir = licenciaConducir;
            this._tipoAccidentado = tipoAccidentado.ToCharArray()[0];
            this._accidentado = estadoAccidentado;

            this._operacionesId = operacionesId;
            this._creador = medicoCreador;
            this._numeroRoseta = numeroRoseta;
            this._numeroPoliza = numeroPoliza;
            this._nombreTitular = nombreTitular;
            this._CITitular = CITitular;

            this._FechaSiniestro = fechaSiniestro;
            this._FechaDenuncia = fechaDenuncia;
            this._TotalOcupantes = totalOcupantes;
            this._CantidadHeridos = cantidadHeridos;
            this._CantidadFallecidos = cantidadFallecidos;
            this._LugarDpto = lugarDpto;
            this._LugarProvincia = lugarProvincia;
            this._zona = zona;
            this._sindicato = sindicato;

            this._tipo = tipo;
            this._cilindrada = cilindrada;
            this._sector = sector;
            this._placa = placa;

            this._nombreES = nombreES;
            this._fechaVisita = fechaVisita;
            this._grado = grado;
            this._diagnosticoPreliminar = diagnosticoPreliminar;

            this._montoGestion = montoGestion;
            this._reservas = reservas;
            this._hospitalarios = hospitalarios;
            this._cirugia = cirujia;
            this._ambulancias = ambulancias;
            this._laboratorios = laboratorios;
            this._farmacia = farmacia;
            this._honorarios = honorarios;
            this._ambulatorios = ambulatorios;
            this._osteosintesis = osteosintesis;
            this._reembolso = reembolso;

            this._gastosHospitalarios = gastosHospitalarios;
            this._gastosCirugia = gastosCirujia;
            this._gastosAmbulancias = gastosAmbulancias;
            this._gastosLaboratorios = gastosLaboratorios;
            this._gastosFarmacia = gastosFarmacia;
            this._gastosHonorarios = gastosHonorarios;
            this._gastosAmbulatorios = gastosAmbulatorios;
            this._gastosOsteosintesis = gastosOsteosintesis;
            this._gastosReembolso = gastosReembolso;

            this._estado = estado;
            this._acuerdo = acuerdo;
            this._rechazado = rechazado;
            this._observaciones = observaciones;
            this._TieneAdjuntos = tieneAdjuntos;
            this.NroChasis = nroChasis;
            this.NroMotor = nroMotor;
        }
    }
}