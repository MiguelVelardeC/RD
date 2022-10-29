using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.TipoMedicamento;
using Artexacta.App.TipoMedicamento.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Presentacion_PresentacionDetails : System.Web.UI.Page
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
        string TipoMedicamentoId = "";
        if (Session["TipoMedicamentoId"] != null && !string.IsNullOrEmpty(Session["TipoMedicamentoId"].ToString()))
        {
            try
            {
                TipoMedicamentoId = Session["TipoMedicamentoId"].ToString();
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el TipoMedicamentoId de Session['TipoMedicamentoId']", ex);
            }
        }
        TipoMedicamentoHF.Value= TipoMedicamentoId.ToString();
        Session["TipoMedicamentoId"] = null;

        if (!string.IsNullOrEmpty(TipoMedicamentoId))
            LoadFormData(TipoMedicamentoId);
    }

    private void LoadFormData(string TipoMedicamentoId)
    {
        TitleLabel.Text = "Editar Presentación de medicamento";
        try
        {
            TipoMedicamento obj = TipoMedicamentoBLL.GetTipoMedicamentoDetails(TipoMedicamentoId);
            PresentacionIdTxt.Text = obj.TipoMedicamentoId;
            PresentacionIdTxt.Enabled = false;
            NombreTxt.Text = obj.Nombre;
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos del Tipo de Presentacion Seleccionado", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener los datos del Tipo de Presentacion de medicamento Seleccionado");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            string TipoMedicamentoId = TipoMedicamentoHF.Value;
            string nombre = NombreTxt.Text;
            if (string.IsNullOrEmpty(TipoMedicamentoId))
            {
                TipoMedicamentoBLL.InsertTipoMedicamento(PresentacionIdTxt.Text, nombre);
                SystemMessages.DisplaySystemMessage("La nueva Presentación se guardó correctamente");
            }
            else
            {
                TipoMedicamentoBLL.UpdateTipoMedicamento(TipoMedicamentoId, nombre);
                SystemMessages.DisplaySystemMessage("La Presentación se guardó correctamente");
            }
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar la Presentacion ", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar la Presentación ");
            return;
        }
        Response.Redirect("~/Clasificadores/Presentacion/PresentacionList.aspx", true);
    }
}