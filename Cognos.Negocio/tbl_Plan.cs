namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Plan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Plan()
        {
            tbl_Poliza = new HashSet<tbl_Poliza>();
        }

        [Key]
        public int PlanId { get; set; }

        [Required]
        [StringLength(500)]
        public string Nombre { get; set; }

        public int? CantidadConsultas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Poliza> tbl_Poliza { get; set; }
    }
}
