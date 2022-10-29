namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstClienteParametrosAPIVL")]
    public partial class rstClienteParametrosAPIVL
    {
        [Key]
        public int parId { get; set; }

        public int ClienteId { get; set; }

        [Required]
        [StringLength(100)]
        public string parContrasena { get; set; }

        public bool parEsNumeroPolizaPredefinida { get; set; }

        [StringLength(20)]
        public string parNumeroPoliza { get; set; }

        public DateTime? parFechaInicio { get; set; }

        public DateTime? parFechaFin { get; set; }

        [StringLength(200)]
        public string parNombrePlan { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
