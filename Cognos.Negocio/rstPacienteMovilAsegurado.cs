namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstPacienteMovilAsegurado")]
    public partial class rstPacienteMovilAsegurado
    {
        [Key]
        public int detId { get; set; }

        public int pacId { get; set; }

        public int AseguradoId { get; set; }

        public int PacienteId { get; set; }

        public virtual rstPacienteMovil rstPacienteMovil { get; set; }

        public virtual tbl_Asegurado tbl_Asegurado { get; set; }

        public virtual tbl_Paciente tbl_Paciente { get; set; }
    }
}
