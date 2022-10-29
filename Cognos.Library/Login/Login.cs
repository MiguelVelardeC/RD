using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Login
{
    public class Login
    {
        public Login()
        {
                
        }
        static Login()
        {}

        public static bool AutenticarActiveDirectory(string usuario, string password)
        {
            try
            {
                //return true;
                return LoginActiveDirectory.Login(usuario, password);
            }
            catch (Exception ex)
            {
                Helper.Log.Guardar(ex.Message, usuario, "Cognos.Services.Login");
                return false;
            }

        }
        public static async Task<bool> AutenticarCFI(string mode, string usuario, string password)
        {
            try
            {
                return await LoginCognos.Autenticar(mode, usuario,password);
            }
            catch (Exception ex)
            {
                Helper.Log.Guardar(ex.Message, usuario, "Cognos.Services.Login");
                return false;
            }
            
        }

        public static async Task<LoginInfo> Autorizar(string usuario)
        {
            try
            {
                return await LoginCognos.Autorizar(usuario);
            }
            catch (Exception ex)
            {
                Helper.Log.Guardar(ex.Message, usuario, "Cognos.Services.Login");
                return null;
            }
        }

        public enum AuthenticationClient
        {
            Cognos = 0,
            Baneco = 1
        }
    }
    
}
