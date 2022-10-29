using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.CodigoArancelario.BLL
{
    public class CodigoArancelarioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CodigoArancelarioBLL(){}

        private static CodigoArancelario FillRecord(CodigoArancelarioDS.CodigoArancelarioRow row)
        {
            CodigoArancelario objCodigoArancelario=new CodigoArancelario(
                 row.CodigoArancelarioId
                ,row.Nombre
                ,row.UMA);
            return objCodigoArancelario;
        }

        public static CodigoArancelario GetCodigoArancelarioByID ( string CodigoArancelarioId )
        {
            if (string.IsNullOrEmpty(CodigoArancelarioId))
                throw new ArgumentException("CodigoArancelarioId cannot null or empty.");

            CodigoArancelario TheCodigoArancelario = null;
            try
            {
                CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter theAdapter = new CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter();
                CodigoArancelarioDS.CodigoArancelarioDataTable theTable = theAdapter.GetCodigoArancelarioByID(CodigoArancelarioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    CodigoArancelarioDS.CodigoArancelarioRow row = theTable[0];
                    TheCodigoArancelario = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting CodigoArancelario data", ex);
                throw;
            }
            return TheCodigoArancelario;
        }

        public static int SearchCodigoArancelario (ref List<CodigoArancelario> _cache, int pageSize, int firstRow, string searchFilter)
        {
            try
            {
                CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter theAdapter = new CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter();
                int? totalRow = 0;
                CodigoArancelarioDS.CodigoArancelarioDataTable theTable = theAdapter.SearchCodigoArancelario(pageSize, firstRow, searchFilter, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CodigoArancelarioDS.CodigoArancelarioRow row in theTable.Rows)
                    {
                        CodigoArancelario theCodigoArancelario = FillRecord(row);
                        _cache.Add(theCodigoArancelario);
                    }
                }
                return (int)totalRow;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting search CodigoArancelario", ex);
                throw;
            }
        }

        public static void InsertCodigoArancelario(string CodigoArancelarioId, string Nombre, decimal uma)
        {
            if (string.IsNullOrEmpty(CodigoArancelarioId))
                throw new ArgumentException("CodigoArancelarioId cannot null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");
            if (uma <= 0)
                throw new ArgumentException("uma cannot be <= 0.");

            try
            {
                CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter theAdapter = new CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter();
                theAdapter.Insert(CodigoArancelarioId, Nombre, uma);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting CodigoArancelario", ex);
                throw;
            }
        }

        public static bool UpdateCodigoArancelario(string CodigoArancelarioId, string Nombre, decimal uma)
        {
            if (string.IsNullOrEmpty(CodigoArancelarioId))
                throw new ArgumentException("CodigoArancelarioId cannot null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");
            if (uma <= 0)
                throw new ArgumentException("uma cannot be <= 0.");

            try
            {
                CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter theAdapter = new CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter();
                theAdapter.Update(CodigoArancelarioId, Nombre, uma);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating CodigoArancelario", ex);
                throw;
            }
        }

        public static bool DeleteCodigoArancelario(string CodigoArancelarioId)
        {
            if (string.IsNullOrEmpty(CodigoArancelarioId))
                throw new ArgumentException("CodigoArancelarioId cannot null or empty.");

            try
            {
                CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter theAdapter = new CodigoArancelarioDSTableAdapters.CodigoArancelarioTableAdapter();
                theAdapter.Delete(CodigoArancelarioId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting CodigoArancelario", ex);
                throw;
            }
        }
    }
}