namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_Proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SOAT_Proveedor()
        {
            tbl_SOAT_GestionMedica = new HashSet<tbl_SOAT_GestionMedica>();
        }

        [Key]
        public int ProveedorId { get; set; }

        [Required]
        [StringLength(500)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string NIT { get; set; }

        [Required]
        [StringLength(3)]
        public string CiudadId { get; set; }

        public int ProveedorAlianzaId { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_GestionMedica> tbl_SOAT_GestionMedica { get; set; }
    }
}
