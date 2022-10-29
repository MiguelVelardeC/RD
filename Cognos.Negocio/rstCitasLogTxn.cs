namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstCitasLogTxn")]
    public partial class rstCitasLogTxn
    {
        [Key]
        public int logId { get; set; }

        public DateTime logFechaHora { get; set; }

        [Required]
        [StringLength(200)]
        public string logComentario { get; set; }

        [Required]
        [StringLength(2)]
        public string logEstado { get; set; }

        public int citId { get; set; }

        [Required]
        [StringLength(100)]
        public string UsuarioId { get; set; }

        [Required]
        [StringLength(1)]
        public string logTipoUsuario { get; set; }

        public virtual rstCitasTxn rstCitasTxn { get; set; }
    }
}
