namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Internacion_Cirugia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InternacionId { get; set; }

        [Required]
        [StringLength(3)]
        public string CiudadId { get; set; }

        public int MedicoId { get; set; }

        public int EspecialidadId { get; set; }

        public decimal? detValorUma { get; set; }

        public int? detCantidadUma { get; set; }

        public decimal? detPorcentajeCirujano { get; set; }

        public decimal? detMontoCirujano { get; set; }

        public decimal? detPorcentajeAnestesiologo { get; set; }

        public decimal? detMontoAnestesiologo { get; set; }

        public decimal? detPorcentajeAyudante { get; set; }

        public decimal? detMontoAyudante { get; set; }

        public decimal? detPorcentajeInstrumentista { get; set; }

        public decimal? detMontoInstrumentista { get; set; }

        public decimal? detMontoTotal { get; set; }

        public decimal? detPorcentajeCoPago { get; set; }

        public decimal? detMontoCoPago { get; set; }

        public DateTime? detFecha { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_CLA_Especialidad tbl_CLA_Especialidad { get; set; }

        public virtual tbl_Internacion tbl_Internacion { get; set; }

        public virtual tbl_RED_Medico tbl_RED_Medico { get; set; }
    }
}
