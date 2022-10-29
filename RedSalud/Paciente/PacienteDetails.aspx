<%@ Page Title="Paciente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PacienteDetails.aspx.cs" Inherits="Paciente_PacienteDetails" %>

<%@ Register Src="~/UserControls/AngularControl.ascx" TagPrefix="RedSalud" TagName="AngularControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angularControllerCreator.js"></script>
    <style>
        .AntecedentesGinecoobstetricosPanel,
        .ExpandCollapse 
        {
            width: 665px;
        }
        .AntecedentesGinecoobstetricosPanel .ControlPanel,
        .AntecedentesGinecoobstetricosPanel .ControlPanel ul
        {
            width: 630px;
        }
        .AntecedentesGinecoobstetricosPanel .ControlPanel div,
        .AntecedentesGinecoobstetricosPanel .ControlPanel li
        {    
            display: inline-block;
            margin-right: 10px;
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn" ng-app>
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title"
                    Text="Nuevo Paciente">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Pacientes"
                        NavigateUrl="~/Paciente/PacienteList.aspx">
                    </asp:HyperLink>
                </div>

                <asp:FormView ID="PacienteFV" runat="server"
                    DataSourceID="PacienteODS"
                    OnItemInserting="PacienteFV_ItemInserting"
                    OnItemCreated="PacienteFV_ItemCreated">
                    <ItemTemplate>
                        <asp:HiddenField ID="PacienteID" runat="server" Value='<%# Bind("PacienteId") %>' />
                        <div>
                            <span class="label">Identificador</span>
                            <asp:Label runat="server"
                                CssClass="normalField"
                                Text='<%# int.Parse("0" + Eval("PacienteId")).ToString("00000") %>' />

                            <span class="label">Nombre</span>
                            <asp:Label ID="NombreTxt" runat="server"
                                CssClass="normalField"
                                Text='<%# Bind("Nombre") %>' />

                           <%-- <span class="label">Apellido</span>
                            <asp:Label ID="ApellidoTxt" runat="server"
                                CssClass="normalField"
                                Text='<%# Bind("Apellido") %>' />--%>

                            <span class="label">Carnet de Identidad</span>
                            <asp:Label ID="CITxt" runat="server"
                                CssClass="normalField"
                                Text='<%# Bind("CarnetIdentidad") %>' />

                            <span class="label">Fecha de nacimiento</span>
                            <asp:Label Text='<%# Eval("FechaNacimientoString") %>' runat="server" />

                            <span class="label">Género</span>
                            <asp:Label Text='<%# Eval("GeneroForDisplay") %>' runat="server" />

                            <span class="label">Dirección</span>
                            <asp:Label ID="DireccionTxt" runat="server"
                                CssClass="bigField"
                                Text='<%# Bind("Direccion") %>' />

                            <span class="label">Teléfono</span>
                            <asp:Label ID="TelefonoTxt" runat="server"
                                CssClass="normalField"
                                Text='<%# Bind("Telefono") %>' />

                            <span class="label">Lugar de Trabajo</span>
                            <asp:Label ID="LugarTrabajoTxt" runat="server"
                                CssClass="bigField"
                                Text='<%# Bind("LugarTrabajo") %>' />

                            <span class="label">Teléfono de la Empresa</span>
                            <asp:Label ID="TelefonoTrabajoTxt" runat="server"
                                CssClass="normalField"
                                Text='<%# Bind("TelefonoTrabajo") %>' />

                            <span class="label">Celular</span>
                            <asp:Label ID="Label3" runat="server"
                                CssClass="normalField"
                                Text='<%# Bind("Celular") %>' />

                            <span class="label">Email</span>
                            <asp:Label ID="EmailTxt" runat="server"
                                CssClass="bigField"
                                Text='<%# Bind("Email") %>' />

                            <span class="label">Estado Civil</span>
                            <asp:Label Text='<%# Eval("EstadoCivil") %>' runat="server" Style="text-transform: capitalize;" />

                            <span class="label">Número de Hijos</span>
                            <asp:Label ID="NroHijosTxt" runat="server"
                                Text='<%# Bind("NroHijos") %>' />

                            <span class="label">Usuario</span>
                            <asp:Label ID="Label4" runat="server"
                                Text='<%# Bind("UsuarioMovil") %>' />

                            <span class="label">Usuario verificado</span>
                            <asp:Label ID="Label5" runat="server"
                                Text='<%# (Eval("UsuarioVerificado").Equals(true) ? "Si" : "No") %>' />

                            <asp:Panel runat="server" GroupingText="Antecedentes Familiares" CssClass="ExpandCollapse">
                                <div style="display: none;">
                                    <RedSalud:AngularControl runat="server" ID="Antecedentes" readOnly="true" maxLength="620" JSonData='<%# Bind("Antecedentes") %>' />
                                </div>
                            </asp:Panel>

                            <%--<span class="label">Antecedentes Alérgicos</span>
                            <asp:Label ID="AlergiasRadEditor" runat="server"
                                Text='<%# Bind("AntecedentesAlergicos") %>'>
                            </asp:Label>--%>

                            <asp:Panel runat="server" GroupingText="Antecedentes Ginecoobstetricos" Visible='<%# !bool.Parse(Eval("Genero").ToString()) %>' 
                                CssClass="ExpandCollapse AntecedentesGinecoobstetricosPanel">
                                <div style="display: none;">
                                    <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoobstetricos" readOnly="true" maxLength="1950"
                                        JSonData='<%# Bind("AntecedentesGinecoobstetricos") %>' />
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="buttonsPanel">
                            <asp:LinkButton Text="" runat="server"
                                CommandName="Edit"
                                CssClass="button">
                                <span>Modificar</span>
                            </asp:LinkButton>
                            <asp:HyperLink NavigateUrl="~/Paciente/PacienteList.aspx" runat="server"
                                CssClass="secondaryButton" Text="Cancelar" />
                        </div>

                        
                        <asp:Panel ID="NewPolizaPanel" runat="server" CssClass="NewPolizaPanel"
                            ToolTip="Agregar Póliza al Paciente" DefaultButton="SavePolizaPacienteLB" style="display: none;">
                            <%--<asp:HiddenField ID="CodigoHF" runat="server" />--%>
                            <asp:Label ID="Label1" Text="Cliente" runat="server" CssClass="label" />
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
                                    ValidationGroup="InsertPoliza"
                                    ErrorMessage="Debe seleccionar un Cliente."
                                    ClientValidationFunction="ClienteCV_Validate"
                                    Display="Dynamic" />
                            </div>

                            <asp:ObjectDataSource ID="ClienteODS" runat="server"
                                TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                                OldValuesParameterFormatString="original_{0}"
                                SelectMethod="getRedClienteList"
                                OnSelected="ClienteODS_Selected" />

                            <span class="label">Fecha de Inicio</span>
                            <telerik:RadDatePicker ID="FechaInicio" runat="server"
                                EnableTyping="true"
                                ShowPopupOnFocus="true"
                                EnableShadows="true"
                                CssClass="normalField">
                            </telerik:RadDatePicker>
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="DateRFV" runat="server"
                                    ControlToValidate="FechaInicio"
                                    ErrorMessage="Debe seleccionar la fecha de inicio de la Póliza."
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza">
                                </asp:RequiredFieldValidator>
                            </div>

                            <span class="label">Fecha de Conclusión</span>
                            <telerik:RadDatePicker ID="FechaFin" runat="server"
                                EnableTyping="true"
                                ShowPopupOnFocus="true"
                                EnableShadows="true"
                                CssClass="normalField">
                            </telerik:RadDatePicker>
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="FechaFin"
                                    ErrorMessage="Debe seleccionar la fecha de conlusión de la Póliza."
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza">
                                </asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="FechaFinCV" runat="server"
                                    ControlToValidate="FechaFin"
                                    ErrorMessage="La Fecha de conlusión debe ser mayor a la fecha de inicio"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ClientValidationFunction="FechaFinCV_Validate" />
                            </div>

                            <span class="label">Número de Póliza</span>
                            <asp:TextBox ID="NroPolizaTxt" runat="server"
                                CssClass="normalField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="NroPolizaTxt"
                                    ErrorMessage="El Número de Póliza es requerido." />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="NroPolizaTxt"
                                    ErrorMessage="El Número de Póliza no puede exceder los 20 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                            </div>

                            <span class="label">Monto Total de la Póliza</span>
                            <asp:TextBox ID="MontoTotalText" runat="server"
                                CssClass="normalField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="MontoTotalText"
                                    ErrorMessage="El Monto total de la Póliza es requerido." />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="MontoTotalText"
                                    ErrorMessage="El Monto solo puede contener números decimales."
                                    ValidationExpression="<%$ Resources: Validations, DecimalFormat  %>" />
                            </div>

                            <span class="label">Monto para Farmacia</span>
                            <telerik:RadNumericTextBox ID="MontoFarmaciaText" runat="server" 
                                MinValue="-1"
                                CssClass="normalField" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="MontoFarmaciaText"
                                    ErrorMessage="El Monto para Farmacia de la Póliza es requerido." />
                            </div>

                            <span class="label">Cobertura de la Póliza</span>
                            <asp:TextBox ID="CoberturaText" runat="server"
                                CssClass="bigField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="CoberturaText"
                                    ErrorMessage="La Cobertura de la Póliza es requerida." />
                                <asp:CustomValidator ID="RegularExpressionValidator2" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="NombrePlanText"
                                    ErrorMessage="El Formato de la Cobertura no es Correcto (Ejemplo: 80/20)."
                                    ClientValidationFunction="Cobertura_Validate" />
                            </div>

                            <span class="label">Nombre del Plan de la Póliza</span>
                            <asp:TextBox ID="NombrePlanText" runat="server"
                                CssClass="bigField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="NombreRFV" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="NombrePlanText"
                                    ErrorMessage="El Nombre del plan es requerido." />
                                <asp:RegularExpressionValidator ID="NombreREVLength" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="NombrePlanText"
                                    ErrorMessage="El Nombre del plan no puede exceder los 100 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                <asp:RegularExpressionValidator ID="NombreREVFormat" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="NombrePlanText"
                                    ErrorMessage="Caracteres inválidos en el Nombre del plan."
                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                            </div>

                            <span class="label">Lugar de la Póliza</span>
                            <asp:DropDownList ID="LugarDDL" runat="server"
                                CssClass="normalField">
                                <asp:ListItem Text="SANTA CRUZ" Value="SCZ" />
                                <asp:ListItem Text="LA PAZ" Value="LPZ" />
                                <asp:ListItem Text="COCHABAMBA" Value="CBBA" />
                            </asp:DropDownList>
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="LugarRFV" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="LugarDDL"
                                    ErrorMessage="El Lugar de la póliza es requerido." />
                            </div>

                            <span class="label">Código Asegurado</span>
                            <asp:TextBox ID="CodigoAseguradoText" runat="server"
                                CssClass="bigField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="CodigoAseguradoText"
                                    ErrorMessage="El Código del Asegurado es requerido." />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="CodigoAseguradoText"
                                    ErrorMessage="El Código del Asegurado no puede exceder los 50 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength50  %>" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="InsertPoliza"
                                    ControlToValidate="CodigoAseguradoText"
                                    ErrorMessage="Caracteres inválidos en el Código del Asegurado."
                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                            </div>
                            <span class="label">Relación con el Asegurado</span>
                            <telerik:RadComboBox ID="RelacionDDL" runat="server"
                                CssClass="normalField">
                                <Items>
                                    <telerik:RadComboBoxItem Text="TITULAR" Value="TITULAR" />
                                    <telerik:RadComboBoxItem Text="ESPOSA" Value="ESPOSA" />
                                    <telerik:RadComboBoxItem Text="ESPOSO" Value="ESPOSO" />
                                    <telerik:RadComboBoxItem Text="HIJO" Value="HIJO" />
                                    <telerik:RadComboBoxItem Text="HIJA" Value="HIJA" />
                                </Items>
                            </telerik:RadComboBox>
                            <div class="buttonsPanel">
                                <asp:LinkButton ID="SavePolizaPacienteLB" Text="" runat="server"
                                    ValidationGroup="InsertPoliza"
                                    CssClass="button"
                                    OnClick="SavePolizaPacienteLB_Click">
                                    <span style="color: #FFF;">Agregar Poliza para el paciente</span>
                                </asp:LinkButton>
                                <asp:HyperLink ID="HyperLink2" runat="server" Text="Cancelar"
                                    NavigateUrl="javascript:closeNewPoliza();">
                                </asp:HyperLink>
                            </div>
                        </asp:Panel>

                        <script type="text/javascript">
                            function FechaFinCV_Validate(sender, args) {
                                args.IsValid = true;
                                var FechaInicio = $find('<%= PacienteFV.FindControl("FechaInicio").ClientID %>').get_selectedDate();
                                var FechaFin = $find('<%= PacienteFV.FindControl("FechaFin").ClientID %>').get_selectedDate();

                                if (FechaFin < FechaInicio) {
                                    args.IsValid = false;
                                }
                            }
                            function Cobertura_Validate(sender, args) {
                                args.IsValid = false;

                                if ($('#<%= PacienteFV.FindControl("CoberturaText").ClientID %>').val().match(/^[0-9]{2}[/][0-9]{2}$/) != null) {
                                    args.IsValid = true;
                                }
                            }
                            function openNewPoliza() {
                                $('.NewPolizaPanel').dialog({ modal: true, width: 350 });
                                $('.ui-widget-overlay').height($(document).height());
                                $('form').append($('.ui-dialog'));
                            }
                            function closeNewPoliza() {
                                $('.NewPolizaPanel').dialog('destroy');
                            }
                        </script>
                        <asp:Panel ID="Panel6" runat="server" GroupingText="Lista de Pólizas del Paciente">
                            <asp:HyperLink ID="NewPoliza" runat="server" Text="Añadir nueva Poliza"
                                NavigateUrl="javascript:openNewPoliza();">
                            </asp:HyperLink>
                            <asp:GridView ID="PolizaGV" runat="server"
                                DataSourceID="PacientePolizaODS"
                                AutoGenerateColumns="false"
                                OnRowCommand="PolizaGV_RowCommand"
                                OnRowDataBound="PolizaGV_RowDataBound"
                                PageSize="5">
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label2" Text="El Paciente no tiene Poliza" runat="server" />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Eliminar"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="EliminarPolizaIB" ImageUrl="~/Images/neutral/delete.png" runat="server"
                                                CommandName="Eliminar" CommandArgument='<%# Bind("PolizaId") %>'
                                                OnClientClick="return confirm('¿Está seguro que desea eliminar la Poliza?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreJuridicoCliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="FechaFinString" HeaderText="Fecha Conclusión" />
                                    <asp:BoundField DataField="NumeroPoliza" HeaderText="Número de Póliza" />
                                    <asp:BoundField DataField="MontoTotalForDisplay" HeaderText="Monto" />
                                    <asp:BoundField DataField="MontoFarmaciaForDisplay" HeaderText="Monto para Farmacia" />
                                    <asp:BoundField DataField="NombrePlan" HeaderText="NombrePlan" />
                                    <asp:BoundField DataField="GastoTotal" HeaderText="Gasto Total" />
                                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="CodigoAsegurado" HeaderText="Código Asegurado" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="TotalPolizasHF" runat="server" />
                            <asp:ObjectDataSource ID="PacientePolizaODS" runat="server"
                                TypeName="Artexacta.App.Poliza.BLL.PolizaBLL"
                                OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetPolizaByPacienteId"
                                OnSelected="PacientePolizaODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="value" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="InsertPacienteLB">
                            <div>
                                <span class="label">Nombre</span>
                                <asp:TextBox ID="NombreTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Nombre") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="NombreRFV" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NombreTxt"
                                        ErrorMessage="El Nombre es requerido." />
                                    <asp:RegularExpressionValidator ID="NombreREVLength" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NombreTxt"
                                        ErrorMessage="El Nombre no puede exceder los 100 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                    <asp:RegularExpressionValidator ID="NombreREVFormat" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NombreTxt"
                                        ErrorMessage="Caracteres inválidos en el Nombre."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>

                                <%--<span class="label">Apellido</span>
                                <asp:TextBox ID="ApellidoTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Apellido") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="ApellidoRFV" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ApellidoTxt"
                                        ErrorMessage="El Apellido es requerido." />
                                    <asp:RegularExpressionValidator ID="ApellidoREV1" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ApellidoTxt"
                                        ErrorMessage="El Apellido no puede exceder los 100 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                    <asp:RegularExpressionValidator ID="ApellidoREV2" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ApellidoTxt"
                                        ErrorMessage="Caracteres inválidos en el Apellido."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>--%>

                                <span class="label">Carnet de Identidad</span>
                                <asp:TextBox ID="CITxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("CarnetIdentidad") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="CITxt"
                                        ErrorMessage="El Carnet de Identidad no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                </div>

                                <span class="label">Fecha de nacimiento</span>
                                <telerik:RadDatePicker ID="FechaNacimiento" runat="server"
                                    EnableTyping="true"
                                    ShowPopupOnFocus="true"
                                    EnableShadows="true"
                                    MinDate="01/01/1900"
                                    SelectedDate='<%# Bind("FechaNacimiento") %>'
                                    CssClass="normalField">
                                </telerik:RadDatePicker>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="DateRFV" runat="server"
                                        ControlToValidate="FechaNacimiento"
                                        ErrorMessage="debe seleccionar la fecha de nacimiento."
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <span class="label">Género</span>
                                <asp:RadioButtonList ID="Genero" runat="server" SelectedValue='<%# Bind("Genero") %>'
                                    CssClass="GeneroRadioButtonList">
                                    <asp:ListItem Text="Masculino" Value="True" />
                                    <asp:ListItem Text="Femenino" Value="False" />
                                </asp:RadioButtonList>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="GeneroRequiredFieldValidator" runat="server"
                                        ControlToValidate="Genero"
                                        ErrorMessage="debe seleccionar el género del paciente."
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <span class="label">Dirección</span>
                                <asp:TextBox ID="DireccionTxt" runat="server"
                                    CssClass="bigField"
                                    Text='<%# Bind("Direccion") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="DireccionTxt"
                                        ErrorMessage="La Dirección es requerido." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="DireccionTxt"
                                        ErrorMessage="La Dirección no puede exceder los 250 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength250  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="DireccionTxt"
                                        ErrorMessage="Caracteres inválidos en la Dirección."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>

                                <span class="label">Telefono</span>
                                <asp:TextBox ID="TelefonoTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Telefono") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTxt"
                                        ErrorMessage="El Telefono es requerido." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTxt"
                                        ErrorMessage="El Telefono no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTxt"
                                        ErrorMessage="Caracteres inválidos en el Telefono."
                                        ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />
                                </div>

                                <span class="label">Lugar de Trabajo</span>
                                <asp:TextBox ID="LugarTrabajoTxt" runat="server"
                                    CssClass="bigField"
                                    Text='<%# Bind("LugarTrabajo") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="LugarTrabajoTxt"
                                        ErrorMessage="El Lugar de Trabajo no puede exceder los 250 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength250  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="LugarTrabajoTxt"
                                        ErrorMessage="Caracteres inválidos en el Lugar de Trabajo."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>

                                <span class="label">Telefono de la Empresa</span>
                                <asp:TextBox ID="TelefonoTrabajoTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("TelefonoTrabajo") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTrabajoTxt"
                                        ErrorMessage="El Telefono de la Empresa no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTrabajoTxt"
                                        ErrorMessage="Caracteres inválidos en el Telefono de la Empresa."
                                        ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />

                                <span class="label">Celular</span>
                                <asp:TextBox ID="txtCelular" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Celular") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtCelular"
                                        ErrorMessage="El celular no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtCelular"
                                        ErrorMessage="Caracteres inválidos en el Telefono de la Empresa."
                                        ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />
                                </div>

                                <span class="label">Email</span>
                                <asp:TextBox ID="EmailTxt" runat="server"
                                    CssClass="bigField"
                                    Text='<%# Bind("Email") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="EmailTxt"
                                        ErrorMessage="El Email no pueden exceder los 100 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="EmailTxt"
                                        ErrorMessage="El Email no tiene el formato correcto."
                                        ValidationExpression="<%$ Resources: Validations, EMailFormat  %>" />
                                </div>

                                <span class="label">Estado Civil</span>
                                <telerik:RadComboBox ID="EstadoCivilDDL" runat="server"
                                    ClientIDMode="Static"
                                    EmptyMessage="Selecciones un estado civil"
                                    CssClass="bigField"
                                    SelectedValue='<%# Bind("EstadoCivil") %>'>
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Casado" Value="CASADO" />
                                        <telerik:RadComboBoxItem Text="Divorciado" Value="DIVORCIADO" />
                                        <telerik:RadComboBoxItem Text="Soltero" Value="SOLTERO" />
                                        <telerik:RadComboBoxItem Text="Viudo" Value="VIUDO" />
                                        <telerik:RadComboBoxItem Text="Casada" Value="CASADA" />
                                        <telerik:RadComboBoxItem Text="Divorciada" Value="DIVORCIADA" />
                                        <telerik:RadComboBoxItem Text="Soltera" Value="SOLTERA" />
                                        <telerik:RadComboBoxItem Text="Viuda" Value="VIUDA" />
                                    </Items>
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                                        ErrorMessage="Debe seleccionar un estado civil."
                                        ClientValidationFunction="EstadoCivilDDLCV_Validate"
                                        Display="Dynamic" />
                                </div>

                                <span class="label">Número de Hijos</span>
                                <asp:TextBox ID="NroHijosTxt" runat="server"
                                    CssClass="smallField"
                                    Text='<%# Bind("NroHijos") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NroHijosTxt"
                                        ErrorMessage="El Nro. de Hijos no puede exceder los 10 digitos."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength10  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NroHijosTxt"
                                        ErrorMessage="Nro. de Hijos solo permite números enteros."
                                        ValidationExpression="<%$Resources:Validations, IntegerFormat %>" />
                                </div>

                                <br />
                                <asp:Panel ID="Panel3" runat="server" GroupingText="Antecedentes Familiares"
                                    CssClass="ExpandCollapse">
                                    <div style="display: none;">
                                        <RedSalud:AngularControl runat="server" ID="Antecedentesfamiliares" readOnly="false" maxLength="620" JSonData='<%# Bind("Antecedentes") %>'
                                            JSonDefaultData='<%# defaultJSonAntecedentes %>' />
                                    </div>
                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server" GroupingText="Antecedentes Ginecoobstetricos"
                                    CssClass="ExpandCollapse AntecedentesGinecoobstetricosPanel">
                                    <div style="display: none;">
                                        <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoobstetricos" readOnly="false" maxLength="1950"
                                            JSonData='<%# Bind("AntecedentesGinecoobstetricos") %>' />
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="buttonsPanel">
                                <asp:LinkButton ID="InsertPacienteLB" Text="" runat="server"
                                    CssClass="button"
                                    CommandName="Insert">
                                        <span>Guardar</span>
                                </asp:LinkButton>
                                <asp:HyperLink ID="ReturnHL" runat="server"
                                    Text="Retornar a la lista de Pacientes"
                                    NavigateUrl="~/Paciente/PacienteList.aspx"
                                    CssClass="secondaryButton" />
                            </div>
                        </asp:Panel>
                    </InsertItemTemplate>

                    <EditItemTemplate>
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="UpdatePacienteLB">
                            <asp:HiddenField ID="PacienteID" runat="server" Value='<%# Bind("PacienteId") %>' />
                            <div>
                                <span class="label">Identificador</span>
                                <asp:Label runat="server"
                                    CssClass="normalField"
                                    Text='<%# int.Parse("0" + Eval("PacienteId")).ToString("00000") %>' />

                                <span class="label">Nombre</span>
                                <asp:TextBox ID="NombreTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Nombre") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="NombreRFV" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NombreTxt"
                                        ErrorMessage="El Nombre es requerido." />
                                    <asp:RegularExpressionValidator ID="NombreREVLength" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NombreTxt"
                                        ErrorMessage="El Nombre no puede exceder los 100 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                    <asp:RegularExpressionValidator ID="NombreREVFormat" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NombreTxt"
                                        ErrorMessage="Caracteres inválidos en el Nombre."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>

                                <%--<span class="label">Apellido</span>
                                <asp:TextBox ID="ApellidoTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Apellido") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="ApellidoRFV" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ApellidoTxt"
                                        ErrorMessage="El Apellido es requerido." />
                                    <asp:RegularExpressionValidator ID="ApellidoREV1" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ApellidoTxt"
                                        ErrorMessage="El Apellido no puede exceder los 100 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                    <asp:RegularExpressionValidator ID="ApellidoREV2" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ApellidoTxt"
                                        ErrorMessage="Caracteres inválidos en el Apellido."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>--%>

                                <span class="label">Carnet de Identidad</span>
                                <asp:TextBox ID="CITxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("CarnetIdentidad") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="CITxt"
                                        ErrorMessage="El Carnet de Identidad no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                </div>

                                <span class="label">Fecha de nacimiento</span>
                                <telerik:RadDatePicker ID="FechaNacimiento" runat="server"
                                    EnableTyping="true"
                                    ShowPopupOnFocus="true"
                                    EnableShadows="true"
                                    MinDate="01/01/1900"
                                    SelectedDate='<%# Bind("FechaNacimiento") %>'
                                    CssClass="normalField">
                                </telerik:RadDatePicker>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="DateRFV" runat="server"
                                        ControlToValidate="FechaNacimiento"
                                        ErrorMessage="debe seleccionar la fecha de nacimiento."
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <span class="label">Género</span>
                                <asp:RadioButtonList ID="Genero" runat="server" SelectedValue='<%# Bind("Genero") %>'
                                    CssClass="GeneroRadioButtonList">
                                    <asp:ListItem Text="Masculino" Value="True" />
                                    <asp:ListItem Text="Femenino" Value="False" />
                                </asp:RadioButtonList>
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="GeneroRequiredFieldValidator" runat="server"
                                        ControlToValidate="Genero"
                                        ErrorMessage="debe seleccionar el género del paciente."
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <span class="label">Dirección</span>
                                <asp:TextBox ID="DireccionTxt" runat="server"
                                    CssClass="bigField"
                                    Text='<%# Bind("Direccion") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="DireccionTxt"
                                        ErrorMessage="La Dirección es requerido." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="DireccionTxt"
                                        ErrorMessage="La Dirección no puede exceder los 250 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength250  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="DireccionTxt"
                                        ErrorMessage="Caracteres inválidos en la Dirección."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>

                                <span class="label">Telefono</span>
                                <asp:TextBox ID="TelefonoTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Telefono") %>' />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTxt"
                                        ErrorMessage="El Telefono es requerido." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTxt"
                                        ErrorMessage="El Telefono no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTxt"
                                        ErrorMessage="Caracteres inválidos en el Telefono."
                                        ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />
                                </div>

                                <span class="label">Lugar de Trabajo</span>
                                <asp:TextBox ID="LugarTrabajoTxt" runat="server"
                                    CssClass="bigField"
                                    Text='<%# Bind("LugarTrabajo") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="LugarTrabajoTxt"
                                        ErrorMessage="El Lugar de Trabajo no puede exceder los 250 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength250  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="LugarTrabajoTxt"
                                        ErrorMessage="Caracteres inválidos en el Lugar de Trabajo."
                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                </div>

                                <span class="label">Telefono de la Empresa</span>
                                <asp:TextBox ID="TelefonoTrabajoTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("TelefonoTrabajo") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTrabajoTxt"
                                        ErrorMessage="El Telefono de la Empresa no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="TelefonoTrabajoTxt"
                                        ErrorMessage="Caracteres inválidos en el Telefono de la Empresa."
                                        ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />
                                </div>

                                <span class="label">Celular</span>
                                <asp:TextBox ID="CelularEditTxt" runat="server"
                                    CssClass="normalField"
                                    Text='<%# Bind("Celular") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="CelularEditTxt"
                                        ErrorMessage="El celular no puede exceder los 20 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="CelularEditTxt"
                                        ErrorMessage="Caracteres inválidos en el Telefono de la Empresa."
                                        ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />
                                </div>

                                <span class="label">Email</span>
                                <asp:TextBox ID="EmailTxt" runat="server"
                                    CssClass="bigField"
                                    Text='<%# Bind("Email") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="EmailTxt"
                                        ErrorMessage="El Email no pueden exceder los 100 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength100  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="EmailTxt"
                                        ErrorMessage="El Email no tiene el formato correcto."
                                        ValidationExpression="<%$ Resources: Validations, EMailFormat  %>" />
                                </div>

                                <span class="label">Estado Civil</span>
                                <telerik:RadComboBox ID="EstadoCivilDDL" runat="server"
                                    ClientIDMode="Static"
                                    EmptyMessage="Selecciones un estado civil"
                                    CssClass="bigField"
                                    SelectedValue='<%# Bind("EstadoCivil") %>'>
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Casado" Value="CASADO" />
                                        <telerik:RadComboBoxItem Text="Divorciado" Value="DIVORCIADO" />
                                        <telerik:RadComboBoxItem Text="Soltero" Value="SOLTERO" />
                                        <telerik:RadComboBoxItem Text="Viudo" Value="VIUDO" />
                                        <telerik:RadComboBoxItem Text="Casada" Value="CASADA" />
                                        <telerik:RadComboBoxItem Text="Divorciada" Value="DIVORCIADA" />
                                        <telerik:RadComboBoxItem Text="Soltera" Value="SOLTERA" />
                                        <telerik:RadComboBoxItem Text="Viuda" Value="VIUDA" />
                                    </Items>
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                                        ErrorMessage="Debe seleccionar un estado civil."
                                        ClientValidationFunction="EstadoCivilDDLCV_Validate"
                                        Display="Dynamic" />
                                </div>

                                <span class="label">Número de Hijos</span>
                                <asp:TextBox ID="NroHijosTxt" runat="server"
                                    CssClass="smallField"
                                    Text='<%# Bind("NroHijos") %>' />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NroHijosTxt"
                                        ErrorMessage="El Nro. de Hijos no puede exceder los 10 digitos."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength10  %>" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="NroHijosTxt"
                                        ErrorMessage="Nro. de Hijos solo permite números enteros."
                                        ValidationExpression="<%$Resources:Validations, IntegerFormat %>" />
                                </div>

                                <br />
                                <asp:Panel ID="Panel3" runat="server" GroupingText="Antecedentes Familiares"
                                    CssClass="ExpandCollapse">
                                    <div style="display: none;">
                                        <RedSalud:AngularControl runat="server" ID="Antecedentesfamiliares" readOnly="false" maxLength="620" JSonData='<%# Bind("Antecedentes") %>'
                                            JSonDefaultData='<%# defaultJSonAntecedentes %>' />
                                    </div>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="Panel4" runat="server" GroupingText="Antecedentes Ginecoobstetricos"
                                    CssClass="ExpandCollapse AntecedentesGinecoobstetricosPanel">
                                    <div style="display: none;">
                                        <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoobstetricos" readOnly="false" maxLength="1950"
                                            JSonData='<%# Bind("AntecedentesGinecoobstetricos") %>' />
                                    </div>
                                </asp:Panel>                                                              
                            </div>

                            <div class="buttonsPanel">
                                <asp:LinkButton ID="UpdatePacienteLB" Text="" runat="server"
                                    CssClass="button"
                                    CommandName="Update">
                                        <span>Guardar</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" Text="Cancelar" runat="server"
                                    ValidationGroup="empty"
                                    CommandName="Cancel" CssClass="secondaryButton" />
                            </div>
                        </asp:Panel>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </div>
    </div>



    <asp:ObjectDataSource ID="PacienteODS" runat="server"
        TypeName="Artexacta.App.Paciente.BLL.PacienteBLL"
        DataObjectTypeName="Artexacta.App.Paciente.Paciente"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPacienteByPacienteId"
        InsertMethod="InsertPaciente"
        UpdateMethod="UpdatePacienteComplete"
        OnSelected="PacienteODS_Selected"
        OnInserted="PacienteODS_Inserted"
        OnUpdated="PacienteODS_Updated">
        <SelectParameters>
            <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('.GeneroRadioButtonList').length > 0) {
                $('.GeneroRadioButtonList input').each(function () {
                    if ($(this).attr('checked') == 'checked') {
                        if ($(this).val() == 'True') {
                            $('.AntecedentesGinecoobstetricosPanel').hide();
                        } else {
                            $('.AntecedentesGinecoobstetricosPanel').show();
                        }
                    }
                });
                $('.GeneroRadioButtonList input').click(function () {
                    if ($(this).attr('checked') == 'checked') {
                        if ($(this).val() == 'True') {
                            $('.AntecedentesGinecoobstetricosPanel').hide();
                        } else {
                            $('.AntecedentesGinecoobstetricosPanel').show();
                        }
                    }
                });
            }
        });
    </script>
    <script type="text/javascript">
        function ClienteCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('ClienteDDL').get_value();

            if (value <= "") {
                args.IsValid = false;
            }
        }
        function EstadoCivilDDLCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('EstadoCivilDDL').get_value();

            if (value == "") {
                args.IsValid = false;
            }
        }
    </script>
    <script type="text/javascript">
        $(".ExpandCollapse fieldset legend").click(function () {
            if ($(this).is('.Expand')) {//colapsar
                $(this).removeClass('Expand')
                $(this).parent().children("div").slideToggle('slow');
            }
            else {
                $(this).addClass('Expand')//expandir
                $(this).parent().children("div").slideToggle('slow');
            }

        });
    </script>
    <asp:HiddenField ID="PacienteIdHF" runat="server" />
</asp:Content>

