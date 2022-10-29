using System;

namespace Cognos.Library.Response
{
    public class ResponseFactory
    {
        #region ctor

        static ResponseFactory()
        {}

        #endregion

        #region IResponse


        public static IResponse Create<T>(T result, string mensaje, string origen, int? totalNroFilas, int? maximoNroFilas)
        {
            return new ResponseData<T>(result, mensaje, origen, totalNroFilas, maximoNroFilas);
        }
        public static IResponse Create(string mensaje, Exception exception, string origen, string user)
        {
            bool showDetails = false;
            return new ResponseError(mensaje, exception, showDetails, origen, user);
        }
        public static IResponse Create(string mensaje, string origen)
        {
            return new ResponseInfo(mensaje, origen);
        }

        #endregion

        private static string ValidateOrigin(ref string origen, string assemby)
        {
            if (string.IsNullOrEmpty(origen))
            {
                try
                {
                    origen = assemby;
                }
                catch (Exception)
                { }
            }
            return origen;
        }
    }
}
