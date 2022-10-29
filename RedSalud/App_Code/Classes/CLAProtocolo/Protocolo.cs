using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Protocolo
{
    /// <summary>
    /// Summary description for Protocolo
    /// </summary>
    public class Protocolo
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public int ProtocoloId { get; set; }
        public string NombreEnfermedad { get; set; }
        public int TipoEnfermedadId { get; set; }
        public string TextoProtocolo { get; set; }

        public string TipoEnfermedadForDisplay
        {
            get
            {
                string tipoEnfermedad = "-";
                try
                {
                    Artexacta.App.TipoEnfermedad.TipoEnfermedad obj = 
                        Artexacta.App.TipoEnfermedad.BLL.TipoEnfermedadBLL.GetTipoEnfermedadByTipoEnfermedadId(this.TipoEnfermedadId);

                    tipoEnfermedad = obj.Nombre;
                }
                catch (Exception)
                {
                    log.Error("Ocurrio un error al obtener el nombre del tipo de enfermedad con ID: " + this.TipoEnfermedadId);
                }
                return tipoEnfermedad;
            }
        }

        public Protocolo(int protocoloId, string nombreEnfermedad, int tipoEnfermedad, string textoProtocolo)
        {
            this.ProtocoloId = protocoloId;
            this.NombreEnfermedad = nombreEnfermedad;
            this.TipoEnfermedadId = tipoEnfermedad;
            this.TextoProtocolo = textoProtocolo;
        }
    }
}