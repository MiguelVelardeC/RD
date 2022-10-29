using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.Caso.CasoForAprobation;
using Artexacta.App.Caso.CasoForAprobation.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Caso;
using Artexacta.App.Configuration;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Caso.BLL;
using Artexacta.App.CasoForAprobation;

public partial class CasoMedico_ListaCasoPorAprobar : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int DataGridPageSize = 20;
    private decimal MontoMinimoEnPoliza;
    private decimal PorcentajeSiniestralidadAlerta;

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchCasoMedico.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchCasoMedico.Config = new CasoAprobationSearch();
        MontoMinimoEnPoliza = Configuration.GetMontoMinimoEnPoliza();
        PorcentajeSiniestralidadAlerta = Configuration.GetPorcentajeSiniestralidadAlerta();
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    public void searchCtl_CasoMedico_OnSearch()
    {
        string sql = SearchCasoMedico.Sql;
        log.Debug("Parameter whereSql: " + sql);
        ActivePageHF.Value = "0";
        BindGrid();
    }

    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page CasoListaAprobacion.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SearchLB_Click(object sender, EventArgs e)
    {
        this.CasoRadGrid.Visible = true;
        CasoRadGrid.DataBind();
    }

    protected void CasoRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Aprobar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "CasoDetailsForAprobation")
            {
                try
                {
                    int Id = Convert.ToInt32(item.GetDataKeyValue("Id").ToString());
                    string Table = item.GetDataKeyValue("Table").ToString();

                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    int CasoId = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("CasoId").ToString());

                    if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                    {
                        CasoRadGrid.DataBind();
                        ExpandRowRadGrid(CasoId, CasoRadGrid.MasterTableView);
                        SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Aprobar el Detalle del Caso Medico");
                        return;
                    }

                    if (CasoForAprobationBLL.AproveCaso(Id, UserId, DateTime.Now, Table))
                    {
                        SystemMessages.DisplaySystemMessage("El detalle del caso medico fue aprobado correctamente.");

                        CasoRadGrid.DataBind();
                        ExpandRowRadGrid(CasoId, CasoRadGrid.MasterTableView);
                    }
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo aprobar el detalle del caso medico.");

                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al aprobar el detalle del caso medico.");
                    log.Error("Function CasoRadGrid_ItemCommand deleting on page ListaCasoPorAprobar.aspx", ex);
                }
            }
        }
        if (e.CommandName == "ExpandCollapse")
        {
            //GridDataItem item = (GridDataItem)e.Item;
            //string GastoIdExpandir = item.GetDataKeyValue("GastoId").ToString();
            //Expandir(GastoIdExpandir);
        }
    }

    protected void ExpandRowRadGrid(int CasoIdExpandir, GridTableView tableView)
    {
        foreach (GridDataItem item in tableView.Items)
        {
            int CasoId = Convert.ToInt32(item.GetDataKeyValue("CasoId").ToString());
            if (CasoId == CasoIdExpandir)
                if (!item.Expanded)
                    item.Expanded = true;
        }
    }
    protected void CasoRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "CasoDetailsForAprobation")
                try
                {
                    Caso objCaso = (Caso)item.OwnerTableView.ParentItem.DataItem;

                    //si es menor al MontoMinimoEnPoliza no permitir Aprobar Caso
                    decimal MontoLibrePaciente = objCaso.MontoTotal - objCaso.GastoTotal;

                    if (MontoLibrePaciente < MontoMinimoEnPoliza)
                    {
                        ImageButton AprobarCasoCommandColumn = (ImageButton)item["AprobarCasoCommandColumn"].Controls[0];
                        if (AprobarCasoCommandColumn != null)
                        {
                            AprobarCasoCommandColumn.Enabled = false;
                            AprobarCasoCommandColumn.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al verificar la alerta de siniestralidad del caso medico.");
                    log.Error("Function RecetaRadGrid_ItemDataBound on page ListaCasoPorAprobacion.aspx", ex);
                }
            else
                try
                {
                    Caso objCaso = (Caso)item.DataItem;

                    //si es menor al MontoMinimoEnPoliza no permitir Aprobar Caso
                    decimal MontoLibrePaciente = objCaso.MontoTotal - objCaso.GastoTotal;
                    if (MontoLibrePaciente < MontoMinimoEnPoliza)
                    {
                        item.CssClass = "MontoMinimoEnPolizaInferior";
                        Image AlertaImg = (Image)item.FindControl("AlertaImg");
                        if (AlertaImg != null)
                        {
                            AlertaImg.ImageUrl = "~/Images/Neutral/alert-red.gif";
                            AlertaImg.ToolTip = "El paciente no cuenta con el monto mínimo suficiente para Aprobar el Detalle del Caso Medico";
                            AlertaImg.Visible = true;
                        }
                    }

                    else
                    {
                        decimal PorcentajeSiniestralidad = (objCaso.GastoTotal / objCaso.MontoTotal * 100);
                        

                        if (PorcentajeSiniestralidad > PorcentajeSiniestralidadAlerta)
                        {
                            item.CssClass = "PorcentajeSiniestralidadAlerta";
                            Image AlertaImg = (Image)item.FindControl("AlertaImg");
                            if (AlertaImg != null)
                                AlertaImg.Visible = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al verificar la alerta de siniestralidad del caso medico.");
                    log.Error("Function RecetaRadGrid_ItemDataBound on page ListaCasoPorAprobacion.aspx", ex);
                }
        }
    }
    protected void CasoRadGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        e.DetailTableView.DataSource = CasoForAprobationBLL.GetCasoListForAprobation((int)dataItem.GetDataKeyValue("CasoId"));
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
            if (string.IsNullOrWhiteSpace(ClienteDDL.SelectedValue))
            {
                ClienteDDL.DataBind();
            }
            int _clienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
            _totalRows = CasoBLL.SearchCasoForAprobation(_cache, DataGridPageSize, firstRow, SearchCasoMedico.Sql, _clienteId, "");
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
}