<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TipoEstudio.ascx.cs" Inherits="UserControls_TipoEstudio" %>
<asp:Panel ID="TipoEstudioPanel" runat="server">
    <style>
    #<%=TipoEstudioPanel.ClientID %> div#tabsDiv {
            width: 700px;
        }
        #<%=TipoEstudioPanel.ClientID %> div.ui-tabs-panel div.LeftPanel {
            float: left;
            width: 48%;
        }
        #<%=TipoEstudioPanel.ClientID %> div.ui-tabs-panel div.RightPanel{
            float: right;
            width: 48%;
        }
        #<%=TipoEstudioPanel.ClientID %> div.EstudioPanel{
            width: 100%;
            margin-top: 5px;
        }
        #<%=TipoEstudioPanel.ClientID %> div.EstudioPanel{
            margin: 5px 0;
        }
    </style>
    <ul id="tabsTitles"></ul>
    <div id="tabsDiv"></div>
    <asp:Panel runat="server" ID="LeftPanel"></asp:Panel>
    <asp:Panel runat="server" ID="RightPanel"></asp:Panel>
    <div class="clear"></div>
    <asp:HiddenField ID="TipoEstudiosHF" runat="server" Value="" />

    <telerik:RadCodeBlock ID="RadCodeBlockClient" runat="server">
      <script type="text/javascript">

      function execute () {
         var parent = $('#<%=TipoEstudioPanel.ClientID%>').parents('div.Default_Popup');
         if (!parent) {
               parent = $('#<%=TipoEstudioPanel.ClientID%>');
         }
         var left = 0;
         var right = 0;
         parent.css('position', 'absolute');
         //parent.css('visibility', 'hidden');
         parent.css('display', 'block');
         var page = 1;
         var div = '';
         $('#<%=LeftPanel.ClientID%> > div').each(function () {
               if (div == '') { div = createTab(page); }
               if (left <= right) {
                  left += $(this).height();
                  $(div + ' .LeftPanel').append($(this).remove());
               } else {
                  right += $(this).height();
                  $(div + ' .RightPanel').append($(this).remove());
               }
               if (left > 300 && right > 300) {
                  div = createTab(++page);
                  right = left = 0;
               }
         }).promise().done(function () {
               var hfToSave = $('#<%=TipoEstudiosHF.ClientID%>');
               $('#<%=TipoEstudioPanel.ClientID%> input').each(function () {
                  var id = $(this).attr('TipoEstudioId');
                  if (hfToSave.val().match(new RegExp('^' + id + ',|,' + id + ',|' + id + '$'))) {
                     $(this).attr('checked', true);
                  } else {
                     $(this).attr('checked', false);
                  }
               });
               $('#<%=TipoEstudioPanel.ClientID%> input').change(function () {
                  var id = $(this).attr('TipoEstudioId');
                  if ($(this).is(':checked')) {
                     var sep = hfToSave.val() == '' ? '' : ',';
                     hfToSave.val(hfToSave.val() + sep + id);
                  } else {
                     if (hfToSave.val() == id) {
                           hfToSave.val('');
                     } if (hfToSave.val().match(new RegExp('^' + id + ','))) {
                           hfToSave.val(hfToSave.val().replace(new RegExp(id + ',', 'gm'), ''));
                     } else {
                           hfToSave.val(hfToSave.val().replace(new RegExp(',' + id, 'gm'), ''));
                     }
                  }
                  var validation = $('#<%=TipoEstudioPanel.ClientID%>').next();
                  if (validation.hasClass("validation") && (hfToSave.val() != '')) {
                           validation.children().hide();
                  }
               });
               $('#<%=TipoEstudioPanel.ClientID%> input:radio').click(function () {
                  if ($(this).attr('isSelected') == 'true') {
                     $(this).attr('checked', false);
                     $(this).attr('isSelected', false);
                     $(this).change();
                  } else {
                     $(this).attr('isSelected', true);
                  }
               });

               $('#<%=TipoEstudioPanel.ClientID%> div#tabsDiv > div').each(function () {
                  var right = $(this).children('.RightPanel');
                  var left = $(this).children('.LeftPanel');
                  if ($(left).html() == '' && $(right).html() == '') {
                     $('#<%=TipoEstudioPanel.ClientID%> ul#tabsTitles a[href="#' + $(this).attr('id') + '"]').parent().remove();
                     $(this).remove();
                  }
               });

               $("#<%=TipoEstudioPanel.ClientID%>").tabs();
         });
         //parent.attr('style', '');
      }

      function afterAsyncPostBack() {
         execute();
      }

      Sys.Application.add_init(appl_init);

      function appl_init() {
         var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
         pgRegMgr.add_endRequest(EndHandler);
      }

      function EndHandler() {
         afterAsyncPostBack();
      }

      //$(document).ready(execute());

      function createTab(page) {
         var title = 'Pág. ' + page;
         $('#<%=TipoEstudioPanel.ClientID%> ul#tabsTitles').append('<li><a href="#tab' + page + '">' + title + '</a></li>');
         $('#<%=TipoEstudioPanel.ClientID%> div#tabsDiv').append('<div id="tab' + page + '"><div class="LeftPanel"></div><div class="RightPanel"></div></div>');
         <%--//$('#<%=TipoEstudioPanel.ClientID%> div#tabsDiv #' + title.replace(' ', '') + ' .RightPanel').append($('#<%=RightPanel.ClientID%> > div').remove());
         //$('#<%=TipoEstudioPanel.ClientID%> div#tabsDiv #' + title.replace(' ', '') + ' .LeftPanel').append($('#<%=LeftPanel.ClientID%> > div').remove());--%>
         return '#<%=TipoEstudioPanel.ClientID%> div#tabsDiv div#tab' + page;
      }
    </script>
    </telerik:RadCodeBlock>
    

</asp:Panel>