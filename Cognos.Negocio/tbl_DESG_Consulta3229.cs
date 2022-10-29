namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_Consulta3229
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string ocupacionPA { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string estadoCivilPA { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(400)]
        public string nombreDireccionMP { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(400)]
        public string fechaMotivoConsultaReciente { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(400)]
        public string tratamientoMedicacion { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int edadPadre { get; set; }

        public int? edadMuertePadre { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int edadMadre { get; set; }

        public int? edadMuerteMadre { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string edadGeneroHermanos { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int numeroVivos { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int numeroMuertos { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(250)]
        public string estadoSaludPadre { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(250)]
        public string estadoSaludMadre { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(250)]
        public string estadoSaludHermanos { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int estaturaCm { get; set; }

        [Key]
        [Column(Order = 15)]
        public decimal pesoKg { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(10)]
        public string tiempoConocePA { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(20)]
        public string relacionParentesco { get; set; }

        public int? pechoExpiracionCm { get; set; }

        public int? pechoInspiracionCm { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int abdomenCm { get; set; }

        [Key]
        [Column(Order = 19)]
        public bool aspectoEnfermizo { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(10)]
        public string presionArterial1 { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(10)]
        public string presionArterial2 { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(10)]
        public string presionArterial3 { get; set; }

        public int? pulsoFrecuenciaDescanso { get; set; }

        public int? pulsoFrecuenciaEsfuerzo { get; set; }

        public int? pulsoFrecuencia5Minutos { get; set; }

        public int? pulsoIrregularidadesDescanso { get; set; }

        public int? pulsoIrregularidadesEsfuerzo { get; set; }

        public int? pulsoIrregularidades5Minutos { get; set; }

        [Key]
        [Column(Order = 23)]
        public bool corazonHipertrofia { get; set; }

        [Key]
        [Column(Order = 24)]
        public bool corazonSoplo { get; set; }

        [Key]
        [Column(Order = 25)]
        public bool corazonDisnea { get; set; }

        [Key]
        [Column(Order = 26)]
        public bool corazonEdema { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(400)]
        public string situacionSoplo { get; set; }

        [Key]
        [Column(Order = 28)]
        public bool soploConstante { get; set; }

        [Key]
        [Column(Order = 29)]
        public bool soploInconstante { get; set; }

        [Key]
        [Column(Order = 30)]
        public bool soploIrradiado { get; set; }

        [Key]
        [Column(Order = 31)]
        public bool soploLocalizado { get; set; }

        [Key]
        [Column(Order = 32)]
        public bool soploSistolico { get; set; }

        [Key]
        [Column(Order = 33)]
        public bool soploDiastolico { get; set; }

        [Key]
        [Column(Order = 34)]
        public bool soploPresistolico { get; set; }

        [Key]
        [Column(Order = 35)]
        public bool soploSuave { get; set; }

        [Key]
        [Column(Order = 36)]
        public bool soploModerado { get; set; }

        [Key]
        [Column(Order = 37)]
        public bool soploFuerte { get; set; }

        [Key]
        [Column(Order = 38)]
        public bool soploAcentua { get; set; }

        [Key]
        [Column(Order = 39)]
        public bool soploDesaparece { get; set; }

        [Key]
        [Column(Order = 40)]
        public bool soploSinCambios { get; set; }

        [Key]
        [Column(Order = 41)]
        public bool soploSeAtenua { get; set; }

        [Key]
        [Column(Order = 42)]
        [StringLength(1000)]
        public string descripcionSoplo { get; set; }

        [Key]
        [Column(Order = 43)]
        [StringLength(1000)]
        public string comentarios { get; set; }

        [Key]
        [Column(Order = 44)]
        [StringLength(400)]
        public string analisisOrina { get; set; }

        [Key]
        [Column(Order = 45)]
        [StringLength(50)]
        public string densidad { get; set; }

        [Key]
        [Column(Order = 46)]
        [StringLength(50)]
        public string albumina { get; set; }

        [Key]
        [Column(Order = 47)]
        [StringLength(50)]
        public string glucosa { get; set; }
    }
}
