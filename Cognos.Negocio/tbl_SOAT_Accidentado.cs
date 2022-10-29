namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_Accidentado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SOAT_Accidentado()
        {
            tbl_SOAT_GastosEjecutados = new HashSet<tbl_SOAT_GastosEjecutados>();
            tbl_SOAT_GestionMedica = new HashSet<tbl_SOAT_GestionMedica>();
            tbl_SOAT_Preliquidacion = new HashSet<tbl_SOAT_Preliquidacion>();
            tbl_File = new HashSet<tbl_File>();
            tbl_SOAT_Siniestro = new HashSet<tbl_SOAT_Siniestro>();
        }

        [Key]
        public int AccidentadoId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string CarnetIdentidad { get; set; }

        public bool Genero { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoCivil { get; set; }

        public bool? LicenciaConducir { get; set; }

        public bool Conductor { get; set; }

        [Required]
        [StringLength(1)]
        public string Tipo { get; set; }

        public bool Estado { get; set; }

        public bool IsIncapacidadTotal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_GastosEjecutados> tbl_SOAT_GastosEjecutados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_GestionMedica> tbl_SOAT_GestionMedica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Preliquidacion> tbl_SOAT_Preliquidacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_File> tbl_File { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Siniestro> tbl_SOAT_Siniestro { get; set; }
    }
}
