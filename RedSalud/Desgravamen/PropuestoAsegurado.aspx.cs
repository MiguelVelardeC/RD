using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System.Web.Services;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedCliente;

public partial class Desgravamen_PropuestoAsegurado : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

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
        set
        {
            if (value < 0)
                PropuestoAseguradoIdHiddenField.Value = "0";
            else
                PropuestoAseguradoIdHiddenField.Value = value.ToString();
        }
    }

    public int FotoId
    {
        get
        {
            int fotoId = 0;
            try
            {
                fotoId = Convert.ToInt32(FotoIDHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'FotoIDHiddenField.Value' to int value", ex);
            }
            return fotoId;
        }
        set
        {
            if (value < 0)
                FotoIDHiddenField.Value = "0";
            else
                FotoIDHiddenField.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FotoPAFileUpload.FilesLoaded += FotoPAFileUpload_FilesLoaded;
        FotoPAFileUpload.MaxFileInputCount = 1;

        if (!IsPostBack)
        {
            ProcessSessionParameters();
            
            int propuestoAseguradoId = this.PropuestoAseguradoId;
            if (propuestoAseguradoId > 0)
            {
                try
                {
                    PropuestoAsegurado obj = PropuestoAseguradoBLL.GetPropuestoAseguradoId(propuestoAseguradoId);
                    CargarPA(obj);

                    SaveAndContinueButton.Visible = false;
                    CrearCitaButton.Visible = true;
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get data for Propuesto Asegurado with id: " + propuestoAseguradoId, ex);
                }
            }

        }
    }

    void FotoPAFileUpload_FilesLoaded(object sender, Artexacta.App.Documents.FileUpload.FilesLoadedArgs e)
    {
        if (e.FilesLoaded != null && e.FilesLoaded.Count > 0)
        {
            FotoId = e.FilesLoaded[0].ID;
            FotoPAUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";

            if (PropuestoAseguradoId > 0)
            {
                PropuestoAseguradoBLL.UpdateFotoId(PropuestoAseguradoId, FotoId);
            }
        }
    }

    private void CargarPA(PropuestoAsegurado obj)
    {
        NombreTextBox.Text = obj.NombreCompleto;
        CedulaTextBox.Text = obj.CarnetIdentidad;
        TelefonoCelularTextBox.Text = obj.TelefonoCelular;
        FechaNacimientoDatePicker.SelectedDate = obj.FechaNacimiento;
        if (!string.IsNullOrWhiteSpace(obj.Genero))
            GeneroRadioButton.Items.FindByValue(obj.Genero).Selected = true;

        if (obj.PropuestoAseguradoId > 0)
        {
            PropuestoAseguradoId = obj.PropuestoAseguradoId;
        }
        /*if (obj.FotoId > 0)
        {*/
        FotoId = obj.FotoId;
        FotoPAUrl.ImageUrl = obj.FotoUrl;

        ClienteIdHiddenField.Value = obj.ClienteId.ToString();

        LoadClienteTitle("Propuesto Asegurado", obj.ClienteId);

        //}
    }

    private void ProcessSessionParameters()
    {
        if (Session["PropuestoAseguradoId"] != null && !string.IsNullOrEmpty(Session["PropuestoAseguradoId"].ToString()))
        {
            try
            {
                int paId = Convert.ToInt32(Session["PropuestoAseguradoId"].ToString());
                //int clienteId = Convert.ToInt32(Session["ClienteId"].ToString());
                PropuestoAseguradoIdHiddenField.Value = paId.ToString();
                //ClienteIdHiddenField.Value = clienteId.ToString();
                //LoadClienteTitle("Propuesto Asegurado", clienteId);
                Session["PropuestoAseguradoId"] = null;
                //Session["ClienteId"] = null;
                return;
            }
            catch (Exception ex)
            {
                log.Error("Cannot get Usuario from SESSION", ex);
            }
        }

        int userId = 0;
        try
        {
            userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
            if (userId == 0)
            {
                userId = -1;
                throw new Exception("No se pudo encontrar el id del usuario");
            }
            /*if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_EXAMEN_MEDICO") ||
                Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_VER_TODOSCASOS"))
                userId = 0;*/
        }
        catch (Exception q)
        {
            if (userId < 0)
            {
                SystemMessages.DisplaySystemErrorMessage("No se pudo obtener el id del usuario " + User.Identity.Name);
            }
            // si es un usuario valido se queda con el id de usuario encontrado
        }

        if (userId < 0)
        {
            Response.Redirect("~/MainPage.aspx");
            return;
        }

        if (Session["ClienteId"] != null && !string.IsNullOrEmpty(Session["ClienteId"].ToString()))
        {
            try
            {
                int clienteId = Convert.ToInt32(Session["ClienteId"].ToString());
                ClienteIdHiddenField.Value = clienteId.ToString();
                LoadClienteTitle("Propuesto Asegurado", clienteId);
                Session["ClienteId"] = null;
                return;
            }
            catch (Exception ex)
            {
                log.Error("Cannot get Cliente from SESSION " + Session["ClienteId"].ToString(), ex);
            }
        }
        else
        {
            int clienteId = 0;
            try
            {
                clienteId = ClienteUsuarioBLL.GetClienteByUsuarioId(userId);
                if (clienteId != 0)
                {
                    ClienteIdHiddenField.Value = clienteId.ToString();
                    LoadClienteTitle("Propuesto Asegurado", clienteId);
                }
                else
                {
                    ClienteIdHiddenField.Value = "0";
                }
                
            }
            catch (Exception)
            {
                SystemMessages.DisplaySystemWarningMessage("El Usuario [" + User.Identity.Name + "] no tiene un cliente asignado");
                //Response.Redirect("~/Desgravamen/PropuestoAseguradoLista.aspx");
                // si es un usuario valido se queda con el id de usuario encontrado
            }
        }
        
    }

    private void LoadClienteTitle(string defaultTitle, int clienteId)
    {
        RedCliente cl = RedClienteBLL.GetRedClienteByClienteId(clienteId);
        if (cl != null)
        {
            LblPropuestoAseguradoTitle.InnerText = defaultTitle + " para " + cl.NombreJuridico;
        }
    }


    private int GuardarPropuestoAsegurado()
    {
        string propuesto = PropuestoAseguradoIdHiddenField.Value;
        string cliente = ClienteIdHiddenField.Value;
        int propuestoAseguradoId = 0;
        int clienteId = 0;
        try
        {
            propuestoAseguradoId = Convert.ToInt32(propuesto);
            clienteId = Convert.ToInt32(cliente);
        }
        catch (Exception ex)
        {
            log.Error("Cannot convert 'PropuestoAseguradoIdHiddenField.Value' or 'ClienteIdHiddenField.Value' " + 
                "to integer value PropuestoAsegurado: " + propuesto + " cliente: " + cliente, ex);
            throw ex;
        }

        try
        {
            PropuestoAsegurado obj = new PropuestoAsegurado()
            {
                PropuestoAseguradoId = propuestoAseguradoId,
                NombreCompleto = NombreTextBox.Text,
                CarnetIdentidad = CedulaTextBox.Text,
                FechaNacimiento = FechaNacimientoDatePicker.SelectedDate.Value,
                TelefonoCelular = TelefonoCelularTextBox.Text,
                FotoId = FotoId,
                Genero = GeneroRadioButton.SelectedValue,
                ClienteId = clienteId
            };

            if (propuestoAseguradoId == 0)
            {
                propuestoAseguradoId = PropuestoAseguradoBLL.InsertPropuestoAsegurado(obj);
            }
            else
            {
                PropuestoAseguradoBLL.UpdatePropuestoAsegurado(obj);
            }

            if (obj.FotoId > 0)
                PropuestoAseguradoBLL.UpdateFotoId(propuestoAseguradoId, obj.FotoId);
        }
        catch (Exception ex)
        {
            throw ex;            
        }

        return propuestoAseguradoId;
    }

    protected void SaveAndContinueButton_Click(object sender, EventArgs e)
    {
        try
        {
            int propuestoAseguradoId = GuardarPropuestoAsegurado();
            Session["PropuestoAseguradoId"] = propuestoAseguradoId;
            
            SystemMessages.DisplaySystemMessage("Se guardaron los datos del Propuesto Asegurado");
        }
        catch (Exception ex)
        {
            string cliente = ClienteIdHiddenField.Value;
            if(cliente == "0" || string.IsNullOrEmpty(cliente))
            {
                SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Propuesto Asegurado, se perdio definicion del cliente. Favor iniciar flujo nuevamente");
                Response.Redirect("~/Desgravamen/PropuestoAseguradoLista.aspx");
            }else{
                SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Propuesto Asegurado");
            }
            log.Error("Cannot save data for PropuestoAsegurado clienteId: "+cliente, ex);
            return;
        }
        Session["CitaDesgravamenId"] = null;

        Response.Redirect("~/Desgravamen/PropuestoAseguradoCita.aspx");
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            GuardarPropuestoAsegurado();
            SystemMessages.DisplaySystemMessage("Se guardaron los datos del Propuesto Asegurado");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Propuesto Asegurado");
            log.Error("Cannot save data for PropuestoAsegurado", ex);
            return;
        }
        Response.Redirect("~/Desgravamen/PropuestoAseguradoLista.aspx");
    }

    protected void CrearCitaButton_Click(object sender, EventArgs e)
    {
        Session["PropuestoAseguradoId"] = PropuestoAseguradoId;
        Session["CitaDesgravamenId"] = null;
        Response.Redirect("~/Desgravamen/PropuestoAseguradoCita.aspx");
    }


    protected void CedulaTextBox_TextChanged(object sender, EventArgs e)
    {
        string ci = CedulaTextBox.Text;
        string clienteId = ClienteIdHiddenField.Value;
        PropuestoAsegurado pa = null;
        try
        {
            int intClienteId = Convert.ToInt32(clienteId);
            pa = PropuestoAseguradoBLL.GetPropuestoAseguradoByCI(ci, intClienteId);
            if (pa != null)
            {
                CargarPA(pa);
            }
            else
            {
                CleanPA();
            }
        }
        catch (Exception q)
        {
            log.Error("There was an error executing GetPropuestoAseguradoByCI CI: "+ci+" cliente: "+clienteId, q);
            throw q;
        }
    }

    private void CleanPA()
    {
        NombreTextBox.Text = "";
        //CedulaTextBox.Text = obj.CarnetIdentidad;
        TelefonoCelularTextBox.Text = "";
        FechaNacimientoDatePicker.SelectedDate = null;
        GeneroRadioButton.ClearSelection();
        PropuestoAseguradoId = 0;       
        FotoId = 0;
        FotoPAUrl.ImageUrl = "";
    }


    protected void EliminarFotoButton_Click(object sender, EventArgs e)
    {
        if (PropuestoAseguradoId <= 0)
            return;

        if (FotoId <= 0)
            return;

        FotoId = 0;
        FotoPAUrl.ImageUrl = "~/Images/Neutral/paciente.jpg";

        PropuestoAseguradoBLL.UpdateFotoId(PropuestoAseguradoId, 0);
    }

    [WebMethod(EnableSession = true)]
    public static PropuestoAsegurado CargarPAAjax(string ci, string clienteId)
    {

        PropuestoAsegurado pa = new PropuestoAsegurado();
        try
        {
            int intClienteId = Convert.ToInt32(clienteId);
            PropuestoAsegurado p = PropuestoAseguradoBLL.GetPropuestoAseguradoByCI(ci, intClienteId);
            if (p != null)
            {
                pa = p;
                pa.FotoUrl_Ajax = "../ImageResize.aspx?ID="+pa.FotoId+"&W=200&H=200";
                pa.FechaNacimientoLong = pa.FechaNacimiento.ToString("yyyy-MM-dd");
            }
        }
        catch (Exception q)
        {
            log.Error("There was an error executing GetPropuestoAseguradoByCI CI: " + ci + " cliente: " + clienteId, q);
            //throw q;
        }


        return pa;
    }
}