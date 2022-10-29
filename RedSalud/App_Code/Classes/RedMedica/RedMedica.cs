using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.RedMedica
{
    /// <summary>
    /// Summary description for RedMedica
    /// </summary>
    public class RedMedica
    {
        private int _RedMedicaId;
        private string _Codigo;
        private string _Nombre;

        public int RedMedicaId {
            get { return this._RedMedicaId; }
            set { this._RedMedicaId = value; }
        }
        public string Codigo {
            get { return this._Codigo; }
            set { this._Codigo = value; }
        }
        public string Nombre {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }
        public string NombreForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_RedMedicaId, this._Nombre); }
        }

        public RedMedica()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedMedica(int RedMedicaId, string Codigo
            , string Nombre)
        {
            this._RedMedicaId = RedMedicaId;
            this._Codigo = Codigo;
            this._Nombre = Nombre;
        }
    }
}