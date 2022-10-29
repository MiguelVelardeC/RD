<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgregarConsolidacion.aspx.cs" Inherits="Consolidacion_AgregarConsolidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
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
                    <span class="label">Fecha máxima</span>
                    <telerik:RadDatePicker ID="FechaFin" runat="server"
                        ShowPopupOnFocus="true" />
                    <div class="validators">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="FechaFin"
                            ErrorMessage="debe seleccionar una fecha máxima."
                            ValidationGroup="SearchCaso"
                            Display="Dynamic" />
                    </div>

                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SearchLB" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="SearchCaso"
                            OnClick="SearchLB_Click">
                        <span>Buscar</span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <telerik:RadGrid ID="GastoD" runat="server"
                    AutoGenerateColumns="false"
                    DataSourceID="GastoDetalleODS"
                    AllowPaging="true"
                    PageSize="20"
                    Visible="false">
                    <MasterTableView>
                        <NoRecordsTemplate>
                            <asp:Label ID="EmptyL" runat="server" Text="No existen Gastos por Consolidar con los parametros de busqueda definidos."></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Aceptar">
                                <ItemTemplate>
                                    <asp:RadioButton ID="AceptadoRb" Text="Aceptar" runat="server" GroupName="Consolidacion" />
                                    <asp:HiddenField ID="GastoDetalleIdHF" runat="server" Value='<%# Bind("GastoDetalleId") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Rechazar">
                                <ItemTemplate>
                                    <asp:RadioButton ID="RechazadoRb" Text="Rechazar" runat="server" GroupName="Consolidacion" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha de Registro" />
                            <telerik:GridBoundColumn DataField="GastoDetalleID" HeaderText="GastoDetalleID" Visible="false" />
                            <telerik:GridBoundColumn DataField="Monto" HeaderText="Monto" />
                            <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento" />
                            <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. Factura/Recibo" />
                            <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la Factura/Recibo" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="GastoDetalleODS" runat="server"
                    TypeName="Artexacta.App.GastoDetalle.BLL.GastoDetalleBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetAllGastoDetalleByProveedorId"
                    OnSelected="GastoDetalleODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ProveedorDDL" Name="ProveedorId" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveConsolidacionLB" Text="" runat="server" CssClass="button" OnClick="SaveConsolidacionLB_Click">
                        <span>Consolidar las filas seleccionadas</span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

