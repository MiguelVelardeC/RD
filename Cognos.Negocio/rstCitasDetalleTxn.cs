namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstCitasDetalleTxn")]
    public partial class rstCitasDetalleTxn
    {
        [Key]
        public int detId { get; set; }

        public int citId { get; set; }

        public int? MedicamentoLINAMEId { get; set; }

        [StringLength(500)]
        public string Medicamento { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoMedicamentoId { get; set; }

        public int? TipoConcentracionId { get; set; }

        [Required]
        [StringLength(4000)]
        public string Indicaciones { get; set; }

        public int? ProveedorId { get; set; }

        public int? Cantidad { get; set; }

        public virtual rstCitasTxn rstCitasTxn { get; set; }

        public virtual tbl_CLA_MedicamentoLINAME tbl_CLA_MedicamentoLINAME { get; set; }

        public virtual tbl_CLA_TipoConcentracion tbl_CLA_TipoConcentracion { get; set; }

        public virtual tbl_CLA_TipoMedicamento tbl_CLA_TipoMedicamento { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }
    }
}
