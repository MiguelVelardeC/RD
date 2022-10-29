using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Security.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Desgravamen_AgendaMedico : SqlViewStatePage
{
    public static readonly ILog log = LogManager.GetLogger("Standard");

    public int MedicoId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(MedicoIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <=  0)
                MedicoIdHiddenField.Value = "0";
            else
                MedicoIdHiddenField.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/MainPage.aspx");
            return;
        }

        if (!IsPostBack)
        {
            int userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
            dtFechaSeleccion.SelectedDate = DateTime.Now;

            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_ALLREVISIONES");
                gridCitas.MasterTableView.GetColumn("NombreMedico").Display = true;
                loadMedicosComboBox();
                panelAdmin.Visible = true;
                medicosComboBox.Visible = true;
            }
            catch (Exception q)
            {

                CargarDatosMedico(userId);
                panelAdmin.Visible = false;
                log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver todas las agendas ", q);                
            }
            //BindData();
        }
    }

    private void BindData()
    {
        gridCitas.DataBind();
    }

    public void CargarDatosMedico(int userId)
    {
        MedicoDesgravamen objMedico = null;
        try
        {
            objMedico = MedicoDesgravamenBLL.GetMedicoDesgravamenByUserId(userId);
            lblMedicoNombre.Text = objMedico.Nombre;
            MedicoId = objMedico.MedicoDesgravamenId;
        }
        catch (Exception q)
        {
            log.Warn("No pudo cargar los daos del medico con el usuario " + userId, q);
        }
    }
    protected void dtFechaSeleccion_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        BindData();
    }
    protected void gridCitas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);
        log.Info("Clic en la cita " + citaDesgravamenId + " para crearla/editarla");

        Session["CITADESGRAVAMENID"] = citaDesgravamenId;
        Response.Redirect("~/Desgravamen/ExamenMedico.aspx");
    }
    protected void AgendaMedicoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la agenda para medico " + MedicoId, e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener las citas, recargar la página por favor");
            e.ExceptionHandled = true;
            return;
        }
    }
    protected void gridCitas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            Label nombrePA = (Label)e.Item.FindControl("nombrePA");
            ImageButton VerPA = (ImageButton)e.Item.FindControl("VerPA");

            ProgramacionCita objCita = (ProgramacionCita)e.Item.DataItem;

            CitaDesgravamen objCitaMain = CitaDesgravamenBLL.GetCitaDesgravamenById(objCita.CitaDesgravamen);
            PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(objCitaMain.PropuestoAseguradoId);

            nombrePA.Text = objPA.NombreCompletoForDisplay;

            bool sinFichaMedica = true;
            try
            {
                int casoSisaID = 0;
                EnlaceDesgravamenSISA objSisa = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(objCitaMain.CitaDesgravamenId, ref casoSisaID);
                if (objSisa != null && !objSisa.Dirty)
                    sinFichaMedica = false;
            }
            catch
            {
                sinFichaMedica = true;
            }

            if (objCita.Estado == "ATENDIDA")
            {
                if (sinFichaMedica)
                    e.Item.CssClass = "FaltaFicha";
                else
                    e.Item.CssClass = "Atendida";
            }
            else
            {
                if (objCita.Pasada > 0)
                {
                    e.Item.CssClass = "NoVino";
                }

                DateTime ahora = DateTime.UtcNow.AddHours(-4.0);
                TimeSpan diferencia = ahora - objCita.FechaHoraCita;
                if (diferencia.TotalHours > 24.0)
                {
                    VerPA.Visible = false;
                }
            }
        }
    }
    protected void cmdUnDiaAntes_Click(object sender, EventArgs e)
    {
        DateTime? seleccionada = dtFechaSeleccion.SelectedDate;
        if (seleccionada == null)
            seleccionada = DateTime.Now;
        dtFechaSeleccion.SelectedDate = ((DateTime)seleccionada).AddDays(-1.0);

        BindData();
    }
    protected void cmdUnDiaDespues_Click(object sender, EventArgs e)
    {
        DateTime? seleccionada = dtFechaSeleccion.SelectedDate;
        if (seleccionada == null)
            seleccionada = DateTime.Now;
        dtFechaSeleccion.SelectedDate = ((DateTime)seleccionada).AddDays(1.0);

        BindData();
    }

    protected void loadMedicosComboBox()
    {
        List<ComboContainer> list = MedicoDesgravamenBLL.GetMedicoDesgravamenCombo();
        list.Insert(0, new ComboContainer()
        {
            ContainerId = "0",
            ContainerName = "Todos"
        });
        medicosComboBox.DataSource = list;
        medicosComboBox.DataValueField = "ContainerId";
        medicosComboBox.DataTextField = "ContainerName";
        medicosComboBox.DataBind();

    }
    protected void medicosComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        MedicoIdHiddenField.Value = medicosComboBox.SelectedValue;
        updateMedicoTitle();
        BindData();
    }

    private void updateMedicoTitle()
    {
        if (MedicoIdHiddenField.Value != "0"){
            lblMedicoNombre.Text = medicosComboBox.SelectedItem.Text;
        }else{

            lblMedicoNombre.Text = "";
        }
    }
    protected void btnSearchAgendaMedico_Click(object sender, EventArgs e)
    {

    }
}