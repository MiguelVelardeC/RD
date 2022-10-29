using System;
using System.Collections.Generic;
using Artexacta.App.Documents;
using System.Linq;
using System.Web;
using log4net;
/// <summary>
/// Summary description for CasoEmergenciaBLL
/// </summary>
/// 
namespace Artexacta.App.CasoEmergecia.BLL
{
public class CasoEmergenciaBLL
    {

        private static readonly ILog log = LogManager.GetLogger("Standard");
        public CasoEmergenciaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static CasoEmergencia FillRecord(CasoEmergenciaDS.Caso_EmergenciaDSRow row)
        {
            CasoEmergencia objCasoEmergencia = new CasoEmergencia(
                row.detId
                ,row.casoId
                ,row.detMontoEmergencia
                ,row.detMontoHonorariosMedicos
                ,row.detMontoFarmacia
                ,row.detMontoLaboratorios
                ,row.detMontoEstudios
                ,row.detMontoOtros
                ,row.detMontoTotal
                ,row.detPorcentajeCopago
                ,row.detMontoCoPago
                ,row.detFecha
                );
            return objCasoEmergencia;
        }
        public static CasoEmergencia GetCasoEmergencia(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId no puede ser menor o igual a cero.");

            CasoEmergencia ObjCasoEmeregencia = null;
            try
            {

                CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter theAdapter = new CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter();

                CasoEmergenciaDS.Caso_EmergenciaDSDataTable theTable = theAdapter.GetAllCasoEmergencia(detId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoEmergenciaDS.Caso_EmergenciaDSRow row in theTable.Rows)
                    {
                        ObjCasoEmeregencia = FillRecord(theTable[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return ObjCasoEmeregencia;
        }
        public static void InsertCasoEmergencia(CasoEmergencia obj)
        {


            int? preId = 0;
            try
            {
                CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter theAdapter = new CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter();
                theAdapter.InsertCasoEmergencia(ref preId, obj.casoId,obj.detMontoFarmacia,obj.detMontoHonorariosMedicos,obj.detMontoFarmacia
                    ,obj.detMontoLaboratorios,obj.detMontoEstudios,obj.detMontoOtros,obj.detMontoTotal,obj.detPorcentajeCopago,obj.detMontoCoPago, obj.detFecha);

                if (preId == null || preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                // obj.detId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso Emergencia", q);
                throw q;
            }
        }
        public static void UpdateCasoEmergencia(CasoEmergencia ObjCasoEmergencia)
        {
            if (ObjCasoEmergencia.detid <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");

            try
            {
                CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter theAdapter = new CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter();
                theAdapter.UpdateCasoEmergencia(
                   ObjCasoEmergencia.detid
                  , ObjCasoEmergencia.detMontoEmergencia
                  , ObjCasoEmergencia.detMontoHonorariosMedicos
                  , ObjCasoEmergencia.detMontoFarmacia
                  , ObjCasoEmergencia.detMontoLaboratorios
                  , ObjCasoEmergencia.detMontoEstudios
                  , ObjCasoEmergencia.detMontoOtros
                  , ObjCasoEmergencia.detMontoTotal
                   );
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Caso Especialidad", ex);
                throw;
            }
        }
        public static void UpdateCasoEmergencia(int detid
                  , decimal detMontoEmergencia
                  , decimal detMontoHonorariosMedicos
                  , decimal detMontoFarmacia
                  , decimal detMontoLaboratorios
                  , decimal detMontoEstudios
                  , decimal detMontoOtros
                  , decimal detMontoTotal)
        {
            if (detid <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");

            try
            {
                CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter theAdapter = new CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter();
                theAdapter.UpdateCasoEmergencia(
                   detid
                  , detMontoEmergencia
                  , detMontoHonorariosMedicos
                  , detMontoFarmacia
                  , detMontoLaboratorios
                  , detMontoEstudios
                  , detMontoOtros
                  , detMontoTotal
                   );
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Caso Especialidad", ex);
                throw;
            }
        }
        public static void InsertFile(int detId, int FileId)
        {
            if (detId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                CasoEmergenciaDSTableAdapters.Caso_Emergencia_GetFileTableAdapter theAdapter = new CasoEmergenciaDSTableAdapters.Caso_Emergencia_GetFileTableAdapter();
                theAdapter.InsertEmergenciaFile(detId,FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Emergencia File", ex);
                throw;
            }
        }


        public static bool DeleteCasoEmergencia(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {

                CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter theAdapter = new CasoEmergenciaDSTableAdapters.Caso_EmergenciaDSTableAdapter();
                theAdapter.DeleteCaso_Emergencia(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Emergencia", ex);
                throw;
            }
        }

        public static List<DocumentFile> GetEmergenciaFiles(int EmergenciaId)
        {
            if (EmergenciaId <= 0)
            {
                throw new ArgumentException("EmergenciaId cannot be equals or less than zero");
            }

            CasoEmergenciaDSTableAdapters.Caso_Emergencia_GetFileTableAdapter adapter = new CasoEmergenciaDSTableAdapters.Caso_Emergencia_GetFileTableAdapter();
            CasoEmergenciaDS.Caso_Emergencia_GetFileDataTable table = adapter.Emergencia_GetFile(EmergenciaId);

            List<DocumentFile> list = new List<DocumentFile>();
            foreach (var row in table)
            {
                DocumentFile obj = DocumentFile.CreateNewTypedDocumentFileObject(
                    row.fileID,
                    row.dateUploaded,
                    row.fileSize,
                    row.fileName,
                    row.extension,
                    row.fileStoragePath);
                list.Add(obj);
            }
            return list;
        }


    }
}