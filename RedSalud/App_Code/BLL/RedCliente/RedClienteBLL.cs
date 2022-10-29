using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User.BLL;
using log4net;

namespace Artexacta.App.RedCliente.BLL
{
    /// <summary>
    /// Summary description for RedClienteBLL
    /// </summary>
    public class RedClienteBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedClienteBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedCliente FillRecord(RedClienteDS.RedClienteRow row)
        {
            RedCliente theNewRecord = new RedCliente(
                row.ClienteId
                , row.CodigoCliente
                , row.NombreJuridico
                , row.Nit
                , row.IsDireccionNull() ? "" : row.Direccion
                , row.IsTelefono1Null() ? "" : row.Telefono1
                , row.IsTelefono2Null() ? "" : row.Telefono2
                , row.IsNombreContactoNull() ? "" : row.NombreContacto
                , row.IsEmailNull() ? "" : row.Email
                , row.CostoConsultaInternista
                , row.NumeroDiasReconsulta
                , row.SoloLiname
                , row.IsSOAT
                , row.IsGestionMedica
                , row.IsDesgravamen);

            return theNewRecord;
        }

        private static RedCliente FillRecord(RedClienteDS.RedClienteRedMedicaRow row)
        {
            RedCliente theNewRecord = new RedCliente(
                row.ClienteId
                , row.CodigoCliente
                , row.NombreJuridico
                , row.Nit
                , row.IsDireccionNull() ? "" : row.Direccion
                , row.IsTelefono1Null() ? "" : row.Telefono1
                , row.IsTelefono2Null() ? "" : row.Telefono2
                , row.IsNombreContactoNull() ? "" : row.NombreContacto
                , row.IsEmailNull() ? "" : row.Email
                , row.CostoConsultaInternista
                , row.NumeroDiasReconsulta
                , row.NombreRedMedica );

            return theNewRecord;
        }

        public static RedCliente GetRedClienteByClienteId(int ClienteId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            RedCliente TheRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetRedClienteByClienteId(ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    RedClienteDS.RedClienteRow row = theTable[0];
                    TheRedCliente = FillRecord(row);             
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting RedCliente data", ex);
                throw;
            }
            return TheRedCliente;
        }

        public static List<RedCliente> getAllRedClienteList()
        {
            return getAllRedClienteList(false, true, false);
        }

        public static List<RedCliente> getAllRedClienteList(bool isSOAT, bool isGestionMedica, bool isDesgravamen)
        {

            List<RedCliente> theList = new List<RedCliente>();
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetAllRedCliente(0);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }

        public static List<RedCliente> getRedClienteForSOATList()
        {
            return getRedClienteForSOATList(false);
        }

        public static List<RedCliente> getRedClienteForSOATList(bool getAll)
        {
            int userId = 0;

            if (!getAll && !LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("SOAT_ALLCLIENTES"))
            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            List<RedCliente> theList = new List<RedCliente>();
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetSOATRedCliente(userId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }

        public static List<RedCliente> getRedClienteList()
        {
            int userId = 0;

            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            List<RedCliente> theList = new List<RedCliente>();
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetAllRedCliente(userId);
                
                if (theTable != null && theTable.Rows.Count > 0)
                { 
                    foreach(RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }

        public static List<RedCliente> getRedClienteListTodos()
        {
            int userId = 0;

            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            List<RedCliente> theList = new List<RedCliente>();
            ////////
            RedCliente cl = new RedCliente()
            {
                ClienteId = 0,
                NombreJuridico = "TODOS"
            };
            ////////
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetAllRedCliente(userId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ////////
                    if (theTable.Rows.Count > 1)
                    {
                        theList.Add(cl);
                    }
                    ////////
                    foreach (RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }
        public static List<RedCliente> GetUserClienteSOATByUserId(int UserId)
        {
            List<RedCliente> theList = new List<RedCliente>();
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetUserClienteSOATByUserId(UserId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }
        //Lista de Clientes para Reportes de Gestion Medica
        public static List<RedCliente> getRedClienteListFE ()
        {
            int userId = 0;

            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            List<RedCliente> theList = new List<RedCliente>();
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetAllRedCliente(userId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    RedCliente theEmpty = new RedCliente();
                    theEmpty.NombreJuridico = "[Todos]";
                    theList.Add(theEmpty);
                    foreach (RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        RedCliente theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }

        public static int InsertRedCliente(string CodigoCliente, string NombreJuridico
            ,decimal Nit, string Direccion, string Telefono1, string Telefono2
            , string NombreContacto, string Email, int CostoConsultaInternista, int NumeroDiasReconsulta,
            bool SoloLiname, bool isSOAT, bool isGestionMedica, bool isDesgravamen) 
        {
            if (string.IsNullOrEmpty(CodigoCliente))
                throw new ArgumentException("CodigoCliente cannot be null or empty.");
            if (string.IsNullOrEmpty(NombreJuridico))
                throw new ArgumentException("NombreJuridico cannot be null or empty.");

            if (Nit<=0)
                throw new ArgumentException("Nit cannot be less than or equal to zero.");

            int? ClienteId = 0;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                theAdapter.InsertRedCliente(ref ClienteId, CodigoCliente, NombreJuridico, Nit, Direccion, 
                    Telefono1, Telefono2, NombreContacto, Email, CostoConsultaInternista, NumeroDiasReconsulta,
                    SoloLiname, isSOAT, isGestionMedica, isDesgravamen);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting RedCliente", ex);
                throw;
            }
            return (int)ClienteId;
        }

        public static bool UpdateRedCliente(int ClienteId, string CodigoCliente, string NombreJuridico
            , decimal Nit, string Direccion, string Telefono1, string Telefono2
            , string NombreContacto, string Email, int CostoConsultaInternista, int NumeroDiasReconsulta,
            bool SoloLiname, bool isSOAT, bool isGestionMedica, bool isDesgravamen)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            if (string.IsNullOrEmpty(CodigoCliente))
                throw new ArgumentException("CodigoCliente cannot be null or empty.");
            if (string.IsNullOrEmpty(NombreJuridico))
                throw new ArgumentException("NombreJuridico cannot be null or empty.");

            if (Nit <= 0)
                throw new ArgumentException("Nit cannot be less than or equal to zero.");

            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                theAdapter.UpdateRedCliente(ClienteId, CodigoCliente, NombreJuridico, Nit, Direccion
                    , Telefono1, Telefono2, NombreContacto, Email, CostoConsultaInternista, NumeroDiasReconsulta,
                    SoloLiname, isSOAT, isGestionMedica, isDesgravamen);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating RedCliente", ex);
                throw;
            }
        }

        public static bool DeleteRedCliente(int ClienteId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                theAdapter.DeleteRedCliente(ClienteId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting RedCliente", ex);
                throw;
            }
        }

        public static int InsertRedCliente(RedCliente objRedCliente)
        {
            if (string.IsNullOrEmpty(objRedCliente.CodigoCliente))
                throw new ArgumentException("CodigoCliente cannot be null or empty.");
            if (string.IsNullOrEmpty(objRedCliente.NombreJuridico))
                throw new ArgumentException("NombreJuridico cannot be null or empty.");

            if (objRedCliente.Nit <= 0)
                throw new ArgumentException("Nit cannot be less than or equal to zero.");

            int? ClienteId = 0;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                theAdapter.InsertRedCliente(ref ClienteId, objRedCliente.CodigoCliente, objRedCliente.NombreJuridico
                    , objRedCliente.Nit, objRedCliente.Direccion, objRedCliente.Telefono1
                    , objRedCliente.Telefono2, objRedCliente.NombreContacto
                    , objRedCliente.Email, objRedCliente.CostoConsultaInternista, objRedCliente.NumeroDiasReconsulta,
                    objRedCliente.SoloLiname, objRedCliente.IsSOAT, objRedCliente.IsGestionMedica, objRedCliente.IsDesgravamen);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting RedCliente", ex);
                throw;
            }
            return (int)ClienteId;
        }

        public static bool UpdateRedCliente(RedCliente objRedCliente)
        {
            if (objRedCliente.ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            if (string.IsNullOrEmpty(objRedCliente.CodigoCliente))
                throw new ArgumentException("CodigoCliente cannot be null or empty.");
            if (string.IsNullOrEmpty(objRedCliente.NombreJuridico))
                throw new ArgumentException("NombreJuridico cannot be null or empty.");

            if (objRedCliente.Nit <= 0)
                throw new ArgumentException("Nit cannot be less than or equal to zero.");

            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                theAdapter.UpdateRedCliente(objRedCliente.ClienteId, objRedCliente.CodigoCliente
                        , objRedCliente.NombreJuridico, objRedCliente.Nit, objRedCliente.Direccion
                        , objRedCliente.Telefono1, objRedCliente.Telefono2, objRedCliente.NombreContacto
                        , objRedCliente.Email, objRedCliente.CostoConsultaInternista, objRedCliente.NumeroDiasReconsulta,
                        objRedCliente.SoloLiname, objRedCliente.IsSOAT, objRedCliente.IsGestionMedica, objRedCliente.IsDesgravamen);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating RedCliente", ex);
                throw;
            }
        }

        public static bool GetSoloLiname(int ClienteId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                return (bool)theAdapter.GetSoloLiname(ClienteId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating RedCliente", ex);
                throw;
            }
        }

        public static List<RedCliente> getRedClienteRedMedicaList()
        {
            List<RedCliente> theList = new List<RedCliente>();
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteRedMedicaTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteRedMedicaTableAdapter();
                RedClienteDS.RedClienteRedMedicaDataTable theTable = theAdapter.GetAllRedClienteRedMedica();

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClienteDS.RedClienteRedMedicaRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCliente", ex);
                throw;
            }
            return theList;
        }

        public static bool InsertRedClienteRedMedica(int ClienteId, int RedMedicaId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            if (RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be less than or equal to zero.");
            try
            {
                RedClienteDSTableAdapters.RedClienteRedMedicaTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteRedMedicaTableAdapter();
                theAdapter.InsertRedClienteRedMedica(ClienteId, RedMedicaId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting RedClienteRedMedica", ex);
                throw;
            }
        }

        public static bool DeleteRedClienteRedMedica(int ClienteId, int RedMedicaId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            if (RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be less than or equal to zero.");
            
            try
            {
                RedClienteDSTableAdapters.RedClienteRedMedicaTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteRedMedicaTableAdapter();
                theAdapter.DeleteRedClienteRedMedica(ClienteId, RedMedicaId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting RedClienteRedMedica", ex);
                throw;
            }
        }

        public static List<RedCliente> GetClientesDesgravamen()
        {
            List<RedCliente> theList = new List<RedCliente>();
            RedCliente theRedCliente = null;
            try
            {
                RedClienteDSTableAdapters.RedClienteTableAdapter theAdapter = new RedClienteDSTableAdapters.RedClienteTableAdapter();
                RedClienteDS.RedClienteDataTable theTable = theAdapter.GetClientesDesgravamen();

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClienteDS.RedClienteRow row in theTable.Rows)
                    {
                        theRedCliente = FillRecord(row);
                        theList.Add(theRedCliente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting list RedCliente", ex);
                throw;
            }
            return theList;
        }

    }
}