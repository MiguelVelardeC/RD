using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.RedClientePrestaciones
{
    /// <summary>
    /// Summary description for RedClientePrestaciones
    /// </summary>
    public class RedClientePrestaciones
    {
        private int _preId;
        private string _prestacion;
        private string _preTipoPrestacion;
        private int _ClienteId;
        private decimal _prePrecio;
        private decimal _preCoPagoMonto;
        private decimal _preCoPagoPorcentaje;
        private int _preCantidadConsultasAno;
        private int _preDiasCarencia;
        private decimal _preMontoTope;

        #region Propiedades
        public int preId
        {
            get { return this._preId; }
            set { this._preId = value; }
        }
        public string Prestacion
        {
            get { return this._prestacion; }
            set { this._prestacion = value; }
        }
        public string TipoPrestacion
        {
            get { return this._preTipoPrestacion; }
            set { this._preTipoPrestacion = value; }
        }
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public decimal Precio
        {
            get { return this._prePrecio; }
            set { this._prePrecio = value; }
        }
        public decimal CoPagoMonto
        {
            get { return this._preCoPagoMonto; }
            set { this._preCoPagoMonto = value; }
        }
        public decimal CoPagoPorcentaje
        {
            get { return this._preCoPagoPorcentaje; }
            set { this._preCoPagoPorcentaje = value; }
        }
        public int CantidadConsultasAno
        {
            get { return this._preCantidadConsultasAno; }
            set { this._preCantidadConsultasAno = value; }
        }
        public int DiasCarencia
        {
            get { return this._preDiasCarencia; }
            set { this._preDiasCarencia = value; }
        }
        public decimal MontoTope
        {
            get { return this._preMontoTope; }
            set { this._preMontoTope = value; }
        }

        #endregion

        public RedClientePrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedClientePrestaciones(int preId, string Prestacion, string TipoPrestacion, int ClienteId
            , decimal Precio, decimal CoPagoMonto, decimal CoPagoPorcentaje, int CantidadConsultasAno
            , int DiasCarencia, decimal MontoTope)
        {
            this._preId = preId;
            this._prestacion = Prestacion;
            this._preTipoPrestacion = TipoPrestacion;
            this._ClienteId = ClienteId;
            this._prePrecio = Precio;
            this._preCoPagoMonto = CoPagoMonto;
            this._preCoPagoPorcentaje = CoPagoPorcentaje;
            this._preCantidadConsultasAno = CantidadConsultasAno;
            this._preDiasCarencia = DiasCarencia;
            this._preMontoTope = MontoTope;
        }
    }

    public class RedEspecialidadPrestaciones
    {
        private int _detId;
        private int _EspecialidadId;
        private string _Especialidad;
        private int _ClienteId;
        private string _DescripcionCantFrecuencia;
        private int _Cantidad;
        private string _Frecuencia;

        #region Propiedades
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }
        }
        public int EspecialidadId
        {
            get { return this._EspecialidadId; }
            set { this._EspecialidadId = value; }
        }
        public string Especialidad
        {
            get { return this._Especialidad; }
            set { this._Especialidad = value; }
        }
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public int Cantidad
        {
            get { return this._Cantidad; }
            set { this._Cantidad = value; }
        }

        public string Frecuencia
        {
            get { return this._Frecuencia; }
            set { this._Frecuencia = value; }
        }

        public string DescripcionCantFrecuencia
        {
            get { return this._DescripcionCantFrecuencia; }
            set { this._DescripcionCantFrecuencia = value; }
        }
        #endregion

        public RedEspecialidadPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedEspecialidadPrestaciones(int detId, int EspecialidadId, string Especialidad, int ClienteId, int Cantidad,string Frecuencia, string DescripcionCantFrecuencia)
        {
            this._detId = detId;
            this._EspecialidadId = EspecialidadId;
            this._Especialidad = Especialidad;
            this._ClienteId = ClienteId;
            this._Cantidad = Cantidad;
            this._Frecuencia = Frecuencia;
            this._DescripcionCantFrecuencia = DescripcionCantFrecuencia;
        }
    }

    public class RedGruposLabPrestaciones
    {
        private int _detId;
        private int _EstudioId;
        private string _Estudio;

        #region Propiedades
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }
        }
        public int EstudioId
        {
            get { return this._EstudioId; }
            set { this._EstudioId = value; }
        }
        public string Estudio
        {
            get { return this._Estudio; }
            set { this._Estudio = value; }
        }


        #endregion

        public RedGruposLabPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedGruposLabPrestaciones(int detId, int EstudioId, string Estudio)
        {
            this._detId = detId;
            this._EstudioId = EstudioId;
            this._Estudio = Estudio;
        }
    }

    public class RedImagenPrestaciones
    {
        private int _detId;
        private int _EstudioId;
        private string _Estudio;

        #region Propiedades
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }
        }
        public int EstudioId
        {
            get { return this._EstudioId; }
            set { this._EstudioId = value; }
        }
        public string Estudio
        {
            get { return this._Estudio; }
            set { this._Estudio = value; }
        }


        #endregion

        public RedImagenPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedImagenPrestaciones(int detId, int EstudioId, string Estudio)
        {
            this._detId = detId;
            this._EstudioId = EstudioId;
            this._Estudio = Estudio;
        }
    }

    public class RedCirugiasPrestaciones
    {
        private int _detId;
        private string _CodigoArancelarioId;
        private string _CodigoArancelario;
        private decimal _detMontoTope;
        private int _detCantidadTope;

        #region Propiedades
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }
        }
        public string CodigoArancelarioId
        {
            get { return this._CodigoArancelarioId; }
            set { this._CodigoArancelarioId = value; }
        }
        public string CodigoArancelario
        {
            get { return this._CodigoArancelario; }
            set { this._CodigoArancelario = value; }
        }
        public decimal detMontoTope
        {
            get { return this._detMontoTope; }
            set { this._detMontoTope = value; }
        }
        public int detCantidadTope
        {
            get { return this._detCantidadTope; }
            set { this._detCantidadTope = value; }
        }

        #endregion

        public RedCirugiasPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedCirugiasPrestaciones(int detId, string CodigoArancelarioId, string CodigoArancelario, decimal detMontoTope, int detCantidadTope)
        {
            this._detId = detId;
            this._CodigoArancelarioId = CodigoArancelarioId;
            this._CodigoArancelario = CodigoArancelario;
            this._detMontoTope = detMontoTope;
            this._detCantidadTope = detCantidadTope;
        }
    }

    public class RedOdontoPrestaciones
    {
        private int _detId;
        private int _PrestacionOdontologicaId;
        private string _PrestacionOdontologica;
        private int _detCantidadConsultasAno;

        #region Propiedades
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }
        }
        public int PrestacionOdontologicaId
        {
            get { return this._PrestacionOdontologicaId; }
            set { this._PrestacionOdontologicaId = value; }
        }
        public string PrestacionOdontologica
        {
            get { return this._PrestacionOdontologica; }
            set { this._PrestacionOdontologica = value; }
        }
        public int detCantidadConsultasAno
        {
            get { return this._detCantidadConsultasAno; }
            set { this._detCantidadConsultasAno = value; }
        }

        #endregion

        public RedOdontoPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedOdontoPrestaciones(int detId, int PrestacionOdontologicaId, string PrestacionOdontologica, int detCantidadConsultasAno)
        {
            this._detId = detId;
            this._PrestacionOdontologicaId = PrestacionOdontologicaId;
            this._PrestacionOdontologica = PrestacionOdontologica;
            this._detCantidadConsultasAno = detCantidadConsultasAno;
        }
    }

    public class RedTipoPrestaciones
    {
        private string _id;
        private string _prestacion;

        #region Propiedades
        public string id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string prestacion
        {
            get { return this._prestacion; }
            set { this._prestacion = value; }
        }

        #endregion

        public RedTipoPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedTipoPrestaciones(string id, string prestacion)
        {
            this._id = id;
            this._prestacion = prestacion;
        }
    }
}