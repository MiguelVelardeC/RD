using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.Reportes
{
    /// <summary>
    /// Summary description for RedMedica
    /// </summary>
    public class Enfermeria
    {
        private string _decimalFormat = "#,##0.00";

        private string _NombreCliente;
        private string _CodigoCaso;
        private string _Medico;
        private string _TipoConsulta;
        private string _MotivoConsulta;
        private string _Observaciones;
        private string _Tratamiento;
        private string _Enfermedad;
        private string _Enfermedad2;
        private string _Enfermedad3;
        private string _DiagnosticoPresuntivo;
        private DateTime _FechaCaso;
        private string _Ciudad;
        private string _Nombre;
        private DateTime _FechaNacimiento;
        private string _Genero;
        private string _CasoCritico;
        private string _NumeroPoliza;
        private string _NombrePlan;
        private string _Medicamento;
        private string _Presentacion;
        private string _Indicaciones;
        private string _TipoDocumento;
		private string _NroFacturaRecibo;
        private DateTime _FechaCreacion;
        private DateTime _FechaGasto;
		private decimal _Monto;
		private int _RowNumber;

        public Enfermeria ( string NombreCliente, string CodigoCaso, string Medico, string TipoConsulta,string MotivoConsulta, 
            string Observaciones, string Tratamiento,string Enfermedad, string Enfermedad2, string Enfermedad3,string DiagnosticoPresuntivo, 
            DateTime FechaCaso, string Ciudad, string Nombre, DateTime FechaNacimiento, string Genero, string CasoCritico, 
            string NumeroPoliza, string NombrePlan, string Medicamento, string Presentacion, string Indicaciones, string TipoDocumento, 
            string NroFacturaRecibo, DateTime FechaCreacion, DateTime FechaGasto, decimal Monto, int RowNumber )
        {
            this.NombreCliente = NombreCliente;
            this.CodigoCaso = CodigoCaso;
            this.Medico = Medico;
            this.TipoConsulta = TipoConsulta;
            this.MotivoConsulta = MotivoConsulta;
            this.Observaciones = Observaciones.StartsWith("[{") ? "" : Observaciones;
            this.Tratamiento = Tratamiento;
            this.Enfermedad = Enfermedad;
            this.Enfermedad2 = Enfermedad2;
            this.Enfermedad3 = Enfermedad3;
            this.DiagnosticoPresuntivo = DiagnosticoPresuntivo;
            this.FechaCaso = FechaCaso;
            this.Ciudad = Ciudad;
            this.Nombre = Nombre;
            this.FechaNacimiento = FechaNacimiento;
            this.Genero = Genero;
            this.CasoCritico = CasoCritico;
            this.NumeroPoliza = NumeroPoliza;
            this.NombrePlan = NombrePlan;
            this.Medicamento = Medicamento;
            this.Presentacion = Presentacion;
            this.Indicaciones = Indicaciones;
            this.TipoDocumento = TipoDocumento;
            this.NroFacturaRecibo = NroFacturaRecibo;
            this.FechaCreacion = FechaCreacion;
            this.FechaGasto = FechaGasto;
            this.Monto = Monto;
            this.RowNumber = RowNumber;
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
        public string Medico
        {
            get { return this._Medico; }
            set { this._Medico = value.ToUpper(); }
        }
        public string TipoConsulta
        {
            get { return this._TipoConsulta; }
            set { this._TipoConsulta = value.ToUpper(); }
        }
        public string MotivoConsulta
        {
            get { return this._MotivoConsulta; }
            set { this._MotivoConsulta = value.ToUpper(); }
        }
        public string Observaciones
        {
            get { return this._Observaciones; }
            set { this._Observaciones = value.ToUpper(); }
        }
        public string Tratamiento
        {
            get { return this._Tratamiento; }
            set { this._Tratamiento = value.ToUpper(); }
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
            get
            {
                if (!string.IsNullOrEmpty(Enfermedad))
                    return Enfermedad + "<br />" +
                           Enfermedad2 + "<br />" +
                           Enfermedad3;
                else
                    return this._DiagnosticoPresuntivo;
            }
        }
        public DateTime FechaCaso
        {
            get { return this._FechaCaso; }
            set { this._FechaCaso = value; }
        }
        public string Ciudad
        {
            get { return this._Ciudad; }
            set { this._Ciudad = value.ToUpper(); }
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
        public string Nombre
        {
            get { return this._Nombre; }
            set { this._Nombre = value.ToUpper(); }
        }
        public DateTime FechaNacimiento
        {
            get { return this._FechaNacimiento; }
            set { this._FechaNacimiento = value; }
        }
        public string Medicamento
        {
            get { return this._Medicamento; }
            set { this._Medicamento = value.ToUpper(); }
        }
        public string Presentacion
        {
            get { return this._Presentacion; }
            set { this._Presentacion = value.ToUpper(); }
        }
        public string Indicaciones
        {
            get { return this._Indicaciones; }
            set { this._Indicaciones = value.ToUpper(); }
        }
        public string TipoDocumento
        {
            get { return this._TipoDocumento; }
            set { this._TipoDocumento = value.ToUpper(); }
        }
        public string NroFacturaRecibo
        {
            get { return this._NroFacturaRecibo; }
            set { this._NroFacturaRecibo = value.ToUpper(); }
        }
        public DateTime FechaCreacion
        {
            get { return this._FechaCreacion; }
            set { this._FechaCreacion = value; }
        }
        public string FechaCreacionToDisplay
        {
            get
            {
                if (this._FechaCreacion == SqlDateTime.MinValue.Value)
                    return "";
                else
                    return this._FechaCreacion.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
        }
        public DateTime FechaGasto
        {
            get { return this._FechaGasto; }
            set { this._FechaGasto = value; }
        }
        public string FechaGastoToDisplay
        {
            get
            {
                if (this._FechaGasto == SqlDateTime.MinValue.Value)
                    return "";
                else
                    return this._FechaGasto.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
        }
        public decimal Monto
        {
            get { return this._Monto; }
            set { this._Monto = value; }
        }
        public int RowNumber
        {
            get { return this._RowNumber; }
            set { this._RowNumber = value; }
        }
        public string MontoToDisplay
        {
            get { return this.Monto.ToString(_decimalFormat); }
        }

        public int EdadCalculadaForDisplay
        {
            get
            {
                int Edad = 0;
                DateTime birthday = FechaNacimiento;

                if (birthday == null || birthday == DateTime.MinValue)
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