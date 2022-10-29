using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Preliquidacion
{
    public class PreliquidacionDetalle
    {
        private int _PreliquidacionDetalleId;
        private int _PreliquidacionId;
        private int _siniestroId;
        private int _accidentadoId;
        private DateTime _fecha;
        private string _proveedor;
        private string _nombreProveedor;
        private DateTime _fechaReciboFactura;
        private string _numeroReciboFactura;
        private bool _isFactura;
        private string _tipo;
        private decimal _cobertura;
        private decimal _monto;
        private bool _estado;

        public PreliquidacionDetalle () { }
        public PreliquidacionDetalle ( int PreliquidacionDetalleId, int PreliquidacionId, string tipo, decimal monto )
        {
            this._PreliquidacionDetalleId = PreliquidacionDetalleId;
            this._PreliquidacionId = PreliquidacionId;
            this._tipo = tipo;
            this._monto = monto;
        }

        public PreliquidacionDetalle ( int PreliquidacionDetalleId, int PreliquidacionId, DateTime fecha,
            string proveedor, DateTime fechaReciboFactura, string numeroReciboFactura, bool isFactura, string tipo, decimal monto, bool estado )
        {
            this._PreliquidacionDetalleId = PreliquidacionDetalleId;
            this._PreliquidacionId = PreliquidacionId;
            this._fecha = fecha;
            this._proveedor = proveedor;
            this._fechaReciboFactura = fechaReciboFactura;
            this._numeroReciboFactura = numeroReciboFactura;
            this._isFactura = isFactura;
            this._tipo = tipo;
            this._monto = monto;
            this._estado = estado;
        }

        public int PreliquidacionDetalleId
        {
            get { return _PreliquidacionDetalleId; }
            set { _PreliquidacionDetalleId = value; }
        }

        public int PreliquidacionId
        {
            get { return _PreliquidacionId; }
            set { _PreliquidacionId = value; }
        }
        public string PreliquidacionIdForDisplay
        {
            get
            {
                if (Tipo == "RESERVAS" || Tipo == "TOTALES" || Tipo == "SALDO")
                    return "";
                else
                    return _PreliquidacionId.ToString();
            }
        }

        public int SiniestroId
        {
            get { return _siniestroId; }
            set { _siniestroId = value; }
        }

        public int AccidentadoId
        {
            get { return _accidentadoId; }
            set { _accidentadoId = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string FechaVisitaForDisplay
        {
            get { return _fecha.ToShortDateString(); }
        }

        public string Proveedor
        {
            get { return _proveedor; }
            set { this._proveedor = value; }
        }
        public string NombreProveedor
        {
            get { return _nombreProveedor; }
        }
        public DateTime FechaReciboFactura
        {
            get { return _fechaReciboFactura; }
            set { this._fechaReciboFactura = value; }
        }

        public string FechaReciboFacturaForDisplay
        {
            get { return _fechaReciboFactura.ToShortDateString(); }
        }
        public string NumeroReciboFactura
        {
            get { return _numeroReciboFactura; }
            set { this._numeroReciboFactura = value; }
        }
        public bool IsFactura
        {
            get { return _isFactura; }
            set { this._isFactura = value; }
        }
        public string IsFacturaForDisplay
        {
            get { return _isFactura ? "SI" : "NO"; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string TipoForDisplay
        {
            get {
                switch (_tipo)
                {
                    case "RESERVAS":
                        return "Reserva estimada";
                    case "CIRUGIA":
                        return "Proforma de Gastos de Cirugías";
                    case "AMBULANCIAS":
                        return "Proforma de Gastos de Ambulancias";
                    case "LABORATORIOS":
                        return "Proforma de Gastos de Laboratorios e Imágenes";
                    case "FARMACIAS":
                        return "Proforma de Gastos de Farmacia";
                    case "HONORARIOS":
                        return "Proforma de Gastos de Honorarios Profesionales";
                    case "AMBULATORIO":
                        return "Proforma de Gastos de Ambulatorios";
                    case "OSTEOSINTESIS":
                        return "Proforma de Gastos de Material de Osteosíntesis";
                    case "REEMBOLSO":
                        return "Proforma de Gastos de Reembolso";
                    case "TOTALES":
                        return "Total de Proforma";
                    default:
                        return "Proforma de Gastos Hospitalarios";
                }
            }
        }

        public decimal Cobertura
        {
            get
            {
                if (_cobertura <= 0)
                {
                    return Configuration.Configuration.GetMontoGestion();
                }
                else
                {
                    return _cobertura;
                }
            }
            set { _cobertura = value; }
        }

        public decimal Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }
        public string MontoForDisplay
        {
            get { return _monto.ToString("#,##0.00"); }
        }
        public bool Estado
        {
            get { return _estado; }
            set { this._estado = value; }
        }
        public string EstadoForDisplay
        {
            get { return _estado ? "APROBADO" : "OBSERVADO"; }
        }
    }
}