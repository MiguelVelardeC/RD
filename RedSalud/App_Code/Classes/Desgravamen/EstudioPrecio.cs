using EvoPdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for CitaMedica
    /// </summary>
    public class EstudioPrecio
    {
        private string _NomEstudio; 
        private decimal _Precioxplaza;
        private string _ciudad;
        private decimal _Precioxciudad;
        private string _Tipo;

        public string NomEstudio
        {
            get { return this._NomEstudio; }
            set { this._NomEstudio = value; }
        }
        public decimal Precioxplaza
        {
            get { return this._Precioxplaza; }
            set { this._Precioxplaza = value; }
        }

        public string ciudad
        {
            get { return this._ciudad; }
            set { this._ciudad = value; }
        }
        public decimal Precioxciudad
        {
            get { return this._Precioxciudad; }
            set { this._Precioxciudad = value; }
        }

        public string Tipo
        {
            get { return this._Tipo; }
            set { this._Tipo = value; }        
        }
        public EstudioPrecio()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public EstudioPrecio(string NomEstudio, decimal Precioxplaza, string ciudad, decimal Precioxciudad, string Tipo)
        {
            this._NomEstudio = NomEstudio;
            this._Precioxplaza = Precioxplaza;
            this._ciudad = ciudad;
            this._Precioxciudad=Precioxciudad;
            this._Tipo = Tipo;
        }
    }
}