<%@ Page Title="Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteAsegurado.aspx.cs" Inherits="Reportes_ReporteAsegurado" %>
<%@ Register Src="../UserControls/SearchUserControl/SearchControl.ascx" TagName="SearchControl" TagPrefix="uc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Reporte de Asegurados" runat="server" CssClass="title" />
            </div>
            <br />
            <br />
            <div class="contentMenu">
                <div>
                    <asp:Label Text="Cliente" runat="server" CssClass="label" />
                    <telerik:RadComboBox ID="ClienteDDL" runat="server"
                        DataSourceID="ClienteODS"
                        CssClass="bigField"
                        DataValueField="ClienteId"
                        DataTextField="NombreJuridico"
                        EmptyMessage="Seleccione un Cliente">
                    </telerik:RadComboBox>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="ClienteDDLRFV" runat="server"
                            ValidationGroup="GenerarReporte"
                            ErrorMessage="Debe seleccionar un Cliente."
                            ControlToValidate="ClienteDDL"
                            Display="Dynamic" />
                    </div>
                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL" SelectMethod="getRedClienteList"
                        OldValuesParameterFormatString="original_{0}" OnSelected="ClienteODS_Selected" />
                </div>
                <div>
                    <asp:Label Text="Código asegurado" runat="server" CssClass="label" />
                    <asp:TextBox ID="CodigoAseguradoTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="CodigoAseguradoTextBoxRFV" runat="server"
                            ValidationGroup="GenerarReporte"
                            ErrorMessage="Debe insertar un Código asegurado."
                            ControlToValidate="CodigoAseguradoTextBox"
                            Display="Dynamic" />
                    </div>
                </div>
                <div>
                    <asp:Label Text="Nro. Póliza" runat="server" CssClass="label" />
                    <asp:TextBox ID="NroPolizaTextBox" runat="server" CssClass="bigField"></asp:TextBox>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="NroPolizaTextBoxRFV" runat="server"
                            ValidationGroup="GenerarReporte"
                            ErrorMessage="Debe insertar un Nro. Póliza."
                            ControlToValidate="NroPolizaTextBox"
                            Display="Dynamic" />
                    </div>
                </div>
                <div class="buttonsPanel">
                    <asp:LinkButton ID="GenerarReporte" runat="server" CssClass="button" ValidationGroup="GenerarReporte"
                        Text="Generar Reporte" OnClick="GenerarReporte_Click" />
                </div>
            </div>
            <div style="width:800px; margin: 0 auto;">
                <rsweb:ReportViewer ID="AseguradosReportViewer" runat="server" 
                    Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportPath="RDLC/ReportAsegurado.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="AsegBodyObjectDataSource" Name="AseguradoDetalleDS" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="AsegBodyObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="GetData" 
                    TypeName="AseguradoDSTableAdapters.AseguradoDetalleTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="AseguradoIdHF" Name="intAseguradoraId" PropertyName="Value" Type="Int32" />
                        <asp:ControlParameter ControlID="NumeroPolizaHF" Name="varNumeroPoliza" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <asp:HiddenField ID="AseguradoIdHF" runat="server" Value="0" />
            <asp:HiddenField ID="NumeroPolizaHF" runat="server" />
        </div>
    </div>
</asp:Content>

