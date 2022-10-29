<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorUser.aspx.cs" Inherits="Proveedor_ProveedorUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Agregar Usuario a un Proveedor Hospital" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Proveedor/ProveedorUserList.aspx" runat="server"
                    Text="Volver a la lista de Usuarios por Proveedor Hospital" />
            </div>
            <div>
                <asp:Label ID="Label3" Text="Usuario" runat="server" CssClass="label" />
                <telerik:RadComboBox ID="UserDDL" runat="server"
                    CssClass="biggerField"
                    DataSourceID="UserODS"
                    DataValueField="UserId"
                    DataTextField="fullName"
                    EmptyMessage="Seleccione un Usuario"
                    AutoPostBack="false"
                    MarkFirstMatch="true">
                </telerik:RadComboBox>
                <div class="validation">
                    <asp:CustomValidator ID="UserCV" runat="server"
                        ValidationGroup="New"
                        ErrorMessage="Debe seleccionar un Usuario."
                        ClientValidationFunction="UserCV_Validate"
                        Display="Dynamic" />
                </div>
                <asp:ObjectDataSource ID="UserODS" runat="server"
                    TypeName="Artexacta.App.User.BLL.UserBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetUsersListForSearch"
                    OnSelected="UserODS_Selected">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="" Name="whereSql" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>

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
                        ValidationGroup="New"
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
                    <asp:LinkButton ID="SaveLB" Text="" runat="server"
                        CssClass="button"
                        ValidationGroup="New"
                        OnClick="SaveLB_Click">
                        <asp:Label ID="Label2" Text="Agregar Usuario al Proveedor" runat="server" />
                    </asp:LinkButton>
                </div>

                <script type="text/javascript">
                    function ProveedorCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= ProveedorHospitalDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                    function UserCV_Validate(sender, args) {
                        args.IsValid = true;

                        var value = $find('<%= UserDDL.ClientID %>').get_value();

                        if (value == "") {
                            args.IsValid = false;
                        }
                    }
                </script>
                <asp:HiddenField ID="CiudadHF" runat="server" Value="TTT" />
            </div>
        </div>
    </div>
</asp:Content>

