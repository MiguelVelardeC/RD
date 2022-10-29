<%@ Page Title="Lista de Medico Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorDesgravamenLista.aspx.cs" Inherits="Desgravamen_ProveedorDesgravamenLista" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="RedSalud" TagName="SearchControl" %>

<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Proveedores Desgravamen</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" NavigateUrl="~/Desgravamen/ProveedorDesgravamenDetalle.aspx" Text="Agregar Proveedor Desgravamen"></asp:HyperLink>
                </div>

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

                &nbsp;&nbsp;
                <asp:Panel runat="server" DefaultButton="botonPorDefecto">
                    PROVEEDOR:
                    <telerik:RadTextBox id="ProveedorNombreTextBox" runat="server" EmptyMessage="Nombre del Proveedor"></telerik:RadTextBox>                
                    USUARIO:
                    <telerik:RadTextBox id="ProveedorUsuarioTextBox" Width="250px" runat="server" EmptyMessage="Nombre de Usuario del Proveedor"></telerik:RadTextBox>                                    
                    CIUDAD:
                    <asp:DropDownList ID="ciudadComboBox" runat="server" 
                        AutoPostBack="false"
                        Width="100px">
                    </asp:DropDownList>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="button"
                    OnClick="btnSearch_Click">
                    <span>BUSCAR</span>
                    </asp:LinkButton>

                    <asp:Button id="botonPorDefecto" Text="" Style="display:none;" runat="server" OnClick="btnSearch_Click" />
                </asp:Panel>
                    <div class="clear" style="margin-bottom: 5px;"></div>

                <telerik:RadGrid ID="ProveedorDesgravamenGridView" runat="server"
                    AutoGenerateColumns="false" DataSourceID="ProveedorDesgravamenDataSource"
                    AllowPaging="false" 
                    OnItemCommand="ProveedorDesgravamenGridView_ItemCommand"
                    OnItemDataBound="ProveedorDesgravamenGridView_ItemDataBound"
                    OnDataBound="ProveedorDesgravamenGridView_DataBound"
                    AllowMultiRowSelection="False">
                    <MasterTableView DataKeyNames="ProveedorMedicoId" ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No existen Proveedores Desgravamen Registrados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DeletePA" runat="server" CssClass="BotonDelete"
                                        ToolTip="Eliminar Proveedor Desgravamen"
                                        CommandName="Delete"
                                        CommandArgument='<%# Eval("ProveedorMedicoId").ToString() %>'
                                        ImageUrl="~/Images/Neutral/delete.png" Width="18px"
                                        Visible="false"  />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="VerPA" runat="server"
                                        ToolTip="Ver Proveedor Desgravamen"
                                        CommandName="VerPA"
                                        CommandArgument='<%# Eval("ProveedorMedicoId") %>'
                                        ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridBoundColumn UniqueName="ProveedorNombre" DataField="ProveedorNombre"
                                HeaderText="Nombre" />
                            <telerik:GridBoundColumn UniqueName="Username" DataField="Username"
                                HeaderText="Username" />
                            <telerik:GridBoundColumn UniqueName="ciudadNombre" DataField="ciudadNombre"
                                HeaderText="Ciudad" />  
                            <telerik:GridBoundColumn UniqueName="EstudiosHabilitadosDisplay" DataField="EstudiosHabilitadosDisplay"
                                HeaderText="Contiene Estudios Activos" />                      
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
                <asp:ObjectDataSource ID="ProveedorDesgravamenDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.ProveedorMedicoBLL"
                    SelectMethod="GetProveedorDesgravamenBySearch"
                    OnSelected="ProveedorDesgravamenDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchPA" PropertyName="Sql" Name="varSql" Type="String" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="PageSize" Name="pageSize" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="CurrentRow" Name="firstRow" Type="Int32" />
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

    <!--<asp:HiddenField ID="UserIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="UserAuthorizedAprobarHF" runat="server" Value="false" />-->

    <script type="text/javascript">

        $(document).ready(function () {
            $('.BotonDelete').click(function () {
                return confirm('Está seguro que quiere eliminar el Proveedor? ');
            });
        });

    </script>
</asp:Content>

