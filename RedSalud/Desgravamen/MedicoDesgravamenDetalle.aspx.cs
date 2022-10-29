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
using Telerik.Web.UI;
using Artexacta.App.Security.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.User;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedCliente;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Desgravamen_MedicoDesgravamen : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int MedicoDesgravamenId
    {
        get
        {
            
            int medicoDesgravamenId = 0;
            try
            {
                medicoDesgravamenId = Convert.ToInt32(MedicoDesgravamenIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'MedicoDesgravamenIdHiddenField.Value' to int value", ex);
            }
            return medicoDesgravamenId;
        }
        set
        {
            if (value < 0)
                MedicoDesgravamenIdHiddenField.Value = "0";
            else
                MedicoDesgravamenIdHiddenField.Value = value.ToString();
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            LoadDataSourceLists();
            CargarClientes();

            int medicoDesgravamenId = 0;

            try
            {
                if (!string.IsNullOrEmpty(this.MedicoDesgravamenIdHiddenField.Value))                
                    medicoDesgravamenId = Convert.ToInt32(this.MedicoDesgravamenIdHiddenField.Value);                
            }
            catch (Exception)
            {
                log.Error("Cannot get data for Medico Desgravamen with id: " + medicoDesgravamenId);
                throw;
            }

            if (medicoDesgravamenId > 0)
            {
                try
                {
                    MedicoDesgravamen obj = MedicoDesgravamenBLL.GetMedicoDesgravamenById(medicoDesgravamenId);

                    //PropuestoAsegurado obj = PropuestoAseguradoBLL.GetPropuestoAseguradoId(medicoDesgravamenId);
                    CargarPA(obj);

                    //SaveAndContinueButton.Visible = true;
                    //CrearCitaButton.Visible = true;
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get data for Medico Desgravamen with id: " + medicoDesgravamenId, ex);
                    throw;
                }
            }

        }
    }

    private void CargarPA(MedicoDesgravamen obj)
    {
        
        NombreTextBox.Text = obj.Nombre;
        ProveedorComboBox.SelectedValue = Convert.ToString(obj.ProveedorMedicoId);
        
        /*int UserIndex = UserRadComboBox.FindItemIndexByValue(Convert.ToString(obj.ProveedorMedicoId));
        UserRadComboBox.SelectedIndex = UserIndex;
        UserRadComboBox.DataBind();*/
        
        UserIDHF.Value = Convert.ToString(obj.UserId);
        User u = UserBLL.GetUserById(obj.UserId);
        string userFullName = u.FullName;
        UserNameHF.Value = userFullName;

        RadComboBoxItem itemCombo = new RadComboBoxItem(UserNameHF.Value, UserIDHF.Value);
        UserRadComboBox.Items.Add(itemCombo);
        UserRadComboBox.SelectedValue = itemCombo.Value;

        DireccionTextBox.Text = obj.Direccion;
        if(obj.MedicoDesgravamenId > 0)
        {
            MedicoDesgravamenId = obj.MedicoDesgravamenId;
        }
        ProveedorComboBox.DataBind();
        AppendTableData();
    }

    private void LoadDataSourceLists()
    {
        ProveedorComboBox.DataSource = ProveedorMedicoBLL.GetProveedorMedico();
        ProveedorComboBox.DataTextField = "ProveedorNombre";
        ProveedorComboBox.DataValueField = "ProveedorMedicoId";
        ProveedorComboBox.DataBind();
    }

    private void ProcessSessionParameters()
    {
        if (Session["MedicoDesgravamenId"] != null && !string.IsNullOrEmpty(Session["MedicoDesgravamenId"].ToString()))
        {
            try
            {
                int paId = Convert.ToInt32(Session["MedicoDesgravamenId"].ToString());
                MedicoDesgravamenIdHiddenField.Value = paId.ToString();
                Session["MedicoDesgravamenId"] = null;
                return;
            }
            catch (Exception ex)
            {
                log.Error("Cannot get Medico Desgravamen Id from SESSION", ex);
            }
        }
        
    }

    private int GuardarMedicoDesgravamen()
    {
        int medicoDesgravamenId = 0;
        string userId = "";
        string proveedor = "";
        try
        {
            medicoDesgravamenId = Convert.ToInt32(MedicoDesgravamenIdHiddenField.Value);
        }
        catch (Exception ex)
        {
            log.Error("Cannot convert 'MedicoDesgravamenIdHiddenField.Value' to integer value");
            throw ex;
        }

        try
        {
            userId = this.UserRadComboBox.SelectedValue;
            int intUserId = Convert.ToInt32(userId);
            proveedor = ProveedorComboBox.SelectedValue;
            int intProveedor = Convert.ToInt32(proveedor);
            MedicoDesgravamen obj = new MedicoDesgravamen()
            {
                MedicoDesgravamenId = medicoDesgravamenId,
                Nombre = NombreTextBox.Text,
                ProveedorMedicoId = intProveedor,
                Direccion = DireccionTextBox.Text,
                UserId = intUserId
            };

            if (medicoDesgravamenId == 0)
            {
                medicoDesgravamenId = MedicoDesgravamenBLL.InsertMedicoDesgravamen(obj);//PropuestoAseguradoBLL.InsertPropuestoAsegurado(obj);
            }
            else
            {
                MedicoDesgravamenBLL.UpdateMedicoDesgravamen(obj);
            }
        }
        catch (Exception ex)
        {
            log.Error("Error on data parsing medicoDesgravamenId: " + medicoDesgravamenId + " Nombre: " + 
                NombreTextBox.Text + " UserId: " + userId + "Proveedor: " + proveedor + 
                "Direccion: " + DireccionTextBox.Text, ex);
            throw ex;            
        }

        return medicoDesgravamenId;
    }

    protected void SaveAndContinueButton_Click(object sender, EventArgs e)
    {        
        try
        {
            int medicoDesgravamenId = GuardarMedicoDesgravamen();
            //Session["MedicoDesgravamenId"] = medicoDesgravamenId;
            MedicoDesgravamenIdHiddenField.Value = medicoDesgravamenId.ToString();
            SystemMessages.DisplaySystemMessage("Los datos de Medico Desgravamen fueron guardados exitosamente");
            //Response.Redirect("~/Desgravamen/MedicoDesgravamenDetalle.aspx");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Medico Desgravamen ");
            log.Error("Cannot save data for MedicoDesgravamen Nombre: "+NombreTextBox.Text, ex);
            return;
        }
        //Session["CitaDesgravamenId"] = null;        
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            GuardarMedicoDesgravamen();
            SystemMessages.DisplaySystemMessage("Los datos de Medico Desgravamen fueron guardados exitosamente");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Medico Desgravamen");
            log.Error("Cannot save data for MedicoDesgravamen", ex);
            return;
        }
        Response.Redirect("~/Desgravamen/MedicoDesgravamenLista.aspx");
    }

    protected void CrearCitaButton_Click(object sender, EventArgs e)
    {
        /*Session["PropuestoAseguradoId"] = PropuestoAseguradoId;
        Session["CitaDesgravamenId"] = null;
        Response.Redirect("~/Desgravamen/PropuestoAseguradoCita.aspx");*/
    }


    protected void CedulaTextBox_TextChanged(object sender, EventArgs e)
    {
        /*string ci = CedulaTextBox.Text;

        PropuestoAsegurado pa = null;
        try
        {
            pa = PropuestoAseguradoBLL.GetPropuestoAseguradoByCI(ci);
            if (pa != null)
                CargarPA(pa);
        }
        catch 
        {
            ;
        }*/
    }

    protected void RadComboBox_DataBinding(object sender, EventArgs e)
    {
        RadComboBox rcb = (sender as RadComboBox);
        rcb.Items.Add(new RadComboBoxItem(rcb.Text, rcb.SelectedValue));
    }

    protected void InsertRadComboBox_DataBinding(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(UserIDHF.Value))
        {
            RadComboBox rcb = (sender as RadComboBox);
            rcb.Items.Add(new RadComboBoxItem(UserNameHF.Value, UserIDHF.Value));
            rcb.SelectedValue = UserIDHF.Value;

            try
            {
                User user = UserBLL.GetUserById(Convert.ToInt32(UserIDHF.Value));
                Username.InnerText = user.Username;
            }
            catch (Exception q)
            {
                log.Error("User Id: " + UserIDHF.Value + " is invalid", q);
                throw;
            } 
            
        }
    }

    private void CargarClientes()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();

        ClientesComboBox.DataSource = list;
        ClientesComboBox.DataTextField = "NombreJuridico";
        ClientesComboBox.DataValueField = "ClienteId";
        ClientesComboBox.DataBind();
    }
    protected void ClientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppendTableData();
    }

    private void AppendTableData()
    {
        string clientId = ClientesComboBox.SelectedValue;
        string medicoId = MedicoDesgravamenIdHiddenField.Value;
        try
        {
            int intClientId = Convert.ToInt32(clientId);
            int intMedicoId = Convert.ToInt32(medicoId);
            List<MedicoHorarios> list = MedicoHorariosBLL.GetMedicoHorarios(intMedicoId, intClientId);
            if (intMedicoId > 0)
            {
                StringBuilder html = new StringBuilder();
                int index = 0;
                foreach (MedicoHorarios mh in list)
                {

                    html.Append("<tr>");

                    html.Append("<td>");
                    html.Append("<input type='image' src='../Images/Neutral/delete.png' id='delRow" + index + "' class='inputDelete' />");
                    html.Append("</td>");

                    html.Append("<td>");
                    html.Append("<input type='hidden' id='medicoHorarioId" + index + "' value='" + mh.MedicoHorariosId + "' />");
                    html.Append("<input type='hidden' id='medicoDesgravamenId" + index + "' value='" + mh.MedicoDesgravamenId + "' />");
                    html.Append("<input type='hidden' id='clienteId' value='" + index + "' />");
                    html.Append("<span id=clienteNombre" + index + ">" + mh.ClienteNombre + "</span>");
                    html.Append("</td>");

                    html.Append("<td>");
                    html.Append("<span id=horaInicio" + index + ">" + new DateTime(mh.HoraInicio.Ticks).ToString("HH:mm") + "</span>");
                    html.Append("</td>");

                    html.Append("<td>");
                    html.Append("<span id=horaFin" + index + ">" + new DateTime(mh.HoraFin.Ticks).ToString("HH:mm") + "</span");
                    html.Append("</td>");

                    html.Append("</tr>");

                    index++;
                }

                DataPlaceHolder.Controls.Add(new Literal() { Text = html.ToString() });
            }            
        }
        catch (Exception q)
        {
            log.Error("An error ocurred when acquiring the list of MedicoHorarios medicoDesgravamenId: " + MedicoDesgravamenIdHiddenField.Value + 
                " ClienteId: "+ClientesComboBox.SelectedValue, q);
            throw q;
        }
        
        
    }

    [WebMethod(EnableSession = true)]
    public static void EliminarHorarios(string horariosId)
    {
        try
        {
            int intHorariosId = Convert.ToInt32(horariosId);
            MedicoHorariosBLL.DeleteMedicoHorarios(intHorariosId);
        }
        catch (Exception q)
        {
            log.Error("Could not delete MedicoHorarios row with Id: " + horariosId, q);
            throw q;
        }
    }

    [WebMethod(EnableSession = true)]
    public static List<MedicoHorariosJSON> loadTable(string clienteId, string medicoId){

        try
        {
            int intClienteId = Convert.ToInt32(clienteId);
            int intMedicoId = Convert.ToInt32(medicoId);

            if (intMedicoId > 0)
            {
                List<MedicoHorarios> list = MedicoHorariosBLL.GetMedicoHorarios(intMedicoId, intClienteId);
                List<MedicoHorariosJSON> listJson = new List<MedicoHorariosJSON>();
                if (list != null)
                {
                    foreach (MedicoHorarios obj in list)
                    {
                        listJson.Add(Desgravamen_MedicoDesgravamen.MedicoHorariosToJSON(obj));
                    }
                }
                return listJson;                
            }

            return new List<MedicoHorariosJSON>();
        }
        catch (Exception q)
        {
            log.Error("The paramaters do not have the correct datatype clienteId: " + clienteId + " medicoId: " + medicoId, q);

            throw q;
        }


        //return null;
    }

    [WebMethod(EnableSession = true)]
    public static MedicoHorariosJSON insertHorario(string medicoId, string clienteId, string horaInicio, string horaFin)
    {
        try
        {
            int intMedicoId = Convert.ToInt32(medicoId);
            int intClienteId = Convert.ToInt32(clienteId);
            TimeSpan TimeHoraInicio = TimeSpan.Parse(horaInicio);
            TimeSpan TimehoraFin = TimeSpan.Parse(horaFin);
            //TimeSpan.Parse("", IFormatProvider

            if(intMedicoId > 0){

                int horarioId = MedicoHorariosBLL.InsertMedicoHorarios(new MedicoHorarios()
                {
                    MedicoDesgravamenId = intMedicoId,
                    ClienteId = intClienteId,
                    HoraInicio = TimeHoraInicio,
                    HoraFin = TimehoraFin
                });

                return (horarioId > 0) ? Desgravamen_MedicoDesgravamen.MedicoHorariosToJSON(MedicoHorariosBLL.GetMedicoHorariosById(horarioId)) : null;                            
            }

            return null;
            /*else{
                List<MedicoHorarios> list = new List<MedicoHorarios>();
                MedicoHorarios objMedicoHorarios = new MedicoHorarios()
                {
                    MedicoDesgravamenId = intMedicoId,
                    ClienteId = intClienteId,
                    HoraInicio = TimeHoraInicio,
                    HoraFin = TimehoraFin
                };

                list.Add(objMedicoHorarios);
                HttpContext.Current.Session["ListHorarios"] = null;
                HttpContext.Current.Session["ListHorarios"] = list;
                return Desgravamen_MedicoDesgravamen.MedicoHorariosToJSON(objMedicoHorarios);
            }*/
        }
        catch (Exception q)
        {
            log.Error("Error while trying to insert MedicoHorarios medicoId: " + medicoId + 
                " clienteId: " + clienteId + " horaInicio: " + horaInicio + " horaFin: " + horaFin, q);
            throw q;
        }

    }

    public static MedicoHorariosJSON MedicoHorariosToJSON(MedicoHorarios mh){
        MedicoHorariosJSON obj = new MedicoHorariosJSON();
            
        if (mh != null)
        {
            obj.MedicoHorariosId = mh.MedicoHorariosId;
            obj.MedicoDesgravamenId = mh.MedicoDesgravamenId;
            obj.ClienteId = mh.ClienteId;
            obj.ClienteNombre = mh.ClienteNombre;

            string strHoraInicio = new DateTime(mh.HoraInicio.Ticks).ToString("HH:mm");
            obj.HoraInicio = strHoraInicio;
            string strHoraFin = new DateTime(mh.HoraFin.Ticks).ToString("HH:mm");
            obj.HoraFin = strHoraFin;
        }
        /*return (mh != null) ? new MedicoHorariosJSON()
            {
                MedicoHorariosId = mh.MedicoHorariosId,
                MedicoDesgravamenId = mh.MedicoDesgravamenId,
                ClienteId = mh.ClienteId,
                HoraInicio = mh.HoraInicio.ToString("HH:mm"),
                HoraFin = mh.HoraFin.ToString("HH:mm")
            } : null;        */

        return obj;
    }

    [WebMethod(EnableSession = true)]
    public static void IsConsultaInsert(int intCitaDesgravamen)
    {
        
    }
}