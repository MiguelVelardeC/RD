using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.User.BLL;
using Telerik.Web.UI;
using Artexacta.App.Caso.BLL;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedCliente;
using Artexacta.App.Caso;
using System.Text.RegularExpressions;
using SearchComponent;
using System.Threading;
using Artexacta.App.Security.BLL;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Medico;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Ciudad;
public partial class CasoMedico_CasoMedicoLista : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int DataGridPageSize = 10;
    private List<string> userPermissions;

    private int NumeroDiasReconsulta
    {
        get
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(NumeroDiasReconsultaHF.Value) && LastClienteHF.Value == ClienteDDL.SelectedValue)
                {
                    return int.Parse(NumeroDiasReconsultaHF.Value);
                }
                else
                {
                    LastClienteHF.Value = ClienteDDL.SelectedValue;
                    RedCliente redCliente = RedClienteBLL.GetRedClienteByClienteId(int.Parse(ClienteDDL.SelectedValue));
                    NumeroDiasReconsultaHF.Value = redCliente.NumeroDiasReconsulta.ToString();
                    return redCliente.NumeroDiasReconsulta;
                }
            }
            catch { return 10; }
        }
    }
    protected void ProcessSessionParameters(bool MANAGE_CASOS, bool CASO_EMERGENCIA, bool MANAGE_ENFERMERIA)
    {
        if (ModeHF.Value.Length == 0)
        {

            if (Session["MODE"] != null && !string.IsNullOrEmpty(Session["MODE"].ToString()))
            {

                string s = Session["MODE"].ToString();
                switch (Session["MODE"].ToString())
                {
                    case "ENFERMERIA":
                        if (MANAGE_CASOS || CASO_EMERGENCIA)
                            ModeHF.Value = "@TipoConsulta ENFER";
                        break;
                    case "EMERGENCIA":
                        if (MANAGE_CASOS || MANAGE_ENFERMERIA)
                            ModeHF.Value = "@TipoConsulta EMERG";
                        break;
                    case "ODONTOLOGIA":
                        if (MANAGE_CASOS)
                            ModeHF.Value = "@TipoConsulta ODONTO";
                        break;
                    default:
                        ModeHF.Value = " ";
                        break;
                }
            }
            else
            {
                Session["MODE"] = null;
                bool casos = userPermissions.Contains("MANAGE_CASOS") || userPermissions.Contains("SOLO_VISTA");

                if (casos)
                    ModeHF.Value = "@TipoConsulta ACCID";
                    ModeHF2.Value = "@TipoConsulta CONONLINE";
            }
        }
        Session["MODE"] = null;
    }

    //para poder que se va mostrar en el RadGrid los  botones de Crear
    private void GetPermissions(bool reducedFunctionality = false)
    {
        userPermissions = SecurityBLL.GetUserPermissions();
        bool SOLO_VISTA = userPermissions.Contains("SOLO_VISTA");
        if (SOLO_VISTA)
        {
            if (!reducedFunctionality)
            {
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnVer").Visible = true;
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnEditar").Visible = false;
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnHistorial").Visible = false;
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnReconsulta").Visible = false;
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnEliminar").Visible = false;
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnEliminar").Visible = false;
                //CasoRadGrid.MasterTableView.GetColumn("CasoGravedad").Visible = false;
                TitleLabel.Text = "Lista de Casos Médicos";

                if (!IsPostBack)
                {
                    ProcessSessionParameters(true, true, true);
                }
            }
            else
            {
                ProcessSessionParameters(true, true, true);
            }
        }
        else
        {
            bool MANAGE_CASOS = userPermissions.Contains("MANAGE_CASOS");
            bool CASO_EMERGENCIA = userPermissions.Contains("CASO_EMERGENCIA");
            bool MANAGE_ENFERMERIA = userPermissions.Contains("MANAGE_ENFERMERIA");

            if (!reducedFunctionality)
            {
                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnVer").Visible = false;

                if (MANAGE_CASOS || CASO_EMERGENCIA || MANAGE_ENFERMERIA)
                {
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnEditar").Visible = true;
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnHistorial").Visible = true;
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnReconsulta").Visible = true;
                }
                else
                {
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnEditar").Visible = false;
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnHistorial").Visible = false;
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnReconsulta").Visible = false;
                }

                CasoRadGrid.MasterTableView.GetColumn("TemplateColumnGastos").Visible = userPermissions.Contains("MANAGE_GASTOS_CASOS");
                if (userPermissions.Contains("DELETE_CASO"))
                {
                    CasoRadGrid.MasterTableView.GetColumn("TemplateColumnEliminar").Visible = true;
                    //si es -1 mostrar todos(es para eliminar)
                    this.ProveedorIdHF.Value = "0";
                }

                if (MANAGE_CASOS)
                {
                    TitleLabel.Text = "Lista de casos médicos";
                }
                else if (CASO_EMERGENCIA)
                {
                    TitleLabel.Text = "Lista de casos de emergencias";
                }
                else if (MANAGE_ENFERMERIA)
                {
                    TitleLabel.Text = "Lista de casos de enfermería";
                }

                //if (!MANAGE_CASOS)
                //{
                //    CasoRadGrid.MasterTableView.GetColumn("CasoGravedad").Visible = false;
                //}
                if (!IsPostBack)
                {
                    ProcessSessionParameters(MANAGE_CASOS, CASO_EMERGENCIA, MANAGE_ENFERMERIA);
                }
            }
            else
            {
                ProcessSessionParameters(MANAGE_CASOS, CASO_EMERGENCIA, MANAGE_ENFERMERIA);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        log.Info("########### Starting Load Caso Medico Lista ###########");
        GetProveedorId();
        SearchCasoMedico.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchCasoMedico.Config = new CasoMedicoSearch();
        GetPermissions();

        int userId = 0;
        try
        {
            userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
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

        //MedicoUsuarioId hiddenfield Id
        Medico medico = null;
        if (userId > 0)
        {
            try
            {
                medico = MedicoBLL.getMedicoByUserId(userId);
            }
            catch (Exception)
            {
                log.Info("No medico is assigned to that UserId" + userId);
                SystemMessages.DisplaySystemWarningMessage("No tiene un medico asignado a su usuario");
            }


            bool isUserAdmin = false;

            if (userPermissions.Contains("VER_TODOS_LOS_CLIENTES_CASOMEDICO"))
            {
                isUserAdmin = true;
            }
            else
            {
                isUserAdmin = false;
            }

            if (isUserAdmin)
            {
                MedicoUsuarioId.Value = "0";
            }
            else
            {
                if (medico != null)
                {
                    MedicoUsuarioId.Value = medico.MedicoId.ToString();
                }

                else
                {
                    MedicoUsuarioId.Value = "-1";
                }
            }
        }
        if (!IsPostBack)
        {
            //loadSaveSearch();
            if (!string.IsNullOrWhiteSpace(ModeHF.Value))
            {
                SearchCasoMedico.Query = "(" + ModeHF.Value + " OR " + ModeHF2.Value + ")"; 
            }
            loadExtraCombos();
            //BindGrid();
        }
        CargarParametrosUser();
        log.Info("########### Ending Load Caso Medico Lista ###########");

    }


    private void loadExtraCombos()
    {
        ciudadComboBox.DataSource = CiudadBLL.getCiudadList();
        ciudadComboBox.DataBind();
        ciudadComboBox.Items.Insert(0, new ListItem
        {
            Selected = true,
            Text = "TODAS",
            Value = ""
        });
        medicoComboBox.DataSource = MedicoBLL.getAllMedicos();
        medicoComboBox.DataBind();
        medicoComboBox.Items.Insert(0, new ListItem
        {
            Selected = true,
            Text = "TODOS",
            Value = ""
        });
        //Check permissions to see what to load
        bool isUserAdmin = false;
        try
        {

            if (userPermissions.Contains("VER_TODOS_LOS_CLIENTES_CASOMEDICO"))
            {
                isUserAdmin = true;
            }
            else
            {
                isUserAdmin = false;
            }
            //////
        }
        catch (Exception q)
        {
            log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver derivaciones como administrador", q);
            isUserAdmin = false;
        }
        List<GenericComboContainer> clientes = null;
        try
        {
            if (isUserAdmin)
            {
                clientes = DerivacionBLL.GetClientesByMedicoIdCombo(0);
            }
            else
            {
                int clienteId = 0;
                int.TryParse(MedicoUsuarioId.Value, out clienteId);
                if (clienteId >= 0)
                    clientes = DerivacionBLL.GetClientesByMedicoIdCombo(clienteId);
            }
        }
        catch (Exception eqq)
        {

        }
        if (isUserAdmin)
        {
            if (clientes != null)
            {
                clientes.Insert(0, new GenericComboContainer()
                {
                    ContainerId = "0",
                    ContainerName = "TODOS"
                });
            }
            else
            {
                clientes = new List<GenericComboContainer>();
                clientes.Add(new GenericComboContainer()
                {
                    ContainerId = "0",
                    ContainerName = "TODOS"
                });
            }
        }

        if (clientes != null)
        {
            ClienteDDL.DataSource = clientes;
            ClienteDDL.DataValueField = "ContainerId";
            ClienteDDL.DataTextField = "ContainerName";
            ClienteDDL.DataBind();
        }
    }

    public void searchCtl_CasoMedico_OnSearch()
    {
        string sql = SearchCasoMedico.Sql;
        log.Debug("Parameter whereSql: " + sql);
        ActivePageHF.Value = "0";
        // BindGrid();
    }
    private void loadSaveSearch()
    {
        if (HttpContext.Current.Request.Cookies["CasoMedicoSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("CasoMedicoSearch");
            string SearchSiniestrosQuery = SearchCasoMedico.Query;
            int DataGridPageSizeTemp = DataGridPageSize;
            string ActivePageHFValue = ActivePageHF.Value;
            try
            {
                SearchCasoMedico.Query = myCookie["QUERY"];
                DataGridPageSize = int.Parse(myCookie["PAGESIZE"]);
                ActivePageHF.Value = myCookie["ACTIVEPAGE"];
                ClienteDDL.SelectedValue = myCookie["CLIENTEID"];
                OrderByHF.Value = myCookie["ORDERBY"];
            }
            catch
            {
                SearchCasoMedico.Query = SearchSiniestrosQuery;
                DataGridPageSize = DataGridPageSizeTemp;
                ActivePageHF.Value = ActivePageHFValue;
            }
        }
    }

    private void saveSearch()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["CasoMedicoSearch"] != null)
        {
            myCookie = Request.Cookies.Get("CasoMedicoSearch");
        }
        else
        {
            myCookie = new HttpCookie("CasoMedicoSearch");
        }
        myCookie["QUERY"] = SearchCasoMedico.Query;
        myCookie["PAGESIZE"] = DataGridPageSize.ToString();
        myCookie["ACTIVEPAGE"] = ActivePageHF.Value;
        myCookie["CLIENTEID"] = ClienteDDL.SelectedValue;
        myCookie["ORDERBY"] = OrderByHF.Value;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void GetProveedorId()
    {
        if (string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
        {
            log.Warn("Usuario no identificado");
            return;
        }
        try
        {
            //si proveedorId>0 mostrar los de emergencia del proveedor,=0 motrar todos exepto los de emergencia
            //si es -1 mostrar todos(es para agregar gastos)
            int ProveedorId = 0;
            ProveedorId = UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name);
            this.ProveedorIdHF.Value = ProveedorId.ToString();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener datos de su cuenta de Usuario.");
            log.Error("Function GetProveedorId on page CasoMedicoLista.aspx", ex);
        }
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page CasoMedicoLista.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void CasoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Casos.");
            log.Error("Function CasoODS_Selected on page CasoMedicoLista.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void CasoRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select") || e.CommandName.Equals("AgregarGastos")
            || e.CommandName.Equals("EliminarCaso") || e.CommandName.Equals("Reconsulta"))
        {
            int CasoId = 0;
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                string MotivoConsultaId = item["MotivoConsultaId"].Text;
                CasoId = Convert.ToInt32(e.CommandArgument.ToString());
                Session["CasoId"] = CasoId.ToString();

                Session["MotivoConsultaIdT"] = MotivoConsultaId;

                Session["RECONSULTA"] = null;
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Casos Medico Seleccionado.");
                log.Error("Function CasoRadGrid_ItemCommand on page CasoMedicoLista.aspx", ex);
                return;
            }

            if (e.CommandName.Equals("Select"))
            {
                GridDataItem item = (GridDataItem)e.Item;
                string MotivoConsultaId = item["MotivoConsultaId"].Text;
                switch (MotivoConsultaId)
                {
                    case "ENFER":
                        //saveSearch();
                        Response.Redirect("Enfermeria.aspx");
                        break;
                    //case "EMERG":
                    //    saveSearch();
                    //    Response.Redirect("Emergencia.aspx");
                    //    break;
                    default:
                        //saveSearch();
                        Response.Redirect("CasoMedicoDetalle.aspx");
                        break;
                }
            }
            else if (e.CommandName.Equals("Reconsulta"))
            {
                //saveSearch();
                Session["CasoId"] = CasoBLL.CreateReconsulta(CasoId).ToString();
                Response.Redirect("CasoMedicoDetalle.aspx");
            }
            else if (e.CommandName.Equals("AgregarGastos"))
            {
                //saveSearch();
                Response.Redirect("~/Gasto/GastoDetalle.aspx");
            }
            else if (e.CommandName.Equals("EliminarCaso"))
            {
                if (CasoBLL.DeleteCasoByCadoId(CasoId))
                    SystemMessages.DisplaySystemMessage("Se elimino correctamente el Caso Medico.");
                else
                    SystemMessages.DisplaySystemErrorMessage("Error, No se puedo elimiar el Caso Medico, Verifique si no tiene gastos Consolidados.");

                BindGrid();
            }
        }
        else if (e.CommandName == "Historial")
        {
            int CasoId = 0;
            int PacienteId = 0;
            try
            {
                string[] ids = e.CommandArgument.ToString().Split(new char[] { ';' });
                CasoId = Convert.ToInt32(ids[0]);
                PacienteId = Convert.ToInt32(ids[1]);
                Session["CasoId"] = CasoId.ToString();
                Session["PacienteId"] = PacienteId.ToString();
                if (CasoId <= 0 || PacienteId <= 0)
                {
                    throw new ArgumentException("CasoId <= 0 or PacienteId <= 0");
                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Casos Medico o del paciente Seleccionado.");
                log.Error("Function CasoRadGrid_ItemCommand on page CasoMedicoLista.aspx [Historial]", ex);
                return;
            }
            //saveSearch();
            Response.Redirect("~/CasoMedico/Historial.aspx");
        }
        else if (e.CommandName == "View")
        {
            try
            {
                int CasoId = 0;
                CasoId = Convert.ToInt32(e.CommandArgument.ToString());
                Session["CasoId"] = CasoId.ToString();
                Response.Redirect("~/CasoMedico/CasoMedicoSoloVista.aspx");
            }
            catch (ThreadAbortException q) {/*Do nothing*/}
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Casos Medico.");
                log.Error("Function CasoRadGrid_ItemCommand on page CasoMedicoLista.aspx [View]", ex);
                return;
            }
        }
    }

    protected void SearchLB_Click(object sender, EventArgs e)
    {
        this.CasoRadGrid.Visible = true;
        CasoRadGrid.DataBind();
    }
    protected void CasoRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {

            GridDataItem da = e.Item as GridDataItem;
            // if (ModeHF.Value=="ODONTO")
            da["TemplateColumnReconsulta"].FindControl("ReconsultaImageButton").Visible = false;

            TimeSpan dateDiff = DateTime.Now.Subtract(DateTime.Parse(da["FechaCreacion"].Text));
            if (dateDiff.Days <= NumeroDiasReconsulta)
            {
                bool isReconsulta = Convert.ToBoolean(DataBinder.Eval(da.DataItem, "IsReconsulta"));
                bool hasReconsultaId = Convert.ToInt32(DataBinder.Eval(da.DataItem, "ReconsultaId")) > 0;
                da["TemplateColumnReconsulta"].FindControl("ReconsultaImageButton").Visible = (!isReconsulta && !hasReconsultaId);
            }
            if (CasoRadGrid.MasterTableView.GetColumn("TemplateColumnGastos").Visible)
            {
                /*int cantGastos = 0;
                try
                {
                    cantGastos = Convert.ToInt32(DataBinder.Eval(da.DataItem, "CantGastos"));
                    bool haveGastos = cantGastos > 0;
                    da["TemplateColumnGastos"].FindControl("GastoIB").Visible = haveGastos;
                }
                catch { }*/
            }
            //mostrar de otro color cuando el caso sea de nivel critico
            //if ((bool)DataBinder.Eval(da.DataItem, "CasoCritico"))
            //{
            //    da.CssClass = (e.Item.ItemType == GridItemType.AlternatingItem) ? "MontoMinimoEnPolizaInferiorAlt" : "MontoMinimoEnPolizaInferior";
            //}

            string motivoConsultaId = DataBinder.Eval(da.DataItem, "MotivoConsultaId").ToString();
            if (motivoConsultaId != "ACCID" && motivoConsultaId != "ODONTO")
            {
                ImageButton detailsImageButton = (ImageButton)da.FindControl("DetailsImageButton");

                detailsImageButton.ToolTip = (motivoConsultaId == "RECASO" ? "EDITAR RECONSULTA" : (motivoConsultaId == "ENFER" ? "EDITAR CASO DE ENFERMERIA" : (motivoConsultaId == "CONONLINE" ? "EDITAR CASO DE TELECONSULTA" : "EDITAR EMERGENCIA")));

                detailsImageButton.ImageUrl = "~/Images/Neutral/" + (motivoConsultaId == "RECASO" ? "reConsulta.png" : (motivoConsultaId == "ENFER" ? "enfermeria.jpg" : (motivoConsultaId == "CONONLINE" ? "tele.png" : "emergencia.jpg")));
            }
            else if (motivoConsultaId == "ODONTO")
            {
                da["TemplateColumnReconsulta"].FindControl("ReconsultaImageButton").Visible = false;
            }
        }
    }
    #region Pager
    protected void PrimeroButton_Click(object sender, EventArgs e)
    {
        // Vamos a la primera página
        ActivePageHF.Value = "0";

        // Desplegamos los datos
        BindGrid();
    }

    protected void AnteriorRapidoButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos a 5 paginas antes
        _activePage -= 5;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void AnteriorButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos a la página anterior
        _activePage -= 1;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void SiguienteButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos la próxima página
        _activePage += 1;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void SiguienteRapidoButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos a 5 paginas despues
        _activePage += 5;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void UltimoButton_Click(object sender, EventArgs e)
    {
        int _totalFilas = Convert.ToInt32(TotalFilasHF.Value);
        decimal _activePage = (decimal)_totalFilas / (decimal)DataGridPageSize;

        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = ((int)_activePage + ((_activePage == (int)_activePage) ? -1 : 0)).ToString();

        // Desplegamos los datos
        BindGrid();
    }
    #endregion
    private void BindGrid()
    {
        // En esta variable almacenamos el total de filas en la base de datos.  No sólo las cargadas.
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los productos leidos aquí. 
        List<Caso> _cache = new List<Caso>();

        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // No podemos ir antes de la página 0
        if (_activePage < 0)
            _activePage = 0;

        // Averiguamos desde que fila tenemos que cargar.
        int firstRow = _activePage * DataGridPageSize;

        if (firstRow < 0)
            firstRow = 0;

        // Obtener los datos de la BD
        try
        {
            int _proveedorId = Convert.ToInt32("0" + ProveedorIdHF.Value);
            if (string.IsNullOrWhiteSpace(ClienteDDL.SelectedValue))
            {
                ClienteDDL.DataBind();
            }
            int _clienteId = Convert.ToInt32(string.IsNullOrEmpty(ClienteDDL.SelectedValue) ? "0" : ClienteDDL.SelectedValue);

            Searcher searcher = new Searcher(new CasoMedicoSearch());
            try
            {
                searcher.Query = "";
                Match match = Regex.Match(SearchCasoMedico.Query.ToLower(), "@diagnostico [^@]*");
                if (match.Success && !Regex.IsMatch(SearchCasoMedico.Query.ToLower(), "@enfermedad2"))
                {
                    string enfermedad = match.Value.Replace("@diagnostico ", "").Replace("@", "");
                    searcher.Query = SearchCasoMedico.Query.ToLower().Replace("@diagnostico", "( @diagnostico") +
                        " OR @Enfermedad2 " + enfermedad +
                        " OR @Enfermedad3 " + enfermedad +
                        " OR @DiagnosticoPresuntivo " + enfermedad + " )";
                }
            }
            catch (Exception q)
            {
                log.Error("Error generating the query for CasoBLL.SearchCaso.", q);
            }

            int userId = 0;
            try
            {
                string sUsername = User.Identity.Name;

                Artexacta.App.User.User u = UserBLL.GetUserByUsername(sUsername);

                if (u != null)
                {
                    userId = u.UserId;
                }
            }
            catch (Exception)
            {
                log.Error("can't find the userId from the username at CasoMedicoLista.aspx");
            }


            string sql = string.IsNullOrWhiteSpace(searcher.Query) ? SearchCasoMedico.Sql : searcher.SqlQuery();

            int _medicoId = 0;
            if (medicoComboBox.SelectedValue.Length > 0)
                _medicoId = Convert.ToInt32(medicoComboBox.SelectedValue);

            _totalRows = CasoBLL.SearchCasoNew(_cache, DataGridPageSize, firstRow, sql, _clienteId, _medicoId, OrderByHF.Value);

            foreach (GridColumn col in CasoRadGrid.Columns)
            {
                if (!string.IsNullOrWhiteSpace(col.SortExpression))
                {

                    col.HeaderText = Regex.Replace(col.HeaderText, "\\s<.+>", "");
                    if (!string.IsNullOrWhiteSpace(col.SortExpression) &&
                        col.SortExpression == OrderByHF.Value.Replace(" DESC", "").Replace(" ASC", ""))
                    {
                        col.HeaderStyle.CssClass = "sortCol";
                        col.HeaderText = col.HeaderText + @" <img class=""sortImg"" style=""width:8px;border-width:0px;"" src=""" +
                            ResolveUrl("~/images/neutral/sort" + (OrderByHF.Value.EndsWith("ASC") ? "Up" : "Down") + ".png") +
                                           @""" title=""ASCENDIENTE"">";
                    }
                    else if (!col.UniqueName.StartsWith("TemplateColumn"))
                    {
                        col.HeaderStyle.CssClass = "sortable";
                    }
                }
            }
        }

        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Casos Médicos");
        }
        // Update the labels that tell us what we did
        LoadedFirstRecordLabel.Text = (firstRow + 1).ToString();
        LoadedNumRecordsLabel.Text = (firstRow + _cache.Count).ToString();
        TotalDBRecordsLabel.Text = _totalRows.ToString();

        // Nos aseguramos que no pasemos la última página 
        if (_activePage > (_totalRows / DataGridPageSize))
            _activePage = _totalRows / DataGridPageSize;

        // Grabar la página en la que estamos en el Viewstate
        ActivePageHF.Value = _activePage.ToString();

        // Registrar los índices de la primera y última fila cargada
        if (_cache.Count == 0)
        {
            PrimeraFilaCargadaHF.Value = "0";
            UltimaFilaCargadaHF.Value = "0";
            LoadedFirstRecordLabel.Text = "0";
        }
        else
        {
            PrimeraFilaCargadaHF.Value = (_cache[0].RowNumber - 1).ToString();
            UltimaFilaCargadaHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
        }

        // Registrar el total de filas leidas
        TotalFilasHF.Value = _totalRows.ToString();

        // Conectar el repeater con los datos cargados del Web
        CasoRadGrid.DataSource = _cache;
        CasoRadGrid.DataBind();

        // Actualizar el estado de habilitado los botones de navegación
        ChangeButtonStates();

        // Al final de esta rutina, los hidden fields PrimeraFilaCargadaHF, UltimaFilaCargadaHF,
        // ActivePageHF y TotalFilasHF tienen los valores de lo que realmente se cargó.
    }
    /// <summary>
    /// Apagamos o prendemos los botones de navegación del Grid2 dependiendo de donde
    /// estamos.
    /// </summary>
    private void ChangeButtonStates()
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);
        int _totalFilas = Convert.ToInt32(TotalFilasHF.Value);

        PreevButton.Enabled = true;
        PreevFastButton.Enabled = true;
        NextButton.Enabled = true;
        NextFastButton.Enabled = true;
        FirstButton.Enabled = true;
        LastButton.Enabled = true;

        // Si esamos en la primera, apagar los botones de primera, anterior y anterior -5
        if (_activePage == 0)
        {
            PreevButton.Enabled = false;
            PreevFastButton.Enabled = false;
            FirstButton.Enabled = false;
            if (_totalFilas <= DataGridPageSize)
            {
                NextButton.Enabled = false;
                NextFastButton.Enabled = false;
                LastButton.Enabled = false;
                return;
            }
        }

        // Si estamos en la última fila, apagar los botones de ultima, siguiente y siguiente + 5
        decimal decActivePage = (decimal)_totalFilas / (decimal)DataGridPageSize;
        decActivePage = ((int)decActivePage + ((decActivePage == (int)decActivePage) ? -1 : 0));
        if (_activePage == decActivePage)
        {
            NextButton.Enabled = false;
            NextFastButton.Enabled = false;
            LastButton.Enabled = false;
        }

        // Mostrar el de anterior -5 sólo si estamos pasada la 5
        if (_activePage < 5)
        {
            PreevFastButton.Enabled = false;
        }

        // Mostrar el de siguiente +5 sólo si faltan > 5 páginas para la última
        if (_activePage > _totalFilas / DataGridPageSize - 5)
        {
            NextFastButton.Enabled = false;
        }
    }
    //protected void ClienteDDL_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindGrid();
    //}
    protected void ClienteDDL_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ClienteDDL.Items.Count > 0)
                ClienteDDL.Items[0].Selected = true;
        }
    }
    protected void CasoRadGrid_SortCommand(object sender, GridSortCommandEventArgs e)
    {
        string sortOrder = "";
        string sortExpression = e.SortExpression;
        if (OrderByHF.Value.StartsWith(sortExpression))
        {
            sortOrder = OrderByHF.Value.EndsWith("ASC") ? "ASC" : OrderByHF.Value.EndsWith("DESC") ? "DESC" : "";
            if (sortOrder == "")
            {
                sortOrder = "ASC";
            }
            else if (sortOrder == "ASC")
            {
                sortOrder = "DESC";
            }
            else if (sortOrder == "DESC")
            {
                sortOrder = "";
            }
        }
        else
        {
            sortOrder = "ASC";
        }
        if (sortExpression.Contains(","))
        {
            sortExpression = sortExpression.Replace(",", " " + sortOrder + ",");
        }
        OrderByHF.Value = sortExpression + " " + sortOrder;
        e.Canceled = true;
        BindGrid();
    }
    protected void boton_Click(object sender, EventArgs e)
    {
        GetPermissions(true);
        SearchCasoMedico.Query = this.BuildQuery();

        if (string.IsNullOrEmpty(SearchCasoMedico.Query))
        {
            SearchCasoMedico.Query = "("+ ModeHF.Value + " OR " + ModeHF2.Value+")";
        }
        else
        {
            if (EsOdontologoHF.Value == "0")
            {

                SearchCasoMedico.Query += @" AND (" + ModeHF.Value + " OR " + ModeHF2.Value + ")";

            }
            else
            {
                SearchCasoMedico.Query += @" AND " + "@TipoConsulta ODONTO";

            }
        }
        BindGrid();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportarAExcel();
    }

    private void ExportarAExcel()
    {

        try
        {

            SearchCasoMedico.Query = this.BuildQuery();

            string sqlQuery = SearchCasoMedico.Sql;
            int _clienteId = Convert.ToInt32(ClienteDDL.SelectedValue);


            int firstRow = 0;

            //int.TryParse(MedicoUsuarioId.Value, out medicoId);

            List<Caso> _cache = new List<Caso>();

            Searcher searcher = new Searcher(new CasoMedicoSearch());
            try
            {
                searcher.Query = "";
                Match match = Regex.Match(SearchCasoMedico.Query.ToLower(), "@diagnostico [^@]*");
                if (match.Success && !Regex.IsMatch(SearchCasoMedico.Query.ToLower(), "@enfermedad2"))
                {
                    string enfermedad = match.Value.Replace("@diagnostico ", "").Replace("@", "");
                    searcher.Query = SearchCasoMedico.Query.ToLower().Replace("@diagnostico", "( @diagnostico") +
                        " OR @Enfermedad2 " + enfermedad +
                        " OR @Enfermedad3 " + enfermedad +
                        " OR @DiagnosticoPresuntivo " + enfermedad + " )";
                }
            }
            catch (Exception q)
            {
                log.Error("Error generating the query for CasoBLL.SearchCaso.", q);
            }

            int userId = 0;
            try
            {
                string sUsername = User.Identity.Name;

                Artexacta.App.User.User u = UserBLL.GetUserByUsername(sUsername);

                if (u != null)
                {
                    userId = u.UserId;
                }
            }
            catch (Exception)
            {
                log.Error("can't find the userId from the username at CasoMedicoLista.aspx");
            }


            int _proveedorId = Convert.ToInt32("0" + ProveedorIdHF.Value);
            if (string.IsNullOrWhiteSpace(ClienteDDL.SelectedValue))
            {
                ClienteDDL.DataBind();
            }

            string sql = string.IsNullOrWhiteSpace(searcher.Query) ? SearchCasoMedico.Sql : searcher.SqlQuery();
            CasoBLL.SearchCaso(_cache, DataGridPageSize, firstRow, sql, _clienteId, _proveedorId, OrderByHF.Value);

            DataTable data = this.ConvertListToDataTable(_cache);

            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("CasoMedico");

                /*DataTable datos = Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorFinanciera(
                    FechaInicio.SelectedDate.Value, FechaFin.SelectedDate.Value, cliente, financiera, estudio, tipoProducto, cobroFinanciera);
                */

                ws.Cells["A1"].LoadFromDataTable(data, true);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                //ws.Cells["A1"].LoadFromDataTable(datos, true);
                ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();



                //Format the header for column 1-6
                using (ExcelRange rng = ws.Cells["A1:G1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(153, 153, 153));
                }

                //Example how to Format Column 1 as numeric 
                using (ExcelRange col = ws.Cells[2, 2, 2 + data.Rows.Count - 1, 2])
                {
                    col.Style.Numberformat.Format = "@";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                /*
                string descFechas = FechaInicio.SelectedDate.Value.Month.ToString().PadLeft(2, '0') +
                    FechaInicio.SelectedDate.Value.Day.ToString().PadLeft(2, '0') + "_" +
                    FechaFin.SelectedDate.Value.Month.ToString().PadLeft(2, '0') +
                    FechaFin.SelectedDate.Value.Day.ToString().PadLeft(2, '0');
                */
                //Write it back to the client
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=CasoMedicoReporte.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }
        }
        catch (Exception q)
        {
            log.Error("Error writing response", q);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al obtener el archivo Excel con la información.");
        }
        finally
        {
            Response.Flush();
            Response.End();
        }
    }

    private DataTable ConvertListToDataTable(List<Caso> list)
    {
        DataTable data = new DataTable();

        if (list == null)
            return null;

        if (list.Count <= 0)
            return null;

        //insert columns
        /*
         ESTADO, CODIGO CASO INICIAL, CODIGO CASO ESPECIALISTA, PACIENTE, DERIVADO POR	ESPECIALISTA ASIGNADO, 
         CLIENTE, CIUDAD DE DERIVACION, FECHA DE DERIVACION
        */
        data.Columns.Add("CÓDIGO CASO");
        data.Columns.Add("MÉDICO");
        data.Columns.Add("NRO. POLIZA");
        data.Columns.Add("PACIENTE");
        data.Columns.Add("FECHA REGISTRO");
        data.Columns.Add("CLIENTE");
        data.Columns.Add("CIUDAD");


        foreach (Caso searchResult in list)
        {
            data.Rows.Add(searchResult.CodigoCaso,
                            searchResult.MedicoName.ToUpper(),
                            searchResult.NumeroPoliza,
                            searchResult.NombrePacienteForDisplay,
                            searchResult.FechaCreacion,
                            searchResult.NombreAseguradoraForDisplay,
                            searchResult.CiudadId);
        }


        return data;
    }


    private string BuildQuery()
    {
        string result = "";

        string ciudad = (ciudadComboBox.SelectedItem != null) ? ciudadComboBox.SelectedValue : "";
        if ((ciudadComboBox.SelectedValue).Length > 0 && ciudadComboBox.SelectedItem != null)
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CIUDAD " + ciudad : result + @"@CIUDAD " + ciudad;
            result = result + " ";
        }
        string medico = (medicoComboBox.SelectedValue != null) ? medicoComboBox.SelectedValue : "";
        if ((medicoComboBox.SelectedValue).Length > 0 && medicoComboBox.SelectedItem != null)
        {
            //result = result + "( ";
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MedicoId " + medico : result + @"@MedicoId " + medico;
            result = result + " ";
            //int userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            //result = (!string.IsNullOrEmpty(userId.ToString())) ? result + @" OR " + @"@UserId" + userId.ToString() : result + @"@UserId " + userId.ToString();
            //result = result + " )";
        }
        /*
        string derivacion = derivacionIdText.Text;

        if (!string.IsNullOrEmpty(derivacion))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @"AND " + @"@DERIVACION " + derivacion : result + @"@DERIVACION " + derivacion;
            result = result + " ";
        }*/

        //string medicoDerivador = medicoDerivadorComboBox.SelectedValue;//value is userId
        //if (medicoDerivadorComboBox.SelectedIndex > 0)
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MEDICODERIVADORID " + medicoDerivador : result + @"@MEDICODERIVADORID " + medicoDerivador;
        //    result = result + " ";
        //}

        //if (medicoDerivadoComboBox.Visible)
        //{
        //    string medicoDerivado = medicoDerivadoComboBox.SelectedValue;
        //    if (medicoDerivadoComboBox.SelectedIndex > 0)
        //    {
        //        result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MEDICODERIVADOID " + medicoDerivado : result + @"@MEDICODERIVADOID " + medicoDerivado;
        //        result = result + " ";
        //    }
        //}
        string codigoCasoInicial = codigoCasoIdText.Text;
        if (!string.IsNullOrEmpty(codigoCasoInicial))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CODIGOCASO " + codigoCasoInicial : result + @"@CODIGOCASO " + codigoCasoInicial;
            result = result + " ";
        }

        string pacienteNombre = pacienteNombreText.Text;
        if (!string.IsNullOrEmpty(pacienteNombre))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@NOMBREPACIENTE " + pacienteNombre : result + @"@NOMBREPACIENTE " + pacienteNombre;
            result = result + " ";
        }

        DateTime dtFechaInicial = (FechaInicio.SelectedDate != null) ? FechaInicio.SelectedDate.Value : DateTime.MinValue;
        DateTime dtFechaFinal = (FechaFin.SelectedDate != null) ? FechaFin.SelectedDate.Value : DateTime.MinValue;

        if (FechaInicio.SelectedDate != null && FechaFin.SelectedDate != null)
        {
            dtFechaInicial = dtFechaInicial.AddDays(-1);
            dtFechaFinal = dtFechaFinal.AddDays(+1);
        }

        string fechaInicial = (dtFechaInicial != DateTime.MinValue) ? dtFechaInicial.ToString("yyyy-MM-dd") : "";
        string fechaFinal = (dtFechaFinal != DateTime.MinValue) ? dtFechaFinal.ToString("yyyy-MM-dd") : "";
        if (!string.IsNullOrEmpty(fechaInicial) && !string.IsNullOrEmpty(fechaFinal))
        {
            result += (string.IsNullOrEmpty(result)) ? "" : " AND ";
            result += @" (@FECHACREACION > " + fechaInicial + " AND @FECHACREACION < " + fechaFinal + ")";
        }
        string numeroPoliza = polizaText.Text.Trim();
        if (!string.IsNullOrEmpty(numeroPoliza))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@NumeroPoliza " + numeroPoliza : result + @"@NumeroPoliza " + numeroPoliza;
            result = result + " ";
        }
        return result;
    }
    private void CargarParametrosUser()
    {
        CargarDatosMedicos();
    }
    private void CargarDatosMedicos()
    {
        try
        {
            string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
            string EspecialidadOdontologo = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadOdontologo"];
            string EspecialidadMG = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadMG"];
            int EspecialidadId = EspecialidadBLL.GetEspecialidadxNombre(EspecialidadMG).EspecialidadId;
            int EspecialidadOdontologiaId = EspecialidadBLL.GetEspecialidadxNombre(EspecialidadOdontologo).EspecialidadId;
            int userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
            Medico medico = null;
            medico = MedicoBLL.getMedicoByUserId(userId);
            Artexacta.App.Validacion.BLL.ValidacionBLL obj = new Artexacta.App.Validacion.BLL.ValidacionBLL();
            if (!obj.VerificarRol(RolAdmin))
            {
                if (medico != null)
                {
                    if (medico.EspecialidadId == EspecialidadId)
                    {

                        CargarCiudadUsuario(UserBLL.GetUserById(userId).CiudadId);
                        List<Medico> modifiedList = new List<Medico>();
                        modifiedList.Add(medico);
                        medicoComboBox.DataSource = modifiedList;
                        medicoComboBox.DataValueField = "MedicoId";
                        medicoComboBox.DataTextField = "Nombre";
                        medicoComboBox.DataBind();
                        medicoComboBox.SelectedValue = medico.MedicoId.ToString();
                        medicoComboBox.Enabled = false;
                        Ocultar();
                    }
                    else
                    {
                        if (medico.EspecialidadId == EspecialidadOdontologiaId)
                        {
                            CargarCiudadUsuario(UserBLL.GetUserById(userId).CiudadId);
                            List<Medico> modifiedList = new List<Medico>();
                            modifiedList.Add(medico);
                            medicoComboBox.DataSource = modifiedList;
                            medicoComboBox.DataValueField = "MedicoId";
                            medicoComboBox.DataTextField = "Nombre";
                            medicoComboBox.DataBind();
                            medicoComboBox.SelectedValue = medico.MedicoId.ToString();
                            medicoComboBox.Enabled = false;
                            Ocultar();

                            EsOdontologoHF.Value = "OK";
                        }
                        else
                        {
                            medicoComboBox.Enabled = true;
                            ciudadComboBox.Enabled = true;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }
    private void CargarCiudadUsuario(string CiudadId)
    {
        List<Ciudad> modifiedList = new List<Ciudad>();
        Ciudad ObjCiudad = CiudadBLL.GetCiudadDetails(CiudadId);
        modifiedList.Add(ObjCiudad);
        ciudadComboBox.DataSource = modifiedList;
        ciudadComboBox.DataValueField = "CiudadId";
        ciudadComboBox.DataTextField = "Nombre";
        ciudadComboBox.DataBind();
        ciudadComboBox.SelectedValue = CiudadId;
        ciudadComboBox.Enabled = false;
    }
    private void Ocultar()
    {
        pacienteNombreText.Visible = false;
        LabelNroPoliza.Visible = false;
        LabelPaciente.Visible = false;
        LabelCodCaso.Visible = false;
        polizaText.Visible = false;
        codigoCasoIdText.Visible = false;

    }

    protected void boton2_Click(object sender, EventArgs e)
    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('hola mundo');", true);
        //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "{some text for type}", "alert('{Text come to here}'); ", true);
        //Page.Header.Title = DateTime.Now.ToString();
        //         Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('FiveDot File uploaded successfully'); alert('TwoDot File uploaded successfully');", true);
        //  Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('FiveDot File uploaded successfully')</script> ;",true);
        Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('FiveDot File uploaded successfully'); alert('TwoDot File uploaded successfully');", true);

        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        ///  ScriptManager.RegisterClientScriptBlock(this, GetType(), "Sc", "ShowDialog();", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Sc", "jqcall();", true);
        //string Message = "Your Support Reference No. is : <b> " + "hola" + " </b> which has been sent on your email. Kindly check the status of your request within next 5 days.";
        //string str_alert_Msg = @"swal({
        //                    type: 'success',
        //                    title:'Good job!',
        //                    text: '" + Message + @"',                                                                 
        //                    html: true
        //                });";
        //        string script = @"<script type='text/javascript'>
        //        var seleccion=confirm('hola mundo ¿ ok?');
        //        if (seleccion==false)
        //            {
        //        $(':text').val('');
        //        }
        //</script>";
        // Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('FiveDot File uploaded successfully'); alert('"+str_alert_Msg+"');", true);
        //  ScriptManager.RegisterStartupScript(this, typeof(Page), "confirm", script, true);
        //   Page.ClientScript.RegisterStartupScript(GetType(), "confirm", script, true);
        //
        //   Page.ClientScript.RegisterStartupScript(GetType(), "Success", "alert('FiveDot File uploaded successfully'); alert('"+ str_alert_Msg + "');"  , true);
    }

}