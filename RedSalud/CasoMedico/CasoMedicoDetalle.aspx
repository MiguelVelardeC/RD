<%@ Page Title="Caso Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CasoMedicoDetalle.aspx.cs" Inherits="CasoMedico_CasoMedicoDetalle" %>

<%--<%@ Register Src="~/UserControls/AngularControl.ascx" TagPrefix="RedSalud" TagName="AngularControl" %>--%>
<%@ Register Src="~/UserControls/AngularControl.ascx" TagPrefix="RedSalud" TagName="AngularControl" %>
<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>
<%@ Register Src="~/UserControls/TipoEstudio.ascx" TagPrefix="RedSalud" TagName="TipoEstudio" %>
<%@ Register Src="~/UserControls/PiezaOdontologia.ascx" TagPrefix="RedSalud" TagName="PiezaOdontologia" %>
<%@ Register Src="~/UserControls/FotoPaciente.ascx" TagPrefix="RedSalud" TagName="FotoPaciente" %>
<%@ Register Src="~/UserControls/FileUpload.ascx" TagPrefix="RedSalud" TagName="FileUpload" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 200px;
        }

        .auto-style2 {
            width: 50px;
        }

        .auto-style3 {
            width: 300px;
        }

        .auto-style4 {
            width: 190px;
        }

        .auto-style5 {
            width: 250px;
        }

        .alert {
            padding: 9px;
            background-color: #f44336;
            color: white;
        }
    </style>
    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angularControllerCreator.js"></script>
    <script type="text/javascript">
        function getTableName(children) {
            return $(children).parents('.PDFExportRadGrid').attr('class').replace('RadGrid', '')
                .replace('RadGrid_Default', '')
                .replace('PDFExportRadGrid', '').trim();

        }
        $(document).ready(function () {

            $('.EstudioRadGrid .ExportAllCheckBox input, .DerivacionRadGrid .ExportAllCheckBox input').hide();
            $('.ExportAllCheckBox input').click(function () {
                var table = getTableName(this);

                if ($('.' + table + ' .ExportCheckBox input[type="checkbox"]:disabled').length ==
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').length) {
                    $(this).attr('checked', '');
                    return false;
                }
                if (table == 'RecetaGrid' && $(this).prop('checked')) {
                    var checkboxToCheck = $('.' + table + ' .ExportCheckBox input[type="checkbox"]');
                    if (checkboxToCheck.length > 4) {
                        $(this).removeAttr('checked');
                        alert('No se puede seleccionar más de 4 remedios para una receta');
                    }
                }
                var checked = $(this).prop('checked');
                $('.' + table + ' .ExportCheckBox input[type="checkbox"]').attr('checked', checked ? 'checked' : '');
                $('.' + table + ' .ExportCheckBox input[type="checkbox"]').prop('checked', checked);
            });
            $('.rgExpPDF, a.LinkPdfButton').click(function () {
                var table = getTableName(this);
                var ids = [];
                $('.' + table + ' .ExportCheckBox input[type="checkbox"]').each(function () {
                    if ($(this).prop('checked')) {
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

                if ((table == 'EstudioRadGrid' || table == 'DerivacionRadGrid' || table == 'InternacionRadGrid') && $(this).prop('checked')) {
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').attr('checked', false);
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').prop('checked', false);
                    $(this).attr('checked', 'checked');
                    $(this).prop('checked', true);
                }
                if (table == 'RecetaGrid' && $(this).prop('checked')) {
                    var checkedCheckbox = $('.' + table + ' .ExportCheckBox input[type="checkbox"]:checked');
                    if (checkedCheckbox.length == 5) {
                        $(this).removeAttr('checked');
                        alert('No se puede seleccionar más de 4 remedios para una receta');
                    }
                }

                var check = ($('.' + table + ' .ExportCheckBox input[type="checkbox"]:checked').length ==
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').length);
                $('.' + table + ' .ExportAllCheckBox input[type="checkbox"]').attr('checked', check ? 'checked' : '');
                $('.' + table + ' .ExportAllCheckBox input[type="checkbox"]').prop('checked', check);
            });
            $('.ExportAllCheckBox input[type="checkbox"]').each(function () {
                var table = getTableName(this);
                if ($('.' + table + ' .ExportCheckBox input[type="checkbox"]').length == 0) {
                    $(this).attr('disabled', true);
                    $('.' + table + '.rgCommandRow').remove();
                }
            });
            $('.EstudioRadGrid .ExportCheckBox input[type="checkbox"], ' +
                '.DerivacionRadGrid .ExportCheckBox input[type="checkbox"]').each(function () {
                    $(this).attr('checked', '');
                    $(this).prop('checked', false);
                });

            var checkboxReceta = $('.RecetaGrid .ExportCheckBox input[type="checkbox"]');
            var checkboxRecetaChecked = checkboxReceta.filter(':checked');
            if (checkboxRecetaChecked.length > 4) {
                for (var i = 4; i < checkboxRecetaChecked.length; i++) {
                    checkboxRecetaChecked.eq(i).removeAttr('checked');
                }
            }

            $('.boton-impresion').click(function () {
                var casoId = $('#<%= CasoIdHF.ClientID %>').val();

                if (casoId > 0) {
                    imprimir(casoId);
                }
            });
        });
    </script>
    <!--Angular Controllers-->
    <script type="text/javascript">
        function validateAngularJS() {


            var valid = true;
            $('span.angularValidation').each(function () {
                if ($(this).css('display') != 'none') {
                    valid = false;
                }
            });
            return valid;
        }
        function exportReceta() {
            var grid = $find("<%=RecetaGrid.ClientID %>");
            grid.ExportToPdf();
        }
        function imprimir(casoId) {
            var Options = 'toolbar=no,location=no,directories=no,status=no, menubar=no, scrollbars=yes, resizable=no, width=800, height=600, top=' + ((screen.height / 2) - 300) + ', left=' + ((screen.width / 2) - 400);
            var ids = '?PacienteId=' + $('#<%= PacienteIdHF.ClientID %>').val();
            if (casoId != 0) {
                ids += '&CasoId=' + casoId;
            }

            window.open('<%= ResolveClientUrl("~/CasoMedico/HistorialPrint.aspx")%>' + ids, 'Impresión de Historias Clinicas', Options);
        }
    </script>
    <asp:Literal ID="cssCritic" runat="server" />
    <style type="text/css">
        ol.DiagnosticoList li {
            list-style-position: inside;
            margin: 5px 0;
        }

        .RadGrid_Default .rgCommandRow a {
            margin: 0 5px;
        }

            .RadGrid_Default .rgCommandRow a:hover {
                text-decoration: underline;
            }

        .recetargExpPDF {
            margin-left: 5px !important;
        }

        .mensajeIncompleto {
            border: 1px solid #808080;
            background-color: #ffd800;
            width: 80%;
            margin: 10px auto;
            padding: 15px;
        }

        .wrapperClass {
            width: 80px !important;
        }

        .PrintIcon {
            background-image: url(../Images/Neutral/ExportPrint.gif);
            background-repeat: no-repeat;
            background-position: right center;
            width: 16px;
            height: 16px;
            margin-right: 1px;
            display: inline-block;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <asp:HiddenField ID="PacienteEdad" runat="server" Value="0" />
    <asp:HiddenField ID="DerivacionId" runat="server" Value="0" />
    <asp:HiddenField ID="CasoDerivadoId" runat="server" Value="0" />
    <div class="oneColumn" ng-app>
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="CasoMedicoTitle" CssClass="title" Text="Registro de Caso Medico" runat="server" />
                <asp:LinkButton ID="cmdImpresionHistoria" CssClass="boton-impresion PrintIcon" Visible="false" runat="server" Text=""></asp:LinkButton>
                <asp:Label ID="CasoMedicoTitleDerivacion" CssClass="title" Text="" runat="server" Visible="false" />
            </div>
            <div class="columnContent">
                <asp:Panel ID="ContentMenuTop" runat="server" CssClass="contentMenu">
                    <asp:HyperLink ID="returnHL" NavigateUrl="~/CasoMedico/CasoMedicoLista.aspx" runat="server"
                        Text="Volver al Listado de Casos Medicos" />

                    <asp:HyperLink ID="newHL" NavigateUrl="~/CasoMedico/CasoMedicoRegistro.aspx" runat="server"
                        Text="Nuevo Caso Médico" Visible="false" />

                    <asp:LinkButton ID="AdminGastosLB" runat="server"
                        Text="Administrar Gastos"
                        OnClick="AdminGastosLB_Click"
                        CssClass="linkBorderLeft" />

                    <asp:HyperLink ID="odontologiaHL" NavigateUrl="#" runat="server"
                        ToolTip="Nueva Prestación" CssClass="NewOdontologia linkBorderLeft">
                        <img src="../App_Themes/Default/images/odontologia.png" alt="Nueva Prestación" />
                    </asp:HyperLink>

                    <asp:HyperLink ID="recetaHL" NavigateUrl="#" runat="server"
                        ToolTip="Nueva Receta"
                        CssClass="NewReceta linkBorderLeft">
                        <img src="../App_Themes/Default/images/receta.png" alt="Nueva Receta" />
                    </asp:HyperLink>

                    <asp:HyperLink ID="ExComplementarioHL" NavigateUrl="#" runat="server"
                        ToolTip="Nuevo Ex. Complementario"
                        CssClass="NewEstudio linkBorderLeft">
                        <img src="../App_Themes/Default/images/excomplementarios.png" alt="Nuevo Ex. Complementario" />
                    </asp:HyperLink>

                    <asp:HyperLink ID="EspecilistaHL" NavigateUrl="#" runat="server"
                        ToolTip="Nueva derivación a especilista "
                        CssClass="NewDerivacion linkBorderLeft">
                        <img src="../App_Themes/Default/images/Especialista.png" alt="Nueva derivación a especialista" />
                    </asp:HyperLink>

                    <asp:HyperLink ID="InternacionHL" NavigateUrl="#" runat="server"
                        ToolTip="Nueva Internación"
                        CssClass="NewInternacion linkBorderLeft">
                        <img src="../App_Themes/Default/images/Internacion.png" alt="Nueva Internación" />
                    </asp:HyperLink>

                    <asp:ImageButton ID="btnvideollamada" runat="server" 
                       
                        ImageUrl="~/App_Themes/Default/images/videollamadaR.png" 
                        ToolTip="Iniciar Video Llamada" OnClick="btnvideollamada_Click" />

                </asp:Panel>
                <div class="error">
                    <asp:Label ID="EmergenciaGastosErrorLabel" runat="server"
                        Text="No se puede modificar la Emergencia por que tiene registros de gastos."
                        Visible="false" />
                    <div class="clearfix"></div>
                    <asp:Label ID="EmergenciaErrorLabel" runat="server"
                        Text="No se puede modificar la Emergencia por que no tiene Hospitales registrados."
                        Visible="false" />
                </div>
                <div class="left" style="max-width: 70%; width: 70%;">
                    <telerik:RadTabStrip ID="CasoTab" runat="server" MultiPageID="CasoMP" SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab Text="Info. consulta" PageViewID="CasoRPV"></telerik:RadTab>
                            <telerik:RadTab Text="Odontologia" ID="OdontologiaRT" runat="server"></telerik:RadTab>
                            <telerik:RadTab Text="Recetas" ID="RecetaRT" runat="server"></telerik:RadTab>
                            <telerik:RadTab Text="Examenes Complementarios" ID="EstudioRT" runat="server"></telerik:RadTab>
                            <telerik:RadTab Text="Especialista" ID="DerivacionRT" runat="server"></telerik:RadTab>
                            <telerik:RadTab Text="Internación" ID="InternacionRT" runat="server"></telerik:RadTab>
                            <telerik:RadTab Text="Emergencia" ID="EmergenciaRT" runat="server"></telerik:RadTab>

                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="CasoMP" runat="server"
                        CssClass="RadMultiPage"
                        SelectedIndex="0">
                        <telerik:RadPageView ID="CasoRPV" runat="server">
                            <div>
                                <%--<asp:Label ID="SubTitleLabel" Text="Registro de Caso Medico" runat="server" CssClass="subTitleLabel" />
                                <br />--%>
                                <asp:FormView ID="CasoFV" runat="server"
                                    OnDataBound="CasoFV_DataBound"
                                    OnModeChanged="CasoFV_ModeChanged"
                                    DataSourceID="CasoODS"
                                    OnItemInserting="CasoFV_ItemInserting">
                                    <ItemTemplate>
                                        <asp:Panel runat="server">
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    //////////////////////////////////
                                                    var PesoDisplayObj = $("#<%= CasoFV.FindControl("Label38").ClientID %>");
                                                    var EstaturaMObj = $("#<%= CasoFV.FindControl("Label40").ClientID %>");
                                                    if (PesoDisplayObj != null && PesoDisplayObj != undefined &&
                                                        EstaturaMObj != null && EstaturaMObj != undefined) {
                                                        var PesoDisplay = PesoDisplayObj.text();
                                                        var EstaturaM = EstaturaMObj.text();


                                                        if (PesoDisplay != null && PesoDisplay != undefined &&
                                                            EstaturaM != null && EstaturaM != undefined) {
                                                            var Peso = PesoDisplay.substring(0, PesoDisplay.length - 2);
                                                            var PesoDouble = parseFloat(Peso);
                                                            var EstaturaDouble = parseFloat(EstaturaM.replace(",", "."));
                                                            var imc = (!isNaN(PesoDouble) && !isNaN(EstaturaDouble)) ? PesoDouble / ((EstaturaDouble) * (EstaturaDouble)) : 0.00;

                                                            $('#Label_SPan').text(imc.toFixed(2));
                                                            getIMCDescription(imc.toFixed(2));
                                                        } else {

                                                        }
                                                    }

                                                    //check age!
                                                    var Edad = $('#<%= PacienteEdad.ClientID %>').val();
                                                    var intEdad = parseInt(Edad);
                                                    if (!isNaN(intEdad)) {
                                                        console.log("Edad " + Edad);
                                                        if (intEdad < 18) {
                                                            $('.labelIMC').hide();
                                                            $('#Label_SPan').hide();
                                                            $('#Label_Descripcion').hide();
                                                        }
                                                    }


                                                    //////////////////////////////////
                                                });
                                                function getIMCDescription(imc) {
                                                    switch (true) {
                                                        case imc < 0:
                                                            $('#Label_Descripcion').text('');
                                                            break;
                                                        case imc == 0:
                                                            $('#Label_Descripcion').text('');
                                                            break;
                                                        case imc < 16.00:
                                                            $('#Label_Descripcion').text('Infrapeso: Delgadez Severa');
                                                            break;
                                                        case imc >= 16.00 && imc <= 16.99:
                                                            $('#Label_Descripcion').text('Infrapeso: Delgadez moderada');
                                                            break;
                                                        case imc >= 17.00 && imc <= 18.49:
                                                            $('#Label_Descripcion').text('Infrapeso: Delgadez aceptable');
                                                            break;
                                                        case imc >= 18.50 && imc <= 24.99:
                                                            $('#Label_Descripcion').text('Peso Normal');
                                                            break;
                                                        case imc >= 25.00 && imc <= 29.99:
                                                            $('#Label_Descripcion').text('Sobrepeso');
                                                            break;
                                                        case imc >= 30.00 && imc <= 34.99:
                                                            $('#Label_Descripcion').text('Obeso: Tipo I');
                                                            break;
                                                        case imc >= 35.00 && imc <= 40.00:
                                                            $('#Label_Descripcion').text('Obeso: Tipo II');
                                                            break;
                                                        case imc > 40.00:
                                                            $('#Label_Descripcion').text('Obeso: Tipo III');
                                                            break;

                                                    }
                                                }
                                            </script>
                                            <asp:HiddenField ID="CasoMedicoIdHF" runat="server" Value='<%# Bind("CasoId") %>' />
                                            <asp:HiddenField ID="CitaIdHF" runat="server" Value='<%# Bind("CitaId") %>' />
                                            <asp:HiddenField ID="CorrelativoHF" runat="server" Value='<%# Bind("Correlativo") %>' />
                                            <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" Value='<%# Bind("MotivoConsultaId") %>' />

                                            <asp:HiddenField ID="EstadoHF" runat="server" Value='<%# Bind("Estado") %>' />
                                            <asp:HiddenField ID="PacienteIdHF" runat="server" Value='<%# Bind("PacienteId") %>' />

                                            <asp:HiddenField ID="HistoriaIdHF" runat="server" Value='<%# Bind("HistoriaId") %>' />
                                            <asp:Label ID="ProveedorLabel" Text="Proveedor de la Emergencia" runat="server"
                                                CssClass="label" Visible="false" />
                                            <asp:Label ID="ProveedorNameLabel" Text='<%# Bind("NombreProveedorJuridico") %>' runat="server" Visible="false" />

                                            <asp:Label ID="Label33" Text="Código del Caso" runat="server" CssClass="label" />
                                            <asp:Label ID="CodigoCasoLabel" Text='<%# Bind("CodigoCaso") %>' runat="server" />

                                            <asp:Label ID="Label37" Text='<%# Convert.ToBoolean(Eval("IsReconsulta").ToString())? "Fecha de Reconsulta":"Fecha de creación" %>' CssClass="label" runat="server"></asp:Label>
                                            <asp:Label ID="FechaCLabel" Text='<%# Bind("FechaCreacionString") %>' runat="server" />

                                            <br />
                                            <br />
                                            <asp:CheckBox CssClass="CasoCriticoEnfermedadCronica" Text="Paciente con enfermedades cronicas" runat="server" Checked='<%# Eval("CasoCritico") %>' Enabled="false" />
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    if ($(".CasoCriticoEnfermedadCronica input").prop("checked")) {
                                                        $('#PacienteCronicoPanel').show();
                                                    } else {
                                                        $('#PacienteCronicoPanel').hide();
                                                    }
                                                });
                                            </script>
                                            <div id="PacienteCronicoPanel" style="display: none;">
                                                <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server" DataSourceID="EnfermedadesCronicasODS">
                                                    <HeaderTemplate>
                                                        <span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li style="font-weight: normal;">
                                                            <asp:Literal Text='<%# Eval("Nombre") %>' runat="server" /></li>
                                                    </ItemTemplate>
                                                    <FooterTemplate></ul></FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <asp:Label ID="MCLabel" Text="Motivo de la consulta" runat="server" CssClass="label" />
                                            <asp:Label ID="MotivoConsultaTxt" runat="server"
                                                Text='<%# Bind("MotivoConsulta") %>' />

                                            <asp:Label Text="Enfermedad Actual" runat="server" CssClass="label"  Visible='<%# !IsEmergencia %>'  />
                                            <asp:Label ID="EnfermedadActualLabel" runat="server" Visible='<%# !IsEmergencia %>'
                                                Text='<%# Bind("EnfermedadActual") %>' />
                                            <br />
                                            <br />
                                            <asp:Panel runat="server" GroupingText="Antecedentes Personales y Patol&#243;gicos"
                                                CssClass="ExpandCollapse" Visible='<%# !IsEmergencia%>' >
                                                <div style="display: none;">
                                                    <RedSalud:AngularControl runat="server" ID="Antecedentesfamiliares" readOnly="true" maxLength="700" JSonData='<%# Bind("Antecedentes") %>'
                                                        JSonDefaultData='<%# defaultJSonAntecedentes %>'  Visible='<%# !IsEmergencia%>'/>
                                                </div>
                                            </asp:Panel>
                                            <br />
                                            <asp:Panel ID="AntecedentesGinecoobstetricosPanel" runat="server" GroupingText="Antecedentes Ginecoobstetricos"
                                                CssClass="ExpandCollapse AntecedentesGinecoobstetricosPanel" Visible='<%# !IsOdontologia && !IsFemenino  && !IsEmergencia%>'>
                                                <div style="display: none;">
                                                    <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoobstetricos" readOnly="true" maxLength="1950"
                                                        JSonData='<%# Bind("AntecedentesGinecoobstetricos") %>' />
                                                </div>
                                            </asp:Panel>

                                            <asp:Label ID="SignosVitalesLabel" Text="Signos vitales" runat="server" CssClass="label"
                                                Visible='<%# !IsOdontologia && !IsEmergencia %>' />
                                            <table id="SignosVitalesTable" runat="server" visible='<%# !IsOdontologia && !IsEmergencia %>'>
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
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" Text="Peso:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label38" runat="server"
                                                            Text='<%# Bind("PesoDisplay") %>' />
                                                    </td>
                                                    <td style="padding-left: 15px;">
                                                        <asp:Label ID="Label39" Text="Estatura (en metros):" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label40" runat="server"
                                                            Text='<%# Bind("EstaturaM") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="label labelIMC" style="">IMC:</span>
                                                    </td>
                                                    <td>
                                                        <span id="Label_SPan"></span>
                                                    </td>
                                                    <td>
                                                        <span id="Label_Descripcion"></span>
                                                    </td>
                                                </tr>
                                            </table>

                                            <asp:Label ID="EFRySLabel" Text='<%# "Examen físico " + (IsEmergencia ? "general" : "regional")  %>' runat="server" CssClass="label"
                                                Visible='<%# !IsOdontologia && !IsEmergencia %>' />
                                            <div runat="server" visible='<%# !IsEmergencia %>'>
                                                <style>
                                                    .ng-scope ul {
                                                        list-style: none;
                                                        margin: 0 !important;
                                                    }
                                                </style>
                                            </div>
                                            <RedSalud:AngularControl runat="server" ID="ExFisico" readOnly="true" maxLength="440"
                                                Visible='<%# !IsOdontologia && !IsEmergencia %>'
                                                JSonData='<%# Bind("ExFisicoRegionalyDeSistema") %>' />

                                            <div runat="server" visible='<%# !IsEmergencia %>'>
                                                <asp:Label ID="BHLabel" Text="Resultados de laboratorio y otros exámenes" runat="server" CssClass="label" />
                                                <asp:Label ID="BiometriaHematicaTxt" runat="server"
                                                    Text='<%# Bind("BiometriaHematica") %>' />
                                            </div>
                                            <asp:Label Text="Observaciones" runat="server" CssClass="label" />
                                            <asp:Label ID="ObservacionesLabel" runat="server" CssClass="ObservacionesLabel"
                                                Text='<%# Bind("Observaciones") %>' />
                                            <asp:TextBox ID="ObservacionesTextBox" runat="server"
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField ObservacionesTextBox"
                                                Text='<%# Eval("Observaciones") %>' Style="display: none;" />
                                            <div id="ObservacionesValidation" class="validation" style="display: none;">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="ObservacionesTextBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en las observaciones."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>

                                            <asp:Label ID="DPLabel" Text="Diagnostico Presuntivo" runat="server" CssClass="label" />
                                            <asp:Label ID="DiagnosticoPresuntivoTxt" runat="server" Style="display: block"
                                                Text='<%# Bind("DiagnosticoPresuntivoForDisplay") %>' />
                                            <asp:Label ID="DiagnosticoPresuntivo2Txt" runat="server" Style="display: block"
                                                Text='<%# Bind("Enfermedad2") %>' />
                                            <asp:Label ID="DiagnosticoPresuntivo3Txt" runat="server" Style="display: block"
                                                Text='<%# Bind("Enfermedad3") %>' />
                                            <asp:Label ID="DiagnosticoPresuntivo4Txt" runat="server" Style="display: block"
                                                Text='<%# Bind("DiagnosticoPresuntivoExtra") %>' />


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
                                            <div id="GuardarObservaciones" class="buttonsPanel" style="display: none;">
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
                                        <asp:Panel runat="server">
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    updateIMC();
                                                    //////////////////////////////////
                                                    //check age!
                                                    var Edad = $('#<%= PacienteEdad.ClientID %>').val();
                                                    var intEdad = parseInt(Edad);
                                                    if (!isNaN(intEdad)) {
                                                        console.log("Edad " + Edad);
                                                        if (intEdad < 18) {
                                                            $('.labelIMC').hide();
                                                            $('#Label_SPan').hide();
                                                            $('#Label_Descripcion').hide();
                                                        }
                                                    }
                                                    /*
                                                    if (PesoDisplay != null && PesoDisplay != undefined &&
                                                        EstaturaM != null && EstaturaM != undefined) {
                                                        var Peso = PesoDisplay.substring(0, PesoDisplay.length - 2);
                                                        var PesoDouble = parseFloat(Peso);
                                                        var EstaturaDouble = parseFloat(EstaturaM.replace(",", "."));
                                                        var imc = (!isNaN(PesoDouble) && !isNaN(EstaturaDouble)) ? PesoDouble / ((EstaturaDouble) * (EstaturaDouble)) : 0.00;
            
                                                        $('#Label_SPan').text(imc.toFixed(2));
                                                    } else {
            
                                                    }*/
                                                    //////////////////////////////////

                                                    updateDerivacionId();
                                                });

                                                function updateDerivacionId() {
                                                    var derivacionParameter = $("#<%= DerivacionId.ClientID %>").val();
                                                    if (derivacionParameter > 0) {
                                                        var derivacionObj = $('#<%= CasoFV.FindControl("DerivacionIdHidden").ClientID %>');
                                                        derivacionObj.val(derivacionParameter);
                                                    }
                                                }

                                                function updateIMC() {
                                                    var itemPeso = $('#<%= CasoFV.FindControl("PesoTextBox").ClientID %>');
                                                    var itemEstatura = $('#<%= CasoFV.FindControl("EstaturaTextBox").ClientID %>');
                                                    var Peso = convertToNumber(itemPeso.val());
                                                    var Estatura = convertToNumber(itemEstatura.val());
                                                    var imc = calculateIMC(Estatura, Peso);
                                                    $('#Label_SPan').text(String(imc).replace(".", ","));
                                                    getIMCDescription(imc);
                                                }

                                                function pesoKeyPress(sender, eventArgs) {
                                                    var item = $('#<%= CasoFV.FindControl("PesoTextBox").ClientID %>');
                                                    var lastKeyCharacter = eventArgs.get_keyCharacter();
                                                    if (isCharacterNumber(lastKeyCharacter)) {
                                                        //console.log("value: " + item.val() + eventArgs.get_keyCharacter());
                                                        var strPeso = item.val() + eventArgs.get_keyCharacter();
                                                        var dblPeso = convertToNumber(strPeso);
                                                        var Estatura = $('#<%= CasoFV.FindControl("EstaturaTextBox").ClientID %>').val();
                                                        var imc = calculateIMC(convertToNumber(Estatura), dblPeso);
                                                        $('#Label_SPan').text(String(imc).replace(".", ","));
                                                        getIMCDescription(imc);
                                                    }
                                                }

                                                function estaturaKeyPress(sender, eventArgs) {
                                                    var item = $('#<%= CasoFV.FindControl("EstaturaTextBox").ClientID %>');
                                                    var lastKeyCharacter = eventArgs.get_keyCharacter();
                                                    if (isCharacterNumber(lastKeyCharacter)) {
                                                        //console.log("value: " + item.val() + eventArgs.get_keyCharacter());
                                                        var strEstatura = item.val() + eventArgs.get_keyCharacter();
                                                        var dblEstatura = convertToNumber(strEstatura);
                                                        var Peso = $('#<%= CasoFV.FindControl("PesoTextBox").ClientID %>').val();
                                                        var imc = calculateIMC(dblEstatura, convertToNumber(Peso));
                                                        $('#Label_SPan').text(String(imc).replace(".", ","));
                                                        getIMCDescription(imc);
                                                    }
                                                }

                                                function isCharacterNumber(character) {
                                                    if (character != null && character != undefined) {
                                                        if (character == '0' || character == '1' || character == '2' || character == '3' ||
                                                            character == '4' || character == '5' || character == '6' || character == '7' ||
                                                            character == '8' || character == '9') {
                                                            return true;
                                                        }
                                                    }
                                                    return false;
                                                }

                                                function convertToNumber(value) {
                                                    var convertDecimals = value.replace(",", ".");
                                                    var doubleNumber = parseFloat(convertDecimals);

                                                    if (!isNaN(doubleNumber)) {
                                                        return doubleNumber;
                                                    }

                                                    return -1;
                                                }

                                                function calculateIMC(estatura, peso) {
                                                    if (estatura != -1 && peso != -1) {
                                                        var estaturaSqurd = (estatura * estatura);
                                                        var result = (peso / estaturaSqurd);
                                                        return result.toFixed(2);
                                                    }
                                                    return 0;
                                                }

                                                function getIMCDescription(imc) {
                                                    switch (true) {
                                                        case imc < 0:
                                                            $('#Label_Descripcion').text('');
                                                            break;
                                                        case imc == 0:
                                                            $('#Label_Descripcion').text('');
                                                            break;
                                                        case imc < 16.00:
                                                            $('#Label_Descripcion').text('Infrapeso: Delgadez Severa');
                                                            break;
                                                        case imc >= 16.00 && imc <= 16.99:
                                                            $('#Label_Descripcion').text('Infrapeso: Delgadez moderada');
                                                            break;
                                                        case imc >= 17.00 && imc <= 18.49:
                                                            $('#Label_Descripcion').text('Infrapeso: Delgadez aceptable');
                                                            break;
                                                        case imc >= 18.50 && imc <= 24.99:
                                                            $('#Label_Descripcion').text('Peso Normal');
                                                            break;
                                                        case imc >= 25.00 && imc <= 29.99:
                                                            $('#Label_Descripcion').text('Sobrepeso');
                                                            break;
                                                        case imc >= 30.00 && imc <= 34.99:
                                                            $('#Label_Descripcion').text('Obeso: Tipo I');
                                                            break;
                                                        case imc >= 35.00 && imc <= 40.00:
                                                            $('#Label_Descripcion').text('Obeso: Tipo II');
                                                            break;
                                                        case imc > 40.00:
                                                            $('#Label_Descripcion').text('Obeso: Tipo III');
                                                            break;

                                                    }
                                                }
                                            </script>
                                            <asp:HiddenField ID="CasoMedicoIdHF" runat="server" Value='<%# Bind("CasoId") %>' />
                                            <asp:HiddenField ID="CitaIdHF" runat="server" Value='<%# Bind("CitaId") %>' />
                                            <asp:HiddenField ID="CorrelativoHF" runat="server" Value='<%# Bind("Correlativo") %>' />
                                            <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" Value='<%# Bind("MotivoConsultaId") %>' />
                                            <asp:HiddenField ID="EstadoHF" runat="server" Value='<%# Bind("Estado") %>' />
                                            <asp:HiddenField ID="PacienteIdHF" runat="server" Value='<%# Bind("PacienteId") %>' />
                                            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Bind("Dirty") %>' />
                                            <asp:HiddenField ID="HistoriaIdHF" runat="server" Value='<%# Bind("HistoriaId") %>' />
                                            <asp:HiddenField ID="CodigoCasoHF" runat="server" Value='<%# Bind("CodigoCaso") %>' />
                                            <asp:HiddenField ID="DerivacionIdHidden" runat="server" Value='<%# Bind("DerivacionId") %>' />

                                            <asp:Label Text="Ciudad" ID="LabelCiudadEmergencia" runat="server" CssClass="label" Visible="false"/>

                                            <asp:ObjectDataSource ID="CiudadEmergenciaODS" runat="server"
                                                TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                                                OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="getCiudadList"
                                                OnSelected="CiudadExComplementarioODS_Selected"></asp:ObjectDataSource>

                                            <telerik:RadComboBox ID="RadCiudadEmergencia" runat="server" Visible="false"
                                                Width="250px"
                                                DataSourceID="CiudadEmergenciaODS"
                                                OnClientSelectedIndexChanged="RadCiudadEmergenciaComboBox_OnClientSelectedIndexChanged"
                                                DataValueField="CiudadId"
                                                DataTextField="Nombre"
                                                AutoPostBack="false"
                                                EmptyMessage="Seleccione una Ciudad"
                                                MarkFirstMatch="true">
                                            </telerik:RadComboBox>

                                            <asp:Label ID="ProveedorLabel" Text="Proveedor de la Emergencia" runat="server" 
                                                CssClass="label" Visible="false" />
                                            <telerik:RadComboBox ID="ProveedorDDL" runat="server" Visible="false"
                                            EnableLoadOnDemand="true"
                                            Width="250px"
                                            EmptyMessage="Seleccione un Proveedor"
                                            OnClientItemsRequesting="ProveedorDDL_OnClientItemsRequesting"
                                                OnClientSelectedIndexChanged="ProveedorDDL_ClientSelectedIndexChanged"
                                                OnClientLoad="ProveedorDProveedorDDL_ClientLoad"
                                            MarkFirstMatch="true"
                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                            AutoPostBack="false">
                                            <WebServiceSettings Method="GetProveedorPorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                          </telerik:RadComboBox>
                                            <asp:HiddenField ID="ProveedorIdHF" runat="server" Value='<%# Bind("ProveedorId") %>' />
                                            <script type="text/javascript">
                                                function ProveedorDDL_ClientSelectedIndexChanged(sender, event) {
                                                    $('#<%= CasoFV.FindControl("ProveedorIdHF").ClientID %>').val(sender.get_value());
                                                }
                                                function ProveedorDProveedorDDL_ClientLoad(sender, event) {
                                                    $('#<%= CasoFV.FindControl("ProveedorIdHF").ClientID %>').val(sender.get_value());
                                                }
                                                function RadCiudadEmergenciaComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                                    $find('<%= CasoFV.FindControl("ProveedorDDL").ClientID %>').clearSelection();
                                                    var combo = $find("<%=  CasoFV.FindControl("ProveedorDDL").ClientID %>");
                                                    combo.requestItems('', false);
                                                }
                                                function ProveedorDDL_OnClientItemsRequesting(sender, eventArgs) {
                                                 var combo = $find("<%= CasoFV.FindControl("RadCiudadEmergencia").ClientID %>");
                                                 var context = eventArgs.get_context();
                                                 context["ciudadId"] = combo.get_value();
                                                 context["redMedicaPaciente"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                                 context["tipoPriveedor"] = 'HOSPITAL';
                                    }
                                            </script>

                                            <%--<asp:Label ID="Label33" Text="Código del Caso" runat="server" CssClass="label" />--%>
                                            <%--<asp:Label ID="CodigoCasoLabel" Text='<%# Bind("CodigoCaso") %>' runat="server" />--%>
                                            <asp:Label Text='<%# Convert.ToBoolean(Eval("IsReconsulta").ToString())? "Fecha de Reconsulta":"Fecha de creación" %>' CssClass="label" runat="server"></asp:Label>
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
                                                Style="display: inline-block;"
                                                SelectedDate='<%# Bind("HoraCreacion") %>'>
                                            </telerik:RadTimePicker>
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="DateRFV" runat="server"
                                                    ControlToValidate="FechaCreacion"
                                                    ErrorMessage="Debe seleccionar la fecha de creación del Caso Medico."
                                                    Display="Dynamic"
                                                    ValidationGroup="UpdateCaso">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <br />
                                            <br />
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
                                                function EnfermedadCronicaRadComboBox_ClientSelectedIndexChanged(sender, eventArgs) {
                                                    var item = eventArgs.get_item();
                                                    $('#<%= CasoFV.FindControl("EnfermedadCronicaIdHF").ClientID%>').val(item.get_value());
                                                }


                                            </script>
                                            <div id="PacienteCronicoPanel" style="display: none;">
                                                <asp:Repeater ID="EnfermedadesCronicasRepeater" runat="server" DataSourceID="EnfermedadesCronicasODS">
                                                    <HeaderTemplate>
                                                        <span class="label">ENFERMEDADES CRÓNICAS DEL PACIENTE</span><ul>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li style="font-weight: normal;">
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
                                                    OnClientSelectedIndexChanged="EnfermedadCronicaRadComboBox_ClientSelectedIndexChanged"
                                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                    AutoPostBack="false"
                                                    MarkFirstMatch="true">
                                                    <WebServiceSettings Method="GetEnfermedadCronicaAutocomplete" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                                </telerik:RadComboBox>
                                                <asp:HiddenField ID="EnfermedadCronicaIdHF" runat="server" Value="" />
                                                <asp:LinkButton ID="NewEnfermedadCronicaImageButton"
                                                    OnClick="NewEnfermedadCronicaImageButton_Click"
                                                    runat="server" CssClass="right" Style="margin-top: 2px; margin-left: 2px;">
                                                    <asp:image imageurl="~/Images/Neutral/new.png" runat="server" />
                                                    AÑADIR NUEVA PATOLOGIA CRONICA
                                                </asp:LinkButton>
                                            </div>
                                            <asp:Label ID="MCLabel" Text="Motivo de la consulta" runat="server" CssClass="label" />
                                            <asp:TextBox ID="MotivoConsultaTxt" runat="server"
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                Text='<%# Bind("MotivoConsulta") %>' />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="MotivoCRFV" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="MotivoConsultaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El motivo de la consulta es requerido." />
                                                <asp:RegularExpressionValidator ID="MotivoCREVLength" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="MotivoConsultaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El motivo de la consulta no puede exceder 2000 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                                <asp:RegularExpressionValidator ID="MotivoCREVFormat" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="MotivoConsultaTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en el nombre Motivo de la consulta."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>
                                            <asp:Label ID="Label14" Text="ENFERMEDAD ACTUAL" runat="server" CssClass="label"  Visible='<%# !IsEmergencia %>'/>
                                            <asp:TextBox ID="EnfermedadActualTextBox" runat="server" Visible='<%# !IsEmergencia %>'
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                Text='<%# Bind("EnfermedadActual") %>' />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="EnfermedadActualTextBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="La enfermedad actual es requerida." />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                    ControlToValidate="EnfermedadActualTextBox"
                                                    Display="Dynamic"
                                                    ErrorMessage="La enfermedad actual no puede exceder 2000 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>"
                                                    ValidationGroup="UpdateCaso">
                                                </asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="EnfermedadActualTextBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en la enfermedad actual."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>
                                            <br />
                                            <asp:Panel runat="server" GroupingText="Antecedentes Personales y Patol&#243;gicos"  Visible='<%# !IsEmergencia%>'
                                                CssClass="ExpandCollapse">
                                                <div style="display: none;">
                                                    <RedSalud:AngularControl runat="server" ID="Antecedentesfamiliares" readOnly="false" maxLength="700" JSonData='<%# Bind("Antecedentes") %>'
                                                        JSonDefaultData='<%# defaultJSonAntecedentes %>' Visible='<%# !IsEmergencia%>' />
                                                </div>
                                            </asp:Panel>
                                            <br />
                                            <asp:Panel ID="AntecedentesGinecoobstetricosPanel" runat="server" GroupingText="Antecedentes Ginecoobstetricos"
                                                CssClass="ExpandCollapse AntecedentesGinecoobstetricosPanel" Visible='<%# !IsOdontologia && !IsFemenino && !IsEmergencia%>'>
                                                <div style="display: none;">
                                                    <RedSalud:AngularControl runat="server" ID="AntecedentesGinecoobstetricos" readOnly="false" maxLength="1950"
                                                        JSonData='<%# Bind("AntecedentesGinecoobstetricos") %>' />
                                                </div>
                                            </asp:Panel>

                                            <asp:Label ID="SignosVitalesLabel" Text="Examenes físicos vitales" runat="server" CssClass="label"
                                                Visible='<%# !IsOdontologia && !IsEmergencia %>' />
                                            <table id="SignosVitalesTable" runat="server" visible='<%# !IsOdontologia && !IsEmergencia %>'>
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
                                                            <asp:RequiredFieldValidator ID="PresionArterialREF" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="PresionArterialTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La Presión Arterial es requerido."></asp:RequiredFieldValidator>
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
                                                            <asp:RequiredFieldValidator ID="TemperaturaRFV" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="TemperaturaTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La Temperatura es requerido."></asp:RequiredFieldValidator>
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
                                                            <asp:RequiredFieldValidator ID="PulsoRFV" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="Pulsotxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="El Pulso es requerido."></asp:RequiredFieldValidator>
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
                                                                ControlToValidate="FrecuenciaCTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La Frecuencia Cardiaca solo puede contener números de 3 dígitos máximo."
                                                                ValidationExpression="<%$ Resources: Validations, TemperaturaFormat  %>" />
                                                            <asp:RequiredFieldValidator ID="FrecuenciaCRFV" runat="server"
                                                                Display="Dynamic"
                                                                ControlToValidate="FrecuenciaCTxt"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La Frecuencia Cardiaca es requerido."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label41" Text="Peso:" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="PesoTextBox" runat="server" CssClass="smallField"
                                                            Width="80px" Value='<%# Bind("PesoDouble") %>'
                                                            NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""
                                                            ClientEvents-OnKeyPress="pesoKeyPress"
                                                            ClientEvents-OnBlur="updateIMC"
                                                            MinValue="0" MaxValue="500" OnDataBinding="PesoTextBox_DataBinding"
                                                            IncrementSettings-InterceptMouseWheel="false"
                                                            IncrementSettings-InterceptArrowKeys="false" />
                                                        kg
                                                      <div class="validation">
                                                          <asp:RequiredFieldValidator ID="PesoRFV" runat="server"
                                                              ControlToValidate="PesoTextBox"
                                                              ValidationGroup="UpdateCaso"
                                                              ErrorMessage="El Peso es obligatorio en Kg"
                                                              Display="Dynamic"></asp:RequiredFieldValidator>
                                                      </div>
                                                    </td>
                                                    <td style="padding-left: 15px;">
                                                        <asp:Label ID="Label42" Text="Estatura (en metros):" runat="server" CssClass="label" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="EstaturaTextBox" runat="server" CssClass="smallField"
                                                            Value='<%# Bind("EstaturaM") %>'
                                                            NumberFormat-DecimalDigits="2"
                                                            MaxValue="3"
                                                            MinValue="0.00"
                                                            WrapperCssClass="wrapperClass"
                                                            RenderMode="Lightweight"
                                                            ClientEvents-OnKeyPress="estaturaKeyPress"
                                                            ClientEvents-OnBlur="updateIMC"
                                                            OnDataBinding="EstaturaTextBox_DataBinding"
                                                            IncrementSettings-InterceptMouseWheel="false"
                                                            IncrementSettings-InterceptArrowKeys="false">
                                                        </telerik:RadNumericTextBox>
                                                        <div class="validation">
                                                            <asp:RequiredFieldValidator ID="RequiredEstatura" runat="server"
                                                                ControlToValidate="EstaturaTextBox"
                                                                ValidationGroup="UpdateCaso"
                                                                ErrorMessage="La estatura (en metros) es obligatoria"
                                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="label labelIMC" style="">IMC:</span>
                                                    </td>
                                                    <td>
                                                        <span id="Label_SPan"></span>
                                                    </td>
                                                    <td>
                                                        <span id="Label_Descripcion"></span>
                                                    </td>
                                                </tr>
                                            </table>

                                            <asp:Label ID="EFRySLabel" Text="Examen físico regional" runat="server" CssClass="label"
                                                Visible='<%# !IsOdontologia && !IsEmergencia %>' />
                                            <RedSalud:AngularControl runat="server" ID="ExFisico" readOnly="false" maxLength="440" JSonData='<%# Bind("ExFisicoRegionalyDeSistema") %>'
                                                JSonDefaultData='<%# defaultJSonExFisicos %>' Visible='<%# !IsOdontologia && !IsEmergencia  %>' />
                                            <div runat="server" visible='<%# !IsEmergencia %>'>
                                                <asp:Label ID="BHLabel" Text="Resultados de laboratorio y otros exámenes" runat="server" CssClass="label" />
                                                <asp:TextBox ID="BiometriaHematicaTxt" runat="server"
                                                    TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                    Text='<%# Bind("BiometriaHematica") %>' />

                                                <div class="validation">
                                                    <asp:RequiredFieldValidator ID="BiometriaHematicaRFV" runat="server"
                                                        Display="Dynamic"
                                                        ControlToValidate="BiometriaHematicaTxt"
                                                        ValidationGroup="UpdateCaso"
                                                        ErrorMessage="Los resultados de laboratorio y otros exámenes es requerido." />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        Display="Dynamic"
                                                        ControlToValidate="BiometriaHematicaTxt"
                                                        ValidationGroup="UpdateCaso"
                                                        ErrorMessage="Los resultados de laboratorio y otros exámenes no puede exceder 2000 caracteres."
                                                        ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                        Display="Dynamic"
                                                        ControlToValidate="BiometriaHematicaTxt"
                                                        ValidationGroup="UpdateCaso"
                                                        ErrorMessage="Caracteres inválidos en los resultados de laboratorio y otros exámenes."
                                                        ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                                </div>

                                            </div>
                                            <asp:Label Text="Observaciones" runat="server" CssClass="label" />
                                            <asp:TextBox ID="ObservacionesTextBox" runat="server"
                                                TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                Text='<%# Bind("Observaciones") %>' />
                                            <div class="validation">
                                                <asp:RequiredFieldValidator ID="ObservacionesRFV" runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="ObservacionesTextBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="La Observacion es requerida." />
                                                <asp:RegularExpressionValidator runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="ObservacionesTextBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="Caracteres inválidos en las observaciones."
                                                    ValidationExpression="<%$Resources:Validations,DescriptionFormat %>" />
                                            </div>

                                            <asp:Label Text="Diagnostico Presuntivo" runat="server" CssClass="label" />
                                            <ol class="DiagnosticoList">
                                                <li>
                                                    <telerik:RadComboBox ID="EnfermedadesComboBox" runat="server"
                                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                                        OnClientLoad="EnfermedadesComboBox_ClientLoad"
                                                        OnClientTextChange="EnfermedadesComboBox_TextChange"
                                                        OnClientSelectedIndexChanged="EnfermedadesComboBox_ClientSelectedIndexChanged">
                                                        <WebServiceSettings Method="GetEnfermedades" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                                    </telerik:RadComboBox>
                                                    <asp:HiddenField runat="server" ID="EnfermedadHF" Value='<%# Bind("EnfermedadId") %>' />
                                                </li>
                                                <li>
                                                    <telerik:RadComboBox ID="Enfermedades2ComboBox" runat="server"
                                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                                        OnClientSelectedIndexChanged="EnfermedadesComboBox_ClientSelectedIndexChanged"
                                                        OnClientTextChange="EnfermedadesComboBox_TextChange">
                                                        <WebServiceSettings Method="GetEnfermedades" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                                    </telerik:RadComboBox>
                                                    <asp:HiddenField runat="server" ID="Enfermedad2HF" Value='<%# Bind("Enfermedad2Id") %>' />
                                                </li>
                                                <li>
                                                    <telerik:RadComboBox ID="Enfermedades3ComboBox" runat="server"
                                                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                                        OnClientLoad="Enfermedades2ComboBox_ClientLoad"
                                                        OnClientSelectedIndexChanged="EnfermedadesComboBox_ClientSelectedIndexChanged">
                                                        <WebServiceSettings Method="GetEnfermedades" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                                    </telerik:RadComboBox>
                                                    <asp:HiddenField runat="server" ID="Enfermedad3HF" Value='<%# Bind("Enfermedad3Id") %>' />
                                                </li>
                                            </ol>
                                            <asp:CheckBox ID="OtroCheckBox" Text="Otro" runat="server"
                                                onclick="OtroCheckBox_click(this);" Checked='<%# !string.IsNullOrWhiteSpace(Eval("DiagnosticoPresuntivo").ToString()) %>' />
                                            <div id="DiagnosticoValidation" class="validation">
                                                <asp:CustomValidator ID="CustomValidator5" runat="server"
                                                    Display="Dynamic"
                                                    ClientValidationFunction="validateDiagnostico"
                                                    ControlToValidate="EnfermedadesComboBox"
                                                    ValidationGroup="UpdateCaso"
                                                    ValidateEmptyText="true"
                                                    ErrorMessage="Debe seleccionar un diagnostico presuntivo." />
                                            </div>
                                            <div id="DiagnosticoPresuntivoDiv" style="display: none; margin-top: 10px;">
                                                <asp:TextBox ID="DiagnosticoPresuntivoTxt" runat="server"
                                                    TextMode="MultiLine" Rows="3" CssClass="biggerField"
                                                    Text='<%# Bind("DiagnosticoPresuntivo") %>' />
                                                <div class="validation">
                                                    <asp:CustomValidator ID="CustomValidator4" runat="server"
                                                        Display="Dynamic"
                                                        ClientValidationFunction="validateDiagnostico"
                                                        ControlToValidate="DiagnosticoPresuntivoTxt"
                                                        ValidationGroup="UpdateCaso"
                                                        ValidateEmptyText="true"
                                                        ErrorMessage="Debe llenar el campo Otro." />
                                                </div>
                                            </div>
                                            <script type="text/javascript">
                                                function validateDiagnostico(sender, arguments) {
                                                    var enfermedad = $find('<%=CasoFV.FindControl("EnfermedadesComboBox").ClientID%>');
                                                    var otro = $('#<%=CasoFV.FindControl("DiagnosticoPresuntivoTxt").ClientID%>');
                                                    if (enfermedad.get_value() != '' || otro.val() != '') {
                                                        arguments.IsValid = true;
                                                    } else {
                                                        arguments.IsValid = false;
                                                    }
                                                }
                                                function Enfermedades2ComboBox_ClientLoad(sender) {
                                                    var combo = $find("<%= CasoFV.FindControl("EnfermedadesComboBox").ClientID %>");
                                                    var combo2 = $find("<%= CasoFV.FindControl("Enfermedades2ComboBox").ClientID %>");
                                                    var combo3 = $find("<%= CasoFV.FindControl("Enfermedades3ComboBox").ClientID %>");
                                                    if (combo.get_text() == '') {
                                                        combo2.disable();
                                                        combo3.disable();
                                                    } else if (combo2.get_text() == '') {
                                                        combo3.disable();
                                                    }
                                                }
                                                function EnfermedadesComboBox_ClientLoad(sender) {
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
                                                    function EnfermedadesComboBox_TextChange(sender, eventArgs) {
                                                        if (sender.get_text() == '') {
                                                            switch (sender.get_id()) {
                                                                case '<%=CasoFV.FindControl("EnfermedadesComboBox").ClientID%>':
                                                                    var combo2 = $find("<%= CasoFV.FindControl("Enfermedades2ComboBox").ClientID %>");
                                                                    combo2.disable();
                                                                    combo2.clearSelection();
                                                                    $('#<%=CasoFV.FindControl("EnfermedadHF").ClientID%>').val('');
                                                                    $('#<%=CasoFV.FindControl("Enfermedad2HF").ClientID%>').val('');
                                                                case '<%=CasoFV.FindControl("Enfermedades2ComboBox").ClientID%>':
                                                                    var combo3 = $find("<%= CasoFV.FindControl("Enfermedades3ComboBox").ClientID %>");
                                                                    combo3.disable();
                                                                    combo3.clearSelection();
                                                                    $('#<%=CasoFV.FindControl("Enfermedad2HF").ClientID%>').val('');
                                                                    $('#<%=CasoFV.FindControl("Enfermedad3HF").ClientID%>').val('');
                                                                    break;
                                                                case '<%=CasoFV.FindControl("Enfermedades3ComboBox").ClientID%>':
                                                                    $('#<%=CasoFV.FindControl("Enfermedad3HF").ClientID%>').val('');
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                    function EnfermedadesComboBox_ClientSelectedIndexChanged(sender, eventArgs) {
                                                        var hiddenField = null;
                                                        switch (sender.get_id()) {
                                                            case '<%=CasoFV.FindControl("EnfermedadesComboBox").ClientID%>':
                                                                hiddenField = '#<%=CasoFV.FindControl("EnfermedadHF").ClientID%>';
                                                                var combo2 = $find("<%= CasoFV.FindControl("Enfermedades2ComboBox").ClientID %>");
                                                                combo2.enable();
                                                                break;
                                                            case '<%=CasoFV.FindControl("Enfermedades2ComboBox").ClientID%>':
                                                                hiddenField = '#<%=CasoFV.FindControl("Enfermedad2HF").ClientID%>';
                                                                var combo3 = $find("<%= CasoFV.FindControl("Enfermedades3ComboBox").ClientID %>");
                                                                combo3.enable();
                                                                break;
                                                            case '<%=CasoFV.FindControl("Enfermedades3ComboBox").ClientID%>':
                                                                hiddenField = '#<%=CasoFV.FindControl("Enfermedad3HF").ClientID%>';
                                                                break;
                                                        }
                                                        var item = eventArgs.get_item();
                                                        $(hiddenField).val(item.get_value());
                                                        $('#DiagnosticoValidation > span').hide();
                                                    }
                                            </script>
                                            <div class="validation">
                                                <asp:RegularExpressionValidator runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="DiagnosticoPresuntivoTxt"
                                                    ValidationGroup="UpdateCaso"
                                                    ErrorMessage="El diagnostico presuntivo no puede exceder 2000 caracteres."
                                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                                <asp:RegularExpressionValidator runat="server"
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
                                                </asp:LinkButton><asp:HyperLink ID="CancelHL"
                                                    NavigateUrl="~/CasoMedico/CasoMedicoRegistro.aspx" runat="server"
                                                    Text="Salir"
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
                                    UpdateMethod="UpdateCasoMedico"
                                    OnSelected="CasoODS_Selected"
                                    OnUpdated="CasoODS_Updated">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" PropertyName="value" Name="CasoId" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="OdontologiaRPV" runat="server">
                            <div>
                                <asp:LinkButton ID="LinkButton4" Text="Nueva Prestación" runat="server"
                                    CssClass="NewOdontologia" />
                                <br />
                                <telerik:RadGrid ID="OdontologiaRadGrid" runat="server"
                                    CssClass="OdontologiaRadGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="OdontologiaODS"
                                    OnItemCommand="OdontologiaRadGrid_ItemCommand"
                                    OnItemDataBound="OdontologiaRadGrid_ItemDataBound"
                                    OnItemCreated="OdontologiaRadGrid_ItemCreated">
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="OdontologiaId" CommandItemDisplay="Top">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif"
                                                    ToolTip="Imprimir" runat="server" OnClick="OdontologiaRadGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgRecetaNullLabel" runat="server" Text="No existen Prestaciones para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" Display="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" runat="server" />
                                                    <asp:Image ImageUrl="../Images/Neutral/ExportPrint.gif" AlternateText="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="OdontologiaIdHF" runat="server" Value='<%# Eval("OdontologiaId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridTemplateColumn UniqueName="Seleccionar" HeaderText="Modificar" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("OdontologiaId") %>'
                                                        CommandName="Select"
                                                        ToolTip="Seleccionar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            <telerik:GridTemplateColumn UniqueName="Delete" Display="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/delete.png"
                                                        CommandArgument='<%# Bind("OdontologiaId") %>'
                                                        CommandName="Delete"
                                                        OnClientClick="return confirm('¿Está seguro de eliminar esta prestación?');"
                                                        ToolTip="Eliminar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="OdontologiaId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="PrestacionOdontologica" HeaderText="Prestación" />
                                            <telerik:GridBoundColumn DataField="Pieza" HeaderText="Pieza" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                            <telerik:GridBoundColumn UniqueName="FechaCreacion" Display="true" DataField="FechaCreacionString" HeaderText="Fecha Creación" />
                                            <telerik:GridTemplateColumn UniqueName="FileManager" Display="true"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>
                                                    <div class="AdjuntosCont">
                                                        <asp:ImageButton ID="FileManagerIB" runat="server"
                                                            ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                            CommandName="ODONTOLOGIA"
                                                            CommandArgument='<%# Eval("OdontologiaId") %>'
                                                            ToolTip="Adjuntar Archivo a Receta"
                                                            OnCommand="FileManager_Command" />
                                                        <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:ObjectDataSource ID="OdontologiaODS" runat="server"
                                    TypeName="Artexacta.App.Odontologia.BLL.OdontologiaBLL"
                                    OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetOdontologiaByCasoId"
                                    OnSelected="OdontologiaODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <telerik:RadGrid ID="OdontologiaForPrintRadGrid" runat="server"
                                    CssClass="OdontologiaForPrintRadGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    Visible="false"
                                    OnItemCreated="OdontologiaRadGrid_ItemCreated">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" PageTopMargin="12mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false" BorderStyle="Thin" BorderType="TopAndBottom">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="OdontologiaId" Font-Size="9px">
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgRecetaNullLabel" runat="server" Text="No existen Prestaciones para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridBoundColumn DataField="OdontologiaId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="PrestacionOdontologicaForDisplay" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="0px">
                                                <ItemTemplate>
                                                    </td></tr>
                                                <tr>
                                                    <td style="padding-top: 5px;"><b>OBSERVACIONES:</b></td><td></td>
                                                </tr>
                                                    <tr style="height: 20pt;">
                                                        <td style="border-bottom: 0.5pt dashed #000;">
                                                            <asp:Literal Text='<%# Eval("Observaciones") %>' runat="server" /></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td><td>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <asp:Panel ID="PrestacionPanel" runat="server"
                                    CssClass="Default_Popup" GroupingText="Nuevo Prestación" DefaultButton="SavePrestacion">


                                    <asp:HiddenField ID="PrestacionHF" runat="server" Value="0" />
                                    <div>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td class="auto-style5">
                                                        <asp:Label runat="server" Text="Seleccione Una Ciudad"></asp:Label></td><td class="auto-style5">
                                                        <asp:Label runat="server" Text="Seleccione Un Proveedor"></asp:Label></td></tr><tr>
                                                    <td class="auto-style5">
                                                        <telerik:RadComboBox ID="RadComboBoxCiudadOdontologia" runat="server"
                                                            AutoPostBack="false"
                                                            DataSourceID="CiudadExComplementarioODS"
                                                            DataTextField="Nombre" DataValueField="CiudadId"
                                                            EmptyMessage="Seleccione una Ciudad" MarkFirstMatch="true"
                                                            OnClientSelectedIndexChanged="CiudadOdontologiaRadComboBox_OnClientSelectedIndexChanged"
                                                            ZIndex="50">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td class="auto-style5">
                                                        <telerik:RadComboBox ID="RadComboBoxProveedorOdontologia" runat="server"
                                                            EnableLoadOnDemand="true"
                                                            EmptyMessage="Seleccione un Proveedor"
                                                            MarkFirstMatch="true"
                                                            OnClientItemsRequesting="ProveedorOdontologiaRadCombo_OnClientItemsRequesting"
                                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                            AutoPostBack="false">
                                                            <WebServiceSettings Method="GetEspecialistasProveedorAutocompletePorCiudadYCliente" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />

                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <span class="label">Prestación</span> <telerik:RadComboBox ID="PrestacionRCB" runat="server"
                                            EmptyMessage="Seleccione un tipo de prestación"
                                            Width="350px"
                                            EnableLoadOnDemand="true"
                                            OnItemDataBound="PrestacionRCB_ItemDataBound"
                                            OnClientItemsRequesting="PrestacionRCB_OnClientItemsRequesting"
                                            OnClientSelectedIndexChanged="PrestacionRCB_ClientSelectedIndexChanged">
                                            <WebServiceSettings Method="GetPrestacionesOdontologicasAutocompleteProveedorYCliente" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />

                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ValidationGroup="Prestacion"
                                                ErrorMessage="Debe seleccionar un tipo de prestación."
                                                ControlToValidate="PrestacionRCB"
                                                Display="Dynamic" />
                                        </div>
                                        <script type="text/javascript">
                                            function PrestacionRCB_ClientSelectedIndexChanged(sender, eventArgs) {
                                                var item = eventArgs.get_item();
                                                if (item.get_value() == 2) {
                                                    $('#<%=PiezaOdontologiaMultiple.ClientID %>').show();
                                                    $('#<%=PiezaOdontologiaSimple.ClientID %> input[type=checkbox]').prop('checked', false);
                                                    $('#<%=PiezaOdontologiaSimple.ClientID %>').hide();
                                                } else {
                                                    $('#<%=PiezaOdontologiaMultiple.ClientID %>').hide();
                                                    $('#<%=PiezaOdontologiaMultiple.ClientID %> input[type=checkbox]').prop('checked', false);
                                                    $('#<%=PiezaOdontologiaSimple.ClientID %>').show();
                                                }

                                            }
                                        </script>

                                        <span class="label">Pieza</span> <RedSalud:PiezaOdontologia runat="server" ID="PiezaOdontologiaMultiple" MultipleSelect="true" Display="false" />
                                        <RedSalud:PiezaOdontologia runat="server" ID="PiezaOdontologiaSimple" />
                                        <div class="clearfix"></div>
                                        <div class="validation">
                                            <span id="validatePieza" style="color: Red; display: none;">Debe seleccionar una pieza.</span> </div><span class="label">Observaciones</span> <asp:TextBox ID="ObservacionesPrestacionTextBox" runat="server"
                                            CssClass="biggerField" TextMode="MultiLine">
                                        </asp:TextBox><div class="buttonsPanel">
                                            <asp:LinkButton ID="SavePrestacion" runat="server" OnClientClick="return validatePieza()"
                                                CssClass="button" ValidationGroup="Prestacion" OnClick="SaveNewOdontologiaLB_Click">
                                                <span>Guardar</span>
                                            </asp:LinkButton><asp:LinkButton ID="CancelPrestacion" Text="Cancelar" runat="server" />
                                            <script type="text/javascript">
                                                function validatePieza() {
                                                    var piezaM = $("#<%=PiezaOdontologiaMultiple.ClientID%> input[type='hidden']").val();
                                                    var piezaS = $("#<%=PiezaOdontologiaSimple.ClientID%> input[type='hidden']").val();
                                                    if ((piezaM == '' || piezaM == undefined || piezaM == null) &&
                                                        (piezaS == '' || piezaS == undefined || piezaS == null)) {
                                                        $('#validatePieza').show();
                                                        return false;
                                                    }
                                                    $('#validatePieza').hide();
                                                    return true;
                                                }
                                            </script>



                                        </div>
                                        <div>
                                            <asp:Label runat="server" ForeColor="Red" Text="Mensaje de Alerta" ID="AlertaOdontologia" Visible="false"></asp:Label></div></div></asp:Panel><asp:ObjectDataSource ID="PrestacionOdontologicaODS" runat="server"
                                    TypeName="Artexacta.App.CLAPrestacionOdontologica.BLL.PrestacionOdontologicaBLL"
                                    SelectMethod="getAllPrestacionOdontologica"
                                    OnSelected="PrestacionOdontologicaODS_Selected"></asp:ObjectDataSource>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RecetaRPV" runat="server">
                            <div>
                                <asp:LinkButton ID="NewRecetaLB" Text="Nueva Receta" runat="server"
                                    CssClass="NewReceta" />
                                <br />
                                <telerik:RadGrid ID="RecetaGrid" runat="server"
                                    CssClass="RecetaGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="RecetaODS"
                                    OnItemCommand="RecetaGrid_ItemCommand"
                                    OnItemDataBound="RecetaGrid_ItemDataBound"
                                    OnItemCreated="RecetaGrid_ItemCreated">
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="DetalleId" CommandItemDisplay="Top">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="true" ExportToPdfText="Exportar a PDF" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="~/Images/Neutral/ExportPrint.gif"
                                                    ToolTip="Imprimir Receta para Farmacia" runat="server"
                                                    OnClick="RecetaGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgRecetaNullLabel" runat="server" Text="No existen Recetas para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" runat="server" />
                                                    <asp:Image ImageUrl="../Images/Neutral/ExportPrint.gif" AlternateText="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("DetalleId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Seleccionar" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("DetalleId") %>'
                                                        CommandName="Select"
                                                        ToolTip="Seleccionar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/delete.png"
                                                        CommandArgument='<%# Bind("DetalleId") %>'
                                                        CommandName="Delete"
                                                        OnClientClick="return confirm('¿Está seguro de eliminar este medicamento de la receta?');"
                                                        ToolTip="Eliminar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="DetalleId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="Medicamento" HeaderText="Medicamento" />
                                            <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="Presentación" />
                                            <telerik:GridBoundColumn DataField="TipoConcentracionNombre" HeaderText="Concentración" />
                                            <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cant" />
                                            <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />
                                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacionString" HeaderText="Fecha Creación" />
                                            <telerik:GridTemplateColumn UniqueName="FileManager"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>
                                                    <div class="AdjuntosCont">
                                                        <asp:ImageButton ID="FileManagerIB" runat="server"
                                                            ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                            CommandName="RECETAS"
                                                            CommandArgument='<%# Eval("DetalleId") %>'
                                                            ToolTip="Adjuntar Archivo a Receta"
                                                            OnCommand="FileManager_Command" />
                                                        <asp:Label ID="Label2" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:ObjectDataSource ID="RecetaODS" runat="server"
                                    TypeName="Artexacta.App.Receta.BLL.RecetaBLL"
                                    OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAllRecetaByCasoId"
                                    OnSelected="RecetaODS_Selected">
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
                                        <Pdf PaperSize="Letter" PageTopMargin="12mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false" BorderStyle="Thin" BorderType="TopAndBottom">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="DetalleId" Font-Size="9px">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="RowNumber" DataField="RowNumber" Visible="false" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="Medicamento" HeaderText="Medicamento" ItemStyle-CssClass="medicamento" />
                                            <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="PRESENTACIÓN" />
                                            <telerik:GridBoundColumn DataField="TipoConcentracionNombre" HeaderText="CONCENTRACIÓN" />
                                            <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cant" />
                                            <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <asp:Panel ID="RecetaPanel" runat="server"
                                    CssClass="Default_Popup" GroupingText="Nueva Receta" DefaultButton="SaveReceta">
                                    <div>
                                        <asp:Label ID="Label52" Text="Ciudad" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="CiudadEditarComboBox" runat="server"
                                            CssClass="biggerField"
                                            DataSourceID="CiudadExComplementarioODS"
                                            DataValueField="CiudadId"
                                            DataTextField="Nombre"
                                            AutoPostBack="false"
                                            EmptyMessage="Seleccione una Ciudad"
                                            OnClientSelectedIndexChanged="CiudadEditarComboBox_OnClientSelectedIndexChanged"
                                            MarkFirstMatch="true">
                                        </telerik:RadComboBox>
                                        <asp:Label ID="Label53" Text="Farmacia" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="ProveedorEditarComboBox" runat="server"
                                            CssClass="biggerField"
                                            EnableLoadOnDemand="true"
                                            EmptyMessage="Seleccione un Proveedor"
                                            OnClientItemsRequesting="ProveedorRecetaEditarDDL_OnClientItemsRequesting"
                                            MarkFirstMatch="true"
                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                            AutoPostBack="false">
                                            <WebServiceSettings Method="GetProveedorPorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                        </telerik:RadComboBox>
                                        <asp:HiddenField ID="RecetaIdHF" runat="server" Value="0" />
                                        <asp:Label Text="Presentación" runat="server" CssClass="label" />
                                        <%--obtener de la DB--%>
                                        <telerik:RadComboBox ID="TipoMedicamentoDDL" runat="server"
                                            EmptyMessage="Seleccione un tipo de presentacion"
                                            CssClass="biggerField"
                                            DataValueField="TipoMedicamentoId"
                                            DataTextField="Nombre">
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator ID="TipoMedicamentoCV" runat="server"
                                                ValidationGroup="Receta"
                                                ErrorMessage="Debe seleccionar un tipo de presentación."
                                                ClientValidationFunction="TipoMedicamentoCV_Validate"
                                                Display="Dynamic" />
                                        </div>

                                        <asp:Label Text="Medicamento" runat="server" CssClass="label" />
                                        <asp:TextBox ID="MedicamentoTxt" runat="server" CssClass="biggerField">
                                        </asp:TextBox><%--<telerik:RadComboBox ID="MedicamentoCombo" runat="server">
                            </telerik:RadComboBox>--%><asp:Label Text="Cantidad" runat="server" CssClass="label" />
                                        <telerik:RadNumericTextBox ID="CantidadTxt" runat="server"
                                            NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                            IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                                        </telerik:RadNumericTextBox>
                                        <div class="validation">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="CantidadTxt"
                                                ValidationGroup="Receta"
                                                ErrorMessage="La cantidad es requerida." />
                                        </div>
                                        <asp:Label Text="Instrucciones de uso" runat="server" CssClass="label" />
                                        <asp:TextBox ID="InstruccionesTxt" runat="server"
                                            TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                                        <div class="validation">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="InstruccionesTxt"
                                                ValidationGroup="Receta"
                                                ErrorMessage="La Instrucción es requerida." />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="InstruccionesTxt"
                                                ValidationGroup="Receta"
                                                ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                                ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                                        </div>
                                        <div class="buttonsPanel">
                                            <asp:LinkButton ID="SaveReceta" Text="" runat="server"
                                                CssClass="button"
                                                ValidationGroup="Receta"
                                                OnClick="SaveReceta_Click">
                                                <asp:Label ID="Label9" Text="Guardar" runat="server" />
                                            </asp:LinkButton><asp:LinkButton ID="CancelRecetaLB" Text="Cancelar" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="ExComplementarioRPV" runat="server">
                            <div style="width: 100%; overflow: hidden; overflow-x: auto; overflow-y: hidden;">
                                <asp:LinkButton ID="NewExComplementarioLB" CssClass="NewEstudio" Text="Nuevo" runat="server" />

                                <telerik:RadGrid ID="EstudioRadGrid" runat="server"
                                    CssClass="EstudioRadGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="EstudioODS"
                                    PageSize="20"
                                    Width="99%"
                                    OnItemDataBound="EstudioRadGrid_ItemDataBound"
                                    OnItemCreated="EstudioRadGrid_ItemCreated">
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="EstudioId, CasoId" CommandItemDisplay="Top"
                                        TableLayout="Auto">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="false" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="~/Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                                    OnClick="EstudioRadGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgEstudioNullLabel" runat="server" Text="No existen examenes complementarios para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" HeaderStyle-Width="20px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" runat="server" />
                                                    <asp:Image ImageUrl="../Images/Neutral/ExportPrint.gif" AlternateText="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("EstudioId")%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Seleccionar" Visible="false"
                                                HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("EstudioId")%>'
                                                        CommandName="Select"
                                                        OnCommand="EstudioRadGrid_ItemCommand"
                                                        ToolTip="Modificar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="DeleteCommandColumn"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/delete.png"
                                                        CommandArgument='<%# Bind("EstudioId")%>'
                                                        OnCommand="EstudioRadGrid_ItemCommand"
                                                        CommandName="Delete"
                                                        OnClientClick="return confirm('¿Está seguro que desea eliminar el examen complementario?');"
                                                        ToolTip="Eliminar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="NombreTipoEstudio" HeaderText="Tipo Examen" />
                                            <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                            <telerik:GridBoundColumn DataField="FechaCreacionString" HeaderText="Fecha de creación" />
                                            <telerik:GridBoundColumn UniqueName="Cubierto" DataField="CubiertoDisplay" HeaderText="Cubierto" Visible="false" />
                                            <telerik:GridCheckBoxColumn UniqueName="Aprobado" DataField="Aprovado" HeaderText="Aprobado" Visible="false" />
                                            <telerik:GridTemplateColumn UniqueName="AprobarColumn" HeaderText="Aprobar" Visible="false"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="AprobarImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/complete.gif"
                                                        CommandArgument='<%# Bind("EstudioId")%>'
                                                        OnCommand="EstudioRadGrid_ItemCommand"
                                                        CommandName="Aprobar"
                                                        OnClientClick="return confirm('¿Está seguro que desea Aprobar el examen complementario del Caso?');"
                                                        ToolTip="Aprobar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="FileManager"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>
                                                    <div class="AdjuntosCont">
                                                        <asp:ImageButton ID="FileManagerIB" runat="server"
                                                            ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                            CommandName="ESTUDIO"
                                                            CommandArgument='<%# Eval("EstudioId") %>'
                                                            OnCommand="FileManager_Command" />
                                                        <asp:Label ID="Label2" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:ObjectDataSource ID="EstudioODS" runat="server"
                                    TypeName="Artexacta.App.Estudio.BLL.EstudioBLL"
                                    OldValuesParameterFormatString="{0}"
                                    SelectMethod="getEstudioListByCasoId"
                                    OnSelected="EstudioODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <telerik:RadGrid ID="EstudioPrintRadGrid" runat="server" Visible="false"
                                    AutoGenerateColumns="false" OnPdfExporting="RadGrid_PdfExporting"
                                    OnItemCreated="EstudioPrintRadGrid_ItemCreated">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="20mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <ItemStyle Height="18px" />
                                    <AlternatingItemStyle Height="18px" />
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="EstudioId" Font-Size="9px">
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="RowNumber" HeaderStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Literal runat="server" Text='<%# Container.DataSetIndex+1 %>'></asp:Literal>.-</ItemTemplate></telerik:GridTemplateColumn><telerik:GridBoundColumn DataField="NombreTipoEstudio" HeaderStyle-HorizontalAlign="Left" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="DerivacionRPV" runat="server">
                            <div>
                                <asp:LinkButton ID="NewDerivacion" CssClass="NewDerivacion" Text="Nueva derivación a especialista" runat="server" />

                                <telerik:RadGrid ID="DerivacionRadGrid" runat="server"
                                    CssClass="DerivacionRadGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="DerivacionODS"
                                    PageSize="20"
                                    OnItemCommand="DerivacionRadGrid_ItemCommand"
                                    OnItemDataBound="DerivacionRadGrid_ItemDataBound"
                                    OnItemCreated="DerivacionRadGrid_ItemCreated"
                                    OnPdfExporting="RadGrid_PdfExporting">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="DerivacionId" CommandItemDisplay="Top">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="true" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="~/Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                                    OnClick="DerivacionRadGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgDerivacionNullLabel" runat="server" Text="No existen derivaciones a especialistas para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" runat="server" />
                                                    <asp:Image ImageUrl="../Images/Neutral/ExportPrint.gif" AlternateText="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="DerivacionIdHiddenField" runat="server" Value='<%# Eval("DerivacionId")%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Seleccionar" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("DerivacionId")%>' CommandName="Select"
                                                        ToolTip="Seleccionar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                CommandName="Delete"
                                                ButtonType="ImageButton"
                                                ItemStyle-Width="40px"
                                                ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Center"
                                                ImageUrl="~/Images/neutral/delete.png"
                                                ConfirmText="¿Está seguro que desea eliminar esta derivación a especialista?" />
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ProveedorNombre" HeaderText="Proveedor" />
                                            <telerik:GridBoundColumn DataField="Observacion" HeaderText="Observaciones" />
                                            <telerik:GridBoundColumn DataField="FechaCreacionString" HeaderText="Fecha de creación" />
                                            <%-- 
                                            <telerik:GridTemplateColumn UniqueName="AprobarColumn" HeaderText="Aprobar" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="AprobarImageButton" runat="server"
                                                        ImageUrl="~/Images/Neutral/complete.gif"
                                                        CommandArgument='<%# Bind("DerivacionId")%>'
                                                        CommandName="Aprobar"
                                                        OnClientClick="return confirm('¿Está seguro que desea Aprobar la derivación a especialista del Caso?');"
                                                        ToolTip="Aprobar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            --%>
                                            <telerik:GridTemplateColumn UniqueName="FileManager"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>
                                                    <div class="AdjuntosCont">
                                                        <asp:ImageButton ID="FileManagerIB" runat="server"
                                                            ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                            CommandName="DERIVACIONES"
                                                            CommandArgument='<%# Eval("DerivacionId") %>'
                                                            OnCommand="FileManager_Command" />
                                                        <asp:Label ID="Label2" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <asp:ObjectDataSource ID="DerivacionODS" runat="server"
                                    TypeName="Artexacta.App.Derivacion.BLL.DerivacionBLL"
                                    OldValuesParameterFormatString="{0}"
                                    SelectMethod="getDerivacionListByCasoId_NEW"
                                    OnSelected="DerivacionODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <telerik:RadGrid ID="DerivacionToPrintRadGrid" runat="server"
                                    AutoGenerateColumns="false"
                                    OnItemCreated="DerivacionRadGrid_ItemCreated"
                                    OnPdfExporting="RadGrid_PdfExporting"
                                    Visible="false"
                                    BorderStyle="None">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" BorderStyle="None" ClientDataKeyNames="DerivacionId" Font-Size="9px">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="23px">
                                                <ItemTemplate><b>A:</b>&nbsp;</ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn HeaderStyle-Width="112px" ItemStyle-Height="30px">
                                                <ItemTemplate></ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ProveedorNombre" HeaderStyle-Width="59px" ItemStyle-CssClass="Separator" />
                                            <telerik:GridTemplateColumn>
                                                <ItemTemplate><b>ESP.:</b><asp:Literal Text='<%# " "+Eval("EspecialidadNombre") %>' runat="server" /></ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn>
                                                <ItemTemplate>
                                                    </td></tr><tr>
                                                        <td colspan="6" style="height: 20px;"><b>RESUMEN DE CASO MÉDICO</b></td></tr><tr>
                                                        <td>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="Observacion" ItemStyle-CssClass="Detalle" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="InternacionRPV" runat="server">
                            <div>
                                <asp:LinkButton ID="NewInternacion" Text="Nueva Internación" runat="server"
                                    CssClass="NewInternacion" />

                                <telerik:RadGrid ID="InternacionRadGrid" runat="server"
                                    CssClass="InternacionRadGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="InternacionODS"
                                    PageSize="20"
                                    OnItemCommand="InternacionRadGrid_ItemCommand"
                                    OnItemDataBound="InternacionRadGrid_ItemDataBound"
                                    MasterTableView-ClientDataKeyNames="InternacionId,CasoId"
                                    OnPdfExporting="RadGrid_PdfExporting">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="InternacionId" CommandItemDisplay="Top">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="true" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="~/Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                                    OnClick="InternacionRadGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgInternacionNullLabel" runat="server" Text="No existen internaciones para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30px">
                                                <HeaderTemplate>
                                                    <asp:Image ImageUrl="../Images/Neutral/ExportPrint.gif" AlternateText="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("InternacionId")%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn Visible="false" HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("InternacionId")%>' CommandName="Select"
                                                        ToolTip="Seleccionar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn UniqueName="TemplateRegistroInternacion" HeaderStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>

                                                    <asp:ImageButton ID="EditCirugia" runat="server"
                                                        ToolTip="Agregar Gastos"
                                                        ImageUrl="~/Images/Neutral/Money.png" Width="24px"
                                                        CommandName="Detalles"
                                                        CommandArgument='<%# Eval("InternacionId")+";"+Eval("Tipo") +";"+Eval("CodigoArancelarioId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                CommandName="Delete"
                                                ButtonType="ImageButton" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ImageUrl="~/Images/neutral/delete.png"
                                                ConfirmText="¿Está seguro que desea eliminar esta internación?" />
                                            <telerik:GridBoundColumn UniqueName="InternacionId" DataField="InternacionId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" />
                                            <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                            <telerik:GridBoundColumn DataField="CodigoArancelario" HeaderText="Procedimiento Médico Quirúrgico" />
                                            <telerik:GridBoundColumn DataField="FechaCreacionString" HeaderText="Fecha de creación" />
                                            <telerik:GridCheckBoxColumn DataField="Aprovado" HeaderText="Aprobado" Visible="false" />
                                            <telerik:GridBoundColumn DataField="CodigoArancelarioId" HeaderText="CodigoArancelarioId" Visible="False" />
                                            <telerik:GridButtonColumn UniqueName="AprobarColumn" Visible="false"
                                                HeaderText="Aprobar"
                                                CommandName="Aprobar"
                                                ButtonType="ImageButton"
                                                ItemStyle-Width="40px"
                                                ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Center"
                                                ImageUrl="~/Images/neutral/complete.gif"
                                                ConfirmText="¿Está seguro que desea Aprobar esta internación del Caso?" />
                                            <telerik:GridTemplateColumn UniqueName="FileManager"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>
                                                    <div class="AdjuntosCont">
                                                        <asp:ImageButton ID="FileManagerIB" runat="server"
                                                            ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                            CommandName="INTERNACION"
                                                            CommandArgument='<%# Eval("InternacionId") %>'
                                                            OnCommand="FileManager_Command" />
                                                        <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                                <asp:ObjectDataSource ID="InternacionODS" runat="server"
                                    TypeName="Artexacta.App.Internacion.BLL.InternacionBLL"
                                    OldValuesParameterFormatString="{0}"
                                    SelectMethod="getInternacionListByCasoId"
                                    OnSelected="InternacionODS_Selected">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <telerik:RadGrid ID="InternacionToPrintRadGrid" runat="server"
                                    AutoGenerateColumns="false"
                                    OnItemCreated="InternacionRadGrid_ItemCreated"
                                    OnItemDataBound="InternacionToPrintRadGrid_ItemDataBound"
                                    OnPdfExporting="RadGrid_PdfExporting"
                                    Visible="false"
                                    BorderStyle="None">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" BorderStyle="None" ClientDataKeyNames="InternacionId" Font-Size="9">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="23px" ItemStyle-Height="30px">
                                                <ItemTemplate><b>SRES:</b></ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn HeaderStyle-Width="113px">
                                                <ItemTemplate></ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="NombreProveedor" HeaderStyle-Width="104px" ItemStyle-CssClass="Separator" />
                                            <telerik:GridTemplateColumn UniqueName="CodigoArancelarioTitle">
                                                <ItemTemplate>
                                                    </td></tr><tr>
                                                        <td colspan="6" style="height: 20px;"><b>PROCEDIMIENTO MÉDICO QUIRÚRGICO:</b></td></tr><tr>
                                                        <td>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="CodigoArancelario" DataField="CodigoArancelario" ItemStyle-CssClass="Detalle" />
                                            <telerik:GridTemplateColumn>
                                                <ItemTemplate>
                                                    </td></tr><tr>
                                                        <td colspan="6" style="height: 20px;"><b>OBSERVACIONES</b></td></tr><tr>
                                                        <td>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="Observaciones" ItemStyle-CssClass="Detalle" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="EmergenciaRPV" runat="server">
                            <div>

                                <telerik:RadGrid ID="EmergenciaRadGrid" runat="server"
                                    CssClass="EmergenciaRadGrid PDFExportRadGrid"
                                    AutoGenerateColumns="false"
                                    PageSize="20"
                                    DataSourceID="EmergenciaODS"
                                    OnItemCommand="EmergenciaRadGrid_ItemCommand"
                                    OnItemDataBound="EmergenciaRadGrid_ItemDataBound"
                                    MasterTableView-ClientDataKeyNames="EmergenciaId,CasoId"
                                    OnPdfExporting="RadGrid_PdfExporting">

                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="EmergenciaId" CommandItemDisplay="Top">
                                        <CommandItemStyle HorizontalAlign="Left" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false"
                                            ShowExportToPdfButton="true" />
                                        <CommandItemTemplate>
                                            <asp:Panel ID="ExportPanel" runat="server" CssClass="right" Style="padding: 10px;">
                                                <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="~/Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                                    OnClick="EmergenciaRadGrid_ExportToPdfButton_Click" />
                                            </asp:Panel>
                                        </CommandItemTemplate>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="MsgEMergenciaNullLabel" runat="server" Text="No existen internaciones para este caso del paciente."></asp:Label></NoRecordsTemplate><Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" runat="server" />
                                                    <asp:Image ImageUrl="../Images/Neutral/ExportPrint.gif" AlternateText="Imprimir" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                    <asp:HiddenField ID="DerivacionIdHiddenField" runat="server" Value='<%# Eval("EmergenciaId")%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="Seleccionar" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DetailsImageButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                                        CommandArgument='<%# Bind("EmergenciaId")%>' CommandName="Select"
                                                        ToolTip="Seleccionar"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="TemplateRegistroEmergencia" HeaderStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                                <ItemTemplate>

                                                    <asp:ImageButton ID="EditEmergencia" runat="server"
                                                        ImageUrl="~/Images/Neutral/Money.png" Width="24px"
                                                        ToolTip="INGRESAR GASTOS DE EMREGENCIA"
                                                        CommandName="Detalles"
                                                        CommandArgument='<%# Eval("EmergenciaId")+";"+Eval("CasoId") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="CasoId" DataField="CasoId" Visible="false" />
                                            <telerik:GridBoundColumn UniqueName="EmergenciaId" DataField="EmergenciaId" HeaderText="EmergenciaId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                            <telerik:GridBoundColumn DataField="detfecha" HeaderText="Fecha de creación" />
                                            <telerik:GridTemplateColumn HeaderText="Resultados de Estudios">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="FullReportePdfButton" runat="server"
                                                        Visible="true"
                                                        CommandArgument='<%# Eval("EmergenciaId") %>'
                                                        CommandName="Completo"
                                                        ImageUrl="~/Images/Neutral/fullpdf.png" Width="18px" />
                                                    <asp:Repeater ID="LaboratoriosMainRepeater" runat="server"
                                                        OnItemDataBound="Repeater1_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:Repeater ID="DocumentosRepeater" runat="server"
                                                                OnItemCommand="DocumentosRepeater_ItemCommand">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="DownloadImageButton" runat="server"
                                                                        ImageUrl='<%# Eval("Icon") %>'
                                                                        CommandArgument='<%# Eval("FileStoragePath") %>'
                                                                        Width="18px" CommandName='<%# Eval("Name") %>'
                                                                        ToolTip='<%# "Archivo: " + Eval("Name").ToString() + " - Subido en: " + Eval("DateUploadedForDisplay").ToString() %>'></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                        </ItemTemplate>
                                                    </asp:Repeater>


                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>

                                </telerik:RadGrid>
                                <asp:ObjectDataSource ID="EmergenciaODS" runat="server"
                                    TypeName="Artexacta.App.Emergencia.BLL.EmergenciaBLL"
                                    OldValuesParameterFormatString="{0}"
                                    SelectMethod="getEmergencia_EmergenciaDetailsByCasoId">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <%--  <telerik:RadGrid ID="RadGrid1" runat="server"
                                    AutoGenerateColumns="false"
                                    OnItemCreated="InternacionRadGrid_ItemCreated"
                                    OnItemDataBound="InternacionToPrintRadGrid_ItemDataBound"
                                    OnPdfExporting="RadGrid_PdfExporting"
                                    Visible="false"
                                    BorderStyle="None">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                        <Pdf PaperSize="Letter" DefaultFontFamily="Arial Unicode MS"
                                            PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" PageBottomMargin="40mm"
                                            AllowModify="false" AllowAdd="false">
                                        </Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" BorderStyle="None" ClientDataKeyNames="InternacionId" Font-Size="9">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="CasoId" Visible="false" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="23px" ItemStyle-Height="30px">
                                                <ItemTemplate><b>SRES:</b></ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="113px">
                                                <ItemTemplate></ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="NombreProveedor" HeaderStyle-Width="104px" ItemStyle-CssClass="Separator" />
                                            <telerik:GridTemplateColumn UniqueName="CodigoArancelarioTitle">
                                                <ItemTemplate>
                                                    </td></tr><tr>
                                                        <td colspan="6" style="height: 20px;"><b>PROCEDIMIENTO MÉDICO QUIRÚRGICO:</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="CodigoArancelario" DataField="CodigoArancelario" ItemStyle-CssClass="Detalle" />
                                            <telerik:GridTemplateColumn>
                                                <ItemTemplate>
                                                    </td></tr><tr>
                                                        <td colspan="6" style="height: 20px;"><b>OBSERVACIONES</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="Observaciones" ItemStyle-CssClass="Detalle" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>--%>
                            </div>
                        </telerik:RadPageView>

                    </telerik:RadMultiPage>
                </div>
                <div class="right" style="width: 29%;">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Información póliza">
                        <asp:Label ID="AseguradoLabel" Text="" runat="server" />
                        <br />
                        <span class="label">Nro. Póliza - Nombre del Plan</span> <asp:Label ID="PolizaLabel" Text="" runat="server" />
                        <br />
                        <span class="label">Valido hasta:</span> <asp:Label ID="FechaFinLabel" Text="" runat="server" />
                        <br />
                        <span class="label" style="visibility: hidden">Siniestralidad:</span> <div id="DivSiniestralidad" runat="server" style="visibility: hidden">
                            <asp:Panel runat="server" ID="SinistralidadMonto">
                                <asp:Label ID="SiniestralidadLabel" Text="" runat="server" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="SiniestralidadPlan" Visible="false" CssClass="table" Style="width: 100%;">
                                <div>
                                    <div class="header">Estudio</div><div class="header" style="width: 65px;">Cantidad Permitida</div><div class="header" style="width: 65px;">Cantidad de Uso</div></div><asp:Repeater runat="server" ID="PlanUsoRepeater">
                                    <ItemTemplate>
                                        <div style="display: table-row;">
                                            <div><%#Eval("Nombre") %></div>
                                            <div style="text-align: center;"><%#Eval("Cantidad") %></div>
                                            <div style="text-align: center;"><%#Eval("CantidadUso") %></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server" CssClass="critic" GroupingText="Información asegurado">
                        <asp:FormView ID="PacienteFV" runat="server"
                            DataSourceID="PacienteODS">
                            <ItemTemplate>
                                <asp:HiddenField ID="GeneroAsegurado" Value='<%# Bind("Genero")%>' runat="server" />
                                <asp:HiddenField ID="GeneroHF" runat="server" Value='<%# Bind("Genero") %>' />
                                <div class="twoColsLeft">
                                    <div style="margin: 0 auto; width: 120px;">
                                        <RedSalud:FotoPaciente runat="server" ID="FotoPaciente" FotoId='<%# Eval("FotoId")%>' Width="120px" PacienteId='<%# Eval("PacienteId")%>' />
                                    </div>

                                    <asp:Label ID="CITitle" Text="Carnet de Identidad" runat="server" CssClass="label" />
                                    <asp:Label ID="CILabel" Text='<%# Bind("CarnetIdentidad")%>' runat="server" />

                                    <asp:Label ID="NombreTitle" Text="Nombre Completo" runat="server" CssClass="label" />
                                    <asp:Label ID="NombreLabel" Text='<%# Bind("Nombre")%>' runat="server" />

                                    <asp:Label ID="Label3" Text="EDAD" runat="server" CssClass="label" />
                                    <asp:Label ID="Label11" Text='<%# Eval("Edad")%>' runat="server" />
                                </div>
                                <div class="twoColsRight">
                                    <asp:LinkButton ID="HistorialLB" Text="Historial" runat="server"
                                        OnClick="HistorialLB_Click" />

                                    <asp:Label ID="FechaNacTitle" Text="Fecha de nacimiento" runat="server" CssClass="label" />
                                    <asp:Label ID="FechaNacLabel" Text='<%# Bind("FechaNacimientoString")%>' runat="server" />

                                    <asp:Label ID="GeneroTitle" Text="Género" runat="server" CssClass="label" />
                                    <asp:Label ID="GeneroLabel" Text='<%# Bind("GeneroForDisplay")%>' runat="server" />

                                    <asp:Label ID="EstadoCivilTitle" Text="Estado civil" runat="server" CssClass="label" />
                                    <asp:Label ID="EstadoCivilLabel" Text='<%# Bind("EstadoCivil")%>' runat="server" />

                                    <asp:Label ID="DirTitle" Text="Dirección" runat="server" CssClass="label" />
                                    <asp:Label ID="DirLabel" Text='<%# Bind("Direccion")%>' runat="server" />

                                    <asp:Label ID="tlfTitle" Text="Teléfono" runat="server" CssClass="label" />
                                    <asp:Label ID="TlfLabel" Text='<%# Bind("Telefono")%>' runat="server" />

                                    <asp:Label ID="LugarTTitle" Text="Lugar de trabajo" runat="server" CssClass="label" />
                                    <asp:Label ID="LugarTLabel" Text='<%# Bind("LugarTrabajo")%>' runat="server" />

                                    <asp:Label ID="TlfOffTitle" Text="Teléfono de Oficina" runat="server" CssClass="label" />
                                    <asp:Label ID="tlfOffLabel" Text='<%# Bind("TelefonoTrabajo")%>' runat="server" />

                                    <asp:Label ID="NroHijosTitle" Text="Nro. de hijos" runat="server" CssClass="label" />
                                    <asp:Label ID="NroHijosLabel" Text='<%# Bind("NroHijos")%>' runat="server" />
                                </div>
                                <div class="buttonsPanel clear">
                                    <asp:LinkButton ID="EditLB" Text="" runat="server"
                                        CommandName="Edit"
                                        CssClass="button">
                                        <asp:Label ID="Label6" Text="Editar" runat="server" />
                                    </asp:LinkButton></div></ItemTemplate><EditItemTemplate>
                                <asp:HiddenField ID="GeneroAsegurado" Value='<%# Bind("Genero")%>' runat="server" />
                                <div class="twoColsLeft">
                                    <asp:HiddenField ID="PacienteId" runat="server" Value='<%# Bind("PacienteId")%>' />
                                    <asp:Label ID="CITitle" Text="Carnet de Indentidad" runat="server" CssClass="label" />
                                    <asp:TextBox ID="CILabel" runat="server" Text='<%# Bind("CarnetIdentidad")%>' />

                                    <asp:Label ID="NombreTitle" Text="Nombre Completo" runat="server" CssClass="label" />
                                    <asp:Label ID="NombreLabel" Text='<%# Eval("Nombre")%>' runat="server" />

                                    <asp:Label ID="FechaNacTitle" Text="Fecha de nacimiento" runat="server" CssClass="label" />
                                    <telerik:RadDatePicker ID="FechaNacDatePicker" MinDate="01/01/1900" runat="server" SelectedDate='<%# Bind("FechaNacimiento") %>'></telerik:RadDatePicker>
                                    <%-- <asp:Label ID="FechaNacLabel" Text='<%# Eval("FechaNacimientoShort")%>' runat="server" />--%>
                                    <div class="validation">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="FechaNacDatePicker"
                                            ErrorMessage="La Fecha De Nacimiento es requerida" />
                                    </div>
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
                                    </asp:LinkButton><asp:LinkButton ID="CancelLB" Text="" runat="server"
                                        CommandName="Cancel" ValidationGroup="cancel_Paciente">
                                        <asp:Label ID="Label8" Text="Cancelar" runat="server" />
                                    </asp:LinkButton></div></EditItemTemplate></asp:FormView><asp:ObjectDataSource ID="PacienteODS" runat="server"
                            TypeName="Artexacta.App.Paciente.BLL.PacienteBLL"
                            DataObjectTypeName="Artexacta.APP.Paciente.Paciente"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetPacienteByPacienteId"
                            OnSelected="PacienteODS_Selected"
                            UpdateMethod="UpdatePacienteBasicByFechaNacimiento"
                            OnUpdated="PacienteODS_Updated">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="PacienteIdHF" Name="PacienteId" PropertyName="value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:HiddenField ID="PacienteIdHF" runat="server" />
                    </asp:Panel>
                </div>
                <div class="clear"></div>

                <asp:Panel ID="NewRecetaPanel" runat="server"
                    CssClass="Default_Popup" GroupingText="Nueva Receta" DefaultButton="SaveNewRecetaLB">
                    <div class="error">
                        <asp:Label ID="MessageRecetaLabel" Text="" runat="server" />
                    </div>
                    <asp:Label ID="Label50" Text="Ciudad" runat="server" CssClass="label" />
                    <telerik:RadComboBox ID="CiudadRecetaRadComboBox" runat="server"
                        CssClass="biggerField"
                        DataSourceID="CiudadExComplementarioODS"
                        DataValueField="CiudadId"
                        DataTextField="Nombre"
                        AutoPostBack="false"
                        EmptyMessage="Seleccione una Ciudad"
                        OnClientSelectedIndexChanged="CiudadRecetaRadComboBox_OnClientSelectedIndexChanged"
                        MarkFirstMatch="true">
                    </telerik:RadComboBox>
                    <asp:Label ID="Label51" Text="Farmacia" runat="server" CssClass="label" />
                    <telerik:RadComboBox ID="FarmaciaRecetaRadComboBox" runat="server"
                        CssClass="biggerField"
                        EnableLoadOnDemand="true"
                        EmptyMessage="Seleccione un Proveedor"
                        OnClientItemsRequesting="ProveedorRecetaDDL_OnClientItemsRequesting"
                        MarkFirstMatch="true"
                        ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                        AutoPostBack="false">
                        <WebServiceSettings Method="GetProveedorPorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                    </telerik:RadComboBox>
                    <asp:Panel ID="Panel3" runat="server" GroupingText="Primer Medicamento">
                        <div>
                            <div id="Liname1SelectorDiv" runat="server">
                                <!--IS LINAME-->
                                <input id="RecetaTypeId" type="radio" name="RecetaType" value="1" checked="checked" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE GENÉRICO <!--IS'T LINAME--><input type="radio" name="RecetaType" value="0" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE COMERCIAL </div><div id="NewRecetaLINAME1" class="NewRecetaLINAME">
                                <asp:Label ID="Label18" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewMedicamentoRadComboBox" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                    OnClientSelectedIndexChanged="NewMedicamentoComboBox_ClientSelectedIndexChanged"
                                    OnClientTextChange="NewMedicamentoComboBox_ClientTextChange">
                                    <WebServiceSettings Method="GetMedicamentoLINAME" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:HiddenField runat="server" ID="NewMedicamentoClaIdHF" Value='<%# Bind("MedicamentoClaId") %>' />
                                <div class="validation">
                                    <span id="NewMedicamentoError" style="display: none;">Debe seleccionar un Medicamento del LINAME.</span> <%--<asp:CustomValidator ID="CustomValidator2" runat="server"
                                            ValidationGroup="NewReceta"
                                            ErrorMessage="Debe seleccionar un Medicamento del LINAME."
                                            ClientValidationFunction="NewMedicamentoRadComboBox_Validate"
                                            ControlToValidate="NewMedicamentoRadComboBox"
                                            Display="Dynamic" />--%></div><asp:Label Text="Grupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewGrupoLabel" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />
                                <asp:Label Text="Subgrupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewSubgrupoLabel" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />

                                <asp:Label ID="Label35" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoMedicamentoRadComboBox" runat="server"
                                    OnClientItemsRequesting="NewOnClientItemsRequesting"
                                    OnClientSelectedIndexChanged="NewTipoMedicamentoRadComboBox_ClientSelectedIndexChanged"
                                    OnClientItemsRequested="NewTipoMedicamentoRadComboBox_ClientItemsRequeted"
                                    OnClientTextChange="NewTipoMedicamentoRadComboBox_ClientTextChange"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoMedicamentoByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <span id="NewTipoMedicamentoError" style="display: none;">Debe seleccionar un tipo de presentación.</span> <%--<asp:CustomValidator ID="CustomValidator6" runat="server"
                                            ValidationGroup="NewReceta"
                                            ErrorMessage=""
                                            ControlToValidate="NewTipoMedicamentoRadComboBox"
                                            ClientValidationFunction="NewTipoMedicamentoRadComboBox_Validate"
                                            Display="Dynamic" />--%></div><asp:Label Text="Concentración" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoConcentracionRadComboBox" runat="server"
                                    OnClientItemsRequesting="NewTipoConcentracionRadComboBox_ClientItemsRequesting"
                                    OnClientItemsRequested="NewTipoConcentracionRadComboBox_ClientItemsRequeted"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoConcentracionByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <span id="NewTipoConcentracionError" style="display: none;">Debe seleccionar un tipo de concentación.</span> <%--<asp:CustomValidator ID="CustomValidator7" runat="server"
                                            ValidationGroup="NewReceta"
                                            ErrorMessage=""
                                            ControlToValidate="NewTipoConcentracionRadComboBox"
                                            ClientValidationFunction="NewTipoConcentracionRadComboBox_Validate"
                                            Display="Dynamic" />--%></div><script type="text/javascript">
                                    function NewTipoMedicamentoRadComboBox_ClientItemsRequeted(combo, eventArgs) {
                                        combo.set_emptyMessage('Seleccione una Presentación');
                                        combo.clearSelection();
                                        try {
                                            var items = combo.get_items();
                                            if (items.get_count() == 1) {
                                                items.getItem(0).select();

                                                if (combo.get_id().indexOf('2') > -1) {
                                                    combo = $find("<%= NewTipoConcentracion2RadComboBox.ClientID %>");
                                                } else if (combo.get_id().indexOf('3') > -1) {
                                                    combo = $find("<%= NewTipoConcentracion3RadComboBox.ClientID %>");
                                                } else if (combo.get_id().indexOf('4') > -1) {
                                                    combo = $find("<%= NewTipoConcentracion4RadComboBox.ClientID %>");
                                                } else {
                                                    combo = $find("<%= NewTipoConcentracionRadComboBox.ClientID %>");
                                                }
                                        combo.requestItems('', false);
                                    }
                                } catch (q) {
                                    console.error('ERROR: ' + q);
                                }
                            }
                            function NewMedicamentoComboBox_ClientSelectedIndexChanged(sender, eventArgs) {
                                var item = eventArgs.get_item();
                                var comboTMID = null;
                                var comboCMID = null;
                                var HiddenID = null;
                                var GroupID = null;
                                var SubgroupID = null;
                                try {

                                    if (sender.get_id().indexOf('2') > -1) {
                                        HiddenID = '#<%=NewMedicamentoCla2IdHF.ClientID%>';
                                        GroupID = '#<%=NewGrupo2Label.ClientID%>';
                                        SubgroupID = '#<%=NewSubgrupo2Label.ClientID%>';
                                        comboTMID = '<%= NewTipoMedicamento2RadComboBox.ClientID %>';
                                        comboCMID = '<%= NewTipoConcentracion2RadComboBox.ClientID %>';
                                    } else if (sender.get_id().indexOf('3') > -1) {
                                        HiddenID = '#<%=NewMedicamentoCla3IdHF.ClientID%>';
                                        GroupID = '#<%=NewGrupo3Label.ClientID%>';
                                        SubgroupID = '#<%=NewSubgrupo3Label.ClientID%>';
                                        comboTMID = '<%= NewTipoMedicamento3RadComboBox.ClientID %>';
                                        comboCMID = '<%= NewTipoConcentracion3RadComboBox.ClientID %>';
                                    } else if (sender.get_id().indexOf('4') > -1) {
                                        HiddenID = '#<%=NewMedicamentoCla4IdHF.ClientID%>';
                                        GroupID = '#<%=NewGrupo4Label.ClientID%>';
                                        SubgroupID = '#<%=NewSubgrupo4Label.ClientID%>';
                                        comboTMID = '<%= NewTipoMedicamento4RadComboBox.ClientID %>';
                                        comboCMID = '<%= NewTipoConcentracion4RadComboBox.ClientID %>';
                                    } else {
                                        HiddenID = '#<%=NewMedicamentoClaIdHF.ClientID%>';
                                        GroupID = '#<%=NewGrupoLabel.ClientID%>';
                                        SubgroupID = '#<%=NewSubgrupoLabel.ClientID%>';
                                        comboTMID = '<%= NewTipoMedicamentoRadComboBox.ClientID %>';
                                        comboCMID = '<%= NewTipoConcentracionRadComboBox.ClientID %>';
                                    }
                            $(HiddenID).val(item.get_value());
                            var id = 0;
                            try {
                                id = parseInt(item.get_value());
                            } catch (q) { alert(q); }
                            if (id <= 0) {
                                clearComboboxes(sender.get_id());
                            } else {
                                var group = item.get_attributes().getAttribute("Grupo");
                                $(GroupID).html(group);
                                var subgroup = item.get_attributes().getAttribute("Subgrupo");
                                $(SubgroupID).html(subgroup);

                                var combo = $find(comboTMID);
                                combo.requestItems('', false);
                                combo = $find(comboCMID);
                                combo.set_emptyMessage('Seleccione una Concentración');
                                combo.clearSelection();
                            }
                        } catch (q) {
                            console.error('ERROR: ' + q);
                        }
                    }
                    function NewTipoMedicamentoRadComboBox_ClientSelectedIndexChanged(sender, eventArgs) {
                        var item = eventArgs.get_item();
                        var comboCMID = null;
                        try {
                            if (sender.get_id().indexOf('2') > -1) {
                                comboCMID = '<%= NewTipoConcentracion2RadComboBox.ClientID %>';
                            } else if (sender.get_id().indexOf('3') > -1) {
                                comboCMID = '<%= NewTipoConcentracion3RadComboBox.ClientID %>';
                            } else if (sender.get_id().indexOf('4') > -1) {
                                comboCMID = '<%= NewTipoConcentracion4RadComboBox.ClientID %>';
                            } else {
                                comboCMID = '<%= NewTipoConcentracionRadComboBox.ClientID %>';
                            }
                    var id = '';
                    try {
                        id = item.get_value();
                    } catch (q) { alert(q); }
                    if (id == '') {
                        var combo = $find(comboCMID);
                        combo.clearItems();
                        combo.set_emptyMessage('Seleccione una Concentración');
                        combo.clearSelection();
                    } else {
                        var combo = $find(comboCMID);
                        combo.requestItems('', false);
                    }
                } catch (q) {
                    console.error('ERROR: ' + q);
                }
            }
            function NewTipoConcentracionRadComboBox_ClientItemsRequeted(combo, eventArgs) {
                try {
                    combo.set_emptyMessage('Seleccione una Concentración');
                    combo.clearSelection();

                    var items = combo.get_items();
                    if (items.get_count() == 1) {
                        items.getItem(0).select();
                    }
                } catch (q) {
                    console.error('ERROR: ' + q);
                }
            }
            function NewOnClientItemsRequesting(sender, eventArgs) {
                try {
                    var context = eventArgs.get_context();
                    var HiddenID = null;

                    if (sender.get_id().indexOf('2') > -1) {
                        HiddenID = '#<%=NewMedicamentoCla2IdHF.ClientID%>';
                    } else if (sender.get_id().indexOf('3') > -1) {
                        HiddenID = '#<%=NewMedicamentoCla3IdHF.ClientID%>';
                    } else if (sender.get_id().indexOf('4') > -1) {
                        HiddenID = '#<%=NewMedicamentoCla4IdHF.ClientID%>';
                    } else {
                        HiddenID = '#<%=NewMedicamentoClaIdHF.ClientID%>';
                    }
            context["MedicamentoClaId"] = $(HiddenID).val();
        } catch (q) {
            console.error('ERROR: ' + q);
        }
    }
    function NewTipoConcentracionRadComboBox_ClientItemsRequesting(sender, eventArgs) {
        try {
            var context = eventArgs.get_context();
            var comboTMID = null;
            var HiddenID = null;

            if (sender.get_id().indexOf('2') > -1) {
                HiddenID = '#<%=NewMedicamentoCla2IdHF.ClientID%>';
                comboTMID = '<%= NewTipoMedicamento2RadComboBox.ClientID %>';
            } else if (sender.get_id().indexOf('3') > -1) {
                HiddenID = '#<%=NewMedicamentoCla3IdHF.ClientID%>';
                comboTMID = '<%= NewTipoMedicamento3RadComboBox.ClientID %>';
            } else if (sender.get_id().indexOf('4') > -1) {
                HiddenID = '#<%=NewMedicamentoCla4IdHF.ClientID%>';
                comboTMID = '<%= NewTipoMedicamento4RadComboBox.ClientID %>';
            } else {
                HiddenID = '#<%=NewMedicamentoClaIdHF.ClientID%>';
                comboTMID = '<%= NewTipoMedicamentoRadComboBox.ClientID %>';
            }
    context["MedicamentoClaId"] = $(HiddenID).val();
    context["TipoMedicamentoId"] = $find(comboTMID).get_value();
} catch (q) {
    console.error('ERROR: ' + q);
}
}
function NewMedicamentoComboBox_ClientTextChange(sender, eventArgs) {
    var HiddenID = null;
    var GroupID = null;
    var SubgroupID = null;
    try {

        if (sender == null || sender == undefined)
            sender = $find('NewMedicamentoComboBox');

        if (sender.get_id().indexOf('2') > -1) {
            HiddenID = '#<%=NewMedicamentoCla2IdHF.ClientID%>';
            GroupID = '#<%=NewGrupo2Label.ClientID%>';
            SubgroupID = '#<%=NewSubgrupo2Label.ClientID%>';
        } else if (sender.get_id().indexOf('3') > -1) {
            HiddenID = '#<%=NewMedicamentoCla3IdHF.ClientID%>';
            GroupID = '#<%=NewGrupo3Label.ClientID%>';
            SubgroupID = '#<%=NewSubgrupo3Label.ClientID%>';
        } else if (sender.get_id().indexOf('4') > -1) {
            HiddenID = '#<%=NewMedicamentoCla4IdHF.ClientID%>';
            GroupID = '#<%=NewGrupo4Label.ClientID%>';
            SubgroupID = '#<%=NewSubgrupo4Label.ClientID%>';
        } else {
            HiddenID = '#<%=NewMedicamentoClaIdHF.ClientID%>';
            GroupID = '#<%=NewGrupoLabel.ClientID%>';
            SubgroupID = '#<%=NewSubgrupoLabel.ClientID%>';
        }
    if (sender.get_text() == '') {
        $(HiddenID).val('');
        $(GroupID).html('');
        $(SubgroupID).html('');
        clearComboboxes(sender.get_id());
    }
} catch (q) {
    console.error('ERROR: ' + q);
}
}
function NewTipoMedicamentoRadComboBox_ClientTextChange(sender, eventArgs) {
    try {
        if (sender.get_text() == '') {
            var comboCMID = null;

            if (sender.get_id().indexOf('2') > -1) {
                comboCMID = '<%= NewTipoConcentracion2RadComboBox.ClientID %>';
            } else if (sender.get_id().indexOf('3') > -1) {
                comboCMID = '<%= NewTipoConcentracion3RadComboBox.ClientID %>';
            } else if (sender.get_id().indexOf('4') > -1) {
                comboCMID = '<%= NewTipoConcentracion4RadComboBox.ClientID %>';
            } else {
                comboCMID = '<%= NewTipoConcentracionRadComboBox.ClientID %>';
            }

    var combo = $find(comboCMID);
    combo.clearItems();
    combo.set_emptyMessage('Seleccione un Medicamento');
    combo.clearSelection();
}
} catch (q) {
    console.error('ERROR: ' + q);
}
}
function clearComboboxes(id) {
    var comboTMID = null;
    var comboCMID = null;

    if (sender.get_id().indexOf('2') > -1) {
        comboTMID = '<%= NewTipoMedicamento2RadComboBox.ClientID %>';
        comboCMID = '<%= NewTipoConcentracion2RadComboBox.ClientID %>';
    } else if (sender.get_id().indexOf('3') > -1) {
        comboTMID = '<%= NewTipoMedicamento3RadComboBox.ClientID %>';
        comboCMID = '<%= NewTipoConcentracion3RadComboBox.ClientID %>';
    } else if (sender.get_id().indexOf('4') > -1) {
        comboTMID = '<%= NewTipoMedicamento4RadComboBox.ClientID %>';
        comboCMID = '<%= NewTipoConcentracion4RadComboBox.ClientID %>';
    } else {
        comboTMID = '<%= NewTipoMedicamentoRadComboBox.ClientID %>';
        comboCMID = '<%= NewTipoConcentracionRadComboBox.ClientID %>';
    }
    var combo = $find("<%= NewTipoMedicamentoRadComboBox.ClientID %>");
    combo.clearItems();
    combo.set_emptyMessage('Seleccione un Medicamento');
    combo.clearSelection();
    combo = $find("<%= NewTipoConcentracionRadComboBox.ClientID %>");
    combo.clearItems();
    combo.set_emptyMessage('Seleccione una Concentracion');
    combo.clearSelection();
}
                                </script></div><div id="NewReceta1" class="NewReceta1" runat="server" style="display: none;">
                                <asp:Label ID="Label19" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento1DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>
                                <div class="validation">
                                    <span id="TipoMedicamentoError" style="display: none;">Debe seleccionar un tipo de presentación.</span> <%--<asp:CustomValidator ID="CustomValidator1" runat="server"
                                            ValidationGroup="NewReceta"
                                            ErrorMessage=""
                                            ControlToValidate="TipoMedicamento1DDL"
                                            ClientValidationFunction="TipoMedicamento1DDL_Validate"
                                            Display="Dynamic" />--%></div><asp:Label ID="Label20" Text="Medicamento" runat="server" CssClass="label" />
                                <asp:TextBox ID="Medicamento1Txt" runat="server" CssClass="biggerField">
                                </asp:TextBox><div class="validation">
                                    <span id="MedicamentoError" style="display: none;">Debe ingresar el nombre del medicamento.</span> </div><%--<telerik:RadComboBox ID="MedicamentoCombo" runat="server">
                                    </telerik:RadComboBox>--%></div><asp:Label Text="Cantidad" runat="server" CssClass="label" />
                            <telerik:RadNumericTextBox ID="Cantidad1Txt" runat="server"
                                NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                            </telerik:RadNumericTextBox>
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Cantidad1Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La cantidad es requerida." />
                            </div>
                            <asp:Label ID="Label21" Text="Instrucciones de uso" runat="server" CssClass="label" />
                            <asp:TextBox ID="Instrucciones1Txt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Instrucciones1Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La Instrucción es requerida." />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Instrucciones1Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel4" runat="server" GroupingText="Segundo Medicamento" CssClass="ExpandCollapse">
                        <div style="display: none;">
                            <div id="Liname2SelectorDiv" runat="server">
                                <!--IS LINAME-->
                                <input id="RecetaType2" type="radio" name="RecetaType2" value="1" checked="checked" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE GENÉRICO <!--IS'T LINAME--><input type="radio" name="RecetaType2" value="0" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE COMERCIAL </div><div id="NewRecetaLINAME2" class="NewRecetaLINAME">
                                <asp:Label ID="Label43" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewMedicamento2RadComboBox" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                    OnClientSelectedIndexChanged="NewMedicamentoComboBox_ClientSelectedIndexChanged"
                                    OnClientTextChange="NewMedicamentoComboBox_ClientTextChange">
                                    <WebServiceSettings Method="GetMedicamentoLINAME" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:HiddenField runat="server" ID="NewMedicamentoCla2IdHF" Value='<%# Eval("MedicamentoClaId") %>' />

                                <asp:Label Text="Grupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewGrupo2Label" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />
                                <asp:Label Text="Subgrupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewSubgrupo2Label" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />

                                <asp:Label ID="Label48" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoMedicamento2RadComboBox" runat="server"
                                    OnClientItemsRequesting="NewOnClientItemsRequesting"
                                    OnClientSelectedIndexChanged="NewTipoMedicamentoRadComboBox_ClientSelectedIndexChanged"
                                    OnClientItemsRequested="NewTipoMedicamentoRadComboBox_ClientItemsRequeted"
                                    OnClientTextChange="NewTipoMedicamentoRadComboBox_ClientTextChange"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoMedicamentoByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>

                                <asp:Label Text="Concentración" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoConcentracion2RadComboBox" runat="server"
                                    OnClientItemsRequesting="NewTipoConcentracionRadComboBox_ClientItemsRequesting"
                                    OnClientItemsRequested="NewTipoConcentracionRadComboBox_ClientItemsRequeted"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoConcentracionByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                            </div>
                            <div id="NewReceta2" runat="server" style="display: none;">
                                <asp:Label ID="Label23" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento2DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>

                                <asp:Label ID="Label24" Text="Medicamento" runat="server" CssClass="label" />
                                <asp:TextBox ID="Medicamento2Txt" runat="server" CssClass="biggerField">
                                </asp:TextBox><%--<telerik:RadComboBox ID="MedicamentoCombo" runat="server">
                                        </telerik:RadComboBox>--%></div><asp:Label Text="Cantidad" runat="server" CssClass="label" />
                            <telerik:RadNumericTextBox ID="Cantidad2Txt" runat="server"
                                NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                            </telerik:RadNumericTextBox>
                            <%-- <div class="validation">
                                <asp:RequiredFieldValidator ID="Cantidad2TxtValidator" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Cantidad2Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La cantidad es requerida." />
                            </div>--%>
                            <asp:Label ID="Label25" Text="Instrucciones de uso" runat="server" CssClass="label" />
                            <asp:TextBox ID="Instrucciones2Txt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Instrucciones2Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel5" runat="server" GroupingText="Tercer Medicamento" CssClass="ExpandCollapse">
                        <div style="display: none;">
                            <div id="Liname3SelectorDiv" runat="server">
                                <!--IS LINAME-->
                                <input id="RecetaTypeID3" type="radio" name="RecetaType3" value="1" checked="checked" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE GENÉRICO <!--IS'T LINAME--><input type="radio" name="RecetaType3" value="0" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE COMERCIAL </div><div id="NewRecetaLINAME3" class="NewRecetaLINAME">
                                <asp:Label ID="Label32" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewMedicamento3RadComboBox" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                    OnClientSelectedIndexChanged="NewMedicamentoComboBox_ClientSelectedIndexChanged"
                                    OnClientTextChange="NewMedicamentoComboBox_ClientTextChange">
                                    <WebServiceSettings Method="GetMedicamentoLINAME" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:HiddenField runat="server" ID="NewMedicamentoCla3IdHF" Value='<%# Bind("MedicamentoClaId") %>' />

                                <asp:Label Text="Grupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewGrupo3Label" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />
                                <asp:Label Text="Subgrupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewSubgrupo3Label" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />

                                <asp:Label ID="Label46" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoMedicamento3RadComboBox" runat="server"
                                    OnClientItemsRequesting="NewOnClientItemsRequesting"
                                    OnClientSelectedIndexChanged="NewTipoMedicamentoRadComboBox_ClientSelectedIndexChanged"
                                    OnClientItemsRequested="NewTipoMedicamentoRadComboBox_ClientItemsRequeted"
                                    OnClientTextChange="NewTipoMedicamentoRadComboBox_ClientTextChange"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoMedicamentoByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>

                                <asp:Label ID="Label47" Text="Concentración" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoConcentracion3RadComboBox" runat="server"
                                    OnClientItemsRequesting="NewTipoConcentracionRadComboBox_ClientItemsRequesting"
                                    OnClientItemsRequested="NewTipoConcentracionRadComboBox_ClientItemsRequeted"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoConcentracionByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                            </div>
                            <div id="NewReceta3" class="NewReceta3" runat="server" style="display: none;">
                                <asp:Label ID="Label26" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento3DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>

                                <asp:Label ID="Label27" Text="Medicamento" runat="server" CssClass="label" />
                                <asp:TextBox ID="Medicamento3Txt" runat="server" CssClass="biggerField">
                                </asp:TextBox><%--<telerik:RadComboBox ID="MedicamentoCombo" runat="server">
                                    </telerik:RadComboBox>--%></div><asp:Label Text="Cantidad" runat="server" CssClass="label" />
                            <telerik:RadNumericTextBox ID="Cantidad3Txt" runat="server"
                                NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                            </telerik:RadNumericTextBox>
                            <%--<div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Cantidad3Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La cantidad es requerida." />
                            </div>--%>
                            <asp:Label ID="Label28" Text="Instrucciones de uso" runat="server" CssClass="label" />
                            <asp:TextBox ID="Instrucciones3Txt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Instrucciones3Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="Panel6" runat="server" GroupingText="Cuarto Medicamento" CssClass="ExpandCollapse">
                        <div style="display: none;">
                            <div id="Liname4SelectorDiv" runat="server">
                                <!--IS LINAME-->
                                <input id="RecetaTypeID4" type="radio" name="RecetaType4" value="1" checked="checked" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE GENÉRICO <!--IS'T LINAME--><input type="radio" name="RecetaType4" value="0" class="radioTipoRemedio" />&nbsp;MEDICAMENTO NOMBRE COMERCIAL </div><div id="NewRecetaLINAME4" class="NewRecetaLINAME">
                                <asp:Label ID="Label34" Text="Medicamento" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewMedicamento4RadComboBox" runat="server"
                                    ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                    AutoPostBack="false" EnableLoadOnDemand="true" CssClass="biggerField"
                                    OnClientSelectedIndexChanged="NewMedicamentoComboBox_ClientSelectedIndexChanged"
                                    OnClientTextChange="NewMedicamentoComboBox_ClientTextChange">
                                    <WebServiceSettings Method="GetMedicamentoLINAME" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                                <asp:HiddenField runat="server" ID="NewMedicamentoCla4IdHF" Value='<%# Bind("MedicamentoClaId") %>' />

                                <asp:Label ID="Label36" Text="Grupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewGrupo4Label" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />
                                <asp:Label ID="Label44" Text="Subgrupo" runat="server" CssClass="label" />
                                <asp:Label ID="NewSubgrupo4Label" Text="" runat="server" CssClass="biggerField"
                                    Style="display: block; border: 1px solid #aaa; height: 40px;" />

                                <asp:Label ID="Label45" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoMedicamento4RadComboBox" runat="server"
                                    OnClientItemsRequesting="NewOnClientItemsRequesting"
                                    OnClientSelectedIndexChanged="NewTipoMedicamentoRadComboBox_ClientSelectedIndexChanged"
                                    OnClientItemsRequested="NewTipoMedicamentoRadComboBox_ClientItemsRequeted"
                                    OnClientTextChange="NewTipoMedicamentoRadComboBox_ClientTextChange"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoMedicamentoByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>

                                <asp:Label ID="Label49" Text="Concentración" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="NewTipoConcentracion4RadComboBox" runat="server"
                                    OnClientItemsRequesting="NewTipoConcentracionRadComboBox_ClientItemsRequesting"
                                    OnClientItemsRequested="NewTipoConcentracionRadComboBox_ClientItemsRequeted"
                                    CssClass="biggerField">
                                    <WebServiceSettings Method="GetTipoConcentracionByMedicamentoClaId" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                </telerik:RadComboBox>
                            </div>
                            <div id="NewReceta4" class="NewReceta4" runat="server" style="display: none;">
                                <asp:Label ID="Label29" Text="Presentación" runat="server" CssClass="label" />
                                <telerik:RadComboBox ID="TipoMedicamento4DDL" runat="server"
                                    EmptyMessage="Seleccione un tipo de presentación"
                                    CssClass="biggerField"
                                    DataValueField="TipoMedicamentoId"
                                    DataTextField="Nombre">
                                </telerik:RadComboBox>

                                <asp:Label ID="Label30" Text="Medicamento" runat="server" CssClass="label" />
                                <asp:TextBox ID="Medicamento4Txt" runat="server" CssClass="biggerField">
                                </asp:TextBox><%--<telerik:RadComboBox ID="MedicamentoCombo" runat="server">
                                    </telerik:RadComboBox>--%></div><asp:Label Text="Cantidad" runat="server" CssClass="label" />
                            <telerik:RadNumericTextBox ID="Cantidad4Txt" runat="server"
                                NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="300"
                                IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false">
                            </telerik:RadNumericTextBox>
                            <%--<div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Cantidad4Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La cantidad es requerida." />
                            </div>--%>
                            <asp:Label Text="Instrucciones de uso" runat="server" CssClass="label" />
                            <asp:TextBox ID="Instrucciones4Txt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                            <div class="validation">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="Instrucciones4Txt"
                                    ValidationGroup="NewReceta"
                                    ErrorMessage="La Instrucción no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SaveNewRecetaLB" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="NewReceta"
                            OnClientClick="return NewReceta_Validate();"
                            OnClick="SaveNewRecetaLB_Click">
                            <asp:Label ID="Label22" Text="Guardar" runat="server" />
                        </asp:LinkButton><asp:LinkButton ID="CancelNewRecetaLB" Text="Cancelar" runat="server" />
                    </div>
                    <script type="text/javascript">
                        function SelectRecipeType(radioButton, panel) {
                            var IsLINAME = $(radioButton).val() == '1';
                            var panelReceta = $('.NewReceta' + panel);
                            var panelLiname = $('#NewRecetaLINAME' + panel);
                            if (IsLINAME) {
                                panelReceta.hide();
                                panelLiname.show();
                            } else {
                                panelReceta.show();
                                panelLiname.hide();
                            }
                        }
                    </script>
                </asp:Panel>

                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    <AjaxSettings>
                        <%--<telerik:AjaxSetting AjaxControlID="CiudadRadComboBox">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="ProveedorExComplementarioDDL"></telerik:AjaxUpdatedControl>
                            </UpdatedControls>
                        </telerik:AjaxSetting>--%>
                        <telerik:AjaxSetting AjaxControlID="TipoEstudioPrestacionesExaComboBox">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="TipoEstudioDDL"></telerik:AjaxUpdatedControl>
                                <telerik:AjaxUpdatedControl ControlID="LabelBusquedaErrorPrestaciones"></telerik:AjaxUpdatedControl>
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="ProveedorExComplementarioDDL">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="TipoEstudioPrestacionesExaComboBox"></telerik:AjaxUpdatedControl>
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="CancelExComplementarioLB">
                            <UpdatedControls>
                                <%--    <telerik:AjaxUpdatedControl ControlID="ExComplementarioPanel"></telerik:AjaxUpdatedControl>--%>
                                <%--  <telerik:AjaxUpdatedControl ControlID="EstudioIdHF"></telerik:AjaxUpdatedControl>
                                <telerik:AjaxUpdatedControl ControlID="ObservacionTxt"></telerik:AjaxUpdatedControl>
                                <telerik:AjaxUpdatedControl ControlID="CiudadRadComboBox"></telerik:AjaxUpdatedControl>
                                <telerik:AjaxUpdatedControl ControlID="ProveedorExComplementarioDDL"></telerik:AjaxUpdatedControl>
                                <telerik:AjaxUpdatedControl ControlID="TipoEstudioDDL"></telerik:AjaxUpdatedControl>
                                <telerik:AjaxUpdatedControl ControlID="TipoEstudioPrestacionesExaComboBox"></telerik:AjaxUpdatedControl>--%>
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                </telerik:RadAjaxLoadingPanel>

                <fieldset>
                    <asp:Panel ID="ExComplementarioPanel" runat="server"
                        CssClass="Default_Popup"
                        GroupingText="Nuevo examen Complementario">
                        <div>
                            <div class="error">
                                <asp:Label ID="MessageExComplementarioLabel" Text="" runat="server" />
                            </div>
                            <asp:HiddenField ID="EstudioIdHF" runat="server" Value="0" />
                            <div id="Contents">
                                <table>
                                    <tr>
                                        <td class="auto-style2">
                                            <asp:Label ID="Label16" runat="server" CssClass="label" Text="Ciudad" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">
                                            <telerik:RadComboBox ID="CiudadRadComboBox" runat="server"
                                                AutoPostBack="false"
                                                DataSourceID="CiudadExComplementarioODS"
                                                DataTextField="Nombre" DataValueField="CiudadId"
                                                EmptyMessage="Seleccione una Ciudad" MarkFirstMatch="true"
                                                OnClientSelectedIndexChanged="CiudadRadComboBox_OnClientSelectedIndexChanged"
                                                Width="250px" ZIndex="50">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">
                                            <asp:Label ID="NewPLabel" Text="Proveedor" runat="server" CssClass="label" />
                                            <telerik:RadComboBox ID="ProveedorExComplementarioDDL" runat="server"
                                                EnableLoadOnDemand="true"
                                                EmptyMessage="Seleccione un Proveedor"
                                                OnClientItemsRequesting="ProveedorExComplementarioDDL_OnClientItemsRequesting"
                                                OnSelectedIndexChanged="ProveedorExComplementarioDDL_SelectedIndexChanged"
                                                MarkFirstMatch="true"
                                                ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                AutoPostBack="true"
                                                Width="250px" ZIndex="50">
                                                <WebServiceSettings Method="GetProveedorPorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                            </telerik:RadComboBox>
                                            <div class="validation">
                                                <asp:CustomValidator ID="ProveedorCV" runat="server"
                                                    ValidationGroup="ExComplementario"
                                                    ErrorMessage="Debe seleccionar un proveedor."
                                                    ClientValidationFunction="ProveedorCV_Validate"
                                                    Display="Dynamic" />
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Label ID="Label54" runat="server" CssClass="label" Text="Tipo Estudio" Visible="true" />

                            <telerik:RadComboBox ID="TipoEstudioPrestacionesExaComboBox" runat="server"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="PrestacionesExaComboBox_SelectedIndexChanged"
                                MarkFirstMatch="false"
                                Width="250px" ZIndex="50">

                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione un Tipo de Estudio" Value="0" />
                                    <telerik:RadComboBoxItem Text="LABORATORIO" Value="LA" />
                                    <telerik:RadComboBoxItem Text="IMAGENOLOGIA" Value="IM" />
                                    <telerik:RadComboBoxItem Text="CARDIOLOGIA" Value="CA" />
                                </Items>
                            </telerik:RadComboBox>
                            <br />
                            <div>
                                <asp:Label ID="LabelBusquedaErrorPrestaciones"
                                    runat="server" CssClass="label" Text="Debe seleccionar un Proveedor" Visible="false" ForeColor="Red" Font-Size="9px" />
                            </div>


                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script type="text/javascript">
                                    createPopup("#<%=ExComplementarioPanel.ClientID %>", ".NewEstudio", "#<%=CancelExComplementarioLB.ClientID %>");
                                    function OpenPopupExComplementario() {
                                        showPopup("#<%=ExComplementarioPanel.ClientID %>");
                                        $("#<%=ExComplementarioPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                                        $("#<%=ExComplementarioPanel.ClientID %> legend").text('Modificar Examen Complementario');
                                    }
                                    $("#<%=CancelExComplementarioLB.ClientID %>").click(function () {
                                        $("#<%=EstudioIdHF.ClientID %>").attr('value', '0');
                                        $("#<%=ObservacionTxt.ClientID %>").attr('value', '');
                                        if ($("#<%=SaveExComplementarioLB.ClientID %>").length)
                                            $("#<%=MessageExComplementarioLabel.ClientID %>").text('');
                                        $("#<%=ExComplementarioPanel.ClientID %> legend").text('Nuevo Examen Complementario');
                                        $find('<%= CiudadRadComboBox.ClientID %>').clearSelection();
                                        $find('<%= ProveedorExComplementarioDDL.ClientID %>').clearSelection();
                                        $find('<%= TipoEstudioDDL.ClientID %>').clearSelection();


                                    });

                                    function TipoEstudioPrestacionesExaComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        execute();
                                    }

                                    function CiudadRadComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        $find('<%= ProveedorExComplementarioDDL.ClientID %>').clearSelection();
                                        var combo = $find("<%= ProveedorExComplementarioDDL.ClientID %>");
                                        combo.requestItems('', false);
                                    }
                                    //  function ProveedorRadComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                    ////      ExComplementarioPanel.TipoEstudioPrestacionesExaComboBox.OnSelectedIndexChanged();
                                    //  }
                                    function ProveedorExComplementarioDDL_OnClientTextChange(sender, eventArgs) {
                                        var combo = $find("<%= TipoEstudioPrestacionesExaComboBox.ClientID %>");
                                        combo.set_selectedIndex('');
                                    }
                                    <%--function ProveedorRadComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        $find('<%= ProveedorExComplementarioDDL.ClientID %>').clearSelection();
                                        var combo = $find("<%= ProveedorExComplementarioDDL.ClientID %>");
                                        combo.requestItems('', false);
                                    }--%>


                                    function ProveedorExComplementarioDDL_OnClientItemsRequesting(sender, eventArgs) {
                                        var combo = $find("<%= CiudadRadComboBox.ClientID %>");
                                        var context = eventArgs.get_context();
                                        context["ciudadId"] = combo.get_value();
                                        context["redMedicaPaciente"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                        context["tipoPriveedor"] = 'LABORATORIO';
                                    }

                                    function CiudadRecetaRadComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        $find('<%= FarmaciaRecetaRadComboBox.ClientID %>').clearSelection();
                                        var combo = $find("<%= FarmaciaRecetaRadComboBox.ClientID %>");
                                        combo.requestItems('', false);
                                    }

                                    function ProveedorRecetaDDL_OnClientItemsRequesting(sender, eventArgs) {
                                        var combo = $find("<%= CiudadRecetaRadComboBox.ClientID %>");
                                        var context = eventArgs.get_context();
                                        context["ciudadId"] = combo.get_value();
                                        context["redMedicaPaciente"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                        context["tipoPriveedor"] = 'FARMACIA';
                                    }

                                    function CiudadEditarComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        $find('<%= ProveedorEditarComboBox.ClientID %>').clearSelection();
                                        var combo = $find("<%= ProveedorEditarComboBox.ClientID %>");
                                        combo.requestItems('', false);
                                    }
                                    function ProveedorRecetaEditarDDL_OnClientItemsRequesting(sender, eventArgs) {
                                        var combo = $find("<%= CiudadEditarComboBox.ClientID %>");
                                        var context = eventArgs.get_context();
                                        context["ciudadId"] = combo.get_value();
                                        context["redMedicaPaciente"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                        context["tipoPriveedor"] = 'FARMACIA';
                                    }
                                </script>
                            </telerik:RadCodeBlock>


                            <asp:Label ID="NewTECLabel" Text="Tipo de examen complementario" runat="server" CssClass="label" />
                            <RedSalud:TipoEstudio runat="server" ID="TipoEstudioDDL" Visible="true" />

                            <div class="validation">
                                <asp:CustomValidator ID="TipoEstudioCV" runat="server"
                                    ValidationGroup="ExComplementario"
                                    ErrorMessage="Debe seleccionar un tipo de examen complementario."
                                    ClientValidationFunction="TipoEstudioCV_Validate"
                                    Display="Dynamic" />
                            </div>

                            <asp:Label ID="NewObLabel" Text="Observaciones para el examen" runat="server" CssClass="label" />
                            <asp:TextBox ID="ObservacionTxt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />

                            <div class="validation">
                                <asp:RequiredFieldValidator ID="ObservacionRFV" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ObservacionTxt"
                                    ValidationGroup="ExComplementario"
                                    ErrorMessage="La Observación es requerida." />
                                <asp:RegularExpressionValidator ID="ObservacionREV" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ObservacionTxt"
                                    ValidationGroup="ExComplementario"
                                    ErrorMessage="La Observación no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>

                            <div class="buttonsPanel">
                                <asp:LinkButton ID="SaveExComplementarioLB" Text="" runat="server"
                                    CssClass="button"
                                    ValidationGroup="ExComplementario"
                                    OnClick="SaveExComplementarioLB_Click">
                                    <asp:Label ID="Label17" Text="Guardar" runat="server" />
                                </asp:LinkButton><asp:LinkButton ID="CancelExComplementarioLB" AutoPostBack="true"
                                    OnClick="CancelExComplementarioLB_Click"
                                    Text="Cancelar" runat="server" />
                            </div>

                            <br />
                            <div>
                                <asp:Label Text="MEnsaje de Error " runat="server" ID="AlertaLaboratorio" ForeColor="Red" Visible="false"></asp:Label></div></div></asp:Panel></fieldset> </contenttemplate><asp:Panel ID="DerivacionPanel" runat="server"
                    CssClass="Default_Popup"
                    GroupingText="Nueva Derivación a especialista">
                    <div>
                        <div class="error">
                            <asp:Label ID="MessageDerivacionLabel" Text="" runat="server" />
                        </div>
                        <asp:HiddenField ID="DerivacionIdHF" runat="server" Value="0" />

                        <asp:Label ID="Label5" Text="Ciudad" runat="server" CssClass="label" />
                        <telerik:RadComboBox ID="CiudadDerivacionComboBox" runat="server"
                            CssClass="biggerField"
                            DataSourceID="CiudadExComplementarioODS"
                            DataValueField="CiudadId"
                            DataTextField="Nombre"
                            AutoPostBack="false"
                            EmptyMessage="Seleccione una Ciudad"
                            OnClientSelectedIndexChanged="CiudadDerivacionComboBox_OnClientSelectedIndexChanged"
                            MarkFirstMatch="true">
                        </telerik:RadComboBox>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                                ControlToValidate="CiudadDerivacionComboBox"
                                ErrorMessage="Debe Seleccionar Una Ciudad"
                                ValidationGroup="Derivacion"
                                Display="Dynamic" />
                        </div>
                        <asp:Label ID="Label31" Text="Especialidad" runat="server" CssClass="label" />
                        <telerik:RadComboBox ID="EspecialidadDerivacionComboBox" runat="server"
                            CssClass="biggerField"
                            DataSourceID="EspecialidadComplementarioODS"
                            DataValueField="EspecialidadId"
                            DataTextField="Especialidad"
                            AutoPostBack="false"
                            EmptyMessage="Seleccione una Especialidad"
                            OnClientSelectedIndexChanged="EspecialidadDerivacionComboBox_OnClientSelectedIndexChanged"
                            MarkFirstMatch="true">
                        </telerik:RadComboBox>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server"
                                ControlToValidate="EspecialidadDerivacionComboBox"
                                ErrorMessage="Debe Seleccionar Una Especialidad"
                                ValidationGroup="Derivacion"
                                Display="Dynamic" />
                        </div>

                        <asp:Label ID="Label10" Text="Proveedor" runat="server" CssClass="label" />
                        <telerik:RadComboBox ID="ProveedorDerivacionDDL" runat="server"
                            CssClass="biggerField"
                            EnableLoadOnDemand="true"
                            OnClientItemsRequesting="ProveedorDerivacionDDL_OnClientItemsRequesting"
                            EmptyMessage="Seleccione un Especialista"
                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                            AutoPostBack="false"
                            MarkFirstMatch="true">
                            <WebServiceSettings Method="GetEspecialistasProveedorAutocompletePorCiudadYCliente" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                        </telerik:RadComboBox>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server"
                                ControlToValidate="ProveedorDerivacionDDL"
                                ErrorMessage="Debe Seleccionar Un Medico Proveedor"
                                ValidationGroup="Derivacion"
                                Display="Dynamic" />
                        </div>
                        <script type="text/javascript">
                            //////////////////////////////////////////////////////////////////////////////////
                            function CiudadDerivacionComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                $find('<%= ProveedorDerivacionDDL.ClientID %>').clearSelection();
                                var combo = $find("<%= ProveedorDerivacionDDL.ClientID %>");
                                combo.requestItems('', false);
                            }
                            function EspecialidadDerivacionComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                $find('<%= ProveedorDerivacionDDL.ClientID %>').clearSelection();
                                var combo = $find("<%= ProveedorDerivacionDDL.ClientID %>");
                                combo.requestItems('', false);
                            }
                            function ProveedorDerivacionDDL_OnClientItemsRequesting(sender, eventArgs) {
                                var combo = $find("<%= CiudadDerivacionComboBox.ClientID %>");
                                var comboEspecialidad = $find("<%= EspecialidadDerivacionComboBox.ClientID %>");
                                var context = eventArgs.get_context();
                                context["ciudadId"] = combo.get_value();
                                context["tipoPriveedor"] = "MEDICO";
                                context["especialidadId"] = comboEspecialidad.get_value();
                                context["clienteId"] = $('#<%= ClienteIdHF.ClientID%>').val();
                            }



                            ///////////////////////////////////////////////////////////////////////////
                            function ProveedorCirujanoDDL_OnClientItemsRequesting(sender, eventArgs) {
                                var combo = $find("<%= CiudadDerivacionComboBox.ClientID %>");
                                var comboCirujano = $find("<%= CirujanoRadComboBox.ClientID %>");
                                var context = eventArgs.get_context();
                                context["ciudadId"] = combo.get_value();
                                context["especialidadId"] = comboCirujano.get_value();
                                context["clienteId"] = $('#<%= ClienteIdHF.ClientID%>').val();
                            }
                        </script>
                        <div class="validation">
                            <asp:CustomValidator ID="ProveedorDerivacionDDLCV" runat="server"
                                ValidationGroup="Cirugia"
                                ErrorMessage="Debe seleccionar un proveedor."
                                ClientValidationFunction="ProveedorDerivacionDDLCV_Validate"
                                Display="Dynamic" />
                        </div>

                        <asp:Label ID="Label12" Text="Observaciones para la derivacion a especialista" runat="server" CssClass="label" />
                        <asp:TextBox ID="ObservacionDerivacionTxt" runat="server"
                            TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                Display="Dynamic"
                                ControlToValidate="ObservacionDerivacionTxt"
                                ValidationGroup="Derivacion"
                                ErrorMessage="La Observación es requerida." />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                                Display="Dynamic"
                                ControlToValidate="ObservacionDerivacionTxt"
                                ValidationGroup="Derivacion"
                                ErrorMessage="La Observación no puede exceder 2000 caracteres."
                                ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                        </div>
                        <div class="buttonsPanel">
                            <asp:LinkButton ID="DerivacionSaveLB" Text="" runat="server"
                                CssClass="button"
                                ValidationGroup="Derivacion"
                                OnClick="DerivacionSaveLB_Click">
                                <asp:Label ID="Label13" Text="Guardar" runat="server" />
                            </asp:LinkButton><asp:LinkButton ID="CancelDerivacionLB" Text="Cancelar" runat="server" />
                        </div>

                        <div>
                            <asp:Label Text="Mensaje de Error " runat="server" ID="AlertaDerivacion" ForeColor="Red" Visible="false"></asp:Label></div></div></asp:Panel><asp:Panel ID="InternacionPanel" runat="server"
                    CssClass="Default_Popup"
                    GroupingText="Nueva Internación">
                    <div>
                        <div class="error">
                            <asp:Label ID="MessageInternacionLabel" Text="" runat="server" />
                        </div>
                        <table>
                            <tr>
                                <td class="auto-style3"></td>
                                <td class="auto-style3">
                                    <input id="IsCirugia" type="checkbox" name="IsCirugia" value="0" style="margin-top: 5px;" />
                                    <label>¿ES CIRUGÍA O PROCEDIMIENTO?</label> </td></tr></table><div id="CodigoArancelario" style="display: none;">
                            <table>
                                <tr>
                                    <td class="auto-style3">
                                        <asp:Label Text="Ciudad" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="CiudadCirugiaComboBox" runat="server"
                                            Width="250px"
                                            DataSourceID="CiudadExComplementarioODS"
                                            OnClientSelectedIndexChanged="CiudadCirugiaComboBox_OnClientSelectedIndexChanged"
                                            DataValueField="CiudadId"
                                            DataTextField="Nombre"
                                            AutoPostBack="false"
                                            EmptyMessage="Seleccione una Ciudad"
                                            MarkFirstMatch="true">

                                            <%--                                            CiudadInternacionComboBox_OnClientSelectedIndexChanged"--%>
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator runat="server"
                                                ValidationGroup="InternacionCirugia"
                                                ErrorMessage="Debe seleccionar una Ciudad."
                                                ClientValidationFunction="CiudadInternacionCirugia_Validate"
                                                Display="Dynamic" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">
                                        <asp:Label ID="Label55" Text="Especialidad" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="EspecialidadCirujanoComboBox" runat="server"
                                            Width="250px"
                                            DataSourceID="EspecialidadComplementarioODS"
                                            DataValueField="EspecialidadId"
                                            DataTextField="Especialidad"
                                            AutoPostBack="false"
                                            EmptyMessage="Seleccione una Especialidad"
                                            OnClientSelectedIndexChanged="EspecialidadCirujanoComboBox_OnClientSelectedIndexChanged"
                                            MarkFirstMatch="true">
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator runat="server"
                                                ValidationGroup="InternacionCirugia"
                                                ErrorMessage="Debe seleccionar una Especialidad."
                                                ClientValidationFunction="EspecialidadCirujano_Validate"
                                                Display="Dynamic" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">
                                        <asp:Label ID="Label56" Text="Cirujano" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="CirujanoRadComboBox" runat="server"
                                            Width="250px"
                                            EnableLoadOnDemand="true"
                                            OnClientItemsRequesting="ProveedorCirujanoDDL_OnClientItemsRequesting"
                                            EmptyMessage="Seleccione un Especialista"
                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                            AutoPostBack="false"
                                            MarkFirstMatch="true">
                                            <WebServiceSettings Method="GetEspecialistasAutocompletePorCiudadYCliente" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator runat="server"
                                                ValidationGroup="InternacionCirugia"
                                                ErrorMessage="Debe seleccionar un Medico Cirujano."
                                                ClientValidationFunction="MedicoCirujano_Validate"
                                                Display="Dynamic" />
                                        </div>
                                    </td>
                                </tr>

                                <script type="text/javascript">
                                    function CiudadCirugiaComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        $find('<%= CirujanoRadComboBox.ClientID %>').clearSelection();
                                        var combo = $find("<%= CirujanoRadComboBox.ClientID %>");
                                        combo.requestItems('', false);

                                        $find('<%= ProveedorCirugiaDLL.ClientID %>').clearSelection();
                                        var combo = $find("<%= ProveedorCirugiaDLL.ClientID %>");
                                        combo.requestItems('', false);
                                    }
                                    function EspecialidadCirujanoComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                        $find('<%= CirujanoRadComboBox.ClientID %>').clearSelection();
                                        var combo = $find("<%= CirujanoRadComboBox.ClientID %>");
                                        combo.requestItems('', false);
                                    }

                                    ///////////////////////////////////////////////////////////////////////////
                                    function ProveedorCirujanoDDL_OnClientItemsRequesting(sender, eventArgs) {
                                        var combo = $find("<%= CiudadCirugiaComboBox.ClientID %>");
                                        var comboEspecialidadCirujano = $find("<%= EspecialidadCirujanoComboBox.ClientID %>");
                                        var context = eventArgs.get_context();
                                        context["ciudadId"] = combo.get_value();
                                        context["especialidadId"] = comboEspecialidadCirujano.get_value();
                                        context["clienteId"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                    }
                                    function ProveedorCirugiaDLL_OnClientItemsRequesting(sender, eventArgs) {
                                        var combo = $find("<%= CiudadCirugiaComboBox.ClientID %>");
                                        var context = eventArgs.get_context();
                                        context["ciudadId"] = combo.get_value();
                                        context["redMedicaPaciente"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                        context["tipoPriveedor"] = 'HOSPITAL';
                                    }
                                </script>
                                <tr>
                                    <td class="auto-style3">
                                        <asp:Label Text="Proveedor" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="ProveedorCirugiaDLL" runat="server"
                                            EnableLoadOnDemand="true"
                                            Width="250px"
                                            EmptyMessage="Seleccione un Proveedor"
                                            OnClientItemsRequesting="ProveedorCirugiaDLL_OnClientItemsRequesting"
                                            MarkFirstMatch="true"
                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                            AutoPostBack="false">
                                            <WebServiceSettings Method="GetProveedorPorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator runat="server"
                                                ValidationGroup="InternacionCirugia"
                                                ErrorMessage="Debe seleccionar un Proveedor."
                                                ClientValidationFunction="ProveedorCirugiaDLL_Validate"
                                                Display="Dynamic" />
                                        </div>
                                        <%--        <telerik:RadComboBox ID="ProveedorCirugiaDLL" runat="server"
                                            Width="250px"
                                            DataSourceID="ProveedorInternacionODS"
                                            DataValueField="ProveedorId"
                                            DataTextField="NombreJuridico"
                                            MarkFirstMatch="true">
                                        </telerik:RadComboBox>--%>
                                      
                                    </td>
                                </tr>
                            </table>
                            <asp:Label Text="Procedimiento Médico Quirúrgico" runat="server" CssClass="label" />
                            <telerik:RadComboBox ID="CodigoArancelarioRadComboBox" runat="server"
                                DataSourceID="CirugiasNotSavedODS"
                                CssClass="biggerField"
                                Filter="StartsWith"
                                EmptyMessage="Seleccione un Procedimiento Quirúrgico."
                                DataTextField="CodigoArancelario"
                                DataValueField="CodigoArancelarioId">
                            </telerik:RadComboBox>
                            <div class="validation">
                                <asp:CustomValidator runat="server"
                                    ValidationGroup="InternacionCirugia"
                                    ErrorMessage="Debe seleccionar una Procedimiento Quirúrgico."
                                    ClientValidationFunction="CodigoArancelario_Validate"
                                    Display="Dynamic" />
                            </div>

                            <%-- <telerik:RadComboBox ID="CodigoArancelarioRadComboBox" runat="server" Visible="false"
                                CssClass="biggerField"
                                EnableLoadOnDemand="true"
                                EmptyMessage="Seleccione un Procedimiento Médico Quirúrgico"
                                ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                AutoPostBack="false"
                                MarkFirstMatch="true">
                                <WebServiceSettings Method="GetCodigoArancelarioAutocomplete" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                            </telerik:RadComboBox>--%>
                            <asp:Label Text="Observaciones" runat="server" CssClass="label" />
                            <asp:TextBox ID="ObservacionCirugiaTxt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ObservacionCirugiaTxt"
                                    ValidationGroup="InternacionCirugia"
                                    ErrorMessage="La Observación es requerida." />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ObservacionCirugiaTxt"
                                    ValidationGroup="InternacionCirugia"
                                    ErrorMessage="La Observación no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>
                            <div class="buttonsPanel">
                                <asp:LinkButton ID="InternacionCirugiaSaveLB" Text="" runat="server"
                                    CssClass="button"
                                    ValidationGroup="InternacionCirugia"
                                    OnClick="InternacionSaveLB_Click">
                                    <asp:Label ID="Label60" Text="Guardar" runat="server" />
                                </asp:LinkButton><asp:LinkButton ID="CancelCirugiaLB" Text="Cancelar" runat="server" />
                            </div>
                            <div>
                                <asp:Label Text="Mensaje de Error " runat="server" ID="AlertaCirugia" ForeColor="Red" Visible="false"></asp:Label></div></div><div id="CasoInternacionV">
                            <asp:HiddenField ID="InternacionUpdateIdHF" runat="server" Value="0" />
                            <asp:HiddenField ID="InternacionIdHF" runat="server" Value="0" />
                            <asp:Label Text="Ciudad" runat="server" CssClass="label" />
                            <table>
                                <tr>
                                    <td class="auto-style3">
                                        <telerik:RadComboBox ID="CiudadInternacionComboBox" runat="server"
                                            Width="250px"
                                            DataSourceID="CiudadExComplementarioODS"
                                            OnClientSelectedIndexChanged="CiudadInternacionComboBox_OnClientSelectedIndexChanged"
                                            DataValueField="CiudadId"
                                            DataTextField="Nombre"
                                            EmptyMessage="Seleccione una Ciudad"
                                            MarkFirstMatch="true">
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator runat="server"
                                                ValidationGroup="Internacion"
                                                ErrorMessage="Debe seleccionar una Ciudad."
                                                ClientValidationFunction="CiudadInternacionInternacionDDLCV_Validate"
                                                Display="Dynamic" />
                                        </div>

                                    </td>
                                    <td class="auto-style3"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">
                                        <asp:Label Text="Proveedor" runat="server" CssClass="label" />
                                        <telerik:RadComboBox ID="ProveedorInternacionDDL" runat="server"
                                            EnableLoadOnDemand="true"
                                            Width="250px"
                                            EmptyMessage="Seleccione un Proveedor"
                                            OnClientItemsRequesting="ProveedorInternacionDLL_OnClientItemsRequesting"
                                            MarkFirstMatch="true"
                                            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                            AutoPostBack="false">
                                            <WebServiceSettings Method="GetProveedorPorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                        </telerik:RadComboBox>
                                        <div class="validation">
                                            <asp:CustomValidator runat="server"
                                                ValidationGroup="Internacion"
                                                ErrorMessage="Debe seleccionar un proveedor."
                                                ClientValidationFunction="ProveedorInternacionDDLCV_Validate"
                                                Display="Dynamic" />
                                        </div>
                                    </td>
                                    <td></td>

                                </tr>

                            </table>
                            <script type="text/javascript">
                                function CiudadInternacionComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                                    $find('<%= ProveedorInternacionDDL.ClientID %>').clearSelection();
                                    var combo = $find("<%= ProveedorInternacionDDL.ClientID %>");
                                    combo.requestItems('', false);
                                }


                                ///////////////////////////////////////////////////////////////////////////                               
                                function ProveedorInternacionDLL_OnClientItemsRequesting(sender, eventArgs) {
                                    var combo = $find("<%= CiudadInternacionComboBox.ClientID %>");
                                    var context = eventArgs.get_context();
                                    context["ciudadId"] = combo.get_value();
                                    context["redMedicaPaciente"] = $('#<%= ClienteIdHF.ClientID%>').val();
                                    context["tipoPriveedor"] = 'HOSPITAL';
                                }
                            </script>

                            <asp:Label Text="Diagnostico Medico" runat="server" CssClass="label" ID="LblDiagnosticoMedico" Visible="true" />
                            <telerik:RadComboBox ID="EnfermedadesInternacionComboBox" runat="server"
                                ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField" Visible="true"
                                EmptyMessage="Seleccione un Diagnostico">
                                <WebServiceSettings Method="GetEnfermedades" Path="../AutoCompleteWS/ComboBoxWebServices.asmx">
                                </WebServiceSettings>
                            </telerik:RadComboBox>
                            <div class="validation">
                                <asp:CustomValidator runat="server"
                                    ValidationGroup="Internacion"
                                    ErrorMessage="Debe seleccionar una Enfermedad."
                                    ClientValidationFunction="EnfermedadesComboBox_Validate"
                                    Display="Dynamic" />
                            </div>

                            <%-- <asp:HiddenField runat="server" ID="EnfermedadHF" Value='<%# Bind("EnfermedadId") %>' />
                            <asp:TextBox ID="DiagnosticoPresuntivoTxt" runat="server" Visible="false"
                                CssClass="biggerField"
                                Text='<%# Bind("DiagnosticoPresuntivo") %>' />--%>



                            <asp:ObjectDataSource ID="ProveedorInternacionODS" runat="server"
                                TypeName="Artexacta.App.Proveedor.BLL.ProveedorBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="getProveedorList"
                                OnSelected="ProveedorInternacionODS_Selected">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="HOSPITAL" Name="TipoProveedorId" Type="String" />
                                    <asp:ControlParameter ControlID="CiudadHF" Name="CiudadId" PropertyName="Value" DbType="String" />
                                    <asp:ControlParameter ControlID="ClienteIdHF" Name="redMedicaIdPaciente" PropertyName="Value" DbType="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <script type="text/javascript">
                                $(document).ready(function () {
                                    $('.radioTipoRemedio').on('change', function () {
                                        var panel0 = $(this).parent().next();
                                        var panel1 = $(this).parent().next().next();
                                        if ($(this).val() == 1) {
                                            panel0.show();
                                            panel1.hide();
                                        } else {
                                            panel0.hide();
                                            panel1.show();
                                        }
                                    });
                                    $('#IsCirugia').click(function () {
                                        var title = $('#<%= InternacionPanel.ClientID%> legend');
                                        if ($("#IsCirugia").prop("checked")) {
                                            $(title).html('NUEVA CIRUGIA');
                                            $('#CodigoArancelario').show();
                                            //////////////////////////////////////
                                            $('#CasoInternacionV').hide();
                                            $("#<%=EsCirugiaHF.ClientID %>").attr('value', '1');
                                        } else {
                                            $(title).html('NUEVA INTERNACIÓN');
                                            $('#CodigoArancelario').hide();
                                            $('#CasoInternacionV').show();
                                            $("#<%=EsCirugiaHF.ClientID %>").attr('value', '0');

                                            //////////////////////

                                        }
                                    });
                                });


                            </script>
                            <asp:Label Text="Observaciones" runat="server" CssClass="label" />
                            <asp:TextBox ID="ObservacionInternacionTxt" runat="server"
                                TextMode="MultiLine" Rows="3" CssClass="biggerField" />
                            <div class="validation">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ObservacionInternacionTxt"
                                    ValidationGroup="Internacion"
                                    ErrorMessage="La Observación es requerida." />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ObservacionInternacionTxt"
                                    ValidationGroup="Internacion"
                                    ErrorMessage="La Observación no puede exceder 2000 caracteres."
                                    ValidationExpression="<%$ Resources: Validations, GenericLength2000  %>" />
                            </div>
                            <div class="buttonsPanel">
                                <asp:LinkButton ID="InternacionSaveLB" Text="" runat="server"
                                    CssClass="button"
                                    ValidationGroup="Internacion"
                                    OnClick="InternacionSaveLB_Click">
                                    <asp:Label ID="Label15" Text="Guardar" runat="server" />
                                </asp:LinkButton><asp:LinkButton ID="CancelInternacionLB" Text="Cancelar" runat="server" />
                            </div>
                            <div>
                                <asp:Label Text="Mensaje de Error " runat="server" ID="AlertaInternacion" ForeColor="Red" Visible="false"></asp:Label></div></div></div></asp:Panel><asp:Panel ID="EditCirugiaPanel" runat="server"
                    CssClass="Default_Popup"
                    GroupingText="Editar Cirugia">
                    <div>
                        <div class="error">
                            <asp:Label ID="Label59" Text="" runat="server" />
                        </div>
                        <table>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Valor UMA" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextValorUma" onkeypress="CalCuloCirugia()" onkeyup="CalCuloCirugia()"
                                        RenderMode="Lightweight" runat="server" Width="120px" AutoPostBack="false" MinValue="0" MaxValue="100"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                                            ControlToValidate="TextValorUma"
                                            ErrorMessage="Se debe Colocar un Valor Uma"
                                            ValidationGroup="CirugiaUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Cantidad UMA" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextCantidadUma" runat="server" Width="120px"
                                        onkeypress="CalCuloCirugia()" onkeyup="CalCuloCirugia()" AutoPostBack="false" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                                            ControlToValidate="TextCantidadUma"
                                            ErrorMessage="Se debe Colocar una Cantidad Uma"
                                            ValidationGroup="CirugiaUpdate"
                                            Display="Dynamic" />

                                    </div>

                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Total" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoTotalCirugia" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>



                            </tr>

                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="% Cirujano" runat="server" CssClass="label" Font-Size="9px" InputType="Number" />
                                    <telerik:RadNumericTextBox ID="TextPorcentajeCirujano" runat="server" Width="120px"
                                        onkeypress="CalCuloCirugia()" onkeyup="CalCuloCirugia()" MinValue="0" MaxValue="100"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                            ControlToValidate="TextPorcentajeCirujano"
                                            ErrorMessage="Se debe Colocar un Porcentaje"
                                            ValidationGroup="CirugiaUpdate"
                                            Display="Dynamic" />

                                    </div>
                                </td>

                                <td class="auto-style4">
                                    <asp:Label Text="Monto Cirujano" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoCirujano" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="% Co-Pago" ID="LabelValorCoPagoCirugia" runat="server" CssClass="label" Font-Size="11px" />
                                    <asp:TextBox ID="TextValorCoPagoCirugia" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="% anestesiólogo" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextPorcentajeAnestesiologo" runat="server" Width="120px"
                                        onkeypress="CalCuloCirugia()" onkeyup="CalCuloCirugia()" MinValue="0" MaxValue="100"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                            ControlToValidate="TextPorcentajeAnestesiologo"
                                            ErrorMessage="Se debe Colocar un Porcentaje"
                                            ValidationGroup="CirugiaUpdate"
                                            Display="Dynamic" />

                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto anestesiólogo" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoAnestesiologo" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Co-Pago" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoCoPagoCirugia" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="% Ayudante" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextPorcentajeAyudante" runat="server" Width="120px"
                                        onkeypress="CalCuloCirugia()" onkeyup="CalCuloCirugia()" MinValue="0" MaxValue="100"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="TextPorcentajeAyudante"
                                            ErrorMessage="Se debe Colocar un Porcentaje"
                                            ValidationGroup="CirugiaUpdate"
                                            Display="Dynamic" />


                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Ayudante" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoAyudante" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="% Instrumentista" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextPorcentajeInstrumentista" runat="server" Width="120px"
                                        onkeypress="CalCuloCirugia()" onkeyup="CalCuloCirugia()" MinValue="0" MaxValue="100"
                                        Culture="tr-TR">
                                        <ClientEvents OnValueChanging="CalCuloCirugia" />
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                            ControlToValidate="TextPorcentajeInstrumentista"
                                            ErrorMessage="Se debe Colocar un Porcentaje"
                                            ValidationGroup="CirugiaUpdate"
                                            Display="Dynamic" />

                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Instrumentista" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoInstrumentista" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto en Bs" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoBsCirugia" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                        <div class="buttonsPanel">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="BtnUploadInternacionCirugia" Text="GUARDAR" runat="server" ValidationGroup="CirugiaUpdate"
                                            OnClick="SaveUpdateCirugiaLB_Click">
                                        </telerik:RadButton>
                                    </td>
                                    <td></td>
                                    <td>
                                        <telerik:RadButton ID="CancelEditCirugiaCoPago" Text="Cancelar" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <div id="MensajeErrorCalculoCirugia" style="display: none;">
                            <asp:Label ID="LabelMensajeErrorCirugia" Text="mensaje" runat="server" HeaderStyle-Font-Bold="true" Font-Size="10px" CssClass="label" />
                        </div>
                    </div>
                </asp:Panel>




                <asp:Panel ID="EditInternacionPanel" runat="server"
                    CssClass="Default_Popup"
                    GroupingText="Editar Internacion">
                    <div>
                        <table>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Emergencia" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoEmergencia" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>

                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="TextMontoEmergencia"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Honorario Medico" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoHonorarioMedico" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>

                                    <div class="validators">

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                            ControlToValidate="TextMontoHonorarioMedico"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Total" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoTotalInternacion" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Farmacia" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoFarmacia" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>

                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="TextMontoFarmacia"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Laboratorio" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoLaboratorio" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="TextMontoLaboratorio"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="% Co-Pago" ID="LabelValorCoPagoInternacion" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextValorCoPagoInternacion" Text="" Enabled="false" runat="server" Width="125px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Estudios" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoEstudios" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>

                                    <div class="validators">

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="TextMontoEstudios"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Hospitalizacion" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoHospitalizacion" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="TextMontoHospitalizacion"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Co-Pago" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoCoPagoInternacion" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Otros" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoOtros" runat="server" Width="120px"
                                        onkeypress="CalCuloInternacion()" onkeyup="CalCuloInternacion()" MinValue="0"
                                        Culture="tr-TR">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="TextMontoOtros"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="InternacionUpdate"
                                            Display="Dynamic" />

                                    </div>
                                </td>
                                <td class="auto-style4"></td>
                                <td class="auto-style4"></td>
                            </tr>
                            <tr>
                                <td class="auto-style4"></td>
                                <td class="auto-style4"></td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto en Bs" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoBsInternacion" Text="" Enabled="false" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                        <div class="buttonsPanel">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="BtnUploadInternacion" Text="GUARDAR" runat="server"
                                            OnClick="SaveUpdateInternacionInternacionLB_Click" ValidationGroup="InternacionUpdate">
                                        </telerik:RadButton>
                                    </td>
                                    <td></td>
                                    <td>
                                        <telerik:RadButton ID="CancelEditInternacionCoPago" Text="Cancelar" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <div id="MensajeErrorCalculoInternacion" style="display: none;">
                            <asp:Label ID="LabelMensajeErrorInternacion" Text="mensaje" runat="server" HeaderStyle-Font-Bold="true" Font-Size="10px" CssClass="label" />
                        </div>
                    </div>

                </asp:Panel>


                <asp:Panel ID="EditEmergencia" runat="server"
                    CssClass="Default_Popup"
                    GroupingText="Editar Emergencia">
                    <div>
                        <table>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Emergencia" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoemergenciaE"
                                        RenderMode="Lightweight" runat="server" Width="120px"
                                        onkeypress="CalCuloEmergencia()" onkeyup="CalCuloEmergencia()"
                                        MinValue="0">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                                            ControlToValidate="TextMontoemergenciaE"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="EmergenciaUpdate"
                                            Display="Dynamic" />
                                    </div>

                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Honorario Medico" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoHonorarioE"
                                        onkeypress="CalCuloEmergencia()" onkeyup="CalCuloEmergencia()" runat="server" Width="120px" MinValue="0">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                            ControlToValidate="TextMontoHonorarioE"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="EmergenciaUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Total" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoTotalE" Enabled="false" runat="server" Width="120px" />

                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Farmacia" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoFarmaciaE" runat="server" Width="120px"
                                        onkeypress="CalCuloEmergencia()" onkeyup="CalCuloEmergencia()" MinValue="0">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                                            ControlToValidate="TextMontoFarmaciaE"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="EmergenciaUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Laboratorio" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoLaboratorioE" runat="server" Width="120px"
                                        onkeypress="CalCuloEmergencia()" onkeyup="CalCuloEmergencia()" MinValue="0">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                                            ControlToValidate="TextMontoLaboratorioE"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="EmergenciaUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label Text="% Co-Pago" ID="LabelTipoCopagoEm" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextValorCoPagoE" Enabled="false" runat="server" Width="125px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Estudios" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoEstudiosE" runat="server" Width="120px"
                                        onkeypress="CalCuloEmergencia()" onkeyup="CalCuloEmergencia()" MinValue="0">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server"
                                            ControlToValidate="TextMontoEstudiosE"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="EmergenciaUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4"></td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Co-Pago" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoCoPagoE" Enabled="false" runat="server" Width="120px"></asp:TextBox></td></tr><tr>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto Otros" runat="server" CssClass="label" Font-Size="9px" />
                                    <telerik:RadNumericTextBox ID="TextMontoOtrosE" runat="server" Width="120px"
                                        onkeypress="CalCuloEmergencia()" onkeyup="CalCuloEmergencia()" MinValue="0">
                                        <NumberFormat AllowRounding="False" KeepNotRoundedValue="True" DecimalSeparator="." GroupSeparator="" GroupSizes="3" DecimalDigits="2" />
                                    </telerik:RadNumericTextBox>
                                    <div class="validators">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                                            ControlToValidate="TextMontoOtrosE"
                                            ErrorMessage="Se debe Colocar un Monto"
                                            ValidationGroup="EmergenciaUpdate"
                                            Display="Dynamic" />
                                    </div>
                                </td>
                                <td class="auto-style4"></td>
                                <td class="auto-style4">
                                    <asp:Label Text="Monto en Bs" runat="server" CssClass="label" Font-Size="9px" />
                                    <asp:TextBox ID="TextMontoBsTotalE" Enabled="false" runat="server" Width="120px"></asp:TextBox></td></tr></table><br /><table>
                            <tr>
                                <td class="auto-style4">
                                    <span class="label">Subir Archivo para Emergencia</span> <RedSalud:FileUpload runat="server" ID="FileUpload" MaxFileInputCount="10" OnFilesLoaded="FileUpload_FilesLoaded" />

                                </td>

                                <td class="auto-style4"></td>
                            </tr>

                        </table>
                        <br />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="BtnUpdateEmergencia" Text="GUARDAR" runat="server" OnClick="SaveUpdateEmergenciaLB_Click"
                                        ValidationGroup="EmergenciaUpdate">
                                    </telerik:RadButton>
                                </td>
                                <td></td>
                                <td>
                                    <telerik:RadButton ID="BtnCancelUpdateEmergencia" Text="Cancelar" runat="server" />
                                </td>
                            </tr>
                        </table>

                        <br />
                        <br />
                        <div id="MEnsajeDErrorEmergencias" style="display: none;">
                            <asp:Label ID="LabelMensajeErrorEmergencia" Text="mensaje" runat="server" HeaderStyle-Font-Bold="true" Font-Size="10px" CssClass="label" />
                        </div>
                    </div>

                </asp:Panel>

                <asp:ObjectDataSource ID="EspecialidadComplementarioODS" runat="server"
                    TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedEspecialidadPrestacionesBLL"
                    OldValuesParameterFormatString="{0}"
                    SelectMethod="GetAllEspecialidadPrestacionesxCliente"
                    OnSelected="EspecialidadComplementarioODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" DbType="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>


                <asp:ObjectDataSource ID="CiudadExComplementarioODS" runat="server"
                    TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="getCiudadList"
                    OnSelected="CiudadExComplementarioODS_Selected"></asp:ObjectDataSource>

                <asp:ObjectDataSource ID="PrestacionesExComplementarioODS" runat="server"
                    TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedClientePrestacionesBLL"
                    OldValuesParameterFormatString="{0}"
                    SelectMethod="GetAllClientePrestacionesXClienteId"
                    OnSelected="PrestacionesExComplementarioODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" DbType="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:ObjectDataSource ID="CirugiasNotSavedODS" runat="server"
                    TypeName="Artexacta.App.RedClientePrestaciones.BLL.RedCirugiasPrestacionesBLL"
                    SelectMethod="GetCirugiasPrestacionesNotSaved">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ClienteIdHF" Name="ClienteId" PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="NotSaved" Type="Boolean" DefaultValue="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>



                <RedSalud:FileManager runat="server" ID="FileManager" />

                <script type="text/javascript">
                    $(document).ready(function () {

                        createPopup("#<%=NewRecetaPanel.ClientID %>", ".NewReceta", "#<%=CancelNewRecetaLB.ClientID %>");
                        createPopup("#<%=RecetaPanel.ClientID %>", ".NewReceta2", "#<%=CancelRecetaLB.ClientID %>");
                        createPopup("#<%=DerivacionPanel.ClientID %>", ".NewDerivacion", "#<%=CancelDerivacionLB.ClientID %>");
                        createPopup("#<%=InternacionPanel.ClientID %>", ".NewInternacion", "#<%=CancelInternacionLB.ClientID %>",
                            function () {
                                $("#IsCirugia").prop("checked", false);
                                $('#<%= InternacionPanel.ClientID%> legend').html('NUEVA INTERNACIÓN');
                                $find('<%=CodigoArancelarioRadComboBox.ClientID%>').clearSelection();
                                /////////////////////////////////////////////////////////////
                                $("#<%=ObservacionCirugiaTxt.ClientID %>").attr('value', '');
                                $("#<%=ObservacionInternacionTxt.ClientID %>").attr('value', '');
                                $find('<%=CiudadCirugiaComboBox.ClientID%>').clearSelection();
                                $find('<%=CiudadInternacionComboBox.ClientID%>').clearSelection();
                                $find('<%=ProveedorInternacionDDL.ClientID%>').clearSelection();
                                $find('<%=ProveedorCirugiaDLL.ClientID%>').clearSelection();
                                $find('<%=CirujanoRadComboBox.ClientID%>').clearSelection();
                                $find('<%=EspecialidadCirujanoComboBox.ClientID%>').clearSelection();
                                $find('<%=EnfermedadesInternacionComboBox.ClientID%>').clearSelection();
                                $('#CodigoArancelario').hide();
                                $('#CasoInternacionV').show(); $("#<%=EsCirugiaHF.ClientID %>").attr('value', '0');

                            });
                        createPopup("#<%=InternacionPanel.ClientID %>", ".NewInternacion", "#<%=CancelCirugiaLB.ClientID %>",
                        function () {
                            $("#IsCirugia").prop("checked", false);
                            $('#<%= InternacionPanel.ClientID%> legend').html('NUEVA INTERNACIÓN');
                            $find('<%=CodigoArancelarioRadComboBox.ClientID%>').clearSelection();
                            /////////////////////////////////////////////////////////////                                
                            $("#<%=ObservacionCirugiaTxt.ClientID %>").attr('value', '');
                            $find('<%=CiudadCirugiaComboBox.ClientID%>').clearSelection();
                            $find('<%=CiudadInternacionComboBox.ClientID%>').clearSelection();
                            $find('<%=ProveedorInternacionDDL.ClientID%>').clearSelection();
                            $find('<%=ProveedorCirugiaDLL.ClientID%>').clearSelection();
                            $find('<%=CirujanoRadComboBox.ClientID%>').clearSelection();
                            $find('<%=EspecialidadCirujanoComboBox.ClientID%>').clearSelection();
                            $find('<%=EnfermedadesInternacionComboBox.ClientID%>').clearSelection();
                            $('#CodigoArancelario').hide();
                            $('#CasoInternacionV').show(); $("#<%=EsCirugiaHF.ClientID %>").attr('value', '0');

                        });
                        ///////////////////////////////////////////////////////////////
                        createPopup("#<%=EditCirugiaPanel.ClientID %>", ".EditCirugia", "#<%=CancelEditCirugiaCoPago.ClientID %>",
                          function () {
                              $('#<%= EditCirugiaPanel.ClientID%> legend').html('Editar Cirugia');
                              $find('<%=TextValorUma.ClientID%>').clear();
                              $find('<%=TextMontoTotalCirugia.ClientID%>').clear();
                              $find('<%=TextPorcentajeCirujano.ClientID%>').clear();
                              $find('<%=TextMontoCirujano.ClientID%>').clear();
                              $find('<%=TextValorCoPagoCirugia.ClientID%>').clear();
                              $find('<%=TextPorcentajeAnestesiologo.ClientID%>').clear();
                              $find('<%=TextMontoAnestesiologo.ClientID%>').clear();
                              $find('<%=TextMontoCoPagoCirugia.ClientID%>').clear();
                              $find('<%=TextPorcentajeInstrumentista.ClientID%>').clear();
                              $find('<%=TextMontoInstrumentista.ClientID%>').clear();
                              $find('<%=TextPorcentajeAyudante.ClientID%>').clear();
                              $find('<%=TextMontoAyudante.ClientID%>').clear();
                              $find('<%=TextMontoBsCirugia.ClientID%>').clear();
                          });

                        createPopup("#<%=EditInternacionPanel.ClientID %>", ".EditInternacion", "#<%=CancelEditInternacionCoPago.ClientID %>",
                         function () {
                             $('#<%= EditInternacionPanel.ClientID%> legend').html('Editar Internacion');
                             $find('<%=TextMontoFarmacia.ClientID%>').clear();
                             $find('<%=TextMontoHonorarioMedico.ClientID%>').clear();
                             $find('<%=TextMontoTotalInternacion.ClientID%>').clear();
                             $find('<%=TextMontoEmergencia.ClientID%>').clear();
                             $find('<%=TextMontoLaboratorio.ClientID%>').clear();
                             $find('<%=TextValorCoPagoInternacion.ClientID%>').clear();
                             $find('<%=TextMontoEstudios.ClientID%>').clear();
                             $find('<%=TextMontoHospitalizacion.ClientID%>').clear();
                             $find('<%=TextMontoCoPagoInternacion.ClientID%>').clear();
                             $find('<%=TextMontoOtros.ClientID%>').clear();
                             $find('<%=TextMontoBsInternacion.ClientID%>').clear();
                         });

                        ///////////////////////////////////////////////////////////////
                        createPopup("#<%=PrestacionPanel.ClientID %>", ".NewOdontologia", "#<%=CancelPrestacion.ClientID %>", null, function () {
                            $find('<%=PrestacionRCB.ClientID%>').clearSelection();
                            $("#<%=PiezaOdontologiaMultiple.ClientID%> input[type='hidden']").val('');
                            $("#<%=PiezaOdontologiaMultiple.ClientID%> input").prop('checked', false);
                            $("#<%=PiezaOdontologiaSimple.ClientID%> input[type='hidden']").val('');
                            $("#<%=PiezaOdontologiaSimple.ClientID%> input").prop('checked', false);
                            $("#<%=ObservacionesPrestacionTextBox.ClientID%>").val('');
                        });
                        <% if (PacienteFV.FindControl("GeneroAsegurado") != null && CasoFV.FindControl("Panel7") != null)
                    {  %>
                        var genero = ($('#<%= (PacienteFV.FindControl("GeneroAsegurado") as HiddenField).ClientID%>').val() == 'True');
                        if (genero) {
                            $('#<%= CasoFV.FindControl("Panel7").ClientID%>').hide();
                        } else {
                            $('#<%= CasoFV.FindControl("Panel7").ClientID%>').show();
                        }
                        <%}%>
                    });


                    createPopup("#<%=EditEmergencia.ClientID %>", ".EditEmergencia", "#<%=BtnCancelUpdateEmergencia.ClientID %>",
                         function () {
                             $('#<%= BtnCancelUpdateEmergencia.ClientID%> legend').html('Editar Cirugia');
                         });
                    ////////////////////////////////////////////////////////

                    function OpenPopupReceta() {
                        showPopup("#<%=RecetaPanel.ClientID %>");
                        $("#<%=RecetaPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=RecetaPanel.ClientID %> legend").text('Modificar Receta');
                    }
                    $("#<%=CancelRecetaLB.ClientID %>").click(function () {
                        $("#<%=RecetaIdHF.ClientID %>").attr('value', '0');
                        $("#<%=MedicamentoTxt.ClientID %>").attr('value', '');
                        $("#<%=InstruccionesTxt.ClientID %>").attr('value', '');
                        if ($("#<%=SaveNewRecetaLB.ClientID %>").length)
                            $("#<%=MessageRecetaLabel.ClientID %>").text('');
                        $("#<%=RecetaPanel.ClientID %> legend").text('Nueva Receta');
                        $find('<%= TipoMedicamentoDDL.ClientID %>').clearSelection();
                    });



                    function OpenPopupDerivacion() {
                        showPopup("#<%=DerivacionPanel.ClientID %>");
                        $("#<%=DerivacionPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=DerivacionPanel.ClientID %> legend").text('Modificar Derivacion a especialista');
                    }
                    $("#<%=CancelDerivacionLB.ClientID %>").click(function () {
                        $("#<%=DerivacionIdHF.ClientID %>").attr('value', '0');
                        $("#<%=ObservacionDerivacionTxt.ClientID %>").attr('value', '');
                        if ($("#<%=DerivacionSaveLB.ClientID %>").length)
                            $("#<%=MessageDerivacionLabel.ClientID %>").text('');
                        $("#<%=DerivacionPanel.ClientID %> legend").text('Nueva Derivación a especialista');
                        $find('<%= CiudadDerivacionComboBox.ClientID %>').clearSelection();
                        $find('<%= EspecialidadDerivacionComboBox.ClientID %>').clearSelection();
                        $find('<%= ProveedorDerivacionDDL.ClientID %>').clearSelection();
                    });

                    function OpenPopupInternacion() {
                        showPopup("#<%=InternacionPanel.ClientID %>");
                        $("#<%=InternacionPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=InternacionPanel.ClientID %> legend").text('Modificar Internación');
                        ////////////////////////////////
                        $('#CasoInternacionV').show();
                        $("#<%=EsCirugiaHF.ClientID %>").attr('value', '0');


                    }
                    //////////////////////////////EditInternacionPanel
                    function OpenPopupEditInternacionPanel() {
                        showPopup("#<%=EditInternacionPanel.ClientID %>");
                        $("#<%=EditInternacionPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=EditInternacionPanel.ClientID %> legend").text('Detalle Internación');
                        ////////////////////////////////
                    }


                    function OpenPopupEditEmergencia() {
                        showPopup("#<%=EditEmergencia.ClientID %>");
                        $("#<%=EditEmergencia.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=EditEmergencia.ClientID %> legend").text('Modificar Emergencia');
                    }




                    function OpenPopupEditCirugiaPanel() {
                        showPopup("#<%=EditCirugiaPanel.ClientID %>");
                        $("#<%=EditCirugiaPanel.ClientID %>" + ", .popup_Mask").fadeIn(500);
                        $("#<%=EditCirugiaPanel.ClientID %> legend").text('Detalle Cirugia');

                        ////////////////////////////////
                    }
                    //
                    $("#<%=CancelInternacionLB.ClientID %>").click(function () {
                        $("#<%=InternacionIdHF.ClientID %>").attr('value', '0');
                        $("#<%=ObservacionInternacionTxt.ClientID %>").attr('value', '');
                        $("#<%=ObservacionCirugiaTxt.ClientID %>").attr('value', '');
                        if ($("#<%=InternacionSaveLB.ClientID %>").length)
                            $("#<%=MessageInternacionLabel.ClientID %>").text('');
                        $("#<%=InternacionPanel.ClientID %> legend").text('Nueva Internación');
                        $find('<%= ProveedorInternacionDDL.ClientID %>').clearSelection();
                        ///////////////////////////////////////
                        $('#CodigoArancelario').hide();
                        $('#CasoInternacionV').show();
                        $("#<%=EsCirugiaHF.ClientID %>").attr('value', '0');


                    });
                    $("#<%=CancelCirugiaLB.ClientID %>").click(function () {
                        $("#<%=InternacionIdHF.ClientID %>").attr('value', '0');
                        $("#<%=ObservacionCirugiaTxt.ClientID %>").attr('value', '');
                        if ($("#<%=InternacionCirugiaSaveLB.ClientID %>").length)
                            $("#<%=MessageInternacionLabel.ClientID %>").text('');
                        $("#<%=InternacionPanel.ClientID %> legend").text('Nueva Cirugia');
                        $find('<%= ProveedorCirugiaDLL.ClientID %>').clearSelection();
                        $find('<%= CiudadCirugiaComboBox.ClientID %>').clearSelection();
                        $find('<%= CirujanoRadComboBox.ClientID %>').clearSelection();
                        $find('<%= CiudadCirugiaComboBox.ClientID %>').clearSelection();

                        ///////////////////////////////////////
                        $('#CodigoArancelario').hide();
                        $('#CasoInternacionV').show();
                        $("#<%=EsCirugiaHF.ClientID %>").attr('value', '0');


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
                    function NewReceta_Validate() {
                        var IsLINAME = true;
                        try {
                            if ($('input[name=RecetaType]:checked').val() != undefined)
                                IsLINAME = $('input[name=RecetaType]:checked').val() == '1';
                        } catch (q) {
                            console.error(q);
                        }
                        var valid = true;
                        if (IsLINAME) {
                            var newMedicamento = $('#<%=NewMedicamentoRadComboBox.ClientID%>').val();
                            if (newMedicamento == null || newMedicamento == undefined || newMedicamento == '') {
                                $('#NewMedicamentoError').show();
                                valid = false;
                            } else {
                                $('#NewMedicamentoError').hide();
                            }
                            var NewTipoMedicamento = $('#<%=NewTipoMedicamentoRadComboBox.ClientID%>').val();
                            if (NewTipoMedicamento == null || NewTipoMedicamento == undefined || NewTipoMedicamento == '') {
                                $('#NewTipoMedicamentoError').show();
                                valid = false;
                            } else {
                                $('#NewTipoMedicamentoError').hide();
                            }

                            var NewTipoConcentracion = $('#<%=NewTipoConcentracionRadComboBox.ClientID%>').val();
                            if (NewTipoConcentracion == null || NewTipoConcentracion == undefined || NewTipoConcentracion == '') {
                                $('#NewTipoConcentracionError').show();
                                valid = false;
                            } else {
                                $('#NewTipoConcentracionError').hide();
                            }
                        } else {
                            var TipoMedicamento = $('#<%=TipoMedicamento1DDL.ClientID%>').val();
                            if (TipoMedicamento == null || TipoMedicamento == undefined || TipoMedicamento == '') {
                                $('#TipoMedicamentoError').show();
                                valid = false;
                            } else {
                                $('#TipoMedicamentoError').hide();
                            }
                            var Medicamento = $('#<%=Medicamento1Txt.ClientID%>').val();
                            if (Medicamento == null || Medicamento == undefined || Medicamento == '') {
                                $('#MedicamentoError').show();
                                valid = false;
                            } else {
                                $('#MedicamentoError').hide();
                            }
                        }
                        return valid;
                    }

                    function TipoEstudioCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $('#<%= TipoEstudioDDL.ClientID %> input[type=hidden]').val();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }

                    function ProveedorCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= ProveedorExComplementarioDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                    function ProveedorDerivacionDDLCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= ProveedorDerivacionDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                    function ProveedorInternacionDDLCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= ProveedorInternacionDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                    function CiudadInternacionInternacionDDLCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= CiudadInternacionComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }


                    function EnfermedadesComboBox_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= EnfermedadesInternacionComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }

                    function CiudadInternacionInternacionDDLCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= CiudadInternacionComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }

                    function CiudadInternacionCirugia_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= CiudadCirugiaComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }

                    function EspecialidadCirujano_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= EspecialidadCirujanoComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }


                    function MedicoCirujano_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= CirujanoRadComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                    function ProveedorCirugiaDLL_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%=  ProveedorCirugiaDLL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }

                    function CodigoArancelario_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%=  CodigoArancelarioRadComboBox.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                </script>

                <script type="text/javascript">

                    $(document).ready(function () {



                        var divExpandCollapse = '<div class="ExpandCollapseIcon"></div>';
                        $(".ExpandCollapse fieldset legend").append(divExpandCollapse);

                        $(".ExpandCollapse fieldset legend .ExpandCollapseIcon").click(function () {
                            //$(this).parent().children("div").toggle("slow");

                            if ($(this).is('.Expand')) {//colapsar
                                $(this).removeClass('Expand')
                                $(this).parent().parent().children("div").slideToggle('slow');
                            }
                            else {
                                $(this).addClass('Expand')//expandir
                                $(this).parent().parent().children("div").slideToggle('slow');
                            }

                        });
                        $('input.EditableWhenEmpty').each(function () {
                            var val = $(this).val().trim();
                            if (val == null || val == undefined) {
                                val = '';
                            }
                            if (val != '') {
                                $(this).prop();
                            }
                        });
                    });
                    //$(".ExpandCollapse fieldset legend").click(function () {
                    //    var divExpandCollapse = '<div class="ExpandCollapseIcon"></div>';
                    //    if ($(this).is('.Expand')) {//colapsar
                    //        $(this).removeClass('Expand')
                    //        $(this).parent().children("div").slideToggle('slow');
                    //    }
                    //    else {
                    //        $(this).addClass('Expand')//expandir
                    //        $(this).parent().children("div").slideToggle('slow');
                    //    }
                    //});
                </script>
                <asp:HiddenField ID="AseguradoIdHF" runat="server" />
                <asp:HiddenField ID="CasoIdHF" runat="server" />
                <asp:HiddenField ID="motivoconsultaid" runat="server" />
                <asp:HiddenField ID="EmergenciaIdHF" runat="server" />
                <asp:HiddenField ID="MotivoConsultaIdHF" runat="server" />
                <asp:HiddenField ID="MotivoConsultaIdTHF" runat="server" />
                <asp:HiddenField ID="GeneroHF" runat="server" />
                <asp:HiddenField ID="CiudadHF" runat="server" />
                <asp:HiddenField ID="RedMedicaIdHF" runat="server" />
                <asp:HiddenField ID="CitaIdHF" runat="server" />


                <asp:HiddenField ID="ClienteIdHF" runat="server" />
                <asp:HiddenField ID="VerValorCoPagoHF" runat="server" />
                <asp:HiddenField ID="VerMontoTopeHF" runat="server" />
                <asp:HiddenField ID="EsCirugiaHF" runat="server" />

                <asp:HiddenField ID="TextMontoBsCirugiaHF" runat="server" />

                <asp:HiddenField ID="TextValorUmaHF" runat="server" />
                <asp:HiddenField ID="TextCantidadUmaHF" runat="server" />
                <asp:HiddenField ID="TextPorcentajeCirujanoHF" runat="server" />
                <asp:HiddenField ID="TextMontoCirujanoHF" runat="server" />
                <asp:HiddenField ID="TextPorcentajeAnestesiologoHF" runat="server" />
                <asp:HiddenField ID="TextMontoAnestesiologoHF" runat="server" />
                <asp:HiddenField ID="TextPorcentajeAyudanteHF" runat="server" />
                <asp:HiddenField ID="TextMontoAyudanteHF" runat="server" />
                <asp:HiddenField ID="TextPorcentajeInstrumentistaHF" runat="server" />
                <asp:HiddenField ID="TextMontoInstrumentistaHF" runat="server" />
                <asp:HiddenField ID="TextMontoTotalCirugiaHF" runat="server" />



                <asp:HiddenField ID="TextMontoEmergenciaHF" runat="server" />
                <asp:HiddenField ID="TextMontoHonorarioMedicoHF" runat="server" />
                <asp:HiddenField ID="TextMontoTotalInternacionHF" runat="server" />

                <asp:HiddenField ID="TextMontoFarmaciaHF" runat="server" />
                <asp:HiddenField ID="TextMontoLaboratorioHF" runat="server" />
                <asp:HiddenField ID="TextValorCoPagoInternacionHF" runat="server" />
                <asp:HiddenField ID="TextMontoEstudiosHF" runat="server" />
                <asp:HiddenField ID="TextMontoHospitalizacionHF" runat="server" />
                <asp:HiddenField ID="TextMontoCoPagoInternacionHF" runat="server" />
                <asp:HiddenField ID="TextMontoOtrosHF" runat="server" />
                <asp:HiddenField ID="TextMontoBsInternacionHF" runat="server" />


                <asp:HiddenField ID="detIdEmergenciaHF" runat="server" Value="0" />

                <asp:HiddenField ID="TextMontoFarmaciaEHF" runat="server" />
                <asp:HiddenField ID="TextMontoLaboratorioEHF" runat="server" />
                <asp:HiddenField ID="TextMontoEstudiosEHF" runat="server" />
                <asp:HiddenField ID="TextMontoHonorarioMedicoEHF" runat="server" />
                <asp:HiddenField ID="TextMontoEmergenciaEHF" runat="server" />
                <asp:HiddenField ID="TextMontoTotalEmergenciaHF" runat="server" />
                <asp:HiddenField ID="TextMontoOtrosEHF" runat="server" />

                <asp:HiddenField ID="TextMontoBsEmergenciaEHF" runat="server" />

                <asp:HiddenField ID="ExportIDHF" runat="server" />
                <asp:HiddenField ID="RadGridExported" runat="server" />
                <asp:HiddenField ID="MedicoNameHF" runat="server" />
                <asp:HiddenField ID="EspecialidadHF" runat="server" />



                <asp:HiddenField ID="FechaEstadoRecetaHF" runat="server" />
                <asp:HiddenField ID="FechaEstadoExamenesHF" runat="server" />
                <asp:HiddenField ID="FechaEstadoEspecialistaHF" runat="server" />

                <asp:HiddenField ID="ReconsultaHF" runat="server" />
                <asp:HiddenField ID="ModeHF" runat="server" />
                <asp:HiddenField ID="ObservacionesHF" runat="server" />

                <asp:HiddenField ID="CasoDesgravamenHF" runat="server" Value="false" />
                <asp:HiddenField ID="CitaDesgravamenIdHF" runat="server" Value="0" />


                <asp:ObjectDataSource ID="EnfermedadesCronicasODS" runat="server"
                    TypeName="Artexacta.App.EnfermedadCronica.BLL.EnfermedadCronicaBLL"
                    OldValuesParameterFormatString="{0}"
                    SelectMethod="GetEnfermedadCronicaByAseguradoId"
                    OnSelected="EnfermedadesCronicasODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="AseguradoIdHF" Name="AseguradoId" PropertyName="Value" DbType="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <script type="text/javascript">
                    //calculo de EMergencia
                    function CalCuloEmergencia() {
                        var TxtMontoEmergenciaE = document.getElementById('<%= TextMontoemergenciaE.ClientID %>');
                        var TxtMontoHonorarioE = document.getElementById('<%= TextMontoHonorarioE.ClientID %>');
                        var TxtMontoFarmaciaE = document.getElementById('<%=TextMontoFarmaciaE.ClientID %>');
                        var TxtMontoLaboratorioE = document.getElementById('<%=TextMontoLaboratorioE.ClientID%>');
                        var TxtValorCoPagoEmergencia = document.getElementById('<%=TextValorCoPagoE.ClientID%>');
                        var TxtMontoEstudiosE = document.getElementById('<%=TextMontoEstudiosE.ClientID%>');
                        var TxtMontoCoPagoE = document.getElementById('<%=TextMontoCoPagoE.ClientID%>');
                        var TxtMontoOtrosE = document.getElementById('<%=TextMontoOtrosE.ClientID%>');
                        var TxtMontoTotalE = document.getElementById('<%=TextMontoTotalE.ClientID%>');
                        var TxtMontoBsEmergencia = document.getElementById('<%=TextMontoBsTotalE.ClientID%>');
                        document.getElementById('<%= TextMontoFarmaciaEHF.ClientID %>').value = TxtMontoFarmaciaE.value;
                        document.getElementById('<%= TextMontoLaboratorioEHF.ClientID %>').value = TxtMontoLaboratorioE.value;
                        document.getElementById('<%= TextMontoEstudiosEHF.ClientID %>').value = TxtMontoEstudiosE.value;
                        document.getElementById('<%= TextMontoHonorarioMedicoEHF.ClientID %>').value = TxtMontoHonorarioE.value;
                        document.getElementById('<%= TextMontoEmergenciaEHF.ClientID %>').value = TxtMontoEmergenciaE.value;
                        document.getElementById('<%= TextMontoOtrosEHF.ClientID %>').value = TxtMontoOtrosE.value;
                        if (TxtMontoEmergenciaE.value >= 0 & TxtMontoHonorarioE.value >= 0 & TxtMontoFarmaciaE.value >= 0 & TxtMontoLaboratorioE.value >= 0 & TxtMontoEstudiosE.value >= 0 & TxtMontoOtrosE.value >= 0) {

                            TxtMontoTotalE.value = (parseFloat(TxtMontoEmergenciaE.value) + parseFloat(TxtMontoHonorarioE.value) + parseFloat(TxtMontoFarmaciaE.value) + parseFloat(TxtMontoLaboratorioE.value) + parseFloat(TxtMontoEstudiosE.value) + parseFloat(TxtMontoOtrosE.value)).toFixed(2);

                            if (TxtMontoTotalE.value.length > 0) {
                                var MontoTope = document.getElementById('<%=VerMontoTopeHF.ClientID%>');
                                if (parseFloat(MontoTope.value) < parseFloat(TxtMontoTotalE.value)) {

                                    $("#MEnsajeDErrorEmergencias").show();
                                    document.getElementById('<%= LabelMensajeErrorEmergencia.ClientID %>').style.color = "#F7F9F9";
                                    $("#MEnsajeDErrorEmergencias").css("color", "#01DF01")
                                    $("#MEnsajeDErrorEmergencias").css({ background: "#FF3333", height: "18px" })
                                    document.getElementById('<%= LabelMensajeErrorEmergencia.ClientID %>').textContent = "El Monto Total de la Emergencia Pasa al Monto Tope=" + parseFloat(MontoTope.value).toFixed(2) + " Bs";
                                    document.getElementById('<%= LabelMensajeErrorEmergencia.ClientID %>').className = "auto-style4";
                                }
                                else {
                                    $("#MEnsajeDErrorEmergencias").hide();
                                }
                                if (TxtValorCoPagoEmergencia.value.length > 0) {
                                    var cadena = document.getElementById('<%=LabelTipoCopagoEm.ClientID%>').textContent;
                                    if (cadena.indexOf('%') != -1) {
                                        TxtMontoCoPagoE.value = (parseFloat(TxtMontoTotalE.value) * (parseFloat(TxtValorCoPagoEmergencia.value) / 100)).toFixed(2);
                                        TxtMontoBsEmergencia.value = (parseFloat(TxtMontoCoPagoE.value)).toFixed(2);

                                    }

                                    else {
                                        TxtMontoBsEmergencia.value = (parseFloat(TxtMontoCoPagoE.value)).toFixed(2);
                                        TxtMontoCoPagoE.value = (parseFloat(TxtMontoCoPagoE.value)).toFixed(2);

                                    }
                                    document.getElementById('<%= TextMontoTotalEmergenciaHF.ClientID %>').value = TxtMontoTotalE.value;
                                    document.getElementById('<%= TextMontoBsEmergenciaEHF.ClientID %>').value = TxtMontoTotalE.value;

                                }
                            }
                        }
                    }

                    //Calculo de Internacion
                    function CalCuloInternacion() {

                        var TxtMontoEmergencia = document.getElementById('<%= TextMontoEmergencia.ClientID %>');
                        var TxtMontoHonorarioMedico = document.getElementById('<%= TextMontoHonorarioMedico.ClientID %>');
                        var TxtMontoTotalInternacion = document.getElementById('<%= TextMontoTotalInternacion.ClientID %>');
                        var TxtMontoFarmacia = document.getElementById('<%=TextMontoFarmacia.ClientID %>');
                        var TxtMontoLaboratorio = document.getElementById('<%=TextMontoLaboratorio.ClientID%>');
                        var TxtValorCoPagoInternacion = document.getElementById('<%=TextValorCoPagoInternacion.ClientID%>');
                        var TxtMontoEstudios = document.getElementById('<%=TextMontoEstudios.ClientID%>');
                        var TxtMontoHospitalizacion = document.getElementById('<%=TextMontoHospitalizacion.ClientID%>');
                        var TxtMontoCoPagoInternacion = document.getElementById('<%=TextMontoCoPagoInternacion.ClientID%>');
                        var TxtMontoOtros = document.getElementById('<%=TextMontoOtros.ClientID%>');
                        var TxtMontoBsInternacion = document.getElementById('<%=TextMontoBsInternacion.ClientID%>');

                        document.getElementById('<%= TextMontoEmergenciaHF.ClientID %>').value = TxtMontoEmergencia.value;
                        document.getElementById('<%= TextMontoHonorarioMedicoHF.ClientID %>').value = TxtMontoHonorarioMedico.value;
                        document.getElementById('<%= TextMontoFarmaciaHF.ClientID %>').value = TxtMontoFarmacia.value;
                        document.getElementById('<%= TextMontoEstudiosHF.ClientID %>').value = TxtMontoEstudios.value;
                        document.getElementById('<%= TextMontoLaboratorioHF.ClientID %>').value = TxtMontoLaboratorio.value;
                        document.getElementById('<%= TextMontoHospitalizacionHF.ClientID %>').value = TxtMontoHospitalizacion.value;
                        document.getElementById('<%= TextMontoOtrosHF.ClientID %>').value = TxtMontoOtros.value;

                        var MontoTope = document.getElementById('<%=VerMontoTopeHF.ClientID%>');

                        if (TxtMontoEmergencia.value >= 0 & TxtMontoHonorarioMedico.value >= 0 & TxtMontoFarmacia.value >= 0 & TxtMontoEstudios.value >= 0 & TxtMontoLaboratorio.value >= 0 & TxtMontoHospitalizacion.value >= 0 & TxtMontoOtros.value >= 0) {

                            TxtMontoTotalInternacion.value = (parseFloat(TxtMontoEmergencia.value) + parseFloat(TxtMontoHonorarioMedico.value) + parseFloat(TxtMontoFarmacia.value) + parseFloat(TxtMontoLaboratorio.value) + parseFloat(TxtMontoEstudios.value) + parseFloat(TxtMontoHospitalizacion.value) + parseFloat(TxtMontoOtros.value)).toFixed(2);
                            document.getElementById('<%= TextMontoTotalInternacionHF.ClientID %>').value = TxtMontoTotalInternacion.value;

                            if (parseFloat(MontoTope.value) < parseFloat(TxtMontoTotalInternacion.value)) {

                                $("#MensajeErrorCalculoInternacion").show();
                                document.getElementById('<%= LabelMensajeErrorInternacion.ClientID %>').style.color = "#F7F9F9";
                                $("#MensajeErrorCalculoInternacion").css("color", "#01DF01")
                                $("#MensajeErrorCalculoInternacion").css({ background: "#FF3333", height: "18px" })
                                document.getElementById('<%= LabelMensajeErrorInternacion.ClientID %>').textContent = "El Monto Total de la Internacion Pasa al Monto Tope=" + (parseFloat(MontoTope.value)).toFixed(2) + " Bs";
                                document.getElementById('<%= LabelMensajeErrorInternacion.ClientID %>').className = "auto-style4";


                            }
                            else {
                                $("#MensajeErrorCalculoInternacion").hide();
                            }
                            if (TxtMontoTotalInternacion.value.length > 0) {

                                if (TxtValorCoPagoInternacion.value.length > 0) {
                                    var cadena = document.getElementById('<%=LabelValorCoPagoInternacion.ClientID%>').textContent;
                                    if (cadena.indexOf('%') != -1) {
                                        TxtMontoCoPagoInternacion.value = (parseFloat(TxtMontoTotalInternacion.value) * (parseFloat(TxtValorCoPagoInternacion.value) / 100)).toFixed(2);
                                        TxtMontoBsInternacion.value = (parseFloat(TxtMontoCoPagoInternacion.value)).toFixed(2);
                                        document.getElementById('<%= TextMontoBsInternacionHF.ClientID %>').value = TxtMontoCoPagoInternacion.value;
                                    }
                                    else {
                                        TxtMontoCoPagoInternacion.value = (parseFloat(TxtValorCoPagoInternacion.value)).toFixed(2);
                                        TxtMontoBsInternacion.value = (parseFloat(TxtValorCoPagoInternacion.value)).toFixed(2);
                                        document.getElementById('<%= TextMontoBsInternacionHF.ClientID %>').value = parseFloat(TxtMontoTotalInternacion.value).toFixed(2);;

                                    }
                                }
                            }
                        }
                    }
                    //Calculo de Cirugia

                    function CalCuloCirugia() {
                        if (event.keyCode == 188)
                            //si es un punto 
                        {

                            event.keyCode = 190; //cambiar por comma          
                        }
                        var TxtValorUma = document.getElementById('<%= TextValorUma.ClientID %>');
                        var TxtValorCantidadUma = document.getElementById('<%= TextCantidadUma.ClientID %>');
                        var TxtMontoTotalCirugia = document.getElementById('<%= TextMontoTotalCirugia.ClientID %>');
                        var TxtPorcentajeCirujano = document.getElementById('<%=TextPorcentajeCirujano.ClientID %>');
                        var TxtMontoCirujano = document.getElementById('<%=TextMontoCirujano.ClientID%>');
                        var TxtValorCoPagoCirugia = document.getElementById('<%=TextValorCoPagoCirugia.ClientID%>');
                        var TxtPorcentajeAnestesiologo = document.getElementById('<%=TextPorcentajeAnestesiologo.ClientID%>');
                        var TxtMontoAnestesiologo = document.getElementById('<%=TextMontoAnestesiologo.ClientID%>');
                        var TxtMontoCoPagoCirugia = document.getElementById('<%=TextMontoCoPagoCirugia.ClientID%>');
                        var TxtPorcentajeAyudante = document.getElementById('<%=TextPorcentajeAyudante.ClientID%>');
                        var TxtMontoAyudante = document.getElementById('<%=TextMontoAyudante.ClientID%>');
                        var TxtPorcentajeInstrumentista = document.getElementById('<%=TextPorcentajeInstrumentista.ClientID%>');
                        var TxtMontoInstrumentista = document.getElementById('<%=TextMontoInstrumentista.ClientID%>');
                        var TxtMontoBsCirugia = document.getElementById('<%=TextMontoBsCirugia.ClientID%>');

                        document.getElementById('<%= TextValorUmaHF.ClientID %>').value = TxtValorUma.value;
                        document.getElementById('<%= TextCantidadUmaHF.ClientID %>').value = TxtValorCantidadUma.value;
                        document.getElementById('<%= TextPorcentajeCirujanoHF.ClientID %>').value = TxtPorcentajeCirujano.value;
                        document.getElementById('<%= TextPorcentajeAnestesiologoHF.ClientID %>').value = TxtPorcentajeAnestesiologo.value;
                        document.getElementById('<%= TextPorcentajeAyudanteHF.ClientID %>').value = TxtPorcentajeAyudante.value;
                        document.getElementById('<%= TextPorcentajeInstrumentistaHF.ClientID %>').value = TxtPorcentajeInstrumentista.value;


                        if (TxtValorUma.value.length > 0 || TxtValorCantidadUma.value.length > 0) {
                            var ValorUma = parseFloat(TxtValorUma.value);
                            var ValorCantidadUma = parseFloat(TxtValorCantidadUma.value);
                            var TotalUma = ValorUma * ValorCantidadUma;
                            if (TotalUma > 0) {
                                if (TxtPorcentajeCirujano.value.length > 0 & (TxtPorcentajeCirujano.value <= 100 & TxtPorcentajeCirujano.value >= 0)) {
                                    TxtMontoCirujano.value = (TotalUma * (parseFloat(TxtPorcentajeCirujano.value) / 100)).toFixed(2);
                                    document.getElementById('<%= TextMontoCirujanoHF.ClientID %>').value = TxtMontoCirujano.value;
                                }
                                else {
                                    if (TxtPorcentajeCirujano.value > 100) {
                                        TxtPorcentajeCirujano.value = 100;
                                        TxtMontoCirujano.value = (TotalUma * (parseFloat(TxtPorcentajeCirujano.value) / 100)).toFixed(2);
                                        document.getElementById('<%= TextMontoCirujanoHF.ClientID %>').value = TxtMontoCirujano.value;
                                    }
                                }
                                if (TxtPorcentajeAnestesiologo.value.length > 0 & (TxtPorcentajeAnestesiologo.value <= 100 & TxtPorcentajeAnestesiologo.value >= 0)) {
                                    TxtMontoAnestesiologo.value = (TotalUma * (parseFloat(TxtPorcentajeAnestesiologo.value) / 100)).toFixed(2);
                                    document.getElementById('<%= TextMontoAnestesiologoHF.ClientID %>').value = TxtMontoAnestesiologo.value;
                                }
                                else {
                                    if (TxtPorcentajeAnestesiologo.value > 100) {
                                        TxtPorcentajeAnestesiologo.value = 100;
                                        TxtMontoAnestesiologo.value = (TotalUma * (parseFloat(TxtPorcentajeAnestesiologo.value) / 100)).toFixed(2);
                                        document.getElementById('<%= TextMontoAnestesiologoHF.ClientID %>').value = TxtMontoAnestesiologo.value;
                                    }
                                }

                                if (TxtPorcentajeAyudante.value.length > 0 & (TxtPorcentajeAyudante.value <= 100 & TxtPorcentajeAyudante.value >= 0)) {
                                    TxtMontoAyudante.value = (TotalUma * (parseFloat(TxtPorcentajeAyudante.value) / 100)).toFixed(2);
                                    document.getElementById('<%= TextMontoAyudanteHF.ClientID %>').value = TxtMontoAyudante.value;
                            }
                            else {
                                if (TxtPorcentajeAyudante.value > 100) {
                                    TxtPorcentajeAyudante.value = 100;
                                    TxtMontoAyudante.value = (TotalUma * (parseFloat(TxtPorcentajeAyudante.value) / 100)).toFixed(2);
                                    document.getElementById('<%= TextMontoAyudanteHF.ClientID %>').value = TxtMontoAyudante.value;
                                }
                            }

                            if (TxtPorcentajeInstrumentista.value.length > 0 & (TxtPorcentajeInstrumentista.value <= 100 & TxtPorcentajeInstrumentista.value >= 0)) {
                                TxtMontoInstrumentista.value = (TotalUma * (parseFloat(TxtPorcentajeInstrumentista.value) / 100)).toFixed(2);
                                document.getElementById('<%= TextMontoInstrumentistaHF.ClientID %>').value = TxtMontoInstrumentista.value;
                            }
                            else {
                                if (TxtPorcentajeInstrumentista.value > 100) {
                                    TxtPorcentajeInstrumentista.value = 100;
                                    TxtMontoInstrumentista.value = (TotalUma * (parseFloat(TxtPorcentajeInstrumentista.value) / 100)).toFixed(2);
                                    document.getElementById('<%= TextMontoInstrumentistaHF.ClientID %>').value = TxtMontoInstrumentista.value;
                                }
                            }

                            TxtMontoTotalCirugia.value = (parseFloat(TxtMontoCirujano.value) + parseFloat(TxtMontoAnestesiologo.value) + parseFloat(TxtMontoAyudante.value) + parseFloat(TxtMontoInstrumentista.value)).toFixed(2);
                            document.getElementById('<%= TextMontoTotalCirugiaHF.ClientID %>').value = TxtMontoTotalCirugia.value;

                                //VerValorCoPagoHF
                                //valor MontoTope de Esa Cirugia
                                var MontoTope = document.getElementById('<%=VerMontoTopeHF.ClientID%>');
                                if (parseFloat(MontoTope.value) < parseFloat(TxtMontoTotalCirugia.value)) {
                                    $("#MensajeErrorCalculoCirugia").show();
                                    document.getElementById('<%= LabelMensajeErrorCirugia.ClientID %>').style.color = "#F7F9F9";
                                    $("#MensajeErrorCalculoCirugia").css("color", "#01DF01")
                                    $("#MensajeErrorCalculoCirugia").css({ background: "#FF3333", height: "18px" })
                                    document.getElementById('<%= LabelMensajeErrorCirugia.ClientID %>').textContent = "El Monto Total de la Cirugia Pasa al Monto Tope=" + parseFloat(MontoTope.value).toFixed(2) + " Bs";
                                    document.getElementById('<%= LabelMensajeErrorCirugia.ClientID %>').className = "auto-style4";
                                }
                                else {
                                    $("#MensajeErrorCalculoCirugia").hide();
                                }
                                if (TxtMontoTotalCirugia.value.length > 0) {
                                    if (TxtValorCoPagoCirugia.value.length > 0) {
                                        var cadena = document.getElementById('<%=LabelValorCoPagoCirugia.ClientID%>').textContent;
                                        if (cadena.indexOf('%') != -1) {
                                            TxtMontoCoPagoCirugia.value = (parseFloat(TxtMontoTotalCirugia.value) * (parseFloat(TxtValorCoPagoCirugia.value) / 100)).toFixed(2);
                                            TxtMontoBsCirugia.value = (parseFloat(TxtMontoCoPagoCirugia.value)).toFixed(2);
                                            document.getElementById('<%= TextMontoBsCirugiaHF.ClientID %>').value = (parseFloat(TxtMontoCoPagoCirugia.value)).toFixed(2);
                                        }
                                        else {
                                            TxtMontoBsCirugia.value = (parseFloat(TxtMontoCoPagoCirugia.value)).toFixed(2);
                                            TxtMontoCoPagoCirugia.value = (parseFloat(TxtMontoCoPagoCirugia.value)).toFixed(2);
                                            document.getElementById('<%= TextMontoBsCirugiaHF.ClientID %>').value = (parseFloat(TxtMontoTotalCirugia.value)).toFixed(2);
                                    }
                                }
                            }
                        }
                    }
                }




                function CiudadOdontologiaRadComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                    $find('<%= RadComboBoxProveedorOdontologia.ClientID %>').clearSelection();
                    var combo = $find("<%= RadComboBoxProveedorOdontologia.ClientID %>");
                    combo.requestItems('', false);
                }

                function ProveedorOdontologiaRadCombo_OnClientItemsRequesting(sender, eventArgs) {
                    var combo = $find("<%= RadComboBoxCiudadOdontologia.ClientID %>");
                    var context = eventArgs.get_context();
                    context["ciudadId"] = combo.get_value();
                    context["tipoPriveedor"] = "MEDICO";
                    context["especialidadId"] = "30";
                    context["clienteId"] = $('#<%= ClienteIdHF.ClientID%>').val();
                }

                function PrestacionRCB_OnClientItemsRequesting(sender, eventArgs) {
                    var combo = $find("<%= RadComboBoxProveedorOdontologia.ClientID %>");
                        var context = eventArgs.get_context();
                        context["ProveedorId"] = combo.get_value();
                        context["clienteId"] = $('#<%= ClienteIdHF.ClientID%>').val();
                }

                function alerta(numero) {
                    alert('Se ha presionado el boton: ' + numero);
                }

                </script>
            </div>
        </div>
    </div>
</asp:Content>
