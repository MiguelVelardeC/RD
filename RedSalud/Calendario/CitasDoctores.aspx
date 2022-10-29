<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CitasDoctores.aspx.cs" 
    Inherits="Calendario_CitasDoctores" UICulture="es-BO" %>

<%@ Register Src="~/UserControls/Cita/CalendarioCita.ascx" TagPrefix="RedSalud" TagName="CalendarioCita" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label2" Text="Call Center de Citas" runat="server" CssClass="title" />
            </div>
        </div>
    </div>

    Seleccionar Médico:
    <telerik:RadComboBox ID="MedicoRadComboBox" runat="server" 
        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
        AutoPostBack="true" EnableLoadOnDemand="true" CssClass="bigField"
        OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged"
        SelectedValue='<%# Bind("MedicoId") %>'>
        <WebServiceSettings Method="GetMedicos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
    </telerik:RadComboBox>

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="CalendarioDoctorLabel" Text="Calendario" runat="server" CssClass="title" Visible="false" />
            </div>
        </div>

        <redsalud:calendariocita runat="server" id="CalendarioDoctor" Visible="false" />
    </div>
    
</asp:Content>

