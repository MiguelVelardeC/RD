using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class CasoMedico_OpenRegRef : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MODE"] = Request.QueryString["MODE"];
        Response.Redirect("~/CasoMedico/CasoMedicoRegistro.aspx", true);

    }


  
}