<%@ Page Title="Médico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorDetails.aspx.cs" Inherits="Proveedor_ProveedorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .addButton {
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="left" style="max-width: 100%; width: 100%; padding: 10px 10px 10px 0px;">
        <telerik:RadTabStrip ID="ProveedorTab" runat="server" MultiPageID="ProveedorMP" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Proveedor" PageViewID="ProveedorRPV"></telerik:RadTab>
                <telerik:RadTab Text="Prestaciones" ID="PrestacionesRT" runat="server"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="ProveedorMP" runat="server"
            CssClass="RadMultiPage"
            SelectedIndex="0">
            <telerik:RadPageView ID="ProveedorRPV" runat="server">
                <div>
                    <div class="frame">
                        <div class="columnHead">
                            <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                                Text="Nuevo Proveedor">
                            </asp:Label>
                        </div>
                        <div class="columnContent">
                            <div class="contentMenu">
                                <asp:LinkButton runat="server" Text="Volver a la Lista de Proveedores"
                                    OnClick="HyperLink1_Click" ValidationGroup="return">
                                </asp:LinkButton>

                            </div>
                            <div style="float: left; width: 30%;">
                                <asp:FormView ID="ProveedorFV" runat="server" DataSourceID="ProveedorODS"
                                    OnDataBound="ProveedorFV_DataBound"
                                    OnItemInserting="ProveedorFV_ItemInserting"
                                    OnItemUpdating="ProveedorFV_ItemUpdating">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="ProveedorIDHiddenField" runat="server" Value='<%# Bind("ProveedorId") %>' />
                                        <asp:HiddenField ID="EspecialidadIDHiddenField" runat="server" Value='<%# Bind("EspecialistaId") %>' />
                                        <div>
                                            <asp:Label Text="Tipo de Proveedor" runat="server" CssClass="label" />
                                            <asp:Label ID="TipoProveedor" Text='<%# Eval("NombreTipoProveedor") %>' runat="server" />
                                            <asp:HiddenField ID="TipoProveedorId" Value='<%# Bind("TipoProveedorId") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Nombre" runat="server" CssClass="label" />
                                            <asp:Label ID="NombreJuridico" Text='<%# Bind("NombreCompletoOrJuridico") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Nombre de Usuario" runat="server" CssClass="label" />
                                            <asp:Label ID="NombreUsuario" Text='<%# Bind("NombreUsuario") %>' runat="server" />
                                        </div>
                                        <asp:Panel ID="EspecialidadContainer" runat="server" Visible="false">
                                            <asp:Label ID="LabelMedicoId" Text='<%# Bind("MedicoId") %>' runat="server" Visible="false" />
                                            <asp:Label Text="Especialidad" runat="server" CssClass="label" />
                                            <asp:Label ID="Especialidad" Text='<%# Bind("NombreEspecialidad") %>' runat="server" />
                                        </asp:Panel>
                                        <asp:Panel ID="SedesContainer" runat="server" Visible="false">
                                            <asp:Label Text="Sedes" runat="server" CssClass="label" />
                                            <asp:Label ID="Sedes" Text='<%# Bind("Sedes") %>' runat="server" />
                                        </asp:Panel>
                                        <asp:Panel ID="ColegioMedicoContainer" runat="server" Visible="false">
                                            <asp:Label Text="Colegio Médico" runat="server" CssClass="label" />
                                            <asp:Label ID="ColegioMedico" Text='<%# Bind("ColegioMedico") %>' runat="server" />
                                        </asp:Panel>
                                        <asp:Panel ID="CostoConsultaContainer" runat="server" Visible="false">
                                            <asp:Label Text="Costo Consulta" runat="server" CssClass="label" />
                                            <asp:Label ID="CostoConsulta" Text='<%# Bind("CostoConsulta") %>' runat="server" />
                                        </asp:Panel>
                                        <asp:Panel ID="PorcentageDescuentoContainer" runat="server" Visible="false">
                                            <asp:Label Text="Porcentage de Descuento" runat="server" CssClass="label" />
                                            <asp:Label ID="PorcentageDescuento" Text='<%# Bind("PorcentageDescuento") %>' runat="server" />
                                        </asp:Panel>
                                        <div>
                                            <asp:Label Text="NIT" runat="server" CssClass="label" />
                                            <asp:Label ID="Nit" Text='<%# Bind("Nit") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Dirección" runat="server" CssClass="label" />
                                            <asp:Label ID="Direccion" Text='<%# Bind("Direccion") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Teléfono Casa" runat="server" CssClass="label" />
                                            <asp:Label ID="TelefonoCasa" Text='<%# Bind("TelefonoCasa") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Teléfono Oficina" runat="server" CssClass="label" />
                                            <asp:Label ID="TelefonoOficina" Text='<%# Bind("TelefonoOficina") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Celular" runat="server" CssClass="label" />
                                            <asp:Label ID="Celular" Text='<%# Bind("Celular") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Estado" runat="server" CssClass="label" />
                                            <asp:Label ID="Estado" Text='<%# Eval("Estado").ToString() == "ACTIVO" ? "Activo" : "Inactivo" %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Observacion" runat="server" CssClass="label" />
                                            <asp:Label ID="Observacion" Text='<%# Bind("Observaciones") %>' runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label Text="Fecha Actualización" runat="server" CssClass="label" />
                                            <asp:Label ID="FechaActualizacion" Text='<%# Bind("FechaActualizacion") %>' runat="server" />
                                        </div>

                                        <div class="buttonsPanel">
                                            <asp:LinkButton Text="" runat="server"
                                                CommandName="Edit" CssClass="button">
                                <span>Modificar</span>
                                            </asp:LinkButton>
                                            <asp:HyperLink NavigateUrl="~/Proveedor/ProveedorList.aspx" runat="server"
                                                CssClass="secondaryButton" Text="Cancelar" />
                                        </div>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                            <AjaxSettings>
                                                <telerik:AjaxSetting AjaxControlID="EspecialidadRadComboBox">
                                                    <UpdatedControls>
                                                        <telerik:AjaxUpdatedControl ControlID="UsuarioRadComboBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="RadComboBoxMedico"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="SedesTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ColegioProveedorTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="DireccionTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="TelefonoOficinaRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="CelularRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="NombresTextBox"></telerik:AjaxUpdatedControl>
                                                    </UpdatedControls>
                                                </telerik:AjaxSetting>
                                                <telerik:AjaxSetting AjaxControlID="RadComboBoxMedico">
                                                    <UpdatedControls>
                                                        <telerik:AjaxUpdatedControl ControlID="UsuarioRadComboBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="SedesTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ColegioProveedorTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="DireccionTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="TelefonoOficinaRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="CelularRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="NombresTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ApellidosTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="EstadoRadComboBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ObservacionesTextBox"></telerik:AjaxUpdatedControl>

                                                    </UpdatedControls>

                                                </telerik:AjaxSetting>
                                                <telerik:AjaxSetting AjaxControlID="TipoProveedorRadComboBox">
                                                    <UpdatedControls>
                                                        <telerik:AjaxUpdatedControl ControlID="UsuarioRadComboBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="RadComboBoxMedico"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="EspecialidadRadComboBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="SedesTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ColegioProveedorTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="DireccionTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="TelefonoOficinaRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="CelularRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="NombresTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ApellidosTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="EstadoRadComboBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="ObservacionesTextBox"></telerik:AjaxUpdatedControl>
                                                    </UpdatedControls>
                                                </telerik:AjaxSetting>
                                                <telerik:AjaxSetting AjaxControlID="UsuarioRadComboBox">
                                                    <UpdatedControls>
                                                        <telerik:AjaxUpdatedControl ControlID="DireccionTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="TelefonoOficinaRadNumericTextBox"></telerik:AjaxUpdatedControl>
                                                        <telerik:AjaxUpdatedControl ControlID="CelularRadNumericTextBox"></telerik:AjaxUpdatedControl>

                                                    </UpdatedControls>
                                                </telerik:AjaxSetting>
                                            </AjaxSettings>
                                        </telerik:RadAjaxManager>
                                        <asp:Panel ID="InsertPanel" runat="server" DefaultButton="InsertProveedorLB">
                                            <asp:HiddenField ID="ProveedorIDHiddenField" runat="server" Value='<%# Bind("ProveedorId") %>' />
                                            <asp:HiddenField ID="EspecialidadIDHiddenField" runat="server" Value='<%# Bind("EspecialistaId") %>' />
                                            <div>
                                                <div>
                                                    <asp:Label Text="Red Médica" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="RedMedicaDDL" runat="server"
                                                        DataSourceID="RedMedicaODS" CssClass="bigField"
                                                        SelectedValue='<%# Bind("RedMedicaId") %>'
                                                        DataValueField="RedMedicaId" DataTextField="Nombre"
                                                        EmptyMessage="Seleccione una Red Médica">
                                                    </telerik:RadComboBox>
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RedMedicaCV" runat="server" Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="RedMedicaDDL" ErrorMessage="Debe seleccionar una Red Médica." />
                                                    </div>
                                                    <asp:ObjectDataSource ID="RedMedicaODS" runat="server" TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                                                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetRedMedicaListxCodigo"
                                                        OnSelected="RedMedicaODS_Selected">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="CodigoRedMedicaHF" Name="Codigo" PropertyName="Value" DbType="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Tipo de Proveedor" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="TipoProveedorRadComboBox" runat="server"
                                                        DataSourceID="TipoProveedorODS" CssClass="bigField"
                                                        SelectedValue='<%# Bind("TipoProveedorId") %>'
                                                        OnClientSelectedIndexChanged="TipoProveedorRadComboBox_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="TipoProveedorRadComboBox_SelectedIndexChanged"
                                                        DataValueField="TipoProveedorId" DataTextField="Nombre" AutoPostBack="true"
                                                        EmptyMessage="Seleccione el tipo de proveedor">
                                                    </telerik:RadComboBox>
                                                    <asp:ObjectDataSource ID="TipoProveedorODS" runat="server" TypeName="Artexacta.App.TipoProveedor.BLL.TipoProveedorBLL"
                                                        OldValuesParameterFormatString="original_{0}" SelectMethod="getTipoProveedorList"
                                                        OnSelected="TipoProveedorODS_Selected" />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="TipoProveedorRequiredFieldValidator" runat="server" Display="Dynamic"
                                                            ControlToValidate="TipoProveedorRadComboBox" ErrorMessage="Debe seleccionar un tipo de proveedor."
                                                            ValidationGroup="insert" />
                                                    </div>
                                                </div>

                                                <div id="DivNombreJuridico">

                                                    <asp:Label Text="Nombre Jurídico" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="NombreJuridico" runat="server"
                                                        CssClass="normalField"
                                                        Text='<%# Bind("NombreJuridico") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="NombreJuridico" CssClass="NombreJuridico"
                                                            ErrorMessage="El Nombre Jurídico es requerido." />
                                                    </div>
                                                    <div>
                                                        <asp:Label Text="Seleccione un Usuario" runat="server" CssClass="label" />
                                                        <telerik:RadComboBox ID="UsuarioRadComboBox" runat="server"
                                                            AutoPostBack="true" EnableLoadOnDemand="true"
                                                            EmptyMessage="Seleccione un Usuario" MarkFirstMatch="true"
                                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                            SelectedValue='<%# Bind("userId") %>' Text='<%# Eval("NombreUsuario") %>'
                                                            OnDataBinding="RadComboBox_DataBinding"
                                                            OnSelectedIndexChanged="UsuarioRadComboBox_SelectedIndexChanged">
                                                            <WebServiceSettings Method="GetUsuariosActivo" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                                        </telerik:RadComboBox>
                                                    
                                                    </div>

                                                </div>
                                                <div id="DivNombres">


                                                    <asp:Label Text="Nombres" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="NombresTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Nombres") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="NombresTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert" CssClass="Nombres"
                                                            ControlToValidate="NombresTextBox" ErrorMessage="Los Nombres son requeridos." />
                                                    </div>
                                                </div>
                                                <div id="DivApellidos">
                                                    <asp:Label Text="Apellidos" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="ApellidosTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Apellidos") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="ApellidosTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert" CssClass="Apellidos"
                                                            ControlToValidate="ApellidosTextBox" ErrorMessage="Los Apellidos son requeridos." />
                                                    </div>
                                                </div>
                                                <div id="DivEspecialidad">
                                                    <asp:Label Text="Especialidad" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="EspecialidadRadComboBox" runat="server"
                                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                        AutoPostBack="True" EnableLoadOnDemand="true"
                                                        SelectedValue='<%# Bind("EspecialidadId") %>' Text='<%# Eval("NombreEspecialidad") %>'
                                                        OnDataBinding="RadComboBox_DataBinding"
                                                        OnSelectedIndexChanged="EspecialidadRadComboBox_OnClientSelectedIndexChanged">
                                                        <WebServiceSettings Method="GetEspecialidad" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                                    </telerik:RadComboBox>

                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="EspecialidadRFV" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert" CssClass="Especialidad"
                                                            ControlToValidate="EspecialidadRadComboBox"
                                                            ErrorMessage="La Especialidad es requerida." />

                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LabelMedicoId" runat="server" Visible="false"
                                                            CssClass="normalField" Text='<%# Bind("MedicoId") %>' />
                                                        <asp:Label Text="Medico Usuario" runat="server" CssClass="label" />
                                                        <asp:DropDownList ID="RadComboBoxMedico" runat="server"
                                                            OnSelectedIndexChanged="RadComboBoxMedico_SelectedIndexChanged"
                                                            AutoPostBack="True"
                                                            Width="320px"
                                                            MarkFirstMatch="true">
                                                            <asp:ListItem Text="Seleccione una Especialidad Primero" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    

                                                    </div>

                                                </div>
                                                <div id="DivSedes">
                                                    <asp:Label Text="Sedes" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="SedesTextBox" runat="server"
                                                        CssClass="normalField"
                                                        Text='<%# Bind("Sedes") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="SedesTextBox" CssClass="Sedes"
                                                            ErrorMessage="El Sedes es requerido." />
                                                    </div>
                                                </div>
                                                <div id="DivColegioMedico">
                                                    <asp:Label Text="Colegio Médico" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="ColegioProveedorTextBox" runat="server"
                                                        CssClass="normalField"
                                                        Text='<%# Bind("ColegioMedico") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="ColegioProveedorTextBox" CssClass="ColegioMedico"
                                                            ErrorMessage="El Colegio Médico es requerido." />
                                                    </div>
                                                </div>
                                                <div id="DivCostoConsulta">
                                                    <asp:Label ID="Label1" Text="Costo de Consulta" runat="server" CssClass="label" />
                                                    <telerik:RadNumericTextBox ID="CostoConsultaTextBox" runat="server"
                                                        CssClass="normalField" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""
                                                        Value='<%# Bind("dCostoConsulta") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="CostoConsultaTextBox" CssClass="CostoConsulta"
                                                            ErrorMessage="El Costo de Consulta es requerido." />
                                                    </div>
                                                </div>
                                                <div id="DivPorcentageDescuento">
                                                    <asp:Label ID="Label3" Text="Porcentage de Descuento" runat="server" CssClass="label" />
                                                    <telerik:RadNumericTextBox ID="PorcentageDescuentoTextBox" runat="server"
                                                        CssClass="normalField" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""
                                                        Value='<%# Bind("dPorcentageDescuento") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="PorcentageDescuentoTextBox" CssClass="PorcentageDescuento"
                                                            ErrorMessage="El Porcentage de Descuento es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="NIT" runat="server" CssClass="label" />
                                                    <telerik:RadNumericTextBox ID="NitTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Nit") %>'
                                                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="NitTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="NitTextBox"
                                                            ErrorMessage="El NIT es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Dirección" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="DireccionTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Direccion") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="DireccionTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="DireccionTextBox"
                                                            ErrorMessage="La Dirección es requerida." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Teléfono de Casa" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="TelefonoCasaRadNumericTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("TelefonoCasa") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="TelefonoCasaRadNumericTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="TelefonoCasaRadNumericTextBox"
                                                            ErrorMessage="El Teléfono de Casa es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Teléfono de la Oficina" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="TelefonoOficinaRadNumericTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("TelefonoOficina") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="TelefonoOficinaRadNumericTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="TelefonoOficinaRadNumericTextBox"
                                                            ErrorMessage="El Teléfono de la Oficina es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Teléfono Celular" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="CelularRadNumericTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Celular") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="CelularRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="CelularRadNumericTextBox"
                                                            ErrorMessage="El Teléfono Celular es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Estado" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="EstadoRadComboBox" runat="server"
                                                        ClientIDMode="Static" CssClass="normalField"
                                                        EmptyMessage="Selecciones un Estado"
                                                        SelectedValue='<%# Bind("Estado") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Activo" Value="ACTIVO" />
                                                            <telerik:RadComboBoxItem Text="Inactivo" Value="INACTIVO" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="CustomValidator2" runat="server"
                                                            ErrorMessage="Debe seleccionar un estado"
                                                            ControlToValidate="EstadoRadComboBox"
                                                            Display="Dynamic" ValidationGroup="insert" />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Observaciones" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="ObservacionesTextBox" runat="server"
                                                        CssClass="bigField" TextMode="MultiLine"
                                                        Text='<%# Bind("Observaciones") %>' />
                                                    <div class="validation"></div>
                                                </div>
                                            </div>
                                            <div class="buttonsPanel">
                                                <asp:LinkButton ID="InsertProveedorLB" Text="" runat="server"
                                                    CssClass="button" OnClick="InsertProveedorLB_Click" ValidationGroup="insert">
                                    <span>Guardar</span>
                                                </asp:LinkButton>
                                                <asp:HyperLink ID="ReturnHL" runat="server" Text="Retornar a la lista de Proveedors"
                                                    NavigateUrl="~/Proveedor/ProveedorList.aspx" CssClass="secondaryButton" />
                                            </div>
                                        </asp:Panel>
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Panel ID="UpdatePanel" runat="server" DefaultButton="UpdateProveedorLB">
                                            <asp:HiddenField ID="ProveedorIDHiddenField" runat="server" Value='<%# Bind("ProveedorId") %>' />
                                            <asp:HiddenField ID="EspecialidadIDHiddenField" runat="server" Value='<%# Bind("EspecialistaId") %>' />
                                            <div>
                                                <div>
                                                    <asp:Label Text="Tipo de Proveedor" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="TipoProveedorRadComboBox" runat="server"
                                                        DataSourceID="TipoProveedorODS" CssClass="bigField"
                                                        SelectedValue='<%# Bind("TipoProveedorId") %>'
                                                        OnClientSelectedIndexChanged="TipoProveedorRadComboBox_ClientSelectedIndexChanged"
                                                        OnDataBound="RadComboBox1_OnDataBound"
                                                        DataValueField="TipoProveedorId" DataTextField="Nombre"
                                                        EmptyMessage="Seleccione el tipo de proveedor">
                                                    </telerik:RadComboBox>
                                                    <asp:ObjectDataSource ID="TipoProveedorODS" runat="server" TypeName="Artexacta.App.TipoProveedor.BLL.TipoProveedorBLL"
                                                        OldValuesParameterFormatString="original_{0}" SelectMethod="getTipoProveedorList"
                                                        OnSelected="TipoProveedorODS_Selected" />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="TipoProveedorRequiredFieldValidator" runat="server" Display="Dynamic"
                                                            ControlToValidate="TipoProveedorRadComboBox" ErrorMessage="Debe seleccionar un tipo de proveedor." />
                                                    </div>
                                                </div>
                                                <div id="DivNombreJuridico">
                                                    <asp:Label Text="Nombre Jurídico" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="NombreJuridico" runat="server"
                                                        CssClass="normalField"
                                                        Text='<%# Bind("NombreJuridico") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            Display="Dynamic" ValidationGroup="update"
                                                            ControlToValidate="NombreJuridico" CssClass="NombreJuridico"
                                                            ErrorMessage="El Nombre Jurídico es requerido." />
                                                    </div>
                                                    <asp:Label Text="Seleccione un Usuario" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="UsuarioRadComboBox" runat="server"
                                                        EnableLoadOnDemand="true"
                                                        EmptyMessage="Seleccione un Usuario" MarkFirstMatch="true"
                                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                        OnDataBinding="RadComboBox_DataBinding"
                                                        SelectedValue='<%# Bind("userId") %>' Text='<%# Eval("NombreUsuario") %>'
                                                        OnSelectedIndexChanged="UsuarioRadComboBox_SelectedIndexChanged">
                                                        <WebServiceSettings Method="GetUsuariosActivo" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>

                                                    </telerik:RadComboBox>
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="UsuarioProRFV" runat="server"
                                                            Display="Dynamic" ValidationGroup="update" CssClass="NombreJuridico"
                                                            ControlToValidate="UsuarioRadComboBox"
                                                            ErrorMessage="El Usuario es requerida." />
                                                    </div>

                                                </div>

                                                <div id="DivNombres">
                                                    <asp:Label Text="Nombres" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="NombresTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Nombres") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="NombresTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update" CssClass="Nombres"
                                                            ControlToValidate="NombresTextBox" ErrorMessage="Los Nombres son requeridos." />
                                                    </div>
                                                </div>

                                                <div id="DivApellidos">
                                                    <asp:Label Text="Apellidos" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="ApellidosTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Apellidos") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="ApellidosTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update" CssClass="Apellidos"
                                                            ControlToValidate="ApellidosTextBox" ErrorMessage="Los Apellidos son requeridos." />
                                                    </div>
                                                </div>
                                                <div id="DivEspecialidad">
                                                    <asp:Label ID="Label2" Text="Especialidad" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="EspecialidadRadComboBox" runat="server" CssClass="bigField"
                                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                        AutoPostBack="True" EnableLoadOnDemand="true"
                                                        SelectedValue='<%# Bind("EspecialidadId") %>' Text='<%# Eval("NombreEspecialidad") %>'
                                                        OnSelectedIndexChanged="EspecialidadRadComboBox_OnClientSelectedIndexChanged"
                                                        OnDataBinding="RadComboBox_DataBinding">
                                                        <WebServiceSettings Method="GetEspecialidad" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                                    </telerik:RadComboBox>

                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="EspecialidadRFV" runat="server"
                                                            Display="Dynamic" ValidationGroup="update" CssClass="Especialidad"
                                                            ControlToValidate="EspecialidadRadComboBox"
                                                            ErrorMessage="La Especialidad es requerida." />

                                                    </div>
                                                    <div>
                                                        <asp:Label ID="LabelMedicoId" runat="server" Visible="false"
                                                            CssClass="normalField" Text='<%# Bind("MedicoId") %>' />
                                                        <asp:Label Text="Medico Usuario" runat="server" CssClass="label" />
                                                        <asp:DropDownList ID="RadComboBoxMedico" runat="server"
                                                            OnSelectedIndexChanged="RadComboBoxMedico_SelectedIndexChanged"
                                                            AutoPostBack="True"
                                                            Width="320px"
                                                            MarkFirstMatch="true">
                                                            <asp:ListItem Text="Seleccione una Especialidad Primero" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>

                                                <div id="DivSedes">
                                                    <asp:Label Text="Sedes" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="SedesTextBox" runat="server"
                                                        CssClass="normalField"
                                                        Text='<%# Bind("Sedes") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            Display="Dynamic" ValidationGroup="update" CssClass="Sedes"
                                                            ControlToValidate="SedesTextBox" ErrorMessage="El Sedes es requerido." />
                                                    </div>
                                                </div>
                                                <div id="DivColegioMedico">
                                                    <asp:Label Text="Colegio Médico" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="ColegioProveedorTextBox" runat="server"
                                                        CssClass="normalField"
                                                        Text='<%# Bind("ColegioMedico") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                            Display="Dynamic" ValidationGroup="update" CssClass="ColegioMedico"
                                                            ControlToValidate="ColegioProveedorTextBox" ErrorMessage="El Colegio Médico es requerido." />
                                                    </div>
                                                </div>
                                                <div id="DivCostoConsulta">
                                                    <asp:Label ID="Label1" Text="Costo de Consulta" runat="server" CssClass="label" />
                                                    <telerik:RadNumericTextBox ID="CostoConsultaTextBox" runat="server"
                                                        CssClass="normalField" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""
                                                        Value='<%# Bind("dCostoConsulta") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="CostoConsultaTextBox" CssClass="CostoConsulta"
                                                            ErrorMessage="El Costo de Consulta es requerido." />
                                                    </div>
                                                </div>
                                                <div id="DivPorcentageDescuento">
                                                    <asp:Label ID="Label3" Text="Porcentage de Descuento" runat="server" CssClass="label" />
                                                    <telerik:RadNumericTextBox ID="PorcentageDescuentoTextBox" runat="server"
                                                        CssClass="normalField" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""
                                                        Value='<%# Bind("dPorcentageDescuento") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                            Display="Dynamic" ValidationGroup="insert"
                                                            ControlToValidate="PorcentageDescuentoTextBox" CssClass="PorcentageDescuento"
                                                            ErrorMessage="El Porcentage de Descuento es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="NIT" runat="server" CssClass="label" />
                                                    <telerik:RadNumericTextBox ID="NitTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Nit") %>'
                                                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="NitTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update"
                                                            ControlToValidate="NitTextBox"
                                                            ErrorMessage="El NIT es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Dirección" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="DireccionTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Direccion") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="DireccionTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update"
                                                            ControlToValidate="DireccionTextBox"
                                                            ErrorMessage="La Dirección es requerida." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Teléfono de Casa" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="TelefonoCasaRadNumericTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("TelefonoCasa") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="TelefonoCasaRadNumericTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update"
                                                            ControlToValidate="TelefonoCasaRadNumericTextBox"
                                                            ErrorMessage="El Teléfono de Casa es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Teléfono de la Oficina" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="TelefonoOficinaRadNumericTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("TelefonoOficina") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="TelefonoOficinaRadNumericTextBoxRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update"
                                                            ControlToValidate="TelefonoOficinaRadNumericTextBox"
                                                            ErrorMessage="El Teléfono de la Oficina es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Teléfono Celular" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="CelularRadNumericTextBox" runat="server"
                                                        CssClass="normalField" Text='<%# Bind("Celular") %>' />
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="CelularRequiredFieldValidator" runat="server"
                                                            Display="Dynamic" ValidationGroup="update"
                                                            ControlToValidate="CelularRadNumericTextBox"
                                                            ErrorMessage="El Teléfono Celular es requerido." />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Estado" runat="server" CssClass="label" />
                                                    <telerik:RadComboBox ID="EstadoRadComboBox" runat="server"
                                                        ClientIDMode="Static" CssClass="normalField"
                                                        EmptyMessage="Selecciones un Estado"
                                                        SelectedValue='<%# Bind("Estado") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Activo" Value="ACTIVO" />
                                                            <telerik:RadComboBoxItem Text="Inactivo" Value="INACTIVO" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <div class="validation">
                                                        <asp:RequiredFieldValidator ID="CustomValidator2" runat="server"
                                                            ErrorMessage="Debe seleccionar un estado"
                                                            ControlToValidate="EstadoRadComboBox"
                                                            Display="Dynamic" ValidationGroup="update" />
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Label Text="Observaciones" runat="server" CssClass="label" />
                                                    <asp:TextBox ID="ObservacionesTextBox" runat="server"
                                                        CssClass="bigField" TextMode="MultiLine"
                                                        Text='<%# Bind("Observaciones") %>' />
                                                    <div class="validation"></div>
                                                </div>
                                            </div>
                                            <div class="buttonsPanel">
                                                <asp:LinkButton ID="UpdateProveedorLB" Text="" runat="server"
                                                    CssClass="button" OnClick="UpdateProveedorLB_Click" CommandName="Update">
                                    <span>Guardar</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="CancelUpdate" Text="Cancelar" runat="server"
                                                    OnClick="CancelUpdate_Click" CssClass="secondaryButton" />
                                            </div>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                </asp:FormView>
                            </div>
                            <asp:HiddenField ID="CodigoRedMedicaHF" runat="server" />
                            <asp:Panel ID="RedMedicaPanel" runat="server" Style="float: left; width: 70%;" GroupingText="Redes Médicas del Proveedor">
                                <div class="contentMenu" style='display: none;'>
                                    <div class="left">
                                        <asp:Label Text="Red Médica" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="RedMedicaDDL" runat="server"
                                            DataSourceID="RedMedicaODS" CssClass="bigField"
                                            DataValueField="RedMedicaId" DataTextField="Nombre"
                                            EmptyMessage="Seleccione una Red Médica">
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:RequiredFieldValidator ID="RedMedicaCV" runat="server" Display="Dynamic" ValidationGroup="InsertPRM"
                                                ControlToValidate="RedMedicaDDL" ErrorMessage="Debe seleccionar una Red Médica." />
                                        </div>
                                        <asp:ObjectDataSource ID="RedMedicaODS" runat="server" TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetRedMedicaListxCodigo"
                                            OnSelected="RedMedicaODS_Selected">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="CodigoRedMedicaHF" Name="Codigo" PropertyName="Value" DbType="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </div>
                                    <div class="addButton left">
                                        <asp:LinkButton runat="server" Style="color: #FFF;" ID="InsertProveedorRedMedica"
                                            CssClass="button" OnClick="InsertRedMedicaProveedorCiudadLB_Click" ValidationGroup="InsertPRM">
                                <span>Añadir</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="gridContainer">
                                    <asp:GridView ID="RedMedicaProveedorGridView" runat="server"
                                        DataSourceID="RedMedicaObjectDataSource" DataKeyNames="RedMedicaId"
                                        CssClass="grid" Width="100%" AutoGenerateColumns="false"
                                        OnRowCommand="RedMedicaProveedorGridView_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="deleteRow" Visible="false"
                                                        OnClientClick="return confirm('¿Está seguro de eliminar esta Red Médica?');"
                                                        CommandName="DeleteRecord" CommandArgument='<%# Bind("RedMedicaId") %>'>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Neutral/delete.png"
                                                            ToolTip="Eliminar" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                            <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <p style="background-color: white">
                                                No hay Redes Médicas Registradas en este proveedor.
                                            </p>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="RedMedicaObjectDataSource" runat="server"
                                        TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                                        SelectMethod="getRedMedicaListByProveedorId"
                                        OnSelected="RedMedicaObjectDataSource_Selected">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ProveedorIdHF" Name="ProveedorId" PropertyName="value" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="ProveedorCiudadPanel" runat="server" Style="float: left; width: 70%;" GroupingText="Direcciones por Ciudad">
                                <div class="contentMenu">
                                    <asp:HyperLink ID="newProveedorCiudad" Text="Nueva Dirección por Ciudad" runat="server"
                                        NavigateUrl="javascript:newProveedorCiudad(true, true);" />
                                </div>
                                <div class="gridContainer">
                                    <asp:GridView ID="CiudadGridView" runat="server"
                                        DataSourceID="CiudadDataSource" DataKeyNames="CiudadId"
                                        CssClass="grid" Width="100%" AutoGenerateColumns="false"
                                        OnRowCommand="CiudadGridView_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="deleteRow"
                                                        OnClientClick="return confirm('¿Está seguro de eliminar esta ciudad?');"
                                                        CommandName="DeleteRecord" CommandArgument='<%# Bind("CiudadId") %>'>
                                            <asp:Image runat="server" ImageUrl="~/Images/Neutral/delete.png"
                                                ToolTip="Eliminar" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="EditButton" runat="server" CssClass="deleteRow"
                                                        CommandName="EditRecord" CommandArgument='<%# Bind("CiudadId") %>'>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                                            ToolTip="Eliminar" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Ciudad" DataField="NombreCiudad" />
                                            <asp:BoundField HeaderText="Dirección" DataField="Direccion" />
                                            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                                            <asp:BoundField HeaderText="Celular" DataField="Celular" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <p style="background-color: white">
                                                No hay Ciudades Registradas en este proveedor.
                                            </p>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="CiudadDataSource" runat="server"
                                        TypeName="Artexacta.App.Proveedor.BLL.ProveedorCiudadBLL"
                                        SelectMethod="getProveedorCiudadByProveedorId"
                                        OnSelected="ProveedorCiudadODS_Selected">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ProveedorIdHF" Name="ProveedorId" PropertyName="value" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                                <asp:Panel runat="server" ID="ProveedorCiudadInsertPanel" ToolTip="Nueva Dirección por Ciudad" Style="display: none;"
                                    DefaultButton="InsertProveedorCiudadLB">
                                    <div>
                                        <div>
                                            <asp:Label Text="Ciudad" runat="server" CssClass="label" />
                                            <asp:Label ID="CiudadLabelText" Text="" runat="server" CssClass="normalField"
                                                Style="border: 1px solid #AAAAAA; height: 20px; display: none;" />
                                            <telerik:RadComboBox ID="CiudadRCB" runat="server"
                                                DataSourceID="CiudadODS" CssClass="normalField"
                                                DataValueField="CiudadId" DataTextField="Nombre"
                                                Text="" EmptyMessage="Seleccione una Ciudad">
                                            </telerik:RadComboBox>
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="CiudadCV" runat="server" Display="Dynamic" ValidationGroup="InsertPC"
                                                    ControlToValidate="CiudadRCB" ErrorMessage="Debe seleccionar una Ciudad." />
                                            </div>
                                            <asp:HiddenField runat="server" ID="CiudadesExistentesHF" />
                                            <asp:ObjectDataSource ID="CiudadODS" runat="server" TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                                                OldValuesParameterFormatString="original_{0}" SelectMethod="getCiudadList"
                                                OnSelected="CiudadODS_Selected">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="CiudadesExistentesHF" Name="ciudadesExistentes" PropertyName="value" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </div>
                                        <div>
                                            <asp:Label Text="Dirección" runat="server" CssClass="label" />
                                            <asp:TextBox ID="PCDireccionTextBox" runat="server" CssClass="normalField" Enabled="true" ReadOnly="false" />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="DireccionTextBoxRequiredFieldValidator" runat="server"
                                                    Display="Dynamic" ControlToValidate="PCDireccionTextBox" ValidationGroup="InsertPC"
                                                    ErrorMessage="La Dirección es requerida." />
                                            </div>
                                        </div>
                                        <div>
                                            <asp:Label Text="Teléfono" runat="server" CssClass="label" />
                                            <telerik:RadNumericTextBox ID="TelefonoRadNumericTextBox" runat="server"
                                                CssClass="normalField"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="TelefonoRadNumericTextBoxRequiredFieldValidator" runat="server"
                                                    Display="Dynamic" ControlToValidate="TelefonoRadNumericTextBox" ValidationGroup="InsertPC"
                                                    ErrorMessage="El Teléfono es requerido." />
                                            </div>
                                        </div>
                                        <div>
                                            <asp:Label Text="Teléfono Celular" runat="server" CssClass="label" />
                                            <telerik:RadNumericTextBox ID="CelularRadNumericTextBox" runat="server"
                                                CssClass="normalField"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="CelularRequiredFieldValidator" runat="server"
                                                    Display="Dynamic" ControlToValidate="CelularRadNumericTextBox" ValidationGroup="InsertPC"
                                                    ErrorMessage="El Teléfono Celular es requerido." />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="buttonsPanel">
                                        <asp:LinkButton ID="InsertProveedorCiudadLB" Text="" runat="server" Style="color: #FFF;"
                                            CssClass="button" OnClick="InsertProveedorCiudadLB_Click" ValidationGroup="InsertPC">
                                <span>Guardar</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="UpdateProveedorCiudadLB" Text="" runat="server" Style="color: #FFF; display: none;"
                                            CssClass="button" OnClick="UpdateProveedorCiudadLB_Click" ValidationGroup="InsertPC">
                                <span>Guardar</span>
                                        </asp:LinkButton>
                                        <asp:HyperLink ID="ReturnHL" runat="server" Text="Cancelar" ValidationGroup="CancelPC"
                                            NavigateUrl="javascript:newProveedorCiudad(false, true);" CssClass="secondaryButton" />
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <div class="clear"></div>
                        </div>

                    </div>

                </div>

            </telerik:RadPageView>

            <telerik:RadPageView ID="PrestacionesRPV" runat="server">
                <div>
                    <div style="width: 100%; margin-bottom: 7px">
                        <asp:DropDownList ID="TiposEstudiosDDL" runat="server"
                            AutoPostBack="true"
                            DataSourceID="TiposEstudiosODS"
                            DataTextField="nombre"
                            DataValueField="id">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="TiposEstudiosODS" runat="server"
                            TypeName="Artexacta.App.ProveedorPrestaciones.BLL.TiposEstudiosProvPrestacionesBLL"
                            SelectMethod="GetAllTipoPrestaciones"></asp:ObjectDataSource>
                    </div>

                    <%--ESTUDIOS--%>
                    <asp:Panel runat="server" ID="EstudioPNL" Visible="true">
                        <div style="width: 100%; margin-bottom: 7px">
                            <asp:DropDownList ID="EstudioDDL" runat="server"
                                AutoPostBack="true"
                                DataSourceID="EdtudioNotSavedODS"
                                DataTextField="Estudio"
                                DataValueField="EstudioId">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="EdtudioNotSavedODS" runat="server"
                                TypeName="Artexacta.App.ProveedorPrestaciones.BLL.RedProvLabImgCarPrestacionesBLL"
                                SelectMethod="GetProvLabImgCarPrestaciones">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TiposEstudiosDDL" Name="TipoEstudio" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <%--<asp:Button runat="server" ID="AgregarEstudioBTN" Text="Agregar" OnClick="AgregarEstudioBTN_Click" />--%>
                        </div>

                        <telerik:RadGrid ID="EstudioPrestacionesRadGrid" runat="server"
                            AutoGenerateColumns="false"
                            DataSourceID="EstudioSavedODS"
                            AllowPaging="true"
                            PageSize="20"
                            OnItemCommand="EstudioPrestacionesRadGrid_ItemCommand"
                            OnUpdateCommand="EstudioPrestacionesRadGrid_UpdateCommand"
                            OnItemCreated="EstudioPrestacionesRadGrid_ItemCreated"
                            Visible="true">
                            <MasterTableView DataKeyNames="detId, EstudioId, CategoriaId"
                                EditMode="InPlace">
                                <NoRecordsTemplate>
                                    <asp:Label runat="server" Text="No existen Estudios configuradas para la categoria seleccionada"></asp:Label>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridEditCommandColumn EditText="Seleccionar" CancelText="Cancelar" UpdateText="Guardar" />
                                    <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                        HeaderText="Eliminar"
                                        CommandName="Delete"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/delete.png"
                                        ConfirmText="¿Está seguro que desea Eliminar esta configuración de Estudio para la categoria seleccionada?" />

                                    <telerik:GridBoundColumn DataField="Estudio" UniqueName="Estudio" HeaderText="Estudio" ReadOnly="True" />
                                    <telerik:GridNumericColumn DataField="detPrecio" UniqueName="detPrecio" HeaderText="Precio" DataType="System.Decimal" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="EstudioSavedODS" runat="server"
                            TypeName="Artexacta.App.ProveedorPrestaciones.BLL.RedProvLabImgCarDetallePrestacionesBLL"
                            UpdateMethod="UpdateProvLabImgCarDetallePrestaciones"
                            SelectMethod="GetProvLabImgCarDetallePrestaciones">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ProveedorIdHF" Name="ProveedorId" PropertyName="Value" Type="Int32" />
                                <asp:ControlParameter ControlID="TiposEstudiosDDL" Name="TipoEstudio" PropertyName="SelectedValue" Type="String" />
                                <asp:ControlParameter ControlID="EstudioDDL" Name="ParentId" PropertyName="SelectedValue" DefaultValue="0" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                    <%-------------------%>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>

    <asp:ObjectDataSource ID="ProveedorODS" runat="server"
        TypeName="Artexacta.App.Proveedor.BLL.ProveedorBLL"
        DataObjectTypeName="Artexacta.App.Proveedor.Proveedor"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProveedorByIdNew"
        InsertMethod="InsertProveedorNew"
        UpdateMethod="UpdateProveedorNew"
        OnSelected="ProveedorODS_Selected"
        OnInserted="ProveedorODS_Inserted"
        OnUpdated="ProveedorODS_Updated">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProveedorIdHF" Name="ProveedorId" PropertyName="value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function newProveedorCiudad(show, newRecord) {
                var defaultButton = '';
                if (newRecord) {
                    var combo = $find("<%= CiudadRCB.ClientID %>");
                    if (combo)
                        combo.clearSelection();
                    $('#<%=PCDireccionTextBox.ClientID%>').val('');
                    var ntbtel = $find("<%= TelefonoRadNumericTextBox.ClientID %>");
                    if (ntbtel)
                        ntbtel.clear();
                    var ntbcel = $find("<%= CelularRadNumericTextBox.ClientID %>");
                    if (ntbcel)
                        ntbcel.clear();
                    $('div.validation span').hide();

                    $('#<%=CiudadLabelText.ClientID%>').hide();
                    $('#<%=CiudadRCB.ClientID%>').show();
                    $('#<%=UpdateProveedorCiudadLB.ClientID%>').hide();
                    $('#<%=InsertProveedorCiudadLB.ClientID%>').show();
                    $('#<%=ProveedorCiudadInsertPanel%>').attr('title', 'Nueva Dirección por Ciudad');
                    defaultButton = '<%=InsertProveedorCiudadLB.ClientID%>';
                }
                else {
                    $('#<%=CiudadLabelText.ClientID%>').css('display', 'block');
                    $('#<%=CiudadRCB.ClientID%>').hide();
                    $('#<%=UpdateProveedorCiudadLB.ClientID%>').show();
                    $('#<%=InsertProveedorCiudadLB.ClientID%>').hide();
                    $('#<%=ProveedorCiudadInsertPanel%>').attr('title', 'Editar Dirección por Ciudad');
                    defaultButton = '<%=UpdateProveedorCiudadLB.ClientID%>';
                }
                $('#<%=ProveedorCiudadInsertPanel%>').keypress(function () {
                    return WebForm_FireDefaultButton(event, defaultButton);
                });
                if (show) {
                    var myDialog = $('#<%=ProveedorCiudadInsertPanel.ClientID%>').dialog({ modal: true });
                    myDialog.parent().appendTo(jQuery("form:first"));
                } else {
                    $('#<%=ProveedorCiudadInsertPanel.ClientID%>').dialog('close');
                }
            }
        </script>
        <%if (ProveedorFV.CurrentMode != FormViewMode.ReadOnly)
            { %>
        <script type="text/javascript">
            function TipoProveedorRadComboBox_ClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                verifyType(item.get_value());
            }

            function enableDisable(id, enable) {
                var div = $('#Div' + id);
                var validator = $('.' + id);

                ValidatorEnable(document.getElementById(validator.attr('id')), enable);
                validator.hide();

                if (enable) {
                    div.show();
                } else {
                    div.hide();
                }
            }


            function verifyType(selectedTipe) {
                var isMedico = (selectedTipe == 'MEDICO');
                enableDisable('NombreJuridico', !isMedico);
                enableDisable('Nombres', isMedico);
                enableDisable('Apellidos', isMedico);
                enableDisable('Especialidad', isMedico);
                enableDisable('Sedes', isMedico);
                enableDisable('ColegioMedico', isMedico);
                enableDisable('CostoConsulta', isMedico);
                enableDisable('PorcentageDescuento', isMedico);
            }
            $(document).ready(function () {
                verifyType('<%= (ProveedorFV.FindControl("TipoProveedorRadComboBox") as RadComboBox).SelectedValue%>');
            });
        </script>
        <%} %>
    </telerik:RadCodeBlock>
    <asp:HiddenField ID="ProveedorIdHF" runat="server" />
    <asp:HiddenField ID="CiudadIdHF" runat="server" />
    <asp:HiddenField ID="UserIDHF" runat="server" />
    <asp:HiddenField ID="UserNameHF" runat="server" />
</asp:Content>

