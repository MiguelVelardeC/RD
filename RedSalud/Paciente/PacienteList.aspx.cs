using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;

public partial class Paciente_PacienteList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchLB_Click(object sender, EventArgs e)
    {
        this.PacienteRadGrid.Visible = true;
        PacienteRadGrid.DataBind();
    }
    
    protected void PacienteRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            try
            {
                int PacienteId = Convert.ToInt32(e.CommandArgument);
                Session["PacienteId"] = PacienteId.ToString();

                Response.Redirect("PacienteDetails.aspx");
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PacienteID in PacienteRadGrid_ItemCommand on page PacienteList.aspx", ex);
            }
        }
        if (e.CommandName.Equals("Eliminar"))
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                int PacienteId = Convert.ToInt32(item.GetDataKeyValue("PacienteId").ToString());

                PacienteBLL.DeletePaciente(PacienteId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el paciente seleccionado");
                PacienteRadGrid.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PacienteID in PacienteRadGrid_ItemCommand on page PacienteList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el paciente, verifique q");
            }
        }
    }
    
    protected void PacienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Pacientes.");
            log.Error("Function PacienteODS_Selected on page PacienteList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}