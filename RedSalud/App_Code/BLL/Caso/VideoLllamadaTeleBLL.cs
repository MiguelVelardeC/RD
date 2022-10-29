using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using VideoLlamadaweb.Modelo.web;
using agora;
using VideoLlamada.TokenGenerator.AgoraIO;

/// <summary>
/// Descripción breve de VideoLllamadaBLL
/// </summary>
namespace Artexacta.App.Caso.VideoLllamadaTele.BLL
{
    public class VideoLllamadaTeleBLL
    {
        public VideoLllamadaTeleBLL()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static VideoLlamadaDto GenerarTokenVideoLLamada(int casosid, string username, ref int val ,ref string mensaje)
        {
                string TipoUsuario = "M";
            
                if (SePuedeGenerarTokenWEB(casosid,username, TipoUsuario, ref mensaje))
                {
                    val = 1;
                    var token = GenerarToken(casosid, TipoUsuario);
                    return token;
                }
                return new VideoLlamadaDto();
        }
        public static DateTime TheDate { get; set; }
        private static bool SePuedeGenerarTokenWEB(int citId,string user,string TipoUsuario,
                                                    ref string mensaje)
        {
            var date = DateTime.Today;
            int? valnumcita = 0;
            string citEstado = "";
            DateTime? citHoraInicio = DateTime.Today;
            DateTime? citHoraFin = DateTime.Today;
            TeleconsultaonlineDSTableAdapters.ValCitaTeleTableAdapter theAdapter = new TeleconsultaonlineDSTableAdapters.ValCitaTeleTableAdapter();
            theAdapter.Val_Cita_Teleconsulta(citId,ref valnumcita, ref citEstado,ref citHoraInicio, ref citHoraFin);

            if (valnumcita ==0)
            {
                mensaje = "La cita ingresada no existe.";
                return false;
            }
            if (citEstado != "AP")
            {
                mensaje = "La cita no se encuentra en estado aprobado.";
                return true; ///----
            }
            string username = "";
            string fullname = "";
            TeleconsultaonlineDSTableAdapters.ValCitaTeleTableAdapter theAdapter2 = new TeleconsultaonlineDSTableAdapters.ValCitaTeleTableAdapter();
            theAdapter2.usp_validarcitaonline_doctor(citId, ref username, ref fullname);

            if (citId!=0)
            {
                if (TipoUsuario.ToUpper() == "M") 
                {
                    if (username != user)
                    {
                        mensaje = "No puede iniciar esta video llamada, no es el médico de la cita.";
                        return false;
                    }
                    int minutesDiff = ((TimeSpan)(citHoraInicio - DateTime.Now)).Minutes;
                    if (minutesDiff > 1)
                    {
                        mensaje = "Todavía no es posible unirse a la video llamada, por favor intente nuevamente a la fecha y hora de la cita.";
                        return false;
                    }
                    if (citHoraFin < DateTime.Now)
                    {
                        mensaje = "La hora de cita ya terminó.";
                        return false;
                    }
                }
                else
                {
                    mensaje = "No se ha definido el tipo de usuario. Vuelva a intentar iniciar sesión.";
                    return false;
                }
            }
            else
            {
                mensaje = "No se encuentra la cita. Vuelva a intentar iniciar sesión.";
                return false;
            }
            
            return true;
        }
        private static VideoLlamadaDto GenerarToken(int citId, string tipoUsuario)
        {
            var appId = ConfigurationManager.AppSettings.Get("AgoraIOAppId");
            var appCertificate = ConfigurationManager.AppSettings.Get("AgoraIOAppCertificate");
            
            var roomName = "cita" + citId.ToString();

            AccessToken at = new AccessToken(appId, appCertificate, roomName);
            var token = at.build();
            //string token = "";

            VideoLlamadaDto vdto = new VideoLlamadaDto();
            vdto.RoomName = roomName;
            vdto.Token = token;
            vdto.appid = appId;
            vdto.appCertificate=appCertificate;

            string mensaje = string.Empty;

            if (tipoUsuario.ToUpper() == ConstantesWEBTELE.TiposUsuarios.Medico)
            {
                //var paciente = (from cit in db.rstCitasTxn
                //                join ase in db.rstPacienteMovilAsegurado on cit.AseguradoId equals ase.AseguradoId
                //                join pac in db.rstPacienteMovil on ase.pacId equals pac.pacId
                //                select pac)
                //                .FirstOrDefault();

                //PushNotification.PushNotification.EnviarAPaciente("Video llamada en espera", "Su médico está esperando a que ingrese a la video llamada.", citId.ToString(),
                //    TipoTransaccionPaciente.CitaPorEmpezar, ref mensaje, paciente.pacTokenNotificacion);
            }

            return vdto;
        }
    }
}