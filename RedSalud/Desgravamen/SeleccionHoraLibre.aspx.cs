using Artexacta.App.Desgravamen;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Utilities.SystemMessages;
using System.Text;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class Desgravamen_SeleccionHoraLibre : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int CitaDesgravamenId
    {
        get
        {
            int citaDesgravamenId = 0;
            try
            {
                citaDesgravamenId = Convert.ToInt32(CitaDesgravamenIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'CitaDesgravamenIdHiddenField.Value' to int value", ex);
            }
            return citaDesgravamenId;
        }
    }

    public bool NecesitaExamen
    {
        get
        {
            bool necesitaExamen = false;
            try
            {
                necesitaExamen = Convert.ToBoolean(NecesitaExamenHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'necesitaExamenHiddenField.Value' to bool value", ex);
            }
            return necesitaExamen;
        }
        set
        {
            NecesitaExamenHiddenField.Value = value.ToString();
        }
    }

    public bool CitaParaProveedor
    {
        get
        {
            bool citaParaProveedor = false;
            try
            {
                citaParaProveedor = Convert.ToBoolean(CitaParaProveedorHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'CitaParaProveedorHiddenField.Value' to bool value", ex);
            }
            return citaParaProveedor;
        }
        set
        {
            CitaParaProveedorHiddenField.Value = value.ToString();
        }
    }

    public bool SelectorSoloFecha
    {
        get
        {
            bool selectorSoloFecha = false;
            try
            {
                selectorSoloFecha = Convert.ToBoolean(SelectorSoloFechaHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'SelectorSoloFechaHiddenField.Value' to bool value", ex);
            }
            return selectorSoloFecha;
        }
        set
        {
            SelectorSoloFechaHiddenField.Value = value.ToString();
        }
    }

    public int ProveedorMedicoId
    {
        get
        {
            int proveedorMedicoId = 0;
            try
            {
                proveedorMedicoId = Convert.ToInt32(ProveedorMedicoIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'ProveedorMedicoIdHiddenField.Value' to int value", ex);
            }
            return proveedorMedicoId;
        }
        set
        {
            ProveedorMedicoIdHiddenField.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            CargarDatosCitaRevisionMedica();
            CargarDatosCitaDesgravamen();
        }
    }

    private void CargarDatosCitaRevisionMedica()
    {
        ProgramacionCita objCita = null;
        try
        {
            objCita = PropuestoAseguradoBLL.GetProgramacionCita(CitaDesgravamenId);
        }
        catch (Exception q)
        {
            log.Error("No tiene cita definida", q);
            SystemMessages.DisplaySystemErrorMessage("La cita no esta definida, volver a página de inicio");
            return;
        }
        if (objCita == null)
        {
            MedicoLabel.Text = "Debe marcar cita";
            FechaHoraCita.Text = "-";
        }
        else
        {
            MedicoLabel.Text = objCita.NombreMedico;
            FechaHoraCita.Text = objCita.FechaHoraCitaForDisplay;
        }
    }

    private void ProcessSessionParameters()
    {
        if (Session["CitaDesgravamenId"] != null && !string.IsNullOrEmpty(Session["CitaDesgravamenId"].ToString()))
        {
            try
            {
                int citaDesgravamenId = Convert.ToInt32(Session["CitaDesgravamenId"].ToString());
                CitaDesgravamenIdHiddenField.Value = citaDesgravamenId.ToString();                
                CitaLabel.Text = citaDesgravamenId.ToString();
                CitaDesgravamen cita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaDesgravamenId);
                ClienteIdHiddenField.Value = cita.ClienteId.ToString();
               
                
            }
            catch (Exception ex)
            {
                log.Error("Cannot get turnoId from SESSION", ex);
            }

        }

        /*if (Session["ClienteId"] != null && !string.IsNullOrEmpty(Session["ClienteId"].ToString()))
        {
            try
            {
                int clienteId = Convert.ToInt32(Session["ClienteId"].ToString());
                ClienteIdHiddenField.Value = clienteId.ToString();

                //CitaLabel.Text = citaDesgravamenId.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Cannot get ClienteId from SESSION", ex);
            }

        }*/

        Session["CitaDesgravamenId"] = null;
    }

    private void CargarDatosCitaDesgravamen()
    {
        CitaDesgravamen objCita = null;
        try
        {
            objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(CitaDesgravamenId);
        }
        catch (Exception q)
        {
            log.Error("No tiene cita definida", q);
            SystemMessages.DisplaySystemErrorMessage("La cita no esta definida, volver a página de inicio");
            return;
        }
        CiudadIdHiddenField.Value = objCita.CiudadId;
        NecesitaExamen = objCita.NecesitaExamen;

        if (!NecesitaExamen)
        {
            SeleccionCitaRevisionMedicaPanel.Visible = false;
        }
        if (!objCita.NecesitaLaboratorio)
        {
            SeleccionCitaLaboPanel.Visible = false;
        }

        PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(objCita.PropuestoAseguradoId);
        PALabel.Text = objPA.NombreCompletoForDisplay;
    }
    protected void SeleccionHoraGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        CitaParaProveedor = true;
        ProveedorMedicoId = 0;
        SelectorSoloFecha = false;
        try
        {
            ProveedorMedicoId = Convert.ToInt32(e.CommandName);
            
            GridDataItem dataItm = e.Item as GridDataItem;
            string value = dataItm["EstudioId"].Text;
            EstudioSelected.Value = value;
        }
        catch
        {
        }

        try
        {
            SelectorSoloFecha = !Convert.ToBoolean(e.CommandArgument);
        }
        catch { }

        if (ProveedorMedicoId == 0)
        {
            log.Error("No puede gravar un proveedor medico con 0");
            SystemMessages.DisplaySystemErrorMessage("Vuelva a intentar el registro por favor");

            SeleccionHoraWindow.VisibleOnPageLoad = false;
            return;
        }

        if (SelectorSoloFecha)
        {
            GuardarCitaButton.CommandName = "Fecha";
            GuardarCitaButton.CommandArgument = ProveedorMedicoId.ToString();
        }
        else
        {
            GuardarCitaButton.CommandName = "FechaHora";
            GuardarCitaButton.CommandArgument = "0";
        }

        CargarGridsFechasYHorarios();
    }
    protected void SeleccionHoraGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            ProgramacionCitaLabo objItem = (ProgramacionCitaLabo)e.Item.DataItem;

            ImageButton SelectFechaHoraButton = (ImageButton)e.Item.FindControl("SelectFechaHoraButton");

            SelectFechaHoraButton.CommandArgument = true.ToString();

            if (NecesitaExamen && !objItem.NecesitaCita)
                SelectFechaHoraButton.Visible = false;

            if (!NecesitaExamen && !objItem.NecesitaCita)
            {
                SelectFechaHoraButton.Visible = true;
                SelectFechaHoraButton.CommandArgument = false.ToString();
            }
        }
    }
    protected void SeleccionHoraDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void SeleccionHoraWindow_Load(object sender, EventArgs e)
    {

    }
    protected void CambiarCitaButton_Click(object sender, EventArgs e)
    {
        CitaParaProveedor = false;

        CargarGridsFechasYHorarios();
    }

    private void CargarGridsFechasYHorarios()
    {
        DataTable dtDias = new DataTable();
        dtDias.Columns.Add("DiaFecha");

        int nbDias = Artexacta.App.Configuration.Configuration.GetDESGNumeroDiasHoraLibre();
        DateTime now = DateTime.UtcNow;
        now = now.AddHours(-4);
        for (int i = 0; i < nbDias; i++)
        {
            DataRow row = dtDias.NewRow();
            row["DiaFecha"] = now.Day;
            dtDias.Rows.Add(row);

            now = now.AddDays(1);
        }

        FechaSeleccionRepeater.DataSource = dtDias;
        FechaSeleccionRepeater.DataBind();

        if (!CitaParaProveedor)
        {
            // Toma la ifnormación para cargar el horario del medico
            CitaDesgravamen objCita = null;
            try
            {
                objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(CitaDesgravamenId);
            }
            catch (Exception q)
            {
                log.Error("No tiene cita definida", q);
                SystemMessages.DisplaySystemErrorMessage("La cita no esta definida, volver a página de inicio");
                return;
            }
            

            List<HoraLibre> horas = new List<HoraLibre>();
            try
            {
                if (ClienteIdHiddenField.Value != null && !string.IsNullOrEmpty(ClienteIdHiddenField.Value))
                {
                    int intClienteId = Convert.ToInt32(ClienteIdHiddenField.Value);
                    horas = HoraLibreBLL.GetHoraLibreMedico(objCita.CiudadId, intClienteId);
                }
            }
            catch (Exception q)
            {
                log.Error("There are no time ranges for this client in MedicoHorarios clientId: "+ClienteIdHiddenField.Value, q);
                SystemMessages.DisplaySystemErrorMessage("No existen Medicos con horarios, favor contactar al Administrador");
            }
            

            FechaHoraRepeater.DataSource = horas;
            FechaHoraRepeater.DataBind();

        }
        else
        {
            // Carga los proveedores (laboratorios)
            string estudio = EstudioSelected.Value;
            string cliente = ClienteIdHiddenField.Value;
            List<HoraLibre> horas = new List<HoraLibre>();
            try 
	        {	        
		        if (ClienteIdHiddenField.Value != null && !string.IsNullOrEmpty(ClienteIdHiddenField.Value)
                && estudio != null && !string.IsNullOrEmpty(estudio))
                {
                    int intEstudio = Convert.ToInt32(estudio);
                    int intCliente = Convert.ToInt32(cliente);
                    horas = HoraLibreBLL.GetHoraLibreProveedor(ProveedorMedicoId, intEstudio, intCliente);
                }
	        }
	        catch (Exception)
	        {
                SystemMessages.DisplaySystemErrorMessage("Hubo un problema cargando los horarios por Estudio");
	        }
            

            FechaHoraRepeater.DataSource = horas;
            FechaHoraRepeater.DataBind();
        }

        if (SelectorSoloFecha)
        {
            SolamenteFechaPanel.Visible = true;
            FechaYHoraPanel.Visible = false;
        }
        else
        {

            SolamenteFechaPanel.Visible = false;
            FechaYHoraPanel.Visible = true;
        }

        SeleccionHoraWindow.VisibleOnPageLoad = true;
    }
    protected void GuardarCitaButton_Click(object sender, EventArgs e)
    {
        MensajesPanel.Visible = false;

        char[] sep = { ',' };

        if (!CitaParaProveedor)
        {
            log.Debug("Va a guardar la cita para el médico en " + FechaHoraSeleccionadaHiddenField.Value);

            string[] fechaMedico = FechaHoraSeleccionadaHiddenField.Value.Split(sep);

            DateTime fechaSeleccionada = new DateTime(Convert.ToInt64(fechaMedico[0]));
            int medicoId = Convert.ToInt32(fechaMedico[1]);
            MedicoDesgravamen objMedico = MedicoDesgravamenBLL.GetMedicoDesgravamenById(medicoId);

            PropuestoAseguradoBLL.SaveProgramacionCita(CitaDesgravamenId, medicoId, objMedico.ProveedorMedicoId, fechaSeleccionada, 0);

            CargarDatosCitaRevisionMedica();
            SeleccionHoraGridView.DataBind();

            SystemMessages.DisplaySystemMessage("La cita ha sido programada exitosamente");
        }
        else
        {
            DateTime fechaSeleccionada = DateTime.MinValue;
            int proveedorId = 0;
            int estudioId = Convert.ToInt32(EstudioSelected.Value);

            log.Debug("Va a guardar la cita para el proveedor en " + FechaHoraSeleccionadaHiddenField.Value);

            if (GuardarCitaButton.CommandName.Equals("Fecha"))
            {
                fechaSeleccionada = FechaLaboratorio.SelectedDate.Value;
                proveedorId = Convert.ToInt32(GuardarCitaButton.CommandArgument);
            }
            else
            {
                string[] fechaProveedor = FechaHoraSeleccionadaHiddenField.Value.Split(sep);

                fechaSeleccionada = new DateTime(Convert.ToInt64(fechaProveedor[0]));
                proveedorId = Convert.ToInt32(fechaProveedor[1]);
            }
            PropuestoAseguradoBLL.SaveProgramacionCita(CitaDesgravamenId, 0, proveedorId, fechaSeleccionada, estudioId);

            SeleccionHoraGridView.DataBind();

            SystemMessages.DisplaySystemMessage("La cita ha sido programada exitosamente");
        }


        SeleccionHoraWindow.VisibleOnPageLoad = false;
    }
    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        MensajesPanel.Visible = false;

        string resultado = DesgravamenManager.CheckOrdenDeServicioParaImprimir(CitaDesgravamenId, NecesitaExamen);

        if (!string.IsNullOrWhiteSpace(resultado))
        {
            MensajesPanel.Visible = true;
            mensajePanelLabel.Text = resultado;
        }

        if (!MensajesPanel.Visible)
        {
            Session["CITADESGRAVAMENID"] = CitaDesgravamenId;
            Response.Redirect("OrdenDeServicioImprimir.aspx");
        }
    }
    protected void EliminarCitaButton_Click(object sender, EventArgs e)
    {
        PropuestoAseguradoBLL.SaveProgramacionCita(CitaDesgravamenId, 0, 0, DateTime.MinValue, 0);

        ProgramacionCitaLaboBLL.Delete(CitaDesgravamenId);

        CargarDatosCitaRevisionMedica();
        SeleccionHoraGridView.DataBind();

        SystemMessages.DisplaySystemMessage("Se eliminó la cita para la revisión médica y los laboratorios");
    }
}