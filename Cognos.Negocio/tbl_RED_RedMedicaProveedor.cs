namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_RedMedicaProveedor
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RedMedicaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProveedorId { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }

        public virtual tbl_RED_RedMedica tbl_RED_RedMedica { get; set; }
    }
}
