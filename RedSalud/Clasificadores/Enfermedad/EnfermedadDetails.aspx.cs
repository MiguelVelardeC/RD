using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Enfermedad;
using Artexacta.App.Enfermedad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Enfermedad_EnfermedadDetails : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
        }
    }

    private void ProcessSessionParameters()
    {
        if (Session["EnfermedadId"] != null && !string.IsNullOrEmpty(Session["EnfermedadId"].ToString()))
        {
            try
            {
                EnfermedadHiddenLabel.Text = Session["EnfermedadId"].ToString();
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el EnfermedadId de Session['EnfermedadId']", ex);
            }
            if (!string.IsNullOrWhiteSpace(EnfermedadHiddenLabel.Text))
                LoadFormData();
            Session["EnfermedadId"] = null;
            return;
        }
    }

    private void LoadFormData()
    {
        TitleLabel.Text = "Editar Enfermedad";
        try
        {
            Enfermedad obj = EnfermedadBLL.GetEnfermedadByID(EnfermedadHiddenLabel.Text);
            CodigoTextBox.Text = obj.EnfermedadId;
            NombreTextBox.Text = obj.Nombre;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos del  de Enfermedad Seleccionado", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al obtener los datos de la Enfermedad Seleccionada");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            string nombre = NombreTextBox.Text;
            string codigo = CodigoTextBox.Text;
            if (string.IsNullOrWhiteSpace(EnfermedadHiddenLabel.Text))
            {
                EnfermedadBLL.InsertEnfermedad(codigo, nombre);
                SystemMessages.DisplaySystemMessage("La nueva Enfermedad se guardó correctamente");
            }
            else
            {
                EnfermedadBLL.UpdateEnfermedad(EnfermedadHiddenLabel.Text, codigo, nombre);
                SystemMessages.DisplaySystemMessage("La Enfermedad se guardó correctamente");
            }            
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar el  de Enfermedad", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al guardar la Enfermedad");
            return;
        }
        Response.Redirect("~/Clasificadores/Enfermedad/EnfermedadList.aspx", true);
    }
}