using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Caso.BLL;
using log4net;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.RedMedica;
using Artexacta.App.RedMedica.BLL;
using Artexacta.App.Medicamento.BLL;
using Artexacta.App.Medicamento;
using Telerik.Web.UI;
using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Configuration;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Caso;
using Artexacta.App.Proveedor.BLL;
using Artexacta.App.Proveedor;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Especialidad;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.EnfermedadCronica.BLL;

public partial class CasoMedico_Enfermeria : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private DateTime FechaEstado;
    private string isExport = "";
    private static string ToolTipCannotDeleteHaveGasto = "No se puede eliminar por que tiene registros de gastos.";
    //private static string ToolTipCannotUpdateByapproved = "No se puede Modificar por que ya esta aprobado.";
    private static string UrlHistorial = "Historial.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        //DateTime fecah= this.FechaCreacion2.SelectedDate.Value;
        if (!IsPostBack)
        {
            ProcessSessionParameters();
            try
            {

            } catch { }
        }
        SetViewMode();
    }
    private void SetViewMode()
    {
        string Mode = ModeHF.Value;

        if (!string.IsNullOrEmpty(Mode))
        {
            CasoFV.ChangeMode(FormViewMode.Edit);
            this.MedicamentoRT.Enabled = false;
        }
    }

    //este talvez obtener del queryString para q se pueda refrescar la pagina
    protected void ProcessSessionParameters()
    {
        int CasoId = 0;
        if (Session["CasoId"] != null && !string.IsNullOrEmpty(Session["CasoId"].ToString()))
        {
            try
            {
                CasoId = Convert.ToInt32(Session["CasoId"]);
                GetPolizaDetails(CasoId);
            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso de Enfermería");
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);
                returnTo();
                return;
            }
            switch (CasoBLL.GetMotivoConsultaId(CasoId))
            {
                case "ACCID":
                    Response.Redirect("~/CasoMedico/CasoMedicoDetalle.aspx");
                    return;
                case "EMERG":
                    Response.Redirect("~/CasoMedico/Emergencia.aspx");
                    return;
            }
        }
        CasoIdHF.Value = CasoId.ToString();
        Session["CasoId"] = null;

        string Mode = "";
        if (Session["Mode"] != null && !string.IsNullOrEmpty(Session["Mode"].ToString()))
        {
            Mode = Session["Mode"].ToString();
        }
        ModeHF.Value = Mode;
        Session["Mode"] = null;

        string reconsulta = "";
        if (Session["RECONSULTA"] != null && !string.IsNullOrEmpty(Session["RECONSULTA"].ToString()))
        {
            try
            {
                CasoFV.ChangeMode(FormViewMode.Edit);
                (CasoFV.FindControl("FechaReconsulta") as RadDatePicker).SelectedDate = DateTime.Now;
                //CasoBLL.UpdateFechaReconsulta(CasoId);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["BolsaId"]);
            }
            reconsulta = Session["RECONSULTA"].ToString();
        }
        ReconsultaHF.Value = reconsulta;
        Session["RECONSULTA"] = null;

        if (Session["RETURNTO"] != null && !string.IsNullOrEmpty(Session["RETURNTO"].ToString()))
        {
            returnHL.CommandArgument = Session["RETURNTO"].ToString();
            Session["RETURNTO"] = returnHL.CommandArgument;
        }
    }

    protected void GetPolizaDetails(int CasoId)
    {
        try
        {
            Poliza objPoliza = PolizaBLL.GetPolizaDetailsByCasoId(CasoId);

            this.ClienteIdHF.Value = objPoliza.ClienteId.ToString();
            AseguradoIdHF.Value = objPoliza.AseguradoId.ToString();
            PacienteIdHF.Value = objPoliza.PacienteId.ToString();
            decimal MontoMinimoPaciente = objPoliza.MontoTotal - objPoliza.GastoTotal;
            decimal MontoMinimoEnPoliza = Configuration.GetMontoMinimoEnPoliza();

            if (MontoMinimoPaciente <= MontoMinimoEnPoliza)
            {
                //deshabilitar todos los botonoes de Nuevo
                string strMessage = "El paciente no cuenta con el monto mínimo suficiente para crear una nueva Medicamento";
                this.NewMedicamentoLB.ToolTip = strMessage;
                this.MessageMedicamentoLabel.Text = strMessage;
                this.SaveNewMedicamentoLB.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de la Póliza.");
            log.Error("Function PolizaODS_Selected on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void CasoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Caso de Enfermería.");
            e.ExceptionHandled = true;
            Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx");
        }
    }
    protected void CasoODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Modificar los datos del Caso de Enfermería.");
            log.Error("Function CasoODS_Updated on page CasoMedicoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            this.ModeHF.Value = "";
            this.MedicamentoRT.Enabled = true;
            //if (LoginSecurity.IsUserAuthorizedPermission("MANAGE_ENFERMERIA"))
        }
    }
    protected void CasoFV_DataBound ( object sender, EventArgs e )
    {
        if (CasoFV.CurrentMode == FormViewMode.Edit)
        {
            string EnfermedadId = DataBinder.Eval(CasoFV.DataItem, "EnfermedadId").ToString();
            if (!string.IsNullOrWhiteSpace(EnfermedadId))
            {
                string Enfermedad = DataBinder.Eval(CasoFV.DataItem, "Enfermedad").ToString();
                RadComboBox EnfermedadesComboBox = (RadComboBox)CasoFV.FindControl("EnfermedadesComboBox");
                RadComboBoxItem item = new RadComboBoxItem("[" + EnfermedadId + "] " + Enfermedad, EnfermedadId);
                EnfermedadesComboBox.Items.Add(item);
                item.Selected = true;
            }
        }
    }

    //Obtiene la fecha de Reconsulta si es que la tiene, caso contrario el de la creacion del caso
    protected DateTime GetFechaTabsCreacion(int CasoId)
    {
        DateTime FechaCreacion = DateTime.UtcNow;
        try
        {
            Caso objCaso = CasoBLL.GetCasoByCasoId(CasoId);
            if (objCaso != null)
            {
                FechaCreacion = objCaso.FechaCreacion;
            }
        }
        catch
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el detalles del Caso de Enfermería.");
        }
        return FechaCreacion;
    }

    #region TabMedicamento
    protected void MedicamentoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Medicamentos.");
            e.ExceptionHandled = true;
        }
    }

    protected void TipoMedicamentoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de tipos de presentaciones medicas");
            e.ExceptionHandled = true;
        }
    }

    protected void MedicamentoGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                Medicamento ObjMedicamento = (Medicamento)e.Item.DataItem;

                if (ObjMedicamento.GastoId > 0)
                {
                    ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                    if (deleteButton != null)
                    {
                        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                        deleteButton.Enabled = false;
                        deleteButton.ToolTip = ToolTipCannotDeleteHaveGasto;
                    }

                    ImageButton DetailsImageButton = (ImageButton)e.Item.FindControl("DetailsImageButton");
                    if (DetailsImageButton != null)
                    {
                        DetailsImageButton.ImageUrl = "~/Images/Neutral/edit_disable.png";
                        DetailsImageButton.Enabled = false;
                        DetailsImageButton.ToolTip = "No se puede Modificar por que tiene registros de gastos.";
                    }
                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algún control en Exámenes complementarios.");
                log.Error("Function EstudioRadGrid_ItemDataBound on page CasoMedicoDetalle.aspx", ex);
            }
        }
    }
    
    protected void MedicamentoGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            switch (e.CommandName)
            {
                case "Select":
                    try
                    {
                        int MedicamentoID = Convert.ToInt32(e.CommandArgument.ToString());

                        Medicamento objMedicamento = MedicamentoBLL.GetMedicamentoById(MedicamentoID);

                        if (objMedicamento != null)
                        {
                            this.MedicamentoIdHF.Value = objMedicamento.MedicamentoId.ToString();
                            this.TipoMedicamentoDDL.SelectedValue = objMedicamento.TipoMedicamentoId;
                            RadComboBoxItem item = new RadComboBoxItem(objMedicamento.MedicamentoNombre, objMedicamento.MedicamentoCLAId.ToString());
                            MedicamentoComboBox.Items.Add(item);
                            item.Selected = true;
                            this.InstruccionesTxt.Text = objMedicamento.Indicaciones;
                            //abrir popup
                            ClientScript.RegisterStartupScript(this.GetType(), "Medicamento", "OpenPopupMedicamento();", true);
                        }
                    }
                    catch
                    {
                        SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Medicamento.");
                    }
                    break;

                case "Delete":
                    {
                        int MedicamentoId = 0;
                        try
                        {
                            MedicamentoId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MedicamentoId"];
                        }
                        catch (Exception ex)
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Medicamento a eliminar.");
                            log.Error("Function EstudioRadGrid_ItemCommand deleting on page CasoMedicoDetalle.aspx", ex);
                        }
                        try
                        {
                            if (MedicamentoBLL.GetGastoIdMedicamento(MedicamentoId) > 0)
                            {
                                SystemMessages.DisplaySystemErrorMessage("No se puede Eliminar el Medicamento por que tiene registros de gastos.");
                                break;
                            }
                        }
                        catch
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al Validar si el Medicamento cuenta con gastos.");
                        }

                        try
                        {
                            if (MedicamentoBLL.DeleteMedicamento(MedicamentoId))
                                SystemMessages.DisplaySystemMessage("Medicamento eliminado correctamente.");
                            else
                                SystemMessages.DisplaySystemErrorMessage("Error al eliminar el Medicamento.");
                        }
                        catch
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al eliminar el Medicamento.");
                        }
                        break;
                    }
            }
        }
    }

    protected void SaveNewMedicamentoLB_Click(object sender, EventArgs e)
    {
        int InsertedNewMedicamento = 0;
        int NewMedicamentoId = 0;
        int ErrorInserted = 0;
        int CasoId = 0;
        DateTime FechaCreacionMedicamento;
        try
        {
            CasoId = Convert.ToInt32(CasoIdHF.Value);
            FechaCreacionMedicamento = GetFechaTabsCreacion(CasoId);
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar el/Los Medicamento(s).");
            log.Error("Function SaveNewMedicamentoLB_Click on page Enfermeria.aspx [" + CasoIdHF.Value + "]", ex);
            return;
        }

        try
        {
            string TipoMedicamentoId = TipoMedicamento1DDL.SelectedValue;
            int MedicamentoCLAId = Convert.ToInt32(MedicamentoRadComboBox.SelectedValue);
            string Indicaciones = Instrucciones1Txt.Text;
            if (!PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
            {
                SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar los Medicamento.");
                CleanMedicamentoFields();
            }
            NewMedicamentoId = MedicamentoBLL.InsertMedicamento(CasoId, MedicamentoCLAId, TipoMedicamentoId, Indicaciones, FechaCreacionMedicamento);
            if (NewMedicamentoId <= 0)
                ErrorInserted++;
            else
                InsertedNewMedicamento++;
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar el nuevo Medicamento.");
            log.Error("Function SaveNewMedicamentoLB_Click on page Enfermeria.aspx 1st medicament", ex);
        }
        try
        {

            //Medicamento 2
            if (!string.IsNullOrEmpty(TipoMedicamento2DDL.SelectedValue)
                && !string.IsNullOrEmpty(MedicamentoRadComboBox1.SelectedValue)
                && !string.IsNullOrEmpty(Instrucciones2Txt.Text))
            {
                NewMedicamentoId = 0;//reset
                NewMedicamentoId = MedicamentoBLL.InsertMedicamento(CasoId, Convert.ToInt32(MedicamentoRadComboBox1.SelectedValue)
                    , TipoMedicamento2DDL.SelectedValue, Instrucciones2Txt.Text, FechaCreacionMedicamento);
                if (NewMedicamentoId <= 0)
                    ErrorInserted++;
                else
                    InsertedNewMedicamento++;
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar el segundo nuevo Medicamento.");
            log.Error("Function SaveNewMedicamentoLB_Click on page Enfermeria.aspx 2nd medicament", ex);
        }
        try
        {
            //Medicamento 3
            if (!string.IsNullOrEmpty(TipoMedicamento3DDL.SelectedValue)
                && !string.IsNullOrEmpty(MedicamentoRadComboBox2.SelectedValue)
                && !string.IsNullOrEmpty(Instrucciones3Txt.Text))
            {
                NewMedicamentoId = 0;//reset
                NewMedicamentoId = MedicamentoBLL.InsertMedicamento(CasoId, Convert.ToInt32(MedicamentoRadComboBox2.SelectedValue)
                    , TipoMedicamento3DDL.SelectedValue, Instrucciones3Txt.Text, FechaCreacionMedicamento);
                if (NewMedicamentoId <= 0)
                    ErrorInserted++;
                else
                    InsertedNewMedicamento++;
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar el tercer nuevo Medicamento.");
            log.Error("Function SaveNewMedicamentoLB_Click on page Enfermeria.aspx 3st medicament", ex);
        }
        try
        {
            //Medicamento 4
            if (!string.IsNullOrEmpty(TipoMedicamento4DDL.SelectedValue)
                && !string.IsNullOrEmpty(MedicamentoRadComboBox3.SelectedValue)
                && !string.IsNullOrEmpty(Instrucciones4Txt.Text))
            {
                NewMedicamentoId = 0;//reset
                NewMedicamentoId = MedicamentoBLL.InsertMedicamento(CasoId, Convert.ToInt32(MedicamentoRadComboBox3.SelectedValue)
                    , TipoMedicamento4DDL.SelectedValue, Instrucciones4Txt.Text, FechaCreacionMedicamento);
                if (NewMedicamentoId <= 0)
                    ErrorInserted++;
                else
                    InsertedNewMedicamento++;
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar el cuarto nuevo Medicamento.");
            log.Error("Function SaveNewMedicamentoLB_Click on page Enfermeria.aspx 4rd medicament", ex);
        }
        try
        {
            //mostrar mensaje de error
            if (ErrorInserted <= 0)
            {
                if (InsertedNewMedicamento > 1)
                    SystemMessages.DisplaySystemMessage(InsertedNewMedicamento.ToString() + " Nuevas Medicamentos insertadas correctamente.");
                else
                    SystemMessages.DisplaySystemMessage("Nueva Medicamento insertada correctamente.");
            }
            MedicamentoGrid.DataBind();
            CleanMedicamentoFields();
            this.MedicamentoRT.Selected = true;
            this.MedicamentoRPV.Selected = true;
        }
        catch (Exception ex)
        {
            log.Error("Function SaveNewMedicamentoLB_Click on page Enfermeria.aspx", ex);
        }
    }

    protected void CleanMedicamentoFields()
    {
        this.TipoMedicamento1DDL.ClearSelection();
        this.TipoMedicamento2DDL.ClearSelection();
        this.TipoMedicamento3DDL.ClearSelection();
        this.TipoMedicamento4DDL.ClearSelection();

        this.MedicamentoRadComboBox.ClearSelection();
        this.MedicamentoRadComboBox1.ClearSelection();
        this.MedicamentoRadComboBox2.ClearSelection();
        this.MedicamentoRadComboBox3.ClearSelection();

        this.Instrucciones1Txt.Text = "";
        this.Instrucciones2Txt.Text = "";
        this.Instrucciones3Txt.Text = "";
        this.Instrucciones4Txt.Text = "";
    }

    protected void SaveMedicamento_Click(object sender, EventArgs e)
    {
        int MedicamentoId = 0;
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            int MedicamentoCLAId = Convert.ToInt32(MedicamentoRadComboBox.SelectedValue);
            string TipoMedicamentoId = TipoMedicamentoDDL.SelectedValue;
            string Indicaciones = InstruccionesTxt.Text;

            MedicamentoId = Convert.ToInt32(this.MedicamentoIdHF.Value);

            if (MedicamentoId <= 0)
            {
                if (PolizaBLL.BoolMontoMinimoEnPolizaPacienteSuperior(CasoId))
                {
                    int NewMedicamentoId = MedicamentoBLL.InsertMedicamento(CasoId, MedicamentoCLAId, TipoMedicamentoId, Indicaciones, DateTime.Now);
                    if (NewMedicamentoId <= 0)
                        SystemMessages.DisplaySystemErrorMessage("Error al insertar la nueva Medicamento.");
                    else
                        SystemMessages.DisplaySystemMessage("Nueva Medicamento insertada correctamente.");
                }
                else
                    SystemMessages.DisplaySystemErrorMessage("El paciente no cuenta con el monto mínimo suficiente para Insertar una Medicamento.");

            }
            else
            {
                if (MedicamentoBLL.GetGastoIdMedicamento(MedicamentoId) > 0)
                    SystemMessages.DisplaySystemErrorMessage("No se puede modificar la Medicamento por que tiene registros de gastos.");
                else
                {
                    if (MedicamentoBLL.UpdateMedicamento(MedicamentoId, MedicamentoId, TipoMedicamentoId, Indicaciones))
                        SystemMessages.DisplaySystemMessage("La Medicamento fue modificada correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("Error al modificar la Medicamento.");
                }
            }

            MedicamentoGrid.DataBind();
            this.MedicamentoIdHF.Value = "0";
            this.MedicamentoComboBox.ClearSelection();
            this.InstruccionesTxt.Text = "";
            this.TipoMedicamentoDDL.ClearSelection();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la Medicamento.");
            log.Error("Function SaveMedicamento_Click on page CasoMedicoDetalle.aspx", ex);
        }
    }

    protected void MedicamentoGrid_ExportToPdfButton_Click(object sender, EventArgs e)
    {
        MedicamentoGrid.MasterTableView.GetColumn("FechaCreacion").Display = false;
        RecetaToPrintRadGrid.DataSourceID = MedicamentoGrid.DataSourceID;
        //RecetaToPrintRadGrid.DataBind();
        ExportGridToPDF(RecetaToPrintRadGrid, "RECETA", "PRESCRIPTION");
    }
    
    protected void MedicamentoGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    private void ExportGridToPDF(RadGrid grid, string fileName, string type)
    {
        try
        {
            int CasoId = Convert.ToInt32(this.CasoIdHF.Value);

            grid.ExportSettings.Pdf.PageFooter.LeftCell.Text = @"<div style=""margin: 0 10px;""><b>FECHA:</b> " + Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy") + "</div>";
            grid.ExportSettings.Pdf.PageFooter.LeftCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Right;
            grid.ExportSettings.Pdf.PageFooter.MiddleCell.Text = @"<div style=""border-top: 1px dashed #000;margin-bottom: 20px;"">FIRMA Y SELLO</div>";
            grid.ExportSettings.Pdf.PageFooter.MiddleCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;

            grid.ExportSettings.FileName = fileName + "_" + Configuration.ConvertToClientTimeZone(DateTime.UtcNow).ToString("dd-MM-yyyy");

            grid.ExportSettings.Pdf.BorderType = GridPdfSettings.GridPdfBorderType.NoBorder;
            grid.ExportSettings.UseItemStyles = true;
            //grid.GridLines = GridLines.Horizontal;
            GridColumn col = grid.Columns.FindByUniqueNameSafe("RowNumber");
            if (col != null)
                col.Visible = true;
            grid.MasterTableView.ExportToPdf();
            CasoBLL.UpdateFechaEstado(CasoId, type);
        }
        catch (Exception q)
        {
            log.Error("Error al exportar a PDF", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
        }
    }
    protected void RecetaGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }

    protected void RadGrid_PdfExporting ( object sender, GridPdfExportingArgs e )
    {
        PrintInfo row = Artexacta.App.Receta.BLL.RecetaBLL.GetRecetaHeadByCasoIdForPrint(Convert.ToInt32(CasoIdHF.Value));
        if (row == null)
        {
            return;
        }
        string imageHeader = @"<div style=""text-align:left;""><img src=""" + ResolveClientUrl("~/Images/LogoPDF.jpg") + @""" alt=""SISTEMA SISA"" style=""height: 50px;"" /></div><br />";
        string title = @"<h1><u>RECETA</u></h1>";
        string subtitle = "<h3>Rp /</h3>";
        e.RawHTML = e.RawHTML.Replace(@"class=""indicaciones""", @"colspan=""4"" style=""border-bottom: 0.5pt dashed #000; padding-left: 10px;""");
        e.RawHTML = e.RawHTML.Replace(@"class=""medicamento""", @"colspan=""2""");

        title = @"<div style=""margin: 20px auto;text-align:center;"">" + title + "</div>\n";
        string pacienteInfo = @"<div style=""margin-top: 20px;""><b>PACIENTE:</b> " + row.NombrePaciente.ToUpper() + "</div>\n" +
                        @"<table id=""test"" border=""0"" style=""border-style:None;width:100%;table-layout:auto;empty-cells:show;"">\n" +
                        "\t<colgroup>\n\t\t<col  />\n\t\t<col  />\n\t</colgroup>\n" +
                        "\t<tbody>\n\t\t<tr>\n\t\t\t<td><b>PÓLIZA:</b> " + row.NumeroPoliza + "</td>\n\t\t\t<td><b>TELÉFONO:</b> " + row.Telefono + "</td>\n\t\t</tr>\n\t</tbody>\n</table>\n";
        string medicoInfo = @"<table id=""test"" border=""0"" style=""border-style:None;width:100%;table-layout:auto;empty-cells:show;"">\n" +
                        "\t<colgroup>\n\t\t<col  />\n\t\t<col  />\n\t</colgroup>\n" +
                        "\t<tbody>\n\t\t<tr>\n\t\t\t<td><b>Enfermera(o):</b> " + row.MedicoNombre.ToUpper() + "</td>\n\t\t\t<td></td>\n\t\t</tr>\n\t</tbody>\n</table>\n";

        string header = pacienteInfo + medicoInfo;

        if (string.IsNullOrEmpty(row.CarnetIdentidad))
        {
            ((RadGrid)sender).ExportSettings.Pdf.PageFooter.RightCell.Text = @"<table border=""0"" style=""border-style:None;height: 40px;"" width=""145px""><colgroup><col width=""5px"" /><col /><col width=""10px"" /></colgroup><tr><td></td><td style=""border-top: 1px dashed #000; text-align: center;"" align=""center""><b>FIRMA PACIENTE</b></td><td></td></tr></table>";
        }
        else
        {
            ((RadGrid)sender).ExportSettings.Pdf.PageFooter.RightCell.Text = @"<table border=""0"" style=""border-style:None;height: 40px;"" width=""145px""><colgroup><col width=""5px"" /><col /><col width=""10px"" /></colgroup><tr><td></td><td style=""border-top: 1px dashed #000; text-align: center;"" align=""center""><b>CI:</b> " + row.CarnetIdentidad + "</td><td></td></tr></table>";
        }
        ((RadGrid)sender).ExportSettings.Pdf.PageFooter.RightCell.TextAlign = GridPdfPageHeaderFooterCell.CellTextAlign.Center;
        //string separator = @"<div style=""padding: 10px;"">&nbsp;</div>";
        e.RawHTML = imageHeader + title + header + subtitle + e.RawHTML;
    }

    private void RadGrid_ItemCreated(RadGrid sender, GridItem Item)
    {
        if (RecetaToPrintRadGrid.ID == sender.ID)
        {
            if (Item.ItemType == GridItemType.Item || Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)Item;
                string id = "MedicamentoId";
                id = item.GetDataKeyValue(id).ToString();

                List<string> ids = new List<string>();
                ids.AddRange(ExportIDHF.Value.Split(new char[] { ',' }));
                if (!ids.Contains(id))
                {
                    Item.Display = false;
                }
            }
            else if (Item.ItemType == GridItemType.CommandItem)
            {
                Item.PrepareItemStyle();
            }
            else if (Item.ItemType == GridItemType.Footer)
            {
                isExport = "";
                ExportIDHF.Value = "";
            }
        }
        else
        {
            if (Item.ItemType == GridItemType.Header)
            {
                try
                {
                    int CasoId = Convert.ToInt32(this.CasoIdHF.Value);
                    //FechaEstado = CasoBLL.GetCasoByCasoId(CasoId).FechaEstadoMedicamento;
                }
                catch { }
            }
            else if ((Item.ItemType == GridItemType.Item || Item.ItemType == GridItemType.AlternatingItem) && FechaEstado != null)
            {
                GridDataItem dataBoundItem = Item as GridDataItem;
                if (dataBoundItem.DataItem != null)
                {
                    bool aprobado = true;
                    if (!aprobado)
                    {
                        (Item.FindControl("ExportCheckBox") as CheckBox).Visible = false;
                    }
                    else
                    {
                        DateTime fecha = ((IFechaCreacion)dataBoundItem.DataItem).FechaCreacion;
                        if (FechaEstado != null)
                        {
                            //Si esta FechaEstado es anterior a fecha.
                            if (FechaEstado.CompareTo(fecha) < 0)
                            {
                                (Item.FindControl("ExportCheckBox") as CheckBox).Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }

    protected void FileManager_Command(object sender, CommandEventArgs e)
    {
        FileManager.OpenFileManager(e.CommandName, Convert.ToInt32(e.CommandArgument), false);
    }

    protected void PacienteODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Paciente.");
            e.ExceptionHandled = true;
        }
        else
        {
            if (PacienteBLL.isCritico(Convert.ToInt32(PacienteIdHF.Value)))
            {
                CasoMedicoTitle.Text += " <span style='color: #f00;'>[Paciente con enfermedades Crónicas]</span>";
                cssCritic.Text = "<style>" +
                                 "  .critic legend" +
                                 "  {" +
                                 "      color: #f00;" +
                                 "  }" +
                                 "  .critic fieldset" +
                                 "  {" +
                                 "      border-color: #F00;" +
                                 "  }" +
                                 "</style>";
            }
        }
    }
    protected void PacienteODS_Updated ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al actualizar los datos del Paciente.");
            e.ExceptionHandled = true;
        }
    }
    protected void HistorialLB_Click ( object sender, EventArgs e )
    {
        try
        {
            int PacienteId = Convert.ToInt32(this.PacienteIdHF.Value);
            int CasoId = Convert.ToInt32(CasoIdHF.Value);

            Session["PacienteId"] = PacienteId.ToString();
            Session["CasoId"] = CasoId.ToString();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener algunos datos para mostrar el historial del paciente.");
            log.Error("Function HistorialLB_Click on page CasoMedicoDetalle.aspx", ex);
        }
        Response.Redirect(UrlHistorial);
    }
    protected void AdminGastosLB_Click ( object sender, EventArgs e )
    {
        int CasoId = 0;
        try
        {
            CasoId = Convert.ToInt32(this.CasoIdHF.Value);
            Session["CasoId"] = CasoId.ToString();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Caso de Enfermería.");
            log.Error("Function AdminGastosLB_Click on page CasoMedicoLista.aspx, convert CasoID", ex);
            return;
        }
        Response.Redirect("~/Gasto/GastoDetalle.aspx");
    }
    private void returnTo ()
    {
        Session["MODE"] = "ENFERMERIA";
        Response.Redirect(returnHL.CommandArgument);
    }
    protected void returnHL_Click ( object sender, EventArgs e )
    {
        returnTo();
    }
    protected void NewEnfermedadCronicaImageButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int aseguradoId = Convert.ToInt32(AseguradoIdHF.Value);
            RadComboBox EnfermedadCronicaRadComboBox = (RadComboBox)CasoFV.FindControl("EnfermedadCronicaRadComboBox");
            int enfermedadCronicaId = Convert.ToInt32(EnfermedadCronicaRadComboBox.SelectedValue);
            EnfermedadCronicaBLL.InsertEnfermedadCronicaAsegurado(aseguradoId, enfermedadCronicaId);
            Repeater EnfermedadesCronicasRepeater = (Repeater)CasoFV.FindControl("EnfermedadesCronicasRepeater");
            EnfermedadesCronicasRepeater.DataBind();
            EnfermedadCronicaRadComboBox.ClearSelection();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al insertar la enfermedad Crónica.");
            log.Error("Function NewEnfermedadCronicaImageButton_Click on page CasoMedicoLista.aspx", q);
        }
    }
    protected void DeleteEnfermedadCronicaImageButton_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int aseguradoId = Convert.ToInt32(AseguradoIdHF.Value);
            int enfermedadCronicaId = Convert.ToInt32(e.CommandArgument);
            EnfermedadCronicaBLL.DeleteEnfermedadCronicaAsegurado(aseguradoId, enfermedadCronicaId);
            Repeater EnfermedadesCronicasRepeater = (Repeater)CasoFV.FindControl("EnfermedadesCronicasRepeater");
            EnfermedadesCronicasRepeater.DataBind();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al eliminar la enfermedad Crónica.");
            log.Error("Function DeleteEnfermedadCronicaImageButton_Command on page CasoMedicoLista.aspx", q);
        }
    }
    protected void EnfermedadesCronicasODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Enfermedades Crónicas.");
            e.ExceptionHandled = true;
        }
    }
    protected void SaveObservacionesLB_Click(object sender, EventArgs e)
    {
        try
        {
            TextBox ObservacionesTextBox = (TextBox)CasoFV.FindControl("ObservacionesTextBox");
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            string CodigoCaso = ((Label)CasoFV.FindControl("CodigoCasoLabel")).Text;
            CasoBLL.UpdateCasoMedico(CasoId, CodigoCaso, ObservacionesTextBox.Text);
            CasoFV.DataBind();
            SystemMessages.DisplaySystemMessage("Observación actualizada.");
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar la Observación.");
            log.Error("Error al actualizar la observacion de caso medico", q);
        }
    }
    protected void CasoODS_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //e.InputParameters["Antecedentes"] = "";
        //e.InputParameters["AntecedentesAlergicos"] = "";
        //e.InputParameters["AntecedentesGinecoobstetricos"] = "";
    }
}