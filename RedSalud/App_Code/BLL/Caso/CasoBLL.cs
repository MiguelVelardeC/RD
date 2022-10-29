using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User.BLL;
using log4net;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Security.BLL;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.RedClientePrestaciones;
using Artexacta.App.RedClientePrestaciones.BLL;
using Artexacta.App.Validacion.BLL;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.ProveedorPrestaciones.BLL;
using Artexacta.App.ProveedorPrestaciones;


namespace Artexacta.App.Caso.BLL
{
    /// <summary>
    /// Summary description for CasoBLL
    /// </summary>
    public class CasoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public static Artexacta.App.Utilities.Bitacora.Bitacora theBitacora = new Artexacta.App.Utilities.Bitacora.Bitacora();

        public CasoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //Caso Medico
        private static Caso FillRecord(CasoDS.CasoRow row)
        {
            Caso objCaso = new Caso(
                row.CasoId
                , row.CodigoCaso
                , row.Correlativo
                , row.CiudadId
                , Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.IsFechaReconsultaNull() ? DateTime.MinValue : Configuration.Configuration.ConvertToClientTimeZone(row.FechaReconsulta)
                , row.IsFechaEstadoRecetaNull() ? DateTime.MinValue : row.FechaEstadoReceta
                , row.IsFechaEstadoEspecialistaNull() ? DateTime.MinValue : row.FechaEstadoEspecialista
                , row.IsFechaEstadoExamenesNull() ? DateTime.MinValue : row.FechaEstadoExamenes
                , row.UserId
                , row.PolizaId
                , row.MotivoConsultaId
                , row.Estado
                , row.Dirty
                , row.PacienteId
                , row.IsAntecedentesNull() ? "" : row.Antecedentes
                , row.IsAntecedentesAlergicosNull() ? "" : row.AntecedentesAlergicos
                , row.IsAntecedentesGinecoobstetricosNull() ? "" : row.AntecedentesGinecoobstetricos
                , row.HistoriaId
                , row.IsPresionArterialNull() ? "" : row.PresionArterial
                , row.IsPulsoNull() ? "" : row.Pulso
                , row.IsTemperaturaNull() ? "" : row.Temperatura
                , row.IsFrecuenciaCardiacaNull() ? "" : row.FrecuenciaCardiaca
                , row.Talla
                , row.Peso
                , row.IsExFisicoRegionalyDeSistemaNull() ? "" : row.ExFisicoRegionalyDeSistema
                , row.IsEnfermedadIdNull() ? "" : row.EnfermedadId
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.IsEnfermedad2IdNull() ? "" : row.Enfermedad2Id
                , row.IsEnfermedad2Null() ? "" : row.Enfermedad2
                , row.IsEnfermedad3IdNull() ? "" : row.Enfermedad3Id
                , row.IsEnfermedad3Null() ? "" : row.Enfermedad3
                , row.DiagnosticoPresuntivo
                , row.BiometriaHematica
                , row.MotivoConsulta
                , row.EnfermedadActual
                , row.Observaciones
                , row.CasoCritico
                , row.ProveedorId
                , row.Proveedor
                );
            objCaso.EstaturaCm = row.EstaturaCm;

            if (!row.IsReconsultaIdNull())
            {
                objCaso.ReconsultaId = row.ReconsultaId;
            }

            return objCaso;
        }
        //Caso Medico Basic (Dirty) AND Search
        private static Caso FillRecord(CasoDS.SearchCasoRow row)
        {
            Caso objCaso = new Caso(
                row.CasoId
                , row.CodigoCaso
                , row.Correlativo
                , row.CiudadId
                , Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.UserId
                , row.PolizaId
                , row.MotivoConsultaId
                , row.Estado
                , row.Dirty
                , row.IsIsGastoBlockedNull() ? false : row.IsGastoBlocked
                , row.PacienteId
                , row.NombrePaciente
                , row.HistoriaId
                , row.IsEnfermedadIdNull() ? "" : row.EnfermedadId
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.IsEnfermedad2IdNull() ? "" : row.Enfermedad2Id
                , row.IsEnfermedad2Null() ? "" : row.Enfermedad2
                , row.IsEnfermedad3IdNull() ? "" : row.Enfermedad3Id
                , row.IsEnfermedad3Null() ? "" : row.Enfermedad3
                , row.DiagnosticoPresuntivo
                , row.MotivoConsulta
                , row.ClienteId
                , row.NombreJuridico
                , row.CodigoAsegurado
                , row.CasoCritico
                , row.IsNumeroPolizaNull() ? null : row.NumeroPoliza
                , row.IsMedicoNameNull() ? null : row.MedicoName
                , row.CantGastos);

            if (!row.IsReconsultaIdNull())
            {
                objCaso.ReconsultaId = row.ReconsultaId;
            }
            if (!row.IsHistoriaCountNull())
            {
                objCaso.HistoriaCount = row.HistoriaCount;
            }

            return objCaso;
        }

        //Search Caso For Aprobation 
        private static Caso FillRecord(CasoDS.SearchCasoForAprobationRow row, int clienteId)
        {
            Caso objCaso = new Caso(
                row.CasoId
                , row.CodigoCaso
                , row.Correlativo
                , row.CiudadId
                , Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.UserId
                , row.PolizaId
                , row.MotivoConsultaId
                , row.Estado
                , row.Dirty
                , row.PacienteId
                , row.NombrePaciente
                , row.HistoriaId
                , row.IsEnfermedadIdNull() ? "" : row.EnfermedadId
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.IsEnfermedad2IdNull() ? "" : row.Enfermedad2Id
                , row.IsEnfermedad2Null() ? "" : row.Enfermedad2
                , row.IsEnfermedad3IdNull() ? "" : row.Enfermedad3Id
                , row.IsEnfermedad3Null() ? "" : row.Enfermedad3
                , row.DiagnosticoPresuntivo
                , row.MotivoConsulta
                , clienteId
                , row.NombreJuridico
                , row.CodigoAsegurado

                , row.NumeroPoliza
                , row.FechaInicio
                , row.FechaFin
                , row.MontoTotal
                , row.NombrePlan
                , row.EstadoPoliza
                , row.GastoTotal
                );

            return objCaso;
        }

        //Historial
        private static Caso FillRecord(CasoDS.HistorialRow row)
        {
            Caso objCaso = new Caso(
                row.CasoId
                , row.CodigoCaso
                , row.Correlativo
                , Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.UserName
                , row.NumeroPoliza
                , row.MotivoConsultaTipo
                , row.Estado
                , row.HistoriaId
                , row.MotivoConsulta
                , row.EnfermedadActual
                , row.IsPresionArterialNull() ? "" : row.PresionArterial
                , row.IsPulsoNull() ? "" : row.Pulso
                , row.IsTemperaturaNull() ? "" : row.Temperatura
                , row.IsFrecuenciaCardiacaNull() ? "" : row.FrecuenciaCardiaca
                , row.Talla
                , row.Peso
                , row.IsExFisicoRegionalyDeSistemaNull() ? "" : row.ExFisicoRegionalyDeSistema
                , row.IsEnfermedadIdNull() ? "" : row.EnfermedadId
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.IsEnfermedad2IdNull() ? "" : row.Enfermedad2Id
                , row.IsEnfermedad2Null() ? "" : row.Enfermedad2
                , row.IsEnfermedad3IdNull() ? "" : row.Enfermedad3Id
                , row.IsEnfermedad3Null() ? "" : row.Enfermedad3
                , row.DiagnosticoPresuntivo
                , row.BiometriaHematica
                , row.Observaciones
                , row.ProveedorId
                , row.Proveedor);

            objCaso.EstaturaCm = row.EstaturaCm;

            return objCaso;
        }

        public static Caso GetCasoByCasoId(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId no puede ser menor o igual a cero.");

            Caso TheCaso = null;
            try
            {
                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                CasoDS.CasoDataTable theTable = theAdapter.GetCasoByCasoId(CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheCaso = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso data", ex);
                throw;
            }
            return TheCaso;
        }

        public static Caso GetCasoBasicByCasoId(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId no puede ser menor o igual a cero.");

            Caso TheCaso = null;
            try
            {
                CasoDSTableAdapters.SearchCasoTableAdapter theAdapter = new CasoDSTableAdapters.SearchCasoTableAdapter();
                CasoDS.SearchCasoDataTable theTable = theAdapter.GetCasoBasicByCasoId(CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    CasoDS.SearchCasoRow row = theTable[0];
                    TheCaso = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso data", ex);
                throw;
            }
            return TheCaso;
        }

        //sin uso
        //public static List<Caso> getCasoListByUserId(int UserId)
        //{
        //    List<Caso> theList = new List<Caso>();
        //    Caso theCaso = null;
        //    try
        //    {
        //        CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
        //        CasoDS.CasoDataTable theTable = theAdapter.GetCasoByUserId(UserId);
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        {
        //            foreach (CasoDS.CasoRow row in theTable.Rows)
        //            {
        //                theCaso = FillRecord(row);
        //                theList.Add(theCaso);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting list Caso", ex);
        //        throw;
        //    }
        //    return theList;
        //}

        public static int SearchCaso(List<Caso> _cache, int pageSize, int firstRow, string where, int ClienteId, int ProveedorId, string orderBy)
        {
            if (ClienteId < 0)////// changed to ClienteId == 0 ////////
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            int? intTotalRows = 0;
            try
            {
                List<string> userPermissions = SecurityBLL.GetUserPermissions();
                bool casos = userPermissions.Contains("MANAGE_CASOS") || userPermissions.Contains("SOLO_VISTA");
                bool emergencia = userPermissions.Contains("CASO_EMERGENCIA");
                bool enfermeria = userPermissions.Contains("MANAGE_ENFERMERIA");
                bool admin = userPermissions.Contains("CASOS_LISTAR_TODOS");

                int userId = 0;

                try
                {
                    userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Error getting the user Id.", e);
                }


                if (casos || emergencia || enfermeria || admin)
                {
                    CasoDSTableAdapters.SearchCasoTableAdapter theAdapter = new CasoDSTableAdapters.SearchCasoTableAdapter();
                    theAdapter.cmdTimeout = 600;
                    CasoDS.SearchCasoDataTable theTable = theAdapter.SearchCaso(ClienteId, ProveedorId, pageSize, firstRow,
                        ref intTotalRows, where, orderBy, casos, enfermeria, emergencia, admin, userId);
                    if (theTable != null && theTable.Rows.Count > 0)
                    {
                        foreach (CasoDS.SearchCasoRow row in theTable.Rows)
                        {
                            Caso theCaso = FillRecord(row);
                            theCaso.RowNumber = row.RowNumber;
                            _cache.Add(theCaso);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Caso", ex);
                throw;
            }
            return (int)intTotalRows;
        }
        public static int SearchCasoNew(List<Caso> _cache, int pageSize, int firstRow, string where, int ClienteId, int MedicoId, string orderBy)
        {
            if (ClienteId < 0)////// changed to ClienteId == 0 ////////
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            int? intTotalRows = 0;
            try
            {
                List<string> userPermissions = SecurityBLL.GetUserPermissions();
                bool casos = userPermissions.Contains("MANAGE_CASOS") || userPermissions.Contains("SOLO_VISTA");
                bool emergencia = userPermissions.Contains("CASO_EMERGENCIA");
                bool enfermeria = userPermissions.Contains("MANAGE_ENFERMERIA");
                bool admin = userPermissions.Contains("CASOS_LISTAR_TODOS");

                int userId = 0;

                try
                {
                    if (MedicoId == 0)
                    {
                        userId = 0;
                    }
                    else
                    {
                        userId = Medico.BLL.MedicoBLL.getMedicoByMedicoId(MedicoId).UserId;
                        MedicoId = 0;
                    }

                }
                catch (Exception e)
                {
                    throw new ArgumentException("Error getting the user Id.", e);
                }


                if (casos || emergencia || enfermeria || admin)
                {
                    CasoDSTableAdapters.SearchCasoTableAdapter theAdapter = new CasoDSTableAdapters.SearchCasoTableAdapter();
                    theAdapter.cmdTimeout = 600;
                    CasoDS.SearchCasoDataTable theTable = theAdapter.SearchCasoNew(ClienteId, MedicoId, pageSize, firstRow,
                        ref intTotalRows, where, orderBy, casos, enfermeria, emergencia, admin, userId);
                    if (theTable != null && theTable.Rows.Count > 0)
                    {
                        foreach (CasoDS.SearchCasoRow row in theTable.Rows)
                        {
                            Caso theCaso = FillRecord(row);
                            theCaso.RowNumber = row.RowNumber;
                            _cache.Add(theCaso);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Caso", ex);
                throw;
            }
            return (int)intTotalRows;
        }
        //for Aprobation
        public static int SearchCasoForAprobation(List<Caso> _cache, int pageSize, int firstRow, string where, int ClienteId, string orderBy)
        //string where, int ClienteId, DateTime FechaInicio, DateTime FechaFin)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            if (string.IsNullOrEmpty(where))
                where = "";

            int? intTotalRows = 0;
            try
            {
                CasoDSTableAdapters.SearchCasoForAprobationTableAdapter theAdapter = new CasoDSTableAdapters.SearchCasoForAprobationTableAdapter();
                theAdapter.cmdTimeout = 600;
                CasoDS.SearchCasoForAprobationDataTable theTable = theAdapter.SearchCasoForAprobation(ClienteId, pageSize, firstRow, ref intTotalRows, where, orderBy);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoDS.SearchCasoForAprobationRow row in theTable.Rows)
                    {
                        Caso theCaso = FillRecord(row, ClienteId);
                        _cache.Add(theCaso);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting SearchCasoForAprobation", ex);
                throw;
            }
            return (int)intTotalRows;
        }

        public static void InsertCasoDesgravamen(EnlaceDesgravamenSISA obj, string ciudadId, int userId)
        {
            obj.CodigoCaso = "D" + obj.CitaDesgravamenId.ToString().PadLeft(7, '0');
            int? casoId = 0;
            try
            {
                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                theAdapter.InsertCasoDesgravamen(ref casoId, obj.CodigoCaso, 0, ciudadId, userId, obj.NumeroPoliza, obj.PacienteId);

                log.Debug("Se insertó el Caso con ID:" + casoId);

                if (casoId == null || casoId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                obj.CasoId = (int)casoId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso", q);
                throw q;
            }
        }

        public static int InsertCasoRecordDirty(Caso objCaso)
        {
            int? CasoId = 0;
            string CodigoCaso = "";
            //if (string.IsNullOrEmpty(objCaso.CodigoCaso))
            //    throw new ArgumentException("CodigoCaso cannot be null.");
            //if (objCaso.Correlativo<=0)
            //    throw new ArgumentException("Correlativo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.CiudadId))
                throw new ArgumentException("CiudadId cannot be null.");
            if (objCaso.UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");
            if (objCaso.PolizaId <= 0)
                throw new ArgumentException("PolizaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.MotivoConsultaId))
                throw new ArgumentException("MotivoConsultaId cannot be null.");
            if (string.IsNullOrEmpty(objCaso.Estado))
                objCaso.Estado = "Abierto";
            if (objCaso.PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            try
            {
                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                theAdapter.InsertCaso(ref CasoId, ref CodigoCaso, objCaso.Correlativo, objCaso.CiudadId
                    , DateTime.UtcNow, objCaso.UserId, objCaso.PolizaId, objCaso.MotivoConsultaId
                    , objCaso.Estado, objCaso.Dirty, objCaso.PacienteId);

                log.Debug("Se insertó el Caso con ID:" + CasoId);

                if (CasoId == null || CasoId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                return (int)CasoId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso", q);
                throw q;
            }
        }

        public static bool UpdateCasoMedicoByEnfermeria(Caso objCaso)
        {
            if (objCaso.CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.CodigoCaso))
                throw new ArgumentException("CodigoCaso cannot be null.");

            if (string.IsNullOrEmpty(objCaso.MotivoConsultaId))
                throw new ArgumentException("MotivoConsultaId cannot be null.");
            if (string.IsNullOrEmpty(objCaso.Estado))
                throw new ArgumentException("Estado cannot be null.");

            if (objCaso.PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            if (objCaso.HistoriaId <= 0)
                throw new ArgumentException("HistoriaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.DiagnosticoPresuntivo) && string.IsNullOrEmpty(objCaso.EnfermedadId))
                throw new ArgumentException("DiagnosticoPresuntivo cannot be null.");

            if (string.IsNullOrEmpty(objCaso.MotivoConsulta))
                objCaso.MotivoConsulta = "";
            if (string.IsNullOrEmpty(objCaso.BiometriaHematica))
                objCaso.BiometriaHematica = "";

            string Antecedentes = "";
            string AntecedentesAlergicos = "";
            string AntecedentesGinecoobstetricos = "";

            if (objCaso.MotivoConsultaId != "ENFER")
            {
                Antecedentes = objCaso.Antecedentes;
                AntecedentesAlergicos = objCaso.AntecedentesAlergicos;
                AntecedentesGinecoobstetricos = objCaso.AntecedentesGinecoobstetricos;
            }

            try
            {
                CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                TheAdapter.UpdateCasoDirty(objCaso.CasoId, 0, 0, objCaso.CodigoCaso, objCaso.Correlativo
                    , objCaso.MotivoConsultaId, objCaso.Estado
                    , Configuration.Configuration.ConvertToUTCFromClientTimeZone(objCaso.FechaCreacion), DateTime.UtcNow
                    , objCaso.PacienteId
                    , Antecedentes, AntecedentesAlergicos, AntecedentesGinecoobstetricos
                    , objCaso.HistoriaId, objCaso.MotivoConsulta, objCaso.EnfermedadActual, objCaso.ProtocoloId
                    , objCaso.PresionArterial, objCaso.Pulso
                    , objCaso.Temperatura, objCaso.FrecuenciaCardiaca, objCaso.Peso, objCaso.EstaturaCm, objCaso.ExFisicoRegionalyDeSistema
                    , objCaso.DiagnosticoPresuntivo, objCaso.EnfermedadId, objCaso.Enfermedad2Id
                    , objCaso.Enfermedad3Id, objCaso.BiometriaHematica, objCaso.Observaciones);
                string msgBitacora = "Insertando Caso de Enfermeria SISA con datos: CasoId:" + objCaso.CasoId + ", Paciente:" + objCaso.PacienteId + ", Antecedentes:" + objCaso.Antecedentes + ", AntecedentesAlergicos:" + objCaso.AntecedentesAlergicos + ", AntecedentesGinecoobstetricos:" + objCaso.AntecedentesGinecoobstetricos;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertEnfermeriaSISA, HttpContext.Current.User.Identity.Name, "EnfermeriaSisa", objCaso.CodigoCaso, msgBitacora);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Caso", q);
                throw;
            }
            return true;
        }
        //private ValidacionBLL objvalidacion = new ValidacionBLL();
        public static bool UpdateCasoMedico(Caso objCaso)
        {
            string MotivoConsultaEM = System.Web.Configuration.WebConfigurationManager.AppSettings["MotivoConsultaEM"]; 
            string MotivoConsultaMG = System.Web.Configuration.WebConfigurationManager.AppSettings["MotivoConsultaMG"];
            string MotivoConsultaODO = System.Web.Configuration.WebConfigurationManager.AppSettings["MotivoConsultaODO"];

            if (objCaso.CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.CodigoCaso))
                throw new ArgumentException("CodigoCaso cannot be null.");
            //if (objCaso.Correlativo <= 0)
            //    throw new ArgumentException("Correlativo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.MotivoConsultaId))
                throw new ArgumentException("MotivoConsultaId cannot be null.");
            if (string.IsNullOrEmpty(objCaso.Estado))
                throw new ArgumentException("Estado cannot be null.");

            if (objCaso.PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            if (objCaso.HistoriaId <= 0)
                throw new ArgumentException("HistoriaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.MotivoConsulta))
                throw new ArgumentException("MotivoConsulta cannot be null.");
            if (string.IsNullOrEmpty(objCaso.DiagnosticoPresuntivo) && string.IsNullOrEmpty(objCaso.EnfermedadId))
                throw new ArgumentException("DiagnosticoPresuntivo cannot be null.");

            if (objCaso.Dirty)
                objCaso.FechaReconsulta = objCaso.FechaCreacion;
            if (objCaso.BiometriaHematica == null)
            {
                objCaso.BiometriaHematica = "";
            }

            if (objCaso.EnfermedadId == objCaso.Enfermedad2Id)
            {
                if (!string.IsNullOrWhiteSpace(objCaso.Enfermedad3Id))
                {
                    if (objCaso.EnfermedadId == objCaso.Enfermedad3Id)
                    {
                        objCaso.Enfermedad2Id = "";
                        objCaso.Enfermedad3Id = "";
                    }
                    else
                    {
                        objCaso.Enfermedad2Id = objCaso.Enfermedad3Id;
                    }
                }
                else
                {
                    objCaso.Enfermedad2Id = "";
                }
            }
            else if (objCaso.EnfermedadId == objCaso.Enfermedad3Id)
            {
                objCaso.Enfermedad3Id = "";
            }
            else if (objCaso.Enfermedad2Id == objCaso.Enfermedad3Id)
            {
                objCaso.Enfermedad3Id = "";
            }

            try
            {
                 if (objCaso.MotivoConsultaId.Contains(MotivoConsultaEM))
                {
                    int ClienteId = 0;
                    CasoDSTableAdapters.SearchCasoTableAdapter theAdapter = new CasoDSTableAdapters.SearchCasoTableAdapter();
                    CasoDS.SearchCasoDataTable theTable = theAdapter.GetCasoBasicByCasoId(objCaso.CasoId);
                    if (theTable != null && theTable.Rows.Count > 0)
                    {
                        CasoDS.SearchCasoRow row = theTable[0];
                        ClienteId = row.ClienteId;
                    }
                    Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(objCaso.CasoId);
                    string respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, ClienteId, "EM", "");


                    bool Existe = BusquedaDeValorCoPagoEmergencias(ClienteId);
                    if (Existe)
                    {
                        if (respuesta == "")
                        {
                            int length = objCaso.Antecedentes.Length;

                            CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                            TheAdapter.UpdateCasoDirty(objCaso.CasoId, objCaso.ProveedorId, objCaso.CitaId, objCaso.CodigoCaso, objCaso.Correlativo
                                , objCaso.MotivoConsultaId, objCaso.Estado, Configuration.Configuration.ConvertToUTCFromClientTimeZone(objCaso.FechaCreacion),
                                System.Data.SqlTypes.SqlDateTime.MinValue.Value, objCaso.PacienteId
                                , objCaso.Antecedentes, objCaso.AntecedentesAlergicos, objCaso.AntecedentesGinecoobstetricos
                                , objCaso.HistoriaId, objCaso.MotivoConsulta, objCaso.EnfermedadActual, objCaso.ProtocoloId
                                , objCaso.PresionArterial, objCaso.Pulso
                                , objCaso.Temperatura, objCaso.FrecuenciaCardiaca, objCaso.Peso, objCaso.EstaturaCm
                                , objCaso.ExFisicoRegionalyDeSistema, objCaso.DiagnosticoPresuntivo, objCaso.EnfermedadId
                                , objCaso.Enfermedad2Id, objCaso.Enfermedad3Id, objCaso.BiometriaHematica, objCaso.Observaciones);
                            string msgBitacora = "Insertando Caso Medico SISA con datos: CasoId:" + objCaso.CasoId + ", Paciente:" + objCaso.PacienteId + ", Antecedentes:" + objCaso.Antecedentes + ", AntecedentesAlergicos:" + objCaso.AntecedentesAlergicos + ", AntecedentesGinecoobstetricos:" + objCaso.AntecedentesGinecoobstetricos;
                            theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertCasoMedicoSISA, HttpContext.Current.User.Identity.Name, "CasoMedicoSisa", objCaso.CodigoCaso, msgBitacora);

                            //para cargar al Caso Emergencia 

                            Artexacta.App.CasoEmergecia.CasoEmergencia ObjCasoEmergencia = new Artexacta.App.CasoEmergecia.CasoEmergencia();
                            ObjCasoEmergencia.casoId = objCaso.CasoId;
                            ObjCasoEmergencia.detFecha = DateTime.Today;
                            ObjCasoEmergencia.detMontoCoPago = BusquedaDeValorCoPagoEmergencias("Monto", ClienteId);
                            ObjCasoEmergencia.detPorcentajeCopago = BusquedaDeValorCoPagoEmergencias("Porcentaje", ClienteId);
                            ObjCasoEmergencia.detMontoEmergencia = 0;
                            ObjCasoEmergencia.detMontoEstudios = 0;
                            ObjCasoEmergencia.detMontoFarmacia = 0;
                            ObjCasoEmergencia.detMontoHonorariosMedicos = 0;
                            ObjCasoEmergencia.detMontoLaboratorios = 0;
                            ObjCasoEmergencia.detMontoOtros = 0;
                            ObjCasoEmergencia.detMontoTotal = 0;
                            Artexacta.App.CasoEmergecia.BLL.CasoEmergenciaBLL.InsertCasoEmergencia(ObjCasoEmergencia);


                        }
                        else
                        {
                            SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + respuesta + " Para Este Servicio");

                            return false;
                        }
                    }
                }
                else
                {

                    if (objCaso.MotivoConsultaId.ToUpper() == MotivoConsultaMG.ToUpper())
                    {

                        if (VerificarValoresCortarvalores(objCaso.Antecedentes))
                        {
                            bool Genero;
                            try {
                                Genero = Paciente.BLL.PacienteBLL.GetPacienteByPacienteId(objCaso.PacienteId).Genero;
                            }
                            catch
                            {
                                Genero = false;
                            }
                            //valor del Genero
                            if (VerificarValoresCortarvalores(objCaso.AntecedentesGinecoobstetricos) | Genero==true)
                            {
                                if (VerificarValoresCortarvalores(objCaso.ExFisicoRegionalyDeSistema) )
                                {

                                    //cortarvalores(objCaso.Antecedentes);

                                    int length = objCaso.Antecedentes.Length;
                                    CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                                    TheAdapter.UpdateCasoDirty(objCaso.CasoId, objCaso.ProveedorId, objCaso.CitaId, objCaso.CodigoCaso, objCaso.Correlativo
                                        , objCaso.MotivoConsultaId, objCaso.Estado, Configuration.Configuration.ConvertToUTCFromClientTimeZone(objCaso.FechaCreacion),
                                        System.Data.SqlTypes.SqlDateTime.MinValue.Value, objCaso.PacienteId
                                        , objCaso.Antecedentes, objCaso.AntecedentesAlergicos, objCaso.AntecedentesGinecoobstetricos
                                        , objCaso.HistoriaId, objCaso.MotivoConsulta, objCaso.EnfermedadActual, objCaso.ProtocoloId
                                        , objCaso.PresionArterial, objCaso.Pulso
                                        , objCaso.Temperatura, objCaso.FrecuenciaCardiaca, objCaso.Peso, objCaso.EstaturaCm
                                        , objCaso.ExFisicoRegionalyDeSistema, objCaso.DiagnosticoPresuntivo, objCaso.EnfermedadId
                                        , objCaso.Enfermedad2Id, objCaso.Enfermedad3Id, objCaso.BiometriaHematica, objCaso.Observaciones);
                                    string msgBitacora = "Insertando Caso Medico SISA con datos: CasoId:" + objCaso.CasoId + ", Paciente:" + objCaso.PacienteId + ", Antecedentes:" + objCaso.Antecedentes + ", AntecedentesAlergicos:" + objCaso.AntecedentesAlergicos + ", AntecedentesGinecoobstetricos:" + objCaso.AntecedentesGinecoobstetricos;
                                    theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertCasoMedicoSISA, HttpContext.Current.User.Identity.Name, "CasoMedicoSisa", objCaso.CodigoCaso, msgBitacora);
                                }
                                else
                                {
                                    SystemMessages.DisplaySystemErrorMessage("Tiene Que llenar Todos Los campos de Examenes Fisicos Regional");
                                    return false;
                                }
                            }
                            else
                            {
                                SystemMessages.DisplaySystemErrorMessage("Tiene Que llenar Todos Los campos de Antecedentes Ginecoobstetricos");
                                return false;
                            }
                        }
                        else
                        {
                            SystemMessages.DisplaySystemErrorMessage("Tiene Que llenar Todos Los campos Antecedentes Personales");
                            return false;
                        }
                    }
                    else
                    {   

                        if (objCaso.MotivoConsultaId.ToUpper() == MotivoConsultaODO.ToUpper())
                        {

                            int ClienteId = 0;
                            CasoDSTableAdapters.SearchCasoTableAdapter theAdapter = new CasoDSTableAdapters.SearchCasoTableAdapter();
                            CasoDS.SearchCasoDataTable theTable = theAdapter.GetCasoBasicByCasoId(objCaso.CasoId);
                            if (theTable != null && theTable.Rows.Count > 0)
                            {
                                CasoDS.SearchCasoRow row = theTable[0];
                                ClienteId = row.ClienteId;
                            }
                            Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(objCaso.CasoId);
                           // string respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, ClienteId, "EM", "");

                            int length = objCaso.Antecedentes.Length;

                            CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                            TheAdapter.UpdateCasoDirty(objCaso.CasoId, objCaso.ProveedorId, objCaso.CitaId, objCaso.CodigoCaso, objCaso.Correlativo
                                , objCaso.MotivoConsultaId, objCaso.Estado, Configuration.Configuration.ConvertToUTCFromClientTimeZone(objCaso.FechaCreacion),
                                System.Data.SqlTypes.SqlDateTime.MinValue.Value, objCaso.PacienteId
                                , objCaso.Antecedentes, objCaso.AntecedentesAlergicos, objCaso.AntecedentesGinecoobstetricos
                                , objCaso.HistoriaId, objCaso.MotivoConsulta, objCaso.EnfermedadActual, objCaso.ProtocoloId
                                , objCaso.PresionArterial, objCaso.Pulso
                                , objCaso.Temperatura, objCaso.FrecuenciaCardiaca, objCaso.Peso, objCaso.EstaturaCm
                                , objCaso.ExFisicoRegionalyDeSistema, objCaso.DiagnosticoPresuntivo, objCaso.EnfermedadId
                                , objCaso.Enfermedad2Id, objCaso.Enfermedad3Id, objCaso.BiometriaHematica, objCaso.Observaciones);
                            string msgBitacora = "Insertando Caso Medico SISA con datos: CasoId:" + objCaso.CasoId + ", Paciente:" + objCaso.PacienteId + ", Antecedentes:" + objCaso.Antecedentes + ", AntecedentesAlergicos:" + objCaso.AntecedentesAlergicos + ", AntecedentesGinecoobstetricos:" + objCaso.AntecedentesGinecoobstetricos;
                            theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertCasoMedicoSISA, HttpContext.Current.User.Identity.Name, "CasoMedicoSisa", objCaso.CodigoCaso, msgBitacora);

                            // para cargar en tabla odontologia
                            Artexacta.App.Odontologia.Odontologia  odonto = new Artexacta.App.Odontologia.Odontologia();
                            odonto.CasoId = objCaso.CasoId;
                            odonto.OdontologiaId = 0;
                            odonto.PrestacionOdontologicaId = BusquedaValorConsultaClinicaOdo();
                            odonto.Pieza ="TODAS";
                            odonto.Observaciones = objCaso.Observaciones;
                            Artexacta.App.Odontologia.BLL.OdontologiaBLL.InsertOdontologia(odonto);
                            //para cargar al Caso Odontologia 


                            //string ValorODO = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorODO"];
                            //int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                            //Artexacta.App.CoPagos.CasoOdontologia ObjCasoOdontologia = new Artexacta.App.CoPagos.CasoOdontologia();
                            //ObjCasoOdontologia.CasoId = objCaso.CasoId;
                            //ObjCasoOdontologia.Fecha = DateTime.Now;
                            //ObjCasoOdontologia.PrestacionOdontologicaId = BusquedaValorConsultaClinicaOdo();
                            //ObjCasoOdontologia.ProveedorId=Artexacta.App.Proveedor.BLL.ProveedorMedicoBLL.GetProveedorPrecioByUserId(UserId).ProveedorId;
                            //ObjCasoOdontologia.detPrecio = BusquedaDePrecioPRestacionesOdontologicas(ObjCasoOdontologia.ProveedorId,ObjCasoOdontologia.PrestacionOdontologicaId);
                            //ObjCasoOdontologia.detCoPagoMonto = BusquedaDeValorCoPago("Monto",ClienteId, ValorODO); ;
                            //ObjCasoOdontologia.detCoPagoPorcentaje = BusquedaDeValorCoPago("Porcentaje", ClienteId, ValorODO);                         
                            //Artexacta.App.CoPagos.BLL.CoPagosBLL.InsertCasoOdontologiaConsultaMedica(ObjCasoOdontologia);
                        }
                        else
                        {
                            int length = objCaso.Antecedentes.Length;

                            CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                            TheAdapter.UpdateCasoDirty(objCaso.CasoId, objCaso.ProveedorId, objCaso.CitaId, objCaso.CodigoCaso, objCaso.Correlativo
                                , objCaso.MotivoConsultaId, objCaso.Estado, Configuration.Configuration.ConvertToUTCFromClientTimeZone(objCaso.FechaCreacion),
                                System.Data.SqlTypes.SqlDateTime.MinValue.Value, objCaso.PacienteId
                                , objCaso.Antecedentes, objCaso.AntecedentesAlergicos, objCaso.AntecedentesGinecoobstetricos
                                , objCaso.HistoriaId, objCaso.MotivoConsulta, objCaso.EnfermedadActual, objCaso.ProtocoloId
                                , objCaso.PresionArterial, objCaso.Pulso
                                , objCaso.Temperatura, objCaso.FrecuenciaCardiaca, objCaso.Peso, objCaso.EstaturaCm
                                , objCaso.ExFisicoRegionalyDeSistema, objCaso.DiagnosticoPresuntivo, objCaso.EnfermedadId
                                , objCaso.Enfermedad2Id, objCaso.Enfermedad3Id, objCaso.BiometriaHematica, objCaso.Observaciones);
                            string msgBitacora = "Insertando Caso Medico SISA con datos: CasoId:" + objCaso.CasoId + ", Paciente:" + objCaso.PacienteId + ", Antecedentes:" + objCaso.Antecedentes + ", AntecedentesAlergicos:" + objCaso.AntecedentesAlergicos + ", AntecedentesGinecoobstetricos:" + objCaso.AntecedentesGinecoobstetricos;
                            theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertCasoMedicoSISA, HttpContext.Current.User.Identity.Name, "CasoMedicoSisa", objCaso.CodigoCaso, msgBitacora);
                        }
                    }
                }
            }

            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Caso", q);
                throw;
            }
            return true;

        }
        private static bool  VerificarValoresCortarvalores(string valor)
        {
            bool CamposCompleto = true;
            char separador = ','; // separador de datos
            valor = valor.Replace("[","").Replace("{", "").Replace("}","").Replace("]", "");
            valor.ToString().Replace('"', ' ').Trim();
            string res = "";
            for (int i=0;i<valor.Length -1 ; i++)
            {
                char tarea = valor[i];
                if (tarea != '\'' & tarea != '"')
                    res =res+ tarea.ToString() ;
            }
            string[] arregloDeSubCadenas = res.Split(separador);

            for (int i=0;i<arregloDeSubCadenas.Count();i++)
            {
                if (arregloDeSubCadenas[i].Contains("value"))
                {
                    char separadorvalue = ':';
                    string[] arreglodevalue = arregloDeSubCadenas[i].Split(separadorvalue);
                    string comparacion = arreglodevalue[1];
                    comparacion=comparacion.Replace(" ","");
                    if (comparacion.Length < 1 | comparacion == " " | comparacion == "")
                    {
                        CamposCompleto = false;
                        break;
                    }
                }
            }
            return CamposCompleto;
        }
       

        //metodo que permite buscar el Copago de Emergencia
        private static decimal BusquedaDeValorCoPagoEmergencias(string TipodePago, int ClienteId)
        {
            decimal CopagoValor = 0;
            string ValorEM = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorEM"];
            List<Artexacta.App.RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones =
                Artexacta.App.RedClientePrestaciones.BLL.RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(ClienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion.Contains(ValorEM))
                {
                    if (ListaPrestaciones[i].CoPagoMonto > 0 & TipodePago == "Monto")
                    {

                        CopagoValor = ListaPrestaciones[i].CoPagoMonto;
                        break;
                    }
                    else
                    {
                        if (ListaPrestaciones[i].CoPagoPorcentaje > 0 & TipodePago == "Porcentaje")
                        {
                            CopagoValor = (ListaPrestaciones[i].CoPagoPorcentaje);
                            break;
                        }
                    }
                }
            }
            return CopagoValor;
        }
        private static bool BusquedaDeValorCoPagoEmergencias(int ClienteId)
        {
            string ValorEM = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorEM"];

            List<Artexacta.App.RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones =
                Artexacta.App.RedClientePrestaciones.BLL.RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(ClienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion.Contains(ValorEM))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UpdateEmergencia(Caso objCaso)
        {
            if (objCaso.CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.CodigoCaso))
                throw new ArgumentException("CodigoCaso cannot be null.");
            //if (objCaso.Correlativo <= 0)
            //    throw new ArgumentException("Correlativo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.MotivoConsultaId))
                throw new ArgumentException("MotivoConsultaId cannot be null.");
            if (string.IsNullOrEmpty(objCaso.Estado))
                throw new ArgumentException("Estado cannot be null.");
            if (objCaso.ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            if (objCaso.PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            if (objCaso.HistoriaId <= 0)
                throw new ArgumentException("HistoriaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(objCaso.MotivoConsulta))
                throw new ArgumentException("MotivoConsulta cannot be null.");
            if (string.IsNullOrEmpty(objCaso.DiagnosticoPresuntivo) && string.IsNullOrEmpty(objCaso.EnfermedadId))
                throw new ArgumentException("DiagnosticoPresuntivo cannot be null.");

            if (objCaso.Dirty)
                objCaso.FechaReconsulta = objCaso.FechaCreacion;
            if (objCaso.BiometriaHematica == null)
            {
                objCaso.BiometriaHematica = "";
            }

            if (objCaso.EnfermedadId == objCaso.Enfermedad2Id)
            {
                if (!string.IsNullOrWhiteSpace(objCaso.Enfermedad3Id))
                {
                    if (objCaso.EnfermedadId == objCaso.Enfermedad3Id)
                    {
                        objCaso.Enfermedad2Id = "";
                        objCaso.Enfermedad3Id = "";
                    }
                    else
                    {
                        objCaso.Enfermedad2Id = objCaso.Enfermedad3Id;
                    }
                }
                else
                {
                    objCaso.Enfermedad2Id = "";
                }
            }
            else if (objCaso.EnfermedadId == objCaso.Enfermedad3Id)
            {
                objCaso.Enfermedad3Id = "";
            }
            else if (objCaso.Enfermedad2Id == objCaso.Enfermedad3Id)
            {
                objCaso.Enfermedad3Id = "";
            }

            try
            {
                CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                TheAdapter.UpdateCasoDirty(objCaso.CasoId, objCaso.ProveedorId, objCaso.CitaId, objCaso.CodigoCaso, objCaso.Correlativo
                    , objCaso.MotivoConsultaId, objCaso.Estado, Configuration.Configuration.ConvertToUTCFromClientTimeZone(objCaso.FechaCreacion),
                    System.Data.SqlTypes.SqlDateTime.MinValue.Value, objCaso.PacienteId
                    , objCaso.Antecedentes, objCaso.AntecedentesAlergicos, objCaso.AntecedentesGinecoobstetricos
                    , objCaso.HistoriaId, objCaso.MotivoConsulta, objCaso.EnfermedadActual, objCaso.ProtocoloId
                    , objCaso.PresionArterial, objCaso.Pulso
                    , objCaso.Temperatura, objCaso.FrecuenciaCardiaca, objCaso.Peso, objCaso.EstaturaCm
                    , objCaso.ExFisicoRegionalyDeSistema, objCaso.DiagnosticoPresuntivo, objCaso.EnfermedadId
                    , objCaso.Enfermedad2Id, objCaso.Enfermedad3Id, objCaso.BiometriaHematica, objCaso.Observaciones);
                string msgBitacora = "Actualizando Emergencia Medica SISA con datos: CasoId:" + objCaso.CasoId + ", Paciente:" + objCaso.PacienteId + ", Antecedentes:" + objCaso.Antecedentes + ", AntecedentesAlergicos:" + objCaso.AntecedentesAlergicos + ", AntecedentesGinecoobstetricos:" + objCaso.AntecedentesGinecoobstetricos;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertEmergenciaSISA, HttpContext.Current.User.Identity.Name, "UpdateEmergencia", objCaso.CodigoCaso, msgBitacora);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Caso", q);
                throw;
            }
            return true;
        }
       
        // metodo que permite buscar el copago dontologia
        public static decimal BusquedaDePrecioPRestacionesOdontologicas(int proveedorId, int PrestacionesOdontologicasId)
        {
            string ValorODO = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorODO"];
            decimal valor = 0;
            List<RedProvLabImgCarDetallePrestaciones> Lista = RedProvLabImgCarDetallePrestacionesBLL.GetProvLabImgCarDetallePrestaciones(proveedorId, ValorODO, 0);
            for (int i = 0; i < Lista.Count; i++)
            {
                if (Lista[i].EstudioId == PrestacionesOdontologicasId)
                {
                    valor = Lista[i].detPrecio;
                }
            }
            return valor;
        }
        public static decimal BusquedaDeValorCoPago(string TipodePago, int clienteId, string TipoPrestacion)
        {
            decimal CopagoValor = 0;
            List<RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(clienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion.Contains(TipoPrestacion))
                {
                    if (TipodePago.Contains("Monto"))
                    {
                        string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoMonto * 100) / 100));
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                    else
                    {
                        if (TipodePago.Contains("Porcentaje"))
                        {
                            string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoPorcentaje * 100) / 100));
                            CopagoValor = decimal.Parse(ValorCoPago);
                            break;
                        }
                    }
                }

            }
            return CopagoValor;
        }
        private static  int BusquedaValorConsultaClinicaOdo()
        {
            string ConsultaOdontologica = System.Web.Configuration.WebConfigurationManager.AppSettings["ConsultaOdontologica"];
            List<Artexacta.App.CLAPrestacionOdontologica.PrestacionOdontologica>  ListPrestacionOdontologica = new List<CLAPrestacionOdontologica.PrestacionOdontologica>();
            ListPrestacionOdontologica = Artexacta.App.CLAPrestacionOdontologica.BLL.PrestacionOdontologicaBLL.getListAllPrestacionOdontologica();
            for (int i=0;i<ListPrestacionOdontologica.Count;i++)
            {
                if (ListPrestacionOdontologica[i].Nombre.ToUpper()==ConsultaOdontologica.ToUpper())
                {
                    return ListPrestacionOdontologica[i].PrestacionOdontologicaId;
                }
            }
            return 1;
        }


        //Historial
        public static List<Caso> HistorialPaciente(int PacienteId, int CasoId)
        {
            List<Caso> theList = new List<Caso>();
            Caso objCaso = null;

            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            try
            {
                CasoDSTableAdapters.HistorialTableAdapter theAdapter = new CasoDSTableAdapters.HistorialTableAdapter();
                theAdapter.cmdTimeout = 600;
                CasoDS.HistorialDataTable theTable = theAdapter.GetHistorialByPacienteId(PacienteId, CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoDS.HistorialRow row in theTable.Rows)
                    {
                        objCaso = FillRecord(row);
                        objCaso.PacienteId = PacienteId;
                        theList.Add(objCaso);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting HistorialPaciente", ex);
                throw;
            }
            return theList;
        }

        //Historial utilizado solo por desgravamen
        public static List<Caso> HistorialPacienteDESG(int PacienteId, int CasoId, int ClienteIdDESG)
        {
            List<Caso> theList = new List<Caso>();
            Caso objCaso = null;

            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            try
            {
                CasoDSTableAdapters.HistorialTableAdapter theAdapter = new CasoDSTableAdapters.HistorialTableAdapter();
                theAdapter.cmdTimeout = 600;
                CasoDS.HistorialDataTable theTable = theAdapter.GetHistorialByPacienteId(PacienteId, CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoDS.HistorialRow row in theTable.Rows)
                    {
                        objCaso = FillRecord(row);
                        objCaso.PacienteId = PacienteId;
                        theList.Add(objCaso);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting HistorialPaciente", ex);
                throw;
            }
            List<Caso> finishedList = new List<Caso>();
            List<int> listNumbers = new List<int>();
            if (theList.Count > 0)
            {
                foreach (Caso c in theList)
                {
                    string sCodigo = c.CodigoCaso;
                    sCodigo = sCodigo.Substring(1);

                    int codigoInt = 0;
                    int.TryParse(sCodigo, out codigoInt);

                    CitaDesgravamen cita = null;
                    try
                    {
                        cita = CitaDesgravamenBLL.GetCitaDesgravamenById(codigoInt);
                    }
                    catch { }

                    if (cita != null && cita.ClienteId == ClienteIdDESG)
                    {
                        finishedList.Add(c);
                    }
                }
            }

            return finishedList;
        }

        public static void UpdateFechaEstado(int CasoId, string type)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            try
            {
                CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                TheAdapter.UpdateCasoFechaEstado(CasoId, type);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Caso", q);
                throw;
            }
        }

        public static void UpdateCasoMedico(int CasoId, string CodigoCaso, string observaciones)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            try
            {
                CasoDSTableAdapters.CasoTableAdapter TheAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                TheAdapter.UpdateCaso(CasoId, observaciones);
                string msgBitacora = "Actualizando Caso Medico SISA con datos: CasoId:" + CasoId + ", observaciones:" + observaciones;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.UpdateCasoMedicoSISA, HttpContext.Current.User.Identity.Name, "CasoMedicoSisa", CodigoCaso, msgBitacora);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Caso", q);
                throw;
            }
        }

        public static bool DeleteCasoByCadoId(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            int userId = 0;

            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            try
            {
                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                theAdapter.DeleteCasoByCasoId(CasoId, userId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Caso", ex);
                return false; //throw;
            }
        }

        public static bool BlockUnlock(int CasoId, bool block)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                return (bool)theAdapter.BlockUnlock(CasoId, block);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Blocking / Unlocking Gasto", ex);
                throw;
            }
        }
        //verifica si el caso tiene gastos por registrar
        public static bool CasoHaveGastosByToRegister(int CasoId)
        {
            bool? HaveGastos = true;
            try
            {
                if (CasoId <= 0)
                    throw new ArgumentException("CasoId cannot be less than or equal to zero.");

                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                theAdapter.Caso_HaveGastosByToRegister(CasoId, ref HaveGastos);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while HaveGastosByToRegister", ex);
                throw;
            }
            return (bool)HaveGastos;
        }

        public static string GetMotivoConsultaId(int CasoId)
        {
            try
            {
                if (CasoId <= 0)
                    throw new ArgumentException("CasoId cannot be less than or equal to zero.");

                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                return theAdapter.GetMotivoConsultaId(CasoId).ToString();
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while HaveGastosByToRegister", ex);
                throw;
            }
        }

        public static int CreateReconsulta(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            int? _CasoId = CasoId;
            try
            {
                CasoDSTableAdapters.CasoTableAdapter theAdapter = new CasoDSTableAdapters.CasoTableAdapter();
                theAdapter.CreateReconsulta(ref _CasoId);

                log.Debug("Se creo la reconsulta con ID:" + _CasoId);

                if (_CasoId == null || _CasoId <= 0)
                {
                    throw new Exception("SQL creó la reconsulta exitosamente pero retornó un status null <= 0");
                }
                return (int)_CasoId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while creating Reconsulta for CASO: " + CasoId, q);
                throw q;
            }
        }
    }
}