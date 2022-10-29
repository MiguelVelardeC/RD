namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_PropuestoAseguradoBackup
    {
        [Key]
        [Column(Order = 0)]
        public int propuestoAseguradoId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string nombreCompleto { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string carnetIdentidad { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime fechaNacimiento { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string telefonoCelular { get; set; }

        public int? fotoId { get; set; }

        [StringLength(1)]
        public string genero { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clienteId { get; set; }
    }
}
