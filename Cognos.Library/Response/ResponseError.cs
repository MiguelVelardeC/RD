using Cognos.Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cognos.Library.Response
{
    public class ResponseError : IResponse
    {
        #region private Vars
        private string mensaje;
        private string origen;
        private string user;
        private bool showDetails;
        private Exception exception;
        //private T result;
        #endregion

        #region ctor
        public ResponseError(string mensaje, Exception exception, bool showDetails, string origen, string user)
        {
            this.mensaje = mensaje;
            this.exception = exception;
            this.user = user;
            //this.result = result;
            this.origen = origen;
            this.LogError();
        }

       

        #endregion

        #region public props
        public TipoMensaje TipoMensaje
        {
            get { return TipoMensaje.Error; }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
        }
        public Exception Exception
        {
            get
            {
                return this.showDetails ? this.exception : new Exception(this.mensaje);
            }
        }
        //public T Result
        //{
        //    get { return this.result; }
        //}
        public string Origen
        {
            get { return this.origen; }
        }
        #endregion

        #region voids

        private void LogError()
        {
            Log.Guardar(this.exception, this.user, this.origen);
        }
        #endregion
    }
}
