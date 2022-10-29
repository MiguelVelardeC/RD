using Artexacta.App.Security.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Artexacta.App.Utilities.Email;

public partial class Security_UserServiceList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        using (Cognos.Negocio.Negocio db = new Cognos.Negocio.Negocio())
        {
            var users = db.tbl_UsuarioServicio.ToList();
            this.UserGridView.DataSource = users;
            this.UserGridView.DataBind();
        }
    }

    protected void UserGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (UserGridView.SelectedDataKey == null)
        {
            return;
        }

        string userName = (string)UserGridView.SelectedDataKey.Value;
        if (string.IsNullOrEmpty(userName))
        {
            return;
        }
    }

    protected void NewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Security/UserService.aspx");
    }

    protected void EditImageButton_Click(object sender, ImageClickEventArgs e)
    {
        //OperationHiddenField.Value = "EDIT";
    }

    protected void DeleteImageButton_Click(object sender, ImageClickEventArgs e)
    {
        //OperationHiddenField.Value = "DELETE";
    }

 }