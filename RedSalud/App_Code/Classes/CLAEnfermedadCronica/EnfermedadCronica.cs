
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.EnfermedadCronica
{
    public class EnfermedadCronica
    {
        private int _enfermedadCronicaId;
        private string _nombre;
        public int RowNumber { get; set; }

        public EnfermedadCronica() {}

        public EnfermedadCronica(int enfermedadCronicaId, string nombre)
        {
            this._enfermedadCronicaId = enfermedadCronicaId;
            this._nombre = nombre;
        }

        public int EnfermedadCronicaId
        {
            get { return _enfermedadCronicaId; }
            set { _enfermedadCronicaId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}