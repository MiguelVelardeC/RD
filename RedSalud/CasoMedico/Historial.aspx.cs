using Artexacta.App.Paciente;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Artexacta.App.LoginSecurity;
using Artexacta.App.EnfermedadCronica.BLL;
using Telerik.Web.UI;

public partial class CasoMedico_Historial : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private string Especialidad;
    public bool IsOdontologo
    {
        get
        {
            if (Especialidad == null)
                try
                {
                    int UserId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    Artexacta.App.Especialidad.Especialidad esp = Artexacta.App.Especialidad.BLL.EspecialidadBLL.GetEspecialidadByUserId(UserId); ;
                    Especialidad = (esp != null) ? esp.Nombre : "";
                }
                catch { }
            if (Especialidad.StartsWith("ODONTOLOGÍA"))
            {
                return true;
            }
            return false;
        }
    }

    public int CitaDesgravamenId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(CitaDesgravamenIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                CitaDesgravamenIdHiddenField.Value = "0";
            else
                CitaDesgravamenIdHiddenField.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FileManager.OnListFileChange += new UserControls_FileManager.OnListFileChangeDelegate(FileManager_FileSave);
            ProcessSessionParameters();
            HideIFViewModeP();
            GetPacienteInfo();
        }
    }

    private void HideIFViewModeP()
    {
        if (ViewModeHF.Value == "P")
        {
            returnHL.Visible = false;
            Print.Visible = false;
            RefreshHyperLink.Visible = false;
        }
    }

    protected void ProcessSessionParameters()
    {
        //PacienteId
        int PacienteId = 0;
        if (Session["PacienteId"] != null && !string.IsNullOrEmpty(Session["PacienteId"].ToString()))
        {
            try
            {
                PacienteId = Convert.ToInt32(Session["PacienteId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session PacienteId:" + Session["PacienteId"]);
            }
        }
        else
        {
            if (Request.QueryString["PacienteId"] != null && !string.IsNullOrEmpty(Request.QueryString["PacienteId"].ToString()))
            {
                try
                {
                    PacienteId = Convert.ToInt32(Request.QueryString["PacienteId"]);
                }
                catch
                {
                    log.Error("no se pudo realizar la conversion de la query PacienteId:" + Request.QueryString["PacienteId"]);
                }
            }
        }
        PacienteIdHF.Value = PacienteId.ToString();
        Session["PacienteId"] = null;

        int CasoId = 0;
        if (Session["CasoId"] != null && !string.IsNullOrEmpty(Session["CasoId"].ToString()))
        {
            try
            {
                CasoId = Convert.ToInt32(Session["CasoId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);
            }
        }
        else
        {
            if (Request.QueryString["CasoId"] != null && !string.IsNullOrEmpty(Request.QueryString["CasoId"].ToString()))
            {
                try
                {
                    CasoId = Convert.ToInt32(Request.QueryString["CasoId"]);
                }
                catch
                {
                    log.Error("no se pudo realizar la conversion de la query CasoId:" + Request.QueryString["CasoId"]);
                }
            }
        }
        CasoIdHF.Value = CasoId.ToString();
        Session["CasoId"] = null;
        ReturlLB.Visible = CasoId > 0;

        string viewMode = "N";
        if (Session["VM"] != null && !string.IsNullOrEmpty(Session["VM"].ToString()))
        {
            viewMode = Session["VM"].ToString();
        }
        else
        {
            if (Request.QueryString["VM"] != null && !string.IsNullOrEmpty(Request.QueryString["VM"].ToString()))
            {
                viewMode = Request.QueryString["VM"].ToString();
            }
        }
        ViewModeHF.Value = viewMode;
        Session["VM"] = null;

        if (Session["CITADESGRAVAMENID"] != null && !string.IsNullOrEmpty(Session["CITADESGRAVAMENID"].ToString()))
        {
            CitaDesgravamenIdHiddenField.Value = Session["CITADESGRAVAMENID"].ToString();
        }
        Session["CITADESGRAVAMENID"] = null;

        if (PacienteId <= 0)
            Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx");
    }


    protected void GetPacienteInfo()
    {
        try
        {
            int PacienteId = Convert.ToInt32(PacienteIdHF.Value);
            Paciente ObjPaciente = PacienteBLL.GetPacienteByPacienteId(PacienteId);
            if (ObjPaciente != null)
            {
                this.NombreLabel.Text = ObjPaciente.Nombre;
                this.GeneroLabel.Text = ObjPaciente.GeneroForDisplay;
                this.TelefonoLabel.Text = ObjPaciente.Telefono;
                this.Antecedentes.JSonData = ObjPaciente.Antecedentes;
                //this.AntecedentesAlergicos.JSonData = ObjPaciente.AntecedentesAlergicos;
                this.AntecedentesGinecoLabel.Visible = !ObjPaciente.Genero;
                this.AntecedentesGinecoNombreLabel.Visible = !ObjPaciente.Genero;
                this.AntecedentesGinecoLabel.JSonData = ObjPaciente.AntecedentesGinecoobstetricos;
                if (ObjPaciente.FotoId > 0)
                {
                    FotoPaciente.FotoId = ObjPaciente.FotoId;
                    FotoPaciente.PacienteId = PacienteId;
                }
                
                IsCriticoHF.Value = (PacienteBLL.isCritico(PacienteId)) ? "1" : "";
                if (IsCriticoHF.Value == "1")
                {
                    EnfermedadesCronicasRepeater.Visible = true;
                    EnfermedadesCronicasRepeater.DataSource = EnfermedadCronicaBLL.GetEnfermedadCronicaByPacienteId(ObjPaciente.PacienteId);
                    EnfermedadesCronicasRepeater.DataBind();
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
                else
                {
                    cssCritic.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del paciente.");
            log.Error("Function GetPacienteInfo on page Historial.aspx", ex);
        }
        
    }
    protected void HistorialODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el Historial del paciente.");
            log.Error("Function HistorialODS_Selected on page Historial.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ReturlLB_Click(object sender, EventArgs e)
    {
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);

            Session["CasoId"] = CasoId.ToString();
            if (ViewModeHF.Value == "P")
            {
                Session["CITADESGRAVAMENID"] = CitaDesgravamenId;
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener algunos datos para mostrar el detalle del caso medico del paciente.");
            log.Error("Function ReturlLB_Click on page Histortial.aspx", ex);
        }
        Response.Redirect("CasoMedicoDetalle.aspx");
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
    protected void HistorialRepeater_ItemDataBound ( object sender, RepeaterItemEventArgs e )
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HiddenField exFisicoRegionalydeSistemaTxt = (HiddenField)e.Item.FindControl("exFisicoRegionalydeSistemaTxt");
            string clientID = exFisicoRegionalydeSistemaTxt.ClientID;
            string script = "<!--Angular Controller-->\n" +
            "<script type=\"text/javascript\">\n" +
            "function Controller_" + e.Item.ItemIndex + "($scope) {\n" +
            "    $scope.master =\n" +
            "    [\n" +
            "        {\n" +
            "            'name': 'Cabeza',\n" +
            "            'group': [{ 'name': 'Cráneo', 'value': '' }, { 'name': 'Cara', 'value': '' }]\n" +
            "        },\n" +
            "        {\n" +
            "            'name': 'Cuello',\n" +
            "            'value': ''\n" +
            "        },\n" +
            "        {\n" +
            "            'name': 'Tórax',\n" +
            "            'group': [{ 'name': 'Aparato Respiratorio', 'value': '' }]\n" +
            "        },\n" +
            "        {\n" +
            "            'name': 'Aparato Cardiocirculatorio',\n" +
            "            'value': ''\n" +
            "        },\n" +
            "        {\n" +
            "            'name': 'Abdomen',\n" +
            "            'group': [{ 'name': 'Aparato Digestivo', 'value': '' }, { 'name': 'Aparato Genitourinario', 'value': '' }]\n" +
            "        },\n" +
            "        {\n" +
            "            'name': 'Extremidades',\n" +
            "            'group': [{ 'name': 'Sistema Osteomuscular', 'value': '' }]\n" +
            "        }\n" +
            "    ];\n" +
            //"    $scope.SaveChanges = function () {\n" +
            //"        var stringMaster = JSON.stringify($scope.master);\n" +
            //"        $('#" + clientID + "').val(stringMaster);\n" +
            //"    };\n" +
            "    $scope.LoadJson = function () {\n" +
            "        try {\n" +
            "            var jsonMaster = $.parseJSON($('#" + clientID + "').val());\n" +
            "            if (jsonMaster != null && jsonMaster != undefined && jsonMaster != '') {" +
            "               $scope.master = jsonMaster;\n" +
            "            }" +
            "        } catch (q) { }\n" +
            "    };\n" +
            "}\n" +
            "</script>";
            (e.Item.FindControl("AngularScript") as Literal).Text = script;
            (e.Item.FindControl("DivController") as HtmlGenericControl).Attributes.Add("ng-controller", "Controller_" + e.Item.ItemIndex);

            if (ViewModeHF.Value == "P")
            {
                Panel pnlGroupHistorial = (Panel)e.Item.FindControl("pnlGroupHistorial");
                if (pnlGroupHistorial != null)
                    pnlGroupHistorial.CssClass = "critic ExpandCollapse Expand";
            }
        }
    }
    protected void FileManager_Command ( object sender, CommandEventArgs e )
    {
        FileManager.OpenFileManager(e.CommandName, Convert.ToInt32(e.CommandArgument), false);
    }
    public void FileManager_FileSave(string ObjectName, string type)
    {
        HistorialRepeater.DataBind();
    }
    protected void RefreshHyperLink_Click ( object sender, EventArgs e )
    {
        Session["PacienteId"] = PacienteIdHF.Value;
        Session["CasoId"] = CasoIdHF.Value;
        Response.Redirect("~/CasoMedico/Historial.aspx");
    }
    protected void MedicamentoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Medicamentos.");
            e.ExceptionHandled = true;
        }
    }
}