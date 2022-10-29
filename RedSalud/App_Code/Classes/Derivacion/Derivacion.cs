using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Derivacion
{
    /// <summary>
    /// Summary description for Derivacion
    /// </summary>
    public class Derivacion : IFechaCreacion
    {
        private int _DerivaconId;
        private int _CasoId;
        private int _ProveedorId;
        private string _Observaciones;

        private int _GastoId;
        private int _AprobacionUserId;
        private DateTime _AprobacionFecha;
        private DateTime _FechaCreacion;

        private string _NombreProveedor;
        private string _Especialidad;

        //Gastos
        private decimal _MontoConFactura;
        private decimal _MontoSinFactura;
        private decimal _RetencionImpuesto;
        private decimal _Total;
        private int _fileCount;

        #region Derivacion
        public int DerivacionId
        {
            get { return this._DerivaconId; }
            set { this._DerivaconId= value; }
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

        public bool Aprovado {
            get {
                if (_AprobacionUserId <= 0)
                    return false;
                else
                    return true;
            }
        }

        #endregion

        public string NombreProveedor
        {
            get { return this._NombreProveedor; }
        }
        public string Especialidad
        {
            get { return this._Especialidad; }
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
        public string NITProveedor
        {
            get
            {
                Proveedor.Proveedor obj = Proveedor.BLL.ProveedorBLL.getProveedorByID(_ProveedorId);
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

        public Derivacion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Derivacion(int DerivacionId, int CasoId
            , int ProveedorId, string Observaciones
            , int GastoId, int AprobacionUserId, DateTime AprobacionFecha
            , DateTime FechaCreacion, string NombreProveedor, string Especialidad
            
            , decimal MontoConFactura, decimal MontoSinFactura
            ,decimal RetencionImpuesto, decimal Total)
        {
            this._DerivaconId = DerivacionId;
            this._CasoId = CasoId;
            this._ProveedorId = ProveedorId;
            this._Observaciones = Observaciones.ToUpper();
            this._GastoId = GastoId;
            this._AprobacionUserId = AprobacionUserId;
            this._AprobacionFecha = AprobacionFecha;
            this._FechaCreacion = FechaCreacion;

            this._NombreProveedor = NombreProveedor.ToUpper();
            this._Especialidad = Especialidad.ToUpper();

            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencionImpuesto;
            this._Total = Total;
        }
    }
}