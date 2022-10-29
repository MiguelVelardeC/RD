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
using System.IO;
using System.Data;
using Artexacta.App.Utilities.Import;
using Artexacta.App.Paciente.BLL;
using Telerik.Web.UI;
using Artexacta.App.User.BLL;
using Artexacta.App.ClienteUsuario.BLL;
using Artexacta.App.RedCliente.BLL;

public partial class Import_ImportParaTerceros : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!LoginSecurity.IsUserAuthorizedPermission("IMPORT_NACIONAL_VIDA"))
            {
                importPacientesButton.Visible = false;
                //ErrorLabel.Text = "No tiene permisos para importar desde Nacional Vida";
                ErrorLabel.Text = "No tiene permisos para importar";
            }

            //Se obtiene el id del usuario
            var usuario = UserBLL.GetUserByUsername(HttpContext.Current.User.Identity.Name);
            if(usuario == null)
            {
                return;
            }
            //Se obtiene el id del cliente
            var cliUsr = ClienteUsuarioBLL.GetClienteUsuarioByUserId(usuario.UserId);
            if(cliUsr == null)
            {
                importPacientesButton.Visible = false;
                //ErrorLabel.Text = "No tiene permisos para importar desde Nacional Vida";
                ErrorLabel.Text = "No tiene asignado un cliente para la carga de Pólizas diarias";
            }
        }
    }
    protected void importPacientesButton_Click ( object sender, EventArgs e )
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
                //Se obtiene el id del usuario
                var usuario = UserBLL.GetUserByUsername(HttpContext.Current.User.Identity.Name);
                //Se obtiene el id del cliente
                var cliUsr = ClienteUsuarioBLL.GetClienteUsuarioByUserId(usuario.UserId);
                //Se obtiene el id de Nacional Vida
                int idNacional = Artexacta.App.Configuration.Configuration.GetNacionalVidaClienteId();
                DataTable dtExcelRecords;
                if (cliUsr.ClienteId == idNacional)
                {
                    //Si es nacional vida
                    dtExcelRecords = ImportElement.GetDataTableFromExcelNacionalVida(stream, ref rowError);
                }else
                {
                    //Sino usar la nueva importacion
                    dtExcelRecords = ImportElement.GetDataTableFromExcelGeneral(stream, ref rowError);
                }

                // forces to read a file
                fileupload.PostedFile.InputStream.Close();                

                if (string.IsNullOrWhiteSpace(rowError))
                {
                    //rowError = PacienteBLL.ValidateImportarPaciente(dtExcelRecords, 4, true, 17);
                    rowError = PacienteBLL.ValidateImportarPaciente(dtExcelRecords, cliUsr.ClienteId, true, 17);
                }
                if (!string.IsNullOrWhiteSpace(rowError))
                {
                    ErrorLabel.Text = rowError;
                    return;
                }
                Session["NacionalVidaDT"] = dtExcelRecords;
                UpdateButton.Visible = true;
                importPacientesButton.Visible = false;
                FileUploadPanel.Visible = false;
                SuccessLabel.Text = dtExcelRecords.Rows.Count + " Filas Subidas y validadas, de click en Actualizar para comenzar la Importación.";
                ProgresoRPB.Visible = true;
            }
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
    protected void UpdateLB_Click(object sender, EventArgs e)
    {
        try
        {
            string rowError = "";
            using (System.Transactions.TransactionScope transaction =
            new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, new TimeSpan(0, 10, 0)))
            {
                //int idCliente = Artexacta.App.Configuration.Configuration.GetNacionalVidaClienteId();
                //Se obtiene el id del usuario
                var usuario = UserBLL.GetUserByUsername(HttpContext.Current.User.Identity.Name);
                //Se obtiene el id del cliente
                var cliUsr = ClienteUsuarioBLL.GetClienteUsuarioByUserId(usuario.UserId);
                int idCliente = cliUsr.ClienteId;
                if (Session["NacionalVidaDT"] != null)
                {
                    DataTable dtExcelRecords = (DataTable)Session["NacionalVidaDT"];

                    RadProgressContext progress = RadProgressContext.Current;
                    int total = dtExcelRecords.Rows.Count;
                    progress.SecondaryTotal = total;
                    progress.PrimaryTotal = 1;

                    PacienteBLL.InactivarPolizas(idCliente);

                    for (int i = 0; i < total; i++)
                    {
                        progress.CurrentOperationText = dtExcelRecords.Rows[i].ItemArray[0];
                        DataTable tbl = new DataTable();
                        ImportElement.AddColumns(tbl);
                        tbl.Rows.Add(dtExcelRecords.Rows[i].ItemArray);

                        PacienteBLL.ImportarPacienteV2(tbl, idCliente, "SCZ", ref rowError);
                        if (!string.IsNullOrWhiteSpace(rowError))
                        {
                            ErrorLabel.Text += rowError;
                        }
                        progress.SecondaryValue = i;
                        progress.SecondaryPercent = (i * 100) / total;

                        progress.PrimaryValue = 1;
                        progress.PrimaryPercent = 100;
                    }

                    if (string.IsNullOrWhiteSpace(rowError))
                    {
                        transaction.Complete();
                        SystemMessages.DisplaySystemMessage("La importación de pacientes se completo satisfactoriamente");
                    }
                }
            }
            UpdateButton.Visible = false;
            importPacientesButton.Visible = true;
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
}