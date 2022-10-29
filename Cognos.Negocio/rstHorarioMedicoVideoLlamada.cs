namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstHorarioMedicoVideoLlamada")]
    public partial class rstHorarioMedicoVideoLlamada
    {
        [Key]
        public int horId { get; set; }

        public DateTime horHoraInicio { get; set; }

        public DateTime horHoraFin { get; set; }

        public int horDia { get; set; }

        public int MedicoId { get; set; }

        public virtual tbl_RED_Medico tbl_RED_Medico { get; set; }
    }
}
