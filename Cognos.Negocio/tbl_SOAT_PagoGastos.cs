namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_PagoGastos
    {
        [Key]
        public int PagoGastosId { get; set; }

        public int GastosEjecutadosDetalleId { get; set; }

        public int UserId { get; set; }

        public DateTime FechaPago { get; set; }

        public bool Efectivo { get; set; }

        [Required]
        [StringLength(50)]
        public string NroCheque { get; set; }

        [Required]
        [StringLength(250)]
        public string bancoEmisor { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        public virtual tbl_SOAT_GastosEjecutadosDetalle tbl_SOAT_GastosEjecutadosDetalle { get; set; }
    }
}
