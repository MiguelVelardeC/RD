namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Emergencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Emergencia()
        {
            tbl_EmergenciaFile = new HashSet<tbl_EmergenciaFile>();
        }

        [Key]
        public int EmergenciaId { get; set; }

        public int CasoId { get; set; }

        public int ProveedorId { get; set; }

        public int? GastoId { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_EmergenciaFile> tbl_EmergenciaFile { get; set; }
    }
}
