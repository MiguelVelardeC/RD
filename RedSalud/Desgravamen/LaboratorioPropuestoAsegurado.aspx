<%@ Page Title="Laboratorio - Propuesto Asegurado" Language="C#" 
    MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="LaboratorioPropuestoAsegurado.aspx.cs" Inherits="Desgravamen_LaboratorioPropuestoAsegurado" %>

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
                <span class="title">Lista de Propuestos Asegurados para Estudios</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="ReporteLink" runat="server"
                        NavigateUrl="~/Desgravamen/ReporteEstudioXPA.aspx">
                        Reporte de Estudios x Propuesto Asegurado
                    </asp:HyperLink>
                </div>
                    
                <p>
                    <asp:Literal ID="LaboratorioNombreLabel" runat="server"></asp:Literal>
                </p>

                <RedSalud:SearchControl ID="SearchLabo" runat="server"
                    Title="Búsqueda"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true" SavedSearchesID="SearchLabo"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />                
                <asp:DropDownList ID="clientesComboBox" runat="server" 
                    AutoPostBack="true"                     
                    OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                </asp:DropDownList><br />                
                <div class="clear" style="margin-bottom: 5px;"></div>
                <asp:Panel id="AdminPanel" runat="server" CssClass="PanelAdmin" style="font-size: 12px;" DefaultButton="boton"> 
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                           <span style="margin-left:30px;">FILTROS DE ADMINISTRACION</span>
                        </h3>
                    </a>
                    <div id="Contents" runat="server" style="padding:1em 0.5em;">
                        CLIENTES:
                        <asp:DropDownList ID="clientesComboBoxAdmin" runat="server" 
                            AutoPostBack="false"
                            Width="155px" 
                            OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged">
                            <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        PROVEEDORES:
                        <asp:DropDownList ID="ProveedoresComboBox" runat="server" 
                            AutoPostBack="false"
                            Width="100px">
                        </asp:DropDownList>
                        CITA:
                        <telerik:RadNumericTextBox id="CitaId" MinValue="0" NumberFormat-DecimalDigits="0" EmptyMessage="Numero de Cita" NumberFormat-GroupSeparator="" runat="server" width="150px" Style=""/>
                        PROPUESTO ASEGURADO:
                        <telerik:RadTextBox ID="nombrePropuestoAsegurado" runat="server" EmptyMessage="Nombre Del Propuesto Asegurado" Width="250px" ></telerik:RadTextBox>                                           
                        <div class="clear" style="margin-bottom: 5px;"></div>                       
                        RANGO DE FECHAS DE LA CITA: <br />
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
                    AllowMultiRowSelection="False">
                    <MasterTableView DataKeyNames="PropuestoAseguradoId" ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay propuestos asegurados registrados para Laboratorio</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Ver" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="VerEstudios" runat="server"
                                        ToolTip="Ver Propuesto Asegurado"
                                        CommandName="VerEstudios"
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Info" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="InfoPA" runat="server"
                                        ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CitaDesgravamenId" DataField="CitaDesgravamenId"
                                HeaderText="Nro" />
                            <telerik:GridBoundColumn UniqueName="Nombre" DataField="NombreCompleto"
                                HeaderText="Nombre Propuesto Asegurado" />
                            <telerik:GridBoundColumn UniqueName="CarnetIdentidad" DataField="CarnetIdentidad"
                                HeaderText="Cédula Identidad" />                            
                            <telerik:GridBoundColumn UniqueName="ClienteNombre" DataField="ClienteNombre"
                                HeaderText="Cliente" />
                            <telerik:GridBoundColumn UniqueName="FechaNacimiento" DataField="FechaNacimientoForDisplay"
                                HeaderText="Fecha de Nacimiento" Display="false" />
                            <telerik:GridBoundColumn UniqueName="NombreProveedor" DataField="NombreProveedor"
                                HeaderText="Proveedor" Display="false" />
                            <telerik:GridBoundColumn UniqueName="EstudioNombre" DataField="EstudioNombre"
                                HeaderText="Estudio" />   
                            <telerik:GridBoundColumn UniqueName="EstudioId" DataField="EstudioId"
                                Display="false" />  
                            <telerik:GridBoundColumn UniqueName="CobroAsegurado" DataField="CobroAseguradoForDisplay"
                                HeaderText="Cobrar al Propuesto Asegurado" Display="false" />
                            <telerik:GridBoundColumn UniqueName="FechaCitaLabo" DataField="FechaCitaLaboForDisplay"
                                HeaderText="Fecha de Cita" />
                            <telerik:GridBoundColumn UniqueName="FechaAtencionLabo" DataField="FechaAtencionLaboForDisplay"
                                HeaderText="Fecha Atención Laboratorio" />                          
                            <telerik:GridTemplateColumn HeaderText="Documentos de Cita">
                                <ItemTemplate>
                                    <asp:Repeater ID="DocumentosRepeater" runat="server" 
                                        DataSource='<%# Eval("LaboratorioFiles") %>'
                                        OnItemCommand="DocumentosRepeater_ItemCommand" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="DownloadImageButton" runat="server"
                                                ImageUrl='<%# Eval("Icon") %>'
                                                CommandArgument='<%# Eval("FileStoragePath") %>'
                                                Width="18px" 
                                                CommandName='<%# Eval("Name") %>'
                                                ToolTip='<%# "Archivo: " + Eval("Name").ToString() + " - Subido en: " + Eval("DateUploadedForDisplay").ToString() %>'></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
            
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                </telerik:RadGrid>
                <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
                <asp:ObjectDataSource ID="PropuestoAseguradoDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.CitaDesgravamenBLL"
                    SelectMethod="GetListaPropuestoAseguradoEstudioBySearch"
                    OnSelected="PropuestoAseguradoDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="UserIdHiddenField" PropertyName="Value" Type="Int32" Name="userId" />
                        <asp:ControlParameter ControlID="SearchLabo" PropertyName="Sql" Name="whereSql" Type="String" />
                        <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="clienteId" Type="Int32" />
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

    <asp:HiddenField ID="UserIdHiddenField" runat="server" Value="-1" />
        <script type="text/javascript">

            $(document).ready(function () {

                $("#PanelButton").click(function () {
                    if (!$("#<%= Contents.ClientID %>").is(":visible")) {
                        $("#<%= Contents.ClientID %>").slideDown();
                        $("#<%= Contents.ClientID %>").val("0");
                    } else {
                        $("#<%= Contents.ClientID %>").slideUp();
                        $("#<%= Contents.ClientID %>").val("1");
                    }
                    return false;
                });

                $("#<%= clientesComboBoxAdmin.ClientID %>").change(function () {

                    $("#<%= ClienteIdHiddenField.ClientID %>").val($(this).val());

                });

        });

    </script>
</asp:Content>

