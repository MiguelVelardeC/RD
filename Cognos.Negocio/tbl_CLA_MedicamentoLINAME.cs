namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_MedicamentoLINAME
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_MedicamentoLINAME()
        {
            rstCitasDetalleTxn = new HashSet<rstCitasDetalleTxn>();
            tbl_CLA_PresentacionMedicamento = new HashSet<tbl_CLA_PresentacionMedicamento>();
            tbl_CLA_TipoConcentracion = new HashSet<tbl_CLA_TipoConcentracion>();
        }

        [Key]
        public int MedicamentoLINAMEId { get; set; }

        public int MedicamentoGrupoId { get; set; }

        public int MedicamentoSubgrupoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(10)]
        public string ClasificacionATQ { get; set; }

        public bool UsoRestringido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasDetalleTxn> rstCitasDetalleTxn { get; set; }

        public virtual tbl_CLA_MedicamentoGrupo tbl_CLA_MedicamentoGrupo { get; set; }

        public virtual tbl_CLA_MedicamentoSubgrupo tbl_CLA_MedicamentoSubgrupo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_PresentacionMedicamento> tbl_CLA_PresentacionMedicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CLA_TipoConcentracion> tbl_CLA_TipoConcentracion { get; set; }
    }
}
