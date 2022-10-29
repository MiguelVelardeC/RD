using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Caso;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Receta.BLL
{
    /// <summary>
    /// Summary description for RecetaBLL
    /// </summary>
    public class RecetaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RecetaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Receta FillRecord(RecetaDS.RecetaRow row)
        {
            return FillRecord(row, 0);
        }
        private static Receta FillRecord(RecetaDS.RecetaRow row, int index)
        {
            Receta objReceta = new Receta(
                row.DetalleId
                , row.CasoId
                , row.Medicamento
                , row.MedicamentoClaId
                , row.Grupo
                , row.Subgrupo
                , row.TipoMedicamentoId
                , row.TipoMedicamento
                , row.TipoConcentracionId
                , row.TipoConcentracion
                , row.Indicaciones
                , Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.UnPrincipioActivo
                , row.IsGastoIdNull() ? 0 : row.GastoId
                , row.MontoConFactura
                , row.MontoSinFactura
                , row.RetencionImpuestos
                , row.Total
                , row.ProveedorId
                , row.cantidad);

            if (!row.IsFileCountNull())
            {
                objReceta.FileCount = row.FileCount;
            }

            return objReceta;
        }

        public static int InsertReceta(int CasoId, string Medicamento, int MedicamentoClaId
            , string TipoMedicamentoId, int TipoConcentracionId, string Indicaciones, DateTime FechaCreacion, int proveedorId, int cantidad)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (proveedorId <= 0)
                throw new ArgumentException("proveedorId cannot be less than or equal to zero.");
            if (cantidad <= 0)
                throw new ArgumentException("cantidad cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Medicamento) && MedicamentoClaId <= 0)
                throw new ArgumentException("Medicamento cannot be null or empty.");
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Medicamento) && TipoConcentracionId <= 0)
                throw new ArgumentException("TipoConcentracionId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Indicaciones))
                throw new ArgumentException("Indicaciones cannot be null or empty.");

            int? DetalleId = 0;

            try
            {
                RecetaDSTableAdapters.RecetaTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaTableAdapter();

                theAdapter.Insert(ref DetalleId, CasoId, Medicamento, MedicamentoClaId
                    , TipoMedicamentoId, TipoConcentracionId, Indicaciones,
                    Configuration.Configuration.ConvertToUTCFromServerTimeZone(FechaCreacion), proveedorId, cantidad);

                if (DetalleId == null || DetalleId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                return (int)DetalleId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso", q);
                throw;
            }
        }

        public static bool UpdateReceta(int DetalleId, string Medicamento, int MedicamentoClaId
            , string TipoMedicamentoId, int TipoConcentracionId, string Indicaciones, int proveedorId, int cantidad)
        {
            if (DetalleId <= 0)
                throw new ArgumentException("DetalleId cannot be less than or equal to zero.");
            if (proveedorId <= 0)
                throw new ArgumentException("proveedorId cannot be less than or equal to zero.");
            if (cantidad <= 0)
                throw new ArgumentException("cantidad cannot be less than or equal to zero.");
            //if (CasoId <= 0)
            //    throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Medicamento))
                throw new ArgumentException("Medicamento cannot be null or empty.");
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Indicaciones))
                throw new ArgumentException("Indicaciones cannot be null or empty.");

            try
            {
                RecetaDSTableAdapters.RecetaTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaTableAdapter();

                theAdapter.Update(DetalleId, Medicamento, MedicamentoClaId
                    , TipoMedicamentoId, TipoConcentracionId, Indicaciones, proveedorId, cantidad);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Receta", q);
                throw;
            }
            return true;
        }

        public static Receta GetRecetaByDetalleId(int DetalleId)
        {
            if (DetalleId <= 0)
                throw new ArgumentException("DetalleId cannot be less than or equal to zero.");

            Receta objReceta = null;
            try
            {
                RecetaDSTableAdapters.RecetaTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaTableAdapter();
                RecetaDS.RecetaDataTable theTable = theAdapter.GetRecetaByDetalleId(DetalleId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objReceta = FillRecord(theTable[0]);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting RecetaDetails", q);
                throw;
            }
            return objReceta;
        }

        public static List<Receta> GetAllRecetaByCasoId(int CasoId)
        {
            return GetAllRecetaByCasoId(CasoId, false);
        }

        public static List<Receta> GetAllRecetaByCasoId(int CasoId, bool isFileVisible)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            List<Receta> theList = new List<Receta>();
            Receta objReceta = null;
            try
            {
                RecetaDSTableAdapters.RecetaTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaTableAdapter();
                RecetaDS.RecetaDataTable theTable = theAdapter.GetAllRecetaByCasoId(CasoId, isFileVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    int index = 0;
                    foreach (RecetaDS.RecetaRow row in theTable.Rows)
                    {
                        objReceta = FillRecord(row, index);
                        theList.Add(objReceta);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting RecetaDetails", q);
                throw;
            }
            return theList;
        }

        public static bool DeleteReceta(int DetalleId)
        {
            if (DetalleId <= 0)
                throw new ArgumentException("DetalleId the receta cannot be less than or equal to zero.");
            try
            {
                RecetaDSTableAdapters.RecetaTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaTableAdapter();
                theAdapter.Delete(DetalleId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Receta", ex);
                throw;
            }
        }

        public static int GetGastoIdReceta(int RecetaId)
        {
            Receta ObjReceta = GetRecetaByDetalleId(RecetaId);
            return ObjReceta.GastoId;
        }

        public static List<DocumentFile> getFileList(int RecetaId, bool IsVisible)
        {
            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                RecetaDSTableAdapters.RecetaFileTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaFileTableAdapter();
                RecetaDS.RecetaFileDataTable theTable = theAdapter.GetFile(RecetaId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RecetaDS.RecetaFileRow row in theTable.Rows)
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
        public static void DeleteFile(int FileId)
        {
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                RecetaDSTableAdapters.RecetaFileTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Receta File", ex);
                throw;
            }
        }
        public static void InsertFile(int RecetaId, int FileId, bool IsVisible)
        {
            if (RecetaId <= 0)
                throw new ArgumentException("RecetaId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                RecetaDSTableAdapters.RecetaFileTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaFileTableAdapter();
                theAdapter.InsertFile(FileId, RecetaId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Receta File", ex);
                throw;
            }
        }

        public static PrintInfo GetRecetaHeadByCasoIdForPrint(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            try
            {
                PrintInfo info = null;
                RecetaDSTableAdapters.RecetaHeadForPrintTableAdapter theAdapter = new RecetaDSTableAdapters.RecetaHeadForPrintTableAdapter();
                RecetaDS.RecetaHeadForPrintDataTable theTable = theAdapter.GetRecetaHeadByCasoIdForPrint(CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    RecetaDS.RecetaHeadForPrintRow row = theTable[0];
                    info = new PrintInfo(row.NombrePaciente, row.NumeroPoliza, row.IsNombrePlanNull() ? "" : row.NombrePlan, row.IsTelefonoNull() ? "" : row.Telefono,
                        row.IsCarnetIdentidadNull() ? "" : row.CarnetIdentidad, row.IsNombreProveedorNull() ? "" : row.NombreProveedor, row.MedicoNombre, row.Especialidad);
                }
                return info;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Receta by CasoId for print", ex);
                throw;
            }
        }
    }
}