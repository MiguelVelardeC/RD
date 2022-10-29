using Cognos.Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cognos.Library.Response
{
    public class ResponseData<T> : IResponse
    {
        #region private Vars
        private string mensaje;
        private string origen;
        private T result;
        private int? totalNroFilas;
        private int? maximoNroFilas;
        #endregion

        #region ctor
        public ResponseData(T result, string mensaje, string origen, int? totalNroFilas, int? maximoNroFilas)
        {
            this.mensaje = mensaje;
            this.result = result;
            this.origen = origen;
            this.totalNroFilas = totalNroFilas;
            this.maximoNroFilas = maximoNroFilas;
        }

        #endregion

        #region public props
        public TipoMensaje TipoMensaje
        {
            get { return TipoMensaje.Data; }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
        }
        public T Result
        {
            get { return this.result; }
        }
        public string Origen
        {
            get { return this.origen; }
        }
        public int? TotalNroFilas
        {
            get { return this.totalNroFilas; }
        }        
        public int? MaximoNroFilas
        {
            get { return this.maximoNroFilas; }
        }
        #endregion
    }
}
