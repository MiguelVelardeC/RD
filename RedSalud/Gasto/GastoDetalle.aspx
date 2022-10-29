<%@ Page Title="Administración de Gastos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GastoDetalle.aspx.cs" Inherits="Gastos_GastoDetalle" %>
<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function getTableName(children) {
            return $(children).parents('.PDFExportRadGrid').attr('class').replace('RadGrid', '')
                                                                        .replace('RadGrid_Default', '')
                                                                        .replace('PDFExportRadGrid', '').trim();
        }
        $(document).ready(function () {
            $('.ExportAllCheckBox input').click(function () {
                var table = getTableName(this);
                var checked = ($(this).attr('checked') == 'checked' || $(this).attr('checked') == true);
                $('.' + table + ' input[type="checkbox"]').attr('checked', checked);
            });
            $('.rgExpPDF').click(function () {
                var table = getTableName(this);
                var ids = [];
                $('.' + table + ' input[type="checkbox"]').each(function () {
                    if ($(this).attr('checked') == 'checked' || $(this).attr('checked') == true) {
                        var id = $(this).parent().next().val();
                        if (id != null && id != undefined && id != '') {
                            ids.push(id);
                        }
                    }
                });
                $('#<%= ExportIDHF.ClientID %>').val(ids.join(','));
                var valid = $('#<%= ExportIDHF.ClientID %>').val() != '';
                if (!valid) {
                    alert('No ha seleccionado ninguna fila para exportar.');
                }
                return valid;
            });
            $('.ExportCheckBox input[type="checkbox"]').change(function () {
                var table = getTableName(this);
                var check = ($('.' + table + ' .ExportCheckBox input[type="checkbox"]:checked').length ==
                    $('.' + table + ' .ExportCheckBox input[type="checkbox"]').length)
                $('.' + table + ' .ExportAllCheckBox input[type="checkbox"]').attr('checked', check);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label15" CssClass="title" Text="Administración de Gastos" runat="server" />
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:HyperLink runat="server" ID="returnHL"
                        Text="Volver a la lista de casos medicos"
                        NavigateUrl="~/CasoMedico/CasoMedicoLista.aspx" />

                    <asp:LinkButton ID="BlockUnlockLB" runat="server" 
                        OnClick="BlockUnlockLB_Click"
                        CssClass="linkBorderLeft" />
                    <asp:Label ID="BlockUnlockLabel" runat="server"
                        Text="Bloqueado" CssClass="linkBorderLeft" />

                    <asp:LinkButton ID="AdminCasoLB" runat="server" 
                        Text="Administrar Caso Medico"
                        OnClick="AdminCasoLB_Click"
                        CssClass="linkBorderLeft"/>
                </div>

                <asp:Panel runat="server" GroupingText="Detalle del Caso">
                    <div>
                        <table>
                            <tr>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label1" Text="Codigo Caso:" runat="server" CssClass="inlineLabel" /></td>
                                <td style="padding-right: 15px;">
                                    <asp:Label ID="CodigoCasoLabel" Text="" runat="server" /></td>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label3" Text="Codigo asegurado:" runat="server" CssClass="inlineLabel" /></td>
                                <td style="padding-right: 15px;">
                                    <asp:Label ID="CodigoAseguradoLabel" Text="" runat="server" /></td>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label2" Text="Cliente:" runat="server" CssClass="inlineLabel" /></td>
                                <td style="padding-right: 15px;">
                                    <asp:Label ID="AseguradoraLabel" Text="" runat="server" /></td>
                            </tr>
                            <tr style="margin-top: 5px;">
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label4" Text="Asegurado:" runat="server" CssClass="inlineLabel" /></td>
                                <td style="padding-right: 15px;">
                                    <asp:Label ID="AseguradoLabel" Text="" runat="server" /></td>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label5" Text="Motivo de la consulta:" runat="server" CssClass="inlineLabel" /></td>
                                <td style="padding-right: 15px;">
                                    <asp:Label ID="MotivoConsultaLabel" Text="" runat="server" /></td>
                                <td style="padding-right: 5px;">
                                    <asp:Label ID="Label13" Text="Fecha:" runat="server" CssClass="inlineLabel" /></td>
                                <td style="padding-right: 15px;">
                                    <asp:Label ID="fechaLabel" Text="" runat="server" /></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <br />

                <telerik:RadTabStrip ID="CasoTab" runat="server" MultiPageID="GastoMP" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab Text="Odontología" id="OdontologiaRT" runat="server"></telerik:RadTab>
                        <telerik:RadTab Text="Emergencia" id="EmergenciaRT" runat="server"></telerik:RadTab>
                        <telerik:RadTab Text="Receta Medica" id="RecetaRT" runat="server"></telerik:RadTab>
                        <telerik:RadTab Text="Exámenes Complementarios" id="EstudioRT" runat="server"></telerik:RadTab>
                        <telerik:RadTab Text="Especialista" id="DerivacionRT" runat="server"></telerik:RadTab>
                        <telerik:RadTab Text="Internación" id="InternacionRT" runat="server"></telerik:RadTab>
                        <telerik:RadTab Text="Enfermeria" id="EnfermeriaRT" runat="server"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="GastoMP" runat="server"
                    CssClass="RadMultiPage">
                    <telerik:RadPageView ID="OdontologiaRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="OdontologiaRadGrid" runat="server"
                                CssClass="OdontologiaRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="OdontologiaODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="OdontologiaRadGrid_ItemCommand"
                                OnItemCreated="OdontologiaRadGrid_ItemCreated"
                                OnItemDataBound="OdontologiaRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="true">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm" 
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoOdontologia" AutoGenerateColumns="False" DataKeyNames="GastoId,OdontologiaId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                            OnClick="ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgRecetaNullLabel" runat="server"
                                            Text="No existen prestaciones odontologicas para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleOdontologia"
                                            DataKeyNames="GastoDetalleId">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="Label14" Text="No existen Detalles de gasto para esta Prestación" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />
                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("OdontologiaId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Estudio--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Odontologia"
                                                    TipoId='<%# Eval("OdontologiaId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="OdontologiaId" HeaderText="OdontologiaId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="PrestacionOdontologica" HeaderText="PrestacionOdontologica" />
                                        <telerik:GridBoundColumn DataField="Pieza" HeaderText="Pieza" />
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="ODONTOLOGIA"
                                                        CommandArgument='<%# Eval("OdontologiaId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label ID="Label17" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="OdontologiaODS" runat="server"
                                TypeName="Artexacta.App.Odontologia.BLL.OdontologiaBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="GetOdontologiaByCasoId"
                                OnSelected="OdontologiaODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="EmergenciaRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="EmergenciaRadGrid" runat="server"
                                CssClass="EmergenciaRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="EmergenciaODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="EmergenciaRadGrid_ItemCommand"
                                OnItemCreated="EmergenciaRadGrid_ItemCreated"
                                OnItemDataBound="EmergenciaRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm"
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoEmergencia" AutoGenerateColumns="False" DataKeyNames="GastoId,EmergenciaId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                            OnClick="EmergenciaRadGrid_ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgEmergenciaNullLabel" runat="server"
                                            Text="No existen Emergencias para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleEmergencia"
                                            DataKeyNames="GastoDetalleId">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="Label11" Text="No existen Detalles de gasto para esta Emergencia" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    CommandArgument="GastoDetalleId"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />

                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("EmergenciaId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Emergencia--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Emergencia"
                                                    TipoId='<%# Eval("EmergenciaId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    NombreProveedor='<%# Eval("NombreProveedor") %>'
                                                    NIT='<%# Eval("NITProveedor") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="EmergenciaId" HeaderText="EmergenciaId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="EMERGENCIA"
                                                        CommandArgument='<%# Eval("EmergenciaId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label ID="Label18" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="EmergenciaODS" runat="server"
                                TypeName="Artexacta.App.Emergencia.BLL.EmergenciaBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="getEmergenciaListByCasoId"
                                OnSelected="EmergenciaODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RecetaRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="RecetaRadGrid" runat="server"
                                CssClass="RecetaRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="RecetaODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="RecetaRadGrid_ItemCommand"
                                OnItemCreated="RecetaRadGrid_ItemCreated"
                                OnItemDataBound="RecetaRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="true">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm" 
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoReceta" AutoGenerateColumns="False" DataKeyNames="GastoId,DetalleId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                            OnClick="RecetaRadGrid_ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgRecetaNullLabel" runat="server"
                                            Text="No existen Recetas medicas para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleReceta"
                                            DataKeyNames="GastoDetalleId">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="Label14" Text="No existen Detalles de gasto para esta Receta" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />
                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("DetalleId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Estudio--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Receta"
                                                    TipoId='<%# Eval("DetalleId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="EstudioId" HeaderText="EstudioId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="Medicamento" HeaderText="Medicamento" />
                                        <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="Presentación" />
                                        <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="RECETAS"
                                                        CommandArgument='<%# Eval("DetalleId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="RecetaODS" runat="server"
                                TypeName="Artexacta.App.Receta.BLL.RecetaBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="GetAllRecetaByCasoId"
                                OnSelected="RecetaODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="EstudioRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="EstudioRadGrid" runat="server"
                                CssClass="EstudioRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="EstudioODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="EstudioRadGrid_ItemCommand"
                                OnItemCreated="EstudioRadGrid_ItemCreated"
                                OnItemDataBound="EstudioRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm"
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoEstudio" AutoGenerateColumns="False" DataKeyNames="GastoId,EstudioId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                            OnClick="EstudioRadGrid_ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgEstudioNullLabel" runat="server"
                                            Text="No existen examenes complementarios para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleEstudio"
                                            DataKeyNames="GastoDetalleId">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="Label14" Text="No existen Detalles de gasto para este Examen Complementario" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />
                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("EstudioId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Estudio--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Estudio"
                                                    TipoId='<%# Eval("EstudioId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    NombreProveedor='<%# Eval("NombreProveedor") %>'
                                                    NIT='<%# Eval("NITProveedor") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="EstudioId" HeaderText="EstudioId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="NombreTipoEstudio" HeaderText="Tipo Examen Complementario" />
                                        <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="ESTUDIO"
                                                        CommandArgument='<%# Eval("EstudioId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="EstudioODS" runat="server"
                                TypeName="Artexacta.App.Estudio.BLL.EstudioBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="getEstudioListByCasoId"
                                OnSelected="EstudioODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="DerivacionRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="DerivacionRadGrid" runat="server"
                                CssClass="DerivacionRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="DerivacionODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="DerivacionRadGrid_ItemCommand"
                                OnItemCreated="DerivacionRadGrid_ItemCreated"
                                OnItemDataBound="DerivacionRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm"
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoDerivacion" AutoGenerateColumns="False" DataKeyNames="GastoId,DerivacionId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir a PDF" runat="server"
                                            OnClick="DerivacionRadGrid_ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgDerivacionNullLabel" runat="server"
                                            Text="No existen Derivaciones para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleDerivacion"
                                            DataKeyNames="GastoDetalleId">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label Text="No existen Detalles de gasto para esta Derivación" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />

                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("DerivacionId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Derivacion--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Derivacion"
                                                    TipoId='<%# Eval("DerivacionId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    NombreProveedor='<%# Eval("NombreProveedor") %>'
                                                    NIT='<%# Eval("NITProveedor") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="DerivacionId" HeaderText="DerivacionId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="DERIVACION"
                                                        CommandArgument='<%# Eval("DerivacionId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="DerivacionODS" runat="server"
                                TypeName="Artexacta.App.Derivacion.BLL.DerivacionBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="getDerivacionListByCasoId"
                                OnSelected="DerivacionODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="InternacionRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="InternacionRadGrid" runat="server"
                                CssClass="InternacionRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="InternacionODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="InternacionRadGrid_ItemCommand"
                                OnItemCreated="InternacionRadGrid_ItemCreated"
                                OnItemDataBound="InternacionRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="false" HideStructureColumns="false">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm"
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoInternacion" AutoGenerateColumns="False" DataKeyNames="GastoId,InternacionId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                            OnClick="InternacionRadGrid_ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgInternacionNullLabel" runat="server"
                                            Text="No existen Internaciones para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleInternacion"
                                            DataKeyNames="GastoDetalleId">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="Label12" Text="No existen Detalles de gasto para esta Internación" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />
                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("InternacionId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Internacion--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Internacion"
                                                    TipoId='<%# Eval("InternacionId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    NombreProveedor='<%# Eval("NombreProveedor") %>'
                                                    NIT='<%# Eval("NITProveedor") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="InternacionId" HeaderText="InternacionId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Proveedor" />
                                        <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="INTERNACION"
                                                        CommandArgument='<%# Eval("InternacionId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label ID="Label16" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="InternacionODS" runat="server"
                                TypeName="Artexacta.App.Internacion.BLL.InternacionBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="getInternacionListByCasoId"
                                OnSelected="InternacionODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="MedicamentoRPV" runat="server">
                        <div>
                            <telerik:RadGrid ID="MedicamentoRadGrid" runat="server"
                                CssClass="MedicamentoRadGrid PDFExportRadGrid"
                                AutoGenerateColumns="false"
                                DataSourceID="MedicamentoODS"
                                PageSize="20"
                                MasterTableView-HierarchyLoadMode="Client"
                                OnItemCommand="MedicamentoRadGrid_ItemCommand"
                                OnItemCreated="MedicamentoRadGrid_ItemCreated"
                                OnItemDataBound="MedicamentoRadGrid_ItemDataBound">
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="true">
                                    <Pdf DefaultFontFamily="Arial Unicode MS" PageWidth="279mm" PageHeight="216mm" 
                                        PageTopMargin="10mm" PageLeftMargin="10mm" PageRightMargin="10mm" PageBottomMargin="10mm"
                                        AllowModify="false" AllowAdd="false">
                                    </Pdf>
                                </ExportSettings>
                                <MasterTableView Name="GastoMedicamento" AutoGenerateColumns="False" DataKeyNames="GastoId,MedicamentoId" CommandItemDisplay="Top">
                                    <CommandItemStyle HorizontalAlign="Left" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                                        ShowExportToPdfButton="true" />
                                    <CommandItemTemplate>
                                        <asp:Panel ID="HeaderPanel" runat="server" CssClass="left" style="display: none;">
                                            <div>
                                                <strong><asp:Label ID="MedicoLabel" Text="Medico:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="MedicoNombreLabel" runat="server" />
                                            </div>
                                            <div>
                                                <strong><asp:Label ID="EspecialidadLabel" Text="Especialidad:&nbsp;" runat="server" /></strong>
                                                <asp:Label ID="EspecialidadNameLabel" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ExportPanel" runat="server" CssClass="right" style="padding: 10px;">
                                        <asp:ImageButton ID="ExportToPdfButton" CssClass="rgExpPDF" ImageUrl="../Images/Neutral/ExportPrint.gif" ToolTip="Imprimir" runat="server"
                                            OnClick="MedicamentoRadGrid_ExportToPdfButton_Click" />
                                        </asp:Panel>
                                    </CommandItemTemplate>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="MsgMedicamentoNullLabel" runat="server"
                                            Text="No existen Medicamentos para este caso del paciente." />
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView runat="server"
                                            AutoGenerateColumns="false"
                                            DataSourceID="GastoDetalleODS"
                                            PageSize="20"
                                            Name="GastoDetalleMedicamento"
                                            DataKeyNames="GastoDetalleID">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="GastoId" MasterKeyField="GastoId" />
                                            </ParentTableRelation>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="Label14" Text="No existen Detalles de gasto para estos Medicamentos de Enfermeria" runat="server" />
                                            </NoRecordsTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn UniqueName="DeleteCommandColumn"
                                                    HeaderText="Eliminar"
                                                    CommandName="Eliminar"
                                                    ButtonType="ImageButton"
                                                    ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/Images/neutral/delete.png"
                                                    ConfirmText="¿Está seguro que desea eliminar el detalle del gasto?" />
                                                <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoForDisplay" HeaderText="Monto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NroFacturaRecibo" HeaderText="Nro. de la factura o recibo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha creación">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaGasto" HeaderText="Fecha de la factura o recibo" DataFormatString="{0:d}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FileId" HeaderText="File" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBox" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="83px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ExportAllCheckBox" CssClass="ExportAllCheckBox" Text="Imprimir" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ExportCheckBox" CssClass="ExportCheckBox" runat="server" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# "{" + Eval("GastoId") + ";" + Eval("MedicamentoId") + "}" %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="AgregarGastos" HeaderText="Agregar Gastos">
                                            <ItemTemplate>
                                                <%--al agregar un gasto por primera vez debe hacer insert a gasto, gasto detalle y update a tbl_Estudio--%>
                                                <asp:ImageButton ID="AddImageButton" runat="server"
                                                    CssClass="AddGasto"
                                                    ImageUrl="~/Images/Neutral/new.png"
                                                    Width="20px"
                                                    TipoNombre="Medicamento"
                                                    TipoId='<%# Eval("MedicamentoId") %>'
                                                    GastoID='<%# Eval("GastoId") %>'
                                                    ToolTip="Agregar Gasto"></asp:ImageButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="EstudioId" HeaderText="EstudioId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GastoId" HeaderText="GastoId" Visible="false" />
                                        <telerik:GridBoundColumn DataField="MedicamentoNombre" HeaderText="Medicamento" />
                                        <telerik:GridBoundColumn DataField="TipoMedicamentoNombre" HeaderText="Presentación" />
                                        <telerik:GridBoundColumn DataField="Indicaciones" HeaderText="Indicaciones" />

                                        <telerik:GridBoundColumn DataField="MontoConFacturaForDisplay" HeaderText="Monto con factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="MontoSinFacturaForDisplay" HeaderText="Monto sin factura" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="RetencionImpuestoForDisplay" HeaderText="Retencion de Impuesto" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="TotalForDisplay" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridTemplateColumn UniqueName="FileManager" HeaderText="Adjuntos"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                            <ItemTemplate>
                                                <div class="AdjuntosCont">
                                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                        CommandName="MEDICAMENTO"
                                                        CommandArgument='<%# Eval("MedicamentoId") %>'
                                                        OnCommand="FileManager_Command" />
                                                    <asp:Label CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            <asp:ObjectDataSource ID="MedicamentoODS" runat="server"
                                TypeName="Artexacta.App.Medicamento.BLL.MedicamentoBLL"
                                OldValuesParameterFormatString="{0}"
                                SelectMethod="GetAllMedicamentoByCasoId"
                                OnSelected="MedicamentoODS_Selected">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CasoIdHF" Name="CasoId" PropertyName="Value" DbType="Int32" />
                                    <asp:Parameter Name="isFileVisible" Type="Boolean" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>

                <asp:ObjectDataSource ID="GastoDetalleODS" runat="server"
                    TypeName="Artexacta.App.GastoDetalle.BLL.GastoDetalleBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetGastoDetalleList"
                    OnSelected="GastoDetalleODS_Selected">
                    <SelectParameters>
                        <asp:SessionParameter Name="GastoId" SessionField="GastoId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:Panel ID="GastoCPanel" runat="server"
                    CssClass="Default_Popup" GroupingText="Nuevo Gasto"
                    DefaultButton="SaveGastoExC">
                    <div>
                        <%--no va a tener editar detalle, que tenga eliminar e insert--%>
                        <asp:HiddenField ID="TipoNombreHF" runat="server" />
                        <asp:HiddenField ID="TipoIdHF" runat="server" />
                        <asp:HiddenField ID="GastoIdHF" runat="server" />

                        <asp:HiddenField ID="ExportIDHF" runat="server" />
                        <asp:HiddenField ID="MedicoNameHF" runat="server" />
                        <asp:HiddenField ID="EspecialidadHF" runat="server" />

                        <div class="ProveedorNitDetails">
                            <span class="label">Proveedor:</span>
                            <asp:Label ID="ProveedorLabel" Text="" runat="server" />
                            <span class="label">NIT:</span>
                            <asp:Label ID="NITLabel" Text="" runat="server" />
                        </div>

                        <asp:Label ID="Label6" Text="Fecha de la factura o recibo" runat="server" CssClass="label" />
                        <telerik:RadDatePicker ID="FechaGastoExC" runat="server"
                            EnableTyping="true"
                            ShowPopupOnFocus="true"
                            EnableShadows="true">
                        </telerik:RadDatePicker>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="FechaGastoExCRFV" runat="server"
                                ControlToValidate="FechaGastoExC"
                                ErrorMessage="debe seleccionar la fecha de la factura o recibo."
                                ValidationGroup="GastoExC"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <asp:Label ID="Label7" Text="Monto" runat="server" CssClass="label" />
                        <%--<asp:TextBox ID="MontoExCTxt" runat="server" CssClass="normalField" />--%>
                        <telerik:RadNumericTextBox ID="MontoRN" runat="server"
                            CssClass="normalField"
                            NumberFormat-DecimalDigits="2">
                        </telerik:RadNumericTextBox>
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                Display="Dynamic"
                                ControlToValidate="MontoRN"
                                ValidationGroup="GastoExC"
                                ErrorMessage="El monto es requerido." />
                        </div>

                        <asp:Label ID="Label8" Text="Número de la factura o recibo" runat="server" CssClass="label" />
                        <asp:TextBox ID="NroFacturaReciboExCTxt" runat="server" CssClass="normalField" />
                        <div class="validation">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                Display="Dynamic"
                                ControlToValidate="NroFacturaReciboExCTxt"
                                ValidationGroup="GastoExC"
                                ErrorMessage="El número de la factura o recibo es requerida." />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                Display="Dynamic"
                                ControlToValidate="NroFacturaReciboExCTxt"
                                ValidationGroup="GastoExC"
                                ErrorMessage="El número de la factura o recibo solo permite números."
                                ValidationExpression="<%$ Resources: Validations, IntegerFormat  %>" />
                        </div>

                        <asp:Label ID="Label9" Text="Tipo de documento" runat="server" CssClass="label" />
                        <telerik:RadComboBox ID="TipoDocumentoExCDDL" runat="server"
                            EmptyMessage="Seleccione un tipo de medicamento"
                            CssClass="bigField"
                            MarkFirstMatch="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="Factura" Text="Factura" />
                                <telerik:RadComboBoxItem Value="Recibo" Text="Recibo" />
                            </Items>
                        </telerik:RadComboBox>
                        <div class="validation">
                            <asp:CustomValidator ID="TipoDocumentoExCCV" runat="server"
                                ValidationGroup="GastoExC"
                                ErrorMessage="Debe seleccionar un tipo de docummento."
                                ClientValidationFunction="TipoDocumentoCV_Validate"
                                Display="Dynamic" />
                        </div>

                        <div class="buttonsPanel">
                            <asp:LinkButton ID="SaveGastoExC" Text="" runat="server"
                                CssClass="button"
                                ValidationGroup="GastoExC"
                                OnClick="SaveGastoExC_Click">
                                <asp:Label ID="Label10" Text="Guardar" runat="server" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="CancelRGastoExCLB" Text="Cancelar" runat="server" />
                        </div>
                    </div>
                </asp:Panel>

                <asp:HiddenField ID="CasoIdHF" runat="server" />
            </div>
            
            <RedSalud:FileManager runat="server" ID="FileManager" />
            <script type="text/javascript">
                $(document).ready(function () {
                    createPopup("#<%=GastoCPanel.ClientID %>", ".AddGasto", "#<%=CancelRGastoExCLB.ClientID %>");
                });
                $(".AddGasto").click(function (e) {
                    e.preventDefault();
                    var TipoNombre = $(this).attr("TipoNombre");
                    var TipoId = $(this).attr("TipoId");
                    var GastoId = $(this).attr("GastoID");
                    var NombreProveedor = $(this).attr("NombreProveedor");
                    var NIT = $(this).attr("NIT");

                    $("#<%=TipoNombreHF.ClientID %>").attr('value', TipoNombre);
                    $("#<%=TipoIdHF.ClientID %>").attr('value', TipoId);
                    $("#<%=GastoIdHF.ClientID %>").attr('value', GastoId);
                    
                    if (TipoNombre == 'Receta' || TipoNombre == 'Medicamento')
                        $(".ProveedorNitDetails").attr('style', 'display:none');
                    else
                    {
                        $(".ProveedorNitDetails").attr('style', 'display:block');
                        $("#<%=ProveedorLabel.ClientID %>").text(NombreProveedor);
                        $("#<%=NITLabel.ClientID %>").text(NIT);
                    }
                })
                $("#<%=CancelRGastoExCLB.ClientID %>").click(function () {
                    $("#<%=TipoNombreHF.ClientID %>").attr('value', "");
                    $("#<%=TipoIdHF.ClientID %>").attr('value', "0");
                    $("#<%=GastoIdHF.ClientID %>").attr('value', "0");
                    $("#<%=ProveedorLabel.ClientID %>").text('');
                    $("#<%=NITLabel.ClientID %>").text('');
                })
                function TipoDocumentoCV_Validate(sender, args) {
                    args.IsValid = true;

                    var value = $find('<%= TipoDocumentoExCDDL.ClientID %>').get_value();

                    if (value <= "") {
                        args.IsValid = false;
                    }
                }
            </script>
        </div>
    </div>
</asp:Content>

