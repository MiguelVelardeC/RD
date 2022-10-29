using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.Medico
{
    public class Medico
    {
        private int _medicoId;
        private int? _fotoId;
        private int _userId;
        private string _nombre;
        private int _especialidadId;
        private string _especialidad;
        private string _sedes;
        private string _colegioMedico;
        private string _estado;
        private string _observacion;
        private DateTime _fechaActualizacion;
        private int _rowNumber;


        public Medico ()
        {
        }

        //edwin suyo
        private string _CiudadId;
        public string CiudadId
        {
            get { return _CiudadId; }
            set { _CiudadId = value; }
        }
       //-------------
       public Medico (int userId,int MedicoId,string fullname,string NombreEspecialidad,string ciudadId)
        {
            this._userId = userId;
            this._medicoId = MedicoId;
            this._nombre = fullname;
            this._especialidad = NombreEspecialidad;
            this._CiudadId = ciudadId;
        }
       //--------------
        public Medico ( int medicoId, int especialidadId, string sedes, string colegioMedico, int userId, string estado, 
            string observacion, bool isExternal, bool isCallCenter, DateTime fechaActualizacion)
        {
            this._medicoId = medicoId;
            this._especialidadId = especialidadId;
            this._sedes = sedes;
            this._colegioMedico = colegioMedico;
            this._userId = userId;
            this._estado = estado;
            this._observacion = observacion;
            this._fechaActualizacion = fechaActualizacion;
            this.IsExternal = IsExternal;
            this.IsCallCenter = isCallCenter;
            
        }

        public Medico ( int medicoId, int userId, string nombre, int especialidadId, string especialidad, string sedes,
            string colegioMedico, string estado, string observacion, bool isExternal, bool isCallCenter, 
            DateTime fechaActualizacion, bool PermiteVD, int? fotoId)
        {
            this._medicoId = medicoId;
            this._userId = userId;
            this._nombre = nombre;
            this._especialidadId = especialidadId;
            this._especialidad = especialidad;
            this._sedes = sedes;
            this._colegioMedico = colegioMedico;
            this._estado = estado;
            this._observacion = observacion;
            this._fechaActualizacion = fechaActualizacion;
            this.IsExternal = isExternal;
            this.IsCallCenter = isCallCenter;
            this.PermiteVideoLLamada = PermiteVD;
            this._fotoId = fotoId;
        }

        public int MedicoId
        {
            get { return this._medicoId; }
            set { this._medicoId = value; }
        }

        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public int EspecialidadId
        {
            get { return this._especialidadId; }
            set { this._especialidadId = value; }
        }

        public string Especialidad
        {
            get { return this._especialidad; }
            set { this._especialidad = value; }
        }

        public string Sedes
        {
            get { return this._sedes; }
            set { this._sedes = value; }
        }

        public string ColegioMedico
        {
            get { return this._colegioMedico; }
            set { this._colegioMedico = value; }
        }

        public string Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }

        public string Observacion
        {
            get { return this._observacion; }
            set { this._observacion = value; }
        }

        public DateTime FechaActualizacion
        {
            get { return this._fechaActualizacion; }
            set { this._fechaActualizacion = value; }
        }

        public int? FotoId
        {
            get { return _fotoId; }
            set { _fotoId = value; }
        }

        public int RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; }
        }

        public string NombreForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_medicoId, _nombre).ToUpper(); }
        }

        public bool IsExternal { get; set; }
        public bool IsCallCenter { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public bool PermiteVideoLLamada { get; set; }

    }

    public class MedicoHorario {
        private string _dia;
        private string _horaInicio;
        private string _horaFin;
        private int _horId;

        public MedicoHorario() { }
        public MedicoHorario(int horId, string dia, string horaInicio, string horaFin) {
            this._dia = dia;
            this._horaInicio = horaInicio;
            this._horaFin = horaFin;
            this._horId = horId;
        }
        public int horId
        {
            get { return _horId; }
            set { _horId = value; }
        }
        public string Dia { 
            get { return _dia; }
            set { _dia = value; } 
        }
        public string HoraInicio
        {
            get { return _horaInicio; }
            set { _horaInicio = value; }
        }
        public string HoraFin
        {
            get { return _horaFin; }
            set { _horaFin = value; }
        }
    }
}