<%@ Page Title="Reporte de Casos Medicos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="ReporteCasoMedico.aspx.cs" Inherits="CasoMedico_ReporteCasoMedico" %>
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
                <asp:Label Text="Reporte de General" runat="server" CssClass="title" />
            </div>
            <div class="columnContent">
                <asp:Panel ID="Search" runat="server">  
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

                    <asp:CheckBox ID="CasoMedicoCheckBox" Text="CASOS MÉDICOS" runat="server" Checked="true" 
                        AutoPostBack="false" />
                    <asp:CheckBox ID="ReconsultaCheckBox" Text="RECONSULTAS" runat="server" 
                        AutoPostBack="false" />
                    <asp:CheckBox ID="EnfermeriaCheckBox" Text="ENFERMERIA" runat="server"
                        AutoPostBack="false" />
                    <asp:CheckBox ID="EmergenciaCheckBox" Text="EMERGENCIAS" runat="server"
                        AutoPostBack="false" />
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
                    ImageErrorUrl="~/images/exclamation.png" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                </asp:Panel>

                <telerik:RadGrid ID="CasoRadGrid" runat="server"
                    DataSourceID="ReporteCasoMedicoODS"
                    AllowPaging="true"
                    PageSize="20"
                    ShowFooter="True"
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
                                HeaderText="FECHA REGISTRO" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />
                            <telerik:GridBoundColumn UniqueName="EnfermedadCronica" DataField="EnfermedadCronica" HeaderText="ENFERMEDAD CRÓNICA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="MotivoConsultaId" DataField="MotivoConsultaIdforDisplay" 
                                HeaderText="TIPO DE CASO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="NombreMedico" DataField="NombreMedico" HeaderText="NOMBRE DEL MEDICO" />
                            <telerik:GridBoundColumn UniqueName="NombrePaciente" DataField="NombrePaciente" HeaderText="NOMBRE DEL PACIENTE" />
                            <telerik:GridBoundColumn UniqueName="CodigoAsegurado" DataField="CodigoAsegurado" HeaderText="CÓDIGO ASEGURADO" />
                            <telerik:GridBoundColumn UniqueName="FechaNacimiento" DataField="FechaNacimiento" HeaderText="FECHA DE NACIMIENTO" 
                                 DataFormatString="{0:dd/MM/yyyy}" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="Edad" DataField="EdadCalculada" HeaderText="EDAD" />
                            <telerik:GridBoundColumn UniqueName="Genero" DataField="Genero" HeaderText="GÉNERO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="CasoCritico" DataField="CasoCritico" HeaderText="CASO CRÍTICO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="NumeroPoliza" DataField="NumeroPoliza" HeaderText="NRO. PÓLIZA" />
                            <telerik:GridBoundColumn UniqueName="NombrePlan" DataField="NombrePlan" HeaderText="NOMBRE DEL PLAN" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="MotivoConsulta" DataField="MotivoConsulta" 
                                HeaderText="MOTIVO DE LA CONSULTA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="EnfermedadActual" DataField="EnfermedadActual" 
                                HeaderText="ENFERMEDAD ACTUAL" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="PresionArterial" DataField="PresionArterial" HeaderText="PRESION ARTERIAL" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Pulso" DataField="Pulso" HeaderText="PULSO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Temperatura" DataField="Temperatura" HeaderText="TEMPERATURA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="FrecuenciaCardiaca" DataField="FrecuenciaCardiaca" HeaderText="FRECUENCIA CARDIACA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Estatura" DataField="EstaturaMetros" HeaderText="ESTATURA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DecimalPeso" DataField="Peso" HeaderText="PESO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="IMC" DataField="IMC" HeaderText="IMC" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="IMCDescription" DataField="IMCDescription" HeaderText="IMC DESCRIPCION" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo" DataField="Enfermedad" HeaderText="DIAGNOSTICO PRESUNTIVO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo2" DataField="Enfermedad2" HeaderText="DIAGNOSTICO PRESUNTIVO 2" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivo3" DataField="Enfermedad3" HeaderText="DIAGNOSTICO PRESUNTIVO 3" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DiagnosticoPresuntivoOtro" DataField="DiagnosticoPresuntivo" HeaderText="OTRO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="BiometriaHematica" DataField="BiometriaHematica" HeaderText="RESULTADOS DE LABORATORIO Y OTROS EXÁMENES" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Observaciones" DataField="Observaciones" HeaderText="OBSERVACIONES" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="DecimalCostoConsultaInternista" DataField="CostoConsultaInternista" 
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                HeaderText="COSTO CONSULTA" ItemStyle-HorizontalAlign="Right" />
                                --%>
                            <telerik:GridBoundColumn UniqueName="Medicamento" DataField="Medicamento" HeaderText="RECETA MEDICAMENTO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Presentacion" DataField="Presentacion" HeaderText="RECETA PRESENTACIÓN" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Concentracion" DataField="Concentracion" HeaderText="RECETA CONCENTRACIÓN" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="DecimalTotalRecetas" DataField="TotalRecetas"  
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                HeaderText="MONTO RECETAS" ItemStyle-HorizontalAlign="Right" />
                            --%>
                            <telerik:GridBoundColumn UniqueName="ProveedorEstudio" DataField="ProveedorEstudio" HeaderText="PROVEEDOR DE LABORATORIO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="TipoEstudio" DataField="TipoEstudio" HeaderText="TIPO DE LABORATORIO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="EstudioObservacion" DataField="EstudioObservacion" HeaderText="OBSERVACION DE LABORATORIO" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="DecimalTotalLaboratorios" DataField="TotalLaboratorios"  
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right"
                                HeaderText="MONTO LABORATORIOS" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            --%>
                            <telerik:GridBoundColumn UniqueName="ProveedorImagen" DataField="ProveedorImagen" HeaderText="PROVEEDOR DE IMAGENOLOGIA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="TipoImagen" DataField="TipoImagen" HeaderText="TIPO DE IMAGENOLOGIA" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="ImagenObservacion" DataField="ImagenObservacion" HeaderText="OBSERVACION DE IMAGENOLOGIA" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="DecimalTotalImagenes" DataField="TotalImagenes"  
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right"
                                HeaderText="MONTO IMAGENOLOGIA" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            --%>
                            <telerik:GridBoundColumn UniqueName="DerivacionMedico" DataField="DerivacionMedico" HeaderText="MÉDICO DE DERIVACIÓN" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DerivacionEspecialidad" DataField="DerivacionEspecialidad" HeaderText="ESPECIALIDAD DE DERIVACIÓN" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="DerivacionObservacion" DataField="DerivacionObservacion" HeaderText="OBSERVACION DERIVACIÓN" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="DecimalTotalDerivacion" DataField="TotalDerivacion"  
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right"
                                HeaderText="MONTO DERIVACIONES" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            --%>
                            <telerik:GridBoundColumn UniqueName="Clinica" DataField="Clinica" HeaderText="CLINICA DE INTERNACIÓN" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="Procedimiento" DataField="Procedimiento" HeaderText="PROCEDIMIENTO" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="IsCirugia" DataField="IsCirugiaForDisplay" HeaderText="Es Cirugia" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="InternacionObservacion" DataField="InternacionObservacion" 
                                HeaderText="OBSERVACION INTERNACIÓN" Visible="false" />
                            <%-- 
                            <telerik:GridBoundColumn UniqueName="DecimalTotalInternacion" DataField="TotalInternacion" 
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right"
                                HeaderText="MONTO INTERNACIONES" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            <telerik:GridBoundColumn UniqueName="DecimalTotalEmergencia" DataField="TotalEmergencia" 
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right" Visible="false"
                                HeaderText="MONTO EMERGENCIAS" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            <telerik:GridBoundColumn UniqueName="DecimalTotalOdontologia" DataField="TotalOdontologia" 
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right" Visible="false"
                                HeaderText="MONTO ODONTOLOGIA" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            <telerik:GridBoundColumn UniqueName="DecimalTotal" DataField="Total" 
                                DataFormatString="{0:###,##0.00}" FooterStyle-HorizontalAlign="Right"
                                HeaderText="TOTALES" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" />
                            --%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ReporteCasoMedicoODS" runat="server"
                    TypeName="Artexacta.App.Reportes.BLL.CasoMedicoBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SearchCasoMedico"
                    OnSelected="ReporteCasoMedicoODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="CasoMedicoCheckBox" PropertyName="Checked" DbType="Boolean" Name="casoMedico" />
                        <asp:ControlParameter ControlID="ReconsultaCheckBox" PropertyName="Checked" DbType="Boolean" Name="reconsulta" />
                        <asp:ControlParameter ControlID="EnfermeriaCheckBox" PropertyName="Checked" DbType="Boolean" Name="enfermeria" />
                        <asp:ControlParameter ControlID="EmergenciaCheckBox" PropertyName="Checked" DbType="Boolean" Name="emergencia" />
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

