using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.GastoDetalle
{
    /// <summary>
    /// Summary description for GastoDetalle
    /// </summary>
    public class GastoDetalle
    {
        private int _GastoDetalleId;
        private int _GastoId;
        private DateTime _FechaCreacion;
        private DateTime _FechaGasto;
        private decimal _Monto;
        private int _NroFacturaRecibo;
        private string _TipoDucumento;//factura/recibo
        private int _FileId;

        private int _ConsolidacionId;
        private bool _Aceptado;
        private bool _Rechazado;

        public int GastoDetalleID
        {
            get { return this._GastoDetalleId; }
            set { this._GastoDetalleId = value; }
        }
        public int GastoId {
            get { return this._GastoId; }
            set { this._GastoId = value; }
        }
        public DateTime FechaCreacion
        {
            get { return this._FechaCreacion; }
            set { this._FechaCreacion = value; }
        }
        public DateTime FechaGasto {
            get { return this._FechaGasto; }
            set { this._FechaGasto = value; }
        }
        public decimal Monto {
            get { return this._Monto; }
            set { this._Monto = value; }
        }
        public string MontoForDisplay
        {
            get { return this._Monto.ToString("#,##0.00 Bs"); }
        }
        public int NroFacturaRecibo {
            get { return this._NroFacturaRecibo; }
            set { this._NroFacturaRecibo = value; }
        }
        public string TipoDocumento {
            get { return this._TipoDucumento; }
            set { this._TipoDucumento = value; }
        }
        public int FileId {
            get { return this._FileId; }
            set { this._FileId = value; }
        }

        public int ConsolidacionId {
            get { return this._ConsolidacionId; }
            set { this._ConsolidacionId = value; }
        }
        public bool Aceptado {
            get { return this._Aceptado; }
            set { this._Aceptado = value; }
        }
        public bool Rechazado {
            get { return this._Rechazado; }
            set { this._Rechazado = value; }
        }
        public bool EnableDelete {
            get
            {
                if (_ConsolidacionId <=0)
                    return true;
                else
                    return false;
            }
        }

        public GastoDetalle() {}

        public GastoDetalle(int GastoDetalleId, int GastoId
            , DateTime FechaCreacion, DateTime FechaGasto
            , decimal Monto, int NroFactura
            , string TipoDocumento, int FileId
            , int ConsolidacionId, bool Aceptado, bool Rechazado )
        {
            this._GastoDetalleId = GastoDetalleId;
            this._GastoId = GastoId;
            this._FechaCreacion = FechaCreacion;
            this._FechaGasto = FechaGasto;
            this._Monto = Monto;
            this._NroFacturaRecibo = NroFactura;
            this._TipoDucumento = TipoDocumento;
            this._FileId = FileId;
            this._ConsolidacionId = ConsolidacionId;
            this._Aceptado = Aceptado;
            this._Rechazado = Rechazado;
        }
    }
}