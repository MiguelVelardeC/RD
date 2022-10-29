namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_TipoMedicamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_TipoMedicamento()
        {
            rstCitasDetalleTxn = new HashSet<rstCitasDetalleTxn>();
            tbl_CLA_PresentacionMedicamento = new HashSet<tbl_CLA_PresentacionMedicamento>();
            tbl_Medicamento = new HashSet<tbl_Medicamento>();
            tbl_Receta = new HashSet<tbl_Receta>();
        }

        [Key]
        [StringLength(10)]
        public string TipoMedicamentoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public bool? Visible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasDetalleTxn> rstCitasDetalleTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_PresentacionMedicamento> tbl_CLA_PresentacionMedicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medicamento> tbl_Medicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Receta> tbl_Receta { get; set; }
    }
}
