
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Enfermedad
{
    public class Enfermedad
    {
        private string _enfermedadId;
        private string _nombre;

        public Enfermedad () { }

        public Enfermedad ( string enfermedadId, string nombre )
        {
            this._enfermedadId = enfermedadId;
            this._nombre = nombre;
        }

        public string EnfermedadId
        {
            get { return _enfermedadId; }
            set { _enfermedadId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
