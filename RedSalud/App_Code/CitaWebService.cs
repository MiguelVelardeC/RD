using Artexacta.App.Cita;
using Artexacta.App.Cita.BLL;
using Artexacta.App.Medico;
using Artexacta.App.Medico.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;

/// <summary>
/// Summary description for CitaWebService
/// </summary>
[WebService(Namespace = "http://sistema.red.salud.com.bo/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class CitaWebService : System.Web.Services.WebService {

    public CitaWebService () {
        ;
    }

    [WebMethod]
    public IEnumerable<AppointmentData> GetAppointments(RedSaludSchedulerInfo schedulerInfo)
    {
        List<AppointmentData> result = new List<AppointmentData>();

        List<Cita> citas = CitaBLL.GetCitaByMedicoProveedorId(schedulerInfo.MedicoId, schedulerInfo.ProveedorId,
            schedulerInfo.ViewStart.AddMilliseconds(schedulerInfo.TimeZoneOffset), 
            schedulerInfo.ViewEnd.AddMilliseconds(schedulerInfo.TimeZoneOffset));

        foreach (Cita objCita in citas)
        {
            AppointmentData objData = new AppointmentData();
            objData.Description = objCita.Description;
            objData.End = objCita.EndTime;
            objData.ID = objCita.CitaId;
            objData.Start = objCita.StartTime;
            objData.Subject = objCita.Subject;
            objData.TimeZoneID = ConfigurationManager.AppSettings["TimeZoneRDSA"].ToString();// "SA Western Standard Time";

            result.Add(objData);
        }

        return result;
    }

    [WebMethod]
    public IEnumerable<AppointmentData> InsertAppointment(RedSaludSchedulerInfo schedulerInfo, AppointmentData appointmentData)
    {
        Cita newCita = new Cita();
        newCita.Description = appointmentData.Description;
        newCita.EndTime = appointmentData.End.AddMilliseconds(schedulerInfo.TimeZoneOffset);
        newCita.MedicoId = schedulerInfo.MedicoId;
        newCita.ProveedorId = schedulerInfo.ProveedorId;
        newCita.StartTime = appointmentData.Start.AddMilliseconds(schedulerInfo.TimeZoneOffset);
        newCita.Subject = appointmentData.Subject;

        CitaBLL.InsertCita(newCita);

        return GetAppointments(schedulerInfo);
    }

    [WebMethod]
    public IEnumerable<AppointmentData> UpdateAppointment(RedSaludSchedulerInfo schedulerInfo, AppointmentData appointmentData)
    {
        Cita newCita = CitaBLL.GetCitaByCitaId(Convert.ToInt32(appointmentData.ID));
        newCita.Description = appointmentData.Description;
        newCita.EndTime = appointmentData.End.AddMilliseconds(schedulerInfo.TimeZoneOffset);
        newCita.StartTime = appointmentData.Start.AddMilliseconds(schedulerInfo.TimeZoneOffset);
        newCita.Subject = appointmentData.Subject;

        CitaBLL.UpdateCita(newCita);

        return GetAppointments(schedulerInfo);
    }

    [WebMethod]
    public IEnumerable<AppointmentData> CreateRecurrenceException(RedSaludSchedulerInfo schedulerInfo, AppointmentData recurrenceExceptionData)
    {
        return new List<AppointmentData>();
    }

    [WebMethod]
    public IEnumerable<AppointmentData> RemoveRecurrenceExceptions(RedSaludSchedulerInfo schedulerInfo, AppointmentData masterAppointmentData)
    {
        return new List<AppointmentData>();
    }

    [WebMethod]
    public IEnumerable<AppointmentData> DeleteAppointment(RedSaludSchedulerInfo schedulerInfo, AppointmentData appointmentData, 
        bool deleteSeries)
    {
        Cita newCita = CitaBLL.GetCitaByCitaId(Convert.ToInt32(appointmentData.ID));

        if (newCita != null)
            CitaBLL.DeleteCita(newCita.CitaId);

        return GetAppointments(schedulerInfo);
    }

    [WebMethod]
    public IEnumerable<ResourceData> GetResources(RedSaludSchedulerInfo schedulerInfo)
    {
        List<ResourceData> result = new List<ResourceData>();
        ResourceData theResource = new ResourceData();
        theResource.Type = "UserId";
        theResource.Key = 1;
        theResource.Text = "User";
        result.Add(theResource);
        return result;
    }
    
}
