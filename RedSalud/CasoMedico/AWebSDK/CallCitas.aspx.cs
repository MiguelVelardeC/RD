using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xunit;
using Artexacta.App.Utilities.SystemMessages;
public partial class CasoMedico_AWebSDK_CallCitas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int casoidval;
            var casoid = Session["CASOTELE"];
            if (casoid != null)
            {
                try
                {
                    casoidval = Convert.ToInt32(Session["CasoId"]);
                    var _appid = Session["_appid"];
                    var _appcertificate = Session["_appCertificate"];
                    var _token = Session["_token"];
                    var _roomname = Session["_roomname"];
                    appid.Value = _appid.ToString();
                    appcertificate.Value = _appcertificate.ToString();
                    token.Value = _token.ToString();
                    channel.Value = _roomname.ToString();
                }
                catch 
                {
                    //SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso");

                    Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx", true);
                }
            }
            else 
            {
                //SystemMessages.DisplaySystemErrorMessage("Ingreso Restringido");
                Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx", true);
            }
            
        }
    }

  

}