using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Preliquidacion
{
    /// <summary>
    /// Summary description for PreliquidacionSearch
    /// </summary>
    public class PreliquidacionSearch
    {
        public PreliquidacionSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int PreliquidacionDetalleId { get; set; }
        public string NumeroRoseta { get; set; }
        public string TipoGasto { get; set; }
        public string Paciente { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public DateTime FechaEmision { get; set; }
        public string IsFactura { get; set; }
        public string NumeroReciboFactura { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string TieneFactura { get; set; }
        public string NumeroFactura { get; set; }
        public decimal MontoFactura { get; set; }
        public int RowNumber { get; set; }
        public int GastosEjecutadosId { get; set; }
        public int GastosEjecutadosDetalleId { get; set; }
        public int PagoGastosId { get; set; }
        public DateTime FechaPago { get; set; }
        public int CantidadDias { get; set; }
        public string FechaPagoForDisplay
        {
            get
            {
                if (FechaPago != DateTime.MinValue)
                    return FechaPago.ToString("dd/MM/yyyy");

                return "-";
            }
        }

        public string EstadoPagoForDisplay
        {
            get
            {
                if (PagoGastosId > 0)
                    return "PAGADO";
                return "PENDIENTE";
            }
        }
    }
}