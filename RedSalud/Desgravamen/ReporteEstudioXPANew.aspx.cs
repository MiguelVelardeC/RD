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
using System.Data;

public partial class Desgravamen_ReporteEstudioXPANew : System.Web.UI.Page
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

        //bool addTodos = false;
        //int userID = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
        List<ProveedorDesgravamen> labos = ProveedorMedicoBLL.GetProveedorMedico(); //ProveedorMedicoBLL.GetProveedorMedicoByUserId(userID);
        /*if (labos.Count == 0)
        {
            addTodos = true;
            labos = ProveedorMedicoBLL.GetProveedorMedico();
        }*/
        LaboratoriosCombo.DataSource = labos;
        LaboratoriosCombo.DataBind();
        //if (addTodos) 
        LaboratoriosCombo.Items.Insert(0, new RadComboBoxItem("Todos", "0"));

        int currentYear = DateTime.Now.Year;
        FechaInicio.SelectedDate = DateTime.Now;//new DateTime(currentYear, 5, 30); //First day of year
        FechaFin.SelectedDate = DateTime.Now;//new DateTime(currentYear, 5, 30); //Last day of year
        LoadClientesToCombo();
        LoadFinancierasToCombo();
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
        EstudiosPorPAGrid.DataSource = null;
        EstudiosPorPAGrid.DataBind();

        DataTable dt = new DataTable();
        // Variables :
        DateTime finicio = Convert.ToDateTime(this.FechaInicio.SelectedDate);
        DateTime ffin = Convert.ToDateTime(this.FechaFin.SelectedDate);
        List<EstudioPrecio> ep = ReporteDesgravamenBLL.getEstudioPrecioList();
        dt = ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorPA(
            SearchPA.Sql,
            finicio,
            ffin,
            Convert.ToInt32(LaboratoriosCombo.SelectedValue),
            Convert.ToInt32(clientesComboBox.SelectedValue),
            CiudadesCombo.SelectedValue.ToString(),
            Convert.ToInt32(financieraComboBox.SelectedValue),
            cobroClienteCombo.SelectedValue,
            Convert.ToInt32(CitaId.Value),
            PropuestoAseguradoTextBox.Text
            );
        int numfilas = dt.Rows.Count;

        // Fila, Colummna[i, j]

        string nomestudio = "";
        string ciudad = "";
        int valor = 0;
        decimal preestudio = 0;

        int LPZ = 0;
        int CBB = 0;
        int ORU = 0;
        int TRJ = 0;
        int STC = 0;
        int TRI = 0;
        int PTS = 0;
        int SCR = 0;
        int ALT = 0;
        int COB = 0;
        int MON = 0;

        List<ReportexPlaza> rp = new List< ReportexPlaza>();

        for (int j = 0; j < dt.Columns.Count; j++)
        {
            if (j >= 10) // inicia 1er Estudio
            {
                nomestudio = dt.Columns[j].ColumnName;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    valor = Convert.ToInt32(dt.Rows[i][j].ToString());
                    if (valor > 0)
                    {
                        ciudad = dt.Rows[i][1].ToString();
                        switch (ciudad)
                        {
                            case "LA PAZ": // LPZ
                                LPZ += valor; break;
                            case "COCHABAMBA": // CBB
                                CBB += valor; break;
                            case "ORURO"://ORU
                                ORU += valor; break;
                            case "TARIJA"://TRJ
                                TRJ += valor; break;
                            case "SANTA CRUZ"://STC
                                STC += valor; break;
                            case "TRINIDAD"://TRI
                                TRI += valor; break;
                            case "POTOSÍ"://PTS
                                PTS += valor; break;
                            case "SUCRE"://SCR
                                SCR += valor; break;
                            case "EL ALTO (LA PAZ)"://ALT
                                ALT += valor; break;
                            case "COBIJA"://COB
                                COB += valor; break;
                            case "MONTERO"://MON
                                MON += valor; break;
                        }

                    }

                }
                //preestudio = ep.Where(x => x.NomEstudio = nomestudio).ToString();
                if ((LPZ + CBB + ORU + TRJ + STC + TRI + PTS + SCR + ALT + COB + MON) != 0)
                {
                    List<EstudioPrecio> pestudio = ep.FindAll(x => x.NomEstudio.Equals(nomestudio) && x.Tipo.Equals("P")).ToList();
                    foreach (EstudioPrecio b in pestudio)
                    {
                        preestudio = b.Precioxplaza;
                    }
                    //preestudio= pestudio.()
                    rp.Add(new ReportexPlaza()
                    {
                        Estudio = nomestudio,
                        precio = preestudio,
                        CBB = CBB,
                        LPZ = LPZ,
                        ORU = ORU,
                        TRJ = TRJ,
                        STC = STC,
                        TRI = TRI,
                        PTS = PTS,
                        SCR = SCR,
                        ALT = ALT,
                        COB = COB,
                        MON = MON
                    });
                    LPZ = 0;
                    CBB = 0;
                    ORU = 0;
                    TRJ = 0;
                    STC = 0;
                    TRI = 0;
                    PTS = 0;
                    SCR = 0;
                    ALT = 0;
                    COB = 0;
                    MON = 0;
                }
                //Console.WriteLine("lA PAZ: " + LAPAZ + "COCHABAMBA:" + COCHABAMBA + "ORURO" + ORURO + "TARIJA" + TARIJA + "SANTACRUZ" + SANTACRUZ
                //    + "TRINIDAD" + TRINIDAD + "POTOSI" + POTOSI + "SUCRE" + SUCRE);
            }

        }
        //Console.WriteLine(String.Join(", ", rp));

        EstudiosPorPAGrid.DataSource = rp;
        EstudiosPorPAGrid.DataBind();
    }
    protected void EstudiosPorPAGrid_DataBound(object sender, EventArgs e)
    {
        //GridBoundColumn boundColumn;

        //Important: first Add column to the collection 
        //boundColumn = new GridBoundColumn();
        //this.EstudiosPorPAGrid.MasterTableView.Columns.Add(boundColumn);

        //Then set properties 
        //boundColumn.HeaderText = "Totales: ";
    }
    protected void ProveedorMedicoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void LaboratoriosCombo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //EstudiosPorPAGrid.DataBind();
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
    protected void EstudiosPorPAGrid_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    {
        try
        {
            if (e.Column is GridBoundColumn)
            {
                GridBoundColumn gbc = (GridBoundColumn)e.Column;
                //e.Column.HeaderText = 
                string type = gbc.DataTypeName;
                string header = gbc.HeaderText;
                if (type == "System.Int32" && header != "ID")
                {
                    gbc.Aggregate = GridAggregateFunction.Sum;
                    gbc.FooterText = " ";
                }
                if (header == "ID")
                {
                    gbc.FooterText = "TOTAL POR ESTUDIO: ";
                }
            }        
        }
        catch (Exception)
        {
        }
        
    }
    protected void EstudiosPorPADataSource_DataBinding(object sender, EventArgs e)
    {
        
    }

    private void LoadFinancierasToCombo()
    {

        int cliente = 0;
        try
        {
            cliente = Convert.ToInt32(clientesComboBox.SelectedValue);
        }
        catch (Exception)
        {
         
        }

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

        financieraComboBox.DataSource = financieras;
        financieraComboBox.DataValueField = "FinancieraId";
        financieraComboBox.DataTextField = "Nombre";
        financieraComboBox.DataBind();
    }
    protected void clientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFinancierasToCombo();
    }
    protected void GenerateReportExcelButton_Click(object sender, EventArgs e)
    {
        EstudiosPorPAGrid.MasterTableView.ExportToExcel();
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

    protected void GenerarReportePlazaProve_Click(object sender, EventArgs e)
    {
        EstudiosPorPAGrid.DataSource = null;
        EstudiosPorPAGrid.DataBind();

        DataTable dt = new DataTable();
        // Variables :
        DateTime finicio = Convert.ToDateTime(this.FechaInicio.SelectedDate);
        DateTime ffin = Convert.ToDateTime(this.FechaFin.SelectedDate);
        List<EstudioPrecio> ep = ReporteDesgravamenBLL.getEstudioPrecioList();
        dt = ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorPA(
            SearchPA.Sql,
            finicio,
            ffin,
            Convert.ToInt32(LaboratoriosCombo.SelectedValue),
            Convert.ToInt32(clientesComboBox.SelectedValue),
            CiudadesCombo.SelectedValue.ToString(),
            Convert.ToInt32(financieraComboBox.SelectedValue),
            cobroClienteCombo.SelectedValue,
            Convert.ToInt32(CitaId.Value),
            PropuestoAseguradoTextBox.Text
            );
        int numfilas = dt.Rows.Count;

        // Fila, Colummna[i, j]

        string nomestudio = "";
        string ciudad = "";
        int valor = 0;

        int LPZ = 0;
        int CBB = 0;
        int ORU = 0;
        int TRJ = 0;
        int STC = 0;
        int TRI = 0;
        int PTS = 0;
        int SCR = 0;
        int ALT = 0;
        int COB = 0;
        int MON = 0;

        decimal precioLPZ = 0;
        decimal precioCBB = 0;
        decimal precioORU = 0;
        decimal precioTRJ = 0;
        decimal precioSTC = 0;
        decimal precioTRI = 0;
        decimal precioPTS = 0;
        decimal precioSCR = 0;
        decimal precioALT = 0;
        decimal precioCOB = 0;
        decimal precioMON = 0;

        List<ReportexPlazaxProveedor> rp = new List<ReportexPlazaxProveedor>();

        for (int j = 0; j < dt.Columns.Count; j++)
        {
            if (j >= 10) // inicia 1er Estudio
            {
                nomestudio = dt.Columns[j].ColumnName;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    valor = Convert.ToInt32(dt.Rows[i][j].ToString());
                    if (valor > 0)
                    {
                        ciudad = dt.Rows[i][1].ToString();
                        switch (ciudad)
                        {
                            case "LA PAZ": // LPZ
                                LPZ += valor; break;
                            case "COCHABAMBA": // CBB
                                CBB += valor; break;
                            case "ORURO"://ORU
                                ORU += valor; break;
                            case "TARIJA"://TRJ
                                TRJ += valor; break;
                            case "SANTA CRUZ"://STC
                                STC += valor; break;
                            case "TRINIDAD"://TRI
                                TRI += valor; break;
                            case "POTOSÍ"://PTS
                                PTS += valor; break;
                            case "SUCRE"://SCR
                                SCR += valor; break;
                            case "EL ALTO (LA PAZ)"://ALT
                                ALT += valor; break;
                            case "COBIJA"://COB
                                COB += valor; break;
                            case "MONTERO"://MON
                                MON += valor; break;
                        }

                    }

                }
                if ((LPZ + CBB + ORU + TRJ + STC + TRI + PTS + SCR + ALT + COB + MON ) != 0)
                {
                    //preestudio = ep.Where(x => x.NomEstudio = nomestudio).ToString();
                    List<EstudioPrecio> pestudio = ep.FindAll(x => x.NomEstudio.Equals(nomestudio)).ToList();
                    foreach (EstudioPrecio b in pestudio)
                    {
                        switch (b.ciudad)
                        {
                            case "LA PAZ": // LPZ
                                precioLPZ = b.Precioxciudad; break;
                            case "COCHABAMBA": // CBB
                                precioCBB = b.Precioxciudad; break;
                            case "ORURO"://ORU
                                precioORU = b.Precioxciudad; break;
                            case "TARIJA"://TRJ
                                precioTRJ = b.Precioxciudad; break;
                            case "SANTA CRUZ"://STC
                                precioSTC = b.Precioxciudad; break;
                            case "TRINIDAD"://TRI
                                precioTRI = b.Precioxciudad; break;
                            case "POTOSÍ"://PTS
                                precioPTS = b.Precioxciudad; break;
                            case "SUCRE"://SCR
                                precioSCR = b.Precioxciudad; break;
                            case "EL ALTO (LA PAZ)"://ALT
                                precioALT = b.Precioxciudad; break;
                            case "COBIJA"://COB
                                precioCOB = b.Precioxciudad; break;
                            case "MONTERO"://MON
                                precioMON = b.Precioxciudad; break;
                        }
                    }
                    //preestudio= pestudio.()
                    rp.Add(new ReportexPlazaxProveedor()
                    {
                        Estudio = nomestudio,
                        precioCBB = precioCBB,
                        CBB = CBB,
                        precioLPZ = precioLPZ,
                        LPZ = LPZ,
                        precioORU = precioORU,
                        ORU = ORU,
                        precioTRJ = precioTRJ,
                        TRJ = TRJ,
                        precioSTC = precioSTC,
                        STC = STC,
                        precioTRI = precioTRI,
                        TRI = TRI,
                        precioPTS = precioPTS,
                        PTS = PTS,
                        precioSCR = precioSCR,
                        SCR = SCR,
                        precioALT = precioALT,
                        ALT = ALT,
                        precioCOB = precioCOB,
                        COB = COB,
                        precioMON = precioMON,
                        MON = MON
                    });
                    LPZ = 0;
                    CBB = 0;
                    ORU = 0;
                    TRJ = 0;
                    STC = 0;
                    TRI = 0;
                    PTS = 0;
                    SCR = 0;
                    ALT = 0;
                    COB = 0;
                    MON = 0;
                }
                //Console.WriteLine("lA PAZ: " + LAPAZ + "COCHABAMBA:" + COCHABAMBA + "ORURO" + ORURO + "TARIJA" + TARIJA + "SANTACRUZ" + SANTACRUZ
                //    + "TRINIDAD" + TRINIDAD + "POTOSI" + POTOSI + "SUCRE" + SUCRE);
            }

        }
        //Console.WriteLine(String.Join(", ", rp));

        EstudiosPorPAGrid.DataSource = rp;
        EstudiosPorPAGrid.DataBind();
    }
}