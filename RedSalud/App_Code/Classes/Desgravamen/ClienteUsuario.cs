using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.ClienteUsuario
{
    /// <summary>
    /// Summary description for ClienteUsuario
    /// </summary>
    public class ClienteUsuario
    {
        public int ClienteId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UsuarioNombre { get; set; }

        public ClienteUsuario()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}