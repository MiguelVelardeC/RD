using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.CLAMedicamento.BLL
{
    public class CLAMedicamentoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CLAMedicamentoBLL()
        {
        }

        private static Medicamento FillRecord(MedicamentoDS.MedicamentoRow row)
        {
            Medicamento objMedicamento=new Medicamento(
                row.MedicamentoId
                ,row.Nombre);
            return objMedicamento;
        }

        public static Medicamento GetMedicamentoDetails(int MedicamentoId)
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be <= 0.");

            Medicamento TheMedicamento = null;
            try
            {
                MedicamentoDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentoDSTableAdapters.MedicamentoTableAdapter();
                MedicamentoDS.MedicamentoDataTable theTable = theAdapter.GetMedicamentoByID(MedicamentoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    MedicamentoDS.MedicamentoRow row = theTable[0];
                    TheMedicamento = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Medicamento data", ex);
                throw;
            }
            return TheMedicamento;
        }

        public static List<Medicamento> getMedicamentoList ()
        {
            return getMedicamentoList("");
        }

        public static List<Medicamento> getMedicamentoList ( string search )
        {
            List<Medicamento> theList = new List<Medicamento>();
            Medicamento theMedicamento = null;
            try
            {
                MedicamentoDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentoDSTableAdapters.MedicamentoTableAdapter();
                int? totalRow = 0;
                MedicamentoDS.MedicamentoDataTable theTable = theAdapter.SearchMedicamento(SqlInt32.MaxValue.Value, 0, search, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (MedicamentoDS.MedicamentoRow row in theTable.Rows)
                    {
                        theMedicamento = FillRecord(row);
                        theList.Add(theMedicamento);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Medicamento", ex);
                throw;
            }
            return theList;
        }

        public static int SearchMedicamento ( ref List<Medicamento> _cache, int pageSize, int firstRow, string searchFilter )
        {
            try
            {
                MedicamentoDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentoDSTableAdapters.MedicamentoTableAdapter();
                int? totalRow = 0;
                MedicamentoDS.MedicamentoDataTable theTable = theAdapter.SearchMedicamento(pageSize, firstRow, searchFilter, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (MedicamentoDS.MedicamentoRow row in theTable.Rows)
                    {
                        Medicamento theMedicamento = FillRecord(row);
                        _cache.Add(theMedicamento);
                    }
                }
                return (int)totalRow;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting search Medicamento", ex);
                throw;
            }
        }

        public static int InsertMedicamento(string Nombre)
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                MedicamentoDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentoDSTableAdapters.MedicamentoTableAdapter();
                int? MedicamentoId = 0;
                theAdapter.Insert(ref MedicamentoId, Nombre);

                return (int)MedicamentoId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Medicamento", ex);
                throw;
            }
        }

        public static bool UpdateMedicamento(int MedicamentoId, string Nombre)
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be <= 0.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                MedicamentoDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentoDSTableAdapters.MedicamentoTableAdapter();
                theAdapter.Update(MedicamentoId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Medicamento", ex);
                throw;
            }
        }

        public static bool DeleteMedicamento ( int MedicamentoId )
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be <= 0.");

            try
            {
                MedicamentoDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentoDSTableAdapters.MedicamentoTableAdapter();
                theAdapter.Delete(MedicamentoId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Medicamento", ex);
                throw;
            }
        }
    }
}