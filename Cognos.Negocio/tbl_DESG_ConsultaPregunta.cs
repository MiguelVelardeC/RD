namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ConsultaPregunta
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int seccion { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string inciso { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clienteId { get; set; }

        [Required]
        [StringLength(250)]
        public string pregunta { get; set; }

        public bool preguntaAbierta { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
