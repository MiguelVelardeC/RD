using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Especialidad_EspecialidadList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void EspecialidadDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Especialidades");
            log.Error("Ocurrio un error al Obtener la lista de Especialidades", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void EspecialidadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int especialidadId = Convert.ToInt32(e.CommandArgument);
                EspecialidadBLL.DeleteEspecialidad(especialidadId);
                SystemMessages.DisplaySystemMessage("El registro seleccionado se eliminó correctamente");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el especialidadId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un eror al eliminar el registro seleccionado");
            }
            EspecialidadGridView.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["EspecialidadId"] = e.CommandArgument;
            Response.Redirect("~/Clasificadores/Especialidad/EspecialidadDetails.aspx");

        }
    }
}