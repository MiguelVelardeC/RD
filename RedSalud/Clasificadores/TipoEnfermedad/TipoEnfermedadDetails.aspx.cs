using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.TipoEnfermedad;
using Artexacta.App.TipoEnfermedad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_TipoEnfermedad_TipoEnfermedadDetails : System.Web.UI.Page
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
        if (Session["TipoEnfermedadId"] != null && !string.IsNullOrEmpty(Session["TipoEnfermedadId"].ToString()))
        {
            int tipoEnfermedadId = 0;
            try
            {
                tipoEnfermedadId = Convert.ToInt32(Session["TipoEnfermedadId"].ToString());
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el TipoEnfermedadId de Session['TipoEnfermedadId']", ex);
            }
            TipoEnfermedadHiddenLabel.Text = tipoEnfermedadId.ToString();
            if(tipoEnfermedadId > 0)
                LoadFormData(tipoEnfermedadId);
            Session["TipoEnfermedadId"] = null;
            return;
        }
        TipoEnfermedadHiddenLabel.Text = "0";
    }

    private void LoadFormData(int tipoEnfermedadId)
    {
        TitleLabel.Text = "Editar Tipo de Enfermedad";
        try
        {
            TipoEnfermedad obj = TipoEnfermedadBLL.GetTipoEnfermedadByTipoEnfermedadId(tipoEnfermedadId);
            NombreTextBox.Text = obj.Nombre;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos del Tipo de Enfermedad Seleccionado", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener los datos del Tipo de Enfermedad Seleccionado");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            int tipoEnfermedadId = Convert.ToInt32(TipoEnfermedadHiddenLabel.Text);
            string nombre = NombreTextBox.Text;
            if (tipoEnfermedadId == 0)
            {
                TipoEnfermedadBLL.InsertTipoEnfermedad(nombre);
                SystemMessages.DisplaySystemMessage("El nuevo Tipo de Enfermedad se guardó correctamente");
            }
            else
            {
                TipoEnfermedadBLL.UpdateTipoEnfermedad(tipoEnfermedadId, nombre);
                SystemMessages.DisplaySystemMessage("El Tipo de Enfermedad se guardó correctamente");
            }            
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar el Tipo de Enfermedad", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar el Tipo de Enfermedad");
            return;
        }
        Response.Redirect("~/Clasificadores/TipoEnfermedad/TipoEnfermedadList.aspx", true);
    }
}