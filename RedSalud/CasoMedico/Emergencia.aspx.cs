using Artexacta.App.Caso;
using Artexacta.App.Caso.BLL;
using Artexacta.App.Emergencia;
using Artexacta.App.Emergencia.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.EnfermedadCronica.BLL;

public partial class CasoMedico_Emergencia : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
        }
        EmergenciaSaveLB.Visible = UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name) > 0;
        Message2Label.Visible = !EmergenciaSaveLB.Visible;
        this.NewEnfermedadCronicaPanel.Visible = EmergenciaSaveLB.Visible;
        if (!EmergenciaSaveLB.Visible)
        {
            cssEditable.Text = "<style>" +
                             "  #" + PacienteCronicoPanel.ClientID + " input" +
                             "  {" +
                             "      display: none!important;" +
                             "  }" +
                             "</style>";
        }
    }

    protected void ProcessSessionParameters()
    {
        int CasoId = 0;
        if (Session["CasoId"] != null && !string.IsNullOrEmpty(Session["CasoId"].ToString()))
        {
            try
            {
                CasoId = Convert.ToInt32(Session["CasoId"]);
                GetDetailsCasoMedico(CasoId);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["BolsaId"]);
            }
            switch (CasoBLL.GetMotivoConsultaId(CasoId))
            {
                case "ENFER":
                    Response.Redirect("~/CasoMedico/Enfermeria.aspx");
                    return;
                case "ACCID":
                    Response.Redirect("~/CasoMedico/CasoMedicoDetalle.aspx");
                    return;
            }
        }
        CasoIdHF.Value = CasoId.ToString();
        Session["CasoId"] = null;

        if (Session["RETURNTO"] != null && !string.IsNullOrEmpty(Session["RETURNTO"].ToString()))
        {
            returnHL.CommandArgument = Session["RETURNTO"].ToString();
            Session["RETURNTO"] = returnHL.CommandArgument;
        }
    }
    
    protected void GetDetailsCasoMedico(int CasoId)
    {
        Caso objCaso = CasoBLL.GetCasoByCasoId(CasoId);
        if (objCaso != null)
        {
            this.CodigoCasoLabel.Text = objCaso.CodigoCaso;
            //nombre del paciente/ numero de 
            Poliza objPoliza = PolizaBLL.GetPolizaByPolizaId(objCaso.PolizaId);
            if (objPoliza != null)
            {
                this.AseguradoIdHF.Value = objPoliza.AseguradoId.ToString();
                this.NombreAseguradoraLabel.Text = objPoliza.NombreJuridicoCliente;
                this.CodigoALabel.Text = objPoliza.CodigoAsegurado;
                this.NombreALabel.Text = objPoliza.NombreCompletoPaciente;
            }

            //if Dirty =true y hay que hacer update del caso
            this.DirtyHF.Value = objCaso.Dirty.ToString();
            PresionArterialTxt.Text = objCaso.PresionArterial;
            TemperaturaTxt.Text = objCaso.Temperatura;
            Pulsotxt.Text = objCaso.Pulso;
            FrecuenciaCTxt.Text = objCaso.FrecuenciaCardiaca;
            if (objCaso.CasoCritico)
            {
                CasoMedicoTitle.Text += " <span style='color: #f00;'>[Paciente con enfermedades Crónicas]</span>";
                PacienteCronicoPanel.Visible = true;
                CasoCriticoEnfermedadCronica.Checked = objCaso.CasoCritico;
                cssCritic.Text = "<style>" +
                                 "  .critic legend" +
                                 "  {" +
                                 "      color: #f00;" +
                                 "  }" +
                                 "  .critic fieldset" +
                                 "  {" +
                                 "      border-color: #F00;" +
                                 "  }" +
                                 "</style>";
            }

            if (!string.IsNullOrWhiteSpace(objCaso.EnfermedadId))
            {
                RadComboBoxItem item = new RadComboBoxItem("[" + objCaso.EnfermedadId + "] " + objCaso.Enfermedad, objCaso.EnfermedadId);
                EnfermedadesComboBox.Items.Add(item);
                item.Selected = true;
            }

            if (string.IsNullOrWhiteSpace(objCaso.DiagnosticoPresuntivo))
            {
                DiagnosticoPresuntivoTxt.Style.Add("display", "none");
                OtroCheckBox.Checked = false;
            }
            else
            {
                DiagnosticoPresuntivoTxt.Text = objCaso.DiagnosticoPresuntivo;
                DiagnosticoPresuntivoTxt.Style.Remove("display");
                OtroCheckBox.Checked = true;
            }
            
            //si no es dirty entonces ya tiene la emergencia, realizar update
            if (!objCaso.Dirty)
            {
                Emergencia objEmergencia = EmergenciaBLL.getEmergenciaDetailsByCasoId(CasoId);
                this.EmergenciaIdHF.Value = objEmergencia.EmergenciaId.ToString();
                FileManager.ObjectId = objEmergencia.EmergenciaId;
                FileManagerPanel.Visible = true;
                this.ObservacionEmergenciaTxt.Text = objEmergencia.Observaciones;
                if (objEmergencia.GastoId > 0)
                {
                    this.EmergenciaSaveLB.Visible = false;
                    this.NewEnfermedadCronicaPanel.Visible = false;
                    this.EnfermedadesCronicasRepeater.FindControl("DeleteEnfermedadCronicaImageButton").Visible = false;
                    this.MessageLabel.Visible = true;
                }
            }
            else
            {
                FileManagerPanel.Visible = false;
            }
        }
    }
    
    protected void EmergenciaSaveLB_Click(object sender, EventArgs e)
    {
        int EmergenciaId = 0;
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            int ProveedorId = UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name);

            if (!string.IsNullOrEmpty(EmergenciaIdHF.Value))
                EmergenciaId = Convert.ToInt32(EmergenciaIdHF.Value);
            string PresionArterial = PresionArterialTxt.Text;
            string Temperatura = TemperaturaTxt.Text;
            string Pulso = Pulsotxt.Text;
            string FrecuenciaCardiaca = FrecuenciaCTxt.Text; 
            string DiagnosticoPresuntivo = DiagnosticoPresuntivoTxt.Text;
            string EnfermedadId = EnfermedadesComboBox.SelectedValue;
            if (OtroCheckBox.Checked)
                EnfermedadId = "";
            else
                DiagnosticoPresuntivo = "";

            if (EmergenciaId <= 0)
            {
                int NewEmergenciaId = EmergenciaBLL.InsertEmergencia(CasoId, ProveedorId, ObservacionEmergenciaTxt.Text,
                    PresionArterial, Pulso, Temperatura, FrecuenciaCardiaca, DiagnosticoPresuntivo, EnfermedadId);
                if (NewEmergenciaId <= 0)
                    SystemMessages.DisplaySystemErrorMessage("Se inserto la nueva emergencia correctamente, pero retorno valor incompleto.");
                else
                    SystemMessages.DisplaySystemMessage("Emergencia insertada correctamente.");
            }
            else
            {
                if (EmergenciaBLL.GetGastoIdEmergencia(EmergenciaId) > 0)
                    SystemMessages.DisplaySystemErrorMessage("No se puede modificar la emergencia por que tiene registros de gastos.");
                else
                {
                    if (EmergenciaBLL.UpdateEmergencia(EmergenciaId, ProveedorId, ObservacionEmergenciaTxt.Text,
                    PresionArterial, Pulso, Temperatura, FrecuenciaCardiaca, DiagnosticoPresuntivo, EnfermedadId))
                        SystemMessages.DisplaySystemMessage("La emergencia fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar la emergencia.");
                }
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la emergencia.");
            log.Error("Function EmergenciaSaveLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
        returnTo();
    }
    private void returnTo ()
    {
        Session["MODE"] = "EMERGENCIA";
        Response.Redirect(returnHL.CommandArgument);
    }
    protected void returnHL_Click ( object sender, EventArgs e )
    {
        returnTo();
    }
    protected void NewEnfermedadCronicaImageButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int aseguradoId = Convert.ToInt32(AseguradoIdHF.Value);
            int enfermedadCronicaId = Convert.ToInt32(EnfermedadCronicaRadComboBox.SelectedValue);
            EnfermedadCronicaBLL.InsertEnfermedadCronicaAsegurado(aseguradoId, enfermedadCronicaId);
            EnfermedadesCronicasRepeater.DataBind();
            EnfermedadCronicaRadComboBox.ClearSelection();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al insertar la enfermedad Crónica.");
            log.Error("Function NewEnfermedadCronicaImageButton_Click on page CasoMedicoLista.aspx", q);
        }
    }
    protected void DeleteEnfermedadCronicaImageButton_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int aseguradoId = Convert.ToInt32(AseguradoIdHF.Value);
            int enfermedadCronicaId = Convert.ToInt32(e.CommandArgument);
            EnfermedadCronicaBLL.DeleteEnfermedadCronicaAsegurado(aseguradoId, enfermedadCronicaId);
            EnfermedadesCronicasRepeater.DataBind();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al eliminar la enfermedad Crónica.");
            log.Error("Function DeleteEnfermedadCronicaImageButton_Command on page CasoMedicoLista.aspx", q);
        }
    }
    protected void EnfermedadesCronicasODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Enfermedades Crónicas.");
            e.ExceptionHandled = true;
        }
    }
}