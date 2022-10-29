using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Protocolo.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Protocolo_ProtocoloList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ProtocoloDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Protocoloes");
            log.Error("Ocurrio un error al Obtener la lista de Protocoloes", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void ProtocoloGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int protocoloId = Convert.ToInt32(e.CommandArgument);
                ProtocoloBLL.DeleteProtocolo(protocoloId);
                SystemMessages.DisplaySystemMessage("El registro seleccionado se eliminó correctamente");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el ProtocoloId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un eror al eliminar el registro seleccionado");
            }
            ProtocoloGridView.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["ProtocoloId"] = e.CommandArgument;
            Response.Redirect("~/Clasificadores/Protocolo/ProtocoloDetails.aspx");

        }
    }
}