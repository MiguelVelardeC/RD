using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using Artexacta.App.User.BLL;
using log4net;

namespace Artexacta.App.UserClienteSOAT.BLL
{
    public class UserClienteSOATBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public static Artexacta.App.Utilities.Bitacora.Bitacora theBitacora = new Artexacta.App.Utilities.Bitacora.Bitacora();

        public UserClienteSOATBLL() { }

        public static void InsertSiniestro(int UserId, int ClienteId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be <= 0.");
            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be <= 0.");

            try
            {
                UserClienteSOATDSTableAdapters.QueriesTableAdapter theAdapter = new UserClienteSOATDSTableAdapters.QueriesTableAdapter();

                theAdapter.InsertUserClienteSOAT(ClienteId, UserId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting UserClienteSOAT", ex);
                throw new ArgumentException("An error was ocurred while inserting UserClienteSOAT.", ex);
            }
        }
        public static void DeleteUserClienteSOAT(int UserId, int ClienteId)
        {
            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            try
            {
                UserClienteSOATDSTableAdapters.QueriesTableAdapter theAdapter = new UserClienteSOATDSTableAdapters.QueriesTableAdapter();
                theAdapter.DeleteUserClienteSOAT(ClienteId, UserId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting UserClienteSOAT", ex);
                throw;
            }
        }
    }
}