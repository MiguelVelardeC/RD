using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.EnfermedadCronica.BLL
{
    public class EnfermedadCronicaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EnfermedadCronicaBLL() { }

        private static EnfermedadCronica FillRecord(EnfermedadCronicaDS.EnfermedadCronicaRow row)
        {
            EnfermedadCronica objEnfermedadCronica = new EnfermedadCronica(
                row.EnfermedadCronicaId
                , row.Nombre);

            if (!row.IsRowNumberNull())
            {
                objEnfermedadCronica.RowNumber = row.RowNumber;
            }
            return objEnfermedadCronica;
        }

        public static EnfermedadCronica GetEnfermedadCronicaByID(int EnfermedadCronicaId)
        {
            if (EnfermedadCronicaId <= 0)
                throw new ArgumentException("EnfermedadCronicaId cannot be <= 0.");

            EnfermedadCronica TheEnfermedadCronica = null;
            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                EnfermedadCronicaDS.EnfermedadCronicaDataTable theTable = theAdapter.GetEnfermedadCronicaByID(EnfermedadCronicaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    EnfermedadCronicaDS.EnfermedadCronicaRow row = theTable[0];
                    TheEnfermedadCronica = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting EnfermedadCronica data", ex);
                throw;
            }
            return TheEnfermedadCronica;
        }

        public static List<EnfermedadCronica> GetEnfermedadCronicaByAseguradoId(int AseguradoId)
        {
            if (AseguradoId <= 0)
                throw new ArgumentException("AseguradoId cannot be <= 0.");

            List<EnfermedadCronica> TheList = new List<EnfermedadCronica>();
            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                EnfermedadCronicaDS.EnfermedadCronicaDataTable theTable = theAdapter.GetEnfermedadCronicaByAseguradoId(AseguradoId);
                foreach ( EnfermedadCronicaDS.EnfermedadCronicaRow row in theTable.Rows)
                {
                    TheList.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting EnfermedadCronica data", ex);
                throw;
            }
            return TheList;
        }

        public static List<EnfermedadCronica> GetEnfermedadCronicaByPacienteId(int AseguradoId)
        {
            if (AseguradoId <= 0)
                throw new ArgumentException("AseguradoId cannot be <= 0.");

            List<EnfermedadCronica> TheList = new List<EnfermedadCronica>();
            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                EnfermedadCronicaDS.EnfermedadCronicaDataTable theTable = theAdapter.GetEnfermedadCronicaByPacienteId(AseguradoId);
                foreach (EnfermedadCronicaDS.EnfermedadCronicaRow row in theTable.Rows)
                {
                    TheList.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting EnfermedadCronica data", ex);
                throw;
            }
            return TheList;
        }

        public static List<EnfermedadCronica> getEnfermedadCronicaList(string search)
        {
            List<EnfermedadCronica> theList = new List<EnfermedadCronica>();
            EnfermedadCronica theEnfermedadCronica = null;
            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                int? totalRow = 0;
                EnfermedadCronicaDS.EnfermedadCronicaDataTable theTable = theAdapter.SearchEnfermedadCronica(SqlInt32.MaxValue.Value, 0, search, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EnfermedadCronicaDS.EnfermedadCronicaRow row in theTable.Rows)
                    {
                        theEnfermedadCronica = FillRecord(row);
                        theList.Add(theEnfermedadCronica);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list EnfermedadCronica", ex);
                throw;
            }
            return theList;
        }

        public static List<EnfermedadCronica> getEnfermedadCronicaList()
        {
            return getEnfermedadCronicaList("");
        }

        public static int SearchEnfermedadCronica(ref List<EnfermedadCronica> _cache, int pageSize, int firstRow, string searchFilter)
        {
            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                int? totalRow = 0;
                EnfermedadCronicaDS.EnfermedadCronicaDataTable theTable = theAdapter.SearchEnfermedadCronica(pageSize, firstRow, searchFilter, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EnfermedadCronicaDS.EnfermedadCronicaRow row in theTable.Rows)
                    {
                        EnfermedadCronica theEnfermedadCronica = FillRecord(row);
                        _cache.Add(theEnfermedadCronica);
                    }
                }
                return (int)totalRow;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting search EnfermedadCronica", ex);
                throw;
            }
        }

        public static void InsertEnfermedadCronicaAsegurado(int AseguradoId, int EnfermedadCronicaId)
        {
            if (AseguradoId <= 0)
                throw new ArgumentException("AseguradoId cannot be <= 0.");
            if (EnfermedadCronicaId <= 0)
                throw new ArgumentException("EnfermedadCronicaId cannot be <= 0.");

            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                theAdapter.InsertEnfermedadCronicaAsegurado(AseguradoId, EnfermedadCronicaId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting EnfermedadCronica", ex);
                throw;
            }
        }

        public static void DeleteEnfermedadCronicaAsegurado(int AseguradoId, int EnfermedadCronicaId)
        {
            if (AseguradoId <= 0)
                throw new ArgumentException("AseguradoId cannot be <= 0.");
            if (EnfermedadCronicaId <= 0)
                throw new ArgumentException("EnfermedadCronicaId cannot be <= 0.");
            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                theAdapter.DeleteEnfermedadCronicaAsegurado(AseguradoId, EnfermedadCronicaId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting EnfermedadCronica", ex);
                throw;
            }
        }

        public static void InsertEnfermedadCronica(string Nombre)
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                int? EnfermedadCronicaId = 0;
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                theAdapter.Insert(ref EnfermedadCronicaId, Nombre);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting EnfermedadCronica", ex);
                throw;
            }
        }

        public static void UpdateEnfermedadCronica(int EnfermedadCronicaId, string Nombre)
        {
            if (EnfermedadCronicaId <= 0)
                throw new ArgumentException("EnfermedadCronicaId cannot be <= 0.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                theAdapter.Update(EnfermedadCronicaId, Nombre);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating EnfermedadCronica", ex);
                throw;
            }
        }

        public static void DeleteEnfermedadCronica(int EnfermedadCronicaId)
        {
            if (EnfermedadCronicaId <= 0)
                throw new ArgumentException("EnfermedadCronicaId cannot be <= 0.");

            try
            {
                EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter theAdapter = new EnfermedadCronicaDSTableAdapters.EnfermedadCronicaTableAdapter();
                theAdapter.Delete(EnfermedadCronicaId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting EnfermedadCronica", ex);
                throw;
            }
        }
    }
}