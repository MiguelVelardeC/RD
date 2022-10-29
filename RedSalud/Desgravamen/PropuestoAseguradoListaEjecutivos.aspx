<%@ Page Title="Lista de Propuesto Asegurado" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PropuestoAseguradoListaEjecutivos.aspx.cs" Inherits="Desgravamen_PropuestoAseguradoListaEjecutivos" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="RedSalud" TagName="SearchControl" %>

<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span id="ListPropuestoAseguradoTitle" runat="server" class="title">Lista de Revisiones para Ejecutivos</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:LinkButton runat="server" NavigateUrl="~/Desgravamen/PropuestoAsegurado.aspx" Visible="false" Text="Agregar Propuesto Asegurado"  ID="LinkPropuestoAsegurado" OnClick="LinkPropuestoAsegurado_Click"></asp:LinkButton>
                </div>
                <asp:TextBox id="IsAccordionActive" Text="0" runat="server" Style="display:none;" />
                <asp:Panel runat="server" Visible="false">
                    <asp:TextBox ID="PaId" runat="server">
                    </asp:TextBox>
                    <asp:Button ID="CitaButton" runat="server" Text="Ir A Cita" OnClick="CitaButton_Click" />
                </asp:Panel>

                <RedSalud:SearchControl ID="SearchPA" runat="server"
                    Title="Búsqueda"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true" SavedSearchesID="SearchPA"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png"
                    Visible="false" />
                <div class="clear" style="margin-bottom: 10px;"></div>
                
                <asp:Panel id="AdminPanel" runat="server" CssClass="PanelAdmin" style="font-size: 12px;" DefaultButton="boton"> 
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                           <span style="margin-left:30px;">FILTROS DE BUSQUEDA</span>
                        </h3>
                    </a>
                    <div id="Contents" style="padding:1em 0.5em;">
                    ESTADO:
                    <asp:DropDownList ID="enlaceRapidoBusqueda" runat="server" 
                        AutoPostBack="false" 
                        OnSelectedIndexChanged="enlaceRapidoBusqueda_SelectedIndexChanged"
                        Style="width:311px; margin-left:15px;">
                        <asp:ListItem Text="Todos" Value=""> </asp:ListItem>
                        <asp:ListItem Text="Los casos NO aprobados solamente" Value="@APROBADO FALSE" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="Los casos aprobados" Value="@APROBADO TRUE"></asp:ListItem>
                    </asp:DropDownList> <br />
                    
                    <div class="clear" style="margin-bottom: 7px;"></div>
                    CLIENTE:
                    <asp:DropDownList ID="clientesComboBox" runat="server" 
                        AutoPostBack="false"
                        Visible="false"
                        Width="150px"
                        Height="22px"
                        style="margin-left:3px" 
                        >
                        <%--OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged" --%>
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    CIUDAD:
                    <asp:DropDownList ID="ciudadComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="100px">
                    </asp:DropDownList>
                    PRODUCTO:
                    <asp:DropDownList ID="tipoProductoComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="150px">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    FINANCIERAS:
                    <asp:DropDownList ID="financieraComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="170px">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <div class="clear" style="margin-bottom: 7px;"></div>
                    CITA:
                    <telerik:RadNumericTextBox id="CitaId" MinValue="0" NumberFormat-DecimalDigits="0" EmptyMessage="Numero de Cita" NumberFormat-GroupSeparator="" runat="server" width="150px" Style="margin-left: 36px;"/>
                    PROPUESTO ASEGURADO:
                    <telerik:RadTextBox ID="nombrePropuestoAsegurado" runat="server" EmptyMessage="Nombre Del Propuesto Asegurado" Width="250px" ></telerik:RadTextBox>                                           
                    <telerik:RadTextBox ID="nroDocumentoPropuestoAsegurado" runat="server" EmptyMessage="CI Del Propuesto Asegurado" Width="200px" ></telerik:RadTextBox>                                           
                    EJECUTIVOS:
                    <asp:DropDownList ID="EjecutivoComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="177px">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    </asp:DropDownList> 
                    <div class="clear" style="margin-bottom: 5px;"></div>                       
                    RANGO DE FECHAS: <br />
                    <div class="clear" style="margin-bottom: 5px;"></div>                                           
                    <telerik:RadDatePicker ID="FechaInicioCita" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="120px"></telerik:RadDatePicker>
                    <telerik:RadDatePicker ID="FechaFinCita" runat="server" DateInput-EmptyMessage="Fecha Final" Width="120px"></telerik:RadDatePicker> 
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="button"
                    OnClick="btnSearch_Click">
                    <span>BUSCAR</span>
                    </asp:LinkButton>                                                              
                    </div>                                                                              
                    <asp:Button id="boton" Text="" Style="display:none;" runat="server" OnClick="btnSearch_Click" />
                </asp:Panel>
                    <div class="clear" style="margin-bottom: 5px;"></div>

                <telerik:RadGrid ID="PropuestoAseguradoGridView" runat="server"
                    AutoGenerateColumns="false" DataSourceID="PropuestoAseguradoDataSource"
                    AllowPaging="false" 
                    OnItemCommand="PropuestoAseguradoGridView_ItemCommand"
                    OnItemDataBound="PropuestoAseguradoGridView_ItemDataBound"
                    OnDataBound="PropuestoAseguradoGridView_DataBound"
                    AllowMultiRowSelection="False">
                    <MasterTableView DataKeyNames="PropuestoAseguradoId" ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay propuestos asegurados registrados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        
                        <Columns>                           
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImprimirOrden" runat="server"
                                        ToolTip="Orden"
                                        CommandName="Orden"
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        ImageUrl="~/Images/Neutral/ExportPrint.gif" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridBoundColumn UniqueName="CitaDesgravamenId" DataField="CitaDesgravamenId"
                                HeaderText="Numero de Cita" />
                            <telerik:GridBoundColumn UniqueName="AprobadoDisplay" DataField="AprobadoDisplay"
                                HeaderText="Estado" />
                            <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre"
                                HeaderText="Nombre Propuesto Asegurado" />
                            <telerik:GridBoundColumn UniqueName="PropuestoAseguradoNroDocumento" DataField="PropuestoAseguradoNroDocumento"
                                HeaderText="CI Propuesto Asegurado" />
                            <telerik:GridBoundColumn UniqueName="TelefonoCelular" DataField="TelefonoCelular"
                                HeaderText="Contacto" />
                            <telerik:GridBoundColumn UniqueName="NombreCiudad" DataField="NombreCiudad"
                                HeaderText="Ciudad" />
                            <telerik:GridBoundColumn UniqueName="TipoProducto" DataField="TipoProductoForDisplay"
                                HeaderText="Producto" />
                            <telerik:GridBoundColumn UniqueName="Financiera" DataField="FinancieraForDisplay"
                                HeaderText="Financiera" />
                            <telerik:GridBoundColumn UniqueName="UsuarioRegistro" DataField="UsuarioRegistro"
                                HeaderText="Usuario" />
                            <telerik:GridBoundColumn UniqueName="FechaCreacionCita" DataField="FechaCreacionCitaForDisplay"
                                HeaderText="Fecha Creacion de la Cita" />
                            <telerik:GridBoundColumn UniqueName="FechaHoraCitaForDisplay" DataField="FechaHoraCitaForDisplay"
                                HeaderText="Fecha y Hora de la Cita" />
                            <telerik:GridTemplateColumn HeaderText="Examen Médico" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ExamenMEdicoCita" runat="server"
                                        Visible='<%# Convert.ToInt32(Eval("TieneExamenMedico")) == 1 %>'
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        CommandName="Examen"
                                        ImageUrl="~/Images/Neutral/historialMedico.png" Width="18px" />
                                    <asp:Image ID="NoNecesitaExamen" runat="server" Visible="false"
                                        ToolTip="No necesita examen médico"
                                        ImageUrl="~/Images/Neutral/delete_disabled.gif" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="NecesitaLaboratorio" DataField="NEcesitaLaboratorioForDisplay"
                                HeaderText="Necesita Estudios" />
                            <telerik:GridTemplateColumn HeaderText="Resultados de Estudios">
                                <ItemTemplate>
                                    <asp:ImageButton ID="FullReportePdfButton" runat="server"
                                        Visible="false"
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        CommandName="Completo"
                                        ImageUrl="~/Images/Neutral/fullpdf.png" Width="18px" />
                                    <asp:Repeater ID="LaboratoriosMainRepeater" runat="server"
                                        OnItemDataBound="Repeater1_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="proveedorMedicoLabo">
                                                <%# Eval("ProveedorNombre") %>: 
                                                <asp:Repeater ID="DocumentosRepeater" runat="server" 
                                                    OnItemCommand="DocumentosRepeater_ItemCommand">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="DownloadImageButton" runat="server"
                                                            ImageUrl='<%# Eval("Icon") %>'
                                                            CommandArgument='<%# Eval("FileStoragePath") %>'
                                                            Width="18px" CommandName='<%# Eval("Name") %>'
                                                            ToolTip='<%# "Archivo: " + Eval("Name").ToString() + " - Subido en: " + Eval("DateUploadedForDisplay").ToString() %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
            
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <ExportSettings ExportOnlyData="true" IgnorePaging="true" HideStructureColumns="false" FileName="PropuestosAsegurados">
                        <Pdf FontType="Subset" PaperSize="Letter" />
                        <Excel Format="Html" />
                        <Csv ColumnDelimiter="Colon" RowDelimiter="NewLine" />
                    </ExportSettings>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="PropuestoAseguradoDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.PropuestoAseguradoBLL"
                    SelectMethod="GetPropuestoAseguradoBySearchALL"
                    OnSelected="PropuestoAseguradoDataSource_Selected"
                    OnDataBinding="PropuestoAseguradoDataSource_DataBinding">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchPA" PropertyName="Sql" Name="whereSql" Type="String" />
                        <asp:ControlParameter ControlID="UserIdHiddenField" PropertyName="Value" Name="intUsuarioId" Type="Int32" />
                        <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="intClienteId" Type="Int32" />      
                        <asp:Parameter Name="eliminado" DefaultValue="false" Type="Boolean" />      
                        <%-- 
                        <asp:ControlParameter ControlID="FechaInicioCita" PropertyName="SelectedDate" Name="dtFechaInicioCita" Type="Datetime" />      
                        <asp:ControlParameter ControlID="FechaFinCita" PropertyName="SelectedDate" Name="dtFechaFinCita" Type="Datetime" />                
                        <asp:ControlParameter ControlID="financieraComboBox" PropertyName="SelectedValue" Name="intFinancieraId" Type="Int32" /> 
                        <asp:ControlParameter ControlID="ciudadComboBox" PropertyName="SelectedValue" Name="varCiudad" Type="String" />
                        <asp:ControlParameter ControlID="tipoProductoComboBox" PropertyName="SelectedValue" Name="varTipoProducto" Type="String" />               
                        <asp:ControlParameter ControlID="EjecutivoComboBox" PropertyName="SelectedValue" Name="intEjecutivoId" Type="Int32" />                
                        <asp:ControlParameter ControlID="CitaId" PropertyName="Value" Name="intCitaDesgravamenId" Type="Int32" />                
                        <asp:ControlParameter ControlID="nombrePropuestoAsegurado" PropertyName="Text" Name="varPropuestoAsegurado" Type="String" />
                        --%>
                        <asp:ControlParameter ControlID="Pager" PropertyName="CurrentRow" Name="firstRow" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="PageSize" Name="pageSize" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" Name="totalRows" PropertyName="TotalRows" Type="Int32" Direction="Output" />                    
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
    
    <asp:HiddenField ID="UserIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="UserAuthorizedAprobarHF" runat="server" Value="false" />
    <asp:HiddenField ID="UserAuthorizedToEditHiddenField" runat="server" Value="false" />
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

            $("#<%= clientesComboBox.ClientID %>").change(function () {
                
                $("#<%= ClienteIdHiddenField.ClientID %>").val($(this).val());
            });

            $("#<%= AdminPanel.ClientID %> h3").click(function () {
                if ($("#<%= IsAccordionActive.ClientID %>").val() != "0") {
                    $("#<%= IsAccordionActive.ClientID %>").val("0");
                } else {
                    $("#<%= IsAccordionActive.ClientID %>").val("1");
                }
            });


            $("#<%= LinkPropuestoAsegurado.ClientID %>").click(function () {
                var clienteValue = $("#<%= ClienteIdHiddenField.ClientID %>").val();
                if (clienteValue != null && clienteValue != undefined && clienteValue != "0") {
                    return true;
                }
                alert("No se ha definido un Cliente");
                return false;
            });

            $('.BotonDelete').click(function () {
                return confirm('Está seguro que quiere eliminar el caso? (Se eliminarán definitivamente: los archivos, el registor del laboratorio, el registro del médico y el caso en general)');
            });
        });

    </script>
</asp:Content>

