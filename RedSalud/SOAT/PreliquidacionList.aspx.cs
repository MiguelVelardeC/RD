using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Accidentado.BLL;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Preliquidacion.BLL;
using Artexacta.App.SearchPreliquidaciones;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Preliquidacion;

public partial class SOAT_PreliquidacionList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int DataGridPageSize = 20;

    private int ClienteId
    {
        get
        {
            if (string.IsNullOrWhiteSpace(ClienteDDL.SelectedValue))
            {
                ClienteDDL.DataBind();
                if (ClienteDDL.Items.Count <= 0)
                {
                    SystemMessages.DisplaySystemWarningMessage("No tiene Clientes Asignados.");
                    return 0;
                }
            }
            return Convert.ToInt32(ClienteDDL.SelectedValue);
        }
        set
        {
            if (ClienteDDL.Items.Count <= 0)
            {
                ClienteDDL.DataBind();
            }
            ListItem item = ClienteDDL.Items.FindByValue(value.ToString());
            if (item != null)
            {
                ClienteDDL.ClearSelection();
                item.Selected = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = PageTitleLabel.Text = "Control del estado de las facturas SOAT" ;
        SearchPreliquidacion.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_PagoGastos_OnSearch);
        SearchPreliquidacion.Config = new SearchPreliquidaciones();
        
        if (!IsPostBack)
        {
            PreliquidacionRadGrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            loadSaveSearch();
            BindGrid();
        }
    }

    public void searchCtl_PagoGastos_OnSearch ()
    {
        string sql = SearchPreliquidacion.Sql;
        log.Debug("Parameter whereSql: " + sql);
        ActivePageHF.Value = "0";
        BindGrid();
    }

    protected void PagoGastosRadGrid_ItemCommand ( object sender, Telerik.Web.UI.GridCommandEventArgs e ){
        if (e.CommandName == "ExportToExcel" && ClienteId > 0)
        {
            PreliquidacionRadGrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            GridColumn col = PreliquidacionRadGrid.Columns.FindByUniqueNameSafe("DecimalMonto");
            if (col != null && col is GridBoundColumn)
            {
                ((GridBoundColumn)col).DataFormatString = "";
            }
            col = PreliquidacionRadGrid.Columns.FindByUniqueNameSafe("DecimalMontoFactura");
            if (col != null && col is GridBoundColumn)
            {
                ((GridBoundColumn)col).DataFormatString = "";
            }

            List<PreliquidacionSearch> _cache = new List<PreliquidacionSearch>();
            PreliquidacionBLL.SearchPreliquidacion(ref _cache, int.MaxValue, 0, ClienteId, SearchPreliquidacion.Sql, OrderByHF.Value);
            PreliquidacionRadGrid.DataSource = _cache;
            PreliquidacionRadGrid.DataBind();
            RedCliente cliente = RedClienteBLL.GetRedClienteByClienteId(ClienteId);
            PreliquidacionRadGrid.ExportSettings.FileName += " [" + cliente.NombreJuridico + "]";
        }
    }
    protected void PagoGastosRadGrid_ExcelExportCellFormatting ( object sender, ExcelExportCellFormattingEventArgs e )
    {
        if (e.FormattedColumn.UniqueName.StartsWith("Decimal"))
        {
            string decimalSimbol = ".";//DecimalSimbolHF.Value;
            string groupSimbol = decimalSimbol == "," ? "." : ",";
            e.Cell.Style["mso-number-format"] = @"\#\" + groupSimbol + @"\#\#0\" + decimalSimbol + "00";
        }
    }

    private void loadSaveSearch ()
    {
        if (HttpContext.Current.Request.Cookies["SOATSearchPreliquidacion"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("SOATSearchPreliquidacion");
            string SearchPreliquidacionQuery = SearchPreliquidacion.Query;
            string OrderByHFValue = OrderByHF.Value;
            int DataGridPageSizeTemp = DataGridPageSize;
            string ActivePageHFValue = ActivePageHF.Value;
            try
            {
                SearchPreliquidacion.Query = myCookie["QUERY"];
                OrderByHF.Value = myCookie["ORDER"];
                DataGridPageSize = int.Parse(myCookie["PAGESIZE"]);
                ActivePageHF.Value = myCookie["ACTIVEPAGE"];
            }
            catch
            {
                SearchPreliquidacion.Query = SearchPreliquidacionQuery;
                OrderByHF.Value = OrderByHFValue;
                DataGridPageSize = DataGridPageSizeTemp;
                ActivePageHF.Value = ActivePageHFValue;
            }
        }
    }
    
    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["SOATSearchPreliquidacion"] != null)
        {
            myCookie = Request.Cookies.Get("SOATSearchPreliquidacion");
        }
        else
        {
            myCookie = new HttpCookie("SOATSearchPreliquidacion");
        }
        myCookie["QUERY"] = SearchPreliquidacion.Query;
        myCookie["ORDER"] = OrderByHF.Value;
        myCookie["PAGESIZE"] = DataGridPageSize.ToString();
        myCookie["ACTIVEPAGE"] = ActivePageHF.Value;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
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
        List<PreliquidacionSearch> _cache = new List<PreliquidacionSearch>();

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
            if (ClienteId > 0)
            {
                _totalRows = PreliquidacionBLL.SearchPreliquidacion(ref _cache, DataGridPageSize, firstRow, ClienteId, SearchPreliquidacion.Sql, OrderByHF.Value);
            }

            foreach (GridColumn col in PreliquidacionRadGrid.Columns)
            {
                col.HeaderText = Regex.Replace(col.HeaderText, "\\s<.+>", "");
                col.HeaderStyle.CssClass = "";
                if (col.UniqueName.Replace("Decimal", "") == OrderByHF.Value.Replace(" DESC", "").Replace(" ASC", ""))
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
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Preliquidaciones");
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
        }
        else
        {
            PrimeraFilaCargadaHF.Value = (_cache[0].RowNumber - 1).ToString();
            UltimaFilaCargadaHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
        }

        // Registrar el total de filas leidas
        TotalFilasHF.Value = _totalRows.ToString();

        // Conectar el repeater con los datos cargados del Web
        PreliquidacionRadGrid.DataSource = _cache;
        PreliquidacionRadGrid.DataBind();

        // Actualizar el estado de habilitado los botones de navegación
        ChangeButtonStates();

        // Al final de esta rutina, los hidden fields PrimeraFilaCargadaHF, UltimaFilaCargadaHF,
        // ActivePageHF y TotalFilasHF tienen los valores de lo que realmente se cargó.
    }
    /// <summary>
    /// Apagamos o prendemos los botones de navegación del Grid2 dependiendo de donde
    /// estamos.
    /// </summary>
    private void ChangeButtonStates ()
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
    protected void ClienteDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page PreliquidacionList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PreliquidacionRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            PreliquidacionSearch objItem = (PreliquidacionSearch)e.Item.DataItem;

            if (objItem != null)
            {
                if (objItem.PagoGastosId > 0)
                {
                    e.Item.CssClass = "Atendida rgRow";
                }
            }

            //InfoPA.Visible = false;
        }
    }
}