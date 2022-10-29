using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Siniestro
{
    /// <summary>
    /// Summary description for SiniestroStat
    /// </summary>
    public class SiniestroStat
    {
        public SiniestroStat()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SiniestroId { get; set; }
        public int CantIlesos { get; set; }
        public int CantHeridos { get; set; }
        public int CantFallecidos { get; set; }
    }
}