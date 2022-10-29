namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_LaboratorioFile
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fileId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proveedorMedicoId { get; set; }

        public int estudioId { get; set; }

        public virtual tbl_DESG_CitaDesgravamen tbl_DESG_CitaDesgravamen { get; set; }

        public virtual tbl_DESG_Estudio tbl_DESG_Estudio { get; set; }

        public virtual tbl_DESG_ProveedorMedico tbl_DESG_ProveedorMedico { get; set; }

        public virtual tbl_File tbl_File { get; set; }
    }
}
