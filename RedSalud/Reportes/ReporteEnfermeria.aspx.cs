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

public partial class Reportes_ReporteEnfermeria : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchEnfermeria.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchEnfermeria.Config = new EnfermeriaSearch();

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
                SearchEnfermeria.Query = ModeHF.Value;
            }
        }
    }
    public void searchCtl_CasoMedico_OnSearch ()
    {
        string sql = SearchEnfermeria.Sql;
        log.Debug("Parameter whereSql: " + sql);
        GridsDataBind();
    }
    private void loadSaveSearch ()
    {
        if (HttpContext.Current.Request.Cookies["ReporteEnfermeriaSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("ReporteEnfermeriaSearch");
            string SearchSiniestrosQuery = SearchEnfermeria.Query;
            try
            {
                SearchEnfermeria.Query = myCookie["QUERY"];
                ClienteDDL.SelectedValue = myCookie["CLIENTEID"];
            }
            catch
            {
                SearchEnfermeria.Query = SearchSiniestrosQuery;
            }
        }
    }

    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["ReporteEnfermeriaSearch"] != null)
        {
            myCookie = Request.Cookies.Get("ReporteEnfermeriaSearch");
        }
        else
        {
            myCookie = new HttpCookie("ReporteEnfermeriaSearch");
        }
        myCookie["QUERY"] = SearchEnfermeria.Query;
        myCookie["CLIENTEID"] = ClienteDDL.SelectedValue;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void ClienteDDL_SelectedIndexChanged ( object sender, EventArgs e )
    {
        GridsDataBind();
    }
    protected void PacienteRadComboBox_SelectedIndexChanged ( object sender, EventArgs e )
    {
        GridsDataBind();
    }
    protected void MedicoRadComboBox_SelectedIndexChanged ( object sender, EventArgs e )
    {
        GridsDataBind();
    }
    protected void GridsDataBind ()
    {
        EnfermeriaRadGrid.DataBind();
    }
    protected void EnfermeriaRadGrid_ExcelExportCellFormatting ( object sender, ExcelExportCellFormattingEventArgs e )
    {
        e.Cell.Text = e.Cell.Text.ToUpper();
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
    }
    protected void export_Click ( object sender, EventArgs e )
    {
        foreach (GridColumn col in EnfermeriaRadGrid.Columns)
        {
            col.Visible = true;
        }
        EnfermeriaRadGrid.MasterTableView.ExportToExcel();
    }

    protected void ReporteEnfermeriaODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el Reporte de Enfermeria.");
            log.Error("Function ReporteEnfermeriaODS_Selected on page ReporteEnfermeria.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page ReporteEnfermeria.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}