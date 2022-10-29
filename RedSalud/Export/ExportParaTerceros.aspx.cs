using Artexacta.App.LoginSecurity;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Export_ExportParaTerceros : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!LoginSecurity.IsUserAuthorizedPermission("EXPORT_FARMACIA_CHAVEZ"))
            {
                exportPacientesButton.Visible = false;
            }
        }
    }
    protected void exportPacientesButton_Click(object sender, EventArgs e)
    {
        string fecha = DateTime.Now.Year.ToString();
        fecha = fecha + DateTime.Now.Month;
        fecha = fecha + DateTime.Now.Day;
        string filename = "ExportPolizas" + fecha + ".csv";
        System.IO.StringWriter tw = new System.IO.StringWriter();

        ExportDSTableAdapters.PolizasXEstadoXFarmaciaTableAdapter adapter =
            new ExportDSTableAdapters.PolizasXEstadoXFarmaciaTableAdapter();

        try
        {
            ExportDS.PolizasXEstadoXFarmaciaDataTable theTable = adapter.GetPolizaXEstadoXFarmacia("1,9,12");

            if (theTable.Rows.Count == 0)
            {
                log.Debug("No hay registros que traer de la base de datos");
                SystemMessages.DisplaySystemWarningMessage("ERror, no hay records de polizas");
                return;
            }
            else
            {
                foreach (ExportDS.PolizasXEstadoXFarmaciaRow row in theTable)
                {
                    try
                    {
                        tw.Write(
                            (row.IspolizaIdNull() ? 0 : row.polizaId) + "|" + 
                            (row.IsaseguradoIdNull() ? 0 : row.aseguradoId) + "|" + 
                            (row.IspacienteIdNull() ? 0 : row.pacienteId) + "|" +
                            (row.IsnombreJuridicoNull() ? "" : row.nombreJuridico) + "|" + 
                            (row.IscodigoNull() ? "" : row.codigo) + "|" + 
                            (row.IsnombreNull() ? "" : row.nombre) + "|" +
                            (row.IsapellidoNull() ? "" : row.apellido) + "|" +
                            (row.IscarnetNull() ? "" : row.carnet) + "|" +
                            (row.IsfechaFinNull() ? "0000-00-00" : 
                                row.fechaFin.Year + "-" + 
                                row.fechaFin.Month.ToString().PadLeft(2, '0') + "-" + 
                                row.fechaFin.Day.ToString().PadLeft(2, '0')) + "|" +
                            (row.IstotalPlanNull() ? "0.00" : row.totalPlan.ToString(CultureInfo.InvariantCulture)) + "|" +
                            (row.IsnombrePlanNull() ? "" : row.nombrePlan) + "|" + 
                            (row.IslugarNull() ? "" : row.lugar) + "|" + 
                            (row.IsColumn1Null() ? "0.00" : row.Column1.ToString("#0.00", CultureInfo.InvariantCulture)) + "|" +
                            row.totalDisponibleFarmacia.ToString("#0.00", CultureInfo.InvariantCulture) + "|" +
                            (row.IscoberturaNull() ? "" : row.cobertura) + "|" + 
                            (row.IsestadoNull() ? "Inactivo" : row.estado));
                        tw.WriteLine();
                    }
                    catch (Exception q)
                    {
                        log.Error("Hubo un error al exportar los datos al archivo", q);
                    }
                }
            }

            //Write the CSV back to the browser.
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = "text/csv";
            this.EnableViewState = false;
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(tw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al traer los datos de polizas ", ex);
            SystemMessages.DisplaySystemErrorMessage("Error al traer los datos de polizas");
            return;
        }
    }
}