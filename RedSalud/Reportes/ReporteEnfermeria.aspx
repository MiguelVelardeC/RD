<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteEnfermeria.aspx.cs" Inherits="Reportes_ReporteEnfermeria" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label Text="Reporte de Enfermeria" runat="server" CssClass="title" />
            </div>
            <div class="columnContent">
                <asp:Panel ID="Search" runat="server">
                    <asp:Label ID="Label1" Text="Fecha Inicial" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <telerik:RadDatePicker ID="FechaIni" runat="server"></telerik:RadDatePicker>
                    <div class="clear" style="margin-bottom: 5px;"></div>
            
                    <asp:Label ID="Label7" Text="Fecha Final" runat="server" 
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
                    <search:SearchControl ID="SearchEnfermeria" runat="server"
                    AdvancedSearchForm="~/UserControls/AdvancedSearch/RepEnfermeriaAdvancedSearch.ascx"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true"
                    SavedSearchesID="searchCtl_RepEnfermeria"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                </asp:Panel>

                <telerik:RadGrid ID="EnfermeriaRadGrid" runat="server"
                    DataSourceID="ReporteEnfermeriaODS"
                    AllowPaging="true"
                    AllowSorting="true"
                    PageSize="20"
                    ShowFooter="True"
                    OnExcelExportCellFormatting="EnfermeriaRadGrid_ExcelExportCellFormatting"
                    AutoGenerateColumns="false">
                    <ExportSettings OpenInNewWindow="true" ExportOnlyData="true" FileName="ReporteCasosMedicos" IgnorePaging="true">
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CodigoCaso" CommandItemDisplay="Top" TableLayout="Fixed" Width="100%"
                        OverrideDataSourceControlSorting="true">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <CommandItemTemplate>
                            <table class="rgCommandTable" border="0" style="width:100%;">
                                <caption>
                                <thead>
                                <tbody>
                                <tr>
                                <td align="left"></td>
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
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="CodigoCaso" DataField="CodigoCaso" HeaderText="CÓDIGO CASO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="Medico" DataField="Medico" HeaderText="MÉDICO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="MotivoConsulta" DataField="MotivoConsulta" HeaderText="MOTIVO DE LA CONSULTA" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Observaciones" DataField="Observaciones" HeaderText="OBSERVACIONES" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Tratamiento" DataField="Tratamiento" HeaderText="TRATAMIENTO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo" DataField="Enfermedad" HeaderText="DIAGNOSTICO PRESUNTIVO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo2" DataField="Enfermedad2" HeaderText="DIAGNOSTICO PRESUNTIVO 2" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo3" DataField="Enfermedad3" HeaderText="DIAGNOSTICO PRESUNTIVO 3" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivoOtro" DataField="DiagnosticoPresuntivo" HeaderText="OTRO DIAGNOSTICO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FechaCaso" DataField="FechaCaso" HeaderText="FECHA DEL CASO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.DateTime" />
                            <telerik:GridBoundColumn UniqueName="Ciudad" DataField="Ciudad" HeaderText="CIUDAD" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre" HeaderText="PACIENTE" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="Genero" DataField="Genero" HeaderText="GENERO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FechaNacimiento" DataField="FechaNacimiento" HeaderText="FECHA DE NACIMIENTO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.DateTime" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="EdadForDisplay" DataField="EdadCalculadaForDisplay" HeaderText="EDAD" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.DateTime" Visible="true" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="CasoCritico" DataField="CasoCritico" HeaderText="CASO CRITICO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                                --%>
                            <telerik:GridBoundColumn UniqueName="NumeroPoliza" DataField="NumeroPoliza" HeaderText="NÚMERO PÓLIZA" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="Medicamento" DataField="Medicamento" HeaderText="Medicamento" />
                            <telerik:GridBoundColumn UniqueName="Presentacion" DataField="Presentacion" HeaderText="Presentación" />
                            <telerik:GridBoundColumn UniqueName="Indicaciones" DataField="Indicaciones" HeaderText="Indicaciones" />
                            <telerik:GridBoundColumn UniqueName="TipoDocumento" DataField="TipoDocumento" HeaderText="Tipo de Documento" />
                            <telerik:GridBoundColumn UniqueName="NroFacturaRecibo" DataField="NroFacturaRecibo" HeaderText="Nro Factura / Recibo" />

                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacionToDisplay" 
                                HeaderText="FECHA REGISTRO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FechaGasto" DataField="FechaGastoToDisplay" 
                                HeaderText="FECHA GASTO" FooterText="Totales" />
                            <telerik:GridBoundColumn UniqueName="DecimalMonto" DataField="Monto" 
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                HeaderText="MONTO" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ReporteEnfermeriaODS" runat="server"
                    TypeName="Artexacta.App.Reportes.BLL.EnfermeriaBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SearchEnfermeria"
                    OnSelected="ReporteEnfermeriaODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchEnfermeria" PropertyName="Sql" DbType="String" Name="where" />
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

