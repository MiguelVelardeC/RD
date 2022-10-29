<%@ Page Title="Proveedor" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorList.aspx.cs" Inherits="Proveedor_ProveedorList" %>
<%@ Register Src="../UserControls/SearchUserControl/SearchControl.ascx" TagName="SearchControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Proveedores" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:LinkButton ID="NewProveedor" runat="server"
                    Text="Agregar un nuevo Proveedor"
                    OnClick="NewProveedor_Click" />
            </div>
            <div>
                <div style="margin-bottom: 10px;">
                    <uc1:SearchControl ID="UserSearchControl" runat="server" Title="Buscar: " DisplayHelp="false"
                        DisplayContextualHelp="false" CssSearch="CSearch" CssSearchError="CSearchErrorPanel"
                        CssSearchAdvanced="CSearch_Advanced_Mask"
                        CssSearchHelp="CSearchHelpPanel" ImageErrorUrl="~/Images/neutral/exclamation.png"
                        ImageHelpUrl="~/Images/neutral/Help.png" />
                </div>
                <div class="left">
                    <asp:Label Text="Ciudad:" runat="server"
                        CssClass="label left" />
                    <telerik:RadComboBox ID="CiudadDDL" runat="server"
                        ClientIDMode="Static"
                        DataSourceID="CiudadODS"
                        DataValueField="CiudadId"
                        DataTextField="Nombre"
                        EmptyMessage="[Todas]"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="CiudadDDL_SelectedIndexChanged"
                        CssClass="bigField left">
                    </telerik:RadComboBox>
                    <asp:ObjectDataSource ID="CiudadODS" runat="server"
                        TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                        OldValuesParameterFormatString="{0}"
                        SelectMethod="getCiudadList"
                        OnSelected="CiudadODS_Selected"></asp:ObjectDataSource>
                </div>
                <div class="right" style="margin-bottom: 10px; ">
                    Mostrados
                    <asp:Literal ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Literal>
                    - 
                    <asp:Literal ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Literal>
                    de 
                    <asp:Literal ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Literal>
                </div>
                <div style="clear: both; margin-bottom: 5px;">
                    <telerik:RadGrid ID="ProveedorRadGrid" runat="server"
                        AutoGenerateColumns="false"
                        AllowPaging="false"
                        OnItemCommand="ProveedorRadGrid_ItemCommand"
                        MasterTableView-DataKeyNames="ProveedorId">
                        <MasterTableView>
                            <NoRecordsTemplate>
                                <asp:Label runat="server" Text="No hay Proveedors Registrados"></asp:Label>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridButtonColumn UniqueName="SeleccionarDeleteCommandColumn"
                                    CommandName="Select"
                                    ButtonType="ImageButton"
                                    ItemStyle-Width="24px"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="24px"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ImageUrl="~/Images/Neutral/select.png" />

                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                    CommandName="Eliminar"
                                    ButtonType="ImageButton"
                                    ItemStyle-Width="40px"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ImageUrl="~/Images/neutral/delete.png"
                                    ConfirmText="¿Está seguro que desea eliminar el Proveedor?" />
                                <telerik:GridBoundColumn DataField="TipoProveedorId" Visible="false" />
                                <telerik:GridBoundColumn DataField="NombreTipoProveedor" HeaderText="Tipo de Proveedor" />
                                <telerik:GridBoundColumn DataField="NombreCompletoOrJuridico" HeaderText="Nombre (ID)" />
                                <telerik:GridBoundColumn DataField="NombreEspecialidad" HeaderText="ESPECIALIDAD" />
                                <telerik:GridBoundColumn DataField="Nit" HeaderText="NIT" 
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="TelefonoOficina" HeaderText="Teléfono Oficina" 
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="CostoConsulta" HeaderText="Costo de Consulta" />
                                <telerik:GridBoundColumn DataField="PorcentageDescuento" HeaderText="Porcentage de Descuento"
                                    DataFormatString="{0} %" />
                                <telerik:GridBoundColumn DataField="CostoConsultaConDescuento" HeaderText="Costo de Consulta con Descuento" />
                                <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />
                                <telerik:GridBoundColumn DataField="Estado" Visible="false" />
                                <telerik:GridTemplateColumn HeaderText="Estado" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:Literal Text='<%#Eval("Estado").ToString() == "ACTIVO" ? "Activo" : "Inactivo" %>' runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="FechaActualizacion" HeaderText="Fecha de Actualización" HeaderStyle-Width="140px" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div id="ButtonsPanel" runat="server" class="buttonsPanel">
                    <asp:Button CssClass="button" ID="PrimeroButton" runat="server" Text="Primera" OnClick="PrimeroButton_Click" />
                    <asp:Button CssClass="button" ID="AnteriorRapidoButton" runat="server" Text="-5 P&aacute;ginas" OnClick="AnteriorRapidoButton_Click" />
                    <asp:Button CssClass="button" ID="AnteriorButton" runat="server" Text="Anterior" OnClick="AnteriorButton_Click" />
                    <asp:Button CssClass="button" ID="SiguienteButton" runat="server" Text="Siguiente" OnClick="SiguienteButton_Click" />
                    <asp:Button CssClass="button" ID="SiguienteRapidoButton" runat="server" Text="+5 P&aacute;ginas" OnClick="SiguienteRapidoButton_Click" />
                    <asp:Button CssClass="button" ID="UltimoButton" runat="server" Text="Ultimo" OnClick="UltimoButton_Click" />
                </div>
            </div>
            <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
            <asp:HiddenField ID="TotalRowsHF" runat="server" Value="0" />
            <asp:HiddenField ID="FirstRowLoadedHF" runat="server" Value="-1" />
            <asp:HiddenField ID="LastRowLoadedHF" runat="server" Value="-1" />

        </div>
    </div>
</asp:Content>

