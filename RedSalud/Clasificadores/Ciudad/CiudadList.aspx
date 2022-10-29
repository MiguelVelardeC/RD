<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CiudadList.aspx.cs" Inherits="Clasificadores_Ciudad_CiudadList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
<div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Ciudades</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Crear Ciudad"
                        NavigateUrl="~/Clasificadores/Ciudad/CiudadDetails.aspx">
                    </asp:HyperLink>
                </div>

                <div class="gridContainer">
                    <asp:GridView ID="CiudadGV" runat="server"
                        DataSourceID="CiudadODS"
                        OnRowCommand="CiudadGV_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" Text="No existen Ciudades" runat="server" />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px" >
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" 
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord" 
                                        CommandArgument='<%# Bind("CiudadId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" 
                                        CssClass="deleteRow"
                                        CommandName="DeleteRecord" 
                                        CommandArgument='<%# Bind("CiudadId") %>'>
                                        <asp:Image ID="Image1" runat="server" 
                                            ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CiudadId" HeaderText="CiudadId" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre de la Ciudad" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="CiudadODS" runat="server"
                        TypeName="Artexacta.App.Ciudad.BLL.CiudadBLL"
                        SelectMethod="getCiudadList"
                        OnSelected="CiudadODS_Selected">
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenButton" runat="server" />
    <div id="dialog-confirm" title="Eliminar" style="display:none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            Esta seguro que desea eliminar la Ciudad seleccionada?
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

