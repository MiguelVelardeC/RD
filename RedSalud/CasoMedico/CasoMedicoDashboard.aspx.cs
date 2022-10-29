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
using Artexacta.App.Caso.BLL;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedCliente;
using Artexacta.App.Caso;
using System.Text.RegularExpressions;
using Artexacta.App.Ciudad.BLL;

public partial class CasoMedico_CasoMedicoDashboard : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
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
            FillComboCiudades();
            FillComboClientes();

            FillAllReportBlocks();
        }
    }
    private void FillAllReportBlocks ()
    {
        if (FechaIni.SelectedDate == null)
        {
            FechaIni.SelectedDate = DateTime.Now;
        }
        if (FechaFin.SelectedDate == null)
        {
            FechaFin.SelectedDate = DateTime.Now;
        }
        FillTotalesConsultas();
        FillConteoConsultas();
        FillTotalesPacientes();
        FillConsultasXPacientes();
        FillConsultasXPacientesCritico();
        FillEnfermedadesXConsultas();
        FillGastosEstudios();
        FillGastosFarmacia();
        FillPrestacionesOdontologicas();
        FillCasosXMes();
        FillEspecialistasXDerivaciones();
        FillDoctoresXCasos();
        FillEnfermedadesCronicas();
    }
    private void FillTotalesConsultas ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.TotalesConsultasTableAdapter adapter =
                new TableroControlDSTableAdapters.TotalesConsultasTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.TotalesConsultasDataTable dtTable = adapter.GetTotalesConsultas(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            TotalesConsultasRadGrid.DataSource = dtTable;
            TotalesConsultasRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error("Error in FillTotalesConsultas in MainPage.aspx", q);
        }
    }
    private void FillConteoConsultas()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.ConteoConsultasTableAdapter adapter =
                new TableroControlDSTableAdapters.ConteoConsultasTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.ConteoConsultasDataTable dtTable = adapter.GetConteoConsultas(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            ConteoConsultasRadGrid.DataSource = dtTable;
            ConteoConsultasRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error("Error in FillConteoConsultas in MainPage.aspx", q);
        }
    }

    private void FillEnfermedadesXConsultas ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.EnfermedadesXConsultasTableAdapter adapter =
                new TableroControlDSTableAdapters.EnfermedadesXConsultasTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.EnfermedadesXConsultasDataTable dtTable = adapter.GetEnfermedadesXConsultas(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            EnfermedadesXConsultasRadGrid.DataSource = dtTable;
            EnfermedadesXConsultasRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error("Error in FillTotalesConsultas in MainPage.aspx", q);
        }
    }

    private void FillTotalesPacientes ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            //string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.TotalesPacientesTableAdapter adapter =
                new TableroControlDSTableAdapters.TotalesPacientesTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.TotalesPacientesDataTable dtTable = adapter.GetTotalesPacientes(clienteID, fecheIni, fecheFin);
            TotalesPacientesRadGrid.DataSource = dtTable;
            TotalesPacientesRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error("Error in FillTotalesPacientes in MainPage.aspx", q);
            SystemMessages.DisplaySystemErrorMessage("No se Pudo cargar la tabla de TOTALES ASEGURADOS.");
        }
    }

    private void FillConsultasXPacientes ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.ConsultasXPacientesTableAdapter adapter =
                new TableroControlDSTableAdapters.ConsultasXPacientesTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.ConsultasXPacientesDataTable dtTable = adapter.GetConsultasXPacientes(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            ConsultasXPacientesRadGrid.DataSource = dtTable;
            ConsultasXPacientesRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }
    private void FillConsultasXPacientesCritico ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.ConsultasXPacientesCriticosTableAdapter adapter =
                new TableroControlDSTableAdapters.ConsultasXPacientesCriticosTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.ConsultasXPacientesCriticosDataTable dtTable = adapter.GetConsultasXPacientesCriticos(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            ConsultasXPacientesCriticosRadGrid.DataSource = dtTable;
            ConsultasXPacientesCriticosRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }
    private void FillGastosEstudios ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.GastosEstudiosTableAdapter adapter =
                new TableroControlDSTableAdapters.GastosEstudiosTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.GastosEstudiosDataTable dtTable = adapter.GetGastosEstudios(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            GastosEstudiosRadGrid.DataSource = dtTable;
            GastosEstudiosRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }

    private void FillGastosFarmacia ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.GastosFarmaciaTableAdapter adapter =
                new TableroControlDSTableAdapters.GastosFarmaciaTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.GastosFarmaciaDataTable dtTable = adapter.GetGastosFarmacia(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            GastosFarmaciaRadGrid.DataSource = dtTable;
            GastosFarmaciaRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }

    private void FillPrestacionesOdontologicas()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.PrestacionesOdontologicasTableAdapter adapter =
                new TableroControlDSTableAdapters.PrestacionesOdontologicasTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.PrestacionesOdontologicasDataTable dtTable = adapter.GetPrestacionesOdontologicas(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            PrestacionesRadGrid.DataSource = dtTable;
            PrestacionesRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }

    private void FillCasosXMes ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.CasosXMesTableAdapter adapter =
                new TableroControlDSTableAdapters.CasosXMesTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.CasosXMesDataTable dtTable = adapter.GetCasosXMes(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            CasosXMesRadGrid.DataSource = dtTable;
            CasosXMesRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Warn("Error al momento de traer los CasosXMes para asegurado 0" + PacienteRadComboBox.SelectedValue, q);
        }
    }
    private void FillEspecialistasXDerivaciones ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.EspecialistasXDerivacionesTableAdapter adapter =
                new TableroControlDSTableAdapters.EspecialistasXDerivacionesTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.EspecialistasXDerivacionesDataTable dtTable = adapter.GetEspecialistasXDerivaciones(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            EspecialistasXDerivacionesRadGrid.DataSource = dtTable;
            EspecialistasXDerivacionesRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }
    private void FillDoctoresXCasos ()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);
            int aseguradoID = Convert.ToInt32("0" + PacienteRadComboBox.SelectedValue);
            DateTime fecheIni = FechaIni.SelectedDate.Value;
            DateTime fecheFin = FechaFin.SelectedDate.Value;
            string ciudadId = CiudadCombo.SelectedValue.ToString();

            TableroControlDSTableAdapters.DoctoresXCasosTableAdapter adapter =
                new TableroControlDSTableAdapters.DoctoresXCasosTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.DoctoresXCasosDataTable dtTable = adapter.GetDoctoresXCasos(aseguradoID, clienteID, ciudadId, fecheIni, fecheFin);
            DoctoresXCasosRadGrid.DataSource = dtTable;
            DoctoresXCasosRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }
    private void FillEnfermedadesCronicas()
    {
        try
        {
            int clienteID = Convert.ToInt32(ClienteCombo.SelectedValue);

            TableroControlDSTableAdapters.EnfermedadesCronicasTableAdapter adapter =
                new TableroControlDSTableAdapters.EnfermedadesCronicasTableAdapter();
            adapter.cmdTimeout = 600;
            TableroControlDS.EnfermedadesCronicasDataTable dtTable = adapter.GetTop10EnfermedadesCronicas(clienteID);
            EnfermedadesCronicasRadGrid.DataSource = dtTable;
            EnfermedadesCronicasRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error(q);
        }
    }

    private void FillComboCiudades ()
    {
        try
        {
            CiudadCombo.DataSource = CiudadBLL.getCiudadList();
            CiudadCombo.DataBind();

            CiudadCombo.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Todas", "TTT"));

            CiudadCombo.Items.FindItemByValue("TTT").Selected = true;
        }
        catch (Exception q)
        {
            log.Error("Error in FillComboCiudades in MainPage.aspx", q);
        }
    }

    private void FillComboClientes ()
    {
        try
        {
            List<RedCliente> lista = Artexacta.App.RedCliente.BLL.RedClienteBLL.getRedClienteList();
            RedCliente empty = new RedCliente();
            empty.NombreJuridico = "TODOS";
            lista.Insert(0, empty);
            ClienteCombo.DataSource = lista;
            ClienteCombo.DataBind();
            if (ClienteCombo.Items.Count > 0)
                ClienteCombo.Items[0].Selected = true;
        }
        catch (Exception q)
        {
            log.Error("Error in FillComboClientes in MainPage.aspx", q);
        }
    }

    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page CasoMedicoRegistro.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteCombo_DataBound ( object sender, EventArgs e )
    {
        try
        {
            if (!IsPostBack)
                ClienteCombo.Items[0].Selected = true;
        }
        catch (Exception q)
        {
            log.Error("Error in ClienteCombo_DataBound in MainPage.aspx", q);
        }
    }
    protected void SearchLB_Click ( object sender, EventArgs e )
    {
        FillAllReportBlocks();
    }
  
    protected void ConsultasXPacientesRadGrid_ItemCommand ( object sender, GridCommandEventArgs e )
    {
        if (e.CommandName == "Historial")
        {
            Session["PacienteId"] = e.CommandArgument;
            Response.Redirect("~/CasoMedico/Historial.aspx");
        }
    }
    public string FormatMoney ( object value )
    {
        decimal dValue = 0;
        decimal.TryParse(value.ToString(), out dValue);
        return dValue.ToString("#,##0.00");
    }
    protected void TotalesConsultasRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = e.Item as GridFooterItem;
            footerItem["Numero"].Text = footerItem["Numero"].Text.Replace("Sum : ", "");
        }
    }
    protected void TotalesPacientesRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            TableCell cell = dataItem["Descripcion"];
            string Descripcion = cell.Text;
            if (Descripcion == "TOTAL ASEGURADOS CON ENFERMEDADES CRÓNICAS" ||
                Descripcion == "TOTAL ASEGURADOS MAYORES DE 60" ||
                Descripcion == "TOTAL MUJERES ASEGURADAS")
            {
                foreach (TableCell cel in dataItem.Cells)
                {
                    cel.CssClass = "BottomBorder";
                }
            }
        }
    }
}