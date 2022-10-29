namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Cliente_Odontologia
    {
        [Key]
        public int detId { get; set; }

        public int PrestacionOdontologicaId { get; set; }

        public int ClienteId { get; set; }

        public int detCantidadConsultasAno { get; set; }

        public virtual tbl_CLA_PrestacionesOdontologicas tbl_CLA_PrestacionesOdontologicas { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
