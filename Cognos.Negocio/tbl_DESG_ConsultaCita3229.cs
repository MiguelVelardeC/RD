namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_ConsultaCita3229
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int citaDesgravamenId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int seccion { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string inciso { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clienteId { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool respuesta { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(4000)]
        public string observacion { get; set; }
    }
}
