<%@ Page Title="<% $ Resources : LoginGlossary, NotAuthorizedTitle %>" Language="C#" MasterPageFile="~/SimpleMasterPage.master" AutoEventWireup="true" 
    CodeFile="NotAuthorized.aspx.cs" Inherits="Authentication_NotAuthorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="columnHead">
            <asp:Label ID="NotAuthorizedTitleLabel" runat="server" CssClass="title" Text="<% $ Resources : LoginGlossary, NotAuthorizedTitle %>" />
        </div>
        <div class="columnContent">
            <asp:Literal ID="NotAuthorizedLabel" runat="server" Text="<% $ Resources : LoginGlossary, NotAuthorizedMessage %>"> 
            </asp:Literal>
            <asp:HyperLink ID="StartHyperLink" runat="server" NavigateUrl="~/MainPage.aspx" CssClass="linkButton"
                Text="<% $ Resources : FatalError, StartHyperLinkText %>">
            </asp:HyperLink>
            
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>

