namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ProveedorMedicoHorarios
    {
        [Key]
        public int proveedorMedicoHorarioId { get; set; }

        public int proveedorMedicoId { get; set; }

        public int estudioId { get; set; }

        public int clienteId { get; set; }

        public TimeSpan horaInicio { get; set; }

        public TimeSpan horaFin { get; set; }

        public virtual tbl_DESG_Estudio tbl_DESG_Estudio { get; set; }

        public virtual tbl_DESG_ProveedorMedico tbl_DESG_ProveedorMedico { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
