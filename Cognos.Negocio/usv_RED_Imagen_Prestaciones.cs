namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usv_RED_Imagen_Prestaciones
    {
        public long? detId { get; set; }

        [Key]
        [Column(Order = 0)]
        public int EstudioId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int? ParentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order { get; set; }

        [StringLength(2)]
        public string CategoriaId { get; set; }
    }
}
