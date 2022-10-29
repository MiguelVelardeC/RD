<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorUserList.aspx.cs" Inherits="Proveedor_ProveedorUserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Usuarios por Proveedor Hospital" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:HyperLink NavigateUrl="~/Proveedor/ProveedorUser.aspx" runat="server" 
                    Text="Agregar Usuario a un Proveedor Hospital"/>
            </div>
            <div>
                <asp:Label ID="NewPLabel" Text="Proveedor Hospital" runat="server" CssClass="label" />
                <telerik:RadComboBox ID="ProveedorHospitalDDL" runat="server"
                    CssClass="biggerField"
                    DataSourceID="ProveedorHospitalODS"
                    DataValueField="ProveedorId"
                    DataTextField="NombreJuridico"
                    EmptyMessage="Seleccione un Proveedor"
                    MarkFirstMatch="true">
                </telerik:RadComboBox>

                <div class="validation">
                    <asp:CustomValidator ID="ProveedorCV" runat="server"
                        ValidationGroup="Search"
                        ErrorMessage="Debe seleccionar un proveedor."
                        ClientValidationFunction="ProveedorCV_Validate"
                        Display="Dynamic" />
                </div>
                <asp:ObjectDataSource ID="ProveedorHospitalODS" runat="server"
                    TypeName="Artexacta.App.Proveedor.BLL.ProveedorBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="getProveedorByTipoProveedorAndCiudadId"
                    OnSelected="ProveedorHospitalODS_Selected">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="HOSPITAL" Name="TipoProveedorId" Type="String" />
                        <asp:ControlParameter ControlID="CiudadHF" Name="CiudadId" PropertyName="Value" DbType="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SearchLB" Text="" runat="server"
                        CssClass="button"
                        ValidationGroup="Search"
                        OnClick="SearchLB_Click">
                        <asp:Label ID="Label2" Text="Buscar" runat="server" />
                    </asp:LinkButton>
                </div>

                <telerik:RadGrid ID="ProveedorUserRadGrid" runat="server"
                    AutoGenerateColumns="false"
                    DataSourceID="ProveedorUserODS"
                    AllowPaging="true"
                    PageSize="20"
                    OnItemCommand="ProveedorUserRadGrid_ItemCommand"
                    Visible="false">
                    <MasterTableView DataKeyNames="ProveedorId,Userid">
                        <NoRecordsTemplate>
                            <asp:Label runat="server" Text="No existen Usuarios para el proveedor seleccionado"></asp:Label>
                        </NoRecordsTemplate>
                        <Columns>
                            <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                HeaderText="Eliminar"
                                CommandName="Delete"
                                ButtonType="ImageButton"
                                ItemStyle-Width="40px"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px"
                                HeaderStyle-HorizontalAlign="Center"
                                ImageUrl="~/Images/neutral/delete.png"
                                ConfirmText="¿Está seguro que desea El Usuario del Proveedor Hospital?" />

                            <telerik:GridBoundColumn DataField="NombreJuridico" HeaderText="Proveedor Hospital" />
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="Usuario" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ProveedorUserODS" runat="server"
                    TypeName="Artexacta.App.ProveedorUser.BLL.ProveedorUserBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="ProveedorUserList"
                    OnSelected="ProveedorUserODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ProveedorHospitalDDL" Name="ProveedorId" PropertyName="SelectedValue" Type="Int32"/>
                    </SelectParameters>
                </asp:ObjectDataSource>

                <script type="text/javascript">
                    function ProveedorCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= ProveedorHospitalDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                </script>

                <asp:HiddenField ID="CiudadHF" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

