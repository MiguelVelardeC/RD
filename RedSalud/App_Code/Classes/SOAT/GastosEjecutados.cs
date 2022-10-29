using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.GastosEjecutados
{
    public class GastosEjecutados
    {
        private int _gastosEjecutadosId;
        private int _siniestroId;
        private int _accidentadoId;
        private DateTime _fecha;
        private decimal _montoGestion;
        private decimal _reservas;
        private decimal _hospitalarios;
        private decimal _cirujia;
        private decimal _ambulancias;
        private decimal _laboratorios;
        private decimal _farmacia;
        private decimal _honorarios;
        private decimal _ambulatorios;
        private decimal _osteosintesis;
        private decimal _siniestrosPagados;

        public GastosEjecutados () { }

        public GastosEjecutados ( int gastosEjecutadosId, int siniestroId, int accidentadoId, DateTime fecha,
            decimal montoGestion,
            decimal reservas, decimal hospitalarios, decimal cirujia, decimal ambulancias, decimal laboratorios,
            decimal farmacia, decimal honorarios, decimal ambulatorios, decimal osteosintesis)
        {
            this._gastosEjecutadosId = gastosEjecutadosId;
            this._siniestroId = siniestroId;
            this._accidentadoId = accidentadoId;
            this._fecha = fecha;
            this._montoGestion = montoGestion;
            this._reservas = reservas;
            this._hospitalarios = hospitalarios;
            this._cirujia = cirujia;
            this._ambulancias = ambulancias;
            this._laboratorios = laboratorios;
            this._farmacia = farmacia;
            this._honorarios = honorarios;
            this._ambulatorios = ambulatorios;
            this._osteosintesis = osteosintesis;
        }

        public int GastosEjecutadosId
        {
            get { return _gastosEjecutadosId; }
            set { _gastosEjecutadosId = value; }
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

        public decimal MontoGestion
        {
            get { return _montoGestion; }
            set { _montoGestion = value; }
        }

        public decimal Reservas
        {
            get { return _reservas; }
            set { _reservas = value; }
        }

        public decimal Hospitalarios
        {
            get { return _hospitalarios; }
            set { _hospitalarios = value; }
        }

        public decimal Cirujia
        {
            get { return _cirujia; }
            set { _cirujia = value; }
        }

        public decimal Ambulancias
        {
            get { return _ambulancias; }
            set { _ambulancias = value; }
        }

        public decimal Laboratorios
        {
            get { return _laboratorios; }
            set { _laboratorios = value; }
        }

        public decimal Farmacia
        {
            get { return _farmacia; }
            set { _farmacia = value; }
        }

        public decimal Honorarios
        {
            get { return _honorarios; }
            set { _honorarios = value; }
        }

        public decimal Ambulatorios
        {
            get { return _ambulatorios; }
            set { _ambulatorios = value; }
        }

        public decimal Osteosintesis
        {
            get { return _osteosintesis; }
            set { _osteosintesis = value; }
        }

        public decimal SiniestrosPagados
        {
            get
            {
                return this._hospitalarios +
                this._cirujia +
                this._ambulancias +
                this._laboratorios +
                this._farmacia +
                this._honorarios +
                this._ambulatorios +
                this._osteosintesis;
            }
        }
    }
    public class GastosEjecutadosDetalle
    {
        private int _gastosEjecutadosDetalleId;
        private int _gastosEjecutadosId;
        private int _preliquidacionDetalleId;
        private int _siniestroId;
        private int _accidentadoId;
        private DateTime _fecha;
        private string _proveedor;
        private string _nombreProveedor;
        private DateTime _fechaReciboFactura;
        private string _numeroReciboFactura;
        private string _tipo;
        private decimal _cobertura;
        private decimal _monto;

        public GastosEjecutadosDetalle () { }
        public GastosEjecutadosDetalle ( int gastosEjecutadosDetalleId, int gastosEjecutadosId, string tipo, decimal monto )
        {
            this._gastosEjecutadosDetalleId = gastosEjecutadosDetalleId;
            this._gastosEjecutadosId = gastosEjecutadosId;
            this._tipo = tipo;
            this._monto = monto;
        }

        public GastosEjecutadosDetalle ( int gastosEjecutadosDetalleId, int gastosEjecutadosId, int preliquidacionDetalleId, DateTime fecha,
            string proveedor, DateTime fechaReciboFactura, string numeroReciboFactura, string tipo, decimal monto )
        {
            this._gastosEjecutadosDetalleId = gastosEjecutadosDetalleId;
            this._gastosEjecutadosId = gastosEjecutadosId;
            this._preliquidacionDetalleId = preliquidacionDetalleId;
            this._fecha = fecha;
            this._proveedor = proveedor;
            this._fechaReciboFactura = fechaReciboFactura;
            this._numeroReciboFactura = numeroReciboFactura;
            this._tipo = tipo;
            this._monto = monto;
        }

        public int GastosEjecutadosDetalleId
        {
            get { return _gastosEjecutadosDetalleId; }
            set { _gastosEjecutadosDetalleId = value; }
        }

        public int GastosEjecutadosId
        {
            get { return _gastosEjecutadosId; }
            set { _gastosEjecutadosId = value; }
        }
        public string GastosEjecutadosIdForDisplay
        {
            get
            {
                if (Tipo == "RESERVAS" || Tipo == "TOTALES" || Tipo == "AHORRO" || Tipo == "SALDO")
                    return "";
                else
                    return _gastosEjecutadosId.ToString();
            }
        }

        public int PreliquidacionDetalleId
        {
            get { return _preliquidacionDetalleId; }
            set { _preliquidacionDetalleId = value; }
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
                    case "HOSPITALARIOS":
                        return "Gastos Hospitalarios";
                    case "CIRUGIA":
                        return "Gastos de Cirugías";
                    case "AMBULANCIAS":
                        return "Gastos de Ambulancias";
                    case "LABORATORIOS":
                        return "Gastos de Laboratorios e Imágenes";
                    case "FARMACIAS":
                        return "Gastos de Farmacia";
                    case "HONORARIOS":
                        return "Gastos de Honorarios Profesionales";
                    case "AMBULATORIO":
                        return "Gastos de Ambulatorios";
                    case "OSTEOSINTESIS":
                        return "Gastos de Material de Osteosíntesis";
                    case "REEMBOLSO":
                        return "Gastos de Reembolso";
                    case "TOTALES":
                        return "Gastos Totales";
                    case "AHORRO":
                        return "Ahorro";
                    case "SALDO":
                        return "Saldo";
                    default:
                        return "Gastos sin clasificar";
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
            get
            {
                if (_tipo == "SALDO")
                {
                    return _monto > 0 ? "AUN TIENE SALDO" : (_monto == 0 ? "-" : "EXCEDE LIMITE SOAT");
                }
                else
                {
                    return _monto.ToString("#,##0.00");
                }
            }
        }

        public bool IsFactura { get; set; }
        public string IsFacturaDisplay
        {
            get
            {
                return (IsFactura)? "FACTURA": "RECIBO";
            }
        }
    }
}