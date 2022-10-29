namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_PolizaVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SiniestroId { get; set; }

        [Required]
        [StringLength(20)]
        public string OperacionId { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroRoseta { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroPoliza { get; set; }

        [Required]
        [StringLength(250)]
        public string NombreTitular { get; set; }

        [Required]
        [StringLength(20)]
        public string CITitular { get; set; }

        [Required]
        [StringLength(10)]
        public string Placa { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(50)]
        public string Sector { get; set; }

        [StringLength(250)]
        public string LugarVenta { get; set; }

        [StringLength(50)]
        public string Cilindrada { get; set; }

        [StringLength(50)]
        public string NroMotor { get; set; }

        [StringLength(50)]
        public string NroChasis { get; set; }

        public virtual tbl_SOAT_Siniestro tbl_SOAT_Siniestro { get; set; }
    }
}
