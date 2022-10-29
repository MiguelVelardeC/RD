using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Caso.BLL;
using Artexacta.App.Caso.VideoLllamadaTele.BLL;
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
using Artexacta.App.EnfermedadCronica.BLL;
using Artexacta.App.Emergencia;
using Artexacta.App.Emergencia.BLL;
using Artexacta.App.Paciente;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using TheArtOfDev.HtmlRenderer.Core;
using System.Text;
using Artexacta.App.Utilities.Text;
using Artexacta.App.TipoMedicamento;
using Artexacta.App.RedClientePrestaciones.BLL;
using Artexacta.App.RedClientePrestaciones;
using Artexacta.App.Internacion_Cirugia.BLL;
using Artexacta.App.Internacion_Cirugia;
using Artexacta.App.Internacion_Internacion.BLL;
using Artexacta.App.Internacion_Internacion;
using Artexacta.App.CoPagos.BLL;
using Artexacta.App.CoPagos;
using Artexacta.App.ProveedorPrestaciones.BLL;
using Artexacta.App.ProveedorPrestaciones;
using Artexacta.App.CasoEmergecia;
using Artexacta.App.CasoEmergecia.BLL;
using Artexacta.App.Validacion.BLL;
using Artexacta.App.Documents.BLL;
using Artexacta.App.Documents.FileUpload;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Medico;
using Artexacta.App.Utilities.Document;
using Artexacta.App.Documents;
public partial class CasoMedico_CasoMedicoDetalle : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private DateTime FechaEstado;
    private ValidacionBLL objValidacion = new ValidacionBLL();
    private string isExport = "";
    private static string ToolTipCannotDeleteHaveGasto = "No se puede eliminar por que tiene registros de gastos.";
    private static string ToolTipCannotUpdateByapproved = "No se puede Modificar por que ya esta aprobado.";
    private static string UrlHistorial = "Historial.aspx";
    public string defaultJSonAntecedentes = "";
    public string defaultJSonAntAlergicos = "";
    public string defaultJSonExFisicos = "";
    public bool IsOdontologia
    {
        get { return MotivoConsultaIdHF.Value == "ODONTO"; }
    }
    public bool IsEmergencia
    {
        get { return MotivoConsultaIdHF.Value == "EMERG"; }
    }
    public bool IsFemenino
    {
         
          get { return GeneroHF.Value.Contains("True"); }
        
    }
    public void ValorGenero()
    {
        try {
            HiddenField VGenero = (HiddenField)PacienteFV.FindControl("GeneroHF");
            if (VGenero != null)
                GeneroHF.Value = VGenero.Value;
        }
        catch(Exception )
        {

        }
    }
    public bool IsApprovedEdit { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      
        //DateTime fecah= this.FechaCreacion2.SelectedDate.Value;
        if (!IsPostBack)
        {

            ProcessSessionParameters();// VIDEOLLAMADA: RECUPERANDO EL MOTIVO DE CONSULTA

            cargarDdlsTipoMedicamento();
        }
      
        Verificarespecialidad();
        if (string.IsNullOrEmpty(ModeHF.Value))
            this.AdminGastosLB.Visible = LoginSecurity.IsUserAuthorizedPermission("MANAGE_GASTOS_CASOS");

        SetViewMode();

        validateMotivoConsulta();// VIDEOLLAMADA: Agregando validación para habilitar el botón.

        FileManager.OnListFileChange += new UserControls_FileManager.OnListFileChangeDelegate(FileManager_FileSave);
        string s = (string.IsNullOrEmpty(PacienteIdHF.Value) ? "" : PacienteIdHF.Value);
        int pacienteId = 0;
        if (int.TryParse(s, out pacienteId))
        {
            Paciente p = PacienteBLL.GetPacienteByPacienteId(pacienteId);
            string edadRAW = p.Edad;
            string Edad;
            if (edadRAW.Length > 0)
                Edad = edadRAW.Substring(0, edadRAW.Length - 4);
            else
                Edad = "0";
            int intEdad = 0;
            if (int.TryParse(Edad, out intEdad))
            {
                PacienteEdad.Value = intEdad.ToString();
            }

            defaultJSonAntecedentes = p.Antecedentes;
        }


        //Sets Print Button Visible LEAVE THIS AS THE LAST LINE TO BE EXECUTED (Before SetViewMode)
        if (CasoFV.CurrentMode == FormViewMode.ReadOnly || IsApprovedEdit)
        {
            cmdImpresionHistoria.Visible = true;
        }

        bool puedeCrearDerivacion = true;

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_DERIVAR_CASOS");
            puedeCrearDerivacion = true;
        }
        catch (Exception)
        {
            puedeCrearDerivacion = false;
        }
        ValorGenero();

        NewDerivacion.Visible = puedeCrearDerivacion;

        HabilitarValidacion();

        //EstudioRadGrid.MasterTableView.GetColumn("AprobarColumn").Visible = LoginSecurity.IsUserAuthorizedPermission("APPROVAL_CASOS");
        //DerivacionRadGrid.MasterTableView.GetColumn("AprobarColumn").Visible = LoginSecurity.IsUserAuthorizedPermission("APPROVAL_CASOS");
        //InternacionRadGrid.MasterTableView.GetColumn("AprobarColumn").Visible = LoginSecurity.IsUserAuthorizedPermission("APPROVAL_CASOS");

    }

    private void cargarDdlsTipoMedicamento()
    {
        List<TipoMedicamento> listaTipoMedicamento = Artexacta.App.TipoMedicamento.BLL.TipoMedicamentoBLL.GetTipoMedicamentoList();
        TipoMedicamento1DDL.DataSource = listaTipoMedicamento;
        TipoMedicamento1DDL.DataBind();
        TipoMedicamento2DDL.DataSource = listaTipoMedicamento;
        TipoMedicamento2DDL.DataBind();
        TipoMedicamento3DDL.DataSource = listaTipoMedicamento;
        TipoMedicamento3DDL.DataBind();
        TipoMedicamento4DDL.DataSource = listaTipoMedicamento;
        TipoMedicamento4DDL.DataBind();
        TipoMedicamentoDDL.DataSource = listaTipoMedicamento;
        TipoMedicamentoDDL.DataBind();
    }

    private void SetViewMode()
    {
        string Mode = ModeHF.Value;

        if (!string.IsNullOrEmpty(Mode))
        {
            CasoFV.ChangeMode(FormViewMode.Edit);
            AdminGastosLB.Visible = false;
            newHL.Visible = false;
            returnHL.Visible = false;
            odontologiaHL.Visible = false;
            recetaHL.Visible = false;
            ExComplementarioHL.Visible = false;
            EspecilistaHL.Visible = false;
            InternacionHL.Visible = false;

            this.OdontologiaRT.Enabled = false;
            this.RecetaRT.Enabled = false;
            this.EstudioRT.Enabled = false;
            this.DerivacionRT.Enabled = false;
            this.InternacionRT.Enabled = false;

            this.OdontologiaRPV.Visible = false;
            this.RecetaRPV.Visible = false;
            this.ExComplementarioRPV.Visible = false;
            this.DerivacionRPV.Visible = false;
            this.InternacionRPV.Visible = false;
            this.EmergenciaRPV.Visible = false;

            if (IsEmergencia)
            {
                RadComboBox ProveedorDDL = (RadComboBox)CasoFV.FindControl("ProveedorDDL");
                RadComboBox RadCiudadEmergencia = (RadComboBox)CasoFV.FindControl("RadCiudadEmergencia");
                Label LabelCiudadEmergencia = (Label)CasoFV.FindControl("LabelCiudadEmergencia");
                Label ProveedorLabel = (Label)CasoFV.FindControl("ProveedorLabel");
                try
                {
                    //int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    //ProveedorDDL.Items.Clear();
                    //ProveedorDDL.DataSource = Artexacta.App.ProveedorUser.BLL.ProveedorUserBLL.ProveedorByUser(UserId);
                    //ProveedorDDL.DataBind();
                    ProveedorLabel.Visible = true;
                    ProveedorDDL.Visible = true;
                    LabelCiudadEmergencia.Visible = true;
                    RadCiudadEmergencia.Visible = true;
                    this.EmergenciaRPV.Visible = true;
                }
                catch (Exception q)
                {
                    log.Error("Error al Obtener los proveedores por usuario", q);
                    SystemMessages.DisplaySystemErrorMessage("Error Obtener los proveedores del usuario.");
                    Response.Redirect("~/CasoMedico/CasoMedicoRegistro.aspx", true);
                }
            }

            ModeHF.Value = "";
        }
        else
        {
            AdminGastosLB.Visible = true;
            returnHL.Visible = true;
            newHL.Visible = false;
            odontologiaHL.Visible = true;
            recetaHL.Visible = true;
            ExComplementarioHL.Visible = true;
            EspecilistaHL.Visible = true;
            InternacionHL.Visible = true;
            if (IsEmergencia && CasoFV.CurrentMode == FormViewMode.ReadOnly)
            {
                Label ProveedorLabel = (Label)CasoFV.FindControl("ProveedorLabel");
                Label ProveedorNameLabel = (Label)CasoFV.FindControl("ProveedorNameLabel");
                ProveedorLabel.Visible = true;
                ProveedorNameLabel.Visible = true;
            }
        }
        if (IsEmergencia)
        {
            LinkButton EditLB = (LinkButton)PacienteFV.FindControl("EditLB");
            HyperLink EditObservacionesHL = (HyperLink)CasoFV.FindControl("EditObservacionesHL");
            EditLB.Visible = false;
            if (UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name) <= 0)
            {
                if (EditObservacionesHL != null)
                    EditObservacionesHL.Enabled = false;
                //EmergenciaErrorLabel.Visible = true;
            }
            else
            {
                //EmergenciaErrorLabel.Visible = false;
                int CasoId = Convert.ToInt32(CasoIdHF.Value);
                Emergencia objEmergencia = EmergenciaBLL.getEmergenciaDetailsByCasoId(CasoId);
                if (objEmergencia != null)
                {
                    EmergenciaIdHF.Value = objEmergencia.EmergenciaId.ToString();
                    FileManager.ObjectId = objEmergencia.EmergenciaId;
                    //FileManagerPanel.Visible = true;
                    if (objEmergencia.GastoId > 0)
                    {
                        if (EditObservacionesHL != null)
                            EditObservacionesHL.Enabled = false;
                        EmergenciaGastosErrorLabel.Visible = true;
                    }
                    else
                    {
                        if (EditObservacionesHL != null)
                            EditObservacionesHL.Enabled = true;
                        EmergenciaGastosErrorLabel.Visible = false;
                    }
                }
            }
        }
    }

    protected void GetCiudadIdAndRedMedicaPaciente()
    {
        int RedMedicaPaciente = 0;
        try
        {
            string UserName = HttpContext.Current.User.Identity.Name;
            User objUser = UserBLL.GetUserByUsername(UserName);
            this.CiudadHF.Value = objUser.CiudadId;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la Cuidad del Usuario.");
            log.Error("Error al obtener la CiudadId del usuario", ex);
        }

        try
        {
            int clienteId = Convert.ToInt32(ClienteIdHF.Value);

            List<RedMedica> ListRedMedica = RedMedicaBLL.getRedMedicaListByClienteId(clienteId);
            /*por ahora obtiene la ultima redmedica de la lista del Cliente (Aseguradora),
              en la practica solo pertenecera a una sola, pero corregir esto mas adelante
             ya q un Cliente puede tener varias RedMedicas a la vez*/
            foreach (RedMedica ObjRedMedica in ListRedMedica)
            {
                RedMedicaPaciente = ObjRedMedica.RedMedicaId;
            }
        }
        catch (Exception)
        {

        }
        this.RedMedicaIdHF.Value = RedMedicaPaciente.ToString();
    }
    private void validateMotivoConsulta()
    {

        var appSet = System.Web.Configuration.WebConfigurationManager.AppSettings;
        string ODO = appSet["ValorODO"];
        string CI = appSet["ValorCI"];
        string ES = appSet["ValorES"];
        string FA = appSet["ValorFA"];
        string IM = appSet["ValorIM"];
        string IN = appSet["ValorIN"];
        string LA = appSet["ValorLA"];

        var prestaciones = RedClientePrestacionesBLL.GetAllClientePrestaciones(int.Parse(ClienteIdHF.Value));

        this.OdontologiaRT.Visible = false;
        this.OdontologiaRPV.Visible = false;
        this.odontologiaHL.Visible = false;
        this.ExComplementarioRPV.Visible = false;
        this.EstudioRT.Visible = false;
        this.DerivacionRT.Visible = false;
        this.DerivacionRPV.Visible = false;
        this.InternacionRT.Visible = false;
        this.InternacionRT.Visible = false;
        this.RecetaRT.Visible = false;
        this.RecetaRPV.Visible = false;
        this.EmergenciaRPV.Visible = false;
        this.EmergenciaRT.Visible = false;


        string motivo = MotivoConsultaIdTHF.Value;
        if (motivo == "CONONLINE")
        {
            btnvideollamada.Visible = true;
        }
        else
        {
            btnvideollamada.Visible = false;


            switch (MotivoConsultaIdHF.Value)
            {
                case "ENFER":
                    this.EmergenciaRPV.Visible = true;
                    this.EmergenciaRT.Visible = true;
                    Response.Redirect("~/CasoMedico/Enfermeria.aspx");
                    return;
                case "EMERG":
                    //Response.Redirect("~/CasoMedico/Emergencia.aspx");
                    CasoMedicoTitle.Text = "Registro de Emergencia";
                    //this.OdontologiaRT.Visible = false;
                    //this.OdontologiaRPV.Visible = false;
                    //this.odontologiaHL.Visible = false;
                    //this.ExComplementarioRPV.Visible = false;
                    //this.EstudioRT.Visible = false;
                    //this.DerivacionRT.Visible = false;
                    //this.DerivacionRPV.Visible = false;
                    //this.InternacionRT.Visible = false;
                    //this.InternacionRT.Visible = false;
                    //this.RecetaRT.Visible = false;
                    //this.RecetaRPV.Visible = false;
                    this.EmergenciaRT.Visible = true;
                    this.EmergenciaRPV.Visible = true;
                    return;
                //case "ODONTO":
                //    //this.RecetaRT.Visible = false;
                //    //this.RecetaRPV.Visible = false;
                //    //this.ExComplementarioRPV.Visible = false;
                //    //this.EstudioRT.Visible = false;
                //    this.DerivacionRT.Visible = false;
                //   this.DerivacionRPV.Visible = false;
                //    this.InternacionRT.Visible = false;
                //    this.InternacionRT.Visible = false;
                //    this.OdontologiaRT.Visible = true;
                //    this.OdontologiaRPV.Visible = true;

                //    if (string.IsNullOrEmpty(ModeHF.Value))
                //        this.odontologiaHL.Visible = true;
                //    break;
                default:
                    break;
            }

            foreach (var pre in prestaciones)
            {
                if (pre.ClienteId != 0)
                {
                    if (pre.TipoPrestacion == CI || pre.TipoPrestacion == IN)
                    {
                        this.InternacionRT.Visible = true;
                        this.InternacionRPV.Visible = true;
                    }
                    else if (pre.TipoPrestacion == ES)
                    {
                        this.DerivacionRT.Visible = true;
                        this.DerivacionRPV.Visible = true;
                    }
                    else if (pre.TipoPrestacion == FA)
                    {
                        this.RecetaRT.Visible = true;
                        this.RecetaRPV.Visible = true;
                    }
                    else if (pre.TipoPrestacion == IM || pre.TipoPrestacion == LA)
                    {
                        this.EstudioRT.Visible = true;
                        this.ExComplementarioRPV.Visible = true;
                    }
                    else if (pre.TipoPrestacion == ODO)
                    {
                        this.OdontologiaRT.Visible = true;
                        this.OdontologiaRPV.Visible = true;
                    }
                }
            }
        }
    }
    //este talvez obtener del queryString para q se pueda refrescar la pagina
    protected void ProcessSessionParameters()
    {
        int CasoId = 0;

        string motivocon = "";

        if (Session["CasoId"] != null && !string.IsNullOrEmpty(Session["CasoId"].ToString()))
        {
            try
            {
                CasoId = Convert.ToInt32(Session["CasoId"]);
                //MotivoConsultaId = Session["MotivoConsultaid"].ToString();
                CasoIdHF.Value = CasoId.ToString();

                if (Session["MotivoConsultaIdT"] != null)
                {
                    motivocon = Session["MotivoConsultaIdT"].ToString();
                    MotivoConsultaIdTHF.Value = motivocon;
                }
                else 
                {
                    MotivoConsultaIdTHF.Value = "";
                }
                
                GetPolizaDetails(CasoId);
                GetCiudadIdAndRedMedicaPaciente();
            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso");
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);
                Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx", true);
                return;
            }
            MotivoConsultaIdHF.Value = CasoBLL.GetMotivoConsultaId(CasoId);
            validateMotivoConsulta();
        }
        //else
        //{
        //    CasoId = 1211;
        //    GetPolizaDetails(CasoId);
        //    GetCiudadIdAndRedMedicaPaciente();
        //    MotivoConsultaIdHF.Value = CasoBLL.GetMotivoConsultaId(CasoId);
        //    validateMotivoConsulta();
        //}
        Session["CasoId"] = null;

        int citaDesgId = 0;
        if (Session["CITADESGRAVAMENID"] != null && !string.IsNullOrEmpty(Session["CITADESGRAVAMENID"].ToString()))
        {
            try
            {
                citaDesgId = Convert.ToInt32(Session["CITADESGRAVAMENID"]);
                CitaDesgravamenIdHF.Value = citaDesgId.ToString();
            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso Desgravamen");
                log.Error("no se pudo realizar la conversion de la session CasoDesgravamen:" + Session["CITADESGRAVAMENID"]);
                Response.Redirect("~/MainPage.aspx");
                return;
            }
        }
        Session["CITADESGRAVAMENID"] = null;

        string Mode = "";
        if (Session["Mode"] != null && !string.IsNullOrEmpty(Session["Mode"].ToString()))
        {
            Mode = Session["Mode"].ToString();
        }

        bool isApprovedEdit = false;
        if (Session["isApproved"] != null && !string.IsNullOrEmpty(Session["isApproved"].ToString()))
        {
            bool.TryParse(Session["IsApproved"].ToString(), out isApprovedEdit);
        }

        ModeHF.Value = Mode;
        IsApprovedEdit = isApprovedEdit;
        Session["Mode"] = null;
        Session["IsApproved"] = null;

        string reconsulta = "";
        if (Session["RECONSULTA"] != null && !string.IsNullOrEmpty(Session["RECONSULTA"].ToString()))
        {
            try
            {
                CasoFV.ChangeMode(FormViewMode.Edit);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session RECONSULTA:" + Session["RECONSULTA"]);
            }
            reconsulta = Session["RECONSULTA"].ToString();
        }
        ReconsultaHF.Value = reconsulta;
        Session["RECONSULTA"] = null;

        string CitaId = "";
        if (Session["CitaId"] != null && !string.IsNullOrEmpty(Session["CitaId"].ToString()))
        {
            CitaId = Session["CitaId"].ToString();
        }
        CitaIdHF.Value = string.IsNullOrWhiteSpace(CitaId) ? "0" : CitaId;
        Session["CitaId"] = null;

        //////////////
        if (Session["DerivacionId"] != null && !string.IsNullOrEmpty(Session["DerivacionId"].ToString()))
        {
            DerivacionId.Value = Session["DerivacionId"].ToString();
        }



        /////////////
        if (CasoId <= 0)
            Response.Redirect(returnHL.NavigateUrl);
    }

    protected void GetPolizaDetails(int CasoId)
    {
        try
        {
            Poliza objPoliza = PolizaBLL.GetPolizaDetailsByCasoId(CasoId);

            int clienteId = objPoliza.ClienteId;
            this.ClienteIdHF.Value = objPoliza.ClienteId.ToString();
            this.PacienteIdHF.Value = objPoliza.PacienteId.ToString();

            decimal MontoMinimoPaciente = 0;
            decimal Porcentaje = 0;
            try
            {
                MontoMinimoPaciente = objPoliza.MontoTotal - objPoliza.GastoTotal;
                if (objPoliza.MontoTotal > 0)
                    Porcentaje = objPoliza.GastoTotal / objPoliza.MontoTotal * 100;
                else
                    Porcentaje = 0;
            }
            catch { }

            decimal MontoMinimoEnPoliza = Configuration.GetMontoMinimoEnPoliza();
            decimal PorcentajeSiniestralidadAlerta = Configuration.GetPorcentajeSiniestralidadAlerta();

            this.AseguradoLabel.Text = objPoliza.NombreJuridicoCliente + "  -  " + objPoliza.CodigoAsegurado;
            this.AseguradoIdHF.Value = objPoliza.AseguradoId.ToString();
            this.PolizaLabel.Text = objPoliza.NumeroPoliza + "  -  " + objPoliza.NombrePlan;
            this.FechaFinLabel.Text = objPoliza.FechaFinString;

            bool isMultPrincipioActivo = !RedClienteBLL.GetSoloLiname(objPoliza.ClienteId);
            Liname1SelectorDiv.Visible = isMultPrincipioActivo;
            NewReceta1.Visible = isMultPrincipioActivo;
            Liname2SelectorDiv.Visible = isMultPrincipioActivo;
            NewReceta2.Visible = isMultPrincipioActivo;
            Liname3SelectorDiv.Visible = isMultPrincipioActivo;
            NewReceta3.Visible = isMultPrincipioActivo;
            Liname4SelectorDiv.Visible = isMultPrincipioActivo;

            GridColumn cubierto = EstudioRadGrid.Columns.FindByUniqueNameSafe("Cubierto");
            if (cubierto != null)
            {
                cubierto.Visible = !(objPoliza.MontoTotal > 0);
            }
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

                        //deshabilitar todos los botonoes de Nuevo

                        string strMessage = "El paciente no cuenta con el monto minimo suficiente para crear una nueva Receta";
                        this.NewRecetaLB.ToolTip = strMessage;
                        this.MessageRecetaLabel.Text = strMessage;
                        this.SaveNewRecetaLB.Visible = false;

                        strMessage = "El paciente no cuenta con el monto minimo suficiente para crear un nuevo Ex. Complementario";
                        this.NewExComplementarioLB.ToolTip = strMessage;
                        this.MessageExComplementarioLabel.Text = strMessage;
                        SaveExComplementarioLB.Visible = false;

                        strMessage = "El paciente no cuenta con el monto minimo suficiente para crear una nueva Derivación a Especialista";
                        this.NewDerivacion.ToolTip = strMessage;
                        this.MessageDerivacionLabel.Text = strMessage;
                        this.DerivacionSaveLB.Visible = false;

                        strMessage = "El paciente no cuenta con el monto minimo suficiente para crear una nueva Internación";
                        this.NewInternacion.ToolTip = strMessage;
                        this.MessageInternacionLabel.Text = strMessage;
                        this.InternacionSaveLB.Visible = false;
                        this.InternacionCirugiaSaveLB.Visible = false;
                    }
                    else
                        if (Porcentaje >= PorcentajeSiniestralidadAlerta)
                        //this.SiniestralidadLabel.CssClass = "PorcentajeSiniestralidadAlerta";
                        this.DivSiniestralidad.Attributes.Add("Class", "PorcentajeSiniestralidadAlerta");
                }
            }
            else
            {
                SinistralidadMonto.Visible = false;
                SiniestralidadPlan.Visible = true;
                PlanUsoRepeater.DataSource = PlanBLL.getPlanUseForAsegurado(objPoliza.AseguradoId);
                PlanUsoRepeater.DataBind();
            }

            // Test DESGRAVAMEN
            Artexacta.App.RedCliente.RedCliente objRedCliente = null;
            try
            {
                objRedCliente = Artexacta.App.RedCliente.BLL.RedClienteBLL.GetRedClienteByClienteId(clienteId);
                if (objRedCliente != null && objRedCliente.CodigoCliente == "DESGRAVAMEN")
                {
                    ContentMenuTop.Visible = false;
                    CasoTab.Tabs[1].Visible = false;
                    CasoTab.Tabs[2].Visible = false;
                    CasoTab.Tabs[3].Visible = false;
                    CasoTab.Tabs[4].Visible = false;
                    CasoTab.Tabs[5].Visible = false;
                    CasoTab.Tabs[6].Visible = false;
                    Panel1.Visible = false;
                    CasoDesgravamenHF.Value = true.ToString();
                }
            }
            catch
            {
                log.Error("No se tiene información del caso para que sea de desgravamen");
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de la Póliza.");
            log.Error("Function PolizaODS_Selected on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void PacienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
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
                CasoMedicoTitle.Text += " <span style='color: #f00;'>[Paciente con enfermedades Crónicas]</span>";
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
    protected void PacienteODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Paciente.");
            e.ExceptionHandled = true;
        }
        else
        {
            string s = (string.IsNullOrEmpty(PacienteIdHF.Value) ? "" : PacienteIdHF.Value);
            int pacienteId = 0;
            if (int.TryParse(s, out pacienteId))
            {
                Paciente p = PacienteBLL.GetPacienteByPacienteId(pacienteId);
                string edadRAW = p.Edad;
                string Edad = edadRAW.Substring(0, edadRAW.Length - 4);
                int intEdad = 0;
                if (int.TryParse(Edad, out intEdad))
                {
                    PacienteEdad.Value = intEdad.ToString();
                }
            }
        }

    }

    protected void CasoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
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
            log.Error("Error al obtener CitaId en el Caso medico.", q);
        }
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Caso Medico.");
            e.ExceptionHandled = true;
        }
    }
    protected void CasoODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Modificar los datos del Caso Medico.");
            log.Error("Function CasoODS_Updated on page CasoMedicoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            this.ModeHF.Value = "";
            if (IsOdontologia) this.OdontologiaRT.Enabled = true;
            this.RecetaRT.Enabled = true;
            this.EstudioRT.Enabled = true;
            this.DerivacionRT.Enabled = true;
            this.InternacionRT.Enabled = true;
        }
        bool es = IsFemenino;
    }

    //Obtiene la fecha de Reconsulta si es que la tiene, caso contrario el de la creacion del caso
    protected DateTime GetFechaTabsCreacion(int CasoId)
    {
        return DateTime.Now;
    }


    #region Odontologia
    protected void OdontologiaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Prestaciones Odontológicas.");
            e.ExceptionHandled = true;
        }
    }

    protected void PrestacionOdontologicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Prestaciones Odontológicas.");
            e.ExceptionHandled = true;
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
    protected void PrestacionRCB_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
    {
        if (e.Item is RadComboBoxItem)
        {
            RadComboBoxItem item = (RadComboBoxItem)e.Item;
            PrestacionOdontologica ObjReceta = (PrestacionOdontologica)e.Item.DataItem;
            item.IsSeparator = ObjReceta.Categoria;
        }
    }
    protected void OdontologiaRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                Odontologia ObjOdontologia = (Odontologia)e.Item.DataItem;

                ImageButton deleteButton = (ImageButton)e.Item.FindControl("DeleteImageButton");
                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                ValidacionBLL obj = new ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {
                    if (deleteButton != null)
                    {
                        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                        deleteButton.Enabled = false;
                        deleteButton.ToolTip = ToolTipCannotDeleteHaveGasto;
                    }

                    if (ObjOdontologia.GastoId > 0)
                    {



                        ImageButton DetailsImageButton = (ImageButton)e.Item.FindControl("DetailsImageButton");
                        if (DetailsImageButton != null)
                        {
                            DetailsImageButton.ImageUrl = "~/Images/Neutral/edit_disable.png";
                            DetailsImageButton.Enabled = false;
                            DetailsImageButton.ToolTip = "No se puede Modificar por que tiene registros de gastos.";
                        }
                    }
                }
                else
                {
                    deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                    deleteButton.Visible = false;

                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algún control en Prestaciones Odontológicas.");
                log.Error("Function OdontologiaGrid_ItemDataBound on page CasoOdontologico.aspx", ex);
            }
        }
    }
    protected void OdontologiaRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    {
                        int OdontologiaId = 0;
                        try
                        {
                            OdontologiaId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OdontologiaId"];
                        }
                        catch (Exception ex)
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al obtener el id de la Prestación Odontológica a eliminar.");
                            log.Error("Function OdontologiaGrid_ItemCommand deleting on page CasoOdontologico.aspx", ex);
                        }
                        try
                        {
                            if (OdontologiaBLL.DeleteOdontologia(OdontologiaId))
                                SystemMessages.DisplaySystemMessage("Odontologia eliminada correctamente.");
                            else
                                SystemMessages.DisplaySystemErrorMessage("Error al eliminar la Prestación Odontologica.");
                        }
                        catch
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al eliminar la Prestación Odontologica.");
                        }
                        break;
                    }
            }
        }
    }

    protected void SaveNewOdontologiaLB_Click(object sender, EventArgs e)
    {
        try
        {
            Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(Convert.ToInt32(CasoIdHF.Value));
            string NombreEstudioOdo = PrestacionRCB.Text;

            string ValorODO = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorODO"];
            string Respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, Convert.ToInt32(ClienteIdHF.Value), ValorODO, NombreEstudioOdo);
            if (Respuesta == "")
            {
                int CasoId = Convert.ToInt32(CasoIdHF.Value);
                int ProveedorId = Convert.ToInt32(RadComboBoxProveedorOdontologia.SelectedValue);
                /*if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                {
                    SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar la Prestación.");
                    CleanOdontologiaFields();
                    return;
                }*/


                try
                {
                    Odontologia odonto = new Odontologia();
                    odonto.CasoId = CasoId;
                    odonto.OdontologiaId = Convert.ToInt32("0" + PrestacionHF.Value);
                    odonto.PrestacionOdontologicaId = Convert.ToInt32(PrestacionRCB.SelectedValue);
                    odonto.Pieza = string.IsNullOrWhiteSpace(PiezaOdontologiaMultiple.SelectedValues) ?
                    PiezaOdontologiaSimple.SelectedValues : PiezaOdontologiaMultiple.SelectedValues;
                    odonto.Observaciones = ObservacionesPrestacionTextBox.Text;

                    OdontologiaBLL.InsertOdontologia(odonto);
                    CasoOdontologia ObjcasoOdontologia = new CasoOdontologia();
                    ObjcasoOdontologia.CasoId = CasoId;
                    ObjcasoOdontologia.PrestacionOdontologicaId = odonto.PrestacionOdontologicaId;
                    ObjcasoOdontologia.ProveedorId = ProveedorId;
                    ObjcasoOdontologia.detPrecio = objValidacion.BusquedaDePrecioPRestacionesOdontologicas(ProveedorId, odonto.PrestacionOdontologicaId);
                    ObjcasoOdontologia.detCoPagoMonto = objValidacion.BusquedaDeValorCoPago("Monto", Convert.ToInt32(ClienteIdHF.Value), ValorODO);
                    ObjcasoOdontologia.detCoPagoPorcentaje = objValidacion.BusquedaDeValorCoPago("Porcentaje", Convert.ToInt32(ClienteIdHF.Value), ValorODO); 
                    ObjcasoOdontologia.Fecha = DateTime.Now;
                    CoPagosBLL.InsertCasoOdontologia(ObjcasoOdontologia);
                    SystemMessages.DisplaySystemMessage("Nueva Prestación insertada correctamente.");
                }
                catch (Exception q)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al insertar la Prestación.");
                    log.Error("Error al insertar la Prestación.", q);
                }
                OdontologiaRadGrid.DataBind();
                CleanOdontologiaFields();
                this.OdontologiaRT.Selected = true;
                this.OdontologiaRPV.Selected = true;
            }
            else
            {
                // AlertaOdontologia.Visible = true;
                // AlertaOdontologia.Text = "Error de " + Respuesta;
                SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");
            }

        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la nueva prestación Odontológica.");
            log.Error("Function SaveNewOdontologiaLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void CleanOdontologiaFields()
    {
        this.PrestacionRCB.ClearSelection();
        this.PrestacionRCB.Text = "";

        this.PiezaOdontologiaMultiple.ClearSelection();
        this.PiezaOdontologiaSimple.ClearSelection();

        this.ObservacionesPrestacionTextBox.Text = "";
    }

    protected void OdontologiaRadGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        try
        {
            string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            OdontologiaForPrintRadGrid.DataSourceID = OdontologiaRadGrid.DataSourceID;
            OdontologiaForPrintRadGrid.DataBind();
            ExportGridToPdf(OdontologiaForPrintRadGrid, codigoCaso + "_PrestacionOdontologica", "ODONTOLOGIA", codigoCaso);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al exportar las Prestaciones Odontologicas.");
            log.Error("Function OdontologiaRadGrid_ExportToPdfButton_Click on page CasoMedicoDetalle.aspx", q);
        }
    }
    protected void OdontologiaRadGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region TabReceta
    protected void RecetaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Recetas.");
            e.ExceptionHandled = true;
        }
    }

    protected void TipoMedicamentoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de tipos de presentaciones medicas");
            e.ExceptionHandled = true;
        }
    }

    protected void RecetaGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                Receta ObjReceta = (Receta)e.Item.DataItem;

                if (ObjReceta.GastoId > 0)
                {
                    ImageButton deleteButton = (ImageButton)item.FindControl("DeleteCommandColumn");
                    if (deleteButton != null)
                    {
                        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                        deleteButton.Enabled = false;
                        deleteButton.ToolTip = ToolTipCannotDeleteHaveGasto;
                    }

                    ImageButton detailsImageButton = (ImageButton)e.Item.FindControl("DetailsImageButton");
                    if (detailsImageButton != null)
                    {
                        detailsImageButton.ImageUrl = "~/Images/Neutral/edit_disable.png";
                        detailsImageButton.Enabled = false;
                        detailsImageButton.ToolTip = "No se puede Modificar por que tiene registros de gastos.";
                    }
                }


                ImageButton deleteButton1 = (ImageButton)e.Item.FindControl("DeleteImageButton");
                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                ValidacionBLL obj = new ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {


                }
                else
                {
                    deleteButton1.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                    deleteButton1.Visible = false;
                }


            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Recetas.");
                log.Error("Function RecetaGrid_ItemDataBound on page CasoMedicoDetalle.aspx", ex);
            }
        }
    }
    protected void RecetaGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            switch (e.CommandName)
            {
                case "Select":
                    try
                    {
                        int DetalleID = Convert.ToInt32(e.CommandArgument.ToString());

                        Receta objReceta = RecetaBLL.GetRecetaByDetalleId(DetalleID);

                        if (objReceta != null)
                        {
                            this.RecetaIdHF.Value = objReceta.DetalleId.ToString();
                            this.TipoMedicamentoDDL.SelectedValue = objReceta.TipoMedicamentoId;
                            this.MedicamentoTxt.Text = objReceta.Medicamento;
                            this.InstruccionesTxt.Text = objReceta.Indicaciones;
                            List<Ciudad> listaCiudades = CiudadBLL.getCiudadListByProveedor(objReceta.ProveedorId);
                            CiudadEditarComboBox.SelectedValue = listaCiudades[0].CiudadId;
                            ProveedorEditarComboBox.SelectedValue = objReceta.ProveedorId.ToString();
                            //abrir popup
                            ClientScript.RegisterStartupScript(this.GetType(), "Receta", "OpenPopupReceta();", true);
                        }
                    }
                    catch
                    {
                        SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de la receta.");
                    }
                    break;

                case "Delete":
                    {
                        int DetalleId = 0;
                        try
                        {
                            DetalleId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DetalleId"];
                        }
                        catch (Exception ex)
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al obtener el id de la receta a eliminar.");
                            log.Error("Function RecetaGrid_ItemCommand deleting on page CasoMedicoDetalle.aspx", ex);
                        }
                        try
                        {
                            if (RecetaBLL.GetGastoIdReceta(DetalleId) > 0)
                            {
                                SystemMessages.DisplaySystemErrorMessage("No se puede Elimnar la receta por que tiene registros de gastos.");
                                break;
                            }
                        }
                        catch
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al Validar si la receta cuenta con gastos.");
                        }

                        try
                        {
                            if (RecetaBLL.DeleteReceta(DetalleId))
                                SystemMessages.DisplaySystemMessage("Receta eliminada correctamente.");
                            else
                                SystemMessages.DisplaySystemErrorMessage("Error al eliminar la receta.");
                        }
                        catch
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al eliminar la receta.");
                        }
                        break;
                    }
            }
        }
    }

    protected void SaveNewRecetaLB_Click(object sender, EventArgs e)
    {
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            /*  if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
              {
                  SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar la Receta.");
                  CleanRecetaFields();
                  return;
              }*/
            int proveedorId = Convert.ToInt32(FarmaciaRecetaRadComboBox.SelectedValue);

            int InsertedNewReceta = 0;
            int ErrorInserted = 0;
            int MedicamentoClaId = Convert.ToInt32("0" + NewMedicamentoClaIdHF.Value);
            string TipoMedicamentoId = NewTipoMedicamentoRadComboBox.SelectedValue;
            if (string.IsNullOrWhiteSpace(TipoMedicamentoId)) { TipoMedicamentoId = TipoMedicamento1DDL.SelectedValue; }
            int TipoConcentracionId = Convert.ToInt32("0" + NewTipoConcentracionRadComboBox.SelectedValue);
            string Medicamento = Medicamento1Txt.Text;
            string Indicaciones = Instrucciones1Txt.Text;

            int cantidad1 = (int)Cantidad1Txt.Value;

            DateTime FechaCreacionReceta = GetFechaTabsCreacion(CasoId);
            int NewRecetaId = RecetaBLL.InsertReceta(CasoId, Medicamento, MedicamentoClaId, TipoMedicamentoId,
                TipoConcentracionId, Indicaciones, FechaCreacionReceta, proveedorId, cantidad1);
            if (NewRecetaId <= 0)
                ErrorInserted++;
            else
                InsertedNewReceta++;

            //receta 2
            bool receta2 = false;
            if (!string.IsNullOrEmpty(Medicamento2Txt.Text))
            {
                receta2 = !string.IsNullOrEmpty(TipoMedicamento2DDL.SelectedValue);
            }
            else if (!string.IsNullOrEmpty(NewMedicamentoCla2IdHF.Value))
            {
                receta2 = !string.IsNullOrEmpty(NewTipoMedicamento2RadComboBox.SelectedValue)
                       && !string.IsNullOrEmpty(NewTipoConcentracion2RadComboBox.SelectedValue);
            }

            if (receta2 && !string.IsNullOrEmpty(Instrucciones2Txt.Text))
            {
                //reset
                MedicamentoClaId = Convert.ToInt32("0" + NewMedicamentoCla2IdHF.Value);
                TipoMedicamentoId = NewTipoMedicamento2RadComboBox.SelectedValue;
                if (string.IsNullOrWhiteSpace(TipoMedicamentoId)) { TipoMedicamentoId = TipoMedicamento2DDL.SelectedValue; }
                TipoConcentracionId = Convert.ToInt32("0" + NewTipoConcentracion2RadComboBox.SelectedValue);
                int cantidad2 = (int)Cantidad2Txt.Value;
                NewRecetaId = 0;
                NewRecetaId = RecetaBLL.InsertReceta(CasoId, Medicamento2Txt.Text, MedicamentoClaId
                    , TipoMedicamentoId, TipoConcentracionId, Instrucciones2Txt.Text, FechaCreacionReceta, proveedorId, cantidad2);
                if (NewRecetaId <= 0)
                    ErrorInserted++;
                else
                    InsertedNewReceta++;
            }

            //receta 3
            bool receta3 = false;
            if (!string.IsNullOrEmpty(Medicamento3Txt.Text))
            {
                receta3 = !string.IsNullOrEmpty(TipoMedicamento3DDL.SelectedValue);
            }
            else if (!string.IsNullOrEmpty(NewMedicamentoCla3IdHF.Value))
            {
                receta3 = !string.IsNullOrEmpty(NewTipoMedicamento3RadComboBox.SelectedValue)
                       && !string.IsNullOrEmpty(NewTipoConcentracion3RadComboBox.SelectedValue);
            }
            if (receta3 && !string.IsNullOrEmpty(Instrucciones3Txt.Text))
            {
                //reset
                MedicamentoClaId = Convert.ToInt32("0" + NewMedicamentoCla3IdHF.Value);
                TipoMedicamentoId = NewTipoMedicamento3RadComboBox.SelectedValue;
                if (string.IsNullOrWhiteSpace(TipoMedicamentoId)) { TipoMedicamentoId = TipoMedicamento3DDL.SelectedValue; }
                TipoConcentracionId = Convert.ToInt32("0" + NewTipoConcentracion3RadComboBox.SelectedValue);
                int cantidad3 = (int)Cantidad3Txt.Value;
                NewRecetaId = 0;
                NewRecetaId = RecetaBLL.InsertReceta(CasoId, Medicamento3Txt.Text, MedicamentoClaId
                    , TipoMedicamentoId, TipoConcentracionId, Instrucciones3Txt.Text, FechaCreacionReceta, proveedorId, cantidad3);
                if (NewRecetaId <= 0)
                    ErrorInserted++;
                else
                    InsertedNewReceta++;
            }

            //receta 4
            bool receta4 = false;
            if (!string.IsNullOrEmpty(Medicamento4Txt.Text))
            {
                receta4 = !string.IsNullOrEmpty(TipoMedicamento4DDL.SelectedValue);
            }
            else if (!string.IsNullOrEmpty(NewMedicamentoCla4IdHF.Value))
            {
                receta4 = !string.IsNullOrEmpty(NewTipoMedicamento4RadComboBox.SelectedValue)
                       && !string.IsNullOrEmpty(NewTipoConcentracion4RadComboBox.SelectedValue);
            }
            if (receta4 && !string.IsNullOrEmpty(Instrucciones4Txt.Text))
            {
                //reset
                MedicamentoClaId = Convert.ToInt32("0" + NewMedicamentoCla4IdHF.Value);
                TipoMedicamentoId = NewTipoMedicamento4RadComboBox.SelectedValue;
                if (string.IsNullOrWhiteSpace(TipoMedicamentoId)) { TipoMedicamentoId = TipoMedicamento4DDL.SelectedValue; }
                TipoConcentracionId = Convert.ToInt32("0" + NewTipoConcentracion4RadComboBox.SelectedValue);
                int cantidad4 = (int)Cantidad4Txt.Value;
                NewRecetaId = 0;
                NewRecetaId = RecetaBLL.InsertReceta(CasoId, Medicamento4Txt.Text, MedicamentoClaId
                    , TipoMedicamentoId, TipoConcentracionId, Instrucciones4Txt.Text, FechaCreacionReceta, proveedorId, cantidad4);
                if (NewRecetaId <= 0)
                    ErrorInserted++;
                else
                    InsertedNewReceta++;
            }

            //mostrar mensaje de error
            if (ErrorInserted > 0)
            {
                if (InsertedNewReceta > 0)
                    SystemMessages.DisplaySystemErrorMessage("Error al insertar " + ErrorInserted.ToString() + " receta(s), " + InsertedNewReceta.ToString() + "receta(s) insertada(s) corecctamente");
                else
                    SystemMessages.DisplaySystemErrorMessage("Error al insertar " + ErrorInserted.ToString() + " receta(s).");
            }
            else
            {
                if (InsertedNewReceta > 1)
                    SystemMessages.DisplaySystemMessage(InsertedNewReceta.ToString() + " Nuevas recetas insertadas correctamente.");
                else
                    SystemMessages.DisplaySystemMessage("Nueva receta insertada correctamente.");
            }
            RecetaGrid.DataBind();
            CleanRecetaFields();
            this.RecetaRT.Selected = true;
            this.RecetaRPV.Selected = true;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la nueva Receta.");
            log.Error("Function SaveNewRecetaLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void CleanRecetaFields()
    {
        this.NewMedicamentoRadComboBox.ClearSelection();
        this.NewMedicamentoRadComboBox.Text = "";
        this.NewMedicamentoClaIdHF.Value = "";
        this.NewMedicamento2RadComboBox.ClearSelection();
        this.NewMedicamento2RadComboBox.Text = "";
        this.NewMedicamentoCla2IdHF.Value = "";
        this.NewMedicamento3RadComboBox.ClearSelection();
        this.NewMedicamento3RadComboBox.Text = "";
        this.NewMedicamentoCla3IdHF.Value = "";
        this.NewMedicamento4RadComboBox.ClearSelection();
        this.NewMedicamento4RadComboBox.Text = "";
        this.NewMedicamentoCla4IdHF.Value = "";

        this.NewGrupoLabel.Text = "";
        this.NewGrupo2Label.Text = "";
        this.NewGrupo3Label.Text = "";
        this.NewGrupo4Label.Text = "";

        this.NewSubgrupoLabel.Text = "";
        this.NewSubgrupo2Label.Text = "";
        this.NewSubgrupo3Label.Text = "";
        this.NewSubgrupo4Label.Text = "";

        this.NewTipoMedicamentoRadComboBox.ClearSelection();
        this.NewTipoMedicamento2RadComboBox.ClearSelection();
        this.NewTipoMedicamento3RadComboBox.ClearSelection();
        this.NewTipoMedicamento4RadComboBox.ClearSelection();

        this.NewTipoConcentracionRadComboBox.ClearSelection();
        this.NewTipoConcentracion2RadComboBox.ClearSelection();
        this.NewTipoConcentracion3RadComboBox.ClearSelection();
        this.NewTipoConcentracion4RadComboBox.ClearSelection();

        this.TipoMedicamento1DDL.ClearSelection();
        this.TipoMedicamento2DDL.ClearSelection();
        this.TipoMedicamento3DDL.ClearSelection();
        this.TipoMedicamento4DDL.ClearSelection();

        this.Medicamento1Txt.Text = "";
        this.Medicamento2Txt.Text = "";
        this.Medicamento3Txt.Text = "";
        this.Medicamento4Txt.Text = "";

        this.Instrucciones1Txt.Text = "";
        this.Instrucciones2Txt.Text = "";
        this.Instrucciones3Txt.Text = "";
        this.Instrucciones4Txt.Text = "";

        this.Cantidad1Txt.Value = 0;
        this.Cantidad2Txt.Value = 0;
        this.Cantidad3Txt.Value = 0;
        this.Cantidad4Txt.Value = 0;
    }

    protected void SaveReceta_Click(object sender, EventArgs e)
    {
        int RecetaId = 0;
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            int MedicamentoClaId = 0;
            string Medicamento = MedicamentoTxt.Text;
            string TipoMedicamentoId = TipoMedicamentoDDL.SelectedValue;
            int TipoConcentracionId = 0;
            string Indicaciones = InstruccionesTxt.Text;
            DateTime FechaCreacion = GetFechaTabsCreacion(CasoId);
            int proveedorId = Convert.ToInt32(ProveedorEditarComboBox.SelectedValue);
            int cantidad = (int)CantidadTxt.Value;
            RecetaId = Convert.ToInt32(this.RecetaIdHF.Value);

            if (RecetaId <= 0)
            {
                if (PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                {
                    int NewRecetaId = RecetaBLL.InsertReceta(CasoId, Medicamento, MedicamentoClaId, TipoMedicamentoId,
                        TipoConcentracionId, Indicaciones, FechaCreacion, proveedorId, cantidad);
                    if (NewRecetaId <= 0)
                        SystemMessages.DisplaySystemErrorMessage("Error al insertar la nueva receta.");
                    else
                        SystemMessages.DisplaySystemMessage("Nueva receta insertada correctamente.");
                }
                else
                    SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar una Receta.");

            }
            else
            {
                if (RecetaBLL.GetGastoIdReceta(RecetaId) > 0)
                    SystemMessages.DisplaySystemErrorMessage("No se puede modificar la receta por que tiene registros de gastos.");
                else
                {
                    if (RecetaBLL.UpdateReceta(RecetaId, Medicamento, MedicamentoClaId, TipoMedicamentoId,
                        TipoConcentracionId, Indicaciones, proveedorId, cantidad))
                        SystemMessages.DisplaySystemMessage("La receta fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar la receta.");
                }
            }

            RecetaGrid.DataBind();
            this.RecetaIdHF.Value = "0";
            this.MedicamentoTxt.Text = "";
            this.InstruccionesTxt.Text = "";
            this.TipoMedicamentoDDL.ClearSelection();
            ProveedorEditarComboBox.ClearSelection();
            CiudadEditarComboBox.ClearSelection();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la Receta.");
            log.Error("Function SaveReceta_Click on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void RecetaGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        try
        {
            string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            RecetaToPrintRadGrid.DataSourceID = RecetaGrid.DataSourceID;
            RecetaToPrintRadGrid.DataBind();
            ExportGridToPdf(RecetaToPrintRadGrid, codigoCaso + "_RecetaFarmacia", "PRESCRIPTION", codigoCaso);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al exportar la Receta.");
            log.Error("Function RecetaGrid_ExportToPdfButton_Click on page CasoMedicoDetalle.aspx", q);
        }
    }
    protected void RecetaGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region TabExamenComplementario
    protected void EstudioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Examen Complementarios.");
            e.ExceptionHandled = true;
        }
    }

    protected void CiudadExComplementarioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de las ciudades para examenes complementarios.");
            log.Error("Function CiudadExComplementarioODS_Selected on page CasoMedicoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void EspecialidadComplementarioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de las especialidades para examenes complementarios.");
            log.Error("Function EspecialidadExComplementarioODS_Selected on page CasoMedicoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void ProveedorExComplementarioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de los proveedores para examenes complementarios.");
            e.ExceptionHandled = true;
        }
    }

    protected void PrestacionesExComplementarioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de las Prestaciones para examenes complementarios.");
            e.ExceptionHandled = true;
        }
    }
    protected void TipoEstudioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de tipo de Examen.");
            e.ExceptionHandled = true;
        }
    }

    protected void EstudioRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                Estudio ObjEstudio = (Estudio)e.Item.DataItem;

                if (!ObjEstudio.Modify)
                {
                    ImageButton DetailsImageButton = (ImageButton)e.Item.FindControl("DetailsImageButton");
                    if (DetailsImageButton != null)
                    {
                        DetailsImageButton.ImageUrl = "~/Images/Neutral/edit_disable.png";
                        DetailsImageButton.Enabled = false;
                        DetailsImageButton.ToolTip = "No se puede Modificar por que ya esta aprobado.";
                    }
                }


                ImageButton deleteButton1 = (ImageButton)e.Item.FindControl("DeleteImageButton");
                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                ValidacionBLL obj = new ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {
                    if (ObjEstudio.GastoId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)e.Item.FindControl("DeleteCommandColumn");
                        //GridButtonColumn deleteButton = (GridButtonColumn)EstudioRadGrid.MasterTableView.GetColumn("DeleteCommandColumn");
                        //(EstudioRadGrid.MasterTableView.GetColumn("DeleteCommandColumn") as GridButtonColumn).ImageUrl = "Delete";
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Visible = false;
                            deleteButton.ToolTip = "No se puede eliminar por que tiene registros de gastos";
                        }
                    }
                }
                else
                {
                    deleteButton1.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                    deleteButton1.Visible = false;
                    deleteButton1.ToolTip = "No se puede eliminar por que Usted No es Admin";
                }
                if (ObjEstudio.Aprovado)
                {
                    ImageButton AprobarColumn = (ImageButton)e.Item.FindControl("AprobarImageButton");
                    if (AprobarColumn != null)
                    {
                        AprobarColumn.Visible = false;
                        AprobarColumn.Enabled = false;
                    }
                }
                //else
                //{
                //    //si es menor al MontoMinimoEnPoliza no permitir Aprobar Caso
                //    Caso objCaso = CasoBLL.GetCasoByCasoId(ObjEstudio.CasoId);
                //    decimal MontoLibrePaciente = objCaso.MontoTotal - objCaso.GastoTotal;
                //    decimal MontoMinimoEnPoliza = Configuration.GetMontoMinimoEnPoliza();
                //    if (MontoLibrePaciente < MontoMinimoEnPoliza)
                //    {
                //        item.CssClass = "MontoMinimoEnPolizaInferior";
                //        Image AlertaImg = (Image)item.FindControl("AlertaImg");
                //        if (AlertaImg != null)
                //        {
                //            AlertaImg.ImageUrl = "~/Images/Neutral/alert-red.gif";
                //            AlertaImg.ToolTip = "El paciente no cuenta con el monto mínimo suficiente para Aprobar el Detalle del Caso Medico";
                //            AlertaImg.Visible = true;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Examenes complementarios.");
                log.Error("Function EstudioRadGrid_ItemDataBound on page CasoMedicoDetalle.aspx", ex);
            }
        }
    }


    protected void EstudioRadGrid_ItemCommand(object sender, CommandEventArgs e)
    {
        int EstudioId = Convert.ToInt32(e.CommandArgument.ToString());
        switch (e.CommandName)
        {
            case "Select":
                try
                {
                    Estudio objEstudio = EstudioBLL.getEstudioByEstudioId(EstudioId);

                    if (objEstudio != null)
                    {
                        this.EstudioIdHF.Value = objEstudio.EstudioId.ToString();
                        this.ProveedorExComplementarioDDL.SelectedValue = objEstudio.ProveedorId.ToString();
                        this.TipoEstudioDDL.SelectedValues = objEstudio.TipoEstudio.ToString();
                        this.ObservacionTxt.Text = objEstudio.Observaciones;
                        if (!objEstudio.Modify)
                        {
                            this.MessageExComplementarioLabel.Text = "No se puede modificar el examen complementario por que ya esta aprobado.";
                            this.SaveExComplementarioLB.Visible = false;
                        }
                        //abrir popup
                        ClientScript.RegisterStartupScript(this.GetType(), "ExComplementario", "OpenPopupExComplementario();", true);
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener el Examen Complementario.");
                    log.Error("Function EstudioRadGrid_ItemCommand on page CasoMedicoDetalle.aspx", ex);
                }
                break;

            case "Delete":
                try
                {
                    //int EstudioId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EstudioId"];
                    //El EstudioId es igual al Orden de ServicioId que realizamos en Copagos
                    CasoLaboratorioImagenologia HaPagado = CoPagosBLL.GetCasoLaboratorioImagenologiaxOrdenDeServicio(0, EstudioId, 0, 0);
                    if (HaPagado != null)
                    {
                        int detid = HaPagado.detId;
                        CasoLaboratorioImagenologia VerificarPago = CoPagosBLL.GetCasoLaboratorioImagenologia(detid);

                        if (VerificarPago.detFechaCoPagoPagado.Contains("S/N"))
                        {
                            if (EstudioBLL.GetGastoIdEstudio(EstudioId) > 0)
                                SystemMessages.DisplaySystemErrorMessage("No se puede eliminar el examen complementario por que tiene registros de gastos.");
                            else
                            {
                                if (CoPagosBLL.DeleteLaboratorioImagenologia(HaPagado.OrdenDeServicioId))
                                {
                                    if (EstudioBLL.DeleteEstudio(EstudioId))
                                        SystemMessages.DisplaySystemMessage("Examen complementario eliminado correctamente.");
                                    else
                                        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el examen complementario.");
                                }
                                else
                                    SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el examen complementario.");
                            }
                        }
                        else
                        {
                            SystemMessages.DisplaySystemErrorMessage("No se puede eliminar el examen complementario por que tiene Registro de CoPagos.");
                        }
                    }
                    else
                    {
                        if (EstudioBLL.GetGastoIdEstudio(EstudioId) > 0)
                            SystemMessages.DisplaySystemErrorMessage("No se puede eliminar el examen complementario por que tiene registros de gastos.");
                        else
                        {
                            if (EstudioBLL.DeleteEstudio(EstudioId))
                                SystemMessages.DisplaySystemMessage("Examen complementario eliminado correctamente.");
                            else
                                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el examen complementario.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el Examen Complementario.");
                    log.Error("Function EstudioRadGrid_ItemCommand deleting on page CasoMedicoDetalle.aspx", ex);
                }
                if (SiniestralidadPlan.Visible)
                {
                    PlanUsoRepeater.DataSource = PlanBLL.getPlanUseForAsegurado(Convert.ToInt32(AseguradoIdHF.Value));
                    PlanUsoRepeater.DataBind();
                }
                break;
            case "Aprobar":
                try
                {
                    int CasoId = int.Parse(CasoIdHF.Value);

                    if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                    {
                        EstudioRadGrid.DataBind();
                        SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Aprobar el Examen Complementario.");
                        break;
                    }
                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    if (EstudioBLL.AproveEstudio(EstudioId, UserId, DateTime.Now))
                        SystemMessages.DisplaySystemMessage("El Examen Complementario del caso medico fue aprobado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo aprobar el Examen Complementario del caso medico.");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al aprobar el Examen Complementario del caso medico.");
                    log.Error("Function EstudioRadGrid_ItemCommand Aprobating on page CasoMedicoDetalle.aspx", ex);
                }
                EstudioRadGrid.DataBind();
                break;
        }
    }
    private decimal BusquedaDeValorCoPagoLaboratorioIma(string TipodePago, string TipoEstudio)
    {
        decimal CopagoValor = 0;
        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion.Contains(TipoEstudio))
            {
                if (TipodePago == "Monto")
                {
                    string ValorCoPago = (ListaPrestaciones[i].CoPagoMonto).ToString("N2");
                    CopagoValor = decimal.Parse(ValorCoPago);
                    break;
                }
                else
                {
                    if (TipodePago == "Porcentaje")
                    {
                        string ValorCoPago = (ListaPrestaciones[i].CoPagoPorcentaje).ToString("N2");
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                }
            }
        }
        return CopagoValor;
    }


    protected void SaveExComplementarioLB_Click(object sender, EventArgs e)
    {
        int EstudioId = 0;

        try
        {

            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            int ProveedorId = Convert.ToInt32(ProveedorExComplementarioDDL.SelectedValue);
            string TipoEstudioId = TipoEstudioDDL.SelectedValues;

            if (!string.IsNullOrEmpty(EstudioIdHF.Value))
                EstudioId = Convert.ToInt32(EstudioIdHF.Value);

            DateTime FechaCreacionEstudio = GetFechaTabsCreacion(CasoId);

            if (EstudioId <= 0)
            {
                /*if (PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                {*/
                string TipoEstudioNombre = TipoEstudioPrestacionesExaComboBox.SelectedValue;

                Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(Convert.ToInt32(CasoIdHF.Value));
                string Respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, Convert.ToInt32(ClienteIdHF.Value), TipoEstudioNombre, "");

                if (Respuesta == "")
                {
                    int NewEstudioId = EstudioBLL.InsertEstudio(CasoId, ProveedorId, TipoEstudioId, ObservacionTxt.Text, FechaCreacionEstudio);
                    if (NewEstudioId <= 0)
                        SystemMessages.DisplaySystemErrorMessage("Se inserto el nuevo examen complementario, pero retorno valor incompleto.");
                    else
                    {
                        char separador = ','; // separador de datos
                        string[] arregloDeSubCadenas = TipoEstudioId.Split(separador);
                        for (int i = 0; i < arregloDeSubCadenas.Count(); i++)
                        {
                            int VEstudioId = int.Parse(arregloDeSubCadenas[i]);
                            List<RedProvLabImgCarDetallePrestaciones> ListaPrecioEstudio = RedProvLabImgCarDetallePrestacionesBLL.GetProvLabImgCarDetallePrestacionesXProveedoresAndEstudioId(ProveedorId, VEstudioId);
                            bool EsImagenologia;
                            string ValorIM = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorIM"];
                            if (ListaPrecioEstudio[0].CategoriaId == ValorIM)
                                EsImagenologia = true;
                            else
                                EsImagenologia = false;
                            CasoLaboratorioImagenologia objcasoLaboratorio = new CasoLaboratorioImagenologia(
                                CasoId, ProveedorId, VEstudioId, NewEstudioId, ListaPrecioEstudio[0].detPrecio
                                , BusquedaDeValorCoPagoLaboratorioIma("Monto", ListaPrecioEstudio[0].CategoriaId)
                                , BusquedaDeValorCoPagoLaboratorioIma("Porcentaje", ListaPrecioEstudio[0].CategoriaId)
                                , EsImagenologia, FechaCreacionEstudio
                                );
                            CoPagosBLL.InsertCasoLaboratorioImagenologia(objcasoLaboratorio);

                        }
                        SystemMessages.DisplaySystemMessage("Examen complementario insertado correctamente.");
                    }
                    try
                    {
                        int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                        EstudioBLL.AproveEstudio(NewEstudioId, UserId, DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        SystemMessages.DisplaySystemErrorMessage("Error al Insertar El nuevo Examen Complementario.");
                        log.Error("Function SaveNewExComplementarioLB_Click on page CasoMedicoDetalle.aspx", ex);
                    }
                }
                else
                {
                    //Mensaje de Error 
                    //AlertaLaboratorio.Visible = true;
                    //AlertaLaboratorio.Text = "Error de " + Respuesta;
                    SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");

                }

                /*   }
                   else
                       SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar un Ex. Complementario.");
              */
            }
            else
            {
               /* if (EstudioBLL.GetGastoIdEstudio(EstudioId) > 0)
                    SystemMessages.DisplaySystemErrorMessage("No se puede modificar el Examen complementario por que tiene registros de gastos.");
                else
                {*/
                    if (EstudioBLL.UpdateEstudio(EstudioId, ProveedorId, TipoEstudioId, ObservacionTxt.Text))
                        SystemMessages.DisplaySystemMessage("El examen complementario fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar el examen complementario.");
                //}
            }

        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Insertar El nuevo Examen Complementario.");
            log.Error("Function SaveNewExComplementarioLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
        finally
        {
            this.EstudioRadGrid.DataBind();
            this.EstudioIdHF.Value = "0";
            this.ObservacionTxt.Text = "";
            this.CiudadRadComboBox.ClearSelection();
            this.ProveedorExComplementarioDDL.ClearSelection();
            this.ProveedorExComplementarioDDL.Text = "";
            this.TipoEstudioDDL.ClearSelection();

            this.EstudioRT.Selected = true;
            this.ExComplementarioRPV.Selected = true;

            if (SiniestralidadPlan.Visible)
            {
                PlanUsoRepeater.DataSource = PlanBLL.getPlanUseForAsegurado(Convert.ToInt32(AseguradoIdHF.Value));
                PlanUsoRepeater.DataBind();
            }
        }

    }

    protected void CancelExComplementarioLB_Click(object sender, EventArgs e)
    {
        this.EstudioIdHF.Value = "0";
        this.ObservacionTxt.Text = "";
        this.CiudadRadComboBox.ClearSelection();
        this.ProveedorExComplementarioDDL.ClearSelection();
        this.TipoEstudioDDL.ClearSelection();
        this.ProveedorExComplementarioDDL.Text = "";
        this.TipoEstudioPrestacionesExaComboBox.SelectedValue = "TTT";
    }

    protected void EstudioRadGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        try
        {
            EstudioPrintRadGrid.DataSource = EstudioBLL.GetEstudioByCasoIdForPrint(Convert.ToInt32(ExportIDHF.Value));
            EstudioPrintRadGrid.DataBind();
            string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            ExportGridToPdf(EstudioPrintRadGrid, codigoCaso + "_Laboratorio", "EXAMS", codigoCaso);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al exportar el Exmanen Complementario.");
            log.Error("Function EstudioRadGrid_ExportToPdfButton_Click on page CasoMedicoDetalle.aspx", q);
        }
        //string ContactoSCZ = @"<div style=""font-size:8px;border-top: 1px solid #29528A;text-align:center;margin-left: 49px;margin-right: -2px;"">" +
        //                     "SANTA CRUZ<br />TELF: 3393730<br />CEL: 69054676-69054675</div>";
        //string ContactoLP = @"<div style=""font-size:8px;border-top: 1px solid #29528A;"">" +
        //                     "LA PAZ<br />TELF: 2971829<br />CEL: 69054668</div>";
        //EstudioPrintRadGrid.ExportSettings.Pdf.PageFooter.LeftCell.Text = @"<div style=""margin-right: 20px;padding-bottom: 6px;""><b>FECHA:</b> " + Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy") + "</div>" + ContactoSCZ;
        //EstudioPrintRadGrid.ExportSettings.Pdf.PageFooter.LeftCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Right;
        //EstudioPrintRadGrid.ExportSettings.Pdf.PageFooter.MiddleCell.Text = @"<div style=""border-top: 1px dashed #000;padding-bottom: 5px;""><b>FIRMA Y SELLO</b></div>" + ContactoLP;
        //EstudioPrintRadGrid.ExportSettings.Pdf.PageFooter.MiddleCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;

        //try
        //{
        //    int CasoId = Convert.ToInt32(this.CasoIdHF.Value);
        //    FechaEstado = CasoBLL.GetCasoByCasoId(CasoId).FechaEstadoExamenes;

        //    string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
        //    EstudioPrintRadGrid.ExportSettings.FileName = codigoCaso + "_Laboratorio_" + Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy");

        //    EstudioPrintRadGrid.ExportSettings.Pdf.BorderType = GridPdfSettings.GridPdfBorderType.NoBorder;
        //    EstudioPrintRadGrid.MasterTableView.ExportToPdf();
        //    CasoBLL.UpdateFechaEstado(CasoId, "EXAMS");
        //}
        //catch (Exception q)
        //{
        //    log.Error("Error al exportar a PDF", q);
        //    SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
        //}
    }

    protected void EstudioRadGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region Derivacion

    protected void DerivacionODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de las derivaciones a especialistas.");
            e.ExceptionHandled = true;
        }
    }

    protected void ProveedorDerivacionODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de los proveedores para derivaciones a especialistas.");
            e.ExceptionHandled = true;
        }
    }

    protected void DerivacionRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                if (deleteButton != null)
                {
                    bool Authorization = false;
                    try
                    {
                        Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_ELIMINAR_DERIVACION_ESPECIALISTAS");
                        Authorization = true;
                    }
                    catch (Exception)
                    {
                        Authorization = false;
                        //throw;
                    }

                    if (!Authorization)
                    {
                        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                        deleteButton.Visible = false;
                        deleteButton.ToolTip = "No cuenta con permisos para eliminar derivaciones";
                    }
                }
                /*
                Derivacion ObjDerivaacion = (Derivacion)e.Item.DataItem;

                if (!ObjDerivaacion.Modify)
                {
                    ImageButton DetailsImageButton = (ImageButton)e.Item.FindControl("DetailsImageButton");
                    if (DetailsImageButton != null)
                    {
                        DetailsImageButton.ImageUrl = "~/Images/Neutral/edit_disable.png";
                        DetailsImageButton.Enabled = false;
                        DetailsImageButton.ToolTip = ToolTipCannotUpdateByapproved;
                    }
                }

                if (ObjDerivaacion.GastoId > 0)
                {
                    ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                    if (deleteButton != null)
                    {
                        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                        deleteButton.Enabled = false;
                        deleteButton.ToolTip = ToolTipCannotDeleteHaveGasto;
                    }
                }

                if (ObjDerivaacion.Aprovado)
                {
                    ImageButton AprobarColumn = (ImageButton)e.Item.FindControl("AprobarImageButton");
                    if (AprobarColumn != null)
                    {
                        AprobarColumn.Visible = false;
                        AprobarColumn.Enabled = false;
                    }
                }*/
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Derivación a especialistas.");
                log.Error("Function DerivacionRadGrid_ItemDataBound on page CasoMedicoDetalle.aspx", ex);
            }
        }
    }

    protected void DerivacionRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":
                try
                {
                    int DerivacionId = Convert.ToInt32(e.CommandArgument.ToString());
                    Derivacion objDerivacion = DerivacionBLL.getDerivacionByDerivacionId(DerivacionId);

                    if (objDerivacion != null)
                    {
                        this.DerivacionIdHF.Value = objDerivacion.DerivacionId.ToString();
                        this.ProveedorDerivacionDDL.SelectedValue = objDerivacion.ProveedorId.ToString();
                        this.ObservacionDerivacionTxt.Text = objDerivacion.Observaciones;
                        if (!objDerivacion.Modify)
                        {
                            this.MessageDerivacionLabel.Text = "No se puede modificar la Derivación a especialista por que ya esta aprobado.";
                            this.DerivacionSaveLB.Visible = false;
                        }
                        //abrir popup
                        ClientScript.RegisterStartupScript(this.GetType(), "Derivacion", "OpenPopupDerivacion();", true);
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener la derivación a especialista.");
                    log.Error("Function DerivacionRadGrid_ItemCommand on page CasoMedicoDetalle.aspx", ex);
                }
                break;

            case "Delete":
                try
                {
                    int DerivacionId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DerivacionId"];
                    if (DerivacionBLL.DeleteDerivacion(DerivacionId))
                        SystemMessages.DisplaySystemMessage("Derivación a especialista eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la derivación a especialista.");

                    /*if (DerivacionBLL.GetGastoIdDerivacion(DerivacionId) > 0)
                        SystemMessages.DisplaySystemErrorMessage("No se puede eliminar la derivación a especialista por que tiene registros de gastos.");
                    else
                    {
                        
                    }*/
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar la derivación a especialista.");
                    log.Error("Function DerivacionRadGrid_ItemCommand deleting on page CasoMedicoDetalle.aspx", ex);
                }
                break;
            case "Aprobar":
                try
                {
                    int DerivacionId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DerivacionId"];
                    int CasoId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CasoId"];

                    if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                    {
                        DerivacionRadGrid.DataBind();
                        SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Aprobar la Derivación a Especialista.");
                        break;
                    }
                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    if (DerivacionBLL.AproveDerivacion(DerivacionId, UserId, DateTime.Now))
                        SystemMessages.DisplaySystemMessage("La Derivación a Especialista del caso medico fue aprobado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo aprobar la Derivación a Especialista del caso medico.");

                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al aprobar la Derivación a Especialista del caso medico.");
                    log.Error("Function DerivacionRadGrid_ItemCommand Aprobating on page CasoMedicoDetalle.aspx", ex);
                }
                DerivacionRadGrid.DataBind();
                break;
        }
    }
    private decimal BusquedaDeValorCoPagoEspecialidad(string TipodePago)
    {
        decimal CopagoValor = 0;
        string ValorES = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorES"];
        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion == ValorES)
            {
                if (TipodePago.Contains("Monto"))
                {

                    CopagoValor = ListaPrestaciones[i].CoPagoMonto;
                    break;
                }
                else
                {
                    if (TipodePago.Contains("Porcentaje"))
                    {
                        string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoPorcentaje * 100) / 100));
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                }
            }

        }
        return CopagoValor;
    }
    protected void DerivacionSaveLB_Click(object sender, EventArgs e)
    {
        int DerivacionId = 0;
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            int proveedorId = Convert.ToInt32(ProveedorDerivacionDDL.SelectedValue);
            Proveedor objproveedorMedico = ProveedorMedicoBLL.GetProveedorPrecioByProveedorId(proveedorId);
            int MedicoId = objproveedorMedico.MedicoId;

            if (!string.IsNullOrEmpty(DerivacionIdHF.Value))
                DerivacionId = Convert.ToInt32(DerivacionIdHF.Value);

            DateTime FechaCreacionDerivacion = GetFechaTabsCreacion(CasoId);
            if (DerivacionId <= 0)
            {
                /*if (PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                {*/
                try
                {
                    Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(Convert.ToInt32(CasoIdHF.Value));
                    string ValorES = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorES"];
                    string Respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, Convert.ToInt32(ClienteIdHF.Value), ValorES, "");
                    if (Respuesta == "")
                    {
                        string GastoRAW = System.Configuration.ConfigurationManager.AppSettings["GestionMedicaCurrentDerivacionGastoId"];
                        int GastoId = 0;

                        bool hello = int.TryParse(GastoRAW, out GastoId);

                        int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                        DerivacionEspecialista de = new DerivacionEspecialista();

                        de.DerivacionId = DerivacionId;
                        de.CasoId = CasoId;
                        de.MedicoId = MedicoId;
                        de.Observacion = ObservacionDerivacionTxt.Text;
                        de.FechaCreacion = FechaCreacionDerivacion;
                        de.UserId = UserId;
                        de.GastoId = (hello) ? GastoId : 0;

                        if (!DerivacionBLL.getDerivacionListByCasoIdandMedico_NEW(CasoId, MedicoId))
                        {

                            int NewDerivacionId = DerivacionBLL.InsertDerivacionNEW(de);
                            if (NewDerivacionId <= 0)
                                SystemMessages.DisplaySystemErrorMessage("Se inserto la nueva derivación a especialista correctamento, pero retorno valor incompleto.");
                            else
                            {


                                CasoEspecialidad objCasoEspecialidad = new CasoEspecialidad(CasoId, int.Parse(EspecialidadDerivacionComboBox.SelectedValue)
                                , objproveedorMedico.ProveedorId, objproveedorMedico.CostoConsulta, BusquedaDeValorCoPagoEspecialidad("Monto"), BusquedaDeValorCoPagoEspecialidad("Porcentaje"), FechaCreacionDerivacion);
                                CoPagosBLL.InsertCasoEspecialidad(objCasoEspecialidad);
                                SystemMessages.DisplaySystemMessage("Derivación a especialista insertada correctamente.");
                            }
                        }
                        else
                        {
                            SystemMessages.DisplaySystemErrorMessage("Ya se ha ingresado una derivacion para este Especialista");
                        }
                    }
                    else
                    {
                        SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");
                    }
                    //DerivacionBLL.AproveDerivacion(NewDerivacionId, UserId, DateTime.Now);
                }
                catch (Exception eq)
                {
                    throw eq;
                }
                /*  }
                  else
                      SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar una Derivación a Especialista.");
             */
            }
            else
            {
                if (DerivacionBLL.GetGastoIdDerivacion(DerivacionId) > 0)
                    SystemMessages.DisplaySystemErrorMessage("No se puede modificar la derivació a especialista por que tiene registros de gastos.");
                else
                {
                    DerivacionEspecialista de = new DerivacionEspecialista();

                    de.DerivacionId = DerivacionId;
                    de.MedicoId = MedicoId;
                    de.Observacion = ObservacionDerivacionTxt.Text;
                    de.CasoIdCreado = CasoId;


                    if (DerivacionBLL.UpdateDerivacionNEW(de))
                        SystemMessages.DisplaySystemMessage("La derivación a especialista fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar la derivación a especialista.");
                }
            }

            this.DerivacionRadGrid.DataBind();
            this.DerivacionIdHF.Value = "0";
            this.ObservacionDerivacionTxt.Text = "";
            this.CiudadDerivacionComboBox.ClearSelection();
            this.EspecialidadDerivacionComboBox.ClearSelection();

            //this.ProveedorDerivacionDDL.EmptyMessage = "Seleccione un Proveedor";
            this.ProveedorDerivacionDDL.ClearSelection();

            this.DerivacionRT.Selected = true;
            this.DerivacionRPV.Selected = true;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la derivación a especialista.");
            log.Error("Function DerivacionSaveLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
    }
    protected void DerivacionRadGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        try
        {
            DerivacionToPrintRadGrid.DataSourceID = DerivacionRadGrid.DataSourceID;
            DerivacionToPrintRadGrid.DataBind();
            string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            ExportGridToPdf(DerivacionToPrintRadGrid, codigoCaso + "_Derivacion_Especialista", "DERIVATION", codigoCaso);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al exportar la derivación a especialista.");
            log.Error("Function DerivacionRadGrid_ExportToPdfButton_Click on page CasoMedicoDetalle.aspx", q);
        }
    }
    protected void DerivacionRadGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region Internacion

    protected void InternacionODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de las internaciones.");
            e.ExceptionHandled = true;
        }
    }

    protected void ProveedorInternacionODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de los proveedores para internación.");
            e.ExceptionHandled = true;
        }
    }

    protected void InternacionRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            GridDataItem da = e.Item as GridDataItem;
            try
            {
                Internacion ObjInternacion = (Internacion)e.Item.DataItem;

                if (!ObjInternacion.Modify)
                {
                    ImageButton DetailsImageButton = (ImageButton)e.Item.FindControl("DetailsImageButton");
                    if (DetailsImageButton != null)
                    {
                        DetailsImageButton.ImageUrl = "~/Images/Neutral/edit_disable.png";
                        DetailsImageButton.Enabled = false;
                        DetailsImageButton.ToolTip = ToolTipCannotUpdateByapproved;
                    }
                }

                //if (InternacionRadGrid.MasterTableView.GetColumn("TemplateRegistroInternacion").Visible)
                //{
                //    int MontoTotal = 0;

                //    try
                //    {
                //        MontoTotal = Convert.ToInt32(DataBinder.Eval(da.DataItem, "MontoTotal"));
                //        bool DeudaPago = MontoTotal > 0;
                //        da["TemplateRegistroInternacion"].FindControl("MontoTotal").Visible = DeudaPago;
                //    }
                //    catch
                //    {
                //    }
                //}
                //if (ObjInternacion.GastoId > 0)
                //{

                    ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                //    if (deleteButton != null)
                //    {
                //        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                //        deleteButton.Enabled = false;
                //        deleteButton.ToolTip = ToolTipCannotDeleteHaveGasto;
                //    }
                //}

                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                ImageButton deleteButton1 = (ImageButton)e.Item.FindControl("DeleteImageButton");
                ValidacionBLL obj = new ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {

                }
                else
                {
                   // deleteButton1.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                    deleteButton.Visible = false;
                }
               
                
                string PermisoInsertarEmergenciaPago = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoInsertarEmergenciaPago"];
                ImageButton ImgBtn = (ImageButton)item["TemplateRegistroInternacion"].FindControl("EditCirugia");
                ValidacionBLL permisosInternacion = new ValidacionBLL();
                if (permisosInternacion.VerificarPermisosInternacionYCirugia())
                {

                    int ValorInternacionId = Convert.ToInt32(DataBinder.Eval(da.DataItem, "InternacionId"));

                    Internacion_Cirugia ObjInternacionCirugia = Internacion_CirugiaBLL.GetInternacionCirugiaxId(ValorInternacionId);
                    Internacion_Internacion ObjInternacionInternacion = Internacion_InternacionBLL.GetInternacionxId(ValorInternacionId);

                    if (ObjInternacionCirugia != null | ObjInternacionInternacion != null)
                    {
                        ImgBtn.Visible = true;
                    }
                    else
                    {
                        ImgBtn.Visible = false;
                    }

                }
                else
                {
                    ImgBtn.Visible = false;
                }
                GridDataItem item2 = (GridDataItem)e.Item;
      
               
               




                if (ObjInternacion.Aprovado)
                {
                    ImageButton AprobarColumn = (ImageButton)e.Item.FindControl("AprobarImageButton");
                    if (AprobarColumn != null)
                    {
                        AprobarColumn.Visible = false;
                        AprobarColumn.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Internación.");
                log.Error("Function InternacionRadGrid_ItemDataBound on page CasoMedicoDetalle.aspx", ex);
            }
        }
    }

    protected void InternacionRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":
                try
                {
                    int InternacionId = Convert.ToInt32(e.CommandArgument.ToString());
                    Internacion objInternacion = InternacionBLL.getInternacionByInternacionId(InternacionId);

                    if (objInternacion != null)
                    {
                        this.InternacionIdHF.Value = objInternacion.InternacionId.ToString();
                        this.ProveedorInternacionDDL.SelectedValue = objInternacion.ProveedorId.ToString();
                        this.ObservacionInternacionTxt.Text = objInternacion.Observaciones;
                        if (!objInternacion.Modify)
                        {
                            this.MessageInternacionLabel.Text = "No se puede modificar la internación por que ya esta aprobado.";
                            this.InternacionSaveLB.Visible = false;
                        }
                        //abrir popup
                        ClientScript.RegisterStartupScript(this.GetType(), "Internacion", "OpenPopupInternacion();", true);
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener la internación.");
                    log.Error("Function InternacionRadGrid_ItemCommand on page CasoMedicoDetalle.aspx", ex);
                }
                break;

            case "Delete":
                try
                {
                    int InternacionId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InternacionId"];
                    Internacion_Cirugia PagoCirugia = Internacion_CirugiaBLL.GetInternacionCirugiaxId(InternacionId);
                    Internacion_Internacion PagoInternacion = Internacion_InternacionBLL.GetInternacionxId(InternacionId);
                    bool EsCirugia = true; ;
                    bool Existe = true;
                    if (PagoCirugia != null)
                    {
                        EsCirugia = true;
                    }
                    else
                    {
                        if (PagoInternacion != null)
                            EsCirugia = false;
                        else
                            Existe = false;
                    }
                    if (Existe)
                    {
                        //if (InternacionBLL.GetGastoIdInternacion(InternacionId) > 0)
                        //    SystemMessages.DisplaySystemErrorMessage("No se puede eliminar la internación por que tiene registros de gastos.");
                        //else
                        {
                            if (EsCirugia)
                            {
                                if (PagoCirugia.detMontoTotal == 0)
                                {
                                    if (Internacion_CirugiaBLL.DeleteInternacion_Cirugia(InternacionId))
                                    {
                                        if (InternacionBLL.DeleteInternacion(InternacionId))
                                            SystemMessages.DisplaySystemMessage("Internación eliminado correctamente.");
                                        else
                                            SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la internación.");
                                    }
                                    else
                                        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la internación.");
                                }
                                else
                                {
                                    SystemMessages.DisplaySystemErrorMessage("No se Puede Eliminar la Internación tiene un monto guardado.");
                                }
                            }
                            else
                            {
                                if (PagoInternacion.detMontoTotal == 0)
                                {
                                    if (Internacion_InternacionBLL.DeleteInternacion_Internacion(InternacionId))
                                    {
                                        if (InternacionBLL.DeleteInternacion(InternacionId))
                                            SystemMessages.DisplaySystemMessage("Internación eliminado correctamente.");
                                        else
                                            SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la internación.");
                                    }
                                    else
                                        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la internación.");
                                }
                                else
                                {
                                    SystemMessages.DisplaySystemErrorMessage("No se Puede Eliminar la Internación tiene un monto guardado.");
                                }

                            }
                        }
                    }
                    else
                    {
                        //if (InternacionBLL.GetGastoIdInternacion(InternacionId) > 0)
                        //    SystemMessages.DisplaySystemErrorMessage("No se puede eliminar la internación por que tiene registros de gastos.");
                        //else
                        {
                            if (InternacionBLL.DeleteInternacion(InternacionId))
                                SystemMessages.DisplaySystemMessage("Internación eliminado correctamente.");
                            else
                                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la internación.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar la internación.");
                    log.Error("Function InternacionRadGrid_ItemCommand deleting on page CasoMedicoDetalle.aspx", ex);
                }
                break;
            case "Aprobar":
                try
                {
                    int InternacionId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InternacionId"];
                    int CasoId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CasoId"];

                    if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                    {
                        InternacionRadGrid.DataBind();
                        SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Aprobar la Internación.");
                        break;
                    }
                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    if (InternacionBLL.AproveInternacion(InternacionId, UserId, DateTime.Now))
                        SystemMessages.DisplaySystemMessage("La Internación del caso medico fue aprobado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo aprobar la Internación del caso medico.");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al aprobar la Internación del caso medico.");
                    log.Error("Function InternacionRadGrid_ItemCommand Aprobating on page CasoMedicoDetalle.aspx", ex);
                }
                InternacionRadGrid.DataBind();
                break;
            case "Detalles":
                try
                {
                    string CadenaRecibida = e.CommandArgument.ToString();

                    char separador = ';'; // separador de datos
                    string[] arregloDeSubCadenas = CadenaRecibida.Split(separador);
                    int IdInternacionRecibida = int.Parse(arregloDeSubCadenas[0].ToString());
                    string TipoEsCirugiaoInternacion = arregloDeSubCadenas[1].ToString();
                    string ValorCodigoArancelariadoId = (arregloDeSubCadenas[2].ToString());
                    InternacionUpdateIdHF.Value = IdInternacionRecibida.ToString();
                    if (TipoEsCirugiaoInternacion.Contains("CIRUGÍA"))
                    {
                        //abrir popup Cirugia
                        ClientScript.RegisterStartupScript(this.GetType(), "Internacion", "OpenPopupEditCirugiaPanel();", true);

                        Internacion_Cirugia ObjInternacionCirugia = Internacion_CirugiaBLL.GetInternacionCirugiaxId(IdInternacionRecibida);

                        CargarDatosCirugia(ObjInternacionCirugia);
                        if (ObjInternacionCirugia.detMontoCoPago > 0)
                        {
                            TextValorCoPagoCirugia.Text = double.Parse(ObjInternacionCirugia.detMontoCoPago.ToString()).ToString("N2").Replace(".", "").Replace(",", ".");
                            TextMontoCoPagoCirugia.Text = TextValorCoPagoCirugia.Text;
                            TextMontoBsCirugia.Text = TextMontoCoPagoCirugia.Text;
                            if (ObjInternacionCirugia.detMontoTotal == 0)
                            {
                             //   TextMontoBsCirugia.Text = string.Format("{0:n2}", (Math.Truncate(ObjInternacionCirugia.detMontoTotal * 100) / 100)).Replace(",", ".");
                                HabilitarCirugia(true);
                            }
                            else
                            {
                                //TextMontoBsCirugia.Text = string.Format("{0:n2}", (Math.Truncate((Convert.ToDouble(ObjInternacionCirugia.detMontoTotal) - Convert.ToDouble(TextMontoCoPagoCirugia.Text)) * 100) / 100)).Replace(",", ".");
                               // TextMontoBsCirugia.Text = Convert.ToDecimal(ObjInternacionCirugia.detMontoTotal - ObjInternacionCirugia.detMontoCoPago).ToString("N2");
                                HabilitarCirugia(false);

                            }
                            LabelValorCoPagoCirugia.Text = "Co-Pago Referencial";
                            LabelValorCoPagoCirugia.CssClass = "auto-style4";
                        }
                        else
                        {
                            if (ObjInternacionCirugia.detPorcentajeCoPago > 0)
                            {
                                TextValorCoPagoCirugia.Text = string.Format("{0:n2}", (Math.Truncate(ObjInternacionCirugia.detPorcentajeCoPago * 100) / 100));

                                if (ObjInternacionCirugia.detMontoTotal == 0)
                                {
                                    TextMontoCoPagoCirugia.Text = "0";
                                    TextMontoBsCirugia.Text = "0";
                                    HabilitarCirugia(true);

                                }
                                else
                                {
                                    TextMontoCoPagoCirugia.Text = Convert.ToDouble(ObjInternacionCirugia.detMontoTotal * (ObjInternacionCirugia.detPorcentajeCoPago / 100)).ToString("N2").Replace(".", "").Replace(",", ".");

                                    // TextMontoBsCirugia.Text = string.Format("{0:n2}", (Math.Truncate(Convert.ToDouble(ObjInternacionCirugia.detMontoTotal) - (Convert.ToDouble(TextMontoCoPagoCirugia.Text))) * 100) / 100).Replace(",", ".");
                                    TextMontoBsCirugia.Text = TextMontoCoPagoCirugia.Text;
                                        //Convert.ToDecimal((ObjInternacionCirugia.detMontoTotal - (ObjInternacionCirugia.detMontoTotal * (ObjInternacionCirugia.detPorcentajeCoPago / 100)))).ToString("N2");

                                    HabilitarCirugia(false);
                                }
                                LabelValorCoPagoCirugia.Text = "Co-Pago Referencial en %";
                                LabelValorCoPagoCirugia.CssClass = "auto-style4";

                            }
                            else
                            {
                                TextValorCoPagoCirugia.Text = "0";
                            }
                        }

                        List<RedCirugiasPrestaciones> ListaCirugiasPrestaciones = RedCirugiasPrestacionesBLL.GetCirugiasPrestacionesNotSaved(int.Parse(ClienteIdHF.Value), false);
                        for (int i = 0; i < ListaCirugiasPrestaciones.Count; i++)
                        {
                            if (ListaCirugiasPrestaciones[i].CodigoArancelarioId == ValorCodigoArancelariadoId)
                            {
                                VerMontoTopeHF.Value = ListaCirugiasPrestaciones[i].detMontoTope.ToString();
                            }
                        }

                    }
                    else
                    {

                        if (TipoEsCirugiaoInternacion.Contains("INTERNACIÓN"))
                        {
                            //abrir popup Internacion
                            ClientScript.RegisterStartupScript(this.GetType(), "Internacion", "OpenPopupEditInternacionPanel();", true);
                            Internacion_Internacion ObjInternacionInternacion = Internacion_InternacionBLL.GetInternacionxId(IdInternacionRecibida);

                            CargarDatosInternacion(ObjInternacionInternacion);
                            if (ObjInternacionInternacion.detMontoCoPago > 0)
                            {
                                TextValorCoPagoInternacion.Text = (((ObjInternacionInternacion.detMontoCoPago * 100) / 100)).ToString("N2").Replace(".", "").Replace(",",".");
                                TextMontoCoPagoInternacion.Text = TextValorCoPagoInternacion.Text;

                                TextMontoBsInternacion.Text = TextMontoCoPagoInternacion.Text;

                                if (ObjInternacionInternacion.detMontoTotal > 0)
                                {
                                     HabilitarInternacion(false);
                                }
                                else
                                {
                                    HabilitarInternacion(true);
                                    TextMontoBsInternacion.Text = "0";

                                }
                                LabelValorCoPagoInternacion.Text = "Co-Pago Referencial";
                                LabelValorCoPagoInternacion.CssClass = "auto-style4";

                            }
                            else
                            {
                                if (ObjInternacionInternacion.detPorcentajeCoPago > 0)
                                {

                                    TextValorCoPagoInternacion.Text = string.Format("{0:n2}", (Math.Truncate(ObjInternacionInternacion.detPorcentajeCoPago))).ToString().Replace(",", ".");

                                    if (ObjInternacionInternacion.detMontoTotal > 0)
                                    {
                                        TextMontoCoPagoInternacion.Text =((ObjInternacionInternacion.detMontoTotal * (ObjInternacionInternacion.detPorcentajeCoPago / 100))).ToString("N2").Replace(".", "");
                                        TextMontoCoPagoInternacion.Text = TextMontoCoPagoInternacion.Text.Replace(",", ".");
                                        TextMontoBsInternacion.Text = TextMontoCoPagoInternacion.Text;
                                            //Convert.ToDouble(ObjInternacionInternacion.detMontoTotal - (ObjInternacionInternacion.detMontoTotal * (ObjInternacionInternacion.detPorcentajeCoPago / 100))).ToString();
                                        HabilitarInternacion(false);

                                    }
                                    else {
                                        TextMontoBsInternacion.Text = "0";
                                        TextMontoCoPagoInternacion.Text = "0";
                                        HabilitarInternacion(true);

                                    }
                                    LabelValorCoPagoInternacion.Text = "Co-Pago Referencial en %";
                                    LabelValorCoPagoInternacion.CssClass = "auto-style4";


                                }

                            }
                            string ValorIN = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorIN"];
                            List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
                            for (int i = 0; i < ListaPrestaciones.Count; i++)
                            {
                                if (ListaPrestaciones[i].TipoPrestacion == ValorIN)
                                {

                                    // VerMontoTopeHF.Value = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].MontoTope * 100) / 100));
                                    VerMontoTopeHF.Value = ListaPrestaciones[i].MontoTope.ToString().Replace(",", ".");
                                    break;

                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener la internación.");
                    log.Error("Function InternacionRadGrid_ItemCommand on page CasoMedicoDetalle.aspx", ex);
                }
                break;
        }
    }
    private void CargarDatosCirugia(Internacion_Cirugia ObjInternacionCirugia)
    {

        TextValorUma.Text = ObjInternacionCirugia.detValorUma.ToString("N2").Replace(".", "").Replace(",", ".");
        TextCantidadUma.Text = ObjInternacionCirugia.detCantidadUma.ToString("N2").Replace(".", "").Replace(",", ".");
        TextPorcentajeCirujano.Text = ObjInternacionCirugia.detPorcentajeCirujano.ToString("N2").Replace(".", "").Replace(",", ".");
        TextMontoCirujano.Text = ObjInternacionCirugia.detMontoCirujano.ToString("N2").Replace(".", "").Replace(",", ".");
        TextPorcentajeAnestesiologo.Text = ObjInternacionCirugia.detPorcentajeAnestesiologo.ToString("N2").Replace(".", "").Replace(",", ".");
        TextMontoAnestesiologo.Text = ObjInternacionCirugia.detMontoAnestesiologo.ToString("N2").Replace(".", "").Replace(",", ".");
        TextPorcentajeAyudante.Text = ObjInternacionCirugia.detPorcentajeAyudante.ToString("N2").Replace(".", "").Replace(",", ".");
        TextMontoAyudante.Text = ObjInternacionCirugia.detMontoAyudante.ToString("N2").Replace(".", "").Replace(",", ".");
        TextPorcentajeInstrumentista.Text = ObjInternacionCirugia.detPorcentajeInstrumentista.ToString("N2").Replace(".", "").Replace(",", ".");
        TextMontoInstrumentista.Text = ObjInternacionCirugia.detMontoInstrumentista.ToString("N2").Replace(".", "").Replace(",", ".");
        TextMontoTotalCirugia.Text = ObjInternacionCirugia.detMontoTotal.ToString("N2").Replace(".","").Replace(",",".");

    }
    private void HabilitarCirugia(bool estado)
    {
        if (estado)
        {
            TextValorUma.Enabled = true;
            TextCantidadUma.Enabled = true;
            TextPorcentajeCirujano.Enabled = true;
            TextPorcentajeAnestesiologo.Enabled = true;
            TextPorcentajeAyudante.Enabled = true;
            TextPorcentajeInstrumentista.Enabled = true;
            BtnUploadInternacionCirugia.Visible = true;

        }
        else
        {
            TextValorUma.Enabled = false;
            TextCantidadUma.Enabled = false;
            TextPorcentajeCirujano.Enabled = false;
            TextPorcentajeAnestesiologo.Enabled = false;
            TextPorcentajeAyudante.Enabled = false;
            TextPorcentajeInstrumentista.Enabled = false;
            BtnUploadInternacionCirugia.Visible = false;
        }
    }
    private void CargarDatosInternacion(Internacion_Internacion ObjInternacionInternacion)
    {

        TextMontoEmergencia.Text = ObjInternacionInternacion.detMontoEmergencia.ToString().Replace(",", ".");
        TextMontoHonorarioMedico.Text = ObjInternacionInternacion.detMontoHonorariosMedicos.ToString().Replace(",", ".");
        TextMontoFarmacia.Text = ObjInternacionInternacion.detMontoFarmacia.ToString().Replace(",", ".");
        TextMontoLaboratorio.Text = ObjInternacionInternacion.detMontoLaboratorios.ToString().Replace(",", ".");
        TextMontoEstudios.Text = ObjInternacionInternacion.detMontoEstudios.ToString().Replace(",", ".");
        TextMontoHospitalizacion.Text = ObjInternacionInternacion.detMontoHospitalizacion.ToString().Replace(",", ".");
        TextMontoOtros.Text = ObjInternacionInternacion.detMontoOtros.ToString().Replace(",", ".");
        TextMontoTotalInternacion.Text = ObjInternacionInternacion.detMontoTotal.ToString("N2");
        TextMontoTotalInternacion.Text = TextMontoTotalInternacion.Text.Replace(".", "").Replace(",",".");

    }
    private void HabilitarInternacion(bool estado)
    {
        if (estado)
        {
            TextMontoEmergencia.Enabled = true;
            TextMontoHonorarioMedico.Enabled = true;
            TextMontoFarmacia.Enabled = true;
            TextMontoLaboratorio.Enabled = true;
            TextMontoEstudios.Enabled = true;
            TextMontoHospitalizacion.Enabled = true;
            TextMontoOtros.Enabled = true;
            BtnUploadInternacion.Visible = true;
        }
        else
        {
            TextMontoEmergencia.Enabled = false;
            TextMontoHonorarioMedico.Enabled = false;
            TextMontoFarmacia.Enabled = false;
            TextMontoLaboratorio.Enabled = false;
            TextMontoEstudios.Enabled = false;
            TextMontoHospitalizacion.Enabled = false;
            TextMontoOtros.Enabled = false;
            BtnUploadInternacion.Visible = false;
        }
    }
    private decimal BusquedaDeValorCoPagoInternacionCirugia(string TipodePago)
    {
        decimal CopagoValor = 0;
        string ValorCI = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorCI"];

        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion == ValorCI)
            {
                if (TipodePago == "Monto")
                {
                    string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoMonto * 100) / 100));
                    CopagoValor = decimal.Parse(ValorCoPago);
                    break;
                }
                else
                {
                    if (TipodePago == "Porcentaje")
                    {
                        string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoPorcentaje * 100) / 100));
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                }
            }

        }
        return CopagoValor;
    }
    private decimal BusquedaDeValorCoPagoInternacionInternacion(string TipodePago)
    {
        decimal CopagoValor = 0;
        string ValorIN = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorIN"];

        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion == ValorIN)
            {
                if (TipodePago == "Monto")
                {
                    string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoMonto * 100) / 100));
                    CopagoValor = decimal.Parse(ValorCoPago);
                    break;
                }
                else
                {
                    if (TipodePago == "Porcentaje")
                    {
                        string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoPorcentaje * 100) / 100));
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                }
            }

        }
        return CopagoValor;
    }
    protected void InternacionSaveLB_Click(object sender, EventArgs e)
    {

        int InternacionId = 0;
        try
        {
            if (EsCirugiaHF.Value.ToString() == "1")
            {
                string ValorCI = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorCI"];

                int CasoId = Convert.ToInt32(CasoIdHF.Value);

                int ProveedorId = Convert.ToInt32(ProveedorCirugiaDLL.SelectedValue);

                if (!string.IsNullOrEmpty(InternacionIdHF.Value))
                    InternacionId = Convert.ToInt32(InternacionIdHF.Value);

                DateTime FechaCreacionInternacion = GetFechaTabsCreacion(CasoId);
                if (InternacionId <= 0)
                {
                    Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(Convert.ToInt32(CasoIdHF.Value));
                    string NombreEstudioOdo = CodigoArancelarioRadComboBox.Text;
                    string Respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, Convert.ToInt32(ClienteIdHF.Value), ValorCI, NombreEstudioOdo);
                    if (Respuesta == "")
                    {
                        /* if (PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                         {*/
                        try
                        {
                            int NewInternacionId = InternacionBLL.InsertInternacion(CasoId, ProveedorId,
                            ObservacionCirugiaTxt.Text, CodigoArancelarioRadComboBox.SelectedValue, FechaCreacionInternacion);

                            if (NewInternacionId <= 0)
                                SystemMessages.DisplaySystemErrorMessage("Se inserto la nueva cirugía correctamente, pero retorno valor incompleto.");
                            else
                            {
                                Internacion_Cirugia objInternacionCirugia = new Internacion_Cirugia(NewInternacionId, CiudadCirugiaComboBox.SelectedValue, int.Parse(CirujanoRadComboBox.SelectedValue)
                                , int.Parse(EspecialidadCirujanoComboBox.SelectedValue), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, BusquedaDeValorCoPagoInternacionCirugia("Porcentaje"),
                                BusquedaDeValorCoPagoInternacionCirugia("Monto"), FechaCreacionInternacion);
                                Internacion_CirugiaBLL.InsertInternacionCirugia(objInternacionCirugia);
                                SystemMessages.DisplaySystemMessage("cirugía insertada correctamente.");

                            }

                            int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                            InternacionBLL.AproveInternacion(NewInternacionId, UserId, DateTime.Now);
                        }
                        catch (Exception ex)
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error de Insertar la cirugía ");
                            log.Error("Function InternacionSaveLB_Click on page CasoMedicoDetalle.aspx", ex);
                        }

                        /*}
                        else
                            SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar una Internación.");
                        */
                    }
                    else
                    {
                        SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");

                    }
                }
                else
                {
                    // if (InternacionBLL.GetGastoIdInternacion(InternacionId) > 0)
                    //  SystemMessages.DisplaySystemErrorMessage("No se puede modificar la internación por que tiene registros de gastos.");
                    //else
                    // {
                    if (InternacionBLL.UpdateInternacion(InternacionId, ProveedorId, ObservacionInternacionTxt.Text, CodigoArancelarioRadComboBox.SelectedValue))
                        SystemMessages.DisplaySystemMessage("La cirugía fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar la internación.");
                    //}
                }

            }
            if (this.EsCirugiaHF.Value.ToString() == "0")
            {
                int CasoId = Convert.ToInt32(CasoIdHF.Value);

                int ProveedorId = Convert.ToInt32(ProveedorInternacionDDL.SelectedValue);

                if (!string.IsNullOrEmpty(InternacionIdHF.Value))
                    InternacionId = Convert.ToInt32(InternacionIdHF.Value);

                DateTime FechaCreacionInternacion = GetFechaTabsCreacion(CasoId);
                if (InternacionId <= 0)
                {
                    string ValorIN = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorIN"];
                    Caso ObjCasoPoliza = CasoBLL.GetCasoByCasoId(Convert.ToInt32(CasoIdHF.Value));
                    string Respuesta = ValidacionBLL.ValidacionDePrestacion(ObjCasoPoliza.PolizaId, Convert.ToInt32(ClienteIdHF.Value), ValorIN, "");

                    if (Respuesta == "")
                    {
                        /*if (PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                        {*/
                        try
                        {
                            int NewInternacionId = InternacionBLL.InsertInternacion(CasoId, ProveedorId,
                                ObservacionInternacionTxt.Text, CodigoArancelarioRadComboBox.SelectedValue, FechaCreacionInternacion);

                            if (NewInternacionId <= 0)
                                SystemMessages.DisplaySystemErrorMessage("Se inserto la nueva internación correctamente, pero retorno valor incompleto.");
                            else
                            {
                                Internacion_Internacion objInternacionInternacion = new Internacion_Internacion(NewInternacionId, CiudadInternacionComboBox.SelectedValue, EnfermedadesInternacionComboBox.SelectedValue
                                , 0, 0, 0, 0, 0, 0, 0, 0, BusquedaDeValorCoPagoInternacionInternacion("Porcentaje"),
                               BusquedaDeValorCoPagoInternacionInternacion("Monto"), FechaCreacionInternacion);
                                Internacion_InternacionBLL.InsertInternacionInternacion(objInternacionInternacion);
                                SystemMessages.DisplaySystemMessage("Internación insertada correctamente.");

                            }

                            int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                            InternacionBLL.AproveInternacion(NewInternacionId, UserId, DateTime.Now);
                        }

                        catch (Exception ex)
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error de Insertar la Internación ");
                            log.Error("Function InternacionSaveLB_Click on page CasoMedicoDetalle.aspx", ex);
                        }
                        /*}
                        else
                            SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar una Internación.");
                         */
                    }
                    else
                    {
                        SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");
                    }
                }
                else
                {
                    /* if (InternacionBLL.GetGastoIdInternacion(InternacionId) > 0)
                         SystemMessages.DisplaySystemErrorMessage("No se puede modificar la internación por que tiene registros de gastos.");
                     else
                     {*/
                    if (InternacionBLL.UpdateInternacion(InternacionId, ProveedorId, ObservacionInternacionTxt.Text, CodigoArancelarioRadComboBox.SelectedValue))
                        SystemMessages.DisplaySystemMessage("La internación fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar la internación.");
                    //}
                }
            }
            this.InternacionRadGrid.DataBind();
            this.InternacionIdHF.Value = "0";
            this.ObservacionInternacionTxt.Text = "";
            this.ProveedorInternacionDDL.ClearSelection();
            this.EsCirugiaHF.Value = "0";
            this.InternacionRT.Selected = true;
            this.InternacionRPV.Selected = true;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la internación.");
            log.Error("Function InternacionSaveLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void SaveUpdateCirugiaLB_Click(object sender, EventArgs e)
    {
        try
        {

            if (decimal.Parse(TextMontoBsCirugiaHF.Value) > 0)
            {
                if (Internacion_CirugiaBLL.UpdateInternacion_Cirugia(int.Parse(InternacionUpdateIdHF.Value)
                  , decimal.Parse(TextValorUmaHF.Value.Replace(".", ","))
                  , int.Parse(TextCantidadUmaHF.Value.Replace(".", ","))
                  , decimal.Parse(TextPorcentajeCirujanoHF.Value.Replace(".", ","))
                  , decimal.Parse(TextMontoCirujanoHF.Value.Replace(".", ","))
                  , decimal.Parse(TextPorcentajeAnestesiologoHF.Value.Replace(".", ","))
                  , decimal.Parse(TextMontoAnestesiologoHF.Value.Replace(".", ","))
                  , decimal.Parse(TextPorcentajeAyudanteHF.Value.Replace(".", ","))
                  , decimal.Parse(TextMontoAyudanteHF.Value.Replace(".", ","))
                  , decimal.Parse(TextPorcentajeInstrumentistaHF.Value.Replace(".", ","))
                  , decimal.Parse(TextMontoInstrumentistaHF.Value.Replace(".", ","))
                  , decimal.Parse(TextMontoTotalCirugiaHF.Value.Replace(".", ","))
                  )
                  )

                    SystemMessages.DisplaySystemMessage("Internacion Cirugia actualizada.");

            }/*
             
            TextBox ObservacionesTextBox = (TextBox)CasoFV.FindControl("ObservacionesTextBox");
             int CasoId = Convert.ToInt32(CasoIdHF.Value);
             string CodigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
             CasoBLL.UpdateCasoMedico(CasoId, CodigoCaso, ObservacionesTextBox.Text);
             CasoFV.DataBind();*/


        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar la Internacion.");
            log.Error("Error al actualizar la Internacion de Cirugia ", q);
        }
    }
    protected void SaveUpdateInternacionInternacionLB_Click(object sender, EventArgs e)
    {
        try
        {

            if (decimal.Parse(TextMontoBsInternacionHF.Value) > 0)
            {
                if (Internacion_InternacionBLL.UpdateInternacion_Internacion(int.Parse(InternacionUpdateIdHF.Value)
                 , decimal.Parse(TextMontoEmergenciaHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoHonorarioMedicoHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoFarmaciaHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoLaboratorioHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoEstudiosHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoHospitalizacionHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoOtrosHF.Value.Replace(".", ","))
                 , decimal.Parse(TextMontoTotalInternacionHF.Value.Replace(".", ","))
                  )
                  )

                    SystemMessages.DisplaySystemMessage("Internacion Internacion actualizada.");

            }/*
             
            TextBox ObservacionesTextBox = (TextBox)CasoFV.FindControl("ObservacionesTextBox");
             int CasoId = Convert.ToInt32(CasoIdHF.Value);
             string CodigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
             CasoBLL.UpdateCasoMedico(CasoId, CodigoCaso, ObservacionesTextBox.Text);
             CasoFV.DataBind();*/


        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar la Internacion.");
            log.Error("Error al actualizar la Internacion ", q);
        }
    }
    protected void InternacionRadGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        try
        {
            InternacionToPrintRadGrid.DataSourceID = InternacionRadGrid.DataSourceID;
            InternacionToPrintRadGrid.DataBind();
            string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            ExportGridToPdf(InternacionToPrintRadGrid, codigoCaso + "_Internacion", "SPECIALIST", codigoCaso);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al exportar la Internación.");
            log.Error("Function InternacionRadGrid_ExportToPdfButton_Click on page CasoMedicoDetalle.aspx", q);
        }
    }
    protected void InternacionRadGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    protected void InternacionToPrintRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            int InternacionId = (int)DataBinder.Eval(e.Item.DataItem, "InternacionId");
            List<string> ids = new List<string>();
            ids.AddRange(ExportIDHF.Value.Split(new char[] { ',' }));
            if (ids.Contains(InternacionId.ToString()))
            {
                bool isInternacion = (bool)DataBinder.Eval(e.Item.DataItem, "isInternacion");

                InternacionToPrintRadGrid.Columns.FindByUniqueNameSafe("CodigoArancelarioTitle").Visible = !isInternacion;
                InternacionToPrintRadGrid.Columns.FindByUniqueNameSafe("CodigoArancelario").Visible = !isInternacion;
            }
        }
    }

    #endregion

    protected void HistorialLB_Click(object sender, EventArgs e)
    {
        try
        {
            int PacienteId = Convert.ToInt32(this.PacienteIdHF.Value);
            int CasoId = Convert.ToInt32(CasoIdHF.Value);

            Session["PacienteId"] = PacienteId.ToString();
            Session["CasoId"] = CasoId.ToString();
            if (!string.IsNullOrWhiteSpace(CitaDesgravamenIdHF.Value))
                Session["CITADESGRAVAMENID"] = CitaDesgravamenIdHF.Value;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener algunos datos para mostrar el historial del paciente.");
            log.Error("Function HistorialLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
        if (CasoDesgravamenHF.Value == true.ToString())
            Session["VM"] = "P"; // Print Historial

        Response.Redirect(UrlHistorial);
    }

    protected void RadGrid_PdfExporting(object sender, GridPdfExportingArgs e)
    {
        try
        {
            string rowHtml = e.RawHTML.ToString();
            if (((RadGrid)sender).ID == RecetaToPrintRadGrid.ID)
            {
                rowHtml = rowHtml.Replace(@"class=""indicaciones""", @"colspan=""5"" style=""border-bottom: 0.5pt dashed #000; padding-left: 10px;""");
                rowHtml = rowHtml.Replace(@"class=""medicamento""", @"colspan=""2""");
            }
            else if (((RadGrid)sender).ID == EstudioPrintRadGrid.ID)
            {
                string Observaciones = "";
                if (!string.IsNullOrWhiteSpace(ObservacionesHF.Value))
                {
                    Observaciones = @"<table border=""0"" style=""border-style:None;font-size:9px;width:100%;table-layout:auto;empty-cells:show;"">" +
                                    "	<colgroup>" +
                                    "		<col  />" +
                                    "	</colgroup>" +
                                    "	<tbody>" +
                                    "       <tr><td><b>OBSERVACIONES:</b></td></tr>" +
                                    "       <tr><td>" + ObservacionesHF.Value.ToUpper() + "</td></tr>" +
                                    "	</tbody>" +
                                    "</table>";
                    rowHtml += Observaciones;
                }
                else return;
            }
            else if (((RadGrid)sender).ID == DerivacionToPrintRadGrid.ID)
            {
                rowHtml = rowHtml.Replace(@"class=""Detalle""", @"colspan=""5""").Replace(@"class=""Separator""", @"colspan=""2""");
            }
            else if (((RadGrid)sender).ID == InternacionToPrintRadGrid.ID)
            {
                rowHtml = rowHtml.Replace(@"class=""Detalle""", @"colspan=""5""").Replace(@"class=""Separator""", @"colspan=""2""");
            }
            else
            {
                return;
            }
            e.RawHTML = rowHtml;
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
            log.Error("Function RadGrid_PdfExporting on page CasoMedicoDetalle.aspx", q);
        }
    }
    private bool addHeaders(RadGrid sender)
    {
        try
        {
            log.Debug("Añadiendo Encabezados (" + sender.ID + ")");
            PrintInfo row = null;
            int type = 0;
            if (sender.ID == RecetaToPrintRadGrid.ID)
            {
                type = 1;
            }
            else if (sender.ID == EstudioPrintRadGrid.ID)
            {
                type = 2;
            }
            else if (sender.ID == DerivacionToPrintRadGrid.ID)
            {
                type = 3;
            }
            else if (sender.ID == InternacionToPrintRadGrid.ID)
            {
                type = 4;
            }
            else if (sender.ID == OdontologiaForPrintRadGrid.ID)
            {
                type = 5;
            }
            else
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
                return false;
            }
            string imageHeader = @"<div style=""text-align:left;padding-bottom: 20px;""><img src=""" + ResolveClientUrl("~/Images/LogoPDF.jpg") + @""" style=""width:100px;height:23px;"" alt=""SISTEMA SISA"" /></div><br />";

            string title = "";
            string subtitle = "";
            string subsubtitle = "";
            log.Debug("Trayendo datos de BD (" + CasoIdHF.Value + ")");
            switch (type)
            {
                case 1:
                    row = RecetaBLL.GetRecetaHeadByCasoIdForPrint(Convert.ToInt32(CasoIdHF.Value));
                    title = @"RECETA MEDICA";
                    subtitle = "SRES.";
                    Proveedor objProveedor = ProveedorBLL.getProveedorByCasoIdReceta(Convert.ToInt32(CasoIdHF.Value));
                    subsubtitle = objProveedor.NombreJuridico;

                    break;
                case 2:
                    if (string.IsNullOrWhiteSpace(ExportIDHF.Value))
                    {
                        new ArgumentException("El ID de Estudios Complementarios es nulo o esta vacio.");
                    }
                    row = EstudioBLL.GetEstudioHeadByCasoIdForPrint(Convert.ToInt32(ExportIDHF.Value));
                    title = "EXAMENES COMPLEMENTARIOS";
                    subtitle = @"SOLICITO:";
                    ObservacionesHF.Value = row.otros;
                    break;
                case 3:
                    if (string.IsNullOrWhiteSpace(ExportIDHF.Value))
                    {
                        new ArgumentException("El ID de Derivacion es nulo o esta vacio.");
                    }
                    row = DerivacionBLL.GetDerivacionHeadByCasoIdForPrint(ExportIDHF.Value);
                    ObservacionesHF.Value = row.otros;
                    title = "ORDEN DE DERIVACION";
                    break;
                case 4:
                    if (string.IsNullOrWhiteSpace(ExportIDHF.Value))
                    {
                        new ArgumentException("El ID de Internacion es nulo o esta vacio.");
                    }
                    row = InternacionBLL.GetInternacionHeadByCasoIdForPrint(ExportIDHF.Value);
                    title = row.otros == "CIRUGIA" ? "ORDEN DE CIRUGÍA" : "ORDEN DE INTERNACIÓN";
                    break;
                case 5:
                    row = OdontologiaBLL.GetOdontologiaHeadByCasoIdForPrint(Convert.ToInt32(CasoIdHF.Value));
                    title = "PRESTACIONES ODONTOLÓGICAS";
                    break;
            }
            if (row == null)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
                return false;
            }
            log.Debug("Añadiendo Titulo (" + title + ")");
            title = @"<div style=""text-align:center;"">" + title + "</div>";




            string ContactoCBBA = @"<div style=""font-size:8px;padding-top:10px;border-top: 1px solid #29528A;text-align:justify;margin-left:-2px;margin-right: 33px;"">" +
                                 @"<div style=""margin-left:19px;"">" + getDireccionCochabamba() + "</div></div>";
            /*@"<div style=""font-size:8px;padding-top:10px;border-top:1px solid #29528A;text-align:center;margin-right: 54px;margin-left: -2px;"">" +
              "COCHABAMBA<br />TELF: 4531437<br />CEL: 69054669 - 79714819</div>";*/
            sender.ExportSettings.Pdf.PageFooter.RightCell.Text = @"<div style=""padding-bottom: 1px;"">&nbsp;</div>" + ContactoCBBA;
            //Tamaño del nombre de la poliza
            log.Debug("CI: " + row.CarnetIdentidad);
            int FontSize = 10;
            System.Drawing.Size textSize = System.Windows.Forms.TextRenderer.MeasureText(row.NombrePoliza, new System.Drawing.Font("Times Roman", FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel));
            int width = textSize.Width;
            double temp = ((double)width / 200);
            int height = (int)temp;
            if (height > 0)
            {
                height = temp <= height ? height : height + 2;
                height = (height * textSize.Height);
            }

            //Tamaño del nombre del paciente
            if (row.NombrePaciente.Length > 45)
            {
                string plus = "";
                int index = row.NombrePaciente.IndexOf(" ", 44);
                if (index < 0)
                    index = row.NombrePaciente.IndexOf(" ", 40);
                if (index < 0)
                {
                    index = 45;
                }
                row.NombrePaciente = row.NombrePaciente.Substring(0, index) + plus;
            }

            textSize = System.Windows.Forms.TextRenderer.MeasureText(row.NombrePaciente, new System.Drawing.Font("Times Roman", FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel));
            width = textSize.Width;
            temp = ((double)width / 229);
            int heightPac = (int)temp;
            if (heightPac > 0)
            {
                heightPac = temp <= heightPac ? heightPac : heightPac + 1;
                heightPac = (heightPac * textSize.Height);
            }

            string diagnostico = "";
            try
            {
                diagnostico += ((Label)CasoFV.FindControl("DiagnosticoPresuntivoTxt")).Text.Trim();
                string diagnostico2 = ((Label)CasoFV.FindControl("DiagnosticoPresuntivo2Txt")).Text.Trim();
                diagnostico += string.IsNullOrWhiteSpace(diagnostico2) ? "" : ", " + diagnostico2;
                string diagnostico3 = ((Label)CasoFV.FindControl("DiagnosticoPresuntivo3Txt")).Text.Trim();
                diagnostico += string.IsNullOrWhiteSpace(diagnostico3) ? "" : ", " + diagnostico3;
                //int index = diagnostico.IndexOf(" ", 45);
                //if (index < 0)
                //    index = diagnostico.IndexOf(" ", 40);
                //if (index < 0)
                //    index = 45;
                //diagnostico = diagnostico.Substring(0, index) + "...";
            }
            catch (Exception q) { log.Warn("Error al obtener diagnostico para exportacion a pdf.", q); }
            textSize = System.Windows.Forms.TextRenderer.MeasureText(diagnostico, new System.Drawing.Font("Arial", FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel));
            width = textSize.Width;
            temp = ((double)width / 150);
            int heightDiag = (int)temp;
            if (heightDiag > 0)
            {
                heightDiag = temp <= heightDiag ? heightDiag : heightDiag + 1;
                heightDiag = (heightDiag * textSize.Height);
            }
            string codigoCaso = "";
            try
            {
                codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            }
            catch (Exception q) { log.Warn("Error al obtener codigo del Caso para exportacion a pdf.", q); }
            //type 1 = Receta, 2 = Examenes comp, 3 = especialista, 4 = Internacion, 5 = Odontologia
            string HeaderLeft =
                @"<table border=""0"" style=""border-style:None;width:100%;table-layout:auto;empty-cells:show;font-size:" + FontSize + @"px;"">" +
                    @"<colgroup><col width=""55px"" /><col /></colgroup>\n\t<tbody>" +
                        @"<tr>" +
                            @"<td></td>" +
                            @"<td style=""" + (heightPac > 0 ? "height:" + heightPac + @".3px;" : "") + @"""><b>PACIENTE:</b></td>" +
                         @"</tr>" +
                        @"<tr>" +
                            @"<td></td>" +
                            @"<td><b>CÉDULA DE IDENTIDAD:</b></td>" +
                        @"</tr>" +
                        @"<tr>" +
                            @"<td></td>" +
                            @"<td><b>NÚMERO DE PÓLIZA:</b></td>" +
                        @"</tr>" +
                        @"<tr>" +
                            @"<td></td>" +
                            @"<td " + (height > 0 ? @"style=""height:" + height + @"px;""" : "") + "><b>NOMBRE DE PÓLIZA:</b></td>" +
                        "</tr>";
            if (type != 3)
            {
                HeaderLeft += @"<tr><td></td><td style=""text-align:left;""><b>MÉDICO:</b></td></tr>";
            }
            HeaderLeft +=
                @"<tr>" +
                    @"<td></td>" +
                    @"<td style=""" + (heightDiag > 0 ? "height:" + (heightDiag + 3) + @".5px;" : "") + @"text-align:left;""><b>DIAGNOSTICO:</b></td>" +
                @"</tr>" +
                @"</tbody>" +
            @"</table>";//+
      //  @"<div style=""border-top: 1px dashed #000;margin-right:135px;margin-left:540px;font-size: 1px;"">&nbsp;</div>";
            switch (type)
            {
                case 2:
                    HeaderLeft +=
                         @"<div style=""padding-top: 5px;padding-left:1px;margin-right:-2px;margin-left: 55px;text-align:left;font-size:" + FontSize + @"px;""><b>SRES.:</b></div>";
                    break;
                case 3:
                    HeaderLeft += 
                         @"<div style=""padding-top: 5px;padding-left:1px;margin-right:-2px;margin-left: 55px;text-align:left;font-size:" + (FontSize - 1) + @"px;""><b>DE:</b></div>";
                    break;
            }
            HeaderLeft += (subtitle == "" ? "" : @"<div style=""padding-top: 5px;margin-left: 55px;text-align:left;font-weight:bold;font-size:" + FontSize + @"px;"">" + subtitle + "</div>") +
            (subsubtitle == "" ? "" : @"<div style=""padding-top: 5px;margin-left: 55px;text-align:left;font-weight:bold;font-size:" + FontSize + @"px;"">" + subsubtitle + "</div>");

            //type 1 = Receta, 2 = Examenes comp, 3 = especialista, 4 = Internacion, 5 = Odontologia
            string HeaderMiddle = @"<table border=""0"" style=""border-style:None;width:100%;table-layout:auto;empty-cells:show;font-size:" + FontSize + @"px;"">" +
                            "<colgroup><col /></colgroup>\n\t<tbody>" +
                            @"<tr><td style=""" + (heightPac > 0 ? "height:" + heightPac + @"px;" : "") + @""">" + row.NombrePaciente.ToUpper() + "</td></tr>" +
                            "<tr><td>" + (string.IsNullOrWhiteSpace(row.CarnetIdentidad) ? "&nbsp;" : row.CarnetIdentidad) + "</td></tr>" +
                            "<tr><td>" + row.NumeroPoliza + "</td></tr>" +
                            @"<tr><td style=""" + (height > 0 ? "height:" + height + @"px;" : "") + @""">" + row.NombrePoliza + "</td></tr>" +
                            (type != 3 ? @"<tr><td style=""text-align:left;font-size:" + FontSize + @"px;"">" + row.MedicoNombre.ToUpper() + "</td></tr>" : "") +
                            @"<tr><td style=""" + (heightDiag > 0 ? "height:" + heightDiag + @"px;" : "") + @"text-align:left;margin-bottom:10px;"">" + diagnostico + "</td></tr>" +
                            "</tbody></table>" +
                            @"<div style=""border-top: 1px dashed #000;margin-right:330px;margin-left:330px;font-size: 1px;"">&nbsp;</div>" 
                            //@"<div style=""border-top: 1px dashed #000;font-size: 1px;"">&nbsp;</div>"
                            +
                            (type == 2 ?  @"<div style=""padding-top: 5px;text-align:left;font-size:" + FontSize + @"px;"">" + row.NombreProveedor.ToUpper() + "</div>" : "") +
                            (type == 3 ?  @"<div style=""padding-top: 5px;text-align:left;font-size:" + (FontSize - 1) + @"px;"">" + row.NombreProveedor.ToUpper() + "</div>" : "");

            //type 1 = Receta, 2 = Examenes comp, 3 = especialista, 4 = Internacion, 5 = Odontologia
            string HeaderRight = @"<table border=""0"" style=""border-style:None;width:100%;table-layout:auto;empty-cells:show;font-size:" + FontSize + @"px;"">" +
                            @"<colgroup><col /><col width=""32px"" /></colgroup>\n\t<tbody>" +
                            @"<tr><td style=""" + (heightPac > 0 ? "height:" + heightPac + @".5px;" : "") + @"""><b></b></td><td></td></tr>" +
                            "<tr><td></td><td></td></tr>" +
                            "<tr><td><b></b></td><td></td></tr>" +
                            "<tr><td " + (height > 0 ? @"style=""height:" + height + @"px;""" : "") + "></td><td></td></tr>" +
                            (type != 3 ? @"<tr><td style=""margin-right:32px;margin-left:-2px;text-align:left;font-size:" + FontSize + @"px;""><b>ESPECIALIDAD.:</b>&nbsp;" + row.Especialidad.ToUpper() + "</td><td></td></tr>" : "") +
                            @"<tr><td style=""" + (heightDiag > 0 ? "height:" + (heightDiag + 3) + @".3px;" : "") + @"text-align:left;""><b>CÓDIGO CASO: </b>" + codigoCaso + "</td><td></td></tr>" +
                            "</tbody></table> " +
                          
            //@"<div style=""border-top: 1px dashed #000;margin-left:-2px;margin-right:32px;font-size: 1px;"">&nbsp;</div>" +

            (type == 2 ? @"<div  style=""padding-top: 5px;text-align:left;margin-right:32px;margin-left:-2px;font-size:" + FontSize + @"px;"">&nbsp;</div>" : "") +
                            (type == 3 ? @"<div style=""padding-top: 5px;text-align:left;margin-right:32px;padding-left:3px;margin-left:-2px;font-size:" + (FontSize - 1) + @"px;""><b>ESPECIALIDAD.:</b>&nbsp;" + row.Especialidad.ToUpper() + "</div>" : "");

            sender.ExportSettings.Pdf.PageHeader.LeftCell.Text = @"<img src=""" + ResolveUrl("~/Images/LogoPDF1.jpg") + @""" alt=""SISTEMA SISA"" height=""60px"" width=""600px"" style=""position:absolute;margin-left: 50px;"" />" +
                "<br />" + (type != 1 && row.otros != "CIRUGIA" ? "<h2>&nbsp;<br />&nbsp;</h2>" : "<h2>&nbsp;</h2>") + "<br />" + HeaderLeft;
            //type 1 = Receta, 2 = Examenes comp, 3 = especialista, 4 = Internacion, 5 = Odontologia -20
            int pageTopMargin = (type == 1 ? 270 : (type == 2 ? 290 : (type == 3 ? 250 : (type == 4 ? 250 : 255)))) + (height > 0 ? (height > 60 ? 50 : 40) : 0) + (heightDiag > 0 ? 20 : 0) + (heightPac > 0 ? 20 : 0);
            sender.ExportSettings.Pdf.PageTopMargin = Unit.Pixel(pageTopMargin);
            sender.ExportSettings.Pdf.PageHeader.LeftCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Right;
            sender.ExportSettings.Pdf.PageHeader.MiddleCell.Text = @"<img src=""" + ResolveUrl("~/Images/LogoPDF3.jpg") + @""" alt=""SISTEMA SISA"" height=""60px"" width=""600px"" style=""position:absolute;margin-left: 50px;"" />" +
                    "<br /><h2>" + title + "</h2><br />" + HeaderMiddle;
            sender.ExportSettings.Pdf.PageHeader.RightCell.Text = @"<img src=""" + ResolveUrl("~/Images/LogoPDF3.jpg") + @""" alt=""SISTEMA SISA"" height=""60px"" width=""600px"" style=""position:absolute;margin-left: 50px;"" />" +
                "<br />" + (type != 1 && row.otros != "CIRUGIA" ? "<h2>&nbsp;<br />&nbsp;</h2>" : "<h2>&nbsp;</h2>") + "<br />" + HeaderRight;
            log.Debug("Encabezado Terminado");
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
            log.Error("Function RadGrid_PdfExporting on page CasoMedicoDetalle.aspx", q);
            return false;
        }
        return true;
    }

    private string getDireccionCochabamba()
    {
        string cbbaAddressRAW = System.Configuration.ConfigurationManager.AppSettings["FormularioCochabambaFooterAddress"];
        string cbbaAddress = "";

        if (!string.IsNullOrEmpty(cbbaAddressRAW))
        {
            cbbaAddress = cbbaAddressRAW.Replace("[", "<");
            cbbaAddress = cbbaAddress.Replace("]", ">");
        }
        return cbbaAddress;
    }

    public static Byte[] PdfSharpConvert(String html)
    {
        Byte[] res = null;
        using (MemoryStream ms = new MemoryStream())
        {
            PdfGenerateConfig config = new PdfGenerateConfig();
            config.PageOrientation = PdfSharp.PageOrientation.Portrait;
            config.PageSize = PdfSharp.PageSize.Letter;
            config.MarginBottom = 20;
            config.MarginTop = 20;
            config.MarginLeft = 20;
            config.MarginRight = 20;
            string css = "/* http://meyerweb.com/eric/tools/css/reset/     v2.0 | 20110126    License: none (public domain) */  html, body, div, span, applet, object, iframe, h1, h2, h3, h4, h5, h6, p, blockquote, pre, a, abbr, acronym, address, big, cite, code, del, dfn, em, img, ins, kbd, q, s, samp, small, strike, strong, sub, sup, tt, var, b, u, i, center, dl, dt, dd, ol, ul, li, fieldset, form, label, legend, table, caption, tbody, tfoot, thead, tr, th, td, article, aside, canvas, details, embed,  figure, figcaption, footer, header, hgroup,  menu, nav, output, ruby, section, summary, time, mark, audio, video {   margin: 0;   padding: 0;   border: 0;   font-size: 100%;   font: inherit;   vertical-align: baseline; } /* HTML5 display-role reset for older browsers */ article, aside, details, figcaption, figure,  footer, header, hgroup, menu, nav, section {   display: block; } body {   line-height: 1; } ol, ul {   list-style: none; } blockquote, q {   quotes: none; } blockquote:before, blockquote:after, q:before, q:after {   content: '';   content: none; } table {   border-collapse: collapse;   border-spacing: 0; } .alto20{height:20px;} .recetario td, .recetario th{border: solid 1px #000; line-height:10px; padding: 1px;} table th{vertical-align: top} .bold{ font-weight: bold;} #encabezado td{ padding: 0 5px;} .lineaLlenado{border-bottom:solid 1px #000;  height: 12px;} #footer{line-height:12px} #footer td{ padding: 0 10px;} .separacion{height:15px;} .recetario td{vertical-align:middle;} #footer th{vertical-align: top} #medicoOrigenDestino td{padding: 0px;}";
            CssData cssData = PdfGenerator.ParseStyleSheet(css, true);
            var pdf = PdfGenerator.GeneratePdf(html, config, cssData);
            pdf.Save(ms);
            res = ms.ToArray();
        }
        return res;
    }


    private void ExportGridToPdf(RadGrid grid, string fileName, string type, string codigoCaso)
    {
        try
        {
            log.Debug("Iniciando Exportacion a pdf (" + fileName + ")");
            int casoId = Convert.ToInt32(this.CasoIdHF.Value);
            switch (type)
            {
                case "PRESCRIPTION":
                    ArmarHtmlParaReceta(codigoCaso, fileName);
                    return;
                case "DERIVATION":
                    ArmarHtmlParaDerivaciones(codigoCaso, fileName);
                    return;
            }


            string contactoSCZ = @"<div style=""font-size:8px;padding-top:10px;text-align:justify;border-top: 1px solid #29528A; margin-left: 49px;margin-right: -2px;"">" +
                @"<div>" + getDireccionScz() + "</div></div>";

            /*@"<div style=""font-size:6px;padding-top:10px;border-top: 1px solid #29528A;text-align:center;margin-left: 49px;margin-right: 0px;"">" + 
             "SANTA CRUZ<br />TELF: 3393730<br />CEL: 69054676 - 78452426</div>";*/
            string contactoLP = @"<div style=""font-size:8px;padding-top:10px;border-top: 1px solid #29528A; text-align:justify;"">" +
                                 @"<div style=""margin-left: 20px;"">" + getDireccionLaPaz() + "</div></div>";

            /*@"<div style=""font-size:8px;padding-top:10px;border-top: 1px solid #29528A;"">" +
             "LA PAZ<br />CEL: 69054668 - 70120002</div>";*/

            grid.ExportSettings.Pdf.DefaultFontFamily = "Arial";
            grid.ExportSettings.Pdf.PageBottomMargin = Unit.Point(130);
            grid.ExportSettings.Pdf.PageFooter.LeftCell.Text = @"<div style=""font-size:9px;margin: 0 10px;padding-bottom: 5px;""><b>FECHA:</b> " + Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy hh:mm") + "</div>" + contactoSCZ;
            grid.ExportSettings.Pdf.PageFooter.LeftCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Right;
            grid.ExportSettings.Pdf.PageFooter.MiddleCell.Text = @"<div style=""font-size:9px;padding-bottom: 5px;"">FIRMA Y SELLO DEL MEDICO</div>" + contactoLP;
            grid.ExportSettings.Pdf.PageFooter.MiddleCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;
            grid.ExportSettings.Pdf.PageHeader.LeftCell.Text = "";
            grid.ExportSettings.FileName = fileName + Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy");
            grid.ExportSettings.Pdf.BorderType = GridPdfSettings.GridPdfBorderType.NoBorder;
            grid.ExportSettings.UseItemStyles = true;
            if (type == "PRESCRIPTION")
            {
                grid.GridLines = GridLines.Horizontal;
            }
           

            GridColumn col = grid.Columns.FindByUniqueNameSafe("RowNumber");
            if (col != null)
                col.Visible = true;
            isExport = grid.ID;
            RadGridExported.Value = grid.ID;
            if (addHeaders(grid))
            {
                grid.MasterTableView.ExportToPdf();
                CasoBLL.UpdateFechaEstado(casoId, type);
                log.Debug("pdf exportado");
            }
        }
        catch (Exception q)
        {
            log.Error("Error al exportar a PDF", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
        }
    }

    private string getDireccionScz()
    {

        string santacruzAddressRAW = System.Configuration.ConfigurationManager.AppSettings["FormularioSantaCruzFooterAddress"];
        string santacruzAddress = "";




        if (!string.IsNullOrEmpty(santacruzAddressRAW))
        {
            santacruzAddress = santacruzAddressRAW.Replace("[", "<");
            santacruzAddress = santacruzAddress.Replace("]", ">");
        }
        return santacruzAddress;
    }

    private string getDireccionLaPaz()
    {
        string lapazAddressRAW = System.Configuration.ConfigurationManager.AppSettings["FormularioLaPazFooterAddress"];
        string lapazAddress = "";
        if (!string.IsNullOrEmpty(lapazAddressRAW))
        {
            lapazAddress = lapazAddressRAW.Replace("[", "<");
            lapazAddress = lapazAddress.Replace("]", ">");
        }
        return lapazAddress;
    }
    private void ArmarHtmlParaDerivaciones(string codigoCaso, string fileName)
    {
        PrintInfo info = DerivacionBLL.GetDerivacionHeadByCasoIdForPrint(ExportIDHF.Value);
        string data = File.ReadAllText(Server.MapPath("~/HtmlTemplates/tempespecialista.html"));
        StringBuilder builder = new StringBuilder(data);
        builder.Replace("{LOGOPDFSRC}", System.Configuration.ConfigurationManager.AppSettings["URLPrincipal"] + "images/logopdf.png");
        builder.Replace("{PACIENTE}", info.NombrePaciente.ToUpper());
        builder.Replace("{CI}", info.CarnetIdentidad.ToUpper());
        builder.Replace("{NO_POLIZA}", info.NumeroPoliza.ToUpper());
        builder.Replace("{NOMBRE_POLIZA}", info.NombrePoliza.ToUpper());

        builder.Replace("{MEDICO_ORIGEN}", info.NombreProveedor.ToUpper());
        builder.Replace("{MEDICO_ORIGEN_ESP}", info.Especialidad.ToUpper());

        //int alturaNombrePaciente = TextUtilities.MeasureStringHeight(info.NombrePaciente, 150, new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel));
        string diagnostico = "";
        int alturaDiagnostico = 0;
        int margenTopFirma = 130;
        try
        {
            diagnostico += ((Label)CasoFV.FindControl("DiagnosticoPresuntivoTxt")).Text.Trim();
            string diagnostico2 = ((Label)CasoFV.FindControl("DiagnosticoPresuntivo2Txt")).Text.Trim();
            diagnostico += string.IsNullOrWhiteSpace(diagnostico2) ? "" : ", " + diagnostico2;
            string diagnostico3 = ((Label)CasoFV.FindControl("DiagnosticoPresuntivo3Txt")).Text.Trim();
            diagnostico += string.IsNullOrWhiteSpace(diagnostico3) ? "" : ", " + diagnostico3;
            //int index = diagnostico.IndexOf(" ", 45);
            //if (index < 0)
            //    index = diagnostico.IndexOf(" ", 40);
            //if (index < 0)
            //    index = 45;
            //diagnostico = diagnostico.Substring(0, index) + "...";
            alturaDiagnostico = TextUtilities.MeasureStringHeight(diagnostico, 300, new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)) + 8;
            margenTopFirma -= alturaDiagnostico;
        }
        catch (Exception e) { }
        builder.Replace("{DIAGNOSTICO}", diagnostico.ToUpper());
        builder.Replace("{ALTURA_DIAGNOSTICO}", alturaDiagnostico.ToString());
        builder.Replace("{CODIGO_CASO}", codigoCaso.ToUpper());
        builder.Replace("{NOMBRE_PROVEEDOR}", info.NombreProveedor.ToUpper());
        builder.Replace("{FECHA}", Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy hh:mm"));
        builder.Replace("{DIRECCION_SCZ}", getDireccionScz());
        builder.Replace("{DIRECCION_CBBA}", getDireccionCochabamba());
        builder.Replace("{DIRECCION_LP}", getDireccionLaPaz());

        DerivacionEspecialista objDerivacion = DerivacionBLL.getDerivacionByDerivacionId_NEW(Convert.ToInt32(ExportIDHF.Value));


        //string filaTrSin = "<tr><td>" + objReceta.Medicamento + "</td><td>" + objReceta.TipoMedicamentoNombre + "</td><td>" + objReceta.TipoConcentracionNombre + "</td><td>" + objReceta.Cantidad + "</td></tr>";
        //string filaTrCon = "<tr><td>" + objReceta.Medicamento + "</td><td>" + objReceta.TipoMedicamentoNombre + "</td><td>" + objReceta.TipoConcentracionNombre + "</td><td>" + objReceta.Cantidad + "</td><td>" + objReceta.Indicaciones + "</td></tr>";
        //medicamentosSinIndicacionHtml += filaTrSin;
        //medicamentosConIndicacionHtml += filaTrCon;
        //alturaParteArriba -= 25;
        builder.Replace("{MEDICO_DESTINO}", objDerivacion.MedicoNombre.ToUpper());

        builder.Replace("{MEDICO_DESTINO_ESP}", objDerivacion.EspecialidadNombre.ToUpper());

        builder.Replace("{OBSERVACIONES}", info.otros.ToUpper());
        builder.Replace("{MARGEN_TOP_FIRMA_FECHA}", margenTopFirma.ToString());

        //string footer = File.ReadAllText(Server.MapPath("~/HtmlTemplates/tempfooter.html"));
        //StringBuilder footerText = new StringBuilder(footer);
        //byte[] pdfFile = IronPDFConvert(builder.ToString(),footerText.ToString());
        log.Debug(builder);
        byte[] pdfFile = PdfSharpConvert(builder.ToString());
        //string FilePath = Server.MapPath("~/" + fileName + ".pdf"); //Replace this
        //File.WriteAllBytes(FilePath, pdfFile);

        //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //response.BinaryWrite(pdfFile);
        //response.ClearContent();
        //response.Clear();
        //response.ContentType = "application/pdf";
        //response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf;");
        //response.TransmitFile(FilePath);
        //response.Flush();
        //response.End();

        Response.Clear();
        MemoryStream ms = new MemoryStream(pdfFile);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
        Response.Buffer = true;
        ms.WriteTo(Response.OutputStream);
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }
    private void ArmarHtmlParaReceta(string codigoCaso, string fileName)
    {
        PrintInfo info = RecetaBLL.GetRecetaHeadByCasoIdForPrint(Convert.ToInt32(CasoIdHF.Value));
        string data = File.ReadAllText(Server.MapPath("~/HtmlTemplates/tempreceta.html"));
        StringBuilder builder = new StringBuilder(data);
        builder.Replace("{LOGOPDFSRC}", System.Configuration.ConfigurationManager.AppSettings["URLPrincipal"] + ResolveClientUrl("~/images/logopdf.png"));
        builder.Replace("{PACIENTE}", info.NombrePaciente.ToUpper());
        builder.Replace("{CI}", info.CarnetIdentidad.ToUpper());
        builder.Replace("{NO_POLIZA}", info.NumeroPoliza.ToUpper());
        builder.Replace("{NOMBRE_POLIZA}", info.NombrePoliza.ToUpper());
        builder.Replace("{MEDICO}", info.MedicoNombre.ToUpper());
        builder.Replace("{MEDICO_ESP}", info.Especialidad.ToUpper());
        int alturaParteArriba = 370;
        int alturaParteAbajo = 370;

        int alturaNombrePaciente = TextUtilities.MeasureStringHeight(info.NombrePaciente, 150, new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel));
        alturaParteArriba -= alturaNombrePaciente;
        alturaParteArriba -= 215;
        alturaParteAbajo -= 170;
        string diagnostico = "";
        int alturaDiagnostico = 0;
        try
        {
            diagnostico += ((Label)CasoFV.FindControl("DiagnosticoPresuntivoTxt")).Text.Trim();
            string diagnostico2 = ((Label)CasoFV.FindControl("DiagnosticoPresuntivo2Txt")).Text.Trim();
            diagnostico += string.IsNullOrWhiteSpace(diagnostico2) ? "" : ", " + diagnostico2;
            string diagnostico3 = ((Label)CasoFV.FindControl("DiagnosticoPresuntivo3Txt")).Text.Trim();
            diagnostico += string.IsNullOrWhiteSpace(diagnostico3) ? "" : ", " + diagnostico3;
            //int index = diagnostico.IndexOf(" ", 45);
            //if (index < 0)
            //    index = diagnostico.IndexOf(" ", 40);
            //if (index < 0)
            //    index = 45;
            //diagnostico = diagnostico.Substring(0, index) + "...";
            alturaDiagnostico = TextUtilities.MeasureStringHeight(diagnostico, 300, new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)) + 8;
            alturaParteArriba -= alturaDiagnostico;
        }
        catch (Exception e) { }
        builder.Replace("{DIAGNOSTICO}", diagnostico.ToUpper());
        builder.Replace("{ALTURA_DIAGNOSTICO}", alturaDiagnostico.ToString());
        builder.Replace("{CODIGO_CASO}", codigoCaso.ToUpper());
        builder.Replace("{NOMBRE_PROVEEDOR}", info.NombreProveedor.ToUpper());
        builder.Replace("{FECHA}", Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy hh:mm"));
        builder.Replace("{DIRECCION_SCZ}", getDireccionScz());
        builder.Replace("{DIRECCION_CBBA}", getDireccionCochabamba());
        builder.Replace("{DIRECCION_LP}", getDireccionLaPaz());
        string medicamentosSinIndicacionHtml = "";
        string medicamentosConIndicacionHtml = "";

        string[] idsExportadas = ExportIDHF.Value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);




        foreach (string id in idsExportadas)
        {
            Receta objReceta = RecetaBLL.GetRecetaByDetalleId(Convert.ToInt32(id));
            string filaTrSin = "<tr><td style=\"text-align:left;\">" + objReceta.Medicamento + "</td><td style=\"text-align:left;\">" + objReceta.TipoMedicamentoNombre + "</td><td style=\"text-align:left;\">" + objReceta.TipoConcentracionNombre + "</td><td>" + objReceta.Cantidad + "</td></tr>";
            string filaTrCon = "<tr><td style=\"text-align:left;\">" + objReceta.Medicamento + "</td><td style=\"text-align:left;\">" + objReceta.TipoMedicamentoNombre + "</td><td style=\"text-align:left;\">" + objReceta.TipoConcentracionNombre + "</td><td>" + objReceta.Cantidad + "</td><td style=\"text-align:left;\">" + objReceta.Indicaciones + "</td></tr>";
            medicamentosSinIndicacionHtml += filaTrSin;
            medicamentosConIndicacionHtml += filaTrCon;
            int alturaMedicamento = TextUtilities.MeasureStringHeight(objReceta.Medicamento, 150, new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel));
            alturaParteArriba -= alturaMedicamento;
            int alturaIndicaciones = TextUtilities.MeasureStringHeight(objReceta.Indicaciones, 150, new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel));
            alturaParteAbajo -= Math.Max(alturaMedicamento, alturaIndicaciones);
        }
        builder.Replace("{MARGIN_TOP_FIRMA}", alturaParteArriba > 0 ? alturaParteArriba.ToString() : "0");
        builder.Replace("{MARGIN_TOP_FOOTER}", alturaParteAbajo > 0 ? alturaParteAbajo.ToString() : "0");
        builder.Replace("{MEDICAMENTOS_SIN_INDICACION}", medicamentosSinIndicacionHtml);
        builder.Replace("{MEDICAMENTOS_CON_INDICACION}", medicamentosConIndicacionHtml);
        log.Debug(builder);
        //string footer = File.ReadAllText(Server.MapPath("~/HtmlTemplates/tempfooter.html"));
        //StringBuilder footerText = new StringBuilder(footer);
        //byte[] pdfFile = IronPDFConvert(builder.ToString(),footerText.ToString());
        byte[] pdfFile = PdfSharpConvert(builder.ToString());
        //string FilePath = Server.MapPath("~/" + fileName + ".pdf"); //Replace this
        //File.WriteAllBytes(FilePath, pdfFile);

        //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //response.BinaryWrite(pdfFile);
        //response.ClearContent();
        //response.Clear();
        //response.ContentType = "application/pdf";
        //response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf;");
        //response.TransmitFile(FilePath);
        //response.Flush();
        //response.End();

        Response.Clear();
        MemoryStream ms = new MemoryStream(pdfFile);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
        Response.Buffer = true;
        ms.WriteTo(Response.OutputStream);
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
    }

    private void GridColumnSafeDisplay(RadGrid grid, string column, bool display)
    {
        try
        {
            GridColumn gColumn = grid.MasterTableView.Columns.FindByUniqueName(column);
            if (gColumn != null) gColumn.Display = display;
        }
        catch { }
    }
    public void EstudioPrintRadGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            item.PrepareItemStyle();
        }
    }
    private void RadGrid_ItemCreated(RadGrid sender, GridItem Item)
    {
        if (isExport == sender.ID)
        {
            if (Item.ItemType == GridItemType.Item || Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)Item;
                string id = "";
                switch (sender.ID)
                {
                    case "RecetaPacienteRadGrid":
                    case "RecetaToPrintRadGrid":
                        id = "DetalleId";
                        break;
                    case "EstudioRadGrid":
                        id = "EstudioId";
                        break;
                    case "DerivacionToPrintRadGrid":
                        id = "DerivacionId";
                        break;
                    case "InternacionToPrintRadGrid":
                        id = "InternacionId";
                        break;
                    case "OdontologiaForPrintRadGrid":
                        id = "OdontologiaId";
                        break;
                }
                id = item.GetDataKeyValue(id).ToString();

                List<string> ids = new List<string>();
                ids.AddRange(ExportIDHF.Value.Split(new char[] { ',' }));
                if (!ids.Contains(id))
                {
                    Item.Display = false;
                }
                else
                {
                    item.PrepareItemStyle();
                }
            }
            else if (Item.ItemType == GridItemType.CommandItem)
            {
                Item.Visible = false;
            }
            else if (Item.ItemType == GridItemType.Footer)
            {
                isExport = "";
            }
        }
        else
        {
            if (Item.ItemType == GridItemType.Header)
            {
                try
                {
                    //int CasoId = Convert.ToInt32(this.CasoIdHF.Value);
                    switch (sender.ID)
                    {
                        case "RecetaGrid":
                            //FechaEstado = CasoBLL.GetCasoByCasoId(CasoId).FechaEstadoReceta;
                            FechaEstado = Convert.ToDateTime(FechaEstadoRecetaHF.Value);
                            break;
                        case "EstudioRadGrid":
                            //FechaEstado = CasoBLL.GetCasoByCasoId(CasoId).FechaEstadoExamenes;
                            FechaEstado = Convert.ToDateTime(FechaEstadoExamenesHF.Value);
                            break;
                        case "DerivacionRadGrid":
                            //FechaEstado = CasoBLL.GetCasoByCasoId(CasoId).FechaEstadoEspecialista;
                            FechaEstado = Convert.ToDateTime(FechaEstadoEspecialistaHF.Value);
                            break;
                    }
                }
                catch { }
            }
            else if ((Item.ItemType == GridItemType.Item || Item.ItemType == GridItemType.AlternatingItem) && FechaEstado != null)
            {
                GridDataItem dataBoundItem = Item as GridDataItem;
                if (dataBoundItem.DataItem != null)
                {
                    bool aprobado = true;
                    if (sender.ID == "EstudioRadGrid")
                    {
                        aprobado = ((Estudio)dataBoundItem.DataItem).Aprovado;
                    }
                    else if (sender.ID == "DerivacionRadGrid")
                    {
                        aprobado = true;//((Derivacion)dataBoundItem.DataItem).Aprovado;
                    }
                    if (!aprobado)
                    {
                        (Item.FindControl("ExportCheckBox") as CheckBox).Visible = false;
                    }
                    else
                    {

                        DateTime fecha = (sender.ID == "DerivacionRadGrid" || sender.ID == "DerivacionToPrintRadGrid") ? ((DerivacionEspecialista)dataBoundItem.DataItem).FechaCreacion : ((IFechaCreacion)dataBoundItem.DataItem).FechaCreacion;
                        if (FechaEstado != null)
                        {
                            //Si esta FechaEstado es anterior a fecha.
                            if (FechaEstado.CompareTo(fecha) < 0)
                            {
                                Control control = Item.FindControl("ExportCheckBox");
                                if (control != null)
                                    (control as CheckBox).Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
    protected string GetCasoFVControlClientID(string id)
    {
        string clientID = "";
        if (CasoFV.FindControl(id) != null)
            clientID = CasoFV.FindControl(id).ClientID;
        return clientID;
    }

    protected void AdminGastosLB_Click(object sender, EventArgs e)
    {
        int CasoId = 0;
        try
        {
            CasoId = Convert.ToInt32(this.CasoIdHF.Value);
            Session["CasoId"] = CasoId.ToString();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Casos Medico.");
            log.Error("Function AdminGastosLB_Click on page CasoMedicoLista.aspx, convert CasoID", ex);
            return;
        }
        Response.Redirect("~/Gasto/GastoDetalle.aspx");
    }
    protected void FileManager_Command(object sender, CommandEventArgs e)
    {
        FileManager.OpenFileManager(e.CommandName, Convert.ToInt32(e.CommandArgument), false);
    }
    public void FileManager_FileSave(string ObjectName, string type)
    {
        switch (ObjectName)
        {
            case "ODONTOLOGIA":
                OdontologiaRadGrid.DataBind();
                break;
            case "RECETAS":
                RecetaGrid.DataBind();
                break;
            case "ESTUDIO":
                EstudioRadGrid.DataBind();
                break;
            case "DERIVACIONES":
                DerivacionRadGrid.DataBind();
                break;
            case "INTERNACION":
                InternacionRadGrid.DataBind();
                break;
        }
    }
    protected void CasoFV_DataBound(object sender, EventArgs e)
    {
        if (CasoFV.DataItem == null)
            return;
        if ((bool)DataBinder.Eval(CasoFV.DataItem, "Dirty") == true &&
            CasoFV.CurrentMode != FormViewMode.Edit)
        {
            Session["CasoId"] = CasoIdHF.Value;

            Session["Mode"] = "Edit";

            if (!string.IsNullOrWhiteSpace(CitaDesgravamenIdHF.Value) && CitaDesgravamenIdHF.Value != "0")
            {
                Session["CITADESGRAVAMENID"] = CitaDesgravamenIdHF.Value;
            }
            if (!string.IsNullOrWhiteSpace(ReconsultaHF.Value))
            {
                Session["RECONSULTA"] = ReconsultaHF.Value;
            }
            Response.Redirect("~/CasoMedico/CasoMedicoDetalle.aspx", true);
            return;
        }

        if (CasoDesgravamenHF.Value == true.ToString())
        {
            HyperLink CancelHL = (HyperLink)CasoFV.FindControl("CancelHL");
            CancelHL.Text = "Retornar al caso desgravamen";
            CancelHL.NavigateUrl = "~/Desgravamen/ExamenMedico.aspx?CITADESGRAVAMENID=" + CitaDesgravamenIdHF.Value;
        }

        if (Session["DerivacionId"] != null && !string.IsNullOrEmpty(Session["DerivacionId"].ToString()))
        {
            HyperLink CancelHL = (HyperLink)CasoFV.FindControl("CancelHL");
            CancelHL.Text = "Retornar al listado de Derivaciones";
            CancelHL.NavigateUrl = "~/CasoMedico/ListaDerivaciones.aspx";
        }

        FechaEstadoRecetaHF.Value = DataBinder.Eval(CasoFV.DataItem, "FechaEstadoReceta").ToString();
        FechaEstadoExamenesHF.Value = DataBinder.Eval(CasoFV.DataItem, "FechaEstadoExamenes").ToString();
        FechaEstadoEspecialistaHF.Value = DataBinder.Eval(CasoFV.DataItem, "FechaEstadoEspecialista").ToString();

        if (CasoFV.CurrentMode == FormViewMode.Edit)
        {
            RadDatePicker FechaCreacion = (RadDatePicker)CasoFV.FindControl("FechaCreacion");
            FechaCreacion.MaxDate = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);

            string EnfermedadId = DataBinder.Eval(CasoFV.DataItem, "EnfermedadId").ToString();
            if (!string.IsNullOrWhiteSpace(EnfermedadId))
            {
                string Enfermedad = DataBinder.Eval(CasoFV.DataItem, "Enfermedad").ToString();
                RadComboBox EnfermedadesComboBox = (RadComboBox)CasoFV.FindControl("EnfermedadesComboBox");
                RadComboBoxItem item = new RadComboBoxItem("[" + EnfermedadId + "] " + Enfermedad, EnfermedadId);
                EnfermedadesComboBox.Items.Add(item);
                item.Selected = true;
            }
            string Enfermedad2Id = DataBinder.Eval(CasoFV.DataItem, "Enfermedad2Id").ToString();
            if (!string.IsNullOrWhiteSpace(Enfermedad2Id))
            {
                string Enfermedad = DataBinder.Eval(CasoFV.DataItem, "Enfermedad2").ToString();
                RadComboBox EnfermedadesComboBox = (RadComboBox)CasoFV.FindControl("Enfermedades2ComboBox");
                RadComboBoxItem item = new RadComboBoxItem("[" + Enfermedad2Id + "] " + Enfermedad, Enfermedad2Id);
                EnfermedadesComboBox.Items.Add(item);
                item.Selected = true;
            }
            string Enfermedad3Id = DataBinder.Eval(CasoFV.DataItem, "Enfermedad3Id").ToString();
            if (!string.IsNullOrWhiteSpace(Enfermedad3Id))
            {
                string Enfermedad = DataBinder.Eval(CasoFV.DataItem, "Enfermedad3").ToString();
                RadComboBox EnfermedadesComboBox = (RadComboBox)CasoFV.FindControl("Enfermedades3ComboBox");
                RadComboBoxItem item = new RadComboBoxItem("[" + Enfermedad3Id + "] " + Enfermedad, Enfermedad3Id);
                EnfermedadesComboBox.Items.Add(item);
                item.Selected = true;
            }

        }

        if (DataBinder.Eval(CasoFV.DataItem, "IsReconsulta").ToString().ToLower() == "true")
        {
            ReconsultaHF.Value = "1";
            CasoMedicoTitle.Text = "Registro de Reconsulta";
            //SubTitleLabel.Text = "Registro de Reconsulta";
            if (DataBinder.Eval(CasoFV.DataItem, "Dirty").ToString().ToLower() == "true")
            {
                CasoFV.ChangeMode(FormViewMode.Edit);
            }
        }
        EmergenciaRadGrid.DataBind();
        OdontologiaRadGrid.DataBind();
    }
    protected void CasoFV_ModeChanged(object sender, EventArgs e)
    {
        if (CasoFV.CurrentMode != FormViewMode.Edit)
        {
            this.RecetaRPV.Visible = true;
            this.ExComplementarioRPV.Visible = true;
            this.DerivacionRPV.Visible = true;
            this.InternacionRPV.Visible = true;

        }
    }
    protected void NewEnfermedadCronicaImageButton_Click(object sender, EventArgs e)
    {
        try
        {
            int aseguradoId = Convert.ToInt32(AseguradoIdHF.Value);
            RadComboBox EnfermedadCronicaRadComboBox = (RadComboBox)CasoFV.FindControl("EnfermedadCronicaRadComboBox");
            int enfermedadCronicaId = Convert.ToInt32(EnfermedadCronicaRadComboBox.SelectedValue);
            EnfermedadCronicaBLL.InsertEnfermedadCronicaAsegurado(aseguradoId, enfermedadCronicaId);
            Repeater EnfermedadesCronicasRepeater = (Repeater)CasoFV.FindControl("EnfermedadesCronicasRepeater");
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
            Repeater EnfermedadesCronicasRepeater = (Repeater)CasoFV.FindControl("EnfermedadesCronicasRepeater");
            EnfermedadesCronicasRepeater.DataBind();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al eliminar la enfermedad Crónica.");
            log.Error("Function DeleteEnfermedadCronicaImageButton_Command on page CasoMedicoLista.aspx", q);
        }
    }
    protected void SaveObservacionesLB_Click(object sender, EventArgs e)
    {
        try
        {
            TextBox ObservacionesTextBox = (TextBox)CasoFV.FindControl("ObservacionesTextBox");
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            string CodigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            CasoBLL.UpdateCasoMedico(CasoId, CodigoCaso, ObservacionesTextBox.Text);
            CasoFV.DataBind();
            SystemMessages.DisplaySystemMessage("Observación actualizada.");
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar la Observación.");
            log.Error("Error al actualizar la observacion de caso medico", q);
        }
    }
    protected void CasoFV_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }
    protected void PesoTextBox_DataBinding(object sender, EventArgs e)
    {
        RadNumericTextBox textbox = (RadNumericTextBox)CasoFV.FindControl("PesoTextBox");
        //textbox.Value = 10.00;
        if (Session["PesoDesgravamen"] != null && !string.IsNullOrEmpty(Session["PesoDesgravamen"].ToString())/*&&
            Session["EstaturaDesgravamen"] != null && !string.IsNullOrEmpty(Session["EstaturaDesgravamen"].ToString())*/)
        {
            try
            {
                double peso = 0.00;
                peso = Convert.ToDouble(Session["PesoDesgravamen"]);
                if (textbox.Value == 0.00)
                    textbox.Value = peso;

            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso");
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);

            }
            Session["PesoDesgravamen"] = null;
            //Session["EstaturaDesgravamen"] = null;
        }

    }
    protected void EstaturaTextBox_DataBinding(object sender, EventArgs e)
    {
        RadNumericTextBox textbox = (RadNumericTextBox)CasoFV.FindControl("EstaturaTextBox");

        if (Session["EstaturaDesgravamen"] != null &&
            !string.IsNullOrEmpty(Session["EstaturaDesgravamen"].ToString()))
        {
            try
            {
                double estatura = 0.00;
                estatura = Convert.ToDouble(Session["EstaturaDesgravamen"]);
                if (textbox.Value <= 0.30)
                    textbox.Value = estatura;
                /*else
                    textbox.Value = 0.00;*/
            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso");
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);

            }
            //Session["PesoDesgravamen"] = null;
            Session["EstaturaDesgravamen"] = null;
        }
        else
        {
            //textbox.Value = 0.00;
        }
    }

    protected void ProveedorExComplementarioDDL_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string valor = ProveedorExComplementarioDDL.SelectedValue;
        if (valor != null & valor != "")
        {
            try
            {
                List<TiposEstudiosProvPrestacionesCategoria> list = TiposEstudiosProvPrestacionesBLL.GetAllEstudioxProveedorxCategoria(Convert.ToInt32(valor));
                List<TiposEstudiosProvPrestacionesCategoria> modifiedList = new List<TiposEstudiosProvPrestacionesCategoria>();
                modifiedList.Add(new TiposEstudiosProvPrestacionesCategoria()
                {
                    categoria = "0",
                    nombre = "SELECCIONE UN ESTUDIO"
                });
                foreach (TiposEstudiosProvPrestacionesCategoria ciudad in list)
                {
                    modifiedList.Add(ciudad);
                }

                TipoEstudioPrestacionesExaComboBox.DataSource = modifiedList;
                TipoEstudioPrestacionesExaComboBox.DataValueField = "categoria";
                TipoEstudioPrestacionesExaComboBox.DataTextField = "nombre";
                TipoEstudioPrestacionesExaComboBox.DataBind();
                TipoEstudioPrestacionesExaComboBox.SelectedValue = "0";
            }
            catch (Exception q)
            {
                log.Error("No se puedo cargar los datos de Ciudad de la BD", q);
                SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Ciudades");
            }
           

        }
    }
    protected void PrestacionesExaComboBox_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string valor = ProveedorExComplementarioDDL.SelectedValue;

        if (valor != null & valor != "")
        {
            if (TipoEstudioPrestacionesExaComboBox.SelectedValue.Length > 0 & TipoEstudioPrestacionesExaComboBox.SelectedValue!="0")
            {
                LabelBusquedaErrorPrestaciones.Visible = false;
                this.TipoEstudioDDL.ClearSelection();
                TipoEstudioDDL.CargarEstudios(int.Parse(ClienteIdHF.Value), int.Parse(ProveedorExComplementarioDDL.SelectedValue.ToString()), TipoEstudioPrestacionesExaComboBox.SelectedValue);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Exec", "execute();", true);
            }
        }
        else
        {
            LabelBusquedaErrorPrestaciones.Visible = true;
        }
    }

    protected void EmergenciaRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        
        if (e.Item is GridDataItem)
        {
           
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
               
                GridDataItem da = e.Item as GridDataItem;
                string PermisoInsertarEmergenciaPago = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoInsertarEmergenciaPago"];
                ImageButton ImgBtn = (ImageButton)item["TemplateRegistroEmergencia"].FindControl("EditEmergencia");
                ValidacionBLL obj = new ValidacionBLL();
                if (obj.VerificarPermisosEmergencia())
                {
                   
                        ImgBtn.Visible = true;
                   
                }
                else
                {
                    ImgBtn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Derivación a especialistas.");
                log.Error("Function DerivacionRadGrid_ItemDataBound on page CasoMedicoDetalle.aspx", ex);
            }
        }
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            Emergencia objItem = (Emergencia)e.Item.DataItem;
            Repeater LaboratoriosMainRepeater = (Repeater)e.Item.FindControl("LaboratoriosMainRepeater");
            LaboratoriosMainRepeater.DataSource = EmergenciaBLL.getEmergenciaByEmergenciaIdf(objItem.EmergenciaId);
            LaboratoriosMainRepeater.DataBind();
        }
    }
    protected void EmergenciaRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            switch (e.CommandName)
            {
                case "Detalles":
                    {
                        string CadenaRecibida = e.CommandArgument.ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "EditEmergencia", "OpenPopupEditEmergencia()", true);
                        char separador = ';'; // separador de datos
                        string[] arregloDeSubCadenas = CadenaRecibida.Split(separador);
                        int detIdEmergencia = int.Parse(arregloDeSubCadenas[0].ToString());
                        detIdEmergenciaHF.Value = detIdEmergencia.ToString();
                        CargarDatosEmergencia(detIdEmergencia);
                        break;
                    }
                case "Completo":
                    {
                        GetFullReportPdf(Convert.ToInt32(e.CommandArgument));
                        break;
                    }
            }
        }
    }

    public void CargarDatosEmergencia(int detIdEmergencia)
    {

        try {
            CasoEmergencia ObjCasoEmergencia = CasoEmergenciaBLL.GetCasoEmergencia(detIdEmergencia);

            TextMontoemergenciaE.Text = ObjCasoEmergencia.detMontoEmergencia.ToString().Replace(".", "").Replace(",", ".");
            TextMontoHonorarioE.Text = ObjCasoEmergencia.detMontoHonorariosMedicos.ToString().Replace(".", "").Replace(",", ".");
            TextMontoFarmaciaE.Text = ObjCasoEmergencia.detMontoFarmacia.ToString().Replace(".", "").Replace(",", ".");
            TextMontoLaboratorioE.Text = ObjCasoEmergencia.detMontoLaboratorios.ToString().Replace(".", "").Replace(",", ".");
            TextMontoEstudiosE.Text = ObjCasoEmergencia.detMontoEstudios.ToString().Replace(".", "").Replace(",", ".");
            TextMontoOtrosE.Text = ObjCasoEmergencia.detMontoOtros.ToString().Replace(".", "").Replace(",", ".");
            TextMontoTotalE.Text = ObjCasoEmergencia.detMontoTotal.ToString("N2").Replace(".", "").Replace(",", ".");

            if (ObjCasoEmergencia.detMontoCoPago > 0)
            {
                TextValorCoPagoE.Text = ObjCasoEmergencia.detMontoCoPago.ToString("N2").Replace(".", "").Replace(",", ".");
                TextMontoCoPagoE.Text = TextValorCoPagoE.Text;

                if (ObjCasoEmergencia.detMontoTotal == 0)
                {
                    TextMontoTotalE.Text = "0";
                    TextMontoBsTotalE.Text = "0";
                    habilitarEmergencia(true);

                }
                else
                {

                    TextMontoCoPagoE.Text = ObjCasoEmergencia.detMontoCoPago.ToString("N2").Replace(".", "").Replace(",", ".");
                    TextMontoBsTotalE.Text = ObjCasoEmergencia.detMontoCoPago.ToString("N2").Replace(".", "").Replace(",", ".");
                    habilitarEmergencia(false);
                }
                LabelTipoCopagoEm.Text = "Co-Pago Referencial";
                LabelTipoCopagoEm.CssClass = "auto-style4";

            }
            else
            {
                if (ObjCasoEmergencia.detPorcentajeCopago > 0)
                {

                    TextValorCoPagoE.Text = ObjCasoEmergencia.detPorcentajeCopago.ToString("N2").Replace(".", "").Replace(",", "."); ;

                    if (ObjCasoEmergencia.detMontoTotal > 0)
                    {
                        TextMontoCoPagoE.Text = ((ObjCasoEmergencia.detMontoTotal * (ObjCasoEmergencia.detPorcentajeCopago / 100))).ToString("N2").Replace(".", "").Replace(",", "."); ;
                        TextMontoBsTotalE.Text = TextMontoCoPagoE.Text;
                        habilitarEmergencia(false);
                    }
                    else {
                        TextMontoBsTotalE.Text = "0";
                        TextMontoCoPagoE.Text = "0";
                        habilitarEmergencia(true);

                    }
                    LabelTipoCopagoEm.Text = "Co-Pago Referencial en %";
                    LabelTipoCopagoEm.CssClass = "auto-style4";
                }
            }
            List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion == "EM")
                {

                    // VerMontoTopeHF.Value = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].MontoTope * 100) / 100));
                    VerMontoTopeHF.Value = ListaPrestaciones[i].MontoTope.ToString().Replace(",", ".");
                    break;

                }

            }
        }catch(Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error de Recuperar la Emergencia.");
            log.Error("Error al actualizar la Internacion ", q);
        }
    }

    private decimal BusquedaDeValorCoPagoEmergencias(string TipodePago)
    {
        decimal CopagoValor = 0;
        string ValorEM = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorEM"];
        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(int.Parse(ClienteIdHF.Value));
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion.Contains(ValorEM))
            {
                if (ListaPrestaciones[i].CoPagoMonto > 0 & TipodePago == "Monto")
                {

                    CopagoValor = ListaPrestaciones[i].CoPagoMonto;
                    break;
                }
                else
                {
                    if (ListaPrestaciones[i].CoPagoPorcentaje > 0 & TipodePago == "Porcentaje")
                    {
                        CopagoValor = (ListaPrestaciones[i].CoPagoPorcentaje);
                        break;
                    }
                }
            }
        }
        return CopagoValor;
    }

    private void habilitarEmergencia(bool Estado)
    {
        if (Estado)
        {
            TextMontoemergenciaE.Enabled = true;
            TextMontoHonorarioE.Enabled = true;
            TextMontoFarmaciaE.Enabled = true;
            TextMontoLaboratorioE.Enabled = true;
            TextMontoEstudiosE.Enabled = true;
            TextMontoOtrosE.Enabled = true;
            BtnUpdateEmergencia.Visible = true;
        }
        else
        {
            TextMontoemergenciaE.Enabled = false;
            TextMontoHonorarioE.Enabled = false;
            TextMontoFarmaciaE.Enabled = false;
            TextMontoLaboratorioE.Enabled = false;
            TextValorCoPagoE.Enabled = false;
            TextMontoEstudiosE.Enabled = false;
            TextMontoCoPagoE.Enabled = false;
            TextMontoOtrosE.Enabled = false;
            TextMontoTotalE.Enabled = false;
            TextMontoBsTotalE.Enabled = false;
            BtnUpdateEmergencia.Visible = false;
        }

    }


    protected void FileUpload_FilesLoaded(object sender, FilesLoadedArgs e)
    {
        foreach (FileLoaded doc in e.FilesLoaded)
        {
            try
            {
                CasoEmergenciaBLL.InsertFile(int.Parse(detIdEmergenciaHF.Value), doc.ID);
                EmergenciaRadGrid.DataBind();
                return;
            }
            catch (Exception ex)
            {

            }

        }
    }

    protected void SaveUpdateEmergenciaLB_Click(object sender, EventArgs e)
    {
        try
        {

            if (decimal.Parse(TextMontoBsEmergenciaEHF.Value) > 0)
            {
                CasoEmergencia objCasoEmergencia = new CasoEmergencia();
                //objCasoEmergencia.detid = int.Parse(detIdEmergenciaHF.Value);
                //objCasoEmergencia.detMontoEmergencia = decimal.Parse(TextMontoEmergenciaEHF.Value.Replace(".", ","));
                //objCasoEmergencia.detMontoEstudios = decimal.Parse(TextMontoEstudiosEHF.Value.Replace(".", ","));
                //objCasoEmergencia.detMontoFarmacia = decimal.Parse(TextMontoFarmaciaEHF.Value.Replace(".", ","));
                //objCasoEmergencia.detMontoHonorariosMedicos = decimal.Parse(TextMontoHonorarioMedicoEHF.Value.Replace(".", ","));
                //objCasoEmergencia.detMontoLaboratorios = decimal.Parse(TextMontoLaboratorioEHF.Value.Replace(".", ","));
                //objCasoEmergencia.detMontoOtros = decimal.Parse(TextMontoOtrosEHF.Value.Replace(".", ","));
                //objCasoEmergencia.detMontoTotal = decimal.Parse(TextMontoTotalEmergenciaHF.Value.Replace(".", ","));

                CasoEmergenciaBLL.UpdateCasoEmergencia(int.Parse(detIdEmergenciaHF.Value)
                    ,decimal.Parse(TextMontoEmergenciaEHF.Value.Replace(".", ","))
                    ,decimal.Parse(TextMontoEstudiosEHF.Value.Replace(".", ","))
                    ,decimal.Parse(TextMontoFarmaciaEHF.Value.Replace(".", ","))
                    ,decimal.Parse(TextMontoHonorarioMedicoEHF.Value.Replace(".", ","))
                    ,decimal.Parse(TextMontoLaboratorioEHF.Value.Replace(".", ","))
                    ,decimal.Parse(TextMontoOtrosEHF.Value.Replace(".", ","))
                    ,decimal.Parse(TextMontoTotalEmergenciaHF.Value.Replace(".", ","))
                    );

                SystemMessages.DisplaySystemMessage("Emergencia del paciente actualizada.");

            }


        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar la Emergencia.");
            log.Error("Error al actualizar la Emergencia ", q);
        }
    }
    //sirve para permitir que rellene todos los campos de en La validacion de Medico General
    private void HabilitarValidacion()
    {
        try
        {
            int Casoid = Convert.ToInt32(CasoIdHF.Value);
            Caso ObjCaso = CasoBLL.GetCasoByCasoId(Casoid);
            RequiredFieldValidator PresionArterialREF = (RequiredFieldValidator)CasoFV.FindControl("PresionArterialREF");
            RequiredFieldValidator TemperaturaRFV = (RequiredFieldValidator)CasoFV.FindControl("TemperaturaRFV");
            RequiredFieldValidator PulsoRFV = (RequiredFieldValidator)CasoFV.FindControl("PulsoRFV");
            RequiredFieldValidator FrecuenciaCRFV = (RequiredFieldValidator)CasoFV.FindControl("FrecuenciaCRFV");
            RequiredFieldValidator PesoRFV = (RequiredFieldValidator)CasoFV.FindControl("PesoRFV");
            RequiredFieldValidator BiometriaHematicaRFV = (RequiredFieldValidator)CasoFV.FindControl("BiometriaHematicaRFV");
            RequiredFieldValidator ObservacionesRFV = (RequiredFieldValidator)CasoFV.FindControl("ObservacionesRFV");
            RequiredFieldValidator RequiredFieldValidator5 = (RequiredFieldValidator)CasoFV.FindControl("RequiredFieldValidator5");
            switch (ObjCaso.MotivoConsultaId)
            {
                case "EMERG":
                    BiometriaHematicaRFV.Enabled = false;
                    RequiredFieldValidator5.Enabled = false;
                    break;
                case "ODONTO":
                    BiometriaHematicaRFV.Enabled = false;
                    ObservacionesRFV.Enabled = false;
                    break;
                case "ENFER":

                    break;
                case "DESG":

                    break;
                case "RECASO":

                    break;
                default:
                    break;
            }
        }
        catch (Exception)
        {

        }




    }
    //Verificar si Es Odontologo que cargue directamente en el combo box
    private void Verificarespecialidad()
    {
        try
        {

            int userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
            Medico medico = null;
            medico = MedicoBLL.getMedicoByUserId(userId);
            if (medico != null)
            {
                string EspecialidadOdontologo = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadOdontologo"];

                if (medico.EspecialidadId == Artexacta.App.Especialidad.BLL.EspecialidadBLL.GetEspecialidadxNombre(EspecialidadOdontologo).EspecialidadId)
                {
                    CargarCiudadUsuario(UserBLL.GetUserById(userId).CiudadId);
                    Proveedor ObjProveedor = ProveedorMedicoBLL.GetProveedorPrecioByMedicoId(medico.MedicoId);
                    List<Proveedor> modifiedList = new List<Proveedor>();

                    modifiedList.Add(ObjProveedor);


                    RadComboBoxProveedorOdontologia.DataSource = modifiedList;
                    RadComboBoxProveedorOdontologia.DataValueField = "ProveedorId";
                    RadComboBoxProveedorOdontologia.DataTextField = "Nombres";
                    RadComboBoxProveedorOdontologia.DataBind();
                    RadComboBoxProveedorOdontologia.SelectedValue = ObjProveedor.ProveedorId.ToString();
                    RadComboBoxProveedorOdontologia.Enabled = false;
                }
                else
                {
                    RadComboBoxCiudadOdontologia.Enabled = true;
                    RadComboBoxProveedorOdontologia.Enabled = true;
                }
            }
        }
        catch (Exception)
        {

        }
    }
    //cargar el nombre de la ciudad en el ComboBox de prestaciones Odontologicas
    private void CargarCiudadUsuario(string CiudadId)
    {
        List<Ciudad> modifiedList = new List<Ciudad>();
        Ciudad ObjCiudad = CiudadBLL.GetCiudadDetails(CiudadId);
        RadComboBoxCiudadOdontologia.Text = ObjCiudad.Nombre;
        RadComboBoxCiudadOdontologia.Enabled = false;
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Emergencia objItem = (Emergencia)e.Item.DataItem;

            Repeater DocumentosRepeater = (Repeater)e.Item.FindControl("DocumentosRepeater");
            DocumentosRepeater.DataSource = objItem.LaboratorioFiles;
            DocumentosRepeater.DataBind();
        }
    }
    protected void DocumentosRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        FileInfo f = new FileInfo(e.CommandName);
        Response.Clear();
        string mimeType = FileUtilities.GetFileMIMEType(f.Extension);
        if (mimeType != null)
        {
            Response.ContentType = mimeType;
        }
        string path = e.CommandArgument.ToString();
        Response.AddHeader("Content-Disposition", "attachment;Filename=\"" + HttpUtility.UrlPathEncode(f.Name) + "\"");
        Response.AddHeader("Content-Length", new FileInfo(path).Length.ToString());
        Response.WriteFile(path);
        Response.Flush();
        Response.End();
    }
    private void GetFullReportPdf_EVO(int EmergenciaId)
    {
        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        Emergencia objCita = EmergenciaBLL.getEmergenciaByEmergenciaId(EmergenciaId);
        byte[] pdfBytesExamenMedico = null;
        //if (objCita.NecesitaExamen)
        //{
        //    pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(EmergenciaId, urlServer);
        //}

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);

        // Caso Medico (la ficha de SISA)
        int casoId = 0;
        Emergencia objEnlace = null;
        try
        {
            objEnlace = EmergenciaBLL.getEmergenciaByEmergenciaId(EmergenciaId);
            casoId = objEnlace.CasoId;
            if (casoId > 0)
            {
                byte[] pdfHistorialSISA = null;
               // pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(objEnlace, urlServer);
                if (pdfHistorialSISA != null)
                    pdfBytesLabos.Add(pdfHistorialSISA);
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener el caso de sisa", q);
        }
        List<string> pdfBytesImg = new List<string>();

        List<Emergencia> listaLabos = EmergenciaBLL.getEmergenciaByEmergenciaIdf(EmergenciaId);
        foreach (Emergencia progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                if (objDoc.Extension.ToLower() == ".pdf")
                {
                    pdfBytesLabos.Add(objDoc.Bytes);
                }
                else
                {
                    pdfBytesImg.Add(objDoc.FileStoragePath);
                }
            }
        }

        byte[] fullOutput = null;

        if (pdfBytesLabos.Count == 1)
        {
            fullOutput = pdfBytesLabos[0];
        }
        else
        {
            EvoPdf.PdfMerge.PdfDocumentOptions pdfDocumentOptions = new EvoPdf.PdfMerge.PdfDocumentOptions();
            pdfDocumentOptions.PdfCompressionLevel = EvoPdf.PdfMerge.PDFCompressionLevel.Normal;
            pdfDocumentOptions.PdfPageSize = EvoPdf.PdfMerge.PdfPageSize.Letter;
            pdfDocumentOptions.PdfPageOrientation = EvoPdf.PdfMerge.PDFPageOrientation.Portrait;
            EvoPdf.PdfMerge.PDFMerge objMerger = new EvoPdf.PdfMerge.PDFMerge(pdfDocumentOptions);

            foreach (byte[] pdfBytes in pdfBytesLabos)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                objMerger.AppendPDFStream(inputStream, 0);
                inputStream.Close();
            }
            foreach (string imgPath in pdfBytesImg)
            {
                objMerger.AppendImageFile(imgPath);
            }

            fullOutput = objMerger.RenderMergedPDFDocument();
        }

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + EmergenciaId + ".pdf; size={0}", fullOutput.Length.ToString()));
        response.BinaryWrite(fullOutput);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();
    }

    private void GetFullReportPdf(int citaId)
    {
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
        {
            GetFullReportPdf_EVO(citaId);
            return;
        }
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
        {
           GetFullReportPdf_HIQ(citaId);
            return;
        }
    }

    private void GetFullReportPdf_HIQ(int EmergenciaId)
    {
        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        Emergencia objCita = EmergenciaBLL.getEmergenciaByEmergenciaId(EmergenciaId);
        byte[] pdfBytesExamenMedico = null;
        //if (objCita.NecesitaExamen)
        //{
        //    pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
        //}

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);

        // Caso Medico (la ficha de SISA)
        int casoId = 0;
        EnlaceDesgravamenSISA objEnlace = null;
        try
        {
          //  objEnlace = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(citaId, ref casoId);
            if (objEnlace != null && casoId > 0)
            {
                byte[] pdfHistorialSISA = null;
              //  pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(objEnlace, urlServer);
                if (pdfHistorialSISA != null)
                    pdfBytesLabos.Add(pdfHistorialSISA);
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener el caso de sisa", q);
        }

        // PDF delos adjuntos
        List<string> pdfBytesImg = new List<string>();
        List<Emergencia> listaLabos = EmergenciaBLL.getEmergenciaByEmergenciaIdf(EmergenciaId);
        foreach (Emergencia progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                if (objDoc.Extension.ToLower() == ".pdf")
                {
                    pdfBytesLabos.Add(objDoc.Bytes);
                }
                else
                {
                    pdfBytesImg.Add(objDoc.FileStoragePath);
                }
            }
        }

        // MERGE de todo

        byte[] fullOutput = null;

        if (pdfBytesLabos.Count == 1 && pdfBytesImg.Count <= 0)
        {
            fullOutput = pdfBytesLabos[0];

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + EmergenciaId + ".pdf; size={0}", fullOutput.Length.ToString()));
            response.BinaryWrite(fullOutput);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();
            return;
        }
        else
        {
            HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
            completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

            foreach (byte[] pdfBytes in pdfBytesLabos)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
                completeDoc.AddDocument(documentToAddMerge);
            }
            foreach (string imgPath in pdfBytesImg)
            {
                HiQPdf.PdfDocument documentToAddMerge = new HiQPdf.PdfDocument();
                // set a demo serial number
                documentToAddMerge.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

                // create a page in document
                HiQPdf.PdfPage page1 = documentToAddMerge.AddPage(HiQPdf.PdfPageSize.Letter, new HiQPdf.PdfDocumentMargins(25, 25, 10, 10), HiQPdf.PdfPageOrientation.Portrait);
                // set a background color for the page
                HiQPdf.PdfRectangle backgroundRectangle = new HiQPdf.PdfRectangle(page1.DrawableRectangle);
                backgroundRectangle.BackColor = System.Drawing.Color.White;
                page1.Layout(backgroundRectangle);

                // create the true type fonts that can be used in document text
                System.Drawing.Font sysFont = new System.Drawing.Font("Arial", 10, System.Drawing.GraphicsUnit.Point);
                HiQPdf.PdfFont pdfFont = documentToAddMerge.CreateFont(sysFont);
                HiQPdf.PdfFont pdfFontEmbed = documentToAddMerge.CreateFont(sysFont, true);

                float crtYPos = 20;
                float crtXPos = 5;

                // layout an opaque JPG image
                HiQPdf.PdfImage opaquePdfImage = new HiQPdf.PdfImage(crtXPos, crtYPos, imgPath);
                HiQPdf.PdfLayoutInfo imageLayoutInfo = page1.Layout(opaquePdfImage);

                completeDoc.AddDocument(documentToAddMerge);
                //documentToAddMerge.Close();
            }

            fullOutput = completeDoc.WriteToMemory();

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + EmergenciaId + ".pdf; size={0}", fullOutput.Length.ToString()));
            response.BinaryWrite(fullOutput);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();

            completeDoc.Close();

        }
    }

    protected void EmergenciaRadGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        try
        {
            EmergenciaRadGrid.DataSourceID = EmergenciaRadGrid.DataSourceID;
            InternacionToPrintRadGrid.DataBind();
            string codigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            ExportGridToPdf(InternacionToPrintRadGrid, codigoCaso + "_Internacion", "SPECIALIST", codigoCaso);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al exportar la Internación.");
            log.Error("Function InternacionRadGrid_ExportToPdfButton_Click on page CasoMedicoDetalle.aspx", q);
        }
    }

    protected void btnvideollamada_Click(object sender, ImageClickEventArgs e)
    {
        //int valor = 0;
        int val= 0;
        
        string username = User.Identity.Name;
        if (username == "admin" || username=="alexis")
        {
            string mensaje = String.Empty;
            var result = VideoLllamadaTeleBLL.GenerarTokenVideoLLamada(Convert.ToInt32(CasoIdHF.Value), username, ref val, ref mensaje);
            //val = 1;
            if (val == 0)
            {
                SystemMessages.DisplaySystemMessage(mensaje);
            }
            else
            {
                Session["CASOTELE"] = CasoIdHF.Value;
                Session["_appid"] = result.appid;
                Session["_appCertificate"] = result.appCertificate;
                Session["_token"] = result.Token;
                Session["_roomname"] = result.RoomName;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OpenWindow", "window.open('/CasoMedico/AWebSDK/CallCitas.aspx');", true);

            }
        }else 
        {
            SystemMessages.DisplaySystemMessage("No tienes permisos.");
        }


    }
    
}