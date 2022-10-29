using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Utilities.Email;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.WebServices.alianza;
using Artexacta.App.WebServices.alianza.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Utilities.Import;

public partial class Test_TestWSAlianza : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        RadProgressArea1.Localization.UploadedFiles = "Finalizados";
        RadProgressArea1.Localization.CurrentFileName = "Procesando";
        RadProgressArea1.Localization.TotalFiles = "Total";

        if (!IsPostBack)
        {
            Session["AseguradosAlianza"] = null;
            updateData.Enabled = false;
        }
    }
    protected void callWebSErvice_Click(object sender, EventArgs e)
    {
        DataTable asegurados = null;
        string errors = "";
        try
        {
            int validos = 0;
            asegurados = Artexacta.App.WebServices.alianza.BLL.EstadoAseguradoBLL.GetDataTableFromWebService(ref validos, ref errors);
            validos = asegurados.Rows.Count - validos;
            lblNumberOfAsegurados.Text = "VA a actualizar: " + asegurados.Rows.Count.ToString() + " registros";
            ErrorLabel.Text = "";
        }
        catch (Exception q)
        {
            log.Error("Error al llamar el webservice, ver log", q);
            ErrorLabel.Text = "errors<br />" + q.Message + "<br />" + q.StackTrace;
            //EmailUtilities.SendErrorWebServiceUpdate(q.StackTrace);
            return;
        }

        Session["AseguradosAlianza"] = asegurados;
        updateData.Enabled = true;
    }

    protected void getCsvFile_Click(object sender, EventArgs e)
    {
        DataTable asegurados = (DataTable)Session["AseguradosAlianza"];

        try {
            StringBuilder tw = new StringBuilder();

            foreach (DataRow row in asegurados.Rows)
            {
                tw.Append(EstadoAseguradoBLL.GetCsvLine(row) + "\n");
            }

            DateTime hoy = DateTime.Now;
            String fechaHoy = hoy.Year.ToString() + hoy.Month.ToString() + hoy.Day.ToString();
            String filename = "AseguradosAlianza" + fechaHoy + ".csv";
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
    protected void updateData_Click(object sender, EventArgs e)
    {
        DataTable asegurados = (DataTable)Session["AseguradosAlianza"];

        RadProgressContext context = RadProgressContext.Current;
        context.SecondaryTotal = asegurados.Rows.Count.ToString();

        //int processed = 0;
        int clienteId = Artexacta.App.Configuration.Configuration.GetAlianzaClienteId();
        int nbEnWebService = (asegurados != null ? asegurados.Rows.Count : 0);
        int nbActualizadosAActivo = 0;
        int nbActualizadosAInactivo = 0;
        int nbInsertados = 0;
        int nbNoValidos = 0;
        int nbErrores = 0;
        string errores = "";

        try
        {
            int totalAsegurados = asegurados.Rows.Count;

            ImportElement.ValidateNulls(ref asegurados, 0, true, ref errores);
            nbNoValidos = totalAsegurados - asegurados.Rows.Count;
            totalAsegurados = asegurados.Rows.Count;

            errores += PacienteBLL.ValidateImportarPaciente(ref asegurados, clienteId, true);
           
            nbNoValidos = totalAsegurados - asegurados.Rows.Count;
            totalAsegurados = asegurados.Rows.Count;

            //PacienteBLL.ImportarPaciente(asegurados, clienteId, "SCZ", true, false, ref errores, ref nbActualizadosAActivo,
            //    ref nbActualizadosAInactivo, ref nbInsertados, ref nbErrores);
            RadProgressContext progress = RadProgressContext.Current;

            int total = asegurados.Rows.Count;
            progress.SecondaryTotal = total;
            progress.PrimaryTotal = 1;

            PacienteBLL.InactivarPolizas(clienteId);

            for (int i = 0; i < total; i++)
            {
                progress.CurrentOperationText = asegurados.Rows[i].ItemArray[0];
                DataTable tbl = new DataTable();
                ImportElement.AddColumns(tbl);
                tbl.Rows.Add(asegurados.Rows[i].ItemArray);
                string rowError = "";
                int actualizadosAActivo = 0;
                int actualizadosAInactivo = 0;
                int insertados = 0;
                int intErrores = 0;
                try
                {
                    bool Insertado = false;
                    PacienteBLL.ImportarPacienteV2(tbl, clienteId, "SCZ", ref rowError, ref Insertado);
                    if ((tbl.Rows[0][ImportElement.EstadoPoliza]).ToString() == "ACTIVO")
                    {
                        actualizadosAActivo++;
                    }
                    else
                    {
                        actualizadosAInactivo++;
                    }
                    if (Insertado)
                    {
                        insertados++;
                    }
                }
                catch (Exception q)
                {
                    log.Error("Error al insertar el asegurado " + asegurados.Rows[i].ItemArray[0] + ":" + q.Message);
                    rowError += "Error al Insertar el Asegurado " + asegurados.Rows[i].ItemArray[0];
                    intErrores = 1;
                }
                if (!string.IsNullOrWhiteSpace(rowError))
                {
                    errores += rowError;
                    nbErrores += intErrores;
                }
                else
                {
                    nbActualizadosAActivo += actualizadosAActivo;
                    nbActualizadosAInactivo += actualizadosAInactivo;
                    nbInsertados += insertados;
                }
                progress.SecondaryValue = i;
                progress.SecondaryPercent = (i * 100) / total;

                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;
            }
        }
        catch (Exception q)
        {
            log.Error("Error al actualizar la informacion del web service, ver log", q);
            ErrorLabel.Text = q.Message + "<br />" + q.StackTrace;
            return;
        }
        
        ErrorLabel.Text = errores;
        string resumen = "Desde Alianza: " + nbEnWebService.ToString() +
            ", No Validos (error en entrada): " + nbNoValidos.ToString() +
            ", Errores: " + nbErrores.ToString() +
            ", Insertados: " + nbInsertados.ToString() +
            ", Convertidos a Activo: " + nbActualizadosAActivo.ToString() +
            ", Convertidos a Inactivo: " + nbActualizadosAInactivo.ToString();

        lblResultUpdate.Text = resumen;

        EmailUtilities.SendEmailWebServiceUpdate("ferchoj.alapa@gmail.com", DateTime.Now, resumen + "<br />" + errores);
    }
}