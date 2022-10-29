using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Caso;
using Artexacta.App.Documents;
using log4net;
using Artexacta.App.Desgravamen;
using Artexacta.App.GenericComboContainer;
namespace Artexacta.App.Derivacion.BLL
{
    /// <summary>
    /// Summary description for DerivacionBLL
    /// From now on only use _NEW versions of duplicate methods
    /// </summary>
    public class DerivacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public DerivacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Derivacion FillRecord(DerivacionDS.DerivacionRow row)
        {
            Derivacion objDerivacion = new Derivacion(
                row.DerivacionId
                , row.CasoId
                , row.ProveedorId
                , row.Observacion
                , row.IsGastoIdNull() ? 0 : row.GastoId
                , row.IsAprobacionUserIdNull() ? 0 : row.AprobacionUserId
                , row.IsAprobacionFechaNull() ? DateTime.MaxValue : Configuration.Configuration.ConvertToClientTimeZone(row.AprobacionFecha)
                , row.IsFechaCreacionNull() ? DateTime.MinValue : Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.IsNombreProveedorNull() ? "" : row.NombreProveedor
                , row.IsEspecialidadNull() ? "" : row.Especialidad

                , row.MontoConFactura
                , row.MontoSinFactura
                , row.RetencionImpuestos
                , row.Total
                );

            if (!row.IsFileCountNull())
            {
                objDerivacion.FileCount = row.FileCount;
            }

            return objDerivacion;
        }

        public static List<Derivacion> getDerivacionListByCasoId(int CasoId)
        {
            return getDerivacionListByCasoId(CasoId, false);
        }

        public static List<Derivacion> getDerivacionListByCasoId(int CasoId, bool isFileVisible)
        {
            List<Derivacion> theList = new List<Derivacion>();
            Derivacion theDerivacion = null;
            try
            {
                DerivacionDSTableAdapters.DerivacionTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionTableAdapter();
                DerivacionDS.DerivacionDataTable theTable = theAdapter.GetDerivacionByCasoId(CasoId, isFileVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (DerivacionDS.DerivacionRow row in theTable.Rows)
                    {
                        theDerivacion = FillRecord(row);
                        theList.Add(theDerivacion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by CasoId", ex);
                throw;
            }
            return theList;
        }

        public static List<DerivacionEspecialista> getDerivacionListByCasoId_NEW(int CasoId)
        {
            List<DerivacionEspecialista> theList = new List<DerivacionEspecialista>();
            DerivacionEspecialista theDerivacion = null;
            try
            {
                DerivacionDSTableAdapters.DerivacionCasoNEWTableAdapter Adapter = new DerivacionDSTableAdapters.DerivacionCasoNEWTableAdapter();
                DerivacionDS.DerivacionCasoNEWDataTable table = Adapter.GetDerivacionByCaso_NEW(CasoId, false);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DerivacionDS.DerivacionCasoNEWRow row in table.Rows)
                    {
                        theDerivacion = new DerivacionEspecialista()
                        {
                            DerivacionId = row.DerivacionId,
                            CasoId = row.CasoId,
                            ProveedorId = row.ProveedorId,
                            ProveedorNombre = row.NombreProveedor,
                            Observacion = row.Observacion,
                            UserId = row.AprobacionUserId,
                            MedicoId = row.MedicoId,
                            MedicoNombre = row.MedicoNombre,
                            FechaCreacion = row.FechaCreacion,
                            EspecialidadNombre = row.Especialidad,
                            FileCount = row.FileCount
                        };
                        theList.Add(theDerivacion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by CasoId", ex);
                throw;
            }
            return theList;
        }

        public static bool getDerivacionListByCasoIdandMedico_NEW(int CasoId, int MedicoId)
        {
            bool? result = false;
            try
            {
                DerivacionDSTableAdapters.DerivacionNEWTableAdapter Adapter = new DerivacionDSTableAdapters.DerivacionNEWTableAdapter();
                int functionResult = 0;
                functionResult = Adapter.VerifyDerivacionByCasoIdAndMedicoId_NEW(CasoId, MedicoId, ref result);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by CasoId", ex);
                throw;
            }
            return (result != null) ? result.Value : false;
        }

        public static Derivacion getDerivacionByDerivacionId(int DerivacionId)
        {
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");
            Derivacion theDerivacion = null;
            try
            {
                DerivacionDSTableAdapters.DerivacionTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionTableAdapter();
                DerivacionDS.DerivacionDataTable theTable = theAdapter.GetDerivacionByDerivacionId(DerivacionId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theDerivacion = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by DerivacionId", ex);
                throw;
            }
            return theDerivacion;
        }

        public static DerivacionEspecialista getDerivacionByDerivacionId_NEW(int DerivacionId)
        {
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");
            DerivacionEspecialista theDerivacion = null;
            try
            {
                DerivacionDSTableAdapters.DerivacionNEWTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionNEWTableAdapter();
                DerivacionDS.DerivacionNEWDataTable theTable = theAdapter.GetDerivacionByDerivacionId_NEW(DerivacionId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theDerivacion = new DerivacionEspecialista()
                    {
                        DerivacionId = theTable[0].DerivacionId,
                        CasoId = theTable[0].CasoId,
                        ProveedorId = theTable[0].ProveedorId,
                        ProveedorNombre = "",
                        UserId = theTable[0].AprobacionUserId,
                        GastoId = 0,
                        MedicoId = theTable[0].MedicoId,
                        MedicoNombre = theTable[0].MedicoNombre,
                        FechaCreacion = (!theTable[0].IsFechaCreacionNull()) ? theTable[0].FechaCreacion : DateTime.MinValue,
                        CasoIdCreado = theTable[0].CasoIdCreado,
                        Observacion = theTable[0].Observacion,
                        EspecialidadNombre = theTable[0].IsEspecialidadNull() ? "---" : theTable[0].Especialidad
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by DerivacionId", ex);
                throw;
            }
            return theDerivacion;
        }

        public static int InsertDerivacion(int CasoId, int ProveedorId
            , string Observaciones, DateTime FechaCreacion)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");

            /* string GastoRAW = System.Configuration.ConfigurationManager.AppSettings["GestionMedicaCurrentDerivacionGastoId"];
             int GastoId = 0;

             bool hello = int.TryParse(GastoRAW, out GastoId);*/


            int? DerivacionId = 0;

            try
            {
                DerivacionDSTableAdapters.DerivacionTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionTableAdapter();
                theAdapter.Insert(ref DerivacionId, CasoId, ProveedorId, Observaciones,
                    Configuration.Configuration.ConvertToUTCFromServerTimeZone(FechaCreacion));
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert new Derivacion", ex);
                throw;
            }
            return (DerivacionId).Value;
        }

        public static bool UpdateDerivacion(int DerivacionId, int ProveedorId, string Observaciones)
        {
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");

            try
            {
                DerivacionDSTableAdapters.DerivacionTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionTableAdapter();
                theAdapter.Update(DerivacionId, ProveedorId, Observaciones);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Derivacion", ex);
                throw;
            }
        }
        public static bool DeleteDerivacion(int DerivacionId)
        {
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");
            try
            {
                DerivacionDSTableAdapters.DerivacionTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionTableAdapter();
                theAdapter.Delete(DerivacionId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Derivacion", ex);
                throw;
            }
        }

        public static bool AproveDerivacion(int DerivacionId, int AprobacionUserId, DateTime FechaAprobacion)
        {
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");
            if (AprobacionUserId <= 0)
                throw new ArgumentException("AprobacionUserId cannot be less than or equal to zero.");

            try
            {
                DerivacionDSTableAdapters.DerivacionTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionTableAdapter();
                theAdapter.ApproveDerivacion(DerivacionId, AprobacionUserId,
                    Configuration.Configuration.ConvertToUTCFromClientTimeZone(FechaAprobacion));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while aprove Derivacion", ex);
                throw;
            }
        }
        public static int GetGastoIdDerivacion(int DerivacionId)
        {
            Derivacion objDetivacion = getDerivacionByDerivacionId(DerivacionId);
            return objDetivacion.GastoId;
        }

        public static List<DocumentFile> getDerivacionFileList(int DerivacionId, bool IsVisible)
        {
            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                DerivacionDSTableAdapters.DerivacionFileTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionFileTableAdapter();
                DerivacionDS.DerivacionFileDataTable theTable = theAdapter.GetDerivacionFiles(DerivacionId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (DerivacionDS.DerivacionFileRow row in theTable.Rows)
                    {
                        theList.Add(DocumentFile.CreateNewTypedDocumentFileObject(
                            row.fileID,
                            row.dateUploaded,
                            row.fileSize,
                            row.fileName,
                            row.extension,
                            row.fileStoragePath
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Derivacion file list", ex);
                throw;
            }
            return theList;
        }
        public static void DeleteFile(int FileId)
        {
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                DerivacionDSTableAdapters.DerivacionFileTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionFileTableAdapter();
                theAdapter.DeleteDerivacionFile(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Derivacion File", ex);
                throw;
            }
        }
        public static void InsertFile(int DerivacionId, int FileId, bool IsVisible)
        {
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                DerivacionDSTableAdapters.DerivacionFileTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionFileTableAdapter();
                theAdapter.InsertFile(FileId, DerivacionId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Derivacion File", ex);
                throw;
            }
        }

        public static PrintInfo GetDerivacionHeadByCasoIdForPrint(string DerivacionId)
        {
            if (string.IsNullOrEmpty(DerivacionId))
                throw new ArgumentException("DerivacionId cannot be null or empty.");

            try
            {
                PrintInfo info = null;
                DerivacionDSTableAdapters.DerivacionHeadForPrintTableAdapter theAdapter = new DerivacionDSTableAdapters.DerivacionHeadForPrintTableAdapter();
                DerivacionDS.DerivacionHeadForPrintDataTable theTable = theAdapter.GetDerivacionHeadByCasoIdForPrint(DerivacionId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    DerivacionDS.DerivacionHeadForPrintRow row = theTable[0];
                    info = new PrintInfo(row.NombrePaciente, row.NumeroPoliza, row.NombrePlan, row.IsTelefonoNull() ? "" : row.Telefono,
                         row.IsCarnetIdentidadNull() ? "" : row.CarnetIdentidad, row.NombreProveedor, "", row.Especialidad);
                    info.otros = row.Observacion;
                }
                return info;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by CasoId for print", ex);
                throw;
            }
        }

        public static int InsertDerivacionNEW(DerivacionEspecialista derivacion)
        {
            try
            {
                int? DerivacionId = 0;

                string proveedorIDRAW = System.Configuration.ConfigurationManager.AppSettings["GestionMedicaGenericEmptyProvider"];
                int proveedorId = 0;
                if (int.TryParse(proveedorIDRAW, out proveedorId))
                {
                    DerivacionDSTableAdapters.DerivacionNEWTableAdapter adapter = new DerivacionDSTableAdapters.DerivacionNEWTableAdapter();
                    adapter.InsertDerivacion_NEW(ref DerivacionId, derivacion.CasoId, proveedorId,
                        derivacion.Observacion, derivacion.FechaCreacion, derivacion.MedicoId,
                        derivacion.UserId, derivacion.GastoId);
                }
                //adapter.InsertDerivacion_NEW(ref DerivacionId, derivacion.CasoId, derivacion.ProveedorId, derivacion.Observacion, derivacion.FechaCreacion, derivacion.MedicoId, derivacion.UserId);
                return DerivacionId.Value;
            }
            catch (Exception eq)
            {
                log.Error("An error ocurred while inserting DerivacionNEW", eq);
                throw eq;
            }
        }

        public static bool UpdateDerivacionNEW(DerivacionEspecialista derivacion)
        {
            try
            {
                DerivacionDSTableAdapters.DerivacionNEWTableAdapter adapter = new DerivacionDSTableAdapters.DerivacionNEWTableAdapter();
                adapter.UpdateDerivacion_NEW(derivacion.DerivacionId, derivacion.Observacion, derivacion.CasoIdCreado);
                //adapter.InsertDerivacion_NEW(ref DerivacionId, derivacion.CasoId, derivacion.ProveedorId, derivacion.Observacion, derivacion.FechaCreacion, derivacion.MedicoId, derivacion.UserId);
                return true;
            }
            catch (Exception eq)
            {
                log.Error("An error ocurred while updating DerivacionNEW", eq);
            }

            return false;
        }

        public static List<DerivacionEspecialistaSearchResult> GetDerivacionEspecialistaBySearch(string varSql, int pageSize, int firstRow, ref int? totalRows, int medicoId)
        {
            bool isUserAdmin = false;
            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_LISTAR_DERIVACIONES_ADMIN");
                isUserAdmin = true;
            }
            catch (Exception eq)
            {
                log.Error("Something happened while getting the authorization credentials for CASOS_LISTA_DERIVACIONES_ADMIN", eq);
                isUserAdmin = false;
            }

            if (medicoId != 0)
            {
                if (!varSql.Contains("tmp.[medicoId]"))
                {

                    varSql = (varSql != "1=1") ? varSql + " AND tmp.[medicoId] = " + medicoId : "tmp.[medicoId] = " + medicoId;
                }
            }

            DerivacionEspecialistaSearchDSTableAdapters.usp_Derivacion_GetDerivacionesBySearchDUMMYTableAdapter adapter =
                new DerivacionEspecialistaSearchDSTableAdapters.usp_Derivacion_GetDerivacionesBySearchDUMMYTableAdapter();


            DerivacionEspecialistaSearchDS.usp_Derivacion_GetDerivacionesBySearchDUMMYDataTable table =
                adapter.GetDerivacionesBySearch(varSql, medicoId, pageSize, firstRow, ref totalRows);

            List<DerivacionEspecialistaSearchResult> list = new List<DerivacionEspecialistaSearchResult>();

            if (medicoId > 0 || (medicoId == 0 && isUserAdmin))
            {

                foreach (DerivacionEspecialistaSearchDS.usp_Derivacion_GetDerivacionesBySearchDUMMYRow row in table)
                {
                    DerivacionEspecialistaSearchResult obj = new DerivacionEspecialistaSearchResult();
                    obj.DerivacionId = row.DerivacionId;
                    obj.CasoId = row.CasoId;
                    obj.GastoId = row.GastoId;
                    obj.CiudadDerivacionId = row.CiudadDerivacionId;
                    obj.CiudadDerivacionNombre = row.CiudadDerivacionNombre;
                    obj.PacienteId = row.PacienteId;
                    obj.PacienteNombre = row.PacienteNombre;
                    obj.DerivadorNombre = row.DerivadorNombre;
                    obj.FechaCreacion = (!row.IsFechaCreacionNull()) ? row.FechaCreacion : DateTime.MinValue;
                    obj.MedicoId = row.MedicoId;
                    obj.MedicoNombre = row.MedicoNombre;
                    obj.DerivacionCasoId = row.DerivacionCasoId;
                    obj.CasoCodigoDerivado = row.CodigoCaso;
                    obj.CasoCodigoDerivacion = row.CodigoCasoDerivacion;
                    obj.EspecialidadNombre = row.EspecialidadNombre;
                    obj.CiudadDerivacionId = row.CiudadDerivacionId;
                    obj.CiudadDerivacionNombre = row.CiudadDerivacionNombre;
                    obj.ClienteId = row.ClienteId;
                    obj.ClienteNombre = row.ClienteNombre;
                    obj.isAtendido = row.IsAtendido;
                    list.Add(obj);
                }
            }

            return list;
        }
        //medico Derivador
        public static List<GenericComboContainer.GenericComboContainer> GetMedicosEspecialista(string ciudadId)
        {
            List<GenericComboContainer.GenericComboContainer> list = new List<GenericComboContainer.GenericComboContainer>();
            try
            {
                if (!string.IsNullOrEmpty(ciudadId))
                {
                    DerivacionComboDSTableAdapters.DerivacionComboTableAdapter adapter =
                    new DerivacionComboDSTableAdapters.DerivacionComboTableAdapter();
                    DerivacionComboDS.DerivacionComboDataTable table = adapter.GetEspecialistaByCiudadCombo(ciudadId);

                    foreach (DerivacionComboDS.DerivacionComboRow row in table)
                    {
                        GenericComboContainer.GenericComboContainer combo = new GenericComboContainer.GenericComboContainer()
                        {
                            ContainerId = row.containerId.ToString(),
                            ContainerName = row.containerName
                        };

                        list.Add(combo);
                    }
                }

            }
            catch (Exception eq)
            {
                string message = "Couldn't get the list of MedicosEspecialistas";
                log.Error(message, eq);
                log.Warn(message, eq);
                log.Debug(message);
            }



            return list;
        }

      
        //medico derivado
        public static List<GenericComboContainer.GenericComboContainer> GetMedicoUserIdByCiudadCombo(string ciudadId)
        {
            List<GenericComboContainer.GenericComboContainer> list = new List<GenericComboContainer.GenericComboContainer>();
            try
            {
                if (!string.IsNullOrEmpty(ciudadId))
                {
                    DerivacionComboDSTableAdapters.DerivacionComboTableAdapter adapter =
                    new DerivacionComboDSTableAdapters.DerivacionComboTableAdapter();
                    DerivacionComboDS.DerivacionComboDataTable table =
                        adapter.GetMedicoUserIdByCiudadCombo(ciudadId);

                    foreach (DerivacionComboDS.DerivacionComboRow row in table)
                    {
                        GenericComboContainer.GenericComboContainer combo = new GenericComboContainer.GenericComboContainer()
                        {
                            ContainerId = row.containerId.ToString(),
                            ContainerName = row.containerName
                        };

                        list.Add(combo);
                    }
                }

            }
            catch (Exception eq)
            {
                string message = "Couldn't get the list of MedicosEspecialistas";
                log.Error(message, eq);
                log.Warn(message, eq);
                log.Debug(message);
            }



            return list;
        }

        public static List<GenericComboContainer.GenericComboContainer> GetMedicosEspecialistaxCiuAndEsp(string ciudadId, int EspecialidadId)
        {
            List<GenericComboContainer.GenericComboContainer> list = new List<GenericComboContainer.GenericComboContainer>();
            try
            {
                if (!string.IsNullOrEmpty(ciudadId))
                {
                    DerivacionComboDSTableAdapters.DerivacionComboTableAdapter adapter =
                    new DerivacionComboDSTableAdapters.DerivacionComboTableAdapter();
                    DerivacionComboDS.DerivacionComboDataTable table = adapter.GetEspecialistaByCiudadAndEspecialidadCombo(ciudadId, EspecialidadId);

                    foreach (DerivacionComboDS.DerivacionComboRow row in table)
                    {
                        GenericComboContainer.GenericComboContainer combo = new GenericComboContainer.GenericComboContainer()
                        {
                            ContainerId = row.containerId.ToString(),
                            ContainerName = row.containerName
                        };

                        list.Add(combo);
                    }
                }

            }
            catch (Exception eq)
            {
                string message = "Couldn't get the list of MedicosEspecialistas";
                log.Error(message, eq);
                log.Warn(message, eq);
                log.Debug(message);
            }



            return list;
        }
        public static List<GenericComboContainer.GenericComboContainer> GetClientesByMedicoIdCombo(int medicoId)
        {
            List<GenericComboContainer.GenericComboContainer> list = new List<GenericComboContainer.GenericComboContainer>();
            try
            {
                DerivacionComboDSTableAdapters.DerivacionComboTableAdapter adapter =
                   new DerivacionComboDSTableAdapters.DerivacionComboTableAdapter();
                DerivacionComboDS.DerivacionComboDataTable table =
                    adapter.GetClientesByMedicoIdCombo(medicoId);

                foreach (DerivacionComboDS.DerivacionComboRow row in table)
                {
                    GenericComboContainer.GenericComboContainer combo = new GenericComboContainer.GenericComboContainer()
                    {
                        ContainerId = row.containerId.ToString(),
                        ContainerName = row.containerName
                    };

                    list.Add(combo);
                }

            }
            catch (Exception eq)
            {
                string message = "Couldn't get the list of Clientes";
                log.Error(message, eq);
                log.Warn(message, eq);
                log.Debug(message);
            }
            return list;
        }

        public static void MarkCasoAsCasoDerivacion(int casoId)
        {
            try
            {
                DerivacionEspecialistaSearchDSTableAdapters.usp_Derivacion_GetDerivacionesBySearchDUMMYTableAdapter adapter =
                new DerivacionEspecialistaSearchDSTableAdapters.usp_Derivacion_GetDerivacionesBySearchDUMMYTableAdapter();

                adapter.MarkCasoAsCasoDerivacion(casoId);
            }
            catch (Exception eq)
            {
                string message = "There was an error executing the method MarkCasoAsCasoDerivacion";
                log.Error(message, eq);
                log.Debug(message, eq);
                log.Fatal(message, eq);
                log.Info(message, eq);
                throw eq;
            }
        }
    }
}