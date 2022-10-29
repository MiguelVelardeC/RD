using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for Financiera
    /// </summary>
    public class Financiera
    {
        public int FinancieraId { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public string CentralCiudadId { get; set; }
        public int ClienteId { get; set; }
        public Financiera()
        {
            
        }
    }
}