using Artexacta.App.RedMedica;
using Artexacta.App.RedMedica.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RedMedica_RedMedicaDetails : System.Web.UI.Page
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
        int RedMedicaId = 0;
        if (Session["RedMedicaId"] != null && !string.IsNullOrEmpty(Session["RedMedicaId"].ToString()))
        {
            try
            {
                RedMedicaId = Convert.ToInt32( Session["RedMedicaId"].ToString());
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el RedMedicaId de Session['RedMedicaId']", ex);
            }
        }
        RedMedicaIdHF.Value = RedMedicaId.ToString();
        Session["RedMedicaId"] = null;

        if (RedMedicaId>0)
            LoadFormData(RedMedicaId);
    }

    private void LoadFormData(int RedMedicaId)
    {
        TitleLabel.Text = "Editar Red Medica";
        try
        {
            RedMedica objRedMedica = RedMedicaBLL.getRedMedicaListByRedMedicaId(RedMedicaId);

            this.CodigoTxt.Text = objRedMedica.Codigo;
            this.NombreTxt.Text = objRedMedica.Nombre;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos de la Red Medica Seleccionada", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener los datos de la Red Medica Seleccionada.");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            int RedMedicaId= Convert.ToInt32(RedMedicaIdHF.Value);
            string Codigo = CodigoTxt.Text;
            string nombre = NombreTxt.Text;

            if (RedMedicaId<=0)
            {
                RedMedicaId= RedMedicaBLL.InsertRedMedica(Codigo,nombre);
                if (RedMedicaId <= 0)
                    SystemMessages.DisplaySystemErrorMessage("Error al intentar guardar la nueva Red Medica.");
                else
                    SystemMessages.DisplaySystemMessage("La nueva Red Medica se guardó correctamente");
            }
            else
            {
                if (RedMedicaBLL.UpdateRedMedica(RedMedicaId,Codigo,nombre))
                    SystemMessages.DisplaySystemMessage("La Red Medica se guardó correctamente");
                else
                    SystemMessages.DisplaySystemErrorMessage("Error al guardar la Red Medica.");
            }
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar la Red Medica", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar la Red Medica. ");
            return;
        }
        Response.Redirect("~/RedMedica/RedMedicaList.aspx", true);
    }
}