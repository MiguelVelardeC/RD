using Artexacta.App.Chart;
using Artexacta.App.SOATEstadisticas.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class SOAT_SOATDashboard : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

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

    private int Gestion
    {
        get
        {
            return int.Parse(GestionCombo.SelectedValue);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int currentYear = DateTime.Now.Year;

                for (int i = Artexacta.App.Constants.FIRST_YEAR_REDSALUD; i <= currentYear; i++)
                {
                    //GestionCombo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(i.ToString(), i.ToString()));
                    GestionCombo.Items.Add(new ListItem(i.ToString()));
                }
                GestionCombo.Items.FindByValue(currentYear.ToString()).Selected = true;
            }
            catch (Exception q)
            {
                log.Error("Error in Page_Load in MainPage.aspx", q);
            }
            BindGrid();
        }
    }
    private void BindGrid()
    {
        ReportesPages.Visible = MayorSiniestralidadDiv.Visible = RadTabStrip1.Visible = (ClienteId > 0);
        if (ReportesPages.Visible)
        {

            EstadisticasDSTableAdapters.SiniestroPagadoXLugarTableAdapter adapterLugar =
                new EstadisticasDSTableAdapters.SiniestroPagadoXLugarTableAdapter();

            SIniestroPagadoXDptoGrid.DataSource = adapterLugar.GetSiniestroPagadoXLugar(Gestion, ClienteId);
            SIniestroPagadoXDptoGrid.DataBind();

            EstadisticasDSTableAdapters.SiniestroPagadoXSectorTableAdapter adapterSector =
                new EstadisticasDSTableAdapters.SiniestroPagadoXSectorTableAdapter();

            SiniestroPagadoXSectorGrid.DataSource = adapterSector.GetSiniestroPagadoXSector(Gestion, ClienteId);
            SiniestroPagadoXSectorGrid.DataBind();

            EstadisticasDSTableAdapters.SiniestroPagadoXTipoXMesTableAdapter adapterTipoMes =
                new EstadisticasDSTableAdapters.SiniestroPagadoXTipoXMesTableAdapter();

            SiniestroPAgadoXTipoXMesGrid.DataSource = adapterTipoMes.GetSiniestroPagadoXTipoXMes(Gestion, ClienteId);
            SiniestroPAgadoXTipoXMesGrid.DataBind();

            EstadisticasDSTableAdapters.SiniestroPagadoXTipoXVehiculoTableAdapter adapterTipoVehiculo =
                new EstadisticasDSTableAdapters.SiniestroPagadoXTipoXVehiculoTableAdapter();

            SiniestroPAgadoXTipoXVehiculoGrid.DataSource = adapterTipoVehiculo.GetSIniestroPagadoXTipoXVehiculo(Gestion, ClienteId);
            SiniestroPAgadoXTipoXVehiculoGrid.DataBind();

            SiniestroPagadoXTipoXSectorGrid.DataSource = adapterTipoVehiculo.GetSiniestroPagadoXTipoXSector(Gestion, ClienteId);
            SiniestroPagadoXTipoXSectorGrid.DataBind();

            EstadisticasDSTableAdapters.CantidadesXMesTableAdapter adapterCantidad =
                new EstadisticasDSTableAdapters.CantidadesXMesTableAdapter();

            CantidadAccidentadosxMesGrid.DataSource = adapterCantidad.GetCantidadesXMes(Gestion, ClienteId);
            CantidadAccidentadosxMesGrid.DataBind();

            EstadisticasDSTableAdapters.SiniestrosXSectorTableAdapter adapterXSector =
                new EstadisticasDSTableAdapters.SiniestrosXSectorTableAdapter();

            CantidadesXSectorXMesGrid.DataSource = adapterXSector.GetCantidadesXSectorXMes(Gestion, ClienteId);
            CantidadesXSectorXMesGrid.DataBind();

            CantidadesXSectorXDptoGrid.DataSource = adapterXSector.GetCantidadesXSectorXDpto(Gestion, ClienteId);
            CantidadesXSectorXDptoGrid.DataBind();

            CantidadesXSectorXTipoGrid.DataSource = adapterXSector.GetCantidadesXSectorXTipo(Gestion, ClienteId);
            CantidadesXSectorXTipoGrid.DataBind();

            RenderEstadisticasTotales();
            //RenderEstadisticasXSector();
            //RenderEstadisticasXVehiculo();
            //RenderEstadisticasXGastos();
            //RenderEstadisticasXLugar();
            //RenderEstadisticasXMes();

            ReadAndDisplay20MayorSiniestralidad();
        }
    }

    private void ReadAndDisplay20MayorSiniestralidad()
    {
        EstadisticasDSTableAdapters.StatsX20MasSiniestralidadTableAdapter adapter =
            new EstadisticasDSTableAdapters.StatsX20MasSiniestralidadTableAdapter();

        try
        {
            EstadisticasDS.StatsX20MasSiniestralidadDataTable theTable = adapter.GetStats20SiniestralidadAprobados(Gestion, ClienteId);
            if (theTable != null)
            {
                MayorSiniestralidadRadGrid.DataSource = theTable;
                MayorSiniestralidadRadGrid.DataBind();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void RenderEstadisticasXMes()
    {
        string xml = "";
        FusionChart fchart = null;
        try
        {
            List<DataSeries> dataPoints = ChartBuilder.GetSOATXMes(Gestion, ClienteId);
            ChartObject objChart =
                ChartObject.NewChart("Siniestralidad x Mes (%)", "", DateTime.Now, ChartType.TypeOfChart.BARCHART2AXIS);
            objChart.TheChartData.SetOfData = dataPoints;

            fchart = new FCColumnChart();
            fchart.TheChart = objChart;

            xml = fchart.GetXMLData();
        }
        catch (Exception q)
        {
            log.Error("Cannot get XML for chart", q);

            SystemMessages.DisplaySystemErrorMessage(Resources.Chart.CorruptedMissingDataMessage);

            return;
        }

        StringBuilder script = new StringBuilder();
        string path = VirtualPathUtility.ToAbsolute("~/FusionCharts/Bar2D.swf");
        script.Append("<script type=\"text/javascript\">\n");

        //script.Append("  var my" + ChartPanelSOATxMes.ClientID + " = new FusionCharts(\"" + path + "\",\n");
        //script.Append("    \"c" + ChartPanelSOATxMes.ClientID + "\", \"100%\", \"100%\", \"0\", \"1\");\n");
        //script.Append("  my" + ChartPanelSOATxMes.ClientID + ".setDataXML(\"" + xml + "\");\n");
        //script.Append("  my" + ChartPanelSOATxMes.ClientID + ".render(\"" + ChartPanelSOATxMes.ClientID + "\");\n");

        //script.Append("</script>\n");

        //FusionChartScriptSOATxMes.Text = script.ToString();
    }

    private void RenderEstadisticasXLugar()
    {
        string xml = "";
        FusionChart fchart = null;
        try
        {
            List<DataSeries> dataPoints = ChartBuilder.GetSOATXLugar(Gestion, ClienteId);
            ChartObject objChart =
                ChartObject.NewChart("Siniestralidad x Ciudad (%)", "", DateTime.Now, ChartType.TypeOfChart.COLUMNCHART);
            objChart.TheChartData.SetOfData = dataPoints;

            fchart = new FCColumnChart();
            fchart.TheChart = objChart;

            xml = fchart.GetXMLData();
        }
        catch (Exception q)
        {
            log.Error("Cannot get XML for chart", q);

            SystemMessages.DisplaySystemErrorMessage(Resources.Chart.CorruptedMissingDataMessage);

            return;
        }

        StringBuilder script = new StringBuilder();
        string path = VirtualPathUtility.ToAbsolute("~/FusionCharts/Column2D.swf");
        script.Append("<script type=\"text/javascript\">\n");

        //script.Append("  var my" + ChartPanelSOATxLugar.ClientID + " = new FusionCharts(\"" + path + "\",\n");
        //script.Append("    \"c" + ChartPanelSOATxLugar.ClientID + "\", \"100%\", \"100%\", \"0\", \"1\");\n");
        //script.Append("  my" + ChartPanelSOATxLugar.ClientID + ".setDataXML(\"" + xml + "\");\n");
        //script.Append("  my" + ChartPanelSOATxLugar.ClientID + ".render(\"" + ChartPanelSOATxLugar.ClientID + "\");\n");

        //script.Append("</script>\n");

        //FusionChartScriptSOATxLugar.Text = script.ToString();
    }

    private void RenderEstadisticasXGastos()
    {
        string xml = "";
        FusionChart fchart = null;
        try
        {
            List<DataSeries> dataPoints = ChartBuilder.GetSOATXGastos(Gestion, ClienteId);
            ChartObject objChart =
                ChartObject.NewChart("Gastos x Tipo (BOB)", "", DateTime.Now, ChartType.TypeOfChart.BARCHART2AXIS);
            objChart.TheChartData.SetOfData = dataPoints;

            fchart = new FCColumnChart();
            fchart.TheChart = objChart;

            xml = fchart.GetXMLData();
        }
        catch (Exception q)
        {
            log.Error("Cannot get XML for chart", q);

            SystemMessages.DisplaySystemErrorMessage(Resources.Chart.CorruptedMissingDataMessage);

            return;
        }

        StringBuilder script = new StringBuilder();
        string path = VirtualPathUtility.ToAbsolute("~/FusionCharts/Bar2D.swf");
        script.Append("<script type=\"text/javascript\">\n");

        //script.Append("  var my" + ChartPanelSOATGastosxTipo.ClientID + " = new FusionCharts(\"" + path + "\",\n");
        //script.Append("    \"c" + ChartPanelSOATGastosxTipo.ClientID + "\", \"100%\", \"100%\", \"0\", \"1\");\n");
        //script.Append("  my" + ChartPanelSOATGastosxTipo.ClientID + ".setDataXML(\"" + xml + "\");\n");
        //script.Append("  my" + ChartPanelSOATGastosxTipo.ClientID + ".render(\"" + ChartPanelSOATGastosxTipo.ClientID + "\");\n");

        //script.Append("</script>\n");

        //FusionChartScriptSOATGastosxTipo.Text = script.ToString();
    }

    private void RenderEstadisticasXVehiculo()
    {
        string xml = "";
        FusionChart fchart = null;
        try
        {
            List<DataSeries> dataPoints = ChartBuilder.GetSOATXVehiculo(Gestion, ClienteId);
            ChartObject objChart =
                ChartObject.NewChart("Siniestralidad por tipo de vehiculo (%)", "", DateTime.Now, ChartType.TypeOfChart.COLUMNCHART);
            objChart.TheChartData.SetOfData = dataPoints;

            fchart = new FCColumnChart();
            fchart.TheChart = objChart;

            xml = fchart.GetXMLData();
        }
        catch (Exception q)
        {
            log.Error("Cannot get XML for chart", q);

            SystemMessages.DisplaySystemErrorMessage(Resources.Chart.CorruptedMissingDataMessage);

            return;
        }

        StringBuilder script = new StringBuilder();
        string path = VirtualPathUtility.ToAbsolute("~/FusionCharts/Column2D.swf");
        script.Append("<script type=\"text/javascript\">\n");

        //script.Append("  var my" + ChartPanelSOATxVehiculo.ClientID + " = new FusionCharts(\"" + path + "\",\n");
        //script.Append("    \"c" + ChartPanelSOATxVehiculo.ClientID + "\", \"100%\", \"100%\", \"0\", \"1\");\n");
        //script.Append("  my" + ChartPanelSOATxVehiculo.ClientID + ".setDataXML(\"" + xml + "\");\n");
        //script.Append("  my" + ChartPanelSOATxVehiculo.ClientID + ".render(\"" + ChartPanelSOATxVehiculo.ClientID + "\");\n");

        //script.Append("</script>\n");

        //FusionChartScriptSOATxVehiculo.Text = script.ToString();
    }

    private void RenderEstadisticasXSector()
    {
        string xml = "";
        FusionChart fchart = null;
        try
        {
            List<DataSeries> dataPoints = ChartBuilder.GetSOATXSector(Gestion, ClienteId);
            ChartObject objChart =
                ChartObject.NewChart("Siniestralidad por sector (%)", "", DateTime.Now, ChartType.TypeOfChart.BARCHART2AXIS);
            objChart.TheChartData.SetOfData = dataPoints;

            fchart = new FCBarChart();
            fchart.TheChart = objChart;

            xml = fchart.GetXMLData();
        }
        catch (Exception q)
        {
            log.Error("Cannot get XML for chart", q);

            SystemMessages.DisplaySystemErrorMessage(Resources.Chart.CorruptedMissingDataMessage);

            return;
        }

        StringBuilder script = new StringBuilder();
        string path = VirtualPathUtility.ToAbsolute("~/FusionCharts/Bar2D.swf");
        script.Append("<script type=\"text/javascript\">\n");

        //script.Append("  var my" + ChartPanelSOATxSector.ClientID + " = new FusionCharts(\"" + path + "\",\n");
        //script.Append("    \"c" + ChartPanelSOATxSector.ClientID + "\", \"100%\", \"100%\", \"0\", \"1\");\n");
        //script.Append("  my" + ChartPanelSOATxSector.ClientID + ".setDataXML(\"" + xml + "\");\n");
        //script.Append("  my" + ChartPanelSOATxSector.ClientID + ".render(\"" + ChartPanelSOATxSector.ClientID + "\");\n");

        //script.Append("</script>\n");

        //FusionChartScriptSOATxSector.Text = script.ToString();
    }

    private void RenderEstadisticasTotales()
    {
        int siniestros = 0;
        int accidentados = 0;
        int fallecidos = 0;

        try
        {
            SOATEstadisticasBLL.GetSOATEstadisticasTotales(ClienteId, Gestion, ref siniestros,ref accidentados, ref fallecidos);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener estadisticas totales.");
            log.Error("Error reading the survey statistics from DB", q);
            return;
        }

        string xml = "";
        FusionChart fchart = null;
        try
        {
            List<DataSeries> dataPoints = ChartBuilder.GetSOATTotales(siniestros, accidentados, fallecidos);
            ChartObject objChart =
                ChartObject.NewChart("TOTALES", "", DateTime.Now, ChartType.TypeOfChart.BARCHART2AXIS);
            objChart.TheChartData.SetOfData = dataPoints;

            fchart = new FCBarChart();
            fchart.TheChart = objChart;

            xml = fchart.GetXMLData();
        }
        catch (Exception q)
        {
            log.Error("Cannot get XML for chart", q);

            SystemMessages.DisplaySystemErrorMessage(Resources.Chart.CorruptedMissingDataMessage);

            return;
        }

        StringBuilder script = new StringBuilder();
        string path = VirtualPathUtility.ToAbsolute("~/FusionCharts/Bar2D.swf");
        script.Append("<script type=\"text/javascript\">\n");

        script.Append("  var my" + ChartPanelSOATTotales.ClientID + " = new FusionCharts(\"" + path + "\",\n");
        script.Append("    \"c" + ChartPanelSOATTotales.ClientID + "\", \"100%\", \"100%\", \"0\", \"1\");\n");
        script.Append("  my" + ChartPanelSOATTotales.ClientID + ".setDataXML(\"" + xml + "\");\n");
        script.Append("  my" + ChartPanelSOATTotales.ClientID + ".render(\"" + ChartPanelSOATTotales.ClientID + "\");\n");

        script.Append("</script>\n");

        FusionChartScriptSOATTotales.Text = script.ToString();
    }
    protected void MayorSiniestralidadRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }
    protected void DetailsImageButton_Command(object sender, CommandEventArgs e)
    {
        string[] siniestroAccidentado = null;
        ImageButton btn = (ImageButton)sender;
        char[] separator = {','};
        siniestroAccidentado = btn.CommandArgument.Split(separator);

        bool todoOk = false;
        try
        {
            int siniestroId = Convert.ToInt32(siniestroAccidentado[0]);
            int accidentadoId = Convert.ToInt32(siniestroAccidentado[1]);

            Session["SiniestroId"] = siniestroId;
            Session["AccidentadoId"] = accidentadoId;

            todoOk = true;
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Hubo un error al leer el siniestro y el accidentado, cargue la página");
            log.Error("Error reading siniestroid y accidentadoId", q);
        }

        if (todoOk)
            Response.Redirect("SOATWizard.aspx");
    }
    private decimal totales = 0;
    protected void SumaTotales_ItemDataBound ( object sender, Telerik.Web.UI.GridItemEventArgs e )
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
        {
            totales = 0;
        }
        else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            decimal total = 0;
            foreach (TableCell cell in e.Item.Cells)
            {
                try
                {
                    total += decimal.Parse(cell.Text.Replace(".", ""));
                }
                catch { }
            }
            ((GridDataItem)e.Item)["TOTALES"].Text = total.ToString("#,##0.00");
            totales += total;
        }
        else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Footer)
        {
            ((GridFooterItem)e.Item)["TOTALES"].Text = totales.ToString("#,##0.00");
        }
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page SOATDashboard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}