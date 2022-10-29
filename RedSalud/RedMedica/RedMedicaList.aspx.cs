using Artexacta.App.RedMedica.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RedMedica_RedMedicaList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RedMedicaGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int RedMedicaId = Convert.ToInt32(e.CommandArgument.ToString());
                if (RedMedicaBLL.DeleteRedMedica(RedMedicaId))
                    SystemMessages.DisplaySystemMessage("La Red Medica seleccionada se eliminó correctamente.");
                else
                    SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al intentar eliminar la Red Medica seleccionada.");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el RedMedicaId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al eliminar la Red Medica seleccionada.");
            }
            RedMedicaGV.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["RedMedicaId"] = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("~/RedMedica/RedMedicaDetails.aspx");

        }
    }
    
    protected void RedMedicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Redes Medicas");
            log.Error("Ocurrio un error al Obtener la lista de Redes Medicas", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}