namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Cliente_Cirugias
    {
        [Key]
        public int detId { get; set; }

        [Required]
        [StringLength(10)]
        public string CodigoArancelarioId { get; set; }

        public int ClienteId { get; set; }

        public decimal? detMontoTope { get; set; }

        public int? detCantidadTope { get; set; }

        public virtual tbl_CLA_CodigoArancelario tbl_CLA_CodigoArancelario { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
