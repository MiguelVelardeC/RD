using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Artexacta.App.User;
using UserDSTableAdapters;
using System.Web.Security;
using System.Configuration;

namespace Artexacta.App.User.BLL
{
    /// <summary>
    /// Summary description for UserBLL
    /// </summary>
    [System.ComponentModel.DataObject]
    public class UserBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        UserTableAdapter _theAdapter = null;

        protected UserTableAdapter theAdapter
        {
            get
            {
                if (_theAdapter == null)
                    _theAdapter = new UserTableAdapter();
                return _theAdapter;
            }
        }

        public UserBLL()
        {
        }

        private static User FillUserRecord(UserDS.UserRow row)
        {
            User theNewRecord = new User(
                row.userId,
                row.fullname,
                row.IscellphoneNull() ? "" : row.cellphone,
                row.IsaddressNull() ? "" : row.address,
                row.IsphonenumberNull() ? "" : row.phonenumber,
                row.IsphoneareaNull() ? 0 : row.phonearea,
                row.IsphonecodeNull() ? 0 : row.phonecode,
                row.username,
                row.IsemailNull() ? "" : row.email,
                row.ciudadId);

            theNewRecord.SignatureFileId = (row.IssignatureFileIdNull()) ? 0 : row.signatureFileId;

            return theNewRecord;
        }

        private static User FillUserRecord(UserDS.SearchUsersActiveRow row)
        {
            User theNewRecord = new User(
                row.userId,
                row.fullname,
                row.IscellphoneNull() ? "" : row.cellphone,
                row.IsaddressNull() ? "" : row.address,
                row.IsphonenumberNull() ? "" : row.phonenumber,
                row.IsphoneareaNull() ? 0 : row.phonearea,
                row.IsphonecodeNull() ? 0 : row.phonecode,
                row.username,
                row.IsemailNull() ? "" : row.email,
                row.ciudadId);
               return theNewRecord;
        }

        public static User GetUserById(int IdUser)
        {
            UserTableAdapter localAdapter = new UserTableAdapter();

            if (IdUser <= 0)
                return null;

            User theUser = null;

            try
            {
                UserDS.UserDataTable table = localAdapter.GetUserById(IdUser);

                if (table != null && table.Rows.Count > 0)
                {
                    UserDS.UserRow row = table[0];
                    theUser = FillUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting user data", q);
                return null;
            }

            return theUser;
        }

        public static User GetUserByUsername(string Username)
        {
            UserTableAdapter localAdapter = new UserTableAdapter();

            if (string.IsNullOrEmpty(Username))
                return null;

            User theUser = null;
            try
            {
                UserDS.UserDataTable table = localAdapter.GetUserByUsername(Username);

                if (table != null && table.Rows.Count > 0)
                {
                    UserDS.UserRow row = table[0];
                    theUser = FillUserRecord(row);
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting user data", q);
            }
            return theUser;
        }

        public List<User> GetUsersListForSearch(string whereSql)
        {
            if (string.IsNullOrEmpty(whereSql))
                whereSql = "1 = 1";

            List<User> theList = new List<User>();
            User theUser = null;

            try
            {
                UserDS.UserDataTable table = theAdapter.GetUsersForSearch(whereSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (UserDS.UserRow row in table.Rows)
                    {
                        theUser = FillUserRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while getting list of Users from data base", q);
                return null;
            }
            return theList;
        }

        public static int GetUserIdByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return 0;

            try
            {
                User theUser = GetUserByUsername(username);

                if (theUser != null && theUser.UserId > 0)
                    return theUser.UserId;
                else
                    return 0;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting user data", q);
                return 0;
            }
        }

        public static int InsertUserRecord(string Username, string Fullname, string Cellphone, string Address, string Phone, 
            int PhoneArea, int PhoneCode, string Email, string CiudadId)
        {
            int? newUserId = 0;

            if (string.IsNullOrEmpty(Username))
                throw new ArgumentException("Username cannot be null or empty.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                UserTableAdapter localAdapter = new UserTableAdapter();

                object resutl = localAdapter.InsertUserRecord(
                    string.IsNullOrEmpty(Fullname) ? "" : Fullname,
                    string.IsNullOrEmpty(Cellphone) ? "" : Cellphone,
                    string.IsNullOrEmpty(Address) ? "" : Address,
                    string.IsNullOrEmpty(Phone) ? "" : Phone,
                    PhoneArea, PhoneCode,
                    Username,
                    string.IsNullOrEmpty(Email) ? "" : Email,
                    CiudadId,
                    ref newUserId);

                log.Debug("Se insertó el usuario " + Username);

                if (newUserId == null || newUserId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                return (int)newUserId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting user", q);
                throw q;
            }
        }


        public static bool UpdateUserRecord(int Userid, string Username, string Fullname,
            string Cellphone, string Address, string Phone,
            int PhoneArea, int PhoneCode, string Email,string CiudadId)
        {
            if (Userid <= 0)
                throw new ArgumentException("Id de usuario no puede ser <= 0");

            if (string.IsNullOrEmpty(Username))
                throw new ArgumentException("Nombre de usuario no puede ser nulo.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId no puede ser nulo o vacio.");
            try
            {
                UserTableAdapter localAdapter = new UserTableAdapter();

                localAdapter.UpdateUserRecord(
                    string.IsNullOrEmpty(Fullname) ? "" : Fullname,
                    string.IsNullOrEmpty(Cellphone) ? "" : Cellphone,
                    string.IsNullOrEmpty(Address) ? "" : Address,
                    string.IsNullOrEmpty(Phone) ? "" : Phone,
                    PhoneArea, PhoneCode, Username, Userid,
                    string.IsNullOrEmpty(Email) ? "" : Email,
                    CiudadId
                    );
                ProveedorUserDSTableAdapters.ProveedorUpdateNewTableAdapter Adapter = new ProveedorUserDSTableAdapters.ProveedorUpdateNewTableAdapter();
                Adapter.UpdateProveedorUserNew(string.IsNullOrEmpty(Fullname) ? "" : Fullname,
                    string.IsNullOrEmpty(Cellphone) ? "" : Cellphone,
                    string.IsNullOrEmpty(Address) ? "" : Address,
                    string.IsNullOrEmpty(Phone) ? "" : Phone,Userid
                    );

                log.Debug("Se modifico el usuario " + Username);
                return true;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while updating user", q);
                return false;
            }
        }


        public static bool DeleteUserRecord(int UserId)
        {
            if (UserId <= 0)
                throw new ArgumentException("Error en el código de usuario a eliminar.");

            UserTableAdapter localAdapter = new UserTableAdapter();

            try
            {
                User theUser = UserBLL.GetUserById(UserId);

                localAdapter.DeleteUserRecord(UserId);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while deleting user", q);
                throw q;
            }

            return true;
        }        

        public static void InsertUserInRoles(string Username, string Rol)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Rol))
                throw new ArgumentException("Error en los parametros de usuarios y roles.");

            try
            {
                int UserId = 0;
                User theUserClass = null;
                theUserClass = GetUserByUsername(Username);
                UserId = theUserClass.UserId;

            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while deleting user", q);
                throw q;
            }
        }

        public static void DeleteUserInRoles(string Username, string Rol)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Rol))
                throw new ArgumentException("Error en los parametros de usuarios y roles.");

            try
            {
                int UserId = 0;
                User theUserClass = null;
                theUserClass = GetUserByUsername(Username);

                if (theUserClass != null)
                    UserId = theUserClass.UserId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while deleting user", q);
                throw q;
            }
        }

        public static int GetUserIdByEmailAddress(string EmailAddress)
        {
            UserTableAdapter localAdapter = new UserTableAdapter();

            log.Debug("Getting the user Id from data base given an email address. Function GetUserIdByEmailAddress from UserBLL");

            try
            {
                Object table = localAdapter.GeUserIdByEmail(EmailAddress);
                if (table != null)
                {
                    return Convert.ToInt32(table.ToString());
                }
            }
            catch { }

            return 0;
        }

        public List<User> GetUsersBySearchParameters(string Username, string Fullname)
        {
            List<User> theList = new List<User>();
            User theUser = null;

            if (!string.IsNullOrEmpty(Username))
            {
                Username = "%" + Username + "%";
            }
            else
            {
                Username = "";
            }

            if (!string.IsNullOrEmpty(Fullname))
            {
                Fullname = "%" + Fullname + "%";
            }
            else
            {
                Fullname = "";
            }

            try
            {
                UserDS.UserDataTable table = theAdapter.GetUsersBySearchParameters(Username, Fullname);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (UserDS.UserRow row in table.Rows)
                    {
                        theUser = FillUserRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while getting list of TradeUsers from data base", q);
                throw q;
            }
            return theList;
        }

        public static List<User> GetUsersForAutoComplete(int? start, int? numItems, string filter, ref int? totalRows)
        {
            UserTableAdapter localAdapter = new UserTableAdapter();

            List<User> theList = new List<User>();
            User theClient = null;

            try
            {
                UserDS.UserDataTable theTable =
                    localAdapter.GetUsersForAutocomplete(numItems, start, filter, ref totalRows);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (UserDS.UserRow theRow in theTable.Rows)
                    {
                        theClient = FillUserRecord(theRow);
                        theList.Add(theClient);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al obtener la lista de Clientes de la Base de Datos", q);
            }
            return theList;
        }

        public static List<User> GetUsersActiveForAutoComplete(int? start, int? numItems, string filter, ref int? totalRows)
        {
            //UserTableAdapter localAdapter = new UserTableAdapter();

            List<User> theList = new List<User>();
            User theClient = null;

            try
            {
                //  EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                //  EspecialidadDS.EspecialidadDataTable theTable = theAdapter.SearchEspecialidad(numItems, start, filter, ref totalRows);

                UserDSTableAdapters.SearchUsersActiveTableAdapter theAdapter = new UserDSTableAdapters.SearchUsersActiveTableAdapter();
                UserDS.SearchUsersActiveDataTable theTable = theAdapter.GetUserSearchUsersActive(numItems, start, filter, ref totalRows);
               // UserDS.UserDataTable theTable =  .GetUserActiveForAutocomplete(numItems, start, filter, ref totalRows);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (UserDS.SearchUsersActiveRow theRow in theTable.Rows)
                    {
                        theClient = FillUserRecord(theRow);
                        theList.Add(theClient);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("Ocurrió un error al obtener la lista de Clientes de la Base de Datos", q);
            }
            return theList;
        }

        public static string GetCuidadIdByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return "";

            try
            {
                User theUser = GetUserByUsername(username);

                if (theUser != null && theUser.UserId > 0)
                    return theUser.CiudadId;
                else
                    return "";
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while geting user data (CiudadId)", q);
                return "";
            }
        }

        public static int GetProveedorIdTheUserName(string Username)
        {
            if (string.IsNullOrEmpty(Username))
                return 0;

            int ProveedorId= 0;
            try
            {
                UserTableAdapter theAdapter = new UserTableAdapter();

                Object ObjProveedorId = theAdapter.GetProveedorIdByUserName(Username);
                if(ObjProveedorId!=null)
                    ProveedorId= Convert.ToInt32(ObjProveedorId.ToString());
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ProveedorId the UserId", ex);
                throw;
            }
            return ProveedorId;
        }

        public static bool UpdateSignatureFileId(int signatureFileId, int userId)
        {
            if (userId <= 0)
                return false;

            try
            {
                UserTableAdapter theAdapter = new UserTableAdapter();
                theAdapter.UpdateUserSignatureId(signatureFileId, userId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating the signatureFile Id of UserId "+userId, ex);
                throw;
            }
        }
    }
}