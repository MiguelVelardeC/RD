using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Historia
{
    /// <summary>
    /// Summary description for Historia
    /// </summary>
    public class Historia
    {
        private int _HistoriaId;
        private int _PacienteId;
        private int _CasoId;
        private string _MotivoConsulta;
        private int _ProtocoloId;
        private string _Diagnostico;
        private string _EnfermedadId;
        private string _Enfermedad;
        private string _DiagnosticoPresuntivo;
        private string _Tratamiento;
        private string _PresionArterial;
        private string _Pulso;
        private string _Temperatura;
        private string _FrecuenciaCardiaca;
        private string _ExFisicoRegionalyDeSistema;

        public int HistoriaId
        {
            get { return this._HistoriaId; }
            set { this._HistoriaId = value; }
        }
        public int PacienteId
        {
            get { return this._PacienteId; }
            set { this._PacienteId = value; }
        }
        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public string MotivoConsulta
        {
            get { return this._MotivoConsulta; }
            set { this._MotivoConsulta = value; }
        }
        public int ProtocoloId
        {
            get { return this._ProtocoloId; }
            set { this._ProtocoloId = value; }
        }
        public string Diagnostico
        {
            get { return this._Diagnostico; }
            set { this._Diagnostico = value; }
        }
        public string EnfermedadId
        {
            get { return this._EnfermedadId; }
            set { this._EnfermedadId = value; }
        }
        public string Enfermedad
        {
            get { return this._Enfermedad; }
            set { this._Enfermedad = value; }
        }
        public string DiagnosticoPresuntivo {
            get { return this._DiagnosticoPresuntivo; }
            set { this._DiagnosticoPresuntivo = value; }
        }
        public string Tratamiento
        {
            get { return this._Tratamiento; }
            set { this._Tratamiento = value; }
        }
        public string PresionArterial
        {
            get { return this._PresionArterial; }
            set { this._PresionArterial = value; }
        }
        public string Pulso
        {
            get { return this._Pulso; }
            set { this._Pulso = value; }
        }
        public string Temperatura
        {
            get { return this._Temperatura; }
            set { this._Temperatura = value; }
        }
        public string FrecuenciaCardiaca
        {
            get { return this._FrecuenciaCardiaca; }
            set { this._FrecuenciaCardiaca = value; }
        }
        public string ExFisicoRegionalyDeSistema {
            get { return this._ExFisicoRegionalyDeSistema; }
            set { this._ExFisicoRegionalyDeSistema = value; }
        }

        public Historia()
        {}

        public Historia (int HistoriaId, int PacienteId, int CasoId, string MotivoConsulta, int ProtocoloId
            //, string Diagnostico
            , string enfermedadId, string enfermedad, string DiagnosticoPresuntivo
            //, string Tratamiento
            , string PresionArterial, string Pulso, string Temperatura, string FrecuenciaCardiaca
            , string ExFisicoRegionalyDeSistema)
        {
            this._HistoriaId = HistoriaId;
            this._PacienteId = PacienteId;
            this._CasoId = CasoId;
            this._MotivoConsulta = MotivoConsulta;
            this._ProtocoloId = ProtocoloId;

            //this._Diagnostico = Diagnostico;
            this._EnfermedadId = enfermedadId;
            this._Enfermedad = enfermedad;
            this._DiagnosticoPresuntivo = DiagnosticoPresuntivo;
            //this._Tratamiento = Tratamiento;

            this._PresionArterial = PresionArterial;
            this._Pulso = Pulso;
            this._Temperatura = Temperatura;
            this._FrecuenciaCardiaca = FrecuenciaCardiaca;
            this._ExFisicoRegionalyDeSistema = ExFisicoRegionalyDeSistema;
        }
    }
}