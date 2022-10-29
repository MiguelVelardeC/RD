using Artexacta.App.CitasVideoLLamada.BLL;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedClientePrestaciones.BLL;
using Artexacta.App.RedMedica.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class CitaVideoLLamadaDetails : System.Web.UI.Page
{

    private static readonly ILog log = LogManager.GetLogger("Standard");
    private bool isUpdating = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        string CodigoRedMedica = System.Web.Configuration.WebConfigurationManager.AppSettings["CodigoRedMedica"];
        CodigoRedMedicaHF.Value = CodigoRedMedica;
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            SetViewMode();

        }

    }

    private void SetViewMode()
    {
        int citId = 0;
        try
        {
            citId = Convert.ToInt32(citIdHF.Value);
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            Response.Redirect("~/Cliente/ClienteList.aspx");
        }
        //CitaFV.ChangeMode(FormViewMode.ReadOnly);
        PolizaFV.ChangeMode(FormViewMode.ReadOnly);
    }

    private void ProcessSessionParameters()
    {
        int citId = 0;
        if (Session["citId"] != null && !string.IsNullOrEmpty(Session["citId"].ToString()))
        {
            try
            {
                citId = Convert.ToInt32(Session["citId"].ToString());
                TitleLabel.Text = "Editar Cliente";
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
                log.Error("No se pudo obtener el citId de Session['citId']", ex);
                Response.Redirect("~/Cliente/ClienteList.aspx");
            }
        }
        citIdHF.Value = citId.ToString();
        polizaIdHF.Value = CitaVideoLLamadaBLL.GetCitaByCitaId(citId).PolizaId.ToString();
        aseguradoIdHF.Value = CitaVideoLLamadaBLL.GetCitaByCitaId(citId).AseguradoId.ToString();
        //var erew = CitaVideoLLamadaBLL.GetPolizaById(Convert.ToInt32(polizaIdHF.Value));
        Session["citId"] = null;
    }

    protected void CitaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            log.Error("Ocurrió un error al Obtener el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PolizaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            log.Error("Ocurrió un error al Obtener el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}