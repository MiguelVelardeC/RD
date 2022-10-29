namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ConsultaClienteCovidPregunta
    {
        [Key]
        public int ConsultaClienteCovidPreguntaId { get; set; }

        public int ClienteId { get; set; }

        public int Seccion { get; set; }

        [Required]
        [StringLength(2)]
        public string Inciso { get; set; }

        [Required]
        [StringLength(500)]
        public string Pregunta { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
