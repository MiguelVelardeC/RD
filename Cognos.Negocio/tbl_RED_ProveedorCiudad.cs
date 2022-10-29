namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_ProveedorCiudad
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProveedorId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string CiudadId { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(20)]
        public string Celular { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }
    }
}
