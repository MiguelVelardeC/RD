using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ConsultaCita
    /// </summary>
    public class ConsultaCitaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public ConsultaCitaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static ConsultaCita FillRecord(CitaMedicaDS.ConsultaCitaRow row)
        {
            ConsultaCita objCita = new ConsultaCita();
            objCita.CitaDesgravamenId = row.citaDesgravamenId;
            objCita.OcupacionPA = row.ocupacionPA;
            objCita.EstadoCivilPA = row.estadoCivilPA;
            objCita.NombreDireccionMP = row.nombreDireccionMP;
            objCita.FechaMotivoConsultaReciente = row.fechaMotivoConsultaReciente;
            objCita.TratamientoMedicacion = row.tratamientoMedicacion;
            objCita.EdadPadre = row.edadPadre;
            objCita.EdadMuertePadre = row.IsedadMuerteMadreNull() ? 0 : row.edadMuertePadre;
            objCita.EdadMadre = row.edadMadre;
            objCita.EdadMuerteMadre = row.IsedadMuerteMadreNull() ? 0 : row.edadMuerteMadre;
            objCita.EdadGeneroHermanos = row.edadGeneroHermanos;
            objCita.NumeroVivos = row.numeroVivos;
            objCita.NumeroMuertos = row.numeroMuertos;
            objCita.EstadoSaludPadre = row.estadoSaludPadre;
            objCita.EstadoSaludMadre = row.estadoSaludMadre;
            objCita.EstadoSaludHermanos = row.estadoSaludHermanos;
            objCita.EstaturaCm = row.estaturaCm;
            objCita.PesoKg = row.pesoKg;
            objCita.TiempoConocePA = row.tiempoConocePA;
            objCita.RelacionParentesco = row.relacionParentesco;
            objCita.PechoExpiracionCm = row.IspechoExpiracionCmNull() ? 0 : row.pechoExpiracionCm;
            objCita.PechoInspiracionCm = row.IspechoInspiracionCmNull() ? 0 : row.pechoInspiracionCm;
            objCita.AbdomenCm = row.abdomenCm;
            objCita.AspectoEnfermizo = row.aspectoEnfermizo;
            objCita.PresionArterial1 = row.presionArterial1;
            objCita.PresionArterial2 = row.presionArterial2;
            objCita.PresionArterial3 = row.presionArterial3;
            objCita.PulsoFrecuenciaDescanso = row.IspulsoFrecuenciaDescansoNull() ? 0 : row.pulsoFrecuenciaDescanso;
            objCita.PulsoFrecuenciaEsfuerzo = row.IspulsoFrecuenciaEsfuerzoNull() ? 0 : row.pulsoFrecuenciaEsfuerzo;
            objCita.PulsoFrecuencia5Minutos = row.IspulsoIrregularidades5MinutosNull() ? 0 : row.pulsoFrecuencia5Minutos;
            objCita.PulsoIrregularidadesDescanso = row.IspulsoIrregularidadesDescansoNull() ? 0 : row.pulsoIrregularidadesDescanso;
            objCita.PulsoIrregularidadesEsfuerzo = row.IspulsoIrregularidadesEsfuerzoNull() ? 0 : row.pulsoIrregularidadesEsfuerzo;
            objCita.PulsoIrregularidades5Minutos = row.IspulsoIrregularidades5MinutosNull() ? 0 : row.pulsoIrregularidades5Minutos;
            objCita.CorazonHipertrofia = row.corazonHipertrofia;
            objCita.CorazonSoplo = row.corazonSoplo;
            objCita.CorazonDisnea = row.corazonDisnea;
            objCita.CorazonEdema = row.corazonEdema;
            objCita.SituacionSoplo = row.situacionSoplo;
            objCita.SoploConstante = row.soploConstante;
            objCita.SoploInconstante = row.soploInconstante;
            objCita.SoploIrradiado = row.soploIrradiado;
            objCita.SoploLocalizado = row.soploLocalizado;
            objCita.SoploSistolico = row.soploSistolico;
            objCita.SoploDiastolico = row.soploDiastolico;
            objCita.SoploPresistolico = row.soploPresistolico;
            objCita.SoploSuave = row.soploSuave;
            objCita.SoploModerado = row.soploModerado;
            objCita.SoploFuerte = row.soploFuerte;
            objCita.SoploAcentua = row.soploAcentua;
            objCita.SoploDesaparece = row.soploDesaparece;
            objCita.SoploSinCambios = row.soploSinCambios;
            objCita.SoploSeAtenua = row.soploSeAtenua;
            objCita.DescripcionSoplo = row.descripcionSoplo;
            objCita.Comentarios = row.comentarios;
            objCita.AnalisisOrina = row.analisisOrina;
            objCita.Densidad = row.densidad;
            objCita.Albumina = row.albumina;
            objCita.Glucosa = row.glucosa;

            return objCita;
        }

        public static ConsultaCita GetConsultaCitaById(int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
                throw new ArgumentException("El id de la cita no puede ser menor o igual que 0");
            ConsultaCita objCita = null;
            try
            {
                CitaMedicaDSTableAdapters.ConsultaCitaTableAdapter theAdapter =
                    new CitaMedicaDSTableAdapters.ConsultaCitaTableAdapter();
                CitaMedicaDS.ConsultaCitaDataTable theTable = theAdapter.GetConsultaCitaById(citaDesgravamenId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objCita = FillRecord(theTable[0]);
                }

                if (objCita == null)
                    return objCita;

                //CitaMedicaDSTableAdapters.ConsultaPreguntaTableAdapter adapterPReguntas =
                //    new CitaMedicaDSTableAdapters.ConsultaPreguntaTableAdapter();
                string secciones = "2,3,4,5,6,7,8,9,10,12,13,14,15,16,17,22,23,24,26";
                //CitaMedicaDS.ConsultaPreguntaDataTable tablePreguntas = 
                //    adapterPReguntas.GetConsultaPreguntaByCitaSeccion(secciones, citaDesgravamenId);
                //List<ConsultaPreguntaCita> preguntasCita = new List<ConsultaPreguntaCita>();
                //if (tablePreguntas != null && tablePreguntas.Rows.Count > 0)
                //{
                //    foreach (CitaMedicaDS.ConsultaPreguntaRow row in tablePreguntas.Rows)
                //    {
                //        ConsultaPreguntaCita objPreguntaCita = ConsultaPreguntaBLL.FillRecord(row);
                //        preguntasCita.Add(objPreguntaCita);
                //    }
                //}

                objCita.Preguntas = ConsultaPreguntaBLL.GetConsultaPreguntaBySeccionCita(secciones, citaDesgravamenId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ConsultaCita by id", ex);
                throw;
            }
            return objCita;
        }

        /// <summary>
        /// Este metodo update sirve tanto para insertar como para actualizar la información de la cita.
        /// </summary>
        /// <param name="objCita"></param>
        public static void UpdateConsultaCita(ConsultaCita objCita, int clienteId)
        {
            if (objCita == null ||
                objCita.CitaDesgravamenId <= 0)
            {
                throw new ArgumentException("El objeto cita no puede ser nul o con id menor o igual a 0");
            }

            try
            {
                CitaMedicaDSTableAdapters.ConsultaCitaTableAdapter theAdapter =
                    new CitaMedicaDSTableAdapters.ConsultaCitaTableAdapter();

                DataTable tblRespuestas = new DataTable();
                tblRespuestas.Columns.Add(new DataColumn("citaDesgravamenId", Type.GetType("System.Int32")));
                tblRespuestas.Columns.Add(new DataColumn("seccion", Type.GetType("System.Int32")));
                tblRespuestas.Columns.Add(new DataColumn("inciso", Type.GetType("System.String")));
                tblRespuestas.Columns.Add(new DataColumn("clienteId", Type.GetType("System.Int32")));
                tblRespuestas.Columns.Add(new DataColumn("pregunta", Type.GetType("System.String")));
                tblRespuestas.Columns.Add(new DataColumn("respuesta", Type.GetType("System.Int32")));
                tblRespuestas.Columns.Add(new DataColumn("observacion", Type.GetType("System.String")));

                if (objCita.Preguntas.Count > 0)
                {
                    foreach (ConsultaPreguntaCita objPregunta in objCita.Preguntas)
                    {
                        DataRow newRow = tblRespuestas.NewRow();
                        newRow["citaDesgravamenId"] = objPregunta.CitaDesgravamenId;
                        newRow["seccion"] = objPregunta.Seccion;
                        newRow["inciso"] = objPregunta.Inciso;
                        newRow["clienteId"] = clienteId;
                        newRow["pregunta"] = objPregunta.Pregunta;
                        newRow["respuesta"] = objPregunta.Respuesta;
                        newRow["observacion"] = objPregunta.Observacion;

                        tblRespuestas.Rows.Add(newRow);
                    }
                }

                theAdapter.Update(
                    objCita.CitaDesgravamenId, objCita.OcupacionPA, objCita.EstadoCivilPA,
                    objCita.NombreDireccionMP, objCita.FechaMotivoConsultaReciente, objCita.TratamientoMedicacion,
                    objCita.EdadPadre, objCita.EdadMuertePadre, objCita.EdadMadre, objCita.EdadMuerteMadre,
                    objCita.EdadGeneroHermanos, objCita.NumeroVivos, objCita.NumeroMuertos,
                    objCita.EstadoSaludPadre, objCita.EstadoSaludMadre, objCita.EstadoSaludHermanos,
                    objCita.EstaturaCm, objCita.PesoKg, objCita.TiempoConocePA,
                    objCita.RelacionParentesco, objCita.PechoExpiracionCm, objCita.PechoInspiracionCm,
                    objCita.AbdomenCm, objCita.AspectoEnfermizo, objCita.PresionArterial1,
                    objCita.PresionArterial2, objCita.PresionArterial3,
                    objCita.PulsoFrecuenciaDescanso, objCita.PulsoFrecuenciaEsfuerzo, objCita.PulsoFrecuencia5Minutos,
                    objCita.PulsoIrregularidadesDescanso, objCita.PulsoIrregularidadesEsfuerzo, objCita.PulsoIrregularidades5Minutos,
                    objCita.CorazonHipertrofia, objCita.CorazonSoplo, objCita.CorazonDisnea, objCita.CorazonEdema,
                    objCita.SituacionSoplo, objCita.SoploConstante, objCita.SoploInconstante, objCita.SoploIrradiado,
                    objCita.SoploLocalizado, objCita.SoploSistolico, objCita.SoploDiastolico, objCita.SoploPresistolico,
                    objCita.SoploSuave, objCita.SoploModerado, objCita.SoploFuerte, objCita.SoploAcentua,
                    objCita.SoploDesaparece, objCita.SoploSinCambios, objCita.SoploSeAtenua, objCita.DescripcionSoplo,
                    objCita.Comentarios, objCita.AnalisisOrina, objCita.Densidad, objCita.Albumina, objCita.Glucosa, 
                    tblRespuestas);
                
                log.Info("El usuario actualizo la consulta de la cita " + objCita.CitaDesgravamenId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ConsultaCita by id", ex);
                throw;
            }
        }
    }
}