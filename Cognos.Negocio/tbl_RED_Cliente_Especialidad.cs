namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Cliente_Especialidad
    {
        [Key]
        public int detId { get; set; }

        public int EspecialidadId { get; set; }

        public int ClienteId { get; set; }

        public int? detCantidadVideoLlamada { get; set; }

        [StringLength(1)]
        public string detFrecuenciaVideoLlamada { get; set; }

        public virtual tbl_CLA_Especialidad tbl_CLA_Especialidad { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
