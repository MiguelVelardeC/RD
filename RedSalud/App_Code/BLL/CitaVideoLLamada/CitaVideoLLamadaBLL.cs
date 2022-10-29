using java.sql;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.CitasVideoLLamada.BLL
{
    /// <summary>
    /// Summary description for CitaBLL
    /// </summary>
    public class CitaVideoLLamadaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");


        public CitaVideoLLamadaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private static CitaVideoLLamada FillRecord(CitasVideoLLamadasDS.CitasVideoLLamadasRow row)
        {
            CitaVideoLLamada objCita = new CitaVideoLLamada();
            objCita.citId = row.citId;
            objCita.Asegurado = row.Asegurado;
            objCita.Ciudad = row.Ciudad;
            objCita.Cliente = row.Cliente;
            objCita.FechaRegistro = row.FechaRegistro;
            objCita.Medico = row.Medico;
            objCita.NroPoliza = row.NumeroPoliza;
            return objCita;
        }
        private static CitaDetalle FillRecord(CitasVideoLLamadasDS.CitaVideoLLamadaUnoRow row)
        {
            CitaDetalle objCita = new CitaDetalle();
            objCita.Calificacion = row.citCalificacion.ToString();
            objCita.citId = row.citId;
            objCita.Comentario = row.citCalififacionComentario;
            objCita.Especialidad = row.especialidad;
            objCita.Estado = row.EstadoNombre;
            objCita.FechaCita = row.citFechaCita;
            objCita.FechaCreacion = row.citFechaHoraCreacion;
            objCita.Medico = row.medico;
            objCita.Motivo = row.citMotivo;
            objCita.Recomendaciones = row.citRecomendaciones;
            objCita.citObservaciones = row.citObservaciones;
            objCita.Enfermedad1 = row.Enfermedad1;
            objCita.Enfermedad2 = row.Enfermedad2;
            objCita.Enfermedad3 = row.Enfermedad3;            
            objCita.PolizaId = row.PolizaId;
            objCita.AseguradoId = row.AseguradoId;

            return objCita;
        }
        private static CitaRecetaDetalle FillRecord(CitasVideoLLamadasDS.CitaVideoLLamada_ObtenerRecetaRow row)
        {
            CitaRecetaDetalle receta = new CitaRecetaDetalle();
            receta.Cantidad = row.Cantidad.ToString();
            receta.Concentracion = row.Concentracion;
            receta.Grupo = row.grupo;
            receta.InstruccionesUso = row.Indicaciones;
            receta.Medicamento = row.Medicamento;
            receta.Presentacion = row.Presentacion;
            receta.SubGrupo = row.subgrupo;
            receta.detId = row.detId;

            return receta;
        }
        public static CitaDetalle GetCitaByCitaId(int CitaId)
        {
            if (CitaId <= 0)
                throw new ArgumentException("CitaId no puede ser menor o igual a cero.");

            CitaDetalle TheCita = null;
            try
            {
                CitasVideoLLamadasDSTableAdapters.CitaVideoLLamada_ObtenerTableAdapter theAdapter = new CitasVideoLLamadasDSTableAdapters.CitaVideoLLamada_ObtenerTableAdapter();
                CitasVideoLLamadasDS.CitaVideoLLamadaUnoDataTable theTable = theAdapter.GetData(CitaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheCita = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting Cita data", ex);
                throw;
            }
            return TheCita;
        }
        public static Poliza.Poliza GetPolizaById(int PolizaId)
        {
            if (PolizaId <= 0)
                throw new ArgumentException("PolizaId no puede ser menor o igual a cero.");

            Poliza.Poliza ThePoliza = null;
            try
            {
                
                CitasVideoLLamadasDSTableAdapters.Poliza_GetPolizaByPolizaIdTableAdapter theAdapter 
                    = new CitasVideoLLamadasDSTableAdapters.Poliza_GetPolizaByPolizaIdTableAdapter();
                theAdapter.cmdTimeout = 120;
                CitasVideoLLamadasDS.Poliza_GetPolizaByPolizaIdDataTable theTable = theAdapter.GetData(PolizaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ThePoliza = new Poliza.Poliza {
                        AseguradoId = theTable[0].AseguradoId,
                        NumeroPoliza = theTable[0].NumeroPoliza,
                        FechaFin = theTable[0].FechaFin,
                        NombrePlan = theTable[0].NombrePlan
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting Cita data", ex);
                throw;
            }
            return ThePoliza;
        }
        public static CitaDatosPaciente GetDatosPacienteByAseguradoId(int AseguradoId)
        {
            if (AseguradoId <= 0)
                throw new ArgumentException("AseguradoId no puede ser menor o igual a cero.");

            CitaDatosPaciente ThePaciente = null;
            try
            {

                CitasVideoLLamadasDSTableAdapters.Paciente_ObtenerPorAseguradoIdTableAdapter theAdapter
                    = new CitasVideoLLamadasDSTableAdapters.Paciente_ObtenerPorAseguradoIdTableAdapter();
                CitasVideoLLamadasDS.Paciente_ObtenerPorAseguradoIdDataTable theTable = theAdapter.GetData(AseguradoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ThePaciente = new CitaDatosPaciente
                    {
                        CarnetIdentidad = theTable[0].CarnetIdentidad,
                        Direccion = theTable[0].Direccion,
                        Edad = theTable[0].Edad,
                        EstadoCivil = theTable[0].EstadoCivil,
                        FechaNacimiento = theTable[0].FechaNacimiento,
                        FotoId = theTable[0].FotoId,
                        Genero = theTable[0].Genero,
                        LugarTrabajo = theTable[0].LugarTrabajo,
                        Nombre = theTable[0].Nombre,
                        NroHijos = theTable[0].NroHijo,
                        Telefono = theTable[0].Telefono,
                        TelefonoTrabajo = theTable[0].TelefonoTrabajo,
                        PacienteId = theTable[0].PacienteId
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting Cita data", ex);
                throw;
            }
            return ThePaciente;
        }
        public static List<CitaRecetaDetalle> GetCitaRecetaByCitaId(int CitaId)
        {
            if (CitaId <= 0)
                throw new ArgumentException("CitaId no puede ser menor o igual a cero.");

            List<CitaRecetaDetalle> TheCita = new List<CitaRecetaDetalle>();
            try
            {
                CitasVideoLLamadasDSTableAdapters.CitaVideoLLamada_ObtenerRecetaTableAdapter theAdapter = new CitasVideoLLamadasDSTableAdapters.CitaVideoLLamada_ObtenerRecetaTableAdapter();
                CitasVideoLLamadasDS.CitaVideoLLamada_ObtenerRecetaDataTable theTable = theAdapter.GetData(CitaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CitasVideoLLamadasDS.CitaVideoLLamada_ObtenerRecetaRow item in theTable.Rows)
                    {
                        TheCita.Add(FillRecord(item));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting Cita data", ex);
                throw;
            }
            return TheCita;
        }

        public static List<CitaVideoLLamada> SearchCitasVideoLLamada(int clienteId, int medicoId, string nroPoliza, string paciente, string ciudad, DateTime fechaInicial, DateTime fechaFinal)
        {
            //if (medicoId != 0 && proveedorId != 0)
            //{
            //    throw new ArgumentException("El medicoId y el proveedor no pueden ser ambos diferentes de 0");
            //}
            
            List<CitaVideoLLamada> TheCita = new List<CitaVideoLLamada>();
            try
            {
                CitasVideoLLamadasDSTableAdapters.CitasVideoLLamadasTableAdapter theAdapter = new CitasVideoLLamadasDSTableAdapters.CitasVideoLLamadasTableAdapter();
                CitasVideoLLamadasDS.CitasVideoLLamadasDataTable theTable = theAdapter.SearchCitasVD(clienteId, medicoId, nroPoliza, paciente, ciudad, fechaInicial, fechaFinal);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CitasVideoLLamadasDS.CitasVideoLLamadasRow row in theTable.Rows)
                    {
                        TheCita.Add(FillRecord(row));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting Cita video llamada data", ex);
                throw;
            }
            return TheCita;
        }
        
    }
}