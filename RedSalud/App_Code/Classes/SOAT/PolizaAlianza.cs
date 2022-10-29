using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Siniestro
{
    public class PolizaAlianza
    {
        private string _numeroRoseta;
        private string _poliza;
        private string _LugarVenta;
        private string _nombreTitular;
        private string _ciTitular;
        private string _placa;
        private string _tipo;
        private string _cilindrada;
        private string _sector;

        public string NumeroRoseta
        {
            get { return _numeroRoseta; }
            set { _numeroRoseta = value; }
        }

        public string Poliza
        {
            get { return _poliza; }
            set { _poliza = value; }
        }

        public string LugarVenta
        {
            get { return this._LugarVenta; }
            set { this._LugarVenta = value; }
        }

        public string NombreTitular
        {
            get { return _nombreTitular; }
            set { _nombreTitular = value; }
        }

        public string CITitular
        {
            get { return _ciTitular; }
            set { _ciTitular = value; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string Cilindrada
        {
            get { return this._cilindrada; }
            set { this._cilindrada = value; }
        }

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public PolizaAlianza() {}

        public PolizaAlianza ( string numeroRoseta, string poliza, string lugarVenta, string nombreTitular, string ciTitular, 
            string placa, string tipo, string cilindrada, string sector )
        {
            this._numeroRoseta = numeroRoseta;
            this._poliza = poliza;
            this._LugarVenta = lugarVenta;
            this._nombreTitular = nombreTitular;
            this._ciTitular = ciTitular;
            this._placa = placa;
            this._tipo = tipo;
            this._cilindrada = cilindrada;
            this._sector = sector;
        }
    }
}