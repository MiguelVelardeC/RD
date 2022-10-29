using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.Utilities.Email;

public partial class Desgravamen_DetalleLaboratoriosPropuestoAsegurado : SqlViewStatePage
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            int citaDesgravamenId = this.CitaDesgravamenId;
            this.EstudiosGridView.DataBind();
            
            if (citaDesgravamenId > 0)
            {
                CitaDesgravamen objCita = null;
                try
                {
                    objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaDesgravamenId);
                    PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(objCita.PropuestoAseguradoId);
                    List<PropuestoAseguradoEstudio> listaLabos = CitaDesgravamenBLL.GetListaPropuestoAseguradoEstudioByCitaId(CitaDesgravamenId);
                    IEnumerable<PropuestoAseguradoEstudio> labosProveedor = 
                        listaLabos.Where<PropuestoAseguradoEstudio>(x => x.ProveedorMedicoId == ProveedorMedicoId);
                    
                    NombreTextBox.Text = objPA.NombreCompleto;
                    CedulaTextBox.Text = objPA.CarnetIdentidad;
                    FechaNacimientoTextBox.Text = objPA.FechaNacimientoForDisplay;
                    CobrarAseguradoTextBox.Text = objCita.CobroAseguradoForDisplay;
                    EstudiosFileManager.ObjectId = citaDesgravamenId;
                    EstudiosFileManager.TotalEstudiosRealizadosNumber = Realizados().Count;
                    if (!objCita.CobroFinanciera)
                        PanelCobro.Visible = true;

                    SetVisibilityPanels(objPA, labosProveedor.First<PropuestoAseguradoEstudio>());
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get data for Propuesto Asegurado with id: " + citaDesgravamenId, ex);
                    return;
                }

                ProgramacionCita objProgramacionCita = null;
                try
                {
                    objProgramacionCita = PropuestoAseguradoBLL.GetProgramacionCita(CitaDesgravamenId);
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get data for ProgramacionCita", ex);
                }
                if (objProgramacionCita == null)
                    return;

                //TimeSpan ts = DateTime.Now - objProgramacionCita.FechaHoraCita;
                //if (objProgramacionCita.Estado.ToLowerInvariant() == CitaMedica.EstadoCita.Atendida.ToString().ToLowerInvariant() && 
                //    ts.Days > 7)
                //{
                //    MarcarNoRealizadoButton.Visible = false;
                //    MarcarRealizadoButton.Visible = false;
                //}
            }
        }
    }

    private List<Telerik.Web.UI.GridDataItem> Realizados()
    {
        List<Telerik.Web.UI.GridDataItem> Response = (from item in this.EstudiosGridView.Items.Cast<Telerik.Web.UI.GridDataItem>()
                                                        where item["Realizado"].Text.ToUpper() == "SI"
                                                        select item).ToList();
        return Response;
    }

    private void SetVisibilityPanels(PropuestoAsegurado objPA, PropuestoAseguradoEstudio objPAE)
    {
        if (objPAE.Aprobado)
        {
            MarcarNoRealizadoButton.Visible = false;
            MarcarRealizadoButton.Visible = false;
            EstudiosFileManager.CanDeleteFiles = false;
        }
        else
        {
            MarcarNoRealizadoButton.Visible = true;
            MarcarRealizadoButton.Visible = true;
            EstudiosFileManager.CanDeleteFiles = true;
        }
    }

    private void ProcessSessionParameters()
    {
        if (Session["CitaDesgravamenId"] != null && !string.IsNullOrEmpty(Session["CitaDesgravamenId"].ToString()))
        {
            try
            {
                int citaId = Convert.ToInt32(Session["CitaDesgravamenId"].ToString());
                CitaDesgravamenIdHiddenField.Value = citaId.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Cannot get turnoId from SESSION", ex);
            }
        }
        if (Session["EstudioId"] != null && !string.IsNullOrEmpty(Session["EstudioId"].ToString()))
        {
            try
            {
                int estudioId = Convert.ToInt32(Session["EstudioId"].ToString());
                EstudioIdCitaLaboHiddenField.Value = estudioId.ToString();
                EstudiosFileManager.EstudioId = estudioId;                
            }
            catch (Exception ex)
            {
                log.Error("Cannot get turnoId from SESSION", ex);
            }
        }


        Session["CitaDesgravamenId"] = null;
        Session["EstudioId"] = null;

        int userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
        List<ProveedorDesgravamen> listaProveedor = ProveedorMedicoBLL.GetProveedorMedicoByUserId(userId);
        if (listaProveedor != null && listaProveedor.Count > 0)
            ProveedorMedicoIdHiddenField.Value = listaProveedor[0].ProveedorMedicoId.ToString();

        EstudiosFileManager.ProveedorMedicoId = ProveedorMedicoId;
    }
    protected void EstudiosDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void MarcarRealizadoButton_Click(object sender, EventArgs e)
    {
        bool noHayError = true;
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<int> estudios = js.Deserialize<List<int>>(EstudiosSeleccionadosHiddenField.Value);
            EstudioBLL.MarcarEstudiosComoRealizado(CitaDesgravamenId, estudios);
            EstudiosGridView.DataBind();
            EstudiosFileManager.TotalEstudiosRealizadosNumber = Realizados().Count;
            EstudiosSeleccionadosHiddenField.Value = "[]";
            SystemMessages.DisplaySystemMessage("Los estudios seleccionados han sido marcados como realizados");

            
        }
        catch (Exception ex)
        {
            log.Error("Cannot mark estudios as realized", ex);
            SystemMessages.DisplaySystemErrorMessage("Los estudios seleccionados no se pudieron marcar como realizados");
            noHayError = false;
        }

        if (noHayError)
        {
            EmailUtilities.CheckFirstAndSendEmailLaboCitaConcluidos(CitaDesgravamenId);
        }
    }

    protected void MarcarNoRealizadoButton_Click(object sender, EventArgs e)
    {
        try
        {
            //int TotalEstudiosRealizadosNumber = EstudiosFileManager.TotalEstudiosRealizadosNumber - 1;
            //int UploadedFilesNumber = EstudiosFileManager.UploadedFilesNumber;

            //if (TotalEstudiosRealizadosNumber >= UploadedFilesNumber)
            //{
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<int> estudios = js.Deserialize<List<int>>(EstudiosSeleccionadosHiddenField.Value);
                EstudioBLL.MarcarEstudiosComoNoRealizado(CitaDesgravamenId, estudios);
                EstudiosGridView.DataBind();
                EstudiosFileManager.TotalEstudiosRealizadosNumber = Realizados().Count;
                EstudiosSeleccionadosHiddenField.Value = "[]";
                SystemMessages.DisplaySystemMessage("Los estudios seleccionados han sido marcados como NO realizados");
            //}else
            //{
            //    throw new ArgumentException("No se puede desmarcar un Estudio que ya tiene un archivo adjuntado.");
            //}
            
        }
        catch (Exception ex)
        {
            log.Error("Cannot mark estudios as realized", ex);
            SystemMessages.DisplaySystemErrorMessage("Los estudios seleccionados no se pudieron marcar como NO realizados");
        }
    }

    protected void OrdenServicioBtn_Click(object sender, EventArgs e)
    {
        Session["CITADESGRAVAMENID"] = CitaDesgravamenId;
        Session["PAGINABACK"] = "DetalleLaboratoriosPropuestoAsegurado.aspx";
        Response.Redirect("OrdenDeServicioImprimir.aspx");
    }
}