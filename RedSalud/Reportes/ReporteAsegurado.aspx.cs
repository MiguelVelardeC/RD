using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Asegurado;
using Artexacta.App.Asegurado.BLL;
using Artexacta.App.Medico;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Microsoft.Reporting.WebForms;
using Telerik.Web.UI;

public partial class Reportes_ReporteAsegurado : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void GenerarReporte_Click ( object sender, EventArgs e )
    {
        try
        {
            int aseguradoraId = int.Parse(ClienteDDL.SelectedValue);
            Asegurado asegurado = AseguradoBLL.GetAseguradoById(aseguradoraId,
                CodigoAseguradoTextBox.Text, NroPolizaTextBox.Text);
            AseguradoIdHF.Value = aseguradoraId.ToString();
            NumeroPolizaHF.Value = asegurado.NroPoliza;
            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("Aseguradora", asegurado.Aseguradora);
            parameters[1] = new ReportParameter("Asegurado", asegurado.Nombre);
            parameters[2] = new ReportParameter("NroPoliza", asegurado.NroPoliza);
            parameters[3] = new ReportParameter("MontoAsignado", asegurado.MontoAsignado.ToString());

            AseguradosReportViewer.LocalReport.SetParameters(parameters);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al generar el Reporte.");
            log.Error("Function GenerarReporte_Click on page ReporteAsegurado.aspx", q);
        }
    }

    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Aseguradoras.");
            log.Error("Function ClienteODS_Selected on page ReporteAsegurado.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}