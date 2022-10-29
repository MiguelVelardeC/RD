using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConstantesWEBTELE
/// </summary>
/// 
namespace VideoLlamadaweb.Modelo.web
{
    public class ConstantesWEBTELE
    {
        public ConstantesWEBTELE()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public class Cita
        {
            public class HistorialTipo
            {
                public const string PacientePendientes = "PE";
                public const string PacienteRealizadas = "RE";
                public const string MedicoPendientes = "PE";
                public const string MedicoPorAprobar = "PA";
            }

            public class EstadoId
            {
                public const string Solicitada = "SO";
                public const string Aprobada = "AP";
                public const string Rechazada = "RE";
                public const string Terminada = "TE";
                public const string Anulada = "AN";
            }
        }

        public class TiposUsuarios
        {
            public const string Paciente = "P";
            public const string Medico = "M";
            public const string UsuarioPacientePorCI = "U";
            public const string Cliente = "C";
        }

        public class TiposHistorial
        {
            public const string VideoLlamada = "VL";
            public const string Clinico = "CL";
        }
    }
}