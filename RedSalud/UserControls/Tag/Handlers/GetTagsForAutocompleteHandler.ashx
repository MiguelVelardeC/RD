<%@ WebHandler Language="C#" Class="GetTagsForAutocompleteHandler" %>

using System;
using System.Web;

public class GetTagsForAutocompleteHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";
        log4net.ILog log = log4net.LogManager.GetLogger("Standard");

        string q = context.Request.Params["q"];
        string type = context.Request.Params["type"];

        System.Collections.Generic.List<string> theList = null;
        try
        {
          if (type == "Proveedor")
            {
                theList = Artexacta.App.Preliquidacion.BLL.PreliquidacionBLL.GetTags(q);
            }
            else
            {
                theList = Artexacta.App.GestionMedica.BLL.GestionMedicaBLL.GetTags(q);
            }
        }
        catch (Exception ex)
        {
            log.Error("Cannot get tags from database", ex);
            theList = new System.Collections.Generic.List<string>();
        }

        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        string theListToString = js.Serialize(theList);
        context.Response.Write(theListToString);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}