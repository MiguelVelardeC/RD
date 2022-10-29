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
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using Artexacta.App.Utilities.Bitacora;

public partial class Desgravamen_ProveedorDesgravamen : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static Bitacora theBitacora = new Bitacora();

    public int ProveedorMedicoId
    {
        get
        {
            
            int proveedorId = 0;
            try
            {
                proveedorId = Convert.ToInt32(ProveedorHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'ProveedorHiddenField.Value' to int value", ex);
            }
            return proveedorId;
        }
        set
        {
            if (value < 0)
                ProveedorHiddenField.Value = "0";
            else
                ProveedorHiddenField.Value = value.ToString();
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!IsPostBack)
        {
            LoadDataSourceLists();
            CargarClientes();
            ProcessSessionParameters();

        }
    }

    private void LoadDataSourceLists()
    {
        List<Ciudad> list = CiudadBLL.getCiudadList();

        CiudadComboBox.DataSource = list;
        CiudadComboBox.DataValueField = "CiudadId";
        CiudadComboBox.DataTextField = "Nombre";
        CiudadComboBox.DataBind();
    }

    private void ProcessSessionParameters()
    {
        if (Session["ProveedorMedicoId"] != null && !string.IsNullOrEmpty(Session["ProveedorMedicoId"].ToString()))
        {
            try
            {
                int paId = Convert.ToInt32(Session["ProveedorMedicoId"].ToString());
                ProveedorHiddenField.Value = paId.ToString();
                LoadProveedorData();
                SaveAndContinueButton.Visible = true;
                Session["ProveedorMedicoId"] = null;
                return;
            }
            catch (Exception ex)
            {
                log.Error("Cannot get Proveedor Desgravamen Id from SESSION", ex);
            }
        }
        else
        {
            SaveAndContinueButton.Visible = false;
        }
        
    }

    private int GuardarProveedorDesgravamen()
    {
            CiudadComboBox.Enabled = true;
            string userId = "";
            string nombreProveedor = NombreTextBox.Text;
            userId = this.UserRadComboBox.SelectedValue;
            int intUserId = Convert.ToInt32(userId);
            string username = UserIDHF.Value;
            string ciudadId = CiudadComboBox.SelectedValue;
            int Duracion = Convert.ToInt32(DuracionCitaTextBox.Value);
            bool isPrincipal = false;
            ProveedorDesgravamen proveedor = new ProveedorDesgravamen()
            {
                ProveedorNombre = nombreProveedor,
                CiudadId = ciudadId,
                UserId = intUserId,
                DuracionCita = Duracion,
                Principal = isPrincipal
            };
            //

            int ProveedorId = Convert.ToInt32(ProveedorHiddenField.Value);
            if (ProveedorId > 0)
            {
                proveedor.ProveedorMedicoId = ProveedorId;
                ProveedorMedicoBLL.UpdateProveedorMedico(proveedor);

                theBitacora.RecordTrace(Bitacora.TraceType.DESGActualizarProveedor, User.Identity.Name, "Desgravamen", ProveedorId.ToString(), "Se actualizo el provedor: "+proveedor.ProveedorNombre);
                return ProveedorId;
            }
            else
            {

                theBitacora.RecordTrace(Bitacora.TraceType.DESGInsertarProveedor, User.Identity.Name, "Desgravamen", ProveedorId.ToString(), "Se inserto el provedor: " + proveedor.ProveedorNombre);
                int id = ProveedorMedicoBLL.InsertProveedorMedico(proveedor);
                ProveedorHiddenField.Value = id.ToString();
                return id;
            }
            //ProveedorMedicoBLL.InsertProveedorMedico();
    }

    protected void SaveAndContinueButton_Click(object sender, EventArgs e)
    {        
        try
        {
            Session["ProveedorMedicoId"] = ProveedorHiddenField.Value;
            Response.Redirect("~/Desgravamen/ProveedorEstudiosDetalle.aspx");
        }
        catch (Exception ex)
        {
            //SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Proveedor Desgravamen ");
            log.Error("Cannot save data for MedicoDesgravamen Nombre: "+NombreTextBox.Text, ex);
            return;
        }    
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            GuardarProveedorDesgravamen();
            SystemMessages.DisplaySystemMessage("Los datos de Proveedor Desgravamen fueron guardados exitosamente");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Proveedor Desgravamen");
            log.Error("Cannot save data for MedicoDesgravamen", ex);
            return;
        }

        SaveAndContinueButton.Visible = true;
        //Response.Redirect("~/Desgravamen/ProveedorDesgravamenLista.aspx");
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
        //List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();

    }
    protected void ClientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //AppendTableData();
    }

    private void LoadProveedorData()
    {
        ProveedorDesgravamen obj = ProveedorMedicoBLL.GetProveedorMedicoId(ProveedorMedicoId);
        NombreTextBox.Text = obj.ProveedorNombre;

        UserIDHF.Value = Convert.ToString(obj.UserId);
        User u = UserBLL.GetUserById(obj.UserId);
        string userFullName = u.FullName;
        UserNameHF.Value = userFullName;

        RadComboBoxItem itemCombo = new RadComboBoxItem(UserNameHF.Value, UserIDHF.Value);
        UserRadComboBox.Items.Add(itemCombo);
        UserRadComboBox.SelectedValue = itemCombo.Value;

        CiudadComboBox.SelectedValue = obj.CiudadId;
        DuracionCitaTextBox.Value = obj.DuracionCita;
        CiudadComboBox.Enabled = false;
    }
}