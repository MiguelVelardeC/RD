namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Internacion_Internacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InternacionId { get; set; }

        [Required]
        [StringLength(3)]
        public string CiudadId { get; set; }

        [Required]
        [StringLength(10)]
        public string EnfermedadId { get; set; }

        public decimal? detMontoEmergencia { get; set; }

        public decimal? detMontoHonorariosMedicos { get; set; }

        public decimal? detMontoFarmacia { get; set; }

        public decimal? detMontoLaboratorios { get; set; }

        public decimal? detMontoEstudios { get; set; }

        public decimal? detMontoHospitalizacion { get; set; }

        public decimal? detMontoOtros { get; set; }

        public decimal? detMontoTotal { get; set; }

        public decimal? detPorcentajeCoPago { get; set; }

        public decimal? detMontoCoPago { get; set; }

        public DateTime? detFecha { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_CLA_Enfermedad tbl_CLA_Enfermedad { get; set; }

        public virtual tbl_Internacion tbl_Internacion { get; set; }
    }
}
