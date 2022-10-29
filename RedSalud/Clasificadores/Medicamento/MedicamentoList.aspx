<%@ Page Title="Lista de Medicamentos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="MedicamentoList.aspx.cs" Inherits="Clasificadores_Medicamento_MedicamentoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Medicamentos</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Nuevo Medicamento" CssClass="addNew"
                        NavigateUrl="~/Clasificadores/Medicamento/MedicamentoDetails.aspx">
                    </asp:HyperLink>
                </div>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="SearchLB">
                    <asp:Label ID="Label6" Text="Buscar Medicamento por código o nombre" runat="server" CssClass="label" />
                    <asp:TextBox ID="SearchTexbox" runat="server" CssClass="biggerField left" />
                    <div class="buttonsPanel left" style="margin:0 20px;">
                        <asp:LinkButton ID="SearchLB" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="SearchCaso">
                            <asp:Label ID="Label7" Text="Buscar" runat="server" />
                        </asp:LinkButton>
                    </div>
                    <div class="clear"></div>
                </asp:Panel>

                <div class="gridContainer">
                    <asp:GridView ID="MedicamentoGridView" runat="server"
                        DataSourceID="MedicamentoDataSource"
                        OnRowCommand="MedicamentoGridView_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord" CommandArgument='<%# Bind("MedicamentoId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="deleteRow"
                                        CommandName="DeleteRecord" CommandArgument='<%# Bind("MedicamentoId") %>'>
                                        <asp:Image runat="server" ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p style="background-color:white">
                                No hay Medicamentos registrados en el sistema.
                            </p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="MedicamentoDataSource" runat="server"
                        TypeName="Artexacta.App.Medicamento.BLL.MedicamentoBLL"
                        SelectMethod="getMedicamentoList"
                        OnSelected="MedicamentoDataSource_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="SearchTexbox" Name="search" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenButton" runat="server" />
    <div id="dialog-confirm" title="Eliminar" style="display:none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            Esta seguro que desea eliminar el registro seleccionado?
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

