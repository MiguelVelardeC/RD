namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Configuration
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumeroDiasCasoAbierto { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal PorcentajeSiniestralidadAlerta { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal MontoMinimoEnPoliza { get; set; }
    }
}
