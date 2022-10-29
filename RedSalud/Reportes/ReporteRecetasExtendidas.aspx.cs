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
using Cognos.Negocio;
using log4net;
using Microsoft.Reporting.WebForms;
using Telerik.Web.UI;

public partial class Reportes_ReporteRecetasExtendidas : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    static bool _isSqlTypesLoaded = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                _isSqlTypesLoaded = true;
            }

            using (var context = new Negocio())
            {
                var clientes = (from c in context.tbl_RED_Cliente
                                select c).ToList();                
                clientes.Insert(0, new tbl_RED_Cliente { ClienteId = 0, NombreJuridico = "Todos" });
                ClienteDDL.DataSource = clientes;
                ClienteDDL.SelectedValue = "0";
                ClienteDDL.DataBind();

                var especialidades = (from es in context.tbl_CLA_Especialidad
                                      select es).ToList();
                especialidades.Insert(0, new tbl_CLA_Especialidad { EspecialidadId = 0, Nombre = "Todos" });
                EspecialidadDDL.DataSource = especialidades;
                EspecialidadDDL.SelectedValue = "0";
                EspecialidadDDL.DataBind();

                var date = DateTime.Today;
                FechaIniDP.SelectedDate = new DateTime(date.Year, date.Month, 1);
                FechaFinDP.SelectedDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            }
        }
    }
    protected void GenerarReporte_Click ( object sender, EventArgs e )
    {
        try
        {           
            ReportParameter[] parameters = new ReportParameter[6];
            parameters[0] = new ReportParameter("EspecialidadId", EspecialidadDDL.SelectedValue);
            parameters[1] = new ReportParameter("EspecialidadNombre", EspecialidadDDL.SelectedItem.Text);
            parameters[2] = new ReportParameter("FechaIni", FechaIniDP.SelectedDate.Value.ToString("yyyy-MM-dd"));
            parameters[3] = new ReportParameter("FechaFin", FechaFinDP.SelectedDate.Value.ToString("yyyy-MM-dd"));
            parameters[4] = new ReportParameter("ClienteId", ClienteDDL.SelectedValue);
            parameters[5] = new ReportParameter("ClienteNombre", ClienteDDL.SelectedItem.Text);

            RedSaludReportViewer.LocalReport.SetParameters(parameters);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al generar el Reporte.");
            log.Error("Function GenerarReporte_Click on page ReporteAsegurado.aspx", q);
        }
    }
}