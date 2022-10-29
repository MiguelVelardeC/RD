
<%@ Page Title="Reporte de Siniestralidad" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteSiniestralidad.aspx.cs" Inherits="Reportes_ReporteSiniestralidad" %>
<%@ Register Src="~/UserControls/SearchUserControl/SearchControl.ascx" TagPrefix="search" TagName="SearchControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="oneColumn">
         <div class="frame">
            <div class="columnHead">
                <span id="ReporteSiniestralidadTitle" runat="server" class="title">Lista de Pacientes - Reporte Siniestralidad</span>
            </div>
             <div class="columnContent">
                 <asp:Panel id="AdminPanel" runat="server" CssClass="PanelAdmin" style="font-size: 12px;" DefaultButton="boton"> 
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                           <span style="margin-left:30px;">FILTROS DE ADMINISTRACION</span>
                        </h3>
                    </a>
                    <div id="Contents" style="padding:1em 0.5em;">
                    CLIENTE:
                    <asp:DropDownList ID="clientesComboBox" runat="server"   AutoPostBack="false" 
                    
                        Style="margin-left:3px;" Height="22px" Width="150px">
                        <asp:ListItem Text="Todos" Value="0"> </asp:ListItem>
                    
                  
                        </asp:DropDownList>

                    RANGO DE FECHAS: 
                    <telerik:RadDatePicker ID="FechaInicio" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="120px"></telerik:RadDatePicker>
                    <telerik:RadDatePicker ID="FechaFin" runat="server" DateInput-EmptyMessage="Fecha Final" Width="120px"></telerik:RadDatePicker> 

                    TIPO BUSQUEDA:
                        <asp:DropDownList ID="TipoBusquedaComboBox" runat="server" CssClass="smallField"
                         AutoPostBack="false">
                        </asp:DropDownList>
                   
                   <div class="clear" style="margin-bottom: 5px;"></div>

                   CIUDAD:
                   <asp:DropDownList ID="ciudadComboBox" runat="server"
                       
                        DataValueField="CiudadId"
                        DataTextField="Nombre"
                                            Width="150px"
                                            AutoPostBack="false"
                                            Style="margin-left: 3px;"
                          >
                          <asp:ListItem Text="Todos" Value="0"></asp:ListItem>                 
                   </asp:DropDownList>
                   
                   NOMBRE:
                   <telerik:RadTextBox ID="NombreText" Text="" EmptyMessage="Nombre del Asegurado" runat="server" Width="180px" />

                   CARNET DE IDENTIDAD:
                   <telerik:RadTextBox ID="CarnetIDText" Text="" EmptyMessage="C.I. del Asegurado" runat="server" Width="180px" />
                            
                        <br />
                        <br /> 
                  
                  <asp:LinkButton ID="btnSearch" runat="server" CssClass="button"
                   OnClick="btnSearch_Click" >
                    <span>BUSCAR</span>
                    </asp:LinkButton>  
                                                                          
                    </div>                                 
                    <asp:Button id="boton" Text="" Style="display:none;" runat="server"  />

                    
                </asp:Panel>
                  <div class="clear" style="margin-bottom: 5px;"></div>


                <telerik:RadGrid ID="ReporteSiniestralidadGrid" runat="server"
                    AutoGenerateColumns="false" 
                    OnExcelExportCellFormatting="ReporteSiniestralidadRadGrid_ExcelExportCellFormatting"     
                    OnItemCommand="ReporteSiniestralidadGrid_ItemCommand"
                    OnItemDataBound="ReporteSiniestralidadGrid_ItemDataBound"
                    AllowPaging="false"
                    AllowSorting="false"
                    MasterTableView-DataKeyNames="CasoId">

                   <MasterTableView  >
                       
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Reportes Siniestralidad registrados</div>
                        </NoRecordsTemplate>
                        
                        
                        <Columns>
                          <telerik:GridTemplateColumn  DataField="CasoId" Visible="true" HeaderText="VER" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" ItemStyle-Width="24px" HeaderStyle-Width="80px" HeaderStyle-Font-Size="12px">
                            <ItemTemplate>
                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/select.png"
                                    CommandArgument='<%# Eval("CasoId") +";"+ Eval("ClienteId") +";"+ Eval("CedulaIdentidad") +";"+ (String.Format("{0:yyyy/MM/dd}", Eval("FechaIni")) ?? "") +";"+ (String.Format("{0:yyyy/MM/dd}", Eval("FechaFin")) ?? "") %> '
                                    Width="28px" CommandName="Select" 
                                    ToolTip="VER"></asp:ImageButton>
                            </ItemTemplate>
                          </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn  DataField="NombrePaciente" Visible="true"
                                HeaderText="Paciente" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" > </telerik:GridBoundColumn> 
                             <telerik:GridBoundColumn  DataField="NombreCliente" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true"  HeaderStyle-Font-Size="12px"
                                HeaderText="Cliente" />
                             <telerik:GridBoundColumn  DataField="CedulaIdentidad" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="Cedula de Identidad" />
                             <telerik:GridBoundColumn  DataField="Relacion" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="Relación" />
                             <telerik:GridBoundColumn  DataField="NumeroPoliza" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="Poliza" />
                             <telerik:GridDateTimeColumn  DataField="FechaIni" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                             <telerik:GridDateTimeColumn  DataField="FechaFin" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}" />
                             <telerik:GridBoundColumn DataField="CasoId" HeaderText="CasoId" Visible="false" />                        
                       </Columns>
                    </MasterTableView>               
                </telerik:RadGrid>
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
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="PreevFastButton" runat="server" OnClick="AnteriorRapidoButton_Click">
                        <asp:Label ID="Label3" Text="-5 Páginas" runat="server" />
              
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="PreevButton" runat="server" OnClick="AnteriorButton_Click">
                        <asp:label ID="Label4" Text="Anterior" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="NextButton" runat="server" OnClick="SiguienteButton_Click">
                        <asp:label ID="Label5" Text="Siguiente" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="NextFastButton" runat="server" OnClick="SiguienteRapidoButton_Click">
                        <asp:Label ID="Label6" Text="+5 Páginas" runat="server" />
                    </asp:LinkButton><asp:LinkButton CssClass="button" ID="LastButton" runat="server" OnClick="UltimoButton_Click">
                        <asp:label ID="Label7" Text="Ultimo" runat="server" />
                    </asp:LinkButton></div><asp:HiddenField ID="ActivePageHF" runat="server" Value="0" />
                <asp:HiddenField ID="TotalFilasHF" runat="server" Value="0" />
                <asp:HiddenField ID="PrimeraFilaCargadaHF" runat="server" Value="-1" />
                <asp:HiddenField ID="UltimaFilaCargadaHF" runat="server" Value="-1" />
            <asp:HiddenField ID="ClienteIdHF" runat="server" />
        </div>
    </div>
</asp:Content>




