using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ReporteCantidadCitasPorFinanceraCiudad
    /// </summary>
    public class ReporteCantidadCitasPorFinanceraCiudad
    {
        public string Financiera { get; set; }
        public string Ciudad { get; set; }
        public int Cantidad { get; set; }

        public ReporteCantidadCitasPorFinanceraCiudad()
        {
            
        }
    }
}