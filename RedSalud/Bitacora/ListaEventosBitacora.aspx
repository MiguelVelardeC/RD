<%@ Page Title="Lista de Eventos de Bitácora" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListaEventosBitacora.aspx.cs" Inherits="Bitacora_ListaEventosBitacora" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="RedSalud" TagName="SearchControl" %>

<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Eventos de la Bitácora</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    
                </div>

                <RedSalud:SearchControl ID="SearchBitacora" runat="server"
                    Title="Búsqueda"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true" SavedSearchesID="Bitacora"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />

                &nbsp;&nbsp;
                Enlaces rápidos: <asp:DropDownList ID="enlaceRapidoBusqueda" runat="server" 
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="enlaceRapidoBusqueda_SelectedIndexChanged">
                    <asp:ListItem Text="Todos" Value="" Selected="True"> </asp:ListItem>
                    <asp:ListItem Text="Eventos de Login al sistema" Value="@evento UserLogin" ></asp:ListItem>
                    <asp:ListItem Text="Creación de casos desgravamen" Value="@evento DESGInsertarCita"></asp:ListItem>
                </asp:DropDownList>
                    <div class="clear" style="margin-bottom: 5px;"></div>

                <telerik:RadGrid ID="BitacoraGridView" runat="server"
                    AutoGenerateColumns="false" 
                    DataSourceID="BitacoraDataSource"
                    AllowPaging="false" 
                    OnItemDataBound="BitacoraGridView_ItemDataBound"
                    AllowMultiRowSelection="False">
                    <MasterTableView DataKeyNames="Id" ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay eventos en la bitácora, ver si el web.config está bien configurado</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        
                        <Columns>
                            
                            <telerik:GridBoundColumn UniqueName="Id" DataField="Id"
                                HeaderText="Id" />
                            <telerik:GridBoundColumn UniqueName="Fecha" DataField="Fecha"
                                HeaderText="Fecha/Hora" />
                            <telerik:GridBoundColumn UniqueName="TipoEvento" DataField="TipoEvento"
                                HeaderText="Tipo de Evento" />
                            <telerik:GridBoundColumn UniqueName="Empleado" DataField="Empleado"
                                HeaderText="Usuario" />
                            <telerik:GridBoundColumn UniqueName="TipoObjeto" DataField="TipoObjeto"
                                HeaderText="Módulo" />
                            <telerik:GridBoundColumn UniqueName="IdObjeto" DataField="IdObjeto"
                                HeaderText="Id Obj." />
                            <telerik:GridBoundColumn UniqueName="Mensaje" DataField="Mensaje"
                                HeaderText="Mensaje" />
                        </Columns>
            
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="BitacoraDataSource" runat="server"
                    TypeName="Artexacta.App.Bitacora.BLL.EventoBitacoraBLL"
                    SelectMethod="getEventoBitacoraList"
                    OnSelected="BitacoraDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchBitacora" PropertyName="Sql" Name="search" Type="String" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="CurrentRow" Name="firstRow" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="PageSize" Name="pageSize" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" Name="totalRows" PropertyName="TotalRows" Type="Int32" Direction="Output" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <RedSalud:PagerControl ID="Pager" runat="server" 
                    PageSize="20" 
                    CurrentRow="0" 
                    InvisibilityMethod="PropertyControl" 
                    OnPageChanged="Pager_PageChanged" />
                <div class="clear"></div>
            </div>
        </div>
    </div>

</asp:Content>

