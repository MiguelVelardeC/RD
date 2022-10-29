using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Caso.BLL;
using log4net;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.RedMedica;
using Artexacta.App.RedMedica.BLL;
using Artexacta.App.Estudio.BLL;
using Artexacta.App.Receta.BLL;
using Artexacta.App.Receta;
using Artexacta.App.Estudio;
using Artexacta.App.Derivacion;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.Internacion;
using Artexacta.App.Internacion.BLL;
using Telerik.Web.UI;
using Artexacta.App.Especialidad;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Configuration;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Caso;
using Artexacta.App.Proveedor.BLL;
using Artexacta.App.Proveedor;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Plan.BLL;
using System.Text.RegularExpressions;
using Artexacta.App.Odontologia;
using Artexacta.App.Odontologia.BLL;
using Artexacta.App.CLAPrestacionOdontologica;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Asegurado.BLL;

public partial class CasoMedico_CasoMedicoDetalle : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public string defaultJSonAntecedentes = "";
    public string defaultJSonAntAlergicos = "";
    public string defaultJSonExFisicos = "";
    public bool IsOdontologia
    {
        get { return MotivoConsultaIdHF.Value == "ODONTO"; }
    }

    protected void Page_Load ( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
        }
    }
    //este talvez obtener del queryString para q se pueda refrescar la pagina
    protected void ProcessSessionParameters ()
    {
        int CasoId = 0;//ACCID 9140, EMERG 529, ENFER 755, ODONTO 8652, RECASO 3510
        if (Session["CasoId"] != null && !string.IsNullOrEmpty(Session["CasoId"].ToString()))
        {
            try
            {
                CasoId = Convert.ToInt32(Session["CasoId"]);
                GetPolizaDetails(CasoId);
            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso");
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);
                Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx");
                return;
            }
        }
        CasoIdHF.Value = CasoId.ToString();
        Session["CasoId"] = null;
        if (CasoId <= 0)
        {
            Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx", true);
        }

        string CitaId = "";
        if (Session["CitaId"] != null && !string.IsNullOrEmpty(Session["CitaId"].ToString()))
        {
            CitaId = Session["CitaId"].ToString();
        }
        CitaIdHF.Value = string.IsNullOrWhiteSpace(CitaId) ? "0" : CitaId;
        Session["CitaId"] = null;
    }

    protected void GetPolizaDetails ( int CasoId )
    {
        try
        {
            Poliza objPoliza = PolizaBLL.GetPolizaDetailsByCasoId(CasoId);

            this.ClienteIdHF.Value = objPoliza.ClienteId.ToString();
            this.PacienteIdHF.Value = objPoliza.PacienteId.ToString();

            decimal MontoMinimoPaciente = objPoliza.MontoTotal - objPoliza.GastoTotal;
            decimal Porcentaje = objPoliza.GastoTotal / objPoliza.MontoTotal * 100;

            decimal MontoMinimoEnPoliza = Configuration.GetMontoMinimoEnPoliza();
            decimal PorcentajeSiniestralidadAlerta = Configuration.GetPorcentajeSiniestralidadAlerta();

            this.AseguradoLabel.Text = objPoliza.NombreJuridicoCliente + "  -  " + objPoliza.CodigoAsegurado;
            this.AseguradoIdHF.Value = objPoliza.AseguradoId.ToString();
            this.PolizaLabel.Text = objPoliza.NumeroPoliza + "  -  " + objPoliza.NombrePlan;
            this.FechaFinLabel.Text = objPoliza.FechaFinString;
            if (objPoliza.MontoTotal > 0)
            {
                SinistralidadMonto.Visible = true;
                SiniestralidadPlan.Visible = false;
                this.SiniestralidadLabel.Text = objPoliza.Siniestralidad;
                if (MontoMinimoPaciente >= 0 && objPoliza.MontoTotal >= 0)
                {
                    if (MontoMinimoPaciente <= MontoMinimoEnPoliza)
                    {
                        this.DivSiniestralidad.Attributes.Add("Class", "MontoMinimoEnPolizaInferior");
                    }
                    else
                        if (Porcentaje >= PorcentajeSiniestralidadAlerta)
                            this.DivSiniestralidad.Attributes.Add("Class", "PorcentajeSiniestralidadAlerta");
                }
            }
            else
            {
                SinistralidadMonto.Visible = false;
                SiniestralidadPlan.Visible = true;
                AseguradoIdHF.Value = objPoliza.AseguradoId.ToString();
                PlanUsoRepeater.DataSource = PlanBLL.getPlanUseForAsegurado(objPoliza.AseguradoId);
                PlanUsoRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de la Póliza.");
            log.Error("Function PolizaODS_Selected on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void PacienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Paciente.");
            e.ExceptionHandled = true;
        }
        else
        {
            if (PacienteBLL.isCritico(Convert.ToInt32(PacienteIdHF.Value)))
            {
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
        }
    }
    protected void PacienteODS_Updated ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Paciente.");
            e.ExceptionHandled = true;
        }
    }
    protected void RecetaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Recetas.");
            log.Error("Function RecetaODS_Selected on page Historial.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CasoDetalleODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Casos en detalle por aprobar.");
            log.Error("Function CasoDetalleODS_Selected on page Historial.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void MedicamentoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Medicamentos.");
            e.ExceptionHandled = true;
        }
    }

    protected void CasoODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        try
        {
            Caso caso = (Caso)e.ReturnValue;
            if (caso != null)
            {
                caso.CitaId = int.Parse("0" + CitaIdHF.Value);
            }
        }
        catch (Exception q)
        {
            log.Error( "Error al obtener CitaId en el Caso medico.", q);
        }
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Caso Medico.");
            e.ExceptionHandled = true;
        }
    }
    protected string GetCasoFVControlClientID(string id)
    {
        string clientID = "";
        if (CasoFV.FindControl(id) != null)
            clientID = CasoFV.FindControl(id).ClientID;
        return clientID;
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