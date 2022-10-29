<%@ Page Title="Definir permisos a Roles" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="DefinePermissionsByRol.aspx.cs" Inherits="Security_DefinePermissionsByRol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TituloLabel" runat="server" Text="Definir permisos a Roles" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                </div>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="SelectLabel" runat="server" Text="Seleccionar Rol:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RoleDropDownList" runat="server" AutoPostBack="True" DataSourceID="RoleObjectDataSource"
                                OnDataBound="RoleDropDownList_DataBound" OnSelectedIndexChanged="RoleDropDownList_SelectedIndexChanged"
                                Width="150px">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:LinkButton ID="AddNewRoleLinkButton" runat="server" CausesValidation="False"
                                OnClick="AddNewRoleLinkButton_Click" CssClass="button">
								<span>Adicionar Nuevo Rol</span>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="ChangeLabel" runat="server" Text="Cambiar permisos para el Rol:"></asp:Label>
                <asp:Label ID="RoleLabel" runat="server" ForeColor="#333333" Font-Bold="True"></asp:Label><br />
                <br />

                <div class="gridContainer">
                    <div class="gridContainer">
                        <asp:GridView ID="RolePermissionGridView" runat="server" AutoGenerateColumns="False"
                            DataSourceID="RolePermissionObjectDataSource" CssClass="rgMasterTable" GridLines="None">
                            <HeaderStyle CssClass="rgHeader head" />
                            <FooterStyle CssClass="foot" />
                            <AlternatingRowStyle CssClass="altRow" />
                            <EmptyDataRowStyle CssClass="gridNoData" />
                            <RowStyle CssClass="row" />
                            <Columns>
                                <asp:TemplateField HeaderText="Rol" SortExpression="Role">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Role") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="RoleHasPermission">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("RoleHasPermission") %>' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="SelectAllLinkButton" runat="server" OnClick="SelectAllLinkButton_Click"
                                            Text="Seleccionar Todo" CssClass="linkButton"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PermissionID" SortExpression="PermissionID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("PermissionID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Description" HeaderText="Descripción del permiso" SortExpression="Description"
                                    ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="SavePermissionsButton" runat="server" OnClick="SavePermissionsButton_Click"
                        CssClass="button">
					<span>Grabar permisos</span>	
                    </asp:LinkButton>
                    <asp:LinkButton ID="ResetPermissionsButton" runat="server" OnClick="ResetPermissionsButton_Click"
                        CssClass="button">
					<span>Resetear permisos</span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>

            </div>
        </div>
    </div>

    <asp:ObjectDataSource ID="RoleObjectDataSource" runat="server" OnSelected="RoleObjectDataSource_Selected"
        SelectMethod="GetAllDefinedRoles" TypeName="Artexacta.App.Security.BLL.SecurityBLL"
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="RolePermissionObjectDataSource" runat="server" OnSelected="PermissionObjectDataSource_Selected"
        SelectMethod="GetPermissionsForRole" TypeName="Artexacta.App.Permissions.Role.BLL.PermissionRoleBLL"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="RoleDropDownList" Name="Role" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

