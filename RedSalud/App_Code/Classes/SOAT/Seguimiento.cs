
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Seguimiento
{
    public class Seguimiento
    {
        private int _siniestroId;
        private string _estado;
        private bool _acuerdo;
        private bool _rechazado;
        private string _observaciones;

        public Seguimiento () { }

        public Seguimiento ( int siniestroId, string estado, bool acuerdo, bool rechazado, string observaciones )
        {
            this._siniestroId = siniestroId;
            this._estado = estado;
            this._acuerdo = acuerdo;
            this._rechazado = rechazado;
            this._observaciones = observaciones;

        }

        public int SiniestroId
        {
            get { return _siniestroId; }
            set { _siniestroId = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public bool Acuerdo
        {
            get { return _acuerdo; }
            set { _acuerdo = value; }
        }

        public bool Rechazado
        {
            get { return _rechazado; }
            set { _rechazado = value; }
        }

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }
    }
}