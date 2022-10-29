namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UsuarioServicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_UsuarioServicio()
        {
            tbl_UsuarioServicioCliente = new HashSet<tbl_UsuarioServicioCliente>();
        }

        [Key]
        public int UsuarioServicioID { get; set; }

        [Required]
        [StringLength(100)]
        public string Usuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Contrasena { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public bool Habilitado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UsuarioServicioCliente> tbl_UsuarioServicioCliente { get; set; }
    }
}
