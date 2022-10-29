using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.TipoMedicamento.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Presentacion_PresentacionList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PresentacionGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                string TipoMedicamentoId = e.CommandArgument.ToString();
                if (TipoMedicamentoBLL.DeleteTipoMedicamento(TipoMedicamentoId))
                    SystemMessages.DisplaySystemMessage("La presentación seleccionada se eliminó correctamente");
                else
                    SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la presentación seleccionada");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el TipoMedicamentoId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un eror al eliminar la presentación seleccionada");
            }
            PresentacionGV.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["TipoMedicamentoId"] = e.CommandArgument.ToString();
            Response.Redirect("~/Clasificadores/Presentacion/PresentacionDetails.aspx");

        }
    }
    protected void TipoMedicamentoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Presentaciones");
            log.Error("Ocurrio un error al Obtener la lista de Presentaciones", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}