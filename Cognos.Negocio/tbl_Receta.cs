namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Receta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Receta()
        {
            tbl_RecetaFile = new HashSet<tbl_RecetaFile>();
        }

        [Key]
        public int DetalleId { get; set; }

        public int CasoId { get; set; }

        public int? MedicamentoClaId { get; set; }

        [StringLength(250)]
        public string Medicamento { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoMedicamentoId { get; set; }

        public int? TipoConcentracionId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Indicaciones { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool UnPrincipioActivo { get; set; }

        public int? GastoId { get; set; }

        public int? proveedorId { get; set; }

        public int? cantidad { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_CLA_TipoMedicamento tbl_CLA_TipoMedicamento { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RecetaFile> tbl_RecetaFile { get; set; }
    }
}
