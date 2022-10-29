using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Utilities.Bitacora
{
    /// <summary>
    /// Summary description for Bitacora
    /// </summary>
    public class Bitacora
    {
        private static readonly ILog log = LogManager.GetLogger("Bitacora_Operaciones");
        private static readonly ILog logStandard = LogManager.GetLogger("Standard");

        public enum TraceType
        {
            UserLogin,                // Un usuario hace un login en un sistema
            UserLogout,               // Un usuario hace un logout en el sistema
            InsertCasoMedicoSISA,     // Un usuario inserta un caso Medico en SISA
            UpdateCasoMedicoSISA,     // Un usuario actualiza un caso Medico en SISA
            InsertCasoMedicoSOAT,     // Un usuario inserta un caso Medico en SOAT
            UpdateCasoMedicoSOAT,     // Un usuario actualiza un caso Medico en SOAT
            InsertEmergenciaSISA,     // Un usuario inserta un caso de emergencia
            InsertEnfermeriaSISA,     // Un usuario inserta un caso de enfermeria
            DESGInsertarCita,         // DESG: Cuando se crea una cita de un PA ya existente
            DESGMedicoGuardaRevision, // DESG: El médico guarda una revisión médica
            DESGEliminarCita,         // Un usuario elimina una cita
            DESGRecuperarCita,        // El usuario recupera la cita
            ImportarPacientes,        // Un usuario importa pacientes
            ActualizarPaciente,       // Un usuario actualiza un paciente
            InsertarPaciente,         // Un usuario inserta un paciente
            EliminarPaciente,         // Un usuario inserta un paciente
            ActualizarPacienteFoto,   // Un usuario actualiza la foto de un paciente
            DESGEliminarProveedor,    // Un usuario elimina un Proveedor
            DESGInsertarProveedor,     // Un usuario inserta un Proveedor
            DESGActualizarProveedor     // Un usuario actualiza un Proveedor
        }

        public Bitacora()
        {
        }

        /// <summary>
        /// Registra una pista en el log de pistas
        /// </summary>
        /// <param name="traceType">El tipo de medida que estamos incluyendo</param>
        /// <param name="usuario">El login del empleado.  Puede ser vació o nulo</param>
        /// <param name="tipoObjeto">El tipo de objecto para el cual aplica la pista.  Puede ser vació o nulo</param>
        /// <param name="idObjeto">El ID del objecto para el cual aplica la pista.  Puede ser vació o nulo</param>
        /// <param name="mensaje">El mensaje detallado para la pista.  Puede ser vació o nulo</param>
        public void RecordTrace(TraceType tipoDeEvento, string usuario, string tipoObjeto,
            string idObjeto, string mensaje)
        {
            // Esto nunca de debería tirar una excepción

            try
            {
                string usuarioReal = string.IsNullOrEmpty(usuario) ? "[Sin Texto]" : usuario;
                string tipoObjetoReal = string.IsNullOrEmpty(tipoObjeto) ? "[Sin Texto]" : tipoObjeto;
                string idObjetoReal = string.IsNullOrEmpty(idObjeto) ? "[Sin Texto]" : idObjeto;
                string mensajeReal = string.IsNullOrEmpty(mensaje) ? "[Sin Texto]" : mensaje;

                log.Info(tipoDeEvento.ToString() + "|" +
                    usuarioReal + "|" +
                    tipoObjetoReal + "|" +
                    idObjetoReal + "|" +
                    mensajeReal);
            }
            catch (Exception exc)
            {
                logStandard.Error(exc.Message);
                // Si no podemos generar este mensaje, el sistema de Logs no está funcionando
                // y por lo tanto no hay nada que podamos hacer al respecto.  
            }
        }

        /// <summary>
        /// Convierte una cadena al tipo de dato correspondiente en la enumeración
        /// </summary>
        /// <param name="type">El valor de la enumeración como cadena</param>
        /// <returns>La enumeración correspondiente a la cadena provista</returns>
        public static TraceType GetTypeFromString(string type)
        {
            switch (type)
            {
                case "UserLogin": return TraceType.UserLogin;
                case "UserLogout": return TraceType.UserLogout;
                case "DESGInsertarCita": return TraceType.DESGInsertarCita;
                default: return TraceType.DESGEliminarCita;
            }
        }

    }
}