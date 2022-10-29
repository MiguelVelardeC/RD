<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteFarmacia.aspx.cs" Inherits="Reportes_ReporteFarmacia" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Reporte de Farmacia" runat="server" CssClass="title" />
            </div>
            <div class="columnContent">
                <asp:Panel ID="Search" runat="server">
                    <asp:Label ID="Label7" Text="Fecha Inicial" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <telerik:RadDatePicker ID="FechaIni" runat="server"></telerik:RadDatePicker>
                    <div class="clear" style="margin-bottom: 5px;"></div>
            
                    <asp:Label ID="Label8" Text="Fecha Final" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <telerik:RadDatePicker ID="FechaFin" runat="server"></telerik:RadDatePicker>
                    <div class="clear" style="margin-bottom: 5px;"></div>
                    <asp:Label ID="Label2" Text="CLIENTE" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <asp:DropDownList ID="ClienteDDL" runat="server"
                        DataSourceID="ClienteODS"
                        style="height:20px;"
                        DataValueField="ClienteId"
                        DataTextField="NombreJuridico"
                        AutoPostBack="false"
                        CssClass="bigField"
                        onchange="ClienteDDL_change()">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getRedClienteListFE"
                        OnSelected="ClienteODS_Selected" />
                    <div class="clear" style="margin-bottom: 5px;"></div>

                    <asp:Label ID="Label4" Text="PACIENTE" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <telerik:RadComboBox ID="PacienteRadComboBox" runat="server" 
                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                        OnClientItemsRequesting="ClientItemsRequesting">
                        <WebServiceSettings Method="GetPacientes" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                    </telerik:RadComboBox>
                    <div class="clear" style="margin-bottom: 5px;"></div>

                    <asp:Label ID="Label5" Text="MÉDICO" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />

                    <telerik:RadComboBox ID="MedicoRadComboBox" runat="server" 
                        ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
                        AutoPostBack="false" EnableLoadOnDemand="true" CssClass="bigField"
                        OnClientItemsRequesting="ClientItemsRequesting">
                        <WebServiceSettings Method="GetUsuariosMedicos" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
                    </telerik:RadComboBox>
                    <script type="text/javascript">
                        function ClienteDDL_change() {
                            var combo = $find("<%= PacienteRadComboBox.ClientID %>");
                            combo.clearItems();
                            combo.set_text("");
                            combo.clearSelection();
                            combo = $find("<%= MedicoRadComboBox.ClientID %>");
                            combo.clearItems();
                            combo.set_text("");
                            combo.clearSelection();
                        }
                        function ClientItemsRequesting(sender, eventArgs) {
                            var context = eventArgs.get_context();
                            context["ClientId"] = $('#<%= ClienteDDL.ClientID %>').val();
                        }
                    </script>
                    <div class="clear" style="margin-bottom: 5px;"></div>
                    
                    <asp:Label ID="Label6" Text="FILTRAR" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <search:SearchControl ID="SearchFarmacia" runat="server"
                    AdvancedSearchForm="~/UserControls/AdvancedSearch/RepFarmaciaAdvancedSearch.ascx"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true"
                    SavedSearchesID="searchCtl_RepFarmacia"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                </asp:Panel>

                <telerik:RadGrid ID="FarmaciaRadGrid" runat="server"
                    DataSourceID="ReporteFarmaciaODS"
                    AllowPaging="true"
                    AllowSorting="true"
                    PageSize="20"
                    ShowFooter="True"
                    OnExcelExportCellFormatting="FarmaciaRadGrid_ExcelExportCellFormatting"
                    AutoGenerateColumns="false">
                    <ExportSettings OpenInNewWindow="true" ExportOnlyData="true" FileName="ReporteCasosMedicos" IgnorePaging="true">
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CodigoCaso" CommandItemDisplay="Top" Width="100%" TableLayout="Fixed"
                        OverrideDataSourceControlSorting="true">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <CommandItemTemplate>
                            <table class="rgCommandTable" border="0" style="width:100%;">
                                <caption>
                                <thead>
                                <tbody>
                                <tr>
                                <td align="left"> </td>
                                <td align="right">|
                                    <asp:Button ID="export" runat="server" CssClass="rgExpXLS"
                                        OnClientClick="return chooseDecimalSeparator();"
                                        ToolTip="EXPORT TO EXCEL" />
                                </td>
                                </tr>
                                </tbody>
                                </table>
                        </CommandItemTemplate>
                        <NoRecordsTemplate>
                            <asp:Label ID="Label3" runat="server" Text="NO EXISTEN DATOS PARA LOS FILTROS SELECCIONADOS."></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="NombreCliente" DataField="NombreCliente" HeaderText="CLIENTE" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" HeaderStyle-Width="90px" />
                            <telerik:GridBoundColumn UniqueName="CodigoCaso" DataField="CodigoCaso" HeaderText="CÓDIGO CASO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" HeaderStyle-Width="60px" />
                            <telerik:GridBoundColumn UniqueName="Medico" DataField="Medico" HeaderText="MÉDICO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" HeaderStyle-Width="70px" />
                            <telerik:GridBoundColumn UniqueName="TipoConsulta" DataField="TipoConsulta" HeaderText="TIPO DE CONSULTA" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FechaCaso" DataField="FechaCaso" HeaderText="FECHA DEL CASO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.DateTime" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Ciudad" DataField="Ciudad" HeaderText="CIUDAD" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre" HeaderText="PACIENTE" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="Genero" DataField="Genero" HeaderText="GENERO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FechaNacimiento" DataField="FechaNacimiento" HeaderText="FECHA DE NACIMIENTO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.DateTime" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="CasoCritico" DataField="CasoCritico" HeaderText="CASO CRITICO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            --%>
                            <telerik:GridBoundColumn UniqueName="NumeroPoliza" DataField="NumeroPoliza" HeaderText="NÚMERO PÓLIZA" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="NombrePlan" DataField="NombrePlan" HeaderText="NOMBRE DEL PLAN" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="IsLinameForDisplay" DataField="IsLinameForDisplay" 
                                HeaderText="UN PRINCIPIO ACTIVO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Medicamento" DataField="Medicamento" HeaderText="Medicamento" />
                            <telerik:GridBoundColumn UniqueName="Presentacion" DataField="Presentacion" HeaderText="Presentación" />
                            <telerik:GridBoundColumn UniqueName="Concentracion" DataField="Concentracion" HeaderText="Concentración" />
                            <telerik:GridBoundColumn UniqueName="Indicaciones" DataField="Indicaciones" HeaderText="Indicaciones" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="NroFacturaRecibo" DataField="NroFacturaRecibo" HeaderText="Nro Factura / Recibo" />
                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacionToDisplay" 
                                HeaderText="FECHA REGISTRO" HeaderStyle-Width="70px" />
                            <telerik:GridBoundColumn UniqueName="FechaGasto" DataField="FechaGastoToDisplay" 
                                HeaderText="FECHA GASTO" FooterText="Totales" HeaderStyle-Width="70px" />
                            <telerik:GridBoundColumn UniqueName="DecimalMonto" DataField="Monto" HeaderStyle-Width="70px"
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                HeaderText="MONTO" ItemStyle-HorizontalAlign="Right" />
                            --%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ReporteFarmaciaODS" runat="server"
                    TypeName="Artexacta.App.Reportes.BLL.FarmaciaBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SearchFarmacia"
                    OnSelected="ReporteFarmaciaODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchFarmacia" PropertyName="Sql" DbType="String" Name="where" />
                        <asp:ControlParameter ControlID="ClienteDDL" PropertyName="SelectedValue" DbType="Int32" Name="ClienteId" />
                        <asp:ControlParameter ControlID="PacienteRadComboBox" PropertyName="SelectedValue" DbType="Int32" Name="AseguradoId" />
                        <asp:ControlParameter ControlID="MedicoRadComboBox" PropertyName="SelectedValue" DbType="Int32" Name="UserId" />
                        <asp:ControlParameter ControlID="FechaIni" PropertyName="SelectedDate" DbType="DateTime" Name="FechaIni" />
                        <asp:ControlParameter ControlID="FechaFin" PropertyName="SelectedDate" DbType="DateTime" Name="FechaFin" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div id="dialog-confirm" title="Exportar a MS Excel" style="display: none;">
                  <p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>Escoja el formato para los decimales:</p>
                </div>
                <asp:HiddenField ID="DecimalSimbolHF" runat="server" />
                <asp:LinkButton ID="realExport" OnClick="export_Click"  runat="server" />
                <script type="text/javascript">
                    function chooseDecimalSeparator() {
                        try {
                            $("#dialog-confirm").dialog({
                                resizable: false,
                                height: 150,
                                modal: true,
                                buttons: {
                                    'Coma [,]': function () {
                                        $('#<%= DecimalSimbolHF.ClientID%>').val(',');
                                        __doPostBack('<%= realExport.ClientID.Replace("_", "$")%>', '');
                                        $(this).dialog('close');
                                    },
                                    'Punto [.]': function () {
                                        $('#<%= DecimalSimbolHF.ClientID%>').val('.');
                                        __doPostBack('<%= realExport.ClientID.Replace("_", "$")%>', '');
                                        $(this).dialog('close');
                                    }
                                }
                            });
                        } catch (q) { }
                        return false;
                    }
                </script>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="ModeHF" runat="server" />
    <asp:HiddenField ID="ProveedorIdHF" runat="server" />
</asp:Content>

