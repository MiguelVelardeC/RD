<%@ Page Title="Importar Pacientes" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImportarPaciente.aspx.cs" Inherits="Paciente_ImportarPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .info
        {
            margin: 10px 0px 10px 0px;
            border:1px solid #fcefa1;
            background-color:#fbf9ee;
            padding: 10px;
        }
        .ruFilePortion {
            display:none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Importar Pacientes</span>
            </div>
            <div class="columnContent">
                <div class="clear"></div>
                <div class="info">
                    Esta opcion permite importar los datos de los pacientes a partir de un archivo Excel. 
                    Dicho archivo Excel debe tener un formato especifico, el cual puede descargarse aqui: 
                    <asp:HyperLink runat="server"
                        NavigateUrl="~/DataFiles/ModeloImportacionPacientes.xlsx">Descargar</asp:HyperLink>
                </div>
                <asp:Panel ID="FileUploadPanel" runat="server">
                    <asp:Label Text="Cliente" runat="server" CssClass="label" />
                    <telerik:RadComboBox ID="ClienteDDL" runat="server"
                        ClientIDMode="Static"
                        DataSourceID="ClienteODS"
                        CssClass="bigField"
                        DataValueField="ClienteId"
                        DataTextField="NombreJuridico"
                        EmptyMessage="Seleccione un Cliente">
                    </telerik:RadComboBox>
                    <div class="validation">
                        <asp:CustomValidator ID="ClienteCV" runat="server"
                            ValidationGroup="Import"
                            ErrorMessage="Debe seleccionar un cliente."
                            ClientValidationFunction="ClienteCV_Validate"
                            Display="Dynamic" />
                    </div>

                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getRedClienteList"
                        OnSelected="ClienteODS_Selected">
                    </asp:ObjectDataSource>

                    <span class="label">Lugar de la Póliza</span>
                    <asp:DropDownList ID="LugarDDL" runat="server"
                        CssClass="normalField">
                        <asp:ListItem Text="SANTA CRUZ" Value="SCZ" />
                        <asp:ListItem Text="LA PAZ" Value="LPZ" />
                        <asp:ListItem Text="COCHABAMBA" Value="CBBA" />
                        <asp:ListItem Text="SUCRE" Value="SCR" />
                    </asp:DropDownList>
                    <div class="validation">
                        <asp:RequiredFieldValidator ID="LugarRFV" runat="server"
                            Display="Dynamic"
                            ValidationGroup="Import"
                            ControlToValidate="LugarDDL"
                            ErrorMessage="El Lugar de la póliza es requerido." />
                    </div>

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
                    <asp:LinkButton ID="uploadFromExcelButton" 
                        runat="server" 
                        CssClass="button"
                        ValidationGroup="Import"
                        OnClick="btnUpload_Click">
                        <span>Importar pacientes</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="UpdateButton" 
                        runat="server" Visible="false"
                        CssClass="button"
                        OnClick="UpdateLB_Click">
                        <span>Actualizar Polizas</span>
                    </asp:LinkButton>
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
        </div>
    </div>
    <script type="text/javascript">
        function ClienteCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('ClienteDDL').get_value();

            if (value <= "") {
                args.IsValid = false;
            }
        }
    </script>
</asp:Content>

