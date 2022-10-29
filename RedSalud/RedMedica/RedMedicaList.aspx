<%@ Page Title="Red Medica" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RedMedicaList.aspx.cs" Inherits="RedMedica_RedMedicaList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
     <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Redes Medicas</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Crear Red Medica"
                        NavigateUrl="~/RedMedica/RedMedicaDetails.aspx">
                    </asp:HyperLink>
                </div>

                <div class="gridContainer">
                    <asp:GridView ID="RedMedicaGV" runat="server"
                        DataSourceID="RedMedicaODS"
                        OnRowCommand="RedMedicaGV_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" Text="No existen Redes Medicas" runat="server" />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" 
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord" 
                                        CommandArgument='<%# Bind("RedMedicaId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" 
                                        CssClass="deleteRow"
                                        CommandName="DeleteRecord" 
                                        CommandArgument='<%# Bind("RedMedicaId") %>'>
                                        <asp:Image ID="Image1" runat="server" 
                                            ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo" HeaderText="Código" />
                            <asp:BoundField DataField="NombreForDisplay" HeaderText="Nombre de la Red Medica (ID)" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="RedMedicaODS" runat="server"
                        TypeName="Artexacta.App.RedMedica.BLL.RedMedicaBLL"
                        SelectMethod="GetRedMedicaList"
                        OnSelected="RedMedicaODS_Selected">
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenButton" runat="server" />
    <div id="dialog-confirm" title="Eliminar" style="display:none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            Esta seguro que desea eliminar la Red Medica seleccionada?
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

