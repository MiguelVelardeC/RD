<%@ Page Title="Proveedor Desgravamen" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProveedorEstudiosDetalle.aspx.cs" Inherits="Desgravamen_ProveedorEstudios" %>

<%@ Register TagPrefix="RedSalud" TagName="FileUpload" Src="~/UserControls/FileUpload.ascx" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="RedSalud" TagName="SearchControl" %>

<%@ Register Src="~/UserControls/PagerControl.ascx" TagName="PagerControl" TagPrefix="RedSalud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
     .linkButton {
     background:none!important;
     border:none; 
     padding:0!important;
     font: inherit;
    }
    .linkButton:hover{
         text-decoration:underline;
    }
    .btnCategoria {
        background-image:none;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">

    <div class="oneColumn">
        <div class="frame">
            <div class="columnHead">
                <span class="title">Proveedor Desgravamen</span>
            </div>
            <div class="columnContent">
                <div class="contentMenu">
                    <asp:Button runat="server" id="Button1" CssClass="linkButton" Text="Volver al Detalle del Proveedor" OnClick="BackButton_Click" />                    
                </div>
                <div class="contentMenu">
                    <asp:LinkButton ID="BtnAgregarEstudio" runat="server" CssClass="button" OnClick="BtnAgregarEstudio_Click">
                        <span>Agregar un Estudio</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="BtnCrearEstudio" runat="server" CssClass="button" OnClick="BtnCrearEstudio_Click">
                        <span>Administrar Estudio</span>
                    </asp:LinkButton>
                </div>
                <asp:HiddenField id="ProveedorHiddenField" Value="0" runat="server"/>
                <asp:HiddenField id="ClienteIdHiddenField" Value="0" runat="server"/>
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
                    ImageErrorUrl="~/images/exclamation.png"
                    Visible="false" />
                <asp:Panel id="PanelBusqueda" runat="server" DefaultButton="botonDefecto">
                    <asp:Button ID="botonDefecto" Style="display:none;" runat="server" OnClick="BusquedaButtonLink_Click"/>
                    <asp:Label runat="server">CLIENTES: </asp:Label>                
                    <asp:DropDownList ID="ClienteComboBox" runat="server">
                    </asp:DropDownList>
                    <asp:Label runat="server">REQUIERE CITA: </asp:Label>                
                    <asp:DropDownList ID="RequiereCitaComboBox" runat="server">
                        <asp:ListItem Text="TODOS" Value="" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="NECESITA CITA" Value="@NECESITACITA 1"></asp:ListItem>
                        <asp:ListItem Text="NO NECESITA CITA" Value="@NECESITACITA 0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label runat="server">ESTADO: </asp:Label>                
                    <asp:DropDownList ID="EstadoComboBox" runat="server">
                        <asp:ListItem Text="Todos" Value=""> </asp:ListItem>
                        <asp:ListItem Text="HABILITADO" Value="@ESTADO 0" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="DESHABILITADO" Value="@ESTADO 1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="BusquedaButtonLink" runat="server" CssClass="button" OnClick="BusquedaButtonLink_Click">
                        <span>Buscar</span>
                    </asp:LinkButton>
                </asp:Panel>
                <div style="margin-top:10px;">                     
                </div> 
                <telerik:RadGrid ID="ProveedorEstudiosDesgravamenGridView" runat="server"
                    AutoGenerateColumns="false" DataSourceID="ProveedorEstudiosDesgravamenDataSource"
                    AllowPaging="false" 
                    OnItemCommand="ProveedorDesgravamenGridView_ItemCommand"
                    OnItemDataBound="ProveedorDesgravamenGridView_ItemDataBound"                    
                    AllowMultiRowSelection="False">
                    <MasterTableView ExpandCollapseColumn-Display="false"
                        CommandItemDisplay="None"
                        AllowSorting="false" 
                        OverrideDataSourceControlSorting="true">
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No existen Estudios Registrados para este Proveedor</div>
                        </NoRecordsTemplate>
                        <AlternatingItemStyle BackColor="#E8F1FF" Font-Size="11px" />
                        <ItemStyle BackColor="#C9DFFC" Font-Size="11px" />
                        <HeaderStyle  Font-Size="11px" />
                        
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="DeletePA" runat="server" CssClass="BotonDelete"
                                        ToolTip="Eliminar Estudio para el Proveedor"
                                        CommandName="Delete"
                                        CommandArgument='<%# Eval("EstudioId").ToString()+":"+Eval("ClienteId") %>'
                                        ImageUrl="~/Images/Neutral/delete.png" Width="18px"
                                        Visible="false"  />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:ImageButton ID="VerPA" runat="server"
                                        ToolTip="Ver Estudio Proveedor Desgravamen"
                                        CommandName="VerPA"
                                        CommandArgument='<%# Eval("EstudioId").ToString() + ":" + Eval("ClienteId") %>'
                                        ImageUrl="~/Images/Neutral/search32.png" Width="18px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                                                        
                            <telerik:GridBoundColumn UniqueName="EstudioNombre" DataField="EstudioNombre"
                                HeaderText="Estudio" />
                            <telerik:GridBoundColumn UniqueName="NecesitaCitaEstado" DataField="NecesitaCitaEstado"
                                HeaderText="Requiere Cita" />
                            <telerik:GridBoundColumn UniqueName="DeshabilitadoEstado" DataField="DeshabilitadoEstado"
                                HeaderText="Estado" />  
                            <telerik:GridBoundColumn UniqueName="HoraInicio" DataField="HoraInicio"
                                HeaderText="Hora Inicial" />  
                            <telerik:GridBoundColumn UniqueName="HoraFin" DataField="HoraFin"
                                HeaderText="Hora Final" />                       
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
                <asp:ObjectDataSource ID="ProveedorEstudiosDesgravamenDataSource" runat="server"
                    TypeName="Artexacta.App.Desgravamen.BLL.ProveedorEstudiosBLL"
                    SelectMethod="GetProveedorEstudiosBySearch"
                    OnSelected="ProveedorEstudiosDesgravamenDataSource_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SearchPA" PropertyName="Sql" Name="varSql" Type="String" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="PageSize" Name="pageSize" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" PropertyName="CurrentRow" Name="firstRow" Type="Int32" />
                        <asp:ControlParameter ControlID="Pager" Name="totalRows" PropertyName="TotalRows" Type="Int32" Direction="Output" />
                        <asp:ControlParameter ControlID="ProveedorHiddenField" PropertyName="Value" Name="intProveedorId" Type="Int32" />
                        <asp:ControlParameter ControlID="ClienteIdHiddenField" PropertyName="Value" Name="intClienteId" Type="Int32" />                    
                    </SelectParameters>
                </asp:ObjectDataSource>

                <RedSalud:PagerControl ID="Pager" runat="server" 
                    PageSize="20" 
                    CurrentRow="0" 
                    InvisibilityMethod="PropertyControl" 
                    OnPageChanged="Pager_PageChanged" />
                <div class="clear"></div>
                <%-- 
                <div class="buttonsPanel">
                    <asp:LinkButton ID="SaveButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveButton_Click">
                        <span>Guardar y Volver al Listado</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="SaveAndContinueButton" runat="server" ValidationGroup="PA" CssClass="button"
                        OnClick="SaveAndContinueButton_Click">
                        <span>Guardar y Configurar Estudios</span>
                    </asp:LinkButton>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Desgravamen/ProveedorDesgravamenLista.aspx" CssClass="secondaryButton"
                        Text="Cancelar">
                    </asp:HyperLink>
                </div>
                --%>
                <asp:HiddenField Id="IsUpdate" Value="0" runat="server" />                                
                <asp:HiddenField Id="InsertEstudioValue" Value="0" runat="server" />
                <asp:HiddenField Id="InsertClienteValue" Value="0" runat="server" />
                <asp:HiddenField ID="HoraInicialComboBoxHiddenField" Value="0" runat="server" />
                <asp:HiddenField ID="HoraFinalComboBoxHiddenField" Value="0" runat="server" />
                <div id="PopupSeleccionHora" style="display: none;">
                    <telerik:RadWindowManager ID="WinMgr" runat="server" EnableViewState="false" DestroyOnClose="true">
                        <Windows>
                            <telerik:RadWindow ID="SeleccionEstudioProveedor" runat="server" Width="300px" Height="350px"
                                Title="AGREGAR ESTUDIO" DestroyOnClose="true"
                                Modal="true" VisibleStatusbar="false" CssClass="radWin" Behaviors="Close,Move"
                                VisibleOnPageLoad="false" 
                                KeepInScreenBounds="true" OnClientClose="OnClientCloseHandler" OnClientShow="OnClientOpenHandler"><%--OnClientShow="SeleccionHoraWindow_OnClientShow" OnLoad="SeleccionHoraWindow_Load" --%>
                                <ContentTemplate>
                                    <asp:Panel ID="FechaYHoraPanel" runat="server" Style="padding-left:14%; padding-right:6%;" Visible="true">                                
                                        <asp:HiddenField Id="EstudioProveedorHiddenId" Value="0" runat="server" />
                                        <asp:HiddenField Id="ClienteEstudioProveedorHiddenId" Value="0" runat="server" />
                                        <asp:HiddenField Id="EstadoEstudioProveedorHiddenId" Value="FALSE" runat="server" />
                                        <asp:HiddenField Id="NecesitaCitaEstudioProveedorHiddenId" Value="FALSE" runat="server" />
                                        <span class="label">Cliente</span>
                                        <telerik:RadComboBox  RenderMode="Native" Width="200px" ID="ClientesEstudioComboBox" runat="server">
                                        </telerik:RadComboBox>
                                        <span id="ClienteSpan" runat="server" visible="false"></span>
                                        <%-- 
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="EstudioComboBox"
                                                ErrorMessage="Se debe seleccionar un Estudio"
                                                ValidationGroup="ESTUDIO"
                                                Display="Dynamic" />
                                        </div>
                                            --%>
                                        <span class="label">Estudio</span>
                                        <telerik:RadComboBox  RenderMode="Native" Width="200px" ID="EstudioComboBox" runat="server" EnableViewState="true">
                                        </telerik:RadComboBox>                                
                                        <span id="EstudioSpan" runat="server" visible="false"></span>
                                         <%-- 
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="EstudioComboBox"
                                                ErrorMessage="Se debe seleccionar un Estudio"
                                                ValidationGroup="ESTUDIO"
                                                Display="Dynamic" />
                                        </div>
                                        --%>                                     
                                        <span class="label">Estado</span>
                                        <asp:DropDownList ID="EstadoEstudioProveedorComboBox" Width="200px" runat="server">
                                            <asp:ListItem Text="HABILITADO" Value="FALSE"></asp:ListItem>
                                            <asp:ListItem Text="DESHABILITADO" Value="TRUE"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- 
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="EstadoEstudioProveedorComboBox"
                                                ErrorMessage="Se debe seleccionar un estado para el estudio"
                                                ValidationGroup="ESTUDIO"
                                                Display="Dynamic" />
                                        </div>
                                        --%>                                    
                                        <span class="label">Necesita Cita</span>
                                        <asp:DropDownList ID="NecesitaCitaEstudioComboBox" Width="200px" runat="server">
                                            <asp:ListItem Text="NO NECESITA CITA" Value="FALSE"></asp:ListItem>
                                            <asp:ListItem Text="NECESITA CITA" Value="TRUE"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="NecesitaCitaEstudioComboBox"
                                                ErrorMessage="Se debe seleccionar si el estudio necesita cita"
                                                ValidationGroup="ESTUDIO"
                                                Display="Dynamic" />
                                        </div>
                                        <div id="PanelRangoHoras" style="display:none;">
                                            <span class="label">Hora Inicial</span>
                                            <telerik:RadComboBox RenderMode="Native" Width="200px" ID="HoraInicialComboBox" runat="server" EnableViewState="true">
                                            </telerik:RadComboBox>
                                            <span class="label">Hora Final</span>
                                            <telerik:RadComboBox RenderMode="Native" Width="200px" ID="HoraFinalComboBox" runat="server" EnableViewState="true">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div style="clear:both;"></div>
                                        <div class="buttonsPanel" style="">
                                        <asp:LinkButton ID="GuardarEstudioProveedor" Width="200px" runat="server" CssClass="button" 
                                            ValidationGroup="ESTUDIO" CommandName="FechaHora" Style="width: 192px;" OnClick="GuardarEstudioProveedor_Click">
                                            <span>Guardar</span>
                                        </asp:LinkButton>
                                        </div>                                
                                    </asp:Panel>
                                    <div style="clear:both;"></div>                                           
                                </ContentTemplate>
                            </telerik:RadWindow>
                            <telerik:RadWindow ID="CreacionEstudioWindow" runat="server" Width="300px" Height="350px"
                                Title="CREAR ESTUDIO" DestroyOnClose="true"
                                Modal="true" VisibleStatusbar="false" CssClass="radWin" Behaviors="Close,Move"
                                VisibleOnPageLoad="false" 
                                KeepInScreenBounds="true" OnClientClose="OnEstudioFormClose" OnClientShow="OnEstudioWindow"><%--OnClientShow="SeleccionHoraWindow_OnClientShow" OnLoad="SeleccionHoraWindow_Load" --%>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Style="padding-left:14%; padding-right:6%;" Visible="true">                                
                                        <asp:HiddenField Id="AddEstudioIdHiddenField" Value="0" runat="server" />
                                        <asp:HiddenField Id="AddOldEstudioNombre" Value="" runat="server" />
                                        <asp:HiddenField Id="AddCategoriaIdHiddenField" Value="0" runat="server" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="CategoriaIdRadButton" runat="server" ToggleType="CheckBox" Checked="true"
                                            GroupName="StandardButton" BackColor="White" CssClass="btnCategoria" ButtonType="StandardButton" AutoPostBack="false" Width="200px" OnClientCheckedChanged="categoriaChanged">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState Text="ES ESTUDIO" Value="ESTUDIO"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState Text="ES CATEGORIA" Value="CATEGORIA" ></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                        <telerik:RadComboBox  RenderMode="Native" ID="EstudioManageCombo" Width="200px" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Value="0" Text="Insertar" />
                                                <telerik:RadComboBoxItem runat="server" Value="1" Text="Modificar" />
                                            </Items>
                                            
                                        </telerik:RadComboBox>
                                        <div id="panelEstudioUpdateCombo" style="display:none;">                                                                        
                                        <span class="label">Estudio</span>
                                        <telerik:RadComboBox  RenderMode="Native" Width="200px" ID="EstudioUpdateCombo" runat="server" EmptyMessage="--Seleccione un estudio--">                                            
                                        </telerik:RadComboBox> 
                                        </div>
                                        <div id="panelCategoriaUpdateCombo" style="display:none;">
                                        <span class="label">Categoria</span>
                                        <telerik:RadComboBox RenderMode="Native" Width="200px" ID="CategoriaUpdateComboBox" runat="server" EnableViewState="true">
                                        </telerik:RadComboBox>
                                        </div>      
                                        <span class="label">Nombre</span>
                                        <telerik:RadTextBox ID="EstudioNombreTextBox" EmptyMessage="Nombre del Estudio/Categoria" Width="200px" runat="server"></telerik:RadTextBox>
                                        <div class="validators">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="EstudioNombreTextBox"
                                                ErrorMessage="Se debe agregar un estudio"
                                                ValidationGroup="AdicionEstudio"
                                                Display="Dynamic" />
                                        </div>
                                        <div id="categoriaPanel" style="display:none;">
                                        <span class="label">Categoria</span>
                                        <telerik:RadComboBox RenderMode="Native" Width="200px" ID="CategoriaEstudioComboBox" runat="server" EnableViewState="true">
                                        </telerik:RadComboBox>
                                        </div>
                                        <div style="clear:both;"></div>
                                        <div class="buttonsPanel" style="">
                                        <asp:LinkButton ID="GuardarEstudio" Width="200px" runat="server" CssClass="button" 
                                            ValidationGroup="AdicionEstudio" CommandName="FechaHora" Style="width: 192px;" OnClick="GuardarEstudio_Click">
                                            <span>Guardar</span>
                                        </asp:LinkButton>
                                        </div>                                
                                    </asp:Panel>
                                    <div style="clear:both;"></div>                                           
                                </ContentTemplate>
                            </telerik:RadWindow>
                        </Windows>
                    </telerik:RadWindowManager>
            </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    $(document).ready(function () {
       
        
       
        $("#<%= HoraInicialComboBox.ClientID %>").change(function () {//.get_value();
            $("#<%= HoraInicialComboBoxHiddenField.ClientID %>").val($find("<%= HoraInicialComboBox.ClientID %>").get_value());
        });

        $("#<%= HoraFinalComboBox.ClientID %>").change(function () {
            $("#<%= HoraFinalComboBoxHiddenField.ClientID %>").val($find("<%= HoraFinalComboBox.ClientID %>").get_value());
        });


        $("#<%= CategoriaEstudioComboBox.ClientID %>").change(function () {
            var value = $find("<%= CategoriaEstudioComboBox.ClientID %>").get_value();
            $("#<%= AddCategoriaIdHiddenField.ClientID %>").val(value);
        });

        $("#<%= CategoriaUpdateComboBox.ClientID %>").change(function () {
            var CategoriaId = $find("<%= CategoriaUpdateComboBox.ClientID %>").get_value();
            if (CategoriaId != "0") {
                $.ajax({
                    type: "POST",
                    url: "ProveedorEstudiosDetalle.aspx/GetCategoria",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        'CategoriaId': CategoriaId
                    }),
                    dataType: "json",
                    success: function (result) {
                        //alert("good");
                        if (result.d != null) {
                            $("#<%= AddEstudioIdHiddenField.ClientID %>").val(result.d.EstudioId);
                            $("#<%= AddCategoriaIdHiddenField.ClientID %>").val(result.d.CategoriaId);
                            $("#<%= EstudioNombreTextBox.ClientID %>").val(result.d.EstudioNombre);
                        } else {
                            alert("No se pudo extraer la informacion del Estudio");
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //alert(xhr.status);
                        //alert(thrownError);
                        $('.HorarioInput').val('');
                    }
                });
            }
        });

        $("#<%= EstudioUpdateCombo.ClientID %>").change(function () {
            var EstudioId = $find("<%= EstudioUpdateCombo.ClientID %>").get_value();
            if (EstudioId != "0") {
                $.ajax({
                    type: "POST",
                    url: "ProveedorEstudiosDetalle.aspx/GetEstudio",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        'EstudioId': EstudioId
                    }),
                    dataType: "json",
                    success: function (result) {
                        //alert("good");
                        if (result.d != null) {
                            $("#<%= AddEstudioIdHiddenField.ClientID %>").val(result.d.EstudioId);
                            $("#<%= AddCategoriaIdHiddenField.ClientID %>").val(result.d.CategoriaId);
                            $("#<%= EstudioNombreTextBox.ClientID %>").val(result.d.EstudioNombre);
                            $("#<%= AddOldEstudioNombre.ClientID %>").val(result.d.EstudioNombre);
                            var combo = $find("<%= CategoriaEstudioComboBox.ClientID %>");
                            var itm = combo.findItemByValue(result.d.CategoriaId);
                            itm.select();
                        } else {
                            alert("No se pudo extraer la informacion del Estudio");
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //alert(xhr.status);
                        //alert(thrownError);
                        $('.HorarioInput').val('');
                    }
                });
            }
        });
        $("#<%= EstudioManageCombo.ClientID %>").change(function () {
            if ($find("<%= EstudioManageCombo.ClientID %>").get_value() == '0') {
                resetAddFormValues();
                if ($find("<%= CategoriaIdRadButton.ClientID %>")._checked) {//es Estudio                                        
                    $("#panelEstudioUpdateCombo").slideUp();
                } else {
                    $("#panelCategoriaUpdateCombo").slideUp();
                }
                //$("#panelEstudioUpdateCombo").slideUp();
            } else {
                //resetAddFormValues();
                if ($find("<%= CategoriaIdRadButton.ClientID %>")._checked) {//es Estudio                                        
                    $("#panelEstudioUpdateCombo").slideDown();
                } else {
                    $("#panelCategoriaUpdateCombo").slideDown();
                }
                $("#<%= AddEstudioIdHiddenField.ClientID %>").val("0");
                $("#<%= AddCategoriaIdHiddenField.ClientID %>").val("0");
                $("#<%= EstudioNombreTextBox.ClientID %>").val("");
                $("#<%= AddOldEstudioNombre.ClientID %>").val("");
            }
            //resetAddFormValues();
        });

        $('.BotonDelete').click(function () {
            return confirm('Está seguro que quiere eliminar el Estudio para el Proveedor? ');
        });
        $("#<%= ClienteComboBox.ClientID %>").change(function () {

            $("#<%= ClienteIdHiddenField.ClientID %>").val($(this).val());
        });

        $("#<%= ClientesEstudioComboBox.ClientID %>").change(function () {

            var clienteId = $find("<%= ClientesEstudioComboBox.ClientID %>");
            $("#<%= InsertClienteValue.ClientID %>").val(clienteId.get_value());
        });

        $("#<%= EstudioComboBox.ClientID %>").change(function () {

            var estudioId = $find("<%= EstudioComboBox.ClientID %>"); 
            $("#<%= InsertEstudioValue.ClientID %>").val(estudioId.get_value());
        });
        $("#<%= NecesitaCitaEstudioComboBox.ClientID %>").change(function () {

            if ($(this).val() == "TRUE") {
                $("#PanelRangoHoras").slideDown();
            } else {
                $("#PanelRangoHoras").slideUp();
            }
        });

        $("#<%= GuardarEstudioProveedor.ClientID %>").click(function () {


            if (ValidateEstudioProveedorForm()) {
                if ($("#<%= NecesitaCitaEstudioComboBox.ClientID %>").val() == "TRUE") {
                    $.ajax({
                        type: "POST",
                        url: "ProveedorEstudiosDetalle.aspx/GetCollisions",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({
                            'ProveedorId': $("#<%= ProveedorHiddenField.ClientID %>").val(),
                            'EstudioId': ($("#<%= EstudioProveedorHiddenId.ClientID %>").val() != "0") ? $("#<%= EstudioProveedorHiddenId.ClientID %>").val() : $find("<%= EstudioComboBox.ClientID %>").get_value(),
                            'ClienteId': ($("#<%= ClienteEstudioProveedorHiddenId.ClientID %>").val() != "0") ? $("#<%= ClienteEstudioProveedorHiddenId.ClientID %>").val() : $find("<%= ClientesEstudioComboBox.ClientID %>").get_value(),
                            'HoraInicio': $find("<%= HoraInicialComboBox.ClientID %>").get_value(),
                            'HoraFin': $find("<%= HoraFinalComboBox.ClientID %>").get_value()
                        }),
                        dataType: "json",
                        success: function (result) {
                            //alert("good");
                            if (result.d != null) {
                                if (result.d.OtherMessage != "ERROR") {
                                    if (confirm('Existe cruce de horarios con este Estudio con el cliente: ' + result.d.ClienteNombre + ' ' + ((result.d.OtherMessage != null)? result.d.OtherMessage: '') + ', Desea Continuar?')) {
                                        eval($("#<%= GuardarEstudioProveedor.ClientID %>").attr("href"));
                                    }
                                } else {
                                    alert('No se pudo verificar las colisiones para el horario');
                                }
                            } else {
                                eval($("#<%= GuardarEstudioProveedor.ClientID %>").attr("href"));
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                } else {
                    eval($("#<%= GuardarEstudioProveedor.ClientID %>").attr("href"));
                }
            }

            return false;
        });
        
        $('#aaaa').blur(function () {

            var ci = $(this).val();

            $.ajax({
                type: "POST",
                url: "<%=ResolveClientUrl("~/AutoCompleteWS/ComboBoxWebServices.asmx") %>/CargarPAConCI",
                contentType: "application/json; charset=utf-8",
                data: { 'ci': ci },
                dataType: "json",
                success: function (result) {
                    alert('good');
                }
            });
        });
    });
    function OnClientCloseHandler(sender, args) {
        $("#<%= EstudioProveedorHiddenId.ClientID %>").val("0");
        $("#<%= ClienteEstudioProveedorHiddenId.ClientID %>").val("0"); 
        $("#<%= IsUpdate.ClientID %>").val("0");
        $("#<%= InsertClienteValue.ClientID %>").val("0");
        $("#<%= InsertEstudioValue.ClientID %>").val("0");
        $("#<%= NecesitaCitaEstudioComboBox.ClientID %>").val("FALSE");
        $("#<%= HoraInicialComboBoxHiddenField.ClientID %>").val("0");
        $("#<%= HoraFinalComboBoxHiddenField.ClientID %>").val("0");
    } 
    function OnClientOpenHandler(sender, args) {
        if ($("#<%= NecesitaCitaEstudioComboBox.ClientID %>").val() == "TRUE") {
            $("#PanelRangoHoras").slideDown();
        }

    }

    function categoriaChanged(sender, args) {

        if (sender._checked) {
            var insertFlag = $find("<%= EstudioManageCombo.ClientID %>").get_value();
            if (insertFlag != "0") {
                $("#panelEstudioUpdateCombo").slideDown();
                $("#panelCategoriaUpdateCombo").slideUp();
            } else {
                $("#panelEstudioUpdateCombo").hide();
                $("#panelCategoriaUpdateCombo").hide();
            }

            $("#categoriaPanel").slideDown();
        } else {
            var insertFlag = $find("<%= EstudioManageCombo.ClientID %>").get_value();
            if (insertFlag != "0") {
                $("#panelEstudioUpdateCombo").slideUp();
                $("#panelCategoriaUpdateCombo").slideDown();
            } else {
                $("#panelEstudioUpdateCombo").hide();
                $("#panelCategoriaUpdateCombo").hide();
            }
            $("#categoriaPanel").slideUp();
        }
    }

    function OnEstudioWindow(sender, args) {
        var categoriaCombo = $find("<%= CategoriaIdRadButton.ClientID %>");
        if (categoriaCombo._checked) {
            $("#categoriaPanel").slideDown();
        } else {
            $("#categoriaPanel").slideUp();
        }
    }

    function resetAddFormValues() {
        $("#<%= AddEstudioIdHiddenField.ClientID %>").val("0");
        $("#<%= AddCategoriaIdHiddenField.ClientID %>").val("0");
        $("#<%= EstudioNombreTextBox.ClientID %>").val("");

        var comboEstudio = $find("<%= EstudioUpdateCombo.ClientID %>");
        var itm = comboEstudio.findItemByValue("0");
        itm.select();

        var comboCategoria = $find("<%= CategoriaUpdateComboBox.ClientID %>");
        var itm2 = comboEstudio.findItemByValue("0");
        itm2.select();

        var comboManage = $find("<%= EstudioManageCombo.ClientID %>");
        var itm3 = comboManage.findItemByValue("0");
        itm3.select();
    } 
    function OnEstudioFormClose() {
        resetAddFormValues();
    }

    function ValidateEstudioProveedorForm() {
        var ObjInicio = $find("<%= HoraInicialComboBox.ClientID %>").get_value();
        var ObjFin = $find("<%= HoraFinalComboBox.ClientID %>").get_value();
        var arrInicio = ObjInicio.split(":");
        var DateInicio = new Date('2016', '01' - 1, '01', arrInicio[0], arrInicio[1], '00');
        var arrFin = ObjFin.split(":");
        var DateFin = new Date('2016', '01' - 1, '01', arrFin[0], arrFin[1], '00');

        if ($("#<%= NecesitaCitaEstudioComboBox.ClientID %>").val() == "TRUE") {
            if ((DateFin > DateInicio) && ObjInicio != "00:00:00" && ObjFin != "00:00:00") {
                return true;
            } else {
                alert("La 'Hora Inicial' debe ser menor a la 'Hora Final' ");
                return false;
            }

            if (ObjInicio == "00:00:00" || ObjFin == "00:00:00") {
                return false;
                alert("El rango no puede contener variables vacias");
            }
        }

        return true;
    }

    $telerik.$(document).ready(function () {

    });
</script>
</asp:Content>

