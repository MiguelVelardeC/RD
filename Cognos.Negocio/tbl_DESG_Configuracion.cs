namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_Configuracion
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int duracionCitaME { get; set; }

        [Key]
        [Column(Order = 1)]
        public TimeSpan horaInicioCitas { get; set; }

        [Key]
        [Column(Order = 2)]
        public TimeSpan horaFinCitas { get; set; }
    }
}
