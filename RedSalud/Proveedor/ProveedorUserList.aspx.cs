using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.ProveedorUser.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.User.BLL;
using Artexacta.App.LoginSecurity;

public partial class Proveedor_ProveedorUserList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (LoginSecurity.IsUserAuthorizedPermission("MANAGE_PROVEEDOR"))
            {
                this.CiudadHF.Value = "TTT";
            }
            else
            {
                this.CiudadHF.Value = UserBLL.GetCuidadIdByUsername(HttpContext.Current.User.Identity.Name);
            }
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
                this.ProveedorHospitalDDL.SelectedValue=ProveedorId.ToString();
                this.ProveedorUserRadGrid.Visible = true;
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session ProveedorId:" + Session["ProveedorId"]);
            }
        }
        Session["ProveedorId"] = null;
    }

    protected void ProveedorUserRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                int ProveedorId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProveedorId"];
                int UserId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Userid"];

                if (ProveedorUserBLL.BoolDeletProveedorUser(ProveedorId, UserId))
                {
                    if (ProveedorUserBLL.DeleteProveedorUSer(ProveedorId, UserId))
                        SystemMessages.DisplaySystemMessage("Se elimino el usuario del proveedor Correctamente.");
                    else
                        SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el usuario del proveedor.");
                }
                else
                    SystemMessages.DisplaySystemWarningMessage("No se puede eliminar el Usuario del proveedor,por que ya tiene un caso medico registrado.");
            }
            catch (Exception)
            {
                SystemMessages.DisplaySystemErrorMessage("Error no se pudo obtener los datos de la fila seleccionada a eliminar.");
                log.Error("Error en la funcion ProveedorUserRadGrid_ItemCommand de la pagina ProveedorUserList.aspx");
            }            
        }
    }
    
    protected void SearchLB_Click(object sender, EventArgs e)
    {
        this.ProveedorUserRadGrid.Visible = true;
        this.ProveedorUserRadGrid.DataBind();
    }
    
    protected void ProveedorHospitalODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Proveedores.");
            log.Error("Function ProveedorHospitalODS_Selected on page ProveedorUserList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        else
            if(!IsPostBack)
                ProcessSessionParameters();
    }
    
    protected void ProveedorUserODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Medicos de Emergencia de proveedor.");
            log.Error("Function ProveedorUserODS_Selected on page ProveedorUserList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}