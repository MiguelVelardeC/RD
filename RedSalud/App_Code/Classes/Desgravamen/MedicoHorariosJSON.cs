using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for MedicoHorariosJSON
    /// </summary>
    public class MedicoHorariosJSON
    {

        public int MedicoHorariosId { get; set; }
        public int MedicoDesgravamenId { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

        public MedicoHorariosJSON()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}