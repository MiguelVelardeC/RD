namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rstPacienteMovil")]
    public partial class rstPacienteMovil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rstPacienteMovil()
        {
            rstPacienteMovilAsegurado = new HashSet<rstPacienteMovilAsegurado>();
        }

        [Key]
        public int pacId { get; set; }

        [Required]
        [StringLength(100)]
        public string pacNombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        public string pacUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string pacContrasena { get; set; }

        [Required]
        [StringLength(50)]
        public string pacCodigoAsegurado { get; set; }

        public bool pacUsuarioVerificado { get; set; }

        public string pacTokenNotificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstPacienteMovilAsegurado> rstPacienteMovilAsegurado { get; set; }
    }
}
