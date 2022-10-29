﻿<%@ Page Title="Tipos de Estudio" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TipoEstudioList.aspx.cs" Inherits="Clasificadores_TipoEstudio_TipoEstudioList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Lista de Tipos de Estudio</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" Text="Crear Tipo de Estudio"
                        NavigateUrl="~/Clasificadores/TipoEstudio/TipoEstudioDetails.aspx">
                    </asp:HyperLink>
                </div>

                <div class="gridContainer">
                    <asp:GridView ID="TipoEstudioGridView" runat="server"
                        DataSourceID="TipoEstudioDataSource"
                        OnRowCommand="TipoEstudioGridView_RowCommand"
                        CssClass="grid" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Height="26px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" ImageUrl="~/Images/Neutral/select.png"
                                        CommandName="EditRecord" CommandArgument='<%# Bind("TipoEstudioId") %>'
                                        ToolTip="Editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="deleteRow"
                                        CommandName="DeleteRecord" CommandArgument='<%# Bind("TipoEstudioId") %>'>
                                        <asp:Image runat="server" ImageUrl="~/Images/Neutral/delete.png"
                                            ToolTip="Eliminar" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Nombre (ID)" DataField="NombreForDisplay" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p style="background-color:white">
                                No hay Tipos de Estudio registrados en el sistema.
                            </p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="TipoEstudioDataSource" runat="server"
                        TypeName="Artexacta.App.TipoEstudio.BLL.TipoEstudioBLL"
                        SelectMethod="getTipoEstudioList"
                        OnSelected="TipoEstudioDataSource_Selected">
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

