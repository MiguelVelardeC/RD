<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Consolidados.aspx.cs" Inherits="Consolidacion_Consolidados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
        <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" CssClass="title" Text="Consolidación de gastos" runat="server" />
            </div>
            <div>
                <asp:Panel ID="Search" runat="server" DefaultButton="SearchLB">
                    <span class="label">Cliente</span>
                    <asp:DropDownList ID="ProveedorDDL" runat="server"
                        DataSourceID="ProveedorODS"
                        CssClass="bigField"
                        DataValueField="ProveedorId"
                        DataTextField="NombreCompletoOrJuridico">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ProveedorODS" runat="server"
                        TypeName="Artexacta.App.Proveedor.BLL.ProveedorBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getProveedorBasicList"
                        OnSelected="ProveedorODS_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CiudadHF" Name="CiudadId" PropertyName="Value" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="CiudadHF" runat="server" />
    
                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SearchLB" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="SearchCaso"
                            OnClick="SearchLB_Click">
                        <span>Buscar</span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <telerik:RadGrid ID="ConsolidadosRadGrid" runat="server"
                    AutoGenerateColumns="false"
                    DataSourceID="ConsolidadosODS"
                    AllowPaging="true"
                    PageSize="20"
                    Visible="false"
                    OnItemCommand="ConsolidadosRadGrid_ItemCommand">
                    <MasterTableView>
                        <NoRecordsTemplate>
                            <asp:Label ID="EmptyL" runat="server" Text="No existen Consolidaciones para el proveedor seleccionado."></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton Text="Ver Reporte" runat="server" 
                                        CommandName="VerReporte" 
                                        CommandArgument='<%# Eval("ConsolidacionId") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="FechaHasta" HeaderText="Gastos revisados hasta la siguiente fecha" />
                            <telerik:GridBoundColumn DataField="MontoTotal" HeaderText="Monto Total" />
                            <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha de la consolidación" />
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="Usuario Administrador" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ConsolidadosODS" runat="server"
                    TypeName="Artexacta.App.Consolidacion.BLL.ConsolidacionBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetListConsolidacion"
                    OnSelected="ConsolidadosODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ProveedorDDL" Name="ProveedorId" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
</asp:Content>

