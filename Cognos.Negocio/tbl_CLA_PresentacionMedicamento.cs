namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_PresentacionMedicamento
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedicamentoLINAMEId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string TipoMedicamentoId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TipoConcentracionId { get; set; }

        public virtual tbl_CLA_MedicamentoLINAME tbl_CLA_MedicamentoLINAME { get; set; }

        public virtual tbl_CLA_TipoConcentracion tbl_CLA_TipoConcentracion { get; set; }

        public virtual tbl_CLA_TipoMedicamento tbl_CLA_TipoMedicamento { get; set; }
    }
}
