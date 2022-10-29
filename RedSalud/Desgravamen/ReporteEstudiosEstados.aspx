<%@ Page Title="Panel de Control de Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteEstudiosEstados.aspx.cs" Inherits="Desgravamen_ReporteEstudiosEstados" %>

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
                <span class="title">Estados de Estudios</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                   
                </div>
                <div style="display:inline-flex; vertical-align:top; width:100%;">
                    <div style="display:block; width:19%;">
                        <span class="label" style="">Ciudad</span>                    
                        <asp:DropDownList ID="CiudadesCombo" runat="server" 
                            AutoPostBack="false" Style="display:inline" Width="200px">
                            <%-- 
                        <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                        <asp:ListItem Text="COCHABAMBA" Value="CBB"></asp:ListItem>
                        <asp:ListItem Text="LA PAZ" Value="LPZ"></asp:ListItem>
                        <asp:ListItem Text="SANTA CRUZ" Value="STC"></asp:ListItem>
                                --%>
                        </asp:DropDownList>
                    </div>                    
                    <div style="display:block; width:19%;">
                        <span class="label" style="">Proveedor</span>   
                        <telerik:RadComboBox ID="LaboratoriosCombo" runat="server" 
                            AutoPostBack="false" DataTextField="ProveedorNombre" DataValueField="ProveedorMedicoId"
                            OnSelectedIndexChanged="LaboratoriosCombo_SelectedIndexChanged" Width="200px">
                        </telerik:RadComboBox>
                    </div>
                    <div style="display:block; width:30%;">
                        <span class="label" style="">Cliente</span>                    
                        <asp:DropDownList ID="clientesComboBox" runat="server" 
                            AutoPostBack="true" OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged"
                            Style="display:inline;" Width="200px">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="display:inline-flex; vertical-align:top; width:100%;">
                    <div style="display:block; width:19%;">
                        <span  class="label" style="">Estudios</span>                 
                        <asp:DropDownList ID="estudiosComboBox" runat="server" 
                            AutoPostBack="false"
                            Width="200px">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>                
                    </div>
                    <div style="display:block; width:19%;">
                        <span  class="label" style="">Estado de Aprobacion</span>
                        <asp:DropDownList ID="comboEstadoAprobado" runat="server" 
                            AutoPostBack="false"
                            Width="200px">
                        <asp:ListItem Text="TODOS" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="APROBADO" Value="1"></asp:ListItem>
                        <asp:ListItem Text="NO APROBADO" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="display:block; width:30%;">
                        <span  class="label" style="">Estado de Estudio</span>
                        <asp:DropDownList ID="comboEstadoRealizado" runat="server" 
                            AutoPostBack="false"
                            Width="200px">
                        <asp:ListItem Text="TODOS" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="REALIZADO" Value="1"></asp:ListItem>
                        <asp:ListItem Text="NO REALIZADO" Value="0"></asp:ListItem>
                        </asp:DropDownList>                                                   
                    </div>
                </div>                                                
                <div class="contentMenu">
                   
                </div>
                                    
                <span  class="label" style="">Rango Fechas de Atencion</span>
                <div style="display:inline-flex; vertical-align:top; width:100%;">
                    <div style="display:block; width:16%;">
                        <span class="label" style="">Fecha Inicial</span>  
                        <telerik:RadDatePicker ID="dtFechaInicioAtencion" runat="server"></telerik:RadDatePicker>
                    </div>
                    <div style="display:block; width:30%;">
                        <span class="label" style="">Fecha Final</span>  
                        <telerik:RadDatePicker ID="dtFechaFinAtencion" runat="server"></telerik:RadDatePicker>
                    </div>

                </div>
                <span  class="label" style="">Incluir Fecha de Citas</span>
                <asp:CheckBox ID="IncludeFechaCita" runat="server" AutoPostBack="false" /><br />               
                <span  class="label rangoCitas" style="">Rango Fechas Citas</span>                    
                <div class="rangoCitas" style="display:inline-flex; vertical-align:top; width:100%;">
                    <div style="display:block; width:16%;">                               
                        <span class="label" style="">Fecha Inicial</span>  
                        <telerik:RadDatePicker ID="dtFechaInicioCita" runat="server"></telerik:RadDatePicker>
                    </div>
                    <div style="display:block; width:30%;">
                        <span class="label" style="">Fecha Final</span>  
                        <telerik:RadDatePicker ID="dtFechaFinCita" runat="server"></telerik:RadDatePicker>
                    </div>
                </div>             
                
                <asp:LinkButton ID="GenerateReportButton" runat="server" CssClass="button"
                    OnClick="GenerateReportButton_Click" Style="margin-top: 10px; margin-bottom: 10px;">
                    <span>Generar Reporte</span>
                </asp:LinkButton>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dtFechaInicioAtencion"
                        ErrorMessage="Debe seleccionar una Fecha de Inicio" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dtFechaFinAtencion"
                        ErrorMessage="Debe seleccionar una Fecha de Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:CompareValidator runat="server" ControlToValidate="dtFechaInicioAtencion"
                        ControlToCompare="dtFechaFinAtencion" Operator="LessThanEqual"
                        ErrorMessage="La Fecha de Inicio no puede ser mayor que la Fecha Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:CompareValidator>
                </div>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dtFechaInicioCita"
                        ErrorMessage="Debe seleccionar una Fecha de Inicio" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dtFechaFinCita"
                        ErrorMessage="Debe seleccionar una Fecha de Fin" Display="Dynamic"
                        ValidationGroup="EstudiosPorFinanciera">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="validation">
                    <asp:CompareValidator runat="server" ControlToValidate="dtFechaInicioCita"
                        ControlToCompare="dtFechaFinCita" Operator="LessThanEqual"
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
                    AutoGenerateColumns="false" 
                    AllowPaging="false" 
                    DataSourceID="EstudiosPorEstadoDataSource"
                    OnDataBound="EstudiosPorPAGrid_DataBound"
                    OnItemDataBound="EstudiosPorPAGrid_ItemDataBound"
                    AllowMultiRowSelection="False"
                    ShowFooter="true">
                    <ExportSettings Excel-Format="Xlsx" FileName="EstadosDelEstudios" ExportOnlyData="true"></ExportSettings>
                    
                    <ClientSettings>
                        <ClientEvents OnColumnResizing="AvoidCalling" />
                        <Scrolling FrozenColumnsCount="1" />
                    </ClientSettings>
                    <MasterTableView 
                        ExpandCollapseColumn-Display="false" 
                        CommandItemDisplay="Top"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true" >
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Estudios Realizados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px"/>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="CitaDesgravamenId" DataField="CitaDesgravamenId"
                                HeaderText="Cita" />
                            <telerik:GridBoundColumn UniqueName="CiudadNombre" DataField="CiudadNombre"
                                HeaderText="Ciudad" />
                            <telerik:GridBoundColumn UniqueName="ProveedorMedicoNombre" DataField="ProveedorMedicoNombre"
                                HeaderText="Proveedor" />
                            <telerik:GridBoundColumn UniqueName="NombreCompleto" DataField="NombreCompleto"
                                HeaderText="Propuesto Asegurado Nombre" />
                            <telerik:GridBoundColumn UniqueName="CarnetIdentidad" DataField="CarnetIdentidad"
                                HeaderText="Carnet Identidad" />    
                            <telerik:GridBoundColumn UniqueName="FinancieraNombre" DataField="FinancieraNombre"
                                HeaderText="Financiera" />      
                            <telerik:GridBoundColumn UniqueName="TipoProductoDescripcion" DataField="TipoProductoDescripcion"
                                HeaderText="Tipo Producto" />       
                            <telerik:GridBoundColumn UniqueName="EstudioNombre" DataField="EstudioNombre"
                                HeaderText="Estudio" />         
                            <telerik:GridBoundColumn UniqueName="FechaCita" DataField="FechaCita"
                                HeaderText="Fecha Cita" />           
                            <telerik:GridBoundColumn UniqueName="FechaAtencion" DataField="FechaAtencion"
                                HeaderText="Fecha Atencion" />           
                            <telerik:GridBoundColumn UniqueName="FechaRealizado" DataField="FechaRealizado"
                                HeaderText="Fecha Realizado" />           
                            <telerik:GridBoundColumn UniqueName="EstadoAprobado" DataField="EstadoAprobado"
                                HeaderText="Estado Aprobado" />              
                            <telerik:GridBoundColumn UniqueName="EstadoRealizado" DataField="EstadoRealizado"
                                HeaderText="Estado Realizado" /> 
                        </Columns>    
                    </MasterTableView>
                </telerik:RadGrid>
                </div>
                <asp:ObjectDataSource ID="EstudiosPorEstadoDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL"
                    SelectMethod="GetReporteEstadoEstudios"
                    OnSelected="EstudiosPorPADataSource_Selected"
                    OnDataBinding="EstudiosPorPADataSource_DataBinding">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="dtFechaInicioAtencion" Name="datFechaInicioAtencion" Type="DateTime" PropertyName="SelectedDate" />
                        <asp:ControlParameter ControlID="dtFechaFinAtencion" Name="datFechaFinAtencion" Type="DateTime" PropertyName="SelectedDate" />
                        <asp:ControlParameter ControlID="dtFechaInicioCita" Name="datFechaInicioCita" Type="DateTime" PropertyName="SelectedDate" />
                        <asp:ControlParameter ControlID="dtFechaFinCita" Name="datFechaFinCita" Type="DateTime" PropertyName="SelectedDate" />
                        <%-- <asp:ControlParameter ControlID="LaboratoriosCombo" Name="proveedorMedicoId" Type="Int32" PropertyName="SelectedValue" /> --%>
                        <asp:ControlParameter ControlID="clientesComboBox" Name="intClienteId" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="estudiosComboBox" Name="intEstudioId" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="CiudadesCombo" Name="varCiudad" Type="String" PropertyName="SelectedValue" />
                        <%-- <asp:ControlParameter ControlID="financieraComboBox" Name="financieraId" Type="Int32" PropertyName="SelectedValue" /> --%>
                        <%-- <asp:ControlParameter ControlID="CitaId" Name="citaId" Type="Int32" PropertyName="Value" /> --%>
                        <asp:ControlParameter ControlID="IncludeFechaCita" Name="bitIncludeFechaCita" Type="Boolean" PropertyName="Checked" />
                        <asp:ControlParameter ControlID="comboEstadoAprobado" Name="bitEstadoAprobado" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="comboEstadoRealizado" Name="bitEstadoRealizado" Type="Int32" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="LaboratoriosCombo" Name="intProveedorMedicoId" Type="Int32" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
<script>
    $(document).ready(function () {

        $('.rangoCitas').hide();

        $("#<%= IncludeFechaCita.ClientID%>").change(function () {
            if ($(this).is(':checked')) {
                $('.rangoCitas').show();
            } else {
                $('.rangoCitas').hide();
            }
        });
    });
</script>
</asp:Content>

