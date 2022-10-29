using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Artexacta.App.Utilities.Bitacora;
using Telerik.Web.UI;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.Siniestralidad.BLL;
using Artexacta.App.Siniestralidad;

public partial class Siniestralidad_ReporteSiniestralidadDetail : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private List<string> userPermissions;
    private static Bitacora theBitacora = new Bitacora();
    private int DataGridPageSize = 20;
    private int PolizaId = 0;
    private int idCliente = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {

                lblFechaForm.Text = DateTime.Now.ToShortDateString();
                ProcessSessionParameters();

            }
            catch (Exception q)
            {
                log.Error("No se puede mostrar la pagina", q);

                SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
                //   Response.Redirect("~/MainPage.aspx");
                return;
            }
        }
    }

    private void ProcessSessionParameters()
    {
        string SessionCaso;
        if (Session["TipoPoliza"] != null && !string.IsNullOrEmpty(Session["TipoPoliza"].ToString()))
        {
            try
            {
                SessionCaso = Convert.ToString(Session["TipoPoliza"]);
               // CommandArgument = '<%# Eval("CasoId") +";"+ Eval("NombreCliente") +";"+ Eval("CedulaIdentidad") %> '


                char separador = ';'; // separador de datos
                string[] arregloDeSubCadenas = SessionCaso.Split(separador);
                ClienteIdHF.Value = arregloDeSubCadenas[1].ToString();
                PolizaIdHF.Value = arregloDeSubCadenas[0].ToString();
                PolizaId =int.Parse(arregloDeSubCadenas[0].ToString());
                idCliente = int.Parse(arregloDeSubCadenas[1].ToString());
                SiniestralidadSearchPolizaDetail objbusqueda = SiniestralidadBLL.SearchSiniestralidadPolizaXCliente(PolizaId, idCliente);
                NumeroPoliza.Text = objbusqueda.NumeroPoliza ;
                NombrePaciente.Text = objbusqueda.NombrePaciente ;
                Carnetidentidad.Text = objbusqueda.CedulaIdentidad;
                string fIniPoliza = arregloDeSubCadenas[3].ToString();
                string fFinPoliza = arregloDeSubCadenas[4].ToString();
                string fIniSearch = arregloDeSubCadenas[5].ToString();
                string fFinSearch = arregloDeSubCadenas[6].ToString();
                FechaIni.Text = (String.Format("{0:dd/MM/yyyy}", DateTime.Parse((string.IsNullOrEmpty(fFinSearch)) ? fIniPoliza : fIniSearch)) ?? "");
                FechaFin.Text = (String.Format("{0:dd/MM/yyyy}", DateTime.Parse((string.IsNullOrEmpty(fFinSearch)) ? fFinPoliza : fFinSearch)) ?? "");
                GestionLabel.Text = arregloDeSubCadenas[3].ToString();
                NombrePoliza.Text = objbusqueda.NombrePoliza;

                BindGrid();
            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID de la Poliza para Siniestralidad");
                log.Error("no se pudo realizar la conversion de la session de Reportes Siniestralidad IdPoliza:" + Session["TipoPoliza"]);
                //   Response.Redirect("~/MainPage.aspx");
                return;
            }

        }

        // Session["TipoCaso"] = null;
    }
    private void BindGrid()
    {
        try {
            // Ponemos los Prestaciones leidos aquí. 
            List<SiniestralidadDetail> _cache = new List<SiniestralidadDetail>();


            // Obtener los datos de la BD
            try
            {
                //aqui controlamos lo queremos buscar

                string fechaIni = (String.Format("{0:yyyy/MM/dd}", DateTime.Parse(FechaIni.Text)) ?? "");
                string fechaFin = (String.Format("{0:yyyy/MM/dd}", DateTime.Parse(FechaFin.Text)) ?? "");
                int _totalRows = SiniestralidadBLL.GetReporteSiniestralidadDetail(_cache, idCliente, PolizaId, fechaIni, fechaFin);


            }
            catch (Exception q)
            {
                log.Error("No se puedo cargar los datos de la BD", q);
                SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Reporte de Siniestralidad Por Poliza");
            }



            //cargamos datos al radgrid y a los textbox de montotope,montoacumulado y habilitado

            ReporteSiniestralidadDetailGrid.DataSource = _cache;
            ReporteSiniestralidadDetailGrid.DataBind();
            MontoTopeProducto.Text = _cache[_cache.Count - 1].MontoTope.ToString();
           // MontoTopeProducto.Text = string.Format("{0:n2}", (Math.Truncate(_cache[_cache.Count - 1].MontoTope * 100) / 100));
            MontoAcumuladoGestion.Text = _cache[_cache.Count - 1].MontoAcumulado.ToString();
            double CantidadConsultasxGestion = double.Parse(_cache[_cache.Count - 1].ConsultasPorAnos.ToString());
            double CantidadConsultaAcumuladaxGestion = double.Parse(_cache[_cache.Count - 1].ConsultasAcumuladas.ToString());
            if (double.Parse(MontoTopeProducto.Text) > double.Parse(MontoAcumuladoGestion.Text)
                & CantidadConsultasxGestion>CantidadConsultaAcumuladaxGestion )
                Estado.Text = "Habilitado";
            else
                Estado.Text = "No Habilitado";
        }
        catch(Exception e)
        {

        }
    }
    protected void ReporteSiniestralidadDetailGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;

        }
    }

    protected void ReporteSiniestralidadDetailGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
    }

    protected void export_Excel(object sender, EventArgs e)
    {
        try
        {
            ///  RadAjaxManager1.EnableAJAX = false;
            ExportarExcel();

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }
    private void ExportarExcel()
    {
        try
        {
            // aqui creamos un objeto de radgrid para no utilizar el que esta en la pantalla y descargar todos los resultados de la busqueda
            RadGrid RadGrid1 = new RadGrid();
            RadGrid1.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Data selected using dates between 1 Jan 2011 to 1 Sep 2011</span>"));
            RadGrid1.ID = "ReporteSiniestralidadDetailGrid";
            //RadGrid1.MasterTableView.DataKeyNames = new string[] { "CasoId" };
            RadGrid1.MasterTableView.AutoGenerateColumns = false;
            RadGrid1.MasterTableView.CommandItemSettings.ShowExportToExcelButton = true;
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            RadGrid1.ExportSettings.ExportOnlyData = true;

            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NombrePrestacion";
            boundColumn.HeaderText = "NOMBRE DE PRESTACION";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.ItemStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.ItemStyle.BackColor = System.Drawing.Color.LightGray;
            boundColumn.HeaderStyle.Width = 200;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "MontoTope";
            boundColumn.HeaderText = "MONTO TOPE";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 150;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "MontoAcumulado";
            boundColumn.HeaderText = "MONTO ACUMULADO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 200;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "CoPagoAcumulado";
            boundColumn.HeaderText = "COPAGO ACUMULADO";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 200;
            boundColumn = new GridBoundColumn();
            boundColumn.DataType = typeof(System.Decimal);
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ConsultasPorAnos";
            boundColumn.HeaderText = "CONSULTA POR AÑOS";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 200;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ConsultasAcumuladas";
            boundColumn.HeaderText = "CONSULTAS ACUMULADAS";
            boundColumn.HeaderStyle.Font.Bold = true;
            boundColumn.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            boundColumn.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            boundColumn.HeaderStyle.Width = 250;
          

            #region "variables de control"
            // Ponemos los Prestaciones leidos aquí. 
            List<SiniestralidadDetail> _cache = new List<SiniestralidadDetail>();



            string fechaIni = (String.Format("{0:yyyy/MM/dd}", DateTime.Parse(FechaIni.Text)) ?? "");
            string fechaFin = (String.Format("{0:yyyy/MM/dd}", DateTime.Parse(FechaFin.Text)) ?? "");
           
            #endregion

            int _totalRows = SiniestralidadBLL.GetReporteSiniestralidadDetail(_cache, Convert.ToInt32( ClienteIdHF.Value),Convert.ToInt32( PolizaIdHF.Value), fechaIni, fechaFin);

            RadGrid1.DataSource = _cache;

            RadGrid1.DataBind();
            this.Controls.Add(RadGrid1);
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            RadGrid1.ExportSettings.HideStructureColumns = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.FileName = "ReporteDeSiniestralidad";
            RadGrid1.MasterTableView.GroupsDefaultExpanded = true;
            RadGrid1.MasterTableView.Caption = string.Empty;
            RadGrid1.MasterTableView.ExportToExcel();

        }
        catch (Exception q)
        {
            log.Error("No se puedo Descargar el Excel", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Descargar de Copagos Pendientes Cobro Lista a Excel");
        }
    }
}