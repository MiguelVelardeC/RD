<%@ Page Title="Presentaciones de medicamentos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PresentacionList.aspx.cs" Inherits="Clasificadores_Presentacion_PresentacionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Presentaciones de medicamentos</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Crear Presentación de medicamento"
                        NavigateUrl="~/Clasificadores/Presentacion/PresentacionDetails.aspx">
                    </asp:HyperLink>
                </div>

                <div class="gridContainer">
                    <asp:GridView ID="PresentacionGV" runat="server"
                        DataSourceID="TipoMedicamentoDataSource"
                        OnRowCommand="PresentacionGV_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <asp:Label Text="No existen Presentaciones" runat="server" />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" 
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord" 
                                        CommandArgument='<%# Bind("TipoMedicamentoId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" 
                                        CssClass="deleteRow"
                                        CommandName="DeleteRecord" 
                                        CommandArgument='<%# Bind("TipoMedicamentoId") %>'>
                                        <asp:Image ID="Image1" runat="server" 
                                            ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TipoMedicamentoId" HeaderText="PresentacionId" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre de la presentación" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="TipoMedicamentoDataSource" runat="server"
                        TypeName="Artexacta.App.TipoMedicamento.BLL.TipoMedicamentoBLL"
                        SelectMethod="GetTipoMedicamentoList"
                        OnSelected="TipoMedicamentoDataSource_Selected">
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenButton" runat="server" />
    <div id="dialog-confirm" title="Eliminar" style="display:none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            Esta seguro que desea eliminar la presentación seleccionada?
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

