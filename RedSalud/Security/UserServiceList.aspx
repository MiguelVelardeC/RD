<%@ Page Title="Lista de Usuarios" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="UserServiceList.aspx.cs" Inherits="Security_UserServiceList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                        <div class="gridContainer">
                        <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="UsuarioServicioID" OnSelectedIndexChanged="UserGridView_SelectedIndexChanged"
                            CssClass="rgMasterTable" GridLines="None">
                            <HeaderStyle CssClass="rgHeader head" />
                            <FooterStyle CssClass="foot" />
                            <AlternatingRowStyle CssClass="altRow" />
                            <EmptyDataRowStyle CssClass="gridNoData" />
                            <RowStyle CssClass="row" />
                            <Columns>
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="26px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkEdit" runat="server" ImageUrl="~/Images/neutral/select.png"
                                            NavigateUrl='<%# "~/Security/UserService.aspx?UsuarioServicioID=" + Eval("UsuarioServicioID")%>' ></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Usuario" HeaderText="User" SortExpression="FullName"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Contrasena" HeaderText="Password" SortExpression="Email"
                                    ItemStyle-Width="120px">
                                    <ItemStyle Width="120px"></ItemStyle>
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="Habilitado" HeaderText="Habilitado?" ReadOnly="True"
                                    SortExpression="Habilitado" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
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

</asp:Content>

