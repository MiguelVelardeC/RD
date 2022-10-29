using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Siniestralidad
/// </summary>
namespace Artexacta.App.Siniestralidad
{
    public class SiniestralidadCliente
    {
        #region "Variables"
        private int _ClienteId;
        private string _CodigoCliente;
        private string _NombreJuridico;
        private string _Nit;
        private string _Direccion;
        private int _RowNumber;
        #endregion

        #region "Metodos"
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public string CodigoCliente
        {
            get { return this._CodigoCliente; }
            set { this._CodigoCliente = value.ToUpper(); }
        }
        public string NombreJuridico
        {
            get { return this._NombreJuridico; }
            set { this._NombreJuridico = value.ToUpper(); }
        }
        public string Nit
        {
            get { return this._Nit; }
            set { this._Nit = value.ToUpper(); }
        }
        public string Direccion
        {
            get { return this._Direccion; }
            set { this._Direccion = value.ToUpper(); }
        }
        public int RowNumber
        {
            get { return this._RowNumber; }
            set { this._RowNumber = value; }
        }
        #endregion

        //reporte de siniestralidad
        public SiniestralidadCliente(int ClienteId, string CodigoCliente, string NombreJuridico, string Nit, string Direccion, int RowNumber)
        {
            this._ClienteId = ClienteId;
            this._CodigoCliente=CodigoCliente;
            this._NombreJuridico=NombreJuridico;
            this._Nit=Nit;
            this._Direccion=Direccion;
            this._RowNumber=RowNumber;
    }
        //
        // TODO: Add constructor logic here
        //
    }
}