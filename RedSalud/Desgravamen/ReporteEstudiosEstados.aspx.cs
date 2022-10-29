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
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Desgravamen;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;

public partial class Desgravamen_ReporteEstudiosEstados : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static bool flag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPA.Config = new ReporteCantidadEstudiosxPASearch();
        SearchPA.OnSearch += SearchPA_OnSearch;

        if (IsPostBack)
            return;

        if (!LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_ESTUDIOXPA"))
        {
            Response.Redirect("../MainPage.aspx");
            return;
        }

        bool addTodos = true;
        List<ProveedorDesgravamen> labos;
        /*int userID = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
        List<ProveedorDesgravamen> labos = ProveedorMedicoBLL.GetProveedorMedicoByUserId(userID);
        if (labos.Count == 0)
        {*/
            addTodos = true;
            labos = ProveedorMedicoBLL.GetProveedorMedico();
        //}
        LaboratoriosCombo.DataSource = labos;
        LaboratoriosCombo.DataBind();
        if (addTodos) 
            LaboratoriosCombo.Items.Insert(0, new RadComboBoxItem("Todos", "0"));

        int currentYear = DateTime.Now.Year;
        dtFechaInicioAtencion.SelectedDate = DateTime.Now;//new DateTime(currentYear, 5, 30); //First day of year
        dtFechaFinAtencion.SelectedDate = DateTime.Now;//new DateTime(currentYear, 5, 30); //Last day of year
        dtFechaInicioCita.SelectedDate = DateTime.Now;//new DateTime(currentYear, 5, 30); //First day of year
        dtFechaFinCita.SelectedDate = DateTime.Now;
        LoadClientesToCombo();
        LoadEstudiosCombo();
        LoadCiudadCombo();
    }

    void SearchPA_OnSearch()
    {

    }

    protected void EstudiosPorPADataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Cannot get Report for EstudiosPorFinanciera", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar el reporte de Cantidad de Estudios por Financiera");
        }
    }

    protected void GenerateReportButton_Click(object sender, EventArgs e)
    {
        //EstudiosPorPAGrid.DataBind();
    }

    protected void EstudiosPorPAGrid_DataBound(object sender, EventArgs e)
    {
        

        //GridBoundColumn boundColumn;

        ////Important: first Add column to the collection 
        //boundColumn = new GridBoundColumn();
        //this.EstudiosPorPAGrid.MasterTableView.Columns.Add(boundColumn);

        ////Then set properties 
        //boundColumn.HeaderText = "Total"; 
    }
    protected void ProveedorMedicoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void LaboratoriosCombo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        EstudiosPorPAGrid.DataBind();
    }
    protected void EstudiosPorPAGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item ||
            e.Item.ItemType == GridItemType.AlternatingItem)
        {
            
        }
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
    }
    
    protected void EstudiosPorPADataSource_DataBinding(object sender, EventArgs e)
    {
        
    }

    
    protected void clientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    private void LoadEstudiosCombo()
    {
        try
        {
            int cliente = Convert.ToInt32(clientesComboBox.SelectedValue);

            
            List<Estudio> estudios = EstudioBLL.GetEstudiosAll(cliente, false);
            estudios.Insert(0, new Estudio()
            {
                EstudioId = 0,
                NombreEstudio = "Todos"
            });
            estudiosComboBox.DataSource = estudios;
            estudiosComboBox.DataValueField = "EstudioId";
            estudiosComboBox.DataTextField = "NombreEstudio";
            estudiosComboBox.DataBind();
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al cargar los parametros del reporte");

        }
    }

    private void LoadCiudadCombo()
    {
        List<ComboContainer> list = CiudadesDesgravamenBLL.GetCiudadesDesgravamenCombo();
        list.Insert(0, new ComboContainer()
        {
            ContainerId = "0",
            ContainerName = "TODOS"
        });

        CiudadesCombo.DataSource = list;
        CiudadesCombo.DataValueField = "ContainerId";
        CiudadesCombo.DataTextField = "ContainerName";
        CiudadesCombo.DataBind();
    }
}