using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Estudio
{
    /// <summary>
    /// Summary description for Estudio
    /// </summary>
    public class Estudio : IFechaCreacion
    {
        private int _EstudioId;
        private int _CasoId;
        private int _ProveedorId;
        private string _TipoEstudioId;
        private string _Observaciones;

        private int _GastoId;
        private int _AprobacionUserId;
        private DateTime _AprobacionFecha;
        private DateTime _FechaCreacion;
        private bool _Cubierto;

        private string _NombreTipoEstudio;
        private string _NombreProveedor;

        //Gastos
        private decimal _MontoConFactura;
        private decimal _MontoSinFactura;
        private decimal _RetencionImpuesto;
        private decimal _Total;
        private int _fileCount;

        #region Estudio
       
        public int EstudioId {
            get { return this._EstudioId; }
            set { this._EstudioId = value; }
        }
        public int CasoId {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public int ProveedorId {
            get { return this._ProveedorId; }
            set { this._ProveedorId=value; }
        }
        public string TipoEstudio
        {
            get { return this._TipoEstudioId; }
            set { this._TipoEstudioId=value; }
        }

        public string Observaciones {
            get { return this._Observaciones; }
            set { this._Observaciones = value; }
        }
        public int GastoId {
            get { return this._GastoId; }
            set { this._GastoId = value; }
        }
        public int AprobacionUserId {
            get { return this._AprobacionUserId; }
            set { this._AprobacionUserId = value; }
        }
        public DateTime AprobacionFecha {
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
        public bool Cubierto
        {
            get { return this._Cubierto; }
            set { this._Cubierto = value; }
        }
        public string CubiertoDisplay
        {
            get { return this._Cubierto ? "CUBIERTO" : "NO CUBIERTO"; }
        }
        #endregion

        public string NombreTipoEstudio {
            get { return this._NombreTipoEstudio; }
        }
        public string NombreProveedor {
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
        public bool Modify {
            get {
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
        public decimal MontoSinFactura {
            get { return this._MontoSinFactura; }
        }
        public decimal RetencionImpuesto {
            get { return this._RetencionImpuesto; }
        }
        public decimal Total {
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
        #endregion
        public int FileCount
        {
            set { _fileCount = value; }
            get { return _fileCount; }
        }
        public string FileCountForDisplay
        {
            get { return _fileCount.ToString("00"); }
        }

        public Estudio()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Estudio ( int EstudioId, int TipoEstudioId, string Nombre )
        {
            this._EstudioId = EstudioId;
            this._TipoEstudioId = TipoEstudioId.ToString();
            this._NombreTipoEstudio = Nombre;
        }

        public Estudio(int EstudioId, int CasoId
            , int ProveedorId, string TipoEstudioId, string Observaciones )
        {
            this._EstudioId = EstudioId;
            this._CasoId = CasoId;
            this._ProveedorId = ProveedorId;
            this._TipoEstudioId = TipoEstudioId;
            this._Observaciones = Observaciones;
        }

        public Estudio(int EstudioId, int CasoId
            , int ProveedorId, string TipoEstudioId, string Observaciones
            , int GastoId, int AprobacionUserId, DateTime AprobacionFecha
            , DateTime FechaCreacion, bool Cubierto, string NombreTipoEstudio, string NombreProveedor
            
            , decimal MontoConFactura, decimal MontoSinFactura
            ,decimal RetencionImpuesto, decimal Total)
        {
            this._EstudioId = EstudioId;
            this._CasoId = CasoId;
            this._ProveedorId = ProveedorId;
            this._TipoEstudioId = TipoEstudioId;
            this._Observaciones = Observaciones.ToUpper();
            this._GastoId = GastoId;
            this._AprobacionUserId = AprobacionUserId;
            this._AprobacionFecha = AprobacionFecha;
            this._FechaCreacion = FechaCreacion;
            this._Cubierto = Cubierto;

            this._NombreTipoEstudio = NombreTipoEstudio.ToUpper();
            this._NombreProveedor = NombreProveedor.ToUpper();

            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencionImpuesto;
            this._Total = Total;
        }

    }
}