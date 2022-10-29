using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_AdvancedSearch_SOATAdvancedSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SC_NombreAuditorTextSearchItem.Visible = Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("SOAT_ALLDPTOS");
    }
}