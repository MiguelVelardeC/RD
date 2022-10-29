using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognos.Negocio;

namespace Cognos.Negocio.Library
{
    public class Autenticacion
    {
        public Autenticacion()
        { }
        static Autenticacion()
        { }
        public static bool Autenticar(string usr, string pwd)
        {
            usr = usr.Trim();
            pwd = pwd.Trim();
            if (usr == string.Empty || pwd == string.Empty)
            {
                return false;
            }
            using (Negocio context = new Negocio())
            {
                var usuario = context.tbl_UsuarioServicio.Where(x => x.Usuario == usr && x.Contrasena == pwd).Select(x=>x.Usuario).FirstOrDefault();
                if (!string.IsNullOrEmpty(usuario))
                {
                    return true;
                }
                return false;
            }
        }
        public static bool Autenticar(string usr, string pwd, out string clientes)
        {
            clientes = "";
            bool isAuth = Autenticar(usr, pwd);
            if (isAuth)
            {
                clientes = ObtenerClientesRelacionadosStr(usr);
                return true;
            }
            return false;
        }

        public static List<tbl_RED_Cliente> ObtenerClientesRelacionados(string usr)
        {
            usr = usr.Trim();
            using (Negocio context = new Negocio())
            {
                var usuario = context.tbl_UsuarioServicio.Where(x => x.Usuario == usr).FirstOrDefault();
                if (usuario == null)
                {
                    return null;
                }
                var clientes = (from usrcli in context.tbl_UsuarioServicioCliente
                                join cli in context.tbl_RED_Cliente on usrcli.ClienteID equals cli.ClienteId
                                where usrcli.UsuarioServicioID == usuario.UsuarioServicioID
                                select cli
                                ).ToList();
                return clientes;
            }
        }
        public static string ObtenerClientesRelacionadosStr(string usr)
        {
            var clientes = ObtenerClientesRelacionados(usr);
            if (clientes == null || clientes.Count <= 0)
            {
                return "";
            }
            string[] clientesStrLst = clientes.Select(x => ("{codigo:" + x.CodigoCliente + ",nombre:" + x.NombreJuridico + "}")).ToArray();
            string clientesStr = string.Join(",", clientesStrLst);
            return clientesStr;
        }
    }
}
