namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_GestionMedica
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SiniestroId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccidentadoId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GestionMedicaId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        public DateTime FechaVisita { get; set; }

        [Required]
        public string DiagnosticoPreliminar { get; set; }

        [Required]
        [StringLength(10)]
        public string Grado { get; set; }

        public int? ProveedorId { get; set; }

        public virtual tbl_SOAT_Accidentado tbl_SOAT_Accidentado { get; set; }

        public virtual tbl_SOAT_Proveedor tbl_SOAT_Proveedor { get; set; }

        public virtual tbl_SOAT_Siniestro tbl_SOAT_Siniestro { get; set; }
    }
}
