using Cognos.Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cognos.Library.Response
{
    public class ResponseInfo : IResponse
    {
        #region private Vars
        private string mensaje;
        private string origen;
        //private T result;
        #endregion

        #region ctor
        public ResponseInfo(string mensaje, string origen)
        {
            this.mensaje = mensaje;
            //this.result = result;
            this.origen = origen;
        }

        #endregion

        #region public props
        public TipoMensaje TipoMensaje
        {
            get { return TipoMensaje.Info; }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
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
    }
}
