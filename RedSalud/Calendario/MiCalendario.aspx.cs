using Artexacta.App.LoginSecurity;
using Artexacta.App.Medico.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Threading;
using System.Globalization;

public partial class Calendario_MiCalendario : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected override void InitializeCulture ()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-BO");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
        base.InitializeCulture();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager manager = RadScriptManager.GetCurrent(Page);
        manager.Scripts.Add(new ScriptReference(ResolveUrl("~/Scripts/jquery.tmpl.js")));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!LoginSecurity.IsUserAuthenticated())
        {
            Response.Redirect("~/MainPage.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LeerMedicoDeUsuarioAutenticado();
        }
    }

    private void LeerMedicoDeUsuarioAutenticado()
    {
        string UserName = HttpContext.Current.User.Identity.Name;
        Artexacta.App.User.User objUser = UserBLL.GetUserByUsername(UserName);

        Artexacta.App.Medico.Medico objMedico = null;

        try
        {
            objMedico = MedicoBLL.getMedicoByUserId(objUser.UserId);
            if (objMedico == null)
                throw new Exception("Ususario NO es medico");
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemWarningMessage("El usuario debe ser un médico para poder ver su calendario");
            Response.Redirect("~/MainPage.aspx");
            return;
        } 

        CalendarView.MedicoId = objMedico.MedicoId;
        CalendarView.UserId = objUser.UserId;
    }
}