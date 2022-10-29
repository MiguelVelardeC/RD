namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ProgramacionCita
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        public int? medicoDesgravamenId { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime fechaHoraCita { get; set; }

        public DateTime fechaCreacion { get; set; }

        public DateTime fechaUltimaModificacion { get; set; }

        public bool aprobado { get; set; }

        [Required]
        [StringLength(1000)]
        public string observacionLabo { get; set; }

        public virtual tbl_DESG_ProgramacionCita tbl_DESG_ProgramacionCita1 { get; set; }

        public virtual tbl_DESG_ProgramacionCita tbl_DESG_ProgramacionCita2 { get; set; }
    }
}
