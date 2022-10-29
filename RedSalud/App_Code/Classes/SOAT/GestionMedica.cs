
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.GestionMedica
{
    public class GestionMedica
    {
        private int _siniestroId;
        private int _accidentadoId;
        private int _gestionMedicaId;
        private string _nombre;
        private DateTime _fechaVisita;
        private string _grado;
        private string _diagnosticoPreliminar;

        public GestionMedica () { }

        public GestionMedica ( int siniestroId, int accidentadoId, int gestionMedicaId, string nombre, DateTime fechaVisita,
            string grado, string diagnosticoPreliminar )
        {
            this._siniestroId = siniestroId;
            this._accidentadoId = accidentadoId;
            this._gestionMedicaId = gestionMedicaId;
            this._nombre = nombre;
            this._fechaVisita = fechaVisita;
            this._grado = grado;
            this._diagnosticoPreliminar = diagnosticoPreliminar;

        }

        public int SiniestroId
        {
            get { return _siniestroId; }
            set { _siniestroId = value; }
        }

        public int AccidentadoId
        {
            get { return _accidentadoId; }
            set { _accidentadoId = value; }
        }

        public int GestionMedicaId
        {
            get { return _gestionMedicaId; }
            set { _gestionMedicaId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public DateTime FechaVisita
        {
            get { return _fechaVisita; }
            set { _fechaVisita = value; }
        }

        public string FechaVisitaForDisplay
        {
            get { return _fechaVisita.ToShortDateString(); }
        }

        public string Grado
        {
            get { return _grado; }
            set { _grado = value; }
        }

        public string DiagnosticoPreliminar
        {
            get { return _diagnosticoPreliminar; }
            set { _diagnosticoPreliminar = value; }
        }

        public int ProveedorId { get; set; }
    }
}