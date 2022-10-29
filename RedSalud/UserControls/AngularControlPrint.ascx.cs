using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_AngularControlPrint : System.Web.UI.UserControl
{
    public bool readOnly
    {
        get { return ReadPanel.Visible; }
        set {
            ReadPanel.Visible = value;
            EditPanel.Visible = !value; 
        }
    }
    public string JSonData
    {
        get { return JSonDataHF.Value; }
        set { JSonDataHF.Value = value; }
    }
    public string CssClass
    {
        get { return Containter.CssClass; }
        set { Containter.CssClass += value; }
    }
    public string OnClientChange { get; set; }
    public string OnClientLoad { get; set; }
    public int maxLength { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        createController();
    }
    protected void createController ()
    {
        string controllerName = this.ClientID + "_Controller";
        if (string.IsNullOrWhiteSpace(OnClientChange)) { OnClientChange = "null"; }
        if (string.IsNullOrWhiteSpace(OnClientLoad)) { OnClientLoad = "null"; }
        createControllerScript.Text += "<script type=\"text/javascript\"> var " + controllerName + " = CreateControll('" +
            JSonDataHF.ClientID + "', '" + ResolveClientUrl("~/Images/calendar.gif") + "', " + maxLength + ", " + OnClientLoad + ", " + OnClientChange + ");</script>";
        if (readOnly)
        {
            ReadPanel.Attributes.Add("ng-controller", controllerName);
        }
        else
        {
            EditPanel.Attributes.Add("ng-controller", controllerName);
        }
    }
}