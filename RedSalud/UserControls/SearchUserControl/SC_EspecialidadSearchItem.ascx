<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SC_EspecialidadSearchItem.ascx.cs" Inherits="UserControls_SearchUserControl_SC_EspecialidadSearchItem" %>

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
            <telerik:RadComboBox ID="EspecialidadRadComboBox" runat="server" CssClass="bigField"
                ShowMoreResultsBox="true" EnableVirtualScrolling="true" EmptyMessage="Seleccione Especialidad"
                AutoPostBack="false" EnableLoadOnDemand="true">
                <WebServiceSettings Method="GetEspecialidad" Path="../AutoCompleteWS/ComboBoxWebServices.asmx"></WebServiceSettings>
            </telerik:RadComboBox>
        </div>
    </div>
</div>
