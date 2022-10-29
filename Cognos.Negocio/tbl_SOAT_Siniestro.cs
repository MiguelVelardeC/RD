namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_Siniestro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SOAT_Siniestro()
        {
            tbl_SOAT_GastosEjecutados = new HashSet<tbl_SOAT_GastosEjecutados>();
            tbl_SOAT_GestionMedica = new HashSet<tbl_SOAT_GestionMedica>();
            tbl_SOAT_Preliquidacion = new HashSet<tbl_SOAT_Preliquidacion>();
            tbl_SOAT_Accidentado = new HashSet<tbl_SOAT_Accidentado>();
            tbl_File = new HashSet<tbl_File>();
        }

        [Key]
        public int SiniestroId { get; set; }

        public DateTime FechaSiniestro { get; set; }

        public DateTime FechaDenuncia { get; set; }

        [Required]
        [StringLength(50)]
        public string LugarDpto { get; set; }

        [Required]
        [StringLength(50)]
        public string LugarProvincia { get; set; }

        [StringLength(500)]
        public string Zona { get; set; }

        [StringLength(250)]
        public string Sindicato { get; set; }

        public bool? IsDeleted { get; set; }

        public int? UserDelete { get; set; }

        public DateTime? DateDeleted { get; set; }

        [StringLength(250)]
        public string Causa { get; set; }

        public int CreatedBy { get; set; }

        public int ClienteId { get; set; }

        [StringLength(500)]
        public string NombreInspector { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_GastosEjecutados> tbl_SOAT_GastosEjecutados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_GestionMedica> tbl_SOAT_GestionMedica { get; set; }

        public virtual tbl_SOAT_PolizaVehiculo tbl_SOAT_PolizaVehiculo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Preliquidacion> tbl_SOAT_Preliquidacion { get; set; }

        public virtual tbl_SOAT_Seguimiento tbl_SOAT_Seguimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Accidentado> tbl_SOAT_Accidentado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_File> tbl_File { get; set; }
    }
}
