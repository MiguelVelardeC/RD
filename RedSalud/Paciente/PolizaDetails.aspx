<%@ Page Title="Poliza" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PolizaDetails.aspx.cs" Inherits="Paciente_PolizaDetails" %>

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
                    Text="Nueva Poliza">
                </asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Volver a la Lista de Pacientes"
                        NavigateUrl="~/Paciente/PolizaList.aspx">
                    </asp:HyperLink>
                </div>
                <asp:FormView ID="PolizaFV" runat="server" DataSourceID="PolizaODS">
                    <ItemTemplate>
                        <span class="label">Cliente</span>
                        <asp:Label ID="Label6" Text='<%# Eval("NombreJuridicoCliente") %>' runat="server" />
                        <span class="label">Asegurado</span>
                        <asp:Label ID="Asegurado" Text='<%# Eval("NombreCompletoPaciente") %>' runat="server" />
                        <span class="label">Número de Póliza</span>
                        <asp:Label ID="Label2" Text='<%# Bind("NumeroPoliza") %>' runat="server" />
                        <span class="label">Fecha de Inicio</span>
                        <asp:Label ID="FechaInicio" Text='<%# Eval("FechaInicioString") %>' runat="server" />
                        <span class="label">Fecha de Conclusión</span>
                        <asp:Label ID="FechaFinLabel" Text='<%# Eval("FechaFinString") %>' runat="server" />
                        <span class="label">Monto Total de la Póliza</span>
                        <asp:Label ID="Label3" Text='<%# Bind("MontoTotalForDisplay") %>' runat="server" />
                        <span class="label">Monto para Farmacia de la Póliza</span>
                        <asp:Label ID="Label10" Text='<%# Bind("MontoFarmaciaForDisplay") %>' runat="server" />
                        <span class="label">Monto Cobertura de la Póliza</span>
                        <asp:Label ID="Label11" Text='<%# Bind("Cobertura") %>' runat="server" />
                        <span class="label">Nombre del Plan de la Póliza</span>
                        <asp:Label ID="Label4" Text='<%# Bind("NombrePlan") %>' runat="server" />
                        <span class="label">Lugar</span>
                        <asp:Label ID="Label9" Text='<%# Eval("LugarForDisplay") %>' runat="server" />
                        <span class="label">Código Asegurado</span>
                        <asp:Label ID="Label7" Text='<%# Bind("CodigoAsegurado") %>' runat="server" />
                        <span class="label">Relación con el Asegurado</span>
                        <asp:Label ID="Label8" Text='<%# Bind("Relacion") %>' runat="server" />
                        <span class="label">Estado</span>
                        <asp:Label ID="Label1" Text='<%# Bind("Estado") %>' runat="server" />
                        <div class="buttonsPanel">
                            <asp:LinkButton ID="EditPolizaLB" Text="" runat="server"
                                CssClass="button"
                                CommandName="Edit">
                                <span>Editar</span>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:HiddenField ID="CodigoHF" runat="server" />--%>
                        <span class="label">Cliente</span>
                        <asp:Label ID="Label6" Text='<%# Eval("NombreJuridicoCliente") %>' runat="server" />
                        <span class="label">Asegurado</span>
                        <asp:Label ID="Asegurado" Text='<%# Eval("NombreCompletoPaciente") %>' runat="server" />
                        <asp:HiddenField runat="server" ID="ClientIdHF" Value='<%# Eval("ClienteId") %>' />

                        <span class="label">Número de Póliza</span>
                        <asp:TextBox ID="NroPolizaTxt" runat="server" Text='<%# Bind("NumeroPoliza") %>'
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

                        <span class="label">Fecha de Inicio</span>
                        <telerik:RadDatePicker ID="FechaInicio" runat="server"
                            EnableTyping="true"
                            ShowPopupOnFocus="true"
                            EnableShadows="true"
                            SelectedDate='<%# Bind("FechaInicio") %>'
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
                            SelectedDate='<%# Bind("FechaFin") %>'
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

                        <span class="label">Monto Total de la Póliza</span>
                        <telerik:RadNumericTextBox ID="MontoTotalText" runat="server" DbValue='<%# Bind("MontoTotal") %>'
                            MinValue="0"
                            CssClass="normalField" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" />
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                Display="Dynamic"
                                ValidationGroup="InsertPoliza"
                                ControlToValidate="MontoTotalText"
                                ErrorMessage="El Monto total de la Póliza es requerido." />
                        </div>

                        <span class="label">Monto para Farmacia</span>
                        <telerik:RadNumericTextBox ID="MontoFarmaciaText" runat="server" DbValue='<%# Bind("MontoFarmacia") %>'
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
                        <asp:TextBox ID="CoberturaText" runat="server" Text='<%# Bind("Cobertura") %>'
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
                        <asp:TextBox ID="NombrePlanText" runat="server" Text='<%# Bind("NombrePlan") %>'
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
                        <asp:DropDownList ID="LugarDDL" runat="server" SelectedValue='<%# Bind("Lugar") %>'
                            CssClass="normalField">
                            <asp:ListItem Text="SANTA CRUZ" Value="SCZ"></asp:ListItem>
                            <asp:ListItem Text="LA PAZ" Value="LPZ"></asp:ListItem>
                            <asp:ListItem Text="COCHABAMBA" Value="CBBA"></asp:ListItem>
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
                                CssClass="bigField" Text='<%# Bind("CodigoAsegurado") %>' />
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
                                SelectedValue='<%# Bind("Relacion") %>'
                                CssClass="normalField">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Titular" Value="TITULAR" Selected="true" />
                                    <telerik:RadComboBoxItem Text="Dependiente" Value="DEPENDIENTE" />
                                </Items>
                            </telerik:RadComboBox>

                            <span class="label">Estado de la póliza</span>
                            <telerik:RadComboBox ID="EstadoDDL" runat="server" SelectedValue='<%# Bind("Estado") %>'
                                CssClass="normalField">
                                <Items>
                                    <telerik:RadComboBoxItem Text="ACTIVO" Value="ACTIVO" />
                                    <telerik:RadComboBoxItem Text="INACTIVO" Value="INACTIVO" />
                                </Items>
                            </telerik:RadComboBox>
                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SavePolizaLB" Text="" runat="server"
                                ValidationGroup="InsertPoliza"
                                CssClass="button"
                                OnClick="SavePolizaLB_Click">
                                <span>Guardar</span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="CancelLinkButton" Text="" runat="server"
                                CssClass="secundaryButton"
                                CommandName="Cancel">
                                <span>Cancelar</span>
                            </asp:LinkButton>
                        </div>
                        <script type="text/javascript">
                            function FechaFinCV_Validate(sender, args) {
                                args.IsValid = true;

                                var FechaInicio = $find('<%= PolizaFV.FindControl("FechaInicio").ClientID %>').get_selectedDate();
                                var FechaFin = $find('<%= PolizaFV.FindControl("FechaFin").ClientID %>').get_selectedDate();

                                if (FechaFin < FechaInicio) {
                                    args.IsValid = false;
                                }
                            }
                            function Cobertura_Validate(sender, args) {
                                args.IsValid = false;

                                if ($('#<%= PolizaFV.FindControl("CoberturaText").ClientID %>').val().match(/^[0-9]{1,2}[/][0-9]{1,2}$/) != null) {
                                    args.IsValid = true;
                                }
                            }
                        </script>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <%--<asp:HiddenField ID="CodigoHF" runat="server" />--%>
                        <asp:Label ID="Label1" Text="Cliente" runat="server" CssClass="label" />
                            <telerik:RadComboBox ID="ClienteDDL" runat="server"
                                ClientIDMode="Static"
                                DataSourceID="ClienteODS"
                                CssClass="bigField"
                                DataValueField="ClienteId"
                                DataTextField="NombreJuridico"
                                EmptyMessage="Seleccione un cliente">
                            </telerik:RadComboBox>
                            <div class="validation">
                                <asp:CustomValidator ID="ClienteCV" runat="server"
                                    ValidationGroup="InsertPoliza"
                                    ErrorMessage="Debe seleccionar un cliente."
                                    ClientValidationFunction="ClienteCV_Validate"
                                    Display="Dynamic" />
                            </div>

                            <asp:ObjectDataSource ID="ClienteODS" runat="server"
                                TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                                OldValuesParameterFormatString="original_{0}"
                                SelectMethod="getRedClienteList"
                                OnSelected="ClienteODS_Selected" />

                        <asp:Label ID="Label5" Text="Paciente" runat="server" CssClass="label" />
                            <telerik:RadComboBox ID="PacienteDDL" runat="server"
                                ClientIDMode="Static"
                                DataSourceID="PacienteODS"
                                CssClass="bigField"
                                DataValueField="PacienteId"
                                DataTextField="Nombre"
                                EmptyMessage="Seleccione un Paciente">
                            </telerik:RadComboBox>
                            <div class="validation">
                                <asp:CustomValidator ID="CustomValidator1" runat="server"
                                    ValidationGroup="InsertPoliza"
                                    ErrorMessage="Debe seleccionar un Paciente."
                                    ClientValidationFunction="PacienteCV_Validate"
                                    Display="Dynamic" />
                            </div>

                            <asp:ObjectDataSource ID="PacienteODS" runat="server"
                                TypeName="Artexacta.App.Paciente.BLL.PacienteBLL"
                                OldValuesParameterFormatString="original_{0}"
                                SelectMethod="getPacienteList"
                                OnSelected="PacienteODS_Selected" />

                        <span class="label">Fecha de Inicio</span>
                        <telerik:RadDatePicker ID="FechaInicio" runat="server"
                            EnableTyping="true"
                            ShowPopupOnFocus="true"
                            EnableShadows="true"
                            SelectedDate='<%# Bind("FechaInicio") %>'
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
                            SelectedDate='<%# Bind("FechaFin") %>'
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
                        <asp:TextBox ID="NroPolizaTxt" runat="server" Text='<%# Bind("NumeroPoliza") %>'
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
                        <asp:TextBox ID="MontoTotalText" runat="server" Text='<%# Bind("MontoTotal") %>'
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

                        <span class="label">Nombre del Plan de la Póliza</span>
                        <asp:TextBox ID="NombrePlanText" runat="server" Text='<%# Bind("NombrePlan") %>'
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
                        <asp:DropDownList ID="LugarDDL" runat="server" SelectedValue='<%# Bind("Lugar") %>'
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

                        </div><span class="label">Código Asegurado</span>
                            <asp:TextBox ID="CodigoAseguradoText" runat="server"
                                CssClass="bigField" Text='<%# Bind("CodigoAsegurado") %>' />
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
                                <telerik:RadComboBoxItem Text="Titular" Value="TITULAR" Selected="true" />
                                <telerik:RadComboBoxItem Text="Dependiente" Value="DEPENDIENTE" />
                            </Items>
                        </telerik:RadComboBox>

                        <span class="label">Estado de la póliza</span>
                        <telerik:RadComboBox ID="EstadoDDL" runat="server" SelectedValue='<%# Bind("Estado") %>'
                            CssClass="normalField">
                            <Items>
                                <telerik:RadComboBoxItem Text="Activo" Value="Activo" Selected="true" />
                                <telerik:RadComboBoxItem Text="Inactivo" Value="Inactivo" />
                            </Items>
                        </telerik:RadComboBox>
                        
                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SavePolizaLB" Text="" runat="server"
                                ValidationGroup="InsertPoliza"
                                CssClass="button"
                                OnClick="SavePolizaLB_Click">
                                <span>Guardar</span>
                            </asp:LinkButton>
                            <asp:HyperLink ID="CancelLinkButton" Text="" runat="server"
                                CssClass="secundaryButton"
                                NavigateUrl="~/Paciente/PolizaList.aspx">
                                <span>Cancelar</span>
                            </asp:HyperLink>
                        </div>
                        <script type="text/javascript">
                            function FechaFinCV_Validate(sender, args) {
                                args.IsValid = true;

                                var FechaInicio = $find('<%= PolizaFV.FindControl("FechaInicio").ClientID %>').get_selectedDate();
                                var FechaFin = $find('<%= PolizaFV.FindControl("FechaFin").ClientID %>').get_selectedDate();

                                if (FechaFin < FechaInicio) {
                                    args.IsValid = false;
                                }
                            }
                        </script>
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
        </div>
    </div>

    <asp:ObjectDataSource ID="PolizaODS" runat="server"
        TypeName="Artexacta.App.Poliza.BLL.PolizaBLL"
        DataObjectTypeName="Artexacta.App.Poliza.Poliza"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPolizaByPolizaId"
        InsertMethod="InsertPoliza"
        UpdateMethod="UpdatePoliza"
        OnSelected="PolizaODS_Selected"
        OnInserted="PolizaODS_Inserted"
        OnUpdated="PolizaODS_Updated">
        <SelectParameters>
            <asp:ControlParameter ControlID="PolizaIdHF" Name="PolizaId" PropertyName="value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <script type="text/javascript">
        function PacienteCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('PacienteRadComboBox').get_value();

            if (value <= "") {
                args.IsValid = false;
            }
        }
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
        function PacienteCV_Validate(sender, args) {
            args.IsValid = true;

            var value = $find('PacienteDDL').get_value();

            if (value <= "") {
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
    <asp:HiddenField ID="PolizaIdHF" runat="server" />
</asp:Content>

