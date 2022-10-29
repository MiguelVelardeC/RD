using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Caso;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Internacion.BLL
{
    /// <summary>
    /// Summary description for InternacionBLL
    /// </summary>
    public class InternacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        
        public InternacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Internacion FillRecord(InternacionDS.InternacionRow row)
        {
            Internacion objInternacion = new Internacion(
                row.InternacionId
                , row.CasoId
                , row.ProveedorId
                , row.Observacion
                , row.IsCodigoArancelarioIdNull() ? "" : row.CodigoArancelarioId
                , row.IsCodigoArancelarioNull() ? "" : row.CodigoArancelario
                , row.IsUMANull() ? 0 : row.UMA
                , row.IsGastoIdNull() ? 0 : row.GastoId
                , row.IsAprobacionUserIdNull() ? 0 : row.AprobacionUserId
                , row.IsAprobacionFechaNull() ? DateTime.MaxValue : Configuration.Configuration.ConvertToClientTimeZone(row.AprobacionFecha)
                , row.IsFechaCreacionNull() ? DateTime.MinValue : Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.NombreProveedor

                , row.MontoConFactura
                , row.MontoSinFactura
                , row.RetencionImpuestos
                , row.Total
                );

            if (!row.IsFileCountNull())
            {
                objInternacion.FileCount = row.FileCount;
            }

            return objInternacion;
        }

        public static List<Internacion> getInternacionListByCasoId ( int CasoId )
        {
            return getInternacionListByCasoId(CasoId, false);
        }

        public static List<Internacion> getInternacionListByCasoId(int CasoId, bool isFileVisible)
        {
            List<Internacion> theList = new List<Internacion>();
            Internacion theInternacion = null;
            try
            {
                InternacionDSTableAdapters.InternacionTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionTableAdapter();
                InternacionDS.InternacionDataTable theTable = theAdapter.GetInternacionByCasoId(CasoId, isFileVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (InternacionDS.InternacionRow row in theTable.Rows)
                    {
                        theInternacion = FillRecord(row);
                        theList.Add(theInternacion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Internacion by CasoId", ex);
                throw;
            }
            return theList;
        }

        public static Internacion getInternacionByInternacionId(int InternacionId)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");
            Internacion theInternacion = null;
            try
            {
                InternacionDSTableAdapters.InternacionTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionTableAdapter();
                InternacionDS.InternacionDataTable theTable = theAdapter.GetInternacionByInternacionId(InternacionId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theInternacion = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Internacion by InternacionId", ex);
                throw;
            }
            return theInternacion;
        }

        public static int InsertInternacion(int CasoId, int ProveedorId
            , string Observaciones, string CodigoArancelarioId, DateTime FechaCreacion)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");

            int? InternacionId = 0;

            try
            {
                InternacionDSTableAdapters.InternacionTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionTableAdapter();
                theAdapter.Insert(ref InternacionId, CasoId, ProveedorId, Observaciones, CodigoArancelarioId,
                    Configuration.Configuration.ConvertToUTCFromServerTimeZone(FechaCreacion));
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert new Internacion", ex);
                throw;
            }
            return (InternacionId).Value;
        }

        public static bool UpdateInternacion(int InternacionId, int ProveedorId, string Observaciones, string CodigoArancelarioId)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Observaciones))
                throw new ArgumentException("Observaciones cannot be null or empty.");

            try
            {
                InternacionDSTableAdapters.InternacionTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionTableAdapter();
                theAdapter.Update(InternacionId, ProveedorId, Observaciones, CodigoArancelarioId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Internacion", ex);
                throw;
            }
        }
        public static bool DeleteInternacion(int InternacionId)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");
            try
            {
                InternacionDSTableAdapters.InternacionTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionTableAdapter();
                theAdapter.Delete(InternacionId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Internacion", ex);
                throw;
            }
        }

        public static bool AproveInternacion(int InternacionId, int AprobacionUserId, DateTime FechaAprobacion)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");
            if (AprobacionUserId <= 0)
                throw new ArgumentException("AprobacionUserId cannot be less than or equal to zero.");

            try
            {
                InternacionDSTableAdapters.InternacionTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionTableAdapter();
                theAdapter.ApproveInternacion(InternacionId, AprobacionUserId, 
                    Configuration.Configuration.ConvertToUTCFromClientTimeZone(FechaAprobacion));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while aprove Internacion", ex);
                throw;
            }
        }

        public static int GetGastoIdInternacion(int InternacionId)
        {
            Internacion objInternacion = getInternacionByInternacionId(InternacionId);
            return objInternacion.GastoId;
        }

        public static List<DocumentFile> getFileList ( int InternacionId, bool IsVisible )
        {
            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                InternacionDSTableAdapters.InternacionFileTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionFileTableAdapter();
                InternacionDS.InternacionFileDataTable theTable = theAdapter.GetFiles(InternacionId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (InternacionDS.InternacionFileRow row in theTable.Rows)
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
                log.Error("An error was ocurred while geting Internacion file list", ex);
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
                InternacionDSTableAdapters.InternacionFileTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Internacion File", ex);
                throw;
            }
        }
        public static void InsertFile ( int InternacionId, int FileId, bool IsVisible )
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                InternacionDSTableAdapters.InternacionFileTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionFileTableAdapter();
                theAdapter.InsertFile(FileId, InternacionId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Internacion File", ex);
                throw;
            }
        }

        public static PrintInfo GetInternacionHeadByCasoIdForPrint ( string InternacionId )
        {
            if (string.IsNullOrEmpty(InternacionId))
                throw new ArgumentException("InternacionId cannot be null or empty.");

            try
            {
                PrintInfo info = null;
                InternacionDSTableAdapters.InternacionForPrintTableAdapter theAdapter = new InternacionDSTableAdapters.InternacionForPrintTableAdapter();
                InternacionDS.InternacionForPrintDataTable theTable = theAdapter.GetInternacionHeadByCasoIdForPrint(int.Parse(InternacionId));
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    InternacionDS.InternacionForPrintRow row = theTable[0];
                    info = new PrintInfo(row.NombrePaciente, row.NumeroPoliza, row.NombrePlan, row.IsTelefonoNull() ? "" : row.Telefono,
                         row.IsCarnetIdentidadNull() ? "" : row.CarnetIdentidad, "", row.NombreProveedor, row.Especialidad);
                    info.otros = row.Tipo;
                }
                return info;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Derivacion by CasoId for print", ex);
                throw;
            }
        }
    }
}