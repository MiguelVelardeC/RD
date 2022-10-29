namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_TipoConcentracion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_TipoConcentracion()
        {
            rstCitasDetalleTxn = new HashSet<rstCitasDetalleTxn>();
            tbl_CLA_PresentacionMedicamento = new HashSet<tbl_CLA_PresentacionMedicamento>();
            tbl_CLA_MedicamentoLINAME = new HashSet<tbl_CLA_MedicamentoLINAME>();
        }

        [Key]
        public int TipoConcentracionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasDetalleTxn> rstCitasDetalleTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_PresentacionMedicamento> tbl_CLA_PresentacionMedicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_MedicamentoLINAME> tbl_CLA_MedicamentoLINAME { get; set; }
    }
}
