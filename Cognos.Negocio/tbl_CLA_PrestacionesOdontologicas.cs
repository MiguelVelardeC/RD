namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CLA_PrestacionesOdontologicas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CLA_PrestacionesOdontologicas()
        {
            tbl_Caso_Odontologia = new HashSet<tbl_Caso_Odontologia>();
            tbl_Odontologia = new HashSet<tbl_Odontologia>();
            tbl_RED_Cliente_Odontologia = new HashSet<tbl_RED_Cliente_Odontologia>();
            tbl_RED_Proveedor_Odontologia = new HashSet<tbl_RED_Proveedor_Odontologia>();
        }

        [Key]
        public int PrestacionOdontologicaId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        public bool? Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Caso_Odontologia> tbl_Caso_Odontologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Odontologia> tbl_Odontologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Odontologia> tbl_RED_Cliente_Odontologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Proveedor_Odontologia> tbl_RED_Proveedor_Odontologia { get; set; }
    }
}
