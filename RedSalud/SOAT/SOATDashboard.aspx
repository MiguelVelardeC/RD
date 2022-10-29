<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SOATDashboard.aspx.cs" Inherits="SOAT_SOATDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/FusionCharts.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Tablero de Control" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:HyperLink ID="ListCoasosSoat" runat="server"
                    CssClass="addNew"
                    Text="Lista de Siniestros"
                    NavigateUrl="~/SOAT/SOATList.aspx" />
            </div>
        </div>
    </div>
    <div class="left" style="margin: 0 10px 10px 0;">
        <asp:Label Text="Cliente" runat="server" CssClass="label" />
        <asp:DropDownList ID="ClienteDDL" runat="server"
            DataSourceID="ClienteODS"
            CssClass="bigField"
            DataValueField="ClienteId"
            DataTextField="NombreJuridico"
            AutoPostBack="true"
            OnSelectedIndexChanged="ClienteDDL_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:ObjectDataSource ID="ClienteODS" runat="server"
            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
            OldValuesParameterFormatString="original_{0}"
            SelectMethod="getRedClienteForSOATList"
            OnSelected="ClienteODS_Selected"></asp:ObjectDataSource>
    </div>
    <div class="left">
        <span class="label">Gestión</span>
        <asp:DropDownList ID="GestionCombo" runat="server" CssClass="smallField"
            AutoPostBack="true">
        </asp:DropDownList>
    </div>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"
        MultiPageID="ReportesPages">
        <Tabs>
            <telerik:RadTab>
                <TabTemplate>
                    REPORTES POR CANTIDADES
                </TabTemplate>
            </telerik:RadTab>
            <telerik:RadTab>
                <TabTemplate>
                    REPORTE ECONOMICO
                </TabTemplate>
            </telerik:RadTab>
        </Tabs>

    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="ReportesPages" runat="server" SelectedIndex="0">
        <telerik:RadPageView runat="server" ID="ReportesPagesCantidades" Selected="true">

            <div class="twoColumnLeft">
                <div class="twoColsLeft">
                    <div class="columnHead">
                        <asp:Label Text="Totales" runat="server" CssClass="title" />
                    </div>

                    <!-- This is where the chart is displayed -->
                    <asp:Panel ID="ChartPanelSOATTotales" runat="server" CssClass="smallSOATChart">
                    </asp:Panel>
                    <asp:Literal ID="FusionChartScriptSOATTotales" runat="server"></asp:Literal>
                </div>

                <div class="twoColsRight">
                    <!-- Siniestros pagados x departamento -->
                    <div class="columnHead">
                        <asp:Label ID="Label14" Text="Cantidad accidentados/fallecidos" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="CantidadAccidentadosxMesGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="mes" HeaderText="Mes"
                                    DataField="nombreMes">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Accidentados" Aggregate="Sum" FooterAggregateFormatString="{0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Nro. Accidentados"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="accidentados">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Fallecidos" Aggregate="Sum" FooterAggregateFormatString="{0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Nro. Fallecidos"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="fallecidos">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="oneColumn">

                    <!-- Cantidad siniestros x sector x mes -->
                    <div class="columnHead">
                        <asp:Label ID="Label15" Text="Cantidad siniestros x sector x mes" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="CantidadesXSectorXMesGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Mes" HeaderText="Mes"
                                    DataField="concepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="OFICIAL" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Oficial"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="OFICIAL">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PARTICULAR" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Particular"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="PARTICULAR">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PÚBLICO" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Público"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="PÚBLICO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTALES" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Total"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="TOTALES">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>

                    <!-- Cantidad siniestros x sector x mes -->
                    <div class="columnHead">
                        <asp:Label ID="Label16" Text="Cantidad siniestros x sector x tipo" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="CantidadesXSectorXTipoGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="TIPO" HeaderText="Tipo Vehículo"
                                    DataField="concepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="OFICIAL" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Oficial"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="OFICIAL">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PARTICULAR" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Particular"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="PARTICULAR">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PÚBLICO" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Público"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="PÚBLICO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTALES" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Total"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="TOTALES">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>

                    <!-- Cantidad siniestros x sector x departamento -->
                    <div class="columnHead">
                        <asp:Label ID="Label17" Text="Cantidad siniestros x sector x departamento" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="CantidadesXSectorXDptoGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="DPTO" HeaderText="Departamento"
                                    DataField="concepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="OFICIAL" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Oficial"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="OFICIAL">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PARTICULAR" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Particular"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="PARTICULAR">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PÚBLICO" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Público"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="PÚBLICO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTALES" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Total"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="TOTALES">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>

                </div>


            </div>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="ReportesPagesEconomico">
            <div class="twoColumnLeft">
                <div class="twoColsLeft">
                    <!-- Siniestros pagados x departamento -->
                    <div class="columnHead">
                        <asp:Label ID="Label9" Text="Siniestros pagados x departamento" runat="server" CssClass="title" />
                    </div>
                    <telerik:RadGrid ID="SIniestroPagadoXDptoGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false">
                        <MasterTableView CssClass="PorcentageContainer">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Departamento" HeaderText="Departamento"
                                    DataField="LugarDpto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Accidentados" Aggregate="Sum" FooterAggregateFormatString="{0:#,##0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Nro. Accidentados"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="accidentados">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Monto" Aggregate="Sum"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Monto Pagado"
                                    DataFormatString="{0:###,##0.00}"
                                    ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-CssClass="PorMonto"
                                    FooterStyle-CssClass="PorTotal"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="monto">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn
                                    HeaderText="Porcentage"
                                    FooterText="100,00 %"
                                    FooterStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-CssClass="PorResultado">
                                </telerik:GridTemplateColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            makePorcentages();
                        });
                        function makePorcentages() {
                            $('.PorcentageContainer').each(function () {
                                var Total = parseFloat($(this).find('td.PorTotal').html().replace(/\./g, '').replace(',', '.'));
                                var Porcentage = 0;
                                $(this).find('tr').each(function () {
                                    if ($(this).children('td.PorMonto').length > 0) {
                                        if (Total > 0) {
                                            var monto = parseFloat($(this).children('td.PorMonto').html().replace(/\./g, '').replace(',', '.'));
                                            var _porcentage = (monto * 100) / Total;
                                            Porcentage += _porcentage;
                                            $(this).children('td.PorResultado').html((_porcentage).toFixed(2).replace('.', ',') + ' %');
                                        } else {
                                            $(this).children('td.PorResultado').html('0 %');
                                        }
                                    }
                                });
                                $(this).find('td.PorTotal').next().html(Porcentage > 100 ? '100 %' : Porcentage.toFixed(2) + ' %');
                            });
                            $('.PorcentageGroupContainer').each(function () {
                                var list = new Array();
                                var Totales = new Array();
                                var start = true;
                                $(this).find('tbody tr').each(function (index) {
                                    var td = $(this).find('td').get(1);
                                    if ($(td).length <= 0) return;
                                    if (start) {
                                        if ($(td).attr('colspan') > 0) {
                                            list.push(index);
                                            start = false;
                                        }
                                    } else {
                                        if ($(td).html().replace('&nbsp;', '').trim() == '') {
                                            var monto = $(this).children('td.PorMonto');
                                            monto.addClass('PorTotal');
                                            monto.removeClass('PorMonto');
                                            Totales.push($(monto).html().replace(/\./g, '').replace(',', '.'));
                                            $(this).children('td.PorResultado').removeClass('PorResultado');
                                            list.push(index);
                                            start = true;
                                        }
                                    }
                                });
                                list.reverse();
                                Totales.reverse();
                                while (list.length > 0) {
                                    var startIndex = list.pop();
                                    var endIndex = list.pop() + 1;
                                    var Total = Totales.pop();
                                    var Porcentage = 0.0;
                                    $(this).find('tbody tr').slice(startIndex, endIndex).each(function () {
                                        if ($(this).children('td.PorMonto').length > 0) {
                                            var porcentage = 0;
                                            if (Total > 0) {
                                                var monto = parseFloat($(this).children('td.PorMonto').html().replace(/\./g, '').replace(',', '.'));
                                                porcentage = ((monto * 100) / Total);
                                            }
                                            Porcentage += porcentage;
                                            $(this).children('td.PorResultado').html(porcentage.toFixed(2).replace('.', ',') + ' %');
                                        } else if ($(this).children('td.PorTotal').length > 0) {
                                            $(this).children('td.PorTotal').next().html((Porcentage > 100 ? 100 : Porcentage).toFixed(2) + '  %');
                                        }
                                    });
                                    var total = $(this).find('tfoot tr td.PorTotal').next();
                                    if ($(total).length > 0) {
                                        $(total).html((Porcentage > 100 ? 100 : Porcentage).toFixed(2) + '  %');
                                    }
                                }
                            });
                        }
                    </script>
                </div>

                <div class="twoColsRight">
                    <!-- Siniestros pagados x sector -->
                    <div class="columnHead">
                        <asp:Label ID="Label10" Text="Siniestros pagados x sector" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="SiniestroPagadoXSectorGrid" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        ShowGroupPanel="false"
                        MasterTableView-ShowGroupFooter="true"
                        AllowSorting="false">
                        <MasterTableView GroupsDefaultExpanded="false" ShowGroupFooter="true" GroupLoadMode="Client"
                            CssClass="PorcentageGroupContainer">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Sector" FieldName="Sector"></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="Sector"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Sector" Visible="false"
                                    DataField="Sector">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Dpto" HeaderText="Departamento"
                                    ItemStyle-CssClass="PorGroup"
                                    DataField="LugarDpto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Accidentados" Aggregate="Sum" FooterAggregateFormatString="{0}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Nro. Accidentados"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="accidentados">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Monto" Aggregate="Sum"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    FooterStyle-CssClass="PorTotal"
                                    HeaderText="Monto Pagado"
                                    DataFormatString="{0:###,##0.00}"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-CssClass="PorMonto"
                                    DataField="monto">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn
                                    HeaderText="Porcentage"
                                    FooterStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-CssClass="PorResultado">
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings RetainGroupFootersVisibility="true" />
                    </telerik:RadGrid>
                </div>

                <div class="oneColumn">
                    <!-- Siniestros pagados x departamento -->
                    <div class="columnHead" style="margin-top: 20px;">
                        <asp:Label ID="Label11" Text="Siniestros pagados x tipo x mes" runat="server" CssClass="title" />
                    </div>
                    <telerik:RadGrid ID="SiniestroPAgadoXTipoXMesGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false"
                        OnItemDataBound="SumaTotales_ItemDataBound">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Mes" HeaderText="Mes"
                                    DataField="nombreMes">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CIRUGIA" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Cirugía Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="CIRUGIA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="OSTEOSINTESIS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Material de Osteosíntesis Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="OSTEOSINTESIS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AMBULATORIO" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Ambulatorios Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="AMBULATORIO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HONORARIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Honorarios Profesionales Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="HONORARIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FARMACIAS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Farmacias Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="FARMACIAS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LABORATORIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Laboratorios e imágenes Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="LABORATORIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HOSPITALARIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Hospitalarios Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="HOSPITALARIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AMBULANCIAS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Ambulancia Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="AMBULANCIAS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="REEMBOLSO" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Reembolso Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="REEMBOLSO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTALES"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Totales Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                    <!-- Siniestros pagados x departamento -->
                    <div class="columnHead">
                        <asp:Label ID="Label13" Text="Siniestros pagados x tipo x sector" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="SiniestroPagadoXTipoXSectorGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false"
                        OnItemDataBound="SumaTotales_ItemDataBound">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="concepto" HeaderText="Sector"
                                    DataField="concepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CIRUGIA" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Cirugía Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="CIRUGIA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="OSTEOSINTESIS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Material de Osteosíntesis Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="OSTEOSINTESIS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AMBULATORIO" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Ambulatorios Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="AMBULATORIO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HONORARIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Honorarios Profesionales Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="HONORARIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FARMACIAS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Farmacias Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="FARMACIAS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LABORATORIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Laboratorios e imágenes Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="LABORATORIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HOSPITALARIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Hospitalarios Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="HOSPITALARIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AMBULANCIAS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Ambulancia Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="AMBULANCIAS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="REEMBOLSO" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Reembolso Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="REEMBOLSO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTALES"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Totales Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>

                    <!-- Siniestros pagados x departamento -->
                    <div class="columnHead">
                        <asp:Label ID="Label12" Text="Siniestros pagados x tipo x vehículo" runat="server" CssClass="title" />
                    </div>

                    <telerik:RadGrid ID="SiniestroPAgadoXTipoXVehiculoGrid" runat="server" AutoGenerateColumns="false"
                        ShowFooter="true"
                        AllowSorting="false"
                        OnItemDataBound="SumaTotales_ItemDataBound">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="concepto" HeaderText="Tipo Vehículo"
                                    DataField="concepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CIRUGIA" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Cirugía Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="CIRUGIA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="OSTEOSINTESIS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Material de Osteosíntesis Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="OSTEOSINTESIS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AMBULATORIO" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Ambulatorios Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="AMBULATORIO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HONORARIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Honorarios Profesionales Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="HONORARIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FARMACIAS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Farmacias Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="FARMACIAS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LABORATORIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Laboratorios e imágenes Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="LABORATORIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HOSPITALARIOS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Hospitalarios Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="HOSPITALARIOS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AMBULANCIAS" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Ambulancia Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="AMBULANCIAS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="REEMBOLSO" Aggregate="Sum"
                                    DataFormatString="{0:###,##0.00}"
                                    FooterAggregateFormatString="{0:###,##0.00}"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Reembolso Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    DataField="REEMBOLSO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTALES"
                                    FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Totales Bs"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <div id="MayorSiniestralidadDiv" runat="server" class="oneColumnRight" style="margin-left: 5px; width: 27%;">
        <div class="columnHead">
            <asp:Label ID="Label4" Text="Casos de Mayor Siniestralidad" runat="server" CssClass="title" />
        </div>

        <telerik:RadGrid ID="MayorSiniestralidadRadGrid" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="false"
            AllowMultiRowSelection="False"
            OnItemCommand="MayorSiniestralidadRadGrid_ItemCommand">
            <MasterTableView DataKeyNames="SiniestroId" ExpandCollapseColumn-Display="false">
                <NoRecordsTemplate>
                    <div style="text-align: center;">No hay siniestros a mostrar.</div>
                </NoRecordsTemplate>
                <AlternatingItemStyle BackColor="#E8F1FF" />
                <ItemStyle BackColor="#C9DFFC" />
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="EditGTC" HeaderTooltip="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="DetailsImageButton" runat="server"
                                ImageUrl="~/Images/Neutral/select2.png"
                                OnCommand="DetailsImageButton_Command"
                                CommandArgument='<%# Eval("siniestroId") + "," + Eval("accidentadoId") %>'
                                Width="24px" CommandName="Select"
                                ToolTip="Editar"></asp:ImageButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn UniqueName="siniestroId" DataField="SiniestroId"
                        HeaderText="Identificador del Siniestro" Visible="false" />
                    <telerik:GridBoundColumn UniqueName="accidentadoId" DataField="accidentadoId"
                        HeaderText="Identificador del Accidentado" Visible="false" />
                    <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre"
                        HeaderText="Accidentado" />
                    <telerik:GridBoundColumn UniqueName="CIAccidentado" DataField="CarnetIdentidad"
                        HeaderText="CI" />
                    <telerik:GridBoundColumn UniqueName="Siniestralidad" DataField="siniestralidad"
                        HeaderText="%" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

</asp:Content>

