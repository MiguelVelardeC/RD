using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.PolizaAlianzaWS
{
    /// <summary>
    /// Summary description for InformacionPolizaAlianza
    /// </summary>
    public class InformacionPolizaAlianza
    {


        public InformacionPolizaAlianza()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string Placa { get; set; }
        public string NroRoseta { get; set; }
        public string NroPoliza { get; set; }
        public string NroMotor { get; set; }
        public int NroOficinaAlianza { get; set; }
        public string NombreOficinaAlianza { get; set; }
        public string NombreTitular { get; set; }
        public string CITitular { get; set; }
    }
}