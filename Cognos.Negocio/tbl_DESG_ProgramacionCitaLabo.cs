namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ProgramacionCitaLabo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proveedorMedicoId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int estudioId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime fechaCita { get; set; }

        public DateTime fechaAtencion { get; set; }

        [Required]
        [StringLength(1000)]
        public string observacion { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public bool aprobado { get; set; }

        public virtual tbl_DESG_CitaDesgravamen tbl_DESG_CitaDesgravamen { get; set; }

        public virtual tbl_DESG_Estudio tbl_DESG_Estudio { get; set; }

        public virtual tbl_DESG_ProveedorMedico tbl_DESG_ProveedorMedico { get; set; }
    }
}
