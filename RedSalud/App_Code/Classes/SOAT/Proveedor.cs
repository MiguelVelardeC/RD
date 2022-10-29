using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.SOAT.Proveedor
{
    /// <summary>
    /// Summary description for Proveedor
    /// </summary>
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string NIT { get; set; }
        public string CiudadId { get; set; }
        public int ProveedorAlianzaId { get; set; }

        public Proveedor()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}