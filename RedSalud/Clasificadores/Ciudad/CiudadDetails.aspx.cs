using Artexacta.App.Ciudad;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clasificadores_Ciudad_CiudadDetails : System.Web.UI.Page
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
        string CiudadId = "";
        if (Session["CiudadId"] != null && !string.IsNullOrEmpty(Session["CiudadId"].ToString()))
        {
            try
            {
                CiudadId = Session["CiudadId"].ToString();
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el CiudadId de Session['CiudadId']", ex);
            }
        }
        CiudadIdHF.Value = CiudadId.ToString();
        Session["CiudadId"] = null;

        if (!string.IsNullOrEmpty(CiudadId))
            LoadFormData(CiudadId);
    }

    private void LoadFormData(string CiudadId)
    {
        TitleLabel.Text = "Editar la Ciudad";
        try
        {
            Ciudad obj = CiudadBLL.GetCiudadDetails(CiudadId);
            CiudadIdTxt.Text = obj.CiudadId;
            CiudadIdTxt.Enabled = false;
            NombreTxt.Text = obj.Nombre;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos de la Ciudad Seleccionada", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener los datos de la Ciudad Seleccionada");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            string CiudadId = CiudadIdHF.Value;
            string nombre = NombreTxt.Text;
            if (string.IsNullOrEmpty(CiudadId))
            {
                CiudadBLL.InsertCiudad(CiudadIdTxt.Text, nombre);
                SystemMessages.DisplaySystemMessage("La nueva Ciudad se guardó correctamente");
            }
            else
            {
                CiudadBLL.UpdateCiudad(CiudadId, nombre);
                SystemMessages.DisplaySystemMessage("La Ciudad se guardó correctamente");
            }
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar la Ciudad ", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar la Ciudad ");
            return;
        }
        Response.Redirect("~/Clasificadores/Ciudad/CiudadList.aspx", true);
    }
}