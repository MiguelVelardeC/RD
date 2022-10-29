namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_Seguimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SiniestroId { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public bool Acuerdo { get; set; }

        public bool Rechazado { get; set; }

        [Required]
        public string Observaciones { get; set; }

        public virtual tbl_SOAT_Siniestro tbl_SOAT_Siniestro { get; set; }
    }
}
