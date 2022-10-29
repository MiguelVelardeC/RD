namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ProveedorMedico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DESG_ProveedorMedico()
        {
            tbl_DESG_EstudioProveedor = new HashSet<tbl_DESG_EstudioProveedor>();
            tbl_DESG_LaboratorioEstudio = new HashSet<tbl_DESG_LaboratorioEstudio>();
            tbl_DESG_LaboratorioFile = new HashSet<tbl_DESG_LaboratorioFile>();
            tbl_DESG_Medico = new HashSet<tbl_DESG_Medico>();
            tbl_DESG_ProgramacionCitaLabo = new HashSet<tbl_DESG_ProgramacionCitaLabo>();
            tbl_DESG_ProveedorMedicoHorarios = new HashSet<tbl_DESG_ProveedorMedicoHorarios>();
        }

        [Key]
        public int proveedorMedicoId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombre { get; set; }

        [Required]
        [StringLength(3)]
        public string ciudadId { get; set; }

        public int userId { get; set; }

        public TimeSpan horaInicioCitas { get; set; }

        public TimeSpan horaFinCitas { get; set; }

        public int duracionCita { get; set; }

        public bool principal { get; set; }

        public bool activo { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_EstudioProveedor> tbl_DESG_EstudioProveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioEstudio> tbl_DESG_LaboratorioEstudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioFile> tbl_DESG_LaboratorioFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_Medico> tbl_DESG_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProgramacionCitaLabo> tbl_DESG_ProgramacionCitaLabo { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProveedorMedicoHorarios> tbl_DESG_ProveedorMedicoHorarios { get; set; }
    }
}
