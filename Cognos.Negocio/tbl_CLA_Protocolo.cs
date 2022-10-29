namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_Protocolo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_Protocolo()
        {
            tbl_Historia = new HashSet<tbl_Historia>();
        }

        [Key]
        public int ProtocoloId { get; set; }

        [Required]
        [StringLength(250)]
        public string NombreEnfermedad { get; set; }

        public int TipoEnfermedadId { get; set; }

        [Required]
        public string TextoProtocolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Historia> tbl_Historia { get; set; }

        public virtual tbl_CLA_TipoEnfermedad tbl_CLA_TipoEnfermedad { get; set; }
    }
}
