using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for LaboratorioFileBLL
    /// </summary>
    public class LaboratorioFileBLL
    {
        public LaboratorioFileBLL()
        {
            
        }

        public static List<DocumentFile> GetLaboratorioFiles(int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter adapter = new LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter();
            LaboratorioFileDS.EstudioFilesDataTable table = adapter.GetEstudioFiles(citaDesgravamenId);

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

        public static List<DocumentFile> GetLaboratorioFiles(int citaDesgravamenId, int proveedorMedicoId, int estudioId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter adapter = new LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter();
            LaboratorioFileDS.EstudioFilesDataTable table = adapter.GetEstudioFilesByProveedorMedico(citaDesgravamenId, proveedorMedicoId, estudioId);

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

        public static void InsertLaboratorioFile(int citaDesgravamenId, int fileId, int proveedorMedicoId, int estudioId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            if (fileId <= 0)
            {
                throw new ArgumentException("fileId cannot be equals or less than zero");
            }

            if (proveedorMedicoId <= 0)
            {
                throw new ArgumentException("proveedorMedicoId cannot be equals or less than zero");
            }

            LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter adapter = new LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter();
            adapter.InsertEstudioFile(fileId, citaDesgravamenId, proveedorMedicoId, estudioId);

        }

        public static void DeleteLaboratorioFile(int citaDesgravamenId, int fileId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            if (fileId <= 0)
            {
                throw new ArgumentException("fileId cannot be equals or less than zero");
            }

            LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter adapter = new LaboratorioFileDSTableAdapters.EstudioFilesTableAdapter();
            adapter.DeleteEstudioFile(fileId, citaDesgravamenId);

        }
    }
}