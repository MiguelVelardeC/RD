using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Artexacta.App.TipoEstudio;
using Artexacta.App.TipoEstudio.BLL;

public partial class UserControls_PiezaOdontologia : System.Web.UI.UserControl
{
    public string SelectedValues
    {
        get { return PiezasHF.Value; }
        set { PiezasHF.Value = value; }
    }
    public override string ClientID
    {
        get { return PiezaPanel.ClientID; }
    }
    public bool MultipleSelect
    {
        get { return MultiSelectPanel.Visible;}
        set
        {
            MultiSelectPanel.Visible = value;
            SelectOnePanel.Visible = !value;
        }
    }
    public bool Display
    {
        set
        {
            if (value)
            {
                PiezaPanel.Style.Remove("display");
            }
            else
            {
                PiezaPanel.Style.Add("display", "none");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearSelection();
        }
    }

    public void ClearSelection ()
    {
        TodasCB.Checked = false;
        SupIzquierdoRBL.ClearSelection();
        SupIzquierdoNinosRBL.ClearSelection();
        InfIzquierdoRBL.ClearSelection();
        InfIzquierdoNinosRBL.ClearSelection();
        SupDerechoRBL.ClearSelection();
        SupDerechoNinosRBL.ClearSelection();
        InfDerechoRBL.ClearSelection();
        InfDerechoNinosRBL.ClearSelection();
        PiezasHF.Value = "";
    }
}