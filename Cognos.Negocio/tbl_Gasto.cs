namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Gasto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Gasto()
        {
            tbl_Derivacion = new HashSet<tbl_Derivacion>();
            tbl_Emergencia = new HashSet<tbl_Emergencia>();
            tbl_Estudio = new HashSet<tbl_Estudio>();
            tbl_GastoDetalle = new HashSet<tbl_GastoDetalle>();
            tbl_Internacion = new HashSet<tbl_Internacion>();
            tbl_Medicamento = new HashSet<tbl_Medicamento>();
            tbl_Odontologia = new HashSet<tbl_Odontologia>();
            tbl_Receta = new HashSet<tbl_Receta>();
        }

        [Key]
        public int GastoId { get; set; }

        public decimal MontoConFactura { get; set; }

        public decimal MontoSinFactura { get; set; }

        public decimal RetencionImpuestos { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? Total { get; set; }

        public int? TipoEstudioId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Derivacion> tbl_Derivacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Emergencia> tbl_Emergencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Estudio> tbl_Estudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GastoDetalle> tbl_GastoDetalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Internacion> tbl_Internacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medicamento> tbl_Medicamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Odontologia> tbl_Odontologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Receta> tbl_Receta { get; set; }
    }
}
