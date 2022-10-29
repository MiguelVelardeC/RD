
<%@ Page Title="Reporte de Siniestralidad - Cliente" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteSiniestralidadCliente.aspx.cs" Inherits="Reportes_ReporteSiniestralidadCliente" %>
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
                <span id="ReporteSiniestralidadTitle" runat="server" class="title">Lista de Clientes - Reporte Siniestralidad</span>
            </div>
            <div class="columnContent">
                <asp:Panel id="AdminPanel" runat="server" CssClass="PanelAdmin" style="font-size: 12px;" DefaultButton="boton"> 
                    <a id="PanelButton" style="text-decoration: none;">
                        <h3 class="sectionTitle" style="background: #e1dddd;">
                           <span style="margin-left:30px;">FILTROS DE ADMINISTRACION</span>
                        </h3>
                    </a>
                    <div id="Contents" style="padding:1em 0.5em;">                   
                        CÓDIGO:
                        <telerik:RadTextBox ID="CodigoText" Text="" EmptyMessage="Código del Cliente" runat="server" Width="180px" />
                        NOMBRE:
                        <telerik:RadTextBox ID="NombreText" Text="" EmptyMessage="Nombre del Cliente" runat="server" Width="180px" />
                        RANGO DE FECHAS: 
                        <telerik:RadDatePicker ID="FechaInicioPicker" runat="server" DateInput-EmptyMessage="Fecha Inicial" Width="120px"></telerik:RadDatePicker>
                        <telerik:RadDatePicker ID="FechaFinPicker" runat="server" DateInput-EmptyMessage="Fecha Final" Width="120px"></telerik:RadDatePicker> 
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
                    MasterTableView-DataKeyNames="ClienteId">

                   <MasterTableView>
                       
                        <NoRecordsTemplate>
                            <div style="text-align: center;">No hay Reportes Siniestralidad registrados</div>
                        </NoRecordsTemplate>                        
                        
                        <Columns>
                          <telerik:GridTemplateColumn  DataField="ClienteId" Visible="true" HeaderText="VER" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" ItemStyle-Width="24px" HeaderStyle-Width="80px" HeaderStyle-Font-Size="12px">
                            <ItemTemplate>
                                <asp:ImageButton ID="DetailsImageButton" runat="server"
                                    ImageUrl="~/Images/Neutral/select.png"
                                    CommandArgument='<%# Eval("ClienteId") +";"
                                         + Eval("CodigoCliente") +";" 
                                         + Eval("NombreJuridico") +";"
                                         + Eval("Nit") +";"
                                         + Eval("Direccion")%> '
                                    Width="28px" CommandName="Select" 
                                    ToolTip="VER"></asp:ImageButton>
                            </ItemTemplate>
                          </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn  DataField="CodigoCliente" Visible="true"
                                HeaderText="Código" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" > </telerik:GridBoundColumn> 
                             <telerik:GridBoundColumn  DataField="NombreJuridico" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true"  HeaderStyle-Font-Size="12px"
                                HeaderText="Cliente" />
                             <telerik:GridBoundColumn  DataField="Nit" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="NIT" />
                             <telerik:GridBoundColumn  DataField="Direccion" Visible="true" HeaderStyle-ForeColor="#3399ff" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                HeaderText="Dirección" />                             
                             <telerik:GridBoundColumn DataField="ClienteId" HeaderText="ClienteId" Visible="false" />                        
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




