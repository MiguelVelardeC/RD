using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ConsultaPreguntaBLL
    /// </summary>
    public class ConsultaPreguntaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public ConsultaPreguntaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static ConsultaPreguntaCita FillRecord(CitaMedicaDS.ConsultaPreguntaRow row)
        {
            ConsultaPreguntaCita objPregunta = new ConsultaPreguntaCita();
            objPregunta.CitaDesgravamenId = row.citaDesgravamenId;
            objPregunta.Inciso = row.inciso;
            objPregunta.Observacion = row.IsobservacionNull() ? "" : row.observacion;
            objPregunta.Pregunta = row.pregunta;
            if (row.IsrespuestaNull())
            {
                objPregunta.RespuestaNotSet = true;
                objPregunta.Respuesta = false;
            }
            else
            {
                objPregunta.RespuestaNotSet = false;
                objPregunta.Respuesta = row.respuesta;
            }
            objPregunta.Seccion = row.seccion;

            return objPregunta;
        }

        public static List<ConsultaPreguntaCita> GetConsultaPreguntaBySeccionCita(string secciones, int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
                throw new ArgumentException("El id de la cita no puede ser menor o igual que 0");
            if (string.IsNullOrWhiteSpace(secciones))
                throw new ArgumentException("La seccion de la cita es un string de numeros separados por coma");

            List<ConsultaPreguntaCita> results = new List<ConsultaPreguntaCita>();
            try
            {
                CitaMedicaDSTableAdapters.ConsultaPreguntaTableAdapter theAdapter =
                    new CitaMedicaDSTableAdapters.ConsultaPreguntaTableAdapter();
                CitaMedicaDS.ConsultaPreguntaDataTable theTable = theAdapter.GetConsultaPreguntaByCitaSeccion(secciones, citaDesgravamenId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CitaMedicaDS.ConsultaPreguntaRow row in theTable.Rows)
                    {
                        ConsultaPreguntaCita objPregunta = FillRecord(row);
                        results.Add(objPregunta);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ConsultaCita by id para la cita " + citaDesgravamenId + " y las secciones " + secciones, ex);
                throw;
            }
            return results;
        }
    }
}