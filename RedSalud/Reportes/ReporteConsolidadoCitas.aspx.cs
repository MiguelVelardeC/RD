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

public partial class Reportes_ReporteConsolidadoCitas : System.Web.UI.Page
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

                var medicos = (from me in context.tbl_RED_Medico
                               join us in context.tbl_SEG_User on me.UserId equals us.userId
                               select new MedicoSimple
                               {
                                   MedicoId = me.MedicoId,
                                   Nombre = us.fullname
                               }).ToList();
                medicos.Insert(0, new MedicoSimple { MedicoId = 0, Nombre = "Todos" });
                MedicoDDL.DataSource = medicos;
                MedicoDDL.SelectedValue = "0";
                MedicoDDL.DataBind();               

                var estados = new List<Estado>
                {
                    new Estado { Id = "*", Nombre = "Todos" },
                    new Estado { Id = "SO", Nombre = "Solicitado" },
                    new Estado { Id = "AP", Nombre = "Aprobado" },
                    new Estado { Id = "TE", Nombre = "Terminado" },
                    new Estado { Id = "AN", Nombre = "Anulado" },
                };
                EstadoDDL.DataSource = estados;
                EstadoDDL.SelectedValue = "*";
                EstadoDDL.DataBind();

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
            ReportParameter[] parameters = new ReportParameter[11];
            parameters[0] = new ReportParameter("EspecialidadId", EspecialidadDDL.SelectedValue);
            parameters[1] = new ReportParameter("FechaIni", FechaIniDP.SelectedDate.Value.ToString("yyyy-MM-dd"));
            parameters[2] = new ReportParameter("FechaFin", FechaFinDP.SelectedDate.Value.ToString("yyyy-MM-dd"));
            parameters[3] = new ReportParameter("ClienteId", ClienteDDL.SelectedValue);
            parameters[4] = new ReportParameter("EspecialidadNombre", EspecialidadDDL.SelectedItem.Text);
            parameters[5] = new ReportParameter("ClienteNombre", ClienteDDL.SelectedItem.Text);
            parameters[6] = new ReportParameter("MedicoId", MedicoDDL.SelectedValue);            
            parameters[7] = new ReportParameter("EstadoId", EstadoDDL.SelectedValue);
            parameters[8] = new ReportParameter("MedicoNombre", MedicoDDL.SelectedItem.Text);            
            parameters[9] = new ReportParameter("EstadoNombre", EstadoDDL.SelectedItem.Text);
            parameters[10] = new ReportParameter("NumeroPoliza", PolizaText.Text);

            RedSaludReportViewer.LocalReport.SetParameters(parameters);
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