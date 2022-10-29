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
    public class Farmacia
    {
        private string _decimalFormat = "#,##0.00";

        private string _NombreCliente;
        private string _CodigoCaso;
        private string _Medico;
        private string _TipoConsulta;
        private DateTime _FechaCaso;
        private string _Ciudad;
        private string _Nombre;
        private DateTime _FechaNacimiento;
        private string _Genero;
        private string _CasoCritico;
        private string _NumeroPoliza;
        private string _NombrePlan;
        private string _Medicamento;
        private bool _IsLiname;
		private string _Presentacion;
        private string _Concentracion;
        private string _Indicaciones;
        private string _TipoDocumento;
		private string _NroFacturaRecibo;
        private DateTime _FechaCreacion;
        private DateTime _FechaGasto;
		private decimal _Monto;
		private int _RowNumber;

        public Farmacia ( string NombreCliente, string CodigoCaso, string Medico, string TipoConsulta, DateTime FechaCaso, string Ciudad,
            string Nombre, DateTime FechaNacimiento, string Genero, string CasoCritico, string NumeroPoliza, string NombrePlan, 
            string Medicamento, bool IsLiname, string Presentacion, string Concentracion, string Indicaciones, string TipoDocumento, 
            string NroFacturaRecibo, DateTime FechaCreacion, DateTime FechaGasto, decimal Monto, int RowNumber )
        {
            this.NombreCliente = NombreCliente; 
            this.CodigoCaso = CodigoCaso;
            this.Medico = Medico;
            this.TipoConsulta = TipoConsulta;
            this.FechaCaso = FechaCaso;
            this.Ciudad = Ciudad;
            this.Nombre = Nombre;
            this.FechaNacimiento = FechaNacimiento;
            this.Genero = Genero;
            this.CasoCritico = CasoCritico;
            this.NumeroPoliza = NumeroPoliza;
            this.NombrePlan = NombrePlan;
            this.Medicamento = Medicamento;
            this.IsLiname = IsLiname;
            this.Presentacion = Presentacion;
            this.Concentracion = Concentracion;
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
        public bool IsLiname
        {
            get { return this._IsLiname; }
            set { this._IsLiname = value; }
        }
        public string IsLinameForDisplay
        {
            get { return this._IsLiname ? "SI" : "NO"; }
        }
        public string Presentacion
        {
            get { return this._Presentacion; }
            set { this._Presentacion = value.ToUpper(); }
        }
        public string Concentracion
        {
            get { return this._Concentracion; }
            set { this._Concentracion = value.ToUpper(); }
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
            get {
                if (this._FechaGasto == SqlDateTime.MinValue.Value)
                    return "";
                else
                    return this._FechaGasto.ToString("dd/MM/yyyy"); 
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
    }
}