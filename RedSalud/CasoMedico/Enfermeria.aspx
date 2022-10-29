<%@ Page Title="Caso Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Enfermeria.aspx.cs"
    Inherits="CasoMedico_Enfermeria" %>

<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function getTableName(children) {
            return $(children).parents('.PDFExportRadGrid').attr('class').replace('RadGrid', '')
                                                                         .replace('RadGrid_Default', '')
                                                                         .replace('PDFExportRadGrid', '').trim();
        }
        $(document).ready(function () {
            $('.ExportAllCheckBox input').click(function () {
                var table = getTableName(this);

                if ($('.' + table + ' .ExportCheckBox input[type="checkbox"]:disabled').length ==
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').length) {
                    $(this).attr('checked', false);
                    return false;
                }

                var checked = ($(this).attr('checked') == 'checked' || $(this).attr('checked') == true);
                $('.' + table + ' .ExportCheckBox input[type="checkbox"]').attr('checked', checked);
            });
            $('.rgExpPDF').click(function () {
                var table = getTableName(this);
                var ids = [];
                $('.' + table + ' .ExportCheckBox input[type="checkbox"]').each(function () {
                    if ($(this).attr('checked') == 'checked' || $(this).attr('checked') == true) {
                        var id = $(this).parent().next().val();
                        if (id != null && id != undefined && id != '') {
                            ids.push(id);
                        }
                    }
                });
                $('#<%= ExportIDHF.ClientID %>').val(ids.join(','));
                var valid = $('#<%= ExportIDHF.ClientID %>').val() != '';
                if (!valid) {
                    alert('No ha seleccionado ninguna fila para exportar.');
                }
                return valid;
            });
            $('.ExportCheckBox input[type="checkbox"]').change(function () {
                var table = getTableName(this);
                var check = ($('.' + table + ' .ExportCheckBox input[type="checkbox"]:checked').length ==
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').length)
                $('.' + table + ' .ExportAllCheckBox input[type="checkbox"]').attr('checked', check);
            });
            $('.ExportAllCheckBox input[type="checkbox"]').each(function () {
                var table = getTableName(this);
                if ($('.' + table + ' .ExportCheckBox input[type="checkbox"]').length == 0) {
                    $(this).attr('disabled', true);
                    $('.' + table + '.rgCommandRow').remove();
                }
            });
        });
        function validateAngularJS() {
            var valid = true;
            $('span.angularValidation').each(function () {
                if ($(this).css('display') != 'none') {
                    valid = false;
                }
            });
            return valid;
        }
    </script>
    <asp:Literal ID="cssCritic" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="CasoMedicoTitle" CssClass="title" Text="Registro de Caso Medico por Enfermeria" runat="server" />
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:LinkButton ID="returnHL" CommandArgument="~/CasoMedico/CasoMedicoLista.aspx" runat="server"
                        OnClick="returnHL_Click"
                        Text="Volver al Listado de Casos Medicos" />

                    <asp:LinkButton ID="AdminGastosLB" runat="server"
                        Text="Administrar Gastos"
                        OnClick="AdminGastosLB_Click"
                        CssClass="linkBorderLeft" />
                </div>
                <div>
                    <telerik:RadTabStrip ID="CasoTab" runat="server" MultiPageID="CasoMP" SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab Text="Informacion consulta" PageViewID="CasoRPV"></telerik:RadTab>
                            <telerik:RadTab Text="Medicamentos" ID="MedicamentoRT" runat="server"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="CasoMP" runat="server"
                        CssClass="RadMultiPage"
                        SelectedIndex="0">
                        <telerik:RadPageView ID="CasoRPV" runat="server">
                            <div class="left" style="width: 50%;">
                                <asp:Label Text="Registro de Caso Medico por Enfermeria" runat="server" CssClass="subTitleLabel" />
                                <br />
                                <asp:FormView ID="CasoFV" runat="server"
                                    OnDataBound="CasoFV_DataBound"
                                    DataSourceID="CasoODS">
                                    <ItemTemplate>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:HiddenField ID="CasoMedicoIdHF" runat="server" Value='<%# Bind("CasoId") %>' />
                                            <asp:HiddenField ID="CorrelativoHF" runat="server" Value='<%# Bind("Correlativo") %>' />
                                            <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" Value='<%# Bind("MotivoConsultaId") %>' />
                                            <asp:HiddenField ID="EstadoHF" runat="server" Value='<%# Bind("Estado") %>' />
                                            <asp:HiddenField ID="PacienteIdHF" runat="server" Value='<%# Bind("PacienteId") %>' />
                                            <asp:HiddenField ID="EnfermedadActualHF" runat="server" Value='<%# Bind("EnfermedadActual") %>' />
                                            <asp:HiddenField ID="ExFisicoRegionalyDeSistemaHF" runat="server" Value='<%# Bind("ExFisicoRegionalyDeSistema") %>' />

                                            <asp:HiddenField ID="HistoriaIdHF" runat="server" Value='<%# Bind("HistoriaId") %>' />

                                            <asp:Label ID="Label33" Text="Código del Caso" runat="server" CssClass="label" />
                                            <asp:Label ID="CodigoCasoLabel" Text='<%# Bind("CodigoCaso") %>' runat="server" />

                                            <span class="label">Fecha de creación</span>
                                            <asp:Label ID="FechaCLabel" Text='<%# Bind("FechaCreacionString") %>' runat="server" />
                                            <br /><br />
                                            <asp:CheckBox ID="CheckBox1" CssClass="CasoCriticoEnfermedadCronica" Text="Paciente con enfermedades cronicas" runat="server" Checked='<%# Eval("CasoCritico") %>' Enabled="false" />
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    if ($(".CasoCriticoEnfermedadCronica input").prop("checked")) {
                                                        $('#PacienteCronicoPanel').show();
                                                    } else {
                                                        $('#PacienteCronicoPanel').hide();
                                                    }
                                                });
                                            </script>
                                            <div id="PacienteCronicoPanel" style="display: none;" >
                                                <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server" DataSourceID="EnfermedadesCronicasODS">
                                                    <HeaderTemplate><span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul></HeaderTemplate>
                                                    <ItemTemplate><li style="font-weight:normal;"><asp:Literal ID="Literal2" Text='<%# Eval("Nombre") %>' runat="server" /></li></ItemTemplate>
                                                    <FooterTemplate></ul></FooterTemplate>
                                                </asp:Repeater>
                                            </div>

                                            <asp:Label ID="Label3" Text="Examenes físicos vitales" runat="server" CssClass="label" />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="PALabel" Text="Presión arterial:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PresionArterialTxt" runat="server"
                                                            Text='<%# Bind("PresionArterial") %>' />
                                                    </td>
                                                    <td style="padding-left: 15px;">
                                                        <asp:Label ID="TempLabel" Text="Temperatura (° C):" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="TemperaturaTxt" runat="server"
                                                            Text='<%# Bind("Temperatura") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="PLabel" Text="Pulso p/min:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Pulsotxt" runat="server"
                                                            Text='<%# Bind("Pulso") %>' />
                                                    </td>
                                                    <td style="padding-left: 15px;">
                                                        <asp:Label ID="FCLabel" Text="Frecuencia cardiaca p/min:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="FrecuenciaCTxt" runat="server"
                                                            Text='<%# Bind("FrecuenciaCardiaca") %>' />
                                                    </td>
                                                </tr>
                                            </table>

                                            <asp:Label ID="MCLabel" Text="Motivo de la Consulta o Sintomatologia" runat="server" CssClass="label" />
                                            <asp:Label ID="MotivoConsultaTxt" runat="server"
                                                Text='<%# Bind("MotivoConsulta") %>' />
                                            <asp:Label ID="EFRySLabel" Text="Observaciones" runat="server" CssClass="label" />
                                            <asp:Label ID="ObservacionesLabel" runat="server"
                                                CssClass="ObservacionesLabel" Text='<%# Bind("Observaciones") %>'  />
                                            <asp:TextBox ID="ObservacionesTextBox" runat="server"
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField ObservacionesTextBox"
                                                Text='<%# Eval("Observaciones") %>' style="display:none;" />
                                            <div id="ObservacionesValidation" class="validation" style="display:none;">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="ObservacionesTextBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en las observaciones."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>

                                            <asp:Label ID="BHLabel" Text="Tratamiento" runat="server" CssClass="label" />
                                            <asp:Label ID="BiometriaHematicaTxt" runat="server"
                                                Text='<%# Bind("BiometriaHematica") %>' />

                                            <asp:Label ID="DPLabel" Text="Diagnostico presuntivo" runat="server" CssClass="label" />
                                            <asp:Label ID="DiagnosticoPresuntivoTxt" runat="server"
                                                Text='<%# Bind("DiagnosticoPresuntivoForDisplay") %>' />
                                            
                                            <div id="EditarObservaciones" class="buttonsPanel">
                                                <asp:HyperLink ID="EditObservacionesHL" runat="server"
                                                    CssClass="button EditObservacionesHL">
                                                    <span>Editar</span>
                                                </asp:HyperLink>
                                                <asp:HyperLink ID="CancelHL"
                                                    NavigateUrl="~/CasoMedico/CasoMedicoLista.aspx" runat="server"
                                                    Text="Retornar a la lista de casos medicos"
                                                    CssClass="secondaryButton" />
                                            </div>
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    $('.EditObservacionesHL').click(function () {
                                                        $('.ObservacionesTextBox').show();
                                                        $('#ObservacionesValidation').show();
                                                        $('.ObservacionesLabel').hide();
                                                        $('#EditarObservaciones').hide();
                                                        $('#GuardarObservaciones').show();
                                                        return false;
                                                    });
                                                    $('.CancelObservacionesHL').click(function () {
                                                        $('.ObservacionesTextBox').hide();
                                                        $('#ObservacionesValidation').hide();
                                                        $('.ObservacionesLabel').show();
                                                        $('#EditarObservaciones').show();
                                                        $('#GuardarObservaciones').hide();
                                                        return false;
                                                    });
                                                });
                                            </script>

                                            <div id="GuardarObservaciones" class="buttonsPanel" style="display:none;">
                                                <asp:LinkButton ID="SaveObservacionesLB" runat="server" ValidationGroup="UpdateCaso"
                                                    CssClass="button" OnClick="SaveObservacionesLB_Click">
                                                    <span>Guardar</span>
                                                </asp:LinkButton>
                                                <asp:HyperLink ID="CancelObservacionesHL" runat="server"
                                                    Text="Cancelar" CssClass="secondaryButton CancelObservacionesHL" />
                                            </div>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:HiddenField ID="CasoMedicoIdHF" runat="server" Value='<%# Bind("CasoId") %>' />
                                            <asp:HiddenField ID="CorrelativoHF" runat="server" Value='<%# Bind("Correlativo") %>' />
                                            <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" Value='<%# Bind("MotivoConsultaId") %>' />
                                            <asp:HiddenField ID="EstadoHF" runat="server" Value='<%# Bind("Estado") %>' />
                                            <asp:HiddenField ID="PacienteIdHF" runat="server" Value='<%# Bind("PacienteId") %>' />
                                            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Bind("Dirty") %>' />
                                            <asp:HiddenField ID="HistoriaIdHF" runat="server" Value='<%# Bind("HistoriaId") %>' />
                                            <asp:HiddenField ID="EnfermedadActualHF" runat="server" Value='<%# Bind("EnfermedadActual") %>' />
                                            <asp:HiddenField ID="ExFisicoRegionalyDeSistemaHF" runat="server" Value='<%# Bind("ExFisicoRegionalyDeSistema") %>' />

                                            <asp:Label ID="Label33" Text="Código del Caso" runat="server" CssClass="label" />
                                            <asp:Label ID="CodigoCasoLabel" Text='<%# Bind("CodigoCaso") %>' runat="server" />

                                            <span class="label">Fecha de creación</span>
                                            <telerik:RadDatePicker ID="FechaCreacion" runat="server"
                                                EnableShadows="true"
                                                SelectedDate='<%# Bind("FechaCreacion") %>'>
                                            </telerik:RadDatePicker>
                                            <telerik:RadTimePicker ID="HoraCreacion" runat="server"
                                                EnableShadows="true"
                                                EnableTyping="false"
                                                TimeView-TimeFormat="HH:mm tt"
                                                DatePopupButton-Visible="false"
                                                TimePopupButton-Visible="false"
                                                Width="70px"
                                                style="display: inline-block;"
                                                SelectedDate='<%# Bind("HoraCreacion") %>'>
                                            </telerik:RadTimePicker>
                                             <div class="validation">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                    ControlToValidate="FechaCreacion"
                                                    ErrorMessage="Debe seleccionar la fecha de creación del Caso Medico."
                                                    Display="Dynamic"
                                                    ValidationGroup="UpdateCaso">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <br /><br />
                                            <asp:CheckBox ID="CheckBox1" CssClass="CasoCriticoEnfermedadCronica" Text="Paciente con enfermedades cronicas" runat="server" Checked='<%# Eval("CasoCritico") %>' />
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    $('.CasoCriticoEnfermedadCronica input').click(function () {
                                                        if ($(".CasoCriticoEnfermedadCronica input").prop("checked")) {
                                                            $('#PacienteCronicoPanel').show();
                                                        } else {
                                                            $('#PacienteCronicoPanel').hide();
                                                        }
                                                    });
                                                    if ($(".CasoCriticoEnfermedadCronica input").prop("checked")) {
                                                        $('#PacienteCronicoPanel').show();
                                                    } else {
                                                        $('#PacienteCronicoPanel').hide();
                                                    }
                                                });
                                            </script>
                                            <div id="PacienteCronicoPanel" style="display: none;">
                                                <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server" DataSourceID="EnfermedadesCronicasODS">
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
                                                    runat="server" CssClass="right" style="margin-top:2px;" />
                                            </div>

                                            <asp:Label ID="Label3" Text="Examenes físicos vitales" runat="server" CssClass="label" />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="PALabel" Text="Presión arterial:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="PresionArterialTxt" runat="server" CssClass="smallField"
                                                            Text='<%# Bind("PresionArterial") %>' />
                                                        <div class="validation">
                                                            <asp:RegularExpressionValidator ID="PresionArterialREV" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="PresionArterialTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage='La Presión arterial puede contener 2 números de 3 dígitos separados por "-", ej 70-120.'
                                                                ValidationExpression="<%$ Resources: Validations, PresionArterialFormat  %>" />
                                                        </div>
                                                    </td>
                                                    <td style="padding-left: 15px;">
                                                        <asp:Label ID="TempLabel" Text="Temperatura (° C):" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TemperaturaTxt" runat="server" CssClass="smallField"
                                                            Text='<%# Bind("Temperatura") %>' />
                                                        <div class="validation">
                                                            <asp:RegularExpressionValidator ID="TemperaturaREVFormat" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="TemperaturaTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La temperatura solo puede contener números de 3 dígitos máximo."
                                                                ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="PLabel" Text="Pulso:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Pulsotxt" runat="server" CssClass="smallField"
                                                            Text='<%# Bind("Pulso") %>' />
                                                        <div class="validation">
                                                            <asp:RegularExpressionValidator ID="PulsoREV" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="Pulsotxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="El Pulso solo puede contener números de 3 dígitos máximo."
                                                                ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                                                        </div>
                                                    </td>
                                                    <td style="padding-left: 15px;">
                                                        <asp:Label ID="FCLabel" Text="Frecuencia cardiaca:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="FrecuenciaCTxt" runat="server" CssClass="smallField"
                                                            Text='<%# Bind("FrecuenciaCardiaca") %>' />
                                                        <div class="validation">
                                                            <asp:RegularExpressionValidator ID="FrecuenciaREV" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="TemperaturaTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La temperatura solo puede contener números de 3 dígitos máximo."
                                                                ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="MCLabel" Text="Motivo de la consulta" runat="server" CssClass="label" />
                                            <asp:TextBox ID="MotivoConsultaTxt" runat="server"
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                Text='<%# Bind("MotivoConsulta") %>' />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="MotivoConsultaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El Motivo de la consulta es requerido." />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="MotivoConsultaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El Motivo de la consulta no puede exceder 2000 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="MotivoConsultaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en el Motivo de la consulta."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>
                                            <asp:Label ID="EFRySLabel" Text="Observaciones" runat="server" CssClass="label" />
                                            <asp:TextBox ID="ExFisico" runat="server" 
                                                TextMode="MultiLine" Rows="3"
                                                Text='<%# Bind("Observaciones") %>' CssClass="biggerField"  />

                                            <asp:Label ID="Label10" Text="Tratamiento" runat="server" CssClass="label" />
                                            <asp:TextBox ID="BiometriaHematicaTxt" runat="server"
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                Text='<%# Bind("BiometriaHematica") %>' />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="BiometriaHematicaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El tratamiento es requerido." />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="BiometriaHematicaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El tratamiento no puede exceder 2000 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="BiometriaHematicaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en el tratamiento."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>
                                            <asp:Label ID="Label11" Text="Diagnostico Presuntivo" runat="server" CssClass="label" />
                                            <telerik:RadComboBox ID="EnfermedadesComboBox" runat="server"
                                                ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                                                OnClientLoad="EnfermedadesComboBox_ClientLoad"
                                                OnClientSelectedIndexChanged="EnfermedadesComboBox_ClientSelectedIndexChanged">
                                                <WebServiceSettings Method="GetEnfermedades" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                            </telerik:RadComboBox>
                                            <asp:HiddenField runat="server" ID="EnfermedadHF" Value='<%# Bind("EnfermedadId") %>' />
                                            <asp:CheckBox ID="OtroCheckBox" Text="Otro" runat="server"
                                                onclick="OtroCheckBox_click(this);" Checked='<%# !string.IsNullOrWhiteSpace(Eval("DiagnosticoPresuntivo").ToString()) %>' />
                                            <div id="DiagnosticoValidation" class="validation">
                                                <asp:CustomValidator ID="RequiredFieldValidator1" runat="server"
                                                    Display="Dynamic"
                                                    ClientValidationFunction="validateDiagnostico"
                                                    ControlToValidate="EnfermedadesComboBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ValidateEmptyText="true"
                                                    ErrorMessage="Debe seleccionar un diagnostico presuntivo." />
                                            </div>
                                            <div id="DiagnosticoPresuntivoDiv" style="display: none;margin-top: 10px;">
                                                <asp:TextBox ID="DiagnosticoPresuntivoTxt" runat="server"
                                                    TextMode="MultiLine" Rows="3" CssClass="biggerField" 
                                                    Text='<%# Bind("DiagnosticoPresuntivo") %>' />
                                                <div class="validation">
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                                                        Display="Dynamic"
                                                        ClientValidationFunction="validateDiagnostico"
                                                        ControlToValidate="DiagnosticoPresuntivoTxt"
                                                        ValidationGroup="UpdateCaso"
                                                        ValidateEmptyText="true"
                                                        ErrorMessage="Debe llenar el campo Otro." />
                                                </div>
                                            </div>
                                            <script type="text/javascript">
                                                function EnfermedadesComboBox_ClientLoad() {
                                                    setTimeout(
                                                        changeOtraEnfermedad($('#<%=CasoFV.FindControl("DiagnosticoPresuntivoTxt").ClientID%>').val().trim() != '')
                                                    , 500);
                                                }
                                                function OtroCheckBox_click(objCheckBox) {
                                                    changeOtraEnfermedad(objCheckBox.checked);
                                                }

                                                function changeOtraEnfermedad(checked) {
                                                    var DiagnosticoPresuntivoTxt = $('#DiagnosticoPresuntivoDiv');

                                                    if (checked) {
                                                        DiagnosticoPresuntivoTxt.show();
                                                    } else {
                                                        $('#<%=CasoFV.FindControl("DiagnosticoPresuntivoTxt").ClientID%>').val('');
                                                        DiagnosticoPresuntivoTxt.hide();
                                                    }
                                                }
                                                function EnfermedadesComboBox_ClientSelectedIndexChanged(sender, eventArgs) {
                                                    var item = eventArgs.get_item();
                                                    $('#<%=CasoFV.FindControl("EnfermedadHF").ClientID%>').val(item.get_value());
                                                    $('#DiagnosticoValidation > span').hide();
                                                }
                                                function validateDiagnostico(sender, arguments) {
                                                    var enfermedad = $find('<%=CasoFV.FindControl("EnfermedadesComboBox").ClientID%>');
                                                    var otro = $('#<%=CasoFV.FindControl("DiagnosticoPresuntivoTxt").ClientID%>');
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
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El diagnostico presuntivo no puede exceder 2000 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="DiagnosticoPresuntivoTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en el diagnostico presuntivo."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>
                                            <div class="buttonsPanel">
                                                <asp:LinkButton ID="LinkButton3" Text="" runat="server"
                                                    CommandName="Update" OnClientClick="return validateAngularJS();"
                                                    ValidationGroup="UpdateCaso"
                                                    CssClass="button">
                                                    <asp:Label ID="Label4" Text="Guardar" runat="server" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="CancelLB" runat="server"
                                                    CommandName="Cancel"
                                                    Text="Cancelar"
                                                    CssClass="secondaryButton">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="CancelHL"
                                                    OnClick="returnHL_Click" runat="server"
                                                    Text="Retornar a la lista de casos medicos"
                                                    CssClass="secondaryButton" />
                                            </div>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                </asp:FormView>

                                <asp:ObjectDataSource ID="CasoODS" runat="server"
                                    TypeName="Artexacta.App.Caso.BLL.CasoBLL"
                                    DataObjectTypeName="Artexacta.App.Caso.Caso"
                                    OldValuesParameterFormatString="Original_{0}"
                                    SelectMethod="GetCasoByCasoId"
                                    UpdateMethod="UpdateCasoMedicoByEnfermeria"
                                    OnSelected="CasoODS_Selected"
                                    OnUpdating="CasoODS_Updating"
                                    OnUpdated="CasoODS_Updated">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" PropertyName="value" Name="CasoId" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="EnfermedadesCronicasODS" runat="server"
                                    TypeName="Artexacta.App.EnfermedadCronica.BLL.EnfermedadCronicaBLL"
                                    OldValuesParameterFormatString="{0}"
                                    SelectMethod="GetEnfermedadCronicaByAseguradoId"
                                    OnSelected="EnfermedadesCronicasODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="AseguradoIdHF" Name="AseguradoId" PropertyName="Value" DbType="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                            <asp:Panel ID="Panel7" runat="server" CssClass="critic right" GroupingText="Información asegurado">
                                <asp:FormView ID="PacienteFV" runat="server"
                                    DataSourceID="PacienteODS">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="GeneroAsegurado" Value='<%# Bind("Genero")%>' runat="server" />
                                        <div class="twoColsLeft">
                                            <asp:Label ID="CITitle" Text="Carnet de Indentidad" runat="server" CssClass="label" />
                                            <asp:Label ID="CILabel" Text='<%# Bind("CarnetIdentidad")%>' runat="server" />

                                            <asp:Label ID="NombreTitle" Text="Nombre Completo" runat="server" CssClass="label" />
                                            <asp:Label ID="NombreLabel" Text='<%# Bind("Nombre")%>' runat="server" />

                                            <asp:Label ID="FechaNacTitle" Text="Fecha de nacimiento" runat="server" CssClass="label" />
                                            <asp:Label ID="FechaNacLabel" Text='<%# Bind("FechaNacimientoShort")%>' runat="server" />

                                            <asp:Label ID="LugarTTitle" Text="Lugar de trabajo" runat="server" CssClass="label" />
                                            <asp:Label ID="LugarTLabel" Text='<%# Bind("LugarTrabajo")%>' runat="server" />

                                            <asp:Label ID="GeneroTitle" Text="Género" runat="server" CssClass="label" />
                                            <asp:Label ID="GeneroLabel" Text='<%# Bind("GeneroForDisplay")%>' runat="server" />

                                            <asp:Label ID="EstadoCivilTitle" Text="Estado civil" runat="server" CssClass="label" />
                                            <asp:Label ID="EstadoCivilLabel" Text='<%# Bind("EstadoCivil")%>' runat="server" />
                                        </div>
                                        <div class="twoColsRight">
                                            <asp:LinkButton ID="HistorialLB" Text="Historial" runat="server"
                                                OnClick="HistorialLB_Click" Visible="false" />

                                            <asp:Label ID="DirTitle" Text="Dirección" runat="server" CssClass="label" />
                                            <asp:Label ID="DirLabel" Text='<%# Bind("Direccion")%>' runat="server" />

                                            <asp:Label ID="tlfTitle" Text="Teléfono" runat="server" CssClass="label" />
                                            <asp:Label ID="TlfLabel" Text='<%# Bind("Telefono")%>' runat="server" />

                                            <asp:Label ID="TlfOffTitle" Text="Teléfono de Oficina" runat="server" CssClass="label" />
                                            <asp:Label ID="tlfOffLabel" Text='<%# Bind("TelefonoTrabajo")%>' runat="server" />

                                            <asp:Label ID="NroHijosTitle" Text="Nro. de hijos" runat="server" CssClass="label" />
                                            <asp:Label ID="NroHijosLabel" Text='<%# Bind("NroHijos")%>' runat="server" />
                                        </div>
                                        <div class="buttonsPanel clear" runat="server" visible="false">
                                            <asp:LinkButton ID="EditLB" Text="" runat="server"
                                                CommandName="Edit"
                                                CssClass="button">
                                                <asp:Label ID="Label6" Text="Editar" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="GeneroAsegurado" Value='<%# Bind("Genero")%>' runat="server" />
                                        <div class="twoColsLeft">
                                            <asp:HiddenField ID="PacienteId" runat="server" Value='<%# Bind("PacienteId")%>' />
                                            <asp:Label ID="CITitle" Text="Carnet de Indentidad" runat="server" CssClass="label" />
                                            <asp:TextBox ID="CILabel" runat="server" Text='<%# Bind("CarnetIdentidad")%>' />

                                            <asp:Label ID="NombreTitle" Text="Nombre Completo" runat="server" CssClass="label" />
                                            <asp:Label ID="NombreLabel" Text='<%# Eval("Nombre")%>' runat="server" />

                                            <asp:Label ID="FechaNacTitle" Text="Fecha de nacimiento" runat="server" CssClass="label" />
                                            <asp:Label ID="FechaNacLabel" Text='<%# Eval("FechaNacimientoShort")%>' runat="server" />

                                            <asp:Label ID="LugarTTitle" Text="Lugar de trabajo" runat="server" CssClass="label" />
                                            <asp:TextBox ID="LugarTLabel" Text='<%# Bind("LugarTrabajo")%>' runat="server" />

                                            <asp:Label ID="GeneroTitle" Text="Género" runat="server" CssClass="label" />
                                            <asp:RadioButtonList ID="Genero" runat="server" SelectedValue='<%# Bind("Genero") %>'>
                                                <asp:ListItem Text="Masculino" Value="True" />
                                                <asp:ListItem Text="Femenino" Value="False" />
                                            </asp:RadioButtonList>

                                            <asp:Label ID="EstadoCivilTitle" Text="Estado civil" runat="server" CssClass="label" />
                                            <asp:TextBox ID="EstadoCivilTxt" Text='<%# Bind("EstadoCivil")%>' runat="server" />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="EstadoCivilRFV" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="EstadoCivilTxt"
                                                    ErrorMessage="El Estado civil es requerido." />
                                                <asp:RegularExpressionValidator ID="EstadoCivilREVLength" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="EstadoCivilTxt"
                                                    ErrorMessage="El Estado civil no puede exceder 20 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                                <asp:RegularExpressionValidator ID="EstadoCivilREVFormat" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="EstadoCivilTxt"
                                                    ErrorMessage="Caracteres inválidos en el Estado civil."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>
                                        </div>
                                        <div class="twoColsRight">
                                            <asp:LinkButton ID="LinkButton1" Text="Historial" runat="server" />

                                            <asp:Label ID="DirTitle" Text="Dirección" runat="server" CssClass="label" />
                                            <asp:TextBox ID="DirTxt" Text='<%# Bind("Direccion")%>' runat="server" />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="DirRFV" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="DirTxt"
                                                    ErrorMessage="La Dirección es requerida." />
                                                <asp:RegularExpressionValidator ID="DirREVLength" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="DirTxt"
                                                    ErrorMessage="La Dirección no puede exceder 250 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength250  %>" />
                                                <asp:RegularExpressionValidator ID="DirREVFormat" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="DirTxt"
                                                    ErrorMessage="Caracteres inválidos en la Dirección."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>

                                            <asp:Label ID="tlfTitle" Text="Teléfono" runat="server" CssClass="label" />
                                            <asp:TextBox ID="TlfTxt" Text='<%# Bind("Telefono")%>' runat="server" />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="TelefonoRFV" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="TlfTxt"
                                                    ErrorMessage="El Teléfono es requerido." />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="TlfTxt"
                                                    ErrorMessage="El Teléfono no puede exceder 20 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength20  %>" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="TlfTxt"
                                                    ErrorMessage="Caracteres inválidos en el Teléfono."
                                                    ValidationExpression="<%$Resources:Validations,PhoneNumberFormat %>" />
                                            </div>

                                            <asp:Label ID="TlfOffTitle" Text="Teléfono de Oficina" runat="server" CssClass="label" />
                                            <asp:TextBox ID="tlfOffLabel" Text='<%# Bind("TelefonoTrabajo")%>' runat="server" />

                                            <asp:Label ID="NroHijosTitle" Text="Nro. de hijos" runat="server" CssClass="label" />
                                            <asp:TextBox ID="NroHijosTxt" Text='<%# Bind("NroHijos")%>' runat="server" />
                                            <div>
                                                <asp:RegularExpressionValidator ID="NroHijosREV" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="NroHijosTxt"
                                                    ErrorMessage="Nro. de hijos solo puede contener números enteros."
                                                    ValidationExpression="<%$Resources:Validations,IntegerFormat %>" />
                                            </div>
                                        </div>
                                        <div class="buttonsPanel clear">
                                            <asp:LinkButton ID="GuardarLB" Text="" runat="server"
                                                CommandName="Update"
                                                CssClass="button">
                                                <asp:Label ID="Label7" Text="Guardar" runat="server" />
                                            </asp:LinkButton><asp:LinkButton ID="CancelLB" runat="server"
                                                CommandName="Cancel">
                                                <asp:Label ID="Label8" Text="Cancelar" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </EditItemTemplate>
                                </asp:FormView>
                                <asp:ObjectDataSource ID="PacienteODS" runat="server"
                                    TypeName="Artexacta.App.Paciente.BLL.PacienteBLL"
                                    DataObjectTypeName="Artexacta.APP.Paciente.Paciente"
                                    OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetPacienteByPacienteId"
                                    OnSelected="PacienteODS_Selected"
                                    UpdateMethod="UpdatePacienteBasic"
                                    OnUpdated="PacienteODS_Updated">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="value" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:HiddenField ID="PacienteIdHF" runat="server" />
                            </asp:Panel>
                            <div class="clear"></div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="MedicamentoRPV" runat="server">
                            <div>
                                <asp:LinkButton ID="NewMedicamentoLB" Text="Nueva Medicamento" runat="server"
                                    CssClass="NewMedicamento" />
                                <br />
                                <%--<asp:Panel ID="HeaderPanel" runat="server" CssClass="left" Style="display: none;">
                                    <div>
                                        <strong>
                                            <asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                        <asp:Label ID="MedicoNombreLabel" runat="server" />
                                    </div>
                                    <div>
                                        <strong>
                                            <asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                        <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                    </div>
                                </asp:Panel>--%>
                                <telerik:RadGrid ID="MedicamentoGrid" runat="server"
                                    CssClass="MedicamentoGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="MedicamentoODS"
                                    OnItemCommand="MedicamentoGrid_ItemCommand"
                                    OnItemDataBound="MedicamentoGrid_ItemDataBound"
                                    OnItemCreated="MedicamentoGrid_ItemCreated">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="20mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="MedicamentoId" CommandItemDisplay="Top">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="true" ExportToPdfText="Exportar a PDF" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif"
                                                    ToolTip="Imprimir" runat="server" OnClick="MedicamentoGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgMedicamentoNullLabel" runat="server" Text="No existen Medicamentos para este caso del paciente."></asp:Label>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("MedicamentoId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Seleccionar" HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("MedicamentoId") %>'
                                                        Width="24px"
                                                        CommandName="Select"
                                                        ToolTip="Seleccionar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                HeaderText="Eliminar"
                                                CommandName="Delete"
                                                ButtonType="ImageButton"
                                                ItemStyle-Width="40px"
                                                ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Center"
                                                ImageUrl="~/Images/neutral/delete.png"
                                                ConfirmText="¿Está seguro que desea eliminar el medicamento?" />
                                            <telerik:GridBoundColumn DataField="MedicamentoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="Presentación" />
                                            <telerik:GridBoundColumn DataField="MedicamentoNombre" HeaderText="Medicamento" />
                                            <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />
                                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacionString" HeaderText="Fecha Creación" />
                                            <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="MEDICAMENTO"
                                                        CommandArgument='<%# Eval("MedicamentoId") %>'
                                                        OnCommand="FileManager_Command" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:ObjectDataSource ID="MedicamentoODS" runat="server"
                                    TypeName="Artexacta.App.Medicamento.BLL.MedicamentoBLL"
                                    OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAllMedicamentoByCasoId"
                                    OnSelected="MedicamentoODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <telerik:RadGrid ID="RecetaToPrintRadGrid" runat="server"
                                    AutoGenerateColumns="false"
                                    OnItemCreated="RecetaGrid_ItemCreated"
                                    OnPdfExporting="RadGrid_PdfExporting"
                                    Visible="false">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS" 
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="20mm"
                                            AllowModify="false" AllowAdd="false" BorderStyle="Thin" BorderType="TopAndBottom"></Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="MedicamentoId">
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="RowNumber" Visible="false" HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Container.DataSetIndex+1 %>'></asp:Literal>.-
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="MedicamentoNombre" HeaderText="Medicamento" ItemStyle-CssClass="medicamento" />
                                            <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="PRESENTACIÓN" />
                                            <telerik:GridTemplateColumn><ItemTemplate></td></tr><tr><td></td><td colspan="4">INDICACIONES:</td></tr><tr style="height:60pt;"><td style="border-bottom: 0.5pt dashed #000;"></ItemTemplate></telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Indicaciones" ItemStyle-CssClass="indicaciones" HeaderStyle-Width="5px" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <asp:Panel ID="MedicamentoPanel" runat="server"
                                    CssClass="Default_Popup" GroupingText="Nuevo Medicamento" DefaultButton="SaveMedicamento">
                                    <div>
                                        <asp:HiddenField ID="MedicamentoIdHF" runat="server" Value="0" />
                                        <asp:Label ID="Label5" Text="Presentación" runat="server" CssClass="label" />

                                        <telerik:RadComboBox ID="TipoMedicamentoDDL" runat="server"
                                            EmptyMessage="Seleccione un tipo de presentacion"
                                            CssClass="biggerField"
                                            DataSourceID="TipoMedicamentoODS"
                                            DataValueField="TipoMedicamentoId"
                                            DataTextField="Nombre">
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator ID="TipoMedicamentoCV" runat="server"
                                                ValidationGroup="Medicamento"
                                                ErrorMessage="Debe seleccionar un tipo de presentación."
                                                ClientValidationFunction="TipoMedicamentoCV_Validate"
                                                Display="Dynamic" />
                                        </div>

                                        <asp:Label ID="Label6" Text="Medicamento" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="MedicamentoComboBox" runat="server"
                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                            AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField">
                                            <WebServiceSettings Method="GetMedicamentos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="MedicamentoComboBox"
                                                ValidationGroup="Medicamento"
                                                ErrorMessage="El medicamento es requerido." />
                                        </div>
                                        <asp:Label ID="Label7" Text="Instrucciones de uso" runat="server" CssClass="label" />
                                        <asp:TextBox ID="InstruccionesTxt" runat="server"
                                            TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                                        <div class="validation">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="InstruccionesTxt"
                                                ValidationGroup="Medicamento"
                                                ErrorMessage="La Instrucción es requerida." />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="InstruccionesTxt"
                                                ValidationGroup="Medicamento"
                                                ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                                ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                        </div>
                                        <div class="buttonsPanel">
                                            <asp:LinkButton ID="SaveMedicamento" Text="" runat="server"
                                                CssClass="button"
                                                ValidationGroup="Medicamento"
                                                OnClick="SaveMedicamento_Click">
                                                <asp:Label ID="Label9" Text="Guardar" runat="server" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="CancelMedicamentoLB" Text="Cancelar" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:ObjectDataSource ID="TipoMedicamentoODS" runat="server"
                                    TypeName="Artexacta.App.TipoMedicamento.BLL.TipoMedicamentoBLL"
                                    SelectMethod="GetTipoMedicamentoList"
                                    OnSelected="TipoMedicamentoODS_Selected"></asp:ObjectDataSource>
                            </div>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>

                <div class="clear"></div>

                <asp:Panel ID="NewMedicamentoPanel" runat="server"
                    CssClass="Default_Popup" GroupingText="Nuevo Medicamento" DefaultButton="SaveNewMedicamentoLB">
                    <div>
                        <div class="error">
                            <asp:Label ID="MessageMedicamentoLabel" Text="" runat="server" />
                        </div>
                        <asp:Panel ID="Panel3" runat="server" GroupingText="Primer Medicamento">
                            <div>
                                <asp:Label ID="Label19" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento1DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataSourceID="TipoMedicamentoODS"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <asp:CustomValidator ID="CustomValidator3" runat="server"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="Debe seleccionar un tipo de presentación."
                                        ClientValidationFunction="TipoMedicamento1CV_Validate"
                                        Display="Dynamic" />
                                </div>

                                <asp:Label ID="Label20" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="MedicamentoRadComboBox" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField">
                                    <WebServiceSettings Method="GetMedicamentos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <asp:RequiredFieldValidator runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="MedicamentoRadComboBox"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="El medicamento es requerido." />
                                </div>
                                <asp:Label ID="Label21" Text="Instrucciones de uso" runat="server" CssClass="label" />
                                <asp:TextBox ID="Instrucciones1Txt" runat="server"
                                    TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                                <div class="validation">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="Instrucciones1Txt"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="La Instrucción es requerida." />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="Instrucciones1Txt"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel4" runat="server" GroupingText="Segundo Medicamento" CssClass="ExpandCollapse">
                            <div style="display: none;">
                                <asp:Label ID="Label23" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento2DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataSourceID="TipoMedicamentoODS"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>

                                <asp:Label ID="Label24" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="MedicamentoRadComboBox1" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField">
                                    <WebServiceSettings Method="GetMedicamentos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:Label ID="Label25" Text="Instrucciones de uso" runat="server" CssClass="label" />
                                <asp:TextBox ID="Instrucciones2Txt" runat="server"
                                    TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="Instrucciones2Txt"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel5" runat="server" GroupingText="Tercer Medicamento" CssClass="ExpandCollapse">
                            <div style="display: none;">
                                <asp:Label ID="Label26" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento3DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataSourceID="TipoMedicamentoODS"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>

                                <asp:Label ID="Label27" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="MedicamentoRadComboBox2" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField">
                                    <WebServiceSettings Method="GetMedicamentos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:Label ID="Label28" Text="Instrucciones de uso" runat="server" CssClass="label" />
                                <asp:TextBox ID="Instrucciones3Txt" runat="server"
                                    TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="Instrucciones3Txt"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel6" runat="server" GroupingText="Cuarto Medicamento" CssClass="ExpandCollapse">
                            <div style="display: none;">
                                <asp:Label ID="Label29" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento4DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataSourceID="TipoMedicamentoODS"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>

                                <asp:Label ID="Label30" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="MedicamentoRadComboBox3" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField">
                                    <WebServiceSettings Method="GetMedicamentos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:Label ID="Label31" Text="Instrucciones de uso" runat="server" CssClass="label" />
                                <asp:TextBox ID="Instrucciones4Txt" runat="server"
                                    TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                                <div class="validation">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="Instrucciones4Txt"
                                        ValidationGroup="NewMedicamento"
                                        ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SaveNewMedicamentoLB" Text="" runat="server"
                                CssClass="button"
                                ValidationGroup="NewMedicamento"
                                OnClick="SaveNewMedicamentoLB_Click">
                                <asp:Label ID="Label22" Text="Guardar" runat="server" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="CancelNewMedicamentoLB" Text="Cancelar" runat="server" />
                        </div>
                    </div>
                </asp:Panel>

                <RedSalud:FileManager runat="server" ID="FileManager" />

                <script type="text/javascript">
                    $(document).ready(function () {
                        createPopup("#<%=NewMedicamentoPanel.ClientID %>", ".NewMedicamento", "#<%=CancelNewMedicamentoLB.ClientID %>");
                        createPopup("#<%=MedicamentoPanel.ClientID %>", "", "#<%=CancelMedicamentoLB.ClientID %>");
                        //< % if (PacienteFV.FindControl("GeneroAsegurado") != null && CasoFV.FindControl("Panel7") != null)
                        //   {  %>
                        //var genero = ($('#< %= (PacienteFV.FindControl("GeneroAsegurado") as HiddenField).ClientID%>').val() == 'True');
                        //if (genero) {
                        //    $('#< %= CasoFV.FindControl("Panel7").ClientID%>').hide();
                        //} else {
                        //    $('#< %= CasoFV.FindControl("Panel7").ClientID%>').show();
                        //}
                        //< %}%>
                    });

                    function OpenPopupMedicamento() {
                        showPopup("#<%=MedicamentoPanel.ClientID %>");
                        $("#<%=MedicamentoPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=MedicamentoPanel.ClientID %> legend").text('Modificar Medicamento');
                    }
                    $("#<%=CancelMedicamentoLB.ClientID %>").click(function () {
                        $("#<%=MedicamentoIdHF.ClientID %>").attr('value', '0');
                        $find('<%= MedicamentoComboBox.ClientID %>').clearSelection();
                        $("#<%=InstruccionesTxt.ClientID %>").attr('value', '');
                        if ($("#<%=SaveNewMedicamentoLB.ClientID %>").length)
                            $("#<%=MessageMedicamentoLabel.ClientID %>").text('');
                        $("#<%=MedicamentoPanel.ClientID %> legend").text('Nuevo Medicamento');
                        $find('<%= TipoMedicamentoDDL.ClientID %>').clearSelection();
                    });
                </script>
                <script type="text/javascript">
                    function TipoMedicamentoCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= TipoMedicamentoDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                    function TipoMedicamento1CV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= TipoMedicamento1DDL.ClientID %>').get_value();

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
                
                <asp:HiddenField ID="AseguradoIdHF" runat="server" />
                <asp:HiddenField ID="CasoIdHF" runat="server" />
                <asp:HiddenField ID="ClienteIdHF" runat="server" />

                <asp:HiddenField ID="ExportIDHF" runat="server" />
                <asp:HiddenField ID="MedicoNameHF" runat="server" />
                <asp:HiddenField ID="EspecialidadHF" runat="server" />

                <asp:HiddenField ID="ReconsultaHF" runat="server" />
                <asp:HiddenField ID="ModeHF" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

