using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ClienteUsuarioBLL
    /// </summary>
    public class ClienteUsuarioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public ClienteUsuarioBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static int GetClienteByUsuarioId(int intUsuarioId)
        {
            if (intUsuarioId <= 0)
                throw new ArgumentException("UsuarioId cannot be equals or less than zero");

                int? intClienteId = 0;
                int? intErrorId = 0;
                ClienteDesgravamenDSTableAdapters.usp_DESG_GetClienteByUsuarioIdTableAdapter adapter = new ClienteDesgravamenDSTableAdapters.usp_DESG_GetClienteByUsuarioIdTableAdapter();
                adapter.GetClienteByUsuarioId(intUsuarioId, false, ref intClienteId, ref intErrorId);
                if (intErrorId != null && intErrorId.Value < 1)
                {
                    return intClienteId != null ? intClienteId.Value : -1;
                }
                else
                {
                    throw new ArgumentException("the stored procedure couldn't find any Clients linked to that particular user");
                }
        }

        public static void SaveClienteUsuario(int userId, int clienteId, int oldClienteId)
        {
            if (userId <= 0)
                throw new ArgumentException("userId cannot be equals or less than zero");
            if (clienteId <= 0)
                throw new ArgumentException("clienteId cannot be equals or less than zero");
            ClienteDesgravamenDSTableAdapters.usp_DESG_GetClienteByUsuarioIdTableAdapter adapter = new ClienteDesgravamenDSTableAdapters.usp_DESG_GetClienteByUsuarioIdTableAdapter();
            adapter.SaveClienteUsuario(userId, clienteId, oldClienteId);
                
        }
        public static void DeleteClienteUsuario(int userId, int clienteId)
        {
            if (userId <= 0)
                throw new ArgumentException("userId cannot be equals or less than zero");
            if (clienteId <= 0)
                throw new ArgumentException("clienteId cannot be equals or less than zero");
            ClienteDesgravamenDSTableAdapters.usp_DESG_GetClienteByUsuarioIdTableAdapter adapter = new ClienteDesgravamenDSTableAdapters.usp_DESG_GetClienteByUsuarioIdTableAdapter();
            adapter.DeleteClienteUsuario(userId, clienteId);

        }
        public static List<ClienteUsuario.ClienteUsuario> GetClienteUsuariosByClienteId(int clienteId)
        {
            ClienteUsuarioDSTableAdapters.ClienteUsuariosTableAdapter adapter = new ClienteUsuarioDSTableAdapters.ClienteUsuariosTableAdapter();
            ClienteUsuarioDS.ClienteUsuariosDataTable table = adapter.GetClienteUsuariosByClienteId(clienteId);

            List<ClienteUsuario.ClienteUsuario> list = new List<ClienteUsuario.ClienteUsuario>();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (ClienteUsuarioDS.ClienteUsuariosRow item in table)
                {
                    ClienteUsuario.ClienteUsuario clienteUsuario = new ClienteUsuario.ClienteUsuario()
                    {
                        ClienteId = item.ClienteId,
                        UserId = item.UserId,
                        Username = item.username,
                        UsuarioNombre = item.fullname
                    };

                    list.Add(clienteUsuario);
                }
            }

            return list;
        }
    }
}