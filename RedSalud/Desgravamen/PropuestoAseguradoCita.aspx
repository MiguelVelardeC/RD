<%@ Page Title="Cita para Propuesto Asegurado" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropuestoAseguradoCita.aspx.cs" Inherits="Desgravamen_PropuestoAseguradoCita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .estudiosComplementarios{
            -moz-column-count: 2;
            -moz-column-gap: 15px;
            -webkit-column-count: 2;
            -webkit-column-gap: 15px;
            column-count: 2;
            column-gap: 15px;
        }

        .estudiosComplementarios span
        { 
            display: block;
            margin: 5px 0px;
        }
        #estudioGrupos
        {
            margin: 10px 0px;
        }
        #estudioGrupos span
        {
            margin-right: 10px;
            display: inline-block;
        }
        #estudiosContainer
        {
            margin: 10px 0px;
            display:none;
        }
        #tabs { 
            padding: 0px; 
        }
        #tabs a {
            text-decoration:none !important;
            color: #212121 !important;
        } 
        #tabs .ui-tabs-nav { 
            background: transparent; 
            border-width: 0px 0px 1px 0px; 
            -moz-border-radius: 0px; 
            -webkit-border-radius: 0px; 
            border-radius: 0px; 
        } 
        #tabs .ui-tabs-panel { 
            margin: 0em 0.2em 0.2em 0.2em; 
        }
        #tabs .ui-tabs-nav li {
            padding-top: 7px;
            padding-bottom: 5px;
            background: none;
        } 
        #tabs .ui-tabs-nav li.ui-state-active { 
            background-color: #b2b2b2;
        }
        .mensajeIncompleto {
            border:1px solid #808080; background-color: #ffd800; width:80%; margin: 10px auto; padding: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span id="LblPropuestoAseguradoTitle" runat="server" class ="title">Cita del Propuesto Asegurado</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/PropuestoAseguradoLista.aspx"
                        Text="Volver a la Lista de Propuestos Asegurado">
                    </asp:HyperLink>
                </div>
                <div ID="MensajeDeIncompleto" Class="mensajeIncompleto">
                    Debe escoger una Ciudad para elegir algun estudio
                </div>
                <span class="label">Nombre</span>
                <asp:TextBox ID="NombreTextBox" runat="server" CssClass="bigField" ReadOnly="true"></asp:TextBox>

                <span class="label">Cédula de Identidad</span>
                <asp:TextBox ID="CedulaTextBox" runat="server" CssClass="normalField" ReadOnly="true"></asp:TextBox>

                <span class="label">Fecha de Nacimiento</span>
                <asp:TextBox ID="FechaNacimientoTextBox" runat="server" CssClass="normalField" ReadOnly="true"></asp:TextBox>

                <span class="label">Tipo de Producto</span>
                <asp:DropDownList ID="TipoProductoCombBox" runat="server">
                    <%-- 
                    <asp:ListItem Text="Desgravamen" Value="DESGRAVAMEN" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Vida Individual" Value="INDIVIDUAL"></asp:ListItem>
                        --%>
                </asp:DropDownList>

                <span class="label">Referencia</span>
                <asp:TextBox ID="ReferenciaTextBox" runat="server" TextMode="MultiLine" Rows="5" CssClass="biggerField"></asp:TextBox>

                <span class="label">Ciudad</span>
                <asp:DropDownList ID="CiudadComboBox" runat="server" AutoPostBack="true"
                    DataTextField="Nombre"
                    DataValueField="CiudadId"
                    DataSourceID="CiudadDataSource"
                    OnDataBound="CiudadComboBox_DataBound">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CiudadDataSource" runat="server"
                    OnSelected="CiudadDataSource_Selected"
                    TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                    SelectMethod="getCiudadListDesgravamen">                   
                    <SelectParameters>                       
                        <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="intClienteId" Type="Int32" />      
                        <%--
                        <asp:Parameter Name="ciudadesExistentes" Type="String" DefaultValue="ALT,COB,MON,ORU,PTS,SCR,TRI,TRJ" />
                        --%>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div class="validation">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CiudadComboBox"
                        ErrorMessage="Debe seleccionar una Ciudad" ValidationGroup="Cita"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <asp:Panel ID="EntidadFinancieraPanel" runat="server">
                    <span class="label">Entidad Financiera</span>
                    <asp:DropDownList ID="EntidadFinancieraComboBox" runat="server"
                        DataSourceID="EntidadFinancieraDataSource" CssClass="bigField"
                        OnDataBound="EntidadFinanciearComboBox_DataBound"
                        DataTextField="Nombre"
                        DataValueField="FinancieraId">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="EntidadFinancieraDataSource" runat="server"
                        OnSelected="EntidadFinancieraDataSource_Selected"
                        TypeName="Artexacta.App.Desgravamen.BLL.FinancieraBLL"
                        SelectMethod="GetFinancieras">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="clienteId" Type="Int32" />
                            <asp:ControlParameter ControlID="CiudadComboBox" PropertyName="SelectedValue" Name="varCiudad" Type="String" />                             
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <div class="validation">
                        <asp:CustomValidator id="CustomValidatorFinanciera"
                           ClientValidationFunction="ValidarSeleccionFinanciera"
                           ValidationGroup="Cita"
                           Display="Dynamic"
                           ErrorMessage="Si el prducto es desgravamen debe elegir una entidad"
                           runat="server"/>
                    </div>
                </asp:Panel>

                

                <asp:CheckBox ID="FacturacionPrivadaChk" runat="server" Text="Facturación Privada" CssClass="label" />

                <asp:CheckBox ID="NecesitaExamen" runat="server" Text="Necesita examen médico" CssClass="label" />

                <asp:CheckBox ID="NecesitaEstudiosChk" runat="server" Text="Necesita estudios complementarios" CssClass="label" />
                <div class="validation">
                    <asp:CustomValidator id="CustomValidateNecesita"
                       ClientValidationFunction="ValidarAlMenosUnNecesita"
                       ValidationGroup="Cita"
                       Display="Dynamic"
                       ErrorMessage="Debe estar marcado que necesita un examen o estudios"
                       runat="server"/>
                </div>

                <div id="estudiosContainer">
                    <span>Elegir los estudios complementarios que se requieren:</span>
                

                    <asp:Repeater ID="GrupoEstudioRepeater" runat="server"
                        DataSourceID="GrupoEstudioDataSource">
                        <HeaderTemplate>
                            <div id="estudioGrupos">
                                <a href="#" id="desmarcarTodo">Desmarcar todo</a>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="GrupoChk" runat="server" Text='<%# Eval("NombreGrupo") %>' data-id='<%# Eval("EstudioGrupoId") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:ObjectDataSource ID="GrupoEstudioDataSource" runat="server"
                        TypeName="Artexacta.App.Desgravamen.BLL.EstudioBLL"
                        SelectMethod="GetEstudioGrupo"
                        OnSelected="GrupoEstudioDataSource_Selected">
                    </asp:ObjectDataSource>

                    <div id="tabs">

                        <asp:Repeater id="Repeater1" runat="server"
                            DataSourceID="EstudiosPadresDataSources"
                            OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Literal ID="EstudioGrupoLabel" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:ObjectDataSource ID="EstudiosPadresDataSources" runat="server"
                            TypeName="Artexacta.App.Desgravamen.BLL.EstudioBLL"
                            SelectMethod="GetEstudiosPadres"
                            OnSelected="EstudiosPadresDataSources_Selected">
                        </asp:ObjectDataSource>

                        <asp:Repeater id="EstudiosRepeater" runat="server"
                            DataSourceID="EstudiosDataSources" 
                            OnItemDataBound="EstudiosRepeater_ItemDataBound"
                            OnDataBinding="EstudiosRepeater_DataBinding">
                            <HeaderTemplate>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                <asp:Literal ID="headerItemTemplateEstudios" runat="server"></asp:Literal>
                                
                                <asp:CheckBox ID="GrupoChk" runat="server" Text='<%# Eval("NombreEstudio") %>'
                                    CssClass='<%# Eval("EstudioGrupoIdsForDisplay") %>'
                                    data-id='<%# Eval("EstudioId") %>' />

                                <asp:Literal ID="footerItemTemplateEstudios" runat="server"></asp:Literal>

                            </ItemTemplate>
                                
                            <FooterTemplate>
                                </div></div>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:ObjectDataSource ID="EstudiosDataSources" runat="server"
                            TypeName="Artexacta.App.Desgravamen.BLL.EstudioBLL"
                            SelectMethod="GetEstudios"
                            OnSelected="ObjectDataSource1_Selected">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="intClienteId" Type="Int32" />                                    
                                <asp:ControlParameter ControlID="CiudadComboBox" PropertyName="SelectedValue" Name="varCiudad" Type="String" /> 
                                <asp:Parameter Name="boolDeshabilitado" DefaultValue="false" Type="Boolean" />   
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
                <asp:HiddenField ID="EstudiosIdsHiddenField" runat="server" Value="[]" />
                <div class="validation">
                    <asp:CustomValidator runat="server" Display="Dynamic"
                        ValidationGroup="Cita" ClientValidationFunction="ValidateEstudios"
                        ErrorMessage="Debe seleccionar al menos un Estudio">
                    </asp:CustomValidator>
                </div>

                <div class="buttonsPanel">

                    <asp:LinkButton ID="SaveButton" runat="server" ValidationGroup="Cita" CssClass="button"
                        OnClick="SaveButton_Click">
                        <span>Guardar</span>
                    </asp:LinkButton>

                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/PropuestoAseguradoLista.aspx" CssClass="secondaryButton"
                        Text="Cancelar">
                    </asp:HyperLink>
                    <asp:HiddenField ID="PropuestoAseguradoIdHiddenField" runat="server" Value="0" />
                    <asp:HiddenField ID="CitaDesgravamenIdHiddenField" runat="server" Value="0" />                    
                    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />

                    <asp:HiddenField ID="CiudadSeleccionada" runat="server" />
                    <asp:HiddenField ID="FinanciaeraSeleccionada" runat="server" />
                    <asp:HiddenField ID="EstudiosSeleccionados" runat="server" Value="[]" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        function ValidarSeleccionFinanciera(source, arguments) {
            var tipoProducto = $('select[name*=TipoProductoCombBox]').val();
            var entidad = $('select[name*=EntidadFinancieraComboBox]').val();

            if (tipoProducto == 'DESGRAVAMEN' && !entidad) {
                arguments.IsValid = false;
            } else {
                arguments.IsValid = true;
            }
        }

        function ValidarAlMenosUnNecesita(source, arguments) {
            if ($('input[id*=NecesitaExamen]').is(':checked') ||
                $('input[id*=NecesitaEstudiosChk]').is(':checked')) {
                arguments.IsValid = true;
                return;
            }
            arguments.IsValid = false;
        }

        $(document).ready(function () {
            if ($("#<%= CiudadComboBox.ClientID %>").val() != "" &&
                $("#<%= CiudadComboBox.ClientID %>").val() != null) {
                $("#MensajeDeIncompleto").hide();
            }


            $("#<%= CiudadComboBox.ClientID %>").change(function () {
                if (!$(this).val() == "" || !$(this).val() != null) {
                    $("#MensajeDeIncompleto").hide();
                }
            });



            $("#tabs").tabs();
            
            $('#<%= TipoProductoCombBox.ClientID %>').change(function () {
                if ($(this).val() != 'DESGRAVAMEN') {
                    $('#<%= EntidadFinancieraPanel.ClientID %>').hide();
                } else {
                    $('#<%= EntidadFinancieraPanel.ClientID %>').show();
                }
            });

            if ($('#<%= TipoProductoCombBox.ClientID %>').val() != 'DESGRAVAMEN') {
                $('#<%= EntidadFinancieraPanel.ClientID %>').hide();
            } else {
                $('#<%= EntidadFinancieraPanel.ClientID %>').show();
             }

            $("#desmarcarTodo").click(function () {
                $('.estudiosComplementarios input').each(function () {
                    $(this).prop("checked", "");
                });

                $('#estudioGrupos input').each(function () {
                    $(this).prop("checked", "");
                });

                return false;
            });

            $("#estudioGrupos input[type='checkbox']").click(function () {
                if (!$(this).is(":checked"))
                    return;
                var classToSelect = $(this).parent().data("id");
                $(".e" + classToSelect + "  input[type='checkbox']").each(function () {
                    if (!$(this).is(":checked"))
                        $(this).prop("checked", "checked");
                });
            });

            $("#<%= NecesitaEstudiosChk.ClientID %>").click(function () {
                if ($(this).is(":checked"))
                    $("#estudiosContainer").show();
                else
                    $("#estudiosContainer").hide();
            });

            $("#<%= SaveButton.ClientID %>").click(function () {
                var ids = [];
                $(".estudiosComplementarios input:checked").each(function () {
                    ids.push($(this).parent().data("id"));
                });
                $("#<%= EstudiosIdsHiddenField.ClientID %>").val(JSON.stringify(ids));
            });

            if($("#<%= NecesitaEstudiosChk.ClientID %>").is(":checked"))
                $("#estudiosContainer").show();

            var estudiosSeleccionados = JSON.parse($("#<%= EstudiosSeleccionados.ClientID %>").val());
            for (var i in estudiosSeleccionados) {
                var estudio = estudiosSeleccionados[i];
                var checkbox = $(".estudiosComplementarios span[data-id='" + estudio.EstudioId + "'] input[type='checkbox']");
                $(checkbox).prop("checked", "checked");                
            }
        });

        function ValidateEstudios(sender, args) {
            if (!$("#<%= NecesitaEstudiosChk.ClientID %>").is(":checked")) {
                args.IsValid = true;
                return;
            }

            args.IsValid = $(".estudiosComplementarios input:checked").length > 0;
        }

    </script>
</asp:Content>

