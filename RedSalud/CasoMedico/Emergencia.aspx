<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Emergencia.aspx.cs" Inherits="CasoMedico_Emergencia" %>

<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="cssCritic" runat="server" />
    <asp:Literal ID="cssEditable" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="CasoMedicoTitle" CssClass="title" Text="Registro de Caso Medico por Emergencia" runat="server" />
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:LinkButton ID="returnHL" CommandArgument="~/CasoMedico/CasoMedicoLista.aspx" runat="server"
                        OnClick="returnHL_Click"
                        Text="Volver al Listado de Casos Medicos" />
                </div>
                <div class="error">
                    <asp:Label ID="MessageLabel" runat="server" 
                        Text="No se puede modificar la Emergencia por que tiene registros de gastos."
                        Visible="false"/>
                    <div class="clearfix"></div>
                    <asp:Label ID="Message2Label" runat="server" 
                        Text="No se puede modificar la Emergencia por que no tiene Hospitales registrados."
                        Visible="false" />
                </div>
                <asp:Panel runat="server" GroupingText="Información Basica" CssClass="critic left" Style="min-width:440px;">
                    <div class="left">
                        <span class="label">Código Caso Medico</span>
                        <asp:Label ID="CodigoCasoLabel" runat="server" />

                        <span class="label">Nombre Cliente</span>
                        <asp:Label ID="NombreAseguradoraLabel" Text="" runat="server" />
                    </div>
                    <div class="left" style="margin-left:20px;">
                        <span class="label">Código Asegurado</span>
                        <asp:Label ID="CodigoALabel" Text="" runat="server" />

                        <span class="label">Nombre Asegurado</span>
                        <asp:Label ID="NombreALabel" Text="" runat="server" />
                    </div>
                    <div class="clear"></div>
                    <div style="margin-top: 5px;">
                        <asp:CheckBox ID="CasoCriticoEnfermedadCronica" CssClass="CasoCriticoEnfermedadCronica" Text="Paciente con enfermedades cronicas" runat="server" Enabled="false" />
                        <asp:Panel runat="server" ID="PacienteCronicoPanel" Visible="false">
                            <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server" 
                                DataSourceID="EnfermedadesCronicasODS">
                                <HeaderTemplate><span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul></HeaderTemplate>
                                <ItemTemplate>
                                    <li style="font-weight:normal;">
                                        <asp:Literal Text='<%# Eval("Nombre") %>' runat="server" /> 
                                        <asp:ImageButton ID="DeleteEnfermedadCronicaImageButton" ImageUrl="~/Images/Neutral/delete.png" 
                                            OnCommand="DeleteEnfermedadCronicaImageButton_Command" CommandArgument='<%# Eval("EnfermedadCronicaId") %>'
                                            runat="server" Width="10px" />
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate></ul></FooterTemplate>
                            </asp:Repeater>
                            <asp:ObjectDataSource ID="EnfermedadesCronicasODS" runat="server"
                                TypeName="Artexacta.App.EnfermedadCronica.BLL.EnfermedadCronicaBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="GetEnfermedadCronicaByAseguradoId"
                                OnSelected="EnfermedadesCronicasODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="AseguradoIdHF" Name="AseguradoId" PropertyName="Value" DbType="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:Panel ID="NewEnfermedadCronicaPanel" runat="server">
                                <asp:Label Text="Añadir Enfermedad Crónica" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="EnfermedadCronicaRadComboBox" runat="server"
                                    CssClass="biggerField"
                                    EnableLoadOnDemand="true"
                                    EmptyMessage="Seleccione una Enfermedad Crónica"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false"
                                    MarkFirstMatch="true">
                                    <WebServiceSettings Method="GetEnfermedadCronicaAutocomplete" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                </telerik:RadComboBox>
                                <asp:ImageButton ID="NewEnfermedadCronicaImageButton" ImageUrl="~/Images/Neutral/new.png" 
                                    OnClick="NewEnfermedadCronicaImageButton_Click"
                                    runat="server" CssClass="right" style="margin-top:2px;margin-left:2px;" />
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="FileManagerPanel" runat="server" GroupingText="Adjuntos" CssClass="left" style="margin-left: 10px;">
                    <RedSalud:FileManager runat="server" ID="FileManager" ObjectName="EMERGENCIA" ShowMode="Normal" />
                </asp:Panel>
                <div class="clear"></div>
                <%--motivo de la consulta / diagnostico presuntivo--%>

                <span class="label">Observaciones para la emergencia</span>
                <asp:TextBox ID="ObservacionEmergenciaTxt" runat="server"
                    TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                <div class="validation">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        Display="Dynamic"
                        ControlToValidate="ObservacionEmergenciaTxt"
                        ValidationGroup="Emergencia"
                        ErrorMessage="La Observación es requerida." />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                        Display="Dynamic"
                        ControlToValidate="ObservacionEmergenciaTxt"
                        ValidationGroup="Emergencia"
                        ErrorMessage="La Observación no puede exceder 2000 caracteres."
                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                </div>


                <span class="label" style="text-decoration: underline;">Exámenes físicos vitales</span>
                <table>
                    <tr>
                        <td>
                            <span class="label">Presión arterial:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="PresionArterialTxt" runat="server" CssClass="smallField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="PresionArterialREV" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="PresionArterialTxt"
                                    ValidationGroup="Emergencia"
                                    ErrorMessage='La Presión arterial puede contener 2 números de 3 dígitos separados por "-", ej 70-120.'
                                    ValidationExpression="<%$ Resources: Validations, PresionArterialFormat  %>" />
                            </div>
                        </td>
                        <td style="padding-left: 15px;">
                            <span class="label">Temperatura (° C):</span>
                        </td>
                        <td>
                            <asp:TextBox ID="TemperaturaTxt" runat="server" CssClass="smallField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="TemperaturaREVFormat" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="TemperaturaTxt"
                                    ValidationGroup="Emergencia"
                                    ErrorMessage="La temperatura solo puede contener números de 3 dígitos máximo."
                                    ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="label">Pulso:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="Pulsotxt" runat="server" CssClass="smallField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="PulsoREV" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Pulsotxt"
                                    ValidationGroup="Emergencia"
                                    ErrorMessage="El Pulso solo puede contener números de 3 dígitos máximo."
                                    ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                            </div>
                        </td>
                        <td style="padding-left: 15px;">
                            <span class="label">Frecuencia cardiaca:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="FrecuenciaCTxt" runat="server" CssClass="smallField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="FrecuenciaREV" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="TemperaturaTxt"
                                    ValidationGroup="Emergencia"
                                    ErrorMessage="La temperatura solo puede contener números de 3 dígitos máximo."
                                    ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                            </div>
                        </td>
                    </tr>
                </table>

                <asp:Label Text="Diagnostico Presuntivo" runat="server" CssClass="label" />
                <telerik:RadComboBox ID="EnfermedadesComboBox" runat="server" 
                    ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField">
                    <WebServiceSettings Method="GetEnfermedades" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                </telerik:RadComboBox>
                <asp:CheckBox ID="OtroCheckBox" Text="Otro" runat="server"
                        onclick="OtroCheckBox_click(this);" />
                <div class="validation">
                    <asp:CustomValidator ID="CustomValidator2" runat="server"
                        Display="Dynamic"
                        ClientValidationFunction="validateDiagnostico"
                        ControlToValidate="EnfermedadesComboBox"
                        ValidationGroup="Emergencia"
                        ValidateEmptyText="true"
                        ErrorMessage="Debe seleccionar un diagnostico presuntivo." />
                </div>
                <div id="DiagnosticoPresuntivoDiv" style="display: none;margin-top: 10px;">
                    <asp:TextBox ID="DiagnosticoPresuntivoTxt" runat="server"
                        TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                    <div class="validation">
                        <asp:CustomValidator ID="CustomValidator4" runat="server"
                            Display="Dynamic"
                            ClientValidationFunction="validateDiagnostico"
                            ControlToValidate="DiagnosticoPresuntivoTxt"
                            ValidationGroup="Emergencia"
                            ValidateEmptyText="true"
                            ErrorMessage="Debe llenar el campo Otro." />
                    </div>
                </div>
                <script type="text/javascript">
                    function OtroCheckBox_click(objCheckBox) {
                        var DiagnosticoPresuntivoTxt = $('#DiagnosticoPresuntivoDiv');

                        if (objCheckBox.checked) {
                            DiagnosticoPresuntivoTxt.show();
                        } else {
                            $('#<%=DiagnosticoPresuntivoTxt.ClientID%>').val('');
                            DiagnosticoPresuntivoTxt.hide();
                        }
                    }
                    function validateDiagnostico(sender, arguments) {
                        var enfermedad = $find('<%=EnfermedadesComboBox.ClientID%>');
                        var otro = $('#<%=DiagnosticoPresuntivoTxt.ClientID%>');
                        if (enfermedad.get_value() != '' || otro.val() != '') {
                            arguments.IsValid = true;
                        } else {
                            arguments.IsValid = false;
                        }
                    }
                </script>
                <div class="validation">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                        Display="Dynamic"
                        ControlToValidate="DiagnosticoPresuntivoTxt"
                        ValidationGroup="Emergencia"
                        ErrorMessage="El diagnostico presuntivo no puede exceder 2000 caracteres."
                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                        Display="Dynamic"
                        ControlToValidate="DiagnosticoPresuntivoTxt"
                        ValidationGroup="Emergencia"
                        ErrorMessage="Caracteres inválidos en el diagnostico presuntivo."
                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="EmergenciaSaveLB" Text="" runat="server"
                        CssClass="button"
                        ValidationGroup="Emergencia"
                        OnClick="EmergenciaSaveLB_Click">
                        <asp:Label Text="Guardar" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton OnClick="returnHL_Click" Text="Cancelar" runat="server" />
                </div>
            </div>
            <asp:HiddenField ID="AseguradoIdHF" runat="server" />
            <asp:HiddenField ID="CasoIdHF" runat="server" />
            <asp:HiddenField ID="EmergenciaIdHF" runat="server" />
            <asp:HiddenField ID="ProveedorId" runat="server" />
            <asp:HiddenField ID="DirtyHF" runat="server" />
        </div>
    </div>
</asp:Content>

