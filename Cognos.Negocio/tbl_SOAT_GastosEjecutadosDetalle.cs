namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_GastosEjecutadosDetalle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SOAT_GastosEjecutadosDetalle()
        {
            tbl_SOAT_PagoGastos = new HashSet<tbl_SOAT_PagoGastos>();
        }

        [Key]
        public int GastosEjecutadosDetalleId { get; set; }

        public int GastosEjecutadosId { get; set; }

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

        public int PreliquidacionDetalleId { get; set; }

        public virtual tbl_SOAT_GastosEjecutados tbl_SOAT_GastosEjecutados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_PagoGastos> tbl_SOAT_PagoGastos { get; set; }
    }
}
