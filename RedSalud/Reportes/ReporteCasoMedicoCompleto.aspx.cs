using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.User.BLL;
using Telerik.Web.UI;
using Artexacta.App.LoginSecurity;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedCliente;
using System.Text.RegularExpressions;
using Artexacta.App.Reportes;
using Artexacta.App.Reportes.BLL;

public partial class CasoMedico_ReporteCasoMedicoCompleto : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int DataGridPageSize = 20;

    protected void ProcessSessionParameters ()
    {
        if (Session["MODE"] != null && !string.IsNullOrEmpty(Session["MODE"].ToString()))
        {
            switch (Session["MODE"].ToString())
            {
                case "ENFERMERIA":
                        ModeHF.Value = "@TipoConsulta ENFER";
                    break;
                case "EMERGENCIA":
                        ModeHF.Value = "@TipoConsulta EMERG";
                    break;
            }
        }
        Session["MODE"] = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //GetProveedorId();
        SearchCasoMedico.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchCasoMedico.Config = new CasoMedicoSearch();

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
            ProcessSessionParameters();
            loadSaveSearch();
            if (!string.IsNullOrWhiteSpace(ModeHF.Value))
            {
                SearchCasoMedico.Query = ModeHF.Value;
            }
        }
    }
    public void searchCtl_CasoMedico_OnSearch ()
    {
        string sql = SearchCasoMedico.Sql;
        log.Debug("Parameter whereSql: " + sql);
        LocalDataBind();
    }
    private void loadSaveSearch ()
    {
        if (HttpContext.Current.Request.Cookies["ReporteCasoMedicoSearch"] != null)
        {
            HttpCookie myCookie = Request.Cookies.Get("ReporteCasoMedicoSearch");
            string SearchSiniestrosQuery = SearchCasoMedico.Query;
            int DataGridPageSizeTemp = DataGridPageSize;
            try
            {
                SearchCasoMedico.Query = myCookie["QUERY"];
                DataGridPageSize = int.Parse(myCookie["PAGESIZE"]);
                ClienteDDL.SelectedValue = myCookie["CLIENTEID"];
            }
            catch
            {
                SearchCasoMedico.Query = SearchSiniestrosQuery;
                DataGridPageSize = DataGridPageSizeTemp;
            }
        }
    }

    private void saveSearch ()
    {
        HttpCookie myCookie = null;
        if (HttpContext.Current.Request.Cookies["ReporteCasoMedicoSearch"] != null)
        {
            myCookie = Request.Cookies.Get("ReporteCasoMedicoSearch");
        }
        else
        {
            myCookie = new HttpCookie("ReporteCasoMedicoSearch");
        }
        myCookie["QUERY"] = SearchCasoMedico.Query;
        myCookie["PAGESIZE"] = DataGridPageSize.ToString();
        myCookie["CLIENTEID"] = ClienteDDL.SelectedValue;
        myCookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(myCookie);
    }
    protected void GetProveedorId()
    {
        try
        {
            this.ProveedorIdHF.Value = UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name).ToString();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener datos de su cuenta de Usuario.");
            log.Error("Function GetProveedorId on page CasoMedicoLista.aspx", ex);
        }
    }
    protected void LocalDataBind ()
    {
        int casos = (CasoMedicoCheckBox.Checked ? 1 : 0) +
                    (ReconsultaCheckBox.Checked ? 1 : 0) +
                    (EnfermeriaCheckBox.Checked ? 1 : 0) +
                    (EmergenciaCheckBox.Checked ? 1 : 0);
        /*CasoRadGrid.Columns.FindByUniqueName("MotivoConsultaId").Visible = (casos > 1);
        if (casos == 0)
            CasoMedicoCheckBox.Checked = true;
        if (string.IsNullOrEmpty(ClienteDDL.SelectedValue))
            ClienteDDL.DataBind();*/
        CasoRadGrid.DataBind();
    }

    protected void SearchLB_Click(object sender, EventArgs e)
    {
        LocalDataBind();
    }
    protected void ClienteDDL_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ClienteDDL.Items[0].Selected = true;
        }
    }
    protected void ReporteCasoMedicoODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el Reporte de Casos Médicos.");
            log.Error("Function ReporteCasoMedicoODS_Selected on page ReporteCasoMedico.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            List<CasoMedicoAtomicoSearch> casos = (List<CasoMedicoAtomicoSearch>)e.ReturnValue;
            int TotalCasos = 0;
            List<string> codigosCaso = new List<string>();
            foreach (CasoMedicoAtomicoSearch caso in casos)
            {
                string key = caso.MotivoConsultaId + caso.CodigoCaso;
                if (!codigosCaso.Contains(key))
                {
                    TotalCasos++;
                    codigosCaso.Add(key);
                }
            }
            CasoRadGrid.PagerStyle.AlwaysVisible = true;
            CasoRadGrid.PagerStyle.PageSizeLabelText = "TAMAÑO DE LA PÁGINA";
            CasoRadGrid.PagerStyle.PagerTextFormat = "{4} | PÁGINA {0} of {1} | " + TotalCasos + " CASOS MÉDICOS | ITEM {2} A {3} DE {5}";
        }
    }

    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page CasoMedicoLista.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CasoRadGrid_ExcelExportCellFormatting ( object sender, ExcelExportCellFormattingEventArgs e )
    {
        e.Cell.Text = e.Cell.Text.ToUpper();
        if (e.Cell.Text.StartsWith("SUM"))
        {
            string decimalSimbol = DecimalSimbolHF.Value;
            e.Cell.Style["mso-number-format"] = @"\#\,\#\#0\.00";
            e.Cell.Text = e.Cell.Text.ToUpper().Replace("SUM", "").Replace(":", "").Trim();
            e.Cell.Text = e.Cell.Text.Replace(decimalSimbol == "," ? "." : ",", decimalSimbol);
        } 
        else if (e.FormattedColumn.UniqueName.StartsWith("Decimal"))
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
        foreach (GridColumn col in CasoRadGrid.Columns)
        {
            col.Visible = true;
            if (col.UniqueName.StartsWith("Decimal"))
                ((GridBoundColumn)col).DataFormatString = "";
        }
        CasoRadGrid.MasterTableView.ExportToExcel();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        SearchCasoMedico.Query = this.BuildQuery();
        CasoRadGrid.DataBind();
    }

    private string BuildQuery()
    {
        string query = "";


        if(!string.IsNullOrEmpty(query) && !string.IsNullOrWhiteSpace(query)){
            
            if (!string.IsNullOrEmpty(txtPoliza.Text))
            {
                string checkNroPoliza = @"@NUMEROPOLIZA";
                if (!query.Contains(checkNroPoliza))
                {
                    query += @" AND @NUMEROPOLIZA "+txtPoliza.Text;
                }
            }


        }else{

            if (!string.IsNullOrEmpty(txtPoliza.Text))
            {
                string checkNroPoliza = @"@NUMEROPOLIZA";
                if (!query.Contains(checkNroPoliza))
                {
                    query += @" @NUMEROPOLIZA " + txtPoliza.Text;
                }
            }
        }


        return query;
    }
}