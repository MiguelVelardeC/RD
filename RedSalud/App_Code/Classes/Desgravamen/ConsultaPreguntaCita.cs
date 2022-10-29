using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ConsultaPreguntaCita
    /// </summary>
    public class ConsultaPreguntaCita
    {
        public int CitaDesgravamenId { get; set; }
        public int Seccion { get; set; }
        public string Inciso { get; set; }
        public string Pregunta { get; set; }
        public bool Respuesta { get; set; }
        public string Observacion { get; set; }
        public bool RespuestaNotSet { get; set; }

        public ConsultaPreguntaCita()
        {
            RespuestaNotSet = false;
        }
        public void CopyDataFrom(ConsultaPreguntaCita objPreguntaCita)
        {
            CitaDesgravamenId = objPreguntaCita.CitaDesgravamenId;
            Seccion = objPreguntaCita.Seccion;
            Inciso = objPreguntaCita.Inciso;
            Pregunta = objPreguntaCita.Pregunta;
            Respuesta = objPreguntaCita.Respuesta;
            Observacion = objPreguntaCita.Observacion;
            RespuestaNotSet = objPreguntaCita.RespuestaNotSet;
        }
    }
}