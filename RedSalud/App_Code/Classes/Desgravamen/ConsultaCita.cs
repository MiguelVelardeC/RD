using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ConsultaCita
    /// </summary>
    public class ConsultaCita
    {
        public int CitaDesgravamenId { get; set; }
	    public string OcupacionPA { get; set; }
	    public string EstadoCivilPA { get; set; }
	    public string NombreDireccionMP { get; set; }
	    public string FechaMotivoConsultaReciente { get; set; }
	    public string TratamientoMedicacion { get; set; }
	    public int EdadPadre { get; set; }
	    public int EdadMuertePadre { get; set; }
	    public int EdadMadre { get; set; }
	    public int EdadMuerteMadre { get; set; }
	    public string EdadGeneroHermanos { get; set; }
	    public int NumeroVivos { get; set; }
	    public int NumeroMuertos { get; set; }
	    public string EstadoSaludPadre { get; set; }
	    public string EstadoSaludMadre { get; set; }
	    public string EstadoSaludHermanos { get; set; }
	    public int EstaturaCm { get; set; }
        public decimal PesoKg { get; set; }
	    public string TiempoConocePA { get; set; }
	    public string RelacionParentesco { get; set; }
	    public int PechoExpiracionCm { get; set; }
	    public int PechoInspiracionCm { get; set; }
	    public int AbdomenCm { get; set; }
	    public bool AspectoEnfermizo { get; set; }
	    public string PresionArterial1 { get; set; }
	    public string PresionArterial2 { get; set; }
	    public string PresionArterial3 { get; set; }
	    public int PulsoFrecuenciaDescanso { get; set; }
	    public int PulsoFrecuenciaEsfuerzo { get; set; }
	    public int PulsoFrecuencia5Minutos { get; set; }
	    public int PulsoIrregularidadesDescanso { get; set; }
	    public int PulsoIrregularidadesEsfuerzo { get; set; }
	    public int PulsoIrregularidades5Minutos { get; set; }
	    public bool CorazonHipertrofia { get; set; }
	    public bool CorazonSoplo { get; set; }
	    public bool CorazonDisnea { get; set; }
	    public bool CorazonEdema { get; set; }
	    public string SituacionSoplo { get; set; }
	    public bool SoploConstante { get; set; }
	    public bool SoploInconstante { get; set; }
	    public bool SoploIrradiado { get; set; }
	    public bool SoploLocalizado { get; set; }
	    public bool SoploSistolico { get; set; }
	    public bool SoploDiastolico { get; set; }
	    public bool SoploPresistolico { get; set; }
	    public bool SoploSuave { get; set; }
	    public bool SoploModerado { get; set; }
	    public bool SoploFuerte { get; set; }
	    public bool SoploAcentua { get; set; }
	    public bool SoploDesaparece { get; set; }
	    public bool SoploSinCambios { get; set; }
	    public bool SoploSeAtenua { get; set; }
	    public string DescripcionSoplo { get; set; }
	    public string Comentarios { get; set; }
	    public string AnalisisOrina { get; set; }
	    public string Densidad { get; set; }
	    public string Albumina { get; set; }
	    public string Glucosa { get; set; }

        public List<ConsultaPreguntaCita> Preguntas { get; set; }

        public ConsultaCita()
        {
            Preguntas = new List<ConsultaPreguntaCita>();
        }

        public void actualizarPregunta(ConsultaPreguntaCita objPreguntaCita)
        {
            IEnumerable<ConsultaPreguntaCita> preguntasFiltradas =
                Preguntas.Where<ConsultaPreguntaCita>(pregunta =>
                    pregunta.Seccion == objPreguntaCita.Seccion && pregunta.Inciso == objPreguntaCita.Inciso);

            if (preguntasFiltradas.Count() == 1)
            {
                preguntasFiltradas.ElementAt(0).CopyDataFrom(objPreguntaCita);
            }
            else
            {
                Preguntas.Add(objPreguntaCita);
            }
        }
    }
}