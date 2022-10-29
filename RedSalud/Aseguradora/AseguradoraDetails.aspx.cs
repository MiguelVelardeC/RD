using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedMedica.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Aseguradora_AseguradoraDetails : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            SetViewMode();
        }
        
    }

    private void SetViewMode()
    {
        int ClienteId = 0;
        try
        {
            ClienteId = Convert.ToInt32(ClienteIdHF.Value);
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            Response.Redirect("~/Aseguradora/AseguradoraList.aspx");
        }
        if (ClienteId <= 0)
            AseguradoraFV.ChangeMode(FormViewMode.Insert);
        else
            AseguradoraFV.ChangeMode(FormViewMode.ReadOnly);
    }

    private void ProcessSessionParameters()
    {
        int ClienteId =0;
        if (Session["ClienteId"] != null && !string.IsNullOrEmpty(Session["ClienteId"].ToString()))
        {
            try
            {
                ClienteId = Convert.ToInt32(Session["ClienteId"].ToString());
                TitleLabel.Text = "Editar Cliente";
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
                log.Error("No se pudo obtener el ClienteId de Session['ClienteId']", ex);
                Response.Redirect("~/Aseguradora/AseguradoraList.aspx");
            }
        }
        ClienteIdHF.Value = ClienteId.ToString();
        Session["ClienteId"] = null;
    }
    
    protected void AseguradoraODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            log.Error("Ocurrió un error al Obtener el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void AseguradoraODS_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Insertar el Cliente.");
            log.Error("Ocurrió un error al Insertar el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            int ClienteId= (int)e.ReturnValue;
            this.ClienteIdHF.Value = ClienteId.ToString();
        }
    }

    protected void AseguradoraODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Modificar el Cliente.");
            log.Error("Ocurrió un error al Modificar el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
    }


    protected void RedMedicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Redes Medicas.");
            log.Error("Ocurrió un error al Obtener la lista de Redes Medicas", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void RedClienteRedMedicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Redes Medicas pertenecientes al Cliente.");
            log.Error("Ocurrió un error al Obtener la lista de Redes Medicas pertenecientes al Cliente.", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void RedClienteRedMedicaGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int ClienteId = Convert.ToInt32(ClienteIdHF.Value);
                int RedMedicaId = Convert.ToInt32(e.CommandArgument.ToString());

                if (RedClienteBLL.DeleteRedClienteRedMedica(ClienteId,RedMedicaId))
                    SystemMessages.DisplaySystemMessage("La Red Medica seleccionada se eliminó del Cliente correctamente.");
                else
                    SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al intentar eliminar la Red Medica seleccionada.");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el RedMedicaId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al eliminar la Red Medica seleccionada.");
            }
            
            GridView RedClienteRedMedicaGV = (GridView)AseguradoraFV.FindControl("RedClienteRedMedicaGV");
            RedClienteRedMedicaGV.DataBind();
        }
    }
    protected void addRedMedicaRedCliente_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList RedMedicaDDL = (DropDownList)AseguradoraFV.FindControl("RedMedicaDDL");
            int RedMedicaId = Convert.ToInt32(RedMedicaDDL.SelectedValue);

            int ClienteId = Convert.ToInt32(ClienteIdHF.Value);
            if (!RedMedicaBLL.ExisteRedClienteRedMedica(ClienteId, RedMedicaId))
                RedClienteBLL.InsertRedClienteRedMedica(ClienteId, RedMedicaId);
            else
                SystemMessages.DisplaySystemErrorMessage("Error, ya existe la Red Medica en el Cliente.");

            GridView RedClienteRedMedicaGV = (GridView)AseguradoraFV.FindControl("RedClienteRedMedicaGV");
            RedClienteRedMedicaGV.DataBind();
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al agregar la la Red Medica al Cliente.");
        }
    }
}