using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Enfermedad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Enfermedad_EnfermedadList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void EnfermedadDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Enfermedades");
            log.Error("Ocurrio un error al Obtener la lista de Enfermedades", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void EnfermedadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                string EnfermedadId = e.CommandArgument.ToString();
                EnfermedadBLL.DeleteEnfermedad(EnfermedadId);
                SystemMessages.DisplaySystemMessage("El registro seleccionado se eliminó correctamente");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el EnfermedadId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al eliminar el registro seleccionado");
            }
            EnfermedadGridView.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["EnfermedadId"] = e.CommandArgument;
            Response.Redirect("~/Clasificadores/Enfermedad/EnfermedadDetails.aspx");

        }
    }
}