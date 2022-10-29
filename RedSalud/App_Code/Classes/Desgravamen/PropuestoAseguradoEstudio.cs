using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for PropuestoAseguradoLaboratorio
    /// </summary>
    public class PropuestoAseguradoEstudio
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public int CitaDesgravamenId { get; set; }
        public int PropuestoAseguradoId { get; set; }
        public string NombreCompleto { get; set; }
        public string CarnetIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool CobroFinanciera { get; set; }
        public int ProveedorMedicoId { get; set; }
        public string NombreProveedor { get; set; }
        public DateTime FechaCita { get; set; }
        public DateTime FechaAtencion { get; set; }
        public bool Aprobado { get; set; }
        public string Observacion { get; set; }
        public int EstudioId { get; set; }
        public string EstudioNombre { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }

        public string FechaCitaLaboForDisplay
        {
            get { return FechaCita == DateTime.MinValue ? "-" : String.Format("{0:dd/MMM/yyyy HH:mm}",FechaCita); }
        }

        public string FechaAtencionLaboForDisplay
        {
            get {
                if (FechaAtencion.Year < 2000)
                    return "-";
                return String.Format("{0:dd/MMM/yyyy HH:mm}",FechaAtencion); 
            }
        }

        public string FechaNacimientoForDisplay
        {
            get { return FechaNacimiento == DateTime.MinValue ? "-" : String.Format("{0:dd/MMM/yyyy}",FechaNacimiento); }
        }

        public string CobroAseguradoForDisplay
        {
            get { return CobroFinanciera ? "No" : "Si"; }
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

        public PropuestoAseguradoEstudio()
        {

        }
    }
}