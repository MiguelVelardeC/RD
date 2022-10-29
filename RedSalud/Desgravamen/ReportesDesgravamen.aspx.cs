using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Desgravamen;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;

public partial class Desgravamen_ReportesDesgravamen : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        int currentYear = DateTime.Now.Year;
        FechaInicio.SelectedDate = DateTime.Now;//new DateTime(currentYear, 1, 1); //First day of year
        FechaFin.SelectedDate = DateTime.Now;//new DateTime(currentYear, 12, 31); //Last day of year
        DtInicialTop.SelectedDate = DateTime.Now;
        DtFinalTop.SelectedDate = DateTime.Now;
        LoadClientesToCombo();
        LoadFinancierasEstudiosCombo();
        CargarCiudadTopFinancieraComboBox();
        
        try
        {
            for (int i = Artexacta.App.Constants.FIRST_YEAR_REDSALUD; i <= currentYear; i++)
            {
                GestionCombo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(i.ToString(), i.ToString()));
            }
            GestionCombo.FindItemByValue(currentYear.ToString()).Selected = true;
        }
        catch (Exception q)
        {
            log.Error("Error in Page_Load in MainPage.aspx", q);
        }

        if (!LoginSecurity.IsUserAuthorizedPermission("MANAGE_DESGRAVAMEN"))
            ListaPAButton.Visible = false;

        if (!LoginSecurity.IsUserAuthorizedPermission("MANAGE_LABDESG"))
            LaboratorioDesgravamenButton.Visible = false;

        /*
         * 
                <asp:ObjectDataSource ID="EstudiosPorFinancieraDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL"
                    SelectMethod="GetReporteCantidadEstudiosPorFinanciera"
                    OnSelected="EstudiosPorFinancieraDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="FechaInicio" Name="fechaInicio" Type="DateTime" PropertyName="SelectedDate" />
                        <asp:ControlParameter ControlID="FechaFin" Name="fechaFin" Type="DateTime" PropertyName="SelectedDate" />
                    </SelectParameters>
                </asp:ObjectDataSource>
         */

        /*DataTable dt = Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorFinanciera(FechaInicio.SelectedDate.Value, FechaFin.SelectedDate.Value, 0);

        EstudiosPorFinancieraGrid.DataSource = dt;
        EstudiosPorFinancieraGrid.DataBind();*/

    }

    protected void CantidadFiancieraCiudadDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get Report for CantidadFiancieraCiudad", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar el reporte de Cantidad de Citas por Financiera por Ciudad");
        }
    }

    protected void EstudiosPorFinancieraDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get Report for EstudiosPorFinanciera", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar el reporte de Cantidad de Estudios por Financiera");
        }
    }

    protected void CantidadCitasPorMedicoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get Report for CantidadCitasPorMedico", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar el reporte de Cantidad de Citas por Médico");
        }
    }

    protected void GenerateReportButton_Click(object sender, EventArgs e)
    {
        try
        {
            int cliente = Convert.ToInt32(clientesComboBox2.SelectedValue);
            int financiera = Convert.ToInt32(financieraComboBox.SelectedValue);
            int estudio = Convert.ToInt32(estudiosComboBox.SelectedValue);
            bool tipoProducto = ProductoTipo.Checked;
            int cobroFinanciera = Convert.ToInt32(cobroClienteCombo.SelectedValue);

            DataTable dt = Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorFinanciera(
                FechaInicio.SelectedDate.Value, FechaFin.SelectedDate.Value, cliente, financiera, estudio, tipoProducto, cobroFinanciera);

            EstudiosPorFinancieraGrid.DataSource = dt;
            EstudiosPorFinancieraGrid.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    protected void GestionCombo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        /*
        int year = DateTime.Now.Year;
        try
        {
            year = Convert.ToInt32(GestionCombo.SelectedValue);
        }
        catch (Exception ex)
        {
            log.Error("Cannot convert GestionCombo.SelectedValue to integer value", ex);
        }*/
        //FechaInicio.SelectedDate = new DateTime(year, 1, 1); //First day of year
        //FechaFin.SelectedDate = new DateTime(year, 12, 31); //Last day of year
        CantidadFiancieraCiudadGridView.DataBind();
        CantidadCitasPorMedicoGridView.DataBind();
        //EstudiosPorFinancieraGrid.DataBind();
    }
    protected void EstudiosPorFinancieraGrid_DataBound(object sender, EventArgs e)
    {
        GridBoundColumn boundColumn;

        //Important: first Add column to the collection 
        boundColumn = new GridBoundColumn();
        this.EstudiosPorFinancieraGrid.MasterTableView.Columns.Add(boundColumn);

        //Then set properties 
        boundColumn.HeaderText = "Total"; 
    }
    protected void ProveedorMedicoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void EstudiosPorFinancieraGrid_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName.Equals("ExportToExcel"))
        {
            ExportarAExcel();
        }
    }

    private void ExportarAExcel()
    {
        if (FechaInicio.SelectedDate == null || FechaFin.SelectedDate == null)
        {
            SystemMessages.DisplaySystemMessage("Debe colocar fecha inicio y fin");
            return;
        }

        try
        {
            int cliente = Convert.ToInt32(clientesComboBox2.SelectedValue);
            int financiera = Convert.ToInt32(financieraComboBox.SelectedValue);
            int estudio = Convert.ToInt32(estudiosComboBox.SelectedValue);
            bool tipoProducto = ProductoTipo.Checked;
            int cobroFinanciera = Convert.ToInt32(cobroClienteCombo.SelectedValue);
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("EstudioxXFinanciera");

                DataTable datos = Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorFinanciera(
                    FechaInicio.SelectedDate.Value, FechaFin.SelectedDate.Value, cliente, financiera, estudio, tipoProducto, cobroFinanciera);

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(datos, true);
                ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();


                
                //Format the header for column 1-6
                using (ExcelRange rng = ws.Cells["A1:BI1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(153, 153, 153));
                }

                //Example how to Format Column 1 as numeric 
                using (ExcelRange col = ws.Cells[2, 2, 2 + datos.Rows.Count - 1, 2])
                {
                    col.Style.Numberformat.Format = "@";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                string descFechas = FechaInicio.SelectedDate.Value.Month.ToString().PadLeft(2,'0') + 
                    FechaInicio.SelectedDate.Value.Day.ToString().PadLeft(2,'0') + "_" +
                    FechaFin.SelectedDate.Value.Month.ToString().PadLeft(2, '0') +
                    FechaFin.SelectedDate.Value.Day.ToString().PadLeft(2, '0');

                //Write it back to the client
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=EstudiosXFinanciera" + descFechas + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }
        }
        catch (Exception q)
        {
            log.Error("Error writing response", q);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al obtener el archivo Excel con la información.");
        }
        finally
        {
            Response.Flush();
            Response.End();
        }
    }
    protected void EstudiosPorFinancieraGrid_DataBinding(object sender, EventArgs e)
    {
        RadGrid theGrid = (RadGrid)sender;
        theGrid.Columns.Clear();
    }

    private void LoadClientesToCombo()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();
        List<RedCliente> modifiedList = new List<RedCliente>();
        modifiedList.Add(new RedCliente()
        {
            ClienteId = 0,
            NombreJuridico = "Todos"
        });
        foreach (RedCliente cliente in list)
        {
            modifiedList.Add(cliente);
        }

        clientesComboBox.DataSource = modifiedList;
        clientesComboBox.DataValueField = "ClienteId";
        clientesComboBox.DataTextField = "NombreJuridico";
        clientesComboBox.DataBind();

        clientesComboBox2.DataSource = modifiedList;
        clientesComboBox2.DataValueField = "ClienteId";
        clientesComboBox2.DataTextField = "NombreJuridico";
        clientesComboBox2.DataBind();
    }

    private void LoadFinancierasEstudiosCombo()
    {
        try
        {
            int cliente = Convert.ToInt32(clientesComboBox2.SelectedValue);

            List<Financiera> financieras = FinancieraBLL.GetFinancieras(cliente, "");
            List<Financiera> financierasTotal = new List<Financiera>();
            financierasTotal.Add(new Financiera() 
            {
                FinancieraId = 0,
                Nombre = "Todos"
            });
            foreach (Financiera f in financieras)
            {
                financierasTotal.Add(f);
            }
            financieraComboBox.DataSource = financierasTotal;
            financieraComboBox.DataValueField = "FinancieraId";
            financieraComboBox.DataTextField = "Nombre";
            financieraComboBox.DataBind();

            List<Estudio> estudios = EstudioBLL.GetEstudiosAll(cliente, false);
            List<Estudio> estudiostotal = new List<Estudio>();
            estudiostotal.Add(new Estudio()
            {
                EstudioId = -1,
                NombreEstudio = "Todos"
            });
            estudiostotal.Add(new Estudio()
            {
                EstudioId = 0,
                NombreEstudio = "Revision Medica"
            });
            foreach (Estudio e in estudios)
            {
                estudiostotal.Add(e);
            }
            estudiosComboBox.DataSource = estudiostotal;
            estudiosComboBox.DataValueField = "EstudioId";
            estudiosComboBox.DataTextField = "NombreEstudio";
            estudiosComboBox.DataBind();
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al cargar los parametros del reporte");
            
        }
    }

    protected void clientesComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFinancierasEstudiosCombo();
    }
    protected void ProductoTipo_CheckedChanged(object sender, EventArgs e)
    {
        if (financieraComboBox.Enabled)
        {
            financieraComboBox.SelectedValue = "0";
            financieraComboBox.Enabled = false;
        }
        else
        {
            financieraComboBox.Enabled = true;
        }
    }
    protected void GenerateExcelReportButton_Click(object sender, EventArgs e)
    {
        ExportarAExcel();
    }
    private void CargarCiudadTopFinancieraComboBox()
    {
        //string ciudadesExistentes = "ALT,COB,MON,ORU,PTS,SCR,TRI,TRJ";
        List<ComboContainer> ciudades = CiudadesDesgravamenBLL.GetCiudadesDesgravamenCombo();//CiudadBLL.getCiudadList(ciudadesExistentes);
        ciudades.Insert(0, new ComboContainer()
        {
            ContainerId = "0",
            ContainerName = "Todos"
        });
        ciudadComboBox.DataSource = ciudades;
        ciudadComboBox.DataValueField = "ContainerId";
        ciudadComboBox.DataTextField = "ContainerName";
        ciudadComboBox.DataBind();

        int cliente = Convert.ToInt32(clientesComboBox2.SelectedValue);

        List<Financiera> financieras = FinancieraBLL.GetFinancieras(cliente, "");

        financieras.Insert(0, new Financiera()
        {
            FinancieraId = 0,
            Nombre = "Todos"
        });
        financieras.Add(new Financiera()
        {
            FinancieraId = -1,
            Nombre = "SIN FINANCIERA"
        });

        financieraTopComboBox.DataSource = financieras;
        financieraTopComboBox.DataValueField = "FinancieraId";
        financieraTopComboBox.DataTextField = "Nombre";
        financieraTopComboBox.DataBind();

    }
    
    protected void clientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCiudadTopFinancieraComboBox();
    }
    protected void BtnTopReportes_Click(object sender, EventArgs e)
    {

    }
    protected void CantidadFiancieraCiudadGridView_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    {
       /* if (e.Column is GridBoundColumn)
        {
            GridBoundColumn gbc = (GridBoundColumn)e.Column;
            //e.Column.HeaderText = 
            string type = gbc.DataTypeName;
            string header = gbc.HeaderText;
            if (type == "System.Int32")
            {
                gbc.Aggregate = GridAggregateFunction.Sum;
                gbc.FooterText = " ";
            }
            if (header == "FINANCIERA")
            {
                gbc.FooterText = "TOTAL: ";
            }
        }*/
    }
}