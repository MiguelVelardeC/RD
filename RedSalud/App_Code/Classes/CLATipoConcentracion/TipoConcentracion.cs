using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.TipoConcentracion
{
    /// <summary>
    /// Summary description for TipoConcentracion
    /// </summary>
    public class TipoConcentracion
    {
        private int _TipoConcentracionId;
        private string _Nombre;

        public int TipoConcentracionId
        {
            get { return this._TipoConcentracionId; }
            set { this._TipoConcentracionId = value; }
        }
        public string Nombre {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }

        public TipoConcentracion()
        {
        }

        public TipoConcentracion ( int TipoConcentracionId, string Nombre )
        {
            this._TipoConcentracionId = TipoConcentracionId;
            this._Nombre = Nombre;
        }
    }
}