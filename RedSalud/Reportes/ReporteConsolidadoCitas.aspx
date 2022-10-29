<%@ Page Title="Reporte de Consolidado de Citas" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteConsolidadoCitas.aspx.cs" Inherits="Reportes_ReporteConsolidadoCitas" %>
<%@ Register Src="../UserControls/SearchUserControl/SearchControl.ascx" TagName="SearchControl" TagPrefix="uc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">        
        <div class="columnHead">
            <asp:Label ID="Label1" Text="Reporte de Consolidado de Citas" runat="server" CssClass="title" />
        </div>
        <asp:Panel runat="server">
            CLIENTE:
            <telerik:RadComboBox ID="ClienteDDL" runat="server"
                DataValueField="ClienteId"
                CssClass="bigField"
                DataTextField="NombreJuridico"
                EmptyMessage="Seleccione un Cliente">
            </telerik:RadComboBox>
                
            ESPECIALIDAD:
            <telerik:RadComboBox ID="EspecialidadDDL" runat="server"
                DataValueField="EspecialidadId"
                CssClass="bigField"
                DataTextField="Nombre"
                EmptyMessage="Seleccione una Especialidad">
            </telerik:RadComboBox>                                         
        </asp:Panel>
        <div class="clear" style="margin-bottom: 10px;"></div>
        <asp:Panel runat="server">
            MÉDICO:
            <telerik:RadComboBox ID="MedicoDDL" runat="server"
                DataValueField="MedicoId"
                CssClass="bigField"
                DataTextField="Nombre"
                EmptyMessage="Seleccione un Médico">
            </telerik:RadComboBox>
                
            FECHA INICIO:
            <telerik:RadDatePicker ID="FechaIniDP" runat="server"                    
                EmptyMessage="Ingrese una fecha inicio">
            </telerik:RadDatePicker>
                
            FECHA FIN:
            <telerik:RadDatePicker ID="FechaFinDP" runat="server"                    
                EmptyMessage="Ingrese una fecha fin">
            </telerik:RadDatePicker>
        </asp:Panel>
        <div class="clear" style="margin-bottom: 10px;"></div>
        <asp:Panel runat="server">
            NÚMERO DE PÓLIZA:
            <telerik:RadTextBox ID="PolizaText" runat="server" MaxLength="20" Width="250px"
                EmptyMessage="Ingrese un Número de Póliza" Text="*">
            </telerik:RadTextBox>
                
            ESTADO:
            <telerik:RadComboBox ID="EstadoDDL" runat="server"
                DataValueField="Id"
                CssClass="bigField"
                DataTextField="Nombre"
                EmptyMessage="Seleccione un Estado">
            </telerik:RadComboBox>                                         
        </asp:Panel>
        <div class="buttonsPanel">
            <asp:LinkButton ID="GenerarReporte" runat="server" CssClass="button" OnClick="GenerarReporte_Click">
                <span>GENERAR REPORTE</span>
            </asp:LinkButton>
        </div>
        <div style="margin: 0 auto; height:auto;">
            <rsweb:ReportViewer ID="RedSaludReportViewer" runat="server" Height="800px" 
                Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                <LocalReport ReportPath="RDLC/ConsolidadoCitas.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="BodyObjectDataSource" Name="RedSaludDataSet" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="BodyObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" 
                SelectMethod="GetData" 
                TypeName="VideoLlamadaDSTableAdapters.ConsolidadoCitasTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="EspecialidadDDL" Name="EspecialidadId" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="FechaIniDP" Name="FechaIni" PropertyName="SelectedDate" Type="DateTime" />
                    <asp:ControlParameter ControlID="FechaFinDP" Name="FechaFin" PropertyName="SelectedDate" Type="DateTime" />
                    <asp:ControlParameter ControlID="ClienteDDL" Name="ClienteId" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="MedicoDDL" Name="MedicoId" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="PolizaText" Name="NumeroPoliza" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="EstadoDDL" Name="EstadoId" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>

