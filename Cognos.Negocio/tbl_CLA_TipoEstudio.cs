namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_TipoEstudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_TipoEstudio()
        {
            tbl_Caso_LaboratoriosImagenologia = new HashSet<tbl_Caso_LaboratoriosImagenologia>();
            tbl_PlanPrestacion = new HashSet<tbl_PlanPrestacion>();
            tbl_RED_Cliente_Imagenologia = new HashSet<tbl_RED_Cliente_Imagenologia>();
            tbl_RED_Cliente_Laboratorios = new HashSet<tbl_RED_Cliente_Laboratorios>();
            tbl_RED_Proveedor_LaboratoriosImagenologia = new HashSet<tbl_RED_Proveedor_LaboratoriosImagenologia>();
            tbl_RED_Proveedor_LaboratoriosImagenologia1 = new HashSet<tbl_RED_Proveedor_LaboratoriosImagenologia>();
            tbl_Estudio = new HashSet<tbl_Estudio>();
        }

        [Key]
        public int EstudioId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int? ParentId { get; set; }

        public int Order { get; set; }

        [StringLength(2)]
        public string CategoriaId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_LaboratoriosImagenologia> tbl_Caso_LaboratoriosImagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PlanPrestacion> tbl_PlanPrestacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Imagenologia> tbl_RED_Cliente_Imagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Laboratorios> tbl_RED_Cliente_Laboratorios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor_LaboratoriosImagenologia> tbl_RED_Proveedor_LaboratoriosImagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor_LaboratoriosImagenologia> tbl_RED_Proveedor_LaboratoriosImagenologia1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Estudio> tbl_Estudio { get; set; }
    }
}
