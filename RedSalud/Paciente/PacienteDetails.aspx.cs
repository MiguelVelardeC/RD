using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.Paciente;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Poliza;

public partial class Paciente_PacienteDetails : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public string defaultJSonAntecedentes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            //DateTime aasdfs= (DateTime)this.FechaNacimiento1.SelectedDate;
            //EstadoCivilDDL1.SelectedValue
        }
        SetViewMode();
    }
    private void SetViewMode()
    {
        if (PacienteIdHF.Value.Equals("0"))
        {
            PacienteFV.ChangeMode(FormViewMode.Insert);
            this.TitleLabel.Text = "Nuevo Paciente";
            this.Title = "Nuevo Paciente";
        }
        else
        {
        //    PacienteFV.ChangeMode(FormViewMode.ReadOnly);
            this.TitleLabel.Text = "Editar Paciente";
            this.Title = "Editar Paciente";
        //    setMinDateCalendar();
        }
    }
    protected void setMinDateCalendar()
    {
        //PacienteFV.ChangeMode(FormViewMode.ReadOnly);
        //RadDatePicker FechaInicio = (RadDatePicker)PacienteFV.FindControl("FechaInicio");
        //FechaInicio.MinDate = DateTime.Now.AddYears(-1);
        //RadDatePicker FechaFin = (RadDatePicker)PacienteFV.FindControl("FechaFin");
        //FechaFin.MinDate = DateTime.Now.AddYears(-1);    
    }
    protected void ProcessSessionParameters()
    {
        int PacienteId = 0;
        if (Session["PacienteId"] != null && !string.IsNullOrEmpty(Session["PacienteId"].ToString()))
        {
            try
            {
                PacienteId = Convert.ToInt32(Session["PacienteId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session PacienteId:" + Session["PacienteId"]);
            }
        }
        PacienteIdHF.Value = PacienteId.ToString();
        Session["PacienteId"] = null;
    }

    protected void PacienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Paciente.");
            log.Error("Function PacienteODS_Selected on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PacienteODS_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Insertar el nuevo Paciente.");
            log.Error("Function PacienteODS_Inserted on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            int PacienteId = (int)e.ReturnValue;
            if (PacienteId <= 0)
                SystemMessages.DisplaySystemErrorMessage("Se inserto el nuevo Paciente pero no se pudo obtener los datos.");
            else
            {
                this.PacienteIdHF.Value = PacienteId.ToString();
                SystemMessages.DisplaySystemMessage("Se inserto el nuevo Paciente correctamente.");
            }

            setMinDateCalendar();
        }

    }
    protected void PacienteODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al modificar el Paciente.");
            log.Error("Function PacienteODS_Updated on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void PacienteFV_ItemCreated(object sender, EventArgs e)
    {
        if (PacienteFV.CurrentMode == FormViewMode.Insert)
        {
            Paciente objPaciente = new Paciente();

            RadEditor AntecedenteTxt = (RadEditor)PacienteFV.FindControl("AntecedentesRadEditor");
            if (AntecedenteTxt != null)
                AntecedenteTxt.Content = objPaciente.StaticEmptyAntecedentes;

            RadEditor AlergiasRadEditor = (RadEditor)PacienteFV.FindControl("AlergiasRadEditor");
            if (AlergiasRadEditor != null)
                AlergiasRadEditor.Content = objPaciente.StaticEmptyAntecedentesAlergicos;
        }
    }

    protected void PacienteFV_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        try
        {
            e.Values["PacienteId"] = 0;
            Paciente objPaciente = new Paciente();

            TextBox NroHijosTxt = (TextBox)PacienteFV.FindControl("NroHijosTxt");
            if (NroHijosTxt != null && string.IsNullOrEmpty(NroHijosTxt.Text))
                e.Values["NroHijos"] = 0;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al intentar guardar el nuevo Paciente.");
            log.Error("Function PacienteFV_ItemInserting on page PacieteDetails.aspx", ex);
        }
    }

    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de las aseguradoras.");
            log.Error("Function ClienteODS_Selected on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SavePolizaPacienteLB_Click(object sender, EventArgs e)
    {
        try
        {
            int ClienteId = 0;
            DateTime FechaInicio = DateTime.Now;
            DateTime FechaFin = DateTime.Now.AddDays(1);
            string NumeroPoliza = "";
            decimal MontoTotal = 0;
            decimal MontoFarmacia = 0;
            string Cobertura = "";
            string NombrePlan = "";
            string Lugar = "";
            string Relacion = "TITULAR";

            RadComboBox ClienteDDL = (RadComboBox)PacienteFV.FindControl("ClienteDDL");
            RadDatePicker FechaInicioRDP = (RadDatePicker)PacienteFV.FindControl("FechaInicio");
            RadDatePicker FechaFinRDP = (RadDatePicker)PacienteFV.FindControl("FechaFin");

            TextBox NroPolizaTxt = (TextBox)PacienteFV.FindControl("NroPolizaTxt");
            TextBox MontoTotalText = (TextBox)PacienteFV.FindControl("MontoTotalText");
            RadNumericTextBox MontoFarmaciaText = (RadNumericTextBox)PacienteFV.FindControl("MontoFarmaciaText");
            TextBox CoberturaText = (TextBox)PacienteFV.FindControl("CoberturaText");
            TextBox NombrePlanText = (TextBox)PacienteFV.FindControl("NombrePlanText");
            DropDownList LugarDDL = (DropDownList)PacienteFV.FindControl("LugarDDL");
            TextBox CodigoAseguradoText = (TextBox)PacienteFV.FindControl("CodigoAseguradoText");
            RadComboBox RelacionDDL = (RadComboBox)PacienteFV.FindControl("RelacionDDL");

            if (ClienteDDL != null && FechaInicio != null && FechaFin != null
                    && MontoTotalText != null && NombrePlanText != null && LugarDDL != null)
            {
                ClienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
                int PacienteId = Convert.ToInt32(PacienteIdHF.Value);
                FechaInicio = Convert.ToDateTime(FechaInicioRDP.SelectedDate);
                FechaFin = Convert.ToDateTime(FechaFinRDP.SelectedDate);
                NumeroPoliza = NroPolizaTxt.Text;
                MontoTotal = Convert.ToDecimal(MontoTotalText.Text);
                MontoFarmacia = (decimal)MontoFarmaciaText.Value;
                Cobertura = CoberturaText.Text;
                NombrePlan = NombrePlanText.Text;
                Lugar = LugarDDL.SelectedValue;
                Relacion = RelacionDDL.SelectedValue;

                string CodigoAsegurado = CodigoAseguradoText.Text;//"CodAsegurado";
                string EstadoPoliza = "Activo";

                List<Poliza> ListPoliza = PolizaBLL.GetPolizaByPacienteId(PacienteId);
                if (ListPoliza == null)
                {
                    //replace CodAsegurado and NumeroPoliza
                    PolizaBLL.InsertPoliza(CodigoAsegurado, ClienteId, PacienteId, Relacion, NumeroPoliza, FechaInicio
                        , FechaFin, MontoTotal, MontoFarmacia, Cobertura, NombrePlan, Lugar, EstadoPoliza, DateTime.Now);
                    SystemMessages.DisplaySystemMessage("Póliza Insertada Correctamente.");
                }
                else
                {
                    bool ExistePolizaAseguradora = false;
                    foreach (Poliza ObjPoliza in ListPoliza)
                    {
                        //verificar si existe una poliza con el mismo clienteID(aseguradora y codAsegurado)
                        if (ObjPoliza.ClienteId == ClienteId && ObjPoliza.CodigoAsegurado == CodigoAsegurado)
                            ExistePolizaAseguradora = true;
                    }
                    if (ExistePolizaAseguradora)
                        //insert solo en tbl_Poliza, ya no en tbl_Asegurado
                        SystemMessages.DisplaySystemWarningMessage("El paciente ya tiene una póliza con el cliente seleccionada, cambie de aseguradora para agregar otra póliza al paciente.");
                    else
                    {
                        PolizaBLL.InsertPoliza(CodigoAsegurado, ClienteId, PacienteId, Relacion, NumeroPoliza, FechaInicio
                            , FechaFin, MontoTotal, MontoFarmacia, Cobertura, NombrePlan, Lugar, EstadoPoliza, DateTime.Now);
                        SystemMessages.DisplaySystemMessage("Póliza Insertada Correctamente.");
                    }
                }

                //GridView PolizaGridView = (GridView)PacienteFV.FindControl("PolizaGV");
                //PolizaGridView.DataBind();
                //ClienteDDL.ClearSelection();
                //FechaInicioRDP.Clear();
                //FechaFinRDP.Clear();
                //NroPolizaTxt.Text = "";
                //MontoTotalText.Text = "";
                //NombrePlanText.Text = "";
                //CodigoAseguradoText.Text = "";
            }
            else
            {
                //error un elemento null
                log.Error("Function SavePolizaPacienteLB_Click on page PacieteDetails.aspx, Error getting object the FormView");
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al agregar una poliza al paciente.");
            log.Error("Function SavePolizaPacienteLB_Click on page PacieteDetails.aspx", ex);

        }
        Session["PacienteId"] = this.PacienteIdHF.Value;
        Response.Redirect("PacienteDetails.aspx");
    }
   
    protected void PacientePolizaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de las polizas del paciente.");
            log.Error("Function PacientePolizaODS_Selected on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            (PacienteFV.FindControl("TotalPolizasHF") as HiddenField).Value = (e.ReturnValue as List<Poliza>).Count.ToString();
        }
    }

    protected void PolizaGV_RowCommand ( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Eliminar")
        {
            try
            {
                int polizaId = Convert.ToInt32(e.CommandArgument);

                PolizaBLL.DeletePoliza(polizaId);
                SystemMessages.DisplaySystemMessage("La poliza fue eliminada.");
            }
            catch (Exception q)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al eliminar la poliza del paciente.");
                log.Error("Function PolizaGV_RowCommand CommandName='Eliminar' on page PacieteDetails.aspx", q);
            }
        }
        (sender as GridView).DataBind();
    }
    protected void PolizaGV_RowDataBound ( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int total = 0;
            try { total = Convert.ToInt32((PacienteFV.FindControl("TotalPolizasHF") as HiddenField).Value); }
            catch { }

            if (total <= 1)
            {
                (sender as GridView).Columns[0].Visible = false;
            }
        }
    }
}