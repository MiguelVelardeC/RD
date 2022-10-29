namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ProgramacionCitaLaboTest
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proveedorMedicoId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int estudioId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime fechaCita { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime fechaAtencion { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1000)]
        public string observacion { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string estado { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool aprobado { get; set; }

        [Key]
        [Column(Order = 8)]
        public DateTime fechaCitaActual { get; set; }
    }
}
