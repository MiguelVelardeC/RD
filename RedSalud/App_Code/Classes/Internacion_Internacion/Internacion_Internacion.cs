using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Internacion_Internacion
/// </summary>
/// 
namespace Artexacta.App.Internacion_Internacion
{
    
    public class Internacion_Internacion
    {
        #region "Atributos"
        private int _preId;
        private int _InternacionId;
        private string _CiudadId;
        private string _EnfermedadId;
        private decimal _detMontoEmergencia;
        private decimal _detMontoHonorariosMedicos;
        private decimal _detMontoFarmacia;
        private decimal _detMontoLaboratorios;
        private decimal _detMontoEstudios;
        private decimal _detMontoHospitalizacion;
        private decimal _detMontoOtros;
        private decimal _detMontoTotal;
        private decimal _detPorcentajeCoPago;
        private decimal _detMontoCoPago;
        private DateTime _detFecha;
        #endregion

        #region "Propiedades"

        public int preId
        {
            get { return this._preId; }
            set { this._preId = value; }
        }
        public int InternacionId
        {
            get { return this._InternacionId; }
            set { this._InternacionId = value; }
        }
        public string CiudadId
        {
            get { return this._CiudadId; }
            set { this._CiudadId = value.ToUpper(); }
        }
        public string EnfermedadId
        {
            get { return this._EnfermedadId; }
            set { this._EnfermedadId = value; }
        }
        public decimal detMontoEmergencia
        {
            get { return this._detMontoEmergencia; }
            set { this._detMontoEmergencia = value; }
        }
        public decimal detMontoHonorariosMedicos
        {
                get { return this._detMontoHonorariosMedicos; }
                set { this._detMontoHonorariosMedicos = value; }
        }
        public decimal detMontoFarmacia
        {
            get { return this._detMontoFarmacia; }
            set { this._detMontoFarmacia = value; }
        }
        public decimal detMontoLaboratorios
        {
            get { return this._detMontoLaboratorios; }
            set { this._detMontoLaboratorios = value; }
        }
        public decimal detMontoEstudios
        {
            get { return this._detMontoEstudios; }
            set { this._detMontoEstudios = value; }
        }
        public decimal detMontoHospitalizacion
        {
            get { return this._detMontoHospitalizacion; }
            set { this._detMontoHospitalizacion = value; }
        }
        public decimal detMontoOtros
        {
            get { return this._detMontoOtros; }
            set { this._detMontoOtros = value; }
        }
        
        public decimal detMontoTotal
        {
            get { return this._detMontoTotal; }
            set { this._detMontoTotal = value; }
        }
        public decimal detPorcentajeCoPago
        {
            get { return this._detPorcentajeCoPago; }
            set { this._detPorcentajeCoPago = value; }
        }
        public decimal detMontoCoPago
        {
            get { return this._detMontoCoPago; }
            set { this._detMontoCoPago = value; }
        }
        public DateTime detFecha
        {
            get { return this._detFecha; }
            set { this._detFecha = value; }
        }

        #endregion
        public Internacion_Internacion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Internacion_Internacion(
        int InternacionId,string CiudadId,string EnfermedadId,decimal detMontoEmergencia,decimal detMontoHonorariosMedicos
        ,decimal detMontoFarmacia,decimal detMontoLaboratorios,decimal detMontoEstudios,decimal detMontoHospitalizacion
        ,decimal detMontoOtros,decimal detMontoTotal,decimal detPorcentajeCoPago,decimal detMontoCoPago,DateTime detFecha)
        {
            this._InternacionId=InternacionId;
            this._CiudadId=CiudadId;
            this._EnfermedadId=EnfermedadId;
            this._detMontoEmergencia=detMontoEmergencia;
            this._detMontoHonorariosMedicos=detMontoHonorariosMedicos;
            this._detMontoFarmacia=detMontoFarmacia;
            this._detMontoLaboratorios=detMontoLaboratorios;
            this._detMontoEstudios=detMontoEstudios;
            this._detMontoHospitalizacion=detMontoHospitalizacion;
            this._detMontoOtros=detMontoOtros;
            this._detMontoTotal=detMontoTotal;
            this._detPorcentajeCoPago=detPorcentajeCoPago;
            this._detMontoCoPago=detMontoCoPago;
            this._detFecha=detFecha;
    }
    }
}