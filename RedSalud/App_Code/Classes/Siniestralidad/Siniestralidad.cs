using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Siniestralidad
/// </summary>
namespace Artexacta.App.Siniestralidad
{
    public class Siniestralidad
    {
        #region "Variables"
        private int _ClienteId;
        private string _NombreCliente;
        private string _NumeroPoliza;
        private string _NombrePaciente;
        private string _CedulaIdentidad;
        private string _Relacion;
        private DateTime _FechaIni;
        private DateTime _FechaFin;
        private int _CasoId;
        private int _RowNumber;
        #endregion

        #region "Metodos"
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public string NombreCliente
        {
            get { return this._NombreCliente; }
            set { this._NombreCliente = value.ToUpper(); }
        }
        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value.ToUpper(); }
        }
        public string NombrePaciente
        {
            get { return this._NombrePaciente; }
            set { this._NombrePaciente = value.ToUpper(); }
        }
        public string CedulaIdentidad
        {
            get { return this._CedulaIdentidad; }
            set { this._CedulaIdentidad = value.ToUpper(); }
        }
        public string Relacion
        {
            get { return this._Relacion; }
            set { this._Relacion = value.ToUpper(); }
        }
        public DateTime FechaIni
        {
            get { return this._FechaIni; }
            set { this._FechaIni = value; }
        }
        public DateTime FechaFin
        {
            get { return this._FechaFin; }
            set { this._FechaFin = value; }
        }
        public int RowNumber
        {
            get { return this._RowNumber; }
            set { this._RowNumber = value; }
        }
        #endregion

        //reporte de siniestralidad
        public Siniestralidad(int CasoId, string NombrePaciente, int ClienteId, string NombreCliente, string NumeroPoliza, string CedulaIdentidad, string Relacion, DateTime FechaIni, DateTime FechaFin, int RowNumber)
        {
            this._ClienteId = ClienteId;
            this._NombreCliente=NombreCliente;
            this._NumeroPoliza=NumeroPoliza;
            this._NombrePaciente=NombrePaciente;
            this._CedulaIdentidad=CedulaIdentidad;
            this._Relacion = Relacion;
            this._FechaIni = FechaIni;
            this._FechaFin = FechaFin;
            this._CasoId=CasoId;
            this._RowNumber=RowNumber;
    }
        //
        // TODO: Add constructor logic here
        //
    }
}