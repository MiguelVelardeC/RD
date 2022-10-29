namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Especialista
    {
        [Key]
        public int EspecialistaId { get; set; }

        public int ProveedorId { get; set; }

        public int EspecialidadId { get; set; }

        [Required]
        [StringLength(50)]
        public string Sedes { get; set; }

        [Required]
        [StringLength(50)]
        public string ColegioMedico { get; set; }

        public decimal? CostoConsulta { get; set; }

        public decimal? PorcentageDescuento { get; set; }

        public virtual tbl_CLA_Especialidad tbl_CLA_Especialidad { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }
    }
}
