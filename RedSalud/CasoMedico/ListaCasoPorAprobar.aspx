<%@ Page Title="Casos Medicos por Aprobar" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListaCasoPorAprobar.aspx.cs" Inherits="CasoMedico_ListaCasoPorAprobar" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .CSearch .SearchTitle
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label CssClass="title" Text="Lista de casos medicos por aprobar" runat="server" />
            </div>
            <div class="columnContent">
                <asp:Panel ID="PanelSearch" runat="server">
                    <asp:Label ID="Label1" Text="Cliente" runat="server" 
                        style="width: 227px;"
                        CssClass="label left" />
                    <asp:DropDownList ID="ClienteDDL" runat="server"
                        DataSourceID="ClienteODS"
                        style="width: 346px; height:20px;"
                        DataValueField="ClienteId"
                        DataTextField="NombreJuridico">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ClienteODS" runat="server"
                        TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                        OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getRedClienteList"
                        OnSelected="ClienteODS_Selected" />
                    <div class="clear" style="margin-bottom: 5px;"></div>

                    <%--<asp:Label ID="Label4" Text="Rango de fecha:" runat="server" CssClass="label" />
                    <div class="left">
                        <asp:Label ID="Label2" Text="desde:" runat="server" />
                        <telerik:RadDatePicker ID="FechaInicio" runat="server"
                            EnableTyping="true"
                            ShowPopupOnFocus="true"
                            EnableShadows="true">
                        </telerik:RadDatePicker>
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="DateRFV" runat="server"
                                ControlToValidate="FechaInicio"
                                ErrorMessage="debe seleccionar una fecha de inicio."
                                ValidationGroup="SearchCaso"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="left" style="margin-left: 10px;">
                        <asp:Label ID="Label3" Text="Hasta" runat="server" />
                        <telerik:RadDatePicker ID="FechaFin" runat="server"
                            ShowPopupOnFocus="true" />
                        <div class="validators">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="FechaFin"
                                ErrorMessage="debe seleccionar una fecha de Final."
                                ValidationGroup="SearchCaso"
                                Display="Dynamic" />
                            <asp:CustomValidator ID="FechaFinCV" runat="server"
                                ControlToValidate="FechaFin"
                                ErrorMessage="La fecha final debe ser mayor o igual a la fecha de inicio."
                                ValidationGroup="SearchCaso"
                                ClientValidationFunction="FechaFinCV_Validate"
                                Display="Dynamic" />
                        </div>
                    </div>
                    <div class="clear"></div>
                    <asp:Label ID="Label5" Text="Buscar por Codigo Caso, Codigo Asegurado o nombre Asegurado" runat="server" CssClass="label" />
                    <asp:TextBox ID="SearchTexbox" runat="server" CssClass="biggerField" />

                    <div class="buttonsPanel">
                        <asp:LinkButton ID="SearchLB" Text="" runat="server"
                            CssClass="button"
                            ValidationGroup="SearchCaso"
                            OnClick="SearchLB_Click">
                            <asp:Label ID="Label6" Text="Buscar" runat="server" />
                        </asp:LinkButton>
                    </div>--%>
                    <search:SearchControl ID="SearchCasoMedico" runat="server"
                    Title="Buscar Casos Médicos por aprobar"
                    AdvancedSearchForm="~/UserControls/AdvancedSearch/CasoAprobationAdvancedSearch.ascx"
                    DisplayHelp="true"
                    DisplayContextualHelp="true"
                    CssSearchAdvanced="CSearch_Advanced_Panel"
                    CssSearch="CSearch"
                    CssSearchHelp="CSearchHelpPanel"
                    CssSearchError="CSearchErrorPanel"
                    SavedSearches="true"
                    SavedSearchesID="searchCtl_CasoMedicoListForAprove"
                    ImageHelpUrl="Images/Neutral/Help.png"
                    ImageErrorUrl="~/images/exclamation.png" />
                    <div class="clear" style="margin-bottom: 5px;"></div>
                </asp:Panel>
                <div style="overflow:auto;">
                <telerik:RadGrid ID="CasoRadGrid" runat="server"
                    AutoGenerateColumns="false"
                    AllowPaging="false"
                    OnItemCommand="CasoRadGrid_ItemCommand"
                    OnItemDataBound="CasoRadGrid_ItemDataBound"
                    MasterTableView-HierarchyLoadMode="Client"
                    MasterTableView-DataKeyNames="CasoId"
                    OnDetailTableDataBind="CasoRadGrid_DetailTableDataBind">
                    <MasterTableView>
                        <NoRecordsTemplate>
                            <asp:Label ID="CasoGridEmptyLabel" runat="server"
                                Text="No existen casos medicos por aprobar con los parametros de busqueda seleccionados."></asp:Label>
                        </NoRecordsTemplate>
                        <DetailTables>
                            <telerik:GridTableView runat="server"
                                AutoGenerateColumns="false"
                                PageSize="20"
                                Name="CasoDetailsForAprobation">
                                <NoRecordsTemplate>
                                    <asp:Label ID="CasoDetailsGridEmptyLabel" Text="No existen Detalles de Caso por aprobar." runat="server" />
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="AprobarCasoCommandColumn"
                                        HeaderText="Aprobar Caso"
                                        CommandName="Aprobar"
                                        ButtonType="ImageButton"
                                        ItemStyle-Width="40px"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ImageUrl="~/Images/neutral/complete.gif"
                                        ConfirmText="¿Está seguro que desea Aprobar el detalle del Caso?" />
                                    <telerik:GridBoundColumn DataField="CastoId" HeaderText="CastoId" Visible="false" />
                                    <telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false" />
                                    <telerik:GridBoundColumn DataField="TipoEstudio" HeaderText="Tipo de Estudio" HeaderStyle-CssClass="MinWidth250" />
                                    <telerik:GridBoundColumn DataField="NombreProveedor" HeaderText="Nombre del Proveedor" HeaderStyle-CssClass="MinWidth250" />
                                    <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" HeaderStyle-CssClass="MinWidth250" />
                                    <telerik:GridBoundColumn DataField="Table" HeaderText="Table" Visible="false" />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <asp:Image ID="AlertaImg" runat="server" ImageUrl="~/Images/Neutral/alerta.png"
                                        Visible="false"
                                        ToolTip="El Paciente supera el porcentaje de siniestralidad de alerta" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CodigoCaso" HeaderText="Código Caso" />
                            <telerik:GridBoundColumn DataField="NombreAseguradoraForDisplay" HeaderText="Cliente (ID)" HeaderStyle-CssClass="MinWidth100" Visible="false" />
                            <telerik:GridBoundColumn DataField="CodigoAsegurado" HeaderText="Código asegurado" />
                            <telerik:GridBoundColumn DataField="NombrePacienteForDisplay" HeaderText="Asegurado (ID)" HeaderStyle-CssClass="MinWidth100" />
                            <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="Fecha registro" DataFormatString="{0:d}" />
                            <telerik:GridBoundColumn DataField="MotivoConsulta" HeaderText="Motivo de la consulta" HeaderStyle-CssClass="MinWidth250" />

                            <telerik:GridBoundColumn DataField="NumeroPoliza" HeaderText="Nro. de Poliza" />
                            <telerik:GridBoundColumn DataField="FechaInicio" HeaderText="Fecha de Inicio" DataFormatString="{0:d}" Visible="false" />
                            <telerik:GridBoundColumn DataField="FechaFin" HeaderText="Fecha Final" DataFormatString="{0:d}" Visible="false" />
                            <%--<telerik:GridBoundColumn DataField="RangoDeFechas" HeaderText="Rango de la póliza" />--%>
                            <telerik:GridBoundColumn DataField="EstadoPoliza" HeaderText="Estado Poliza" />
                            <telerik:GridBoundColumn DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:#,#0.00}" />
                            <telerik:GridBoundColumn DataField="GastoTotal" HeaderText="Gasto Total" DataFormatString="{0:#,#0.00}" />
                            <telerik:GridBoundColumn DataField="NombrePlan" HeaderText="Nombre Plan" HeaderStyle-CssClass="MinWidth100" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                </div>
                <%--<asp:ObjectDataSource ID="CasoODS" runat="server"
                    TypeName="Artexacta.App.Caso.BLL.CasoBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SearchCasoForAprobation"
                    OnSelected="CasoODS_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchTexbox" Name="Search" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="ClienteDDL" Name="ClienteId" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="FechaInicio" Name="FechaInicio" PropertyName="SelectedDate" Type="DateTime" />
                        <asp:ControlParameter ControlID="FechaFin" Name="FechaFin" PropertyName="SelectedDate" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:ObjectDataSource ID="CasoDetalleODS" runat="server"
                    TypeName="Artexacta.App.Caso.CasoForAprobation.BLL.CasoForAprobationBLL"
                    OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetCasoListForAprobation"
                    OnSelected="CasoDetalleODS_Selected">
                    <SelectParameters>
                        <asp:SessionParameter Name="CasoId" SessionField="CasoId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>--%>
            </div>
                <div>
                    <span style="font-weight: bold;">Primer registro:</span>
                    <asp:Label ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Label>
                    <span style="font-weight: bold;">Número de registros cargados:</span>
                    <asp:Label ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Label>
                    <span style="font-weight: bold;">Número total de registros:</span>
                    <asp:Label ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Label>
                </div>
                <div class="buttonsPanel">
                    <asp:LinkButton CssClass="button" ID="FirstButton" runat="server" OnClick="PrimeroButton_Click">
                        <asp:label ID="Label2" Text="Primera" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="PreevFastButton" runat="server" OnClick="AnteriorRapidoButton_Click">
                        <asp:label ID="Label3" Text="-5 Páginas" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="PreevButton" runat="server" OnClick="AnteriorButton_Click">
                        <asp:label ID="Label4" Text="Anterior" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="NextButton" runat="server" OnClick="SiguienteButton_Click">
                        <asp:label ID="Label5" Text="Siguiente" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="NextFastButton" runat="server" OnClick="SiguienteRapidoButton_Click">
                        <asp:label ID="Label6" Text="+5 Páginas" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton CssClass="button" ID="LastButton" runat="server" OnClick="UltimoButton_Click">
                        <asp:label ID="Label7" Text="Ultimo" runat="server" />
                    </asp:LinkButton>
                </div>

                <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
                <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
                <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
                <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="ClienteIdHF" runat="server" />
        </div>
    </div>
</asp:Content>
