namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_Financiera
    {
        [Key]
        public int financieraId { get; set; }

        [Required]
        [StringLength(3)]
        public string centralCiudadId { get; set; }

        public int clienteId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string nit { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
