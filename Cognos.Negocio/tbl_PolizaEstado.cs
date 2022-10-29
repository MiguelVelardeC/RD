namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_PolizaEstado
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PolizaId { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Fecha { get; set; }

        public virtual tbl_Poliza tbl_Poliza { get; set; }
    }
}
