namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Proveedor_LaboratoriosImagenologia
    {
        [Key]
        public int detId { get; set; }

        public int ProveedorId { get; set; }

        [Required]
        [StringLength(2)]
        public string CategoriaId { get; set; }

        public int? ParentId { get; set; }

        public int EstudioId { get; set; }

        public decimal detPrecio { get; set; }

        public virtual tbl_CLA_TipoEstudio tbl_CLA_TipoEstudio { get; set; }

        public virtual tbl_CLA_TipoEstudio tbl_CLA_TipoEstudio1 { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }
    }
}
