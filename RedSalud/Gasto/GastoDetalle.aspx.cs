using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Caso;
using Artexacta.App.Caso.BLL;
using Artexacta.App.Especialidad;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Estudio.BLL;
using Artexacta.App.Gasto.BLL;
using Artexacta.App.GastoDetalle.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.GastoDetalle;
using Artexacta.App.LoginSecurity;

public partial class Gastos_GastoDetalle : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private string isExport = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.FechaGastoExC.MaxDate=DateTime.Now;
            ProcessSessionParameters();
            GetDetailsCaso();
        }
        FileManager.OnListFileChange += new UserControls_FileManager.OnListFileChangeDelegate(FileManager_FileSave);
        this.AdminCasoLB.Visible = LoginSecurity.IsUserAuthorizedPermission("MANAGE_CASOS");
    }

    public void FileManager_FileSave ( string ObjectName, string type )
    {
        switch (ObjectName)
        {
            case "RECETAS":
                RecetaRadGrid.DataBind();
                break;
            case "ESTUDIO":
                EstudioRadGrid.DataBind();
                break;
            case "DERIVACIONES":
                DerivacionRadGrid.DataBind();
                break;
            case "INTERNACION":
                InternacionRadGrid.DataBind();
                break;
            case "MEDICAMENTO":
                MedicamentoRadGrid.DataBind();
                break;
        }
    }

    protected void ProcessSessionParameters()
    {
        int CasoId = 0;
        if (Session["CasoId"] != null && !string.IsNullOrEmpty(Session["CasoId"].ToString()))
        {
            try
            {
                CasoId = Convert.ToInt32(Session["CasoId"]);

                GastoMP.SelectedIndex = 2;
                CasoTab.SelectedIndex = 2;
                CasoTab.Tabs.FindTabByText("Odontología").Visible = false;
                OdontologiaRPV.Visible = false;
                CasoTab.Tabs.FindTabByText("Emergencia").Visible = false;
                EmergenciaRPV.Visible = false;
                CasoTab.Tabs.FindTabByText("Enfermeria").Visible = false;
                MedicamentoRPV.Visible = false;

                if (CasoBLL.GetMotivoConsultaId(CasoId) == "ENFER")
                {
                    CasoTab.Tabs.FindTabByText("Enfermeria").Visible = true;
                    MedicamentoRPV.Visible = true;
                    GastoMP.SelectedIndex = 6;
                    CasoTab.SelectedIndex = 6;
                }
                else if (CasoBLL.GetMotivoConsultaId(CasoId) == "EMERG")
                {
                    CasoTab.Tabs.FindTabByText("Emergencia").Visible = true;
                    EmergenciaRPV.Visible = true;
                    GastoMP.SelectedIndex = 1;
                    CasoTab.SelectedIndex = 1;
                }
                else if (CasoBLL.GetMotivoConsultaId(CasoId) == "ODONTO")
                {
                    CasoTab.Tabs.FindTabByText("Odontología").Visible = true;
                    OdontologiaRPV.Visible = true;
                    GastoMP.SelectedIndex = 0;
                    CasoTab.SelectedIndex = 0;
                }

                if (CasoBLL.GetMotivoConsultaId(CasoId) != "ENFER")
                {
                    CasoTab.Tabs.FindTabByText("Receta Medica").Visible = true;
                    RecetaRPV.Visible = true;
                    CasoTab.Tabs.FindTabByText("Exámenes Complementarios").Visible = true;
                    EstudioRPV.Visible = true;
                    CasoTab.Tabs.FindTabByText("Especialista").Visible = true;
                    DerivacionRPV.Visible = true;
                    CasoTab.Tabs.FindTabByText("Internación").Visible = true;
                    InternacionRPV.Visible = true;
                }
                else
                {
                    CasoTab.Tabs.FindTabByText("Receta Medica").Visible = false;
                    RecetaRPV.Visible = false;
                    CasoTab.Tabs.FindTabByText("Exámenes Complementarios").Visible = false;
                    EstudioRPV.Visible = false;
                    CasoTab.Tabs.FindTabByText("Especialista").Visible = false;
                    DerivacionRPV.Visible = false;
                    CasoTab.Tabs.FindTabByText("Internación").Visible = false;
                    InternacionRPV.Visible = false;
                }
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session CasoId:" + Session["CasoId"]);
            }
        }
        CasoIdHF.Value = CasoId.ToString();
        Session["CasoId"] = null;

        if (Session["RETURNTO"] != null && !string.IsNullOrEmpty(Session["RETURNTO"].ToString()))
        {
            returnHL.NavigateUrl = Session["RETURNTO"].ToString();
            Session["RETURNTO"] = returnHL.NavigateUrl;
        }
    }

    protected void GetDetailsCaso()
    {
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            Caso objCaso = CasoBLL.GetCasoBasicByCasoId(CasoId);

            if (objCaso!= null)
            {
                this.CodigoCasoLabel.Text = objCaso.CodigoCaso;
                this.AseguradoraLabel.Text = objCaso.NombreAseguradora;
                this.CodigoAseguradoLabel.Text = objCaso.CodigoAsegurado;
                this.AseguradoLabel.Text = objCaso.NombrePaciente;
                this.MotivoConsultaLabel.Text = objCaso.MotivoConsulta;
                this.fechaLabel.Text = objCaso.FechaCreacion.ToString();

                IsGastoBlocked(objCaso.IsGastoBlocked);
            }
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos del Caso.");
            log.Error("Function GetDetailsCaso on page GastoDetalle.aspx", ex);
        }
        
    }

    public void IsGastoBlocked ( bool isBlocked )
    {
        if (LoginSecurity.IsUserAuthorizedPermission("BLOCK_UNLOCK_GASTOS"))
        {
            BlockUnlockLB.Visible = true;
            BlockUnlockLabel.Visible = false;
            BlockUnlockLB.Text = isBlocked ? "Desbloquear" : "Bloquear";
            BlockUnlockLB.OnClientClick = isBlocked ? "return confirm('Se podrán añadir o eliminar los gastos ¿Esta seguro?');" : "return confirm('No se podrán añadir ni eliminar los gastos ¿Esta seguro?');";
        }
        else
        {
            BlockUnlockLB.Visible = false;
            BlockUnlockLabel.Visible = isBlocked;
        }
        
        RecetaRadGrid.Columns[1].Visible = !isBlocked;
        RecetaRadGrid.MasterTableView.DetailTables[0].Columns[0].Visible = !isBlocked;
        RecetaRadGrid.DataBind();
        EstudioRadGrid.Columns[1].Visible = !isBlocked;
        EstudioRadGrid.MasterTableView.DetailTables[0].Columns[0].Visible = !isBlocked;
        EstudioRadGrid.DataBind();
        DerivacionRadGrid.Columns[1].Visible = !isBlocked;
        DerivacionRadGrid.MasterTableView.DetailTables[0].Columns[0].Visible = !isBlocked;
        DerivacionRadGrid.DataBind();
        InternacionRadGrid.Columns[1].Visible = !isBlocked;
        InternacionRadGrid.MasterTableView.DetailTables[0].Columns[0].Visible = !isBlocked;
        InternacionRadGrid.DataBind();
        EmergenciaRadGrid.Columns[1].Visible = !isBlocked;
        EmergenciaRadGrid.MasterTableView.DetailTables[0].Columns[0].Visible = !isBlocked;
        EmergenciaRadGrid.DataBind();
    }

    #region Receta

    protected void RecetaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Recetas.");
            log.Error("Function RecetaODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void RecetaRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleReceta")
                try
                {
                    GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                    if (ObjGastoDetalle.ConsolidacionId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Enabled = false;
                            deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Receta Medica.");
                    log.Error("Function RecetaRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
                }
        }
    }

    protected void RecetaRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleReceta")
            {
                try
                {
                    int DetalleId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleId").ToString());

                    if (GastoDetalleBLL.DeleteGastoDetalle(DetalleId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo eliminar el detalle del gasto.");

                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Receta");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function RecetaRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }
        }
    }
    protected void RecetaRadGrid_ExportToPdfButton_Click ( object sender, EventArgs e )
    {
        ExportGridToPDF(RecetaRadGrid, "Gastos_Receta");
    }
    protected void RecetaRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    //derivacion es especalista
    #region Derivacion
    protected void DerivacionODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de derivacion a especialista.");
            log.Error("Function DerivacionODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void DerivacionRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleDerivacion")
                try
                {
                    GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                    if (ObjGastoDetalle.ConsolidacionId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Enabled = false;
                            deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Derivación a especialista.");
                    log.Error("Function DerivacionRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
                }
        }
    }
    protected void DerivacionRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleDerivacion")
            {
                try
                {
                    int DetalleId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleId").ToString());

                    if (GastoDetalleBLL.DeleteGastoDetalle(DetalleId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo eliminar el detalle del gasto.");

                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Derivacion");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function DerivacionRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }
        }
    }
    protected void DerivacionRadGrid_ExportToPdfButton_Click ( object sender, EventArgs e )
    {
        ExportGridToPDF(DerivacionRadGrid, "Gastos_Derivacion_Especialista");
    }
    protected void DerivacionRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region Emergencia
    protected void EmergenciaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Emergencia.");
            log.Error("Function EmergenciaODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void EmergenciaRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleEmergencia")
                try
                {
                    GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                    if (ObjGastoDetalle.ConsolidacionId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Enabled = false;
                            deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Emergencia.");
                    log.Error("Function EmergenciaRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
                }
        }
    }

    protected void EmergenciaRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleEmergencia")
            {
                try
                {
                    int DetalleId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleId").ToString());
     
                    if (GastoDetalleBLL.DeleteGastoDetalle(DetalleId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo eliminar el detalle del gasto.");

                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Emergencia");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function EmergenciaRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }
        }
    }
    protected void EmergenciaRadGrid_ExportToPdfButton_Click ( object sender, EventArgs e )
    {
        ExportGridToPDF(EmergenciaRadGrid, "Gastos_Emergencias");
    }
    protected void EmergenciaRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region Estudio

    protected void EstudioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Examen Complementarios.");
            log.Error("Function EstudioODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void EstudioRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleEstudio")
            try
            {
                GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                if (ObjGastoDetalle.ConsolidacionId > 0)
                {
                    ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                    if (deleteButton != null)
                    {
                        deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                        deleteButton.Enabled = false;
                        deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                    }
                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Examenes complementarios.");
                log.Error("Function EstudioRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
            }
        }
    }

    protected void EstudioRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleEstudio")
            {
                try
                {
                    int DetalleId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleId").ToString());

                    if (GastoDetalleBLL.DeleteGastoDetalle(DetalleId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo eliminar el detalle del gasto.");

                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Estudio");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function EstudioRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }

            //else if (item.OwnerTableView.Name == "Parent")
            //{
            //    Response.Write("Parent Delete");
            //    string parentval = item.GetDataKeyValue("OrderID").ToString();//Access the parent datakey value
            //    //Code to delete
            //}
        }
        if (e.CommandName == "ExpandCollapse")
        {
            //GridDataItem item = (GridDataItem)e.Item;
            //string GastoIdExpandir = item.GetDataKeyValue("GastoId").ToString();
            //Expandir(GastoIdExpandir);
        }
    }
    protected void EstudioRadGrid_ExportToPdfButton_Click ( object sender, EventArgs e )
    {
        ExportGridToPDF(EstudioRadGrid, "Gastos_Examenes_Complementarios");
    }
    protected void EstudioRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }

    #endregion

    #region Internacion
    protected void InternacionODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Internación.");
            log.Error("Function InternacionODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void InternacionRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleInternacion")
                try
                {
                    GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                    if (ObjGastoDetalle.ConsolidacionId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Enabled = false;
                            deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Internación.");
                    log.Error("Function InternacionRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
                }
        }
    }

    protected void InternacionRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleInternacion")
            {
                try
                {
                    int DetalleId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleId").ToString());
   
                    if (GastoDetalleBLL.DeleteGastoDetalle(DetalleId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo eliminar el detalle del gasto.");

                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Internacion");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function InternacionRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }
        }
    }
    protected void InternacionRadGrid_ExportToPdfButton_Click ( object sender, EventArgs e )
    {
        ExportGridToPDF(InternacionRadGrid, "Gastos_Internacion");
    }
    protected void InternacionRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    #region Medicamento

    protected void MedicamentoODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Medicamentos.");
            log.Error("Function MedicamentoODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void MedicamentoRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleMedicamento")
                try
                {
                    GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                    if (ObjGastoDetalle.ConsolidacionId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Enabled = false;
                            deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener algún control en Medicamento de Enfermería.");
                    log.Error("Function MedicamentoRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
                }
        }
    }

    protected void MedicamentoRadGrid_ItemCommand ( object sender, Telerik.Web.UI.GridCommandEventArgs e )
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleMedicamento")
            {
                try
                {
                    int MedicamentoId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleID").ToString());

                    if (GastoDetalleBLL.DeleteGastoDetalle(MedicamentoId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el detalle del gasto.");

                    MedicamentoRadGrid.DataBind();
                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Medicamento");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function MedicamentoRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }
        }
    }
    protected void MedicamentoRadGrid_ExportToPdfButton_Click ( object sender, EventArgs e )
    {
        ExportGridToPDF(MedicamentoRadGrid, "Gastos_MedicamentoEnfermeria");
    }
    protected void MedicamentoRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    #endregion

    protected void GastoDetalleODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de detalle del gasto.");
            log.Error("Function GastoDetalleODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SaveGastoExC_Click(object sender, EventArgs e)
    {
        try
        {
            int GastoId = Convert.ToInt32(GastoIdHF.Value);
            decimal Monto = 0;
            int NroFacturaRecibo = 0;

            DateTime FechaGasto = FechaGastoExC.SelectedDate.Value;

            Monto = Convert.ToDecimal(this.MontoRN.Value);
            NroFacturaRecibo = Convert.ToInt32(NroFacturaReciboExCTxt.Text);
            string TipoDoc = TipoDocumentoExCDDL.SelectedValue;
            //RetencionImpuestos expresado en %(13%=13.0)
            decimal RetencionImpuesto = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("RetencionImpuestos"));

            if (GastoId <= 0)
            {//insert Gasto (derivacion, emergencia, estudio e internacion)
                int TipoId = Convert.ToInt32(TipoIdHF.Value);
                string TipoNombre = TipoNombreHF.Value;
                switch (TipoNombre)
                {
                    case "Odontologia":
                        GastoId = GastoBLL.InsertNewGastoOdontologia(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        break;
                    case "Receta":
                        GastoId = GastoBLL.InsertNewGastoReceta(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        break;
                    case "Derivacion":
                        GastoId = GastoBLL.InsertNewGastoDerivacion(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        break;

                    case "Emergencia":
                        GastoId = GastoBLL.InsertNewGastoEmergencia(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        break;
                    case "Estudio":
                        GastoId = GastoBLL.InsertNewGastoEstudio(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        break;
                    case "Internacion":
                        GastoId = GastoBLL.InsertNewGastoInternacion(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        break;
                    case "Medicamento":
                        GastoId = GastoBLL.InsertNewGastoMedicamento(DateTime.Now, FechaGasto, Monto, NroFacturaRecibo
                            , TipoDoc, 0, RetencionImpuesto, TipoId);
                        MedicamentoRadGrid.DataBind();
                        break;
                    default:
                        SystemMessages.DisplaySystemErrorMessage("No se pudo Insertar el nuevo Detalle de gasto por que falta un dato.");
                        break;
                }

                if (GastoId < 0)
                    SystemMessages.DisplaySystemErrorMessage("no se pudo Insertar el nuevo Detalle de gasto.");
                else if(GastoId==0)
                    SystemMessages.DisplaySystemErrorMessage("Se inserto correctamente el nuevo Detalle de gasto pero retorno valor cero.");
                else
                    SystemMessages.DisplaySystemMessage("Detalle de gasto insertado correctamente.");
            }
            else
            {//insert gasto Detalle (derivacion, emergencia, estudio e internacion)
                int GastoDetalleId = GastoBLL.InsertGastoDetalle(GastoId, DateTime.Now, FechaGasto
                    , Monto, NroFacturaRecibo, TipoDoc, 0, RetencionImpuesto);
                if (GastoDetalleId <= 0)
                    SystemMessages.DisplaySystemErrorMessage("no se pudo Insertar el nuevo Detalle de gasto.");
                else
                    SystemMessages.DisplaySystemMessage("Detalle de gasto insertado correctamente.");
            }
            
            //que se muestre lo q se inserto
            ExpandRowRadGridByTipoNombre(GastoId, TipoNombreHF.Value);

            ClearPopup();
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al insertar el detalle de gasto.");
            log.Error("Function SaveGastoExC_Click inerting on page GastoDetalle.aspx", ex);
        }
    }

    protected void ExpandRowRadGridByTipoNombre(int GastoIdExpandir, string TipoNombre)
    {
        switch (TipoNombre)
        {
            case "Odontologia":
                OdontologiaRadGrid.DataBind();
                ExpandRowRadGrid(GastoIdExpandir, OdontologiaRadGrid.MasterTableView);
                break;
            case "Receta":
                RecetaRadGrid.DataBind();
                ExpandRowRadGrid(GastoIdExpandir, RecetaRadGrid.MasterTableView);
                break;
            case "Derivacion":
                DerivacionRadGrid.DataBind();
                ExpandRowRadGrid(GastoIdExpandir, DerivacionRadGrid.MasterTableView);
                break;
            case "Emergencia":
                EmergenciaRadGrid.DataBind();
                ExpandRowRadGrid(GastoIdExpandir, EmergenciaRadGrid.MasterTableView);
                break;
            case "Estudio":
                EstudioRadGrid.DataBind();
                ExpandRowRadGrid(GastoIdExpandir, EstudioRadGrid.MasterTableView);
                break;
            case "Internacion":
                InternacionRadGrid.DataBind();
                ExpandRowRadGrid(GastoIdExpandir, InternacionRadGrid.MasterTableView);
                break;
            default:
                break;
        }
    }
    
    protected void ExpandRowRadGrid(int GastoIdExpandir, GridTableView tableView)
    {
        foreach (GridDataItem item in tableView.Items)
        {
            int GastoId = Convert.ToInt32(item.GetDataKeyValue("GastoId").ToString());
            if (GastoId == GastoIdExpandir)
                if (!item.Expanded)
                    item.Expanded = true;
        }
    }

    protected void ClearPopup()
    {
        this.TipoNombreHF.Value = "";
        this.TipoIdHF.Value = "0";
        this.GastoIdHF.Value = "0";
        this.FechaGastoExC.Clear();
        //this.MontoExCTxt.Text = "";
        this.MontoRN.Text = "";
        this.NroFacturaReciboExCTxt.Text = "";
        this.TipoDocumentoExCDDL.ClearSelection();
    }

    private void createRecetaHeaderforPDFExport ( RadGrid grid )
    {
        GridItem ghItem = grid.MasterTableView.GetItems(GridItemType.CommandItem)[0];
        (ghItem.FindControl("ExportPanel") as Panel).Visible = false;
        (ghItem.FindControl("ExportPanel") as Panel).Attributes.CssStyle.Clear();

        if (string.IsNullOrWhiteSpace(MedicoNameHF.Value))
        {
            Artexacta.App.User.User objUser = UserBLL.GetUserByUsername(HttpContext.Current.User.Identity.Name);
            MedicoNameHF.Value = objUser.FullName;
            Especialidad especialidad = EspecialidadBLL.GetEspecialidadByUserId(objUser.UserId);
            EspecialidadHF.Value = especialidad.Nombre;
        }
        (ghItem.FindControl("MedicoNombreLabel") as Label).Text = MedicoNameHF.Value;
        (ghItem.FindControl("EspecialidadNameLabel") as Label).Text = EspecialidadHF.Value;

        (ghItem.FindControl("HeaderPanel") as Panel).Attributes.CssStyle.Clear();
    }

    private void ExportGridToPDF ( RadGrid grid, string fileName )
    {
        try
        {
            grid.ExportSettings.FileName = fileName + "_" + DateTime.Now.ToString("dd-MM-yyyy");

            grid.Rebind();
            grid.MasterTableView.GetColumn("CheckBox").Display = false;
            grid.MasterTableView.GetColumn("AgregarGastos").Display = false;
            grid.MasterTableView.DetailTables[0].GetColumn("DeleteCommandColumn").Visible = false;
            grid.MasterTableView.DetailTables[0].GetColumn("DeleteCommandColumn").Display = false;
            grid.MasterTableView.ExpandCollapseColumn.Display = false;
            grid.MasterTableView.HierarchyDefaultExpanded = true;
            grid.GridLines = GridLines.Both;
            grid.MasterTableView.BorderWidth = 0;
            grid.MasterTableView.DetailTables[0].BorderWidth = 0;
            grid.MasterTableView.DetailTables[0].Attributes.CssStyle.Add("border", "none");

            isExport = grid.ID;
            grid.MasterTableView.ExportToPdf();
        }
        catch (Exception q)
        {
            log.Error("Error al exportar a PDF", q);
            SystemMessages.DisplaySystemErrorMessage("Error al Exportar a PDF");
        }
    }

    private void RadGrid_ItemCreated ( RadGrid sender, GridItem Item )
    {
        if (Item.OwnerTableView.Name != sender.MasterTableView.Name)
        {
            if (Item is GridNoRecordsItem)
            {
                Item.OwnerTableView.ParentItem.BorderStyle = BorderStyle.None;
                Item.OwnerTableView.ParentItem.Expanded = false;
            }
            return;
        }
        if (isExport == sender.ID)
        {
            if (Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)Item;
                string id1 = "GastoId";
                string id2 = "";
                switch (sender.ID)
                {
                    case "OdontologiaRadGrid": id2 = "OdontologiaId"; break;
                    case "RecetaRadGrid": id2 = "DetalleId"; break;
                    case "EstudioRadGrid": id2 = "EstudioId"; break;
                    case "DerivacionRadGrid": id2 = "DerivacionId"; break;
                    case "EmergenciaRadGrid": id2 = "EmergenciaId"; break;
                    case "MedicamentoRadGrid": id2 = "MedicamentoId"; break;
                }
                id1 = item.GetDataKeyValue(id1) == null ? "0" : item.GetDataKeyValue(id1).ToString();
                id2 = item.GetDataKeyValue(id2) == null ? "0" : item.GetDataKeyValue(id2).ToString();

                List<string> ids = new List<string>();
                ids.AddRange(ExportIDHF.Value.Split(new char[] { ',' }));
                if (!ids.Contains("{" + id1 + ";" + id2 + "}"))
                {
                    Item.Display = false;
                }
            }
            else if (Item.ItemType == GridItemType.CommandItem)
            {
                createRecetaHeaderforPDFExport(sender);
                Item.PrepareItemStyle();
            }
            else if (Item.ItemType == GridItemType.Footer)
            {
                isExport = "";
                ExportIDHF.Value = "";
            }
        }
    }

    protected void AdminCasoLB_Click(object sender, EventArgs e)
    {
        int CasoId = 0;
        string MotivoConsulta = "";
        try
        {
            CasoId = Convert.ToInt32(this.CasoIdHF.Value);
            Session["CasoId"] = CasoId.ToString();
            MotivoConsulta = CasoBLL.GetMotivoConsultaId(CasoId);
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Casos Medico.");
            log.Error("Function AdminCaso_Click on page GastoDetalle.aspx, error convert CasoId.", ex);
            return;
        }
        switch (MotivoConsulta)
        {
            //case "EMERG":
            //    Response.Redirect("~/CasoMedico/Emergencia.aspx");
            //    return;
            case "ENFER":
                Response.Redirect("~/CasoMedico/Enfermeria.aspx");
                return;
            default:
                Response.Redirect("~/CasoMedico/CasoMedicoDetalle.aspx");
                return;
        }
    }
    protected void FileManager_Command ( object sender, CommandEventArgs e )
    {
        FileManager.OpenFileManager(e.CommandName, Convert.ToInt32(e.CommandArgument), true);
    }
    protected void BlockUnlockLB_Click ( object sender, EventArgs e )
    {
        try
        {
            int CasoId = Convert.ToInt32(CasoIdHF.Value);
            IsGastoBlocked(CasoBLL.BlockUnlock(CasoId, BlockUnlockLB.Text == "Bloquear"));
        }
        catch (Exception q)
        {
            log.Error("Error blocking / unlocking Gasto.", q);
            SystemMessages.DisplaySystemErrorMessage("Error al intentar " + BlockUnlockLB.Text + " los gastos.");
        }
    }
    #region Odontologia
    protected void OdontologiaODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de Odontología.");
            log.Error("Function OdontologiaODS_Selected on page GastoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void OdontologiaRadGrid_ItemCommand ( object sender, GridCommandEventArgs e )
    {
        if (e.CommandName == "Eliminar")
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleOdontologia")
            {
                try
                {
                    int DetalleId = Convert.ToInt32(item.GetDataKeyValue("GastoDetalleId").ToString());

                    if (GastoDetalleBLL.DeleteGastoDetalle(DetalleId))
                        SystemMessages.DisplaySystemMessage("Detalle de gasto eliminado correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("no se pudo eliminar el detalle del gasto.");

                    int GastoIdExpandir = Convert.ToInt32(item.OwnerTableView.ParentItem.GetDataKeyValue("GastoId").ToString());
                    ExpandRowRadGridByTipoNombre(GastoIdExpandir, "Odontologia");
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al eliminar el detalle del gasto.");
                    log.Error("Function OdontologiaRadGrid_ItemCommand deleting on page GastoDetalle.aspx", ex);
                }
            }
        }
    }
    protected void OdontologiaRadGrid_ItemCreated ( object sender, GridItemEventArgs e )
    {
        RadGrid_ItemCreated(sender as RadGrid, e.Item);
    }
    protected void OdontologiaRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "GastoDetalleOdontologia")
                try
                {
                    GastoDetalle ObjGastoDetalle = (GastoDetalle)item.DataItem;

                    if (ObjGastoDetalle.ConsolidacionId > 0)
                    {
                        ImageButton deleteButton = (ImageButton)item["DeleteCommandColumn"].Controls[0];
                        if (deleteButton != null)
                        {
                            deleteButton.ImageUrl = "~/Images/neutral/delete_disabled.gif";
                            deleteButton.Enabled = false;
                            deleteButton.ToolTip = "No se puede eliminar por que el gasto ya fue consolidado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener algun control en Prestaciones Odontologicas.");
                    log.Error("Function RecetaRadGrid_ItemDataBound on page GastoDetalle.aspx", ex);
                }
        }
    }
    protected void ExportToPdfButton_Click ( object sender, ImageClickEventArgs e )
    {
        ExportGridToPDF(OdontologiaRadGrid, "Gastos_Odontologia");
    }
    #endregion
}