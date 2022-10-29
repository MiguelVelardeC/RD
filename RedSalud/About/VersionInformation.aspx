<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VersionInformation.aspx.cs" Inherits="About_VersionInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="columns sixteen">
        <h1>
            <asp:Label ID="lblOneColTitle" runat="server"
                Text="Información de la versión e Historial de cambios" />
        </h1>
    </div>
    <div class="clearfix"></div>
    
    <div class="columns sixteen">        
        <asp:DataList ID="ChangesDataList" runat="server" Width="100%">
            <HeaderTemplate>
                <table class="grid" style="width: 100%;">
                    <thead>
                    <tr class="head">
                        <th>
                            <asp:Literal ID="DateLabel" runat="server"
                                Text="Fecha"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="Literal1" runat="server"
                                Text="Versión"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="Literal2" runat="server"
                                Text="Descripción"></asp:Literal>
                        </th>
                    </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="row">
                    <td>
                        <asp:Label ID="DateLabel" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="VersionLabel" runat="server" Text='<%# Bind("Version") %>'></asp:Label>
                    </td>
                    <td class="alignLeft">
                        <asp:Literal ID="DescriptionLiteral" runat="server" Text='<%# Bind("Description") %>'></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="altRow">
                    <td>
                        <asp:Label ID="DateLabel" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="VersionLabel" runat="server" Text='<%# Bind("Version") %>'></asp:Label>
                    </td>
                    <td class="alignLeft">
                        <asp:Literal ID="DescriptionLiteral" runat="server" Text='<%# Bind("Description") %>'></asp:Literal>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>
    </div>
</asp:Content>