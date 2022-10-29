namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_EnfermedadCronica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_EnfermedadCronica()
        {
            tbl_Asegurado = new HashSet<tbl_Asegurado>();
        }

        [Key]
        public int EnfermedadCronicaId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Asegurado> tbl_Asegurado { get; set; }
    }
}
