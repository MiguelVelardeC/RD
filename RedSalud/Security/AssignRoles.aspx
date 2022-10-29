<%@ Page Title="Asignar Roles" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="AssignRoles.aspx.cs" Inherits="Security_AssignRoles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Asignar Roles</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                </div>

                <asp:Label ID="RoleLabel" runat="server" Text="Rol: "></asp:Label>
                <telerik:RadComboBox ID="RoleDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RoleDropDownList_SelectedIndexChanged"
                    Width="150px">
                </telerik:RadComboBox>
                <asp:ImageButton ID="DeleteRolImageButton" runat="server" ImageUrl="~/Images/neutral/delete-role.png"
                    OnClick="DeleteRolImageButton_Click" ToolTip="Eliminar Rol" Width="21px" />
                <asp:ConfirmButtonExtender ID="DeleteRolImageButton_ConfirmButtonExtender" runat="server"
                    ConfirmText="¿Esta seguro de eliminar el rol?" Enabled="True" TargetControlID="DeleteRolImageButton">
                </asp:ConfirmButtonExtender>

                <table>
                    <tr>
                        <td nowrap="nowrap" align="center">
                            <asp:Label ID="UsersInLabel" runat="server" Text="Usuarios en el Rol"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td nowrap="nowrap" align="center">
                            <asp:Label ID="UsersNotInLabel" runat="server" Text="Usuarios no en el Rol"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="InRoleListBox" runat="server" Height="300px" Width="200px" SelectionMode="Multiple"
                                AutoPostBack="True" OnSelectedIndexChanged="InRoleListBox_SelectedIndexChanged">
                            </asp:ListBox>
                        </td>
                        <td valign="bottom">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="AddInImageButton" runat="server" ImageUrl="~/Images/neutral/to-left-arrow.png"
                                OnClick="AddInImageButton_Click" ToolTip="Mover Usuario a Rol" /><br />
                            <br />
                            <br />
                            <br />
                            <asp:ImageButton ID="AddOutImageButton" runat="server" ImageUrl="~/Images/neutral/to-right-arrow.png"
                                OnClick="AddOutImageButton_Click" ToolTip="Mover cuenta fuera del Rol" />
                        </td>
                        <td>
                            <asp:ListBox ID="OutRoleListBox" runat="server" Height="300px" Width="200px" SelectionMode="Multiple"
                                AutoPostBack="True" OnSelectedIndexChanged="OutRoleListBox_SelectedIndexChanged">
                            </asp:ListBox>
                        </td>
                        <td valign="bottom">
                            &nbsp;
                        </td>
                        <td style="width: 100px" valign="top">
                            <asp:Panel ID="EmployeeRolePanel" runat="server" Width="300px" Visible="False" CssClass="panelFormat">
                                <asp:Label ID="TitleLabel" runat="server" Text="Información del Usuario Seleccionado"
                                    Font-Size="Small" Font-Bold="true"></asp:Label><br />
                                <br />
                                <asp:Label ID="User1Label" runat="server" Text="Nombre de Usuario:"></asp:Label>
                                <asp:Label ID="UserLabel" runat="server"></asp:Label><br />
                                <asp:Label ID="UserEmail1Label" runat="server" Text="Correo Electrónico:"></asp:Label>
                                <asp:Label ID="UserEmailLabel" runat="server"></asp:Label><br />
                                <br />
                                <asp:Label ID="RolesLabel" runat="server" Text="Roles para el Usuario"></asp:Label><br />
                                <br />
                                <asp:CheckBoxList ID="UserRoleCheckBoxList" runat="server">
                                </asp:CheckBoxList>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="SaveRolesButton" runat="server" OnClick="SaveRolesButton_Click"
                                                CssClass="button">
												<span>Grabar los Roles del Usuario</span>	
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="ResetRolesButton" runat="server" OnClick="ResetRolesButton_Click"
                                                CssClass="button">
												<span>Resetar los Roles del usuario</span>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="AddNewRoleLinkButton" runat="server" OnClick="AddNewRoleLinkButton_Click"
                    CssClass="button">
					<span>Adicionar Nuevo Rol</span>	
                </asp:LinkButton>
                <asp:LinkButton ID="AddNewUserLinkButton" runat="server" OnClick="AddNewUserLinkButton_Click"
                    CssClass="button">
					<span>Adicionar Nuevo Usuario</span>	
                </asp:LinkButton>
                <div class="clear"></div>
            </div>
        </div>
    </div>
</asp:Content>

