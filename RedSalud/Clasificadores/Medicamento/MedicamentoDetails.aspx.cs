using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.CLAMedicamento;
using Artexacta.App.CLAMedicamento.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Medicamento_MedicamentoDetails : System.Web.UI.Page
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
        if (Session["MedicamentoId"] != null && !string.IsNullOrEmpty(Session["MedicamentoId"].ToString()))
        {
            try
            {
                MedicamentoHiddenLabel.Text = Session["MedicamentoId"].ToString();
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el MedicamentoId de Session['MedicamentoId']", ex);
            }
            if (!string.IsNullOrWhiteSpace(MedicamentoHiddenLabel.Text))
                LoadFormData();
            Session["MedicamentoId"] = null;
            return;
        }
    }

    private void LoadFormData()
    {
        TitleLabel.Text = "Editar Medicamento";
        try
        {
            int MedicamentoId = Convert.ToInt32(MedicamentoHiddenLabel.Text);
            Medicamento obj = CLAMedicamentoBLL.GetMedicamentoDetails(MedicamentoId);
            NombreTextBox.Text = obj.Nombre;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos del  de Medicamento Seleccionado", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al obtener los datos del Medicamento Seleccionado");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            string nombre = NombreTextBox.Text;
            int MedicamentoId = Convert.ToInt32(MedicamentoHiddenLabel.Text);
            if (MedicamentoId <= 0)
            {
                MedicamentoHiddenLabel.Text = CLAMedicamentoBLL.InsertMedicamento(nombre).ToString();
                SystemMessages.DisplaySystemMessage("El nuevo Medicamento se guardó correctamente");
            }
            else
            {
                CLAMedicamentoBLL.UpdateMedicamento(MedicamentoId, nombre);
                SystemMessages.DisplaySystemMessage("El Medicamento se guardó correctamente");
            }            
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar el  de Medicamento", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al guardar el Medicamento");
            return;
        }
        Response.Redirect("~/Clasificadores/Medicamento/MedicamentoList.aspx", true);
    }
}