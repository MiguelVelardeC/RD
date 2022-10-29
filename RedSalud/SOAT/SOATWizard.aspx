<%@ Page Title="SOAT" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="SOATWizard.aspx.cs" Inherits="SOAT_SOATWizard" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/Tag/TagSelector.ascx" TagPrefix="RedSalud" TagName="TagSelector" %>
<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/tagit.js" type="text/javascript"></script>
    <style>
        .valNumbers,
        .valDecimal
        {
            text-align: right;
        }
        .ResumenBlock{
            display: inline-block;
        }
        .ConductorDataPanel
        {
            margin-bottom: 2px;
        }
        .ConductorDataPanel .label
        {
            display: inline-block;
            margin-top: 0;
        }
        .ConductorDataPanel fieldset
        {
            padding: 0 10px 5px;
        }
        .NoStrong
        {
            font-weight: normal;
        }
        legend
        {
            color: #29528A!important;
            font-size: 15px;
            font-weight: bold;
        }
        .RadCalendarPopup
        {
            z-index: 9001 !important;
        }
        .tagit ul.ui-autocomplete.ui-front.ui-menu.ui-widget.ui-widget-content.ui-corner-all{
            z-index: 9001;
        }
    </style>
    <script type="text/javascript">
        var somethingChanged = false;
        $(document).ready(function () {
            $('.text-readonly').attr('readonly', 'readonly');

            $('input, textarea').change(function () {
                if (!$(this).hasClass('disable')) {
                    somethingChanged = true;
                }
            });
            $('input.valNumbers').keypress(function (e) {
                switch (e.keyCode) {
                    case 8:  // Backspace
                    case 9:  // Tab
                    case 13: // Enter
                    case 37: // Left
                    case 38: // Up
                    case 39: // Right
                    case 40: // Down
                        return true;

                    default:
                        break;
                }
                return (getString(e).match(/^[0-9]$/) != null);
            });
            
            $('input.valDecimal').focus(function () {
                $(this).select();
            });
            $('input.valDecimal').keypress(function (e) {
                switch (e.keyCode) {
                    case 8:  // Backspace
                    case 46: // Delete
                    case 9:  // Tab
                    case 13: // Enter
                    case 37: // Left
                    case 38: // Up
                    case 39: // Right
                    case 40: // Down
                        return true;

                    default:
                        break;
                }
                var key = getString(e);
                if($(this).val() == '' && (key == ',' || key == '.')){
                    return false;
                }
                if ($(this).val().match(/[.,]/) != null && key == '.') {
                    return false;
                }
                if ($(this).val().match(/[,.]/) != null && key == ',' ) {
                    return false;
                }
                return (key.match(/^([0-9]|[,.])$/) != null);
            });
            $('select.disable').each(function () {
                $(this).after('<input id="' + $(this).attr('id') + '" ' +
                    'class="disable" type="text" value="' + $(this).val() + '" name="' + $(this).attr('name') + '">');
                $(this).hide();
            });
            $("input.disable, .RadPicker.disable input").attr('tabindex', -1);
            $("input.disable, .RadPicker.disable input").bind('paste', function () { return false; });
            $("input.disable, .RadPicker.disable input").bind('cut', function () { return false; });
            $('.RadPicker.disable a.rcCalPopup').attr('onclick', 'return false;');
            $('input.disable, .RadPicker.disable input, textarea.disable').keypress(function (e) {
                var ctrlDown = e.ctrlKey || e.metaKey; // Mac support
                if (ctrlDown && getString(e) == 'c') return true;
                switch (e.keyCode) {
                    case 9:  // Tab
                    case 37: // Left
                    case 38: // Up
                    case 39: // Right
                    case 40: // Down
                        return true;

                    default:
                        break;
                }
                return false;
            });
        });
        function getString(e) {
            var evtobj = window.event ? event : e; //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
            var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode;
            return String.fromCharCode(unicode);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="columnHead">
        <span class="title">Siniestro SOAT</span>
    </div>
    <div class="contentMenu">
        <asp:LinkButton runat="server"
            ID="BackToList"
            Text="Volver a la Lista de Siniestros"
            OnClick="BackToList_Click" />
        <asp:LinkButton ID="NewAccidentadoLB" runat="server" 
            OnClick="NewAccidentadoLB_Click" CssClass="addNew">
            <span>Nuevo Accidentado</span>
        </asp:LinkButton>
    </div>
     <asp:Panel ID="SiniestroDataPanel" runat="server" Visible="false">
        <asp:Label runat="server" ID="NombreCliente" Visible="false"
            CssClass="label" />
        <div class="ResumenBlock">
            <asp:Label runat="server" ID="FechaSiniestro"
                CssClass="label" />
            <asp:Label runat="server" ID="FechaDenuncia"
                CssClass="label" />
        </div>
        <div class="ResumenBlock">
            <asp:Label runat="server" ID="NroPoliza"
                CssClass="label" />
            <asp:Label runat="server" ID="OperacionId"
                CssClass="label" />
        </div>
         <div class="ResumenBlock">
            <asp:Label runat="server" ID="LugarSiniestro"
                CssClass="label" />
            <asp:Label runat="server" ID="Label4"
                CssClass="label" />
        </div>
        <div class="ResumenBlock">
            <asp:Label runat="server" Text="&nbsp;" CssClass="label" />
            <%-- 
            <asp:Label runat="server" ID="EstadoCasoLabel" Text="Estado:" style="font-weight: bold;" />
            --%>
            <asp:DropDownList ID="EstadoCasoDDL" runat="server" AutoPostBack="true" Visible="false"
                OnSelectedIndexChanged="EstadoCasoDDL_SelectedIndexChanged">
                <asp:ListItem Text="EN CURSO" Value="EN CURSO" />
                <asp:ListItem Text="APROBADO" Value="APROBADO" />
                <asp:ListItem Text="TERMINADO" Value="TERMINADO" />
            </asp:DropDownList>
        </div>
        <asp:Panel ID="AccidentadoDataPanel" runat="server" Visible="false" CssClass="ResumenBlock">
            <div class="ResumenBlock">
            <asp:Label runat="server" ID="NombreAccidentadoTextBox"
                CssClass="label" />
            <asp:Label runat="server" ID="CIAccidentadoTextBox"
                CssClass="label" />
            </div>
            <div class="ResumenBlock">
                <asp:Label runat="server" ID="EstadoSeguimientoLabel" Visible="false"
                    CssClass="label" />
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="Step1Panel" runat="server" DefaultButton="NextWizardLB">
        <style>
            .PanelSiniestro > fieldset
            {
                min-height: 400px;
            }
            .PanelPoliza fieldset
            {
                min-height: 230px;
            }
            .PanelVehiculo fieldset
            {
                min-height: 140px;
            }
        </style>
        <asp:Panel CssClass="PanelSiniestro" GroupingText="Siniestro" runat="server" style="float:left; margin-right: 5px;" ObjectName="SINIESTROFILES">
             
            <asp:DropDownList ID="ClienteDDL" runat="server"
                DataSourceID="ClienteODS"
                style="width: 346px; height:20px;"
                DataValueField="ClienteId"
                DataTextField="NombreJuridico"
                AutoPostBack="false"
                Visible="false">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ClienteODS" runat="server"
                TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="getRedClienteForSOATList"
                OnSelected="ClienteODS_Selected">
            </asp:ObjectDataSource>
                
            <span class="label">Numero de Siniestro:</span>
            <asp:TextBox runat="server" ID="OperacionesIdTextBox"
                CssClass="normalField" />
            <asp:HiddenField runat="server" ID="OperacionesIdHF" />
            <div class="validation">
                <asp:RequiredFieldValidator ID="IdOperacionesRFV" runat="server"
                    ControlToValidate="OperacionesIdTextBox"
                    ErrorMessage="Debe ingresar el Identificador de Operaciones."
                    Display="Dynamic"
                    ValidationGroup="step1">
                </asp:RequiredFieldValidator>
            </div>

            <span class="label">Fecha de siniestro:</span>
            <telerik:RadDatePicker runat="server" ID="FechaSiniestroRDP"
                CssClass="normalField" Width="200px">
            </telerik:RadDatePicker>
            <div class="validation">
                <asp:RequiredFieldValidator ID="FechaSiniestroRFV" runat="server"
                    ControlToValidate="FechaSiniestroRDP"
                    ErrorMessage="Debe seleccionar la fecha del siniestro."
                    Display="Dynamic"
                    ValidationGroup="step1">
                </asp:RequiredFieldValidator>
            </div>
        
            <span class="label">Fecha de denuncia:</span>
            <telerik:RadDatePicker runat="server" ID="FechaDenunciaRDP"
                CssClass="normalField" Width="200px">
            </telerik:RadDatePicker>
            <div class="validation">
                <asp:RequiredFieldValidator ID="FechaDenunciaRFV" runat="server"
                    ControlToValidate="FechaDenunciaRDP"
                    ErrorMessage="Debe seleccionar la fecha de denuncia del siniestro."
                    Display="Dynamic"
                    ValidationGroup="step1">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="FechaFinCV" runat="server"
                    ControlToValidate="FechaDenunciaRDP"
                    ErrorMessage="La Fecha de denuncia debe ser mayor a la fecha de siniestro"
                    Display="Dynamic"
                    ValidationGroup="step1"
                    ClientValidationFunction="FechaDenunciaCV_Validate" />
            </div>
        
            <span class="label">Causa:</span>
            <asp:RadioButtonList runat="server" ID="TipoCausaRBL" CssClass="normalField">
                <asp:ListItem Value="N" Text="NINGUNO" Selected="True" />
                <asp:ListItem Value="H" Text="FACTOR HUMANO" />
                <asp:ListItem Value="M" Text="FACTOR MECANICO" />
                <asp:ListItem Value="C" Text="FACTOR CLIMATOLÓGICO" />
            </asp:RadioButtonList>
            <span id="CausaLabel" class="label">Detalles de la Causa:</span>
            <asp:TextBox ID="CausaTextBox" runat="server" TextMode="MultiLine" CssClass="normalField" />
            <div class="validation"></div>

            <asp:Panel runat="server" GroupingText="Lugar del Siniestro" Width="220px">
                <span class="label">Departamento:</span>
                <asp:DropDownList runat="server" ID="DepartamentoDDL"
                    CssClass="normalField" onChange="DepartamentoDDL_onselect();">
                    <asp:ListItem Text="LA PAZ" />
                    <asp:ListItem Text="EL ALTO" />
                    <asp:ListItem Text="SANTA CRUZ" />
                    <asp:ListItem Text="MONTERO" />
                    <asp:ListItem Text="COCHABAMBA" />
                    <asp:ListItem Text="CHUQUISACA" />
                    <asp:ListItem Text="ORURO" />
                    <asp:ListItem Text="POTOSÍ" />
                    <asp:ListItem Text="TARIJA" />
                    <asp:ListItem Text="BENI" />
                    <asp:ListItem Text="PANDO" />
                </asp:DropDownList>
            
                <span class="label">Provincia:</span>
                <asp:DropDownList runat="server" ID="ProvinciaDDL" onchange="ProvinciaDDL_onselect();"
                    CssClass="normalField">
                    <asp:ListItem Text="" />
                </asp:DropDownList>
                <asp:HiddenField runat="server" ID="ProvinciaHF" />
            </asp:Panel>

            <span class="label">Area:</span>
            <asp:RadioButtonList runat="server" CssClass="normalField" ID="AreaRadioList">
                <asp:ListItem Selected="True" Value="URBANA">Urbana</asp:ListItem>
                <asp:ListItem Value="RURAL">Rural</asp:ListItem>
            </asp:RadioButtonList>
            <%-- 
            <asp:TextBox runat="server" ID="ZonaTextBox"
                CssClass="normalField" />
                --%>

            <span class="label">Tiene Sindicato:</span>
            <asp:RadioButtonList runat="server" ID="TieneSindicatoRBL" 
                CssClass="normalField" RepeatDirection="Horizontal">
                <asp:ListItem Text="SI" Value="S"></asp:ListItem>
                <asp:ListItem Text="NO" Value="N"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ErrorMessage="Debe Seleccionar si tiene Sindicato."
                ControlToValidate="TieneSindicatoRBL" runat="server"
                Display="Dynamic" ValidationGroup="step1" />

            <div id="SindicatoPanel">
                <span class="label">Nombre del Sindicato:</span>
                <asp:TextBox runat="server" ID="SindicatoTextBox"
                    CssClass="normalField" />
            </div>
            
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#<%=TieneSindicatoRBL.ClientID%> input').click(function () {
                        if ($('#<%=TieneSindicatoRBL.ClientID%> input:checked').val() == 'N') {
                            $('#SindicatoPanel').hide();
                            $('#<%=SindicatoTextBox.ClientID%>').val('');
                        } else {
                            $('#SindicatoPanel').show();
                        }
                    });
                    $('#<%=TieneSindicatoRBL.ClientID%> input:checked').click();
                    DepartamentoDDL_onselect();
                });
            
                function ProvinciaDDL_onselect() {
                    $('#<%= ProvinciaHF.ClientID%>').val($("#<%=ProvinciaDDL.ClientID%> option:selected").text());
                }
                function DepartamentoDDL_onselect() {
                    switch ($("#<%=DepartamentoDDL.ClientID%> option:selected").text()) {
                        case 'LA PAZ':
                            addLaPazProv();
                            break;
                        case 'SANTA CRUZ':
                            addSCZProv();
                            break;
                        case 'COCHABAMBA':
                            addCCBProv();
                            break;
                        case 'CHUQUISACA':
                            addCQSProv();
                            break;
                        case 'POTOSÍ':
                            addPotosiProv();
                            break;
                        case 'TARIJA':
                            addTJRProv();
                            break;
                        case 'BENI':
                            addBeniProv();
                            break;
                        case 'PANDO':
                            addPandoProv();
                            break;
                        case 'EL ALTO':
                            addElAltoProv();
                            break;
                        case 'MONTERO':
                            addMonteroProv();
                            break;
                        case 'ORURO':
                            addOruroProv();
                            break;
                    }
                    $('#<%= ProvinciaHF.ClientID%>').val($("#<%=ProvinciaDDL.ClientID%> option").first().text());
                }
                function addLaPazProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Aroma,Bautista Saavedra,Abel Iturralde,Caranavi,Eliodoro Camacho,' + 
                        'Franz Tamayo,Gualberto Villaroel,Ingavi,Inquisivi,General José Manuel Pando,Larecaja,Loayza,Los Andes,' +
                        'Manco Kapac,Muñecas,Nor Yungas,Omasuyos,Pacajes,Pedro Domingo Murillo,Sud Yungas');
                }
                function addSCZProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Andrés Ibáñez,Ignacio Warnes,José Miguel de Velasco,Ichilo,Chiquitos,Sara,Cordillera,' + 
                        'Vallegrande,Florida,Santistevan,Ñuflo de Chávez,Ángel Sandoval,Caballero,Germán Busch,Guarayos');
                }
                function addCCBProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Arani,Esteban Arce,Arque,Ayopaya,Campero,Capinota,Cercado,Carrasco,Chapare,Germán Jordán,' +
                        'Mizque,Punata,Quillacollo,Tapacarí,Bolívar,Tiraque');
                }
                function addCQSProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Oropeza,Juana Azurduy de Padilla,Jaime Zudáñez,Tomina,Hernando Siles,Yamparáez,Nor Cinti,' +
                        'Sud Cinti,Belisario Boeto,Luis Calvo');
                }
                function addOruroProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Sabaya,Carangas,Cercado,Eduardo Avaroa,Ladislao Cabrera,Litoral,Mejillones,Nor Carangas,' +
                        'Pantaleón Dalence,Poopó');
                }
                function addPotosiProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Alonzo de Ibáñez,Antonio Quijarro,Bernardino Bilbao,Charcas,Chayanta,Cornelio Saavedra,' +
                        'Daniel Saavedra,Enrique Baldivieso,José María Linares,Modesto Omiste');
                }
                function addTJRProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Aniceto Arce,Burdet O\'Connor,Cercado,Eustaquio Méndez,Gran Chaco,José María Avilés');
                }
                function addBeniProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Cercado,Antonio Vaca Díez,Mariscal José Ballivián Segurola,Yacuma,Moxos,Marbán,Mamoré,Iténez');
                }
                function addPandoProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Abuná,Federico Román,Madre de Dios,Manuripi,Nicolás Suárez');
                }
                function addElAltoProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'El Alto');
                }
                function addMonteroProv() {
                    var dropDownListRef = $('#<%= ProvinciaDDL.ClientID %>');
                    createOptions(dropDownListRef, 'Montero');
                }

                function createOptions(DDL, Text) {
                    DDL.find('option').remove();

                    var texts = Text.split(',');
                    for (var i = 0; i < texts.length; i++) {
                        var text = texts[i];
                        var option1 = document.createElement("option");
                        option1.text = text.toUpperCase();
                        option1.value = text.toUpperCase();
                        DDL.append(option1);
                    }
                    return option1;
                }
                function FechaDenunciaCV_Validate(sender, args) {
                    args.IsValid = true;
                    var FechaInicio = $find('<%= FechaSiniestroRDP.ClientID %>').get_selectedDate();
                    var FechaFin = $find('<%= FechaDenunciaRDP.ClientID %>').get_selectedDate();

                    if (FechaFin < FechaInicio) {
                        args.IsValid = false;
                    }
                }
            </script>
        </asp:Panel>
        <asp:Panel runat="server" style="float:left;margin-right: 5px;">
            <asp:Panel CssClass="PanelPoliza" runat="server" GroupingText="Datos de la Poliza y/o Certificado">
                

                <span class="label">Número de roseta:</span>
                <asp:TextBox runat="server" ID="NumeroRosetaTextBox"
                    CssClass="normalField" />
                <div class="validation">
                    <asp:RequiredFieldValidator ID="NumeroRosetaRFV" runat="server"
                        ControlToValidate="NumeroRosetaTextBox"
                        ErrorMessage="Debe ingresar el número de roseta."
                        Display="Dynamic"
                        ValidationGroup="step1">
                    </asp:RequiredFieldValidator>
                </div>
                <span class="label">Placa: </span>
                <asp:TextBox runat="server" ID="PlacaTextBox" onchange="createIdentificadorDeOperacion();"
                    CssClass="normalField" />
                <asp:LinkButton ID="CargaAlianzaLB" Visible="false" runat="server"
                    OnClientClick="return CargaAlianzaLB_Click();">
                    <span>Cargar de Alianza</span>
                </asp:LinkButton>
                <div class="validation">
                    <asp:RequiredFieldValidator ID="PlacaRequiredFieldValidator" runat="server"
                        ControlToValidate="PlacaTextBox"
                        ErrorMessage="Debe ingresar el número de placa del vehiculo."
                        Display="Dynamic"
                        ValidationGroup="step1">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="PlacaTextBox"
                        ValidationExpression="[A-Za-z0-9]+"
                        ErrorMessage="El número de placa del vehiculo solo puede contener numero y letras."
                        Display="Dynamic"
                        ValidationGroup="step1">
                    </asp:RegularExpressionValidator>
                </div>
                <%-- 
                <span class="label">Número de Certificado o Poliza: </span>
                <asp:TextBox runat="server" ID="PolizaTextBox"
                    CssClass="normalField" />
                <div class="validation">
                    <asp:RequiredFieldValidator ID="PolizaRFV" runat="server"
                        ControlToValidate="PolizaTextBox"
                        ErrorMessage="Debe ingresar el número de certificado o poliza."
                        Display="Dynamic"
                        ValidationGroup="step1">
                    </asp:RequiredFieldValidator>
                </div>
                --%>
                <span class="label">Lugar Venta: </span>
                <asp:DropDownList runat="server" ID="LugarVentaDDL"
                    CssClass="normalField">
                    <asp:ListItem Text="" Selected="True" />
                    <asp:ListItem Text="LA PAZ" />
                    <asp:ListItem Text="EL ALTO" />
                    <asp:ListItem Text="SANTA CRUZ" />
                    <asp:ListItem Text="MONTERO" />
                    <asp:ListItem Text="COCHABAMBA" />
                    <asp:ListItem Text="CHUQUISACA" />
                    <asp:ListItem Text="ORURO" />
                    <asp:ListItem Text="POTOSÍ" />
                    <asp:ListItem Text="TARIJA" />
                    <asp:ListItem Text="BENI" />
                    <asp:ListItem Text="PANDO" />
                </asp:DropDownList>

                <span class="label">Nombre o Razón Social:</span>
                <asp:TextBox runat="server" ID="NombreTitularTextBox"
                    CssClass="normalField" />
                <div class="validation">
                    <asp:RequiredFieldValidator ID="NombreTitularRFV" runat="server"
                        ControlToValidate="NombreTitularTextBox"
                        ErrorMessage="Debe ingresar los nombres o la razón social."
                        Display="Dynamic"
                        ValidationGroup="step1">
                    </asp:RequiredFieldValidator>
                </div>

                <span class="label">Carnet de Identidad o NIT: </span>
                <asp:TextBox runat="server" ID="CITitularTextBox"
                    CssClass="normalField" />
                <div class="validation">
                    <asp:RegularExpressionValidator ID="CITitularREV" runat="server"
                        ControlToValidate="CITitularTextBox"
                        ValidationExpression="^[0-9]+(LP|SC|CB|CHQ|OR|PT|TJ|BE|PA)?$"
                        ErrorMessage="El formato del CI o NIT no es valido ."
                        Display="Dynamic"
                        ValidationGroup="step1">
                    </asp:RegularExpressionValidator>
                </div>
                <asp:Panel ID="Panel1" CssClass="PanelVehiculo" Style="margin-top: 20px;" runat="server" GroupingText="Vehiculo">
                <span class="label">Tipo de Vehiculo:</span>
                <asp:DropDownList runat="server" ID="TipoVehiculoDDL"
                    CssClass="normalField">
                    <asp:ListItem Text="AUTO" Selected="True" />
                    <asp:ListItem Text="AUTOBUS" />
                    <asp:ListItem Text="CAMIÓN" />
                    <asp:ListItem Text="CAMIONETA" />
                    <asp:ListItem Text="COLECTIVO" />
                    <asp:ListItem Text="FURGONETA" />
                    <asp:ListItem Text="JEEP" />
                    <asp:ListItem Text="MICROBUS" />
                    <asp:ListItem Text="MINIBUS" />
                    <asp:ListItem Text="MOTOCICLETA" />
                    <asp:ListItem Text="OMNIBÚS" />
                    <asp:ListItem Text="REMOLQUE-ACOPLADO" />
                    <asp:ListItem Text="TRACTO-CAMIÓN" />
                    <asp:ListItem Text="VAGONETA" />
                </asp:DropDownList>

                <span class="label">Nro de Chasis:</span>
                <asp:TextBox runat="server" ID="NroChasisTextBox"
                    CssClass="normalField" />

                <span class="label">Nro de Motor:</span>
                <asp:TextBox runat="server" ID="NroMotorTextBox"
                    CssClass="normalField" />

                <span class="label">Sector:</span>
                <asp:DropDownList runat="server" ID="SectorDDL"
                    CssClass="normalField">
                    <asp:ListItem Text="PÚBLICO" Selected="True" />
                    <asp:ListItem Text="PARTICULAR" />
                    <asp:ListItem Text="OFICIAL" />
                </asp:DropDownList>
            </asp:Panel>
            </asp:Panel>
            
        </asp:Panel>

            <asp:Panel runat="server" GroupingText="Datos Adicionales" Width="564" style="float:left;">
                <span class="label" style="display: none;">Acuerdo Transaccional:</span>
                <asp:RadioButtonList runat="server" Visible="false" ID="AcuerdoTransaccionalRBL" 
                    CssClass="normalField" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <%-- 
                <asp:RequiredFieldValidator ErrorMessage="Debe Seleccionar si tiene Acuerdo Transaccional." 
                    ControlToValidate="AcuerdoTransaccionalRBL" runat="server"
                    Display="Dynamic" ValidationGroup="step1" />
                --%>
                <span class="label" style="display:none;">Repetición:</span>
                <asp:RadioButtonList runat="server" Visible="false" ID="RechazadoRBL" 
                    CssClass="normalField" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <%-- 
                <asp:RequiredFieldValidator ErrorMessage="Debe Seleccionar si es Repetición." 
                    ControlToValidate="RechazadoRBL" runat="server"
                    Display="Dynamic" ValidationGroup="step1" />
                --%>
                <span class="label">Observaciones:</span>
                <asp:TextBox runat="server" ID="ObservacionesTextBox" TextMode="MultiLine"
                    style="width: 270px;" />
                <span class="label">Nombre Inspector:</span>
                <asp:TextBox runat="server" ID="NombreInspectorTextBox"
                    style="width: 270px;" />
                
            </asp:Panel>
            <RedSalud:FileManager ID="DatosAdicionalesFileManager" runat="server" Visible="false" ShowMode="Normal" ObjectName="SINIESTRO" />
        <script type="text/javascript">

            function CargaAlianzaLB_Click() {
                
                if ($('#<%=NumeroRosetaTextBox.ClientID%>').val() != '' && $('#<%=NumeroRosetaTextBox.ClientID%>').val() != '') {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'ResolveClientUrl("~/SOAT/SOATWizard.aspx") /CargarDeAlianza',
                        data: '{ nroRoseta : ' + $('#<%=NumeroRosetaTextBox.ClientID%>').val() + ', placa : ' + $('#<%=PlacaTextBox.ClientID%>').val() + ' }',
                        dataType: "json",
                        success: function (msg) {
                            var answerData = msg.d;
                            /*var total = parseInt(answerData.split('####')[0]);

                            if (total > 1) {
                                var polizas = answerData.split('####')[1].split('###');
                                var polizasDdl = '#PolizasAliazaDDL.ClientID';
                                $(polizasDdl).html('');
                                for (var i = 0; i < polizas.length; i++) {
                                    var poliza = polizas[i].split('#;#');
                                    $('<option value="' + polizas[i] + '">' + poliza[0] + ' | ' + poliza[5] + '</option>').appendTo(polizasDdl);
                                }
                                ShowSPDialog();
                            } else if (total == 1) {
                                loadPoliza(answerData.split('####')[1].split('#;#'));
                            }
                            console.log(answerData);*/
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                }
                return false;
            }
            function loadPoliza(polizaAlianza) {
                try {
                    $('#<%=NumeroRosetaTextBox.ClientID%>').val(polizaAlianza[0]);
                    /*$('#PolizaTextBox.ClientID').val(polizaAlianza[1]);*/
                    $('#<%=LugarVentaDDL.ClientID%>').val(polizaAlianza[2]);
                    $('#<%=NombreTitularTextBox.ClientID%>').val(polizaAlianza[3]);
                    $('#<%=CITitularTextBox.ClientID%>').val(polizaAlianza[4]);
                    $('#<%=PlacaTextBox.ClientID%>').val(polizaAlianza[5]);
                    $('#<%=TipoVehiculoDDL.ClientID%>').val(polizaAlianza[6]);
                    //$('#CilindradaTextBox.ClientID').val(polizaAlianza[7]);
                    $('#<%=SectorDDL.ClientID%>').val(polizaAlianza[8]);
                    /*$('#PolizaTextBox.ClientID').blur();*/
                } catch (e) {
                    alert('Error al Seleccionar la Póliza');
                }
            }
            $(document).ready(function () {
                /*, #PolizaTextBox.ClientID*/
                $('#<%=NumeroRosetaTextBox.ClientID%>').blur(function () {
                    var numero = $('#<%=NumeroRosetaTextBox.ClientID%>').val().trim();
                    /*
                    var poliza = $('#PolizaTextBox.ClientID').val().trim();
                    if (numero != poliza) {
                        $('#NumeroRosetaTextBox.ClientID, #PolizaTextBox.ClientID').css('background-color', '#FFF9F7');
                        $('#NumeroRosetaTextBox.ClientID, #PolizaTextBox.ClientID').css('border', '1px solid #FF0000');
                    } else {
                        $('#NumeroRosetaTextBox.ClientID, #PolizaTextBox.ClientID').attr('style','');
                    }*/
                });
            });
        </script>
        <div class="clear"></div>
    </asp:Panel>
    <asp:Panel ID="Step2Panel" runat="server">
        <asp:Panel ID="AccidentadosListPanel" runat="server" Visible="true">
             <asp:HiddenField runat="server" ID="AccidentadoListHiddenField" Value="" />            
             <asp:Panel ID="ConductorDataPanel" runat="server" Visible="false" 
                 CssClass="ConductorDataPanel" GroupingText="Conductor">
                 <asp:HiddenField ID="ConductorIdHF" runat="server" /> 
                <asp:Label runat="server" ID="NombreConductorLabel"
                    CssClass="label" />
                <asp:Label runat="server" ID="CIConductorLabel"
                    CssClass="label" />
                <asp:Label runat="server" ID="SexoConductorLabel"
                    CssClass="label" />
                <asp:Label runat="server" ID="FechaNacimientoLabel"
                    CssClass="label" />
                <asp:Label runat="server" ID="EstadoCivilLabel"
                    CssClass="label" />
                <asp:Label runat="server" ID="TieneLicenciaLabel"
                    CssClass="label" />
                 
                <asp:LinkButton ID="ModifyDriverLB" runat="server"
                    CssClass="label" OnClick="ModifyDriverLB_Click">
                    <span class="NoStrong">Modificar</span>
                </asp:LinkButton>
            </asp:Panel>

            <asp:Panel runat="server" DefaultButton="SearchAccidentadoLB">
                <asp:Label ID="Label6" Text="Buscar Siniestro por nombre, apellido o CI" runat="server" CssClass="label" />
                <div class="buttonsPanel">
                    <asp:TextBox ID="SearchAccidentadoTexbox" runat="server" CssClass="biggerField left" />
                    <asp:LinkButton ID="SearchAccidentadoLB" Text="" runat="server"
                        CssClass="button left"
                        OnClick="SearchAccidentadoLB_Click">
                        <asp:Label ID="Label7" Text="Buscar" runat="server" />
                    </asp:LinkButton>
                    <div class="clear"></div>
                </div>
            </asp:Panel>
            <telerik:RadGrid ID="AccidentadoRadGrid" runat="server"
                AutoGenerateColumns="false"
                DataSourceID="AccidentadoODS"
                AllowPaging="true"
                PageSize="20"
                OnPreRender="AccidentadoRadGrid_PreRender"
                MasterTableView-DataKeyNames="AccidentadoId"
                OnItemDataBound="AccidentadoRadGrid_ItemDataBound">
                <MasterTableView>
                    <NoRecordsTemplate>
                        <span>No hay accidentados en este siniestro.</span>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="DeleteAccidentado">
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteImageButton" runat="server"
                                    ImageUrl="~/Images/neutral/delete.png"
                                    OnCommand="AccidentadoButton_Command"
                                    CommandArgument='<%# Eval("AccidentadoId") %>'
                                    OnClientClick="return confirm('¿Está seguro que desea eliminar el Accidentado?');"
                                    CommandName="Eliminar"
                                    ToolTip="Eliminar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="EditAccidentado" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/select2.png"
                                    OnCommand="AccidentadoButton_Command"
                                    CommandArgument='<%# Eval("AccidentadoId") + "," + Eval("EstadoSeguimiento") %>'
                                    CommandName="Select"
                                    ToolTip="Editar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="GestionMedicaAccidentado">
                            <ItemTemplate>
                                <asp:ImageButton ID="GestionImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/gestionMedica.png"
                                    OnCommand="AccidentadoButton_Command"
                                    CommandArgument='<%# Eval("AccidentadoId") %>'
                                    Width="22px" CommandName="AddGestion"
                                    ToolTip="Añadir Visita Médica"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PreliquidacionDetalleInsert" HeaderText="" Visible="true">
                            <ItemTemplate>
                                <asp:ImageButton ID="PreliquidacionImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/preliquidacion.png"
                                    OnClientClick='<%# "return OpenNGDialog(0, null, " + Eval("AccidentadoId") + ", \"G\");" %>'
                                    Width="22px" ToolTip="Añadir Gastos Médicos"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-CssClass="PreliquidacionDetalle" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="GastosImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/gastosMedicos.png"
                                    OnClientClick='<%# "return OpenNGDialog(0, null, " + Eval("AccidentadoId") + ", \"R\");" %>'
                                    Width="22px" ToolTip="Añadir Gastos Facturados"></asp:ImageButton>
                                <asp:HiddenField runat="server" ID="PreliquidaciondetalleidHF" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="NombreForDisplay" HeaderText="Nombre" />
                        <telerik:GridBoundColumn DataField="CarnetIdentidad" HeaderText="CI" />
                        <telerik:GridBoundColumn DataField="GeneroForDisplay" HeaderText="Sexo" />
                        <telerik:GridBoundColumn DataField="FechaNacimientoForDisplay" HeaderText="Fecha de Nacimiento" />
                        <telerik:GridBoundColumn DataField="EstadoCivil" HeaderText="Estado Civil" />
                        <telerik:GridBoundColumn DataField="LicenciaConducirForDisplay" HeaderText="Tiene Licencia" />
                        <telerik:GridBoundColumn DataField="TipoForDisplay" HeaderText="Tipo" />
                        <telerik:GridBoundColumn DataField="EstadoForDisplay" HeaderText="Accidentado / Fallecido" />
                        <telerik:GridBoundColumn DataField="SiniestroPreliquidacionForDisplay" HeaderText="Total Pre- liquidado" Visible="false" />
                        <telerik:GridBoundColumn DataField="SiniestroPagadoForDisplay" HeaderText="Total Gastos" />
                        <telerik:GridBoundColumn DataField="EstadoSeguimiento" HeaderText="Estado" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                            <ItemTemplate>
                                <div class="AdjuntosCont">
                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                        CommandName="ACCIDENTADO"
                                        CommandArgument='<%# Eval("AccidentadoId") %>'
                                        ToolTip="Adjuntar Archivo al Accidentado"
                                        OnCommand="FileManager_Command" />
                                    <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

            <asp:ObjectDataSource ID="AccidentadoODS" runat="server"
                TypeName="Artexacta.App.Accidentado.BLL.AccidentadoBLL"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="SearchAccidentado"
                OnSelected="AccidentadoODS_Selected">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SearchAccidentadoTexbox" Name="search" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <RedSalud:FileManager runat="server" ID="FileManager" />
        </asp:Panel>
        <asp:Panel ID="AccidentadoModifyPanel" runat="server" Visible="false" DefaultButton="NextWizardLB">
            <div id="accidentadoInfo" style="float: left;width:300px;">
            <span class="label">Nombre Completo:</span>
            <asp:TextBox runat="server" ID="NombresTextBox"
                CssClass="normalField" />
            <div class="validation">
                <asp:RequiredFieldValidator ID="NombresRFV" runat="server"
                    ControlToValidate="NombresTextBox"
                    ErrorMessage="Debe ingresar el nombre completo."
                    Display="Dynamic"
                    ValidationGroup="step2">
                </asp:RequiredFieldValidator>
            </div>

            <span class="label">Carnet de Identidad: </span>
            <asp:TextBox runat="server" ID="CITextBox"
                CssClass="normalField" />
                <div class="validation">
                    <asp:RequiredFieldValidator ID="CIRFV" runat="server"
                        ControlToValidate="CITextBox"
                        ErrorMessage="Debe ingresar la cédula de identidad."
                        Display="Dynamic"
                        ValidationGroup="step2">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="CIREV" runat="server"
                        ControlToValidate="CITextBox"
                        ErrorMessage="El formato del CI no es valido, debe ser sin espacios y con el codigo de ciudad."
                        ValidationExpression="^\s*[0-9]+(LP|SC|CB|CHQ|OR|PT|TJA|BE|PDO|lp|sc|cb|chq|or|pt|tja|be|pdo)?\s*$"
                        Display="Dynamic"
                        ValidationGroup="step2">
                    </asp:RegularExpressionValidator>
                </div>

            <span class="label">Sexo:</span>
            <asp:RadioButtonList runat="server" ID="SexoRBL" 
                CssClass="normalField" RepeatDirection="Horizontal">
                <asp:ListItem Text="MASCULINO" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="FEMENINO" Value="0"></asp:ListItem>
            </asp:RadioButtonList>

            <span class="label">Fecha de nacimiento:</span>
            <telerik:RadDatePicker runat="server" ID="FechaNacimientoRDP"
                CssClass="normalField" Width="200px" MinDate="1900-01-01">
                    <ClientEvents OnDateSelected="FechaNacimientoRDP_DateSelected" />
            </telerik:RadDatePicker>
            <div class="validation">
                <asp:RequiredFieldValidator ID="FechaNacimientoRFV" runat="server"
                    ControlToValidate="FechaNacimientoRDP"
                    ErrorMessage="Debe ingresar la fecha de nacimiento."
                    Display="Dynamic"
                    ValidationGroup="step2">
                </asp:RequiredFieldValidator>
            </div>
        
            <span class="label">Edad:</span>
            <asp:TextBox runat="server" ID="EdadTextBox"
                CssClass="normalField disable" />

            <span class="label">Estado Civil:</span>
            <asp:DropDownList runat="server" ID="EstadoCivilDDL"
                CssClass="normalField">
                <asp:ListItem Text="SOLTERO(A)" Selected="True" />
                <asp:ListItem Text="CASADO(A)" />
                <asp:ListItem Text="DIVORCIADO(A)" />
                <asp:ListItem Text="VIUDO(A)" />
            </asp:DropDownList>
            <asp:panel runat="server" ID="ReservaDetallePNL" class="reservaDetalle">
                <span style="font-weight:bold;" class="label">Reserva:</span>
                <asp:TextBox runat="server" ID="ReservaAccidentadoTextBox" Enabled="false"
                CssClass="normalField" style="text-align: right;" Visible="false" />
                <asp:ImageButton ID="ReservaImageButton" runat="server"
                    ImageUrl="~/Images/Neutral/select2.png"
                    OnClientClick="return OpenNGDialog(0, this, 0, 'RES');"
                    Width="20px"
                    ToolTip="Editar" Visible="false"></asp:ImageButton>
            </asp:panel>
            <span class="label">Tiene Licencia de Conducir:</span>
            <asp:RadioButtonList runat="server" ID="LicenciaDDL" 
                CssClass="normalField" RepeatDirection="Horizontal">
                <asp:ListItem Text="SI" Value="S"></asp:ListItem>
                <asp:ListItem Text="NO" Value="N"></asp:ListItem>
            </asp:RadioButtonList>

            <asp:Label ID="TipoAccidentadoLabel" Text="Tipo de Accidentado:" CssClass="label" runat="server" />
            <asp:RadioButtonList runat="server" ID="TipoAccidentadoRBL"
                CssClass="normalField" RepeatDirection="Horizontal">
                <asp:ListItem Text="PASAJERO" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="PEATON" Value="E"></asp:ListItem>
            </asp:RadioButtonList>

            <span class="label">Estado:</span>
            <asp:RadioButtonList runat="server" ID="EstadoRBL"
                Width="345px" RepeatDirection="Vertical">
                <asp:ListItem Text="HERIDO" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="INCAPACIDAD TOTAL" Value="I"></asp:ListItem>
                <asp:ListItem Text="FALLECIDO" Value="F"></asp:ListItem>
            </asp:RadioButtonList>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#<%=EstadoRBL.ClientID%> input').click(function () {
                        if ($('#<%=EstadoRBL.ClientID%> input:checked').val() == '-') {
                            $('#<%=GastosEjecutadosListPanel.ClientID%>').hide();
                            $('#<%=PreliquidacionListPanel.ClientID%>').hide();
                            $('#<%=GestionMedicaListPanel.ClientID%>').hide();
                        } else {
                            $('#<%=GastosEjecutadosListPanel.ClientID%>').show();
                            $('#<%=PreliquidacionListPanel.ClientID%>').show();
                            $('#<%=GestionMedicaListPanel.ClientID%>').show();
                        }
                        $('#accidentadoInfo').css('min-height', $('#<%=GastosEjecutadosListPanel.ClientID%>').height() + 
                            $('#<%=PreliquidacionListPanel.ClientID%>').height() + $('#<%=GestionMedicaListPanel.ClientID%>').height() + 'px');
                    });
                    $('#<%=EstadoRBL.ClientID%> input:checked').click();
                });
                function FechaNacimientoRDP_DateSelected(sender, eventArgs) {
                    var today = new Date();
                    var birthDate = sender.get_selectedDate();
                    var age = today.getFullYear() - birthDate.getFullYear();
                    var m = today.getMonth() - birthDate.getMonth();
                    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                        age--;
                    }
                    $('#<%=EdadTextBox.ClientID%>').val(age + ' años' );
                }
            </script>
            </div>
            <div style="float: left; width: 800px;">
                <asp:Panel ID="GestionMedicaListPanel" runat="server" 
                    GroupingText="Visitas Médicas" style="display:none;">
                    <asp:LinkButton ID="NewGestionMedicaLB" runat="server" 
                        CommandName="AddGestion" CssClass="addNew" Visible="false"
                        OnCommand="AccidentadoButton_Command">
                        <span>Nueva Visita Médica</span>
                    </asp:LinkButton>
                    <telerik:RadGrid ID="GestionMedicaRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        DataSourceID="GestionMedicaODS"
                        AllowPaging="true"
                        OnPreRender="GestionMedicaRadGrid_PreRender"
                        PageSize="5"
                        MasterTableView-DataKeyNames="AccidentadoId">
                        <MasterTableView>
                            <NoRecordsTemplate>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="EditGestionMedica" HeaderText="" ItemStyle-Width="24px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DetailsImageButton" runat="server"
                                            ImageUrl="~/Images/Neutral/select.png"
                                            OnCommand="GestionMedicaRadGrid_Command"
                                            CommandArgument='<%# Eval("GestionMedicaId") %>'
                                            CommandName="Select"
                                            ToolTip="Editar"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="DeleteGestionMedica" ItemStyle-Width="24px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DeleteImageButton" runat="server"
                                            ImageUrl="~/Images/neutral/delete.png"
                                            OnCommand="GestionMedicaRadGrid_Command"
                                            CommandArgument='<%# Eval("GestionMedicaId") %>'
                                            OnClientClick="return confirm('¿Está seguro que desea eliminar la Gestion Médica?');"
                                            CommandName="Eliminar"
                                            ToolTip="Eliminar"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                        
                                <telerik:GridBoundColumn DataField="Nombre" HeaderText="Establecimiento de Salud" />
                                <telerik:GridBoundColumn DataField="FechaVisitaForDisplay" HeaderText="Fecha de Visita" />
                                <telerik:GridBoundColumn DataField="Grado" HeaderText="Grado" />
                                <telerik:GridBoundColumn DataField="DiagnosticoPreliminar" HeaderText="Observaciones" Visible="true" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:ObjectDataSource ID="GestionMedicaODS" runat="server"
                        TypeName="Artexacta.App.GestionMedica.BLL.GestionMedicaBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetAllGestionMedicaBySiniestroID"
                        OnSelected="GestionMedicaODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="AccidentadoIDHF" Name="AccidentadoId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            
                <asp:Panel ID="PreliquidacionListPanel" runat="server" 
                    GroupingText="Gastos de Proforma" style="display:none;">
                    
                    <telerik:RadGrid ID="PreliquidacionRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        Visible="false"
                        DataSourceID="PreliquidacionODS"
                        AllowPaging="false"
                        MasterTableView-HierarchyLoadMode="Client"
                        OnItemCreated="PreliquidacionRadGrid_ItemCreated"
                        MasterTableView-DataKeyNames="PreliquidacionDetalleId">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Tipo">
                            <NoRecordsTemplate>
                                <asp:Label ID="Label1" runat="server" Text="No hay Pre-liquidaciones para mostrar"></asp:Label>
                            </NoRecordsTemplate>
                            <DetailTables>
                                <telerik:GridTableView
                                    AutoGenerateColumns="false"
                                    DataSourceID="PreliquidacionDetalleODS"
                                    PageSize="20"
                                    Name="PreliquidacionDetalle"
                                    DataKeyNames="PreliquidacionDetalleId">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="Tipo" MasterKeyField="Tipo" />
                                    </ParentTableRelation>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                                    ImageUrl="~/Images/Neutral/select.png"
                                                    fecha='<%# Eval("FechaVisitaForDisplay")%>'
                                                    tipo='<%# Eval("Tipo")%>'
                                                    monto='<%# Eval("Monto")%>'
                                                    proveedor='<%# Eval("Proveedor")%>'
                                                    fechaReciboFactura='<%# Eval("FechaReciboFacturaForDisplay")%>'
                                                    NumeroReciboFactura='<%# Eval("NumeroReciboFactura")%>'
                                                    isFactura='<%# Eval("IsFactura")%>'
                                                    estado='<%# Eval("Estado")%>'
                                                    OnClientClick='<%# "OpenNGDialog(" + Eval("PreliquidacionDetalleId") + ", this, 0, \"G\");return false;" %>'
                                                    ToolTip="Editar"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="DeletePreliquidacion" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    OnCommand="PreliquidacionRadGrid_Command"
                                                    OnClientClick="return confirm('¿Está seguro que desea eliminar el Gasto de Proforma?');"
                                                    CommandArgument='<%# Eval("PreliquidacionDetalleId") %>'
                                                    CommandName="Eliminar"
                                                    ToolTip="Eliminar"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PreliquidacionDetalleId" 
                                            ItemStyle-CssClass="PreliquidacionDetalleId" Display="false" />
                                        <telerik:GridBoundColumn DataField="TipoForDisplay" HeaderText="Tipo de Gasto" />
                                        <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" Visible="false" />
                                        <telerik:GridBoundColumn DataField="FechaVisitaForDisplay" HeaderText="Fecha" />
                                        <telerik:GridBoundColumn DataField="Proveedor" HeaderText="Proveedor" />
                                        <telerik:GridBoundColumn DataField="FechaReciboFacturaForDisplay" HeaderText="Fecha de Recepción" />
                                        <telerik:GridBoundColumn DataField="NumeroReciboFactura" HeaderText="Numero del Recibo / Factura" />
                                        <telerik:GridBoundColumn DataField="IsFacturaForDisplay" HeaderText="Es Factura" />
                                        <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto" 
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="EstadoForDisplay" HeaderText="Estado" />
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn DataField="TipoForDisplay" HeaderText="Tipo de Gasto" />
                                <telerik:GridBoundColumn DataField="PreliquidacionIdForDisplay" HeaderText="Cantidad de Pagos" />
                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto" 
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:ObjectDataSource ID="PreliquidacionODS" runat="server"
                        TypeName="Artexacta.App.Preliquidacion.BLL.PreliquidacionBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetPreliquidacionBySiniestroIdAndAccidentadoId"
                        OnSelected="PreliquidacionODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="AccidentadoIDHF" Name="AccidentadoId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="PreliquidacionDetalleODS" runat="server"
                        TypeName="Artexacta.App.Preliquidacion.BLL.PreliquidacionBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetPreliquidacionDetalleByTipo"
                        OnSelected="PreliquidacionODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="AccidentadoIDHF" Name="AccidentadoId" PropertyName="Value" Type="Int32" />
                            <asp:SessionParameter Name="Tipo" SessionField="Tipo" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
                <asp:Panel ID="GastosEjecutadosListPanel" runat="server"  style="display:none;"
                    GroupingText="Gastos Medicos">
                    <asp:LinkButton ID="NewPreliquidacionLB" runat="server" CssClass="addNew" Visible="false">
                        <span>Añadir Gasto</span>
                    </asp:LinkButton>
                    <%-- 
                    <asp:LinkButton ID="NewGastosEjecutadosLB" runat="server" CssClass="addNew" CausesValidation="false">
                        <span>Añadir Gastos Facturados</span>
                    </asp:LinkButton>
                    --%>
                    <div class="right">
                        <span style="font-weight:bold; display:none;">Ahorro:</span>
                        <asp:TextBox runat="server" ID="AhorroTexBox" BackColor="#48A6B0" ForeColor="#FFFFFF"
                            CssClass="smallField disable" style="text-align: right;" Visible="false" />
                    </div>
                    <div class="right" style="margin-right: 20px;">
                        
                    </div>
                    <telerik:RadGrid ID="GastosEjecutadosRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        DataSourceID="GastosEjecutadosODS"
                        AllowPaging="false"
                        OnPreRender="GastosEjecutadosRadGrid_PreRender"
                        MasterTableView-HierarchyLoadMode="Client"
                        OnItemCreated="GastosEjecutadosRadGrid_ItemCreated"
                        MasterTableView-DataKeyNames="GastosEjecutadosDetalleId">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Tipo">
                            <NoRecordsTemplate>
                                <asp:Label runat="server" Text="No hay Facturas Para mostrar"></asp:Label>
                            </NoRecordsTemplate>
                            <DetailTables>
                                <telerik:GridTableView AutoGenerateColumns="false"
                                    DataSourceID="GastosEjecutadosDetalleODS"
                                    PageSize="20"
                                    Name="GastosEjecutadosDetalle"
                                    DataKeyNames="GastosEjecutadosDetalleId">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="Tipo" MasterKeyField="Tipo" />
                                    </ParentTableRelation>
                                    <Columns>
                                        <telerik:GridTemplateColumn 
                                            UniqueName="EditGastosEjecutados" 
                                            HeaderText="" 
                                            ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                                    ImageUrl="~/Images/Neutral/select.png"
                                                    fecha='<%# Eval("FechaVisitaForDisplay")%>'
                                                    tipo='<%# Eval("Tipo")%>'
                                                    proveedor='<%# Eval("Proveedor")%>'
                                                    fechaReciboFactura='<%# Eval("FechaReciboFacturaForDisplay")%>'
                                                    NumeroReciboFactura='<%# Eval("NumeroReciboFactura")%>'
                                                    monto='<%# Eval("Monto")%>'
                                                    isFactura="False"
                                                    estado="False"
                                                    preliquidacionId='<%# Eval("PreliquidacionDetalleId")%>'
                                                    OnClientClick='<%# "return OpenNGDialog(" + Eval("GastosEjecutadosDetalleId") + ", this, 0, \"G\");" %>'
                                                    ToolTip="Editar"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn 
                                            UniqueName="DeleteGastosEjecutados" 
                                            ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    OnCommand="GastosEjecutadosRadGrid_Command"
                                                    CommandArgument='<%# Eval("GastosEjecutadosDetalleId") %>'
                                                    OnClientClick="return confirm('¿Está seguro que desea eliminar el Gasto Médico?');"
                                                    CommandName="Eliminar"
                                                    ToolTip="Eliminar"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PreliquidacionDetalleId" Display="false" />
                                        <telerik:GridBoundColumn DataField="TipoForDisplay" HeaderText="Tipo de Gasto" />
                                        <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" Visible="false" />
                                        <telerik:GridBoundColumn DataField="FechaVisitaForDisplay" HeaderText="Fecha" />
                                        <telerik:GridBoundColumn DataField="Proveedor" HeaderText="Proveedor" />
                                        <telerik:GridBoundColumn DataField="FechaReciboFacturaForDisplay" HeaderText="Fecha de la Factura" />
                                <telerik:GridBoundColumn DataField="IsFacturaDisplay" HeaderText="Tipo Documento" />
                                        <telerik:GridBoundColumn DataField="NumeroReciboFactura" HeaderText="Numero de la Factura" />
                                        <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto en Bs." 
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn DataField="TipoForDisplay" HeaderText="Tipo de Gasto" />
                                <telerik:GridBoundColumn DataField="GastosEjecutadosIdForDisplay" HeaderText="Cantidad de Pagos" />
                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto en Bs." 
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:ObjectDataSource ID="GastosEjecutadosODS" runat="server"
                        TypeName="Artexacta.App.GastosEjecutados.BLL.GastosEjecutadosBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetGastosEjecutadosBySiniestroIdAndAccidentadoId"
                        OnSelected="GastosEjecutadosODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="AccidentadoIDHF" Name="AccidentadoId" PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="GastosEjecutadosDetalleODS" runat="server"
                        TypeName="Artexacta.App.GastosEjecutados.BLL.GastosEjecutadosBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetGastosEjecutadosDetalleByTipo"
                        OnSelected="GastosEjecutadosODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SiniestroIDHF" Name="SiniestroId" PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="AccidentadoIDHF" Name="AccidentadoId" PropertyName="Value" Type="Int32" />
                            <asp:SessionParameter Name="Tipo" SessionField="Tipo" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="Step3Panel" runat="server" GroupingText="Gestion Médica" DefaultButton="NextWizardLB">
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
        <asp:ObjectDataSource ID="CiudadExComplementarioODS" runat="server"
            TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
            OldValuesParameterFormatString="{0}"
            SelectMethod="getCiudadList"
            OnSelected="CiudadExComplementarioODS_Selected">
        </asp:ObjectDataSource>

        <asp:Label ID="Label10" Text="Nombre de Establecimiento de Salud:" runat="server" CssClass="label" />
        <telerik:RadComboBox ID="ProveedorGestionDDL" runat="server"
            CssClass="biggerField"
            EnableLoadOnDemand="true"
            OnClientItemsRequesting="ProveedorGestionDDL_OnClientItemsRequesting"
            OnClientSelectedIndexChanged="ProveedorGestion_OnClientIndexChanged"
            EmptyMessage="Seleccione un Proveedor"
            OnClientBlur="ProveedorGestionDLL_OnClientBlur"
            ShowMoreResultsBox="true" EnableVirtualScrolling="true"
            AutoPostBack="false"
            MarkFirstMatch="true">
            <WebServiceSettings Method="GetSOATProveedorAutocompletePorCiudad" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
        </telerik:RadComboBox>
        <script type="text/javascript">
            function CiudadDerivacionComboBox_OnClientSelectedIndexChanged(sender, eventArgs) {
                $find('<%= ProveedorGestionDDL.ClientID %>').clearSelection();
                var combo = $find("<%= ProveedorGestionDDL.ClientID %>");
                combo.requestItems('', false);
            }
            function ProveedorGestionDDL_OnClientItemsRequesting(sender, eventArgs) {
                var combo = $find("<%= CiudadDerivacionComboBox.ClientID %>");
                var context = eventArgs.get_context();
                context["ciudadId"] = combo.get_value();
            }
            function ProveedorGestion_OnClientIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                //item.get_text();
                $('#<%= NombreTextBox.ClientID %>').val(item.get_text());
            }
            function ProveedorGestionDLL_OnClientBlur(sender, eventArgs) {
                var item = $find("<%= ProveedorGestionDDL.ClientID %>");
                //item.get_text();
                $('#<%= NombreTextBox.ClientID %>').val(item.get_text());
            }
        </script>
        <div class="bigField">
            <%-- 
            <RedSalud:TagSelector runat="server" ID="NombreTextBox" MaxTags="1" readonly="readonly" />
            --%>
        </div>
        <asp:TextBox runat="server" ID="NombreTextBox" CssClass="text-readonly" Width="438px" style="margin-top: 10px;" />
        <div class="validation">
            <span id="NombreRFV" runat="server" style="color:Red;display:none;">El valor es incorrecto</span>
        </div>

        <span class="label">Fecha de Visita del Inspector y/o Médico Auditor:</span>
        <telerik:RadDatePicker runat="server" ID="FechaVisitaRDP" 
            CssClass="normalField" Width="200px">
        </telerik:RadDatePicker>
        <div class="validation">
            <asp:RequiredFieldValidator ID="FechaVisitaRFV" runat="server"
                ControlToValidate="FechaVisitaRDP"
                ErrorMessage="Debe seleccionar la fecha de visita."
                Display="Dynamic"
                ValidationGroup="step3">
            </asp:RequiredFieldValidator>
        </div>

        <div id="ReservaDiv" runat="server">
            <span class="label">Reserva estimada de Siniestro:</span>
            <asp:TextBox runat="server" ID="ReservaGMTextBox"
                CssClass="normalField" />
            <div class="validation">
                <asp:RequiredFieldValidator ID="ReservaGMRFV" runat="server"
                    ControlToValidate="ReservaGMTextBox"
                    ErrorMessage="Debe ingresar un monto de reserva."
                    Display="Dynamic"
                    ValidationGroup="step3">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator 
                ID="ReservaGMCV" 
                runat="server"
                ControlToValidate="ReservaGMTextBox"
                ErrorMessage="El monto de la reserva no puede exceder la cobertura"
                ControlToCompare="CoberturaGMTextBox"
                Operator="LessThanEqual"
                Type="Double"                   
                ValidationGroup="step3"
                CultureInvariantValues="false"         
                ></asp:CompareValidator>
                <asp:CompareValidator ID="compareValues"
                    ControlToValidate="CoberturaGMTextBox"
                    Operator="DataTypeCheck"
                    ErrorMessage="Invalid Data Type"
                    ValidationGroup="step3"
                    Type="Double"
                    CultureInvariantValues="false"
                    runat="server">
                </asp:CompareValidator>
            </div>
            <span class="label">Cobertura:</span>
            <asp:TextBox runat="server" ID="CoberturaGMTextBox"
                CssClass="normalField disable" /> <!--valDecimal-->            
            
        </div>

        <span class="label">Grado del Accidente:</span>
        <asp:DropDownList runat="server" ID="GradoDDL"
            CssClass="normalField">
            <asp:ListItem Text="LEVE" Selected="True" />
            <asp:ListItem Text="MODERADO" />
            <asp:ListItem Text="GRAVE" />
            <asp:ListItem Text="MUY GRAVE" />
            <asp:ListItem Text="FATAL" />
        </asp:DropDownList>

        <span class="label">Observaciones:</span>
        <asp:TextBox runat="server" ID="DiagnosticoPreliminarTextBox" TextMode="MultiLine"
            CssClass="biggerField" />
        <script type="text/javascript">
            $(document).ready(function () {
                $('#<%=NextWizardLB.ClientID%>').click(function () {
                    try {
                        var combo = $find('<%= ProveedorGestionDDL.ClientID %>');
                        //alert(combo.get_selectedItem().get_value());
                        var InsertFlag = $('#<%=GestionMedicaIdHF.ClientID %>').val();
                        if ((combo.get_selectedItem() == null && (InsertFlag == "NEW" || InsertFlag == "FIRST")) ||
                            ($('#<%=NombreTextBox.ClientID%>').val().length <= 0 && InsertFlag != "NEW" && InsertFlag != "FIRST")) {
                            $('#<%=NombreRFV.ClientID%>').show();
                            return false;
                        } else {
                            $('#<%=NombreRFV.ClientID%>').hide();
                            return true;
                        }
                    } catch (q) {
                        $('#<%=NombreRFV.ClientID%>').show();
                        return false;
                    }
                });
            });
        </script>
    </asp:Panel>
    <div class="buttonsPanel">
        <asp:LinkButton ID="PreevWizardLB" runat="server"
            CommandName="preev"
            CssClass="button" OnClick="NextWizardLB_Click">
            <asp:label ID="PreevWizardLabel" text="Anterior" ForeColor="#FFFFFF" runat="server" />
        </asp:LinkButton>
        <asp:HiddenField ID="SaveHF" runat="server" Value="" />
        <asp:LinkButton ID="NextWizardLB" runat="server"
            CommandName="next" OnClientClick="return NextWizardLB_Click();"
            CssClass="button" OnClick="NextWizardLB_Click">
            <asp:label ID="NextWizardLabel" text="" ForeColor="#FFFFFF" runat="server" />
        </asp:LinkButton>
    </div>
    <asp:Panel ID="NewGastoPanel" runat="server" ToolTip="" style="display: none;"
        DefaultButton="NGSaveLB">
        <div id="NGPFecha">
            <span class="label">Fecha:</span>
            <telerik:RadDatePicker runat="server" ID="NGFechaRDP" TabIndex="-1" Enabled="false"
                CssClass="normalField" Width="200px" DatePopupButton-Visible="false" 
                SelectedDate='<%# Artexacta.App.Configuration.Configuration.ConvertToClientTimeZone(DateTime.UtcNow) %>'>
            </telerik:RadDatePicker>
        </div>
        <div id="ProformaDiv" style="display:none;">
            <span class="label">Proforma:</span>
            <asp:DropDownList runat="server" ID="NGPreliquidacionDDL"
                CssClass="normalField"></asp:DropDownList>
        </div>
        <span class="label">Tipo de Gasto:</span>
        <asp:DropDownList runat="server" ID="NGTipoGastoDDL"
            CssClass="normalField">
            <asp:ListItem Text="Reserva" Value="RESERVA" />
            <asp:ListItem Text="Gastos Hospitalarios" Value="HOSPITALARIOS" />
            <asp:ListItem Text="Gastos de Cirugías" Value="CIRUGIA" />
            <asp:ListItem Text="Gastos de Ambulancias" Value="AMBULANCIAS" />
            <asp:ListItem Text="Gastos de Laboratorios e Imágenes" Value="LABORATORIOS" />
            <asp:ListItem Text="Gastos de Farmacia" Value="FARMACIAS" />
            <asp:ListItem Text="Gastos de Honorarios Profesionales" Value="HONORARIOS" />
            <asp:ListItem Text="Gastos de Ambulatorios" Value="AMBULATORIO" />
            <asp:ListItem Text="Gastos de Material de Osteosíntesis" Value="OSTEOSINTESIS" />
            <asp:ListItem Text="Gastos de Reembolso" Value="REEMBOLSO" />
        </asp:DropDownList>
        <div id="ProveedorDiv">
            <span class="label">Proveedor</span>
            <div class="normalField">
                <RedSalud:TagSelector runat="server" ID="ProveedorTags" CssClass="normalField" MaxTags="1"
                    Type="Proveedor" />
                <asp:TextBox ID="ProveedorDisable" runat="server" Enabled="false" />
                <asp:HiddenField runat="server" ID="ProveedorHF" />
            </div>
            <div class="validation">
                <span id="ProveedorTagsVal" style="display:none;color:#F00;">
                    Debe seleccionar el Proveedor.
                </span>
            </div>
        </div>
        <div id="fechaReciboFacturaDiv">
            <span id="fechaReciboFacturaLabel" class="label"></span>
            <telerik:RadDatePicker runat="server" ID="FechaReciboFacturaRDP" TabIndex="-1"
                CssClass="normalField" Width="200px">
            </telerik:RadDatePicker>
            <div class="validation">
                <span id="FechaReciboFacturaRFV" style="display:none;color:#F00;">Debe seleccionar la fecha.</span>
            </div>
        </div>
        
        <div id="NumeroReciboFacturaDiv">
            <span id="NumeroReciboFacturaLabel" class="label">:</span>
            <asp:TextBox runat="server" ID="NumeroReciboFacturaTextBox"
                CssClass="normalField" />
            <div class="validation">
                <span id="NumeroReciboFacturaRFV" style="display:none;color:#F00;">Debe ingresar el Número.</span>
            </div>
        </div>
        <div id="isFacturaDiv" style="margin-top: 5px;font-weight: bold; text-transform: uppercase;">
            <asp:CheckBox ID="IsFacturaCheckBox" Text="Es Factura" runat="server" />
        </div>

        <span class="label">Monto:</span>
        <asp:TextBox runat="server" ID="NGMontoTextBox"
            CssClass="normalField valDecimal" />
        <div class="validation">
            <asp:RequiredFieldValidator ID="NGMontoRFV" runat="server"
                ControlToValidate="NGMontoTextBox"
                ErrorMessage="Debe ingresar el monto del gasto."
                Display="Dynamic"
                ValidationGroup="stepNG">
            </asp:RequiredFieldValidator>
            <asp:CustomValidator ID="NGMontoREV" runat="server"
                ControlToValidate="NGMontoTextBox"
                ErrorMessage="Debe ingresar el monto del gasto."
                ClientValidationFunction="validationMonto"
                Display="Dynamic"
                ValidationGroup="stepNG">
            </asp:CustomValidator>
        </div>

        <div id="EstadoDiv" style="display:none;">
            <span class="label">Estado:</span>
            <asp:DropDownList runat="server" ID="EstadoDDL"
                CssClass="normalField">
                <asp:ListItem Selected="True" Text="APROBADO" Value="1" />
                <asp:ListItem Text="OBSERVADO" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="buttonsPanel">
            <div id="CopyToGastosDiv" style="display:none; margin-bottom: 20px;text-align:right;">
                <asp:HyperLink ID="CopyToGastosHL" Text="Copiar a Gastos" NavigateUrl="javascript:copyToGastos();" runat="server" />
            </div>
            <asp:LinkButton ID="NGSaveLB" runat="server"
                ValidationGroup="stepNG" OnClientClick="return validate_NGSaveLB();"
                CssClass="button" OnClick="NGSaveLB_Click">
                <span style="color: #FFF;">Guardar</span>
            </asp:LinkButton>
            <asp:HyperLink Text="Cancelar" NavigateUrl="javascript:CloseNGDialog();" runat="server" />
        </div>
        <asp:HiddenField runat="server" ID="TypeHF" />
        <asp:HiddenField runat="server" ID="saveIdHF" />
        <asp:HiddenField runat="server" ID="saveGastoFacturadoId" />
        <asp:HiddenField runat="server" ID="PreliquidacionDetalleIdHF" />
    </asp:Panel>
    <asp:Panel ID="SelectPolizaPanel" runat="server" ToolTip="SELECCIONE PÓLIZA A CARGAR" style="display: none;">
        <span class="label">PÓLIZAS:</span>
        <asp:DropDownList runat="server" ID="PolizasAliazaDDL"
            CssClass="normalField">
        </asp:DropDownList>
        <div class="buttonsPanel">
            <asp:LinkButton ID="SPSaveLB" runat="server"
                CssClass="button" OnClientClick="return SPSaveLB_Click();">
                <span style="color: #FFF;">SELECCIONAR</span>
            </asp:LinkButton>
            <asp:HyperLink Text="Cancelar" NavigateUrl="javascript:CloseSPDialog();" runat="server" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
        function NextWizardLB_Click() {
            if ($('#<%=StepHF.ClientID%>').val() == '1') {
                var numeroSiniestro = $('#<%=OperacionesIdTextBox.ClientID%>').val().toUpperCase();
                if (!numeroSiniestro.match(/[0-9]{4}-[0-9]{4}-(SCZ|LPZ|PT|CBBA|CH|TJA|MONT|ALTO|OR|BE|PDO|RB)$/)) {
                    return !confirm('El formato del numero del siniestro no parece estar correcto.\n¿Desea revisarlo? (Ejemplo: 0001-2015-SCZ)');
                }
            }
            return true;
        }
        var AddedValues = 0;
        function loadPreliquidacionDetalleId() {
            var ddl = $('#<%=NGPreliquidacionDDL.ClientID%>');
            var clear = true;
            $('td.PreliquidacionDetalle').each(function () {
                if (clear) {
                    ddl.html('');
                    clear = false;
                }
                var list = $(this).children('input[type=hidden]').val().split('#;#');
                for (var i = 0; i < list.length; i++) {
                    var value = list[i];
                    if (value != '') {
                        var text = value.split('###')[2];
                        var proveedor = value.split('###')[1];
                        value = value.split('###')[0];
                        var newOption = '<option value="' + value + '" proveedor="' + proveedor + '">'
                            + text + '</option>';
                        ddl.prepend(newOption);
                    }
                }
            });
            ddl.change(function () {
                var proveedor = $(this).children("option:selected").attr('proveedor');
                if (proveedor == undefined) {
                    var first = $(this).children().first();
                    proveedor = $(first).attr('proveedor');
                    $(this).val($(first).attr('value'));
                }
                $('#<%=ProveedorTags.ClientID%>').hide();
                $('#<%=ProveedorDisable.ClientID%>').show();
                $('#<%=ProveedorDisable.ClientID%>').val(proveedor);
                $('#<%=ProveedorHF.ClientID%>').val(proveedor);
                $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val($('#<%=NGPreliquidacionDDL.ClientID%>').val());

            }).change();
        }
        function validate_NGSaveLB() {
            if ($('#<%=NGTipoGastoDDL.ClientID%>').val() == 'RESERVA') {
                return true;
            }

            $('#FechaReciboFacturaRFV').hide();
            $('#NumeroReciboFacturaRFV').hide();
            $('#ProveedorTagsVal').hide();
            var valid = true;
            if ($find('<%=FechaReciboFacturaRDP.ClientID%>').isEmpty()) {
                $('#FechaReciboFacturaRFV').show();
                valid = false;
            }
            if ($('#<%=NumeroReciboFacturaTextBox.ClientID%>').val() == '') {
                $('#NumeroReciboFacturaRFV').show();
                valid = false;
            }
            var tags = <%=ProveedorTags.ClientID%>getSelectedTags();
            if (tags == undefined || tags == null) {
                tags = '';
            }

            if (tags == '' && $('#<%=ProveedorDisable.ClientID%>').val() == '') {
                $('#ProveedorTagsVal').show();
                valid = false;
            }
            return valid;
        }
        function validationMonto(source, arguments) {
            if ($('#<%= NGMontoTextBox.ClientID%>').val() > 0) {
                arguments.IsValid = true;
            } else {
                arguments.IsValid = false;
            }
        }
        function OpenNGDialog(Id, GE, AccidentadoId, type) {
            try {
                
                if ((type == 'R') && (Id <= 0)) {
                    if ($('#<%=NGPreliquidacionDDL.ClientID%>').children().size() <= 0) {
                        alert('Todas las proformas tienen factura.');
                        return false;
                    }
                }
                if (AddedValues > 0) {
                    $("#<%=NGPreliquidacionDDL.ClientID%> option[value='" + AddedValues + "']").remove();
                }

                $('#NGPFecha').show();
                $('#ProveedorDiv').show();
                $('#<%=ProveedorTags.ClientID%>').show();
                $('#<%=ProveedorDisable.ClientID%>').hide();
                $('#<%=NGTipoGastoDDL.ClientID%>').prop('disabled', '');
                $('#<%=NGPreliquidacionDDL.ClientID%>').val('');
                $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val('');
                $('#<%=ProveedorDisable.ClientID%>').val('');
                $('#<%=ProveedorHF.ClientID%>').val('');
                var proveedorTags = $('#<%=ProveedorTags.ClientID%>');
                proveedorTags.tagit("fill", '');
                var DatePicker = $find('<%=NGFechaRDP.ClientID%>');
                $('#<%=TypeHF.ClientID%>').val(type);
                var res = $('#<%=NGTipoGastoDDL.ClientID%>').find('option[value="RESERVA"]');
                $('#CopyToGastosDiv').hide();
                if (type == 'R') {
                    $("#<%=NGTipoGastoDDL.ClientID%> option").removeAttr('selected');
                    if ($(res).length > 0) $(res).remove();
                    $('#fechaReciboFacturaLabel').html('Fecha de Recepción');
                    $('#NumeroReciboFacturaLabel').html('Número de la Factura');
                    $('#isFacturaDiv').hide();
                    $('#EstadoDiv').hide();
                    $('#ProformaDiv').show();
                    loadPreliquidacionDetalleId();
                } else if (type == 'RES') {
                    if ($(res).length <= 0) {
                        var newOption = '<option value="RESERVA" selected="selected">Reserva</option>';
                        $('#<%=NGTipoGastoDDL.ClientID%>').prepend(newOption);
                    }
                    $('#<%=NGTipoGastoDDL.ClientID%>').prop('disabled', 'disabled');
                    $('#NGPFecha').hide();
                    $('#ProformaDiv').hide();
                } else {
                    if ($(res).length > 0) $(res).remove();
                    $('#<%=NGTipoGastoDDL.ClientID%> option').first().attr('selected', 'selected');
                    $('#fechaReciboFacturaLabel').html('Fecha de Recepción');
                    $('#NumeroReciboFacturaLabel').html('Número Recibo / Factura');
                    $('#isFacturaDiv').show();
                    //$('#EstadoDiv').show();
                    $('#ProformaDiv').hide();
                }
                var title = "";
                var DatePicker2 = $find('<%=FechaReciboFacturaRDP.ClientID%>');
                if (Id <= 0) {
                    title = (type == 'R' ? 'Nuevo Gasto Facturado' : 'Nuevo Gasto Medico');
                    DatePicker.set_selectedDate(new Date());
                    DatePicker2.set_selectedDate(new Date());
                    $('#<%=NumeroReciboFacturaTextBox.ClientID%>').val('');
                    $('#<%=NGMontoTextBox.ClientID%>').val('0.00');
                    $('#<%=saveIdHF.ClientID%>').val('0');
                    $('#<%=EstadoDDL.ClientID%>').val('1');
                    //$('<=NGFechaRDP.ClientID%>').removeClass('disable');
                } else {
                    $('#<%=saveIdHF.ClientID%>').val(Id);
                    title = (type == 'R' ? 'Modificar Gasto Facturado' : 'Modificar Proforma');
                    if (type == 'G') {
                        if ($('#<%=NGPreliquidacionDDL.ClientID%> option[value=' + Id + ']').length > 0) {
                            $('#CopyToGastosDiv').show();
                        }
                    }
                    var date = $(GE).attr('fecha').split('/');
                    DatePicker.set_selectedDate(new Date(date[2], date[1] - 1, date[0]));
                    date = $(GE).attr('fechaReciboFactura').split('/');
                    DatePicker2.set_selectedDate(new Date(date[2], date[1] - 1, date[0]));
                    addPreliquidacionText($(GE).attr('preliquidacionId'));
                    $('#<%= NumeroReciboFacturaTextBox.ClientID%>').val($(GE).attr('NumeroReciboFactura'));
                    $('#<%=IsFacturaCheckBox.ClientID%>').prop('checked', $(GE).attr('isFactura') == 'True');
                    $('#<%=NGTipoGastoDDL.ClientID%>').val($(GE).attr('tipo'));
                    $('#<%=NGMontoTextBox.ClientID%>').val(parseInt($(GE).attr('monto')).toFixed(2));
                    $('#<%=EstadoDDL.ClientID%>').val($(GE).attr('estado') == 'True' ? '1' : '0');
                    if (type == 'R') {
                        $('#<%=NGPreliquidacionDDL.ClientID%>').val($(GE).attr('preliquidacionId'));
                        $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val($(GE).attr('preliquidacionId'));
                        $('#<%=ProveedorDisable.ClientID%>').val($(GE).attr('proveedor'));
                        $('#<%=ProveedorHF.ClientID%>').val($(GE).attr('proveedor'));
                    } else {
                        proveedorTags.tagit("fill", [{ label: $(GE).attr('proveedor'), value: $(GE).attr('proveedor') }]);
                        proveedorTags.next().val($(GE).attr('proveedor'));
                    }
                    //$('<=NGFechaRDP.ClientID%>').addClass('disable');
                }
                if (AccidentadoId) {
                    $('#<%=AccidentadoIdHF.ClientID%>').val(AccidentadoId);
                }
                if (type == 'RES') {
                    title = 'Modificar Reserva';
                    $('#fechaReciboFacturaDiv').hide();
                    $('#NumeroReciboFacturaDiv').hide();
                    $('#ProveedorDiv').hide();
                    $('#isFacturaDiv').hide();
                    $('#EstadoDiv').hide();
                    var monto = $('#<%=ReservaAccidentadoTextBox.ClientID%>').val();
                        var sepMil = monto.charAt(monto.length - 3) == ',' ? '.' : ',';
                        $('#<%= NGMontoTextBox.ClientID %>').val(monto.replace(sepMil, '').replace(',', '.'));
                }
                $('#<%=NewGastoPanel.ClientID%>').dialog({ modal: true, resizable: false });
                $('#<%=NewGastoPanel.ClientID%>').dialog("option", "title", title);
                $('.ui-widget-overlay').height($(document).height());
                $('form').append($('.ui-dialog'));
                $('.validation span').hide();
            } catch (q) { console.log(q);}
            return false;
        }
        function addPreliquidacionText(preliquidacionId) {
            var ddl = $('#<%=NGPreliquidacionDDL.ClientID%>');
            $('td.PreliquidacionDetalleId').each(function () {
                if ($(this).html() == preliquidacionId) {
                    var isFactura = $(this).next().next().next().next().next().next().html() == 'SI';
                    var monto = $(this).next().next().next().next().next().next().next().html();
                    var text = (isFactura ? '' : '') + $(this).next().next().next().next().next().html() +
                                ' (' + monto + ')';
                    var proveedor = $(this).next().next().next().html();
                    var newOption = '<option value="' + preliquidacionId + '" proveedor="' + proveedor + '">'
                        + text + '</option>';
                    ddl.prepend(newOption);
                    AddedValues = preliquidacionId;
                    return false;
                }
            });
        }
        function copyToGastos() {
            $('#CopyToGastosDiv').hide();
            $('span.ui-dialog-title').html('Nuevo Gasto Facturado');
            $('#<%=TypeHF.ClientID%>').val('R');
            $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val($('#<%=saveIdHF.ClientID%>').val());
            $('#<%=saveIdHF.ClientID%>').val('');
            var res = $('#<%=NGTipoGastoDDL.ClientID%>').find('option[value="RESERVA"]');
            if ($(res).length > 0) $(res).remove();
            $('#fechaReciboFacturaLabel').html('Fecha de la Factura');
            $('#NumeroReciboFacturaLabel').html('Número de la Factura');
            $('#isFacturaDiv').hide();
            $('#EstadoDiv').hide();
        }
        function ShowNGDialog() {
            $('#<%=NewGastoPanel.ClientID%>').dialog({ modal: true, resizable: false });
            $('.ui-widget-overlay').height($(document).height());
            $('form').append($('.ui-dialog'));
        }
        function CloseNGDialog() {
            $('#<%=NewGastoPanel.ClientID%>').dialog('destroy');
        }
        function ShowSPDialog() {
            $('#<%=SelectPolizaPanel.ClientID%>').dialog({ modal: true, resizable: false });
            $('.ui-widget-overlay').height($(document).height());
            $('form').append($('.ui-dialog'));
        }
        function CloseSPDialog() {
            $('#<%=SelectPolizaPanel.ClientID%>').dialog('destroy');
        }
        function SPSaveLB_Click() {
            loadPoliza($('#<%=PolizasAliazaDDL.ClientID%>').val().split('#;#'));
            CloseSPDialog();
            return false;
        }
        function validatePreev(valGroup) {
            if (somethingChanged) {
                if (confirm('¿Desea salvar los cambios antes de ir atrás?')) {
                    $('#<%= SaveHF.ClientID %>').val('1');
                    return;
                } else {
                    $('#<%= SaveHF.ClientID %>').val('');
                }
            }
            for (i = 0; i < Page_Validators.length; i++) {
                if (Page_Validators[i].validationGroup == valGroup) {
                    ValidatorEnable(Page_Validators[i], false);
                }
            }
        }
    </script>
    <asp:HiddenField runat="server" ID="StepHF" Value="1" />
    <asp:HiddenField runat="server" ID="SiniestroIDHF" Value="" />
    <asp:HiddenField runat="server" ID="AccidentadoIdHF" Value="" />
    <asp:HiddenField runat="server" ID="GestionMedicaIdHF" Value="" />
    <asp:HiddenField runat="server" ID="IsSavedHF" Value="0" />
    <asp:HiddenField runat="server" ID="IsFinishedHF" Value="0" />
</asp:Content>