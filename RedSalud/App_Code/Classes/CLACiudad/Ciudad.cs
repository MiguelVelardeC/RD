using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Ciudad
{
    /// <summary>
    /// Summary description for Ciudad
    /// </summary>
    public class Ciudad
    {
        private string _CiudadId;
        private string _Nombre;

        public string CiudadId {
            get { return this._CiudadId; }
            set { this._CiudadId = value; }
        }
        public string Nombre {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }

        public Ciudad()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Ciudad(string CiudadId, string Nombre)
        {
            this._CiudadId = CiudadId;
            this._Nombre = Nombre;
        }
    }
}