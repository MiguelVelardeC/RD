using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.User.BLL;
using Telerik.Web.UI;

public partial class Consolidacion_Consolidados : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.CiudadHF.Value = UserBLL.GetCuidadIdByUsername(HttpContext.Current.User.Identity.Name);
    }

    protected void ProveedorODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de los Proveedores.");
            log.Error("Function ProveedorODS_Selected on page Consolidados.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }


    protected void SearchLB_Click(object sender, EventArgs e)
    {
        this.ConsolidadosRadGrid.Visible = true;
        this.ConsolidadosRadGrid.DataBind();
    }
    protected void ConsolidadosODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de las Consolidaciones.");
            log.Error("Function ConsolidadosODS_Selected on page Consolidados.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ConsolidadosRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            if (e.CommandName.Equals("VerReporte"))
            {
                int ConsolidacionId = Convert.ToInt32(e.CommandArgument.ToString());
                if (ConsolidacionId <= 0)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error, no se pudo obtener obtener la consolidacion, actualize la página e intentelo de nuevo.");
                    return;
                }
                Session["ConsolidacionId"] = ConsolidacionId.ToString();
                Response.Redirect("~/Reportes/ReporteConsolidado.aspx");
            }
        }
    }
}