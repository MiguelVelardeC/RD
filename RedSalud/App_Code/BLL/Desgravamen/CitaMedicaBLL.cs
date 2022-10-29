using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for CitaMedicaBLL
    /// </summary>
    public class CitaMedicaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CitaMedicaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static CitaMedica FillRecord(CitaMedicaDS.CitaMedicaDesgravamenRow row)
        {
            CitaMedica objCita = new CitaMedica();

            objCita.Estado = row.IsestadoNull() ? CitaMedica.EstadoCita.ErrorEstado : CitaMedica.GetEstadoCita(row.estado);
            objCita.CitaDesgravamenId = row.citaDesgravamenId;
            objCita.CiudadId = row.ciudadId;
            objCita.CobroFinanciera = row.cobroFinanciera;
            objCita.FechaCreacion = row.IsfechaCreacionNull() ? DateTime.MinValue : row.fechaCreacion;
            objCita.FechaHoraCita = row.IsfechaHoraCitaNull() ? DateTime.MinValue : row.fechaHoraCita;
            objCita.FechaUltimaModificacion = row.IsfechaUltimaModificacionNull() ? DateTime.MinValue : row.fechaUltimaModificacion;
            objCita.FinancieraId = row.IsfinancieraIdNull() ? 0 : row.financieraId;
            objCita.MedicoDesgravamenId = row.IsmedicoDesgravamenIdNull() ? 0 : row.medicoDesgravamenId;
            objCita.NecesitaExamen = row.necesitaExamen;
            objCita.NecesitaLaboratorio = row.necesitaLaboratorio;
            objCita.NombreCiudad = row.nombreCiudad;
            objCita.PropuestoAseguradoId = row.propuestoAseguradoId;
            objCita.ProveedorMedicoId = row.IsproveedorMedicoIdNull() ? 0 : row.proveedorMedicoId;
            objCita.Referencia = row.referencia;

            objCita.Aprobado = row.IsaprobadoNull() ? false : row.aprobado;

            return objCita;
        }

        public static CitaMedica GetCitaMedicaById(int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
                throw new ArgumentException("El id de la cita no puede ser menor o igual que 0");
            CitaMedica objCita = null;
            try
            {
                CitaMedicaDSTableAdapters.CitaMedicaDesgravamenTableAdapter theAdapter =
                    new CitaMedicaDSTableAdapters.CitaMedicaDesgravamenTableAdapter();
                CitaMedicaDS.CitaMedicaDesgravamenDataTable theTable = theAdapter.GetCItaMedicaById(citaDesgravamenId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objCita = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting CitaMedica by id", ex);
                throw;
            }
            return objCita;
        }

        public static EnlaceDesgravamenSISA GetCasoIdByCitaDesgravamenId(int citaId, ref int returnCasoId)
        {
            EnlaceDesgravamenSISA result = null;
            CitaMedicaDSTableAdapters.CasoMedicoTableAdapter theAdapter = new CitaMedicaDSTableAdapters.CasoMedicoTableAdapter();

            try
            {
                int? casoId = 0;
                CitaMedicaDS.CasoMedicoDataTable tblCaso = theAdapter.GetCasoByCitaDesgravamenId(citaId, ref casoId);
                if (casoId == null)
                {
                    log.Warn("El SP devolvio nulo cuando se llamo al caso por cita con id " + citaId);
                    casoId = 0;
                }
                returnCasoId = (int)casoId;

                if (casoId > 0)
                {
                    CitaMedicaDS.CasoMedicoRow row = tblCaso[0];
                    result = new EnlaceDesgravamenSISA()
                    {
                        PacienteId = row.PacienteId,
                        CodigoAsegurado = row.CodigoAsegurado,
                        CodigoCaso = row.CodigoCaso,
                        CitaDesgravamenId = row.citaDesgravamenId,
                        CarnetIdentidad = row.carnetIdentidad,
                        CasoId = row.CasoId,
                        NumeroPoliza = row.NumeroPoliza,
                        PolizaId = row.PolizaId,
                        PropuestoAseguradoId = row.propuestoAseguradoId,
                        Dirty = row.Dirty
                    };

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception q)
            {
                log.Error("No se pudo obtener el id del caso", q);
                return null;
            }
        }
    }
}