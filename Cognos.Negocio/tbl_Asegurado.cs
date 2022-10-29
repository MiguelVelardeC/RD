namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Asegurado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Asegurado()
        {
            rstCitasTxn = new HashSet<rstCitasTxn>();
            rstPacienteMovilAsegurado = new HashSet<rstPacienteMovilAsegurado>();
            tbl_Poliza = new HashSet<tbl_Poliza>();
            tbl_CLA_EnfermedadCronica = new HashSet<tbl_CLA_EnfermedadCronica>();
        }

        [Key]
        public int AseguradoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        public int ClienteId { get; set; }

        public int PacienteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Relacion { get; set; }

        public bool? CasoCritico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasTxn> rstCitasTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstPacienteMovilAsegurado> rstPacienteMovilAsegurado { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }

        public virtual tbl_Paciente tbl_Paciente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Poliza> tbl_Poliza { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_EnfermedadCronica> tbl_CLA_EnfermedadCronica { get; set; }
    }
}
