using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Artexacta.App.Consolidacion;
using Artexacta.App.Consolidacion.BLL;

public partial class Reportes_ReporteConsolidado : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
        }
    }

    protected void ProcessSessionParameters()
    {
        int ConsolidacionId = 0;
        if (Session["ConsolidacionId"] != null && !string.IsNullOrEmpty(Session["ConsolidacionId"].ToString()))
        {
            try
            {
                ConsolidacionId = Convert.ToInt32(Session["ConsolidacionId"]);
                GenerarReporte(ConsolidacionId);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session ConsolidacionId:" + Session["ConsolidacionId"]);
            }
        }
        if (ConsolidacionId <= 0)
            this.ErrorPanel.Visible = true;

        ConsolidacionIdHF.Value = ConsolidacionId.ToString();
        Session["ConsolidacionId"] = null;
    }

    protected void GenerarReporte(int ConsolidacionId) {
        try
        {
            //int ConsolidacionId = Convert.ToInt32(ConsolidacionIdHF.Value);
            Consolidacion objConsolidacion = ConsolidacionBLL.GetConsolidacionDetails(ConsolidacionId);
            if (objConsolidacion != null)
            {
                ReportParameter[] parameters = new ReportParameter[4];

                parameters[0] = new ReportParameter("UserAdmin", objConsolidacion.UserName);
                parameters[1] = new ReportParameter("FechaConsolidado", objConsolidacion.FechaCreacion.ToString());
                parameters[2] = new ReportParameter("FechaRevisado", objConsolidacion.FechaHasta.ToShortDateString());
                parameters[3] = new ReportParameter("MontoTotal", objConsolidacion.MontoTotal.ToString());

                ConsolidacionReportViewer.LocalReport.SetParameters(parameters);
            }
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al generar el Reporte.");
            log.Error("Function GenerarReporte_Click on page ReporteAsegurado.aspx", q);
        }
    }
    
}