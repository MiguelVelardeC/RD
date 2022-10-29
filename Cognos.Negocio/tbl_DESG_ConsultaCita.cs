namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ConsultaCita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        [Required]
        [StringLength(250)]
        public string ocupacionPA { get; set; }

        [Required]
        [StringLength(20)]
        public string estadoCivilPA { get; set; }

        [Required]
        [StringLength(400)]
        public string nombreDireccionMP { get; set; }

        [Required]
        [StringLength(400)]
        public string fechaMotivoConsultaReciente { get; set; }

        [Required]
        [StringLength(400)]
        public string tratamientoMedicacion { get; set; }

        public int edadPadre { get; set; }

        public int? edadMuertePadre { get; set; }

        public int edadMadre { get; set; }

        public int? edadMuerteMadre { get; set; }

        [Required]
        [StringLength(50)]
        public string edadGeneroHermanos { get; set; }

        public int numeroVivos { get; set; }

        public int numeroMuertos { get; set; }

        [Required]
        [StringLength(250)]
        public string estadoSaludPadre { get; set; }

        [Required]
        [StringLength(250)]
        public string estadoSaludMadre { get; set; }

        [Required]
        [StringLength(250)]
        public string estadoSaludHermanos { get; set; }

        public int estaturaCm { get; set; }

        public decimal pesoKg { get; set; }

        [Required]
        [StringLength(10)]
        public string tiempoConocePA { get; set; }

        [Required]
        [StringLength(20)]
        public string relacionParentesco { get; set; }

        public int? pechoExpiracionCm { get; set; }

        public int? pechoInspiracionCm { get; set; }

        public int abdomenCm { get; set; }

        public bool aspectoEnfermizo { get; set; }

        [Required]
        [StringLength(10)]
        public string presionArterial1 { get; set; }

        [Required]
        [StringLength(10)]
        public string presionArterial2 { get; set; }

        [Required]
        [StringLength(10)]
        public string presionArterial3 { get; set; }

        public int? pulsoFrecuenciaDescanso { get; set; }

        public int? pulsoFrecuenciaEsfuerzo { get; set; }

        public int? pulsoFrecuencia5Minutos { get; set; }

        public int? pulsoIrregularidadesDescanso { get; set; }

        public int? pulsoIrregularidadesEsfuerzo { get; set; }

        public int? pulsoIrregularidades5Minutos { get; set; }

        public bool corazonHipertrofia { get; set; }

        public bool corazonSoplo { get; set; }

        public bool corazonDisnea { get; set; }

        public bool corazonEdema { get; set; }

        [Required]
        [StringLength(400)]
        public string situacionSoplo { get; set; }

        public bool soploConstante { get; set; }

        public bool soploInconstante { get; set; }

        public bool soploIrradiado { get; set; }

        public bool soploLocalizado { get; set; }

        public bool soploSistolico { get; set; }

        public bool soploDiastolico { get; set; }

        public bool soploPresistolico { get; set; }

        public bool soploSuave { get; set; }

        public bool soploModerado { get; set; }

        public bool soploFuerte { get; set; }

        public bool soploAcentua { get; set; }

        public bool soploDesaparece { get; set; }

        public bool soploSinCambios { get; set; }

        public bool soploSeAtenua { get; set; }

        [Required]
        [StringLength(1000)]
        public string descripcionSoplo { get; set; }

        [Required]
        [StringLength(1000)]
        public string comentarios { get; set; }

        [Required]
        [StringLength(400)]
        public string analisisOrina { get; set; }

        [Required]
        [StringLength(50)]
        public string densidad { get; set; }

        [Required]
        [StringLength(50)]
        public string albumina { get; set; }

        [Required]
        [StringLength(50)]
        public string glucosa { get; set; }

        public virtual tbl_DESG_CitaDesgravamen tbl_DESG_CitaDesgravamen { get; set; }
    }
}
