using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Utilities.Email;
using Artexacta.App.User.BLL;

public partial class Desgravamen_PropuestoAseguradoCita : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public static Bitacora theBitacora = new Bitacora();

    private int primeraVezEstudios = -1;

    public int PropuestoAseguradoId
    {
        get
        {
            int propuestoAseguradoId = 0;
            try
            {
                propuestoAseguradoId = Convert.ToInt32(PropuestoAseguradoIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'PropuestoAseguradoIdHiddenField.Value' to int value", ex);
            }
            return propuestoAseguradoId;
        }
    }

    public int CitaDesgravamenId
    {
        get
        {
            int citaDesgravamenId = 0;
            try
            {
                citaDesgravamenId = Convert.ToInt32(CitaDesgravamenIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'CitaDesgravamenIdHiddenField.Value' to int value", ex);
            }
            return citaDesgravamenId;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        ProcessSessionParameters();
        int citaDesgravamenId = this.CitaDesgravamenId;
        List<Estudio> estudiosSeleccionados = null;

        if (citaDesgravamenId > 0)
        {
            try
            {
                CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaDesgravamenId);
                PropuestoAseguradoIdHiddenField.Value = objCita.PropuestoAseguradoId.ToString();
                ReferenciaTextBox.Text = objCita.Referencia;
                NecesitaEstudiosChk.Checked = objCita.NecesitaLaboratorio;
                NecesitaExamen.Checked = objCita.NecesitaExamen;
                FacturacionPrivadaChk.Checked = !objCita.CobroFinanciera;
                CiudadSeleccionada.Value = objCita.CiudadId;
                FinanciaeraSeleccionada.Value = objCita.FinancieraId.ToString();

                estudiosSeleccionados = EstudioBLL.GetEstudiosByCitaDesgravamenIdForEjecutivo(citaDesgravamenId);
                JavaScriptSerializer js = new JavaScriptSerializer();
                EstudiosSeleccionados.Value = js.Serialize(estudiosSeleccionados);
            }
            catch (Exception ex)
            {
                log.Error("Error to get data for CitaDesgravamen with id " + citaDesgravamenId, ex);
            }

            ProgramacionCita obj = null;
            List<PropuestoAseguradoEstudio> listaPALabos = null;
            try
            {
                obj = PropuestoAseguradoBLL.GetProgramacionCita(CitaDesgravamenId);
                listaPALabos = CitaDesgravamenBLL.GetListaPropuestoAseguradoEstudioByCitaId(CitaDesgravamenId);
            }
            catch (Exception ex)
            {
                log.Error("Cannot get data for ProgramacionCita", ex);
            }

            bool citaAprobadaOPasada = false;
            bool laboAProbado = false;
            bool laboRealizado = false;

            if (estudiosSeleccionados != null)
            {
                foreach (Estudio objEstudio in estudiosSeleccionados)
                {
                    if (objEstudio.Realizado)
                    {
                        laboRealizado = true;
                        break;
                    }
                }
            }

            if (obj != null && (DateTime.Now > obj.FechaHoraCita || obj.Aprobado))
            {
                citaAprobadaOPasada = true;
            }
            if (listaPALabos != null && listaPALabos.Count > 0)
            {
                laboAProbado = listaPALabos[0].Aprobado;
            }
            if (citaAprobadaOPasada || laboAProbado || laboRealizado)
                SaveButton.Visible = false;
        }
        try
        {
            PropuestoAsegurado obj = PropuestoAseguradoBLL.GetPropuestoAseguradoId(PropuestoAseguradoId);
            NombreTextBox.Text = obj.NombreCompleto;
            FechaNacimientoTextBox.Text = obj.FechaNacimiento.ToShortDateString();
            CedulaTextBox.Text = obj.CarnetIdentidad;
            ClienteIdHiddenField.Value = obj.ClienteId.ToString();
            LoadClienteTitle("Cita del Propuesto Asegurado", obj.ClienteId);
            List<TipoProductoDesgravamen> list = TipoProductoDesgravamenBLL.GetTipoProductosByCliente(obj.ClienteId);

            TipoProductoCombBox.DataSource = list;
            TipoProductoCombBox.DataValueField = "Codigo";
            TipoProductoCombBox.DataTextField = "Descripcion";
            TipoProductoCombBox.DataBind();
        }
        catch (Exception ex)
        {
            log.Error("Cannot get Propuesto Asegurado with id " + PropuestoAseguradoIdHiddenField.Value, ex);
        }        
    }

    private void ProcessSessionParameters()
    {
        if (Session["PropuestoAseguradoId"] != null && !string.IsNullOrEmpty(Session["PropuestoAseguradoId"].ToString()))
        {
            try
            {
                int id = Convert.ToInt32(Session["PropuestoAseguradoId"].ToString());
                PropuestoAseguradoIdHiddenField.Value = id.ToString();
                
            }
            catch (Exception ex)
            {
                log.Error("Cannot get turnoId from SESSION", ex);
            }

        }

        if (Session["CitaDesgravamenId"] != null && !string.IsNullOrEmpty(Session["CitaDesgravamenId"].ToString()))
        {
            try
            {
                int citaDesgravamenId = Convert.ToInt32(Session["CitaDesgravamenId"].ToString());
                CitaDesgravamenIdHiddenField.Value = citaDesgravamenId.ToString();
                
            }
            catch (Exception ex)
            {
                log.Error("Cannot get turnoId from SESSION", ex);
            }

        }
        Session["PropuestoAseguradoId"] = null;
        Session["CitaDesgravamenId"] = null;

        if(CitaDesgravamenId == 0 && PropuestoAseguradoId == 0)
            Response.Redirect("~/Desgravamen/PropuestoAseguradoLista.aspx");
    }

    protected void EntidadFinancieraDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get List of Financieras", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar la lista de Financieras");
        }
    }


    protected void CiudadDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get List of Ciudades", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar la lista de Ciudades");
        }
    }

    protected void EntidadFinanciearComboBox_DataBound(object sender, EventArgs e)
    {
        EntidadFinancieraComboBox.Items.Insert(0, new ListItem("Seleccione una Entidad Financiera...", ""));
        if (string.IsNullOrEmpty(FinanciaeraSeleccionada.Value))
            return;
        ListItem item = EntidadFinancieraComboBox.Items.FindByValue(FinanciaeraSeleccionada.Value);
        if (item != null)
            item.Selected = true;

    }

    protected void CiudadComboBox_DataBound(object sender, EventArgs e)
    {
        CiudadComboBox.Items.Insert(0, new ListItem("Seleccione una Ciudad...", ""));
        if (string.IsNullOrEmpty(CiudadSeleccionada.Value))
            return;
        ListItem item = CiudadComboBox.Items.FindByValue(CiudadSeleccionada.Value);
        if (item != null)
            item.Selected = true;
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string tipoProd = TipoProductoCombBox.SelectedValue;
        string ciudad = CiudadComboBox.SelectedValue;
        string clienteId = ClienteIdHiddenField.Value;

        int citaDesgravamenId = CitaDesgravamenId;
        int financieraId = 0;
        int userId = 0;

        try
        {
            financieraId = Convert.ToInt32(EntidadFinancieraComboBox.SelectedValue);
        }
        catch { financieraId = 0;}

        try
        {
            userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
        }
        catch 
        {
            SystemMessages.DisplaySystemWarningMessage("No se puede obtener el id del usuario actual, recargar la página o volver a entrar al sistema");
            return;
        }

        if (tipoProd != "DESGRAVAMEN")
            financieraId = 0;

        try
        {
            int intClienteId = Convert.ToInt32(clienteId);
            string referencia = ReferenciaTextBox.Text;
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<int> estudioIds = js.Deserialize<List<int>>(EstudiosIdsHiddenField.Value);

            if (citaDesgravamenId == 0)
            {
                citaDesgravamenId = CitaDesgravamenBLL.InsertCitaDesgravamen(PropuestoAseguradoId, tipoProd, financieraId, ciudad,
                    NecesitaExamen.Checked,
                    NecesitaEstudiosChk.Checked, !FacturacionPrivadaChk.Checked, ReferenciaTextBox.Text, userId, estudioIds, intClienteId);
                theBitacora.RecordTrace(Bitacora.TraceType.DESGInsertarCita, User.Identity.Name, "Desgravamen", citaDesgravamenId.ToString(), "Creación de cita desgravamen con id " + citaDesgravamenId);

                SendEmailToUser(citaDesgravamenId, userId, PropuestoAseguradoId);
            }
            else
                CitaDesgravamenBLL.UpdateCitaDesgravamen(citaDesgravamenId, PropuestoAseguradoId, tipoProd, financieraId, ciudad,
                    NecesitaExamen.Checked,
                    NecesitaEstudiosChk.Checked, !FacturacionPrivadaChk.Checked, ReferenciaTextBox.Text, estudioIds, intClienteId);
            SystemMessages.DisplaySystemMessage("La cita fue guardada correctamente"); 
        }
        catch (Exception ex)
        {
            log.Error("An error occurred when trying to create a record for 'Cita Propuesto Asegurado' with clientId: "+clienteId, ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al crear la Cita para el Propuesto Asegurado");
            return;
        }

        Session["CitaDesgravamenId"] = citaDesgravamenId;
        Session["CiudadId"] = ciudad;
        Session["ClienteId"] = ClienteIdHiddenField.Value;
        Response.Redirect("~/Desgravamen/SeleccionHoraLibre.aspx");
    }

    public void SendEmailToUser(int citaDesgravamen, int userId, int propuestoAsegurado)
    {

        Artexacta.App.User.User user = UserBLL.GetUserById(userId);
        PropuestoAsegurado propuestoAseguradoObj = PropuestoAseguradoBLL.GetPropuestoAseguradoId(propuestoAsegurado);

        string subjectRAW = System.Configuration.ConfigurationManager.AppSettings["DESGMailSubject"];
        string bodyRAW = System.Configuration.ConfigurationManager.AppSettings["DESGMailBody"];
        

        if (!string.IsNullOrEmpty(subjectRAW) && !string.IsNullOrEmpty(bodyRAW) && user != null && propuestoAseguradoObj != null)
        {
            string subject = subjectRAW.Replace("%PROPUESTOASEGURADO%", propuestoAseguradoObj.NombreCompleto.ToUpper() + "");
            string body = bodyRAW.Replace("%CASO%", citaDesgravamen + "").Replace("%PROPUESTOASEGURADO%", propuestoAseguradoObj.NombreCompleto.ToUpper());

            body = body.Replace("[", "<");
            body = body.Replace("]", ">");

            EmailUtilities.SendEmail(user.Email, subject, body);
        }
    }
    

    protected void GrupoEstudioDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get List of Estudios", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar la lista de Estudios");
        }
    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get List of EstudioGrupos", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar la lista de Grupos de Estudio");
        }
    }
    protected void EstudiosPadresDataSources_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Estudio obj = (Estudio)e.Item.DataItem;
            Literal EstudioGrupoLabel = (Literal)(e.Item.FindControl("EstudioGrupoLabel"));
            EstudioGrupoLabel.Text = "<li><a href=\"#tabs-" + obj.EstudioId + "\">" + obj.NombreEstudio + "</a></li>";
        }
    }
    protected void EstudiosRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Estudio obj = (Estudio)e.Item.DataItem;

            Literal headerItemTemplateEstudios = (Literal)(e.Item.FindControl("headerItemTemplateEstudios"));
            Literal footerItemTemplateEstudios = (Literal)(e.Item.FindControl("footerItemTemplateEstudios"));

            if (primeraVezEstudios < 0)
            {
                headerItemTemplateEstudios.Text = "<div id=\"tabs-" + obj.ParentEstudioId + "\"><div class=\"estudiosComplementarios\">";
                primeraVezEstudios = obj.ParentEstudioId;
                return;
            }

            if (primeraVezEstudios != obj.ParentEstudioId)
            {
                headerItemTemplateEstudios.Text = "</div></div><div id=\"tabs-" + obj.ParentEstudioId + "\"><div class=\"estudiosComplementarios\">";
                primeraVezEstudios = obj.ParentEstudioId;
            }
        }
    }
    protected void EstudiosRepeater_DataBinding(object sender, EventArgs e)
    {
        primeraVezEstudios = -1;
    }


    private void LoadClienteTitle(string defaultTitle, int clienteId)
    {
        RedCliente cl = RedClienteBLL.GetRedClienteByClienteId(clienteId);
        if (cl != null)
        {
            LblPropuestoAseguradoTitle.InnerText = defaultTitle + " para " + cl.NombreJuridico;
        }
    }
}