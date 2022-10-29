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
using Artexacta.App.Caso.BLL;

public partial class CasoMedico_HistorialDESG : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FileManager.OnListFileChange += new UserControls_FileManager.OnListFileChangeDelegate(FileManager_FileSave);
            ProcessSessionParameters();
            GetPacienteInfo();
        }
    }

    protected void ProcessSessionParameters()
    {
        //PacienteId
        int PacienteId = 0;
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
        PacienteIdHF.Value = PacienteId.ToString();
        int CasoId = 0;
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
        CasoIdHF.Value = CasoId.ToString();

        int ClienteId = 0;
        if (Request.QueryString["ClienteId"] != null && !string.IsNullOrEmpty(Request.QueryString["ClienteId"].ToString()))
        {
            try
            {
                ClienteId = Convert.ToInt32(Request.QueryString["ClienteId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la query ClienteId:" + Request.QueryString["ClienteId"]);
            }
        }
        ClienteIdDESG.Value = ClienteId.ToString();


        if (PacienteId <= 0 || ClienteId <= 0)
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
                this.LITPacienteNombre.Text = ObjPaciente.Nombre;
                Page.Title = "Historia_Clinica_[" + ObjPaciente.Nombre.Replace(" ", "_") + "]";
                this.GeneroLabel.Text = ObjPaciente.GeneroForDisplay;
                this.FechaNacLabel.Text = ObjPaciente.FechaNacimientoShort;
                this.EdadLabel.Text = ObjPaciente.Edad;
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
                
                if (PacienteBLL.isCritico(PacienteId))
                {
                    EnfermedadesCronicasRepeater.Visible = true;
                    EnfermedadesCronicasRepeater.DataSource = EnfermedadCronicaBLL.GetEnfermedadCronicaByPacienteId(ObjPaciente.PacienteId);
                    EnfermedadesCronicasRepeater.DataBind();
                    
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
    protected void ImprimirButton_Click(object sender, EventArgs e)
    {

        string uri = Request.UrlReferrer.AbsoluteUri;
        try
        {
            byte[] pdfHistorialSISA = null;

            log.Info("The Referrer is: " + uri);

            pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(Request.UrlReferrer.AbsoluteUri);

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=HistoriaClinica" + PacienteIdHF.Value + ".pdf; size={0}", pdfHistorialSISA.Length.ToString()));
            response.BinaryWrite(pdfHistorialSISA);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();
        }
        catch (Exception)
        {
            log.Error("Referrer is: " + uri);
            throw;
        }
        return;
    }
}