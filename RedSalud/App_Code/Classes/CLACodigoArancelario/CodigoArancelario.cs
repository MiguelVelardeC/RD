
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.CodigoArancelario
{
    public class CodigoArancelario
    {
        private string _codigoArancelarioId;
        private string _nombre;
        private decimal _uMA;

        public CodigoArancelario () {}

        public CodigoArancelario (string codigoArancelarioId, string nombre, decimal uMA)
        {
            this._codigoArancelarioId = codigoArancelarioId;
            this._nombre = nombre;
            this._uMA = uMA;
        }

        public string CodigoArancelarioId
        {
            get { return _codigoArancelarioId; }
            set { _codigoArancelarioId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public decimal UMA
        {
            get { return _uMA; }
            set { _uMA = value; }
        }
    }
}