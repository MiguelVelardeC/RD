using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Documents;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ProgramacionCitaLabo
    /// </summary>
    public class ProgramacionCitaLabo
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public int CitaDesgravamenId { get; set; }
        public int EstudioId { get; set; }
        public string EstudioNombre { get; set; }
        public int ProveedorMedicoId { get; set; }
        public string ProveedorNombre { get; set; }
        public bool NecesitaCita { get; set; }
        public DateTime FechaCita { get; set; }
        public string EstudiosRealizados
        {
            get
            {
                return EstudioNombre + " - " + ProveedorNombre;
            }
        }
        public List<DocumentFile> LaboratorioFiles
        {
            get
            {
                if (CitaDesgravamenId <= 0)
                    return new List<DocumentFile>();
                List<DocumentFile> list = null;
                try
                {
                    list = LaboratorioFileBLL.GetLaboratorioFiles(CitaDesgravamenId, ProveedorMedicoId, EstudioId);
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get List of Files for CitaDesgravamen", ex);
                    list = new List<DocumentFile>();
                }
                return list;
            }
        }

        public string FechaCitaForDisplay
        {
            get
            {
                return FechaCita == DateTime.MinValue ? "-" : String.Format("{0:dd/MMM/yyyy HH:mm}", FechaCita);
            }
        }

        public ProgramacionCitaLabo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}