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
using Artexacta.App.Siniestro;
using Artexacta.App.Siniestro.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.GestionMedica.BLL;
using Artexacta.App.GastosEjecutados;
using Artexacta.App.Preliquidacion;
using Artexacta.App.Preliquidacion.BLL;
using Artexacta.App.GastosEjecutados.BLL;
using Artexacta.App.Configuration;
using System.IO;
using Artexacta.App.Documents;

public partial class SOAT_SiniestroList : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int DataGridPageSize = 20;
    private int UserId
    {
        get {
            int userId = 0;

            try
            {
                userId = int.Parse("0" + UsuarioIdHF.Value);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            return userId;
        }
        set { UsuarioIdHF.Value = value.ToString(); }
    }

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
        SearchSiniestros.Config = new SiniestroSearch();
        SearchSiniestros.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_Siniestros_OnSearch);
        
        if (isExportToExcel())
        {
            try
            {
                loadSaveSearch();
                ExportToExcelRadGrid.Visible = true;
                if (ClienteId > 0)
                {
                    ExportToExcelRadGrid.DataSource = SiniestroBLL.GetSiniestroForExport(SearchSiniestros.Sql, ClienteId, UserId, OrderByHF.Value);
                    ExportToExcelRadGrid.ExportSettings.FileName = "SOAT";
                    ExportToExcelRadGrid.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
                    ExportToExcelRadGrid.ExportSettings.ExportOnlyData = true;
                    ExportToExcelRadGrid.ExportSettings.OpenInNewWindow = true;
                    ExportToExcelRadGrid.DataBind();
                    RedCliente cliente = RedClienteBLL.GetRedClienteByClienteId(ClienteId);
                    ExportToExcelRadGrid.ExportSettings.FileName += " [" + cliente.NombreJuridico + "]";
                    ExportToExcelRadGrid.MasterTableView.ExportToExcel();
                }
            }
            catch (Exception q)
            {
                log.Error("Function ExportToExcelRadGrid_ItemCreated on page SOATWizard.aspx", q);
                SystemMessages.DisplaySystemErrorMessage("Error al exportar datos.");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CLOSEEXPORT", "window.close()", true);
            }
            return;
        }
        bool seeAllDptos = LoginSecurity.IsUserAuthorizedPermission("SOAT_ALLDPTOS");
        FileManager.OnListFileChange += new UserControls_FileManager.OnListFileChangeDelegate(FileManager_FileSave);
        SiniestroRadGrid.Columns.FindByUniqueName("NoSortDeleteGTC").Visible = LoginSecurity.IsUserAuthorizedPermission("DELETE_SOAT");
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated && !seeAllDptos)
                loadUserFilter();
            loadSaveSearch();
            BindGrid();
            bool puedeExportarAExcel = false;
            bool puedeEliminarArchivosSiniestro = false;
            //
            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_EXPORT_EXCEL_SINIESTRO");
                puedeExportarAExcel = true;
            }
            catch (Exception)
            {
                puedeExportarAExcel = false;
            }

            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_SINIESTRO_FILES");
                puedeEliminarArchivosSiniestro = true;
            }
            catch (Exception)
            {
                puedeEliminarArchivosSiniestro = false;
            }
            FileManager.CanOnlyDeleteFiles = puedeEliminarArchivosSiniestro;
            ExportToExcelLB.Visible = puedeExportarAExcel;
        }
    }
    private bool isExportToExcel()
    {
        if (Session["ExportToExcel"] != null && !string.IsNullOrEmpty(Session["ExportToExcel"].ToString()))
        {
            DecimalSimbolHF.Value = Session["ExportToExcel"].ToString();
            Session["ExportToExcel"] = null;
            return true;
        }
        return false;
    }

    public void FileManager_FileSave ( string ObjectName, string type )
    {
        BindGrid();
    }

    private void loadUserFilter ()
    {
        try
        {
            UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Error getting the user Id.", e);
        }
    }

    protected void DepartamentoDDL_SelectedIndexChanged ( object sender, EventArgs e )
    {
        ActivePageHF.Value = "0";
        BindGrid();
    }
    public void searchCtl_Siniestros_OnSearch ()
    {
        string sql = SearchSiniestros.Sql;
        log.Debug("Parameter whereSql: " + sql);
        ActivePageHF.Value = "0";
        BindGrid();
    }

    protected void SiniestroRadGrid_ItemCommand ( object sender, Telerik.Web.UI.GridCommandEventArgs e ){
        commands(e.CommandName.ToString(), e.CommandArgument.ToString(), (GridDataItem)e.Item);
    }

    private void loadSaveSearch ()
    {
        if (HttpContext.Current.Request.Cookies["SOATSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("SOATSearch");
            string SearchSiniestrosQuery = SearchSiniestros.Query;
            string UsuarioIdHFValue = UsuarioIdHF.Value;
            string OrderByHFValue = OrderByHF.Value;
            int DataGridPageSizeTemp = DataGridPageSize;
            string ActivePageHFValue = ActivePageHF.Value;
            try
            {
                ClienteId = Convert.ToInt32(myCookie["CLIENTE"]);
                SearchSiniestros.Query = myCookie["QUERY"];
                UsuarioIdHF.Value = myCookie["DTO"];
                OrderByHF.Value = myCookie["ORDER"];
                DataGridPageSize = int.Parse(myCookie["PAGESIZE"]);
                ActivePageHF.Value = myCookie["ACTIVEPAGE"];
            }
            catch
            {
                SearchSiniestros.Query = SearchSiniestrosQuery;
                UsuarioIdHF.Value = UsuarioIdHFValue;
                OrderByHF.Value = OrderByHFValue;
                DataGridPageSize = DataGridPageSizeTemp;
                ActivePageHF.Value = ActivePageHFValue;
            }
        }
    }
    
    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["SOATSearch"] != null)
        {
            myCookie = Request.Cookies.Get("SOATSearch");
        }
        else
        {
            myCookie = new HttpCookie("SOATSearch");
        }
        myCookie["CLIENTE"] = ClienteId.ToString();
        myCookie["QUERY"] = SearchSiniestros.Query;
        myCookie["DTO"] = UsuarioIdHF.Value;
        myCookie["ORDER"] = OrderByHF.Value;
        myCookie["PAGESIZE"] = DataGridPageSize.ToString();
        myCookie["ACTIVEPAGE"] = ActivePageHF.Value;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    private void commands (string commandName, string commandArgument, GridDataItem Item)
    {
        if (commandName.Equals("Select"))
        {
            try
            {
                string[] strArgument = commandArgument.ToString().Split(new char[] { ',' });
                int SiniestroId = Convert.ToInt32(strArgument[0]);
                Session["SiniestroId"] = SiniestroId;
                Session["AccidentadoId"] = null;
                Session["AccidentadoList"] = "True";
                Session["Teminado"] = strArgument[1] == "TERMINADO" ? "1" : null;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir SiniestroID in SiniestroRadGrid_ItemCommand on page SiniestroList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("Error al seleccionar el Siniestro");
            }
            saveSearch();
            Response.Redirect("~/SOAT/SOATWizard.aspx");
        }
        else if (commandName.Equals("SelectAccidentado"))
        {
            try
            {
                int AccidentadoId = Convert.ToInt32(commandArgument);
                int SiniestroId = Convert.ToInt32(Item.OwnerTableView.ParentItem.GetDataKeyValue("SiniestroId"));
                Session["SiniestroId"] = SiniestroId;
                Session["AccidentadoId"] = AccidentadoId;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir SiniestroID in SiniestroRadGrid_ItemCommand on page SiniestroList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("Error al seleccionar el Siniestro");
            }
            saveSearch();
            Response.Redirect("~/SOAT/SOATWizard.aspx");
        }
        else if (commandName.Equals("EliminarAccidentado"))
        {
            try
            {
                int AccidentadoId = Convert.ToInt32(commandArgument);
                int SiniestroId = Convert.ToInt32(Item.OwnerTableView.ParentItem.GetDataKeyValue("SiniestroId"));

                AccidentadoBLL.DeleteAccidentado(AccidentadoId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el Accidentado seleccionado");
                //SiniestroRadGrid.MasterTableView.DetailTables[0].Rebind();
                BindGrid();
                /*
                Session["SiniestroId"] = SiniestroId;
                Session["AccidentadoId"] = AccidentadoId;
                */
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir SiniestroID in SiniestroRadGrid_ItemCommand on page SiniestroList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("Error al seleccionar el Siniestro");
            }
        }
        else if (commandName.Equals("AddGestionMedicaAccidentado"))
        {
            bool isSuccessful = false;
            try
            {
                int AccidentadoId = Convert.ToInt32(commandArgument);
                int SiniestroId = Convert.ToInt32(Item.OwnerTableView.ParentItem.GetDataKeyValue("SiniestroId"));
                Session["SiniestroId"] = SiniestroId;
                Session["AccidentadoId"] = AccidentadoId;
                Session["GestionMedica"] = "3";
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir SiniestroID in SiniestroRadGrid_ItemCommand on page SiniestroList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("Error al seleccionar el Siniestro");
            }

            if (isSuccessful)
            {
                saveSearch();
                Response.Redirect("~/SOAT/SOATWizard.aspx");
            }
        }
        else if (commandName.Equals("Eliminar"))
        {
            try
            {
                int SiniestroId = Convert.ToInt32(commandArgument);

                SiniestroBLL.DeleteSiniestro(SiniestroId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el Siniestro seleccionado");
                BindGrid();
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir SiniestroID in SiniestroRadGrid_ItemCommand on page SiniestroList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el Siniestro.");
            }
        }
        else if (commandName.Equals("PrintSiniestro"))
        {
            int siniestroId = Convert.ToInt32(commandArgument);

            //Session["siniestro"] = siniestroId;
            //Session["PAGINABACK"] = "SOATList.aspx";
            //Response.Redirect("~/SOAT/SiniestroImprimir.aspx?siniestro="+siniestroId);
            GetFullReportPdf(siniestroId);
        }
        else if (commandName.Equals("PrintSiniestroOnly"))
        {
            int siniestroId = Convert.ToInt32(commandArgument);

            //Session["siniestro"] = siniestroId;
            //Session["PAGINABACK"] = "SOATList.aspx";
            //Response.Redirect("~/SOAT/SiniestroImprimir.aspx?siniestro="+siniestroId);
            GetSiniestroReportPdf(siniestroId);
        }
        else if (commandName.Equals("Expand"))
        {
            SiniestroOpenHF.Value = commandArgument;
            ExpandCollapseItem(true);
        }
        else if (commandName.Equals("Collapse"))
        {
            SiniestroOpenHF.Value = commandArgument;
            ExpandCollapseItem(false);
        }
    }
    protected void ExpandCollapseItem ( bool expand )
    {
        try
        {
            int SiniestroId = Convert.ToInt32(SiniestroOpenHF.Value);
            GridDataItem Item = SiniestroRadGrid.MasterTableView.FindItemByKeyValue("SiniestroId", SiniestroId);
            if (expand)
            {
                Item.Expanded = true;
                Item.FindControl("NoSortExpandImageButton").Visible = false;
                Item.FindControl("CollapseLinkButton").Visible = true;
            }
            else
            {
                Item.Expanded = false;
                Item.FindControl("NoSortExpandImageButton").Visible = true;
                Item.FindControl("CollapseLinkButton").Visible = false;
                SiniestroOpenHF.Value = "";
            }
        } catch { }
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
        int _totalAccidentados = 0;
        decimal _totalPreliquidado = 0;
        decimal _totalPagado = 0;

        // Ponemos los productos leidos aquí. 
        List<SiniestroForList> _cache = new List<SiniestroForList>();

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
                _totalRows = SiniestroBLL.SearchSiniestroForList(_cache, ref _totalAccidentados, ref _totalPreliquidado, ref _totalPagado,
                    DataGridPageSize, firstRow, SearchSiniestros.Sql, ClienteId, UserId, OrderByHF.Value);
            }

            foreach (GridColumn col in SiniestroRadGrid.Columns)
            {
                col.HeaderStyle.CssClass = "";

                col.HeaderText = Regex.Replace(col.HeaderText, "\\s<.+>", "");
                if (col.UniqueName == OrderByHF.Value.Replace(" DESC", "").Replace(" ASC", ""))
                {
                    col.HeaderStyle.CssClass = "sortCol";
                    col.HeaderText = col.HeaderText + @" <img class=""sortImg"" style=""width:8px;border-width:0px;"" src=""" +
                        ResolveUrl("~/images/neutral/sort" + (OrderByHF.Value.EndsWith("ASC") ? "Up" : "Down") + ".png") + 
                                       @""" title=""ASCENDIENTE"">";
                }
                else if (!col.UniqueName.StartsWith("NoSort"))
                {
                    col.HeaderStyle.CssClass = "sortable";
                }
            }
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Siniestros");
        }
        // Update the labels that tell us what we did
        LoadedFirstRecordLabel.Text = (firstRow + 1).ToString();
        LoadedNumRecordsLabel.Text = (firstRow + _cache.Count).ToString();
        TotalDBRecordsLabel.Text = _totalRows.ToString();
        TotalAccidentadosRecordsLabel.Text = _totalAccidentados.ToString();
        //TotalPreliquidadoLabel.Text = _totalPreliquidado.ToString("#,##0.00 Bs");
        TotalPagadoLabel.Text = _totalPagado.ToString("#,##0.00 Bs");
        //AhorroLabel.Text = (_totalPreliquidado - _totalPagado).ToString("#,##0.00 Bs");

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
        SiniestroRadGrid.DataSource = _cache;
        SiniestroRadGrid.DataBind();
        SiniestroRadGrid.MasterTableView.DetailTables[0].DataBind();

        ExpandCollapseItem(true);

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
    protected void AccidentadoODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Accidentados.");
            log.Error("Function AccidentadoODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void SiniestroRadGrid_DetailTableDataBind ( object sender, GridDetailTableDataBindEventArgs e )
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        if (dataItem.Expanded)
        {
            e.DetailTableView.DataSource = AccidentadoBLL.SearchAccidentado("", (int)dataItem.GetDataKeyValue("SiniestroId"));
        }
    }
    protected void FileManager_Command ( object sender, CommandEventArgs e )
    {
        FileManager.OpenFileManager(e.CommandName, Convert.ToInt32(e.CommandArgument));
    }
    protected void ExportToExcelLB_Click ( object sender, EventArgs e )
    {
        Session["ExportToExcel"] = DecimalSimbolHF.Value;
        saveSearch();
        Response.Redirect("SOATList.aspx");
    }
    private void FormatCols(GridItem item){
        if (item.Cells.Count <= 57)
            return;
        if (item is GridHeaderItem)
        {
            item.Cells[6].Style.Add("width", "82px");//FECHA DE NACIMIENTO
            item.Cells[9].Style.Add("width", "80px");//CARNET IDENTIDAD O NIT
            item.Cells[10].Style.Add("width", "80px");//PASAJERO / PEATON
            item.Cells[11].Style.Add("width", "90px");//ACCIDENTADO / FALLECIDO
            item.Cells[12].Style.Add("width", "90px");//IDENTIFICADOR DE OPERACIÓN
            item.Cells[13].Style.Add("width", "80px");//NUMERO DE ROSETA
            item.Cells[14].Style.Add("width", "80px");//Nº DE CERTIFICADO O POLIZA
            item.Cells[16].Style.Add("width", "100px");//CARNET IDENTIDAD O NIT DEL TITULAR
            item.Cells[17].Style.Add("width", "80px");//FECHA OCURRIDO SINIESTRO
            item.Cells[21].Style.Add("width", "60px");//GESTION SOAT
            item.Cells[22].Style.Add("width", "80px");//FECHA DE DENUNCIA
            item.Cells[23].Style.Add("width", "100px");//TOTAL OCUPANTES DEL VEHICULO
            item.Cells[24].Style.Add("width", "75px");//CANTIDAD DE HERIDOS
            item.Cells[25].Style.Add("width", "75px");//CANTIDAD DE FALLECIDOS
            item.Cells[26].Style.Add("width", "100px");//LUGAR - DEPARTAMENTO
            item.Cells[33].Style.Add("width", "80px");//PLACA
            item.Cells[35].Style.Add("width", "100px");//FECHA DE VISITA DEL INSPECTOR Y/O MEDICO AUDITOR
            //item.Cells[39].Style.Add("width", "100px");//RESERVA ESTIMADA DE SINIESTRO
            for (int i = 39; i <= 46; i++)
            {
                item.Cells[i].Style.Add("width", "100px");
                item.Cells[i].Style.Add("mso-number-format", @"$0" + DecimalSimbolHF.Value + "00");
            }
            //item.Cells[40].Style.Add("width", "100px");// \
            //item.Cells[41].Style.Add("width", "100px");// |
            //item.Cells[42].Style.Add("width", "100px");// |
            //item.Cells[43].Style.Add("width", "100px");//  > GASTOS 
            //item.Cells[44].Style.Add("width", "100px");// |
            //item.Cells[45].Style.Add("width", "100px");// |
            //item.Cells[46].Style.Add("width", "100px");// /
            //item.Cells[47].Style.Add("width", "100px");//MONTO SINIESTROS PAGADOS
            //item.Cells[48].Style.Add("width", "100px");//SALDO A FAVOR ASEGURADO
            item.Cells[49].Style.Add("width", "110px");//CONTROL DE SALDOS
            item.Cells[50].Style.Add("width", "100px");//ESTADO ACTUAL DEL CASO
            item.Cells[52].Style.Add("width", "100px");//FECHA DE APROBACION POR RED SALUD DEL PAGO
            item.Cells[53].Style.Add("width", "100px");//DIAS TRANSCURRIDOS DESDE LA APROBACION
            item.Cells[54].Style.Add("width", "80px");//FECHA DE PAGO
            item.Cells[55].Style.Add("width", "100px");//DIAS TRANSCURRIDOS DESDE EL PAGO
            item.Cells[56].Style.Add("width", "100px");//ACUERDO TRANSACCIONAL Y/O REPETICION
        }
        else
        {
            List<int> cellsAlignCenter = new List<int>(new int[] { 0, 1, 2, 3, 15, 28, 29, 37, 57});
            int i = 0;
            foreach (TableCell cell in item.Cells){
                if (!cellsAlignCenter.Contains(i))
                {
                    item.Cells[i].Style.Add("text-align", "center");
                }
                i++;
            }
        }
    }
    protected void ExportToExcelRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        try
        {
            if (ExportToExcelRadGrid.DataSource == null)
            {
                Response.Redirect(Request.RawUrl, true);
                return;
            }
            FormatCols(e.Item);
            if (e.Item is GridHeaderItem)
            {
                foreach (TableCell cell in e.Item.Cells)
                {

                    if (cell.Text == "Nro")
                    {
                        cell.Style.Add("border-top", "0.6px solid #000");
                        cell.Style.Add("border-left", "0.6px solid #000");
                        cell.Style.Add("border-right", "1px double #000");
                        cell.Style.Add("border-bottom", "1px double #000");
                    }
                    else
                    {
                        cell.Style.Add("border", "1px double #000");
                    }
                    cell.Style.Add("background-color", "#E4DFEC");
                    cell.Style.Add("height", "70px");
                }
            }
            else if ((e.Item.ItemIndex + 1) == (ExportToExcelRadGrid.DataSource as List<SiniestroForExport>).Count)
            {
                foreach (TableCell cell in e.Item.Cells)
                {
                    cell.Style.Add("border-bottom", "1px double #000");
                    cell.Style.Add("border-left", "1px double #000");
                    cell.Style.Add("border-right", "1px double #000");
                }
            }
            else if (e.Item is GridItem && !(e.Item is GridFooterItem))
            {
                foreach (TableCell cell in e.Item.Cells)
                {
                    cell.Style.Add("border-bottom", "0.6px dotted #000");
                    cell.Style.Add("border-left", "1px double #000");
                    cell.Style.Add("border-right", "1px double #000");
                }
            }
        }
        catch (Exception q)
        {
            log.Error("Function ExportToExcelRadGrid_ItemCreated on page SOATWizard.aspx", q);
            SystemMessages.DisplaySystemErrorMessage("Error al exportar datos.");
            Response.Redirect(Request.RawUrl, true);
        }
    }
    protected void ExportToExcelRadGrid_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e)
    {
        try
        {

            e.Cell.Text = e.Cell.Text.ToUpper().Replace("&NBSP;", "");
            if (ExportToExcelRadGrid.DataSource == null) return;
            GridDataItem item = e.Cell.Parent as GridDataItem;
            int rowIndex = ((int)(e.Cell.Parent as GridDataItem).GetDataKeyValue("RowNumber")) + 1;
            if (e.FormattedColumn.UniqueName == "DecimalSiniestrosPreliquidacion")
            {
                e.Cell.Attributes["formula"] = "=SUM(AL" + rowIndex + ":AS" + rowIndex + ")";
            }
            if (e.FormattedColumn.UniqueName == "DecimalSiniestrosPagados")
            {
                e.Cell.Attributes["formula"] = "=SUM(AU" + rowIndex + ":BC" + rowIndex + ")";
            }
            if (e.FormattedColumn.UniqueName.StartsWith("Decimal"))
            {
                string decimalSimbol = DecimalSimbolHF.Value;
                e.Cell.Style["mso-number-format"] = @"\#\,\#\#0\.00";
                e.Cell.Text = e.Cell.Text.Replace(decimalSimbol == "," ? "." : ",", decimalSimbol);
            }
            else if (e.FormattedColumn.DataType == typeof(string))
            {
                e.Cell.Style["mso-number-format"] = @"\@";
            }
            else if (e.FormattedColumn.DataType == typeof(DateTime))
            {
                e.Cell.Style["mso-number-format"] = "Short Date";
            }
        }
        catch (Exception q)
        {
            log.Error("Function ExportToExcelRadGrid_ItemCreated on page SOATWizard.aspx", q);
            Response.Redirect(Request.RawUrl, true);
        }
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void SiniestroRadGrid_PreRender(object sender, EventArgs e)
    {

        //bool puedeModificarAccidentado = false;
        bool puedeEliminarSiniestro = false;
        bool puedeInsertarSiniestro = false;
        bool puedeEliminarAccidentado = false;
        bool puedeInsertarGestionMedica = false;
        bool puedeInsertarGastosEjecutados = false;

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_ACCIDENTADOS");
            puedeEliminarAccidentado = true;
        }
        catch (Exception)
        {
            puedeEliminarAccidentado = false;
        }

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_VISITA_MEDICA");
            puedeInsertarGestionMedica = true;
        }
        catch (Exception)
        {
            puedeInsertarGestionMedica = false;
        }

        
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_GASTO_MEDICO");
            puedeInsertarGastosEjecutados = true;
        }
        catch (Exception)
        {
            puedeInsertarGastosEjecutados = false;
        }
        
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_SINIESTRO");
            puedeEliminarSiniestro = true;
        }
        catch (Exception)
        {
            puedeEliminarSiniestro = false;
        }

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_SINIESTRO");            
            puedeInsertarSiniestro = true;
        }
        catch (Exception)
        {
            puedeInsertarSiniestro = false;
        }

        NuevoSiniestroHyperLink.Visible = puedeInsertarSiniestro;
        NuevoSiniestroHyperLink.Enabled = puedeInsertarSiniestro;

        SiniestroRadGrid.MasterTableView.GetColumn("NoSortDeleteGTC").Visible = puedeEliminarSiniestro;
        
        GridTableView ch = SiniestroRadGrid.MasterTableView.DetailTables[0];
        if (ch != null)
        {
            ch.GetColumn("DeleteAccidentado").Visible = puedeEliminarAccidentado;
            ch.GetColumn("GestionMedicaAccidentado").Visible = puedeInsertarGestionMedica;
            ch.GetColumn("PreliquidacionDetalleInsert").Visible = puedeInsertarGastosEjecutados;
        }

        //SiniestroRadGrid.MasterTableView.Rebind();
        //PreliquidacionDetalleInsert
    }
    protected void NGSaveLB_Click(object sender, EventArgs e)
    {
        try
        {

            if (TypeHF.Value == "G")
            {
                PreliquidacionDetalle PreliquidacionDetalle = new PreliquidacionDetalle();

                int siniestroId = 0;
                int.TryParse(SiniestroDetalleId.Value, out siniestroId);

                int accidentadoId = 0;
                int.TryParse(AccidentadoDetalleId.Value, out accidentadoId);

                PreliquidacionDetalle.SiniestroId = siniestroId;
                PreliquidacionDetalle.AccidentadoId = accidentadoId;
                PreliquidacionDetalle.Tipo = NGTipoGastoDDL.SelectedValue;
                PreliquidacionDetalle.Fecha = FechaForDB(NGFechaRDP.SelectedDate);
                try
                {
                    PreliquidacionDetalle.Proveedor =
                        ProveedorTags.SelectedTags.Split(new char[] { ',' })[0].ToUpper();
                }
                catch
                {
                    PreliquidacionDetalle.Proveedor = ProveedorTags.SelectedTags.ToUpper();
                }
                PreliquidacionDetalle.FechaReciboFactura = FechaForDB(FechaReciboFacturaRDP.SelectedDate);
                PreliquidacionDetalle.NumeroReciboFactura = NumeroReciboFacturaTextBox.Text;
                PreliquidacionDetalle.IsFactura = IsFacturaCheckBox.Checked;
                PreliquidacionDetalle.Monto = convertToDecimal(NGMontoTextBox.Text);
                PreliquidacionDetalle.PreliquidacionDetalleId = int.Parse("0" + saveIdHF.Value);
                PreliquidacionDetalle.Estado = EstadoDDL.SelectedValue == "1";
                if (PreliquidacionDetalle.PreliquidacionDetalleId <= 0)
                {
                    //en el insert el saveIDHF.value es el id de preliquidacionDetalleId

                    int idPreliquidacion = PreliquidacionBLL.InsertPreliquidacionDetalle(PreliquidacionDetalle);

                    if (idPreliquidacion <= 0)
                    {
                        SystemMessages.DisplaySystemErrorMessage("Hubo un error insertando el gastro, no se puedo procesar la preliquidacion automatica");
                        return;
                    }


                    GastosEjecutadosDetalle gastosEjecutadosDetalle = new GastosEjecutadosDetalle();
                    gastosEjecutadosDetalle.SiniestroId = PreliquidacionDetalle.SiniestroId;
                    gastosEjecutadosDetalle.AccidentadoId = PreliquidacionDetalle.AccidentadoId;
                    gastosEjecutadosDetalle.PreliquidacionDetalleId = idPreliquidacion;  //int.Parse("0" + PreliquidacionDetalleIdHF.Value);
                    gastosEjecutadosDetalle.Tipo = PreliquidacionDetalle.Tipo; //NGTipoGastoDDL.SelectedValue;
                    gastosEjecutadosDetalle.Fecha = PreliquidacionDetalle.Fecha;  //FechaForDB(NGFechaRDP.SelectedDate);
                    gastosEjecutadosDetalle.Proveedor = PreliquidacionDetalle.Proveedor; //ProveedorHF.Value;
                    gastosEjecutadosDetalle.FechaReciboFactura = PreliquidacionDetalle.FechaReciboFactura; //FechaForDB(FechaReciboFacturaRDP.SelectedDate);
                    gastosEjecutadosDetalle.NumeroReciboFactura = PreliquidacionDetalle.NumeroReciboFactura; //NumeroReciboFacturaTextBox.Text;
                    gastosEjecutadosDetalle.Monto = PreliquidacionDetalle.Monto;  //convertToDecimal(NGMontoTextBox.Text);
                    gastosEjecutadosDetalle.GastosEjecutadosDetalleId = 0;//int.Parse("0" + saveIdHF.Value);


                    GastosEjecutadosBLL.InsertGastosEjecutadosDetalle(gastosEjecutadosDetalle);
                }
                else
                {
                    //en el update el saveIDHF.value es el id de preliquidacionDetalleId asi que 
                    //lo cambiamos y obtenemos el id de preliquidacion en base al gastosEjecutadosDetalleId
                   
                }
            }
            NGTipoGastoDDL.ClearSelection();
            NGFechaRDP.SelectedDate = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);
            ProveedorTags.SelectedTags = "";
            FechaReciboFacturaRDP.SelectedDate = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);
            NumeroReciboFacturaTextBox.Text = "";
            IsFacturaCheckBox.Checked = false;
            NGMontoTextBox.Text = "";
            EstadoDDL.SelectedValue = "1";



            //MoveToStep("2");
        }
        catch (Exception eq)
        {

        }
    }
    private DateTime FechaForDB(DateTime? fecha)
    {
        if (fecha == null)
        {
            fecha = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);
        }
        return Configuration.ConvertToUTCFromClientTimeZone((DateTime)fecha);
    }

    private decimal convertToDecimal(string textToConvert)
    {
        try
        {
            string coma = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
            textToConvert = textToConvert.Replace(",", coma).Replace(".", coma);
            return decimal.Parse(textToConvert);
        }
        catch
        {
            return 0;
        }
    }

    private void GetFullReportPdf_EVO(int SiniestroId)
    {
        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        Siniestro siniestro = SiniestroBLL.GetSiniestroByID(SiniestroId);
        byte[] pdfBytesSiniestroReport = null;

        pdfBytesSiniestroReport = Siniestro.GetSiniestroEnPdf(new EnlaceSOATSISA(){SiniestroId = SiniestroId}, urlServer);

        EvoPdf.PdfMerge.PdfDocumentOptions pdfDocumentOptions = new EvoPdf.PdfMerge.PdfDocumentOptions();
        pdfDocumentOptions.PdfCompressionLevel = EvoPdf.PdfMerge.PDFCompressionLevel.Normal;
        pdfDocumentOptions.PdfPageSize = EvoPdf.PdfMerge.PdfPageSize.Letter;
        pdfDocumentOptions.PdfPageOrientation = EvoPdf.PdfMerge.PDFPageOrientation.Portrait;
        EvoPdf.PdfMerge.PDFMerge objMerger = new EvoPdf.PdfMerge.PDFMerge(pdfDocumentOptions);

        MemoryStream inputStream = new MemoryStream(pdfBytesSiniestroReport);
        objMerger.AppendPDFStream(inputStream, 0);
        inputStream.Close();

        byte[] finishedReport = null;

        finishedReport = objMerger.RenderMergedPDFDocument();
        /*
        if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
        }

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);

        // Caso Medico (la ficha de SISA)
        int casoId = 0;
        EnlaceDesgravamenSISA objEnlace = null;
        try
        {
            objEnlace = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(citaId, ref casoId);
            if (casoId > 0)
            {
                byte[] pdfHistorialSISA = null;
                pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(objEnlace, urlServer);
                if (pdfHistorialSISA != null)
                    pdfBytesLabos.Add(pdfHistorialSISA);
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener el caso de sisa", q);
        }
        List<string> pdfBytesImg = new List<string>();

        List<ProgramacionCitaLabo> listaLabos = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(citaId);
        foreach (ProgramacionCitaLabo progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                if (objDoc.Extension.ToLower() == ".pdf")
                {
                    pdfBytesLabos.Add(objDoc.Bytes);
                }
                else
                {
                    pdfBytesImg.Add(objDoc.FileStoragePath);
                }
            }
        }

        byte[] fullOutput = null;

        if (pdfBytesLabos.Count == 1)
        {
            fullOutput = pdfBytesLabos[0];
        }
        else
        {
            EvoPdf.PdfMerge.PdfDocumentOptions pdfDocumentOptions = new EvoPdf.PdfMerge.PdfDocumentOptions();
            pdfDocumentOptions.PdfCompressionLevel = EvoPdf.PdfMerge.PDFCompressionLevel.Normal;
            pdfDocumentOptions.PdfPageSize = EvoPdf.PdfMerge.PdfPageSize.Letter;
            pdfDocumentOptions.PdfPageOrientation = EvoPdf.PdfMerge.PDFPageOrientation.Portrait;
            EvoPdf.PdfMerge.PDFMerge objMerger = new EvoPdf.PdfMerge.PDFMerge(pdfDocumentOptions);

            foreach (byte[] pdfBytes in pdfBytesLabos)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                objMerger.AppendPDFStream(inputStream, 0);
                inputStream.Close();
            }
            foreach (string imgPath in pdfBytesImg)
            {
                objMerger.AppendImageFile(imgPath);
            }

            fullOutput = objMerger.RenderMergedPDFDocument();
        }*/

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        response.AddHeader("Content-Disposition", String.Format("attachment; filename=ReporteSiniestro1" + "" + ".pdf; size={0}", finishedReport.Length.ToString()));
        response.BinaryWrite(finishedReport);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();
    }

    private void GetFullReportPdf_HIQ(int siniestroId)
    {
        Siniestro objSiniestro = null;
        try
        {
            objSiniestro = SiniestroBLL.GetSiniestroByID(siniestroId);
        }
        catch (Exception eq)
        {
            log.Error("can't find siniestro", eq);
        }


        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        byte[] pdfBytesSiniestroReport = null;
        pdfBytesSiniestroReport = Siniestro.GetSiniestroEnPdf(new EnlaceSOATSISA { SiniestroId = siniestroId }, urlServer);
        //CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaId);
        //byte[] pdfBytesExamenMedico = null;
        /*if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
        }

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);
        
        // Caso Medico (la ficha de SISA)
        int casoId = 0;
        EnlaceDesgravamenSISA objEnlace = null;
        try
        {
            objEnlace = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(citaId, ref casoId);
            if (objEnlace != null && casoId > 0)
            {
                byte[] pdfHistorialSISA = null;
                pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(objEnlace, urlServer);
                if (pdfHistorialSISA != null)
                    pdfBytesLabos.Add(pdfHistorialSISA);
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener el caso de sisa", q);
        }

        // PDF delos adjuntos
        List<string> pdfBytesImg = new List<string>();
        List<ProgramacionCitaLabo> listaLabos = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(citaId);
        foreach (ProgramacionCitaLabo progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                if (objDoc.Extension.ToLower() == ".pdf")
                {
                    pdfBytesLabos.Add(objDoc.Bytes);
                }
                else
                {
                    pdfBytesImg.Add(objDoc.FileStoragePath);
                }
            }
        }*/

        HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        
        MemoryStream inputStreamOriginal = new MemoryStream(pdfBytesSiniestroReport);
        //HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        //completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        HiQPdf.PdfDocument documentToAddMergeOriginal = HiQPdf.PdfDocument.FromStream(inputStreamOriginal);
        completeDoc.AddDocument(documentToAddMergeOriginal);
        // MERGE de todo
        List<string> imagesAdded = new List<string>();
        List<byte[]> pdfsAdded = new List<byte[]>();

        try
        {            
            List<DocumentFile> rawFiles =  SiniestroBLL.GetSiniestroFiles(siniestroId);
            foreach (DocumentFile file in rawFiles)
            {
                if (file.Extension.ToLower() == ".pdf")
                {
                    pdfsAdded.Add(file.Bytes);
                }
                else
                {
                    imagesAdded.Add(file.FileStoragePath);
                }
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener la lista de archivos", q);
            
        }

        byte[] fullOutput = null;

        if (pdfsAdded.Count > 0)
        {
            foreach (byte[] pdfBytes in pdfsAdded)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
                completeDoc.AddDocument(documentToAddMerge);
            }
        }

        if (imagesAdded.Count > 0)
        {
            foreach (string imgPath in imagesAdded)
            {
                HiQPdf.PdfDocument documentToAddMerge = new HiQPdf.PdfDocument();
                // set a demo serial number
                documentToAddMerge.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

                // create a page in document
                HiQPdf.PdfPage page1 = documentToAddMerge.AddPage(HiQPdf.PdfPageSize.Letter, new HiQPdf.PdfDocumentMargins(25, 25, 10, 10), HiQPdf.PdfPageOrientation.Portrait);
                // set a background color for the page
                HiQPdf.PdfRectangle backgroundRectangle = new HiQPdf.PdfRectangle(page1.DrawableRectangle);
                backgroundRectangle.BackColor = System.Drawing.Color.White;
                page1.Layout(backgroundRectangle);

                // create the true type fonts that can be used in document text
                System.Drawing.Font sysFont = new System.Drawing.Font("Arial", 10, System.Drawing.GraphicsUnit.Point);
                HiQPdf.PdfFont pdfFont = documentToAddMerge.CreateFont(sysFont);
                HiQPdf.PdfFont pdfFontEmbed = documentToAddMerge.CreateFont(sysFont, true);

                float crtYPos = 20;
                float crtXPos = 5;

                // layout an opaque JPG image
                HiQPdf.PdfImage opaquePdfImage = new HiQPdf.PdfImage(crtXPos, crtYPos, imgPath);
                HiQPdf.PdfLayoutInfo imageLayoutInfo = page1.Layout(opaquePdfImage);

                completeDoc.AddDocument(documentToAddMerge);
            }
        }

        //MemoryStream inputStream = new MemoryStream(pdfBytesSiniestroReport);
        //HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        //completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        //HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
        //completeDoc.AddDocument(documentToAddMerge);

        fullOutput = completeDoc.WriteToMemory();

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        if(objSiniestro != null)
            response.AddHeader("Content-Disposition", String.Format("attachment; filename=ReporteCompletoSiniestro_" + objSiniestro.OperacionId + ".pdf; size={0}", fullOutput.Length.ToString()));
        else
            response.AddHeader("Content-Disposition", String.Format("attachment; filename=ReporteCompletoSiniestro_" + "" + ".pdf; size={0}", fullOutput.Length.ToString()));
        response.BinaryWrite(fullOutput);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();

        completeDoc.Close();
        /*if (pdfBytesLabos.Count == 1 && pdfBytesImg.Count <= 0)
        {
            fullOutput = pdfBytesLabos[0];

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + citaId + ".pdf; size={0}", fullOutput.Length.ToString()));
            response.BinaryWrite(fullOutput);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();
            return;
        }
        else
        {
            HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
            completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

            foreach (byte[] pdfBytes in pdfBytesLabos)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
                completeDoc.AddDocument(documentToAddMerge);
            }
            foreach (string imgPath in pdfBytesImg)
            {
                HiQPdf.PdfDocument documentToAddMerge = new HiQPdf.PdfDocument();
                // set a demo serial number
                documentToAddMerge.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

                // create a page in document
                HiQPdf.PdfPage page1 = documentToAddMerge.AddPage(HiQPdf.PdfPageSize.Letter, new HiQPdf.PdfDocumentMargins(25, 25, 10, 10), HiQPdf.PdfPageOrientation.Portrait);
                // set a background color for the page
                HiQPdf.PdfRectangle backgroundRectangle = new HiQPdf.PdfRectangle(page1.DrawableRectangle);
                backgroundRectangle.BackColor = System.Drawing.Color.White;
                page1.Layout(backgroundRectangle);

                // create the true type fonts that can be used in document text
                System.Drawing.Font sysFont = new System.Drawing.Font("Arial", 10, System.Drawing.GraphicsUnit.Point);
                HiQPdf.PdfFont pdfFont = documentToAddMerge.CreateFont(sysFont);
                HiQPdf.PdfFont pdfFontEmbed = documentToAddMerge.CreateFont(sysFont, true);

                float crtYPos = 20;
                float crtXPos = 5;

                // layout an opaque JPG image
                HiQPdf.PdfImage opaquePdfImage = new HiQPdf.PdfImage(crtXPos, crtYPos, imgPath);
                HiQPdf.PdfLayoutInfo imageLayoutInfo = page1.Layout(opaquePdfImage);

                completeDoc.AddDocument(documentToAddMerge);
                //documentToAddMerge.Close();
            }

            fullOutput = completeDoc.WriteToMemory();

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + citaId + ".pdf; size={0}", fullOutput.Length.ToString()));
            response.BinaryWrite(fullOutput);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();

            completeDoc.Close();

        }*/
    }

    private void GetSiniestroReportPdf_HIQ(int siniestroId)
    {
        Siniestro objSiniestro = null;
        try
        {
            objSiniestro = SiniestroBLL.GetSiniestroByID(siniestroId);
        }
        catch (Exception eq)
        {
            log.Error("can't find siniestro", eq);
        }


        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        byte[] pdfBytesSiniestroReport = null;
        pdfBytesSiniestroReport = Siniestro.GetSiniestroEnPdf(new EnlaceSOATSISA { SiniestroId = siniestroId }, urlServer);
        

        HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

        MemoryStream inputStreamOriginal = new MemoryStream(pdfBytesSiniestroReport);
        //HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        //completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        HiQPdf.PdfDocument documentToAddMergeOriginal = HiQPdf.PdfDocument.FromStream(inputStreamOriginal);
        completeDoc.AddDocument(documentToAddMergeOriginal);
        

        //MemoryStream inputStream = new MemoryStream(pdfBytesSiniestroReport);
        //HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        //completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        //HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
        //completeDoc.AddDocument(documentToAddMerge);

        byte[] fullOutput = null;

        fullOutput = completeDoc.WriteToMemory();

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        if (objSiniestro != null)
            response.AddHeader("Content-Disposition", String.Format("attachment; filename=ReporteSiniestro_" + objSiniestro.OperacionId + ".pdf; size={0}", fullOutput.Length.ToString()));
        else
            response.AddHeader("Content-Disposition", String.Format("attachment; filename=ReporteSiniestro" + "" + ".pdf; size={0}", fullOutput.Length.ToString()));
        response.BinaryWrite(fullOutput);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();

        completeDoc.Close();
        
    }



    private void GetFullReportPdf(int siniestroId)
    {
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
        {
            GetFullReportPdf_EVO(siniestroId);
            return;
        }
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
        {
            GetFullReportPdf_HIQ(siniestroId);
            return;
        }
    }

    private void GetSiniestroReportPdf(int siniestroId)
    {
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
        {
            GetSiniestroReportPdf_HIQ(siniestroId);
            return;
        }
    }
}