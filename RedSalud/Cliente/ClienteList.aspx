<%@ Page Title="Cliente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ClienteList.aspx.cs" Inherits="Cliente_ClienteList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Clientes</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Crear Cliente"
                        NavigateUrl="~/Cliente/ClienteDetails.aspx">
                    </asp:HyperLink>
                </div>

                <div class="gridContainer">
                    <asp:GridView ID="ClienteGV" runat="server"
                        DataSourceID="ClienteODS"
                        OnRowCommand="ClienteGV_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" Text="No existen Clientes" runat="server" />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="15px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server"
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord"
                                        CommandArgument='<%# Bind("ClienteId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="15px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server"
                                        CssClass="deleteRow"
                                        CommandName="DeleteRecord"
                                        CommandArgument='<%# Bind("ClienteId") %>'>
                                        <asp:Image ID="Image1" runat="server"
                                            ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CodigoCliente" HeaderText="Código Cliente" />
                            <asp:BoundField DataField="NombreJuridicoForDisplay" HeaderText="Nombre Juridico (ID)" />
                            <asp:BoundField DataField="Nit" HeaderText="Nit" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="Telefono1" HeaderText="Telefono 1" />
                            <asp:BoundField DataField="Telefono2" HeaderText="Telefono 2" />
                            <asp:BoundField DataField="NombreContacto" HeaderText="Nombre Contacto" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="CostoConsultaInternista" HeaderText="Costo Consulta Internista" />

                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                        SelectMethod="getRedClienteList"
                        OnSelected="ClienteODS_Selected">
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenButton" runat="server" />
    <div id="dialog-confirm" title="Eliminar" style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            ¿Está seguro que desea eliminar el Cliente seleccionado?
        </p>
    </div>

    <script type="text/javascript">
        $(function () {
            $(".deleteRow").click(function () {
                $("#<%= HiddenButton.ClientID %>").val($(this).attr("href"));
                $("#dialog-confirm").dialog("open");
                return false;
            });

            $("#dialog-confirm").dialog({
                resizable: false,
                height: 140,
                modal: true,
                autoOpen: false,
                buttons: {
                    "Eliminar": function () {
                        eval($("#<%= HiddenButton.ClientID %>").val());
                        $(this).dialog("close");
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>
</asp:Content>

