namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Internacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Internacion()
        {
            tbl_InternacionFile = new HashSet<tbl_InternacionFile>();
        }

        [Key]
        public int InternacionId { get; set; }

        public int CasoId { get; set; }

        public int ProveedorId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Observacion { get; set; }

        public int? GastoId { get; set; }

        public int? AprobacionUserId { get; set; }

        public DateTime? AprobacionFecha { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [StringLength(10)]
        public string CodigoArancelarioId { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_InternacionFile> tbl_InternacionFile { get; set; }

        public virtual tbl_Internacion_Cirugia tbl_Internacion_Cirugia { get; set; }

        public virtual tbl_Internacion_Internacion tbl_Internacion_Internacion { get; set; }
    }
}
