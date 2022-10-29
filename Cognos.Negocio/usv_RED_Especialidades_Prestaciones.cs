namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usv_RED_Especialidades_Prestaciones
    {
        public long? detId { get; set; }

        [Key]
        [Column(Order = 0)]
        public int EspecialidadId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string Especialidad { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CantidadVideoLLamadas { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string FrecuenciaVideoLLamadas { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClienteId { get; set; }
    }
}
