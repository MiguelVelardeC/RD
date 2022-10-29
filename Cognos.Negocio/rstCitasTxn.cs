namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstCitasTxn")]
    public partial class rstCitasTxn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rstCitasTxn()
        {
            rstCitasDetalleTxn = new HashSet<rstCitasDetalleTxn>();
            rstCitasLogTxn = new HashSet<rstCitasLogTxn>();
        }

        [Key]
        public int citId { get; set; }

        public DateTime citFechaHoraCreacion { get; set; }

        public int ClienteId { get; set; }

        public int AseguradoId { get; set; }

        public int MedicoId { get; set; }

        public int EspecialidadId { get; set; }

        public int PolizaId { get; set; }

        [Required]
        [StringLength(150)]
        public string citMotivo { get; set; }

        public DateTime citFechaCita { get; set; }

        public DateTime citHoraInicio { get; set; }

        public DateTime citHoraFin { get; set; }

        public int citTiempoAtencion { get; set; }

        [Required]
        [StringLength(2)]
        public string citEstado { get; set; }

        [StringLength(200)]
        public string citRecomendaciones { get; set; }

        public int? citCalificacion { get; set; }

        [StringLength(250)]
        public string citCalififacionComentario { get; set; }

        [StringLength(10)]
        public string EnfermedadId { get; set; }

        [StringLength(10)]
        public string Enfermedad2Id { get; set; }

        [StringLength(10)]
        public string Enfermedad3Id { get; set; }

        public int? citCalificacionVideoLlamada { get; set; }

        public bool citRecordatorioEnviadoAlMovil { get; set; }

        [StringLength(500)]
        public string citObservaciones { get; set; }

        public bool citRecordatorioEnviadoAlAdministrador { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasDetalleTxn> rstCitasDetalleTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasLogTxn> rstCitasLogTxn { get; set; }

        public virtual tbl_Asegurado tbl_Asegurado { get; set; }

        public virtual tbl_CLA_Enfermedad tbl_CLA_Enfermedad { get; set; }

        public virtual tbl_CLA_Enfermedad tbl_CLA_Enfermedad1 { get; set; }

        public virtual tbl_CLA_Enfermedad tbl_CLA_Enfermedad2 { get; set; }

        public virtual tbl_CLA_Especialidad tbl_CLA_Especialidad { get; set; }

        public virtual tbl_Poliza tbl_Poliza { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }

        public virtual tbl_RED_Medico tbl_RED_Medico { get; set; }
    }
}
