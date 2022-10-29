<%@ Page Title="Lista de Copagos Pendiente de Cobro" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CopagoLista.aspx.cs" Inherits="CasoMedico_CopagoLista" %>


<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sectionTitle:hover {
            background: #e1dddd;
        }

        .sectionTitle:focus {
            background: #e1dddd;
        }

        .PanelAdmin {
            border: 1px solid grey;
        }

        #PanelButton, #PanelButton:visited, #PanelButton:hover, #PanelButton:active {
            color: #333 !important;
        }

        .auto-style4 {
            width: 350px;
            height: 10px;
        }

        .auto-style6 {
            width: 350px;
            height: 40px;
        }

        .auto-style7 {
            width: 300px;
        }

        .auto-style8 {
            width: 12px;
        }

        .auto-style5 {
            width: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span id="ListPropuestoAseguradoTitle" runat="server" class="title">Lista de Copagos Pendiente de Cobro</span>
            </div>
            <div class="columnContent">
                <asp:Panel ID="AdminPanel" runat="server" CssClass="PanelAdmin" Style="font-size: 12px;" DefaultButton="boton">
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                            <span style="margin-left: 30px;">FILTROS DE ADMINISTRACION</span>
                        </h3>
                    </a>
                    <div style="padding: 1em 0.5em 0em;">
                        <asp:Label ID="LabelAlerta" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                    <div id="Contents" style="padding: 1em 0.5em;">
                        <asp:Label ID="LabelCiudad" runat="server" Text="CIUDAD:"></asp:Label>
                        <asp:DropDownList ID="ciudadComboBox" runat="server"
                            DataValueField="CiudadId"
                            DataTextField="Nombre"
                            Width="100px"
                            AutoPostBack="true"
                            Style="margin-left: 3px;"
                            OnSelectedIndexChanged="CiudadComboBox_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="LabelCliente" runat="server" Text="CLIENTE:"></asp:Label>
                        <asp:DropDownList ID="clientesComboBox" runat="server" AutoPostBack="true"
                            Style="margin-left: 3px;" Height="22px" Width="150px"
                            OnSelectedIndexChanged="ClienteRadComboBox_SelectedIndexChanged">
                            <asp:ListItem Text="Todos" Value=""> </asp:ListItem>
                            <%-- <asp:ListItem Text="Los casos NO aprobados solamente" Value="@APROBADO FALSE" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="Los casos aprobados" Value="@APROBADO TRUE"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:Label ID="LabelTipoProveedor" runat="server" Text="TIPO PROVEEDOR:"></asp:Label>
                        <asp:DropDownList ID="TipoProveedorComboBox" runat="server" AutoPostBack="true"
                            Height="22px" Style="margin-left: 3px" Visible="true" Width="100px"
                            OnSelectedIndexChanged="TipoProveedorComboBox_SelectedIndexChanged">
                            <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="LabelProveedor" runat="server" Text="PROVEEDOR:"></asp:Label>
                        <asp:DropDownList ID="ProveedorComboBox" runat="server" AutoPostBack="false"
                            Height="22px" Style="margin-left: 3px" Visible="true" Width="150px">
                            <%--OnSelectedIndexChanged="clientesComboBox_SelectedIndexChanged" --%>
                            <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="LabelMedicoComboBox" runat="server" Text="Medico:"></asp:Label>
                        <asp:DropDownList ID="MedicoComboBox" runat="server" AutoPostBack="false"
                            Height="22px" Style="margin-left: 3px" Visible="true" Width="150px">

                            <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                        <br />
                        <div class="clear" style="margin-bottom: 7px;"></div>
                        <asp:Label ID="LabelCodigoCaso" runat="server" Text="CODIGO DEL CASO:"></asp:Label>
                        <telerik:RadTextBox ID="codigoCasoIdText" Text="" EmptyMessage="Código de Caso" runat="server" Width="150px" />
                        <asp:Label ID="LabelNombre" runat="server" Text="NOMBRE:"></asp:Label>
                        <telerik:RadTextBox ID="NombreText" Text="" EmptyMessage="Nombre de los Asegurados" runat="server" Width="180px" />

                        <asp:Label ID="LabelCI" runat="server" Text="C.I:"></asp:Label>
                        <telerik:RadTextBox ID="CIText" Text="" EmptyMessage="CI del Cliente" runat="server" Width="150px" />

                        <asp:Label ID="LabelEstado" runat="server" Text="ESTADO:"></asp:Label>
                        <asp:DropDownList ID="estadoComboBox" runat="server"
                            AutoPostBack="false"
                            Width="80px"
                            Style="margin-left: 3px;">
                            <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                            <asp:ListItem Text="COBRADO" Value="1"></asp:ListItem>
                            <asp:ListItem Text="NO COBRADO" Value="2"></asp:ListItem>

                        </asp:DropDownList>
                        <br />
                        <br />

                        <asp:Label ID="LabelFechaInicio" runat="server" Text="FECHA INICIO:"></asp:Label>
                        <telerik:RadDatePicker ID="FECHAINICIOCOPAGOS" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="120px"></telerik:RadDatePicker>
                        <asp:Label ID="LabelFechaFin" runat="server" Text="FECHA FINAL:"></asp:Label>
                        <telerik:RadDatePicker ID="FECHAFINALCOPAGOS" runat="server" DateInput-EmptyMessage="Fecha Final" Width="120px"></telerik:RadDatePicker>
                        <div class="clear" style="margin-bottom: 7px;"></div>
                        <asp:LinkButton ID="BtnNewCaso" runat="server" CssClass="button"
                            OnClick="BtnNewCaso_Click">
                        <span>Registrar Nuevo Caso</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="button"
                            OnClick="btnSearch_Click">
                        <span>BUSCAR Post Consulta</span>
                        </asp:LinkButton>


                    </div>
                    <asp:Button ID="boton" Text="" Style="display: none;" runat="server" />
                </asp:Panel>
                <div class="right RadGrid_Default" style="border: none;">
                    <asp:LinkButton ID="ExportToExcelMatrizNacional" runat="server" OnClick="export_Matriz_Nacional_Click">
                            <span class="left rgExpXLS" style="width:14px; height: 16px;margin-right:5px;"></span>
                            <span class="left">Exportar a Excel matriz nacional</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="realExportMatrizNacional" runat="server" OnClick="export_Matriz_Nacional_Click"></asp:LinkButton>
                </div>
                <div class="right RadGrid_Default" style="border: none;">
                    <asp:LinkButton ID="ExportToExcelLB" runat="server" OnClick="export_Click">
                            <span class="left rgExpXLS" style="width:14px; height: 16px;margin-right:10px;"></span>
                            <span class="left">Exportar a Excel</span>
                            <span class="left">...</span>
                    
                    </asp:LinkButton>
                    <asp:LinkButton ID="realExport" runat="server" OnClick="export_Click"></asp:LinkButton>
                </div>

                <div class="clear" style="margin-bottom: 5px;"></div>

                <telerik:RadGrid ID="PrestacionesCoPagoGrid" runat="server" Visible="true"
                    AutoGenerateColumns="false"
                    OnExcelExportCellFormatting="CoPagoLRadGrid_ExcelExportCellFormatting"
                    OnItemCommand="PrestacioneCoPagoGrid_ItemCommand"
                    OnItemDataBound="PrestacioneCoPagoGrid_ItemDataBound"
                    AllowPaging="false"
                    AllowSorting="false"
                    MasterTableView-DataKeyNames="CasoId"
                    OnDetailTableDataBind="PrestacioneCoPagoGrid_DetailTableDataBind">
                    <ExportSettings OpenInNewWindow="true" ExportOnlyData="true" FileName="ReporteCoPagoLista" IgnorePaging="true">
                        <Excel Format="Html" />
                    </ExportSettings>
                    <MasterTableView CommandItemDisplay="Top" Width="100%" TableLayout="Fixed"
                        OverrideDataSourceControlSorting="true">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        <CommandItemTemplate>
                            <table class="rgCommandTable" border="0" style="width: 100%;">
                                <caption>
                                    <thead>
                                        <tbody>
                                            <tr>
                                                <td align="left"><b><%--EXPORTAR A EXCEL |--%></b>
                                                    <asp:Button ID="export" runat="server" CssClass="rgExpXLS" Visible="false"
                                                        OnClick="export_Click"
                                                        ToolTip="EXPORT TO EXCEL" />
                                                </td>
                                                <td align="right"></td>
                                            </tr>
                                        </tbody>
                            </table>
                        </CommandItemTemplate>
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Copagos registrados</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle Font-Size="11px" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="VER" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true" ItemStyle-Width="24px" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DetailsImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/select.png"
                                        CommandArgument='<%# Eval("TipoCaso")+";"+Eval("detId")%> '
                                        Width="28px" CommandName="Select"
                                        ToolTip="VER"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn Visible="true"
                                UniqueName="TemplateColumnEliminar" ShowSortIcon="false"
                                HeaderStyle-Font-Bold="true" ItemStyle-Width="24px" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DeleteImageButton" runat="server"
                                        ImageUrl="~/Images/Neutral/delete.png"
                                        CommandArgument='<%# Eval("TipoCaso")+";"+Eval("detId")%> '
                                        Width="28px" CommandName="Delete"
                                        OnClientClick="return confirm('¿Está seguro de eliminar esta prestación?');"
                                        ToolTip="Eliminar"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NombrePaciente" Visible="true"
                                HeaderText="Paciente Nombre" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NombreCliente" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="Cliente Nombre" />
                            <telerik:GridBoundColumn DataField="NombreProveedor" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="Proveedor" />
                            <telerik:GridBoundColumn DataField="NombreTipoProveedor" Visible="true" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="Tipo de Servicio" />
                            <telerik:GridBoundColumn DataField="FechaHoraForDisplay" UniqueName="FechaHoraForDisplay" Visible="true"
                                HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true" ItemStyle-Width="1px" ItemStyle-Height="1px"
                                HeaderText="Fecha" />
                            <telerik:GridBoundColumn DataField="TipoCaso" Visible="false" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="Tipo De Caso" />
                            <telerik:GridBoundColumn UniqueName="Estado1" DataField="Estado" Visible="True" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Bold="true"
                                HeaderText="ESTADO" />
                            <telerik:GridBoundColumn DataField="detId" HeaderText="detId" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <span id="span1" style="font-weight: bold;">Primer registro:</span>
                        </td>
                        <td>
                            <asp:Label ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <span id="span2" style="font-weight: bold;">Número de registros cargados:</span>
                        </td>
                        <td>
                            <asp:Label ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <span id="span3" style="font-weight: bold;">Número total de registros:</span>
                        </td>
                        <td>
                            <asp:Label ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="buttonsPanel">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="button" ID="FirstButton" runat="server" OnClick="PrimeroButton_Click">
                                <asp:Label ID="Label2" Text="Primera" runat="server" />
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton CssClass="button" ID="PreevFastButton" runat="server" OnClick="AnteriorRapidoButton_Click">
                                <asp:Label ID="Label3" Text="-5 Páginas" runat="server" />
                            </asp:LinkButton>
                        </td>
                        <td>
                       <asp:LinkButton CssClass="button" ID="PreevButton" runat="server" OnClick="AnteriorButton_Click">
                            <asp:Label ID="Label4" Text="Anterior" runat="server" />
                        </asp:LinkButton>
                        </td>
                        <td>
                        <asp:LinkButton CssClass="button" ID="NextButton" runat="server" OnClick="SiguienteButton_Click">
                            <asp:Label ID="Label5" Text="Siguiente" runat="server" />                        
                        </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton CssClass="button" ID="NextFastButton" runat="server" OnClick="SiguienteRapidoButton_Click">
                            <asp:Label ID="Label6" Text="+5 Páginas" runat="server" />
                        </asp:LinkButton>
                        </td>
                        <td>
                        <asp:LinkButton CssClass="button" ID="LastButton" runat="server" OnClick="UltimoButton_Click">
                        <asp:Label ID="Label7" Text="Ultimo" runat="server" />
                        </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
            <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
            <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />


            <asp:HiddenField ID="ClienteIdHF" runat="server" />

        </div>

        <asp:HiddenField ID="MedicoIdHF" Value="0" runat="server" />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
        <search:SearchControl ID="SearchCasoMedico" runat="server"
            Title="Buscar Casos Médicos"
            AdvancedSearchForm="~/UserControls/AdvancedSearch/CasoMedicoAdvancedSearch.ascx"
            DisplayHelp="true"
            DisplayContextualHelp="true"
            CssSearchAdvanced="CSearch_Advanced_Panel"
            CssSearch="CSearch"
            CssSearchHelp="CSearchHelpPanel"
            CssSearchError="CSearchErrorPanel"
            SavedSearches="true"
            SavedSearchesID="searchCtl_CasoMedicoList"
            ImageHelpUrl="Images/Neutral/Help.png"
            Visible="false"
            ImageErrorUrl="~/images/exclamation.png" />


        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnCrearCaso">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="LabelMensajeErrorNewCaso"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnVerificarConsulta">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PacienteDropDownList"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TPacienteId"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TNombre"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LabelNombrePAciente"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="PrestacionesCoPagoGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="UltimaFilaCargadaHF" />


                        <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>

                    </UpdatedControls>
                </telerik:AjaxSetting>

              <telerik:AjaxSetting AjaxControlID="NextButton">
                  <UpdatedControls>
                       <telerik:AjaxUpdatedControl  ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>
                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                  </telerik:AjaxSetting>
                  <telerik:AjaxSetting AjaxControlID="PreevButton">
                  <UpdatedControls>
                       <telerik:AjaxUpdatedControl  ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>
                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                  </telerik:AjaxSetting>
                
                  <telerik:AjaxSetting AjaxControlID="NextFastButton">
                  <UpdatedControls>
                       <telerik:AjaxUpdatedControl  ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>
                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                  </telerik:AjaxSetting>

                
                  <telerik:AjaxSetting AjaxControlID="LastButton">
                  <UpdatedControls>
                       <telerik:AjaxUpdatedControl  ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>
                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                  </telerik:AjaxSetting>

                  <telerik:AjaxSetting AjaxControlID="PreevFastButton">
                  <UpdatedControls>
                       <telerik:AjaxUpdatedControl  ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>
                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                  </telerik:AjaxSetting>

                 <telerik:AjaxSetting AjaxControlID="FirstButton">
                  <UpdatedControls>
                       <telerik:AjaxUpdatedControl  ControlID="PrestacionesCoPagoGrid"></telerik:AjaxUpdatedControl>
                       <telerik:AjaxUpdatedControl  ControlID="LoadedFirstRecordLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LoadedNumRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalDBRecordsLabel"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ActivePageHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="TotalFilasHF"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PrimeraFilaCargadaHF"></telerik:AjaxUpdatedControl>

                       <telerik:AjaxUpdatedControl ControlID="PreevButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="NextFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="LastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="PreevFastButton"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FirstButton"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                  </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="export">
                    <UpdatedControls>
                    </UpdatedControls>
                </telerik:AjaxSetting>


            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadWindowManager ID="WinMgr" runat="server" EnableViewState="false" DestroyOnClose="true">
            <Windows>

                <telerik:RadWindow ID="CreacionEstudioWindow" runat="server" Width="750px" Height="350px"
                    Title="CREAR CASO NUEVO" DestroyOnClose="true"
                    Modal="true" VisibleStatusbar="false" CssClass="radWin" Behaviors="Close,Move"
                    VisibleOnPageLoad="false"
                    KeepInScreenBounds="true" OnClientClose="OnEstudioFormClose">

                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" Style="padding-left: 5%; padding-right: 6%;" Visible="true">
                            <div>
                                <div class="error">
                                    <asp:Label ID="Label59" Text="" runat="server" />
                                </div>
                                <asp:Label ID="CiudadIDHL" Visible="false" runat="server"></asp:Label>
                                <table>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label1" runat="server" Text="CLIENTE:" Font-Size="11px" CssClass="label"></asp:Label>
                                        </td>
                                        <td class="auto-style4">
                                            <telerik:RadComboBox ID="CNClienteRadComboBox" runat="server" AutoPostBack="false"
                                                Width="280px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label Text="Introducir N° C.I. o Nombre del Paciente:" runat="server" CssClass="label" Font-Size="11px" />
                                        </td>
                                        <td class="auto-style6">
                                            <asp:TextBox ID="TextCIValidar" Text="" runat="server" Width="250px" AutoPostBack="false" EnableViewState="false">
                                            </asp:TextBox>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:LinkButton ID="BtnVerificarConsulta" runat="server" CssClass="button" OnClick="BuscarPaciente_Onclick"
                                                AutoPostBack="true">
                            <span>Buscar Paciente</span>
                                            </asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label Text="Nombre:" ID="TNombre" runat="server" CssClass="label" Font-Size="11px" Visible="true" />
                                        </td>
                                        <td class="auto-style6">
                                            <asp:Label ID="LabelNombrePAciente" Text="" runat="server" AutoPostBack="false" Visible="false">
                                            </asp:Label>
                                            <telerik:RadComboBox ID="PacienteDropDownList" runat="server"
                                                Visible="true"
                                                Width="280px"
                                                RenderMode="Classic"
                                                EnableViewState="true"
                                                EmptyMessage="Seleccione un Paciente"
                                                OnClientSelectedIndexChanged="PacienteDropDownList_OnClientSelectedIndexChanged">
                                                <%-- onChange="PacienteDropDownList_OnClientSelectedIndexChanged();"--%>
                                            </telerik:RadComboBox>

                                        </td>
                                        <td class="auto-style4"></td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label Text="Seleccione una Poliza:" runat="server" ID="TPoliza" CssClass="label" Font-Size="11px" Visible="true" />
                                        </td>
                                        <td class="auto-style6">
                                            <telerik:RadComboBox ID="ListaDePolizaDropDown" runat="server" Visible="true" EnableViewState="true"
                                                Width="280px"
                                                OnClientItemsRequesting="PolizaCliente_OnClientItemsRequesting"
                                                EmptyMessage="Seleccione una Poliza"
                                                RenderMode="Classic"
                                                OnClientSelectedIndexChanged="ListaDePolizaDropDown_OnClientSelectedIndexChanged">
                                                <WebServiceSettings Method="GetPolizaxPacienteId" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                            </telerik:RadComboBox>
                                            <div class="validators">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                                                    ControlToValidate="ListaDePolizaDropDown"
                                                    ErrorMessage="Debe Seleccionar Una Cliente"
                                                    ValidationGroup="CreateNewCaso"
                                                    Display="Dynamic" />
                                            </div>
                                        </td>
                                        <td class="auto-style4"></td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label Text="Medico General:" runat="server" ID="TMedico" CssClass="label" Font-Size="11px" Visible="true" />
                                        </td>
                                        <td class="auto-style6">
                                            <telerik:RadComboBox ID="MedicoRadCombo" runat="server"
                                                Width="280px"
                                                OnClientItemsRequesting="MedicoRadCombo_OnClientItemsRequesting"
                                                EmptyMessage="Seleccione un Especialista"
                                                AutoPostBack="false"
                                                RenderMode="Lightweight">
                                                <WebServiceSettings Method="GetMedicoGeneralxCiudadxCliente" Path="~/AutoCompleteWS/ComboBoxWebServices.asmx" />
                                            </telerik:RadComboBox>
                                            <div class="validators">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="MedicoRadCombo"
                                                    ErrorMessage="Debe Seleccionar Un Medico"
                                                    ValidationGroup="CreateNewCaso"
                                                    Display="Dynamic" />
                                            </div>
                                        </td>
                                        <td class="auto-style4"></td>
                                    </tr>
                                </table>
                                <table style="margin-top: 20px;">
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="BtnCrearCaso" Visible="true" Text=" Crear Caso " AutoPostBack="true" runat="server"
                                                OnClick="BtnCrearCaso_Onclick" ValidationGroup="CreateNewCaso" />

                                        </td>
                                        <td class="auto-style5"></td>
                                        <td>
                                            <telerik:RadButton ID="CancelNewCasoCoPago" Visible="false" Text="Cancelar" runat="server" OnClick="CancelNewCaso_Click" />

                                        </td>
                                    </tr>
                                </table>

                                <br />
                                <br />
                            </div>
                            <asp:Label ID="LabelMensajeErrorNewCaso" ForeColor="Red" Visible="false" Text="eroor" runat="server" HeaderStyle-Font-Bold="true" Font-Size="10px" CssClass="label" />

                        </asp:Panel>
                        <div style="clear: both;"></div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function PacienteDropDownList_OnClientSelectedIndexChanged(sender, eventArgs) {
                $find('<%= ListaDePolizaDropDown.ClientID %>').clearSelection();
                var combo = $find("<%= ListaDePolizaDropDown.ClientID %>");
                combo.requestItems('', false);
            }

            function PolizaCliente_OnClientItemsRequesting(sender, eventArgs) {
                var comboPaciente = $find("<%= PacienteDropDownList.ClientID %>");
                var context = eventArgs.get_context();
                context["PacienteId"] = comboPaciente.get_value();
           <%-- context["PacienteId"] = document.getElementById('<%=PacienteDropDownList.ClientID%>').value;--%>
            }
            function ListaDePolizaDropDown_OnClientSelectedIndexChanged(sender, eventArgs) {
                $find('<%=  MedicoRadCombo.ClientID %>').clearSelection();
                var combo = $find("<%= MedicoRadCombo.ClientID %>");
                combo.requestItems('', false);
            }



            function MedicoRadCombo_OnClientItemsRequesting(sender, eventArgs) {
                var comboPaciente = $find("<%= PacienteDropDownList.ClientID %>");
                var comboPoliza = $find("<%= ListaDePolizaDropDown.ClientID %>");
                var context = eventArgs.get_context();
                context["PacienteId"] = comboPaciente.get_value();
                <%--context["PacienteId"] = document.getElementById('<%=PacienteDropDownList.ClientID%>').value;--%>
                context["PolizaId"] = comboPoliza.get_value();
            }
            function OnEstudioFormClose() {
                $("#<%= TextCIValidar.ClientID %>").val("");
                $find('<%=PacienteDropDownList.ClientID%>').clearSelection();
                $find('<%=ListaDePolizaDropDown.ClientID%>').clearSelection();
                $find('<%=MedicoRadCombo.ClientID%>').clearSelection();
                $find('<%=CNClienteRadComboBox.ClientID%>').clearSelection();


            }
            function CloseModal() {

                var window = $find('<%=CreacionEstudioWindow.ClientID %>');
                window.close();
            }


        </script>
    </telerik:RadCodeBlock>

</asp:Content>
