using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.ProveedorUser
{
    /// <summary>
    /// Summary description for ProveedorUser
    /// </summary>
    public class ProveedorUser
    {
        private int _ProveedorId;
        private string _NombreJuridico;
        private int _UserId;
        private string _UserName;

        public int ProveedorId
        {
            get { return this._ProveedorId; }
            set { this._ProveedorId = value; }
        }
        public string NombreJuridico
        {
            get { return this._NombreJuridico; }
            set { this._NombreJuridico = value; }
        }

        public int UserId
        {
            get { return this._UserId; }
            set { this._UserId = value; }
        }
        public string UserName {
            get { return this._UserName; }
            set { this._UserName = value; }
        }

        public ProveedorUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ProveedorUser(int ProveedorId, string NombreJuridico, int UserId, string UserName)
        {
            this._ProveedorId = ProveedorId;
            this._NombreJuridico = NombreJuridico;
            this._UserId = UserId;
            this._UserName = UserName;
        }
    }
}