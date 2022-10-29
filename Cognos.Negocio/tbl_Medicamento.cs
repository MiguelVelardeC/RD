namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Medicamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Medicamento()
        {
            tbl_MedicamentoFile = new HashSet<tbl_MedicamentoFile>();
        }

        [Key]
        public int MedicamentoId { get; set; }

        public int CasoId { get; set; }

        public int MedicamentoClaId { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoMedicamentoId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Indicaciones { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? GastoId { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_CLA_MedicamentoEnfermeria tbl_CLA_MedicamentoEnfermeria { get; set; }

        public virtual tbl_CLA_TipoMedicamento tbl_CLA_TipoMedicamento { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_MedicamentoFile> tbl_MedicamentoFile { get; set; }
    }
}
