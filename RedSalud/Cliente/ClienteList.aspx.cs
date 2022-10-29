using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cliente_ClienteList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Clientes.");
            log.Error("Ocurrió un error al Obtener la lista de Clientes", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int RedClienteId = Convert.ToInt32(e.CommandArgument.ToString());
                if (RedClienteBLL.DeleteRedCliente(RedClienteId))
                    SystemMessages.DisplaySystemMessage("El Cliente seleccionado se eliminó correctamente.");
                else
                    SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al intentar eliminar el Cliente seleccionado.");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el RedClienteId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al eliminar el Cliente seleccionado.");
            }
            ClienteGV.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["ClienteId"] = e.CommandArgument.ToString();
            Response.Redirect("~/Cliente/ClienteDetails.aspx");

        }
    }
}