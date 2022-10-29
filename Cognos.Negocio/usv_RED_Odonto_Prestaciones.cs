namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usv_RED_Odonto_Prestaciones
    {
        public long? detId { get; set; }

        [Key]
        [Column(Order = 0)]
        public int PrestacionOdontologicaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string Nombre { get; set; }

        public bool? Categoria { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int detCantidadConsultasAno { get; set; }
    }
}
