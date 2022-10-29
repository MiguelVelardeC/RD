namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Odontologia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Odontologia()
        {
            tbl_OdontologiaFile = new HashSet<tbl_OdontologiaFile>();
        }

        [Key]
        public int OdontologiaId { get; set; }

        public int CasoId { get; set; }

        public int PrestacionOdontologicaId { get; set; }

        [StringLength(200)]
        public string Pieza { get; set; }

        [Required]
        [StringLength(2000)]
        public string Detalle { get; set; }

        [StringLength(2000)]
        public string Observaciones { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? GastoId { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_CLA_PrestacionesOdontologicas tbl_CLA_PrestacionesOdontologicas { get; set; }

        public virtual tbl_Gasto tbl_Gasto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OdontologiaFile> tbl_OdontologiaFile { get; set; }
    }
}
