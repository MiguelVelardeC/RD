<%@ Page Title="Asignar Roles por Usuario" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="AssignRolesByUser.aspx.cs" Inherits="Security_AssignRolesByUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel1" runat="server" Text="Asignar Roles por Usuario" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                </div>

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text="Nombre de Usuario: " CssClass="normalText"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server" Width="150px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="NameFormatRegularExpressionValidator" runat="server"
                                ControlToValidate="UserNameTextBox" Display="Dynamic" ErrorMessage="Caracteres inválidos en el nombre del usuario."
                                ValidationExpression="^[A-Za-z\-_\s\.]+$">*</asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="NameLengthRegularExpressionValidator" runat="server"
                                ControlToValidate="UserNameTextBox" Display="Dynamic" ErrorMessage="El nombre de usuario no puede exceder los 50 caracteres."
                                ValidationExpression="[\w\W]{0,50}">*</asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:LinkButton ID="SearchButton" runat="server" CssClass="button" OnClick="SearchButton_Click">
                            <span>Buscar Usuarios</span></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Corrija los siguientes errores para continuar:"
                                Width="100%" />
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="UserPanel" runat="server" CssClass="gridContainer">
                    <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" DataSourceID="UsersObjectDataSource"
                        DataKeyNames="UserName" OnSelectedIndexChanged="UserGridView_SelectedIndexChanged"
                        CssClass="rgMasterTable" GridLines="None">
                        <HeaderStyle CssClass="rgHeader head" />
                        <FooterStyle CssClass="foot" />
                        <AlternatingRowStyle CssClass="altRow" />
                        <EmptyDataRowStyle CssClass="gridNoData" />
                        <RowStyle CssClass="row" />
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="SelectImageButton" runat="server" CommandName="Select" ImageUrl="~/Images/neutral/select.png"
                                        CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre de Usuario" SortExpression="UserName">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>' Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" SortExpression="Email"
                                ItemStyle-Width="200px" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>

                <div class="clear">
                </div>
            </div>

            <div style="clear: both; padding-top: 10px;">
                <asp:Panel ID="EmployeeRolePanel" runat="server" Width="300px" CssClass="frame" Visible="false">
                    <asp:Label ID="TitleLabel" runat="server" Text="Información del Usuario Seleccionado"
                        Font-Size="Small" Font-Bold="true"></asp:Label><br />
                    <br />
                    <asp:Label ID="User1Label" runat="server" Text="Nombre de Usuario"></asp:Label>
                    <asp:Label ID="UserLabel" runat="server" Font-Bold="True"></asp:Label><br />
                    <asp:Label ID="UserEmail1Label" runat="server" Text="Correo Electrónico"></asp:Label>
                    <asp:Label ID="UserEmailLabel" runat="server" Font-Bold="True"></asp:Label><br />
                    <br />
                    <asp:Label ID="RolesLabel" runat="server" Text="Roles para el Usuario"></asp:Label><br />
                    <br />
                    <asp:CheckBoxList ID="UserRoleCheckBoxList" runat="server">
                    </asp:CheckBoxList>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="SaveRolesButton" runat="server" CssClass="button" OnClick="SaveRolesButton_Click">
									<span>Grabar los Roles del Usuario.</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="1" cellspacing="1">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ResetRolesButton" runat="server" CssClass="button" OnClick="ResetRolesButton_Click">
									<span>Resetar los Roles del usuario</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>

        </div>
    </div>
    <asp:ObjectDataSource ID="UsersObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="LookUpUsers" TypeName="Artexacta.App.Security.BLL.SecurityBLL"
        OnSelected="UsersObjectDataSource_Selected">
        <SelectParameters>
            <asp:ControlParameter ControlID="UserNameHiddenField" Name="UserName" PropertyName="Value"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="UserNameHiddenField" runat="server" />
</asp:Content>

