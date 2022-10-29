namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Caso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Caso()
        {
            tbl_Derivacion = new HashSet<tbl_Derivacion>();
            tbl_Emergencia = new HashSet<tbl_Emergencia>();
            tbl_Estudio = new HashSet<tbl_Estudio>();
            tbl_Historia = new HashSet<tbl_Historia>();
            tbl_Internacion = new HashSet<tbl_Internacion>();
            tbl_Medicamento = new HashSet<tbl_Medicamento>();
            tbl_Odontologia = new HashSet<tbl_Odontologia>();
            tbl_Receta = new HashSet<tbl_Receta>();
            tbl_Caso_Emergencia = new HashSet<tbl_Caso_Emergencia>();
            tbl_Caso_Especialidad = new HashSet<tbl_Caso_Especialidad>();
            tbl_Caso_LaboratoriosImagenologia = new HashSet<tbl_Caso_LaboratoriosImagenologia>();
            tbl_Caso_Odontologia = new HashSet<tbl_Caso_Odontologia>();
        }

        [Key]
        public int CasoId { get; set; }

        [Required]
        [StringLength(10)]
        public string CodigoCaso { get; set; }

        public int Correlativo { get; set; }

        [Required]
        [StringLength(3)]
        public string CiudadId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int UserId { get; set; }

        public int PolizaId { get; set; }

        [Required]
        [StringLength(10)]
        public string MotivoConsultaId { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        public bool Dirty { get; set; }

        public DateTime? FechaEstadoReceta { get; set; }

        public DateTime? FechaEstadoExamenes { get; set; }

        public DateTime? FechaEstadoEspecialista { get; set; }

        public DateTime? FechaReconsulta { get; set; }

        public int? CostoConsulta { get; set; }

        public bool IsDeleted { get; set; }

        public int? UserDelete { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool? IsGastoBlocked { get; set; }

        public int? ReconsultaId { get; set; }

        public bool IsCasoDerivado { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_CLA_MotivoConsulta tbl_CLA_MotivoConsulta { get; set; }

        public virtual tbl_Poliza tbl_Poliza { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Derivacion> tbl_Derivacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Emergencia> tbl_Emergencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Estudio> tbl_Estudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Historia> tbl_Historia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion> tbl_Internacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medicamento> tbl_Medicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Odontologia> tbl_Odontologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Receta> tbl_Receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Emergencia> tbl_Caso_Emergencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Especialidad> tbl_Caso_Especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_LaboratoriosImagenologia> tbl_Caso_LaboratoriosImagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Odontologia> tbl_Caso_Odontologia { get; set; }
    }
}
