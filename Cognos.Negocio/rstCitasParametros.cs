namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rstCitasParametros
    {
        [Key]
        public int parId { get; set; }

        public int parCalificacionMinimaCita { get; set; }

        [Required]
        public string parCalificacionCorreosNotificacion { get; set; }

        public int parHistorialUltimasCitasCantidad { get; set; }

        public int parSolicitudCitaCantidadDiasPosteriores { get; set; }

        [Required]
        [StringLength(100)]
        public string parClienteIdCorreoPersonalizadoBancoFassil { get; set; }

        [Required]
        [StringLength(100)]
        public string parTelefonoCorreoPersonalizadoBancoFassil { get; set; }
    }
}
