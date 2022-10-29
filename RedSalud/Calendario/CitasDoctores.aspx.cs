using Artexacta.App.LoginSecurity;
using Artexacta.App.Medico;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Utilities.SystemMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Threading;
using System.Globalization;

public partial class Calendario_CitasDoctores : System.Web.UI.Page
{
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
    }

    protected void RadComboBox1_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int medicoId = 0;
        int userId = 0;
        Medico obj = null;
        try
        {
            medicoId = Convert.ToInt32(MedicoRadComboBox.SelectedValue);
            obj = MedicoBLL.getMedicoByMedicoId(medicoId);
            userId = obj.UserId;
        }
        catch (Exception)
        {
            SystemMessages.DisplaySystemErrorMessage("Seleccione un médico o recargue la página");
            return;
        }
        CalendarioDoctor.MedicoId = medicoId;
        CalendarioDoctor.UserId = userId;
        CalendarioDoctor.Visible = true;

        CalendarioDoctorLabel.Text = "Calendario Dr. " + obj.NombreForDisplay;
        CalendarioDoctorLabel.Visible = true;
    }
}