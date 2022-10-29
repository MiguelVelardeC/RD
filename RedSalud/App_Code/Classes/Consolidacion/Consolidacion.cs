using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Consolidacion
{
    /// <summary>
    /// Summary description for Consolidacion
    /// </summary>
    public class Consolidacion
    {
        private int _ConsolidacionId;
        private int _ProveedorId;
        //private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        private decimal _MontoTotal;
        private int _UserId;
        private DateTime _FechaCreacion;


        public int ConsolidacionId {
            get { return this._ConsolidacionId; }
        }
        public int ProveedorId {
            get { return this._ProveedorId; }
        }
        //public DateTime FechaDesde {
        //    get { return this._FechaDesde; }
        //}
        public DateTime FechaHasta {
            get { return this._FechaHasta; }
        }
        public decimal MontoTotal {
            get { return this._MontoTotal; }
        }
        public int UserId {
            get { return this._UserId; }
        }
        
        public DateTime FechaCreacion {
            get { return this._FechaCreacion; }
        }
        public string UserName {

            get
            {
                User.User objUser = User.BLL.UserBLL.GetUserById(_UserId);
                return objUser.FullName;
            }
        }

        //public string ProveedorName {
        //    get {
        //        Proveedor.Proveedor objProveedor = Proveedor.BLL.ProveedorBLL.GetDetails();
        //    }
        //}

        public Consolidacion()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Consolidacion(int ConsolidacionId, int ProveedorId
            , DateTime FechaHasta, decimal MontoTotal
            , int UserId, DateTime FechaCreacion)
        {
            this._ConsolidacionId = ConsolidacionId;
            this._ProveedorId = ProveedorId;
            this._FechaHasta = FechaHasta;
            this._MontoTotal = MontoTotal;
            this._UserId=UserId;
            this._FechaCreacion=FechaCreacion;
        }
    }
}