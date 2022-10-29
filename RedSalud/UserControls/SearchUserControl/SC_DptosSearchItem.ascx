<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SC_DptosSearchItem.ascx.cs" Inherits="UserControls_SearchUserControl_SC_DptosSearchItem" %>
<div id="CSearch_Item_Container">
    <div id="CSearch_Item_Operation">
        <asp:RadioButtonList ID="rbtnOperation" runat="server" 
            RepeatDirection="Horizontal"
            CssClass="CSearch_Item_Operation_List" CellPadding="0" CellSpacing="0" 
            BorderWidth="0" BorderStyle="None">
            <asp:ListItem Text="AND" Value="AND" Selected="True"></asp:ListItem>
            <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:Literal ID="lblSpace" runat="server" Visible="false" Text="&nbsp;"></asp:Literal>
    </div>
    
    <div id="CSearch_Item_Panel">
        <asp:Label ID="lblTitle" runat="server" 
                CssClass="CSearch_Item_Label"></asp:Label>
        <div id="CSearch_Item_Control">
            <%--<asp:TextBox ID="CSearch_Item_AspnetControl" runat="server" 
                    CssClass="normalField"></asp:TextBox>--%>
            <asp:DropDownList runat="server" ID="CSearch_Item_AspnetControl"
                CssClass="normalField">
                <asp:ListItem Value="" Text="[TODAS]" Selected="True" />
                <asp:ListItem Text="LA PAZ" />
                <asp:ListItem Text="EL ALTO" />
                <asp:ListItem Text="SANTA CRUZ" />
                <asp:ListItem Text="MONTERO" />
                <asp:ListItem Text="COCHABAMBA" />
                <asp:ListItem Text="CHUQUISACA" />
                <asp:ListItem Text="ORURO" />
                <asp:ListItem Text="POTOSÍ" />
                <asp:ListItem Text="TARIJA" />
                <asp:ListItem Text="BENI" />
                <asp:ListItem Text="PANDO" />
            </asp:DropDownList>
        </div>
    </div>
</div>