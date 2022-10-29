<%@ Control Language="C#" AutoEventWireup="true" 
    CodeFile="ContextualHelp.ascx.cs" 
    Inherits="UserControls_ContextualHelp" %>
<script language='javascript' type="text/javascript">
    $(document).ready(function () {
        $('a[id=modal_<%= this.ID %>]').click(function (e) {
            e.preventDefault();

            $.scrollTo($('#TOPSPX'), 800);

            var href = $(this).attr('href');
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();

            $('#<%= CHelpMaskId.ClientID %>').css({ 'width': maskWidth - 5, 'height': maskHeight - 5 });
            $('#<%= CHelpMaskId.ClientID %>').fadeIn(500);
            $('#<%= CHelpMaskId.ClientID %>').fadeTo('fast', 0.8);
            var winH = $(window).height();
            var winW = $(window).width();
            $(href).css('top', winH / 2 - $(href).height() / 2);
            $(href).css('left', winW / 2 - $(href).width() / 2);
            $(href).fadeIn(500);

            return false;
        });
        $('a[id=closeBox_<%= this.ID %>]').click(function (e) {
            e.preventDefault();
            $('#<%= CHelpMaskId.ClientID %>').hide();
            var href = $(this).attr('href');
            $(href).fadeOut(100);

            return false;
        });
    });
</script>
<style type='text/css'>
.CHelpMaskCss 
{
    background-color:#000;
    position:fixed; left:0; top:0; z-index:9000; display:none;
}
.CHelpMainCss
{
    z-index:9999;    
}
</style>
<%-- Main CSS --%>
<asp:Literal ID="MainCssStyleLabel" runat="server"></asp:Literal>
<%-- LINK --%>
<asp:Literal runat="server" ID="ltlLink"></asp:Literal>
<%-- DIV O POPUP --%>
<asp:Literal runat="server" ID="ltlHelpFile"></asp:Literal>
<%-- MASK --%>
<asp:Panel ID="CHelpMaskId" runat="server" CssClass="CHelpMaskCss"></asp:Panel>