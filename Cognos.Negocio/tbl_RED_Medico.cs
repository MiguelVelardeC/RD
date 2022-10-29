namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Medico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RED_Medico()
        {
            rstCitasTxn = new HashSet<rstCitasTxn>();
            rstHorarioMedicoVideoLlamada = new HashSet<rstHorarioMedicoVideoLlamada>();
            tbl_Caso_Especialidad = new HashSet<tbl_Caso_Especialidad>();
            tbl_Cita = new HashSet<tbl_Cita>();
            tbl_Internacion_Cirugia = new HashSet<tbl_Internacion_Cirugia>();
            tbl_RED_Proveedor = new HashSet<tbl_RED_Proveedor>();
            tbl_RED_Cliente = new HashSet<tbl_RED_Cliente>();
        }

        [Key]
        public int MedicoId { get; set; }

        public int EspecialidadId { get; set; }

        [Required]
        [StringLength(50)]
        public string Sedes { get; set; }

        [Required]
        [StringLength(50)]
        public string ColegioMedico { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        [Required]
        [StringLength(1000)]
        public string Observacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public bool isExternal { get; set; }

        public bool isCallCenter { get; set; }

        public bool PermiteVideoLlamada { get; set; }

        public int? FotoId { get; set; }

        public string TokenNotificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasTxn> rstCitasTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstHorarioMedicoVideoLlamada> rstHorarioMedicoVideoLlamada { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Especialidad> tbl_Caso_Especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cita> tbl_Cita { get; set; }

        public virtual tbl_CLA_Especialidad tbl_CLA_Especialidad { get; set; }

        public virtual tbl_File tbl_File { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion_Cirugia> tbl_Internacion_Cirugia { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor> tbl_RED_Proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente> tbl_RED_Cliente { get; set; }
    }
}
