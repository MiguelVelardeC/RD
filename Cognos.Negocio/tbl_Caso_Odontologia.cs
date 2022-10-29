namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Caso_Odontologia
    {
        [Key]
        public int detId { get; set; }

        public int CasoId { get; set; }

        public int PrestacionOdontologicaId { get; set; }

        public int ProveedorId { get; set; }

        public decimal detPrecio { get; set; }

        public decimal detCoPagoMonto { get; set; }

        public decimal detCoPagoPorcentaje { get; set; }

        public DateTime? detFechaCoPagoPagado { get; set; }

        public DateTime detFecha { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_CLA_PrestacionesOdontologicas tbl_CLA_PrestacionesOdontologicas { get; set; }
    }
}
