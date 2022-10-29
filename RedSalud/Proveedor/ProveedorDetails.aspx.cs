using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.Proveedor;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Poliza;
using System.Collections;
using System.Collections.Specialized;
using Artexacta.App.Proveedor.BLL;
using Artexacta.App.RedMedica.BLL;
using Artexacta.App.ProveedorPrestaciones.BLL;
using Artexacta.App.ProveedorPrestaciones;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Medico;
using System.Configuration;
using System.Web.Services;
using Artexacta.App.User.BLL;
using Artexacta.App.User;

public partial class Proveedor_ProveedorDetails : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
        
            ProcessSessionParameters();
            SetViewMode();
           
        }
        string CodigoRedMedica = System.Web.Configuration.WebConfigurationManager.AppSettings["CodigoRedMedica"];
        CodigoRedMedicaHF.Value = CodigoRedMedica;

    }
    private void SetViewMode()
    {
        if (ProveedorIdHF.Value.Equals("0"))
        {
            ProveedorFV.ChangeMode(FormViewMode.Insert);
            this.Title = this.TitleLabel.Text = "Nuevo Proveedor";
            newProveedorCiudad.Visible = false;
            ProveedorCiudadPanel.Visible = false;
            CiudadGridView.Columns[0].Visible = false;
            CiudadGridView.Columns[1].Visible = false;
            RedMedicaPanel.Visible = false;
        }
        else
        {
            ProveedorFV.ChangeMode(FormViewMode.ReadOnly);
            this.Title = this.TitleLabel.Text = "Editar Proveedor";
            newProveedorCiudad.Visible = true;
            ProveedorCiudadPanel.Visible = true;
            RedMedicaPanel.Visible = true;

        }
    }
    protected void ProcessSessionParameters()
    {
        int ProveedorId = 0;
        if (Session["ProveedorId"] != null && !string.IsNullOrEmpty(Session["ProveedorId"].ToString()))
        {
            try
            {
                ProveedorId = Convert.ToInt32(Session["ProveedorId"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session ProveedorId:" + Session["ProveedorId"]);
            }
        }
        ProveedorIdHF.Value = ProveedorId.ToString();
        Session["ProveedorId"] = null;
        int UserID = 0;
        if (Session["UserID"] != null && !string.IsNullOrEmpty(Session["UserID"].ToString()))
        {
            try
            {
                UserID = Convert.ToInt32(Session["UserID"]);
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session UserID:" + Session["UserID"]);
            }
        }
        UserIDHF.Value = UserID.ToString();
        Session["UserID"] = null;
        if (Session["UserName"] != null && !string.IsNullOrEmpty(Session["UserName"].ToString()))
        {
            try
            {
                UserNameHF.Value = Session["UserName"].ToString();
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session UserName:" + Session["UserName"]);
            }
        }
        Session["UserName"] = null;
    }

    protected void ProveedorODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los datos del Médico.");
            log.Error("Function ProveedorODS_Selected on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }


        //newProveedorCiudad.Visible = (ProveedorFV.CurrentMode == FormViewMode.Edit);
        //ProveedorCiudadPanel.Visible = (ProveedorFV.CurrentMode != FormViewMode.Insert);
        //CiudadGridView.Columns[0].Visible = (ProveedorFV.CurrentMode == FormViewMode.Edit);
        //CiudadGridView.Columns[1].Visible = (ProveedorFV.CurrentMode == FormViewMode.Edit);
    }
    protected void ProveedorODS_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Insertar el nuevo Médico.");
            log.Error("Function ProveedorODS_Inserted on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            int ProveedorId = (int)e.ReturnValue;
            if (ProveedorId <= 0)
                SystemMessages.DisplaySystemErrorMessage("Se inserto el nuevo Médico pero no se pudo obtener los datos.");
            else
            {
                this.ProveedorIdHF.Value = ProveedorId.ToString();
                SystemMessages.DisplaySystemMessage("Se inserto el nuevo Médico correctamente.");
            }
        }

    }
    protected void ProveedorODS_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al modificar el Proveedor.");
            log.Error("Function ProveedorODS_Updated on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CancelUpdate_Click(object sender, EventArgs e)
    {
        ProveedorFV.ChangeMode(FormViewMode.ReadOnly);
        this.Title = this.TitleLabel.Text = "Médico";
    }
    protected void RadComboBox_DataBinding(object sender, EventArgs e)
    {
        try
        {
            RadComboBox rcb = (sender as RadComboBox);
            rcb.Items.Add(new RadComboBoxItem(rcb.Text, rcb.SelectedValue));
        }
        catch (Exception q)
        {
            log.Error("Error in RadComboBox_DataBinding on page ProveedorDetails.aspx", q);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener las Especialidades.");
        }
    }
    protected void RadComboBox1_OnDataBound(object sender, EventArgs e)
    {
        string itemText = "MÉDICO";
        var rcb = (RadComboBox)sender;

        foreach (RadComboBoxItem item in rcb.Items)
        {
            if (itemText == item.Text.ToUpper())
            {
                item.Enabled = false;
            }
        }
    }
    //protected void InsertRadComboBox_DataBinding ( object sender, EventArgs e )
    //{
    //    try
    //    {
    //        if (!string.IsNullOrWhiteSpace(UserIDHF.Value))
    //        {
    //            RadComboBox rcb = (sender as RadComboBox);
    //            rcb.Items.Add(new RadComboBoxItem(UserNameHF.Value, UserIDHF.Value));
    //            rcb.SelectedValue = UserIDHF.Value;
    //        }
    //    }
    //    catch (Exception q)
    //    {
    //        log.Error("Error in InsertRadComboBox_DataBinding on page ProveedorDetails.aspx", q);
    //        SystemMessages.DisplaySystemErrorMessage("Error al obtener el Proveedor.");
    //    }
    //}
    protected void ProveedorFV_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (ProveedorFV.CurrentMode == FormViewMode.ReadOnly)
            {
                string TipoProveedorId = (ProveedorFV.FindControl("TipoProveedorId") as HiddenField).Value;
                if (TipoProveedorId == "MEDICO")
                {
                    ProveedorFV.FindControl("EspecialidadContainer").Visible = true;
                    ProveedorFV.FindControl("ColegioMedicoContainer").Visible = true;
                    ProveedorFV.FindControl("SedesContainer").Visible = true;
                    ProveedorFV.FindControl("CostoConsultaContainer").Visible = true;
                    ProveedorFV.FindControl("PorcentageDescuentoContainer").Visible = true;

                }

            }
            else
            {
                Label LabelMedicoId = (Label)ProveedorFV.FindControl("LabelMedicoId");
                if (LabelMedicoId.Text.Length > 0)
                {
                    string s = LabelMedicoId.Text;
                    CargarComboMedicoxMedicoId(int.Parse(s));
                    DeshabilitarOpcionesMedicos();
                }
            }
        }
        catch (Exception q)
        {
            log.Error("Error in ProveedorFV_DataBound on page ProveedorDetails.aspx", q);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el Proveedor.");
            Response.Redirect("~/Proveedor/ProveedorList.aspx", true);
        }
    }
    protected void HyperLink1_Click(object sender, EventArgs e)
    {
        Session["ProveedorId"] = null;
        Response.Redirect("~/Proveedor/ProveedorList.aspx");
    }
    protected void ProveedorFV_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Cancel = true;
        try
        {
            string[] keys = new string[] { "ProveedorId", "RedMedicaId", "EspecialistaId", "EspecialidadId" };
            foreach (string key in keys)
            {
                if (string.IsNullOrWhiteSpace(e.Values[key].ToString()))
                {
                    e.Values[key] = 0;
                }
            }
            Proveedor proveedor = new Proveedor(e.Values);
            ProveedorBLL.InsertProveedorNew(ref proveedor);
            ProveedorIdHF.Value = proveedor.ProveedorId.ToString();
            ProveedorFV.ChangeMode(FormViewMode.ReadOnly);
            RedMedicaPanel.Visible = true;
            RedMedicaProveedorGridView.DataBind();
            ProveedorCiudadPanel.Visible = true;
            newProveedorCiudad.Visible = true;
            CiudadGridView.DataBind();
            SystemMessages.DisplaySystemMessage("Proveedor Guardado.");
        }
        catch (Exception q)
        {
            log.Error("Error Inserting the Proveedor", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Guardar el Proveedor.");
        }
    }
    protected void InsertProveedorLB_Click(object sender, EventArgs e)
    {
        ProveedorFV.InsertItem(false);
    }

    protected void ProveedorFV_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        e.Cancel = true;
        bool changed = false;
        try
        {
            foreach (string key in e.NewValues.Keys)
            {
                if (!e.NewValues[key].Equals(e.OldValues[key]))
                {
                    changed = true;
                    break;
                }
            }
            e.NewValues["RedMedicaId"] = 0;
            if (changed)
            {
                Proveedor proveedor = new Proveedor(e.NewValues);
                ProveedorBLL.UpdateProveedorNew(proveedor);
                //ProveedorBLL.UpdateProveedor(proveedor);
            }
            SystemMessages.DisplaySystemMessage("Proveedor Modificado.");
            changed = true;
        }
        catch (Exception q)
        {
            changed = false;
            log.Error("Error Updating the Proveedor", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Modificar el Proveedor.");
        }
        if (changed)
        {
            Session["ProveedorId"] = ProveedorIdHF.Value;
            Response.Redirect("~/Proveedor/ProveedorDetails.aspx");
        }
    }
    protected void UpdateProveedorLB_Click(object sender, EventArgs e)
    {
        ProveedorFV.UpdateItem(false);
    }
    protected void RedMedicaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Aseguradoras.");
            log.Error("Function RedMedicaODS_Selected on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void TipoProveedorODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de tipos de Proveedor.");
            log.Error("Function TipoProveedorODS_Selected on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CiudadODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Ciudades.");
            log.Error("Function CiudadODS_Selected on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ProveedorCiudadODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Ciudades del Proveedor.");
            log.Error("Function ProveedorCiudadODS_Selected on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
        {
            List<ProveedorCiudad> proveedorCiudades = (e.ReturnValue as List<ProveedorCiudad>);
            CiudadesExistentesHF.Value = "";
            foreach (ProveedorCiudad proveedorCiudad in proveedorCiudades)
            {
                CiudadesExistentesHF.Value += proveedorCiudad.CiudadId;
                if (proveedorCiudades.IndexOf(proveedorCiudad) < (proveedorCiudades.Count - 1))
                {
                    CiudadesExistentesHF.Value += ",";
                }
            }
        }
    }
    protected void InsertProveedorCiudadLB_Click(object sender, EventArgs e)
    {
        try
        {
            ProveedorCiudad pc = new ProveedorCiudad();
            pc.ProveedorId = int.Parse(ProveedorIdHF.Value);
            pc.CiudadId = CiudadRCB.SelectedValue;
            pc.Direccion = PCDireccionTextBox.Text;
            pc.Telefono = TelefonoRadNumericTextBox.Text;
            pc.Celular = CelularRadNumericTextBox.Text;
            ProveedorCiudadBLL.InsertProveedorCiudad(pc);
            SystemMessages.DisplaySystemMessage("Dirección por Ciudad Guardada.");
            CiudadGridView.DataBind();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al insertar la Dirección por Ciudad.");
            log.Error("Function InsertProveedorCiudadLB_Click on page ProveedorDetails.aspx", q);
        }
    }
    protected void CiudadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int ProveedorId = int.Parse(ProveedorIdHF.Value);
                string CiudadId = e.CommandArgument.ToString();
                ProveedorCiudadBLL.DeleteProveedorCiudad(ProveedorId, CiudadId);
                SystemMessages.DisplaySystemMessage("Dirección por Ciudad Eliminada.");
                CiudadGridView.DataBind();
            }
            catch (Exception q)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al eliminar la Dirección por Ciudad.");
                log.Error("Function CiudadGridView_RowCommand CommandName = '" + e.CommandName + "' on page ProveedorDetails.aspx", q);
            }
        }
        else if (e.CommandName == "EditRecord")
        {
            try
            {
                int ProveedorId = int.Parse(ProveedorIdHF.Value);
                string CiudadId = e.CommandArgument.ToString();
                ProveedorCiudad pc = ProveedorCiudadBLL.GetProveedorCiudadByCiudadIdAndProveedorId(ProveedorId, CiudadId);
                CiudadRCB.SelectedValue = pc.CiudadId;
                CiudadLabelText.Text = pc.NombreCiudad;
                PCDireccionTextBox.Text = pc.Direccion;
                TelefonoRadNumericTextBox.Text = pc.Telefono;
                CelularRadNumericTextBox.Text = pc.Celular;

                //ProveedorCiudadInsertPanel.ToolTip = "Editar Dirección por Ciudad";
                //ProveedorCiudadInsertPanel.DefaultButton = "UpdateProveedorCiudadLB";
                //CiudadLabelText.Attributes.CssStyle.Remove("display");
                //CiudadRCB.Attributes.CssStyle.Add("display", "none");
                //InsertProveedorCiudadLB.Attributes.CssStyle.Add("display", "none");
                //UpdateProveedorCiudadLB.Attributes.CssStyle.Remove("display");
                ScriptManager.RegisterStartupScript(this, this.GetType(), this.ClientID + "_UpdateProveedorCiudad", "$(document).ready(function(){newProveedorCiudad(true);});", true);
            }
            catch (Exception q)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al eliminar la Dirección por Ciudad.");
                log.Error("Function CiudadGridView_RowCommand CommandName = '" + e.CommandName + "' on page ProveedorDetails.aspx", q);
            }
        }
    }
    protected void UpdateProveedorCiudadLB_Click(object sender, EventArgs e)
    {
        try
        {
            ProveedorCiudad pc = new ProveedorCiudad();
            pc.ProveedorId = int.Parse(ProveedorIdHF.Value);
            pc.CiudadId = CiudadRCB.SelectedValue;
            pc.Direccion = PCDireccionTextBox.Text;
            pc.Telefono = TelefonoRadNumericTextBox.Text;
            pc.Celular = CelularRadNumericTextBox.Text;
            ProveedorCiudadBLL.UpdateProveedorCiudad(pc);
            SystemMessages.DisplaySystemMessage("Dirección por Ciudad Guardada.");
            CiudadGridView.DataBind();
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al actualizar la Dirección por Ciudad.");
            log.Error("Function InsertProveedorCiudadLB_Click on page ProveedorDetails.aspx", q);
        }
    }
    protected void RedMedicaObjectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Redes Medicas del Proveedor.");
            log.Error("Function RedMedicaObjectDataSource_Selected on page ProveedorDetails.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void InsertRedMedicaProveedorCiudadLB_Click(object sender, EventArgs e)
    {
        try
        {
            int ProveedorId = int.Parse(ProveedorIdHF.Value);
            int RedMedicaId = int.Parse(RedMedicaDDL.SelectedValue);

            RedMedicaBLL.InsertRedMedicaProveedor(ProveedorId, RedMedicaId);
            RedMedicaProveedorGridView.DataBind();
            SystemMessages.DisplaySystemMessage("Se Insertó la Red Médica a este proveedor.");
        }
        catch (Exception q)
        {
            log.Error("Error al insertar la red medica " + q.Message);
            SystemMessages.DisplaySystemErrorMessage("Error Insertando la Red Médica a este proveedor.");
        }
    }
    protected void RedMedicaProveedorGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                int ProveedorId = int.Parse(ProveedorIdHF.Value);
                int RedMedicaId = int.Parse(e.CommandArgument.ToString());
                RedMedicaBLL.DeleteRedMedicaProveedor(ProveedorId, RedMedicaId);
                RedMedicaProveedorGridView.DataBind();
                SystemMessages.DisplaySystemMessage("Red Médica Eliminada.");
            }
            catch (Exception q)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al eliminar la Red Médica.");
                log.Error("Function RedMedicaProveedorGridView_RowCommand CommandName = '" + e.CommandName + "' on page ProveedorDetails.aspx", q);
            }
        }
    }

    protected void EstudioPrestacionesRadGrid_HideDeleteButton()
    {
        foreach (GridDataItem dataItem in EstudioPrestacionesRadGrid.Items)
        {
            int Id = Convert.ToInt32(dataItem.GetDataKeyValue("detId"));
            ImageButton delete = (ImageButton)dataItem["DeleteCommandColumn"].Controls[0];
            if (Id < 1)
            {
                delete.Visible = false;
                //delete.Style["display"] = "none";
            }
            else
            {
                delete.Visible = true;
                //delete.Style["display"] = "block";
            }
        }
    }

    protected void EstudioPrestacionesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sId = (e.Item as GridDataItem).GetDataKeyValue("detId").ToString();
            string sCategoriaId = TiposEstudiosDDL.SelectedValue;
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(sId);
            }
            catch
            {
                log.Error("No pudo convertir algunos de los ids requeridos");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                RedProvLabImgCarDetallePrestacionesBLL.DeleteProvLabImgCarDetallePrestaciones(Id, sCategoriaId);
                log.Info("Se elimino completamente la información de Estudio " + Id.ToString());
                //theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Cliente Prestacion", preId.ToString(), "El Id es la Prestacion");
                SystemMessages.DisplaySystemMessage("La información de Estudio " + Id.ToString() + " ha sido eliminada");
            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("El Estudio " + Id + " no pudo ser eliminado");
            }
            EstudioPrestacionesRadGrid.DataBind();
            return;
        }
    }

    protected void EstudioPrestacionesRadGrid_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
            string sEstudioId = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["EstudioId"].ToString();
            //string sCategoriaId = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CategoriaId"].ToString();
            string sCategoriaId = TiposEstudiosDDL.SelectedValue;
            RedProvLabImgCarDetallePrestacionesBLL.UpdateProvLabImgCarDetallePrestaciones(id,
                                                                int.Parse(ProveedorIdHF.Value),
                                                                sCategoriaId,
                                                                int.Parse(EstudioDDL.SelectedValue),
                                                                int.Parse(sEstudioId),
                                                                decimal.Parse(values["detPrecio"]));
            EstudioPrestacionesRadGrid.DataBind();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            return;
        }
    }

    protected void EstudioPrestacionesRadGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            //the item is in edit mode    
            GridEditableItem editedItem = e.Item as GridEditableItem;
            ((RadNumericTextBox)editedItem["detPrecio"].Controls[0]).Width = Unit.Pixel(80);
        }

        EstudioPrestacionesRadGrid_HideDeleteButton();
    }
    protected void EspecialidadRadComboBox_OnClientSelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox RadComboxEspecialidad = (RadComboBox)ProveedorFV.FindControl("EspecialidadRadComboBox");
        if (RadComboxEspecialidad.SelectedValue.Length > 0)
        {
            int idEspecialidad = int.Parse(RadComboxEspecialidad.SelectedValue);
            CargarComboMedico(idEspecialidad);
        }
    }
    private void CargarComboMedico(int EspecialidadId)
    {
        DropDownList RadComboxMedico = (DropDownList)ProveedorFV.FindControl("RadComboBoxMedico");
        try
        {
            List<Medico> modifiedList = new List<Medico>();

            List<Medico> list = MedicoBLL.getEspecialistasAutocompleteNew(EspecialidadId);

            if (list.Count > 0)
            {
                modifiedList.Add(new Medico()
                {
                    MedicoId = 0,
                    Nombre = "Selecciona Un Medico"
                });
                foreach (Medico medico in list)
                {
                    modifiedList.Add(medico);
                }

                RadComboxMedico.DataSource = modifiedList;
                RadComboxMedico.DataValueField = "MedicoId";
                RadComboxMedico.DataTextField = "Nombre";
                RadComboxMedico.DataBind();
                Limpiar();

            }
            else
            {
                RadComboxMedico.Items.Clear();
                modifiedList.Add(new Medico()
                {
                    MedicoId = 0,
                    Nombre = "No Existe Medicos Usuarios Para esa Especialidad"
                });
                RadComboxMedico.DataSource = modifiedList;
                RadComboxMedico.DataValueField = "MedicoId";
                RadComboxMedico.DataTextField = "Nombre";
                RadComboxMedico.DataBind();
            }

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Proveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Proveedor");
        }
    }

    private void CargarComboMedicoxMedicoId(int MedicoId)
    {
        DropDownList RadComboxMedico = (DropDownList)ProveedorFV.FindControl("RadComboBoxMedico");
        try
        {
            List<Medico> modifiedList = new List<Medico>();

            List<Medico> list = MedicoBLL.getEspecialistasxMedicoId(MedicoId);

            if (list.Count > 0)
            {

                foreach (Medico medico in list)
                {
                    modifiedList.Add(medico);
                }

                RadComboxMedico.DataSource = modifiedList;
                RadComboxMedico.DataValueField = "MedicoId";
                RadComboxMedico.DataTextField = "Nombre";
                RadComboxMedico.DataBind();


            }
            else
            {
                RadComboxMedico.Items.Clear();
                modifiedList.Add(new Medico()
                {
                    MedicoId = 0,
                    Nombre = "No Existe Medicos Usuarios Para esa Especialidad"
                });
                RadComboxMedico.DataSource = modifiedList;
                RadComboxMedico.DataValueField = "MedicoId";
                RadComboxMedico.DataTextField = "Nombre";
                RadComboxMedico.DataBind();
            }

        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de Proveedor de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la Carga de Proveedor");
        }
    }

    protected void RadComboBoxMedico_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDatosMedicos();
    }
    private void CargarDatosMedicos()
    {

        DropDownList RadComboxMedico = (DropDownList)ProveedorFV.FindControl("RadComboBoxMedico");
        int MedicoId = int.Parse(RadComboxMedico.SelectedValue);

        if (MedicoId > 0)
        {
            Label LabelMedicoId = (Label)ProveedorFV.FindControl("LabelMedicoId");
            LabelMedicoId.Text = MedicoId.ToString();
            List<Medico> list = MedicoBLL.getEspecialistasxMedicoId(MedicoId);
            TextBox NombresTextBox = (TextBox)ProveedorFV.FindControl("NombresTextBox");
            TextBox ApellidosTextBox = (TextBox)ProveedorFV.FindControl("ApellidosTextBox");
            TextBox SedesTextBox = (TextBox)ProveedorFV.FindControl("SedesTextBox");
            TextBox ColegioProveedorTextBox = (TextBox)ProveedorFV.FindControl("ColegioProveedorTextBox");
            TextBox DireccionTextBox = (TextBox)ProveedorFV.FindControl("DireccionTextBox");
            TextBox TelefonoOficinaRadNumericTextBox = (TextBox)ProveedorFV.FindControl("TelefonoOficinaRadNumericTextBox");
            TextBox CelularRadNumericTextBox = (TextBox)ProveedorFV.FindControl("CelularRadNumericTextBox");
            TextBox ObservacionesTextBox = (TextBox)ProveedorFV.FindControl("ObservacionesTextBox");
            RadComboBox EstadoRadComboBox = (RadComboBox)ProveedorFV.FindControl("EstadoRadComboBox");
            NombresTextBox.Text = list[0].Nombre;
            ApellidosTextBox.Text = list[0].Nombre;
            SedesTextBox.Text = list[0].Sedes;
            ColegioProveedorTextBox.Text = list[0].ColegioMedico;
            DireccionTextBox.Text = list[0].Direccion;
            TelefonoOficinaRadNumericTextBox.Text = list[0].Telefono;
            CelularRadNumericTextBox.Text = list[0].Celular;
            //Es ACTIVO por que la lista solo trae a los Usuarios Activos
            EstadoRadComboBox.SelectedValue = "ACTIVO";
          

            ObservacionesTextBox.Text = list[0].Observacion;
        }
    }
    private void Limpiar()
    {
        TextBox NombresTextBox = (TextBox)ProveedorFV.FindControl("NombresTextBox");
        TextBox ApellidosTextBox = (TextBox)ProveedorFV.FindControl("ApellidosTextBox");
        TextBox SedesTextBox = (TextBox)ProveedorFV.FindControl("SedesTextBox");
        TextBox ColegioProveedorTextBox = (TextBox)ProveedorFV.FindControl("ColegioProveedorTextBox");
        TextBox DireccionTextBox = (TextBox)ProveedorFV.FindControl("DireccionTextBox");
        TextBox TelefonoOficinaRadNumericTextBox = (TextBox)ProveedorFV.FindControl("TelefonoOficinaRadNumericTextBox");
        TextBox CelularRadNumericTextBox = (TextBox)ProveedorFV.FindControl("CelularRadNumericTextBox");
        TextBox ObservacionesTextBox = (TextBox)ProveedorFV.FindControl("ObservacionesTextBox");
        ApellidosTextBox.Text = "";
        NombresTextBox.Text = "";
        SedesTextBox.Text = "";
        ColegioProveedorTextBox.Text = "";
        DireccionTextBox.Text = "";
        TelefonoOficinaRadNumericTextBox.Text = "";
        CelularRadNumericTextBox.Text = "";
        ObservacionesTextBox.Text = "";
    }//
    
    protected void TipoProveedorRadComboBox_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox TipoProveedorRadComboBox = (RadComboBox)ProveedorFV.FindControl("TipoProveedorRadComboBox");
        if (TipoProveedorRadComboBox.SelectedValue.Contains("MEDICO"))
        {
            DeshabilitarOpcionesMedicos();
            RadComboBox EspecialidadRadComboBox = (RadComboBox)ProveedorFV.FindControl("EspecialidadRadComboBox");
            DropDownList RadComboBoxMedico = (DropDownList)ProveedorFV.FindControl("RadComboBoxMedico");
            EspecialidadRadComboBox.Enabled = true;
            RadComboBoxMedico.Enabled = true;
            TipoProveedorRadComboBox.Enabled = true;

        }
        else
        { 
            HabilitarOpciones();
        }
        Limpiar();
    }
    private void CargarDatosUsuarios()
    {
        RadComboBox UsuarioRadComboBox = (RadComboBox)ProveedorFV.FindControl("UsuarioRadComboBox");

        if (UsuarioRadComboBox.SelectedValue.Length > 0)
        {
            int Usuarioid = int.Parse(UsuarioRadComboBox.SelectedValue);
            User list = UserBLL.GetUserById(Usuarioid);
            // TextBox NombresTextBox = (TextBox)ProveedorFV.FindControl("NombresTextBox");
            TextBox DireccionTextBox = (TextBox)ProveedorFV.FindControl("DireccionTextBox");
            TextBox TelefonoOficinaRadNumericTextBox = (TextBox)ProveedorFV.FindControl("TelefonoOficinaRadNumericTextBox");
            TextBox CelularRadNumericTextBox = (TextBox)ProveedorFV.FindControl("CelularRadNumericTextBox");
            // NombresTextBox.Text = list.Nombre;
            DireccionTextBox.Text = list.Address;
            TelefonoOficinaRadNumericTextBox.Text = list.PhoneNumber;
            CelularRadNumericTextBox.Text = list.CellPhone;
        }
    }
    private void DeshabilitarOpcionesMedicos()
    {
        RadComboBox TipoProveedorRadComboBox = (RadComboBox)ProveedorFV.FindControl("TipoProveedorRadComboBox");
        if (TipoProveedorRadComboBox.SelectedValue.Contains("MEDICO"))
        {
            RadComboBox EspecialidadRadComboBox = (RadComboBox)ProveedorFV.FindControl("EspecialidadRadComboBox");
            RadComboBox EstadoRadComboBox = (RadComboBox)ProveedorFV.FindControl("EstadoRadComboBox");
            TextBox NombresTextBox = (TextBox)ProveedorFV.FindControl("NombresTextBox");
            TextBox ApellidosTextBox = (TextBox)ProveedorFV.FindControl("ApellidosTextBox");
            TextBox SedesTextBox = (TextBox)ProveedorFV.FindControl("SedesTextBox");
            TextBox ColegioProveedorTextBox = (TextBox)ProveedorFV.FindControl("ColegioProveedorTextBox");
            TextBox DireccionTextBox = (TextBox)ProveedorFV.FindControl("DireccionTextBox");
            TextBox TelefonoOficinaRadNumericTextBox = (TextBox)ProveedorFV.FindControl("TelefonoOficinaRadNumericTextBox");
            TextBox CelularRadNumericTextBox = (TextBox)ProveedorFV.FindControl("CelularRadNumericTextBox");
            TextBox ObservacionesTextBox = (TextBox)ProveedorFV.FindControl("ObservacionesTextBox");
            DropDownList RadComboBoxMedico = (DropDownList)ProveedorFV.FindControl("RadComboBoxMedico");
            ApellidosTextBox.Enabled = false;
            NombresTextBox.Enabled = false;
            SedesTextBox.Enabled = false;
            ColegioProveedorTextBox.Enabled = false;
            DireccionTextBox.Enabled = false;
            TelefonoOficinaRadNumericTextBox.Enabled = false;
            CelularRadNumericTextBox.Enabled = false;
            TipoProveedorRadComboBox.Enabled = false;
            EspecialidadRadComboBox.Enabled = false;
            RadComboBoxMedico.Enabled = false;
            EstadoRadComboBox.Enabled = false;
            ObservacionesTextBox.Enabled = false;
        }
        else
        {

        }
    }
    private void HabilitarOpciones()
    {

        TextBox DireccionTextBox = (TextBox)ProveedorFV.FindControl("DireccionTextBox");
        TextBox TelefonoOficinaRadNumericTextBox = (TextBox)ProveedorFV.FindControl("TelefonoOficinaRadNumericTextBox");
        TextBox CelularRadNumericTextBox = (TextBox)ProveedorFV.FindControl("CelularRadNumericTextBox");
        TextBox ObservacionesTextBox = (TextBox)ProveedorFV.FindControl("ObservacionesTextBox");
        RadComboBox EstadoRadComboBox = (RadComboBox)ProveedorFV.FindControl("EstadoRadComboBox");
        DireccionTextBox.Enabled = true;
        TelefonoOficinaRadNumericTextBox.Enabled = true;
        CelularRadNumericTextBox.Enabled = true;
        ObservacionesTextBox.Enabled = true;
        EstadoRadComboBox.Enabled = true;
     
    }
    protected void UsuarioRadComboBox_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
       //  CargarDatosUsuarios();
    }

   
}
