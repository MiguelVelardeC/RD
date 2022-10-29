<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImportParaTerceros.aspx.cs" Inherits="Import_ImportParaTerceros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .ruFilePortion {
            display:none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
        <div class="oneColumn">
            <div class="frame">
                <div class="columnHead">
                    <asp:Label ID="Label1" CssClass="title" Text="Cargar Pólizas Diario" runat="server" />
                </div>
            </div>
            <asp:Panel ID="FileUploadPanel" runat="server">
                <span class="label">Seleccionar archivo</span>
                <asp:FileUpload ID="fileupload" runat="server"  />
                <div class="validation">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fileupload"
                        ErrorMessage="Debe seleccionar un archivo para importar"
                        Display="Dynamic"
                        ValidationGroup="Import">
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            <asp:Label ID="SuccessLabel" runat="server" ForeColor="#339966" />
            <div class="buttonsPanel">
                <asp:Button ID="importPacientesButton" runat="server" 
                    Text="Importar Polizas" 
                    ValidationGroup="Import"
                    OnClick="importPacientesButton_Click" />
                <asp:Button ID="UpdateButton" runat="server" 
                    Text="Actualizar Polizas" Visible="false"
                    OnClick="UpdateLB_Click" />
            </div>
            <asp:Label ID="ErrorLabel" runat="server" ForeColor="#FF7575" />
            <telerik:RadProgressManager ID="RadProgressManager" runat="server" />
            <telerik:RadProgressArea ID="ProgresoRPB" runat="server"
                 Width="500px" Visible="false">
                <Localization Uploaded="Importando..." UploadedFiles="Total Importando"
                    ElapsedTime="Tiempo Transcurrido" Total="" TotalFiles="" TransferSpeed="" 
                    EstimatedTime="" Cancel="Cancelar"
                    CurrentFileName="Importando Poliza: "  />
            </telerik:RadProgressArea>
        </div>
</asp:Content>

