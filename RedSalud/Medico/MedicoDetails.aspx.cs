using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.Medico;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Poliza;
using Artexacta.App.Medico.BLL;
//using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

public partial class Medico_MedicoDetails : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            SetViewMode();
        }
    }
    private void SetViewMode()
    {
        if (MedicoIdHF.Value.Equals("0"))
        {
            MedicoFV.ChangeMode(FormViewMode.Insert);
            this.Title = this.TitleLabel.Text = "Nuevo Médico";
            MedicoClienteListPanel.Visible = false;
        }
        else
        {
            MedicoFV.ChangeMode(FormViewMode.ReadOnly);
            this.Title = this.TitleLabel.Text = "Editar Médico";
            MedicoClienteListPanel.Visible = true;
        }
        MedicoFV_ModeChanged();
    }
    protected void ProcessSessionParameters()
    {
        int MedicoId = 0;
        if (Session["MedicoId"] != null && !string.IsNullOrEmpty(Session["MedicoId"].ToString()))
        {
            try
            {
                MedicoId = Convert.ToInt32(Session["MedicoId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session MedicoId:" + Session["MedicoId"]);
            }
        }
        MedicoIdHF.Value = MedicoId.ToString();
        Session["MedicoId"] = null;
        int UserID = 0;
        if (Session["UserID"] != null && !string.IsNullOrEmpty(Session["UserID"].ToString()))
        {
            try
            {
                UserID = Convert.ToInt32(Session["UserID"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session UserID:" + Session["UserID"]);
            }
        }
        UserIDHF.Value = UserID.ToString();
        Session["UserID"] = null;
        if (Session["UserName"] != null && !string.IsNullOrEmpty(Session["UserName"].ToString()))
        {
            try
            {
                UserNameHF.Value = Session["UserName"].ToString();
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session UserName:" + Session["UserName"]);
            }
        }
        Session["UserName"] = null;
    }

    protected void MedicoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Médico.");
            log.Error("Function MedicoODS_Selected on page MedicoDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void MedicoODS_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Insertar el nuevo Médico.");
            log.Error("Function MedicoODS_Inserted on page MedicoDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            int MedicoId = (int)e.ReturnValue;
            if (MedicoId <= 0)
                SystemMessages.DisplaySystemErrorMessage("Se inserto el nuevo Médico pero no se pudo obtener los datos.");
            else
            {
                this.MedicoIdHF.Value = MedicoId.ToString();
                SystemMessages.DisplaySystemMessage("Se inserto el nuevo Médico correctamente.");
            }
        }

    }
    protected void MedicoODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al modificar el Médico.");
            log.Error("Function MedicoODS_Updated on page MedicoDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CancelUpdate_Click ( object sender, EventArgs e )
    {
        MedicoFV.ChangeMode(FormViewMode.ReadOnly);
        MedicoFV_ModeChanged();
    }
    protected void RadComboBox_DataBinding ( object sender, EventArgs e )
    {
        RadComboBox rcb = (sender as RadComboBox);
        rcb.Items.Add(new RadComboBoxItem(rcb.Text, rcb.SelectedValue));
    }
    protected void InsertRadComboBox_DataBinding ( object sender, EventArgs e )
    {
        if (!string.IsNullOrWhiteSpace(UserIDHF.Value))
        {
            RadComboBox rcb = (sender as RadComboBox);
            rcb.Items.Add(new RadComboBoxItem(UserNameHF.Value, UserIDHF.Value));
            rcb.SelectedValue = UserIDHF.Value;
        }
    }
    protected void MedicoClienteButton_Command ( object sender, CommandEventArgs e )
    {
        if (e.CommandName == "Eliminar") try
        {
            MedicoBLL.DeleteMedicoCliente(Convert.ToInt32(MedicoIdHF.Value), Convert.ToInt32(e.CommandArgument));
            MedicoClienteRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error("Error al eliminar MedicoCliente", q);
            SystemMessages.DisplaySystemErrorMessage("Error al intentar eliminar el Cliente");
        }
    }
    protected void NewMedicoClienteLB_Click ( object sender, EventArgs e )
    {
        try
        {
            MedicoBLL.InsertMedicoCliente(Convert.ToInt32(MedicoIdHF.Value), Convert.ToInt32(ClienteDDL.SelectedValue));
            MedicoClienteRadGrid.DataBind();
        }catch(Exception q){
            log.Error("Error al insertar MedicoCliente", q);
            SystemMessages.DisplaySystemErrorMessage("Error al intentar Añadir el Cliente");
        }
    }
    protected void MedicoClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los Clientes relacionados al médico.");
            log.Error("Function MedicoClienteODS_Selected on page MedicoDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void MedicoHorarioButton_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Eliminar") try
            {
                MedicoBLL.DeleteMedicoHorario(Convert.ToInt32(e.CommandArgument));
                MedicoHorarioRadGrid.DataBind();
            }
            catch (Exception q)
            {
                log.Error("Error al eliminar MedicoHorario", q);
                SystemMessages.DisplaySystemErrorMessage("Error al intentar eliminar el horario del médico");
            }
    }
    protected void NewMedicoHorarioLB_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime horaInicio = new DateTime(1900,01,01,Convert.ToInt32(HoraInicioHoraDDL.SelectedValue), Convert.ToInt32(HoraInicioMinutoDDL.SelectedValue),0);
            DateTime horaFin = new DateTime(1900, 01, 01, Convert.ToInt32(HoraFinHoraDDL.SelectedValue), Convert.ToInt32(HoraFinMinutoDDL.SelectedValue), 0);

            if (chkLunes.Checked) {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 2, horaInicio, horaFin);
            }
            if (chkMartes.Checked)
            {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 3, horaInicio, horaFin);
            }
            if (chkMiercoles.Checked)
            {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 4, horaInicio, horaFin);
            }
            if (chkJueves.Checked)
            {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 5, horaInicio, horaFin);
            }
            if (chkViernes.Checked)
            {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 6, horaInicio, horaFin);
            }
            if (chkSabado.Checked)
            {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 7, horaInicio, horaFin);
            }
            if (chkDomingo.Checked)
            {
                MedicoBLL.InsertMedicoHorario(Convert.ToInt32(MedicoIdHF.Value), 1, horaInicio, horaFin);
            }

            MedicoHorarioRadGrid.DataBind();
        }
        catch (Exception q)
        {
            log.Error("Error al insertar MedicoCliente", q);
            SystemMessages.DisplaySystemErrorMessage("Error al intentar Añadir el Cliente");
        }
    }
    protected void MedicoHorarioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los horarios relacionados al médico.");
            log.Error("Function MedicoHorarioODS_Selected on page MedicoDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los Clientes.");
            log.Error("Function ClienteODS_Selected on page MedicoDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void MedicoFV_ModeChanged ( object sender, EventArgs e ){
        MedicoFV_ModeChanged();
    }
    protected void MedicoFV_ModeChanged ()
    {
        switch (MedicoFV.CurrentMode)
        {
            case FormViewMode.Edit:
                MedicoClienteRadGrid.Columns[0].Visible = true;
                MedicoHorarioRadGrid.Columns[0].Visible = true;
                NewMedicoClienteLB.Visible = true;
                AgregarHorarioPNL.Visible = true;
                ClienteLabel.Visible = true;
                ClienteDDL.Visible = true;
                CargarHoraDDL();
                CargarMinutosDDL();
                break;
            default:
                MedicoClienteRadGrid.Columns[0].Visible = false;
                MedicoHorarioRadGrid.Columns[0].Visible = false;
                NewMedicoClienteLB.Visible = false;
                AgregarHorarioPNL.Visible = false;
                ClienteLabel.Visible = false;
                ClienteDDL.Visible = false;
                break;
        }
        MedicoClienteRadGrid.DataBind();
        MedicoHorarioRadGrid.DataBind();
    }
    private void CargarHoraDDL() {
        HoraInicioHoraDDL.Items.Clear();
        HoraFinHoraDDL.Items.Clear();
        for (int i = 0; i < 24; i++)
        {
            string hora = i.ToString();

            if (i < 10) hora = "0" + hora;
            
            ListItem itemI = new ListItem(hora, hora);
            ListItem itemF = new ListItem(hora, hora);

            HoraInicioHoraDDL.Items.Add(itemI);
            HoraFinHoraDDL.Items.Add(itemF);
        }
        var fecha = DateTime.Now;
        string horaI = fecha.ToString("HH");
        string horaF = DateTime.Now.ToString("HH");

        HoraInicioHoraDDL.SelectedValue = horaI;
        HoraInicioHoraDDL.DataBind();
        HoraFinHoraDDL.SelectedValue = horaF;
        HoraFinHoraDDL.DataBind();
    }
    private void CargarMinutosDDL()
    {
        HoraInicioMinutoDDL.Items.Clear();
        HoraFinMinutoDDL.Items.Clear();
        for (int i = 0; i < 60; i++)
        {
            string minuto = i.ToString();

            if (i < 10) minuto = "0" + minuto;

            ListItem itemI = new ListItem(minuto, minuto);
            ListItem itemF = new ListItem(minuto, minuto);
            HoraInicioMinutoDDL.Items.Add(itemI);
            HoraFinMinutoDDL.Items.Add(itemF);
        }
        var fecha = DateTime.Now;
        string minuteI = fecha.ToString("mm");
        string minuteF = DateTime.Now.AddMinutes(20).ToString("mm");

        HoraInicioMinutoDDL.SelectedValue = minuteI;//(minuteI < 10 ? "0" + minuteI.ToString() : minuteI.ToString());
        HoraInicioMinutoDDL.DataBind();

        HoraFinMinutoDDL.SelectedValue = minuteF;// (minuteF < 10 ? "0" + minuteF.ToString() : minuteF.ToString());
        HoraFinMinutoDDL.DataBind();
        
    }
}