<%@ Page Title="Definir permisos a Usuarios" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="DefinepermissionsByUser.aspx.cs" Inherits="Security_DefinepermissionsByUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" Text="Definir permisos a Usuarios" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                </div>

                <asp:Label ID="NameLabel" runat="server" Text="Nombre de Usuario:" CssClass="label"></asp:Label>
                <asp:TextBox ID="UserNameTextBox" runat="server" Width="200px" Height="16px"></asp:TextBox>
                <div class="validation">
                    <asp:RegularExpressionValidator ID="UserNameFormatRegularExpressionValidator" runat="server"
                        ControlToValidate="UserNameTextBox" Display="Dynamic" ErrorMessage="Caracteres inválidos en el nombre del usuario."
                        ValidationExpression="<%$ Resources : Validations , ShortDescriptionFormat %>">*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="UserNameLengthRegularExpressionValidator" runat="server"
                        ControlToValidate="UserNameTextBox" Display="Dynamic" ErrorMessage="El nombre de usuario no puede exceder los 50 caracteres."
                        ValidationExpression="<%$ Resources : Validations , GenericLength50 %>">*</asp:RegularExpressionValidator>
                </div>
                <asp:Label ID="Label1" runat="server" Text="Nombre Completo:" CssClass="label"></asp:Label>
                <asp:TextBox ID="FullnameTextBox" runat="server" Width="200px" Height="16px"></asp:TextBox>
                <div class="validation">
                    <asp:RegularExpressionValidator ID="FullNameFormatRegularExpressionValidator" runat="server"
                        ControlToValidate="FullnameTextBox" Display="Dynamic" ValidationExpression="<%$ Resources : Validations , ShortDescriptionFormat %>"
                        ErrorMessage="Caracteres inválidos en el nombre completo del usuario">*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="FullNameLengthRegularExpressionValidator" runat="server"
                        ControlToValidate="FullnameTextBox" Display="Dynamic" ValidationExpression="<%$ Resources : Validations , GenericLength250 %>"
                        ErrorMessage="El Nombre Completo no puede exceder de 250 caracteres.">*</asp:RegularExpressionValidator>
                </div>
                <div class="validation">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Corrija los siguientes errores para continuar:"
                        Width="100%" />
                </div>
                <div class="buttonsPanel">
                    <asp:LinkButton ID="SubmitButton" runat="server" CssClass="button" OnClick="SubmitButton_Click">
									<span>Buscar Usuarios</span>
                    </asp:LinkButton>
                </div>

                <div style="clear: both;"></div>

                <div class="gridContainer">
                    <asp:GridView ID="EmployeeGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="UserId,FullName" DataSourceID="UserObjectDataSource" OnDataBound="EmployeeGridView_DataBound"
                        OnRowDataBound="EmployeeGridView_RowDataBound" OnSelectedIndexChanged="EmployeeGridView_SelectedIndexChanged"
                        PageSize="5" Visible="False" CssClass="rgMasterTable" GridLines="None">
                        <HeaderStyle CssClass="rgHeader head" />
                        <FooterStyle CssClass="foot" />
                        <AlternatingRowStyle CssClass="altRow" />
                        <EmptyDataRowStyle CssClass="gridNoData" />
                        <RowStyle CssClass="row" />
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="SelectImageButton" runat="server" CommandName="Select" ImageUrl="~/Images/neutral/select.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Username" HeaderText="Nombre de Usuario" SortExpression="Username"
                                ItemStyle-Width="140px" />
                            <asp:BoundField DataField="FullName" HeaderText="Nombre Completo" SortExpression="FullName"
                                ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="EmptyLabel" runat="server" Text="No se encontró el usuario indicado."></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div style="clear: both;">
                    &nbsp;</div>
                <asp:Panel ID="PermissionPanel" runat="server" Visible="False" DefaultButton="SavePermissionsButton"
                    CssClass="frame" Width="500px">
                    <asp:Label ID="SelectLabel" runat="server" Text="Debe seleccionar un Usuario." Visible="False"></asp:Label>
                    <asp:Label ID="ChangingLabel" runat="server" Text="Cambiar permisos para el Usuario:"
                        Visible="False"></asp:Label>
                    <asp:Label ID="UserLabel" runat="server" Font-Bold="True"></asp:Label><br />
                    <div style="clear: both;">
                    </div>
                    <div class="gridContainer">
                        <asp:GridView ID="EmployeePermissionsGridView" runat="server" AutoGenerateColumns="False"
                            DataSourceID="UserPermissionsObjectDataSource" DataKeyNames="UserId,PermissionId"
                            CssClass="rgMasterTable" GridLines="None">
                            <HeaderStyle CssClass="rgHeader head" />
                            <FooterStyle CssClass="foot" />
                            <AlternatingRowStyle CssClass="altRow" />
                            <EmptyDataRowStyle CssClass="gridNoData" />
                            <RowStyle CssClass="row" />
                            <Columns>
                                <asp:TemplateField HeaderText="PermissionId" SortExpression="PermissionId" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="PermissionIDLabel" runat="server" Text='<%# Bind("PermissionId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UserId" SortExpression="UserId" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="UserIDLabel" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="UserHasPermission" ItemStyle-Width="120px">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("UserHasPermission") %>' />
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="SelectAllLinkButton" runat="server" OnClick="SelectAllLinkButton_Click"
                                            CssClass="linkButton">
										    <span>Seleccionar Todo</span>	
                                        </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("UserHasPermission") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Description" HeaderText="Descripción" SortExpression="Description"
                                    ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SavePermissionsButton" runat="server" OnClick="SavePermissionsButton_Click"
                            CssClass="button">
							<span>Grabar permisos</span>	
                        </asp:LinkButton>
                        <asp:LinkButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CssClass="secondaryButton">
							<span>Resetear permisos</span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <div class="clear"></div>
            </div>
        </div>
    </div>

    <asp:ObjectDataSource ID="UserObjectDataSource" runat="server" OnSelected="UserObjectDataSource_Selected"
        SelectMethod="GetUsersBySearchParameters" TypeName="Artexacta.App.User.BLL.UserBLL"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="UserNameHiddenField" Name="Username" PropertyName="Value"
                Type="String" />
            <asp:ControlParameter ControlID="FullnameHiddenField" Name="Fullname" PropertyName="Value"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="FullnameHiddenField" runat="server" />
    <asp:HiddenField ID="UserNameHiddenField" runat="server" />
    <asp:HiddenField ID="SelectedUserIdHiddenField" runat="server" />
    <asp:ObjectDataSource ID="UserPermissionsObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        OnSelected="UserPermissionsObjectDataSource_Selected" SelectMethod="GetPermissionsForUser"
        TypeName="Artexacta.App.Permissions.User.BLL.PermissionUserBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="SelectedUserIdHiddenField" Name="UserId" PropertyName="Value"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

