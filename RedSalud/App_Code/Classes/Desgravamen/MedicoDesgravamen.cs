using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for MedicoDesgravamen
    /// </summary>
    public class MedicoDesgravamen
    {
        public int MedicoDesgravamenId { get; set; }
        public int ProveedorMedicoId { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public int UserId { get; set; }

        public MedicoDesgravamen()
        {
            MedicoDesgravamenId = 0;
            ProveedorMedicoId = 0;
            Direccion = "";
            Nombre = "";
            UserId = 0;
        }
    }
}