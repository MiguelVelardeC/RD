namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_Medico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DESG_Medico()
        {
            tbl_DESG_MedicoHorarios = new HashSet<tbl_DESG_MedicoHorarios>();
        }

        [Key]
        public int medicoDesgravamenId { get; set; }

        [Required]
        [StringLength(200)]
        public string nombre { get; set; }

        public int proveedorMedicoId { get; set; }

        [Required]
        [StringLength(200)]
        public string direccion { get; set; }

        public int userId { get; set; }

        public virtual tbl_DESG_ProveedorMedico tbl_DESG_ProveedorMedico { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_MedicoHorarios> tbl_DESG_MedicoHorarios { get; set; }
    }
}
