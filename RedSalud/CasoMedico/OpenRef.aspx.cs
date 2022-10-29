using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CasoMedico_OpenRef : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MODE"] = Request.QueryString["MODE"];
        Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx", true);
    }
}