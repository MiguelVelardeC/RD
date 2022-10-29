using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Reportes;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;

public partial class Reportes_ReporteEmergencia : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchEmergencia.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchEmergencia.Config = new EmergenciaSearch();

        if (!IsPostBack)
        {
            try
            {
                int currentYear = DateTime.Now.Year;

                FechaIni.SelectedDate = new DateTime(currentYear, 1, 1);
                FechaFin.SelectedDate = new DateTime((currentYear + 1), 1, 1).AddDays(-1);
            }
            catch (Exception q)
            {
                log.Error("Error in Page_Load in MainPage.aspx", q);
            }
            loadSaveSearch();
            if (!string.IsNullOrWhiteSpace(ModeHF.Value))
            {
                SearchEmergencia.Query = ModeHF.Value;
            }
        }
    }
    public void searchCtl_CasoMedico_OnSearch ()
    {
        string sql = SearchEmergencia.Sql;
        log.Debug("Parameter whereSql: " + sql);
        DataBindRadGrid();
    }
    private void loadSaveSearch ()
    {
        if (HttpContext.Current.Request.Cookies["ReporteEmergenciaSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("ReporteEmergenciaSearch");
            string SearchSiniestrosQuery = SearchEmergencia.Query;
            try
            {
                SearchEmergencia.Query = myCookie["QUERY"];
                ClienteDDL.SelectedValue = myCookie["CLIENTEID"];
            }
            catch
            {
                SearchEmergencia.Query = SearchSiniestrosQuery;
            }
        }
    }

    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["ReporteEmergenciaSearch"] != null)
        {
            myCookie = Request.Cookies.Get("ReporteEmergenciaSearch");
        }
        else
        {
            myCookie = new HttpCookie("ReporteEmergenciaSearch");
        }
        myCookie["QUERY"] = SearchEmergencia.Query;
        myCookie["CLIENTEID"] = ClienteDDL.SelectedValue;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void ClienteDDL_SelectedIndexChanged ( object sender, EventArgs e )
    {
        DataBindRadGrid();
    }
    protected void PacienteRadComboBox_SelectedIndexChanged ( object sender, EventArgs e )
    {
        DataBindRadGrid();
    }
    protected void MedicoRadComboBox_SelectedIndexChanged ( object sender, EventArgs e )
    {
        DataBindRadGrid();
    }
    protected void DataBindRadGrid()
    {
        EmergenciaRadGrid.DataBind();
    }
    protected void EmergenciaRadGrid_ExcelExportCellFormatting ( object sender, ExcelExportCellFormattingEventArgs e )
    {
        e.Cell.Text = e.Cell.Text.ToUpper();
        if (e.FormattedColumn.UniqueName.StartsWith("Decimal"))
        {
            string decimalSimbol = DecimalSimbolHF.Value;
            e.Cell.Style["mso-number-format"] = @"\#\,\#\#0\.00";
            e.Cell.Text = e.Cell.Text.Replace(decimalSimbol == "," ? "." : ",", decimalSimbol);
        } else if (e.FormattedColumn.DataType == typeof(string))
        {
            e.Cell.Style["mso-number-format"] = @"\@";
        }
    }
    protected void export_Click ( object sender, EventArgs e )
    {
        foreach (GridColumn col in EmergenciaRadGrid.Columns)
        {
            col.Visible = true;
        }
        EmergenciaRadGrid.MasterTableView.ExportToExcel();
    }

    protected void ReporteEmergenciaODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el Reporte de Emergencia.");
            log.Error("Function ReporteEmergenciaODS_Selected on page ReporteEmergencia.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page ReporteEmergencia.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void EmergenciaRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (((DateTime)DataBinder.Eval(item.DataItem, "FechaNacimiento")) < DateTime.Parse("01/01/1900"))
            {
                item["FechaNacimiento"].Text = "";
            }
            if (((DateTime)DataBinder.Eval(item.DataItem, "FechaCreacionGasto")) < DateTime.Parse("01/01/1900"))
            {
                item["FechaCreacionGasto"].Text = "";
            }
            if (((DateTime)DataBinder.Eval(item.DataItem, "FechaGasto")) < DateTime.Parse("01/01/1900"))
            {
                item["FechaGasto"].Text = "";
            }
        }
    }
}