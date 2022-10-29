<%@ Page Title="Lista de Derivaciones" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListaDerivaciones.aspx.cs" Inherits="CasoMedico_ListaDerivaciones" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="RedSalud" TagName="SearchControl" %>

<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sectionTitle:hover {
            background: #e1dddd;
        }

        .sectionTitle:focus {
            background: #e1dddd;
        }

        .PanelAdmin {
            border: 1px solid grey;
        }

        #PanelButton, #PanelButton:visited, #PanelButton:hover, #PanelButton:active {
            color: #333 !important;
        }

        .Atendida {
            background-color: #85E59D !important;
        }

        .auto-style4 {
            width: 124px;
        }

        .auto-style5 {
            width: 125px;
        }

        .auto-style6 {
            width: 250px;
        }

        .RadGrid_Default .rgRow td {
            border-width: 0 0 0 0 !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#PanelButton").click(function () {
                if (!$("#Contents").is(":visible")) {
                    $("#Contents").slideDown();
                } else {
                    $("#Contents").slideUp();
                }
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <asp:HiddenField ID="MedicoUsuarioId" Value="0" runat="server" />
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Derivaciones</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink Visible="false" runat="server" NavigateUrl="#" Text="Crear Nuevo Caso Medico"></asp:HyperLink>
                </div>

                <RedSalud:SearchControl ID="SearchPA" runat="server"
                    Title="Búsqueda"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="
                    "
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true" SavedSearchesID="SearchPA"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png"
                    Visible="false" />

                &nbsp;&nbsp;
                 <asp:Panel ID="AdminPanel" runat="server" CssClass="PanelAdmin" Style="font-size: 12px;" DefaultButton="boton">
                     <a id="PanelButton" style="text-decoration: none;">
                         <h3 class="sectionTitle" style="background: #e1dddd;">
                             <span style="margin-left: 30px;">FILTROS DE BUSQUEDA</span>
                         </h3>
                     </a>
                     <div id="Contents" style="padding: 1em 0.5em;">

                         <table>
                             <tr>
                                 <td>MEDICO DERIVADOR:
                    <asp:DropDownList ID="medicoDerivadorComboBox" runat="server"
                        AutoPostBack="false"
                        Width="150px"
                        Height="22px">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                                 </td>

                                 <td class="auto-style5">
                                     <asp:Label ID="LabelEspecialidadDerivado" Text=" Especialidad Del Derivado:" runat="server" />

                                 </td>
                                 <td class="auto-style6">
                                     <telerik:RadComboBox ID="EspecialidadDerivadoRadComboBox" runat="server"
                                         ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                         EmptyMessage="Seleccione un Especialidad "
                                         AutoPostBack="True" EnableLoadOnDemand="true" Width="240px"
                                         OnSelectedIndexChanged="EspecialidadDerivadoRadComboBox_OnClientSelectedIndexChanged">
                                         <WebServiceSettings Method="GetEspecialidad" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                                     </telerik:RadComboBox>
                                 </td>
                                 <td>
                                     <asp:Label runat="server" ID="derivadoLabel">DERIVADO:</asp:Label>
                                     <asp:DropDownList ID="medicoDerivadoComboBox" runat="server"
                                         AutoPostBack="false"
                                         Width="150px"
                                         Height="22px"
                                         Style="margin-left: 5px;">
                                         <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                     </asp:DropDownList></td>
                             </tr>
                         </table>
                         <div class="clear" style="margin-bottom: 7px;"></div>
                         CIUDAD:
                    <asp:DropDownList ID="ciudadComboBox" runat="server"
                        AutoPostBack="false"
                        Width="150px"
                        Style="margin-left: 77px;">
                    </asp:DropDownList>
                         CLIENTE:
                    <asp:DropDownList ID="clienteComboBox" runat="server"
                        AutoPostBack="false"
                        Width="150px"
                        Height="22px"
                        Style="margin-left: 69px;">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                         <asp:Label runat="server" ID="Label1" Style="margin-left: 100px;">ESTADO:</asp:Label>
                         <asp:DropDownList ID="estadoComboBox" runat="server"
                             AutoPostBack="false"
                             Width="150px"
                             Height="22px"
                             Style="margin-left: 11px;">
                             <asp:ListItem Text="TODOS" Value="-1"></asp:ListItem>
                             <asp:ListItem Text="PENDIENTE" Value="0"></asp:ListItem>
                             <asp:ListItem Text="ATENDIDO" Value="1"></asp:ListItem>
                         </asp:DropDownList>
                         <telerik:RadTextBox ID="derivacionIdText" Visible="false" EmptyMessage="Nro Derivacion" runat="server" Width="150px" Style="margin-left: 41px;" />
                         <div class="clear" style="margin-bottom: 7px;"></div>
                         CASO INICIAL:
                    <telerik:RadTextBox ID="codigoCasoIdText" EmptyMessage="Codigo de Caso" runat="server" Width="150px" Style="margin-left: 41px;" />
                         CASO DERIVACION:
                    <telerik:RadTextBox ID="codigoCasoDerivacionText" EmptyMessage="Codigo de Caso de la derivacion" Style="margin-left: 7px;" runat="server" Width="250px"></telerik:RadTextBox>
                         PACIENTE:
                    <telerik:RadTextBox ID="pacienteNombreText" runat="server" EmptyMessage="Nombre Del Paciente" Width="250px"></telerik:RadTextBox>
                         <div class="clear" style="margin-bottom: 7px;"></div>
                         RANGO DE FECHAS:                                   
                    <telerik:RadDatePicker ID="FechaInicio" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="177px" Style="margin-left: 9px;"></telerik:RadDatePicker>
                         <telerik:RadDatePicker ID="FechaFin" runat="server" DateInput-EmptyMessage="Fecha Final" Width="177px"></telerik:RadDatePicker>
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

                <telerik:RadGrid ID="MedicoDesgravamenGridView" runat="server"
                    AutoGenerateColumns="false" DataSourceID="MedicoDesgravamenDataSource"
                    AllowPaging="false"
                    OnItemCommand="MedicoDesgravamenGridView_ItemCommand"
                    OnItemDataBound="MedicoDesgravamenGridView_ItemDataBound"
                    OnDataBound="MedicoDesgravamenGridView_DataBound"
                    OnExcelExportCellFormatting="DerivacionesRadGrid_ExcelExportCellFormatting"
                    AllowMultiRowSelection="False">
                    <ExportSettings OpenInNewWindow="true" ExportOnlyData="true" FileName="ReporteDerivaciones" IgnorePaging="true">
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView DataKeyNames="DerivacionId" ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false"
                        OverrideDataSourceControlSorting="true">
                        <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <CommandItemTemplate>
                            <table class="rgCommandTable" border="0" style="width: 100%;">
                                <caption>
                                    <thead>
                                        <tbody>
                                            <tr>
                                                <td align="left"></td>
                                                <td align="right">|   
                                    <asp:Button ID="export" runat="server" CssClass="rgExpXLS"
                                        ToolTip="EXPORT TO EXCEL" />
                                                </td>
                                            </tr>
                                        </tbody>
                            </table>
                        </CommandItemTemplate>
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay derivaciones</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle Font-Size="11px" />

                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DeletePA" runat="server" CssClass="BotonDelete"
                                        ToolTip="Eliminar Derivacion"
                                        CommandName="Delete"
                                        CommandArgument='<%# Eval("DerivacionId").ToString() %>'
                                        ImageUrl="~/Images/Neutral/delete.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="VerPA" runat="server"
                                        ToolTip="Crear Nuevo Caso Medico Para Derivacion"
                                        CommandName="VerPA"
                                        CommandArgument='<%# Eval("DerivacionId")+"|"+Eval("PacienteId") %>'
                                        ImageUrl="~/Images/Neutral/select.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="DerivacionId" DataField="DerivacionId"
                                HeaderText="Nro Derivacion" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="isAtendidoDisplay" DataField="isAtendidoDisplay"
                                HeaderText="Estado" />
                            <telerik:GridBoundColumn UniqueName="CasoCodigoDerivado" DataField="CasoCodigoDerivado"
                                HeaderText="Codigo Caso Inicial" />
                            <telerik:GridBoundColumn UniqueName="CasoCodigoDerivacion" DataField="CasoCodigoDerivacion"
                                HeaderText="Codigo Caso Especialista" />
                            <telerik:GridBoundColumn UniqueName="PacienteNombre" DataField="PacienteNombre"
                                HeaderText="Paciente" />
                            <telerik:GridBoundColumn UniqueName="DerivadorNombre" DataField="DerivadorNombre"
                                HeaderText="Derivado Por" />
                            <telerik:GridBoundColumn UniqueName="MedicoNombre" DataField="MedicoNombre"
                                HeaderText="Especialista Asignado"
                                Display="false" />
                            <telerik:GridBoundColumn UniqueName="EspecialidadNombre" DataField="EspecialidadNombre"
                                HeaderText="ESPECIALIDAD" 
                                Display="false"/>                            
                            <telerik:GridBoundColumn UniqueName="ClienteNombre" DataField="ClienteNombre"
                                HeaderText="Cliente"
                                Display="true" />
                            <telerik:GridBoundColumn UniqueName="CiudadDerivacionNombre" DataField="CiudadDerivacionNombre"
                                HeaderText="Ciudad De Derivacion" />
                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacion"
                                HeaderText="Fecha de Derivacion" />
                        </Columns>

                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <%-- 
                    <ExportSettings ExportOnlyData="true" IgnorePaging="true" HideStructureColumns="false" FileName="PropuestosAsegurados">
                        <Pdf FontType="Subset" PaperSize="Letter" />
                        <Excel Format="Html" />
                        <Csv ColumnDelimiter="Colon" RowDelimiter="NewLine" />
                    </ExportSettings>
                    --%>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="MedicoDesgravamenDataSource" runat="server"
                    TypeName="Artexacta.App.Derivacion.BLL.DerivacionBLL"
                    SelectMethod="GetDerivacionEspecialistaBySearch"
                    OnSelected="MedicoDesgravamenDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchPA" PropertyName="Sql" Name="varSql" Type="String" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="PageSize" Name="pageSize" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="CurrentRow" Name="firstRow" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" Name="totalRows" PropertyName="TotalRows" Type="Int32" Direction="Output" />
                        <asp:ControlParameter ControlID="MedicoUsuarioId" Name="medicoId" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <RedSalud:PagerControl ID="Pager" runat="server"
                    PageSize="10"
                    CurrentRow="0"
                    InvisibilityMethod="PropertyControl"
                    OnPageChanged="Pager_PageChanged" />
                <div class="clear"></div>
            </div>
        </div>
    </div>

    <!--<asp:HiddenField ID="UserIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="UserAuthorizedAprobarHF" runat="server" Value="false" />-->

    <script type="text/javascript">

        $(document).ready(function () {
            $('.BotonDelete').click(function () {
                return confirm('Está seguro que quiere eliminar el medico? ');
            });
        });

    </script>
</asp:Content>

