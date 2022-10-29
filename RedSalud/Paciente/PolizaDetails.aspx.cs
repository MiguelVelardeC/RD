using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using System.Data;

public partial class Paciente_PolizaDetails : System.Web.UI.Page
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
        if (PolizaIdHF.Value.Equals("0"))
        {
            PolizaFV.ChangeMode(FormViewMode.Insert);
            this.TitleLabel.Text = "Nuevo Poliza";
            this.Title = "Nuevo Poliza";
        }
        else
        {
        //    PolizaFV.ChangeMode(FormViewMode.ReadOnly);
            this.TitleLabel.Text = "Editar Poliza";
            this.Title = "Editar Poliza";
        //    setMinDateCalendar();
        }
    }
    protected void setMinDateCalendar()
    {
        //PolizaFV.ChangeMode(FormViewMode.ReadOnly);
        //RadDatePicker FechaInicio = (RadDatePicker)PolizaFV.FindControl("FechaInicio");
        //FechaInicio.MinDate = DateTime.Now.AddYears(-1);
        //RadDatePicker FechaFin = (RadDatePicker)PolizaFV.FindControl("FechaFin");
        //FechaFin.MinDate = DateTime.Now.AddYears(-1);    
    }
    protected void ProcessSessionParameters()
    {
        int PolizaId = 0;
        if (Session["PolizaId"] != null && !string.IsNullOrEmpty(Session["PolizaId"].ToString()))
        {
            try
            {
                PolizaId = Convert.ToInt32(Session["PolizaId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session PolizaId:" + Session["PolizaId"]);
            }
        }
        PolizaIdHF.Value = PolizaId.ToString();
        Session["PolizaId"] = null;
    }

    protected void PolizaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Poliza.");
            log.Error("Function PolizaODS_Selected on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PolizaODS_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Insertar la nueva Poliza.");
            log.Error("Function PolizaODS_Inserted on page PolizaDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            int PolizaId = (int)e.ReturnValue;
            if (PolizaId <= 0)
                SystemMessages.DisplaySystemErrorMessage("Se inserto la nueva Poliza pero no se pudo obtener los datos.");
            else
            {
                this.PolizaIdHF.Value = PolizaId.ToString();
                SystemMessages.DisplaySystemMessage("Se inserto la nueva Poliza correctamente.");
            }

            setMinDateCalendar();
        }

    }
    protected void PolizaODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al modificar la Póliza.");
            log.Error("Function PolizaODS_Updated on page PolizaDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SavePolizaLB_Click ( object sender, EventArgs e )
    {
        bool insert = true;
        try
        {
            int ClienteId = 0;
            int PacienteId = 0;
            DateTime FechaInicio = DateTime.Now;
            DateTime FechaFin = DateTime.Now.AddDays(1);
            string NumeroPoliza = "";
            decimal MontoTotal = 0;
            decimal MontoFarmacia = 0;
            string Cobertura = "";
            string NombrePlan = "";
            string Lugar = "";

            RadDatePicker FechaInicioRDP = (RadDatePicker)PolizaFV.FindControl("FechaInicio");
            RadDatePicker FechaFinRDP = (RadDatePicker)PolizaFV.FindControl("FechaFin");

            TextBox NroPolizaTxt = (TextBox)PolizaFV.FindControl("NroPolizaTxt");
            RadNumericTextBox MontoTotalText = (RadNumericTextBox)PolizaFV.FindControl("MontoTotalText");
            RadNumericTextBox MontoFarmaciaText = (RadNumericTextBox)PolizaFV.FindControl("MontoFarmaciaText");
            TextBox CoberturaText = (TextBox)PolizaFV.FindControl("CoberturaText");
            TextBox NombrePlanText = (TextBox)PolizaFV.FindControl("NombrePlanText");
            DropDownList LugarDDL = (DropDownList)PolizaFV.FindControl("LugarDDL");
            RadComboBox RelacionDDL = (RadComboBox)PolizaFV.FindControl("RelacionDDL");

            TextBox CodigoAseguradoText = (TextBox)PolizaFV.FindControl("CodigoAseguradoText");
            RadComboBox EstadoDDL = (RadComboBox)PolizaFV.FindControl("EstadoDDL");

            if (FechaInicio != null && FechaFin != null && LugarDDL != null
                    && MontoTotalText != null && NombrePlanText != null)
            {
                int PolizaId = Convert.ToInt32(PolizaIdHF.Value);

                FechaInicio = Convert.ToDateTime(FechaInicioRDP.SelectedDate);
                FechaFin = Convert.ToDateTime(FechaFinRDP.SelectedDate);
                NumeroPoliza = NroPolizaTxt.Text;
                MontoTotal = (decimal)MontoTotalText.Value;
                MontoFarmacia = (decimal)MontoFarmaciaText.Value;
                Cobertura = CoberturaText.Text;
                NombrePlan = NombrePlanText.Text;
                Lugar = LugarDDL.SelectedValue;
                string Relacion = RelacionDDL.SelectedValue;

                string CodigoAsegurado = CodigoAseguradoText.Text;//"CodAsegurado";
                string EstadoPoliza = EstadoDDL.SelectedValue;
                insert = PolizaId <= 0;
                if (!insert)
                {
                    HiddenField ClienteHF = (HiddenField)PolizaFV.FindControl("ClientIdHF");
                    ClienteId = Convert.ToInt32(ClienteHF.Value);

                    PolizaBLL.UpdatePoliza(PolizaId, NumeroPoliza, FechaInicio, FechaFin, MontoTotal, MontoFarmacia, 
                        Cobertura, NombrePlan, Lugar, CodigoAsegurado, ClienteId, Relacion, EstadoPoliza, DateTime.UtcNow);
                    SystemMessages.DisplaySystemMessage("Póliza Actualizo Correctamente.");
                }
                else
                {
                    RadComboBox PacienteDDL = (RadComboBox)PolizaFV.FindControl("PacienteDDL");
                    RadComboBox ClienteDDL = (RadComboBox)PolizaFV.FindControl("ClienteDDL");

                    ClienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
                    PacienteId = Convert.ToInt32(PacienteDDL.SelectedValue);
                    //replace CodAsegurado and NumeroPoliza
                    Session["PolizaId"] = PolizaBLL.InsertPoliza(CodigoAsegurado, ClienteId, PacienteId, Relacion, NumeroPoliza, FechaInicio
                        , FechaFin, MontoTotal, MontoFarmacia, Cobertura, NombrePlan, Lugar, EstadoPoliza, DateTime.Now);
                    SystemMessages.DisplaySystemMessage("Póliza Insertada Correctamente.");
                }
            }
            else
            {
                //error un elemento null
                log.Error("Function SavePolizaLB_Click on page PolizaDetails.aspx, Error getting object the FormView");
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al agregar una Poliza.");
            log.Error("Function SavePolizaLB_Click on page PolizaDetails.aspx", ex);

        }

        if (insert)
            Response.Redirect("PolizaDetails.aspx");
        else
            PolizaFV.ChangeMode(FormViewMode.ReadOnly);
    }
   
    protected void PolizaPolizaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de las polizas del Poliza.");
            log.Error("Function PolizaPolizaODS_Selected on page PacieteDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de los Clientes.");
            log.Error("Function ClienteODS_Selected on page PolizaDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PacienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {

        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de los Pacientes.");
            log.Error("Function PacienteODS_Selected on page PolizaDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}