using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.RedCliente
{
    /// <summary>
    /// Summary description for RedCliente
    /// </summary>
    public class RedCliente
    {
        private int _ClienteId;
        private string _CodigoCliente;
        private string _NombreJuridico;
        private decimal _Nit;
        private string _Direccion;
        private string _Telefono1;
        private string _Telefono2;
        private string _NombreContacto;
        private string _Email;
        private int _CostoConsutaInternista;
        private int _NumeroDiasReconsulta;
        private bool _SoloLiname;
        private bool _isSOAT;
        private bool _isGestionMedica;
        private bool _isDesgravamen;

        private string _NombreRedMedica;

        #region Propiedades
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public string CodigoCliente
        {
            get { return this._CodigoCliente; }
            set { this._CodigoCliente = value; }
        }
        public string NombreJuridico
        {
            get { return this._NombreJuridico; }
            set { this._NombreJuridico = value; }
        }
        public decimal Nit
        {
            get { return this._Nit; }
            set { this._Nit = value; }
        }
        public string Direccion
        {
            get { return this._Direccion; }
            set { this._Direccion = value; }
        }
        public string Telefono1
        {
            get { return this._Telefono1; }
            set { this._Telefono1 = value; }
        }
        public string Telefono2
        {
            get { return this._Telefono2; }
            set { this._Telefono2 = value; }
        }
        public string NombreContacto {
            get { return this._NombreContacto; }
            set { this._NombreContacto = value; }
        }
        public string Email {
            get { return this._Email; }
            set { this._Email = value; }
        }
        public int CostoConsultaInternista
        {
            get { return this._CostoConsutaInternista; }
            set { this._CostoConsutaInternista = value; }
        }
        public int NumeroDiasReconsulta
        {
            get { return this._NumeroDiasReconsulta; }
            set { this._NumeroDiasReconsulta = value; }
        }
        public bool SoloLiname
        {
            get { return this._SoloLiname; }
            set { this._SoloLiname = value; }
        }
        public bool IsSOAT 
        {
            get { return this._isSOAT; }
            set { this._isSOAT = value; }
        }
        public bool IsGestionMedica
        {
            get { return this._isGestionMedica; }
            set { this._isGestionMedica = value; }
        }
        public bool IsDesgravamen
        {
            get { return this._isDesgravamen; }
            set { this._isDesgravamen = value; }
        }

        public string NombreRedMedica
        {
            get { return this._NombreRedMedica; }
        }
        public string NombreJuridicoForDisplay
        {
            get { return TextUtilities.MakeForDisplay(ClienteId, this._NombreJuridico); }
        }
        #endregion

        public RedCliente()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedCliente(int ClienteId, string CodigoCliente, string NombreJuridico, decimal Nit
            , string Direccion, string Telefono1, string Telefono2, string NombreContacto
            , string Email, int CostoConsultaInternista, int NumeroDiasReconsulta, 
            bool SoloLiname, bool isSOAT, bool isGestionMedica, bool isDesgravamen)
        {
            this._ClienteId = ClienteId;
            this._CodigoCliente = CodigoCliente;
            this._NombreJuridico = NombreJuridico;
            this._Nit = Nit;
            this._Direccion = Direccion;
            this._Telefono1 = Telefono1;
            this._Telefono2 = Telefono2;
            this._NombreContacto = NombreContacto;
            this._Email = Email;
            this._CostoConsutaInternista = CostoConsultaInternista;
            this._NumeroDiasReconsulta = NumeroDiasReconsulta;
            this._SoloLiname = SoloLiname;
            this._isSOAT = isSOAT;
            this._isGestionMedica = isGestionMedica;
            this._isDesgravamen = isDesgravamen;
        }

        //RedClienteRedMedica
        public RedCliente(int ClienteId, string CodigoCliente, string NombreJuridico, decimal Nit
            , string Direccion, string Telefono1, string Telefono2, string NombreContacto
            , string Email, int CostoConsultaInternista, int NumeroDiasReconsulta, string NombreRedMedica )
        {
            this._ClienteId = ClienteId;
            this._CodigoCliente = CodigoCliente;
            this._NombreJuridico = NombreJuridico;
            this._Nit = Nit;
            this._Direccion = Direccion;
            this._Telefono1 = Telefono1;
            this._Telefono2 = Telefono2;
            this._NombreContacto = NombreContacto;
            this._Email = Email;
            this._CostoConsutaInternista = CostoConsultaInternista;
            this._NumeroDiasReconsulta = NumeroDiasReconsulta;
            this._NombreRedMedica = NombreRedMedica;
        }
    }
}