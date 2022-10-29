<%@ Page Title="Usuario Servicio" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="UserService.aspx.cs" Inherits="Security_UserService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="TitleLabel" runat="server" Text="Usuario Servicio" CssClass="title"></asp:Label>
            </div>
            <div class="columnContent">
                <div class="left" style="width: 30%;">
                    <asp:Label runat="server" Text="Username:" CssClass="label"></asp:Label>
				    <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="Password:" CssClass="label"></asp:Label>
				    <asp:TextBox ID="txtPassword" runat="server" Width="200px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="Habilitado:" CssClass="label"></asp:Label>
				    <asp:CheckBox ID="chkHabilitado" runat="server" />
                    <div class="validation">      
					
				    </div>
				<div class="buttonsPanel">
					<asp:LinkButton ID="InsertButton" runat="server" OnClick="InsertButton_Click"
						CssClass="button" >
						<span>Guardar</span>	
					</asp:LinkButton>
					<asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click"
						CssClass="secondaryButton" >
						<span>Cancelar</span>
					</asp:LinkButton>
				</div>
                </div>
                <div class="left" style="width: 70%;">
                        <div class="gridContainer" runat="server" id="divGrid">
                    <asp:Panel ID="ClientesAsociados" runat="server" 
                        GroupingText="Clientes Asociados">
                    <asp:DropDownList runat="server" ID="lstCodigoClientes" Width="350px"></asp:DropDownList>
                    <asp:LinkButton ID="btnInsertar" runat="server" OnClick="btnInsertar_Click"
						CssClass="button" >
						<span>Agregar Cliente</span>	
					</asp:LinkButton>
                    <asp:GridView ID="ClientesGridView" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="UsuarioServicioCliente"
                        CssClass="rgMasterTable" GridLines="None">
                        <HeaderStyle CssClass="rgHeader head" />
                        <FooterStyle CssClass="foot" />
                        <AlternatingRowStyle CssClass="altRow" />
                        <EmptyDataRowStyle CssClass="gridNoData" />
                        <RowStyle CssClass="row" />
                        <Columns>
                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkEdit" runat="server" ImageUrl="~/Images/neutral/delete.png" 
                                        NavigateUrl='<%# "~/Security/UserService.aspx?UsuarioServicioID=" + Eval("UsuarioServicioID") + "&UsuarioServicioCliente=" + Eval("UsuarioServicioCliente")%>' 
                                        onclick="return confirm('Seguro que desa eliminar?');"
                                        ></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CodigoCliente" HeaderText="Codigo" SortExpression="CodigoCliente"
                                ItemStyle-Width="200px">
                                <ItemStyle Width="200px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NombreJuridico" HeaderText="Nombre" SortExpression="Nombre"
                                ItemStyle-Width="300px">
                                <ItemStyle Width="300px"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="EmptyLiteral" runat="server" Text="No se encuentran datos"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                     </asp:Panel>
                </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="PostBackPageHiddenField" runat="server" />
</asp:Content>

