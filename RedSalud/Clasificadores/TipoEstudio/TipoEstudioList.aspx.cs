using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.TipoEstudio.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_TipoEstudio_TipoEstudioList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TipoEstudioDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Tipos de Estudioes");
            log.Error("Ocurrio un error al Obtener la lista de Tipos de Estudioes", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void TipoEstudioGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int tipoEstudioId = Convert.ToInt32(e.CommandArgument);
                TipoEstudioBLL.DeleteTipoEstudio(tipoEstudioId);
                SystemMessages.DisplaySystemMessage("El registro seleccionado se eliminó correctamente");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el tipoEstudioId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un eror al eliminar el registro seleccionado");
            }
            TipoEstudioGridView.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["TipoEstudioId"] = e.CommandArgument;
            Response.Redirect("~/Clasificadores/TipoEstudio/TipoEstudioDetails.aspx");

        }
    }
}