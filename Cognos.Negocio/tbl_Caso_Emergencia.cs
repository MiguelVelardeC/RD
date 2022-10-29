namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Caso_Emergencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Caso_Emergencia()
        {
            tbl_File = new HashSet<tbl_File>();
        }

        [Key]
        public int detId { get; set; }

        public int casoId { get; set; }

        public decimal detMontoEmergencia { get; set; }

        public decimal detMontoHonorariosMedicos { get; set; }

        public decimal detMontoFarmacia { get; set; }

        public decimal detMontoLaboratorios { get; set; }

        public decimal detMontoEstudios { get; set; }

        public decimal detMontoOtros { get; set; }

        public decimal detMontoTotal { get; set; }

        public decimal detPorcentajeCopago { get; set; }

        public decimal detMontoCoPago { get; set; }

        public DateTime detFecha { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_File> tbl_File { get; set; }
    }
}
