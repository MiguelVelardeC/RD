using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.CoPagos.BLL;
using Artexacta.App.Caso;
using Artexacta.App.Caso.BLL;
using Artexacta.App.CoPagos;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.TipoProveedor.BLL;
using Artexacta.App.TipoProveedor;
using Artexacta.App.Proveedor;
using Artexacta.App.Proveedor.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using System.Web;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Paciente;
using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.User;
using Artexacta.App.Medico;
using Artexacta.App.Medico.BLL;
using System.Web.UI.WebControls;
using Artexacta.App.Siniestralidad.BLL;
using Artexacta.App.Siniestralidad;
using Artexacta.App.RedClientePrestaciones.BLL;
using Artexacta.App.RedClientePrestaciones;
using Artexacta.App.Validacion.BLL;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Medico;
using Artexacta.App.Especialidad.BLL;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;
using System.IO;
using Artexacta.App.Security.BLL;
using Artexacta.App.Caso;

public partial class CasoMedico_CopagoLista : Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private List<string> userPermissions;
    private static Bitacora theBitacora = new Bitacora();
    private int DataGridPageSize = 20;
    protected void Page_Load(object sender, EventArgs e)
    {

        SearchCasoMedico.OnSearch +=
         new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchCasoMedico.Config = new CasoMedicoSearch();

        if (IsPostBack)
        {

            return;

        }

        CargarFechaActual();
        CargarParametros();



    }
    private void CargarFechaActual()
    {

        FECHAINICIOCOPAGOS.SelectedDate = DateTime.Now;
        FECHAFINALCOPAGOS.SelectedDate = DateTime.Now;
    }

    private void LoadCiudadesComboBox()
    {
        try
        {
            List<Ciudad> list = CiudadBLL.getCiudadList();
            List<Ciudad> modifiedList = new List<Ciudad>();
            modifiedList.Add(new Ciudad()
            {
                CiudadId = "TTT",
                Nombre = "Todos"
            });
            foreach (Ciudad ciudad in list)
            {
                modifiedList.Add(ciudad);
            }

            ciudadComboBox.DataSource = modifiedList;
            ciudadComboBox.DataValueField = "CiudadId";
            ciudadComboBox.DataTextField = "Nombre";
            ciudadComboBox.DataBind();
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Ciudad de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Ciudades");
        }
    }

    private void LoadClientesToCombo(int medicoId = 0)
    {

        List<GenericComboContainer> clientes = null;
        try
        {

            clientes = DerivacionBLL.GetClientesByMedicoIdCombo(medicoId);


        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos del Cliente de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Cliente");
        }

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


        if (clientes != null)
        {
            clientesComboBox.DataSource = clientes;
            clientesComboBox.DataValueField = "ContainerId";
            clientesComboBox.DataTextField = "ContainerName";

            CNClienteRadComboBox.DataSource = clientes;
            CNClienteRadComboBox.DataValueField = "ContainerId";
            CNClienteRadComboBox.DataTextField = "ContainerName";
            CNClienteRadComboBox.DataBind();
            clientesComboBox.DataBind();

        }
    }

    private void LoadProveedorToCombo()
    {
        try
        {
            List<Proveedor> list = ProveedorBLL.GetProveedorActivo();
            List<Proveedor> modifiedList = new List<Proveedor>();
            modifiedList.Add(new Proveedor()
            {
                ProveedorId = 0,
                NombreJuridico = "Todos"
            });
            foreach (Proveedor proveedor in list)
            {
                modifiedList.Add(proveedor);
            }

            ProveedorComboBox.DataSource = modifiedList;
            ProveedorComboBox.DataValueField = "ProveedorId";
            ProveedorComboBox.DataTextField = "NombreJuridico";
            ProveedorComboBox.DataBind();
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Proveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Proveedor");
        }
    }
    private void LoadTipoProveedorToCombo()
    {
        try
        {
            List<TipoProveedor> list = TipoProveedorBLL.getTipoProveedorList();
            List<TipoProveedor> modifiedList = new List<TipoProveedor>();
            modifiedList.Add(new TipoProveedor()
            {
                TipoProveedorId = "0",
                Nombre = "Todos"
            });
            foreach (TipoProveedor tipoproveedor in list)
            {
                modifiedList.Add(tipoproveedor);
            }

            TipoProveedorComboBox.DataSource = modifiedList;
            TipoProveedorComboBox.DataValueField = "TipoProveedorId";
            TipoProveedorComboBox.DataTextField = "Nombre";
            TipoProveedorComboBox.DataBind();
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de TipoProveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga los datos TipoProveedor");
        }
    }


    private void LoadProveedorToComboCiudadTipoProveedor()
    {
        try
        {
            List<Proveedor> modifiedList = new List<Proveedor>();
            if ((TipoProveedorComboBox.SelectedValue == "0" && ciudadComboBox.SelectedValue != "TTT")
                | (TipoProveedorComboBox.SelectedValue != "0" && ciudadComboBox.SelectedValue != "TTT")
                | (TipoProveedorComboBox.SelectedValue == "0" && ciudadComboBox.SelectedValue == "TTT")
                | (TipoProveedorComboBox.SelectedValue != "0" && ciudadComboBox.SelectedValue == "TTT")
                )
            {
                List<Proveedor> list = ProveedorBLL.getProveedorByTipoProveedorAndCiudadIdNull(TipoProveedorComboBox.SelectedValue, ciudadComboBox.SelectedValue);

                modifiedList.Add(new Proveedor()
                {
                    ProveedorId = 0,
                    NombreJuridico = "Todos"
                });
                foreach (Proveedor proveedor in list)
                {
                    modifiedList.Add(proveedor);
                }

                ProveedorComboBox.DataSource = modifiedList;
                ProveedorComboBox.DataValueField = "ProveedorId";
                ProveedorComboBox.DataTextField = "NombreJuridico";
                ProveedorComboBox.DataBind();
            }
            else
            {

                ProveedorComboBox.Items.Clear();
                modifiedList.Add(new Proveedor()
                {
                    ProveedorId = 0,
                    NombreJuridico = "Todos"
                });
                ProveedorComboBox.DataSource = modifiedList;
                ProveedorComboBox.DataValueField = "ProveedorId";
                ProveedorComboBox.DataTextField = "NombreJuridico";
                ProveedorComboBox.DataBind();
            }
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Proveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Proveedor");
        }
    }

    private void LoadMedicoToComboCiudadCliente()
    {
        try
        {
            List<Medico> modifiedList = new List<Medico>();
            if ((clientesComboBox.SelectedValue == "0" && ciudadComboBox.SelectedValue != "TTT")
                | (clientesComboBox.SelectedValue != "0" && ciudadComboBox.SelectedValue != "TTT")
                | (clientesComboBox.SelectedValue == "0" && ciudadComboBox.SelectedValue == "TTT")
                | (clientesComboBox.SelectedValue != "0" && ciudadComboBox.SelectedValue == "TTT")
                )
            {
                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                string RolRecepcionista = System.Web.Configuration.WebConfigurationManager.AppSettings["RolRecepcionista"];
                string RolMedicoGeneral = System.Web.Configuration.WebConfigurationManager.AppSettings["RolMedicoGeneral"];
                string PermisoExportarMatrizNacional = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoExportarMatrizNacional"];
                userPermissions = SecurityBLL.GetUserPermissions();
                if (!userPermissions.Contains(PermisoExportarMatrizNacional))
                {
                    ExportToExcelMatrizNacional.Visible = false;
                    realExportMatrizNacional.Visible = false;
                }

                ValidacionBLL obj = new ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {
                    CargarMedicosxCliente();
                }
                else
                {
                    if (obj.VerificarRol(RolRecepcionista))
                    {
                        CargarMedicosxCliente();
                    }
                    else
                    {
                        if (obj.VerificarRol(RolMedicoGeneral))
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Medico de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Medico");
        }
    }
    private void CargarMedicosxCliente()
    {
        List<Medico> modifiedList = new List<Medico>();
        string EspecialidadMG = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadMG"];
        int EspecialidadId = EspecialidadBLL.GetEspecialidadxNombre(EspecialidadMG).EspecialidadId;
        List<Medico> list = new List<Medico>();
        string ciudad = ciudadComboBox.SelectedValue;
        if (ciudadComboBox.SelectedValue.Contains("TTT"))
            ciudad = "";
        MedicoBLL.GetALLMedico(list, ciudad, Convert.ToInt32(clientesComboBox.SelectedValue), EspecialidadId);

        modifiedList.Add(new Medico()
        {
            MedicoId = 0,
            Nombre = "Todos"
        });
        foreach (Medico medico in list)
        {
            modifiedList.Add(medico);
        }

        MedicoComboBox.DataSource = modifiedList;
        MedicoComboBox.DataValueField = "MedicoId";
        MedicoComboBox.DataTextField = "Nombre";
        MedicoComboBox.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        this.PrestacionesCoPagoGrid.Visible = true;
        ActivePageHF.Value = "0";
        if (Convert.ToInt32(MedicoIdHF.Value) > 0)
            BindGridMedico();
        else
            BindGrid();

    }



    protected void CiudadComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProveedorToComboCiudadTipoProveedor();
        LoadMedicoToComboCiudadCliente();
    }
    protected void ClienteRadComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProveedorToComboCiudadTipoProveedor();
        LoadMedicoToComboCiudadCliente();
    }

    protected void TipoProveedorComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProveedorToComboCiudadTipoProveedor();

    }
    protected void PrestacioneCoPagoGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

        if (e.CommandName == "Select")
        {

            Session["TipoCaso"] = e.CommandArgument;
            Response.Redirect("~/CoPagos/CoPagoDetail.aspx");

        }
        if (e.CommandName == "Delete")
        {
            char separador = ';'; // separador de datos
            string Cadena = e.CommandArgument.ToString();
            string[] arregloDeSubCadenas = Cadena.Split(separador);


            if (arregloDeSubCadenas[0].Contains("CASO_LABORATORIOSIMAGENOLOGIA"))
            {
                try
                {
                    int OrdenDeServicioId = int.Parse(arregloDeSubCadenas[1].ToString());
                    CoPagosBLL.DeleteLaboratorioImagenologia(OrdenDeServicioId);
                   // SystemMessages.DisplaySystemMessage("Se elimino correctamente el CoPago.");
                }
                catch (Exception)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error, No se puedo elimiar el CoPago.");
                    return;
                }
                BindGrid();
            }
            if (arregloDeSubCadenas[0].Contains("CASO_ESPECIALIDAD"))
            {
                try
                {
                    int detid = int.Parse(arregloDeSubCadenas[1].ToString());
                    CoPagosBLL.DeleteEspecialidad(detid);
                   // SystemMessages.DisplaySystemMessage("Se elimino correctamente el CoPago.");
                }
                catch (Exception)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error, No se puedo elimiar el CoPago.");
                    return;
                }
                BindGrid();

            }
            if (arregloDeSubCadenas[0].Contains("CASO_ODONTOLOGIA"))
            {
                try { 
                int detid = int.Parse(arregloDeSubCadenas[1].ToString());
                CoPagosBLL.DeleteOdontologia(detid);
                }
                catch (Exception)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error, No se puedo elimiar el CoPago.");
                    return;
                }
               
            }


            
        }


    }
    protected void PrestacioneCoPagoGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            string estado = item["Estado1"].Text;

            GridDataItem da = e.Item as GridDataItem;
            string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];

            ImageButton ImgBtn = (ImageButton)item["TemplateColumnEliminar"].FindControl("DeleteImageButton");

            ValidacionBLL obj = new ValidacionBLL();
            if (obj.VerificarRol(RolAdmin))
            {
                string PermisosEliminarCoPagos = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoEliminarCoPagos"];
                userPermissions = SecurityBLL.GetUserPermissions();
                if (userPermissions.Contains(PermisosEliminarCoPagos))
                {
                    //    if (estado.Contains("NO COBRADO"))
                    //    {
                    ImgBtn.Visible = true;
                }//    }
                  else
                {

                    ImgBtn.Visible = false;
                }
            }
            else
            {
                ImgBtn.Visible = false;
            }
        }




    }

    protected void PrestacioneCoPagoGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
    }
    private void BindGrid()
    {
        // En esta variable almacenamos el total de filas en la base de datos.  No sólo las cargadas.
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los Prestaciones leidos aquí. 
        List<CoPagos> _cache = new List<CoPagos>();
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
            //aqui controlamos lo queremos buscar
            int estado;
            string dciudad;
            int dcliente;
            string dtipoPro;
            int prove;
            string caso;
            string nombre;
            string carnet;
            DateTime fechaini;
            DateTime fechafin;
            if (ciudadComboBox.SelectedValue == "TTT")
                dciudad = "";

            else
                dciudad = ciudadComboBox.SelectedValue;
            if (clientesComboBox.SelectedValue == "0")
                dcliente = 0;
            else
                dcliente = int.Parse(clientesComboBox.SelectedValue.ToString());
            if (TipoProveedorComboBox.SelectedValue == "0")
                dtipoPro = "";
            else
                dtipoPro = TipoProveedorComboBox.SelectedValue;
            if (ProveedorComboBox.SelectedValue == "0")
                prove = 0;
            else
                prove = int.Parse(ProveedorComboBox.SelectedValue.ToString());

            estado = int.Parse(estadoComboBox.SelectedValue);

            if (codigoCasoIdText.Text == "" || codigoCasoIdText.Text == " ")
            {
                caso = "";
            }
            else
            {
                caso = (codigoCasoIdText.Text);
            }
            if (NombreText.Text == null || NombreText.Text == "")
                nombre = "";
            else
                nombre = this.NombreText.Text.ToString();
            if (CIText.Text == null || CIText.Text == "")
                carnet = "";
            else
                carnet = this.CIText.DisplayText.ToString();
            if (FECHAINICIOCOPAGOS.SelectedDate == null || FECHAINICIOCOPAGOS.SelectedDate.ToString() == "")
                fechaini = Convert.ToDateTime("01/01/2000");
            else
                fechaini = Convert.ToDateTime(FECHAINICIOCOPAGOS.SelectedDate);

            if (FECHAFINALCOPAGOS.SelectedDate == null || FECHAFINALCOPAGOS.SelectedDate.ToString() == "")
                fechafin = Convert.ToDateTime("01/01/3000");
            else
                fechafin = Convert.ToDateTime(FECHAFINALCOPAGOS.SelectedDate);



            _totalRows = CoPagosBLL.GetPrestacionesCoPagoALL(_cache, dciudad, dcliente, dtipoPro, prove, Convert.ToInt32(MedicoComboBox.SelectedValue), estado, caso, nombre, carnet, fechaini, fechafin, DataGridPageSize, firstRow);


        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);

            int userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
            Medico medico = medico = MedicoBLL.getMedicoByUserId(userId);
            string RolRecepcionista = System.Web.Configuration.WebConfigurationManager.AppSettings["RolRecepcionista"];
            ValidacionBLL obj = new ValidacionBLL();
            if (medico != null && !obj.VerificarRol(RolRecepcionista))
            {
                SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Copagos Pendientes Cobro Lista");
            }            
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
            //  PrimeraFilaCargadaHF.Value = (_cache[0].RowNumber - 1).ToString();
            //  UltimaFilaCargadaHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
            //
        }

        // Registrar el total de filas leidas
        TotalFilasHF.Value = _totalRows.ToString();


        // Actualizar el estado de habilitado los botones de navegación

        PrestacionesCoPagoGrid.DataSource = _cache;
        PrestacionesCoPagoGrid.DataBind();
        ChangeButtonStates();

    }
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
        //PreevFastButton
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
    protected void CoPagoLRadGrid_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e)
    {

    }

    protected void export_Click(object sender, EventArgs e)
    {
        try
        {
            ///  RadAjaxManager1.EnableAJAX = false;

            if (Convert.ToInt32(MedicoIdHF.Value) > 0)
                ExportarCoPagosMedicoGeneral();
            else
                ExportarCoPagos();

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }

    protected void export_Matriz_Nacional_Click(object sender, EventArgs e)
    {
        try
        {
            ///  RadAjaxManager1.EnableAJAX = false;
            ExportarCoPagosMatrizNacional();

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }

    protected void ciudadComboBox_TextChanged(object sender, EventArgs e)
    {
        LoadProveedorToComboCiudadTipoProveedor();

    }

    protected void TipoProveedorComboBox_TextChanged(object sender, EventArgs e)
    {
        LoadProveedorToComboCiudadTipoProveedor();

    }
    protected void CancelNewCaso_Click(object sender, EventArgs e)
    {
        LimpiarAlCancelar();
        BindGrid();
    }
    private void LimpiarAlCancelar()
    {
        this.TextCIValidar.Text = "";
        this.LabelNombrePAciente.Text = "";
        // this.LabelPacienteId.Text = "";
        this.MedicoRadCombo.Items.Clear();
        //  this.MedicoRadCombo.Items.Add(new ListItem("Seleccione un Medico", "0"));
        this.ListaDePolizaDropDown.Items.Clear();
        // this.ListaDePolizaDropDown.Items.Add(new ListItem("Seleccione un Cliente", "0"));
        LabelMensajeErrorNewCaso.Text = "";
        this.PacienteDropDownList.Items.Clear();
        //   this.PacienteDropDownList.Items.Add(new ListItem("Seleccione un Paciente", "0"));
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

        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void BuscarPaciente_Onclick(object sender, EventArgs e)

    {
        try
        {


            if (!string.IsNullOrEmpty(TextCIValidar.Text))
            {
                SearchCasoMedico.Query = this.BuildQuery();
                string sql = SearchCasoMedico.Sql;
                LoadPaciente(sql);
            }
            else
            {
                LabelMensajeErrorNewCaso.Text = "Debe introducir un numero de documento de identidad.";
            }


        }
        catch (Exception ex)
        {
            log.Error("Error de Coneccion de la Base de Datos", ex);
        }

    }
    private void LoadPaciente(string search)
    {
        try
        {
            PacienteDropDownList.ClearSelection();
            List<Paciente> SearchPaciente = null;
            ValidacionBLL obj = new ValidacionBLL();
            string a = CNClienteRadComboBox.Text;
            string a1 = CNClienteRadComboBox.SelectedValue;
            if (obj.VerificarRol("Medico General"))
            {
                SearchPaciente = PacienteBLL.GetAllPacienteSearch(search, Convert.ToInt32(MedicoIdHF.Value), Convert.ToInt32(CNClienteRadComboBox.SelectedValue));

            }
            else
            {
                SearchPaciente = PacienteBLL.GetAllPacienteSearch(search, 0, Convert.ToInt32(CNClienteRadComboBox.SelectedValue));

            }

            List<Paciente> modifiedList = new List<Paciente>();
            modifiedList.Add(new Paciente()
            {
                PacienteId = 0,
                Nombre = "Seleccione Un Paciente"
            });
            foreach (Paciente Paciente in SearchPaciente)
            {
                modifiedList.Add(Paciente);
            }

            PacienteDropDownList.DataSource = modifiedList;
            PacienteDropDownList.DataValueField = "PacienteId";
            PacienteDropDownList.DataTextField = "Nombre";
            PacienteDropDownList.DataBind();
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de los Pacientes de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Pacientes");
        }
    }
    protected void PacienteDropDownList_OnClientSelectedIndexChanged(object o, EventArgs e)
    {
        if (Convert.ToInt32(PacienteDropDownList.SelectedValue) > 0)
        {
            LoadPolizaDelPaciente(Convert.ToInt32(PacienteDropDownList.SelectedValue));
            LabelMensajeErrorNewCaso.Visible = false;
            //  LabelPacienteId.Text = PacienteDropDownList.SelectedValue;
        }
        else
        {
            LabelMensajeErrorNewCaso.Visible = true;
            LabelMensajeErrorNewCaso.Text = "Realize Una Busqueda Primero de Algun Paciente";
        }
    }


    //Esto Me Carga Todos los usuarios Medicos segun la Ciudad Donde se logue el que va a registrar
    private void LoadMedicoGeneral(string CiudadId, int clienteId)
    {
        try
        {
            if (clienteId > 0)
            {
                List<Medico> _cache = new List<Medico>();
                int EspecialidadId = EspecialidadBLL.GetEspecialidadxNombre("MEDICO GENERAL").EspecialidadId;
                int cant = MedicoBLL.GetALLMedico(_cache, CiudadId, clienteId, EspecialidadId);
                List<Medico> modifiedList = new List<Medico>();
                modifiedList.Add(new Medico()
                {
                    UserId = 0,
                    Nombre = "Eliga Un Medico"
                });
                foreach (Medico medico in _cache)
                {
                    modifiedList.Add(medico);
                }

                MedicoRadCombo.DataSource = modifiedList;
                MedicoRadCombo.DataValueField = "userId";
                MedicoRadCombo.DataTextField = "Nombre";
                MedicoRadCombo.DataBind();
                LabelMensajeErrorNewCaso.Visible = false;
                if (Convert.ToInt32(MedicoIdHF.Value) > 0)
                {
                    string UserName = HttpContext.Current.User.Identity.Name;
                    User objUser = UserBLL.GetUserByUsername(UserName);
                    MedicoRadCombo.SelectedValue = objUser.UserId.ToString();
                    MedicoRadCombo.Enabled = false;
                }
                else
                {

                }
            }
            else
            {
                LabelMensajeErrorNewCaso.Visible = true;
                LabelMensajeErrorNewCaso.Text = "Seleccione Un Paciente Primero";
            }
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de los Medicos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Medico");
        }
    }
    //Esto Me carga Las Polizas que puede Tener un Paciente

    private void LoadPolizaDelPaciente(int PacienteId)
    {
        try
        {
            if (PacienteId > 0)
            {
                List<Poliza> list = PolizaBLL.GetPolizaByPacienteId(PacienteId);
                List<Poliza> modifiedList = new List<Poliza>();
                modifiedList.Add(new Poliza()
                {
                    PolizaId = 0,
                    NombreJuridicoCliente = "Eliga un Cliente"
                });
                foreach (Poliza Poliza in list)
                {
                    modifiedList.Add(Poliza);
                }

                ListaDePolizaDropDown.DataSource = modifiedList;
                ListaDePolizaDropDown.DataValueField = "PolizaId";
                ListaDePolizaDropDown.DataTextField = "NombreJuridicoCliente";
                ListaDePolizaDropDown.DataBind();
            }
            else
            {
                LabelMensajeErrorNewCaso.Visible = true;
                LabelMensajeErrorNewCaso.Text = "Seleccione un Paciente Primero";
            }
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la Poliza de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Polizas");
        }
    }

    //Con el Nuevo Pop Pup Formulario
    protected void BtnNewCaso_Click(object sender, EventArgs e)
    {
        // loadAddEstudioForm();
        CreacionEstudioWindow.VisibleOnPageLoad = true;
        CargarParametros();
    }
    protected void BtnCrearCaso_Onclick(object sender, EventArgs e)
    {

        int dato = 0;
        string datocadena = "";
        try
        {
            // Obtener los datos de la BD
            List<PolizaValidation> ListaDeDatosPaciente = PolizaBLL.GetAllPolizaValidation(int.Parse(ListaDePolizaDropDown.SelectedValue), datocadena, dato, dato, dato, datocadena, TextCIValidar.Text);
            //aqui controlamos lo queremos buscar
            ValidacionBLL objeto = new ValidacionBLL();
            string Respuesta = ValidacionBLL.ValidacionDePrestacion(ListaDeDatosPaciente[0].PolizaId, ListaDeDatosPaciente[0].ClienteId, "MG", "");
            if (Respuesta == "")
            {
                //aqui agregamos el caso Dirty
                int userId = int.Parse(MedicoRadCombo.SelectedValue);
                int ClienteId = ListaDeDatosPaciente[0].ClienteId;
                string MotivodeConsulta = System.Web.Configuration.WebConfigurationManager.AppSettings["MotivoConsultaMG"];
                int CasoId = InsertNewCasoDirty(
                    int.Parse(ListaDePolizaDropDown.SelectedValue)
                    , int.Parse(PacienteDropDownList.SelectedValue), MotivodeConsulta, userId);
                if (CasoId <= 0)
                {
                    LabelMensajeErrorNewCaso.Visible = true;
                    LabelMensajeErrorNewCaso.Text = "Error al Crear el Caso";

                }

                else
                {
                    CasoEspecialidad ObjcasoMedicoGeneral = new CasoEspecialidad();
                    ObjcasoMedicoGeneral.MedicoId = MedicoBLL.getMedicoByUserId(userId).MedicoId;
                    ObjcasoMedicoGeneral.CasoId = CasoId;
                    //medico General
                    string EspecialidadMG = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadMG"];
                    ObjcasoMedicoGeneral.EspecialidadId = EspecialidadBLL.GetEspecialidadxNombre(EspecialidadMG).EspecialidadId;
                    ObjcasoMedicoGeneral.detCoPagoMonto = BusquedaDeValorCoPagoMG("Monto", ClienteId);
                    ObjcasoMedicoGeneral.detPrecio = BusquedaPrecioMG(ClienteId);
                    ObjcasoMedicoGeneral.detCoPagoPorcentaje = BusquedaDeValorCoPagoMG("Porcentaje", ClienteId);
                    ObjcasoMedicoGeneral.Fecha = DateTime.Now;

                    CoPagosBLL.InsertCasoEspecialidadMedicoGeneral(ObjcasoMedicoGeneral);
                    this.PrestacionesCoPagoGrid.Visible = true;
                    ActivePageHF.Value = "0";
                    if (Convert.ToInt32(MedicoIdHF.Value) > 0)
                    {
                        BindGridMedico();
                    }
                    else
                    {
                        BindGrid();
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
               "loquesea",
               "alert('SE HA CREADO CORRECTAMENTE EL CASO PARA EL PACIENTE " + PacienteDropDownList.Text + "');",
               true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);

                    CreacionEstudioWindow.VisibleOnPageLoad = false;

                    return;
                }
            }

            else
            {
                LabelMensajeErrorNewCaso.Visible = true;
                LabelMensajeErrorNewCaso.Text = "ERROR DE " + Respuesta + " DE LA :" + ListaDePolizaDropDown.Text;
            }
        }
        catch (Exception q)
        {
            LabelMensajeErrorNewCaso.Visible = true;
            LabelMensajeErrorNewCaso.Text = "ERROR DE BASE AL CREAR EL CASO Cargue Bien Los Datos ";
        }
    }
    private decimal BusquedaDeValorCoPagoMG(string TipodePago, int clienteId)
    {
        decimal CopagoValor = 0;
        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(clienteId);
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion == "MG")
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
    private decimal BusquedaPrecioMG(int clienteId)
    {
        decimal CopagoValor = 0;
        List<RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(clienteId);
        for (int i = 0; i < ListaPrestaciones.Count; i++)
        {
            if (ListaPrestaciones[i].TipoPrestacion == "MG")
            {

                string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].Precio * 100) / 100));
                CopagoValor = decimal.Parse(ValorCoPago);
                break;

            }

        }
        return CopagoValor;
    }
    //Insertar Nuevo Caso Dirty
    protected int InsertNewCasoDirty(int PolizaId, int PacienteId, string MoticoConsultaId, int Iduser)
    {
        int CasoId = 0;
        try
        {
            //string UserName = HttpContext.Current.User.Identity.Name;
            //User objUser = UserBLL.GetUserByUsername(UserName);//CIUDAD DEL USUARIO
            User objUser = UserBLL.GetUserById(Iduser);//CIUDAD DEL MEDICO GENERAL
            string CiudadId = objUser.CiudadId;
            Caso objCaso = new Caso();
            objCaso.Correlativo = 0;
            objCaso.CiudadId = CiudadId;
            objCaso.UserId = Iduser;
            objCaso.PolizaId = PolizaId;
            objCaso.MotivoConsultaId = MoticoConsultaId;
            objCaso.Estado = "Abierto";//Abierto//Cerrado
            objCaso.Dirty = true;
            objCaso.PacienteId = PacienteId;

            CasoId = CasoBLL.InsertCasoRecordDirty(objCaso);
        }
        catch (Exception ex)
        {
            log.Error("Error to insert new CasoDirty", ex);
        }
        return CasoId;
    }
    private void CargarParametros()
    {
        try
        {

            string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
            string RolRecepcionista = System.Web.Configuration.WebConfigurationManager.AppSettings["RolRecepcionista"];
            string RolMedicoGeneral = System.Web.Configuration.WebConfigurationManager.AppSettings["RolMedicoGeneral"];
            string PermisoExportarMatrizNacional = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoExportarMatrizNacional"];
            userPermissions = SecurityBLL.GetUserPermissions();
            if (!userPermissions.Contains(PermisoExportarMatrizNacional))
            {
                ExportToExcelMatrizNacional.Visible = false;
                realExportMatrizNacional.Visible = false;
            }

            ValidacionBLL obj = new ValidacionBLL();
            if (obj.VerificarRol(RolAdmin))
            {
                CargarDatosAdmin();
            }
            else
            {
                if (obj.VerificarRol(RolRecepcionista))
                {
                    CargarDatosRecepcionista();
                }
                else
                {
                    if (obj.VerificarRol(RolMedicoGeneral))
                    {
                        CargarDatosMedicoGeneral();
                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception)
        {

        }

    }
    private void CargarParametrosNewCaso()
    {
        try
        {

            string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
            string RolRecepcionista = System.Web.Configuration.WebConfigurationManager.AppSettings["RolRecepcionista"];
            string RolMedicoGeneral = System.Web.Configuration.WebConfigurationManager.AppSettings["RolMedicoGeneral"];
            string PermisoExportarMatrizNacional = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoExportarMatrizNacional"];
            userPermissions = SecurityBLL.GetUserPermissions();
            if (!userPermissions.Contains(PermisoExportarMatrizNacional))
            {
                ExportToExcelMatrizNacional.Visible = false;
                realExportMatrizNacional.Visible = false;
            }

            ValidacionBLL obj = new ValidacionBLL();
            if (obj.VerificarRol(RolAdmin))
            {
                CargarDatosAdmin();
            }
            else
            {
                if (obj.VerificarRol(RolRecepcionista))
                {
                    CargarDatosRecepcionista();
                }
                else
                {
                    if (obj.VerificarRol(RolMedicoGeneral))
                    {
                        CargarDatosMedicoGeneral();
                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception)
        {

        }

    }
    private void CargarDatosNoAdmin()
    {
        LabelCI.Visible = false;
        LabelCodigoCaso.Visible = false;
        LabelNombre.Visible = false;
        codigoCasoIdText.Visible = false;
        NombreText.Visible = false;
        CIText.Visible = false;

    }
    private void CargarDatosAdmin()
    {
        LoadCiudadesComboBox();
        LoadClientesToCombo();
        LoadTipoProveedorToCombo();
        LoadProveedorToComboCiudadTipoProveedor();
        LoadMedicoToComboCiudadCliente();
        BindGrid();
    }


    private void CargarDatosRecepcionista()
    {
        string UserName = HttpContext.Current.User.Identity.Name;
        User objUser = UserBLL.GetUserByUsername(UserName);
        int userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
        Medico medico = medico = MedicoBLL.getMedicoByUserId(userId);
        CargarCiudadUsuario(objUser.CiudadId);
        LoadTipoProveedorToCombo();
        if(medico != null)
        {
            LoadClientesToCombo(medico.MedicoId);
        }else
        {
            LabelAlerta.Text = "Su usuario no tiene asignado el rol de médico y por lo tanto no tiene clientes.";
            LabelAlerta.Visible = true;
        }        
        LoadMedicoToComboCiudadCliente();
        LoadProveedorToComboCiudadTipoProveedor();
        BindGrid();
    }
    private void CargarDatosMedicoGeneral()
    {
        try
        {
            string UserName = HttpContext.Current.User.Identity.Name;
            User objUser = UserBLL.GetUserByUsername(UserName);
            int userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
            Medico medico = medico = MedicoBLL.getMedicoByUserId(userId);
            MedicoIdHF.Value = medico.MedicoId.ToString();

            CargarCiudadUsuario(objUser.CiudadId);
            LoadProveedorMedico(medico.MedicoId, objUser.FullName);
            LoadClientesToComboxMedico(medico.MedicoId);
            LoadTipoProveedorMedicoToCombo();
            LabelCodigoCaso.Visible = false;
            codigoCasoIdText.Visible = false;
            ProveedorComboBox.Visible = false;
            LabelProveedor.Visible = false;
            BindGridMedico();

        }
        catch (Exception)
        {

        }
    }
    private void LoadClientesToComboxMedico(int MedicoId)
    {

        List<GenericComboContainer> clientes = null;
        try
        {

            clientes = DerivacionBLL.GetClientesByMedicoIdCombo(MedicoId);

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos del Cliente de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de BD de los Clientes del Usuario");
        }

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


        if (clientes != null)
        {
            clientesComboBox.DataSource = clientes;
            clientesComboBox.DataValueField = "ContainerId";
            clientesComboBox.DataTextField = "ContainerName";
            CNClienteRadComboBox.DataSource = clientes;
            CNClienteRadComboBox.DataValueField = "ContainerId";
            CNClienteRadComboBox.DataTextField = "ContainerName";
            clientesComboBox.DataBind();
            CNClienteRadComboBox.DataBind();
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
    private void LoadTipoProveedorMedicoToCombo()
    {
        try
        {
            List<TipoProveedor> list = TipoProveedorBLL.getTipoProveedorList();
            List<TipoProveedor> modifiedList = new List<TipoProveedor>();

            foreach (TipoProveedor tipoproveedor in list)
            {
                modifiedList.Add(tipoproveedor);
            }

            TipoProveedorComboBox.DataSource = modifiedList;
            TipoProveedorComboBox.DataValueField = "TipoProveedorId";
            TipoProveedorComboBox.DataTextField = "Nombre";
            TipoProveedorComboBox.DataBind();
            TipoProveedorComboBox.SelectedValue = "MEDICO";
            TipoProveedorComboBox.Enabled = false;
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de TipoProveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga los datos TipoProveedor");
        }
    }

    private void LoadProveedorMedico(int MedicoId, string Nombre)
    {
        try
        {
            Medico objMedico = new Medico();
            objMedico.MedicoId = MedicoId;
            objMedico.Nombre = Nombre;
            List<Medico> modifiedList = new List<Medico>();
            modifiedList.Add(objMedico);
            MedicoComboBox.DataSource = modifiedList;
            MedicoComboBox.DataValueField = "MedicoId";
            MedicoComboBox.DataTextField = "Nombre";
            MedicoComboBox.DataBind();
            MedicoComboBox.SelectedValue = objMedico.MedicoId.ToString();
            MedicoComboBox.Enabled = false;
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Proveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Proveedor");
        }
    }

    private void BindGridMedico()
    {
        // En esta variable almacenamos el total de filas en la base de datos.  No sólo las cargadas.
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los Prestaciones leidos aquí. 
        List<CoPagos> _cache = new List<CoPagos>();
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
            //aqui controlamos lo queremos buscar

            int ClienteId;
            string nombre;
            string carnet;
            DateTime fechaini;
            DateTime fechafin;
            int estado = int.Parse(estadoComboBox.SelectedValue);
            string ciudadId = ciudadComboBox.SelectedValue;
            string TipoProveedorId = TipoProveedorComboBox.SelectedValue;
            int MedicoId = Convert.ToInt32(MedicoComboBox.SelectedValue);
            if (clientesComboBox.SelectedValue == "0")
                ClienteId = 0;
            else
                ClienteId = int.Parse(clientesComboBox.SelectedValue.ToString());
            if (NombreText.Text == null || NombreText.Text == "")
                nombre = "";
            else
                nombre = this.NombreText.Text.ToString();
            if (CIText.Text == null || CIText.Text == "")
                carnet = "";
            else
                carnet = this.CIText.DisplayText.ToString();
            if (FECHAINICIOCOPAGOS.SelectedDate == null || FECHAINICIOCOPAGOS.SelectedDate.ToString() == "")
                fechaini = Convert.ToDateTime("01/01/2000");
            else
                fechaini = Convert.ToDateTime(FECHAINICIOCOPAGOS.SelectedDate);

            if (FECHAFINALCOPAGOS.SelectedDate == null || FECHAFINALCOPAGOS.SelectedDate.ToString() == "")
                fechafin = Convert.ToDateTime("01/01/3000");
            else
                fechafin = Convert.ToDateTime(FECHAFINALCOPAGOS.SelectedDate);
            _totalRows = CoPagosBLL.GetPrestacionesCoPagoALLMG(_cache, ciudadId, ClienteId, TipoProveedorId, MedicoId, estado, nombre, carnet, fechaini, fechafin, DataGridPageSize, firstRow);


        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Copagos Pendientes Cobro Lista");
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
            //  PrimeraFilaCargadaHF.Value = (_cache[0].RowNumber - 1).ToString();
            //  UltimaFilaCargadaHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
            //
        }

        // Registrar el total de filas leidas
        TotalFilasHF.Value = _totalRows.ToString();


        // Actualizar el estado de habilitado los botones de navegación

        PrestacionesCoPagoGrid.DataSource = _cache;
        PrestacionesCoPagoGrid.DataBind();
        ChangeButtonStates();

    }

    private void ExportarCoPagos()
    {
        try
        {
            // aqui creamos un objeto de radgrid para no utilizar el que esta en la pantalla y descargar todos los resultados de la busqueda
            RadGrid RadGrid1 = new RadGrid();
            RadGrid1.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Data selected using dates between 1 Jan 2011 to 1 Sep 2011</span>"));
            RadGrid1.ID = "RadGrid1";
            RadGrid1.MasterTableView.DataKeyNames = new string[] { "CasoId" };
            RadGrid1.MasterTableView.AutoGenerateColumns = false;
            RadGrid1.MasterTableView.CommandItemSettings.ShowExportToExcelButton = true;
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            RadGrid1.ExportSettings.ExportOnlyData = true;

            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Estado";
            boundColumn.HeaderText = "ESTADO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 100;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombrePaciente";
            boundColumn.HeaderText = "NOMBRE DE PACIENTE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreCliente";
            boundColumn.HeaderText = "NOMBRE CLIENTE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 300;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreProveedor";
            boundColumn.HeaderText = "NOMBRE PROVEEDOR";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreTipoProveedor";
            boundColumn.HeaderText = "TIPO DE SERVICIO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "FechaCita";
            boundColumn.HeaderText = "FECHA";
            boundColumn.DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "MontoTotal";
            boundColumn.HeaderText = "MONTO TOTAL";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 110;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.String);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ValorCoPago";
            boundColumn.HeaderText = "VALOR DE COPAGO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "MontoAPagar";
            boundColumn.HeaderText = "MONTO A COBRAR";
            //  boundColumn.DataType = System.Decimal;
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;

            #region "variables de control"
            //parametros para cargar
            List<CoPagos> _cache = new List<CoPagos>();
            int estado;
            string dciudad;
            int dcliente;
            string dtipoPro;
            int prove;
            string caso;
            string nombre;
            string carnet;
            DateTime fechaini;
            DateTime fechafin;
            if (ciudadComboBox.SelectedValue == "TTT")
                dciudad = "";

            else
                dciudad = ciudadComboBox.SelectedValue;
            if (clientesComboBox.SelectedValue == "0")
                dcliente = 0;
            else
                dcliente = int.Parse(clientesComboBox.SelectedValue.ToString());
            if (TipoProveedorComboBox.SelectedValue == "0")
                dtipoPro = "";
            else
                dtipoPro = TipoProveedorComboBox.SelectedValue;
            if (ProveedorComboBox.SelectedValue == "0")
                prove = 0;
            else
                prove = int.Parse(ProveedorComboBox.SelectedValue.ToString());

            estado = int.Parse(estadoComboBox.SelectedValue);
            if (codigoCasoIdText.Text == "" || codigoCasoIdText.Text == " ")
            {
                caso = "";
            }
            else
            {
                caso = (codigoCasoIdText.Text);
            }
            if (NombreText.Text == null || NombreText.Text == "")
                nombre = "";
            else
                nombre = this.NombreText.Text.ToString();
            if (CIText.Text == null || CIText.Text == "")
                carnet = "";
            else
                carnet = this.CIText.DisplayText.ToString();
            if (FECHAINICIOCOPAGOS.SelectedDate == null || FECHAINICIOCOPAGOS.SelectedDate.ToString() == "")
                fechaini = Convert.ToDateTime("01/01/2000");
            else
                fechaini = Convert.ToDateTime(FECHAINICIOCOPAGOS.SelectedDate);

            if (FECHAFINALCOPAGOS.SelectedDate == null || FECHAFINALCOPAGOS.SelectedDate.ToString() == "")
                fechafin = Convert.ToDateTime("01/01/3000");
            else
                fechafin = Convert.ToDateTime(FECHAFINALCOPAGOS.SelectedDate);

            #endregion

            int _totalRows = CoPagosBLL.GetPrestacionesCoPagoALL(_cache, dciudad, dcliente, dtipoPro, prove, Convert.ToInt32(MedicoComboBox.SelectedValue), estado, caso, nombre, carnet, fechaini, fechafin, 0, -1);

            RadGrid1.DataSource = _cache;

            RadGrid1.DataBind();
            this.Controls.Add(RadGrid1);
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            RadGrid1.ExportSettings.HideStructureColumns = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.FileName = "ReportesCoPagoLista";
            RadGrid1.MasterTableView.GroupsDefaultExpanded = true;
            RadGrid1.MasterTableView.Caption = string.Empty;
            RadGrid1.MasterTableView.ExportToExcel();


            /*DataTable data = this.ConvertListToDataTable(_cache);

            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("EstudioxXFinanciera");



                ws.Cells["A1"].LoadFromDataTable(data, true);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                //ws.Cells["A1"].LoadFromDataTable(datos, true);
                ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();



                //Format the header for column 1-6
                using (ExcelRange rng = ws.Cells["A1:I1"])
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


                //Write it back to the client
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=COPAGOREPORTES.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            }*/
        }
        catch (Exception q)
        {
            log.Error("No se puedo Descargar el Excel", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }

    private void ExportarCoPagosMatrizNacional()
    {
        try
        {
            // aqui creamos un objeto de radgrid para no utilizar el que esta en la pantalla y descargar todos los resultados de la busqueda
            RadGrid RadGrid1 = new RadGrid();
            RadGrid1.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Data selected using dates between 1 Jan 2011 to 1 Sep 2011</span>"));
            RadGrid1.ID = "RadGrid1";
            RadGrid1.MasterTableView.DataKeyNames = new string[] { "CasoId" };
            RadGrid1.MasterTableView.AutoGenerateColumns = false;
            RadGrid1.MasterTableView.CommandItemSettings.ShowExportToExcelButton = true;
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            RadGrid1.ExportSettings.ExportOnlyData = true;

            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Estado";
            boundColumn.HeaderText = "ESTADO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 100;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombrePaciente";
            boundColumn.HeaderText = "NOMBRE DE PACIENTE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "CarnetIdentidad";
            boundColumn.HeaderText = "cARNET IDENTIDAD";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NumeroPoliza";
            boundColumn.HeaderText = "MUMERO DE POLIZA";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "CodigoCaso";
            boundColumn.HeaderText = "CODIGO DEL CASO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreCliente";
            boundColumn.HeaderText = "NOMBRE CLIENTE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 300;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreCiudad";
            boundColumn.HeaderText = "CIUDAD";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreProveedor";
            boundColumn.HeaderText = "NOMBRE PROVEEDOR";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreTipoProveedor";
            boundColumn.HeaderText = "TIPO DE SERVICIO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "FechaCita";
            boundColumn.HeaderText = "FECHA";
            boundColumn.DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "MontoTotal";
            boundColumn.HeaderText = "MONTO TOTAL";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 110;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.String);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ValorCoPago";
            boundColumn.HeaderText = "VALOR DE COPAGO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "MontoAPagar";
            boundColumn.HeaderText = "MONTO A COBRAR";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 130;






            #region "variables de control"
            //parametros para cargar
            List<CoPagos> _cache = new List<CoPagos>();
            int estado;
            string dciudad;
            int dcliente;
            string dtipoPro;
            int prove;
            string caso;
            string nombre;
            string carnet;
            DateTime fechaini;
            DateTime fechafin;
            if (ciudadComboBox.SelectedValue == "TTT")
                dciudad = "";

            else
                dciudad = ciudadComboBox.SelectedValue;
            if (clientesComboBox.SelectedValue == "0")
                dcliente = 0;
            else
                dcliente = int.Parse(clientesComboBox.SelectedValue.ToString());
            if (TipoProveedorComboBox.SelectedValue == "0")
                dtipoPro = "";
            else
                dtipoPro = TipoProveedorComboBox.SelectedValue;
            if (ProveedorComboBox.SelectedValue == "0")
                prove = 0;
            else
                prove = int.Parse(ProveedorComboBox.SelectedValue.ToString());

            estado = int.Parse(estadoComboBox.SelectedValue);
            if (codigoCasoIdText.Text == "" || codigoCasoIdText.Text == " ")
            {
                caso = "";
            }
            else
            {
                caso = (codigoCasoIdText.Text);
            }
            if (NombreText.Text == null || NombreText.Text == "")
                nombre = "";
            else
                nombre = this.NombreText.Text.ToString();
            if (CIText.Text == null || CIText.Text == "")
                carnet = "";
            else
                carnet = this.CIText.DisplayText.ToString();
            if (FECHAINICIOCOPAGOS.SelectedDate == null || FECHAINICIOCOPAGOS.SelectedDate.ToString() == "")
                fechaini = Convert.ToDateTime("01/01/2000");
            else
                fechaini = Convert.ToDateTime(FECHAINICIOCOPAGOS.SelectedDate);

            if (FECHAFINALCOPAGOS.SelectedDate == null || FECHAFINALCOPAGOS.SelectedDate.ToString() == "")
                fechafin = Convert.ToDateTime("01/01/3000");
            else
                fechafin = Convert.ToDateTime(FECHAFINALCOPAGOS.SelectedDate);

            #endregion

            int _totalRows = CoPagosBLL.GetPrestacionesCoPagoALLNacional(_cache, dciudad, dcliente, dtipoPro, prove, Convert.ToInt32(MedicoComboBox.SelectedValue), estado, caso, nombre, carnet, fechaini, fechafin, 0, -1);

            RadGrid1.DataSource = _cache;

            RadGrid1.DataBind();
            this.Controls.Add(RadGrid1);
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            RadGrid1.ExportSettings.HideStructureColumns = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.FileName = "ReportesCoPagoLista";
            RadGrid1.MasterTableView.GroupsDefaultExpanded = true;
            RadGrid1.MasterTableView.Caption = string.Empty;
            RadGrid1.MasterTableView.ExportToExcel();

        }
        catch (Exception q)
        {
            log.Error("No se puedo Descargar el Excel", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }

    private DataTable ConvertListToDataTable(List<CoPagos> list)
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
        data.Columns.Add("ESTADO");
        data.Columns.Add("NOMBRE DE PACIENTE");
        data.Columns.Add("NOMBRE CLIENTE");
        data.Columns.Add("NOMBRE PROVEEDOR");
        data.Columns.Add("TIPO DE SERVICIO");
        data.Columns.Add("FECHA");
        data.Columns.Add("MONTO TOTAL");
        data.Columns.Add("VALOR DE COPAGO");
        data.Columns.Add("MONTO A COBRAR");


        foreach (CoPagos searchResult in list)
        {
            data.Rows.Add(searchResult.Estado,
                            searchResult.NombrePaciente,
                            searchResult.NombreCliente,
                            searchResult.NombreProveedor,
                            searchResult.NombreTipoProveedor,
                            searchResult.FechaCita,
                            searchResult.MontoTotal,
                            searchResult.ValorCoPago,
                            searchResult.MontoAPagar);
        }


        return data;
    }

    private void ExportarCoPagosMedicoGeneral()
    {
        try
        {
            // aqui creamos un objeto de radgrid para no utilizar el que esta en la pantalla y descargar todos los resultados de la busqueda
            RadGrid RadGrid1 = new RadGrid();
            RadGrid1.ID = "RadGrid1";
            RadGrid1.MasterTableView.DataKeyNames = new string[] { "CasoId" };
            RadGrid1.MasterTableView.AutoGenerateColumns = false;
            RadGrid1.MasterTableView.CommandItemSettings.ShowExportToExcelButton = true;
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            RadGrid1.ExportSettings.ExportOnlyData = true;

            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombrePaciente";
            boundColumn.HeaderText = "NOMBRE DE PACIENTE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreCliente";
            boundColumn.HeaderText = "NOMBRE CLIENTE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 300;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreProveedor";
            boundColumn.HeaderText = "NOMBRE PROVEEDOR";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombreTipoProveedor";
            boundColumn.HeaderText = "TIPO DE SERVICIO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "FechaCita";
            boundColumn.HeaderText = "FECHA";
            boundColumn.DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Estado";
            boundColumn.HeaderText = "ESTADO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 100;
            #region "variables de control"
            //parametros para cargar


            // Ponemos los Prestaciones leidos aquí. 
            List<CoPagos> _cache = new List<CoPagos>();

            //aqui controlamos lo queremos buscar

            int ClienteId;
            string nombre;
            string carnet;
            DateTime fechaini;
            DateTime fechafin;
            int estado = int.Parse(estadoComboBox.SelectedValue);
            string ciudadId = ciudadComboBox.SelectedValue;
            string TipoProveedorId = TipoProveedorComboBox.SelectedValue;
            int MedicoId = Convert.ToInt32(MedicoComboBox.SelectedValue);
            if (clientesComboBox.SelectedValue == "0")
                ClienteId = 0;
            else
                ClienteId = int.Parse(clientesComboBox.SelectedValue.ToString());
            if (NombreText.Text == null || NombreText.Text == "")
                nombre = "";
            else
                nombre = this.NombreText.Text.ToString();
            if (CIText.Text == null || CIText.Text == "")
                carnet = "";
            else
                carnet = this.CIText.DisplayText.ToString();
            if (FECHAINICIOCOPAGOS.SelectedDate == null || FECHAINICIOCOPAGOS.SelectedDate.ToString() == "")
                fechaini = Convert.ToDateTime("01/01/2000");
            else
                fechaini = Convert.ToDateTime(FECHAINICIOCOPAGOS.SelectedDate);

            if (FECHAFINALCOPAGOS.SelectedDate == null || FECHAFINALCOPAGOS.SelectedDate.ToString() == "")
                fechafin = Convert.ToDateTime("01/01/3000");
            else
                fechafin = Convert.ToDateTime(FECHAFINALCOPAGOS.SelectedDate);


            #endregion

            int _totalRows = CoPagosBLL.GetPrestacionesCoPagoALLMG(_cache, ciudadId, ClienteId, TipoProveedorId, MedicoId, estado, nombre, carnet, fechaini, fechafin, 0, -1);

            RadGrid1.DataSource = _cache;

            RadGrid1.DataBind();
            this.Controls.Add(RadGrid1);
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            RadGrid1.ExportSettings.HideStructureColumns = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.FileName = "ReportesCoPagoLista";
            RadGrid1.MasterTableView.GroupsDefaultExpanded = true;
            RadGrid1.MasterTableView.Caption = string.Empty;
            RadGrid1.MasterTableView.ExportToExcel();
        }
        catch (Exception q)
        {
            log.Error("No se puedo Descargar el Excel", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }

    private string BuildQuery()
    {
        string result = "";

        string CarnetIdentidad = TextCIValidar.Text;
        if (!string.IsNullOrEmpty(CarnetIdentidad))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" OR " + @"@CarnetIdentidad " + CarnetIdentidad : result + @"@CarnetIdentidad " + CarnetIdentidad;
            result = result + " ";
        }

        string pacienteNombre = TextCIValidar.Text;
        if (!string.IsNullOrEmpty(pacienteNombre))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" OR " + @"@NOMBREPACIENTE " + pacienteNombre : result + @"@NOMBREPACIENTE " + pacienteNombre;
            result = result + " ";
        }
        return result;
    }


}
