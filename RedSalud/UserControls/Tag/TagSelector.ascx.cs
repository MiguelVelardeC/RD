using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class UserControls_Tag_TagSelector : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public string CssClass
    {
        set { CssClassHiddenField.Value = value; }
        get { return CssClassHiddenField.Value; }
    }

    public override string ClientID
    {
        get { return tags.ClientID; }
    }

    public bool Enabled
    {
        set { EnabledHF.Value = value ? "" : "0"; }
        get { return EnabledHF.Value == ""; }
    }
    public string Type
    {//"GestionMedica" : "Proveedor"
        set { TypeHF.Value = value; }
        get { return TypeHF.Value; }
    }

    public int MaxTags
    {
        set { MaxTagsHiddenField.Value = value.ToString(); }
        get
        {
            int maxTags = 0;
            try
            {
                maxTags = Convert.ToInt32(MaxTagsHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert MaxTagsHiddenField.Value to integer value. Returning zero as default value.", ex);
            }
            return maxTags;
        }
    }

    public string SelectedTags
    {
        set { SelectedTagsHiddenField.Value = value; }
        get { return SelectedTagsHiddenField.Value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        tags.Attributes["class"] = "tagit " + CssClassHiddenField.Value;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string script = "$('#" + tags.ClientID + "').tagit({" +
            "triggerKeys: ['enter', 'comma', 'tab']," +
            "initialTags: initialTags," +
            "tagSource: function( request, response ) {" +
                "var term = $.trim(request.term);" +
                "var splitValues = term.split(' ');" +
                "if(splitValues.length > 1){" +
                    "term = splitValues[0];" +
                    "for(var i = 1; i < splitValues.length; i++){" +
                        "var str = $.trim(splitValues[i]);" +
                        "if(str != '')" +
                            "term += ' ' + str;" +
                    "}" +
                "}" +
                "var param = '?q=' + term + '&selected=' + " + ClientID + "getSelectedTags() + '&type=" + Type + "';" +
				"$.ajax({" +
					"url: '" + VirtualPathUtility.ToAbsolute("~/UserControls/Tag/Handlers/GetTagsForAutocompleteHandler.ashx") + "' + param," +
					"dataType: 'json'," +
					"success: function( data ) {" +
						"response( $.map( data, function( item ) {" +
                             "return {" +
							    "label: item," +
							    "value: item" +
                            "}" +
						"}));" +
					"}" +
				"});" +
			"}," +
            "tagsChanged: function(tagValue, action, element) {" +
                "var t = " + ClientID + "getSelectedTags();" +
                "$('#" + SelectedTagsHiddenField.ClientID  + "').val(t);" +
            "}";
        if (MaxTags > 0)
            script += ", maxTags: " + MaxTags;

        if (!Enabled)
            script = "$('#" + tags.ClientID + "').tagit({ initialTags: initialTags, allowNewTags: false,minLength: 1";

        string doNotRemove = "$('.tagit-close, .tagit-new').hide();";

        MaxTagsLiteral.Text = script + "});" + (Enabled ? "" : doNotRemove);
    }
}