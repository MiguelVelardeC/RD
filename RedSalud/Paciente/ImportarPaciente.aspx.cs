using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Utilities.Import;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.RedSalud.ExcelProcessing;

public partial class Paciente_ImportarPaciente : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string rowError = "";
        ErrorLabel.Text = "";
        try
        {
            if (fileupload.HasFile)
            {
                if (!fileupload.FileName.EndsWith(".xlsx"))
                {
                    ErrorLabel.Text = "Error en el formato del Archivo, solo se acepta archivos '.xlsx'.";
                    return;
                }
                Stream stream = fileupload.PostedFile.InputStream;
                DataTable dtExcelRecords = ImportElement.GetDataTableFromExcel(stream, ref rowError); //GetDataTableFromExcel(stream, ref rowError);
                // forces to read a file
                fileupload.PostedFile.InputStream.Close();
                if (string.IsNullOrWhiteSpace(rowError))
                {
                    rowError = PacienteBLL.ValidateImportarPaciente(dtExcelRecords, Convert.ToInt32(ClienteDDL.SelectedValue), true);
                }

                if (!string.IsNullOrWhiteSpace(rowError))
                {
                    ErrorLabel.Text = rowError;
                    return;
                }
                Session["NacionalVidaDT"] = dtExcelRecords;
                UpdateButton.Visible = true;
                uploadFromExcelButton.Visible = false;
                FileUploadPanel.Visible = false;
                SuccessLabel.Text = dtExcelRecords.Rows.Count + " Filas Subidas y validadas, de click en Actualizar para comenzar la Importación.";
                ProgresoRPB.Visible = true;
            }
        }
        catch (TimeoutException toex)
        {
            ErrorLabel.Text = "Error de conexión al importar pacientes.";
            log.Error("Error while loading Excel survey" + toex);
            SystemMessages.DisplaySystemErrorMessage("Error al importar el archivo");
        }
        catch (InvalidOperationException suex)
        {
            log.Error("Error while loading Excel survey" + suex);
            ErrorLabel.Text = "Ocurrió un error al importar los pacientes. Por favor verifique que el formato del archivo cargado este correcto.";
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al importar los pacientes. Por favor verifique que el formato del archivo cargado este correcto");
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = "Ocurrió un error desconocido al importar los pacientes.";
            log.Error("Error while loading Excel survey" + ex);
            SystemMessages.DisplaySystemErrorMessage("Error al importar el archivo");
        }
    }
    private DataTable GetDataTableFromExcel(Stream stream, ref string rowError)
    {
        try
        {
            List<ExColumn> columns = new List<ExColumn>();
            columns.Add(new StringExColumn("Nombre *", true, true));
            columns.Add(new DateExColumn("FechaNacimiento *", true, true));
            columns.Add(new StringExColumn("Genero *", true, true));
            columns.Add(new StringExColumn("CarnetIdentidad", false, false));
            columns.Add(new StringExColumn("Direccion", false, false));
            columns.Add(new StringExColumn("Telefono", false, false));
            columns.Add(new StringExColumn("LugarTrabajo", false, false));
            columns.Add(new StringExColumn("TelefonoTrabajo", false, false));
            columns.Add(new StringExColumn("EstadoCivil", false, false));
            columns.Add(new StringExColumn("NroHijo", false, false));
            columns.Add(new StringExColumn("Antecedentes", false, false));
            columns.Add(new StringExColumn("AntecedentesAlergicos", false, false));
            columns.Add(new StringExColumn("AntecedentesGinecoobstetricos", false, false));
            columns.Add(new StringExColumn("Email", false, false));
            columns.Add(new StringExColumn("Codigo Asegurado *", true, true));
            columns.Add(new StringExColumn("NumeroPoliza *", true, true));
            columns.Add(new DateExColumn("FechaInicio *", true, true));
            columns.Add(new DateExColumn("FechaFin *", true, true));
            columns.Add(new DecimalExColumn("MontoTotal *", true, true, true));
            columns.Add(new DecimalExColumn("MontoFarmacia*", true, true, true));
            columns.Add(new StringExColumn("Cobertura*", true, true));
            columns.Add(new StringExColumn("NombrePlan *", true, true));
            columns.Add(new StringExColumn("Estado Poliza *", true, true));

            List<String> errors = new List<string>();
            DataSet newDataSet = ExProcess.ReadExcelSpreadhseet(stream, columns, errors);
            foreach (string error in errors)
            {
                rowError += error + "<br />";
            }

            DataTable tbl = new DataTable();
            ImportElement.AddColumns(tbl);

            for (int i = 0; i < newDataSet.Tables[0].Rows.Count; i++)
            {
                DataRow theRow = newDataSet.Tables[0].Rows[i];
                var row = tbl.NewRow();
                string minError = "";
                row["Nombre"] = ImportElement.ValidateField("Nombre", theRow["Nombre *"].ToString(), ref minError);
                row["FechaNacimiento"] = ImportElement.ValidateField("Nombre", theRow["FechaNacimiento *"].ToString(), ref minError);
                row["Genero"] = ImportElement.ValidateField("Genero", theRow["Genero *"].ToString(), ref minError);
                row["CarnetIdentidad"] = ImportElement.ValidateField("CarnetIdentidad", theRow["CarnetIdentidad"].ToString(), ref minError);
                row["Direccion"] = ImportElement.ValidateField("Direccion", theRow["Direccion"].ToString(), ref minError);
                row["Telefono"] = ImportElement.ValidateField("Telefono", theRow["Telefono"].ToString(), ref minError);
                row["TelefonoTrabajo"] = ImportElement.ValidateField("TelefonoTrabajo", theRow["TelefonoTrabajo"].ToString(), ref minError);
                row["EstadoCivil"] = ImportElement.ValidateField("EstadoCivil", theRow["EstadoCivil"].ToString(), ref minError);
                row["NroHijo"] = ImportElement.ValidateField("NroHijo", theRow["NroHijo"].ToString(), ref minError);
                row["Antecedentes"] = ImportElement.ValidateField("Antecedentes", theRow["Antecedentes"].ToString(), ref minError);
                row["AntecedentesAlergicos"] = ImportElement.ValidateField("AntecedentesAlergicos", theRow["AntecedentesAlergicos"].ToString(), ref minError);
                row["AntecedentesGinecoobstetricos"] = ImportElement.ValidateField("AntecedentesGinecoobstetricos", theRow["AntecedentesGinecoobstetricos"].ToString(), ref minError);
                row["Email"] = ImportElement.ValidateField("Email", theRow["Email"].ToString(), ref minError);
                row["CodigoAsegurado"] = ImportElement.ValidateField("CodigoAsegurado", theRow["Codigo Asegurado *"].ToString(), ref minError);
                row["NumeroPoliza"] = ImportElement.ValidateField("NumeroPoliza", theRow["NumeroPoliza *"].ToString(), ref minError);
                row["FechaInicio"] = ImportElement.ValidateField("FechaInicio", theRow["FechaInicio *"].ToString(), ref minError);
                row["FechaFin"] = ImportElement.ValidateField("FechaFin", theRow["FechaFin *"].ToString(), ref minError);
                row["MontoTotal"] = ImportElement.ValidateField("MontoTotal", theRow["MontoTotal *"].ToString(), ref minError);
                row["MontoFarmacia"] = ImportElement.ValidateField("MontoFarmacia", theRow["MontoFarmacia *"].ToString(), ref minError);
                row["Cobertura"] = ImportElement.ValidateField("Cobertura", theRow["Cobertura *"].ToString(), ref minError);
                row["NombrePlan"] = ImportElement.ValidateField("NombrePlan", theRow["NombrePlan *"].ToString(), ref minError);
                row["EstadoPoliza"] = ImportElement.ValidateField("EstadoPoliza", theRow["EstadoPoliza *"].ToString(), ref minError);

                if (!string.IsNullOrWhiteSpace(minError))
                {
                    errors.Add("Error en la fila " + (i + 1) + ":<div style='margin-left: 20px;'>" + minError + "</div>");
                }

                tbl.Rows.Add(row);
            }
            return tbl;
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = "Ocurrió un error al leer los datos del archivo.";
            log.Error("Error while loading Excel survey" + ex);
            SystemMessages.DisplaySystemErrorMessage("Error al leer el archivo");
            return null;
        }
    }
    protected void UpdateLB_Click(object sender, EventArgs e)
    {
        try
        {
            string rowError = "";
            ErrorLabel.Text = "";
            using (System.Transactions.TransactionScope transaction =
            new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, new TimeSpan(0, 10, 0)))
            {
                int idCliente = Artexacta.App.Configuration.Configuration.GetNacionalVidaClienteId();
                if (Session["NacionalVidaDT"] != null)
                {
                    DataTable dtExcelRecords = (DataTable)Session["NacionalVidaDT"];

                    RadProgressContext progress = RadProgressContext.Current;
                    int total = dtExcelRecords.Rows.Count;
                    progress.SecondaryTotal = total;
                    progress.PrimaryTotal = 1;

                    progress.CurrentOperationText = "Inactivando Polizas";
                    PacienteBLL.InactivarPolizas(Convert.ToInt32(ClienteDDL.SelectedValue));

                    for (int i = 0; i < total; i++)
                    {
                        progress.CurrentOperationText = dtExcelRecords.Rows[i].ItemArray[0];
                        DataTable tbl = new DataTable();
                        ImportElement.AddColumns(tbl);
                        tbl.Rows.Add(dtExcelRecords.Rows[i].ItemArray);

                        PacienteBLL.ImportarPacienteV2(tbl, Convert.ToInt32(ClienteDDL.SelectedValue), LugarDDL.SelectedValue, ref rowError);

                        if (!string.IsNullOrWhiteSpace(rowError))
                        {
                            ErrorLabel.Text += rowError;
                        }
                        progress.SecondaryValue = i;
                        progress.SecondaryPercent = (i * 100) / total;

                        progress.PrimaryValue = 1;
                        progress.PrimaryPercent = 100;
                    }
                    Artexacta.App.Utilities.Bitacora.Bitacora theBitacora = new Artexacta.App.Utilities.Bitacora.Bitacora();
                    string msgBitacora = "Importando Pacientes de '" + ClienteDDL.SelectedItem.Text + "': total importados = " + total;
                    theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.ImportarPacientes, HttpContext.Current.User.Identity.Name, "PACIENTE", ClienteDDL.SelectedValue, msgBitacora);

                    if (string.IsNullOrWhiteSpace(rowError))
                    {
                        transaction.Complete();
                        SystemMessages.DisplaySystemMessage("La importación de pacientes se completo satisfactoriamente");
                    }
                    else
                    {
                        log.Error(rowError);
                        transaction.Dispose();
                    }
                }
            }
            UpdateButton.Visible = false;
            uploadFromExcelButton.Visible = true;
            FileUploadPanel.Visible = true;
            SuccessLabel.Text = (!string.IsNullOrWhiteSpace(rowError)) ? "No se Importo debido a Errores" : "La importación de pacientes se completo satisfactoriamente";
            ProgresoRPB.Visible = false;
        }
        catch (InvalidOperationException suex)
        {
            log.Error("Error while loading Excel survey", suex);
            ErrorLabel.Text = "Ocurrió un error al importar los pacientes. Por favor verifique que el formato del archivo cargado este correcto.";
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al importar los pacientes. Por favor verifique que el formato del archivo cargado este correcto");
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = "Ocurrió un error desconocido al importar los pacientes.";
            log.Error("Error while loading Excel survey", ex);
            SystemMessages.DisplaySystemErrorMessage("Error al importar el archivo");
        }
    }
    protected void ClienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos de las aseguradoras.");
            log.Error("Function ClienteODS_Selected on page ImportarPaciente.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}