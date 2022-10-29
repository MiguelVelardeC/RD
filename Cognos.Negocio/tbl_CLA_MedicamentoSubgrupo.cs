namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_MedicamentoSubgrupo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_MedicamentoSubgrupo()
        {
            tbl_CLA_MedicamentoLINAME = new HashSet<tbl_CLA_MedicamentoLINAME>();
        }

        [Key]
        public int MedicamentoSubgrupoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(2)]
        public string Codigo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_MedicamentoLINAME> tbl_CLA_MedicamentoLINAME { get; set; }
    }
}
