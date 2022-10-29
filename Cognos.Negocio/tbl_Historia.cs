namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Historia
    {
        [Key]
        public int HistoriaId { get; set; }

        public int PacienteId { get; set; }

        public int CasoId { get; set; }

        [Required]
        [StringLength(2000)]
        public string MotivoConsulta { get; set; }

        public int? ProtocoloId { get; set; }

        [Required]
        [StringLength(2000)]
        public string DiagnosticoPresuntivo { get; set; }

        [StringLength(20)]
        public string PresionArterial { get; set; }

        [StringLength(20)]
        public string Pulso { get; set; }

        [StringLength(20)]
        public string Temperatura { get; set; }

        [StringLength(20)]
        public string FrecuenciaCardiaca { get; set; }

        [StringLength(4000)]
        public string ExFisicoRegionalyDeSistema { get; set; }

        [Required]
        [StringLength(2000)]
        public string BiometriaHematica { get; set; }

        [StringLength(10)]
        public string EnfermedadId { get; set; }

        [StringLength(10)]
        public string Enfermedad2Id { get; set; }

        [StringLength(10)]
        public string Enfermedad3Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Talla { get; set; }

        public decimal Peso { get; set; }

        [Required]
        [StringLength(2000)]
        public string EnfermedadActual { get; set; }

        [Required]
        public string Observaciones { get; set; }

        public int EstaturaCm { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_CLA_Enfermedad tbl_CLA_Enfermedad { get; set; }

        public virtual tbl_CLA_Protocolo tbl_CLA_Protocolo { get; set; }

        public virtual tbl_Paciente tbl_Paciente { get; set; }
    }
}
