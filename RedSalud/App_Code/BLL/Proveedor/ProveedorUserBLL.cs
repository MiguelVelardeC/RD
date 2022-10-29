using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.ProveedorUser.BLL
{
    /// <summary>
    /// Summary description for ProveedorUserBLL
    /// </summary>
    public class ProveedorUserBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public ProveedorUserBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static ProveedorUser FillRecord(ProveedorUserDS.ProveedorUserRow row)
        {
            ProveedorUser objPU = new ProveedorUser(
                row.ProveedorId
                ,row.NombreJuridico
                ,row.UserId
                ,row.fullname
                );

            return objPU;
        }

        public static List<ProveedorUser> ProveedorUserList(int ProveedorId)
        {
            List<ProveedorUser> theList = new List<ProveedorUser>();

            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            ProveedorUser objP = null;
            try
            {
                ProveedorUserDSTableAdapters.ProveedorUserTableAdapter theAdapter = new ProveedorUserDSTableAdapters.ProveedorUserTableAdapter();
                ProveedorUserDS.ProveedorUserDataTable theTable = theAdapter.GetProveedorUserByProveedorId(ProveedorId);

                if (theTable!= null && theTable.Rows.Count>0)
                    foreach (ProveedorUserDS.ProveedorUserRow row in theTable.Rows)
                    {
                        objP = FillRecord(row);
                        theList.Add(objP);
                    }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list ProveedorUser", ex);
                throw;
            }

            return theList;
        }

        public static List<ProveedorUserDS.ProveedorByUserRow> ProveedorByUser(int UserId)
        {
            List<ProveedorUserDS.ProveedorByUserRow> theList = new List<ProveedorUserDS.ProveedorByUserRow>();

            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");

            try
            {
                ProveedorUserDSTableAdapters.ProveedorByUserTableAdapter theAdapter = new ProveedorUserDSTableAdapters.ProveedorByUserTableAdapter();
                ProveedorUserDS.ProveedorByUserDataTable theTable = theAdapter.GetProveedorByUser(UserId);

                if (theTable != null && theTable.Rows.Count > 0)
                    foreach (ProveedorUserDS.ProveedorByUserRow row in theTable.Rows)
                    {
                        theList.Add(row);
                    }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list ProveedorUser", ex);
                throw;
            }

            return theList;
        }

        public static bool BoolInsertProveedorUser(int ProveedorId, int UserId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");

            try
            {
                bool? boolInsert = false;
                ProveedorUserDSTableAdapters.ProveedorUserTableAdapter theAdapter = new ProveedorUserDSTableAdapters.ProveedorUserTableAdapter();
                theAdapter.BoolInsertProveedorUser(ProveedorId, UserId,ref boolInsert);

                return (bool)boolInsert;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while BoolInsertProveedorUser ProveedorUser", ex);
                throw;
            }
        }

        public static bool InsertProveedorUser(int ProveedorId, int UserId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");

            try
            {
                ProveedorUserDSTableAdapters.ProveedorUserTableAdapter theAdapter = new ProveedorUserDSTableAdapters.ProveedorUserTableAdapter();
                theAdapter.Insert(ProveedorId, UserId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting ProveedorUser", ex);
                throw;
            }
        }

        public static bool BoolDeletProveedorUser(int ProveedorId, int UserId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");

            try
            {
                bool? BoolDelet = false;

                ProveedorUserDSTableAdapters.ProveedorUserTableAdapter theAdapter = new ProveedorUserDSTableAdapters.ProveedorUserTableAdapter();
                theAdapter.BoolDeletProveedorUser(ProveedorId, UserId, ref BoolDelet);

                return (bool)BoolDelet;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting ProveedorUser", ex);
                throw;
            }
        }

        public static bool DeleteProveedorUSer(int ProveedorId, int UserId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");

            try
            {
                ProveedorUserDSTableAdapters.ProveedorUserTableAdapter theAdapter = new ProveedorUserDSTableAdapters.ProveedorUserTableAdapter();
                theAdapter.Delete(ProveedorId, UserId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting ProveedorUser", ex);
                throw;
            }
        }
        
    }
}