<%@ Page Title="Reporte de Casos Medicos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="ReporteCasoMedicoCompleto.aspx.cs" Inherits="CasoMedico_ReporteCasoMedicoCompleto" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .CSearch .SearchTitle
        {
            font-weight: bold;
        }
        .AdvancedSearchLink
        {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label Text="Reporte de Casos Medicos" runat="server" CssClass="title" />
            </div>
            <div class="columnContent">
                <asp:Panel ID="Search" runat="server" DefaultButton="boton">  
                    <asp:Label Text="Fecha Inicial" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <telerik:RadDatePicker ID="FechaIni" runat="server"></telerik:RadDatePicker>
                    <div class="clear" style="margin-bottom: 5px;"></div>
            
                    <asp:Label Text="Fecha Final" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <telerik:RadDatePicker ID="FechaFin" runat="server"></telerik:RadDatePicker>
                    <div class="clear" style="margin-bottom: 5px;"></div>

                    <asp:Label Text="CLIENTE" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <asp:DropDownList ID="ClienteDDL" runat="server"
                        DataSourceID="ClienteODS"
                        style="width: 346px; height:20px;"
                        DataValueField="ClienteId"
                        DataTextField="NombreJuridico"
                        AutoPostBack="false"
                        OnDataBound="ClienteDDL_DataBound">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getRedClienteListFE"
                        OnSelected="ClienteODS_Selected" />
                    <div class="clear" style="margin-bottom: 5px;"></div>                    
                    <asp:Label ID="lblPoliza" Text="NUMERO POLIZA" runat="server" 
                        style="width: 145px;"
                        CssClass="label left" />
                    <asp:TextBox ID="txtPoliza" runat="server">
                    </asp:TextBox>
                    <asp:CheckBox ID="CasoMedicoCheckBox" Text="CASOS MÉDICOS" runat="server" Checked="true" 
                        AutoPostBack="false" Visible="false" />
                    <asp:CheckBox ID="ReconsultaCheckBox" Text="RECONSULTAS" runat="server" 
                        AutoPostBack="false" Visible="false" />
                    <asp:CheckBox ID="EnfermeriaCheckBox" Text="ENFERMERIA" runat="server"
                        AutoPostBack="false" Visible="false" />
                    <asp:CheckBox ID="EmergenciaCheckBox" Text="EMERGENCIAS" runat="server"
                        AutoPostBack="false" Visible="false" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                    
                    <search:SearchControl ID="SearchCasoMedico" runat="server"
                    Title="FILTRAR"
                    AdvancedSearchForm="~/UserControls/AdvancedSearch/RepCasoMedicoAdvancedSearch.ascx"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true"
                    SavedSearchesID="searchCtl_RepCasoMedico"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png"
                    Visible="false" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="button" OnClick="btnSearch_Click">
                    <span>BUSCAR</span>
                    </asp:LinkButton>                    
                    <div class="clear" style="margin-bottom: 5px;"></div>                                     
                    <asp:Button id="boton" Text="" Style="display:none;" runat="server" /> 
                </asp:Panel>

                <telerik:RadGrid ID="CasoRadGrid" runat="server"
                    DataSourceID="ReporteCasoMedicoODS"
                    AllowPaging="true"
                    PageSize="20"
                    ShowFooter="false"
                    AllowSorting="true"
                    OnExcelExportCellFormatting="CasoRadGrid_ExcelExportCellFormatting"
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
                                <td align="right">                                    |   
                                    <asp:Button ID="export" runat="server" CssClass="rgExpXLS"
                                        OnClientClick="return chooseDecimalSeparator();"
                                        ToolTip="EXPORT TO EXCEL" />
                                </td>
                                </tr>
                                </tbody>
                                </table>
                        </CommandItemTemplate>
                        <NoRecordsTemplate>
                            <asp:Label runat="server" Text="NO EXISTEN DATOS PARA LOS FILTROS SELECCIONADOS."></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="NombreCliente" DataField="NombreCliente" HeaderText="CLIENTE" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="CodigoCaso" DataField="CodigoCaso" HeaderText="CÓDIGO CASO" 
                                ItemStyle-HorizontalAlign="Left" DataType="System.String" />
                            <telerik:GridBoundColumn UniqueName="Ciudad" DataField="Ciudad" HeaderText="CIUDAD" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FechaCreacion" DataField="FechaCreacion" 
                                HeaderText="FECHA REGISTRO" DataFormatString="{0:dd/MM/yyyy}" />
                            <telerik:GridBoundColumn UniqueName="MotivoConsultaDesc" DataField="MotivoConsultaDesc" 
                                HeaderText="TIPO DE CASO" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="CasoIdDerivacionForDisplay" DataField="CasoIdDerivacionForDisplay" HeaderText="ES DERIVACION" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="NombreMedico" DataField="Medico" HeaderText="NOMBRE DEL MEDICO" />
                            <telerik:GridBoundColumn UniqueName="NombrePaciente" DataField="NombrePaciente" HeaderText="NOMBRE DEL PACIENTE" />
                            <telerik:GridBoundColumn UniqueName="CodigoAsegurado" DataField="CodigoAsegurado" HeaderText="CÓDIGO ASEGURADO" />
                            <telerik:GridBoundColumn UniqueName="FechaNacimiento" DataField="FechaNacimiento" HeaderText="FECHA DE NACIMIENTO" 
                                 DataFormatString="{0:dd/MM/yyyy}" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="Edad" DataField="Edad" HeaderText="EDAD" />
                            <telerik:GridBoundColumn UniqueName="Genero" DataField="Genero" HeaderText="GÉNERO" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="NumeroPoliza" DataField="NumeroPoliza" HeaderText="NRO. PÓLIZA" />
                            <telerik:GridBoundColumn UniqueName="NombrePlan" DataField="NombrePlan" HeaderText="NOMBRE DEL PLAN" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="PresionArterial" DataField="PresionArterialForDisplay" HeaderText="PRESION ARTERIAL" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="Pulso" DataField="PulsoForDisplay" HeaderText="PULSO" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="Temperatura" DataField="TemperaturaForDisplay" HeaderText="TEMPERATURA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FrecuenciaCardiaca" DataField="FrecuenciaCardiacaForDisplay" HeaderText="FRECUENCIA CARDIACA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="EstaturaCm" DataField="EstaturaCmForDisplay" HeaderText="Estatura" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="DecimalPeso" DataField="Peso" HeaderText="PESO" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="IMC" DataField="IMC" HeaderText="IMC" Visible="true" />                            
                            <telerik:GridBoundColumn UniqueName="IMCDescription" DataField="IMCDescription" HeaderText="IMC Descripcion" Visible="true" />                            
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo" DataField="EnfermedadForDisplay" HeaderText="DIAGNOSTICO PRESUNTIVO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo2" DataField="Enfermedad2ForDisplay" HeaderText="DIAGNOSTICO PRESUNTIVO 2" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo3" DataField="Enfermedad3ForDisplay" HeaderText="DIAGNOSTICO PRESUNTIVO 3" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivoOtro" DataField="DiagnosticoPresuntivoForDisplay" HeaderText="OTRO" Visible="false" />
                                                        
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ReporteCasoMedicoODS" runat="server"
                    TypeName="Artexacta.App.Reportes.BLL.CasoMedicoBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SearchCasoMedicoCompleto"
                    OnSelected="ReporteCasoMedicoODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchCasoMedico" PropertyName="Sql" DbType="String" Name="where" />
                        <asp:ControlParameter ControlID="ClienteDDL" PropertyName="SelectedValue" DbType="Int32" Name="ClienteId" />
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

