namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_Estudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DESG_Estudio()
        {
            tbl_DESG_Estudio1 = new HashSet<tbl_DESG_Estudio>();
            tbl_DESG_EstudioProveedor = new HashSet<tbl_DESG_EstudioProveedor>();
            tbl_DESG_LaboratorioEstudio = new HashSet<tbl_DESG_LaboratorioEstudio>();
            tbl_DESG_LaboratorioFile = new HashSet<tbl_DESG_LaboratorioFile>();
            tbl_DESG_ProgramacionCitaLabo = new HashSet<tbl_DESG_ProgramacionCitaLabo>();
            tbl_DESG_ProveedorMedicoHorarios = new HashSet<tbl_DESG_ProveedorMedicoHorarios>();
            tbl_DESG_EstudioGrupo = new HashSet<tbl_DESG_EstudioGrupo>();
        }

        [Key]
        public int estudioId { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        public int? parentEstudioId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_Estudio> tbl_DESG_Estudio1 { get; set; }

        public virtual tbl_DESG_Estudio tbl_DESG_Estudio2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_EstudioProveedor> tbl_DESG_EstudioProveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioEstudio> tbl_DESG_LaboratorioEstudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioFile> tbl_DESG_LaboratorioFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProgramacionCitaLabo> tbl_DESG_ProgramacionCitaLabo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProveedorMedicoHorarios> tbl_DESG_ProveedorMedicoHorarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_EstudioGrupo> tbl_DESG_EstudioGrupo { get; set; }
    }
}
