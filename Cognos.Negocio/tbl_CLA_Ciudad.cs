namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_Ciudad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_Ciudad()
        {
            tbl_Caso = new HashSet<tbl_Caso>();
            tbl_DESG_CitaDesgravamen = new HashSet<tbl_DESG_CitaDesgravamen>();
            tbl_DESG_Financiera = new HashSet<tbl_DESG_Financiera>();
            tbl_DESG_ProveedorMedico = new HashSet<tbl_DESG_ProveedorMedico>();
            tbl_Paciente = new HashSet<tbl_Paciente>();
            tbl_RED_ProveedorCiudad = new HashSet<tbl_RED_ProveedorCiudad>();
            tbl_SEG_User = new HashSet<tbl_SEG_User>();
            tbl_SOAT_Proveedor = new HashSet<tbl_SOAT_Proveedor>();
            tbl_Internacion_Cirugia = new HashSet<tbl_Internacion_Cirugia>();
            tbl_Internacion_Internacion = new HashSet<tbl_Internacion_Internacion>();
        }

        [Key]
        [StringLength(3)]
        public string CiudadId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso> tbl_Caso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_CitaDesgravamen> tbl_DESG_CitaDesgravamen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_Financiera> tbl_DESG_Financiera { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProveedorMedico> tbl_DESG_ProveedorMedico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Paciente> tbl_Paciente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_ProveedorCiudad> tbl_RED_ProveedorCiudad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SEG_User> tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Proveedor> tbl_SOAT_Proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion_Cirugia> tbl_Internacion_Cirugia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion_Internacion> tbl_Internacion_Internacion { get; set; }
    }
}
