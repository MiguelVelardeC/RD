using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Enfermedad.BLL
{
    public class EnfermedadBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EnfermedadBLL(){}

        private static Enfermedad FillRecord(EnfermedadDS.EnfermedadRow row)
        {
            Enfermedad objEnfermedad=new Enfermedad(
                row.EnfermedadId
                ,row.Nombre);
            return objEnfermedad;
        }

        public static Enfermedad GetEnfermedadByID ( string EnfermedadId )
        {
            if (string.IsNullOrEmpty(EnfermedadId))
                throw new ArgumentException("EnfermedadId cannot null or empty.");

            Enfermedad TheEnfermedad = null;
            try
            {
                EnfermedadDSTableAdapters.EnfermedadTableAdapter theAdapter = new EnfermedadDSTableAdapters.EnfermedadTableAdapter();
                EnfermedadDS.EnfermedadDataTable theTable = theAdapter.GetEnfermedadByID(EnfermedadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    EnfermedadDS.EnfermedadRow row = theTable[0];
                    TheEnfermedad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Enfermedad data", ex);
                throw;
            }
            return TheEnfermedad;
        }

        public static List<Enfermedad> getEnfermedadList (string search)
        {
            List<Enfermedad> theList = new List<Enfermedad>();
            Enfermedad theEnfermedad = null;
            try
            {
                EnfermedadDSTableAdapters.EnfermedadTableAdapter theAdapter = new EnfermedadDSTableAdapters.EnfermedadTableAdapter();
                int? totalRow = 0;
                EnfermedadDS.EnfermedadDataTable theTable = theAdapter.SearchEnfermedad(SqlInt32.MaxValue.Value, 0, search, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EnfermedadDS.EnfermedadRow row in theTable.Rows)
                    {
                        theEnfermedad = FillRecord(row);
                        theList.Add(theEnfermedad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Enfermedad", ex);
                throw;
            }
            return theList;
        }

        public static List<Enfermedad> getEnfermedadList ()
        {
            return getEnfermedadList("");
        }

        public static int SearchEnfermedad (ref List<Enfermedad> _cache, int pageSize, int firstRow, string searchFilter)
        {
            try
            {
                EnfermedadDSTableAdapters.EnfermedadTableAdapter theAdapter = new EnfermedadDSTableAdapters.EnfermedadTableAdapter();
                int? totalRow = 0;
                EnfermedadDS.EnfermedadDataTable theTable = theAdapter.SearchEnfermedad(pageSize, firstRow, searchFilter, ref totalRow);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EnfermedadDS.EnfermedadRow row in theTable.Rows)
                    {
                        Enfermedad theEnfermedad = FillRecord(row);
                        _cache.Add(theEnfermedad);
                    }
                }
                return (int)totalRow;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting search Enfermedad", ex);
                throw;
            }
        }

        public static bool InsertEnfermedad(string EnfermedadId, string Nombre)
        {
            if (string.IsNullOrEmpty(EnfermedadId))
                throw new ArgumentException("EnfermedadId cannot null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                EnfermedadDSTableAdapters.EnfermedadTableAdapter theAdapter = new EnfermedadDSTableAdapters.EnfermedadTableAdapter();
                theAdapter.Insert(EnfermedadId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Enfermedad", ex);
                throw;
            }
        }

        public static bool UpdateEnfermedad(string oldEnfermedadId, string EnfermedadId, string Nombre)
        {
            if (string.IsNullOrEmpty(oldEnfermedadId))
                throw new ArgumentException("EnfermedadId cannot null or empty.");
            if (string.IsNullOrEmpty(EnfermedadId))
                throw new ArgumentException("EnfermedadId cannot null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                EnfermedadDSTableAdapters.EnfermedadTableAdapter theAdapter = new EnfermedadDSTableAdapters.EnfermedadTableAdapter();
                theAdapter.Update(oldEnfermedadId, EnfermedadId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Enfermedad", ex);
                throw;
            }
        }

        public static bool DeleteEnfermedad(string EnfermedadId)
        {
            if (string.IsNullOrEmpty(EnfermedadId))
                throw new ArgumentException("EnfermedadId cannot null or empty.");

            try
            {
                EnfermedadDSTableAdapters.EnfermedadTableAdapter theAdapter = new EnfermedadDSTableAdapters.EnfermedadTableAdapter();
                theAdapter.Delete(EnfermedadId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Enfermedad", ex);
                throw;
            }
        }
    }
}