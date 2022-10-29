using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.TipoEnfermedad
{
    /// <summary>
    /// Summary description for TipoEnfermedad
    /// </summary>
    public class TipoEnfermedad
    {
        private int _TipoEnfermedadId;//tipoEnfermedad
        private string _Nombre;

        public int TipoEnfermedadId
        {
            get { return this._TipoEnfermedadId; }
            set { this._TipoEnfermedadId = value; }
        }
        public string Nombre {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }
        public string NombreForDisplay
        {
            get { return TextUtilities.MakeForDisplay(TipoEnfermedadId, this._Nombre); }
        }

        public TipoEnfermedad()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public TipoEnfermedad(int tipoEnfermedadId, string nombre) {
            this._TipoEnfermedadId = tipoEnfermedadId;
            this._Nombre = nombre;
        }
    }
}