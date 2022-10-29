using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cognos.Library.Login
{
    public class LoginInfo
    {
        public LoginInfo()
        {}
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Applicacion { get; set; }
        public string Entorno { get; set; }
        public string[] Roles { get; set; }
        public string[] RoleNames { get; set; }
        public string TipoBanca { get; set; }
        public string TipoBancaID { get; set; }
        public string Plaza { get; set; }
        public string PlazaID { get; set; }
        public string Agencia { get; set; }
        public string AgenciaID { get; set; }
        public string Jerarquia { get; set; }
    }
}