<%@ Page Title="Siniestro" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SOATList.aspx.cs"
    Inherits="SOAT_SiniestroList" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/Tag/TagSelector.ascx" TagPrefix="RedSalud" TagName="TagSelector" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>
<%@ Register Src="~/UserControls/FileManager.ascx" TagPrefix="RedSalud" TagName="FileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/tagit.js" type="text/javascript"></script>
    <style>
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

        .rgExpandCol {
            padding: 4px 2px!important;
        }

        .RadGrid_Default .rgExpand {
            background-position: 2px -496px!important;
        }

        .RadGrid_Default .rgCollapse {
            background-position: -1px -444px!important;
        }
        .RadCalendarPopup
        {
            z-index: 9001 !important;
        }
        .tagit ul.ui-autocomplete.ui-front.ui-menu.ui-widget.ui-widget-content.ui-corner-all{
            z-index: 9001;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" runat="Server">
    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <asp:Label ID="Label1" Text="Lista de Siniestros" runat="server" CssClass="title" />
            </div>
            <div class="contentMenu">
                <asp:HyperLink ID="NuevoSiniestroHyperLink" runat="server" Visible="false"
                    CssClass="addNew"
                    Text="Agregar un nuevo Siniestro"
                    NavigateUrl="~/SOAT/SOATWizard.aspx" />
            </div>
            <asp:Panel runat="server">
                <%--<asp:Label Text="Buscar Siniestro por nombre, apellido o CI" runat="server" CssClass="label" />--%>
                <div class="buttonsPanel">
                    <div id="ClienteSOATContainer" style="display:none;">
                        <asp:Label ID="Label3" Visible="false" Text="Cliente" runat="server"
                            Style="width: 185px;"
                            CssClass="label left" />
                        <asp:DropDownList ID="ClienteDDL" runat="server"
                            DataSourceID="ClienteODS"
                            Style="width: 346px; height: 20px;"
                            DataValueField="ClienteId"
                            DataTextField="NombreJuridico"
                            AutoPostBack="true"
                            visible="false"
                            OnSelectedIndexChanged="ClienteDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ClienteODS" runat="server"
                            TypeName="Artexacta.App.RedCliente.BLL.RedClienteBLL"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="getRedClienteForSOATList"
                            OnSelected="ClienteODS_Selected"></asp:ObjectDataSource>
                        <div class="clear" style="margin-bottom: 5px;"></div>
                    </div>
                    <div class="left">
                        <search:SearchControl ID="SearchSiniestros" runat="server"
                            Title="<b>Buscar Siniestros SOAT</b>"
                            DisplayHelp="true"
                            DisplayContextualHelp="true"
                            CssSearchAdvanced="CSearch_Advanced_Panel"
                            CssSearch="CSearch"
                            CssSearchHelp="CSearchHelpPanel"
                            CssSearchError="CSearchErrorPanel"
                            SavedSearches="true"
                            SavedSearchesID="searchCtl_SOATList"
                            ImageHelpUrl="Images/Neutral/Help.png"
                            ImageErrorUrl="~/images/exclamation.png"
                            AdvancedSearchForm="~/UserControls/AdvancedSearch/SOATAdvancedSearch.ascx" />
                    </div>
                    <div class="right RadGrid_Default" style="border: none;">
                        <asp:LinkButton ID="ExportToExcelLB" runat="server" OnClientClick="return chooseDecimalSeparator();">
                            <span class="left rgExpXLS" style="width:14px; height: 16px;margin-right:5px;"></span>
                            <span class="left">Exportar a Excel</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="realExport" runat="server" OnClick="ExportToExcelLB_Click"></asp:LinkButton>
                    </div>
                    <div class="clear"></div>
                </div>
            </asp:Panel>

            <telerik:RadGrid ID="SiniestroRadGrid" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="false"
                AllowMultiRowSelection="False"
                OnPreRender="SiniestroRadGrid_PreRender"
                OnItemCommand="SiniestroRadGrid_ItemCommand"
                OnDetailTableDataBind="SiniestroRadGrid_DetailTableDataBind">
                <ClientSettings>
                    <ClientEvents OnColumnClick="OnColumnClick" />
                </ClientSettings>
                <MasterTableView DataKeyNames="SiniestroId" ExpandCollapseColumn-Display="false">
                    <NoRecordsTemplate>
                        <div style="text-align: center;">No hay Siniestros de SOAT a mostrar.</div>
                    </NoRecordsTemplate>
                    <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                    <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                    <HeaderStyle Font-Size="11px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ExpandCollapseGTC" ItemStyle-CssClass="rgExpandCol">
                            <ItemTemplate>
                                <asp:LinkButton ID="NoSortExpandImageButton" runat="server"
                                    CssClass="rgExpand"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("SiniestroId") %>'
                                    Width="10px" CommandName="Expand"
                                    ToolTip="Ver Accidentados"></asp:LinkButton>
                                <asp:LinkButton ID="CollapseLinkButton" runat="server"
                                    CssClass="rgCollapse" Visible="false"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("SiniestroId") %>'
                                    Width="10px" CommandName="Collapse"
                                    ToolTip="Ocultar Accidentados"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="NoSortDeleteGTC" HeaderTooltip="Eliminar" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteImageButton" runat="server"
                                    ImageUrl="~/Images/neutral/delete.png"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("SiniestroId") %>'
                                    OnClientClick="return confirm('¿Está seguro que desea eliminar el Siniestro?');"
                                    Width="24px" CommandName="Eliminar"
                                    ToolTip="Eliminar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="NoSortEditGTC" HeaderTooltip="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/select.png"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("SiniestroId")+ "," + Eval("EstadoSeguimiento")  %>'
                                    Width="24px" CommandName="Select"
                                    ToolTip="Editar"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="NoSortPrintOnlySiniestroGTC" HeaderTooltip="Imprimir Solo Siniestro">
                            <ItemTemplate>
                                <asp:ImageButton ID="PrintSiniestroOnlyImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/ExportPrint.gif"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("SiniestroId")  %>'
                                    Width="24px" CommandName="PrintSiniestroOnly"
                                    ToolTip="Imprimir Siniestro"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="NoSortPrintGTC" HeaderTooltip="Imprimir Siniestro y Adjuntos">
                            <ItemTemplate>
                                <asp:ImageButton ID="PrintImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/fullpdf.png"
                                    OnCommand="DetailsImageButton_Command"
                                    CommandArgument='<%# Eval("SiniestroId")  %>'
                                    Width="24px" CommandName="PrintSiniestro"
                                    ToolTip="Imprimir Siniestro con Adjuntos"></asp:ImageButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="SiniestroId" DataField="SiniestroId"
                            HeaderText="Identificador del Siniestro" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="FechaSiniestro" DataField="FechaSiniestroForDisplay"
                            HeaderText="Fecha del Siniestro" HeaderStyle-Width="85px" DataType="System.DateTime" />
                        <telerik:GridBoundColumn UniqueName="FechaDenuncia" DataField="FechaDenunciaForDisplay"
                            HeaderText="Fecha de la Denuncia" HeaderStyle-Width="85px" DataType="System.DateTime" />
                        <telerik:GridBoundColumn UniqueName="NoSortCantidadHeridos" DataField="CantidadHeridos"
                            HeaderText="H" HeaderTooltip="Cantidad de Heridos" HeaderStyle-Width="20px" />
                        <telerik:GridBoundColumn UniqueName="NoSortCantidadFallecidos" DataField="CantidadFallecidos"
                            HeaderText="F" HeaderTooltip="Cantidad de Fallecidos" HeaderStyle-Width="20px" />
                        <telerik:GridBoundColumn UniqueName="NoSortTotalOcupantes" DataField="TotalOcupantes"
                            HeaderText="T" HeaderTooltip="Total Accidentados" HeaderStyle-Width="20px" />
                        <telerik:GridBoundColumn UniqueName="Lugar" DataField="Lugar"
                            HeaderText="Lugar del Siniestro" Visible="false" HeaderStyle-Width="110px" />
                        <telerik:GridBoundColumn UniqueName="OperacionId" DataField="IdentificadorOperaciones"
                            HeaderText="Identificador de Operaciones" HeaderStyle-Width="95px" />
                        <telerik:GridBoundColumn UniqueName="NumeroRoseta" DataField="NumeroRoseta"
                            HeaderText="Número de Roseta" Visible="false" HeaderStyle-Width="60px" />
                        <telerik:GridBoundColumn UniqueName="NumeroPoliza" DataField="NumeroPoliza"
                            HeaderText="Número de Póliza" HeaderStyle-Width="60px" />
                        <telerik:GridBoundColumn UniqueName="Placa" DataField="Placa"
                            HeaderText="Placa" HeaderStyle-Width="60px" />
                        <telerik:GridBoundColumn UniqueName="TipoVehiculo" DataField="Tipo"
                            HeaderText="Tipo de Vehiculo" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="Sector" DataField="Sector"
                            HeaderText="Sector" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="NombreTitular" DataField="NombreTitular"
                            HeaderText="Titular" />
                        <telerik:GridBoundColumn UniqueName="CITitular" DataField="CarnetIdentidad"
                            HeaderText="CI del Titular" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="NombreAuditor" DataField="NombreAuditor"
                            HeaderText="Auditor" />
                        <telerik:GridBoundColumn UniqueName="SiniestrosPreliquidacion" DataField="SiniestrosPreliquidacionForDisplay"
                            HeaderText="Total Proforma" HeaderStyle-Width="50px" Visible="false" />
                        <telerik:GridBoundColumn UniqueName="SiniestrosPagados" DataField="SiniestrosPagadosForDisplay"
                            HeaderText="Total Gasto" HeaderStyle-Width="50px" />
                        <telerik:GridBoundColumn UniqueName="NoSortEstadoSeguimiento" DataField="EstadoSeguimiento"
                            HeaderText="Estado" HeaderStyle-Width="60px" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="NoSortFileManagerGTC" HeaderText="Adjuntos"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                            <ItemTemplate>
                                <div class="AdjuntosCont">
                                    <asp:ImageButton ID="FileManagerIB" runat="server"
                                        ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                        CommandName="SINIESTRO"
                                        CommandArgument='<%# Eval("SiniestroId") %>'
                                        ToolTip="Adjuntar Archivo a Siniestro de SOAT"
                                        OnCommand="FileManager_Command" />
                                    <asp:Label ID="Label2" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="AccidentadoId" Width="100%" Name="Accidentados"
                            runat="server">
                            <NoRecordsTemplate>
                                <div style="text-align: center;">No hay Accidentados a mostrar en este Siniestro de SOAT.</div>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="DeleteAccidentado">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DeleteImageButton" runat="server"
                                            ImageUrl="~/Images/neutral/delete.png"
                                            OnCommand="DetailsImageButton_Command"
                                            CommandArgument='<%# Eval("AccidentadoId") %>'
                                            OnClientClick="return confirm('¿Está seguro que desea eliminar el Accidentado?');"
                                            CommandName="EliminarAccidentado"
                                            ToolTip="Eliminar"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DetailsImageButton" runat="server"
                                            ImageUrl="~/Images/Neutral/select2.png"
                                            OnCommand="DetailsImageButton_Command"
                                            CommandArgument='<%# Eval("AccidentadoId") %>'
                                            Style="float: right;"
                                            Width="20px" CommandName="SelectAccidentado"
                                            ToolTip="Editar"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>                                
                                <telerik:GridTemplateColumn UniqueName="GestionMedicaAccidentado">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="GestionImageButton" runat="server"
                                            ImageUrl="~/Images/Neutral/gestionMedica.png"
                                            OnCommand="DetailsImageButton_Command"
                                            CommandArgument='<%# Eval("AccidentadoId") %>'
                                            Width="22px" CommandName="AddGestionMedicaAccidentado"
                                            ToolTip="Añadir Visita Médica"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>                                
                                <telerik:GridTemplateColumn UniqueName="PreliquidacionDetalleInsert" HeaderText="" Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="PreliquidacionImageButton" runat="server"
                                            ImageUrl="~/Images/Neutral/preliquidacion.png"
                                            OnClientClick='<%# "return OpenNGDialog(0, null, " + Eval("AccidentadoId") + ","+Eval("SiniestroId") +", \"G\");" %>'
                                            Width="22px" ToolTip="Añadir Gastos Médicos"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>                                
                                <telerik:GridTemplateColumn UniqueName="CartaGarantiaDetalle" HeaderText="" Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="CartaGarantiaDetalleImageButton" runat="server"
                                            OnCommand="DetailsImageButton_Command"
                                            CommandArgument='<%# Eval("SiniestroId") + "|" + Eval("AccidentadoId") %>'
                                            ImageUrl="~/Images/Neutral/carta-garantia-light.png"                                            
                                            Width="22px" ToolTip="Generar Carta Garantia"></asp:ImageButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="NoSortNombre" DataField="NombreForDisplay" HeaderText="Nombre" />
                                <telerik:GridBoundColumn UniqueName="NoSortCarnetIdentidad" DataField="CarnetIdentidad" HeaderText="CI" />
                                <telerik:GridBoundColumn UniqueName="NoSortGenero" DataField="GeneroForDisplay" HeaderText="Sexo" />
                                <telerik:GridBoundColumn UniqueName="NoSortFechaNacimiento" DataField="FechaNacimientoForDisplay" HeaderText="Fecha de Nacimiento" />
                                <telerik:GridBoundColumn UniqueName="NoSortEstadoCivil" DataField="EstadoCivil" HeaderText="Estado Civil" />
                                <telerik:GridBoundColumn UniqueName="NoSortLicenciaConducir" DataField="LicenciaConducirForDisplay" HeaderText="Tiene Licencia" />
                                <telerik:GridBoundColumn UniqueName="NoSortTipo" DataField="TipoForDisplay" HeaderText="Tipo" />
                                <telerik:GridBoundColumn UniqueName="NoSortEstado" DataField="EstadoForDisplay" HeaderText="Accidentado / Fallecido" />
                                <telerik:GridBoundColumn UniqueName="NoSortSiniestroPagado" DataField="SiniestroPagadoForDisplay" HeaderText="Total Gastos" />
                                <telerik:GridBoundColumn UniqueName="NoSortEstadoSeguimiento" DataField="EstadoSeguimiento" HeaderText="Estado" Visible="false" />
                                <telerik:GridTemplateColumn HeaderText="Adjuntos"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="24px">
                                    <ItemTemplate>
                                        <div class="AdjuntosCont">
                                            <asp:ImageButton ID="FileManagerIB" runat="server"
                                                ImageUrl="~/Images/Neutral/adjuntar.png" Width="24px"
                                                CommandName="ACCIDENTADO"
                                                CommandArgument='<%# Eval("AccidentadoId") %>'
                                                OnCommand="FileManager_Command" />
                                            <asp:Label ID="Label2" CssClass="AdjuntosNumber" Text='<%# Eval("FileCountForDisplay") %>' runat="server" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
            <div>
                <span style="font-weight: bold;">Viendo Siniestros del </span>
                <asp:Label ID="LoadedFirstRecordLabel" runat="server" Text=""></asp:Label>
                <span style="font-weight: bold;">al </span>
                <asp:Label ID="LoadedNumRecordsLabel" runat="server" Text=""></asp:Label>
                <span style="font-weight: bold;">de un total de </span>
                <asp:Label ID="TotalDBRecordsLabel" runat="server" Text=""></asp:Label>
                <span style="font-weight: bold;">| Accidentados:</span>
                <asp:Label ID="TotalAccidentadosRecordsLabel" runat="server" Text=""></asp:Label>
                <%-- 
                <span style="font-weight: bold;">| Total Proforma:</span>
                <asp:Label ID="TotalPreliquidadoLabel" Visible="false" runat="server" Text=""></asp:Label>
                    --%>
                <span style="font-weight: bold;">| Total Facturado:</span>
                <asp:Label ID="TotalPagadoLabel" runat="server" Text=""></asp:Label>
                <%-- 
                <span style="font-weight: bold;">| Ahorro:</span>
                <asp:Label ID="AhorroLabel" runat="server" Text=""></asp:Label>
                    --%>
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

            <RedSalud:FileManager runat="server" ID="FileManager" />

            <asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
            <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
            <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="OrderByHF" runat="server" />
            <asp:HiddenField ID="UsuarioIdHF" runat="server" />
            <asp:HiddenField ID="SiniestroOpenHF" runat="server" />
        </div>
    </div>
    <div style="display: none;">
        <telerik:RadGrid ID="ExportToExcelRadGrid" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="false"
            AllowMultiRowSelection="False"
            Visible="false"
            OnItemCreated="ExportToExcelRadGrid_ItemCreated"
            OnExcelExportCellFormatting="ExportToExcelRadGrid_ExcelExportCellFormatting"
            BorderStyle="Double" BorderColor="#000" BorderWidth="1px">
            <ExportSettings ExportOnlyData="false" OpenInNewWindow="true"></ExportSettings>
            <MasterTableView ShowHeader="true" DataKeyNames="RowNumber">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                <Columns>
                    <telerik:GridBoundColumn DataField="RowNumber" HeaderText="Nro" />
                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="NOMBRE COMPLETO" />
                    <telerik:GridBoundColumn DataField="Genero" HeaderText="SEXO" />
                    <telerik:GridBoundColumn DataField="Edad" HeaderText="EDAD" />
                    <telerik:GridBoundColumn DataField="FechaNacimiento" HeaderText="FECHA DE NACIMIENTO" DataType="System.DateTime" DataFormatString="{0:dd-MM-yyyy}" />
                    <telerik:GridBoundColumn DataField="EstadoCivil" HeaderText="ESTADO CIVIL" />
                    <telerik:GridBoundColumn DataField="EsConductor" HeaderText="ES CONDUCTOR" />
                    <telerik:GridBoundColumn DataField="LicenciaConducir" HeaderText="TIENE LICENCIA" />
                    <telerik:GridBoundColumn DataField="CarnetIdentidad" HeaderText="CARNET IDENTIDAD O NIT" />
                    <telerik:GridBoundColumn DataField="TipoAccidentado" HeaderText="PASAJERO / PEATON" />
                    <telerik:GridBoundColumn DataField="EstadoAccidentado" HeaderText="ACCIDENTADO / FALLECIDO" />
                    <telerik:GridBoundColumn DataField="IdentificadorOperaciones" HeaderText="IDENTIFICADOR DE OPERACIÓN" />
                    <telerik:GridBoundColumn DataField="Creador" HeaderText="USUARIO" />
                    <telerik:GridBoundColumn DataField="NumeroRoseta" HeaderText="NÚMERO  DE ROSETA" />
                    <telerik:GridBoundColumn DataField="NumeroPoliza" HeaderText="Nº DE CERTIFICADO O PÓLIZA" />
                    <telerik:GridBoundColumn DataField="NombreTitular" HeaderText="TITULAR" />
                    <telerik:GridBoundColumn DataField="CITitular" HeaderText="CARNÉ DE IDENTIDAD O NIT DEL TITULAR" />
                    <telerik:GridBoundColumn DataField="FechaSiniestro" HeaderText="FECHA OCURRIDO SINIESTRO" DataType="System.DateTime" DataFormatString="{0:dd-MM-yyyy}" />
                    <telerik:GridBoundColumn DataField="Dia" HeaderText="DÍA" />
                    <telerik:GridBoundColumn DataField="Mes" HeaderText="MES" />
                    <telerik:GridBoundColumn DataField="Semana" HeaderText="SEMANA" />
                    <telerik:GridBoundColumn DataField="Gestion" HeaderText="GESTIÓN SOAT" />
                    <telerik:GridBoundColumn DataField="FechaDenuncia" HeaderText="FECHA DE DENUNCIA" DataType="System.DateTime" DataFormatString="{0:dd-MM-yyyy}" />
                    <telerik:GridBoundColumn DataField="TotalOcupantes" HeaderText="TOTAL OCUPANTES DEL VEHÍCULO" />
                    <telerik:GridBoundColumn DataField="CantidadHeridos" HeaderText="CANTIDAD DE HERIDOS" />
                    <telerik:GridBoundColumn DataField="CantidadFallecidos" HeaderText="CANTIDAD DE FALLECIDOS" />
                    <telerik:GridBoundColumn DataField="LugarDpto" HeaderText="LUGAR - DEPARTAMENTO" />
                    <telerik:GridBoundColumn DataField="LugarProvincia" HeaderText="LUGAR - PROVINCIA" />
                    <telerik:GridBoundColumn DataField="Zona" HeaderText="ZONA" />
                    <telerik:GridBoundColumn DataField="Sindicato" HeaderText="SINDICATO" />
                    <telerik:GridBoundColumn DataField="TipoVehiculo" HeaderText="TIPO DE VEHÍCULO" />
                    <telerik:GridBoundColumn DataField="Cilindrada" HeaderText="CILINDRADA" />
                    <telerik:GridBoundColumn DataField="NroChasis" HeaderText="NRO CHASIS" />
                    <telerik:GridBoundColumn DataField="NroMotor" HeaderText="NRO MOTOR" />
                    <telerik:GridBoundColumn DataField="Sector" HeaderText="SECTOR" />
                    <telerik:GridBoundColumn DataField="Placa" HeaderText="Nº PLACA DEL VEHÍCULO" />
                    <telerik:GridBoundColumn DataField="NombreES" HeaderText="NOMBRE ESTABLECIMIENTO DE SALUD" />
                    <telerik:GridBoundColumn DataField="FechaVisita" HeaderText="FECHA DE VISITA DEL INSPECTOR Y/O MEDICO AUDITOR" DataType="System.DateTime" DataFormatString="{0:dd-MM-yyyy}" />
                    <telerik:GridBoundColumn DataField="Grado" HeaderText="GRADO" />
                    <telerik:GridBoundColumn DataField="DiagnosticoPreliminar" HeaderText="DIAGNOSTICO PRELIMINAR DEL ACCIDENTADO" />
                    <telerik:GridBoundColumn DataField="Reservas" UniqueName="DecimalReserva" HeaderText="RESERVA ESTIMADA DE SINIESTRO BS" />
                    <telerik:GridBoundColumn DataField="Hospitalarios" UniqueName="DecimalHospitalarios" HeaderText="PROFORMA DE HOSPITALARIOS Bs" />
                    <telerik:GridBoundColumn DataField="Cirugia" UniqueName="DecimalCirugia" HeaderText="PROFORMA DE CIRUGÍA BS" />
                    <telerik:GridBoundColumn DataField="Ambulancias" UniqueName="DecimalAmbulancias" HeaderText="PROFORMA DE AMBULANCIAS Bs" />
                    <telerik:GridBoundColumn DataField="Laboratorios" UniqueName="DecimalLaboratorios" HeaderText="PROFORMA DE LABORATORIOS E IMÁGENES Bs" />
                    <telerik:GridBoundColumn DataField="Farmacia" UniqueName="DecimalFarmacia" HeaderText="PROFORMA DE FARMACIAS Bs" />
                    <telerik:GridBoundColumn DataField="Honorarios" UniqueName="DecimalHonorarios" HeaderText="PROFORMA DE HONORARIOS MÉDICOS Bs" />
                    <telerik:GridBoundColumn DataField="Ambulatorios" UniqueName="DecimalAmbulatorios" HeaderText="PROFORMA AMBULATORIOS Bs" />
                    <telerik:GridBoundColumn DataField="Osteosintesis" UniqueName="DecimalOsteosintesis" HeaderText="PROFORMA DE MATERIAL DE OSTEOSINTESIS Bs" />
                    <telerik:GridBoundColumn DataField="GastosReembolso" UniqueName="DecimalGastosReembolso" HeaderText="PROFORMA DE REEMBOLSO Bs" />
                    <telerik:GridBoundColumn DataField="SiniestrosPreliquidacion" UniqueName="DecimalSiniestrosPreliquidacion" HeaderText="MONTO SINIESTROS DE PROFORMA Bs" />
                    <telerik:GridBoundColumn DataField="GastosHospitalarios" UniqueName="DecimalHospitalarios" HeaderText="GASTOS HOSPITALARIOS Bs" />
                    <telerik:GridBoundColumn DataField="GastosCirugia" UniqueName="DecimalCirugia" HeaderText="GASTOS DE CIRUGÍA BS" />
                    <telerik:GridBoundColumn DataField="GastosAmbulancias" UniqueName="DecimalGastosAmbulancias" HeaderText="GASTOS DE AMBULANCIAS Bs" />
                    <telerik:GridBoundColumn DataField="GastosLaboratorios" UniqueName="DecimalGastosLaboratorios" HeaderText="GASTOS DE LABORATORIOS E IMÁGENES Bs" />
                    <telerik:GridBoundColumn DataField="GastosFarmacia" UniqueName="DecimalGastosFarmacia" HeaderText="GASTOS DE FARMACIAS Bs" />
                    <telerik:GridBoundColumn DataField="GastosHonorarios" UniqueName="DecimalGastosHonorarios" HeaderText="GASTOS DE HONORARIOS MÉDICOS Bs" />
                    <telerik:GridBoundColumn DataField="GastosAmbulatorios" UniqueName="DecimalGastosAmbulatorios" HeaderText="GASTOS AMBULATORIOS Bs" />
                    <telerik:GridBoundColumn DataField="GastosOsteosintesis" UniqueName="DecimalGastosOsteosintesis" HeaderText="GASTOS DE MATERIAL DE OSTEOSINTESIS Bs" />
                    <telerik:GridBoundColumn DataField="GastosReembolso" UniqueName="DecimalGastosReembolso" HeaderText="GASTOS DE REEMBOLSO Bs" />
                    <telerik:GridBoundColumn DataField="SiniestrosPagados" UniqueName="DecimalSiniestrosPagados" HeaderText="MONTO SINIESTROS PAGADOS Bs" />
                    <telerik:GridBoundColumn DataField="Ahorro" UniqueName="DecimalAhorro" HeaderText="AHORRO" />
                    <telerik:GridBoundColumn DataField="SaldoAFavor" UniqueName="DecimalSaldoAFavor" HeaderText="SALDO A FAVOR ASEGURADO" />
                    <telerik:GridBoundColumn DataField="ControlSaldos" HeaderText="CONTROL DE SALDOS" />
                    <telerik:GridBoundColumn DataField="EstadoSeguimiento" HeaderText="ESTADO ACTUAL DEL CASO" />
                    <telerik:GridBoundColumn DataField="Acuerdo" HeaderText="ACUERDO TRANSACCIONAL" />
                    <telerik:GridBoundColumn DataField="Rechazado" HeaderText="REPETICIÓN" />
                    <telerik:GridBoundColumn DataField="Observaciones" HeaderText="OBSERVACIONES" />
                    <telerik:GridBoundColumn DataField="TieneAdjuntos" HeaderText="TIENE ADJUNTOS" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div id="dialog-confirm" title="Exportar a MS Excel" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Escoja el formato para los decimales:</p>
    </div>
    <asp:Panel ID="NewGastoPanel" runat="server" ToolTip="" style="display: none;"
        DefaultButton="NGSaveLB">
        <div id="NGPFecha">
            <span class="label">Fecha:</span>
            <telerik:RadDatePicker runat="server" ID="NGFechaRDP" TabIndex="-1" Enabled="false"
                CssClass="normalField" Width="200px" DatePopupButton-Visible="false" 
                SelectedDate='<%# Artexacta.App.Configuration.Configuration.ConvertToClientTimeZone(DateTime.UtcNow) %>'>
            </telerik:RadDatePicker>
        </div>
        <div id="ProformaDiv" style="display:none;">
            <span class="label">Proforma:</span>
            <asp:DropDownList runat="server" ID="NGPreliquidacionDDL"
                CssClass="normalField"></asp:DropDownList>
        </div>
        <span class="label">Tipo de Gasto:</span>
        <asp:DropDownList runat="server" ID="NGTipoGastoDDL"
            CssClass="normalField">
            <asp:ListItem Text="Reserva" Value="RESERVA" />
            <asp:ListItem Text="Gastos Hospitalarios" Value="HOSPITALARIOS" />
            <asp:ListItem Text="Gastos de Cirugías" Value="CIRUGIA" />
            <asp:ListItem Text="Gastos de Ambulancias" Value="AMBULANCIAS" />
            <asp:ListItem Text="Gastos de Laboratorios e Imágenes" Value="LABORATORIOS" />
            <asp:ListItem Text="Gastos de Farmacia" Value="FARMACIAS" />
            <asp:ListItem Text="Gastos de Honorarios Profesionales" Value="HONORARIOS" />
            <asp:ListItem Text="Gastos de Ambulatorios" Value="AMBULATORIO" />
            <asp:ListItem Text="Gastos de Material de Osteosíntesis" Value="OSTEOSINTESIS" />
            <asp:ListItem Text="Gastos de Reembolso" Value="REEMBOLSO" />
        </asp:DropDownList>
        <div id="ProveedorDiv">
            <span class="label">Proveedor</span>
            <div class="normalField">
                <RedSalud:TagSelector runat="server" ID="ProveedorTags" CssClass="normalField" MaxTags="1"
                    Type="Proveedor" />
                <asp:TextBox ID="ProveedorDisable" runat="server" Enabled="false" />
                <asp:HiddenField runat="server" ID="ProveedorHF" />
            </div>
            <div class="validation">
                <span id="ProveedorTagsVal" style="display:none;color:#F00;">
                    Debe seleccionar el Proveedor.
                </span>
            </div>
        </div>
        <div id="fechaReciboFacturaDiv">
            <span id="fechaReciboFacturaLabel" class="label"></span>
            <telerik:RadDatePicker runat="server" ID="FechaReciboFacturaRDP" TabIndex="-1"
                CssClass="normalField" Width="200px">
            </telerik:RadDatePicker>
            <div class="validation">
                <span id="FechaReciboFacturaRFV" style="display:none;color:#F00;">Debe seleccionar la fecha.</span>
            </div>
        </div>
        
        <div id="NumeroReciboFacturaDiv">
            <span id="NumeroReciboFacturaLabel" class="label">:</span>
            <asp:TextBox runat="server" ID="NumeroReciboFacturaTextBox"
                CssClass="normalField" />
            <div class="validation">
                <span id="NumeroReciboFacturaRFV" style="display:none;color:#F00;">Debe ingresar el Número.</span>
            </div>
        </div>
        <div id="isFacturaDiv" style="margin-top: 5px;font-weight: bold; text-transform: uppercase;">
            <asp:CheckBox ID="IsFacturaCheckBox" Text="Es Factura" runat="server" Checked="true" />
        </div>

        <span class="label">Monto:</span>
        <asp:TextBox runat="server" ID="NGMontoTextBox"
            CssClass="normalField valDecimal" />
        <div class="validation">
            <asp:RequiredFieldValidator ID="NGMontoRFV" runat="server"
                ControlToValidate="NGMontoTextBox"
                ErrorMessage="Debe ingresar el monto del gasto."
                Display="Dynamic"
                ValidationGroup="stepNG">
            </asp:RequiredFieldValidator>
            <asp:CustomValidator ID="NGMontoREV" runat="server"
                ControlToValidate="NGMontoTextBox"
                ErrorMessage="Debe ingresar el monto del gasto."
                ClientValidationFunction="validationMonto"
                Display="Dynamic"
                ValidationGroup="stepNG">
            </asp:CustomValidator>
        </div>

        <div id="EstadoDiv" style="display:none;">
            <span class="label">Estado:</span>
            <asp:DropDownList runat="server" ID="EstadoDDL"
                CssClass="normalField">
                <asp:ListItem Selected="True" Text="APROBADO" Value="1" />
                <asp:ListItem Text="OBSERVADO" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="buttonsPanel">
            <div id="CopyToGastosDiv" style="display:none; margin-bottom: 20px;text-align:right;">
                <asp:HyperLink ID="CopyToGastosHL" Text="Copiar a Gastos" NavigateUrl="javascript:copyToGastos();" runat="server" />
            </div>
            <asp:LinkButton ID="NGSaveLB" runat="server"
                ValidationGroup="stepNG" OnClientClick="return validate_NGSaveLB();"
                CssClass="button" OnClick="NGSaveLB_Click">
                <span style="color: #FFF;">Guardar</span>
            </asp:LinkButton>
            <asp:HyperLink ID="HyperLink2" Text="Cancelar" NavigateUrl="javascript:CloseNGDialog();" runat="server" />
        </div>
        <asp:HiddenField runat="server" ID="TypeHF" />
        <asp:HiddenField runat="server" ID="SiniestroDetalleId" />
        <asp:HiddenField runat="server" ID="AccidentadoDetalleId" />
        <asp:HiddenField runat="server" ID="saveIdHF" />
        <asp:HiddenField runat="server" ID="saveGastoFacturadoId" />
        <asp:HiddenField runat="server" ID="PreliquidacionDetalleIdHF" />
    </asp:Panel>
    <asp:HiddenField ID="DecimalSimbolHF" runat="server" />
    <script type="text/javascript">
        var AddedValues = 0;
        function loadPreliquidacionDetalleId() {
            var ddl = $('#<%=NGPreliquidacionDDL.ClientID%>');
            var clear = true;
            $('td.PreliquidacionDetalle').each(function () {
                if (clear) {
                    ddl.html('');
                    clear = false;
                }
                var list = $(this).children('input[type=hidden]').val().split('#;#');
                for (var i = 0; i < list.length; i++) {
                    var value = list[i];
                    if (value != '') {
                        var text = value.split('###')[2];
                        var proveedor = value.split('###')[1];
                        value = value.split('###')[0];
                        var newOption = '<option value="' + value + '" proveedor="' + proveedor + '">'
                            + text + '</option>';
                        ddl.prepend(newOption);
                    }
                }
            });
            ddl.change(function () {
                var proveedor = $(this).children("option:selected").attr('proveedor');
                if (proveedor == undefined) {
                    var first = $(this).children().first();
                    proveedor = $(first).attr('proveedor');
                    $(this).val($(first).attr('value'));
                }
                $('#<%=ProveedorTags.ClientID%>').hide();
                $('#<%=ProveedorDisable.ClientID%>').show();
                $('#<%=ProveedorDisable.ClientID%>').val(proveedor);
                $('#<%=ProveedorHF.ClientID%>').val(proveedor);
                $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val($('#<%=NGPreliquidacionDDL.ClientID%>').val());

            }).change();
        }
        function validate_NGSaveLB() {
            if ($('#<%=NGTipoGastoDDL.ClientID%>').val() == 'RESERVA') {
                return true;
            }

            $('#FechaReciboFacturaRFV').hide();
            $('#NumeroReciboFacturaRFV').hide();
            $('#ProveedorTagsVal').hide();
            var valid = true;
            if ($find('<%=FechaReciboFacturaRDP.ClientID%>').isEmpty()) {
                $('#FechaReciboFacturaRFV').show();
                valid = false;
            }
            if ($('#<%=NumeroReciboFacturaTextBox.ClientID%>').val() == '') {
                $('#NumeroReciboFacturaRFV').show();
                valid = false;
            }
            var tags = <%=ProveedorTags.ClientID%>getSelectedTags();
            if (tags == undefined || tags == null) {
                tags = '';
            }

            if (tags == '' && $('#<%=ProveedorDisable.ClientID%>').val() == '') {
                $('#ProveedorTagsVal').show();
                valid = false;
            }
            return valid;
        }
        function validationMonto(source, arguments) {
            if ($('#<%= NGMontoTextBox.ClientID%>').val() > 0) {
                arguments.IsValid = true;
            } else {
                arguments.IsValid = false;
            }
        }
        function OpenNGDialog(Id, GE, AccidentadoId, SiniestroId, type) {
            try {

                if (AddedValues > 0) {
                    $("#<%=NGPreliquidacionDDL.ClientID%> option[value='" + AddedValues + "']").remove();
                }

                $('#NGPFecha').show();
                $('#ProveedorDiv').show();
                $('#<%=ProveedorTags.ClientID%>').show();
                $('#<%=ProveedorDisable.ClientID%>').hide();
                $('#<%=NGTipoGastoDDL.ClientID%>').prop('disabled', '');
                $('#<%=NGPreliquidacionDDL.ClientID%>').val('');
                $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val('');
                $('#<%=ProveedorDisable.ClientID%>').val('');
                $('#<%=ProveedorHF.ClientID%>').val('');
                var proveedorTags = $('#<%=ProveedorTags.ClientID%>');
                proveedorTags.tagit("fill", '');
                var DatePicker = $find('<%=NGFechaRDP.ClientID%>');
                $('#<%=TypeHF.ClientID%>').val(type);
                var res = $('#<%=NGTipoGastoDDL.ClientID%>').find('option[value="RESERVA"]');
                $('#CopyToGastosDiv').hide();
                if (type == 'R') {
                    $("#<%=NGTipoGastoDDL.ClientID%> option").removeAttr('selected');
                    if ($(res).length > 0) $(res).remove();
                    $('#fechaReciboFacturaLabel').html('Fecha de Recepción');
                    $('#NumeroReciboFacturaLabel').html('Número de la Factura');
                    $('#isFacturaDiv').hide();
                    $('#EstadoDiv').hide();
                    $('#ProformaDiv').show();
                    loadPreliquidacionDetalleId();
                } else if (type == 'RES') {
                    if ($(res).length <= 0) {
                        var newOption = '<option value="RESERVA" selected="selected">Reserva</option>';
                        $('#<%=NGTipoGastoDDL.ClientID%>').prepend(newOption);
                    }
                    $('#<%=NGTipoGastoDDL.ClientID%>').prop('disabled', 'disabled');
                    $('#NGPFecha').hide();
                    $('#ProformaDiv').hide();
                } else {
                    if ($(res).length > 0) $(res).remove();
                    $('#<%=NGTipoGastoDDL.ClientID%> option').first().attr('selected', 'selected');
                    $('#fechaReciboFacturaLabel').html('Fecha de Recepción');
                    $('#NumeroReciboFacturaLabel').html('Número Recibo / Factura');
                    $('#isFacturaDiv').show();
                    //$('#EstadoDiv').show();
                    $('#ProformaDiv').hide();
                }
            var title = "";
            var DatePicker2 = $find('<%=FechaReciboFacturaRDP.ClientID%>');
                if (Id <= 0) {
                    title = (type == 'R' ? 'Nuevo Gasto Facturado' : 'Nuevo Gasto Medico');
                    DatePicker.set_selectedDate(new Date());
                    DatePicker2.set_selectedDate(new Date());
                    $('#<%=NumeroReciboFacturaTextBox.ClientID%>').val('');
                    $('#<%=NGMontoTextBox.ClientID%>').val('0.00');
                    $('#<%=saveIdHF.ClientID%>').val('0');
                    $('#<%=EstadoDDL.ClientID%>').val('1');
                    //$('<=NGFechaRDP.ClientID%>').removeClass('disable');
                } else {
                    $('#<%=saveIdHF.ClientID%>').val(Id);
                    title = (type == 'R' ? 'Modificar Gasto Facturado' : 'Modificar Proforma');
                    if (type == 'G') {
                        if ($('#<%=NGPreliquidacionDDL.ClientID%> option[value=' + Id + ']').length > 0) {
                            $('#CopyToGastosDiv').show();
                        }
                    }
                    var date = $(GE).attr('fecha').split('/');
                    DatePicker.set_selectedDate(new Date(date[2], date[1] - 1, date[0]));
                    date = $(GE).attr('fechaReciboFactura').split('/');
                    DatePicker2.set_selectedDate(new Date(date[2], date[1] - 1, date[0]));
                    addPreliquidacionText($(GE).attr('preliquidacionId'));
                    $('#<%= NumeroReciboFacturaTextBox.ClientID%>').val($(GE).attr('NumeroReciboFactura'));
                    $('#<%=IsFacturaCheckBox.ClientID%>').prop('checked', $(GE).attr('isFactura') == 'True');
                    $('#<%=NGTipoGastoDDL.ClientID%>').val($(GE).attr('tipo'));
                    $('#<%=NGMontoTextBox.ClientID%>').val(parseInt($(GE).attr('monto')).toFixed(2));
                    $('#<%=EstadoDDL.ClientID%>').val($(GE).attr('estado') == 'True' ? '1' : '0');
                    if (type == 'R') {
                        $('#<%=NGPreliquidacionDDL.ClientID%>').val($(GE).attr('preliquidacionId'));
                        $('#<%=PreliquidacionDetalleIdHF.ClientID%>').val($(GE).attr('preliquidacionId'));
                        $('#<%=ProveedorDisable.ClientID%>').val($(GE).attr('proveedor'));
                        $('#<%=ProveedorHF.ClientID%>').val($(GE).attr('proveedor'));
                    } else {
                        proveedorTags.tagit("fill", [{ label: $(GE).attr('proveedor'), value: $(GE).attr('proveedor') }]);
                        proveedorTags.next().val($(GE).attr('proveedor'));
                    }
                    //$('<=NGFechaRDP.ClientID%>').addClass('disable');
                }
                if (AccidentadoId) {
                    $('#<%= AccidentadoDetalleId.ClientID %>').val(AccidentadoId);
                }

                if (SiniestroId) {
                    $('#<%= SiniestroDetalleId.ClientID %>').val(SiniestroId);
                }
                
                $('#<%=NewGastoPanel.ClientID%>').dialog({ modal: true, resizable: false });
                $('#<%=NewGastoPanel.ClientID%>').dialog("option", "title", title);
                $('.ui-widget-overlay').height($(document).height());
                $('form').append($('.ui-dialog'));
                $('.validation span').hide();
            } catch (q) { console.log(q); }
            return false;
        }
        function ShowNGDialog() {
            $('#<%=NewGastoPanel.ClientID%>').dialog({ modal: true, resizable: false });
            $('.ui-widget-overlay').height($(document).height());
            $('form').append($('.ui-dialog'));
        }
        function CloseNGDialog() {
            $('#<%=NewGastoPanel.ClientID%>').dialog('destroy');
        }
        function OnColumnClick(sender, args) {
            if (args.get_gridColumn().get_uniqueName().match('^(TemplateColumn.*)|(NoSort.*)$') == null) {
                //var masterTable = $find("<%= SiniestroRadGrid.ClientID %>").get_masterTableView();
                var newOrderBy = args.get_gridColumn().get_uniqueName();
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
        function chooseDecimalSeparator() {
            try {
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 150,
                    modal: true,
                    buttons: {
                        'Coma [,]': function () {
                            setTarget();
                            $('#<%= DecimalSimbolHF.ClientID%>').val(',');
                            __doPostBack('<%= realExport.ClientID.Replace("_", "$")%>', '');
                            $(this).dialog('close');
                        },
                        'Punto [.]': function () {
                            setTarget();
                            $('#<%= DecimalSimbolHF.ClientID%>').val('.');
                            __doPostBack('<%= realExport.ClientID.Replace("_", "$")%>', '');
                            $(this).dialog('close');
                        }
                    }
                });
            } catch (q) { }
            return false;
        }
        function setTarget() {
            document.forms[0].target = "_blank";
            setTimeout(function () {
                document.forms[0].target = "_self";
            }, 1000);
        }
        $(document).ready(function () {
            if ($('#ClienteSOATContainer select option').length <= 1) {
                $('#ClienteSOATContainer select').hide();
                $('#ClienteSOATContainer').prepend('<span class="fakeInput" style="width:342px;">' + $('#ClienteSOATContainer select option').html() + '</span>');
            } else {
                $('#ClienteSOATContainer select').show();
            }
        });
    </script>
</asp:Content>
