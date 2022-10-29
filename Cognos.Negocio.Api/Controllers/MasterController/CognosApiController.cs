using Cognos.Library.Helper;
using Cognos.Library.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cognos.Negocio.Api.Controllers
{
    public class CognosApiController : ApiController
    {
        #region props

        public string Usuario
        {
            get
            {
                try
                {
                    string user = RequestContext.Principal.Identity.Name;
                    return user;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        #region response

        [NonAction]
        public IHttpActionResult ResponseVal(string msg)
        {
            return ResponseMessage(Request.CreateResponse<IResponse>(HttpStatusCode.InternalServerError, ResponseFactory.Create(msg,"Negocio")));
        }
        [NonAction]
        public IHttpActionResult ResponseOk<T>(T result, int? totalNroFilas = null, int? maximoNroFilas = null)
        {
            return Ok<IResponse>(ResponseFactory.Create<T>(result, "", "Negocio", totalNroFilas, maximoNroFilas));
        }
        [NonAction]
        public IHttpActionResult ResponseOk()
        {
            return Ok<IResponse>(ResponseFactory.Create("", "Negocio"));
        }
        [NonAction]
        public IHttpActionResult ResponseEx(Exception ex, string msg = "Un error ha ocurrido.")
        {
            string origen = string.Format("Controller [{0}], Action [{1}], Url [{2}]",
                    ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                    , ActionContext.ActionDescriptor.ActionName
                    , ActionContext.Request.RequestUri);
            return ResponseMessage(Request.CreateResponse<IResponse>(HttpStatusCode.InternalServerError, ResponseFactory.Create(msg, ex, origen, this.Usuario)));
        }        

        [NonAction]
        public void LogearEx(Exception ex)
        {
            try
            {
                string Origen = string.Format("Controller [{0}], Action [{1}], Url [{2}]",
                    ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                    , ActionContext.ActionDescriptor.ActionName
                    , ActionContext.Request.RequestUri);
                Log.Guardar(ex, ActionContext.RequestContext.Principal.Identity.Name ?? ""
                    , Origen);
            }
            catch (Exception)
            {
            }
        }

        [NonAction]
        public static void LogearEx(Exception ex, string origen, string usuario)
        {
            try
            {
                Log.Guardar(ex, usuario, origen);
            }
            catch (Exception)
            {
            }
        }

        #endregion

    }
}
