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
using System.Text.RegularExpressions;

public partial class Desgravamen_ProveedorEstudios : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static Bitacora theBitacora = new Bitacora();
    private static string IS_UPDATE = "1";
    private static string IS_NOT_UPDATE = "0";
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

        SearchPA.Config = new ProveedorEstudiosSearchConfig();
        SearchPA.OnSearch += SearchPA_OnSearch;
        
        if (!IsPostBack)
        {
            CargarClientes();
            ProcessSessionParameters();
            //loadEstudioProveedorWindowForm();
            SearchPA.Query = buildQuery();
        }
    }
    void SearchPA_OnSearch()
    {
        ProveedorEstudiosDesgravamenGridView.DataBind();
    }

    private void ProcessSessionParameters()
    {
        if (Session["ProveedorMedicoId"] != null && !string.IsNullOrEmpty(Session["ProveedorMedicoId"].ToString()))
        {
            try
            {
                int paId = Convert.ToInt32(Session["ProveedorMedicoId"].ToString());
                ProveedorHiddenField.Value = paId.ToString();
                //LoadProveedorData();
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
            if (string.IsNullOrEmpty(ProveedorHiddenField.Value) || ProveedorHiddenField.Value == "0")
            {
                Response.Redirect("~/Desgravamen/ProveedorDesgravamenLista.aspx", false);
                SystemMessages.DisplaySystemErrorMessage("El codigo del proveedor no es valido, se debe seleccionar un proveedor");
            }
        }
        
    }

    private int GuardarProveedorDesgravamen()
    {
        return 0;
    }

    protected void SaveAndContinueButton_Click(object sender, EventArgs e)
    {        
        try
        {
            //int medicoDesgravamenId = GuardarMedicoDesgravamen();
            int Proveedor = GuardarProveedorDesgravamen();
            //ProveedorHiddenField.Value = medicoDesgravamenId.ToString();
            SystemMessages.DisplaySystemMessage("Los datos del Proveedor Desgravamen fueron guardados exitosamente");
            //Response.Redirect("~/Desgravamen/MedicoDesgravamenDetalle.aspx");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del Proveedor Desgravamen ");
            log.Error("Cannot save data for Proveedor Estudio", ex);
            return;
        }
        //Session["CitaDesgravamenId"] = null;        
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
        Response.Redirect("~/Desgravamen/ProveedorDesgravamenLista.aspx");
    }

    private void CargarClientes()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();
        ClienteComboBox.DataSource = list;
        ClienteComboBox.DataValueField = "ClienteId";
        ClienteComboBox.DataTextField = "NombreJuridico";
        ClienteComboBox.DataBind();

        ClienteComboBox.SelectedIndex = 0;
        ClienteIdHiddenField.Value = ClienteComboBox.SelectedValue;


    }
    protected void ClientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //AppendTableData();
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        try
        {
            int proveedor = Convert.ToInt32(ProveedorHiddenField.Value);

            if (ProveedorHiddenField.Value != "0")
            {
                Session["ProveedorMedicoId"] = proveedor.ToString();
                Response.Redirect("~/Desgravamen/ProveedorDesgravamenDetalle.aspx", false);
            }
            else
            {
                Response.Redirect("~/Desgravamen/ProveedorDesgravamenLista.aspx", false);
            }
        }
        catch (Exception eq)
        {
            log.Error("Error trying to convert HiddenField to int "+ProveedorHiddenField.Value, eq);
            SystemMessages.DisplaySystemErrorMessage("Proveedor no tiene un codigo valido Error: [" + ProveedorHiddenField.Value+"]");
        }
    }

    protected void ProveedorDesgravamenGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "VerPA")
        {
            //Session["ProveedorMedicoId"] = e.CommandArgument;
            //Response.Redirect("~/Desgravamen/ProveedorDesgravamenDetalle.aspx");
            try
            {
                string RowData = e.CommandArgument.ToString();
                string[] data = RowData.Split(':');

                EstudioProveedorHiddenId.Value = data[0];
                ClienteEstudioProveedorHiddenId.Value = data[1];
                IsUpdate.Value = IS_UPDATE;
                loadEstudioProveedorWindowForm();
                loadEstudioProveedorFormData();
                SeleccionEstudioProveedor.VisibleOnPageLoad = true;
            }
            catch (Exception eq)
            {
                log.Error("Error trying to choose a doctor from the listing ", eq);
                SystemMessages.DisplaySystemErrorMessage("No se pudo escoger la informacion");
            }
            return;
        }

        if (e.CommandName == "Delete")
        {

            int ProveedorDesgravamenId = 0;
            int EstudioId = 0;
            int ClienteId = 0;
            try
            {
                string RowData = e.CommandArgument.ToString();
                string[] data = RowData.Split(':');

                if (int.TryParse(data[0], out EstudioId) && 
                    int.TryParse(data[1], out ClienteId) && 
                    int.TryParse(ProveedorHiddenField.Value, out ProveedorDesgravamenId) &&
                    data[0] != "0" && data[1] != "0" && ProveedorHiddenField.Value != "0")
                {

                    int oldProveedorId = ProveedorDesgravamenId;
                    int oldEstudioId = EstudioId;
                    ProveedorEstudiosBLL.DeleteEstudioProveedor(EstudioId, ProveedorDesgravamenId, ClienteId);
                    log.Info("El EstudioId " + oldEstudioId + " del proveedor "+oldProveedorId+" ha sido eliminado por el usuario " + User.Identity.Name);
                    //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarProveedor, User.Identity.Name, "Desgravamen", ProveedorDesgravamenId.ToString(), "El Id es el ProveedorId");
                    SystemMessages.DisplaySystemMessage("El Estudio del Proveedor Desgravamen ha sido eliminado");
                }
                else
                {
                    log.Error("No pudo convertir " + e.CommandArgument.ToString() + " a id");
                    SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                }
            }
            catch(Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("El Proveedor Desgravamen no pudo ser eliminado");
            }
            ProveedorEstudiosDesgravamenGridView.DataBind();
            return;
        }
    }

    protected void ProveedorDesgravamenGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            ProveedorEstudios objItem = (ProveedorEstudios)e.Item.DataItem;

            ImageButton VerPA = (ImageButton)e.Item.FindControl("VerPA");
            ImageButton DelPA = (ImageButton)e.Item.FindControl("DeletePA");

            if (!Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_ADMIN"))
            {
                VerPA.Visible = false;
                DelPA.Visible = false;
            }
            else
            {
                VerPA.Visible = true;
                DelPA.Visible = true;
            }

            //InfoPA.Visible = false;
        }
    }

    protected void ProveedorEstudiosDesgravamenDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Ocurrio un error al tratar de obtener la lista de Medicos Desgravamen", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener la lista de Estudios Proveedor Desgravamen");
            e.ExceptionHandled = true;
        }
        int totalRows = 0;
        try
        {
            totalRows = Convert.ToInt32(e.OutputParameters["totalRows"]);
        }
        catch (Exception ex)
        {
            log.Error("Failed to get OuputParameter 'totalRows'", ex);
        }
        Pager.TotalRows = totalRows;
        if (totalRows == 0)
        {
            Pager.Visible = false;
            return;
        }
        if (Pager.CurrentRow > Pager.TotalRows)
        {
            Pager.CurrentRow = 0;
        }
        Pager.Visible = true;
        Pager.BuildPagination();
    }

    protected void Pager_PageChanged(int row)
    {
        //PropuestoAseguradoGridView.DataBind();
    }

    private string buildQuery()
    {
        string queryBuilder = "";

        if (RequiereCitaComboBox.SelectedIndex > 0)
        {
            if (!string.IsNullOrEmpty(queryBuilder))
            {
                queryBuilder += " AND ";
            }
            queryBuilder += @"" + RequiereCitaComboBox.SelectedValue;
        }

        if (EstadoComboBox.SelectedIndex > 0)
        {
            if (!string.IsNullOrEmpty(queryBuilder))
            {
                queryBuilder += " AND ";
            }
            queryBuilder += @""+EstadoComboBox.SelectedValue;
        }


        return queryBuilder;
    }

    protected void BusquedaButtonLink_Click(object sender, EventArgs e)
    {
        SearchPA.Query = buildQuery();
        ProveedorEstudiosDesgravamenGridView.Rebind();
    }
    protected void BtnAgregarEstudio_Click(object sender, EventArgs e)
    {
        loadEstudioProveedorWindowForm();
        InsertClienteValue.Value = ClientesEstudioComboBox.SelectedValue;
        InsertEstudioValue.Value = EstudioComboBox.SelectedValue;
        HoraInicialComboBoxHiddenField.Value = HoraInicialComboBox.SelectedValue;
        HoraFinalComboBoxHiddenField.Value = HoraInicialComboBox.SelectedValue;

        SeleccionEstudioProveedor.VisibleOnPageLoad = true;
    }

    private void loadEstudioProveedorWindowForm()
    {

        try
        {
            
            List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();

            RedCliente cl = new RedCliente();
            ClientesEstudioComboBox.DataSource = list;
            ClientesEstudioComboBox.DataValueField = "ClienteId";
            ClientesEstudioComboBox.DataTextField = "NombreJuridico";
            ClientesEstudioComboBox.DataBind();

            List<ComboContainer> Estudios = EstudioBLL.GetEstudiosAllCombo();
            EstudioComboBox.DataSource = Estudios;
            EstudioComboBox.DataTextField = "ContainerName";
            EstudioComboBox.DataValueField = "ContainerId";
            EstudioComboBox.DataBind();
        }
        catch (Exception eq)
        {
            log.Error("There was an error loading getCLientesDesgravamen(), getEstudiosAllCombo()", eq);
            SystemMessages.DisplaySystemErrorMessage("");
        }

        CalculateTimeIntervals();
    }

    private void CalculateTimeIntervals()
    {
        try
        {
            int proveedorId = Convert.ToInt32(ProveedorHiddenField.Value);
            ProveedorDesgravamen obj = ProveedorMedicoBLL.GetProveedorMedicoId(proveedorId);
            int interval = obj.DuracionCita;

            List<ComboContainer> list = new List<ComboContainer>();
            string horaInicial = System.Configuration.ConfigurationManager.AppSettings["DESGHoraInicial"];
            string horaFinal = System.Configuration.ConfigurationManager.AppSettings["DESGHoraFinal"];
            string[] splitInicial = horaInicial.Split(':');
            string[] splitFinal = horaFinal.Split(':');
            
            int hora = Convert.ToInt32(splitInicial[0]);
            int minuto = Convert.ToInt32(splitInicial[1]);

            TimeSpan timeInitial = new TimeSpan(hora, minuto, 0);

            hora = Convert.ToInt32(splitFinal[0]);
            minuto = Convert.ToInt32(splitFinal[1]);

            TimeSpan timeFinal = new TimeSpan(hora, minuto, 0);


            TimeSpan Proveedor_Interval = TimeSpan.FromMinutes(interval);

            while (timeInitial.CompareTo(timeFinal) <= 0)
            {
                DateTime dtInicial = DateTime.Today + timeInitial;
                string Id = dtInicial.ToString("HH:mm:ss");
                string Name = dtInicial.ToString("hh:mm tt");

                list.Add(new ComboContainer()
                {
                    ContainerId = Id,
                    ContainerName = Name
                });
                timeInitial = timeInitial.Add(Proveedor_Interval);
            }
            if (list.Count > 0)
            {
                HoraInicialComboBox.DataSource = list;
                HoraInicialComboBox.DataValueField = "ContainerId";
                HoraInicialComboBox.DataTextField = "ContainerName";
                HoraInicialComboBox.DataBind();

                HoraFinalComboBox.DataSource = list;
                HoraFinalComboBox.DataValueField = "ContainerId";
                HoraFinalComboBox.DataTextField = "ContainerName";
                HoraFinalComboBox.DataBind();
            }
            else
            {
                log.Error("No Time intervals were generated for Provider: " + ProveedorHiddenField.Value + "Check Interval: " + interval);
                SystemMessages.DisplaySystemErrorMessage("No se generaron intervalos de tiempo para escoger, contactar al administrador");
            }
        }
        catch (Exception e)
        {
            log.Error("Error converting proveedorId to int" + ProveedorHiddenField.Value, e);
            SystemMessages.DisplaySystemErrorMessage("El proceso de creacion no puede cargar rangos");
        }

    }

    private void loadEstudioProveedorFormData()
    {
        if (!string.IsNullOrEmpty(EstudioProveedorHiddenId.Value) && EstudioProveedorHiddenId.Value != "0" &&
            !string.IsNullOrEmpty(ClienteEstudioProveedorHiddenId.Value) && ClienteEstudioProveedorHiddenId.Value != "0")
        {
            try
            {
                int EstudioId = Convert.ToInt32(EstudioProveedorHiddenId.Value);
                int ProveedorId = Convert.ToInt32(ProveedorHiddenField.Value);
                int ClienteId = Convert.ToInt32(ClienteEstudioProveedorHiddenId.Value);
                ProveedorEstudios obj = ProveedorEstudiosBLL.GetProveedorEstudiosById(EstudioId, ProveedorId, ClienteId);
                if (obj != null && obj.EstudioId > 0 && obj.ProveedorMedicoId > 0 && obj.ClienteId > 0)
                {
                    EstudioComboBox.SelectedValue = obj.EstudioId.ToString();
                    ClientesEstudioComboBox.SelectedValue = obj.ClienteId.ToString();
                    string Deshabilitado = obj.Deshabilitado.ToString().ToUpper();
                    EstadoEstudioProveedorComboBox.SelectedValue = Deshabilitado;
                    string NecesitaEstudio = obj.NecesitaCita.ToString().ToUpper();
                    NecesitaCitaEstudioComboBox.SelectedValue = NecesitaEstudio;

                    EstudioSpan.InnerText = EstudioComboBox.SelectedItem.Text;
                    ClienteSpan.InnerText = ClientesEstudioComboBox.SelectedItem.Text;
                    EstudioSpan.Visible = true;
                    ClienteSpan.Visible = true;
                    EstudioComboBox.Visible = false;
                    ClientesEstudioComboBox.Visible = false;

                    DateTime dt_HoraInicio = DateTime.Today + obj.HoraInicio;
                    DateTime dt_HoraFin = DateTime.Today + obj.HoraFin;
                    string str_HoraInicio = dt_HoraInicio.ToString("HH:mm:ss");
                    string str_HoraFin = dt_HoraFin.ToString("HH:mm:ss");

                    HoraInicialComboBoxHiddenField.Value = str_HoraInicio;
                    HoraFinalComboBoxHiddenField.Value = str_HoraFin;

                    if (HoraInicialComboBox.Items.FindItemByValue(str_HoraInicio) != null)
                    {
                        HoraInicialComboBox.SelectedValue = str_HoraInicio;
                    }
                    else
                    {
                        if (str_HoraInicio != "00:00:00")
                        {
                            HoraInicialComboBox.Items.Insert(0, new RadComboBoxItem(dt_HoraInicio.ToString("hh:mm tt"), str_HoraInicio));
                        }
                        else
                        {
                            HoraInicialComboBox.Items.Insert(0, new RadComboBoxItem(str_HoraInicio, str_HoraInicio));
                        }
                    }

                    if (HoraFinalComboBox.Items.FindItemByValue(str_HoraFin) != null)
                    {
                        HoraFinalComboBox.SelectedValue = str_HoraFin;
                    }
                    else
                    {
                        if (str_HoraInicio != "00:00:00")
                        {
                            HoraFinalComboBox.Items.Insert(0, new RadComboBoxItem(dt_HoraFin.ToString("hh:mm tt"), str_HoraFin));
                        }
                        else
                        {
                            HoraFinalComboBox.Items.Insert(0, new RadComboBoxItem(str_HoraFin, str_HoraFin));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Error parsing EstudioProveedor Data", e);
                SystemMessages.DisplaySystemErrorMessage("No se pudo realizar la carga del estudio, favor seleccionar un estudio valido");
            }
        }
    }
    protected void GuardarEstudioProveedor_Click(object sender, EventArgs e)
    {
        saveEstudioProveedor();
        IsUpdate.Value = IS_NOT_UPDATE;
    }

    private bool validateFormData()
    {
        bool result;
        if (string.IsNullOrEmpty(EstadoEstudioProveedorComboBox.SelectedValue) ||
            !Boolean.TryParse(EstadoEstudioProveedorComboBox.SelectedValue.ToLower(), out result))
        {
            return false;
        }

        if (string.IsNullOrEmpty(NecesitaCitaEstudioComboBox.SelectedValue) ||
            !Boolean.TryParse(NecesitaCitaEstudioComboBox.SelectedValue.ToLower(), out result))
        {
            return false;
        }
        bool isNecesita = bool.Parse(NecesitaCitaEstudioComboBox.SelectedValue.ToLower());

        if (isNecesita)
        {
            Regex regex = new Regex(@"(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]");

            if (string.IsNullOrEmpty(HoraInicialComboBoxHiddenField.Value)
                || HoraInicialComboBoxHiddenField.Value == "00:00:00" || !regex.IsMatch(HoraInicialComboBoxHiddenField.Value))
            {
                return false;
            }

            if (string.IsNullOrEmpty(HoraFinalComboBoxHiddenField.Value) || HoraFinalComboBoxHiddenField.Value == "00:00:00"
                || !regex.IsMatch(HoraFinalComboBoxHiddenField.Value))
            {
                return false;
            }
        }

        int i;

        if (IsUpdate.Value == IS_NOT_UPDATE)
        {
            if (string.IsNullOrEmpty(InsertEstudioValue.Value) ||
                !int.TryParse(InsertEstudioValue.Value, out i))
            {
                return false;
            }

            if (string.IsNullOrEmpty(InsertClienteValue.Value) ||
                !int.TryParse(InsertClienteValue.Value, out i))
            {
                return false;
            }        
        }

        return true;
    }

    private void saveEstudioProveedor()
    {
        try
        {
            if (validateFormData())
            {
                int i;
                if (!string.IsNullOrEmpty(EstudioProveedorHiddenId.Value) &&
                    EstudioProveedorHiddenId.Value != "0" && 
                    int.TryParse(EstudioProveedorHiddenId.Value, out i) &&
                    !string.IsNullOrEmpty(ClienteEstudioProveedorHiddenId.Value) &&
                    ClienteEstudioProveedorHiddenId.Value != "0" &&
                    int.TryParse(ClienteEstudioProveedorHiddenId.Value, out i) &&
                    !string.IsNullOrEmpty(ProveedorHiddenField.Value) &&
                    ProveedorHiddenField.Value != "0" &&
                    int.TryParse(ProveedorHiddenField.Value, out i))
                {
                    
                    

                    //update
                    int estudioId = Convert.ToInt32(EstudioProveedorHiddenId.Value);
                    int proveedorId = Convert.ToInt32(ProveedorHiddenField.Value);
                    int clienteId = Convert.ToInt32(ClienteEstudioProveedorHiddenId.Value);

                    
                    bool estadoDeshabilitado = Convert.ToBoolean(EstadoEstudioProveedorComboBox.SelectedValue.ToLower());
                    bool necesitaCita = Convert.ToBoolean(NecesitaCitaEstudioComboBox.SelectedValue.ToLower());
                    TimeSpan timeInicio = TimeSpan.Parse("00:00:00");
                    TimeSpan timeFin = TimeSpan.Parse("00:00:00");

                    if (necesitaCita)
                    {
                        timeInicio = TimeSpan.Parse(HoraInicialComboBoxHiddenField.Value);
                        timeFin = TimeSpan.Parse(HoraFinalComboBoxHiddenField.Value);
                    }

                    ProveedorEstudiosBLL.UpdateEstudioProveedor(estudioId, proveedorId, clienteId, necesitaCita, estadoDeshabilitado, timeInicio, timeFin);
                    SystemMessages.DisplaySystemMessage("Se actualizaron los datos");
                    ProveedorEstudiosDesgravamenGridView.Rebind();
                    
                    
                }
                else if (EstudioProveedorHiddenId.Value == "0" && ClienteEstudioProveedorHiddenId.Value == "0" &&
                    ProveedorHiddenField.Value != "0" && int.TryParse(ProveedorHiddenField.Value, out i))
                {
                    //Insert
                    int estudioId = Convert.ToInt32(InsertEstudioValue.Value);
                    int proveedorId = Convert.ToInt32(ProveedorHiddenField.Value);
                    int clienteId = Convert.ToInt32(InsertClienteValue.Value);

                    ProveedorDesgravamen prov = ProveedorMedicoBLL.GetProveedorMedicoId(proveedorId);


                    int ProveedorActual = ProveedorEstudiosBLL.VerifyProveedorEstudio(prov.CiudadId, estudioId, clienteId);

                    if (ProveedorActual != 0)
                    {
                        ProveedorDesgravamen p = ProveedorMedicoBLL.GetProveedorMedicoId(ProveedorActual);
                        SystemMessages.DisplaySystemErrorMessage("El Estudio ya se encuentra registrado en esta plaza para el proveedor: " + p.ProveedorNombre);
                    }
                    else
                    {

                        bool estadoDeshabilitado = Convert.ToBoolean(EstadoEstudioProveedorComboBox.SelectedValue.ToLower());
                        bool necesitaCita = Convert.ToBoolean(NecesitaCitaEstudioComboBox.SelectedValue.ToLower());
                        TimeSpan timeInicio = TimeSpan.Parse("00:00:00");
                        TimeSpan timeFin = TimeSpan.Parse("00:00:00");
                        if (necesitaCita)
                        {
                            timeInicio = TimeSpan.Parse(HoraInicialComboBoxHiddenField.Value);
                            timeFin = TimeSpan.Parse(HoraFinalComboBoxHiddenField.Value);
                        }
                        ProveedorEstudios obj = ProveedorEstudiosBLL.GetProveedorEstudiosById(estudioId, proveedorId, clienteId);

                        if (obj != null)
                        {
                            log.Error("User tried to Insert a row with the same PK as something else - Estudio:[" + estudioId + "] Proveedor:[" + proveedorId + "] clienteId:[" + clienteId + "]");
                            SystemMessages.DisplaySystemWarningMessage("El estudio ya se encuentra agregado para el cliente y proveedor, favor verificar");
                        }
                        else
                        {
                            ProveedorEstudiosBLL.InsertEstudioProveedor(estudioId, proveedorId, clienteId, necesitaCita, estadoDeshabilitado, timeInicio, timeFin);
                            SystemMessages.DisplaySystemMessage("Se ha agregado el Estudio correctamente");
                        }
                    }
                }
                else
                {
                    log.Error("The data that's up for saving is not valid EstudioId: [" + EstadoEstudioProveedorHiddenId.Value +
                        "] ProveedorId: [" + ProveedorHiddenField.Value + "] ClienteId: [" + ClienteIdHiddenField.Value + "]");
                    SystemMessages.DisplaySystemErrorMessage("Los datos ingresados no son validos");
                }
            }
            else
            {
                log.Error("Error on the data from the Form (ComboBoxes on Modal)");
                SystemMessages.DisplaySystemErrorMessage("Los datos ingresados no son validos, seleccione los datos correctamente");
            }
            resetValues();
            ProveedorEstudiosDesgravamenGridView.Rebind();
        }
        catch (Exception e)
        {
            log.Error("Error of Insert/Update EstudioId: [" + EstadoEstudioProveedorHiddenId.Value +
                    "] ProveedorId: [" + ProveedorHiddenField.Value + "] ClienteId: [" + ClienteIdHiddenField.Value + "]", e);
            SystemMessages.DisplaySystemErrorMessage("Hubo un error al ingresar datos, no se ha guardado la informacion");
        }
    }

    private void resetValues()
    {
        ClienteEstudioProveedorHiddenId.Value = "0";
        EstudioProveedorHiddenId.Value = "0";
        InsertEstudioValue.Value = "0";
        InsertClienteValue.Value = "0";
        HoraInicialComboBoxHiddenField.Value = "0";
        HoraFinalComboBoxHiddenField.Value = "0";
        NecesitaCitaEstudioComboBox.SelectedValue = "FALSE";
        AddOldEstudioNombre.Value = "";
    }
    protected void BtnCrearEstudio_Click(object sender, EventArgs e)
    {
        loadAddEstudioForm();
        CreacionEstudioWindow.VisibleOnPageLoad = true;
    }

    private void loadAddEstudioForm()
    {
        List<ComboContainer> list = EstudioBLL.GetCategoriasEstudio();
        CategoriaEstudioComboBox.DataSource = list;
        CategoriaEstudioComboBox.DataValueField = "ContainerId";
        CategoriaEstudioComboBox.DataTextField = "ContainerName";
        CategoriaEstudioComboBox.DataBind();

        List<ComboContainer> list2 = EstudioBLL.GetCategoriasEstudio();
        list2.Insert(0, new ComboContainer()
        {
            ContainerId = "0",
            ContainerName = "--Seleccione una Categoria--"
        });
        CategoriaUpdateComboBox.DataSource = list2;
        CategoriaUpdateComboBox.DataValueField = "ContainerId";
        CategoriaUpdateComboBox.DataTextField = "ContainerName";
        CategoriaUpdateComboBox.DataBind();

        if (list != null && list.Count > 0)
        {
            string value = list[0].ContainerId;
            CategoriaEstudioComboBox.SelectedValue = value;
            AddCategoriaIdHiddenField.Value = value;
            
        }

        List<ComboContainer> listEstudios = EstudioBLL.GetEstudiosAllCombo();
        listEstudios.Insert(0, new ComboContainer() {
            ContainerId = "0",
            ContainerName = "--Seleccione un Estudio--"
        });

        EstudioUpdateCombo.DataSource = listEstudios;
        EstudioUpdateCombo.DataValueField = "ContainerId";
        EstudioUpdateCombo.DataTextField = "ContainerName";
        EstudioUpdateCombo.EmptyMessage = "--Seleccione un Estudio--";
        EstudioUpdateCombo.DataBind();
        //EstudioUpdateCombo

        resetValues();
    }

    [WebMethod(EnableSession = true)]
    public static EstudioDesgravamen GetEstudio(string EstudioId)
    {
        int intEstudioId = 0;
        if (int.TryParse(EstudioId, out intEstudioId)) {
            try 
	        {	        
		        EstudioDesgravamen estudio = EstudioBLL.GetEstudioById(intEstudioId);
                return estudio;
	        }
	        catch (Exception eq)
	        {		
		      log.Error("There was en error when using function GetEstudioById",eq);               
	        }
            
        }
        return null;
    }

    [WebMethod(EnableSession = true)]
    public static EstudioDesgravamen GetCategoria(string CategoriaId)
    {
        int intCategoriaId = 0;
        if (int.TryParse(CategoriaId, out intCategoriaId))
        {
            try
            {
                EstudioDesgravamen estudio = EstudioBLL.GetEstudioById(intCategoriaId);
                return estudio;
            }
            catch (Exception eq)
            {
                log.Error("There was en error when using function GetEstudioById", eq);
            }

        }
        return null;
    }

    protected void GuardarEstudio_Click(object sender, EventArgs e)
    {
        try
        {

            //check that there isn't another estudio named like it//                                                
            int cantEstudiosInsert = 0;
            string estudioNombre = (string.IsNullOrEmpty(EstudioNombreTextBox.Text)? "": EstudioNombreTextBox.Text);
            cantEstudiosInsert = EstudioBLL.CountEstudioByName(estudioNombre);
            if (cantEstudiosInsert > 0 && AddOldEstudioNombre.Value != EstudioNombreTextBox.Text)
            {
                SystemMessages.DisplaySystemErrorMessage("No se pudieron guardar los datos, existe un estudio con el mismo nombre");
                return;
            }
            ///////////////////////////////////////////////////////

            if (CategoriaIdRadButton.SelectedToggleState.Value == "ESTUDIO")
            {
                if (EstudioManageCombo.SelectedValue == "0")
                {
                    //insertar
                    //int estudioId = 0;
                    int categoriaId = 0;
                    if (!string.IsNullOrEmpty(EstudioNombreTextBox.Text) &&
                        !string.IsNullOrEmpty(AddCategoriaIdHiddenField.Value) &&
                        AddCategoriaIdHiddenField.Value != "0" &&
                        int.TryParse(AddCategoriaIdHiddenField.Value, out categoriaId))
                    {                        

                        EstudioDesgravamen obj = new EstudioDesgravamen()
                        {
                            EstudioNombre = EstudioNombreTextBox.Text,
                            CategoriaId = categoriaId
                        };
                        if (EstudioBLL.InsertEstudio(obj) > 0)
                        {                            
                            SystemMessages.DisplaySystemMessage("Se ha guardado el estudio existosamente");
                        }
                        else
                        {
                            log.Error("Something wrong happened and the estudio wasn't saved");
                            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del estudio");
                        }

                    }
                    else
                    {
                        log.Error("the data to insert isn't in the correct format, EstudioNombreTextBox: " + EstudioNombreTextBox.Text + " CategoriaId: " + AddCategoriaIdHiddenField.Value);
                        SystemMessages.DisplaySystemErrorMessage("Los datos ingresados no son válidos");
                    }
                }
                else
                {
                    //actualizar
                    int estudioId = 0;
                    int categoriaId = 0;
                    if (!string.IsNullOrEmpty(EstudioNombreTextBox.Text) &&
                        !string.IsNullOrEmpty(AddCategoriaIdHiddenField.Value) &&
                        !string.IsNullOrEmpty(AddEstudioIdHiddenField.Value) &&
                        AddCategoriaIdHiddenField.Value != "0" &&
                        AddEstudioIdHiddenField.Value != "0" &&
                        int.TryParse(AddCategoriaIdHiddenField.Value, out categoriaId) &&
                        int.TryParse(AddEstudioIdHiddenField.Value, out estudioId))
                    {
                        EstudioDesgravamen obj = new EstudioDesgravamen()
                        {
                            EstudioId = estudioId,
                            EstudioNombre = EstudioNombreTextBox.Text,
                            CategoriaId = categoriaId
                        };
                        EstudioBLL.UpdateEstudio(obj);    
                        SystemMessages.DisplaySystemMessage("Se ha guardado el estudio existosamente");                        
                    }
                    else
                    {
                        log.Error("the data to update isn't in the correct format, EstudioNombreTextBox: " + EstudioNombreTextBox.Text + " CategoriaId: " + AddCategoriaIdHiddenField.Value + " EstudioId: "+AddEstudioIdHiddenField.Value);
                        SystemMessages.DisplaySystemErrorMessage("Los datos ingresados no son válidos");
                    }
                }
            }
            else
            {
                if (EstudioManageCombo.SelectedValue == "0")
                {
                    //insertar
                    //int estudioId = 0;
                    int categoriaId = 0;
                    if (!string.IsNullOrEmpty(EstudioNombreTextBox.Text))
                    {
                        EstudioDesgravamen obj = new EstudioDesgravamen()
                        {
                            EstudioNombre = EstudioNombreTextBox.Text,
                            CategoriaId = categoriaId
                        };
                        if (EstudioBLL.InsertEstudio(obj) > 0)
                        {
                            SystemMessages.DisplaySystemMessage("Se ha guardado la categoria existosamente");
                        }
                        else
                        {
                            log.Error("Something wrong happened and the estudio wasn't saved");
                            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del estudio");
                        }

                    }
                    else
                    {
                        log.Error("the data to insert isn't in the correct format, EstudioNombreTextBox: " + EstudioNombreTextBox.Text + " CategoriaId: " + AddCategoriaIdHiddenField.Value);
                        SystemMessages.DisplaySystemErrorMessage("Los datos ingresados no son válidos");
                    }
                }
                else
                {
                    //actualizar
                    int estudioId = 0;
                    int categoriaId = 0;
                    if (!string.IsNullOrEmpty(EstudioNombreTextBox.Text) &&
                        !string.IsNullOrEmpty(AddEstudioIdHiddenField.Value) &&
                        AddEstudioIdHiddenField.Value != "0" &&
                        int.TryParse(AddEstudioIdHiddenField.Value, out estudioId))
                    {
                        EstudioDesgravamen obj = new EstudioDesgravamen()
                        {
                            EstudioId = estudioId,
                            EstudioNombre = EstudioNombreTextBox.Text,
                            CategoriaId = categoriaId
                        };
                        EstudioBLL.UpdateEstudio(obj);
                        SystemMessages.DisplaySystemMessage("Se ha guardado la categoria existosamente");
                    }
                    else
                    {
                        log.Error("the data to update isn't in the correct format, EstudioNombreTextBox: " + EstudioNombreTextBox.Text + " CategoriaId: " + AddCategoriaIdHiddenField.Value + " EstudioId: " + AddEstudioIdHiddenField.Value);
                        SystemMessages.DisplaySystemErrorMessage("Los datos ingresados no son válidos");
                    }
                }
            }
            resetFormAddValues();
            ProveedorEstudiosDesgravamenGridView.Rebind();
        }
        catch (Exception eq)
        {
            log.Error("there was an error when trying to save", eq);
            SystemMessages.DisplaySystemErrorMessage("No se pudo guardar los datos del estudio");
        }
    }

    private void resetFormAddValues()
    {
        AddEstudioIdHiddenField.Value = "0";
        AddCategoriaIdHiddenField.Value = "0";
        EstudioNombreTextBox.Text = "";
        EstudioManageCombo.SelectedValue = "0";

        
    }

    
    private static ClientEstudioTimeRangeCollisionJSON GetProveedorEstudioCollisions(int ProveedorId, int EstudioId, int ClienteId,
        TimeSpan HoraInicio, TimeSpan HoraFin)
    {
        int? collisions = 0;            
        List<ClientEstudioTimeRangeCollisionJSON> list = ProveedorEstudiosBLL.GetTimeCollisionsByProveedorEstudio(
                    ProveedorId, EstudioId, ClienteId, HoraInicio, HoraFin, out collisions);

        if (list != null && list.Count > 0)
        {
            ClientEstudioTimeRangeCollisionJSON obj = list[0];
            if (list.Count > 1)
                obj.OtherMessage = "y Otros";

            return obj;
        }
        else
        {
            if (collisions < 0)
                throw new Exception("Collisions returned with Error Variable SET to -1");
        }

        return null;
    }

    [WebMethod(EnableSession = true)]
    public static ClientEstudioTimeRangeCollisionJSON GetCollisions(string ProveedorId, string EstudioId, string ClienteId,
        string HoraInicio, string HoraFin)
    {
        ClientEstudioTimeRangeCollisionJSON jsonResponse = new ClientEstudioTimeRangeCollisionJSON();
        int intProveedorId = 0;
        int intEstudioId = 0;
        int intClienteId = 0;
        TimeSpan TimeInicio = new TimeSpan(0, 0, 0);
        TimeSpan TimeFin = new TimeSpan(0, 0, 0);
        try
        {
            intProveedorId = Convert.ToInt32(ProveedorId);
            intEstudioId = Convert.ToInt32(EstudioId);
            intClienteId = Convert.ToInt32(ClienteId);
            if (HoraInicio != "00:00:00" && HoraFin != "00:00:00")
            {
                TimeInicio = TimeSpan.Parse(HoraInicio);
                TimeFin = TimeSpan.Parse(HoraFin);

                ClientEstudioTimeRangeCollisionJSON obj = Desgravamen_ProveedorEstudios.GetProveedorEstudioCollisions(intProveedorId, intEstudioId, intClienteId, TimeInicio, TimeFin);
                return obj;
            }
            else
            {
                throw new Exception();
            }
                        
        }
        catch (Exception)
        {
            log.Error("Error while Parsing Parameters, ProveedorId: "+ProveedorId+
                ", EstudioId: "+EstudioId+", HoraInicio: "+HoraInicio+", HoraFin: "+HoraFin);
            SystemMessages.DisplaySystemErrorMessage("Hubo un error verificando las colisiones");
        }
        jsonResponse.OtherMessage = "ERROR";

        return jsonResponse;
    }
}