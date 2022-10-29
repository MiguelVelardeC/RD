namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_MedicoHorarios
    {
        [Key]
        public int MedicoHorariosId { get; set; }

        public int medicoDesgravamenId { get; set; }

        public int clienteId { get; set; }

        public TimeSpan horaInicio { get; set; }

        public TimeSpan horaFin { get; set; }

        public virtual tbl_DESG_Medico tbl_DESG_Medico { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
