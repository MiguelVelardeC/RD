using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Derivacion
{
    /// <summary>
    /// Summary description for DerivacionEspecialista
    /// </summary>
    public class DerivacionEspecialista
    {
        public DerivacionEspecialista()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int DerivacionId { get; set; }
        public int CasoId { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public int FileCount { get; set; }
        public int UserId { get; set; }
        public int GastoId { get; set; }
        public int MedicoId { get; set; }
        public string MedicoNombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CasoIdCreado { get; set; }
        public string Observacion { get; set; }
        public string FileCountForDisplay
        {
            get { return FileCount.ToString("00"); }
        }
        public string FechaCreacionString
        {
            get { return FechaCreacion.ToString(); }
        }

        public int EspecialidadId { get; set; }
        public string EspecialidadNombre { get; set; }
    }
}