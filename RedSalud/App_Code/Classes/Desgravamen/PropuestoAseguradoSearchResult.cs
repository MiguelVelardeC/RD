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
    /// Summary description for PropuestoAseguradoSearchResult
    /// </summary>
    public class PropuestoAseguradoSearchResult
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public int PropuestoAseguradoId { get; set; }
        public int CitaDesgravamenId { get; set; }
        public string Nombre { get; set; }
        public string TelefonoCelular { get; set; }
        public string TipoProducto { get; set; }
        public int FinancieraId { get; set; }
        public string Financiera { get; set; }
        public DateTime FechaCreacionProgramacionCita { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public string CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public int TieneExamenMedico { get; set; }
        public bool NecesitaExamenMedico { get; set; }
        public bool CobroFinanciera { get; set; }
        public bool Aprobado { get; set; }
        public bool AprobadoLabo { get; set; }
        public string NombreMedico { get; set; }
        public string UsuarioRegistro { get; set; }
        public int ClienteId { get; set; }
        public string NombreJuridico { get; set; }
        public string PropuestoAseguradoNroDocumento { get; set; }

        public string AprobadoDisplay { 
            get {
                if (Aprobado)
                {
                    return "Aprobado";
                }
                return "No Aprobado";
            } 
        }



        public string NecesitaExamenMedicoForDisplay
        {
            get
            {
                return NecesitaExamenMedico ? "Si" : "No";
            }
        }
        public bool NecesitaLaboratorio { get; set; }
        public string NecesitaLaboratorioForDisplay
        {
            get
            {
                return NecesitaLaboratorio ? "Si" : "No";
            }
        }

        public string FechaCreacionCitaForDisplay
        {
            get { return FechaCreacionProgramacionCita == DateTime.MinValue ? "-" : String.Format("{0:dd/MMM/yyyy HH:mm:ss}", FechaCreacionProgramacionCita); }
        }

        public string FechaHoraCitaForDisplay
        {
            get
            {
                if (!NecesitaExamenMedico && FechaHoraCita != DateTime.MinValue)
                    return String.Format("{0:dd/MMM/yyyy}",FechaHoraCita);
                if (FechaHoraCita != DateTime.MinValue)
                    return String.Format("{0:dd/MMM/yyyy HH:mm:ss}", FechaHoraCita);
                return "-";
            }
        }

        public string TipoProductoForDisplay
        {
            get
            {
                try
                {
                    TipoProductoDesgravamen tp =  TipoProductoDesgravamenBLL.GetTipoProductoByCodigo(TipoProducto, ClienteId);
                    return (tp != null) ? tp.Descripcion : "-";

                }
                catch (Exception)
                {

                    return "-";
                }
                /*if (TipoProducto == "DESGRAVAMEN")
                    return "Desgravamen";
                if (TipoProducto == "INDIVIDUAL")
                    return "Vida Individual";*/
            }
        }

        public string FinancieraForDisplay
        {
            get { return string.IsNullOrEmpty(Financiera) ? "-" : Financiera; }
        }

        public string NombreJuridicoForDisplay
        {
            get { return string.IsNullOrEmpty(NombreJuridico) ? "-" : NombreJuridico; }
        }

        public string AprobadoForDisplay
        {
            get
            {
                return Aprobado ? "Si" : "No";
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
                    list = LaboratorioFileBLL.GetLaboratorioFiles(CitaDesgravamenId);
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get List of Files for CitaDesgravamen", ex);
                    list = new List<DocumentFile>();
                }
                return list;
            }
        }

        public PropuestoAseguradoSearchResult()
        {
            
        }
    }
}