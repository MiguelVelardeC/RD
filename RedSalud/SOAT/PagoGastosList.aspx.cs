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
using Artexacta.App.PagoGastos;
using Artexacta.App.PagoGastos.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;

public partial class SOAT_PagoGastosList : System.Web.UI.Page
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
        bool ADMIN_SOAT_PAGOS = LoginSecurity.IsUserAuthorizedPermission("ADMIN_SOAT_PAGOS");
        PagoGastosRadGrid.Columns.FindByUniqueName("TemplateColumnDeleteGTC").Visible = ADMIN_SOAT_PAGOS;
        PagoGastosRadGrid.Columns.FindByUniqueName("TemplateColumnPagarGasto").Visible = LoginSecurity.IsUserAuthorizedPermission("INSERT_SOAT_PAGOS");
        PagoGastosRadGrid.MasterTableView.CommandItemDisplay = ADMIN_SOAT_PAGOS ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
        this.Page.Title = PageTitleLabel.Text = (ADMIN_SOAT_PAGOS) ? "Control del estado de las facturas SOAT" : "Pago de facturas";
        SearchPagoGastos.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_PagoGastos_OnSearch);
        SearchPagoGastos.Config = new PagoGastosSearch(ADMIN_SOAT_PAGOS);
        MostrarPagadasCheckBox.Visible = !ADMIN_SOAT_PAGOS;
        
        if (!IsPostBack)
        {
            MostrarPagadasCheckBox.Checked = ADMIN_SOAT_PAGOS;
            loadSaveSearch();
            BindGrid();
        }
    }

    public void searchCtl_PagoGastos_OnSearch ()
    {
        string sql = SearchPagoGastos.Sql;
        log.Debug("Parameter whereSql: " + sql);
        ActivePageHF.Value = "0";
        BindGrid();
    }

    protected void PagoGastosRadGrid_ItemCommand ( object sender, Telerik.Web.UI.GridCommandEventArgs e ){
        if (e.CommandName == "ExportToExcel")
        {
            GridColumn col = PagoGastosRadGrid.Columns.FindByUniqueName("TemplateColumnDeleteGTC");
            if(col != null)
                col.Visible = false;
            col = PagoGastosRadGrid.Columns.FindByUniqueName("TemplateColumnPagarGasto");
            if(col != null)
                col.Visible = false;
            PagoGastosRadGrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            col = PagoGastosRadGrid.Columns.FindByUniqueNameSafe("DecimalMonto");
            if (col != null && col is GridBoundColumn)
            {
                ((GridBoundColumn)col).DataFormatString = "";
            }
            PagoGastosRadGrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            List<PagoGastosDS.SearchPagoGastosRow> _cache = new List<PagoGastosDS.SearchPagoGastosRow>();

            PagoGastosBLL.SearchPagoGastos(ref _cache, int.MaxValue, 0, ClienteId, SearchPagoGastos.Sql, OrderByHF.Value,
                    MostrarPagadasCheckBox.Checked ? "T" : "A");

            RedCliente cliente = RedClienteBLL.GetRedClienteByClienteId(ClienteId);
            PagoGastosRadGrid.ExportSettings.FileName += " [" + cliente.NombreJuridico + "]";
            PagoGastosRadGrid.DataSource = _cache;
            PagoGastosRadGrid.DataBind();
        }
        else
        {
            commands(e.CommandName.ToString(), e.CommandArgument.ToString(), (GridDataItem)e.Item);
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
        if (HttpContext.Current.Request.Cookies["SOATPAGOSSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("SOATPAGOSSearch");
            string SearchPagoGastosQuery = SearchPagoGastos.Query;
            string OrderByHFValue = OrderByHF.Value;
            int DataGridPageSizeTemp = DataGridPageSize;
            string ActivePageHFValue = ActivePageHF.Value;
            try
            {
                ClienteId = Convert.ToInt32(myCookie["CLIENTE"]);
                SearchPagoGastos.Query = myCookie["QUERY"];
                OrderByHF.Value = myCookie["ORDER"];
                DataGridPageSize = int.Parse(myCookie["PAGESIZE"]);
                ActivePageHF.Value = myCookie["ACTIVEPAGE"];
            }
            catch
            {
                SearchPagoGastos.Query = SearchPagoGastosQuery;
                OrderByHF.Value = OrderByHFValue;
                DataGridPageSize = DataGridPageSizeTemp;
                ActivePageHF.Value = ActivePageHFValue;
            }
        }
    }
    
    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["SOATPAGOSSearch"] != null)
        {
            myCookie = Request.Cookies.Get("SOATPAGOSSearch");
        }
        else
        {
            myCookie = new HttpCookie("SOATPAGOSSearch");
        }
        myCookie["CLIENTE"] = ClienteId.ToString();
        myCookie["QUERY"] = SearchPagoGastos.Query;
        myCookie["ORDER"] = OrderByHF.Value;
        myCookie["PAGESIZE"] = DataGridPageSize.ToString();
        myCookie["ACTIVEPAGE"] = ActivePageHF.Value;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void DetailsImageButton_Command ( object sender, CommandEventArgs e )
    {
        GridDataItem item = null;
        if (sender is ImageButton)
        {
            item = ((sender as ImageButton).Parent as GridTableCell).Item as GridDataItem;
        }
        commands(e.CommandName.ToString(), e.CommandArgument.ToString(), item);
    }
    private void commands (string commandName, string commandArgument, GridDataItem Item)
    {
        if (commandName.Equals("Eliminar"))
        {
            try
            {
                int PagoGastosId = Convert.ToInt32(commandArgument);

                PagoGastosBLL.DeletePagoGastos(PagoGastosId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el pago seleccionado");
                BindGrid();
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PagoGastosID in PagoGastosRadGrid_ItemCommand on page PagoGastosList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el PagoGastos.");
            }
        }
        else if (commandName == "ExportToExcel" && ClienteId > 0)
        {
            PagoGastosRadGrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            List<PagoGastosDS.SearchPagoGastosRow> _cache = new List<PagoGastosDS.SearchPagoGastosRow>();
            
            PagoGastosBLL.SearchPagoGastos(ref _cache, int.MaxValue, 0, ClienteId, SearchPagoGastos.Sql, OrderByHF.Value,
                    MostrarPagadasCheckBox.Checked ? "T" : "A");
            PagoGastosRadGrid.DataSource = _cache;
            PagoGastosRadGrid.DataBind();
            GridColumn col = PagoGastosRadGrid.Columns.FindByUniqueNameSafe("DecimalMonto");
            if (col != null && col is GridBoundColumn)
            {
                ((GridBoundColumn)col).DataFormatString = "";
            }
        }
    }
    protected void PagoGastosRadGrid_ItemDataBound ( object sender, Telerik.Web.UI.GridItemEventArgs e )
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            PagoGastosDS.SearchPagoGastosRow objPago = (PagoGastosDS.SearchPagoGastosRow)item.DataItem;
            if (objPago.PagoGastosId <= 0)
            {
                item.Cells[PagoGastosRadGrid.Columns.FindByUniqueName("TemplateColumnDeleteGTC").OrderIndex].Text = "";
            }
            else
            {
                item.Cells[PagoGastosRadGrid.Columns.FindByUniqueName("TemplateColumnPagarGasto").OrderIndex].Text = "";
            }
            if (objPago.FechaPago <= DateTime.Parse("01-01-1900"))
            {
                item.Cells[PagoGastosRadGrid.Columns.FindByUniqueName("FechaPago").OrderIndex].Text = "";
                item.Cells[PagoGastosRadGrid.Columns.FindByUniqueName("Efectivo").OrderIndex].Text = "";
            }
            int dias = int.Parse("0" + DiasTranscurridos(objPago.FechaPago, objPago.FechaEmision).Replace(" días", ""));
            if (dias >= 16)
            {
                item.CssClass = "alertaDias";
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
        List<PagoGastosDS.SearchPagoGastosRow> _cache = new List<PagoGastosDS.SearchPagoGastosRow>();

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
                _totalRows = PagoGastosBLL.SearchPagoGastos(ref _cache, DataGridPageSize, firstRow, ClienteId, SearchPagoGastos.Sql, OrderByHF.Value,
                    MostrarPagadasCheckBox.Checked ? "T" : "A");
            }

            foreach (GridColumn col in PagoGastosRadGrid.Columns)
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
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de PagoGastos");
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
        PagoGastosRadGrid.DataSource = _cache;
        PagoGastosRadGrid.DataBind();

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
    protected void NGSaveLB_Click ( object sender, EventArgs e )
    {
        PagoGastos pago = new PagoGastos();
        try
        {
            pago.GastosEjecutadosDetalleId = int.Parse(GastosEjecutadosDetalleIdHF.Value);
            pago.Efectivo = EfectivoCheckBox.Checked;
            pago.NroCheque = NumeroChequeTextBox.Text;
            pago.BancoEmisor = BancoEmisorTextBox.Text;
            int userId = 0;
            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception q)
            {
                SystemMessages.DisplaySystemErrorMessage("No se pudo obtener el Usuario.");
                log.Error(q);
                return;
            }
            pago.UserId = userId;
            PagoGastosBLL.InsertPagoGastos(ref pago);
            BindGrid();
            SystemMessages.DisplaySystemMessage("Pago insertado correctamente.");
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Insertar el Pago.");
            log.Error(q);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "NPOpenPOPUP", "ShowNPDialog(" + GastosEjecutadosDetalleIdHF.Value + ");", true);
        }
    }
    protected void MostrarPagadas_CheckedChanged ( object sender, EventArgs e )
    {
        BindGrid();
    }

    public string DiasTranscurridos (DateTime fechapago, DateTime fecharecepcion)
    {
        if (fechapago <= DateTime.Parse("01-01-1900"))
        {
            return "";
        }
        TimeSpan ts = fechapago - fecharecepcion;

        int differenceInDays = ts.Days;
        return differenceInDays + " días";
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
            log.Error("Function ClienteODS_Selected on page PagoGastosList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}