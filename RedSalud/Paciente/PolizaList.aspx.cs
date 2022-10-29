using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.Poliza;

public partial class Poliza_PolizaList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static int DataGridPageSize = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserSearchControl.Config = new PolizaSearch();
        UserSearchControl.OnSearch += new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(UserSearchControl_OnSearch);
        if (!IsPostBack)
        {

            BindGrid();
        }
    }


    protected void UserSearchControl_OnSearch()
    {
        log.Debug("Binding GridView on Search");
        BindGrid();
    }
    
    protected void PolizaRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            try
            {
                int PolizaId = Convert.ToInt32(e.CommandArgument);
                Session["PolizaId"] = PolizaId.ToString();

                Response.Redirect("PolizaDetails.aspx");
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PolizaID in PolizaRadGrid_ItemCommand on page PolizaList.aspx", ex);
            }
        }
        //if (e.CommandName.Equals("Eliminar"))
        //{
        //    try
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;
        //        int PolizaId = Convert.ToInt32(item.GetDataKeyValue("PacienteId").ToString());

        //        PolizaBLL.Delete(PolizaId);
        //        SystemMessages.DisplaySystemMessage("Se elimino correctamente la Poliza seleccionado");
        //        PolizaRadGrid.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Error al obtener/Convertir PolizaID in PolizaRadGrid_ItemCommand on page PolizaList.aspx", ex);
        //        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la Poliza");
        //    }
        //}
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page PolizaList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    private void BindGrid()
    {
        // En esta variable almacenamos el total de filas en la base de datos.  No sólo las cargadas.
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los productos leidos aquí. 
        List<Poliza> _cache = new List<Poliza>();

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
            if (ClienteDDL.Items.Count <= 0)
            {
                ClienteDDL.DataBind();
            }
            if (ClienteDDL.SelectedValue == "")
            {
                ListItem select = ClienteDDL.Items[0];
                select.Selected = true;
            }
            string sqlWhere = "[A].[ClienteId] = " + ClienteDDL.SelectedValue + " AND " + UserSearchControl.Sql;
            _totalRows = PolizaBLL.GetPolizaBySearchByPolizaOrAseguradoOrPaciente(ref _cache, sqlWhere, DataGridPageSize, firstRow, "");

            // Update the labels that tell us what we did
            LoadedFirstRecordLabel.Text = (firstRow + 1).ToString();
            LoadedNumRecordsLabel.Text = (firstRow + _cache.Count).ToString();
            TotalDBRecordsLabel.Text = _totalRows.ToString();
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error al cargar los datos");
            return;
        }

        // Nos aseguramos que no pasemos la última página 
        if (_activePage > _totalRows / DataGridPageSize)
            _activePage = _totalRows / DataGridPageSize;

        // Grabar la página en la que estamos en el Viewstate
        ActivePageHF.Value = _activePage.ToString();

        // Registrar los índices de la primera y última fila cargada
        if (_cache.Count == 0)
        {
            FirstRowLoadedHF.Value = "0";
            LastRowLoadedHF.Value = "0";
        }
        else
        {
            FirstRowLoadedHF.Value = (_cache[0].RowNumber - 1).ToString();
            LastRowLoadedHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
        }

        // Registrar el total de filas leidas
        TotalRowsHF.Value = _totalRows.ToString();

        // Conectar el repeater con los datos cargados del Web
        PolizaRadGrid.DataSource = _cache;
        PolizaRadGrid.DataBind();

        if (_totalRows < DataGridPageSize)
        {
            ButtonsPanel.Visible = false;
            return;
        }

        // Actualizar el estado de habilitado los botones de navegación
        AnteriorButton.Enabled = true;
        AnteriorRapidoButton.Enabled = true;
        SiguienteButton.Enabled = true;
        SiguienteRapidoButton.Enabled = true;
        PrimeroButton.Enabled = true;
        UltimoButton.Enabled = true;

        // Si esamos en la primera, apagar los botones de primera, anterior y anterior -5
        if (_activePage == 0)
        {
            AnteriorButton.Enabled = false;
            AnteriorRapidoButton.Enabled = false;
            PrimeroButton.Enabled = false;
        }

        // Si estamos en la última fila, apagar los botones de ultima, siguiente y siguiente + 5
        decimal decActivePage = (decimal)_totalRows / (decimal)DataGridPageSize;
        decActivePage = ((int)decActivePage + ((decActivePage == (int)decActivePage) ? -1 : 0));
        if (_activePage == decActivePage)
        {
            SiguienteButton.Enabled = false;
            SiguienteRapidoButton.Enabled = false;
            UltimoButton.Enabled = false;
        }

        // Mostrar el de anterior -5 sólo si estamos pasada la 5
        if (_activePage < 5)
        {
            AnteriorRapidoButton.Enabled = false;
        }

        // Mostrar el de siguiente +5 sólo si faltan > 5 páginas para la última
        if (_activePage > _totalRows / DataGridPageSize - 5)
        {
            SiguienteRapidoButton.Enabled = false;
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
        int _totalFilas = Convert.ToInt32(TotalRowsHF.Value);
        decimal _activePage = (decimal)_totalFilas / (decimal)DataGridPageSize;

        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = ((int)_activePage + ((_activePage == (int)_activePage) ? -1 : 0)).ToString();

        // Desplegamos los datos
        BindGrid();
    }
}