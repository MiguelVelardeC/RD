using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Plan
{
    public class Plan
    {
        string _Nombre;
        int _TipoEstudio;
        int _Cantidad;
        int _CantidadUso;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public int TipoEstudio
        {
            get { return _TipoEstudio; }
            set { _TipoEstudio = value; }
        }
        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        public int CantidadUso
        {
            get { return _CantidadUso; }
            set { _CantidadUso = value; }
        }

        public Plan ()
        {
        }

        public Plan (string nombre, int tipoEstudio, int cantidad, int cantidadUso)
        {
            this._Nombre = nombre;
            this._TipoEstudio = tipoEstudio;
            this._Cantidad = cantidad;
            this._CantidadUso = cantidadUso;
        }
    }
}