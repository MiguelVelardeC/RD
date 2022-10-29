using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.ClienteUsuario.BLL
{
    public class ClienteUsuarioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");


        public ClienteUsuarioBLL()
        {
        }


        private static ClienteUsuario FillRecord(ClienteUsuario2DS.ClienteUsuarioRow row)
        {
            ClienteUsuario objCita = new ClienteUsuario();
            objCita.ClienteId = row.ClienteId;
            objCita.UserId = row.UserId;

            return objCita;
        }

        public static ClienteUsuario GetClienteUsuarioByUserId(int UserId)
        {
            if (UserId <= 0)
                throw new ArgumentException("UserId no puede ser menor o igual a cero.");

            ClienteUsuario TheCita = null;
            try
            {
                ClienteUsuario2DSTableAdapters.ClienteUsuarioTableAdapter theAdapter = 
                    new ClienteUsuario2DSTableAdapters.ClienteUsuarioTableAdapter();
                ClienteUsuario2DS.ClienteUsuarioDataTable theTable = theAdapter.GetByUser(UserId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheCita = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ClienteUsuario data", ex);
                throw;
            }
            return TheCita;
        }        

        public static void InsertClienteUsuario(ClienteUsuario clienteUsuario)
        {
            if (clienteUsuario == null)
                throw new ArgumentException("ClienteUsuario cannot be null.");
            if (clienteUsuario.ClienteId < 0)
                throw new ArgumentException("ClienteId no debe ser 0 o negativo");
            if (clienteUsuario.UserId < 0)
                throw new ArgumentException("UserId no debe ser 0 o negativo");

            try
            {                
                ClienteUsuario2DSTableAdapters.ClienteUsuarioTableAdapter TheAdapter = new ClienteUsuario2DSTableAdapters.ClienteUsuarioTableAdapter();
                TheAdapter.Insert(clienteUsuario.ClienteId, clienteUsuario.UserId);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Inserting ClienteUsuario", q);
                throw;
            }
        }

        public static void DeleteClienteUsuario(int UserId)
        {
            if (UserId <= 0)
                throw new ArgumentException("UserId no debe ser 0 o negativo");

            try
            {
                ClienteUsuario2DSTableAdapters.ClienteUsuarioTableAdapter TheAdapter = new ClienteUsuario2DSTableAdapters.ClienteUsuarioTableAdapter();
                TheAdapter.Delete(UserId);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while deleting ClienteUsuario", q);
                throw;
            }
        }
    }
}