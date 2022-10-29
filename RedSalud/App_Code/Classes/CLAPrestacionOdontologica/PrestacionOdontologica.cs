
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.CLAPrestacionOdontologica
{
    public class PrestacionOdontologica
    {
        private int _prestacionOdontologicaId;
        private string _nombre;
        private bool _categoria;

        public PrestacionOdontologica () { }

        public PrestacionOdontologica ( int prestacionOdontologicaId, string nombre, bool categoria )
        {
            this._prestacionOdontologicaId = prestacionOdontologicaId;
            this._nombre = nombre;
            this._categoria = categoria;

        }

        public int PrestacionOdontologicaId
        {
            get { return _prestacionOdontologicaId; }
            set { _prestacionOdontologicaId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public bool Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }
    }
}