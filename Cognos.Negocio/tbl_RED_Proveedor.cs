namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RED_Proveedor()
        {
            rstCitasDetalleTxn = new HashSet<rstCitasDetalleTxn>();
            tbl_Caso_Especialidad = new HashSet<tbl_Caso_Especialidad>();
            tbl_Caso_LaboratoriosImagenologia = new HashSet<tbl_Caso_LaboratoriosImagenologia>();
            tbl_Cita = new HashSet<tbl_Cita>();
            tbl_Consolidacion = new HashSet<tbl_Consolidacion>();
            tbl_Derivacion = new HashSet<tbl_Derivacion>();
            tbl_Emergencia = new HashSet<tbl_Emergencia>();
            tbl_Estudio = new HashSet<tbl_Estudio>();
            tbl_Internacion = new HashSet<tbl_Internacion>();
            tbl_Receta = new HashSet<tbl_Receta>();
            tbl_RED_Especialista = new HashSet<tbl_RED_Especialista>();
            tbl_RED_ProveedorCiudad = new HashSet<tbl_RED_ProveedorCiudad>();
            tbl_RED_RedMedicaProveedor = new HashSet<tbl_RED_RedMedicaProveedor>();
            tbl_RED_Proveedor_LaboratoriosImagenologia = new HashSet<tbl_RED_Proveedor_LaboratoriosImagenologia>();
            tbl_RED_Proveedor_Odontologia = new HashSet<tbl_RED_Proveedor_Odontologia>();
            tbl_SEG_User1 = new HashSet<tbl_SEG_User>();
        }

        [Key]
        public int ProveedorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreJuridico { get; set; }

        public decimal Nit { get; set; }

        [Required]
        [StringLength(250)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(20)]
        public string TelefonoCasa { get; set; }

        [Required]
        [StringLength(20)]
        public string TelefonoOficina { get; set; }

        [Required]
        [StringLength(20)]
        public string Celular { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        [Required]
        [StringLength(1000)]
        public string Observacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoProveedorId { get; set; }

        public int? userIdProv { get; set; }

        public int? MedicoIdProv { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasDetalleTxn> rstCitasDetalleTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Especialidad> tbl_Caso_Especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_LaboratoriosImagenologia> tbl_Caso_LaboratoriosImagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cita> tbl_Cita { get; set; }

        public virtual tbl_CLA_TipoProveedor tbl_CLA_TipoProveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Consolidacion> tbl_Consolidacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Derivacion> tbl_Derivacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Emergencia> tbl_Emergencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Estudio> tbl_Estudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion> tbl_Internacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Receta> tbl_Receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Especialista> tbl_RED_Especialista { get; set; }

        public virtual tbl_RED_Medico tbl_RED_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_ProveedorCiudad> tbl_RED_ProveedorCiudad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_RedMedicaProveedor> tbl_RED_RedMedicaProveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor_LaboratoriosImagenologia> tbl_RED_Proveedor_LaboratoriosImagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor_Odontologia> tbl_RED_Proveedor_Odontologia { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SEG_User> tbl_SEG_User1 { get; set; }
    }
}
