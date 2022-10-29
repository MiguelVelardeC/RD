using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.LoginSecurity;

public partial class UserControls_AdvancedSearch_PagoSOATAdvancedSearch : System.Web.UI.UserControl
{
    protected void Page_Load ( object sender, EventArgs e )
    {
        bool ADMIN_SOAT_PAGOS = LoginSecurity.IsUserAuthorizedPermission("ADMIN_SOAT_PAGOS");
        SC_TipoGastoSearchItem.Visible = ADMIN_SOAT_PAGOS;
        SC_PacienteSearchItem.Visible = ADMIN_SOAT_PAGOS;
        SC_FechaEmisionBetweenSearchItem.Visible = ADMIN_SOAT_PAGOS;
        SC_UsuarioTextSearchItem.Visible = ADMIN_SOAT_PAGOS;
        SC_PacienteSearchItem.Visible = ADMIN_SOAT_PAGOS;
        SC_NroChequeTextSearchItem.Visible = ADMIN_SOAT_PAGOS;
    }
}