using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Asegurado
{
    /// <summary>
    /// Summary description for Ciudad
    /// </summary>
    public class Asegurado
    {
        private int _AseguradoId;
        private string _Aseguradora;
        private string _Nombre;
        private string _NroPoliza;
        private decimal _MontoAsignado;

        public int AseguradoId
        {
            get { return this._AseguradoId; }
            set { this._AseguradoId = value; }
        }
        public string Aseguradora
        {
            get { return this._Aseguradora; }
            set { this._Aseguradora = value; }
        }
        public string Nombre
        {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }
        public string NroPoliza
        {
            get { return this._NroPoliza; }
            set { this._NroPoliza = value; }
        }
        public decimal MontoAsignado
        {
            get { return this._MontoAsignado; }
            set { this._MontoAsignado = value; }
        }

        public Asegurado() {}
        public Asegurado ( int AseguradoId, string Aseguradora, string Nombre, string NroPoliza, decimal MontoAsignado )
        {
            this._AseguradoId = AseguradoId;
            this._Aseguradora = Aseguradora;
            this._Nombre = Nombre;
            this._NroPoliza = NroPoliza;
            this._MontoAsignado = MontoAsignado;
        }
    }
}