using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserControls_Cita_CalendarioCita : System.Web.UI.UserControl
{
    public int MedicoId
    {
        get
        {
            try
            {
                return Convert.ToInt32(MedicoIdHiddenField.Value);
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            MedicoIdHiddenField.Value = value.ToString();
        }
    }

    public int UserId
    {
        get
        {
            try
            {
                return Convert.ToInt32(UserIdHiddenField.Value);
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            UserIdHiddenField.Value = value.ToString();
        }
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        RadScheduler1.Provider = new Artexacta.App.Cita.RedSaludSchedulerProvider();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadScheduler1.ResourceTypes.Add(new ResourceType("UserId", false));
            RadScheduler1.SelectedDate = DateTime.UtcNow;
            //TimeZoneLabel.Text = RadScheduler1.TimeZoneID;
            RadScheduler1.TimeZoneID = ConfigurationManager.AppSettings["TimeZoneRDSA"].ToString();
            RadScheduler1.AdvancedForm.EnableResourceEditing = false;
            RadScheduler1.AdvancedForm.EnableTimeZonesEditing = false;
            CultureInfo newCulture = CultureInfo.CreateSpecificCulture("es-BO");
            RadScheduler1.Culture = newCulture;
        }
    }
}