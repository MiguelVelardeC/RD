<%@ Page Title="Lista de Enfermedades" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="EnfermedadList.aspx.cs" Inherits="Clasificadores_Enfermedad_EnfermedadList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Enfermedades</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Nueva Enfermedad" CssClass="addNew"
                        NavigateUrl="~/Clasificadores/Enfermedad/EnfermedadDetails.aspx">
                    </asp:HyperLink>
                </div>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="SearchLB">
                    <asp:Label ID="Label6" Text="Buscar Enfermedad por código o nombre" runat="server" CssClass="label" />
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
                    <asp:GridView ID="EnfermedadGridView" runat="server"
                        DataSourceID="EnfermedadDataSource"
                        OnRowCommand="EnfermedadGridView_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord" CommandArgument='<%# Bind("EnfermedadId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="deleteRow"
                                        CommandName="DeleteRecord" CommandArgument='<%# Bind("EnfermedadId") %>'>
                                        <asp:Image runat="server" ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Código" DataField="EnfermedadId" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p style="background-color:white">
                                No hay Enfermedades registrados en el sistema.
                            </p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="EnfermedadDataSource" runat="server"
                        TypeName="Artexacta.App.Enfermedad.BLL.EnfermedadBLL"
                        SelectMethod="getEnfermedadList"
                        OnSelected="EnfermedadDataSource_Selected">
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

