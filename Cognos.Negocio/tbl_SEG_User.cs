namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SEG_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SEG_User()
        {
            tbl_Caso = new HashSet<tbl_Caso>();
            tbl_Consolidacion = new HashSet<tbl_Consolidacion>();
            tbl_Derivacion = new HashSet<tbl_Derivacion>();
            tbl_DESG_CitaDesgravamen = new HashSet<tbl_DESG_CitaDesgravamen>();
            tbl_DESG_ClienteUsuario = new HashSet<tbl_DESG_ClienteUsuario>();
            tbl_DESG_Medico = new HashSet<tbl_DESG_Medico>();
            tbl_DESG_ProveedorMedico = new HashSet<tbl_DESG_ProveedorMedico>();
            tbl_Estudio = new HashSet<tbl_Estudio>();
            tbl_Internacion = new HashSet<tbl_Internacion>();
            tbl_RED_Medico = new HashSet<tbl_RED_Medico>();
            tbl_RED_Proveedor = new HashSet<tbl_RED_Proveedor>();
            tbl_SOAT_PagoGastos = new HashSet<tbl_SOAT_PagoGastos>();
            tbl_RED_Cliente = new HashSet<tbl_RED_Cliente>();
            tbl_RED_Cliente1 = new HashSet<tbl_RED_Cliente>();
            tbl_RED_Cliente2 = new HashSet<tbl_RED_Cliente>();
            tbl_RED_Proveedor1 = new HashSet<tbl_RED_Proveedor>();
        }

        [Key]
        public int userId { get; set; }

        [Required]
        [StringLength(500)]
        public string fullname { get; set; }

        [StringLength(50)]
        public string cellphone { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [StringLength(25)]
        public string phonenumber { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        public int? phonearea { get; set; }

        public int? phonecode { get; set; }

        [Required]
        [StringLength(3)]
        public string ciudadId { get; set; }

        public int? signatureFileId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso> tbl_Caso { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Consolidacion> tbl_Consolidacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Derivacion> tbl_Derivacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_CitaDesgravamen> tbl_DESG_CitaDesgravamen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ClienteUsuario> tbl_DESG_ClienteUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_Medico> tbl_DESG_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProveedorMedico> tbl_DESG_ProveedorMedico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Estudio> tbl_Estudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion> tbl_Internacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Medico> tbl_RED_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor> tbl_RED_Proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_PagoGastos> tbl_SOAT_PagoGastos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente> tbl_RED_Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente> tbl_RED_Cliente1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente> tbl_RED_Cliente2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor> tbl_RED_Proveedor1 { get; set; }
    }
}
