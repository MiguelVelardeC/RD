using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.TipoEnfermedad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_TipoEnfermedad_TipoEnfermedadList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TipoEnfermedadDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Tipos de Enfermedades");
            log.Error("Ocurrio un error al Obtener la lista de Tipos de Enfermedades", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void TipoEnfermedadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int tipoEnfermedadId = Convert.ToInt32(e.CommandArgument);
                TipoEnfermedadBLL.DeleteTipoEnfermedad(tipoEnfermedadId);
                SystemMessages.DisplaySystemMessage("El registro seleccionado se eliminó correctamente");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el tipoEnfermedadId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un eror al eliminar el registro seleccionado");
            }
            TipoEnfermedadGridView.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["TipoEnfermedadId"] = e.CommandArgument;
            Response.Redirect("~/Clasificadores/TipoEnfermedad/TipoEnfermedadDetails.aspx");

        }
    }
}