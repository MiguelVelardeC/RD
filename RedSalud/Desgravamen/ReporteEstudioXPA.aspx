<%@ Page Title="Panel de Control de Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteEstudioXPA.aspx.cs" Inherits="Desgravamen_ReporteEstudioXPA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

<script>
    function AvoidCalling() {
        return false;
    }
</script>
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Estudios realizados a Propuestos Asegurados</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                   
                </div>
                <div>
                     <div style="display:block; float:left;">
                        <span class="label" style="">Ciudad</span>                    
                        <asp:DropDownList ID="CiudadesCombo" runat="server" 
                            AutoPostBack="false">
                            <%-- 
                        <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                        <asp:ListItem Text="COCHABAMBA" Value="CBB"></asp:ListItem>
                        <asp:ListItem Text="LA PAZ" Value="LPZ"></asp:ListItem>
                        <asp:ListItem Text="SANTA CRUZ" Value="STC"></asp:ListItem>
                                --%>
                        </asp:DropDownList>               
                        <span class="label" style="">Proveedor</span>   
                        <telerik:RadComboBox ID="LaboratoriosCombo" runat="server" 
                            AutoPostBack="false" DataTextField="ProveedorNombre" DataValueField="ProveedorMedicoId"
                            OnSelectedIndexChanged="LaboratoriosCombo_SelectedIndexChanged"  >
                        </telerik:RadComboBox>
                        <span class="label" style="">Cliente</span>                    
                        <asp:DropDownList ID="clientesComboBox" runat="server" 
                            AutoPostBack="true" OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="display:block; float:right; width:80%;">
                        <span class="label" style="">Financiera</span>
                        <asp:DropDownList ID="financieraComboBox" runat="server" 
                            AutoPostBack="false"
                            Width="177px">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList><br />
                        <span class="label" style="">Cobro al Cliente</span>
                        <asp:DropDownList ID="cobroClienteCombo" runat="server" 
                            AutoPostBack="false"
                            Width="177px">
                        <asp:ListItem Text="TODOS" Value="TODOS"></asp:ListItem>
                        <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                        </asp:DropDownList>                 
                        <span class="label" style="">Número de Cita</span>
                        <telerik:RadNumericTextBox id="CitaId" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" runat="server" width="177px"/>
                        <%-- 
                        <span class="label" style="display:inline">Cantidad de Estudios por Propuesto Asegurado</span>                                
                            --%>
                    </div>
                </div>                
                <span class="label">PROPUESTO ASEGURADO</span>
                <asp:TextBox ID="PropuestoAseguradoTextBox" runat="server" CssClass="normalField" Style="width:409px !important;"></asp:TextBox>
                <div class="contentMenu">
                   
                </div>                                
                <span class="label" style="display:inline">Fecha Inicio</span>  
                <telerik:RadDatePicker ID="FechaInicio" runat="server"></telerik:RadDatePicker>
                &nbsp;&nbsp;&nbsp;
                <span class="label" style="display:inline">Fecha Fin</span>  
                <telerik:RadDatePicker ID="FechaFin" runat="server"></telerik:RadDatePicker>
                <asp:LinkButton ID="GenerateReportButton" runat="server" CssClass="button"
                    OnClick="GenerateReportButton_Click">
                    <span>Generar Reporte</span>
                </asp:LinkButton>
                <asp:LinkButton ID="GenerateReportExcelButton" runat="server" CssClass="button"
                    OnClick="GenerateReportExcelButton_Click">
                    <span>Exportar a Excel</span>
                </asp:LinkButton>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FechaInicio"
                        ErrorMessage="Debe seleccionar una Fecha de Inicio" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FechaFin"
                        ErrorMessage="Debe seleccionar una Fecha de Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:CompareValidator runat="server" ControlToValidate="FechaInicio"
                        ControlToCompare="FechaFin" Operator="LessThanEqual"
                        ErrorMessage="La Fecha de Inicio no puede ser mayor que la Fecha Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:CompareValidator>
                </div>
                
                <RedSalud:SearchControl ID="SearchPA" runat="server"
                    Title="Búsqueda"
                    DisplayHelp="true"
                    Visible="false"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true" SavedSearchesID="SearchReportePA"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />
                
                <div style="width:100%;overflow:scroll;">
                <telerik:RadGrid ID="EstudiosPorPAGrid" runat="server"
                    AutoGenerateColumns="true" 
                    AllowPaging="false" 
                    DataSourceID="EstudiosPorPADataSource"
                    OnDataBound="EstudiosPorPAGrid_DataBound" OnItemDataBound="EstudiosPorPAGrid_ItemDataBound"
                    AllowMultiRowSelection="False"
                    ShowFooter="true"
                    OnColumnCreated="EstudiosPorPAGrid_ColumnCreated"
                     >
                    <ExportSettings Excel-Format="Xlsx" FileName="EstudiosPorPA" ExportOnlyData="true"></ExportSettings>
                    <ClientSettings>
                        <ClientEvents OnColumnResizing="AvoidCalling" />
                        <Scrolling FrozenColumnsCount="1" />
                    </ClientSettings>
                    <MasterTableView 
                        ExpandCollapseColumn-Display="false" 
                        CommandItemDisplay="Top"
                        UseAllDataFields="true" >
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Estudios Realizados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px"/>
                            
                    </MasterTableView>
                </telerik:RadGrid>
                </div>
                <asp:ObjectDataSource ID="EstudiosPorPADataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL"
                    SelectMethod="GetReporteCantidadEstudiosPorPA"
                    OnSelected="EstudiosPorPADataSource_Selected"
                    OnDataBinding="EstudiosPorPADataSource_DataBinding">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchPA" Name="whereSql" Type="String" PropertyName="Sql" />
                        <asp:ControlParameter ControlID="FechaInicio" Name="fechaInicio" Type="DateTime" PropertyName="SelectedDate" />
                        <asp:ControlParameter ControlID="FechaFin" Name="fechaFin" Type="DateTime" PropertyName="SelectedDate" />
                        <asp:ControlParameter ControlID="LaboratoriosCombo" Name="proveedorMedicoId" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="clientesComboBox" Name="clienteId" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="CiudadesCombo" Name="ciudadId" Type="String" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="financieraComboBox" Name="financieraId" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="CitaId" Name="citaId" Type="Int32" PropertyName="Value" />
                        <asp:ControlParameter ControlID="cobroClienteCombo" Name="cobroCliente" Type="String" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="PropuestoAseguradoTextBox" Name="propuestoAsegurado" Type="String" PropertyName="Text" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
</asp:Content>

