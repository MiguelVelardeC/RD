<%@ Page Title="Recuperacion Citas Eliminadas" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RecuperacionCitasEliminadas.aspx.cs" 
    Inherits="Desgravamen_RecuperacionCitasEliminadas" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="RedSalud" TagName="SearchControl" %>

<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span id="ListPropuestoAseguradoTitle" runat="server" class="title">Recuperacion de Citas Eliminadas</span>
            </div>
            <div class="columnContent">

                <RedSalud:SearchControl ID="SearchPA" runat="server"
                    Title="Búsqueda"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true" SavedSearchesID="SearchPA"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />

                &nbsp;&nbsp;
                Filtro: <asp:DropDownList ID="clientesComboBox" runat="server" 
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                </asp:DropDownList>

                <div class="clear" style="margin-bottom: 5px;"></div>

                <telerik:RadGrid ID="PropuestoAseguradoGridView" runat="server"
                    AutoGenerateColumns="false" DataSourceID="PropuestoAseguradoDataSource"
                    AllowPaging="false" 
                    OnItemCommand="PropuestoAseguradoGridView_ItemCommand"
                    OnItemDataBound="PropuestoAseguradoGridView_ItemDataBound"
                    AllowMultiRowSelection="False">
                    <MasterTableView DataKeyNames="PropuestoAseguradoId" ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay propuestos asegurados registrados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        
                        <Columns>

                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="VerPA" runat="server"
                                        ToolTip="Recuperar Cita"
                                        CommandName="VerPA"
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImprimirOrden" runat="server"
                                        ToolTip="Orden"
                                        CommandName="Orden"
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        ImageUrl="~/Images/Neutral/ExportPrint.gif" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn UniqueName="CitaDesgravamenId" DataField="CitaDesgravamenId"
                                HeaderText="Cita" />
                            <telerik:GridBoundColumn UniqueName="Nombre" DataField="Nombre"
                                HeaderText="Nombre" />
                            <telerik:GridBoundColumn UniqueName="TelefonoCelular" DataField="TelefonoCelular"
                                HeaderText="Contacto" />
                            <telerik:GridBoundColumn UniqueName="NombreCiudad" DataField="NombreCiudad"
                                HeaderText="Ciudad" />
                            <telerik:GridBoundColumn UniqueName="TipoProducto" DataField="TipoProductoForDisplay"
                                HeaderText="Producto" />
                            <telerik:GridBoundColumn UniqueName="Financiera" DataField="FinancieraForDisplay"
                                HeaderText="Financiera" />
                            <telerik:GridBoundColumn UniqueName="UsuarioRegistro" DataField="UsuarioRegistro"
                                HeaderText="Usuario" />
                            <telerik:GridBoundColumn UniqueName="FechaCreacionCita" DataField="FechaCreacionCitaForDisplay"
                                HeaderText="Fecha Creacion de la Cita" />
                            <telerik:GridBoundColumn UniqueName="FechaHoraCitaForDisplay" DataField="FechaHoraCitaForDisplay"
                                HeaderText="Fecha y Hora de la Cita" />
                            <telerik:GridTemplateColumn HeaderText="Examen Médico" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ExamenMEdicoCita" runat="server"
                                        Visible='<%# Convert.ToInt32(Eval("TieneExamenMedico")) == 1 %>'
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        CommandName="Examen"
                                        ImageUrl="~/Images/Neutral/historialMedico.png" Width="18px" />
                                    <asp:Image ID="NoNecesitaExamen" runat="server" Visible="false"
                                        ToolTip="No necesita examen médico"
                                        ImageUrl="~/Images/Neutral/delete_disabled.gif" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="NecesitaLaboratorio" DataField="NEcesitaLaboratorioForDisplay"
                                HeaderText="Necesita Estudios" />
                            <telerik:GridTemplateColumn HeaderText="Resultados de Estudios">
                                <ItemTemplate>
                                    <asp:ImageButton ID="FullReportePdfButton" runat="server"
                                        Visible="false"
                                        CommandArgument='<%# Eval("CitaDesgravamenId") %>'
                                        CommandName="Completo"
                                        ImageUrl="~/Images/Neutral/fullpdf.png" Width="18px" />
                                    <asp:Repeater ID="LaboratoriosMainRepeater" runat="server"
                                        OnItemDataBound="Repeater1_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="proveedorMedicoLabo">
                                                <%# Eval("ProveedorNombre") %>: 
                                                <asp:Repeater ID="DocumentosRepeater" runat="server" 
                                                    OnItemCommand="DocumentosRepeater_ItemCommand">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="DownloadImageButton" runat="server"
                                                            ImageUrl='<%# Eval("Icon") %>'
                                                            CommandArgument='<%# Eval("FileStoragePath") %>'
                                                            Width="18px" CommandName='<%# Eval("Name") %>'
                                                            ToolTip='<%# "Archivo: " + Eval("Name").ToString() + " - Subido en: " + Eval("DateUploadedForDisplay").ToString() %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
            
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <ExportSettings ExportOnlyData="true" IgnorePaging="true" HideStructureColumns="false" FileName="PropuestosAsegurados">
                        <Pdf FontType="Subset" PaperSize="Letter" />
                        <Excel Format="Html" />
                        <Csv ColumnDelimiter="Colon" RowDelimiter="NewLine" />
                    </ExportSettings>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="PropuestoAseguradoDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.PropuestoAseguradoBLL"
                    SelectMethod="GetPropuestoAseguradoBySearch"
                    OnSelected="PropuestoAseguradoDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchPA" PropertyName="Sql" Name="whereSql" Type="String" />                                               
                        <asp:ControlParameter ControlID="UserIdHiddenField" PropertyName="Value" Name="intUsuarioId" Type="Int32" />
                        <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="intClienteId" Type="Int32" /> 
                        <asp:Parameter Name="eliminado" DefaultValue="true" Type="Boolean" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="CurrentRow" Name="firstRow" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="PageSize" Name="pageSize" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" Name="totalRows" PropertyName="TotalRows" Type="Int32" Direction="Output" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <RedSalud:PagerControl ID="Pager" runat="server" 
                    PageSize="10" 
                    CurrentRow="0" 
                    InvisibilityMethod="PropertyControl" 
                    OnPageChanged="Pager_PageChanged" />
                <div class="clear"></div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="UserIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="ClienteIdHiddenField" runat="server" Value="0" />
    <asp:HiddenField ID="UserAuthorizedAprobarHF" runat="server" Value="false" />

</asp:Content>

