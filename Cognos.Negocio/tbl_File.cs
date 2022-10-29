namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_File
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_File()
        {
            tbl_DerivacionFile = new HashSet<tbl_DerivacionFile>();
            tbl_DESG_LaboratorioFile = new HashSet<tbl_DESG_LaboratorioFile>();
            tbl_DESG_PropuestoAsegurado = new HashSet<tbl_DESG_PropuestoAsegurado>();
            tbl_EmergenciaFile = new HashSet<tbl_EmergenciaFile>();
            tbl_EstudioFile = new HashSet<tbl_EstudioFile>();
            tbl_GastoDetalle = new HashSet<tbl_GastoDetalle>();
            tbl_InternacionFile = new HashSet<tbl_InternacionFile>();
            tbl_MedicamentoFile = new HashSet<tbl_MedicamentoFile>();
            tbl_OdontologiaFile = new HashSet<tbl_OdontologiaFile>();
            tbl_Paciente = new HashSet<tbl_Paciente>();
            tbl_RecetaFile = new HashSet<tbl_RecetaFile>();
            tbl_RED_Medico = new HashSet<tbl_RED_Medico>();
            tbl_Caso_Emergencia = new HashSet<tbl_Caso_Emergencia>();
            tbl_SOAT_Accidentado = new HashSet<tbl_SOAT_Accidentado>();
            tbl_SOAT_Siniestro = new HashSet<tbl_SOAT_Siniestro>();
        }

        [Key]
        public int fileId { get; set; }

        [Required]
        [StringLength(1000)]
        public string fileName { get; set; }

        [Required]
        [StringLength(50)]
        public string extension { get; set; }

        [Required]
        public string documentText { get; set; }

        public DateTime dateUploaded { get; set; }

        public long fileSize { get; set; }

        [Required]
        public string fileStoragePath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DerivacionFile> tbl_DerivacionFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioFile> tbl_DESG_LaboratorioFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_PropuestoAsegurado> tbl_DESG_PropuestoAsegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_EmergenciaFile> tbl_EmergenciaFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_EstudioFile> tbl_EstudioFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GastoDetalle> tbl_GastoDetalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_InternacionFile> tbl_InternacionFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_MedicamentoFile> tbl_MedicamentoFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OdontologiaFile> tbl_OdontologiaFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Paciente> tbl_Paciente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RecetaFile> tbl_RecetaFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Medico> tbl_RED_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Emergencia> tbl_Caso_Emergencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Accidentado> tbl_SOAT_Accidentado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Siniestro> tbl_SOAT_Siniestro { get; set; }
    }
}
