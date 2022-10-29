<%@ Page Title="Lista de Usuarios" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="UserList.aspx.cs" Inherits="Security_UserList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/SearchUserControl/SearchControl.ascx" TagName="SearchControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Usuarios</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:ImageButton ID="NewImageButton" runat="server" ImageUrl="~/Images/neutral/new.png"
                        OnClick="NewButton_Click" />
                    <asp:LinkButton ID="NewButton" runat="server" Text="Nuevo Usuario" CssClass="linkButton"
                        OnClick="NewButton_Click">
                    </asp:LinkButton>
                </div>

                <uc1:SearchControl ID="UserSearchControl" runat="server" Title="Buscar: " DisplayHelp="true"
                    DisplayContextualHelp="true" CssSearch="CSearch" CssSearchError="CSearchErrorPanel"
                    CssSearchHelp="CSearchHelpPanel" ImageErrorUrl="~/Images/neutral/exclamation.png"
                    ImageHelpUrl="~/Images/neutral/Help.png" />
                <div style="clear: both; margin-bottom: 5px;">
                </div>

                <div class="gridContainer">
                    <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" DataSourceID="UserObjectDataSource"
                        DataKeyNames="UserName" OnSelectedIndexChanged="UserGridView_SelectedIndexChanged"
                        CssClass="rgMasterTable" GridLines="None">
                        <HeaderStyle CssClass="rgHeader head" />
                        <FooterStyle CssClass="foot" />
                        <AlternatingRowStyle CssClass="altRow" />
                        <EmptyDataRowStyle CssClass="gridNoData" />
                        <RowStyle CssClass="row" />
                        <Columns>
                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditImageButton" runat="server" CommandName="Select" ImageUrl="~/Images/neutral/select.png"
                                        OnClick="EditImageButton_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DeleteImageButton" runat="server" CommandName="Select" ImageUrl="~/Images/neutral/delete.png"
                                        OnClick="DeleteImageButton_Click" />
                                    <asp:ConfirmButtonExtender ID="DeleteImageButton_ConfirmButtonExtender" runat="server"
                                        ConfirmText="¿Está seguro de eliminar el usuario?" Enabled="True" TargetControlID="DeleteImageButton">
                                    </asp:ConfirmButtonExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Desbloquear" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BlockImageButton" runat="server" CausesValidation="False" CommandName="Select"
                                        ImageUrl="~/Images/neutral/unlock.png" OnClick="BlockImageButton_Click" />
                                    <asp:ConfirmButtonExtender ID="BlockImageButton_ConfirmButtonExtender" runat="server"
                                        ConfirmText="¿Esta seguro de desbloquear al usuario?" Enabled="True" TargetControlID="BlockImageButton">
                                    </asp:ConfirmButtonExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Restablecer contraseña" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ResetImageButton" runat="server" CausesValidation="False" CommandName="Select"
                                        ImageUrl="~/Images/neutral/emptypass.png" OnClick="ResetImageButton_Click" />
                                    <asp:ConfirmButtonExtender ID="ResetImageButton_ConfirmButtonExtender" runat="server"
                                        ConfirmText="Se cambiará la contraseña del usuario y será enviada por correo electrónico. ¿Esta seguro de continuar?"
                                        Enabled="True" TargetControlID="ResetImageButton">
                                    </asp:ConfirmButtonExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" ReadOnly="True"
                                SortExpression="UserName" ItemStyle-Width="150px">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Nombre Completo" SortExpression="FullName"
                                ItemStyle-Width="200px">
                                <ItemStyle Width="200px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" SortExpression="Email"
                                ItemStyle-Width="120px">
                                <ItemStyle Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="IsOnline" HeaderText="Esta Conectado" ReadOnly="True"
                                SortExpression="IsOnline" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="IsAproved" HeaderText="Aprobado" SortExpression="IsAproved"
                                ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="IsBlocked" HeaderText="Bloqueado" ReadOnly="True" SortExpression="IsBlocked"
                                ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            </asp:CheckBoxField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="EmptyLiteral" runat="server" Text="No se encuentran usuarios registrados"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>

    <asp:ObjectDataSource ID="UserObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsersListForSearch" TypeName="Artexacta.App.User.BLL.UserBLL"
        OnSelected="UserObjectDataSource_Selected">
        <SelectParameters>
            <asp:ControlParameter ControlID="UserSearchControl" Name="whereSql" PropertyName="Sql"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="UsernameHiddenField" runat="server" />
    <asp:HiddenField ID="OperationHiddenField" runat="server" />

</asp:Content>

