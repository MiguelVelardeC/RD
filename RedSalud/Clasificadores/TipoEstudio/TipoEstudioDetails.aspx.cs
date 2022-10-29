using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.TipoEstudio;
using Artexacta.App.TipoEstudio.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_TipoEstudio_TipoEstudioDetails : System.Web.UI.Page
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
        if (Session["TipoEstudioId"] != null && !string.IsNullOrEmpty(Session["TipoEstudioId"].ToString()))
        {
            int tipoEstudioId = 0;
            try
            {
                tipoEstudioId = Convert.ToInt32(Session["TipoEstudioId"].ToString());
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el TipoEstudioId de Session['TipoEstudioId']", ex);
            }
            TipoEstudioHiddenLabel.Text = tipoEstudioId.ToString();
            if(tipoEstudioId > 0)
                LoadFormData(tipoEstudioId);
            Session["TipoEstudioId"] = null;
            return;
        }
        TipoEstudioHiddenLabel.Text = "0";
    }

    private void LoadFormData(int tipoEstudioId)
    {
        TitleLabel.Text = "Editar Tipo de Estudio";
        try
        {
            TipoEstudio obj = TipoEstudioBLL.GetTipoEstudioById(tipoEstudioId);
            NombreTextBox.Text = obj.Nombre;
            ParentDDL.DataBind();
            ListItem item = ParentDDL.Items.FindByValue(obj.PadreId.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos del Tipo de Estudio Seleccionado", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener los datos del Tipo de Estudio Seleccionado");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            int tipoEstudioId = Convert.ToInt32(TipoEstudioHiddenLabel.Text);
            string nombre = NombreTextBox.Text;
            if (tipoEstudioId == 0)
            {
                TipoEstudioBLL.InsertTipoEstudio(nombre, Convert.ToInt32(ParentDDL.SelectedValue));
                SystemMessages.DisplaySystemMessage("El nuevo Tipo de Estudio se guardó correctamente");
            }
            else
            {
                TipoEstudioBLL.UpdateTipoEstudio(tipoEstudioId, nombre, Convert.ToInt32(ParentDDL.SelectedValue));
                SystemMessages.DisplaySystemMessage("El Tipo de Estudio se guardó correctamente");
            }            
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar el Tipo de Estudio", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar el Tipo de Estudio");
            return;
        }
        Response.Redirect("~/Clasificadores/TipoEstudio/TipoEstudioList.aspx", true);
    }
    protected void ParentODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los Tipos de estudio para agrupar.");
            e.ExceptionHandled = true;
            log.Error("Error on ParentODS_Selected on page TipoEstudioDetails.aspx", e.Exception);
        }
    }
}