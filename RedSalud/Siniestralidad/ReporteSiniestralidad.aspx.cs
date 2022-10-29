using System;
using System.Collections.Generic;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Utilities.SystemMessages;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.Ciudad;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Desgravamen;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.Siniestralidad;
using Artexacta.App.Siniestralidad.BLL;
using Artexacta.App.Security.BLL;
public partial class Reportes_ReporteSiniestralidad : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private List<string> userPermissions;
    private static Bitacora theBitacora = new Bitacora();
    private int DataGridPageSize = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
       // SearchPaciente.OnSearch +=
       //new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_ReporteSiniestralidad_OnSearch);
        if (IsPostBack)
        
          return;
        
        LoadCiudadesComboBox();
        LoadClientesToCombo();
        LoadTipoBusquedaComboBox();
        BindGrid();
    }

    protected static object[] GetTiposBusqueda()
    {
        string sTiposEstudios = System.Web.Configuration.WebConfigurationManager.AppSettings["TiposBusquedaSiniestralidad"];
        var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        object[] table = jss.Deserialize<dynamic>(sTiposEstudios);
        return table;
    }

    private void LoadTipoBusquedaComboBox()
    {
        try
        {
            object[] theTable = GetTiposBusqueda();
            
            if (theTable != null && theTable.Count() > 0)
            {
                foreach (Dictionary<string, object> row in theTable)
                {
                    TipoBusquedaComboBox.Items.Add(new ListItem(row["nombre"].ToString(), row["id"].ToString()));
                }
            }
            TipoBusquedaComboBox.Items.FindByValue("").Selected = true;
        }
        catch (Exception q)
        {
            log.Error("Error in Page_Load in MainPage.aspx", q);
        }
    
    }

    private void LoadCiudadesComboBox()
    {
        try {
            List<Ciudad> list = CiudadBLL.getCiudadListFromPoliza();
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

    private void LoadClientesToCombo()
    {

        List<GenericComboContainer> clientes = null;
        try
        {

            clientes = DerivacionBLL.GetClientesByMedicoIdCombo(0);


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
            clientesComboBox.DataBind();
         
        }
    }
    protected void ReporteSiniestralidadGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
    }

    protected void ReporteSiniestralidadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;

        }
    }
    protected void ReporteSiniestralidadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

            //Session["TipoPoliza"] = e.CommandArgument + ";"+ GestionComboBox.SelectedValue.ToString();
            string dfecini = (String.Format("{0:yyyy/MM/dd}", FechaInicio.SelectedDate) ?? "");
            string dfecfin = (String.Format("{0:yyyy/MM/dd}", FechaFin.SelectedDate) ?? "");
            Session["TipoPoliza"] = e.CommandArgument + ";" + dfecini + ";" + dfecfin;
            Response.Redirect("~/Siniestralidad/ReporteSiniestralidadDetail.aspx");
            return;
        }
    }
    protected void ReporteSiniestralidadRadGrid_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e)
    {

    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {

        this.ReporteSiniestralidadGrid.Visible = true;
        ActivePageHF.Value = "0";
        BindGrid();

      
    }
    private void BindGrid()
    {
        // En esta variable almacenamos el total de filas en la base de datos.  No sólo las cargadas.
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los productos leidos aquí. 
        List<Siniestralidad> _cache = new List<Siniestralidad>();
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // No podemos ir antes de la página 0
        if (_activePage < 0) _activePage = 0;

        // Averiguamos desde que fila tenemos que cargar.
        int firstRow = _activePage * DataGridPageSize;

        if (firstRow < 0) firstRow = 0;

        // Obtener los datos de la BD
        try
        {
            //aqui controlamos lo queremos buscar
            int dcliente;
            string dfecini;
            string dfecfin;
            string dtipobusqueda;
            string dciudad;
            string dnombre;
            string dcarnet;
            
            dcliente = (clientesComboBox.SelectedValue == "0") ? dcliente = 0 : int.Parse(clientesComboBox.SelectedValue.ToString());
            dfecini = (String.Format("{0:yyyy/MM/dd}", FechaInicio.SelectedDate) ?? "");
            dfecfin = (String.Format("{0:yyyy/MM/dd}", FechaFin.SelectedDate) ?? "");
            dtipobusqueda = TipoBusquedaComboBox.SelectedValue;
            dciudad = ciudadComboBox.SelectedValue;
            dnombre = (NombreText.Text == null) ? "" : this.NombreText.Text.ToString();
            dcarnet = (CarnetIDText.Text == null) ? "" : this.CarnetIDText.Text.ToString();

            _totalRows = SiniestralidadBLL.GetReporteSiniestralidadALL(_cache, dcliente, dfecini, dfecfin, dtipobusqueda, dciudad, dnombre, dcarnet, DataGridPageSize, firstRow);

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Reporte Siniestralidad Cobro Lista");
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
       /*      PrimeraFilaCargadaHF.Value = (_cache[0].RowNumber - 1).ToString();
             UltimaFilaCargadaHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
         */   //
        }

        // Registrar el total de filas leidas
        TotalFilasHF.Value = _totalRows.ToString();


        // Actualizar el estado de habilitado los botones de navegación

        ReporteSiniestralidadGrid.DataSource = _cache;
        ReporteSiniestralidadGrid.DataBind();
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
}