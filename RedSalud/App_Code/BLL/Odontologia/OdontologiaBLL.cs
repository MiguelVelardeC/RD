using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using log4net;
using Artexacta.App.Caso;

namespace Artexacta.App.Odontologia.BLL
{
    /// <summary>
    /// Summary description for OdontologiaBLL
    /// </summary>
    public class OdontologiaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public OdontologiaBLL () {}
        private static Odontologia FillRecord(OdontologiaDS.OdontologiaRow row)
        {
            Odontologia objTipoMed = new Odontologia(
                    row.OdontologiaId,
                    row.CasoId,
                    row.PrestacionOdontologicaId,
                    row.PrestacionOdontologica,
                    row.IsPiezaNull() ? "" : row.Pieza, 
                    row.Detalle, 
                    row.IsObservacionesNull() ? "" : row.Observaciones, 
                    Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion),
                    row.IsGastoIdNull() ? 0 : row.GastoId);

            if (!row.IsFileCountNull())
            {
                objTipoMed.FileCount = row.FileCount;
            }

            return objTipoMed;
        }

        public static int InsertOdontologia ( Odontologia theObj )
        {
            if (theObj.CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (theObj.PrestacionOdontologicaId <= 0)
                throw new ArgumentException("PrestacionOdontologicaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(theObj.Pieza))
                throw new ArgumentException("Pieza cannot be null or empty.");
            if (string.IsNullOrEmpty(theObj.Detalle))
                theObj.Detalle = "";

            int? OdontologiaId = 0;

            try
            {
                OdontologiaDSTableAdapters.OdontologiaTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaTableAdapter();
                theAdapter.Insert(ref OdontologiaId, theObj.CasoId, theObj.PrestacionOdontologicaId, theObj.Pieza, 
                    theObj.Detalle, theObj.Observaciones, theObj.GastoId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert new Odontologia", ex);
                throw;
            }
            return (OdontologiaId).Value;
        }

        public static bool UpdateOdontologia ( Odontologia theObj )
        {
            if (theObj.OdontologiaId <= 0)
                throw new ArgumentException("OdontologiaId cannot be less than or equal to zero.");
            if (theObj.CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (theObj.PrestacionOdontologicaId <= 0)
                throw new ArgumentException("PrestacionOdontologicaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(theObj.Pieza))
                throw new ArgumentException("Pieza cannot be null or empty.");
            if (string.IsNullOrEmpty(theObj.Detalle))
                throw new ArgumentException("Detalle cannot be null or empty.");

            try
            {
                OdontologiaDSTableAdapters.OdontologiaTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaTableAdapter();
                theAdapter.Update(theObj.OdontologiaId, theObj.PrestacionOdontologicaId, theObj.Pieza,
                    theObj.Detalle, theObj.Observaciones, theObj.GastoId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Odontologia", ex);
                throw;
            }
        }
        public static bool DeleteOdontologia ( int OdontologiaId )
        {
            if (OdontologiaId <= 0)
                throw new ArgumentException("OdontologiaId cannot be less than or equal to zero.");
            try
            {
                OdontologiaDSTableAdapters.OdontologiaTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaTableAdapter();
                theAdapter.Delete(OdontologiaId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Odontologia", ex);
                throw;
            }
        }


        public static Odontologia GetOdontologiaById ( int OdontologiaId )
        {
            return GetOdontologiaById(OdontologiaId, false);
        }
        public static Odontologia GetOdontologiaById ( int OdontologiaId, bool isFileVisible )
        {
            if (OdontologiaId <= 0)
                throw new ArgumentException("OdontologiaId cannot be null or empty.");

            Odontologia objTipoMed = new Odontologia();

            try
            {
                OdontologiaDSTableAdapters.OdontologiaTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaTableAdapter();
                OdontologiaDS.OdontologiaDataTable theTable = theAdapter.GetOdontologiaByID(OdontologiaId, isFileVisible);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objTipoMed = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Odontologia list", ex);
                throw;
            }
            return objTipoMed;
        }
        public static List<Odontologia> GetOdontologiaByCasoId ( int CasoId )
        {
            return GetOdontologiaByCasoId(CasoId, false);
        }

        public static List<Odontologia> GetOdontologiaByCasoId ( int CasoId, bool isFileVisible )
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId  cannot be null or empty.");

            List<Odontologia> listTipoMed = new List<Odontologia>();

            try
            {
                OdontologiaDSTableAdapters.OdontologiaTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaTableAdapter();
                OdontologiaDS.OdontologiaDataTable theTable = theAdapter.GetOdontologiaByCasoId(CasoId, isFileVisible);
                foreach (OdontologiaDS.OdontologiaRow row in theTable.Rows)
                {
                    Odontologia objTipoMed = FillRecord(row);
                    listTipoMed.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Odontologia list", ex);
                throw;
            }
            return listTipoMed;
        }

        public static PrintInfo GetOdontologiaHeadByCasoIdForPrint(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            try
            {
                PrintInfo info = null;
                OdontologiaDSTableAdapters.OdontologiaHeadForPrintTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaHeadForPrintTableAdapter();
                OdontologiaDS.OdontologiaHeadForPrintDataTable theTable = theAdapter.GetOdontologiaHeadByCasoIdForPrint(CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    OdontologiaDS.OdontologiaHeadForPrintRow row = theTable[0];
                    info = new PrintInfo(row.NombrePaciente, row.NumeroPoliza, row.NombrePlan, row.IsTelefonoNull() ? "" : row.Telefono,
                        row.IsCarnetIdentidadNull() ? "" : row.CarnetIdentidad, "", row.MedicoNombre, row.Especialidad);
                }
                return info;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Odontologia by CasoId for print", ex);
                throw;
            }
        }

        public static List<DocumentFile> getFileList ( int OdontologiaId, bool IsVisible )
        {

            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                OdontologiaDSTableAdapters.OdontologiaFileTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaFileTableAdapter();
                OdontologiaDS.OdontologiaFileDataTable theTable = theAdapter.GetFile(OdontologiaId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (OdontologiaDS.OdontologiaFileRow row in theTable.Rows)
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
                log.Error("An error was ocurred while geting Odontologia file list", ex);
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
                OdontologiaDSTableAdapters.OdontologiaFileTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Odontologia File", ex);
                throw;
            }
        }
        public static void InsertFile ( int OdontologiaId, int FileId, bool IsVisible )
        {
            if (OdontologiaId <= 0)
                throw new ArgumentException("OdontologiaId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                OdontologiaDSTableAdapters.OdontologiaFileTableAdapter theAdapter = new OdontologiaDSTableAdapters.OdontologiaFileTableAdapter();
                theAdapter.Insert(FileId, OdontologiaId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Odontologia File", ex);
                throw;
            }
        }
    }
}