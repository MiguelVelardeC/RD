<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarioCita.ascx.cs" Inherits="UserControls_Cita_CalendarioCita" %>

<%@ Register Src="~/UserControls/Cita/AdvancedForm.ascx" TagPrefix="redsalud" TagName="AdvancedCita" %>

<%--<Templates>--%>
<script id="tmplAppDescription" type="text/x-jquery-tmpl">
            <div style='font-style:italic;'>${Description}</div>
</script>
<%--</Templates>--%>

<style type="text/css">
    .RecurrenceEditor 
    {
        visibility:hidden;
    }
    .rsAdvOptionsScroll
    {
        height: auto !important;   
    }
    ul li.rsAllDayWrapper 
    {
        visibility: hidden;
    }
</style>

<script type="text/javascript">
    //<![CDATA[
    var categoryNames = new Array();
 
    function OnClientAppointmentsPopulating(sender, eventArgs) {
        addSelectedCategoriesToArray(categoryNames);
        eventArgs.get_schedulerInfo().CategoryNames = categoryNames;
        eventArgs.get_schedulerInfo().MedicoId = <%= MedicoId.ToString() %>; // medicoidtostring
        eventArgs.get_schedulerInfo().ProveedorId = 0;
        categoryNames = new Array(); //clear the array
    }

    function OnClientAppointmentsPopulated(sender, eventArgs) {
        var scheduler = $find('<%=RadScheduler1.ClientID %>');

        var citas = scheduler.get_appointments();
        citas.forEach(function(o) {
            var now = new Date();
            if (o.get_start().getTime() < now.getTime()) {
                o.set_allowDelete(false);
                o.set_allowEdit(false);
            }
        });
    }
 
    function OnClientAppointmentWebServiceInserting(sender, args) {
        //set a default Calendar resource
        if (args.get_appointment().get_resources().get_count() == 0) {
            var defaultCalendarResource = sender.get_resources().getResourceByTypeAndKey("UserId", "1"); 
            args.get_appointment().get_resources().add(defaultCalendarResource);
        }
    }
 
    function addSelectedCategoriesToArray(categoryNamesArray) {
        var $ = $telerik.$;
    }
 
    function rebindScheduler() {
        var scheduler = $find('<%=RadScheduler1.ClientID %>');
        scheduler.rebind();
    }
 
    function onSchedulerDataBound(scheduler) {
        var $ = jQuery;
        $(".rsAptDelete").each(function () {
            var apt = scheduler.getAppointmentFromDomElement(this);
            // creating an object containing the data that should be applied on the template
            var tmplValue = { Description: apt.get_description() };
            // instantiate the template, populate it and insert before the delete handler (".rsAptDelete")
            $("#tmplAppDescription").tmpl(tmplValue).insertBefore(this);
        });
    }


    /* Helper functions */
                
    /// <summary>
    ///          Returns the appointments in the specified period filtered by the specified resource.
    /// </summary>
    /// <param name="scheduler">
    ///          RadScheduler's client-side object
    /// </param>
    /// <param name="start" type="Date">
    ///          The start of the period
    /// </param>
    /// <param name="end" type="Date">
    ///          The end of the period
    /// </param>
    /// <param name="resource">
    ///          The resource (room or user) to filter by
    /// </param>
    /// <param name="appointment">
    ///          The current appointment
    /// </param>
    function getAppointmentsInRangeByResource(scheduler, start, end, resource, appointment)
    {
        //Get all appointments within the specified time period
        var result = scheduler.get_appointments().getAppointmentsStartingInRange(start, end);
                     
        //If specified remove the appointment from the appointment list
        if (appointment)
            result.remove(appointment);
                     
        //Filter the appointments based on the resource
        return result;//.findByResource(resource);
    }
                
    /// <summary>
    ///          Checks if the user associated with the specified appointment has another
    ///          appointment in the specified time period
    /// </sumary>
    /// <param name="scheduler">
    ///          RadScheduler's client-side object
    /// </param>
    /// <param name="start" type="Date">
    ///          The start of the period
    /// </param>
    /// <param name="end" type="Date">
    ///          The end of the period
    /// </param>
    /// <param name="appointment">
    ///          The current appointment
    /// </param>
    function isUserOccupied(scheduler, start, end, appointment)
    {
        //get the "User" resource associated with the appointment
        var currentUser = 1;
        //get all appointments in this period which are associated with this resource
        var appointmentsForThisUser = getAppointmentsInRangeByResource(scheduler, start, end, currentUser, appointment);
        //if the list of appointments is not empty the user has other appointments besides the specified in this time period
        return appointmentsForThisUser.get_count() > 0;
    }
                
    /// <summary>
    ///          Checks if the specified time slot is occupied and warns to prevent appointment rescheduling.
    ///          Called by the resizeEnd and moveEnd client-side event handlers.
    /// </sumary>
    function warnIfOccupied(start, end, sender, args)
    {
        var slot = args.get_targetSlot();
        var appointment = args.get_appointment();
                     
        if (isUserOccupied(sender, start, end, appointment))
        {
            alert("El doctor está ocupado en el periodo indicado");
            args.set_cancel(true);
        }
                     
        appointment.get_element().style.border = "";
    }
    /// <summary>
    ///          Checks if the specified time slot is occupied and visually shows it to the user.
    ///          Called by the resizeing and moving client-side event handlers.
    /// </sumary>
    function highlightIfOccupied(start, end, sender, args)
    {
        var appointment = args.get_appointment();
        var slot = args.get_targetSlot();
                     
        if(isUserOccupied(sender, start, end, appointment))
        {
            appointment.get_element().style.border = "1px solid red";
            return;
        }
                     
        appointment.get_element().style.border = "";
    }
                
    function onAppointmentResizing(sender, args) {
 
        var start = args.get_targetSlot().get_startTime();
        var end = args.get_targetSlot().get_endTime();
 
        highlightIfOccupied(start, end, sender, args);
    }
 
    function onAppointmentResizeEnd(sender, args) {
        var start = args.get_newStartTime();
        var end = args.get_newEndTime();
 
        warnIfOccupied(start, end, sender, args);
    }
                
    function onAppointmentMoving(sender, args)
    {
        var start = args.get_targetSlot().get_startTime();
        var end = new Date(start.getTime() + args.get_appointment().get_duration());
        highlightIfOccupied(start, end, sender, args);
    }
                
    function onAppointmentMoveEnd(sender, args)
    {
        var start = args.get_targetSlot().get_startTime();
        var end = new Date(start.getTime() + args.get_appointment().get_duration());
                     
        var now = new Date();
        if (start.getTime() < now.getTime()) {
            alert("No puede mover una cita a un tiempo anterior al actual");
            args.set_cancel(true);
            return;
        }
        warnIfOccupied(start, end, sender, args);
    }
           
    function onAppointmentInserting(sender, args)
    {
        var slot = args.get_targetSlot();
        var start = slot.get_startTime();
        var end = new Date(start.getTime() + sender.get_durationOfSelectedArea());

        var now = new Date();
        if (start.getTime() < now.getTime()) {
            alert("No puede crear citas en tiempo pasado");
            args.set_cancel(true);
            return;
        }

        if (isUserOccupied(sender, start, end, slot))
        {
            alert("El médico ya se encuentra ocupado en este espacio de tiempo");
            args.set_cancel(true);
        }
    }

    function onAppointmentDeleting(sender, args)
    {
        var unaCita = args.get_appointment();
        var start = unaCita.get_start();

        var now = new Date();
        if (start.getTime() < now.getTime()) {
            alert("No puede eliminar una cita pasada");
            args.set_cancel(true);
            return;
        }
    }

    Sys.Application.add_load(todayLinkBug); 
  
    function todayLinkBug()  
    {  
        $('.rsToday')[0].click();
    }
    //]]>
</script>


    <div class="exampleContainer">
        <telerik:RadScheduler runat="server" ID="RadScheduler1" 
            SelectedView="WeekView"
            FirstDayOfWeek="Monday" 
            LastDayOfWeek="Saturday"
            DayStartTime="05:00:00"
            DayEndTime="23:00:00" 
            StartEditingInAdvancedForm="true"
            StartInsertingInAdvancedForm="true"
            Culture="es"
            MinutesPerRow="15"
            WeekView-HeaderDateFormat="dd/MM/yyyy"
            DayView-HeaderDateFormat="dd/MM/yyyy"
            TimelineView-HeaderDateFormat="dd/MM/yyyy" 
            TimelineView-ColumnHeaderDateFormat="dd/MM/yyyy" 
            OnClientAppointmentsPopulating="OnClientAppointmentsPopulating" 
            OnClientAppointmentsPopulated="OnClientAppointmentsPopulated" 
            OnClientAppointmentWebServiceInserting="OnClientAppointmentWebServiceInserting"

            OnClientAppointmentInserting="onAppointmentInserting"
            OnClientAppointmentMoving="onAppointmentMoving" 
            OnClientAppointmentMoveEnd="onAppointmentMoveEnd"
            OnClientAppointmentResizing="onAppointmentResizing" 
            OnClientAppointmentResizeEnd="onAppointmentResizeEnd"

            Height="800"
            EnableDescriptionField="true" 
            OverflowBehavior="Auto" 
            AppointmentStyleMode="Default"             
            OnClientDataBound="onSchedulerDataBound" 
            ShowAllDayRow="False">
            <AdvancedForm Modal="true"></AdvancedForm>

            <TimelineView GroupBy="Calendar" GroupingDirection="Vertical"></TimelineView>
            
            <WebServiceSettings Path="~/Calendario/CitaWebService.asmx" ResourcePopulationMode="ServerSide">
            </WebServiceSettings>
            
            <ResourceStyles>
                    <%--AppointmentStyleMode must be explicitly set to Default (see above) otherwise setting BackColor/BorderColor
                            will switch the appointments to Simple rendering (no rounded corners and gradients)--%>
                <telerik:ResourceStyleMapping Type="UserId" Text="User" BackColor="#D0ECBB"
                    BorderColor="#B0CC9B"></telerik:ResourceStyleMapping>
            </ResourceStyles>
            <TimeSlotContextMenus>
                <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerTimeSlotContextMenu">
                    <Items>
                        <telerik:RadMenuItem Text="Nueva Cita" Value="CommandAddAppointment">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadSchedulerContextMenu>
            </TimeSlotContextMenus>
            <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>

        </telerik:RadScheduler>
    </div>

<asp:HiddenField ID="MedicoIdHiddenField" runat="server" />
<asp:HiddenField ID="UserIdHiddenField" runat="server" />