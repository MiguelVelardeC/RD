<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MiCalendario.aspx.cs" 
    Inherits="Calendario_MiCalendario" UICulture="es-BO" %>

<%@ Register Src="~/UserControls/Cita/CalendarioCita.ascx" TagPrefix="RedSalud" TagName="CalendarioCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <redsalud:calendariocita runat="server" id="CalendarView" />

</asp:Content>