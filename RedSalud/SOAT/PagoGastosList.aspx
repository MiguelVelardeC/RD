<%@ Page Title="PagoGastos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PagoGastosList.aspx.cs"
    Inherits="SOAT_PagoGastosList" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .SearchTitle {
            display: block;
            font-weight: bold;
            float: none!important;
        }

        .alertaDias {
            background-color: #FFCCCB;
        }

        .sortable {
            cursor: pointer!important;
        }

        .sortCol {
            font-weight: bold!important;
            cursor: pointer!important;
        }

        .sortImg {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="PageTitleLabel" Text="" runat="server" CssClass="title" />
            </div>
            <asp:Panel runat="server">
                <div class="buttonsPanel" style="float: left;">
                    <div id="ClienteSOATContainer">
                        <asp:Label Text="Cliente" runat="server" CssClass="label" />
                        <asp:DropDownList ID="ClienteDDL" runat="server" DataSourceID="ClienteODS"
                            Style="width: 348px; display: block;" AutoPostBack="true"
                            DataValueField="ClienteId" DataTextField="NombreJuridico"
                            OnSelectedIndexChanged="ClienteDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ClienteODS" runat="server"
                            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="getRedClienteForSOATList"
                            OnSelected="ClienteODS_Selected"></asp:ObjectDataSource>
                    </div>
                    <search:SearchControl ID="SearchPagoGastos" runat="server"
                        Title="Buscar"
                        DisplayHelp="true"
                        DisplayContextualHelp="false"
                        CssSearch="CSearch"
                        CssSearchHelp="CSearchHelpPanel"
                        CssSearchError="CSearchErrorPanel"
                        AdvancedSearchForm="~/UserControls/AdvancedSearch/PagoSOATAdvancedSearch.ascx"
                        SavedSearches="false"
                        SavedSearchesID="searchCtl_PagoGastosList"
                        ImageHelpUrl="Images/Neutral/Help.png"
                        ImageErrorUrl="~/images/exclamation.png" />
                </div>
                <div style="float: left; margin-top: 34px;">
                    <asp:CheckBox ID="MostrarPagadasCheckBox" Text="MOSTRAR TAMBIEN PAGADAS" runat="server"
                        OnCheckedChanged="MostrarPagadas_CheckedChanged" AutoPostBack="true" />
                </div>
            </asp:Panel>

            <telerik:RadGrid ID="PagoGastosRadGrid" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="false"
                AllowMultiRowSelection="False"
                Width="100%"
                OnItemCommand="PagoGastosRadGrid_ItemCommand"
                OnItemDataBound="PagoGastosRadGrid_ItemDataBound"
                OnExcelExportCellFormatting="PagoGastosRadGrid_ExcelExportCellFormatting">
                <ClientSettings>
                    <ClientEvents OnColumnClick="OnColumnClick" />
                </ClientSettings>
                <ExportSettings ExportOnlyData="true" Excel-Format="Html" FileName="GastosPagados" OpenInNewWindow="true"></ExportSettings>
                <MasterTableView DataKeyNames="PagoGastosId">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowCancelChangesButton="false"
                        ShowExportToExcelButton="true" ShowRefreshButton="false" />
                    <NoRecordsTemplate>
                        <div style="text-align: center;">No hay Pagos de SOAT a mostrar.</div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnPagarGasto" HeaderText="Pagar">
                            <ItemTemplate>
                                <asp:ImageButton ID="PagarGasto" runat="server"
                                    ImageUrl="~/Images/Neutral/gastosMedicos.png"
                                    OnClientClick='<%# "return ShowNPDialog(" + Eval("GastosEjecutadosDetalleId") + ");" %>'
                                    Width="24px" ToolTip="Pagar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnDeleteGTC" HeaderText="Eliminar" HeaderTooltip="Eliminar">
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteImageButton" runat="server"
                                    ImageUrl="~/Images/neutral/delete.png"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("PagoGastosId") %>'
                                    OnClientClick="return confirm('¿Está seguro que desea eliminar el Pago?');"
                                    Width="24px" CommandName="Eliminar"
                                    ToolTip="Eliminar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="GED.Tipo" DataField="TipoGasto"
                            HeaderText="Tipo de Gasto" />
                        <telerik:GridBoundColumn UniqueName="A.Nombre" DataField="Paciente"
                            HeaderText="Paciente" />
                        <telerik:GridBoundColumn UniqueName="Proveedor" DataField="Proveedor"
                            HeaderText="Proveedor" />
                        <telerik:GridBoundColumn UniqueName="GED.FechaReciboFactura" DataField="FechaEmision"
                            HeaderText="Fecha de Recepción" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="30px" />
                        <telerik:GridBoundColumn UniqueName="GED.NumeroReciboFactura" DataField="NumeroReciboFactura"
                            HeaderText="Número de Factura" />
                        <telerik:GridBoundColumn UniqueName="DecimalGED.Monto" DataField="Monto" DataType="System.Decimal"
                            HeaderText="Monto" DataFormatString="{0:#,##0.00}" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn UniqueName="FechaPago" DataField="FechaPago"
                            HeaderText="Fecha de Pago" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="30px" />
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnDiasTranscurridos"
                            HeaderText="Días Transcurridos" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# DiasTranscurridos((DateTime)Eval("FechaPago"), (DateTime)Eval("FechaEmision")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="fullname" DataField="User"
                            HeaderText="Usuario" />
                        <telerik:GridTemplateColumn UniqueName="Efectivo"
                            HeaderText="Pago en Efectivo" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# (bool)Eval("Efectivo") ? "SI" : "NO" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="NroCheque" DataField="NroCheque"
                            HeaderText="Nro. de Cheque" />
                        <telerik:GridBoundColumn UniqueName="bancoEmisor" DataField="bancoEmisor"
                            HeaderText="Banco Emisor" />
                        <telerik:GridBoundColumn UniqueName="PagoGastosId" DataField="Estado"
                            HeaderText="Estado" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div>
                <span style="font-weight: bold;">Viendo Pagos del </span>
                <asp:Label ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Label>
                <span style="font-weight: bold;">al </span>
                <asp:Label ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Label>
                <span style="font-weight: bold;">de un total de </span>
                <asp:Label ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Label>
            </div>
            <div class="buttonsPanel">
                <asp:LinkButton CssClass="button" ID="FirstButton" runat="server" OnClick="PrimeroButton_Click">
                    <asp:label Text="Primera" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton CssClass="button" ID="PreevFastButton" runat="server" OnClick="AnteriorRapidoButton_Click">
                    <asp:label Text="-5 Páginas" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton CssClass="button" ID="PreevButton" runat="server" OnClick="AnteriorButton_Click">
                    <asp:label Text="Anterior" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton CssClass="button" ID="NextButton" runat="server" OnClick="SiguienteButton_Click">
                    <asp:label Text="Siguiente" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton CssClass="button" ID="NextFastButton" runat="server" OnClick="SiguienteRapidoButton_Click">
                    <asp:label Text="+5 Páginas" runat="server" />
                </asp:LinkButton>
                <asp:LinkButton CssClass="button" ID="LastButton" runat="server" OnClick="UltimoButton_Click">
                    <asp:label Text="Ultimo" runat="server" />
                </asp:LinkButton>
            </div>


            <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
            <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
            <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="OrderByHF" runat="server" />
            <asp:HiddenField ID="GastosEjecutadosDetalleIdHF" runat="server" />

            <asp:Panel ID="NewPagoPanel" runat="server" ToolTip="Datos del Pago" Style="display: none;"
                DefaultButton="NGSaveLB">
                <div style="margin-top: 5px; font-weight: bold; text-transform: uppercase;">
                    <asp:CheckBox ID="EfectivoCheckBox" Text="Pago en Efectivo" runat="server" onclick="EfectivoCheckBox_CLick(this);" />
                    <script type="text/javascript">
                        function EfectivoCheckBox_CLick(sender) {
                            if ($(sender).prop('checked')) {
                                $('#<%=BancoEmisorTextBox.ClientID%>').attr("disabled", "disabled");
                                $('#<%=NumeroChequeTextBox.ClientID%>').attr("disabled", "disabled");
                                $('#<%=BancoEmisorTextBox.ClientID%>').val('');
                                $('#<%=NumeroChequeTextBox.ClientID%>').val('');
                            } else {
                                $('#<%=BancoEmisorTextBox.ClientID%>').removeAttr("disabled");
                                $('#<%=NumeroChequeTextBox.ClientID%>').removeAttr("disabled");
                            }
                        }
                    </script>
                </div>

                <div>
                    <span class="label">Nro de Cheque:</span>
                    <asp:TextBox runat="server" ID="NumeroChequeTextBox"
                        CssClass="normalField" />
                    <div class="validation">
                        <span id="NumeroChequeError" style="display: none; color: Red;">El Número de Cheque es Requerido</span>
                    </div>
                </div>

                <div>
                    <span class="label">Banco Emisor:</span>
                    <asp:TextBox runat="server" ID="BancoEmisorTextBox"
                        CssClass="normalField" />
                    <div class="validation"><span id="BancoEmisorError" style="display: none; color: Red;">El Banco Emisor es Requerido</span></div>
                </div>

                <span class="label">Al hacer clic en Confirmar Pago confirmo la entrega del cheque y/o efectivo correspondiente al pago de la factura del proveedor.
                    <br />
                    <br />
                    Se guardará la información de la fecha del pago y también de mi *usuario*.
                </span>

                <div class="buttonsPanel">
                    <asp:LinkButton ID="NGSaveLB" runat="server"
                        ValidationGroup="NPSave" OnClientClick="return ClientValidate();"
                        CssClass="button" OnClick="NGSaveLB_Click">
                        <span style="color: #FFF;">Confirmar Pago</span>
                    </asp:LinkButton>
                    <asp:HyperLink Text="Cancelar" NavigateUrl="javascript:CloseNPDialog();" runat="server" />
                </div>
                <asp:HiddenField runat="server" ID="TypeHF" />
                <asp:HiddenField runat="server" ID="saveIdHF" />
                <asp:HiddenField runat="server" ID="DecimalSimbolHF" />
            </asp:Panel>
            <script type="text/javascript">
                $(document).ready(function () {
                    var n = 1.1;
                    $('#<%= DecimalSimbolHF.ClientID%>').val(n.toLocaleString().substring(1, 2));
                });
                function ClientValidate() {
                    var checked = $('#<%=EfectivoCheckBox.ClientID %>').prop('checked');
                    var nroCheque = $('#<%=NumeroChequeTextBox.ClientID %>').val();
                    var bancoEmisor = $('#<%=BancoEmisorTextBox.ClientID %>').val();
                    if (!checked) {
                        if (nroCheque == '') {
                            $('#NumeroChequeError').show();
                            return false;
                        }
                        if (bancoEmisor == '') {
                            $('#BancoEmisorError').show();
                            return false;
                        }
                    }
                    $('#BancoEmisorError').hide();
                    $('#NumeroChequeError').hide();
                    return true;
                }

                function ShowNPDialog(id) {
                    $('#<%=EfectivoCheckBox.ClientID%>').prop('checked', '');
                    $('#<%=GastosEjecutadosDetalleIdHF.ClientID%>').val(id);
                    $('#<%=NewPagoPanel.ClientID%>').dialog({ modal: true, resizable: false });
                    $('.ui-widget-overlay').height($(document).height());
                    $('form').append($('.ui-dialog'));
                    return false;
                }
                function CloseNPDialog() {
                    $('#<%=NewPagoPanel.ClientID%>').dialog('destroy');
                }
                function OnColumnClick(sender, args) {
                    if (args.get_gridColumn().get_uniqueName().match('^TemplateColumn.*$') == null) {
                        var newOrderBy = args.get_gridColumn().get_uniqueName().replace('Decimal', '');
                        var splitOrderBy = $('#<%=OrderByHF.ClientID%>').val().split(' ');
                        var direction = splitOrderBy.length > 1 ? splitOrderBy[1] : '';

                        if (newOrderBy != splitOrderBy[0].trim()) {
                            direction = '';
                        }

                        if (direction == '') {
                            direction = 'DESC';
                        } else if (direction == 'DESC') {
                            direction = 'ASC';
                        } else if (direction == 'ASC') {
                            newOrderBy = '';
                            direction = '';
                        }
                        $('#<%=OrderByHF.ClientID%>').val(newOrderBy + ' ' + direction);
                        $('input.SearchButton').click();
                        $('.CSearchSearchToolsPanel').hide();
                        $('.CSearchSavedSearchPanel').hide();
                    }
                }
            </script>
        </div>
    </div>
</asp:Content>
