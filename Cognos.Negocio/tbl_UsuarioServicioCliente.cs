namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UsuarioServicioCliente
    {
        [Key]
        public int UsuarioServicioCliente { get; set; }

        public int? UsuarioServicioID { get; set; }

        public int? ClienteID { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }

        public virtual tbl_UsuarioServicio tbl_UsuarioServicio { get; set; }
    }
}
