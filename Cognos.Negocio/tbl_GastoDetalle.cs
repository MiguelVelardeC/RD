namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_GastoDetalle
    {
        [Key]
        public int GastoDetalleId { get; set; }

        public int GastoId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaGasto { get; set; }

        public decimal Monto { get; set; }

        public int NroFacturaRecibo { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoDocumento { get; set; }

        public int? fileId { get; set; }

        public int? ConsolidacionId { get; set; }

        public bool? Aceptado { get; set; }

        public bool? Rechazado { get; set; }

        public decimal? RetencionImpuestoPorcentaje { get; set; }

        public virtual tbl_Consolidacion tbl_Consolidacion { get; set; }

        public virtual tbl_File tbl_File { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }
    }
}
