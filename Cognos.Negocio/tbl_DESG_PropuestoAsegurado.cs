namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_PropuestoAsegurado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DESG_PropuestoAsegurado()
        {
            tbl_DESG_CitaDesgravamen = new HashSet<tbl_DESG_CitaDesgravamen>();
        }

        [Key]
        public int propuestoAseguradoId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(20)]
        public string carnetIdentidad { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaNacimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string telefonoCelular { get; set; }

        public int? fotoId { get; set; }

        [StringLength(1)]
        public string genero { get; set; }

        public int clienteId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_CitaDesgravamen> tbl_DESG_CitaDesgravamen { get; set; }

        public virtual tbl_File tbl_File { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }
    }
}
