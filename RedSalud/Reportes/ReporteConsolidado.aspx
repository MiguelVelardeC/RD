<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteConsolidado.aspx.cs" Inherits="Reportes_ReporteConsolidado" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label32" CssClass="title" Text="Reporte de consolidación" runat="server" />
            </div>
            <br />
            <div class="clear"></div>
            <div style="width: 800px; margin: 0 auto; height:auto;">
                <asp:Panel ID="ErrorPanel" runat="server" Visible="false">
                    <span>No se pudo obtener el reporte de la consolidación, seleccione una consolidacion en la página</span>
                    <asp:HyperLink ID="ReturnHL" runat="server" Text="Consolidados" NavigateUrl="~/Consolidacion/Consolidados.aspx"></asp:HyperLink>
                </asp:Panel>
                <rsweb:ReportViewer ID="ConsolidacionReportViewer" runat="server"
                    Font-Names="Verdana"
                    Font-Size="8pt"
                    WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt"
                    Width="100%"
                    Height="100%">
                    <LocalReport ReportPath="RDLC/ReportConsolidacion.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ConslidacionODS" Name="ConsolidacionDS" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ConslidacionODS" runat="server"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetConsolidacionById"
                    TypeName="GastoDSTableAdapters.ConsolidacionTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ConsolidacionIdHF" Name="ConsolidacionId" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="ConsolidacionIdHF" runat="server" Value="5" />
</asp:Content>

