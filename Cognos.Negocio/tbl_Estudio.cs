namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Estudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Estudio()
        {
            tbl_Caso_LaboratoriosImagenologia = new HashSet<tbl_Caso_LaboratoriosImagenologia>();
            tbl_EstudioFile = new HashSet<tbl_EstudioFile>();
            tbl_CLA_TipoEstudio = new HashSet<tbl_CLA_TipoEstudio>();
        }

        [Key]
        public int EstudioId { get; set; }

        public int CasoId { get; set; }

        public int ProveedorId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Observacion { get; set; }

        public int? GastoId { get; set; }

        public int? AprobacionUserId { get; set; }

        public DateTime? AprobacionFecha { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public bool Cubierto { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_LaboratoriosImagenologia> tbl_Caso_LaboratoriosImagenologia { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_EstudioFile> tbl_EstudioFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_TipoEstudio> tbl_CLA_TipoEstudio { get; set; }
    }
}
