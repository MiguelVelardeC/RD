
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.PagoGastos
{
    public class PagoGastos
    {
        private int _pagoGastosId;
        private int _gastosEjecutadosDetalleId;
        private int _userId;
        private DateTime _fechaPago;
        private bool _efectivo;
        private string _nroCheque;
        private string _bancoEmisor;

        public PagoGastos () { }

        public PagoGastos ( int pagoGastosId, int gastosEjecutadosDetalleId, int userId, DateTime fechaPago, bool efectivo,
            string nroCheque, string bancoEmisor )
        {
            this._pagoGastosId = pagoGastosId;
            this._gastosEjecutadosDetalleId = gastosEjecutadosDetalleId;
            this._userId = userId;
            this._fechaPago = fechaPago;
            this._efectivo = efectivo;
            this._nroCheque = nroCheque;
            this._bancoEmisor = bancoEmisor;
        }

        public int PagoGastosId
        {
            get { return _pagoGastosId; }
            set { _pagoGastosId = value; }
        }

        public int GastosEjecutadosDetalleId
        {
            get { return _gastosEjecutadosDetalleId; }
            set { _gastosEjecutadosDetalleId = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public DateTime FechaPago
        {
            get { return _fechaPago; }
            set { _fechaPago = value; }
        }

        public bool Efectivo
        {
            get { return _efectivo; }
            set { _efectivo = value; }
        }

        public string NroCheque
        {
            get { return _nroCheque; }
            set { _nroCheque = value; }
        }

        public string BancoEmisor
        {
            get { return _bancoEmisor; }
            set { _bancoEmisor = value; }
        }
    }
}