using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.CLAMedicamento.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Medicamento_MedicamentoList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void MedicamentoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Medicamentos");
            log.Error("Ocurrio un error al Obtener la lista de Medicamentos", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void MedicamentoGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int MedicamentoId = Convert.ToInt32(e.CommandArgument);
                CLAMedicamentoBLL.DeleteMedicamento(MedicamentoId);
                SystemMessages.DisplaySystemMessage("El registro seleccionado se eliminó correctamente");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el MedicamentoId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al eliminar el registro seleccionado");
            }
            MedicamentoGridView.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["MedicamentoId"] = e.CommandArgument;
            Response.Redirect("~/Clasificadores/Medicamento/MedicamentoDetails.aspx");

        }
    }
}