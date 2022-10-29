namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_EstudioGrupo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DESG_EstudioGrupo()
        {
            tbl_DESG_Estudio = new HashSet<tbl_DESG_Estudio>();
        }

        [Key]
        public int estudioGrupoId { get; set; }

        [Required]
        [StringLength(250)]
        public string nombreGrupo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_Estudio> tbl_DESG_Estudio { get; set; }
    }
}
