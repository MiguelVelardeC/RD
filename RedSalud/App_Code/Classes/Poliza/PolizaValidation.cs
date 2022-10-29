using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PolizaValidation
/// </summary>
/// 
namespace Artexacta.App.Poliza
{
    public class PolizaValidation
    {
        #region "Variables"
        private int _PolizaId;
        private int _AseguradoId;
        private int _ClienteId;
        private int _PacienteId;
        private string _NumeroPoliza;
        private DateTime _FechaInicio;
        private DateTime _FechaFin;
        private bool _VigenciaPoliza;
        private string _EstadoPoliza;
        private string _Codigocaso;
        private string _Relacion;
        private string _NombrePaciente;
        private string _CarnetIdentidad;
        private string _NombreCliente;
        #endregion
        #region "Propiedades"

        public int PolizaId
        {
            set { this._PolizaId = value; }
            get { return this._PolizaId; }
        }
        public int AseguradoId
        {
            get { return this._AseguradoId; }
            set { this._AseguradoId = value; }
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
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value; }
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
        public bool VigenciaPoliza
        {
            get { return this._VigenciaPoliza; }
            set { this._VigenciaPoliza = value; }
        }
        public string EstadoPoliza
        {
            get { return this._EstadoPoliza; }
            set { this._EstadoPoliza = value; }
        }
        public string Codigocaso
        {
            get { return this._Codigocaso; }
            set { this._Codigocaso = value; }
        }
        public string Relacion
        {
            get { return this._Relacion; }
            set { this._Relacion = value; }
        }
        public string NombrePaciente
        {
            get { return this._NombrePaciente; }
            set { this._NombrePaciente = value; }
        }
        public string CarnetId
        {
            get { return this._CarnetIdentidad; }
            set { this._CarnetIdentidad = value; }
        }
        public string NombreCliente
        {
            get { return this._NombreCliente; }
            set { this._NombreCliente = value; }
        }

        #endregion
        public PolizaValidation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public PolizaValidation(int PolizaId, int AseguradoId, int ClienteId, int PacienteId, string NumeroPoliza, DateTime FechaInicio
         , DateTime FechaFin, int VigenciaPoliza, string Codigocaso, string Relacion, string NombrePaciente, string CarnetIdentidad, string NombreCliente,string EstadoPoliza)
        {
            _PolizaId = PolizaId;
            _AseguradoId = AseguradoId;
            _ClienteId = ClienteId;
            _PacienteId = PacienteId;
            _NumeroPoliza = NumeroPoliza;
            _FechaInicio = FechaInicio;
            _FechaFin = FechaFin;
            _Codigocaso = Codigocaso;
            _Relacion = Relacion;
            _NombrePaciente = NombrePaciente;
            _CarnetIdentidad = CarnetIdentidad;
            _NombreCliente = NombreCliente;
            _EstadoPoliza = EstadoPoliza;
            if (VigenciaPoliza == 1)
                _VigenciaPoliza = true;
            else
                _VigenciaPoliza = false;
        }
    }
}