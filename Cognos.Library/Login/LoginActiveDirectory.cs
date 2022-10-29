using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Login
{
    public class LoginActiveDirectory
    {
        static LoginActiveDirectory()
        {}

        public static bool Login(string usr, string pwd)
        {
            //try
            //{
            //    string dPath = System.Configuration.ConfigurationManager.AppSettings.Get("ActiveDirectory"); //"LDAP://juno/DC=cognos,DC=com,DC=bo";
            //    //string dPath = LoginDomainManager.RootPath;
            //    var dEntry = new System.DirectoryServices.DirectoryEntry(dPath, usr, pwd);
            //    var dSearcher = new System.DirectoryServices.DirectorySearcher(dEntry);
            //    var dResult = dSearcher.FindOne();
            //    if (dResult != null)
            //    {
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            return false;
        }
    }
}
