using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Especialidad;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Especialidad_EspecialidadDetails : System.Web.UI.Page
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
        if (Session["EspecialidadId"] != null && !string.IsNullOrEmpty(Session["EspecialidadId"].ToString()))
        {
            int especialidadId = 0;
            try
            {
                especialidadId = Convert.ToInt32(Session["EspecialidadId"].ToString());
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el EspecialidadId de Session['EspecialidadId']", ex);
            }
            EspecialidadHiddenLabel.Text = especialidadId.ToString();
            if (especialidadId > 0)
                LoadFormData(especialidadId);
            Session["EspecialidadId"] = null;
            return;
        }
        EspecialidadHiddenLabel.Text = "0";
    }

    private void LoadFormData(int especialidadId)
    {
        TitleLabel.Text = "Editar Especialidad";
        try
        {
            Especialidad obj = EspecialidadBLL.GetEspecialidadById(especialidadId);
            NombreTextBox.Text = obj.Nombre;
            EstadoDropDownList.SelectedValue = (obj.Estado) ? "1" : "0";
            TiempoAtencion.Text = obj.TiempoAtencion.ToString();
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
            int especialidadId = Convert.ToInt32(EspecialidadHiddenLabel.Text);
            int tiempoAtencion = Convert.ToInt32(TiempoAtencion.Text);
            string nombre = NombreTextBox.Text;
            bool estado = EstadoDropDownList.SelectedValue == "1";
            if (especialidadId == 0)
            {
                EspecialidadBLL.InsertEspecialidad(nombre, estado, tiempoAtencion);
                SystemMessages.DisplaySystemMessage("La nueva especialidad se guardó correctamente");
            }
            else
            {
                EspecialidadBLL.UpdateEspecialidad(especialidadId, nombre, estado, tiempoAtencion);
                SystemMessages.DisplaySystemMessage("La nueva especialidad se guardó correctamente");
            }
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar el Tipo de Enfermedad", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar el Tipo de Enfermedad");
            return;
        }
        Response.Redirect("~/Clasificadores/Especialidad/EspecialidadList.aspx", true);
    }
}