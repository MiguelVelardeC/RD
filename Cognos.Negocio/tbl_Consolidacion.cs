namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Consolidacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Consolidacion()
        {
            tbl_GastoDetalle = new HashSet<tbl_GastoDetalle>();
        }

        [Key]
        public int ConsolidacionId { get; set; }

        public int ProveedorId { get; set; }

        public DateTime FechaHasta { get; set; }

        public decimal MontoTotal { get; set; }

        public int UserId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GastoDetalle> tbl_GastoDetalle { get; set; }
    }
}
