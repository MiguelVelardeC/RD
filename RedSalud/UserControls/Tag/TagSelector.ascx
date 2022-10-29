<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagSelector.ascx.cs" Inherits="UserControls_Tag_TagSelector" %>

<ul id="tags" runat="server" style="overflow: hidden;">
</ul>

<asp:HiddenField ID="SelectedTagsHiddenField" runat="server" Value="" />
<asp:HiddenField ID="CssClassHiddenField" runat="server" Value="biggerField" />
<asp:HiddenField ID="MaxTagsHiddenField" runat="server" Value="0" />
<asp:HiddenField ID="EnabledHF" runat="server" Value="" />
<asp:HiddenField ID="TypeHF" runat="server" Value="" />


<script type="text/javascript">
    $(document).ready(function () {
        var initialTags = new Array();
        var tags = $("#<%= SelectedTagsHiddenField.ClientID %>").val().split(",");
        for(var i=0; i < tags.length; i++){
            var value = $.trim(tags[i]);
            initialTags[initialTags.length] = {
                label: value,
				value: value
            }
        }
        <asp:Literal ID="MaxTagsLiteral" runat="server" />
    });

    function <%= ClientID %>getSelectedTags()
    {
        var tags = $("#<%= tags.ClientID %>").tagit("tags");
        if(!tags || tags.length == 0)
            return "";
        var str = tags[0].value;
        for(var i = 1 ; i < tags.length ; i++)
            str += "," + tags[i].value;
        return str;
    }
</script>

