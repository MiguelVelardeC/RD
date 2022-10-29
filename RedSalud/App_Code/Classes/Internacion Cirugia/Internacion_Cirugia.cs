using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Internacion_Internacion
/// </summary>
/// 
namespace Artexacta.App.Internacion_Cirugia
{
    public class Internacion_Cirugia
    {
        #region "Atributos"
        private int _preId;
        private int _InternacionId;
        private string _CiudadId;
        private int _MedicoId;
        private int _EspecialidadId;
        private decimal _detValorUma;
        private int _detCantidadUma;
        private decimal _detPorcentajeCirujano;
        private decimal _detMontoCirujano;
        private decimal _detPorcentajeAnestesiologo;
        private decimal _detMontoAnestesiologo;
        private decimal _detPorcentajeAyudante;
        private decimal _detMontoAyudante;
        private decimal _detPorcentajeInstrumentista;
        private decimal _detMontoInstrumentista;
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
        public int MedicoId
        {
            get { return this._MedicoId; }
            set { this._MedicoId = value; }
        }
        public int EspecialidadId
        {
            get { return this._EspecialidadId; }
            set { this._EspecialidadId = value; }
        }
        public decimal detValorUma
        {
            get { return this._detValorUma; }
            set { this._detValorUma = value; }
        }
        public int detCantidadUma
        {
            get { return this._detCantidadUma; }
            set { this._detCantidadUma = value; }
        }
        public decimal detPorcentajeCirujano
        {
            get { return this._detPorcentajeCirujano; }
            set { this._detPorcentajeCirujano = value; }
        }
        public decimal detMontoCirujano
        {
            get { return this._detMontoCirujano; }
            set { this._detMontoCirujano = value; }
        }
        public decimal detPorcentajeAnestesiologo
        {
            get { return this._detPorcentajeAnestesiologo; }
            set { this._detPorcentajeAnestesiologo = value; }
        }
        public decimal detMontoAnestesiologo
        {
            get { return this._detMontoAnestesiologo; }
            set { this._detMontoAnestesiologo = value; }
        }
        public decimal detPorcentajeAyudante
        {
            get { return this._detPorcentajeAyudante; }
            set { this._detPorcentajeAyudante = value; }
        }
        public decimal detMontoAyudante
        {
            get { return this._detMontoAyudante; }
            set { this._detMontoAyudante = value; }
        }
        public decimal detPorcentajeInstrumentista
        {
            get { return this._detPorcentajeInstrumentista; }
            set { this._detPorcentajeInstrumentista = value; }
        }
        public decimal detMontoInstrumentista
        {
            get { return this._detMontoInstrumentista; }
            set { this._detMontoInstrumentista = value; }
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
        public Internacion_Cirugia()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Internacion_Cirugia(
        int InternacionId, string CiudadId, int MedicoId, int EspecialidadId, decimal detValorUma, int detCantidadUma
        , decimal detPorcentajeCirujano, decimal detMontoCirujano, decimal detPorcentajeAnestesiologo, decimal detMontoAnestesiologo
        , decimal detPorcentajeAyudante, decimal detMontoAyudante, decimal detPorcentajeInstrumentista, decimal detMontoInstrumentista
        , decimal detMontoTotal, decimal detPorcentajeCoPago, decimal detMontoCoPago, DateTime detFecha
        )
        {
            this._InternacionId = InternacionId;
            this._CiudadId = CiudadId;
            this._MedicoId = MedicoId;
            this._EspecialidadId = EspecialidadId;
            this._detValorUma = detValorUma;
            this._detCantidadUma = detCantidadUma;
            this._detPorcentajeCirujano = detPorcentajeCirujano;
            this._detMontoCirujano= detMontoCirujano;
            this._detPorcentajeAnestesiologo =detPorcentajeAnestesiologo;
            this._detMontoAnestesiologo=detMontoAnestesiologo;
            this._detPorcentajeAyudante=detPorcentajeAyudante;
            this._detMontoAyudante=detMontoAyudante;
            this._detPorcentajeInstrumentista=detPorcentajeInstrumentista;
            this._detMontoInstrumentista=detMontoInstrumentista;
            this._detMontoTotal=detMontoTotal;
            this._detPorcentajeCoPago=detPorcentajeCoPago;
            this._detMontoCoPago=detMontoCoPago;
            this._detFecha=detFecha;
        }
    }
}