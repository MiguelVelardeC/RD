namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_CitaDesgravamenBackup
    {
        [Key]
        [Column(Order = 0)]
        public int citaDesgravamenId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int propuestoAseguradoId { get; set; }

        public int? financieraId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ciudadId { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool necesitaExamen { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool necesitaLaboratorio { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool cobroFinanciera { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1000)]
        public string referencia { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(20)]
        public string tipoProducto { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool deleted { get; set; }

        public int? ejecutivoId { get; set; }

        public DateTime? fechaRegistro { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clienteId { get; set; }
    }
}
