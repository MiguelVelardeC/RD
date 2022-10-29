using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for TipoProductoDesgravamen
    /// </summary>
    public class TipoProductoDesgravamen
    {
        public string Codigo { get; set; }
        public int ClienteId { get; set; }
        public string Descripcion { get; set; }

        public TipoProductoDesgravamen()
        {

        }
    }
}