<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Messages.ascx.cs" Inherits="UserControls_Messages" %>
<%-- This is the panel that contains the text that should be shown when the Messages are Shown --%>
<asp:HiddenField ID="MessageHiddenField" runat="server" />
<asp:HiddenField ID="ShowMessageHiddenField" runat="server" Value="false" />
<asp:Literal ID="ltScript" runat="server" />
<script type="text/javascript">
    var opened = false;
    function loadGrowlMessages(onLoad) {
        var messageStr = $('#<%=MessageHiddenField.ClientID %>').val();
        if (messageStr == '' && onLoad == false) {
            $('#systemMessagesContent').jGrowl('close');
            $('#systemMessagesContent').jGrowl('<%= GetGlobalResourceObject("Messages","NoMessages").ToString() %>');
            return;
        } else if (messageStr == '') {
            return;
        }
        var messagesList = messageStr.split("!");
        $('#systemMessagesContent').jGrowl('close');
        for (var i = 0; i < messagesList.length; i++) {
            $('#systemMessagesContent').jGrowl(decodeURIComponent(messagesList[i]), { sticky: true });
        }
    }
    $(document).ready(function () {
        $.jGrowl.defaults.closerTemplate = '<div>[ <%= GetGlobalResourceObject("Messages","CloseAll").ToString() %>]</div>';
        loadGrowlMessages(true);
        if ($('#<%=ShowMessageHiddenField.ClientID %>').val().toLowerCase() == 'true')
            setTimeout(function () {
                $('#systemMessagesContent').jGrowl('close');
            }, 3000);
        $('#systemMessagesLink').click(function () {
            if (opened) {
                $('#systemMessagesContent').jGrowl('close');
                opened = false;
            } else {
                opened = true;
                loadGrowlMessages(false);
            }
            return false;
        });
    });
</script>
<div id="systemMessagesContent" class="jGrowl bottom-right">
</div>
<div class="notificationButton" style="float: left; margin-right: 6px; margin-top: -3px;
    position: relative;">
    <asp:HyperLink NavigateUrl="#" runat="server" ID="systemMessagesLink" ClientIDMode="Static"
        ToolTip='<%$ Resources: Messages, ViewMessages %>'>
        <span class="messageNumber" style="background: none repeat scroll 0 0 red; color: #FFFFFF;
            font-family: verdana; font-size: 1em; opacity: 0.85; padding: 0 4px 0 2px; position: absolute;
            text-align: left; top: 0;"><span>
                <asp:Literal ID="MessagesNumberLiteral" runat="server" />
            </span></span>
        <asp:Image ID="MessagesImage" runat="server" Width="20" Height="20" />
    </asp:HyperLink>
</div>
