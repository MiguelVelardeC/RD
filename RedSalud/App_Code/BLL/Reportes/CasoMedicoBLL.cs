using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User.BLL;
using log4net;
using Artexacta.App.LoginSecurity;

namespace Artexacta.App.Reportes.BLL
{
    /// <summary>
    /// Summary description for CasoBLL
    /// </summary>
    public class CasoMedicoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CasoMedicoBLL () {}

        private static CasoMedico FillRecord ( CasoMedicoDS.CasoMedicoRow row )
        {
            CasoMedico objCaso = new CasoMedico(
                  row.NombreCliente
                , row.CodigoCaso
                , row.Correlativo
                , row.Ciudad
                , row.FechaCreacion
                , row.EnfermedadCronica
                , row.MotivoConsultaId
                , row.Estado
                , row.Medico
                , row.Codigo
                , row.Nombre
                , row.FechaNacimiento
                , row.IsGeneroNull() ? "" : row.Genero
                , row.IsCasoCriticoNull() ? "" : row.CasoCritico
                , row.IsAntecedentesNull() ? "" : row.Antecedentes
                , row.IsAntecedentesAlergicosNull() ? "" : row.AntecedentesAlergicos
                , row.IsAntecedentesGinecoobstetricosNull() ? "" : row.AntecedentesGinecoobstetricos
                , row.NumeroPoliza
                , row.IsNombrePlanNull() ? "" : row.NombrePlan
                , row.MotivoConsulta
                , row.EnfermedadActual
                , row.IsPresionArterialNull() ? "" : row.PresionArterial
                , row.IsPulsoNull() ? "" : row.Pulso
                , row.IsTemperaturaNull() ? "" : row.Temperatura
                , row.IsFrecuenciaCardiacaNull() ? "" : row.FrecuenciaCardiaca
                , row.Talla
                , row.Peso
                , row.IsExFisicoRegionalyDeSistemaNull() ? "" : row.ExFisicoRegionalyDeSistema
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.IsEnfermedad2Null() ? "" : row.Enfermedad2
                , row.IsEnfermedad3Null() ? "" : row.Enfermedad3
                , row.DiagnosticoPresuntivo
                , row.BiometriaHematica
                , row.Observaciones
                , row.CostoConsultaInternista
                , row.Medicamento
                , row.Presentacion
                , row.Concentracion
                , row.RecetaTotal
                , row.ProveedorEstudio
                , row.TipoEstudio
                , row.EstudioObservacion
                , row.EstudioTotal
                , row.ProveedorImagen
                , row.TipoImagen
                , row.ImagenObservacion
                , row.ImagenTotal
                , row.DerivacionMedico
                , row.DerivacionEspecialidad
                , row.DerivacionObservacion
                , row.DerivacionTotal
                , row.Clinica
                , row.Procedimiento
                , row.IsCirugia
                , row.InternacionObservacion
                , row.InternacionTotal
                , row.TotalEmergencia
                , row.TotalOdontologia
                );

            return objCaso;
        }

        public static List<CasoMedico> SearchCasoMedico ( bool casoMedico, bool reconsulta, bool enfermeria, bool emergencia,
            string where, int ClienteId, DateTime FechaIni, DateTime FechaFin )
        {
            List<CasoMedico> _cache = new List<CasoMedico>();
            try
            {
                CasoMedicoDSTableAdapters.CasoMedicoTableAdapter theAdapter = new CasoMedicoDSTableAdapters.CasoMedicoTableAdapter();
                theAdapter.cmdTimeout = 600;
                CasoMedicoDS.CasoMedicoDataTable theTable = theAdapter.GetReporteCasoMedico(ClienteId, casoMedico, reconsulta, enfermeria, emergencia, FechaIni, FechaFin, where);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoMedicoDS.CasoMedicoRow row in theTable.Rows)
                    {
                        CasoMedico theCaso = FillRecord(row);
                        theCaso.EstaturaCm = row.EstaturaCm;
                        _cache.Add(theCaso);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Caso", ex);
                throw;
            }
            return _cache;
        }

        public static List<CasoMedicoAtomicoSearch> SearchCasoMedicoCompleto(string where, int ClienteId, DateTime FechaIni, DateTime FechaFin)
        {
            List<CasoMedicoAtomicoSearch> _cache = new List<CasoMedicoAtomicoSearch>();
            try
            {

                CasoMedicoDSTableAdapters.CasoMedicoCompletoTableAdapter theAdapter = new CasoMedicoDSTableAdapters.CasoMedicoCompletoTableAdapter();                
                CasoMedicoDS.CasoMedicoCompletoDataTable theTable = theAdapter.GetCasoMedicoCompleto(ClienteId, FechaIni, FechaFin, where);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoMedicoDS.CasoMedicoCompletoRow row in theTable.Rows)
                    {
                        CasoMedicoAtomicoSearch theCaso = new CasoMedicoAtomicoSearch()
                        {
                            NombreCliente = (!row.IsNombreClienteNull())? row.NombreCliente: "",
                            CodigoCaso = (!row.IsCodigoCasoNull())? row.CodigoCaso: "",
                            Ciudad = (!row.IsCiudadNull())? row.Ciudad: "",
                            FechaCreacion = (!row.IsFechaCreacionNull())? row.FechaCreacion: DateTime.MinValue,
                            MotivoConsultaId = (!row.IsMotivoConsultaIdNull())? row.MotivoConsultaId: "",
                            Medico = (!row.IsMedicoNull())? row.Medico: "",
                            NombrePaciente = (!row.IsNombreNull())? row.Nombre: "",
                            CodigoAsegurado = (!row.IsCodigoNull())? row.Codigo: "",
                            FechaNacimiento = (!row.IsFechaNacimientoNull())? row.FechaNacimiento: DateTime.MinValue,
                            Edad = (!row.IsEdadNull())? row.Edad: 0,
                            Genero = (!row.IsGeneroNull())? row.Genero: "",
                            NumeroPoliza = (!row.IsNumeroPoliza1Null())? row.NumeroPoliza1: "",
                            NombrePlan = (!row.IsNombrePlanNull())? row.NombrePlan: "",
                            MotivoConsulta = (!row.IsMotivoConsultaNull())?row.MotivoConsulta: "",
                            EnfermedadActual = (!row.IsEnfermedadActualNull())?row.EnfermedadActual: "",
                            PresionArterial = (!row.IsPresionArterialNull())?row.PresionArterial: "",
                            Pulso = (!row.IsPulsoNull())? row.Pulso: "",
                            Temperatura = (!row.IsTemperatura1Null())? row.Temperatura1.ToString(): "",
                            FrecuenciaCardiaca = (!row.IsFrecuenciaCardiaca1Null())? row.FrecuenciaCardiaca1.ToString(): "",
                            Talla = (!row.IsTallaNull())? row.Talla: "",
                            EstaturaCm = (!row.IsEstaturaCmNull())? row.EstaturaCm: 0,
                            Peso = (!row.IsPesoNull())? double.Parse(row.Peso.ToString()) : 0,
                            Enfermedad = (!row.IsEnfermedadNull())? row.Enfermedad: "",
                            Enfermedad2 = (!row.IsEnfermedad2Null())? row.Enfermedad2: "",
                            Enfermedad3 = (!row.IsEnfermedad3Null())? row.Enfermedad3: "",
                            DiagnosticoPresuntivo = (!row.IsDiagnosticoPresuntivoNull())? row.DiagnosticoPresuntivo: "",
                            Observaciones = (!row.IsObservacionesNull())? row.Observaciones: "",
                            CasoIdDerivacion = (!row.IsCasoIdDerivacionNull())? row.CasoIdDerivacion: false
                        };
                        _cache.Add(theCaso);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Caso", ex);
                throw;
            }
            return _cache;
        }
    }
}