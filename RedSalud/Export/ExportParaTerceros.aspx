<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExportParaTerceros.aspx.cs" Inherits="Export_ExportParaTerceros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
        <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" CssClass="title" Text="Exportar datos para farmacia" runat="server" />
            </div>
        </div>
            <br />
            <asp:Button ID="exportPacientesButton" runat="server" Text="Exportar Pacientes Activos/Inactivos Farmacia Chavez" OnClick="exportPacientesButton_Click" />
        </div>
</asp:Content>

