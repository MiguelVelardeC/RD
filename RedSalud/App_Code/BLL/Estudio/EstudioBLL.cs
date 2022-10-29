using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Caso;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Estudio.BLL
{
    /// <summary>
    /// Summary description for EstudioBLL
    /// </summary>
    public class EstudioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EstudioBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Estudio FillRecord(EstudioDS.EstudioRow row)
        {
            Estudio objEstudio = new Estudio(
                row.EstudioId
                ,row.CasoId
                ,row.ProveedorId
                ,row.TipoEstudioId
                ,row.Observacion
                ,row.IsGastoIdNull()? 0:row.GastoId
                ,row.IsAprobacionUserIdNull()? 0:row.AprobacionUserId
                ,row.IsAprobacionFechaNull() ? DateTime.MaxValue : Configuration.Configuration.ConvertToClientTimeZone(row.AprobacionFecha)
                ,row.IsFechaCreacionNull() ? DateTime.MinValue : Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                ,row.Cubierto
                ,row.Nombre
                ,row.NombreProveedor

                ,row.MontoConFactura
                ,row.MontoSinFactura
                ,row.RetencionImpuestos
                ,row.Total
                );

            if (!row.IsFileCountNull())
            {
                objEstudio.FileCount = row.FileCount;
            }

            return objEstudio;
        }

        private static Estudio FillRecord ( EstudioDS.EstudioForPrintRow row, int index )
        {
            Estudio objEstudio = new Estudio(
                row.EstudioId
                , row.TipoEstudioId
                , row.Nombre
                );

            return objEstudio;
        }
        
        public static List<Estudio> getEstudioListByCasoId ( int CasoId )
        {
            return getEstudioListByCasoId(CasoId, false);
        }

        public static List<Estudio> getEstudioListByCasoId(int CasoId, bool isFileVisible)
        {
            List<Estudio> theList = new List<Estudio>();
            Estudio theEstudio = null;
            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                EstudioDS.EstudioDataTable theTable = theAdapter.GetEstudioByCasoId(CasoId, isFileVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EstudioDS.EstudioRow row in theTable.Rows)
                    {
                        theEstudio = FillRecord(row);
                        theList.Add(theEstudio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Estudio by CasoId", ex);
                throw;
            }
            return theList;
        }

        public static Estudio getEstudioByEstudioId(int EstudioId)
        {
            if (EstudioId<=0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");
            Estudio theEstudio = null;
            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                EstudioDS.EstudioDataTable theTable = theAdapter.GetEstudioByEstudioId(EstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                        theEstudio = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Estudio by EstudioId", ex);
                throw;
            }
            return theEstudio;
        }
        
        public static int InsertEstudio(int CasoId,int ProveedorId, string TipoEstudioId
            , string Observaciones, DateTime FechaCreacion)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoEstudioId))
                throw new ArgumentException("TipoEstudioId cannot be null or empty.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");

            int? EstudioId = 0;

            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                theAdapter.Insert(ref EstudioId, CasoId, ProveedorId, TipoEstudioId, Observaciones,
                    Configuration.Configuration.ConvertToUTCFromServerTimeZone(FechaCreacion));
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert new Estudio", ex);
                throw;
            }
            return (EstudioId).Value;
        }

        public static bool UpdateEstudio(int EstudioId, int ProveedorId, string TipoEstudioId, string Observaciones)
        {
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoEstudioId))
                throw new ArgumentException("TipoEstudioId cannot be null or empty.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");

            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                theAdapter.Update(EstudioId, ProveedorId, TipoEstudioId, Observaciones);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Estudio", ex);
                throw;
            }
        }
        public static bool DeleteEstudio(int EstudioId)
        {
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");
            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                theAdapter.Delete(EstudioId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Estudio", ex);
                throw;
            }
        }
        public static bool DeleteEstudio(Estudio objEstudio)
        {
            if (objEstudio.EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");
            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                theAdapter.Delete(objEstudio.EstudioId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Estudio", ex);
                throw;
            }
        }

        public static bool AproveEstudio(int EstudioId, int AprobacionUserId, DateTime FechaAprobacion)
        {
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");
            if (AprobacionUserId <= 0)
                throw new ArgumentException("AprobacionUserId cannot be less than or equal to zero.");

            try
            {
                EstudioDSTableAdapters.EstudioTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioTableAdapter();
                theAdapter.ApproveEstudio(EstudioId, AprobacionUserId, 
                    Configuration.Configuration.ConvertToUTCFromClientTimeZone(FechaAprobacion));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while aprove Estudio", ex);
                throw;
            }
        }
        public static int GetGastoIdEstudio(int EstudioId)
        {
            Estudio ObjEstudio = getEstudioByEstudioId(EstudioId);
            return ObjEstudio.GastoId;
        }

        public static List<DocumentFile> getFileList ( int EstudioId, bool IsVisible )
        {

            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                EstudioDSTableAdapters.EstudioFileTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioFileTableAdapter();
                EstudioDS.EstudioFileDataTable theTable = theAdapter.GetFile(EstudioId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EstudioDS.EstudioFileRow row in theTable.Rows)
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
                log.Error("An error was ocurred while geting Receta file list", ex);
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
                EstudioDSTableAdapters.EstudioFileTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Receta File", ex);
                throw;
            }
        }
        public static void InsertFile ( int EstudioId, int FileId, bool IsVisible )
        {
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                EstudioDSTableAdapters.EstudioFileTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioFileTableAdapter();
                theAdapter.InsertFile(FileId, EstudioId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Estudio File", ex);
                throw;
            }
        }

        public static List<Estudio> GetEstudioByCasoIdForPrint ( int EstudioId )
        {
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");

            List<Estudio> theList = new List<Estudio>();
            try
            {
                EstudioDSTableAdapters.EstudioForPrintTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioForPrintTableAdapter();
                EstudioDS.EstudioForPrintDataTable theTable = theAdapter.GetEstudioByCasoIdForPrint(EstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int c = 1;
                    foreach (EstudioDS.EstudioForPrintRow row in theTable.Rows)
                    {
                        Estudio theEstudio = theEstudio = FillRecord(row, c++);
                        theList.Add(theEstudio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Estudio by CasoId for print", ex);
                throw;
            }
            return theList;
        }

        public static PrintInfo GetEstudioHeadByCasoIdForPrint ( int EstudioId )
        {
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");

            try
            {
                PrintInfo info = null;
                EstudioDSTableAdapters.EstudioHeadForPrintTableAdapter theAdapter = new EstudioDSTableAdapters.EstudioHeadForPrintTableAdapter();
                EstudioDS.EstudioHeadForPrintDataTable theTable = theAdapter.GetEstudioHeadByCasoIdForPrint(EstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    EstudioDS.EstudioHeadForPrintRow row = theTable[0];
                    info = new PrintInfo(row.NombrePaciente, row.NumeroPoliza, row.NombrePlan, row.IsTelefonoNull() ? "" : row.Telefono,
                         row.IsCarnetIdentidadNull() ? "" : row.CarnetIdentidad, row.NombreProveedor, row.MedicoNombre, row.Especialidad);
                    info.otros = row.Observacion;
                }
                return info;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Estudio by CasoId for print", ex);
                throw;
            }
        }
    }
}