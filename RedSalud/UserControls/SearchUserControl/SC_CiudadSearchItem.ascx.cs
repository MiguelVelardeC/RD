using Artexacta.App.Ciudad.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_SearchUserControl_SC_CiudadSearchItem : AbstractSearchItem
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public bool EnableAll
    {
        get { return EnableAllHF.Value == "1"; }
        set { EnableAllHF.Value = value ? "1" : "0"; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EnableAllHF.Value))
            EnableAllHF.Value = "1";
        try
        {
            CiudadCombo.DataSource = CiudadBLL.getCiudadList();
            CiudadCombo.DataBind();

            CiudadCombo.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Todas", "TTT"));
            CiudadCombo.Items.FindItemByValue("TTT").Selected = true;
        }
        catch (Exception q)
        {
            log.Error("Error in FillComboCiudades in MainPage.aspx", q);
        }
        rbtnOperation.Visible = ShowAndOrButtons;
        lblSpace.Visible = !ShowAndOrButtons;
        lblTitle.Text = this._title;
    }

    public override string GetValue()
    {
        if (string.IsNullOrEmpty(CiudadCombo.SelectedValue.Trim()))
            return "";
        if (EnableAllHF.Value == "0" && CiudadCombo.SelectedValue == "TTT")
            return "";
        return "@" + this.SearchColumnKey + " \"" + CiudadCombo.SelectedValue + "\"";
    }

    public override string GetOperation()
    {
        return rbtnOperation.SelectedValue;
    }

}