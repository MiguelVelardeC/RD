using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Artexacta.App.TipoEstudio;
using Artexacta.App.TipoEstudio.BLL;
using Artexacta.App.ProveedorPrestaciones.BLL;

public partial class UserControls_TipoEstudio : System.Web.UI.UserControl
{ 
    private int _ProveedorId;
    private int _ClienteId;

    public int ProveedorId
    {
        get { return _ProveedorId; }
        set { _ProveedorId = value; }
    }
    public int ClienteIds
    {
        get { return _ClienteId; }
        set { _ClienteId = value; }
    }
    public string SelectedValues
    {
        get { return TipoEstudiosHF.Value; }
        set { TipoEstudiosHF.Value = value; }
    }
    public override string ClientID
    {
        get { return TipoEstudioPanel.ClientID; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearSelection();
        }
        //string comboId = this.Parent.FindControl("TipoEstudioPrestacionesExaComboBox").ClientID;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Exec", "execute('" + comboId + "');", true);
        //GenerateList(TipoEstudioBLL.getTipoEstudioParentList(), LeftPanel, 1);
    }
    int ExisteEstudioSinHijos = 0;
    private void GenerateList(List<TipoEstudio> list, Panel panel, int level)
    {
        foreach (TipoEstudio tipo in list)
        {
            if (tipo.CantHijos == 0)
            {
                if (level > 2)
                {
                    //----------- LEVEL 3 ------------
                    RadioButton rb = new RadioButton();
                    rb.ID = tipo.Nombre.Replace(" ", "_").Replace(".", "") + "_" + tipo.TipoEstudioId;
                    rb.GroupName = tipo.Nombre.Replace(" ", "_").Replace(".", "");
                    rb.Text = tipo.Nombre;
                    rb.InputAttributes.Add("TipoEstudioId", tipo.TipoEstudioId.ToString());
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Controls.Add(rb);
                    panel.Controls.Add(div);
                }
                else
                {
                    if (!tipo.Nombre.Contains("No Existe"))
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = tipo.Nombre;
                        cb.ID = tipo.Nombre.Replace(" ", "_").Replace(".", "") + "_" + tipo.TipoEstudioId;
                        cb.InputAttributes.Add("TipoEstudioId", tipo.TipoEstudioId.ToString());
                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Controls.Add(cb);
                        panel.Controls.Add(div);
                    }
                    else
                    {
                        //----------- LEVEL 2 ------------
                        Label lb = new Label();
                        lb.Text = tipo.Nombre;
                        HtmlGenericControl div = new HtmlGenericControl("div");
                        div.Controls.Add(lb);
                        panel.Controls.Add(div);
                    }
                }
            }
            else
            {
                //----------- LEVEL 1 ------------
                List<TipoEstudio> childrens = tipo.ChildrensNew(ProveedorId,tipo.TipoEstudioId);
                //List<TipoEstudio> childrens = tipo.Childrens;
                Panel pn = new Panel();
                pn.CssClass = "EstudioPanel";
                pn.GroupingText = tipo.Nombre + " ";
                pn.ID = tipo.Nombre.Replace(" ", "_") + "_" + tipo.TipoEstudioId;
                panel.Controls.Add(pn);
                GenerateList(childrens, pn, level + 1);
            }
        }
    }

    public void ClearSelection()
    {
        TipoEstudiosHF.Value = "";
    }
    public void CargarEstudios(int ClienteId,int proveedorId,string TipoEstudio)
    {
        ProveedorId = proveedorId;
        ClienteIds = ClienteId;

        GenerateList(TiposEstudiosProvPrestacionesBLL.getTipoEstudioParentList(ClienteId,proveedorId,TipoEstudio), LeftPanel, 1);

       // string script = @"
       // function execute () {
       //  var parent = $('#" + TipoEstudioPanel.ClientID + @"').parents('div.Default_Popup');
       //  if (!parent) {
       //        parent = $('#" + TipoEstudioPanel.ClientID + @"');
       //  }
       //  var left = 0;
       //  var right = 0;
       //  parent.css('position', 'absolute');
       //  //parent.css('visibility', 'hidden');
       //  parent.css('display', 'block');
       //  var page = 1;
       //  var div = '';
       //  $('#" + LeftPanel.ClientID + @" > div').each(function () {
       //        if (div == '') { div = createTab(page); }
       //        if (left <= right) {
       //           left += $(this).height();
       //           $(div + ' .LeftPanel').append($(this).remove());
       //        } else {
       //           right += $(this).height();
       //           $(div + ' .RightPanel').append($(this).remove());
       //        }
       //        if (left > 300 && right > 300) {
       //           div = createTab(++page);
       //           right = left = 0;
       //        }
       //  }).promise().done(function () {
       //     var hfToSave = $('#" + TipoEstudiosHF.ClientID + @"');
       //     $('#" + TipoEstudioPanel.ClientID + @" input').each(function () {
       //         var id = $(this).attr('TipoEstudioId');
       //         if (hfToSave.val().match(new RegExp('^' + id + ',|,' + id + ',|' + id + '$'))) {
       //             $(this).attr('checked', true);
       //         } else {
       //             $(this).attr('checked', false);
       //         }
       //      });
       //      $('#" + TipoEstudioPanel.ClientID + @" input').change(function () {
       //         var id = $(this).attr('TipoEstudioId');
       //         if ($(this).is(':checked')) {
       //             var sep = hfToSave.val() == '' ? '' : ',';
       //             hfToSave.val(hfToSave.val() + sep + id);
       //         } else {
       //             if (hfToSave.val() == id) {
       //                 hfToSave.val('');
       //             } if (hfToSave.val().match(new RegExp('^' + id + ','))) {
       //                 hfToSave.val(hfToSave.val().replace(new RegExp(id + ',', 'gm'), ''));
       //             } else {
       //                 hfToSave.val(hfToSave.val().replace(new RegExp(',' + id, 'gm'), ''));
       //             }
       //         }
       //         var validation = $('#" + TipoEstudioPanel.ClientID + @"').next();
       //         if (validation.hasClass('validation') && (hfToSave.val() != '')) {
       //                 validation.children().hide();
       //         }
       //     });
       //     $('#" + TipoEstudioPanel.ClientID + @" input:radio').click(function () {
       //         if ($(this).attr('isSelected') == 'true') {
       //                          $(this).attr('checked', false);
       //                          $(this).attr('isSelected', false);
       //                          $(this).change();
       //         } else {
       //                          $(this).attr('isSelected', true);
       //         }
       //     });

       //     $('#" + TipoEstudioPanel.ClientID + @" div#tabsDiv > div').each(function () {
       //         var right = $(this).children('.RightPanel');
       //         var left = $(this).children('.LeftPanel');
       //         if ($(left).html() == '' && $(right).html() == '') {
       //                          $('#" + TipoEstudioPanel.ClientID + @" ul#tabsTitles a[href=""#' + $(this).attr('id') + '""]').parent().remove();
       //                          $(this).remove();
       //         }
       //     });

       //     $('#" + TipoEstudioPanel.ClientID + @"').tabs();
       //  });
       //  parent.attr('style', '');
       //}";

       // //-----------------------

       // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Exec", script, true);
    }

}