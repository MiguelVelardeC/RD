using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Emergencia.BLL
{
    /// <summary>
    /// Summary description for EmergenciaBLL
    /// </summary>
    public class EmergenciaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EmergenciaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Emergencia FillRecord(EmergenciaDS.EmergenciaRow row)
        {
            Emergencia objEmergencia = new Emergencia(
                row.EmergenciaId
                , row.CasoId
                , row.ProveedorId
                , row.Observacion
                , row.IsGastoIdNull() ? 0 : row.GastoId
                , row.NombreProveedor

                , row.MontoConFactura
                , row.MontoSinFactura
                , row.RetencionImpuestos
                , row.Total
                , row.IsFileCountNull() ? 0 : row.FileCount
                );

            return objEmergencia;
        }

        //edwin suyo 
        private static Emergencia FillRecord(EmergenciaDS.Emergencia_EmergenciaRow row)
        {
            Emergencia objEmergencia = new Emergencia(
                row.EmergenciaId
                , row.CasoId
                , row.ProveedorId
                , row.Observacion
                , row.IsGastoIdNull() ? 0 : row.GastoId
                , row.NombreProveedor
                ,row.detFecha
                , row.MontoConFactura
                , row.MontoSinFactura
                , row.RetencionImpuestos
                , row.Total
                , row.IsFileCountNull() ? 0 : row.FileCount
                );

            return objEmergencia;
        }
        
        public static List<Emergencia> getEmergencia_EmergenciaDetailsByCasoId(int CasoId)
        {
            List<Emergencia> List = new List<Emergencia>();
            Emergencia theEmergencia = null;
            try
            {
                EmergenciaDSTableAdapters.Emergencia_EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.Emergencia_EmergenciaTableAdapter();
                EmergenciaDS.Emergencia_EmergenciaDataTable theTable = theAdapter.Emergencia_Emergencia_GetEmergenciaByCasoId(CasoId, false);


                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EmergenciaDS.Emergencia_EmergenciaRow row in theTable.Rows)
                    {
                        theEmergencia = null;
                        theEmergencia = FillRecord(row);
                        List.Add(theEmergencia);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Emergencia by CasoId", ex);
                throw;
            }
            return List;
        }
        //como ahora un caso solo deberia tener una emergencia entonces get list ya no es necesario
        public static List<Emergencia> getEmergenciaListByCasoId ( int CasoId )
        {
            return getEmergenciaListByCasoId(CasoId, false);
        }
        public static List<Emergencia> getEmergenciaListByCasoId ( int CasoId, bool isFileVisible )
        {
            List<Emergencia> theList = new List<Emergencia>();
            Emergencia theEmergencia = null;
            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                EmergenciaDS.EmergenciaDataTable theTable = theAdapter.GetEmergenciaByCasoId(CasoId, isFileVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EmergenciaDS.EmergenciaRow row in theTable.Rows)
                    {
                        theEmergencia = FillRecord(row);
                        theList.Add(theEmergencia);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Emergencia by CasoId", ex);
                throw;
            }
            return theList;
        }
       
        public static Emergencia getEmergenciaDetailsByCasoId(int CasoId)
        {
            Emergencia theEmergencia = null;
            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                EmergenciaDS.EmergenciaDataTable theTable = theAdapter.GetEmergenciaByCasoId(CasoId,true);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theEmergencia = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Emergencia by CasoId", ex);
                throw;
            }
            return theEmergencia;
        }

        public static Emergencia getEmergenciaByEmergenciaId(int EmergenciaId)
        {
            if (EmergenciaId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");
            Emergencia theEmergencia = null;
            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                EmergenciaDS.EmergenciaDataTable theTable = theAdapter.GetEmergenciaByEmergenciaId(EmergenciaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theEmergencia = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Emergencia by EmergenciaId", ex);
                throw;
            }
            return theEmergencia;
        }
      
        public static List<Emergencia> getEmergenciaByEmergenciaIdf(int EmergenciaId)
        {
            List<Emergencia> list = new List<Emergencia>();
            if (EmergenciaId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");
            Emergencia theEmergencia = null;

            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                EmergenciaDS.EmergenciaDataTable theTable = theAdapter.GetEmergenciaByEmergenciaId(EmergenciaId);

   

            foreach (EmergenciaDS.EmergenciaRow row in theTable)
            {
                    theEmergencia = FillRecord(row);
                    list.Add(theEmergencia);
            }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Emergencia by EmergenciaId", ex);
                throw;
            }
            return list;
        }
        public static int InsertEmergencia(int CasoId, int ProveedorId, string Observaciones, string PresionArterial, 
            string Pulso, string Temperatura, string FrecuenciaCardiaca, string DiagnosticoPresuntivo, string EnfermedadId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");
            if (string.IsNullOrEmpty(DiagnosticoPresuntivo) && (string.IsNullOrEmpty(EnfermedadId)))
                throw new ArgumentException("DiagnosticoPresuntivo cannot be null or empty.");

            int? EmergenciaId = 0;

            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                theAdapter.Insert(ref EmergenciaId, CasoId, ProveedorId, Observaciones, PresionArterial, Pulso, Temperatura, 
                    FrecuenciaCardiaca, DiagnosticoPresuntivo, EnfermedadId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert new Emergencia", ex);
                throw;
            }
            return (EmergenciaId).Value;
        }

        public static bool UpdateEmergencia(int EmergenciaId, int ProveedorId, string Observaciones, string PresionArterial, 
            string Pulso, string Temperatura, string FrecuenciaCardiaca, string DiagnosticoPresuntivo, string EnfermedadId)
        {
            if (EmergenciaId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");
            if (string.IsNullOrEmpty(DiagnosticoPresuntivo) && (string.IsNullOrEmpty(EnfermedadId)))
                throw new ArgumentException("DiagnosticoPresuntivo cannot be null or empty.");

            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                theAdapter.Update(EmergenciaId, ProveedorId, Observaciones, PresionArterial, Pulso, Temperatura, 
                    FrecuenciaCardiaca, DiagnosticoPresuntivo, EnfermedadId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Emergencia", ex);
                throw;
            }
        }
        public static bool DeleteEmergencia(int EmergenciaId)
        {
            if (EmergenciaId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");
            try
            {
                EmergenciaDSTableAdapters.EmergenciaTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaTableAdapter();
                theAdapter.Delete(EmergenciaId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Emergencia", ex);
                throw;
            }
        }
        public static int GetGastoIdEmergencia(int EmergenciaId)
        {
            Emergencia objEmergencia = getEmergenciaByEmergenciaId(EmergenciaId);
            return objEmergencia.GastoId;
        }

        public static List<DocumentFile> getFileList ( int EmergenciaId, bool IsVisible )
        {

            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                EmergenciaDSTableAdapters.EmergenciaFileTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaFileTableAdapter();
                EmergenciaDS.EmergenciaFileDataTable theTable = theAdapter.GetFile(EmergenciaId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EmergenciaDS.EmergenciaFileRow row in theTable.Rows)
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
                log.Error("An error was ocurred while geting Emergencia file list", ex);
                throw;
            }
            return theList;
        }
        public static void DeleteFile ( int FileId )
        {
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                EmergenciaDSTableAdapters.EmergenciaFileTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Emergencia File", ex);
                throw;
            }
        }
        public static void InsertFile ( int EmergenciaId, int FileId, bool IsVisible )
        {
            if (EmergenciaId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                EmergenciaDSTableAdapters.EmergenciaFileTableAdapter theAdapter = new EmergenciaDSTableAdapters.EmergenciaFileTableAdapter();
                theAdapter.InsertFile(FileId, EmergenciaId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Emergencia File", ex);
                throw;
            }
        }
    }
}