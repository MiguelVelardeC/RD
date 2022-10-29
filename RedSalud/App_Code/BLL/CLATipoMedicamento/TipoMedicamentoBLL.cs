using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.TipoMedicamento.BLL
{
    /// <summary>
    /// Summary description for TipoMedicamentoBLL
    /// </summary>
    public class TipoMedicamentoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public TipoMedicamentoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static TipoMedicamento FillRecord(TipoMedicamentoDS.TipoMedicamentoRow row)
        {
            TipoMedicamento objTipoMed = new TipoMedicamento(
                    row.TipoMedicamentoId,
                    row.Nombre);
            return objTipoMed;
        }

        public static List<TipoMedicamento> GetTipoMedicamentoList()
        {
            List<TipoMedicamento> theList = new List<TipoMedicamento>();
            TipoMedicamento objTipoMed = null;

            try
            {
                TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter theAdapter = new TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter();
                TipoMedicamentoDS.TipoMedicamentoDataTable theTable = theAdapter.GetAllTipoMedicamento();
                if (theTable != null && theTable.Rows.Count > 0)
                foreach(TipoMedicamentoDS.TipoMedicamentoRow row in theTable.Rows)
                {
                    objTipoMed = FillRecord(row);
                    theList.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoMedicamento", ex);
                throw;
            }
            return theList;
        }

        public static TipoMedicamento GetTipoMedicamentoDetails( string TipoMedicamentoId)
        {
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");

            TipoMedicamento objTipoMed = null;

            try
            {
                TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter theAdapter = new TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter();
                TipoMedicamentoDS.TipoMedicamentoDataTable theTable = theAdapter.GetTipoMedicamentoById(TipoMedicamentoId);
                if (theTable != null && theTable.Rows.Count > 0)
                    objTipoMed = FillRecord(theTable[0]);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting TipoMedicamento Details", ex);
                throw;
            }
            return objTipoMed;
        }

        public static List<TipoMedicamento> GetTipoMedicamentoByMedicamentoClaId ( int MedicamentoClaId )
        {
            if (MedicamentoClaId <= 0)
                throw new ArgumentException("MedicamentoClaId cannot be null or empty.");

            List<TipoMedicamento> listTipoMed = new List<TipoMedicamento>();

            try
            {
                TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter theAdapter = new TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter();
                TipoMedicamentoDS.TipoMedicamentoDataTable theTable = theAdapter.GetTipoMedicamentoByMedicamentoClaId(MedicamentoClaId);
                foreach (TipoMedicamentoDS.TipoMedicamentoRow row in theTable.Rows)
                {
                    TipoMedicamento objTipoMed = FillRecord(row);
                    listTipoMed.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting TipoMedicamento", ex);
                throw;
            }
            return listTipoMed;
        }

        public static bool InsertTipoMedicamento(string TipoMedicamentoId, string Nombre)
        {
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");

            try
            {
                TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter theAdapter = new TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter();
                theAdapter.Insert(TipoMedicamentoId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting TipoMedicamento.", ex);
                throw;
            }
        }

        public static bool UpdateTipoMedicamento(string TipoMedicamentoId, string Nombre)
        {
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");
            try
            {
                TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter theAdapter = new TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter();
                theAdapter.Update(TipoMedicamentoId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating TipoMedicamento.", ex);
                throw;
            }
        }

        public static bool DeleteTipoMedicamento(string TipoMedicamentoId)
        {
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");

            try
            {
                TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter theAdapter = new TipoMedicamentoDSTableAdapters.TipoMedicamentoTableAdapter();
                theAdapter.Delete(TipoMedicamentoId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting TipoMedicamento.", ex);
                throw;
            }
        }
    }
}