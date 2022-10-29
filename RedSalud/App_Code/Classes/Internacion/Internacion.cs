using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Internacion
{
    /// <summary>
    /// Summary description for Internacion
    /// </summary>
    public class Internacion : IFechaCreacion
    {
        private int _InternacionId;
        private int _CasoId;
        private int _ProveedorId;
        private string _Observaciones;
        private string _CodigoArancelarioId;
        private string _CodigoArancelario;
        private decimal _UMA;

        private int _GastoId;
        private int _AprobacionUserId;
        private DateTime _AprobacionFecha;
        private DateTime _FechaCreacion;

        private string _NombreProveedor;


        //Gastos
        private decimal _MontoConFactura;
        private decimal _MontoSinFactura;
        private decimal _RetencionImpuesto;
        private decimal _Total;
        private int _fileCount;

        #region Internacion

        public int InternacionId
        {
            get { return this._InternacionId; }
            set { this._InternacionId = value; }
        }
        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public int ProveedorId
        {
            get { return this._ProveedorId; }
            set { this._ProveedorId = value; }
        }

        public string Observaciones
        {
            get { return this._Observaciones; }
            set { this._Observaciones = value; }
        }
        public string CodigoArancelarioId
        {
            get { return this._CodigoArancelarioId; }
            set { this._CodigoArancelarioId = value; }
        }
        public string Tipo
        {
            get { return isInternacion ? "INTERNACIÓN" : "CIRUGÍA"; }
        }
        public bool isInternacion
        {
            get { return this._CodigoArancelarioId == ""; }
        }
        public string CodigoArancelario
        {
            get { return this._CodigoArancelario; }
            set { this._CodigoArancelario = value; }
        }
        public decimal UMA
        {
            get { return this._UMA; }
            set { this._UMA = value; }
        }
        public int GastoId
        {
            get { return this._GastoId; }
            set { this._GastoId = value; }
        }
        public int AprobacionUserId
        {
            get { return this._AprobacionUserId; }
            set { this._AprobacionUserId = value; }
        }
        public DateTime AprobacionFecha
        {
            get { return this._AprobacionFecha; }
            set { this._AprobacionFecha = value; }
        }
        DateTime IFechaCreacion.FechaCreacion
        {
            get { return this._FechaCreacion; }
            set { this._FechaCreacion = value; }
        }
        public string FechaCreacionString
        {
            get { return _FechaCreacion.ToString(); }
        }

        #endregion

        public string NombreProveedor
        {
            get { return this._NombreProveedor; }
        }
        public bool Aprovado
        {
            get
            {
                if (_AprobacionUserId <= 0)
                    return false;
                else
                    return true;
            }
        }
        public bool Modify
        {
            get
            {
                if (_AprobacionUserId <= 0)
                    return true;
                else
                    return false;
            }
        }

        public string NITProveedor {
            get { Proveedor.Proveedor obj= Proveedor.BLL.ProveedorBLL.getProveedorByID(_ProveedorId);
            if (obj != null)
                return obj.Nit;
            else
                return "-";
            }
        }

        #region Gastos
        public decimal MontoConFactura
        {
            get { return this._MontoConFactura; }
        }
        public decimal MontoSinFactura
        {
            get { return this._MontoSinFactura; }
        }
        public decimal RetencionImpuesto
        {
            get { return this._RetencionImpuesto; }
        }
        public decimal Total
        {
            get { return this._Total; }
        }
        public string MontoConFacturaForDisplay
        {
            get { return this._MontoConFactura.ToString("#,##0.00 Bs"); }
        }
        public string MontoSinFacturaForDisplay
        {
            get { return this._MontoSinFactura.ToString("#,##0.00 Bs"); }
        }
        public string RetencionImpuestoForDisplay
        {
            get { return this._RetencionImpuesto.ToString("#,##0.00 Bs"); }
        }
        public string TotalForDisplay
        {
            get { return this._Total.ToString("#,##0.00 Bs"); }
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
        #endregion

        public Internacion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Internacion(int InternacionId, int CasoId
            , int ProveedorId, string Observaciones
            , string CodigoArancelarioId, string CodigoArancelario, decimal UMA
            , int GastoId, int AprobacionUserId
            , DateTime AprobacionFecha, DateTime FechaCreacion
            , string NombreProveedor
            
            , decimal MontoConFactura, decimal MontoSinFactura
            ,decimal RetencionImpuesto, decimal Total)
        {
            this._InternacionId = InternacionId;
            this._CasoId = CasoId;
            this._ProveedorId = ProveedorId;
            this._Observaciones = Observaciones.ToUpper();
            this._CodigoArancelarioId = CodigoArancelarioId.ToUpper();
            this._CodigoArancelario = CodigoArancelario.ToUpper();
            this._UMA = UMA;
            this._GastoId = GastoId;
            this._AprobacionUserId = AprobacionUserId;
            this._AprobacionFecha = AprobacionFecha;
            this._FechaCreacion = FechaCreacion;

            this._NombreProveedor = NombreProveedor.ToUpper();

            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencionImpuesto;
            this._Total = Total;
        }
    }
}