namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_PreliquidacionDetalle
    {
        [Key]
        public int PreliquidacionDetalleId { get; set; }

        public int PreliquidacionId { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }

        public decimal Monto { get; set; }

        [Required]
        [StringLength(500)]
        public string Proveedor { get; set; }

        public DateTime FechaReciboFactura { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroReciboFactura { get; set; }

        public bool IsFactura { get; set; }

        public bool Estado { get; set; }

        public virtual tbl_SOAT_Preliquidacion tbl_SOAT_Preliquidacion { get; set; }
    }
}
