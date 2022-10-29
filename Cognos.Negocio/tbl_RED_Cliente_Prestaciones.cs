namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Cliente_Prestaciones
    {
        [Key]
        public int preId { get; set; }

        [Required]
        [StringLength(2)]
        public string preTipoPrestacion { get; set; }

        public int ClienteId { get; set; }

        public decimal? prePrecio { get; set; }

        public decimal? preCoPagoMonto { get; set; }

        public decimal? preCoPagoPorcentaje { get; set; }

        public int? preCantidadConsultasAno { get; set; }

        public int? preDiasCarencia { get; set; }

        public decimal? preMontoTope { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
