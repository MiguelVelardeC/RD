<%@ Page Title="PagoGastos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PreliquidacionList.aspx.cs"
    Inherits="SOAT_PreliquidacionList" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .SearchTitle {
            display: block;
            font-weight: bold;
            float: none!important;
        }

        .LeftBorder {
            border-right: 1px dashed #828282!important;
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
                
        .Atendida {
            background-color: #85E59D !important;
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
                <div class="buttonsPanel">
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
                    <search:SearchControl ID="SearchPreliquidacion" runat="server"
                        Title="Buscar"
                        DisplayHelp="true"
                        DisplayContextualHelp="false"
                        CssSearch="CSearch"
                        CssSearchHelp="CSearchHelpPanel"
                        CssSearchError="CSearchErrorPanel"
                        AdvancedSearchForm="~/UserControls/AdvancedSearch/PreliquidacionesAdvancedSearch.ascx"
                        SavedSearches="false"
                        SavedSearchesID="searchCtl_PreliquidacionList"
                        ImageHelpUrl="Images/Neutral/Help.png"
                        ImageErrorUrl="~/images/exclamation.png" />
                </div>
            </asp:Panel>

            <telerik:RadGrid ID="PreliquidacionRadGrid" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="false"
                AllowMultiRowSelection="False"
                Width="100%"
                OnItemDataBound="PreliquidacionRadGrid_ItemDataBound"
                OnItemCommand="PagoGastosRadGrid_ItemCommand"
                OnExcelExportCellFormatting="PagoGastosRadGrid_ExcelExportCellFormatting">
                <ClientSettings>
                    <ClientEvents OnColumnClick="OnColumnClick" />
                </ClientSettings>
                <ExportSettings ExportOnlyData="true" Excel-Format="Html" FileName="ListaPreliquidaciones" OpenInNewWindow="true"></ExportSettings>
                <MasterTableView DataKeyNames="PreliquidacionDetalleId" CommandItemDisplay="None">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowCancelChangesButton="false"
                        ShowExportToExcelButton="true" ShowRefreshButton="false" />
                    <NoRecordsTemplate>
                        <div style="text-align: center;">No hay Pagos de SOAT a mostrar.</div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="P.NumeroRoseta" DataField="NumeroRoseta"
                            HeaderText="Número de Roseta" />                        
                        <telerik:GridBoundColumn UniqueName="P.EstadoPagoForDisplay" DataField="EstadoPagoForDisplay"
                            HeaderText="Estado de Pago" />
                        <telerik:GridBoundColumn UniqueName="PRE.Tipo" DataField="TipoGasto"
                            HeaderText="Tipo de Gasto" />
                        <telerik:GridBoundColumn UniqueName="A.Nombre" DataField="Paciente"
                            HeaderText="Paciente" />
                        <telerik:GridBoundColumn UniqueName="PRE.Proveedor" DataField="Proveedor"
                            HeaderText="Proveedor" />
                        <telerik:GridBoundColumn UniqueName="PRE.Fecha" DataField="FechaAprobacion"
                            HeaderText="Fecha de Registro de Proforma" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="30px" />
                        <telerik:GridBoundColumn UniqueName="PRE.FechaReciboFactura" DataField="FechaEmision"
                            HeaderText="Fecha de Recepción" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="30px" />
                        <telerik:GridBoundColumn UniqueName="PRE.FechaPago" DataField="FechaPagoForDisplay"
                            HeaderText="Fecha de Pago" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="30px" />
                        <telerik:GridBoundColumn UniqueName="PRE.CantidadDias" DataField="CantidadDias" 
                            HeaderText="Cantidad de Dias" HeaderStyle-Width="30px" />
                        <telerik:GridBoundColumn UniqueName="IsFactura" DataField="IsFactura"
                            HeaderText="Tipo" />
                        <telerik:GridBoundColumn UniqueName="NumeroReciboFactura" DataField="NumeroReciboFactura"
                            HeaderText="Número de Factura / Recibo" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="DecimalMonto" DataField="Monto" DataType="System.Decimal"
                            HeaderText="Monto" DataFormatString="{0:#,##0.00}" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Right"/>
                        <telerik:GridBoundColumn UniqueName="PRE.Estado" DataField="Estado"
                            ItemStyle-CssClass="LeftBorder" FooterStyle-CssClass="LeftBorder" HeaderStyle-CssClass="LeftBorder"
                            HeaderText="Estado" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="GED.TieneFactura" DataField="TieneFactura"
                            HeaderText="Tiene Factura" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="GED.NumeroReciboFactura" DataField="NumeroFactura"
                            HeaderText="Factura" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="DecimalGED.Monto" DataField="MontoFactura" DataType="System.Decimal"
                            HeaderText="Monto" DataFormatString="{0:#,##0.00}" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Right" Visible="false" />
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
        </div>
    </div>
    <script type="text/javascript">
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
</asp:Content>
