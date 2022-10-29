namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usv_RED_Cliente_Prestaciones
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int preId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string prestacion { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string preTipoPrestacion { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClienteId { get; set; }

        public int? prePrecio { get; set; }

        public int? preCoPagoMonto { get; set; }

        public int? preCoPagoPorcentaje { get; set; }

        public int? preCantidadConsultasAno { get; set; }

        public int? preDiasCarencia { get; set; }

        public int? preMontoTope { get; set; }
    }
}
