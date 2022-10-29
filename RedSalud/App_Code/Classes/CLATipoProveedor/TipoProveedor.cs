using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.TipoProveedor
{
    /// <summary>
    /// Summary description for TipoProveedor
    /// </summary>
    public class TipoProveedor
    {
        private string _TipoProveedorId;
        private string _Nombre;

        public string TipoProveedorId
        {
            get { return this._TipoProveedorId; }
            set { this._TipoProveedorId = value; }
        }
        public string Nombre {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }

        public TipoProveedor()
        {
        }
        public TipoProveedor ( string TipoProveedorId, string Nombre )
        {
            this._TipoProveedorId = TipoProveedorId;
            this._Nombre = Nombre;
        }
    }
}