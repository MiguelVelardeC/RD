using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.ProveedorUser.BLL;
using log4net;
using Artexacta.App.User.BLL;
using Artexacta.App.LoginSecurity;

public partial class Proveedor_ProveedorUser : System.Web.UI.Page
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

    protected void ProveedorHospitalODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Proveedores.");
            log.Error("Function ProveedorHospitalODS_Selected on page ProveedorUser.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SaveLB_Click(object sender, EventArgs e)
    {
        bool boolInsert = false;
        int ProveedorId = 0;
        try
        {
            ProveedorId = Convert.ToInt32(ProveedorHospitalDDL.SelectedValue);
            int UserId = Convert.ToInt32(UserDDL.SelectedValue);

            if (ProveedorUserBLL.BoolInsertProveedorUser(ProveedorId, UserId))
            {
                if (ProveedorUserBLL.InsertProveedorUser(ProveedorId, UserId))
                {
                    SystemMessages.DisplaySystemMessage("Se agrego el Usuario al proveedor correctamente.");
                    boolInsert = true;
                }
                else
                    SystemMessages.DisplaySystemErrorMessage("No se pudo agregar el nuevo Usuario al proveedor.");
            }
            else
                SystemMessages.DisplaySystemErrorMessage("El Usuario seleccionado ya pertenece a dicho proveedor.");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al intentar agregar el Usuario al proveedor.");
            log.Error("Function SaveLB_Click on page ProveedorUserList.aspx", ex);
        }
        if (boolInsert)
        {
            Session["ProveedorId"] = ProveedorId.ToString();
            Response.Redirect("~/Proveedor/ProveedorUserList.aspx");
        }
    }
    protected void UserODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Usuarios.");
            log.Error("Function UserODS_Selected on page ProveedorUser.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}