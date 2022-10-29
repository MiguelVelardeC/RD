namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Caso_LaboratoriosImagenologia
    {
        [Key]
        public int detId { get; set; }

        public int CasoId { get; set; }

        public int ProveedorId { get; set; }

        public int EstudioId { get; set; }

        public int OrdenDeServicioId { get; set; }

        public decimal detPrecio { get; set; }

        public decimal detCoPagoMonto { get; set; }

        public decimal detCoPagoPorcentaje { get; set; }

        public DateTime? detFechaCoPagoPagado { get; set; }

        public bool detEsImagenologia { get; set; }

        public DateTime detFecha { get; set; }

        public virtual tbl_Caso tbl_Caso { get; set; }

        public virtual tbl_CLA_TipoEstudio tbl_CLA_TipoEstudio { get; set; }

        public virtual tbl_Estudio tbl_Estudio { get; set; }

        public virtual tbl_RED_Proveedor tbl_RED_Proveedor { get; set; }
    }
}
