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

public partial class Reportes_ReporteOdontologia : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchOdontologia.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchOdontologia.Config = new OdontologiaSearch();

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
                SearchOdontologia.Query = ModeHF.Value;
            }
            DataBindGrid();
        }
    }
    public void searchCtl_CasoMedico_OnSearch ()
    {
        string sql = SearchOdontologia.Sql;
        log.Debug("Parameter whereSql: " + sql);
        DataBindGrid();
    }
    private void loadSaveSearch ()
    {
        if (HttpContext.Current.Request.Cookies["ReporteOdontologiaSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("ReporteOdontologiaSearch");
            string SearchSiniestrosQuery = SearchOdontologia.Query;
            try
            {
                SearchOdontologia.Query = myCookie["QUERY"];
                ClienteDDL.SelectedValue = myCookie["CLIENTEID"];
            }
            catch
            {
                SearchOdontologia.Query = SearchSiniestrosQuery;
            }
        }
    }

    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["ReporteOdontologiaSearch"] != null)
        {
            myCookie = Request.Cookies.Get("ReporteOdontologiaSearch");
        }
        else
        {
            myCookie = new HttpCookie("ReporteOdontologiaSearch");
        }
        myCookie["QUERY"] = SearchOdontologia.Query;
        myCookie["CLIENTEID"] = ClienteDDL.SelectedValue;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void ClienteDDL_SelectedIndexChanged ( object sender, EventArgs e )
    {
        DataBindGrid();
    }
    protected void PacienteRadComboBox_SelectedIndexChanged ( object sender, EventArgs e )
    {
        DataBindGrid();
    }
    protected void MedicoRadComboBox_SelectedIndexChanged ( object sender, EventArgs e )
    {
        DataBindGrid();
    }
    protected void DataBindGrid ()
    {
        OdontologiaRadGrid.DataBind();
    }
    protected void OdontologiaRadGrid_ExcelExportCellFormatting ( object sender, ExcelExportCellFormattingEventArgs e )
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
        foreach (GridColumn col in OdontologiaRadGrid.Columns)
        {
            col.Visible = true;
        }
        OdontologiaRadGrid.MasterTableView.ExportToExcel();
    }

    protected void ReporteOdontologiaODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el Reporte de Odontologia.");
            log.Error("Function ReporteOdontologiaODS_Selected on page ReporteOdontologia.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            List<Odontologia> casos = (List<Odontologia>)e.ReturnValue;
            int TotalCasos = 0;
            List<string> codigosCaso = new List<string>();
            foreach (Odontologia caso in casos)
            {
                if (!codigosCaso.Contains(caso.CodigoCaso))
                {
                    TotalCasos++;
                    codigosCaso.Add(caso.CodigoCaso);
                }
            }
            OdontologiaRadGrid.PagerStyle.AlwaysVisible = true;
            OdontologiaRadGrid.PagerStyle.PageSizeLabelText = "TAMAÑO DE LA PÁGINA";
            OdontologiaRadGrid.PagerStyle.PagerTextFormat = "{4} | PÁGINA {0} of {1} | " + TotalCasos + " CASOS MÉDICOS | ITEM {2} A {3} DE {5}";
        }
    }
    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page ReporteOdontologia.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void OdontologiaRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (((DateTime)DataBinder.Eval(item.DataItem, "FechaNacimiento")) < DateTime.Parse("01/01/1900"))
            {
                item["FechaNacimiento"].Text = "";
            }
            /*
            if (((DateTime)DataBinder.Eval(item.DataItem, "FechaCreacionGasto")) < DateTime.Parse("01/01/1900"))
            {
                item["FechaCreacionGasto"].Text = "";
            }
            if (((DateTime)DataBinder.Eval(item.DataItem, "FechaGasto")) < DateTime.Parse("01/01/1900"))
            {
                item["FechaGasto"].Text = "";
            }
            */
        }
    }
}