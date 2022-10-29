using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.CLAPrestacionOdontologica;
using Artexacta.App.Utilities.SystemMessages;
using Telerik.Web.UI;

public partial class UserControls_SearchUserControl_SC_PrestacionSearchItem : AbstractSearchItem
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rbtnOperation.Visible = ShowAndOrButtons;
        lblSpace.Visible = !ShowAndOrButtons;
        lblTitle.Text = this._title;
    }

    public override string GetValue()
    {
        if (string.IsNullOrEmpty(PrestacionRCB.SelectedValue.Trim()))
            return "";
        return "@" + this.SearchColumnKey + " \"" + PrestacionRCB.SelectedValue + "\"";
    }

    public override string GetOperation()
    {
        return rbtnOperation.SelectedValue;
    }

    protected void PrestacionRCB_ItemDataBound ( object sender, RadComboBoxItemEventArgs e )
    {
        if (e.Item is RadComboBoxItem)
        {
            RadComboBoxItem item = (RadComboBoxItem)e.Item;
            PrestacionOdontologica ObjReceta = (PrestacionOdontologica)e.Item.DataItem;
            item.IsSeparator = ObjReceta.Categoria;
        }
    }
    protected void PrestacionOdontologicaODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener las Prestaciones Odontológicas para la busqueda.");
            e.ExceptionHandled = true;
        }
    }
}