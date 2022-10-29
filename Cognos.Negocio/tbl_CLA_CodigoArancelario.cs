namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_CodigoArancelario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_CodigoArancelario()
        {
            tbl_RED_Cliente_Cirugias = new HashSet<tbl_RED_Cliente_Cirugias>();
        }

        [Key]
        [StringLength(10)]
        public string CodigoArancelarioId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Nombre { get; set; }

        public decimal? UMA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Cirugias> tbl_RED_Cliente_Cirugias { get; set; }
    }
}
