<%@ Page Title="Caso Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CasoMedicoLista.aspx.cs" Inherits="CasoMedico_CasoMedicoLista" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .CSearch .SearchTitle {
            font-weight: bold;
        }

        .sortable {
            cursor: pointer !important;
        }

        .sortCol {
            font-weight: bold !important;
            cursor: pointer !important;
        }

        .sortImg {
            float: right;
        }

        th.rgHeader > a {
            color: #333 !important;
        }

        .AdjuntosNumber {
            bottom: 3px !important;
            right: -4px !important;
            color: #333;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <asp:HiddenField ID="UserIdHidden" runat="server" Value="0" />
     <asp:HiddenField ID="MedicoUsuarioId" runat="server" Value="0" />
   <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" CssClass="title" />
            </div>
            <div class="columnContent">
                <%--                <div class="contentMenu">
                    <asp:HyperLink runat="server" ID="NewCasoHL"
                        Text="Agregar un nuevo Caso Medico"
                        NavigateUrl="~/CasoMedico/CasoMedicoRegistro.aspx" />
                </div>--%>
                <asp:Panel ID="Search" runat="server">
                    <asp:Panel ID="AdminPanel" runat="server" CssClass="PanelAdmin" Style="font-size: 12px;" DefaultButton="boton">
                        <a id="PanelButton" style="text-decoration: none;">
                            <h3 class="sectionTitle" style="background: #e1dddd;">
                                <span style="margin-left: 30px;">FILTROS DE BUSQUEDA</span>
                            </h3>
                        </a>
                        <div class="clear" style="margin-bottom: 7px;"></div>
                        <div id="Contents" style="padding: 1em 0.5em;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label Text="Cliente" runat="server"
                                            Style="width: 174px;"
                                            CssClass="label left" />
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ClienteDDL" runat="server"
                                            Style="width: 346px; height: 20px;"
                                            
                                            OnDataBound="ClienteDDL_DataBound">
                                        </asp:DropDownList>
                                       <%-- <asp:ObjectDataSource ID="ClienteODS" runat="server"
                                            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                                            OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="getRedClienteListTodos"
                                            OnSelected="ClienteODS_Selected"></asp:ObjectDataSource>
                                        --%><asp:CheckBox ID="checkTodosClientes" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="font-weight: bold">MEDICO:</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="medicoComboBox" runat="server"
                                            AutoPostBack="false"
                                            DataTextField="Nombre"
                                            DataValueField="MedicoId"
                                            Width="150px"
                                            Height="22px">                                            
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelCodCaso" Text="CÓDICO CASO:" style="font-weight: bold" runat="server">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="codigoCasoIdText" EmptyMessage="Código de Caso" runat="server" Width="250px" />
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelNroPoliza" Text="Nro Póliza:" style="font-weight: bold;" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="polizaText" runat="server" EmptyMessage="Número de Poliza" Width="250px"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelPaciente" runat="server" style="font-weight: bold" Text="PACIENTE:">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="pacienteNombreText" runat="server" EmptyMessage="Nombre Del Paciente" Width="250px"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <span style="font-weight: bold">CIUDAD: 
                                        </span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ciudadComboBox" runat="server"
                                            DataValueField="CiudadId"
                                            DataTextField="Nombre"
                                            Width="150px"
                                            Style="margin-left: 77px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: bold">RANGO DE FECHAS:
                                    </span>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="FechaInicio" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="177px"></telerik:RadDatePicker>
                                        <telerik:RadDatePicker ID="FechaFin" runat="server" DateInput-EmptyMessage="Fecha Final" Width="177px"></telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                            <asp:LinkButton ID="btnSearch" runat="server" CssClass="button" OnClick="boton_Click">
                    <span>BUSCAR</span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnExportExcel" runat="server" CssClass="button" OnClick="btnExportExcel_Click">
                    <span>EXPORTAR A EXCEL</span>
                            </asp:LinkButton>
                            <div class="clear" style="margin-bottom: 5px;"></div>
                        </div>

                         <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" OnClick="boton2_Click">
                    <span>mensaje de error</span>
                            </asp:LinkButton>--%>
                            
                        <asp:Button ID="boton" Text="" Style="display: none;" runat="server" OnClick="boton_Click" />
                    </asp:Panel>
                    <%--
                    <asp:Label ID="Label4" Text="Rango de fecha:" runat="server" CssClass="label" />
                    <div class="left">
                        <asp:Label Text="desde:" runat="server" />
                        <telerik:RadDatePicker ID="FechaInicio" runat="server"
                            EnableTyping="true"
                            ShowPopupOnFocus="true"
                            EnableShadows="true">
                        </telerik:RadDatePicker>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="DateRFV" runat="server"
                                ControlToValidate="FechaInicio"
                                ErrorMessage="debe seleccionar una fecha de inicio."
                                ValidationGroup="SearchCaso"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="left" style="margin-left: 10px;">
                        <asp:Label Text="Hasta" runat="server" />
                        <telerik:RadDatePicker ID="FechaFin" runat="server"
                            ShowPopupOnFocus="true" />
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="FechaFin"
                                ErrorMessage="debe seleccionar una fecha de Final."
                                ValidationGroup="SearchCaso"
                                Display="Dynamic" />
                            <asp:CustomValidator ID="FechaFinCV" runat="server"
                                ControlToValidate="FechaFin"
                                ErrorMessage="La fecha final debe ser mayor o igual a la fecha de inicio."
                                ValidationGroup="SearchCaso"
                                ClientValidationFunction="FechaFinCV_Validate"
                                Display="Dynamic" />
                        </div>
                    </div>
                    <div class="clear"></div>
                    <asp:Label Text="Buscar por Codigo Caso, Codigo Asegurado o nombre Asegurado" runat="server" CssClass="label" />
                    <asp:TextBox ID="SearchTexbox" runat="server" CssClass="biggerField" />

                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SearchLB" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="SearchCaso"
                            OnClick="SearchLB_Click">
                            <asp:Label ID="Label3" Text="Buscar" runat="server" />
                        </asp:LinkButton>
                    </div>--%>
                    <search:SearchControl ID="SearchCasoMedico" runat="server"
                        Title="Buscar Casos Médicos"
                        AdvancedSearchForm="~/UserControls/AdvancedSearch/CasoMedicoAdvancedSearch.ascx"
                        DisplayHelp="true"
                        DisplayContextualHelp="true"
                        CssSearchAdvanced="CSearch_Advanced_Panel"
                        CssSearch="CSearch"
                        CssSearchHelp="CSearchHelpPanel"
                        CssSearchError="CSearchErrorPanel"
                        SavedSearches="true"
                        SavedSearchesID="searchCtl_CasoMedicoList"
                        ImageHelpUrl="Images/Neutral/Help.png"
                        Visible="false"
                        ImageErrorUrl="~/images/exclamation.png" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                </asp:Panel>

                <telerik:RadGrid ID="CasoRadGrid" runat="server"
                    AutoGenerateColumns="false"
                    AllowSorting="true"
                    OnItemCommand="CasoRadGrid_ItemCommand"
                    OnItemDataBound="CasoRadGrid_ItemDataBound"
                    OnSortCommand="CasoRadGrid_SortCommand">
                    <SortingSettings EnableSkinSortStyles="false"></SortingSettings>
                    <MasterTableView DataKeyNames="CasoId">
                        <NoRecordsTemplate>
                            <asp:Label runat="server" Text="No existen Casos Medicos para el Cliente seleccionado."></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumnVer" ShowSortIcon="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ViewImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandArgument='<%# Bind("CasoId") %>'
                                        Width="24px" CommandName="View"
                                        ToolTip="Ver Caso Medico"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumnEditar" ShowSortIcon="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DetailsImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandArgument='<%# Bind("CasoId") %>'
                                        Width="24px" CommandName="Select"
                                        ToolTip="Editar Caso Medico"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumnHistorial" ShowSortIcon="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                <ItemTemplate>
                                    <div class="AdjuntosCont">
                                        <asp:LinkButton ID="HistorialImageButton" runat="server"
                                            CommandArgument='<%# Eval("CasoId") + ";" + Eval("PacienteId") %>'
                                            Width="24px" CommandName="Historial"
                                            ToolTip="Ver Historial del Paciente">
                                            <asp:Image ImageUrl="~/Images/Neutral/historialMedico.png" runat="server" />
                                            <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("HistoriaCount") %>' runat="server" ></asp:Label>
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumnReconsulta" ShowSortIcon="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ReconsultaImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/reConsulta.png"
                                        CommandArgument='<%# Bind("CasoId") %>'
                                        Width="24px" CommandName="Reconsulta"
                                        ToolTip="Reconsulta"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumnGastos" ShowSortIcon="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="GastoIB" runat="server"
                                        ImageUrl="~/Images/Neutral/Money.png"
                                        CommandArgument='<%# Bind("CasoId") %>'
                                        Width="24px" CommandName="AgregarGastos"
                                        ToolTip="AgregarGastos"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumnEliminar" Visible="false" ShowSortIcon="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server"
                                        CssClass="deleteRow"
                                        CommandName="EliminarCaso"
                                        CommandArgument='<%# Bind("CasoId") %>'>
                                        <asp:Image ID="Image1" runat="server"
                                            ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn UniqueName="CasoId" DataField="CasoId" HeaderText="Caso Id" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="CodigoCaso" DataField="CodigoCaso" HeaderText="Código Caso"
                                SortExpression="[CodigoCaso]" />
                            <telerik:GridBoundColumn UniqueName="NombreJuridico" DataField="NombreAseguradoraForDisplay" HeaderText="Cliente (ID)"
                                SortExpression="[C].[NombreJuridico]" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="MedicoName" DataField="MedicoName" HeaderText="Médico"
                                SortExpression="[fullname]" />
                            <telerik:GridBoundColumn UniqueName="Codigo" DataField="CodigoAsegurado" HeaderText="Código asegurado" Visible="false"
                                SortExpression="[A].[Codigo]" />
                            <telerik:GridBoundColumn UniqueName="NumeroPoliza" DataField="NumeroPoliza" HeaderText="Nro. Póliza"
                                SortExpression="[POLIZA].[NumeroPoliza]" />
                            <telerik:GridBoundColumn UniqueName="NombrePaciente" DataField="NombrePacienteForDisplay" HeaderText="Asegurado (ID)"
                                SortExpression="[P].[Nombre]" />
                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacion" HeaderText="Fecha registro"
                                SortExpression="[FechaCreacion]" />
                            <telerik:GridBoundColumn UniqueName="NombreAseguradoraForDisplay" DataField="NombreAseguradoraForDisplay" HeaderText="Cliente"
                                SortExpression="[NombreAseguradora]" />
                            <telerik:GridBoundColumn UniqueName="CiudadId" DataField="CiudadId" HeaderText="Ciudad"
                                SortExpression="[CiudadId]" />
                            <telerik:GridBoundColumn UniqueName="MotivoConsultaIdForDisplay" DataField="MotivoConsultaIdforDisplay" HeaderText="Tipo consulta" Visible="false"
                                SortExpression="[MotivoConsultaId]" />
                            <telerik:GridBoundColumn UniqueName="MotivoConsultaId" DataField="MotivoConsultaId" Display="false"
                                SortExpression="[MotivoConsultaId]" />
                            <%--<telerik:GridBoundColumn UniqueName="MotivoConsulta" DataField="MotivoConsulta" HeaderText="Motivo de la consulta"
                                SortExpression="[E].[Observacion], [H].[MotivoConsulta]" />--%>
                            <%--<telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo" DataField="DiagnosticoPresuntivoForDisplay" HeaderText="Diagnóstico Presuntivo"
                                SortExpression="[EN].[Nombre],[E2].[Nombre],[E3].[Nombre],[H].[DiagnosticoPresuntivo]" />--%>
                            <telerik:GridBoundColumn DataField="CasoCritico" HeaderText="CasoCritico" Visible="false"
                                SortExpression="[A].[CasoCritico]" />
                            <%--<telerik:GridCheckBoxColumn DataField="CasoCritico" UniqueName="CasoGravedad" HeaderText="Caso de gravedad" HeaderStyle-Width="15px"
                                SortExpression="[A].[CasoCritico]" />--%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div>
                    <span style="font-weight: bold;">Primer registro:</span>
                    <asp:Label ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Label>
                    <span style="font-weight: bold;">Número de registros cargados:</span>
                    <asp:Label ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Label>
                    <span style="font-weight: bold;">Número total de registros:</span>
                    <asp:Label ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Label>
                </div>
                <div class="buttonsPanel">
                    <asp:LinkButton CssClass="button" ID="FirstButton" runat="server" OnClick="PrimeroButton_Click">
                        <asp:Label ID="Label1" Text="Primera" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="PreevFastButton" runat="server" OnClick="AnteriorRapidoButton_Click">
                        <asp:Label ID="Label3" Text="-5 Páginas" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="PreevButton" runat="server" OnClick="AnteriorButton_Click">
                        <asp:Label ID="Label4" Text="Anterior" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="NextButton" runat="server" OnClick="SiguienteButton_Click">
                        <asp:Label ID="Label5" Text="Siguiente" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="NextFastButton" runat="server" OnClick="SiguienteRapidoButton_Click">
                        <asp:Label ID="Label6" Text="+5 Páginas" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="LastButton" runat="server" OnClick="UltimoButton_Click">
                        <asp:Label ID="Label7" Text="Ultimo" runat="server" />
                    </asp:LinkButton></div><asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
                <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
                <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
                <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />
                <asp:HiddenField ID="ProveedorIdHF" runat="server" />
                <asp:HiddenField ID="HiddenButton" runat="server" />
                <asp:HiddenField ID="LastClienteHF" runat="server" />
                <asp:HiddenField ID="NumeroDiasReconsultaHF" runat="server" />
                <asp:HiddenField ID="ModeHF" runat="server" value=""/>
                <asp:HiddenField ID="ModeHF2" runat="server" value=""/>
                <asp:HiddenField ID="EsOdontologoHF" Value="0" runat="server" />
                <asp:HiddenField ID="OrderByHF" runat="server" />
                <div id="dialog-confirm" title="Eliminar" style="display: none">
                    <p>
                        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                        ¿Está seguro que desea eliminar el Caso Médico seleccionado? </p></div>
                
                <script type="text/javascript">
                    $(function () {
                        $(".deleteRow").click(function () {
                            $("#<%= HiddenButton.ClientID %>").val($(this).attr("href"));
                            $("#dialog-confirm").dialog("open");
                            return false;
                        });

                        $("#dialog-confirm").dialog({
                            resizable: false,
                            height: 140,
                            modal: true,
                            autoOpen: false,
                            buttons: {
                                "Eliminar": function () {
                                    eval($("#<%= HiddenButton.ClientID %>").val());
                                    $(this).dialog("close");
                                },
                                "Cancelar": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    });
                </script><%--<script type="text/javascript">
                    function FechaFinCV_Validate(sender, args) {
                        args.IsValid = true;
                        var FechaInicio = $find('<%= FechaInicio.ClientID %>').get_selectedDate();
                        var FechaFin = $find('<%= FechaFin.ClientID %>').get_selectedDate();

                        if (FechaFin < FechaInicio) {
                            args.IsValid = false;
                        }
                    }
                </script>--%>
 
       

            </div></div></div></asp:Content>

