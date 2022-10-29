namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_LOG_Bitacoras
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime fecha { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string tipoEvento { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string empleado { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string tipoObjeto { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string idObjeto { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(8000)]
        public string mensaje { get; set; }
    }
}
