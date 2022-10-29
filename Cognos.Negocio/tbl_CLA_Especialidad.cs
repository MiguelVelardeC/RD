namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_Especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_Especialidad()
        {
            rstCitasTxn = new HashSet<rstCitasTxn>();
            tbl_Caso_Especialidad = new HashSet<tbl_Caso_Especialidad>();
            tbl_RED_Especialista = new HashSet<tbl_RED_Especialista>();
            tbl_RED_Medico = new HashSet<tbl_RED_Medico>();
            tbl_Internacion_Cirugia = new HashSet<tbl_Internacion_Cirugia>();
            tbl_RED_Cliente_Especialidad = new HashSet<tbl_RED_Cliente_Especialidad>();
        }

        [Key]
        public int EspecialidadId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public int espTiempoAtencionVD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasTxn> rstCitasTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Especialidad> tbl_Caso_Especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Especialista> tbl_RED_Especialista { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Medico> tbl_RED_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion_Cirugia> tbl_Internacion_Cirugia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Especialidad> tbl_RED_Cliente_Especialidad { get; set; }
    }
}
