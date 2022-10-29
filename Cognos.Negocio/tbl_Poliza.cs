namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Poliza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Poliza()
        {
            rstCitasTxn = new HashSet<rstCitasTxn>();
            tbl_Caso = new HashSet<tbl_Caso>();
            tbl_PolizaEstado = new HashSet<tbl_PolizaEstado>();
        }

        [Key]
        public int PolizaId { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroPoliza { get; set; }

        public int AseguradoId { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public decimal MontoTotal { get; set; }

        [StringLength(100)]
        public string NombrePlan { get; set; }

        [Required]
        [StringLength(10)]
        public string Lugar { get; set; }

        public decimal MontoFarmacia { get; set; }

        [Required]
        [StringLength(10)]
        public string Cobertura { get; set; }

        public int? PlanId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasTxn> rstCitasTxn { get; set; }

        public virtual tbl_Asegurado tbl_Asegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso> tbl_Caso { get; set; }

        public virtual tbl_Plan tbl_Plan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PolizaEstado> tbl_PolizaEstado { get; set; }
    }
}
