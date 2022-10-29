using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Poliza
{
    /// <summary>
    /// Summary description for Poliza
    /// </summary>
    public class Poliza
    {
        private int _PolizaId;
        private string _NumeroPoliza;
        private int _AseguradoId;
        private DateTime _FechaInicio;
        private DateTime _FechaFin;
        private decimal _MontoTotal;
        private decimal _MontoFarmacia;
        private string _Cobertura;
        private string _NombrePlan;
        private string _Lugar;
        private string _Estado;
        private DateTime _FechaEstado;

        private string _CodigoAsegurado;
        private int _ClienteId;
        private int _PacienteId;
        private string _Relacion;
        private string _NombreCompletoPaciente;
        private string _CI;
        private decimal _GastoTotal;
        private string _NombreJuridicoCliente;
        private int _CasoId;
        private bool _CasoCritico;
        private int _RowNumber;

        #region Poliza

        public int PolizaId
        {
            get { return this._PolizaId; }
            set { this._PolizaId = value; }
        }
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value; }
        }
        public int AseguradoId
        {
            get { return this._AseguradoId; }
            set { this._AseguradoId = value; }
        }
        public string Relacion
        {
            get { return this._Relacion; }
            set { this._Relacion = value; }
        }
        public DateTime FechaInicio
        {
            get { return this._FechaInicio; }
            set { this._FechaInicio = value; }
        }
        public DateTime FechaFin
        {
            get { return this._FechaFin; }
            set { this._FechaFin = value; }
        }
        public decimal MontoTotal
        {
            get { return this._MontoTotal; }
            set { this._MontoTotal = value; }
        }
        public string MontoTotalForDisplay
        {
            get { return (this._MontoTotal < 0 ? 0 : this._MontoTotal).ToString("0.00"); }
        }
        public decimal MontoFarmacia
        {
            get { return this._MontoFarmacia; }
            set { this._MontoFarmacia = value; }
        }
        public string MontoFarmaciaForDisplay
        {
            get { return this._MontoFarmacia.ToString("0.00"); }
        }
        public string Cobertura
        {
            get { return this._Cobertura; }
            set { this._Cobertura = value; }
        }
        public string NombrePlan
        {
            get { return this._NombrePlan; }
            set { this._NombrePlan = value; }
        }
        public string Lugar
        {
            get { return string.IsNullOrWhiteSpace(this._Lugar) ? "SCZ" : this._Lugar; }
            set { this._Lugar = value; }
        }
        public string LugarForDisplay
        {
            get
            {
                switch (_Lugar)
                {
                    case "LPZ":
                        this._Lugar = "LA PAZ";
                        break;
                    case "CBBA":
                        this._Lugar = "COCHABAMBA";
                        break;
                    default:
                        this._Lugar = "SANTA CRUZ";
                        break;
                }
                return this._Lugar;

            }
        }
        public string Estado
        {
            get
            {
                return this._Estado.ToLower() == "inactivo" ? this._Estado.ToUpper() : "ACTIVO";
            }
            set { this._Estado = value; }
        }
        public DateTime FechaEstado
        {
            get { return this._FechaEstado; }
            set { this._FechaEstado = value; }
        }
        #endregion

        public string CodigoAsegurado
        {
            get { return this._CodigoAsegurado; }
        }
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public int PacienteId
        {
            get { return this._PacienteId; }
            set { this._PacienteId = value; }
        }
        public string NombreCompletoPaciente
        {
            set { this._NombreCompletoPaciente = value; }
            get { return this._NombreCompletoPaciente; }
        }
        public string CI
        {
            set { this._CI = value; }
            get { return this._CI; }
        }
        public decimal GastoTotal
        {
            get { return this._GastoTotal; }
        }
        public bool CasoCritico
        {
            get { return _CasoCritico; }
            set { _CasoCritico = value; }
        }
        public int RowNumber
        {
            get { return _RowNumber; }
            set { _RowNumber = value; }
        }
        public int CasoId
        {
            get { return _CasoId; }
            set { _CasoId = value; }
        }
        public string GastoTotalForDisplay
        {
            get { return this._GastoTotal.ToString("#,##0.00"); }
        }
        public string FechaInicioString
        {
            get { return _FechaInicio.ToShortDateString(); }
        }
        public string FechaFinString
        {
            get { return _FechaFin.ToShortDateString(); }
        }
        public string Siniestralidad
        {
            get
            {
                decimal Porcentaje = _GastoTotal * 100 / _MontoTotal;
                return GastoTotalForDisplay + " Bs / " + MontoTotalForDisplay + " Bs (" + Porcentaje.ToString("#,##0.00") + "%)";
            }
        }

        public string NombreJuridicoCliente
        {
            set { this._NombreJuridicoCliente= value.ToUpper(); }
            get { return this._NombreJuridicoCliente; }
        }

        public Poliza()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Poliza(int PolizaId, string NumeroPoliza, int AseguradoId
            , DateTime FechaInicio
            , DateTime FechaFin, decimal MontoTotal, decimal MontoFarmacia
            , string Cobertura, string NombrePlan, string lugar, string Estado
            , string CodigoAsegurado, int ClienteId
            , int PacienteId, string Relacion, string NombreCompletoPaciente
            , string CI, string NombreJuridicoCliente, decimal GastoTotal)
        {
            this._PolizaId = PolizaId;
            this._NumeroPoliza = NumeroPoliza;
            this._AseguradoId = AseguradoId;
            this._FechaInicio = FechaInicio;
            this._FechaFin = FechaFin;
            this._MontoTotal = MontoTotal;
            this._MontoFarmacia = MontoFarmacia;
            this._Cobertura = Cobertura;
            this._NombrePlan = NombrePlan;
            this._Lugar = lugar;
            this._Estado = Estado;

            this._CodigoAsegurado = CodigoAsegurado;
            this._ClienteId = ClienteId;
            this._PacienteId = PacienteId;
            this._Relacion = Relacion;
            this._NombreCompletoPaciente = NombreCompletoPaciente;
            this._CI = CI;

            this._NombreJuridicoCliente = NombreJuridicoCliente;
            this._GastoTotal = GastoTotal;
        }
    }
}