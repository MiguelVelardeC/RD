<%@ Page Title="Registro de Caso Medico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CasoMedicoRegistro.aspx.cs" Inherits="CasoMedico_CasoMedicoRegistro" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .CSearch .SearchTitle {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" CssClass="title" Text="Registro de Caso Medico" runat="server" />
            </div>
            <div class="columnContent">
                <asp:Panel runat="server">
                    <%--<asp:Label Text="Codigo asegurado ó CI ó Póliza ó Nombre" runat="server" CssClass="label" />
                    <asp:TextBox ID="SearchTexbox" runat="server"
                        CssClass="biggerField" />

                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SearchCaso" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="SearchCaso"
                            OnClick="SearchCaso_Click">
                    <asp:label text="Buscar" runat="server" />
                        </asp:LinkButton>
                    </div>--%>
                    <search:SearchControl ID="SearchCasoMedico" runat="server"
                        Title="Buscar Casos Médicos"
                        AdvancedSearchForm="~/UserControls/AdvancedSearch/PolizaAdvancedSearch.ascx"
                        DisplayHelp="true"
                        DisplayContextualHelp="true"
                        CssSearchAdvanced="CSearch_Advanced_Panel"
                        CssSearch="CSearch"
                        Visible="false"
                        CssSearchHelp="CSearchHelpPanel"
                        CssSearchError="CSearchErrorPanel"
                        SavedSearches="true"
                        SavedSearchesID="searchCtl_CasoMedicoReg"
                        ImageHelpUrl="Images/Neutral/Help.png"
                        ImageErrorUrl="~/images/exclamation.png" />
                    <div class="clear"></div>

                    <asp:Panel ID="AdminPanel" runat="server" CssClass="PanelAdmin" Style="font-size: 12px;" DefaultButton="boton">
                        <a id="PanelButton" style="text-decoration: none;">
                            <h3 class="sectionTitle" style="background: #e1dddd;">
                                <span style="margin-left: 30px;">FILTROS DE BUSQUEDA</span>
                            </h3>
                        </a>
                        <div class="clear" style="margin-bottom: 5px;"></div>
                        <div id="Contents" style="padding: 1em 0.5em;">

                            <table style="border: 0 none;">
                                <tr>
                                    <td>
                                        <asp:Label Text="Cliente" runat="server"
                                            Style="width: 145px;" CssClass="label left" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ClienteDDL" runat="server"
                                            Style="width: 346px; height: 20px;"
                                            DataValueField="ClienteId"
                                            DataTextField="NombreJuridico">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <span style="font-weight: bold;">Nro Póliza:</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="polizaText" runat="server" EmptyMessage="Número de Poliza" Width="250px"></telerik:RadTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td><b>PACIENTE:</b>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="pacienteNombreText" runat="server" EmptyMessage="Nombre Del Paciente" Width="346px"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <span style="margin-left: 10px; font-weight: bold;">CI:</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="carnetPacienteText" runat="server" EmptyMessage="Carnet de Identidad" Width="250px"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <span style="margin-left: 10px; font-weight: bold;">ESTADO:</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="estadoComboBox" runat="server"
                                            AutoPostBack="false"
                                            Width="150px"
                                            Height="22px"
                                            Style="margin-left: 11px;">
                                            <asp:ListItem Text="TODOS" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="ACTIVO" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="INACTIVO" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
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


                        <asp:Button ID="boton" Text="" Style="display: none;" runat="server" OnClick="boton_Click" />
                    </asp:Panel>
                    <div class="clear" style="margin-bottom: 5px;"></div>
                </asp:Panel>
                <telerik:RadGrid ID="PacienteRadGrid" runat="server"
                    AutoGenerateColumns="false"
                    PageSize="20"
                    AllowPaging="true"
                    OnItemDataBound="PacienteRadGrid_ItemDataBound"
                    OnItemCommand="PacienteRadGrid_ItemCommand">
                    <MasterTableView>
                        <NoRecordsTemplate>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CrearCasoMedico" HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DetailsImageButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                        CommandArgument='<%# Bind("PolizaId") %>' CommandName="Select"
                                        ToolTip="Crear Caso Médico"></asp:ImageButton>
                                    <asp:HiddenField ID="PacienteId" runat="server" Value='<%# Bind("PacienteId") %>' />

                                    <asp:Image ID="AlertaImg" runat="server" ImageUrl="~/Images/Neutral/alerta.png"
                                        Visible="false"
                                        ToolTip="El Paciente supera el porcentaje de siniestralidad de alerta" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Reconsulta" HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ReconsultaImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/reConsulta.png"
                                        CommandArgument='<%# Bind("CasoId") %>'
                                        CommandName="Reconsulta"
                                        ToolTip="Reconsulta"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CrearEnfermeria" HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EnfermeriaImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/enfermeria.png"
                                        CommandArgument='<%# Bind("PolizaId") %>'
                                        CommandName="CrearEnfermeria"
                                        ToolTip="Crear Enfermeria"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CrearEmergencia" HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EmergenciaImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/emergencia.png"
                                        CommandArgument='<%# Bind("PolizaId") %>'
                                        CommandName="CrearEmergencia"
                                        ToolTip="Crear Emergencia"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="CodigoAsegurado" HeaderText="Código Asegurado" />
                            <telerik:GridBoundColumn DataField="NombreCompletoPaciente" HeaderText="Nombre Paciente" />
                            <telerik:GridBoundColumn DataField="NumeroPoliza" HeaderText="Número de Póliza" />
                            <telerik:GridBoundColumn DataField="NombreJuridicoCliente" HeaderText="Cliente" />
                            <telerik:GridBoundColumn UniqueName="Estado" DataField="Estado" HeaderText="Estado" />
                            <telerik:GridBoundColumn DataField="FechaFinString" HeaderText="Fecha Fin" />
                            <telerik:GridBoundColumn DataField="LugarForDisplay" HeaderText="Ciudad" />
                            <%--<telerik:GridBoundColumn DataField="MontoTotalForDisplay" HeaderText="Monto" />
                                <telerik:GridBoundColumn DataField="GastoTotalForDisplay" HeaderText="Gasto Total" />--%>
                            <telerik:GridBoundColumn DataField="CasoCritico" HeaderText="CasoCritico" Display="false" />
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
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="PreevFastButton" runat="server" OnClick="AnteriorRapidoButton_Click">
                        <asp:Label ID="Label3" Text="-5 Páginas" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="PreevButton" runat="server" OnClick="AnteriorButton_Click">
                        <asp:Label ID="Label4" Text="Anterior" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="NextButton" runat="server" OnClick="SiguienteButton_Click">
                        <asp:Label ID="Label5" Text="Siguiente" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="NextFastButton" runat="server" OnClick="SiguienteRapidoButton_Click">
                        <asp:Label ID="Label6" Text="+5 Páginas" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="LastButton" runat="server" OnClick="UltimoButton_Click">
                        <asp:Label ID="Label7" Text="Ultimo" runat="server" />
                    </asp:LinkButton>
                </div>

                <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
                <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
                <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
                <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />
                <asp:HiddenField ID="OrderByHF" runat="server" />
                <asp:HiddenField ID="ClienteIdHF" runat="server" />
                <asp:HiddenField ID="ModeHF" runat="server" />
                <asp:HiddenField ID="NumeroDiasReconsultaHF" runat="server" />
                <asp:HiddenField ID="LastClienteHF" runat="server" />
                <asp:HiddenField ID="UserIdHiddenField" runat="server" />
                <asp:HiddenField ID="MedicoUsuarioId" runat="server" Value="0" />
                <asp:HiddenField ID="MotivoRegistroHF" runat="server" Value="0" />
            </div>
        </div>
    </div>
</asp:Content>
