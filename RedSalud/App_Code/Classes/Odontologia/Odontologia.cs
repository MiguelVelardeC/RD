
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Odontologia
{
    public class Odontologia : IFechaCreacion
    {
        private int _odontologiaId;
        private int _casoId;
        private int _prestacionOdontologicaId;
        private string _prestacionOdontologica;
        private string _pieza;
        private string _detalle;
        private string _observaciones;
        private DateTime _FechaCreacion;
        private int _gastoId;
        private int _fileCount;

        public Odontologia () { }

        public Odontologia ( int odontologiaId, int casoId, int prestacionOdontologicaId, string  prestacionOdontologica, string pieza, string detalle, string observaciones, DateTime fechaCreacion, int gastoId )
        {
            this._odontologiaId = odontologiaId;
            this._casoId = casoId;
            this._prestacionOdontologicaId = prestacionOdontologicaId;
            this._prestacionOdontologica = prestacionOdontologica;
            this._pieza = pieza;
            this._detalle = detalle;
            this._observaciones = observaciones;
            this._FechaCreacion = fechaCreacion;
            this._gastoId = gastoId;
        }

        public int OdontologiaId
        {
            get { return _odontologiaId; }
            set { _odontologiaId = value; }
        }

        public int CasoId
        {
            get { return _casoId; }
            set { _casoId = value; }
        }

        public int PrestacionOdontologicaId
        {
            get { return _prestacionOdontologicaId; }
            set { _prestacionOdontologicaId = value; }
        }

        public string PrestacionOdontologica
        {
            get { return _prestacionOdontologica; }
            set { _prestacionOdontologica = value; }
        }

        public string PrestacionOdontologicaForDisplay
        {
            get { return _prestacionOdontologica + (_pieza == "ALL" ? " EN TODAS LAS PIEZAS" : " EN PIEZA " + _pieza); }
        }

        public string Pieza
        {
            get { return _pieza == "ALL" ? "TODAS" : _pieza; }
            set { _pieza = value; }
        }

        public string Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        DateTime IFechaCreacion.FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public string FechaCreacionString
        {
            get { return _FechaCreacion.ToString(); }
        }

        public int GastoId
        {
            get { return _gastoId; }
            set { _gastoId = value; }
        }

        public int FileCount
        {
            set { _fileCount = value; }
            get { return _fileCount; }
        }
        public string FileCountForDisplay
        {
            get { return _fileCount.ToString("00"); }
        }
    }
}