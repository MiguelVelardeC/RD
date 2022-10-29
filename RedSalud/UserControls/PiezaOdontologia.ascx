<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PiezaOdontologia.ascx.cs" Inherits="UserControls_PiezaOdontologia" %>
<asp:Panel ID="PiezaPanel" runat="server">
    <span class="label">PIEZA: <asp:CheckBox ID="TodasCB" AutoPostBack="false" runat="server" Text="Todas" /></span>
    <style>
        .piezaRBL input
        {
            display:block;
            margin:0 auto;
        }
        
    </style>
    <asp:Panel ID="SelectOnePanel" runat="server" style="width:350px;">
        <div style="float:left;">
            <asp:RadioButtonList ID="SupIzquierdoRBL" runat="server" CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="18" Value="18" />
                <asp:ListItem Text="17" Value="17" />
                <asp:ListItem Text="16" Value="16" />
                <asp:ListItem Text="15" Value="15" />
                <asp:ListItem Text="14" Value="14" />
                <asp:ListItem Text="13" Value="13" />
                <asp:ListItem Text="12" Value="12" />
                <asp:ListItem Text="11" Value="11" />
            </asp:RadioButtonList>
            <asp:RadioButtonList ID="SupIzquierdoNinosRBL" runat="server" style="float:right;" 
                CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="55" Value="55" />
                <asp:ListItem Text="54" Value="54" />
                <asp:ListItem Text="53" Value="53" />
                <asp:ListItem Text="52" Value="52" />
                <asp:ListItem Text="51" Value="51" />
            </asp:RadioButtonList>
            <div class="clearfix"></div>
            <asp:RadioButtonList ID="InfIzquierdoNinosRBL" runat="server" style="float:right;" 
                CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="85" Value="85" />
                <asp:ListItem Text="84" Value="84" />
                <asp:ListItem Text="83" Value="83" />
                <asp:ListItem Text="82" Value="82" />
                <asp:ListItem Text="81" Value="81" />
            </asp:RadioButtonList>
            <div class="clearfix"></div>
            <asp:RadioButtonList ID="InfDerechoRBL" runat="server" 
                CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="48" Value="48" />
                <asp:ListItem Text="47" Value="47" />
                <asp:ListItem Text="46" Value="46" />
                <asp:ListItem Text="45" Value="45" />
                <asp:ListItem Text="44" Value="44" />
                <asp:ListItem Text="43" Value="43" />
                <asp:ListItem Text="42" Value="42" />
                <asp:ListItem Text="41" Value="41" />
            </asp:RadioButtonList>
        </div>
        <div style="float:right;">
            <asp:RadioButtonList ID="SupDerechoRBL" runat="server" 
                CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="21" Value="21" />
                <asp:ListItem Text="22" Value="22" />
                <asp:ListItem Text="23" Value="23" />
                <asp:ListItem Text="24" Value="24" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="26" Value="26" />
                <asp:ListItem Text="27" Value="27" />
                <asp:ListItem Text="28" Value="28" />
            </asp:RadioButtonList>
            <asp:RadioButtonList ID="SupDerechoNinosRBL" runat="server"
                CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="61" Value="61" />
                <asp:ListItem Text="62" Value="62" />
                <asp:ListItem Text="63" Value="63" />
                <asp:ListItem Text="64" Value="64" />
                <asp:ListItem Text="65" Value="65" />
            </asp:RadioButtonList>
            <asp:RadioButtonList ID="InfDerechoNinosRBL" runat="server" 
                CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="71" Value="71" />
                <asp:ListItem Text="72" Value="72" />
                <asp:ListItem Text="73" Value="73" />
                <asp:ListItem Text="74" Value="74" />
                <asp:ListItem Text="75" Value="75" />
            </asp:RadioButtonList>
            <asp:RadioButtonList ID="InfIzquierdoRBL" runat="server" CssClass="piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="31" Value="31" />
                <asp:ListItem Text="32" Value="32" />
                <asp:ListItem Text="33" Value="33" />
                <asp:ListItem Text="34" Value="34" />
                <asp:ListItem Text="35" Value="35" />
                <asp:ListItem Text="36" Value="36" />
                <asp:ListItem Text="37" Value="37" />
                <asp:ListItem Text="38" Value="38" />
            </asp:RadioButtonList>
        </div>
        <div class="clearfix"></div>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#<%=TodasCB.ClientID%>').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('disabled', true);
                        $('#<%=PiezasHF.ClientID%>').val('ALL');
                    } else {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('disabled', false);
                        $('#<%=PiezasHF.ClientID%>').val('');
                    }
                });
                $('#<%=SupIzquierdoRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=SupIzquierdoNinosRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=InfIzquierdoRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=InfIzquierdoNinosRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=SupDerechoRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=SupDerechoNinosRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=InfDerechoRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoNinosRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
                $('#<%=InfDerechoNinosRBL.ClientID%> input').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('#<%=SupIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfIzquierdoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoRBL.ClientID%> input').prop('checked', false);
                        $('#<%=SupDerechoNinosRBL.ClientID%> input').prop('checked', false);
                        $('#<%=InfDerechoRBL.ClientID%> input').prop('checked', false);
                    }
                    getSelected();
                });
            });
            function getSelected() {
                var value = $('#<%=SupIzquierdoRBL.ClientID%> input:checked').val();
                if (value == undefined) {
                    value = $('#<%=SupIzquierdoNinosRBL.ClientID%> input:checked').val();
                }
                if (value == undefined) {
                    value = $('#<%=InfIzquierdoRBL.ClientID%> input:checked').val();
                }
                if (value == undefined) {
                    value = $('#<%=InfIzquierdoNinosRBL.ClientID%> input:checked').val();
                }
                if (value == undefined) {
                    value = $('#<%=SupDerechoRBL.ClientID%> input:checked').val();
                }
                if (value == undefined) {
                    value = $('#<%=SupDerechoNinosRBL.ClientID%> input:checked').val();
                }
                if (value == undefined) {
                    value = $('#<%=InfDerechoRBL.ClientID%> input:checked').val();
                }
                if (value == undefined) {
                    value = $('#<%=InfDerechoNinosRBL.ClientID%> input:checked').val();
                }
                $('#<%=PiezasHF.ClientID%>').val(value);
            }
        </script>
    </asp:Panel>
    <asp:Panel ID="MultiSelectPanel" runat="server" style="width:350px;" Visible="false">
        <div style="float:left;">
            <asp:CheckBoxList runat="server" CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="18" Value="18" />
                <asp:ListItem Text="17" Value="17" />
               <asp:ListItem Text="16" Value="16" />
                <asp:ListItem Text="15" Value="15" />
                <asp:ListItem Text="14" Value="14" />
                <asp:ListItem Text="13" Value="13" />
                <asp:ListItem Text="12" Value="12" />
                <asp:ListItem Text="11" Value="11" />
            </asp:CheckBoxList>
            <asp:CheckBoxList ID="RadioButtonList2" runat="server" style="float:right;" 
                CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="55" Value="55" />
                <asp:ListItem Text="54" Value="54" />
                <asp:ListItem Text="53" Value="53" />
                <asp:ListItem Text="52" Value="52" />
                <asp:ListItem Text="51" Value="51" />
            </asp:CheckBoxList>
            <div class="clearfix"></div>
            <asp:CheckBoxList ID="RadioButtonList3" runat="server" style="float:right;" 
                CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="85" Value="85" />
                <asp:ListItem Text="84" Value="84" />
                <asp:ListItem Text="83" Value="83" />
                <asp:ListItem Text="82" Value="82" />
                <asp:ListItem Text="81" Value="81" />
            </asp:CheckBoxList>
            <div class="clearfix"></div>
            <asp:CheckBoxList ID="RadioButtonList4" runat="server" 
                CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="48" Value="48" />
                <asp:ListItem Text="47" Value="47" />
                <asp:ListItem Text="46" Value="46" />
                <asp:ListItem Text="45" Value="45" />
                <asp:ListItem Text="44" Value="44" />
                <asp:ListItem Text="43" Value="43" />
                <asp:ListItem Text="42" Value="42" />
                <asp:ListItem Text="41" Value="41" />
            </asp:CheckBoxList>
        </div>
        <div style="float:right;">
            <asp:CheckBoxList ID="RadioButtonList5" runat="server" 
                CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="21" Value="21" />
                <asp:ListItem Text="22" Value="22" />
                <asp:ListItem Text="23" Value="23" />
                <asp:ListItem Text="24" Value="24" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="26" Value="26" />
                <asp:ListItem Text="27" Value="27" />
                <asp:ListItem Text="28" Value="28" />
            </asp:CheckBoxList>
            <asp:CheckBoxList ID="RadioButtonList6" runat="server"
                CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="61" Value="61" />
                <asp:ListItem Text="62" Value="62" />
                <asp:ListItem Text="63" Value="63" />
                <asp:ListItem Text="64" Value="64" />
                <asp:ListItem Text="65" Value="65" />
            </asp:CheckBoxList>
            <asp:CheckBoxList ID="RadioButtonList7" runat="server" 
                CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="71" Value="71" />
                <asp:ListItem Text="72" Value="72" />
                <asp:ListItem Text="73" Value="73" />
                <asp:ListItem Text="74" Value="74" />
                <asp:ListItem Text="75" Value="75" />
            </asp:CheckBoxList>
            <asp:CheckBoxList ID="RadioButtonList8" runat="server" CssClass="piezasList piezaRBL" RepeatDirection="Horizontal">
                <asp:ListItem Text="31" Value="31" />
                <asp:ListItem Text="32" Value="32" />
                <asp:ListItem Text="33" Value="33" />
                <asp:ListItem Text="34" Value="34" />
                <asp:ListItem Text="35" Value="35" />
                <asp:ListItem Text="36" Value="36" />
                <asp:ListItem Text="37" Value="37" />
                <asp:ListItem Text="38" Value="38" />
            </asp:CheckBoxList>
        </div>
        <div class="clearfix"></div>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#<%=TodasCB.ClientID%>').on('change', function () {
                    if ($(this).is(':checked')) {
                        $('.piezasList input[type=checkbox]').prop('disabled', true);
                        $('.piezasList input[type=checkbox]').prop('checked', false);
                        $('#<%=PiezasHF.ClientID%>').val('ALL');
                    } else {
                        $('.piezasList input[type=checkbox]').prop('disabled', false);
                        $('#<%=PiezasHF.ClientID%>').val('');
                    }
                });
                $('.piezasList input[type=checkbox]').click(function () {
                    var selectedValues = [];
                    $(".piezasList input:checked").each(function () {
                        selectedValues.push($(this).next().html());
                    });
                    if (selectedValues.length > 0) {
                        $('#<%=PiezasHF.ClientID%>').val(selectedValues);
                    } else {
                        $('#<%=PiezasHF.ClientID%>').val('');
                    }
                });
            });
        </script>
    </asp:Panel>
    <asp:HiddenField ID="PiezasHF" runat="server" Value="" />
</asp:Panel>