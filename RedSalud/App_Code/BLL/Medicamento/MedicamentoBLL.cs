using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Medicamento.BLL
{
    public class MedicamentoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public MedicamentoBLL() {}

        private static Medicamento FillRecord(MedicamentosDS.MedicamentoRow row)
        {
            Medicamento objMedicamento = new Medicamento(
                row.MedicamentoId
                ,row.CasoId
                ,row.MedicamentoClaId
                ,row.Medicamento
                ,row.TipoMedicamentoId
                ,row.TipoMedicamento
                ,row.Indicaciones
                ,Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                ,row.IsGastoIdNull() ? 0 : row.GastoId
                
                ,row.MontoConFactura
                ,row.MontoSinFactura
                ,row.RetencionImpuestos
                , row.Total
                , row.IsFileCountNull() ? 0 : row.FileCount);

            return objMedicamento;
        }

        public static int InsertMedicamento(int CasoId, int MedicamentoCLAId, string TipoMedicamentoId
            , string Indicaciones, DateTime FechaCreacion)
        {
            if(CasoId<=0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (MedicamentoCLAId<=0)
                throw new ArgumentException("MedicamentoCLAId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Indicaciones))
                throw new ArgumentException("Indicaciones cannot be null or empty.");

            int? DetalleId = 0;
            
            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();

                theAdapter.Insert(ref DetalleId, CasoId, MedicamentoCLAId
                    ,TipoMedicamentoId,Indicaciones,
                    Configuration.Configuration.ConvertToUTCFromClientTimeZone(FechaCreacion));

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

        public static bool UpdateMedicamento ( int MedicamentoId, int MedicamentoCLAId
            , string TipoMedicamentoId, string Indicaciones)
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be less than or equal to zero.");
            if (MedicamentoCLAId <= 0)
                throw new ArgumentException("MedicamentoCLAId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Indicaciones))
                throw new ArgumentException("Indicaciones cannot be null or empty.");

            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();

                theAdapter.Update(MedicamentoId, MedicamentoCLAId, TipoMedicamentoId, Indicaciones);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Medicamento", q);
                throw;
            }
            return true;
        }

        public static Medicamento GetMedicamentoById ( int MedicamentoId )
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be less than or equal to zero.");

            Medicamento objMedicamento = null;
            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();
                MedicamentosDS.MedicamentoDataTable theTable = theAdapter.GetMedicamentoByID(MedicamentoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objMedicamento = FillRecord(theTable[0]);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting MedicamentoDetails", q);
                throw;
            }
            return objMedicamento;
        }

        public static List<Medicamento> GetAllMedicamentoByCasoId ( int CasoId )
        {
            return GetAllMedicamentoByCasoId(CasoId, false);
        }
        public static List<Medicamento> GetAllMedicamentoByCasoId(int CasoId, bool isFileVisible)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            List<Medicamento> theList = new List<Medicamento>();
            Medicamento objMedicamento = null;
            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();
                MedicamentosDS.MedicamentoDataTable theTable = theAdapter.GetAllMedicamentoByCasoId(CasoId, isFileVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach(MedicamentosDS.MedicamentoRow row in theTable.Rows)
                    {
                        objMedicamento = FillRecord(row);
                        theList.Add(objMedicamento);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting MedicamentoDetails", q);
                throw;
            }
            return theList;
        }

        public static bool DeleteMedicamento(int DetalleId)
        {
            if (DetalleId <= 0)
                throw new ArgumentException("DetalleId the Medicamento cannot be less than or equal to zero.");
            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();
                theAdapter.Delete(DetalleId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Medicamento", ex);
                throw;
            }
        }

        public static int GetGastoIdMedicamento(int MedicamentoId)
        {
            Medicamento ObjMedicamento = GetMedicamentoById(MedicamentoId);
            return ObjMedicamento.GastoId;
        }

        public static List<DocumentFile> getFileList ( int MedicamentoId, bool IsVisible )
        {
            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                MedicamentosDSTableAdapters.MedicamentoFileTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoFileTableAdapter();
                MedicamentosDS.MedicamentoFileDataTable theTable = theAdapter.GetFile(MedicamentoId, IsVisible);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (MedicamentosDS.MedicamentoFileRow row in theTable.Rows)
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
                log.Error("An error was ocurred while geting Medicamento file list", ex);
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
                MedicamentosDSTableAdapters.MedicamentoFileTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Medicamento File", ex);
                throw;
            }
        }
        public static void InsertFile ( int MedicamentoId, int FileId, bool IsVisible )
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                MedicamentosDSTableAdapters.MedicamentoFileTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoFileTableAdapter();
                theAdapter.Insert(FileId, MedicamentoId, IsVisible);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Medicamento File", ex);
                throw;
            }
        }
    }
}