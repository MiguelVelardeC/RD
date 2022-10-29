namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ConsultaCitaCovid
    {
        [Key]
        public int ConsultaCitaCovidId { get; set; }

        public int CitaDesgravamenId { get; set; }

        public bool TieneVacuna { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreVacunas { get; set; }

        [Required]
        [StringLength(100)]
        public string PrimeraDosisFecha { get; set; }

        [Required]
        [StringLength(100)]
        public string SegundaDosisFecha { get; set; }

        [Required]
        [StringLength(100)]
        public string OtrasDosisFecha { get; set; }

        public bool TuvoCovid { get; set; }

        [Required]
        [StringLength(100)]
        public string FechaDiagnostico { get; set; }

        [Required]
        [StringLength(100)]
        public string FechaNegativo { get; set; }

        [Required]
        [StringLength(2000)]
        public string DetalleTratamiento { get; set; }

        [Required]
        [StringLength(100)]
        public string TiempoHospitalizacion { get; set; }

        [Required]
        [StringLength(2000)]
        public string SecuelasPostCovid { get; set; }

        public virtual tbl_DESG_CitaDesgravamen tbl_DESG_CitaDesgravamen { get; set; }
    }
}
