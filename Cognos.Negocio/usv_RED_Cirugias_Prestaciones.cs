namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usv_RED_Cirugias_Prestaciones
    {
        public long? detId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string CodigoArancelarioId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2000)]
        public string Nombre { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int detMontoTope { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int detCantidadTope { get; set; }
    }
}
