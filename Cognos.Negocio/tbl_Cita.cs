namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Cita
    {
        [Key]
        public int CitaId { get; set; }

        public int? MedicoId { get; set; }

        public int? ProveedorId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(500)]
        public string Subject { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public int? PacienteId { get; set; }

        public int? CasoId { get; set; }

        public virtual tbl_Paciente tbl_Paciente { get; set; }

        public virtual tbl_RED_Medico tbl_RED_Medico { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }
    }
}
