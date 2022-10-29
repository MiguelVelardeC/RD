using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for MedicoHorarios
    /// </summary>
    public class MedicoHorarios
    {
        public MedicoHorarios()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int MedicoHorariosId { get; set; }
        public int MedicoDesgravamenId { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}