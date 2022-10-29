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

public partial class Cliente_ClienteDetails : System.Web.UI.Page
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
        int ClienteId = 0;
        try
        {
            ClienteId = Convert.ToInt32(ClienteIdHF.Value);
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            Response.Redirect("~/Cliente/ClienteList.aspx");
        }
        if (ClienteId <= 0)
            ClienteFV.ChangeMode(FormViewMode.Insert);
        else
            ClienteFV.ChangeMode(FormViewMode.ReadOnly);
    }

    private void ProcessSessionParameters()
    {
        int ClienteId = 0;
        if (Session["ClienteId"] != null && !string.IsNullOrEmpty(Session["ClienteId"].ToString()))
        {
            try
            {
                ClienteId = Convert.ToInt32(Session["ClienteId"].ToString());
                TitleLabel.Text = "Editar Cliente";
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
                log.Error("No se pudo obtener el ClienteId de Session['ClienteId']", ex);
                Response.Redirect("~/Cliente/ClienteList.aspx");
            }
        }
        ClienteIdHF.Value = ClienteId.ToString();
        Session["ClienteId"] = null;
    }

    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener el Cliente.");
            log.Error("Ocurrió un error al Obtener el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteODS_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Insertar el Cliente.");
            log.Error("Ocurrió un error al Insertar el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            int ClienteId = (int)e.ReturnValue;
            this.ClienteIdHF.Value = ClienteId.ToString();
        }
    }

    protected void ClienteODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Modificar el Cliente.");
            log.Error("Ocurrió un error al Modificar el Cliente", e.Exception);
            e.ExceptionHandled = true;
        }
    }


    protected void RedMedicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Redes Medicas.");
            log.Error("Ocurrió un error al Obtener la lista de Redes Medicas", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void RedClienteRedMedicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al Obtener la lista de Redes Medicas pertenecientes al Cliente.");
            log.Error("Ocurrió un error al Obtener la lista de Redes Medicas pertenecientes al Cliente.", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void RedClienteRedMedicaGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int ClienteId = Convert.ToInt32(ClienteIdHF.Value);
                int RedMedicaId = Convert.ToInt32(e.CommandArgument.ToString());

                if (RedClienteBLL.DeleteRedClienteRedMedica(ClienteId, RedMedicaId))
                    SystemMessages.DisplaySystemMessage("La Red Medica seleccionada se eliminó del Cliente correctamente.");
                else
                    SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al intentar eliminar la Red Medica seleccionada.");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir el RedMedicaId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al eliminar la Red Medica seleccionada.");
            }

            GridView RedClienteRedMedicaGV = (GridView)ClienteFV.FindControl("RedClienteRedMedicaGV");
            RedClienteRedMedicaGV.DataBind();
        }
    }
    protected void addRedMedicaRedCliente_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList RedMedicaDDL = (DropDownList)ClienteFV.FindControl("RedMedicaDDL");
            int RedMedicaId = Convert.ToInt32(RedMedicaDDL.SelectedValue);

            int ClienteId = Convert.ToInt32(ClienteIdHF.Value);
            if (!RedMedicaBLL.ExisteRedClienteRedMedica(ClienteId, RedMedicaId))
                RedClienteBLL.InsertRedClienteRedMedica(ClienteId, RedMedicaId);
            else
                SystemMessages.DisplaySystemErrorMessage("Error, ya existe la Red Medica en el Cliente.");

            GridView RedClienteRedMedicaGV = (GridView)ClienteFV.FindControl("RedClienteRedMedicaGV");
            RedClienteRedMedicaGV.DataBind();
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al agregar la la Red Medica al Cliente.");
        }
    }

    protected void ClientePrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sPreId = (e.Item as GridDataItem).GetDataKeyValue("preId").ToString();
            int preId = 0;
            try
            {
                preId = Convert.ToInt32(sPreId);
            }
            catch
            {
                log.Error("No pudo convertir " + sPreId + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedClientePrestacionesBLL.DeleteRedClientePrestaciones(preId);
                log.Info("Se elimino completamente la información de Cliente Prestacion " + preId.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de Cliente Prestacion " + preId.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La prestacion de Cliente " + preId + " no pudo ser eliminada");
            }
            ClientePrestacionesRadGrid.DataBind();
            return;
        }
    }

    protected void ClientePrestacionesRadGrid_UpdateCommand(object source,
        Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        GridEditManager editMan = editedItem.EditManager;
        Dictionary<string, string> values = new Dictionary<string, string>();
        int id = int.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["preId"].ToString());
        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
        {
            if (column is IGridEditableColumn)
            {
                GridBoundColumn colDisplay = (GridBoundColumn)column;
                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                if (editableCol.IsEditable)
                {
                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                    string editorText = null;
                    if (editor is GridTextColumnEditor)
                    {
                        editorText = (editor as GridTextColumnEditor).Text;
                        values.Add(colDisplay.DataField, editorText);
                    }
                }
            }
        }

        try
        {
            PrestacionesAlert.Visible = false;
            if (decimal.Parse(values["CoPagoMonto"]) != 0 && decimal.Parse(values["CoPagoPorcentaje"]) != 0)
            {
                isUpdating = true;
                PrestacionesAlert.Visible = true;
                e.Canceled = true;
                return;
            }
            string sTipoPrestacion = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["TipoPrestacion"].ToString();
            RedClientePrestacionesBLL.UpdateClientePrestaciones(id,
                                                                sTipoPrestacion,
                                                                int.Parse(ClienteIdHF.Value),
                                                                decimal.Parse(values["Precio"]),
                                                                decimal.Parse(values["CoPagoMonto"]),
                                                                decimal.Parse(values["CoPagoPorcentaje"]),
                                                                int.Parse(values["CantidadConsultasAno"]),
                                                                int.Parse(values["DiasCarencia"]),
                                                                decimal.Parse(values["MontoTope"]));
            ClientePrestacionesRadGrid.DataBind();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            return;
        }
    }

    protected void ClientePrestacionesRadGrid_PreRender(object sender, EventArgs e)
    {
        if (!isUpdating)
        {
            PrestacionesAlert.Visible = false;
        }
        isUpdating = false;
        foreach (GridDataItem dataItem in ClientePrestacionesRadGrid.Items)
        {
            int preId = Convert.ToInt32(dataItem.GetDataKeyValue("preId"));
            ImageButton delete = (ImageButton)dataItem["DeleteCommandColumn"].Controls[0];
            if (preId < 1)
            {
                delete.Visible = false;
            }
        }
    }

    protected void ClientePrestacionesRadGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            //the item is in edit mode    
            GridEditableItem editedItem = e.Item as GridEditableItem;
            ((RadNumericTextBox)editedItem["Precio"].Controls[0]).Width = Unit.Pixel(80);
            ((RadNumericTextBox)editedItem["CoPagoMonto"].Controls[0]).Width = Unit.Pixel(80);
            ((RadNumericTextBox)editedItem["CoPagoPorcentaje"].Controls[0]).Width = Unit.Pixel(80);
            ((RadNumericTextBox)editedItem["CantidadConsultasAno"].Controls[0]).Width = Unit.Pixel(80);
            ((RadNumericTextBox)editedItem["DiasCarencia"].Controls[0]).Width = Unit.Pixel(80);
            ((RadNumericTextBox)editedItem["MontoTope"].Controls[0]).Width = Unit.Pixel(80);
            string TipoPrestacion = editedItem.GetDataKeyValue("TipoPrestacion").ToString();
            if (TipoPrestacion == "ES")
                EspecialidadesPNL.Visible = true;
            else if (TipoPrestacion == "LA")
                GruposLabPNL.Visible = true;
            else if (TipoPrestacion == "IM")
                ImagenPNL.Visible = true;
            else if (TipoPrestacion == "OD")
                OdontoPNL.Visible = true;
            else if (TipoPrestacion == "CI")
                CirugiasPNL.Visible = true;
        }
        else if (e.Item is GridDataItem)
        {
            //the item is in regular mode
            GridDataItem dataItem = e.Item as GridDataItem;
            string TipoPrestacion = dataItem.GetDataKeyValue("TipoPrestacion").ToString();
            if (TipoPrestacion == "ES")
                EspecialidadesPNL.Visible = false;
            else if (TipoPrestacion == "LA")
                GruposLabPNL.Visible = false;
            else if (TipoPrestacion == "IM")
                ImagenPNL.Visible = false;
            else if (TipoPrestacion == "OD")
                OdontoPNL.Visible = false;
            else if (TipoPrestacion == "CI")
                CirugiasPNL.Visible = false;
        }
    }

    protected void EspecialidadPrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sId = (e.Item as GridDataItem).GetDataKeyValue("detId").ToString();
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(sId);
            }
            catch
            {
                log.Error("No pudo convertir " + sId + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedEspecialidadPrestacionesBLL.DeleteRedEspecialidadPrestaciones(Id);
                log.Info("Se elimino completamente la información de especialidad " + Id.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de especialidad " + Id.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La especialidad " + Id + " no pudo ser eliminada");
            }
            EspecialidadPrestacionesRadGrid.DataBind();
            EspecialidadesDDL.DataBind();
            return;
        }
    }

    protected void AgregarEspecialidadBTN_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            RedEspecialidadPrestacionesBLL.UpdateEspecialidadPrestaciones(id,
                                                                int.Parse(EspecialidadesDDL.SelectedValue),
                                                                int.Parse(ClienteIdHF.Value),
                                                                FrecuenciaDDL.SelectedValue,
                                                                int.Parse(txtCantVideoLLamadas.Text));
            EspecialidadPrestacionesRadGrid.DataBind();
            EspecialidadesDDL.DataBind();
        }
        catch (Exception ex)
        {
            return;
        }
    }

    protected void GruposLabPrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sId = (e.Item as GridDataItem).GetDataKeyValue("detId").ToString();
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(sId);
            }
            catch
            {
                log.Error("No pudo convertir " + sId + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedGruposLabPrestacionesBLL.DeleteRedGruposLabPrestaciones(Id);
                log.Info("Se elimino completamente la información de grupo de laboratorio " + Id.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de grupo de laboratorio " + Id.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("El grupo de laboratorio " + Id + " no pudo ser eliminado");
            }
            GruposLabPrestacionesRadGrid.DataBind();
            GruposLabDDL.DataBind();
            return;
        }
    }

    protected void AgregarGruposLabBTN_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            RedGruposLabPrestacionesBLL.UpdateGruposLabPrestaciones(id,
                                                                int.Parse(GruposLabDDL.SelectedValue),
                                                                int.Parse(ClienteIdHF.Value));
            GruposLabPrestacionesRadGrid.DataBind();
            GruposLabDDL.DataBind();
        }
        catch (Exception ex)
        {
            return;
        }
    }

    protected void ImagenPrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sId = (e.Item as GridDataItem).GetDataKeyValue("detId").ToString();
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(sId);
            }
            catch
            {
                log.Error("No pudo convertir " + sId + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedImagenPrestacionesBLL.DeleteImagenPrestaciones(Id);
                log.Info("Se elimino completamente la información de imagenologia " + Id.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de imagenologia " + Id.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La imagenologia " + Id + " no pudo ser eliminada");
            }
            ImagenPrestacionesRadGrid.DataBind();
            ImagenDDL.DataBind();
            return;
        }
    }

    protected void AgregarImagenBTN_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            RedImagenPrestacionesBLL.UpdateImagenPrestaciones(id,
                                                                int.Parse(ImagenDDL.SelectedValue),
                                                                int.Parse(ClienteIdHF.Value));
            ImagenPrestacionesRadGrid.DataBind();
            ImagenDDL.DataBind();
        }
        catch (Exception ex)
        {
            return;
        }
    }

    protected void CirugiasPrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sId = (e.Item as GridDataItem).GetDataKeyValue("detId").ToString();
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(sId);
            }
            catch
            {
                log.Error("No pudo convertir " + sId + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedCirugiasPrestacionesBLL.DeleteCirugiasPrestaciones(Id);
                log.Info("Se elimino completamente la información de cirugias " + Id.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de cirugias " + Id.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La cirugia " + Id + " no pudo ser eliminada");
            }
            CirugiasPrestacionesRadGrid.DataBind();
            CirugiasDDL.DataBind();
            return;
        }
    }

    protected void CirugiasPrestacionesRadGrid_UpdateCommand(object source,
        Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        GridEditManager editMan = editedItem.EditManager;
        Dictionary<string, string> values = new Dictionary<string, string>();
        int id = int.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["detId"].ToString());
        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
        {
            if (column is IGridEditableColumn)
            {
                GridBoundColumn colDisplay = (GridBoundColumn)column;
                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                if (editableCol.IsEditable)
                {
                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                    string editorText = null;
                    if (editor is GridTextColumnEditor)
                    {
                        editorText = (editor as GridTextColumnEditor).Text;
                        values.Add(colDisplay.DataField, editorText);
                    }
                }
            }
        }

        try
        {
            string sCodigoArancelarioId = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CodigoArancelarioId"].ToString();
            RedCirugiasPrestacionesBLL.UpdateCirugiasPrestaciones(id,
                                                                sCodigoArancelarioId,
                                                                int.Parse(ClienteIdHF.Value),
                                                                decimal.Parse(values["detMontoTope"]),
                                                                int.Parse(values["detCantidadTope"]));
            CirugiasPrestacionesRadGrid.DataBind();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            return;
        }
    }

    protected void CirugiasPrestacionesRadGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            //the item is in edit mode    
            GridEditableItem editedItem = e.Item as GridEditableItem;
            ((RadNumericTextBox)editedItem["detMontoTope"].Controls[0]).Width = Unit.Pixel(80);
            ((RadNumericTextBox)editedItem["detCantidadTope"].Controls[0]).Width = Unit.Pixel(80);
        }
    }

    protected void AgregarCirugiasBTN_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            RedCirugiasPrestacionesBLL.UpdateCirugiasPrestaciones(id,
                                                                CirugiasDDL.SelectedValue,
                                                                int.Parse(ClienteIdHF.Value),
                                                                0,
                                                                0);
            CirugiasPrestacionesRadGrid.DataBind();
            CirugiasDDL.DataBind();
        }
        catch (Exception ex)
        {
            return;
        }
    }

    protected void OdontoPrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sId = (e.Item as GridDataItem).GetDataKeyValue("detId").ToString();
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(sId);
            }
            catch
            {
                log.Error("No pudo convertir " + sId + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedOdontoPrestacionesBLL.DeleteOdontoPrestaciones(Id);
                log.Info("Se elimino completamente la información de odontologia " + Id.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de odontologia " + Id.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La odontologia " + Id + " no pudo ser eliminada");
            }
            OdontoPrestacionesRadGrid.DataBind();
            OdontoDDL.DataBind();
            return;
        }
    }

    protected void OdontoPrestacionesRadGrid_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;
        GridEditManager editMan = editedItem.EditManager;
        Dictionary<string, string> values = new Dictionary<string, string>();
        int id = int.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["detId"].ToString());
        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
        {
            if (column is IGridEditableColumn)
            {
                GridBoundColumn colDisplay = (GridBoundColumn)column;
                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                if (editableCol.IsEditable)
                {
                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                    string editorText = null;
                    if (editor is GridTextColumnEditor)
                    {
                        editorText = (editor as GridTextColumnEditor).Text;
                        values.Add(colDisplay.DataField, editorText);
                    }
                }
            }
        }

        try
        {
            string sPrestacionOdontologicaId = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["PrestacionOdontologicaId"].ToString();
            RedOdontoPrestacionesBLL.UpdateOdontoPrestaciones(id,
                                                                int.Parse(sPrestacionOdontologicaId),
                                                                int.Parse(ClienteIdHF.Value),
                                                                int.Parse(values["detCantidadConsultasAno"]));
            OdontoPrestacionesRadGrid.DataBind();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            return;
        }
    }

    protected void OdontoPrestacionesRadGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            //the item is in edit mode    
            GridEditableItem editedItem = e.Item as GridEditableItem;
            ((RadNumericTextBox)editedItem["detCantidadConsultasAno"].Controls[0]).Width = Unit.Pixel(80);
        }
    }

    protected void AgregarOdontoBTN_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            RedOdontoPrestacionesBLL.UpdateOdontoPrestaciones(id,
                                                                int.Parse(OdontoDDL.SelectedValue),
                                                                int.Parse(ClienteIdHF.Value),
                                                                0);
            OdontoPrestacionesRadGrid.DataBind();
            OdontoDDL.DataBind();
        }
        catch (Exception ex)
        {
            return;
        }
    }
}