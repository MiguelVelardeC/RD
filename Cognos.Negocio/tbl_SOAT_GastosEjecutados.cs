namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SOAT_GastosEjecutados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SOAT_GastosEjecutados()
        {
            tbl_SOAT_GastosEjecutadosDetalle = new HashSet<tbl_SOAT_GastosEjecutadosDetalle>();
        }

        [Key]
        public int GastosEjecutadosId { get; set; }

        public int SiniestroId { get; set; }

        public int AccidentadoId { get; set; }

        public decimal MontoGestion { get; set; }

        public virtual tbl_SOAT_Accidentado tbl_SOAT_Accidentado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_GastosEjecutadosDetalle> tbl_SOAT_GastosEjecutadosDetalle { get; set; }

        public virtual tbl_SOAT_Siniestro tbl_SOAT_Siniestro { get; set; }
    }
}
